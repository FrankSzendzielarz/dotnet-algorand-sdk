using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartContractLibrary : CompilerState
    {
        // SourceGenerator first compiles each SmartContractLibrary into a set of SmartContractLibraryMethods, one for each method,
        // that the compiler's CompilationGroup maintains. 
        //
        // When a method call is found in one of these Scopes, to another method in a SmartContractLibrary,
        // then the SmartContractLibraryMethod, or one of its child codebuilders, has a dependent SmartContractLibraryMethod registered on it.
        //
        // During compilation, in any CompilerState, when a method call is found to a library method, the method is treated as being in a root scope. 
        //  ..all stack variables are pushed from the current scope.
        //
        // During code generation, all dependent SmartContractLibraryMethods are added into the output code as subroutines.

        internal SmartContractLibrary(InitialState parent) : base(parent) { }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            throw new ErrorDiagnosticException("E009");
        }


        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E069");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E069");
        }
        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E069");
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
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
        internal override CompilerState EnterMethodScope(ISymbol method, SemanticModel semanticModel)
        {
            if (method is IMethodSymbol methodSymbol)
            {
                if (methodSymbol.IsStatic) { 
                    return new SmartContractLibraryFunction(this, method as IMethodSymbol, semanticModel);
                }
                else
                {
                    throw new ErrorDiagnosticException("E070");
                }
            }
            else
            {
                throw new ErrorDiagnosticException("E009");
            }
            
        }

        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void YieldExpressionResult()
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
      

        internal override void Return(bool hasReturnValue)
        {
            Code.ret();
        }

        internal override CompilerState Leave()
        {

            return Parent;
        }
        internal override void BuildValueFromStack(ISymbol construct)
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



    }
}
