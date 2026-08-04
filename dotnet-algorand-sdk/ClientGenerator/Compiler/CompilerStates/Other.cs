using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class Other : CompilerState
    {
        internal Other(CompilerState parent) : base(parent) { }

        internal override void ReportDiagnostic(Action<Diagnostic> reportDiagnostic, Diagnostic diagnostic)
        {
            //do nothing
        }
        internal override CompilerState EnterGoto()
        {
            //do nothing
            return this;
        }
        internal override void YieldExpressionResult()
        {
            //do nothing
        }
        internal override CompilerState EnterIfStatementScope()
        {
            //do nothing
            return this;
        }
        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            //do nothing
            return this;
        }



        internal override void Duplicate()
        {
            //do nothing
        }
        internal override void UnaryOperator(TealTypeUtils.UnaryModifier modifier, string typeName)
        {
            //do nothing
        }
        internal override void BuildValueFromStack(ISymbol construct)
        {
            //do nothing
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            //Do nothing
        }
        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            //Do nothing
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            //Do nothing
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            //Do nothing
        }

        internal override CompilerState EnterClassScope()
        {
            //Do nothing
            return this;
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            //Do nothing
            return this;
        }
        internal override CompilerState EnterLoopScope()
        {
            //Do nothing
            return this;
        }
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            //Do nothing
            return this;
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {
            //Do nothing
            return this;
        }

        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {
            //Do nothing
            return this;
        }

        internal override CompilerState EnterMethodScope(ISymbol method, SemanticModel semanticModel)
        {
            //Do nothing
            return this;
        }

        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            //Do nothing
            return this;
        }
        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            //Do nothing
            return this;
        }


        internal override CompilerState EnterSmartContractScope(ISymbol contract)
        {
            //TODO - enter smart contract scope to support contract scope in a parent class
            return this;
        }
        internal override CompilerState EnterSmartSignatureScope(ISymbol contract)
        {
            //TODO see above
            return this;
        }


        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            //Do nothing
        }
        internal override CompilerState Leave()
        {
            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            //Do nothing
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            //Do nothing
        }
        internal override void AddLiteralNumeric(ulong value)
        {
            //Do nothing
        }
        internal override void AddLiteralString(string value)
        {
            //Do nothing
        }
        internal override void AddLiteralByteConstant(byte[] value)
        {
            //Do nothing
        }
    }
}
