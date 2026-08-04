using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartContract : SmartProgramContainer
    {

     

        internal SmartContract(InitialState parent, ISymbol contract) : base(parent)
        {
            Scope = Scope.NewChildScope();
            var code = new SmartContractCode(Scope);
            code.Name=contract.Name;
            Code.AddChildCode(code);
            Code = code;
            
            
        }
        internal override void UnaryOperator(TealTypeUtils.UnaryModifier modifier, string typeName)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }


        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {

            AddGlobalVariable(symbol, valueType);
        }

     

        internal override void Duplicate()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddLocalVariable(symbol, valueType);
        }

        internal override void DeclareScratchVariable(ISymbol symbol,   Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
            throw new WarningDiagnosticException("W000");
        }

        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E001");
        }

        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterCodeBlockScope()
        {

            throw new ErrorDiagnosticException("E006");
        }

        internal override CompilerState EnterLoopScope()
        {
            throw new ErrorDiagnosticException("E006");
        }
        internal override CompilerState EnterABIMethodScope(IMethodSymbol method, Core.OnCompleteType callType, string optionalLabel, SemanticModel semanticModel)
        {
            return new SmartContractABIMethod(this, method, callType, optionalLabel, semanticModel);
        }
        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {

            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterMethodScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E010");
        }

        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            return new SmartContractProgram(this,methodSymbol);
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
            throw new ErrorDiagnosticException("E009");
        }
        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void StoreToVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E011");
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
    }
}
