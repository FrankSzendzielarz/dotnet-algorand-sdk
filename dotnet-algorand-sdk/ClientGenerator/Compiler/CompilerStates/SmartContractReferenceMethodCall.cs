using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartContractReferenceMethodCall : GroupInnerTransaction
    {






        internal SmartContractReferenceMethodCall(CompilerState parent) : base(parent)
        {

        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this, i);
        }

        internal override CompilerState EnterTupleExpression(TupleExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            return CheckIfTupleIsGroupInnerTransactionAndEnter(node, semanticModel, substate);
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

        internal override CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {

            root.RegisterReturnValue(node, model);
            return new OutArgument(this);
        }

        internal override void InvokeFromReferencedVariable(ISymbol referenceSymbol, SemanticModel model, InvocationExpressionSyntax node, string name, List<IParameterSymbol> nulledOptionals = null, Dictionary<string, string> literals = null)
        {
            var referencedMethod = model.GetSymbolInfo(node).Symbol;

            if (TealTypeUtils.GetABIMethodDetails(referencedMethod, out Core.OnCompleteType callType, out string selector))
            {

                NextTransaction();

                Code.int_literal_constant("appl");
                Code.itxn_field("TypeEnum");
                ReadVariableCode(referenceSymbol);
                Code.itxn_field("ApplicationID");
                Code.int_literal_constant(callType.ToString());
                Code.itxn_field("OnCompletion");
                Code.byte_string_literal(selector);
                Code.itxn_field("ApplicationArgs"); //selector always arg0


                int outs = 0;


                foreach (var arg in node.ArgumentList.Arguments)
                {
                    var typeSymbol = model.GetTypeInfo(arg.Expression).Type;


                    if (arg.RefKindKeyword.Text == "out")
                    {
                        if (outs++ > 0) throw new ErrorDiagnosticException("E040");
                        continue;
                    }


                    if (AccountRefVariable.IsAccountRef(typeSymbol))
                    {
                        Code.itxn_field("Accounts");
                        continue;
                    }

                    if (AssetRefVariable.IsAssetRef(typeSymbol))
                    {
                        Code.itxn_field("Assets");
                        continue;
                    }

                    if (ApplicationRefVariable.IsApplicationRef(typeSymbol))
                    {
                        Code.itxn_field("Applications");
                        continue;
                    }

                    if (TealTypeUtils.IsInnerTransaction(typeSymbol))
                    {
                        //do nothing
                        continue;
                    }

                    if (typeSymbol.IsTupleType)
                    {
                        //has to be a group transaction of some type if it's a tuple type
                        IsGroupInnerTransaction(typeSymbol as INamedTypeSymbol);
                        //do nothing
                        continue;
                    }

                    //any other type should is a normal transaction argument 
                    if (TealTypeUtils.IsIntegral(typeSymbol)) { Code.itob(); }
                    Code.itxn_field("ApplicationArgs");

                }
            }
            else
            {
                throw new ErrorDiagnosticException("E041");
            }


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
