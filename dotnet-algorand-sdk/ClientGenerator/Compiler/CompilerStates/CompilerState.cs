using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Predefineds;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static AVM.ClientGenerator.Compiler.TealTypeUtils;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    /// <summary>
    /// CompilerState is a concept in what can be seen as an 'intermediate representation' of the program.
    /// As the C# syntax walker progresses through the syntax tree, certain elements change the compiler state
    /// from the perspective of TEAL generation. Eg: encountering a Class may do nothing, but encountering a Class
    /// that implements AlgorandSmartContract represents entering a SmartContract compiler state. 
    /// 
    /// The CompilerState handles instructions to generate code or reserve variables. If a request is being made to 
    /// generate code in the wrong state then these are compiler errors.
    /// </summary>
    internal abstract class CompilerState
    {
        /// <summary>
        /// Scope is a concept in the source domain and is used by the compiler to maintain mappings of local variables and scratch space indexes.
        /// </summary>
        protected Scope Scope { get; set; }
        private CodeBuilder code = null;
        /// <summary>
        /// Code is a concept in the output domain and represents a list of instructions, such as Program or Subroutine
        /// CodeBuilder is used to implement this because it is invoked 'lazily' after compilation is complete to
        /// give the optimisers a chance to modify the instructions given here and adjust scratch space positions.
        /// 
        /// </summary>
        protected CodeBuilder Code { get { return code ?? Parent?.Code; } set { code = value; } }

        //TODO - should this be moved into Code?
        private string currentCommentText = "";

        internal bool ExpectingExpressionReturnValue { get; set; } = true;

        /// <summary>
        /// The state is a breadcrumb pointer to the current position in an intermediate representation of the source syntax tree
        /// </summary>
        private CompilerState parent;
        protected CompilerState Parent
        {
            get { return parent; }
            private set
            {
                parent = value;
                Scope = parent?.Scope ?? Scope.NewRootScope(); //inherit scopes by default

            }
        }

        internal CompilationGroup CompilationGroup { get; set; }

        internal T FirstParent<T>() where T : CompilerState
        {
            if (Parent == null) return default(T);
            if (Parent is T) return (T)Parent;

            return Parent.FirstParent<T>();


        }

        internal void SetCommentText(string comment)
        {
            currentCommentText = comment;
        }

        protected void WriteComment()
        {
            Code.AddComment(currentCommentText);
        }

        protected CompilerState(CompilerState parent)
        {
            Parent = parent;

            if (parent == null)
            {
                Code = new CodeBuilder();
            }
            else
            {
                CompilationGroup = parent.CompilationGroup;
            }
        }




        protected ScratchVariable AddScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            return Scope.AddScratchVariable(symbol, valueType);
        }

        protected GlobalVariable AddGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            return Scope.AddGlobalVariable(symbol, valueType);
        }

        protected LocalVariable AddLocalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            return Scope.AddLocalVariable(symbol, valueType);
        }



        protected void ReadVariableCode(ISymbol symbol)
        {
            Scope.ReadVariable(symbol, Code);
        }

        protected void StoreVariableCode(ISymbol symbol)
        {
            Scope.StoreVariable(symbol, Code);
        }



        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private void InvokePredefinedTypeMethod(string methodName, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals)
        {
            var predefineds = new AlgorandPredefineds(Code, Scope, nulledOptionals, literals);
            var type = predefineds.GetType();
            var method = type.GetMethod(methodName);
            if (method != null)
            {
                var defaultParms = method.GetParameters().Select(p => GetDefaultValue(p.ParameterType)).ToArray();
                method.Invoke(predefineds, defaultParms);
            }
        }


        protected void PushLiteralByteArrayToStack(byte[] val)
        {
            Code.byte_literal_constant("0x" + val.ToHex());
        }

        protected void PushLiteralNumericToStack(ulong val)
        {
            Code.int_literal_constant(val);
        }

        protected void PushLiteralStringToStack(string s)
        {
            Code.byte_string_literal(s);
        }
        protected CompilerState CheckIfTupleIsGroupInnerTransactionAndEnter(TupleExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            INamedTypeSymbol namedTypeSymbol = (INamedTypeSymbol)semanticModel.GetTypeInfo(node).Type;

            IsGroupInnerTransaction(namedTypeSymbol);

            return new GroupInnerTransaction(substate);
        }




        protected void IsGroupInnerTransaction(INamedTypeSymbol namedTypeSymbol)
        {

            bool isInvalidTuple(INamedTypeSymbol n)
            {
                return n.TupleElements.Where(a =>
                {
                    INamedTypeSymbol fieldType = (INamedTypeSymbol)a.Type;
                    if (fieldType.IsTupleType)
                    {
                        return isInvalidTuple(fieldType);
                    }
                    else
                        return !IsInnerTransaction(fieldType);

                }
                ).Any();
            }

            if (isInvalidTuple(namedTypeSymbol)) throw new ErrorDiagnosticException("E036");
        }

        private static bool isDirectTypeOf(ITypeSymbol methodContainerType, string type)
        {
            SymbolDisplayFormat format = new SymbolDisplayFormat(
                globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
            );
            return methodContainerType.ToDisplayString(format) == type;
        }

        private static bool isTypeOrSubtypeOf(ITypeSymbol methodContainerType,string type)
        {
            
            while (methodContainerType != null)
            {
                if (isDirectTypeOf(methodContainerType,type ))
                {
                    return true;
                }
                methodContainerType = methodContainerType.BaseType;
            }
            return false;
        }

        private static bool isLibraryCall(IMethodSymbol symbol)
        {
            var receiverType = symbol.ReceiverType;
            return isTypeOrSubtypeOf(receiverType , typeof(AVM.ClientGenerator.Core.SmartContractLibrary).FullName);
        }

        private static bool isSmartContractOrSignature(ITypeSymbol receiverType)
        {
            return  isTypeOrSubtypeOf( receiverType, typeof(AVM.ClientGenerator.Core.SmartContract).FullName) ||
                    isTypeOrSubtypeOf(receiverType, typeof(AVM.ClientGenerator.Core.SmartSignature).FullName) ;
        }

        protected void CallSub(IMethodSymbol invocationSymbol, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals)
        {
            if (invocationSymbol == null) throw new ErrorDiagnosticException("E012");

            //at this point any parameters are pushed onto the stack already 

            switch (invocationSymbol.MethodKind)
            {
                case MethodKind.Ordinary:
                case MethodKind.PropertyGet:
                    //if the Kind is Ordinary or prop getter then either it is a potentially invalid reference to a non-local subroutine
                    //or it is a call to a predefined function, such as SwitchLocalStorageContext. All valid
                    //code is defined on AlgorandSmartContract/Sig
                    if (isSmartContractOrSignature(invocationSymbol.ReceiverType))
                    {
                        //code for predefined functions is inlined
                        string methodName = invocationSymbol.Name;
                        InvokePredefinedTypeMethod(methodName, nulledOptionals, literals);

                    }
                    else
                    {
                        //If this is a call to a library function, then callsub the function and register a dependency in the CodeBuilder
                        // (this is later used to add SmartContractLibraryMethods from the CompilationGroup context into the program output)
                        //Calls to library functions are treated as calls to Root scopes. They are external.
                        // It's up to the final optimisers and source generator to include the CodeBuilder dependencies in the final output.
                        if (isLibraryCall(invocationSymbol))
                        {
                            //methods defined on the base class are predefineds offered by AVM.ClientGenerator
                            if (isDirectTypeOf(invocationSymbol.ReceiverType, typeof(AVM.ClientGenerator.Core.SmartContractLibrary).FullName))
                            {
                                string methodName = invocationSymbol.Name;
                                InvokePredefinedTypeMethod(methodName, nulledOptionals, literals);
                            }
                            else
                            {
                                Code.RegisterLibraryDependency(invocationSymbol);
                                invokeMethodAndPreserveStack(invocationSymbol, SmartContractLibraryMethod.GetLibraryMethodLabel(invocationSymbol), Scope.Root);
                            }

                        }
                        else
                            throw new ErrorDiagnosticException("E010"); //invalid call to non-local function
                    }


                    break;
                case MethodKind.LocalFunction:

                    //ignore nulled optionals here because optionals are not permitted on custom functions
                    if (FirstParent<InnerTransactionLocalFunction>() != null) throw new ErrorDiagnosticException("E035");

                    var childFunction = Scope.AllSubScopes.Where(scope => scope.IsSymbolisedBy(invocationSymbol)).FirstOrDefault();
                    string name = LocalFunction.GetUniqueName(invocationSymbol);
                    if (childFunction == null)
                    {
                        //should be a sibling-scoped or parent-scope function
                        var visibleScope = Scope.AllVisibleSuperScopes.Where(scope => scope.IsSymbolisedBy(invocationSymbol)).FirstOrDefault();
                        invokeMethodAndPreserveStack(invocationSymbol, name, visibleScope);
                    }
                    else
                    {
                        // no need to work with the stack on child static scopes
                        
                        Code.callsub(name);
                    }

                    break;

                default:
                    throw new ErrorDiagnosticException("E013");

            }
        }

      

        private void invokeMethodAndPreserveStack(IMethodSymbol invocationSymbol, string label, Scope visibleScope)
        {
            if (visibleScope == null)
            {
                throw new ErrorDiagnosticException("E015");
            }
            else
            {
                int pushedVars = Scope.PushAllScratchVars(Code, visibleScope, 0); //leaving this scope, so save the locals
                if (pushedVars > 0)
                {
                    int variableCount = invocationSymbol.Parameters.Length;
                    foreach (var param in invocationSymbol.Parameters)
                    {
                        Code.uncover(pushedVars + variableCount - 1);  //move the parameters to the top of the stack from under the pushed locals and keep their ordering
                    }
                }
                Code.callsub(label);            //call the sub

                if (pushedVars > 0 && !invocationSymbol.ReturnsVoid)
                {
                    Code.cover(pushedVars);                         //if the method returns something, preserve it

                }

                Scope.PopAllScratchVars(Code, visibleScope);                //store the preserved scratch vars back in reverse order from the stack

            }
        }

        internal void WriteLabel(string label)
        {
            Code.AddLabel(label);
        }

        internal void AddComment(string comment)
        {
            Code.AddComment(comment);
        }

        internal virtual void Duplicate()
        {
            Code.dup();
        }



        internal virtual void LoopCondition()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal virtual void LoopStatement()
        {
            throw new ErrorDiagnosticException("E006");
        }


        internal virtual void Continue()
        {
            var firstLoop = FirstParent<Loop>();
            if (firstLoop == null) throw new ErrorDiagnosticException("E006");
            firstLoop.Continue();
        }

        internal virtual void Return(bool hasReturnValue)
        {
            if (parent != null)
            {
                parent.Return(hasReturnValue);
            }
            else
            {
                throw new ErrorDiagnosticException("E006");
            }
        }

        internal virtual void Break()
        {
            var firstLoop = FirstParent<Loop>();
            if (firstLoop == null) throw new ErrorDiagnosticException("E006");
            firstLoop.Break();
        }

        internal virtual string ConditionalAndLeft()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void ConditionalAndRight(string exitFalseLabel)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual string ConditionalOrLeft()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void ConditionalOrRight(string exitTrueLabel)
        {
            throw new ErrorDiagnosticException("E017");
        }



        internal virtual void Addition(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void Subtraction(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void Multiplication(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void Division(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void Remainder(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void LeftShift(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void RightShift(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void BitwiseAnd(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void BitwiseOr(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void LessThan(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void LessThanOrEquals(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void GreaterThan(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void GreaterThanOrEquals(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void ExclusiveOr(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void EqualsExpression(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void NotEquals(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void UnaryOperator(UnaryModifier modifier, string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal virtual void Increment(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal virtual void Decrement(string typeName)
        {
            throw new ErrorDiagnosticException("E017");
        }




        internal virtual void ReportDiagnostic(Action<Diagnostic> reportDiagnostic, Diagnostic diagnostic) { reportDiagnostic(diagnostic); }
        internal abstract void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType);
        internal abstract void DeclareLocalVariable(ISymbol symbol, Core.VariableType valueType);
        internal abstract void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType);
        //Treated differently as a ValueTuple is also an InnerTransaction invocation in the InnerTransaction context.
        internal virtual void DeclareValueTupleVariable(ISymbol symbol, SemanticModel model)
        {
            if (Parent != null) Parent.DeclareValueTupleVariable(symbol, model);
            else throw new ErrorDiagnosticException("E008");
        }
        internal virtual void DeclareInnerTransactionVariable(ISymbol symbol, SemanticModel model)
        {
            if (Parent != null) Parent.DeclareInnerTransactionVariable(symbol, model);
            else throw new ErrorDiagnosticException("E008");
        }



        /// <summary>
        /// An IdentifierNameSyntax is encountered
        /// </summary>
        /// <param name="symbol"></param>
        internal abstract void IdentifierNameSyntax(ISymbol symbol);
        internal abstract void YieldExpressionResult();
        internal abstract CompilerState EnterExpressionEvaluationScope(bool isVoid);

        internal abstract void ReadFromVariable(ISymbol symbol);
        internal abstract void StoreToVariable(ISymbol symbol);


        internal virtual void LabeledStatement(ISymbol symbol)
        {
            string label = Code.GetLabeledStatement(symbol);
            Code.AddLabel(label);
        }


        internal virtual CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal virtual CompilerState EnterGoto()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal virtual CompilerState EnterABIMethodScope(IMethodSymbol method, Core.OnCompleteType callType, string optionalLabel, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E009");
        }



        internal virtual CompilerState EnterTupleExpression(TupleExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            if (Parent != null)
            {
                return Parent.EnterTupleExpression(node, semanticModel, substate);

            }
            else
            {
                throw new ErrorDiagnosticException("E009");
            }
        }

        internal virtual CompilerState EnterABIStructScope(TealSharpSyntaxWalker compiler, INamedTypeSymbol symbol)
        {
            return new ABIStruct(compiler, this, symbol);
        }

        internal virtual CompilerState EnterExpressionStatementScope()
        {
            return new ExpressionStatement(this);
        }

        internal abstract CompilerState EnterClassScope();

        internal virtual CompilerState EnterSmartContractScope(ISymbol contract) { throw new ErrorDiagnosticException("E009"); }

        internal virtual CompilerState EnterSmartSignatureScope(ISymbol sig) { throw new ErrorDiagnosticException("E009"); }

        internal virtual CompilerState EnterSmartContractLibraryScope() { throw new ErrorDiagnosticException("E009"); }

        

        internal abstract CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol);
        internal abstract CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol);

        internal virtual CompilerState EnterMethodScope(ISymbol method, SemanticModel semanticModel) { throw new ErrorDiagnosticException("E009"); }

        internal abstract CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel);

        internal abstract CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel);

        internal abstract CompilerState EnterIfStatementScope();

        internal virtual void ElseClause()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal abstract CompilerState EnterCodeBlockScope();

        internal virtual void ObjectCreation(ObjectCreationExpressionSyntax methodOrFunction, List<IParameterSymbol> nulledOptionals, SemanticModel semanticModel, Dictionary<string, string> literals = null)
        {
            if (Parent != null)
            {
                Parent.ObjectCreation(methodOrFunction, nulledOptionals, semanticModel, literals);
            }
            else
            {
                throw new ErrorDiagnosticException("E009");
            }

        }

        internal abstract CompilerState EnterLoopScope();

        internal abstract CompilerState EnterByteArrayInitialiserScope();

        internal abstract CompilerState Leave();

        internal abstract void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null);

        /// <summary>
        /// Build value from stack is used when a value, such as byteslice, is being built up part by part, such as during a byte array initialiser like {0,1,2,someFunc()}
        /// </summary>
        /// <param name="construct"></param>
        internal abstract void BuildValueFromStack(ISymbol construct);

        internal abstract void AddLiteralNumeric(ulong value);

        internal abstract void AddLiteralByteConstant(byte[] value);

        internal abstract void AddLiteralString(string value);

        internal virtual void SaveToReferencedVariable(ISymbol referenceSymbol, SimpleNameSyntax name, Core.StorageType storageType)
        {
            //TODO override in those states where this is not expected to give error diag

            Scope.StoreReferencedVariable(referenceSymbol, name.Identifier, storageType, Code);



        }

        #region LOAD FROM - ARCHITECTURE IN PROGRESS




        // The general aim here is to move the following three methods (LoadFromType, LoadFromStruct, LoadFromInnerTxn) into one method and have the
        // ScratchVariable subtypes removed. So there'd be one type of scratch variable, holding
        // the relevant ulong or byte array, but the type of the C# expression determines what
        // should be invoked.
        // Also this section is for read accesses. Ideally the same convergence happens for storing
        // and method invocations.
        internal virtual void LoadFromType(ITypeSymbol expressionType, string fieldName,    Core.StorageType storageType)
        {
            //TODO - override in states where this does not make sense to generate the relevant syntax errors
            ITypePredefined predefineds = IdentifyTypeForPredefinedMembers(expressionType);
            if (predefineds is SmartContractRefPredefineds smartContractPredefined && !(storageType == Core.StorageType.Protocol))
            {
                switch (storageType)
                {
                    case Core.StorageType.Local:
                        smartContractPredefined.LoadLocal(fieldName);
                        break;
                    case Core.StorageType.Global:
                        smartContractPredefined.LoadGlobal(fieldName);
                        break;
                }
            }
            else
            {
                if (TealTypeUtils.IsInnerTransaction(expressionType))
                {
                    //no predefineds for single inner transactions
                    Code.itxn(fieldName);
                }
                else
                {


                    var type = predefineds.GetType();
                    var method = type.GetMethod(fieldName);
                    if (method != null)
                    {
                        method.Invoke(predefineds, new object[] { });
                    }
                    else
                    {
                        throw new ErrorDiagnosticException("E047");
                    }
                }
            }
        }


        //TODO override in states where this does not make sense
        internal virtual void LoadFromStruct(string structName, string fieldName)
        {
            StructPredefineds sp = new StructPredefineds(Code, CompilationGroup);

            sp.Load(structName, fieldName);

        }

        internal virtual void LoadFromGroupInnerTransaction(ISymbol referenceSymbol, string rhs, Core.StorageType storageType)
        {

            if (Parent != null)
            {

                Parent.LoadFromGroupInnerTransaction(referenceSymbol, rhs, storageType);
            }
            else
            {

                throw new ErrorDiagnosticException("E020");
            }

        }

        internal virtual void LoadFromArrayElement(ITypeSymbol expressionType)
        {
            ITypePredefined predefineds = IdentifyTypeForPredefinedMembers(expressionType);
            if (predefineds is IArrayTypePredefined arp && arp != null)
            {
                arp.GetAtIndex();
            }
            else
            {
                throw new ErrorDiagnosticException("E006");
            }
        }

        private ITypePredefined IdentifyTypeForPredefinedMembers(ITypeSymbol expressionType)
        {
            string[] integerTypes = { "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong" };
            ITypePredefined predefineds = null;
            string typeString = expressionType.ToString();

            if (AccountRefVariable.IsAccountRef(expressionType)) { return new AccountRefPredefineds(Code); }
            if (AssetRefVariable.IsAssetRef(expressionType)) { return new AssetRefPredefineds(Code); }
            if (ApplicationRefVariable.IsApplicationRef(expressionType)) { return new SmartContractRefPredefineds(Code); }
            if (TransactionRefVariable.IsTxRef(expressionType)) { return new TransactionRefVariablePredefineds(Code); }
            if (typeString == "system.numerics.biginteger") { return new BigIntegerPredefineds(Code); }
            if (typeString == "byte[]") { return new ByteArrayPredefineds(Code); }
            if (integerTypes.Contains(typeString)) { return new IntegerPredefineds(Code); }
            if (typeString == "string") { return new StringPredefineds(Code); }
            //if type is an array type and not byte[] (already handled above) then we can launch a predefined
            //that just knows about the underlying type and supports getter
            if (expressionType is IArrayTypeSymbol arts) { return new ArrayAccessorPredefineds(Code, arts); }

            return predefineds;
        }

        #endregion

        internal virtual CompilerState EnterVariableMethodInvocation(ISymbol referenceSymbol, InvocationExpressionSyntax node, SemanticModel semanticModel, CompilerState substate)
        {
            //TODO override in those states where this is expected to give error diag
            var v = Scope.FindVariable(referenceSymbol);
            if (v.HasValue && !(v.Value.Item1 is ApplicationRefVariable))  //App call only allowed in InnerTransactionLocalFunction
            {
                return this;
            }
            else
            {
                if (Parent != null) return Parent.EnterVariableMethodInvocation(referenceSymbol, node, semanticModel, substate);
                else
                    throw new ErrorDiagnosticException("E039");
            }
        }


        internal virtual void InvokeFromReferencedVariable(ISymbol referenceSymbol, SemanticModel model, InvocationExpressionSyntax node, string name, List<IParameterSymbol> nulledOptionals = null, Dictionary<string, string> literals = null)
        {
            //TODO override in those states where this is not expected to give error diag
            var v = Scope.FindVariable(referenceSymbol);
            if (v.HasValue && !(v.Value.Item1 is ApplicationRefVariable))  //App call only allowed in InnerTransactionLocalFunction
            {
                ReadVariableCode(referenceSymbol);
                Scope.InvokeReferencedVariable(referenceSymbol, model, node, name, Code, nulledOptionals, literals);
            }
            else
            {
                if (Parent != null) Parent.InvokeFromReferencedVariable(referenceSymbol, model, node, name, nulledOptionals, literals);
                else
                    throw new ErrorDiagnosticException("E039");
            }



        }




        internal virtual void RegisterStructAccessor(ISymbol declaratorSymbol, ABIEncodingType encoding, int byteWidth)
        {
            throw new ErrorDiagnosticException("E009");
        }


        internal virtual void Cast(string fromType, string toType)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal virtual void LoopContinuePoint()
        {
            throw new ErrorDiagnosticException("E006");
        }
    }








}
