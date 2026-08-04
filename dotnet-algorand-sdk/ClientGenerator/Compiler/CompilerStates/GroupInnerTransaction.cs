using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class GroupInnerTransaction : CompilerState
    {
        private bool firstTransaction = true;
        protected GroupInnerTransaction root;
        internal int transactionCounter = 0;
        private List<(int transactionIndex, ArgumentSyntax syntax, SemanticModel model)> returnValues = new List<(int transactionIndex, ArgumentSyntax syntax, SemanticModel model)>();


        protected bool isRootGroupInner(CompilerState cs)
        {
            return (FirstParent<GroupInnerTransaction>() == null);
        }

        internal GroupInnerTransaction(CompilerState parent) : base(parent)
        {
            if (isRootGroupInner(this))
            {
                Code.itxn_begin();
                root = this;
            }
            else
            {
                root = FirstParent<GroupInnerTransaction>();
            }
        }
        internal void NextTransaction()
        {
            if (!firstTransaction)
            {
                Code.itxn_next();
            }
            else
            {
                firstTransaction = false;
            }
            root.transactionCounter++;

        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
        }

        internal override CompilerState EnterTupleExpression(TupleExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            return CheckIfTupleIsGroupInnerTransactionAndEnter(node, semanticModel, substate);
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            //TODO only allow smart contract reference calls
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal void RegisterReturnValue(ArgumentSyntax node, SemanticModel model)
        {
            this.returnValues.Add((root.transactionCounter, node, model));
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override CompilerState EnterVariableMethodInvocation(ISymbol referenceSymbol, InvocationExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            var v = Scope.FindVariable(referenceSymbol);
            if (v.HasValue)
            {
                if (v.Value.Item1 is ApplicationRefVariable)
                {
                    transactionCounter++;
                    return new SmartContractReferenceMethodCall(substate);
                }
                else
                {
                    return this;
                }
            }
            else
            {
                if (Parent != null) return Parent.EnterVariableMethodInvocation(referenceSymbol, node, semanticModel, substate);
                else
                    throw new ErrorDiagnosticException("E039");
            }
        }

        internal override void ObjectCreation(ObjectCreationExpressionSyntax ctor, List<IParameterSymbol> nulledOptionals, SemanticModel semanticModel = null, Dictionary<string, string> literals = null)
        {
            //TODO - literals, but not urgent
            if (isRootGroupInner(this))
            {


                NextTransaction();



                var invocationSymbol = semanticModel.GetSymbolInfo(ctor).Symbol as IMethodSymbol;
                if (!TealTypeUtils.IsInnerTransaction(invocationSymbol.ContainingType))
                {
                    throw new ErrorDiagnosticException("E036");
                }
                string txType = "";
                switch (invocationSymbol.ContainingType.Name)
                {
                    case "Payment": txType = "pay"; break;
                    case "KeyRegistration": txType = "keyreg"; break;
                    case "AssetTransfer": txType = "axfer"; break;
                    case "AssetFreeze": txType = "afrz"; break;
                    case "AssetConfiguration": txType = "acfg"; break;
                    case "AssetClawback": txType = "axfer"; break;
                    case "AssetAccept": txType = "axfer"; break;
                    case "AppCall": txType = "appl"; break;
                    default: throw new Exception("Unknown tx type in inner.");
                }
                Code.int_literal_constant(txType);
                Code.itxn_field("TypeEnum");
                foreach (var arg in ctor.ArgumentList.Arguments.Reverse())//reverse for stack
                {
                    var ctorParm = (semanticModel.GetOperation(arg) as IArgumentOperation).Parameter;
                    if (nulledOptionals.Contains(ctorParm))
                    {
                        continue; //skip fields that aren't set
                    }
                    var parameterName = ctorParm.Name;
                    parameterName = char.ToUpper(parameterName.First()) + parameterName.Substring(1); //capitalise first letter

                    Code.itxn_field(parameterName);

                }
            }
            else
            {
                Parent.ObjectCreation(ctor, nulledOptionals, semanticModel, literals);
            }




        }


        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            throw new ErrorDiagnosticException("E006");
        }
        internal override CompilerState EnterLoopScope()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E006");
        }
        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E006");
        }

      
        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            throw new ErrorDiagnosticException("E009");
        }


        internal override CompilerState Leave()
        {
            if (isRootGroupInner(this))
            {
                Code.itxn_submit();

                var dontStripJunk = Code.ReserveLabel();

                //now handle any return values, if this or a child group is SmartContractReferenceMethodCall
                foreach (var retVal in returnValues)
                {

                    Code.gitxn((byte)retVal.transactionIndex, "LastLog");
                    Code.dup();
                    Code.len();
                    Code.int_literal_constant(4);
                    Code.less_than();
                    Code.bnz(dontStripJunk);
                    Code.dup();
                    Code.substring(0, 4);
                    Code.byte_string_literal("0x151f7c75");  //Arc4 compatibility
                    Code.equals();
                    Code.bz(dontStripJunk);
                    Code.extract(4, 0); //get the remainder of the return value
                    Code.AddLabel(dontStripJunk);
                    // check the type and btoi if necessary
                    var typeInfo = retVal.model.GetTypeInfo(retVal.syntax.Expression);
                    if (TealTypeUtils.IsIntegral(typeInfo)) Code.btoi();

                    var operandSymbol = retVal.model.GetSymbolInfo(retVal.syntax.Expression).Symbol;
                    StoreToVariable(operandSymbol);
                }
            }


            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            //TODO remove/prevent as necessary
            ReadVariableCode(symbol);
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            //TODO remove/prevent as necessary
            StoreVariableCode(symbol);
        }

        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void AddLiteralByteConstant(byte[] value)
        {

            throw new ErrorDiagnosticException("E009");
        }
        internal override void AddLiteralNumeric(ulong value)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void AddLiteralString(string value)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            ReadVariableCode(symbol);
        }
    }
}
