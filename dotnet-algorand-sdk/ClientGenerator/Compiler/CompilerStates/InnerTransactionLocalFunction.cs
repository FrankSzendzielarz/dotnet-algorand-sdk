using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class InnerTransactionLocalFunction : LocalFunction
    {

        private bool itd = false;
        private bool innerTransactionDeclared { get { return itd; } set { itd = value; if (value) Code.SuppressInnerTransactions(); } }
        internal InnerTransactionLocalFunction(CompilerState parent, IMethodSymbol func, SemanticModel semanticModel) : base(parent,func,semanticModel)
        {
        
            if (FirstParent<InnerTransactionLocalFunction>() != null)
            {
                throw new ErrorDiagnosticException("E042");
            }

        }
        internal override CompilerState EnterTupleExpression(TupleExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            if (!innerTransactionDeclared)
            {
                return CheckIfTupleIsGroupInnerTransactionAndEnter(node, semanticModel, substate);
            }
            else
            {
                throw new ErrorDiagnosticException("E044");
            }
        }
        internal override void DeclareInnerTransactionVariable(ISymbol symbol, SemanticModel model)
        {
            if (!innerTransactionDeclared)
            {
                Scope.AddInnerTransactionVariable(symbol);
                innerTransactionDeclared = true;
            }
            else
            {
                throw new ErrorDiagnosticException("E038");
            }
        }
        internal override void DeclareValueTupleVariable(ISymbol symbol, SemanticModel model)
        {
            if (!(symbol is ILocalSymbol ls)) throw new ErrorDiagnosticException("E009");

            IsGroupInnerTransaction((INamedTypeSymbol)ls.Type);

            if (!innerTransactionDeclared)
            {
                Scope.AddInnerTransactionVariable(symbol);
                innerTransactionDeclared = true;
            }
            else
            {
                throw new ErrorDiagnosticException("E038");
            }

        }
        internal override CompilerState EnterVariableMethodInvocation(ISymbol referenceSymbol, InvocationExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
           
            var v = Scope.FindVariable(referenceSymbol);
            if (v.HasValue ) 
            {
                if (v.Value.Item1 is ApplicationRefVariable)
                {
                   
                    return new SmartContractReferenceMethodCall(substate);
                }
                else
                {
                    return this;
                }
            }
            else
            {
                if (Parent != null) return Parent.EnterVariableMethodInvocation(referenceSymbol, node, semanticModel,substate);
                else
                    throw new ErrorDiagnosticException("E039");
            }
        }


        private int findTransactionIndex(INamedTypeSymbol namedTypeSymbol,int index,string prefix,string rhs)
        {
            

            if (!String.IsNullOrWhiteSpace(prefix)) prefix = prefix + ".";
            foreach(var tupleElement in namedTypeSymbol.TupleElements)
            {
                string teName=prefix+tupleElement.Name;
                if (teName == rhs) return index;
                INamedTypeSymbol fieldType = (INamedTypeSymbol)tupleElement.Type;
                if (fieldType.IsTupleType)
                {
                    var i = findTransactionIndex(fieldType, index, teName, rhs);
                    if (i != -1) return i;
                }
                index++;

            }
            return -1;
            
            
               
                

            
        }


        internal override void ObjectCreation(ObjectCreationExpressionSyntax ctor, List<IParameterSymbol> nulledOptionals, SemanticModel semanticModel = null, Dictionary<string, string> literals = null)
        {
            if (!innerTransactionDeclared)
            {

                var invocationSymbol = semanticModel.GetSymbolInfo(ctor).Symbol as IMethodSymbol;
                if (!TealTypeUtils.IsInnerTransaction(invocationSymbol.ContainingType))
                {
                    throw new ErrorDiagnosticException("E036");
                }
                Code.itxn_begin();
                string txType="";
                switch (invocationSymbol.ContainingType.Name)
                {
                    case "Payment": txType="pay"; break;
                    case "KeyRegistration": txType = "keyreg"; break;
                    case "AssetTransfer":  txType = "axfer";  break;
                    case "AssetFreeze": txType = "afrz"; break;
                    case "AssetConfiguration": txType = "acfg"; break;
                    case "AssetClawback": txType = "axfer"; break;
                    case "AssetAccept": txType = "axfer"; break;
                    case "AppCall": txType = "appl"; break;
                    default: throw new Exception("Unknown tx type in inner.");
                }
                Code.int_literal_constant(txType);
                Code.itxn_field("TypeEnum");

                foreach (var arg in ctor.ArgumentList.Arguments.Reverse())//reverse for stack
                {
                    var ctorParm = (semanticModel.GetOperation(arg) as IArgumentOperation).Parameter;
                    if (nulledOptionals.Contains(ctorParm))
                    {
                        continue; //skip fields that aren't set
                    }
                    var parameterName = ctorParm.Name;
                    parameterName = char.ToUpper(parameterName.First()) + parameterName.Substring(1); //capitalise first letter
                    
                    Code.itxn_field(parameterName);

                }
                Code.itxn_submit();
               
            }
            else
            {
                throw new ErrorDiagnosticException("E044");
            }

           
        }



        internal override void LoadFromGroupInnerTransaction(ISymbol referenceSymbol, string rhs, Core.StorageType storageType)
        {
            var symbol = referenceSymbol as ILocalSymbol;
            if (symbol != null)
            {
                IsGroupInnerTransaction(symbol.Type as INamedTypeSymbol);

                string txnToFind = rhs.Substring(0, rhs.LastIndexOf('.'));

                int index = findTransactionIndex(symbol.Type as INamedTypeSymbol, 0, "", txnToFind);

                Code.gitxn((byte)index, rhs.Substring(rhs.LastIndexOf('.') + 1) );

            }
            else
            {
                throw new ErrorDiagnosticException("E020");
            }
        }

     


        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            return new ByteArrayInitialiser(this);
        }
        internal override CompilerState EnterGoto()
        {
            return new Goto(this);
        }
        internal override CompilerState EnterIfStatementScope()
        {
            return new IfStatement(this);
        }
        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
        }
        internal override void YieldExpressionResult()
        {
            //Do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this, i);
        }
        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            return new CodeBlock(this);
        }

        internal override CompilerState EnterLoopScope()
        {
            return new Loop(this);
        }


        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {

            throw new ErrorDiagnosticException("E035");
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
            throw new ErrorDiagnosticException("E009");
        }

        internal override void Return(bool hasReturnValue)
        {
            Code.retsub();
        }

        internal override CompilerState Leave()
        {
            //handle out vars by pushing them onto the stack. they are then loaded by the calling scope
            List<ScratchVariable> scratchVariables = new List<ScratchVariable>();
            foreach (var param in localFunctionSymbol.Parameters)
            {
                if (param.RefKind == RefKind.Out)
                {
                    ReadFromVariable(param);
                }
            }



            //ret out
            Code.retsub();
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

        internal override CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {
            return new OutArgument(this);
        }

    }
}
