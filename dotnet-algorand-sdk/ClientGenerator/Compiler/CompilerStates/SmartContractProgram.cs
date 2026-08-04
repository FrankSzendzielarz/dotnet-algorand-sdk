using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartContractProgram : CompilerState
    {


        internal SmartContractProgram(SmartProgramContainer parent, IMethodSymbol func) : base(parent)
        {

            var code = new SmartContractProgramCode(func.Name);
            Code.AddChildCode(code);
            Code = code;
            Scope = Scope.NewChildScope();


            //this is just for the app call parameter to the smartcontract program
            foreach (var param in func
                                   .Parameters
                                   .Where(param => param.RefKind == RefKind.In && TransactionRefVariable.IsTxRef(param.Type))
                   )

            {
                Scope.AddTransactionRefVariable(param);
            }
        }


        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }


        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }
        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
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
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            return new ByteArrayInitialiser(this);
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {
            return new LocalFunction(this, method as IMethodSymbol, semanticModel);
        }
        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new InnerTransactionLocalFunction(this, method as IMethodSymbol, semanticModel);
        }
      



        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void YieldExpressionResult()
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
       

        internal override void Return(bool hasReturnValue)
        {


            Code.ret();

        }

        internal override CompilerState Leave()
        {




            return Parent;
        }
        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
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
