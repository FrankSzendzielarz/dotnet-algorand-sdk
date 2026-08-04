using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class OutArgument : CompilerState
    {
        internal OutArgument(CompilerState parent) : base(parent) { }


        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new OutArgument(this);

        }
        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            //TODO - can this be done?
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            //TODO - can this be done?
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
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
            throw new ErrorDiagnosticException("E009");
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
            //Ignore
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
        
    }
}
