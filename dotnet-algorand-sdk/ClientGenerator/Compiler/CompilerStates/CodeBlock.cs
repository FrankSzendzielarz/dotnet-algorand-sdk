using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class CodeBlock : CompilerState
    {
        internal CodeBlock(CompilerState parent) : base(parent)
        {
            Scope = Scope.NewChildScope();
        }


        internal override void YieldExpressionResult()
        {
            //do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool isVoid)
        {
            return new ExpressionEvaluation(this,isVoid);
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
            AddScratchVariable(symbol, valueType);
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
    }
}
