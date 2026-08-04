using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class Loop : CompilerState
    {

        private string loopStartLabel;
        private string loopExitLabel;
        private string loopContinueLabel;

        internal Loop(CompilerState parent) : base(parent) {
            Scope = Scope.NewChildScope(); //loops need a new scope immediately, as variable declarations in for loops are scoped to the for and not outside it
            loopStartLabel = Code.ReserveLabel();
            loopExitLabel = Code.ReserveLabel();
            loopContinueLabel= Code.ReserveLabel();
        }


        internal override void YieldExpressionResult()
        {
            //do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this, i);
        }


        internal override CompilerState EnterGoto()
        {
            return new Goto(this);
        }


        internal override CompilerState EnterIfStatementScope()
        {
            return new IfStatement(this);
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

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol,valueType);
        }
        internal override void DeclareValueTupleVariable(ISymbol symbol, SemanticModel model)
        {
            if (Parent != null)
            {
                Parent.DeclareValueTupleVariable(symbol, model);

            }
            else
            {
                throw new ErrorDiagnosticException("E008");
            }
        }
        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E007");
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
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState Leave()
        {
            Code.b(loopStartLabel); //loop
            Code.AddLabel(loopExitLabel);
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
        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
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

        internal override void LoopCondition()
        {
            Code.bz(loopExitLabel);
           
        }

        internal override void LoopStatement()
        {
            Code.AddLabel(loopStartLabel);
        }

        internal override void LoopContinuePoint()
        {
            Code.AddLabel(loopContinueLabel);
        }

        internal override void Break()
        {
            Code.b(loopExitLabel);
        }

        internal override void Continue()
        {
            Code.b(loopContinueLabel);
        }
    }
}
