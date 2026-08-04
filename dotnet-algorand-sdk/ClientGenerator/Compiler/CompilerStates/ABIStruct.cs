using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class ABIStruct : CompilerState
    {
        INamedTypeSymbol structSymbol;
        TealSharpSyntaxWalker compiler;

        public ABIStruct(TealSharpSyntaxWalker walker, CompilerState parent, INamedTypeSymbol structSymbol) : base(parent)
        {
            this.structSymbol = structSymbol;
            compiler = walker;
            compiler.AddStruct(structSymbol);
        }

        internal override void RegisterStructAccessor(ISymbol fieldSymbol, ABIEncodingType encoding, int byteWidth)
        {
            compiler.AddStructAccessor(structSymbol, fieldSymbol, encoding, byteWidth);
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

        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            //?
        }

        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterLoopScope()
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterMethodScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterSmartContractScope(ISymbol contract)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override CompilerState EnterSmartSignatureScope(ISymbol contract)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            //?
        }

        internal override CompilerState Leave()
        {
            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E059");
        }

        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E059");
        }
    }
}
