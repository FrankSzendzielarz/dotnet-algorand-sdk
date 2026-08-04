using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartSignature : SmartProgramContainer
    {
        internal SmartSignature(InitialState parent, ISymbol sig) : base(parent) {

            Scope = Scope.NewChildScope();
            var code = new SmartSignatureCode(Scope);
            code.Name = sig.Name;
            Code.AddChildCode(code);
            Code = code;
            
        }



        internal override CompilerState EnterABIMethodScope(IMethodSymbol method, Core.OnCompleteType callType, string optionalLabel, SemanticModel semanticModel)
        {
            return new SmartSignatureABIMethod(this, method,  optionalLabel, semanticModel);
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void Duplicate()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType variableType)
        {
            AddGlobalVariable(symbol, variableType);
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType variableType)
        {
            AddLocalVariable(symbol, variableType);
        }

        internal override void DeclareScratchVariable(ISymbol symbol,   Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
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
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            return new SmartSignatureProgram(this, methodSymbol);
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

        internal override void StoreToVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E011");
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
            throw new ErrorDiagnosticException("E009");
        }
    }
}
