using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Optimisers;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    /// <summary>
    /// The InitialState when the compiler is about to process a CompilationGroup.
    /// </summary>
    internal class InitialState : CompilerState
    {
        internal InitialState() : base(null) { }




        internal override void Duplicate()
        {
            throw new ErrorDiagnosticException("E009");
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


        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            throw new ErrorDiagnosticException("E009");
        }


        internal override void YieldExpressionResult()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterClassScope()
        {
            return new Other(this);
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            throw new ErrorDiagnosticException("E006");
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

        internal override CompilerState EnterSmartContractScope(ISymbol contract)
        {
            return new SmartContract(this, contract);
        }

        internal override CompilerState EnterSmartSignatureScope(ISymbol logicsig)
        {
            return new SmartSignature(this, logicsig);
        }

        internal override CompilerState EnterSmartContractLibraryScope()
        {
            return new SmartContractLibrary(this);
        }

        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState Leave()
        {
            return null;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
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

        internal List<ContractDeclaration> GetContractDeclarations(List<IOptimiser> optimisers)
        {
            
            

            var declarations = Code.ContractDeclarations;
            foreach (var declaration in declarations)
            {
                try
                {
                    declaration.code.OptimiseCode(optimisers);
                }
                catch
                {
                    throw new ErrorDiagnosticException("E045");
                }

                var codeCostAndDependencies = declaration.code.GetApprovalProgramCode(CompilationGroup, new List<CompiledCodeModel.CodeBuilder>());
                

                declaration.Code = codeCostAndDependencies.code;
                declaration.ApprovalProgramCost = codeCostAndDependencies.cost;


                var csCodeAndCost = declaration.code.GetClearStateCode(CompilationGroup);
                declaration.ClearState = csCodeAndCost.code;
                declaration.ClearStateProgramCost = csCodeAndCost.cost;

            }

            return declarations;



        }

        internal List<SmartSignatureDeclaration> GetSmartSignatureDeclarations(List<IOptimiser> optimisers)
        {
            var declarations = Code.SigDeclarations;
            foreach (var declaration in declarations)
            {
                try
                {
                    declaration.code.OptimiseCode(optimisers);
                }
                catch
                {
                    throw new ErrorDiagnosticException("E045");
                }

                var codeAndCost = declaration.code.GetApprovalProgramCode(CompilationGroup,new List<CompiledCodeModel.CodeBuilder>());
                declaration.Code = codeAndCost.code;
                declaration.ProgramCost = codeAndCost.cost;

            }

            return declarations;
        }
    }
}
