using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Core;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class IfStatement : CompilerState
    {
        private string endIfLabel;
        internal IfStatement(CompilerState parent) : base(parent)
        {
            endIfLabel = Code.ReserveLabel();
            Code.bz(endIfLabel);
        }


        internal override void YieldExpressionResult()
        {
            //do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
        }
        internal override CompilerState EnterGoto()
        {
            return new Goto(this);
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
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
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {

            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
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

      


        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void ElseClause()
        {
            string oldEndIfLabel = endIfLabel;
            endIfLabel = Code.ReserveLabel();
            Code.b(endIfLabel);
            Code.AddLabel(oldEndIfLabel);
        }


        internal override CompilerState Leave()
        {
            Code.AddLabel(endIfLabel);
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
        internal override CompilerState EnterIfStatementScope()
        {
            return new IfStatement(this);
        }
    }
}
