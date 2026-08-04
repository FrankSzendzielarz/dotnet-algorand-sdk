using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class ByteArrayInitialiser : CompilerState
    {


        internal ByteArrayInitialiser(CompilerState parent) : base(parent)
        {

            Code.byte_string_literal(""); //pre-initialise with empty slice
        }



        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
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
            throw new ErrorDiagnosticException("E006");
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

            Code.itob(); //convert byte stack val to bytearray
            Code.extract(7, 0);
            Code.concat(); //

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
