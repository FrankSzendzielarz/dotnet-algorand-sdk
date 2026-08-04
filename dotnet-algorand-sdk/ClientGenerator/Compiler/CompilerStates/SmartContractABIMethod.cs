using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartContractABIMethod : CompilerState
    {
        private IMethodSymbol functionSymbol;
        private string exit;
        internal SmartContractABIMethod(CompilerState parent, IMethodSymbol func, Core.OnCompleteType callType, string optionalLabel, SemanticModel semanticModel) : base(parent)
        {

            //New Code target and new Scope
            var code = new SmartContractMethodCode();
            if (string.IsNullOrWhiteSpace(optionalLabel))
            {
                code.Name = $"0x{func.GetMethodSelector(semanticModel)}"; 
            }
            else
            {
                code.Name = $"\"\"{optionalLabel}\"\"";
            }

            code.Label = func.Name;
            code.OnCompletion = callType.ToString();
            Code.AddChildCode(code);
            Code = code;

            Scope = Scope.NewChildScope();
            Scope.Label = func;
            functionSymbol = func;

            Code.AddEmptyLine();
            Code.AddLabel(func.Name);

            int assetIndex = 0;
            int accountIndex = 1;
            int applicationIndex = 1;
            int argumentIndex = 1;
            int transactionGroupIndex = 0;

            //deal with transaction references separately: reversed because the index should be a NEGATIVE offset relative to the current transaction.
            var lastTxnParm = func
                .Parameters
                .Where(param => TransactionRefVariable.IsTxRef(param.Type))
                .LastOrDefault();
            if (lastTxnParm != null && lastTxnParm.Type.ToString() != "AVM.ClientGenerator.Core.AppCallTransactionReference")
            {
                throw new ErrorDiagnosticException("E034");
            }
            foreach (var param in func
                                    .Parameters
                                    .Reverse()
                                    .Where(param => TransactionRefVariable.IsTxRef(param.Type))
                    )

            {
                Code.int_literal_constant(transactionGroupIndex.ToString());
                transactionGroupIndex++;
                Scope.AddTransactionRefVariable(param);

                Scope.StoreVariable(param, Code);
            }

            foreach (var param in func.Parameters)
            {
                if (param.RefKind == RefKind.None || param.RefKind == RefKind.In)
                {


                    if (AssetRefVariable.IsAssetRef(param.Type))
                    {
                        Code.txn_arrayfield("Assets", assetIndex++);
                        Scope.AddAssetRefVariable(param);
                        Scope.StoreVariable(param, Code);
                    }
                    else
                    if (AccountRefVariable.IsAccountRef(param.Type))
                    {
                        Code.txn_arrayfield("Accounts", accountIndex++);
                        Scope.AddAccountRefVariable(param);
                        Scope.StoreVariable(param, Code);

                    }
                    else
                    if (ApplicationRefVariable.IsApplicationRef(param.Type))
                    {
                        Code.txn_arrayfield("Applications", applicationIndex++);
                        Scope.AddApplicationRefVariable(param);
                        Scope.StoreVariable(param, Code);
                    }
                    else
                    if (TransactionRefVariable.IsTxRef(param.Type))
                    {
                        //already handled
                    }
                    else
                    {
                        //Argument variable - need to now define a normal scope scratch var but pre-load it with the transaction argument
                        Core.VariableType machineValueType = TealTypeUtils.DetermineType(semanticModel, (param.DeclaringSyntaxReferences.First().GetSyntax() as ParameterSyntax).Type);

                        var sv = AddScratchVariable(param, machineValueType);
                        Code.txna("ApplicationArgs", (byte)(argumentIndex++));
                        if (machineValueType == Core.VariableType.UInt64) Code.btoi();
                        sv.AddSaveCode(Code, Scope);
                    }
                }
                else
                {
                    throw new ErrorDiagnosticException("E018");
                }

            }

            exit = Code.ReserveLabel();


        }


        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            return new ByteArrayInitialiser(this);
        }
        internal override CompilerState EnterGoto()
        {
            return new Goto(this);
        }
        internal override CompilerState EnterIfStatementScope()
        {
            return new IfStatement(this);
        }
        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
        }
        internal override void YieldExpressionResult()
        {
            //Do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
        }
        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            return new CodeBlock(this);
        }
        internal override CompilerState EnterLoopScope()
        {
            return new Loop(this);
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new LocalFunction(this, method as IMethodSymbol, semanticModel);
        }

        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new InnerTransactionLocalFunction(this, method as IMethodSymbol, semanticModel);
        }

       

        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }


      


        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            //TODO - this line just allows the return type as an identifier name syntax as valid syntax when
            //       processing the sub syntax tree of a method declaration. Consider if any other checks 
            //       are needed here.       
            if (!symbol.Equals(functionSymbol.ReturnType, SymbolEqualityComparer.Default))
                throw new ErrorDiagnosticException("E009");
        }

        internal override void Return(bool hasReturnValue)
        {
            if (hasReturnValue)
            {
                if (functionSymbol.ReturnType.IsIntegral())
                {
                    Code.itob();
                }
                Code.log();
            }
            Code.b(exit);

        }

        internal override CompilerState Leave()
        {
            Code.AddLabel(exit);
            Code.int_literal_constant(1);
            Code.ret();
            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            ReadVariableCode(symbol);
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            StoreVariableCode(symbol);
        }

        internal override void AddLiteralByteConstant(byte[] value)
        {
            PushLiteralByteArrayToStack(value);
        }


        internal override void AddLiteralNumeric(ulong value)
        {
            PushLiteralNumericToStack(value);
        }

        internal override void AddLiteralString(string value)
        {
            PushLiteralStringToStack(value);
        }

        internal override CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {
            return new OutArgument(this);
        }

    }
}
