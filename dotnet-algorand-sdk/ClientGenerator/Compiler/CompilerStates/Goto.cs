using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class Goto : CompilerState
    {
        internal Goto(CompilerState parent) : base(parent)
        {

        }


        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            //do nothing 
            return new Goto(this);
        }
        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterLoopScope()
        {
            throw new ErrorDiagnosticException("E006");
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
            string gotoLabel = Code.GetLabeledStatement(symbol); //
            Code.b(gotoLabel);
        }

        internal override CompilerState Leave()
        {
            return Parent;
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



        internal override CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }
    }
}
