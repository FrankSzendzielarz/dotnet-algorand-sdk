using AVM.ClientGenerator.Compiler.CompilerStates;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using AVM.ClientGenerator.Core.Attributes;
using AVM.ClientGenerator.Optimisers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using static AVM.ClientGenerator.Compiler.TealTypeUtils;

namespace AVM.ClientGenerator.Compiler
{
    internal class TealSharpSyntaxWalker : CSharpSyntaxWalker
    {



        private SemanticModel semanticModel;
        private Action<Diagnostic> reportDiagnostic;
        private Location location;
        private CompilerState State = new InitialState();
        private CompilationGroup compilationGroup;


        public TealSharpSyntaxWalker(CompilationGroup compilationGroup, Action<Diagnostic> reportDiagnostic, SemanticModel semanticModel, Location location)
        {
            this.location = location;
            this.semanticModel = semanticModel;
            this.reportDiagnostic = reportDiagnostic;
            this.compilationGroup = compilationGroup;
            this.State.CompilationGroup = compilationGroup;
        }



        internal List<ContractDeclaration> GetSmartContracts(List<IOptimiser> optimisers)
        {

            if (State is InitialState state)
            {
                var cd = state.GetContractDeclarations(optimisers);
                foreach (var dec in cd)
                {
                    //TODO obsolete since dynamic costs
                    var diagnostic = DiagnosticDescriptors.Create("I001", location, 0, "", messageArgs: new object[] { dec.Name, dec.ApprovalProgramCost });
                    reportDiagnostic(diagnostic);
                }
                return cd;

            }
            else
            {
                return new List<ContractDeclaration>();
            }
        }

        internal List<SmartSignatureDeclaration> GetSmartSignatures(List<IOptimiser> optimisers)
        {

            if (State is InitialState state)
            {
                var cd = state.GetSmartSignatureDeclarations(optimisers);
                foreach (var dec in cd)
                {
                    var diagnostic = DiagnosticDescriptors.Create("I001", location, 0, "", messageArgs: new object[] { dec.Name, dec.ProgramCost });
                    reportDiagnostic(diagnostic);
                }
                return cd;

            }
            else
            {
                return new List<SmartSignatureDeclaration>();
            }
        }



        private bool TryStateOperation(Action operation, CSharpSyntaxNode node, string info, bool mandateStateChange = true, [CallerLineNumber] int line = 0, [CallerMemberName] string caller = "")
        {
            CompilerState currentState = State;
            try
            {
                // currentState.SetCommentText(node.ToString());

                operation();

                if (mandateStateChange && currentState == State) return true;
                return false;
            }
            catch (ErrorDiagnosticException ex)
            {
                var diagnostic = DiagnosticDescriptors.Create(ex.Diagnostic, node.GetLocation(), line, caller, messageArgs: info);
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                return true;
            }
            catch (WarningDiagnosticException ex)
            {
                var diagnostic = DiagnosticDescriptors.Create(ex.Diagnostic, node.GetLocation(), line, caller, messageArgs: info);
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                return false;
            }
            catch (InfoDiagnosticException ex)
            {
                var diagnostic = DiagnosticDescriptors.Create(ex.Diagnostic, node.GetLocation(), line, caller, messageArgs: info);
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                return false;
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is DiagnosticException de)
                {
                    var diagnostic = DiagnosticDescriptors.Create(de.Diagnostic, node.GetLocation(), line, caller, messageArgs: info);
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return false;
                }
                else
                {
                    var diagnostic = DiagnosticDescriptors.Create("E012", node.GetLocation(), line, caller, messageArgs: info);
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return true;
                }
            }
            catch (Exception)
            {
                var diagnostic = DiagnosticDescriptors.Create("E012", node.GetLocation(), line, caller, messageArgs: info);
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                return true;
            }
        }

        private bool IsSmartContractProgram(IMethodSymbol method)
        {
            return (method.IsOverride && method.OverriddenMethod.ContainingType.Name == typeof(Core.SmartContract).Name);
        }

        private bool IsSmartSignatureProgram(IMethodSymbol method)
        {
            return (method.IsOverride && method.OverriddenMethod.ContainingType.Name == typeof(Core.SmartSignature).Name);
        }

        private bool isClassSmartContract(INamedTypeSymbol symbol)
        {
            if (symbol.BaseType == null) return false;
            if (symbol.BaseType.Name == typeof(Core.SmartContract).Name) return true;
            return isClassSmartContract(symbol.BaseType);
        }

        private bool isClassSmartContractLibrary(INamedTypeSymbol symbol)
        {
            if (symbol.BaseType == null) return false;
            if (symbol.BaseType.Name == typeof(Core.SmartContractLibrary).Name) return true;
            return isClassSmartContractLibrary(symbol.BaseType);
        }

        private bool isClassSmartSignature(INamedTypeSymbol symbol)
        {
            if (symbol.BaseType == null) return false;
            if (symbol.BaseType.Name == typeof(Core.SmartSignature).Name) return true;
            return isClassSmartSignature(symbol.BaseType);
        }

        private bool legacyMechanismNeededForGroupInnerTransaction(ExpressionSyntax expression)
        {
            //if the expression is itself a member access expression where returning an inner transaction
            //where the parent(the expression of the member access expression) is a group (a valuetuple)
            var expressionType = semanticModel.GetTypeInfo(expression).Type;
            if (expressionType != null && expression is MemberAccessExpressionSyntax mae && TealTypeUtils.IsInnerTransaction(expressionType))
            {
                var parentType = semanticModel.GetTypeInfo(mae.Expression).Type;
                if (parentType != null && parentType.IsTupleType)
                {
                    return true;
                }
            }
            return false;
        }

        private void handleStringLiteral(LiteralExpressionSyntax node)
        {

            var typeInfo = semanticModel.GetTypeInfo(node);
            var value = semanticModel.GetConstantValue(node);
            if (value.HasValue)
            {
                TryStateOperation(() => State.AddLiteralString(value.Value as String), node, node.Token.ValueText, false);
            }
            else
            {
                var d = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                reportDiagnostic(d);
            }
        }
        private void handleNumericLiteral(LiteralExpressionSyntax node, TealTypeUtils.UnaryModifier unaryModifier)
        {

            var typeInfo = semanticModel.GetTypeInfo(node);
            var value = semanticModel.GetConstantValue(node);
            if (value.HasValue)
            {
                if (TealTypeUtils.GetRepresentedNumeric(typeInfo.Type, value.Value, unaryModifier, out ulong? representedNumeric, out byte[] representedNumericAsBytes))
                {
                    if (representedNumeric != null)
                    {
                        TryStateOperation(() => State.AddLiteralNumeric(representedNumeric.Value), node, node.Token.ValueText, false);
                    }
                    else
                    {
                        TryStateOperation(() => State.AddLiteralByteConstant(representedNumericAsBytes), node, node.Token.ValueText, false);
                    }
                }
                else
                {
                    var d2 = DiagnosticDescriptors.Create("E008", node.GetLocation(), messageArgs: semanticModel.GetTypeInfo(node).Type.Name);
                    reportDiagnostic(d2);
                }
            }
            else
            {
                var d = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                reportDiagnostic(d);
            }
        }

        private int lastLineNumber = -1;


        /// <summary>
        /// Common function to all visits.
        /// Must be used in all overrides.
        /// </summary>
        /// <param name="node"></param>
        private void visit(CSharpSyntaxNode node, Action visit)
        {
            /* 
             *  Parent syntaxes determine what to do with expression evaluation return values.
             *  For example,    ExpressionStatement discards its child expression return value, if there is one.
             *                  Or the For loop discards the expression return values from its iterators but not its conditions.
             *  Discarding a value means checking if the expression return type is not void, and if not, then popping the stack.
             *  
             *  So:
             *  1. The walker needs to inform the compiler state if a value is to be expected.
             *  2. When the expressionevaluation is entered, it does so with the parent expectations.
             *  3. When the expressionevaluation is left, it discards the value or not, depending on the parent expectations and if the type is void.
             *  
             */

            if (
                    !(State is ExpressionEvaluation) &&        // if we aren't already evaluating expressions
                    (node is ExpressionSyntax expression)      // and if this is an expression
                )
            {
                var operation = semanticModel.GetOperation(node);
                if (operation != null)
                {
                    bool expressionProducesNoValue = false;
                    var typeInfo = semanticModel.GetTypeInfo(expression);
                    var type = typeInfo.Type;
                    //if the expression generates 'void' , an inner transaction, such as a call to a SmartContractReference, or an ObjectCreation of type inner transaction,
                    //then the expression is considered as not generating a value.
                    if (type != null && (type.SpecialType == SpecialType.System_Void || isGroupInnerTransaction(type) || IsInnerTransaction(type)))
                    {
                        expressionProducesNoValue = true;
                    }


                    bool error = TryStateOperation(() => State = State.EnterExpressionEvaluationScope(expressionProducesNoValue), expression, "");
                    if (!error)
                    {
                        visit();
                        TryStateOperation(() => State = State.Leave(), expression, "");
                    }

                }
                else
                {
                    visit();
                }
            }
            else
            {
                visit();
            }



        }
        private void checkForImplicitCast(CSharpSyntaxNode c)
        {
            Microsoft.CodeAnalysis.TypeInfo t = semanticModel.GetTypeInfo(c);
            if (t.Type != null && !t.Type.Equals(t.ConvertedType, SymbolEqualityComparer.Default))
            {
                var from = t.Type.ToString().ToLower();
                var to = t.ConvertedType.ToString().ToLower();

                TryStateOperation(() => State.Cast(from, to), c, "", false);

            }
        }
        private bool checkArgumentList(IMethodSymbol method, ArgumentListSyntax args, ExpressionSyntax node, out List<IParameterSymbol> nulledOptionals, out Dictionary<string, string> literals, bool allowOptionals = false)
        {
            //ban optional calling, ban named parameters, and allow nulls if the predefined parameter is optional

            nulledOptionals = new List<IParameterSymbol>();
            literals = new Dictionary<string, string>();

            if (method.Parameters.Length != args.Arguments.Count && !allowOptionals)
            {
                var diagnostic = DiagnosticDescriptors.Create("E028", node.GetLocation(), messageArgs: node.Kind().ToString());
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                return false;
            }

            int c = 0;
            foreach (var arg in args.Arguments)
            {

                if (arg.NameColon != null && !allowOptionals)
                {
                    var diagnostic = DiagnosticDescriptors.Create("E029", node.GetLocation(), messageArgs: node.Kind().ToString());
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return false;
                }

                var val = semanticModel.GetConstantValue(arg.Expression);

                //check for literal mandate
                var literalMandate = method.Parameters[c].GetAttributes().Where(a => a.AttributeClass.Name == nameof(LiteralAttribute)).FirstOrDefault(); ;
                if (!val.HasValue && literalMandate != null)
                {
                    var diagnostic = DiagnosticDescriptors.Create("E033", node.GetLocation(), messageArgs: node.Kind().ToString());
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return false;
                }
                if (literalMandate != null && val.HasValue)
                {
                    literals.Add(method.Parameters[c].Name, val.Value.ToString());
                }

                //check for null being passed to a non optional
                if (val.HasValue && val.Value == null)
                {
                    if (!method.Parameters[c].IsOptional)
                    {
                        var diagnostic = DiagnosticDescriptors.Create("E030", node.GetLocation(), messageArgs: node.Kind().ToString());
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        return false;
                    }
                    else
                    {
                        nulledOptionals.Add(method.Parameters[c]);
                    }
                }


                c++;
            }


            return true;


        }
        private bool isGroupInnerTransaction(ITypeSymbol ts)
        {
            if (ts.IsTupleType)
            {
                return true;
            }

            return false;
        }

        internal void AddStruct(INamedTypeSymbol structSymbol)
        {
            var structName = structSymbol.ContainingNamespace + "." + structSymbol.Name;
            compilationGroup.AddEncodedStructure(structName);
        }
        internal void AddStructAccessor(INamedTypeSymbol structSymbol, ISymbol fieldSymbol, ABIEncodingType encoding, int byteWidth)
        {
            var structName = structSymbol.ContainingNamespace + "." + structSymbol.Name;

            compilationGroup.AddAccessor(structName, fieldSymbol, encoding, byteWidth);

        }

        #region Supported Syntaxes
        public override void Visit(SyntaxNode n)
        {
            CSharpSyntaxNode node = n as CSharpSyntaxNode;
            if (node == null) return;
            visit(node, () =>
                {
                    if (node is CSharpSyntaxNode csnode)
                    {
                        FileLinePositionSpan span = node.SyntaxTree.GetLineSpan(node.Span);

                        int lineNumber = span.StartLinePosition.Line;
                        if (lastLineNumber != lineNumber)
                        {
                            lastLineNumber = lineNumber;
                            var lineText = node.SyntaxTree.GetText().Lines[lineNumber];
                            TryStateOperation(() => State.AddComment(lineText.ToString().Replace('"', ' ')), csnode, "", false);
                        }
                    }

                    base.Visit(node);

                    if (node is ExpressionSyntax expression)
                    {

                        checkForImplicitCast(node);
                    }

                }
            );
        }
        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            bool error = TryStateOperation(() => State = State.EnterExpressionStatementScope(), node, "ExpressionStatement");
            if (!error)
            {
                Visit(node.Expression);

                TryStateOperation(() => State = State.Leave(), node, "ExpressionStatement");
            }
        }
        public override void VisitTryStatement(TryStatementSyntax node)
        {
            visit(node, () =>
            {

                var diagnostic = DiagnosticDescriptors.Create("E051", node.GetLocation(), messageArgs: "");
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
            });

        }
        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            visit(node, () =>
            {
                bool error = false;
                var symbol = semanticModel.GetDeclaredSymbol(node);
                if (Utilities.IsAbiStruct(symbol))
                {

                    if (symbol.BaseType != null && symbol.BaseType.Name != typeof(System.ValueType).Name)
                    {
                        var diagnostic = DiagnosticDescriptors.Create("E058", node.GetLocation(), messageArgs: "");
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        return;
                    }

                    error = TryStateOperation(() => State = State.EnterABIStructScope(this, symbol), node, node.Identifier.ValueText);

                    if (!error)
                    {
                        foreach (var m in node.Members)
                        {
                            if (m is FieldDeclarationSyntax fd)
                            {
                                if (fd.Declaration != null)
                                {

                                    foreach (var declarator in fd.Declaration.Variables)
                                    {


                                        //determine if there's a fixed length declared on the field....used for fixed length byte arrays
                                        var fieldSymbol = semanticModel.GetDeclaredSymbol(declarator);
                                        int? fixedLength = TealTypeUtils.FixedSize(fieldSymbol);

                                        var valueType = TealTypeUtils.DetermineEncodingType(semanticModel, fd.Declaration.Type, fixedLength);
                                        if (valueType.Encoding == ABIEncodingType.Unsupported)
                                        {
                                            var diagnostic = DiagnosticDescriptors.Create("E008", node.GetLocation(), messageArgs: semanticModel.GetTypeInfo(node).Type.Name);
                                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                            continue;
                                        }

                                        var declaratorSymbol = semanticModel.GetDeclaredSymbol(declarator);
                                        if (declarator.Initializer != null)
                                        {
                                            var diagnostic = DiagnosticDescriptors.Create("E060", declarator.GetLocation(), messageArgs: "");
                                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                            continue;
                                        }
                                        TryStateOperation(() => State.RegisterStructAccessor(declaratorSymbol, valueType.Encoding, valueType.ByteWidth), declarator, declarator.Identifier.ToString(), false);

                                    }
                                }

                            }
                            else
                            {
                                var diagnostic = DiagnosticDescriptors.Create("E059", m.GetLocation(), messageArgs: "");
                                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            }
                        }
                        TryStateOperation(() => State = State.Leave(), node, node.Identifier.ValueText);
                    }

                }
            });
        }
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            visit(node, () =>
            {
                bool error = false;

                void visitAndLeave()
                {
                    if (!error)
                    {
                        foreach (var m in node.Members)
                        {
                            base.Visit(m);
                        }
                        TryStateOperation(() => State = State.Leave(), node, node.Identifier.ValueText);
                    }
                }

                var symbol = semanticModel.GetDeclaredSymbol(node);

                if (isClassSmartContract(symbol))
                {
                    error = TryStateOperation(() => State = State.EnterSmartContractScope(symbol), node, node.Identifier.ValueText);
                    visitAndLeave();
                    return;
                }

                if (isClassSmartSignature(symbol))
                {
                    error = TryStateOperation(() => State = State.EnterSmartSignatureScope(symbol), node, node.Identifier.ValueText);
                    visitAndLeave();
                    return;
                }

                if (isClassSmartContractLibrary(symbol))
                {
                    error = TryStateOperation(() => State = State.EnterSmartContractLibraryScope(), node, node.Identifier.ValueText);
                    visitAndLeave();
                    return;
                }

                error = TryStateOperation(() => State = State.EnterClassScope(), node, node.Identifier.ValueText);
                visitAndLeave();
            });
        }
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            visit(node, () =>
            {

                var methodSymbol = semanticModel.GetDeclaredSymbol(node);

                bool error = false;
                if (IsSmartContractProgram(methodSymbol))
                {
                    error = TryStateOperation(() => State = State.EnterSmartContractProgramScope(methodSymbol), node, node.Identifier.ValueText);
                }
                else
                {
                    if (IsSmartSignatureProgram(methodSymbol))
                    {
                        error = TryStateOperation(() => State = State.EnterSmartSignatureProgramScope(methodSymbol), node, node.Identifier.ValueText);
                    }
                    else
                    {
                        if (GetABIMethodDetails(methodSymbol, out Core.OnCompleteType callType, out string optionalLabel))
                        {

                            error = TryStateOperation(() => State = State.EnterABIMethodScope(methodSymbol, callType, optionalLabel, semanticModel), node, node.Identifier.ValueText);

                        }
                        else
                        {
                            error = TryStateOperation(() => State = State.EnterMethodScope(methodSymbol, semanticModel), node, node.Identifier.ValueText);
                        }
                    }
                }

                if (!error)
                {
                    base.VisitMethodDeclaration(node);
                    TryStateOperation(() => State = State.Leave(), node, node.Identifier.ValueText);
                }
            });


        }
        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            visit(node, () =>
            {
                var invocationSymbol = semanticModel.GetSymbolInfo(node).Symbol as IMethodSymbol;


                if (invocationSymbol != null)
                {
                    if (!checkArgumentList(invocationSymbol, node.ArgumentList, node, out List<IParameterSymbol> nulledOptionals, out Dictionary<string, string> literals)) return;

                    //if the invocation is of a local identifier, eg a local function or parent method
                    if (node.Expression is IdentifierNameSyntax)
                    {
                        int c = 0;
                        foreach (var arg in node.ArgumentList.Arguments)
                        {
                            var literalMandate = invocationSymbol.Parameters[c++].GetAttributes().Where(a => a.AttributeClass.Name == nameof(LiteralAttribute)).FirstOrDefault(); ;
                            if (literalMandate == null)
                            {
                                Visit(arg);
                            }
                        }

                        bool error = TryStateOperation(() => State.CallMethodOrFunction(invocationSymbol, nulledOptionals, literals), node, "", false);
                        if (error) return;

                        var outsToLoad = node.ArgumentList.Arguments.Reverse().Where(arg => arg.RefKindKeyword.ToString() == "out").ToList();
                        foreach (ArgumentSyntax arg in outsToLoad)
                        {
                            var operandSymbol = semanticModel.GetSymbolInfo(arg.Expression).Symbol;
                            if (operandSymbol != null)
                            {
                                TryStateOperation(() => State.StoreToVariable(operandSymbol), arg, operandSymbol.Name, false);
                            }
                        }
                        return;
                    }

                    //if the invocation is of a method belonging to some variable
                    if (node.Expression is MemberAccessExpressionSyntax mae)
                    {
                        if (mae.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                            mae.Expression is IdentifierNameSyntax referenceVariableSyntax)
                        {
                            var referenceSymbol = semanticModel.GetSymbolInfo(referenceVariableSyntax).Symbol;
                            if (referenceSymbol != null)
                            {
                                var state = State;
                                bool error = TryStateOperation(() => State = State.EnterVariableMethodInvocation(referenceSymbol, node, semanticModel, State), node, mae.Name.Identifier.ValueText, false);
                                if (!error)
                                {
                                    if (!checkArgumentList(invocationSymbol, node.ArgumentList, node, out nulledOptionals, out literals)) return;


                                    //BUG: The out field can be technically anywhere in the parameter list
                                    // The actual position in the gitxn represented by the smart reference method for the result depends on the 
                                    // total number of transactions in the group , and shouldn't depend on the position of the out parameter
                                    // The group is defined by the return type of the inner app call actually.
                                    // !! Add check to force out return to be last argument

                                    //eg: 
                                    //simpleContract.SplitPayment(new Payment(simpleContractAccount, amountToPay), opupContract.Opup(out bool success), 22.0M, recipient1, recipient2, out ulong recipient2share);
                                    // the above converts to a group transaction :
                                    // 1. Payment
                                    // 2. AppCall (opup)
                                    // 3. AppCall (current)


                                    //HACK if the state'ts changed then we're dealing with a inner transaction
                                    List<ArgumentSyntax> list = node.ArgumentList.Arguments.ToList();
                                    if (state != State)
                                    {
                                        list = new List<ArgumentSyntax>();
                                        //!! Cannot reverse inner transactions because that would mean the group transaction ordering would be wrong
                                        // 1. Visit only the args that are inner transactions in the current order.
                                        // 2. Reverse the rest
                                        // 3. The SmartContractMethodCall state Invokefromref'd variable needs to NOT reverse.
                                        foreach (var arg in node.ArgumentList.Arguments)
                                        {
                                            var typeSymbol = semanticModel.GetTypeInfo(arg.Expression).Type;

                                            if (arg.RefKindKeyword.Text == "out")
                                            {
                                                if (arg != node.ArgumentList.Arguments.Last())
                                                {
                                                    var err = DiagnosticDescriptors.Create("E068", node.GetLocation(), messageArgs: node.Kind().ToString());
                                                    State.ReportDiagnostic(reportDiagnostic, err);
                                                }
                                                Visit(arg);
                                                continue;
                                            }
                                            if (AccountRefVariable.IsAccountRef(typeSymbol))
                                            {
                                                list.Add(arg);
                                                continue;
                                            }

                                            if (AssetRefVariable.IsAssetRef(typeSymbol))
                                            {
                                                list.Add(arg);
                                                continue;
                                            }

                                            if (ApplicationRefVariable.IsApplicationRef(typeSymbol))
                                            {
                                                list.Add(arg);
                                                continue;
                                            }

                                            if (TealTypeUtils.IsInnerTransaction(typeSymbol))
                                            {
                                                Visit(arg);
                                                continue;
                                            }

                                            if (typeSymbol.IsTupleType)
                                            {
                                                //has to be a group transaction of some type if it's a tuple type
                                                isGroupInnerTransaction(typeSymbol as INamedTypeSymbol);
                                                Visit(arg);
                                                continue;
                                            }

                                            list.Add(arg);

                                        }

                                        list.Reverse();
                                    }

                                    int c = 0;
                                    foreach (var arg in list)
                                    {
                                        var literalMandate = invocationSymbol.Parameters[c++].GetAttributes().Where(a => a.AttributeClass.Name == nameof(LiteralAttribute)).FirstOrDefault(); ;
                                        if (literalMandate == null)
                                        {
                                            Visit(arg);
                                        }
                                    }
                                    TryStateOperation(() => State.InvokeFromReferencedVariable(referenceSymbol, semanticModel, node, mae.Name.Identifier.ValueText, nulledOptionals, literals), mae, mae.Name.ToString(), false);
                                    if (State != state) // we may or may not have changed state
                                    {
                                        TryStateOperation(() => State = State.Leave(), node, mae.Name.Identifier.ValueText);
                                    }
                                }

                            }
                            return;
                        }




                    }
                    var diagnostic = DiagnosticDescriptors.Create("E048", node.GetLocation(), messageArgs: node.Kind().ToString());
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                }
            });

        }
        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            visit(node, () =>
            {

                var functionSymbol = semanticModel.GetDeclaredSymbol(node);

                var innertransaction =
                          functionSymbol
                          .GetAttributes()
                          .Where(a => a.AttributeClass.Name == nameof(InnerTransactionCallAttribute))
                          .FirstOrDefault();

                if (innertransaction != null)
                {
                    bool error = TryStateOperation(() => State = State.EnterInnerTransactionScope(functionSymbol, semanticModel), node, node.Identifier.ValueText);
                    if (!error)
                    {
                        //base.VisitLocalFunctionStatement(node);
                        Visit(node.ParameterList);
                        Visit(node.Body);
                        TryStateOperation(() => State = State.Leave(), node, node.Identifier.ValueText);
                    }
                }
                else
                {
                    bool error = TryStateOperation(() => State = State.EnterFunctionScope(functionSymbol, semanticModel), node, node.Identifier.ValueText);
                    if (!error)
                    {
                        //base.VisitLocalFunctionStatement(node);
                        Visit(node.ParameterList);
                        Visit(node.Body);
                        TryStateOperation(() => State = State.Leave(), node, node.Identifier.ValueText);
                    }
                }
            });

        }
        public override void VisitIfStatement(IfStatementSyntax node)
        {
            visit(node, () =>
            {
                Visit(node.Condition); // get the condition on the stack
                bool error = TryStateOperation(() => State = State.EnterIfStatementScope(), node, "If");
                if (!error)
                {
                    Visit(node.Statement);
                    if (node.Else != null)
                    {
                        error = TryStateOperation(() => State.ElseClause(), node, "Else", false);
                        if (error)
                        {
                            var diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        }
                        else
                        {
                            Visit(node.Else);
                        }
                    }
                    TryStateOperation(() => State = State.Leave(), node, "If");
                }
            });

        }
        public override void VisitTupleExpression(TupleExpressionSyntax node)
        {
            visit(node, () =>
            {

                bool error = TryStateOperation(() => State = State.EnterTupleExpression(node, semanticModel, State), node, "");
                if (!error)
                {
                    base.VisitTupleExpression(node);
                    TryStateOperation(() => State = State.Leave(), node, "");
                }
            });
        }
        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            visit(node, () =>
            {
                var invocationSymbol = semanticModel.GetSymbolInfo(node).Symbol as IMethodSymbol;

                if (invocationSymbol != null)
                {

                    if (!checkArgumentList(invocationSymbol, node.ArgumentList, node, out List<IParameterSymbol> nulledOptionals, out Dictionary<string, string> literals, allowOptionals: true)) return;

                    List<string> parmNames = new List<string>();

                    foreach (var arg in node.ArgumentList.Arguments)
                    {

                        Visit(arg);


                    }

                    bool error = TryStateOperation(() => State.ObjectCreation(node, nulledOptionals, semanticModel, literals), node, "", false);

                    var outsToLoad = node.ArgumentList.Arguments.Reverse().Where(arg => arg.RefKindKeyword.ToString() == "out").ToList();
                    foreach (ArgumentSyntax arg in outsToLoad)
                    {
                        var operandSymbol = semanticModel.GetSymbolInfo(arg.Expression).Symbol;
                        if (operandSymbol != null)
                        {
                            TryStateOperation(() => State.StoreToVariable(operandSymbol), arg, operandSymbol.Name, false);
                        }
                    }
                }

            });
        }
        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            visit(node, () =>
            {
                // get the output type of the expression
                var typeInfo = semanticModel.GetTypeInfo(node);
                var type = typeInfo.Type;
                bool expressionReturnsVoid = false;
                if (type != null && type.SpecialType == SpecialType.System_Void)
                {
                    expressionReturnsVoid = true;
                }
                bool error = TryStateOperation(() => State = State.EnterExpressionEvaluationScope(expressionReturnsVoid), node, "Binary");
                if (!error)
                {
                    var typeSymbolRight = semanticModel.GetTypeInfo(node.Right).ConvertedType;
                    var typeSymbolLeft = semanticModel.GetTypeInfo(node.Left).ConvertedType;

                    Diagnostic diagnostic;
                    if (typeInfo.HasSupportedOperators())
                    {

                        string typeName = typeSymbolLeft.Name;

                        switch (node.Kind())
                        {
                            case SyntaxKind.AddExpression:

                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.Addition(typeName), node, "", false);
                                break;
                            case SyntaxKind.SubtractExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.Subtraction(typeName), node, "", false);
                                break;
                            case SyntaxKind.MultiplyExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.Multiplication(typeName), node, "", false);
                                break;
                            case SyntaxKind.DivideExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.Division(typeName), node, "", false);
                                break;
                            case SyntaxKind.ModuloExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.Remainder(typeName), node, "", false);
                                break;
                            case SyntaxKind.LogicalAndExpression:
                                Visit(node.Left);
                                //check for false, reserve a label, exit if false
                                string andLabel = "";
                                TryStateOperation(() => andLabel = State.ConditionalAndLeft(), node, "", false);
                                Visit(node.Right);
                                TryStateOperation(() => State.ConditionalAndRight(andLabel), node, "", false);
                                break;
                            case SyntaxKind.LogicalOrExpression:
                                Visit(node.Left);
                                //check for false, reserve a label, exit if false
                                string orLabel = "";
                                TryStateOperation(() => orLabel = State.ConditionalOrLeft(), node, "", false);
                                Visit(node.Right);
                                TryStateOperation(() => State.ConditionalOrRight(orLabel), node, "", false);
                                break;
                            case SyntaxKind.LeftShiftExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                TryStateOperation(() => State.LeftShift(typeName), node, "", false);
                                break;
                            case SyntaxKind.RightShiftExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                TryStateOperation(() => State.RightShift(typeName), node, "", false);
                                break;
                            case SyntaxKind.BitwiseAndExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.BitwiseAnd(typeName), node, "", false);
                                break;
                            case SyntaxKind.BitwiseOrExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.BitwiseOr(typeName), node, "", false);
                                break;

                            case SyntaxKind.ExclusiveOrExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.ExclusiveOr(typeName), node, "", false);
                                break;


                            case SyntaxKind.LessThanExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.LessThan(typeName), node, "", false);
                                break;
                            case SyntaxKind.LessThanOrEqualExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.LessThanOrEquals(typeName), node, "", false);
                                break;
                            case SyntaxKind.GreaterThanExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.GreaterThan(typeName), node, "", false);
                                break;
                            case SyntaxKind.GreaterThanOrEqualExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.GreaterThanOrEquals(typeName), node, "", false);
                                break;
                            case SyntaxKind.EqualsExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.EqualsExpression(typeName), node, "", false);
                                break;
                            case SyntaxKind.NotEqualsExpression:
                                Visit(node.Left);
                                Visit(node.Right);

                                if (!typeSymbolRight.Equals(typeSymbolLeft, SymbolEqualityComparer.Default))
                                {
                                    diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                    return;
                                }
                                TryStateOperation(() => State.NotEquals(typeName), node, "", false);
                                break;


                            default:
                                diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                                return;

                        }
                    }
                    else
                    {
                        diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    }


                }
                TryStateOperation(() => State = State.Leave(), node, "BinaryExpression");


            });


        }
        public override void VisitParameter(ParameterSyntax node)
        {
            visit(node, () =>
            {

                //determine type
                Core.VariableType machineValueType = TealTypeUtils.DetermineType(semanticModel, node.Type);
                if (machineValueType == Core.VariableType.Unsupported)
                {
                    var msgArg = semanticModel.GetTypeInfo(node.Type).Type?.Name;
                    var diagnostic = DiagnosticDescriptors.Create("E008", node.GetLocation(), messageArgs: msgArg ?? "");
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return;
                }
            });
        }
        public override void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
        {
            visit(node, () =>
            {
                var typeName = semanticModel.GetTypeInfo(node.Operand).ConvertedType.Name;
                Diagnostic diagnostic;
                switch (node.Kind())
                {


                    case SyntaxKind.PostIncrementExpression:
                        if (node.Operand is IdentifierNameSyntax || node.Operand is ParenthesizedExpressionSyntax)
                        {
                            var operandSymbol = semanticModel.GetSymbolInfo(node.Operand).Symbol;
                            if (operandSymbol != null)
                            {
                                TryStateOperation(() => State.ReadFromVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                                TryStateOperation(() => State.YieldExpressionResult(), node.Operand, operandSymbol.Name, false);
                                TryStateOperation(() => State.Increment(typeName), node, "Increment", false);
                                TryStateOperation(() => State.StoreToVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                            }
                        }
                        else
                        {
                            //TODO - add support for Indexers eg t[1]++; 
                            diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        }

                        break;
                    case SyntaxKind.PostDecrementExpression:
                        if (node.Operand is IdentifierNameSyntax || node.Operand is ParenthesizedExpressionSyntax)
                        {
                            var operandSymbol = semanticModel.GetSymbolInfo(node.Operand).Symbol;
                            if (operandSymbol != null)
                            {
                                TryStateOperation(() => State.ReadFromVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                                TryStateOperation(() => State.YieldExpressionResult(), node.Operand, operandSymbol.Name, false);
                                TryStateOperation(() => State.Decrement(typeName), node, "Decrement", false);
                                TryStateOperation(() => State.StoreToVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                            }
                        }
                        else
                        {
                            //TODO - add support for Indexers eg t[1]++; 
                            diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        }
                        break;
                    default:
                        diagnostic = DiagnosticDescriptors.Create("W003", node.GetLocation(), messageArgs: node.Kind().ToString());
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        break;


                }




            });
        }
        public override void VisitDeclarationExpression(DeclarationExpressionSyntax node)
        {
            visit(node, () =>
            {
                //TODO this assumes an Out context

                Core.VariableType machineValueType = TealTypeUtils.DetermineType(semanticModel, node.Type); ;

                if (machineValueType == Core.VariableType.Unsupported)
                {
                    var diagnostic = DiagnosticDescriptors.Create("E008", node.GetLocation(), messageArgs: semanticModel.GetTypeInfo(node.Type).Type.Name);
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return;
                }

                // SemanticModel.GetDeclaredSymbol(VariableDesignationSyntax) always returns null (RS1039);
                // the SingleVariableDesignationSyntax overload is the one that actually resolves the symbol.
                var declaratorSymbol = node.Designation is SingleVariableDesignationSyntax single
                    ? semanticModel.GetDeclaredSymbol(single)
                    : null;
                TryStateOperation(() => State.DeclareScratchVariable(declaratorSymbol, machineValueType), node.Designation, declaratorSymbol?.Name, false);


            });
        }
        public override void VisitArgument(ArgumentSyntax node)
        {
            visit(node, () =>
            {

                //TODO - most of the below assumes that args are only to method/func calls
                //       but they are also to things like ValueTuples

                switch (node.RefKindKeyword.ToString().ToLower())
                {
                    case "out":
                        //out arguments are ignored (they are handled by the subroutine scope) unless they contain a declarationexpression:
                        bool error = TryStateOperation(() => State = State.EnterOutArgumentScope(node, semanticModel), node, "");
                        if (!error)
                        {
                            base.VisitArgument(node);
                            TryStateOperation(() => State = State.Leave(), node, "");
                        }
                        break;
                    case "":
                        var constant = semanticModel.GetConstantValue(node.Expression);
                        if (constant.HasValue && constant.Value == null)
                        {
                            if (node.Expression is ObjectCreationExpressionSyntax ocs)
                            {
                                base.VisitArgument(node);
                            }
                        }
                        else
                        {
                            base.VisitArgument(node);
                        }
                        break;
                    default:
                        var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.RefKindKeyword.ToString());
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        break;

                }




            });
        }
        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            //do nothing!
        }
        public override void VisitAttribute(AttributeSyntax node)
        {
            //do nothing
        }
        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {


            foreach (var m in node.Members)
            {
                base.Visit(m);
            }
        }
        public override void VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
        {
            foreach (var member in node.Members)
            {
                Visit(member);
            }
        }
        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            visit(node, () =>
            {
                var idSymbol = semanticModel.GetSymbolInfo(node).Symbol;
                if (idSymbol != null)
                {
                    if (idSymbol is IPropertySymbol ip)
                    {
                        //TODO - check here if this can be abused when setters are detected
                        bool error = TryStateOperation(() => State.CallMethodOrFunction(ip.GetMethod, new List<IParameterSymbol>()), node, "", false);
                    }
                    else
                    {
                        if (!TryStateOperation(() => State.IdentifierNameSyntax(idSymbol), node, idSymbol.Name, false))
                        {
                            System.Diagnostics.Debug.WriteLine(idSymbol.Name);
                        }
                    }


                    base.VisitIdentifierName(node);
                }

            });
        }
        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            visit(node, () =>
            {

                bool error = TryStateOperation(() => State = State.EnterGoto(), node, "");

                if (!error)
                {
                    Visit(node.Expression);
                    TryStateOperation(() => State = State.Leave(), node, "");
                }

            });
        }
        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            visit(node, () =>
            {
                if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression))
                {


                    //#HACK Checking to see if the conditions apply for the legacy code that is subject to the ongoing
                    //      rearchitecture around type accessing and scratch variables.

                    if (!legacyMechanismNeededForGroupInnerTransaction(node.Expression))
                    {
                        var expressionType = semanticModel.GetTypeInfo(node.Expression).Type;
                        if (node.Expression != null)
                        {
                            //first visit the thing being accessed (recursively)
                            Visit(node.Expression);
                        }
                        var fieldName = semanticModel.GetSymbolInfo(node.Name).Symbol.Name;
                        if (Utilities.IsAbiStruct(expressionType))
                        {
                            var structName = expressionType.ContainingNamespace + "." + expressionType.Name;
                            TryStateOperation(() => State.LoadFromStruct(structName, fieldName), node, structName, false);
                        }
                        else
                        {
                            var storageDeclaration = semanticModel
                                                        .GetSymbolInfo(node)
                                                        .Symbol
                                                        ?.GetAttributes()
                                                        .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                                                        .FirstOrDefault();

                            Core.StorageType storageType = Core.StorageType.Scratch;
                            if (storageDeclaration != null)
                            {
                                var st = storageDeclaration.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).FirstOrDefault();
                                storageType = (Core.StorageType)st.Value;
                            }

                            TryStateOperation(() => State.LoadFromType(expressionType, fieldName, storageType), node, fieldName, false);
                        }
                    }
                    else
                    {
                        #region LEGACY INNER TRANSACTION MECHANISM 
                        //#TODO Legacy code which needs to be reworked to fit above approach
                        //  This is just a way of accessing InnerTransactions, which are txn groups or groups of txn groups represented as [nested] valuetuples
                        if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                            node.Expression is MemberAccessExpressionSyntax)
                        {
                            //need to get the RHS of the expression
                            MemberAccessExpressionSyntax currentNode = node;
                            List<string> rhs = new List<string>();
                            bool done = false;
                            do
                            {
                                var symb = semanticModel.GetSymbolInfo(currentNode);
                                if (symb.Symbol?.Name != null)
                                {
                                    rhs.Add(symb.Symbol.Name);

                                    var subExpression = currentNode.Expression as MemberAccessExpressionSyntax;
                                    if (subExpression == null) done = true;
                                    else currentNode = subExpression;
                                }


                            } while (!done);

                            if (currentNode.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                                 currentNode.Expression is IdentifierNameSyntax rvs)
                            {

                                var storageDeclaration = semanticModel
                                    .GetSymbolInfo(currentNode)
                                    .Symbol
                                    ?.GetAttributes()
                                    .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                                    .FirstOrDefault();

                                Core.StorageType storageType = Core.StorageType.Scratch;
                                if (storageDeclaration != null)
                                {
                                    var st = storageDeclaration.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).FirstOrDefault();
                                    storageType = (Core.StorageType)st.Value;
                                }

                                var referenceSymbol = semanticModel
                                     .GetSymbolInfo(rvs)
                                     .Symbol;
                                if (referenceSymbol != null)
                                {
                                    rhs.Reverse();
                                    TryStateOperation(() => State.LoadFromGroupInnerTransaction(referenceSymbol, String.Join(".", rhs), storageType), currentNode, currentNode.Name.ToString(), false);
                                }

                            }
                            else
                            {
                                var diagnostic = DiagnosticDescriptors.Create("E020", node.GetLocation(), messageArgs: node.Kind().ToString());
                                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            }

                        }
                        else
                        {
                            var diagnostic = DiagnosticDescriptors.Create("E020", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        }
                        #endregion
                    }



                }



                //****** OLD CODE ***************
                //if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                //    node.Expression is IdentifierNameSyntax referenceVariableSyntax)
                //{



                //    var storageDeclaration = semanticModel
                //        .GetSymbolInfo(node)
                //        .Symbol
                //        ?.GetAttributes()
                //        .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                //        .FirstOrDefault();

                //    StorageType storageType = StorageType.Scratch;
                //    if (storageDeclaration != null)
                //    {
                //        var st = storageDeclaration.ConstructorArguments.Where(kv => kv.Type.Name == nameof(StorageType)).FirstOrDefault();
                //        storageType = (StorageType)st.Value;
                //    }

                //    var referenceSymbol = semanticModel
                //         .GetSymbolInfo(referenceVariableSyntax)
                //         .Symbol;
                //    if (referenceSymbol != null)
                //    {
                //        TryStateOperation(() => State.LoadUsingPredefined(referenceSymbol, node.Name, storageType), node, node.Name.ToString(), false);
                //    }

                //}
                //else
                //{

                //}






            });
        }



        public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            visit(node, () =>
            {
                var symbol = semanticModel.GetDeclaredSymbol(node);
                bool error = TryStateOperation(() => State.LabeledStatement(symbol), node, symbol.Name, false);

                if (!error)
                {
                    base.VisitLabeledStatement(node);

                }

            });

        }
        public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
        {
            visit(node, () =>
            {

                if (node.Operand is LiteralExpressionSyntax literal)
                {
                    //the operand of a unary operator is a literal - the interpretation of the value can be done at compile time

                    switch (node.Kind())
                    {
                        case SyntaxKind.UnaryMinusExpression:
                            //TODO - check numeric promotion
                            handleNumericLiteral(literal, UnaryModifier.Minus);
                            break;
                        case SyntaxKind.UnaryPlusExpression:
                            //TODO - check numeric promotion
                            handleNumericLiteral(literal, UnaryModifier.None);
                            break;
                        case SyntaxKind.BitwiseNotExpression:
                            //TODO - check numeric promotion
                            handleNumericLiteral(literal, UnaryModifier.OnesComplement);
                            break;
                        case SyntaxKind.LogicalNotExpression:
                            handleNumericLiteral(literal, UnaryModifier.LogicalNegate);
                            break;
                        default:
                            var diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            break;



                    }

                }
                else
                {

                    //TODO !! Tests for combining UnaryModifiers, eg PreIncrementExpressions int y = - --x or y=-(x++) etc
                    var typeName = semanticModel.GetTypeInfo(node.Operand).ConvertedType.Name;


                    Diagnostic diagnostic;
                    switch (node.Kind())
                    {
                        case SyntaxKind.UnaryMinusExpression:
                            Visit(node.Operand);
                            TryStateOperation(() => State.UnaryOperator(UnaryModifier.Minus, typeName), node, "Unary operator", false);
                            break;
                        case SyntaxKind.UnaryPlusExpression:
                            //no modifier
                            Visit(node.Operand);
                            break;
                        case SyntaxKind.BitwiseNotExpression:
                            Visit(node.Operand);
                            TryStateOperation(() => State.UnaryOperator(UnaryModifier.OnesComplement, typeName), node, "Unary operator", false);
                            break;
                        case SyntaxKind.LogicalNotExpression:
                            Visit(node.Operand);
                            TryStateOperation(() => State.UnaryOperator(UnaryModifier.LogicalNegate, typeName), node, "Unary operator", false);
                            break;
                        case SyntaxKind.PreIncrementExpression:
                            if (node.Operand is IdentifierNameSyntax || node.Operand is ParenthesizedExpressionSyntax)
                            {
                                var operandSymbol = semanticModel.GetSymbolInfo(node.Operand).Symbol;
                                if (operandSymbol != null)
                                {

                                    TryStateOperation(() => State.ReadFromVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                                    TryStateOperation(() => State.Increment(typeName), node, "Increment", false);
                                    TryStateOperation(() => State.YieldExpressionResult(), node.Operand, operandSymbol.Name, false);
                                    TryStateOperation(() => State.StoreToVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                                }
                            }
                            else
                            {
                                //TODO - add support for Indexers eg t[1]++; 


                                diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            }

                            break;
                        case SyntaxKind.PreDecrementExpression:
                            if (node.Operand is IdentifierNameSyntax || node.Operand is ParenthesizedExpressionSyntax)
                            {
                                var operandSymbol = semanticModel.GetSymbolInfo(node.Operand).Symbol;
                                if (operandSymbol != null)
                                {
                                    TryStateOperation(() => State.ReadFromVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                                    TryStateOperation(() => State.Decrement(typeName), node, "Decrement", false);
                                    TryStateOperation(() => State.YieldExpressionResult(), node.Operand, operandSymbol.Name, false);
                                    TryStateOperation(() => State.StoreToVariable(operandSymbol), node.Operand, operandSymbol.Name, false);
                                }
                            }
                            else
                            {
                                //TODO - add support for Indexers eg t[1]++; 


                                diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            }
                            break;
                        default:
                            diagnostic = DiagnosticDescriptors.Create("E009", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            break;


                    }


                }

            });

        }
        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {


            visit(node, () =>
            {

                Core.VariableType machineValueType = Core.VariableType.Unsupported;
                Core.StorageType storageType = Core.StorageType.Scratch;


                //determine type
                machineValueType = TealTypeUtils.DetermineType(semanticModel, node.Type);
                if (machineValueType == Core.VariableType.Unsupported)
                {
                    var diagnostic = DiagnosticDescriptors.Create("E008", node.GetLocation(), messageArgs: semanticModel.GetTypeInfo(node.Type).Type.Name);
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    return;
                }
                foreach (var declarator in node.Variables)
                {
                    var storageDeclaration = semanticModel
                      .GetDeclaredSymbol(declarator)
                      .GetAttributes()
                      .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                      .FirstOrDefault();

                    if (storageDeclaration != null)
                    {
                        var st = storageDeclaration.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).First();
                        storageType = (Core.StorageType)st.Value;
                    }
                    else
                    {
                        storageType = Core.StorageType.Scratch;
                    }

                    var declaratorSymbol = semanticModel.GetDeclaredSymbol(declarator);


                    //do the right hand side first, if there's one
                    if (declarator.Initializer != null) base.Visit(declarator.Initializer);


                    switch (storageType)
                    {

                        case Core.StorageType.Global:
                            TryStateOperation(() => State.DeclareGlobalVariable(declaratorSymbol, machineValueType), declarator, declarator.Identifier.ToString(), false);
                            break;
                        case Core.StorageType.Local:
                            TryStateOperation(() => State.DeclareLocalVariable(declaratorSymbol, machineValueType), declarator, declarator.Identifier.ToString(), false);
                            break;
                        case Core.StorageType.Scratch:
                            switch (machineValueType)
                            {
                                case Core.VariableType.ValueTuple:

                                    TryStateOperation(() => State.DeclareValueTupleVariable(declaratorSymbol, semanticModel), declarator, declarator.Identifier.ToString(), false);
                                    break;

                                case Core.VariableType.InnerTransaction:
                                    TryStateOperation(() => State.DeclareInnerTransactionVariable(declaratorSymbol, semanticModel), declarator, declarator.Identifier.ToString(), false);

                                    return;
                                default:
                                    TryStateOperation(() => State.DeclareScratchVariable(declaratorSymbol, machineValueType), declarator, declarator.Identifier.ToString(), false);
                                    break;

                            }
                            break;

                    }

                    //assign if there is something to assign
                    if (declarator.Initializer != null)
                    {
                        var leftSymbol = semanticModel.GetDeclaredSymbol(declarator);
                        TryStateOperation(() => State.StoreToVariable(leftSymbol), declarator, declarator.Identifier.ValueText, false);
                    }

                    

                }
            });
        }
        //public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        //{
        //    visit(node, () =>
        //    {

        //        if (node.Initializer != null)
        //        {


        //            base.Visit(node.Initializer);
        //            var leftSymbol = semanticModel.GetDeclaredSymbol(node);
        //            TryStateOperation(() => State.StoreToVariable(leftSymbol), node, node.Identifier.ValueText, false);


        //        }
        //    });
        //}
        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            visit(node, () =>
            {
                if (node.Expression != null)
                {
                    bool expressionReturnsVoid = false;
                    var typeInfo = semanticModel.GetTypeInfo(node.Expression);
                    var type = typeInfo.Type;
                    if (type != null && type.SpecialType == SpecialType.System_Void)
                    {
                        expressionReturnsVoid = true;
                    }
                    bool error = TryStateOperation(() => State = State.EnterExpressionEvaluationScope(expressionReturnsVoid), node.Expression, "");
                    if (!error)
                    {
                        Visit(node.Expression); //get expression
                        TryStateOperation(() => State.Return(node.Expression != null), node, "", false);
                        TryStateOperation(() => State = State.Leave(), node.Expression, "");

                    }
                }


            });
        }
        public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            visit(node, () =>
            {
                TryStateOperation(() => State.Continue(), node, "", false);
            });
        }
        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            visit(node, () =>
            {
                TryStateOperation(() => State.Break(), node, "", false);
            });
        }
        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            visit(node, () =>
            {
                bool error = TryStateOperation(() => State = State.EnterLoopScope(), node, "");
                if (!error)
                {
                    TryStateOperation(() => State.LoopStatement(), node, "", false);
                    TryStateOperation(() => State.LoopContinuePoint(), node, "", false);

                    Visit(node.Condition);

                    TryStateOperation(() => State.LoopCondition(), node, "", false);

                    Visit(node.Statement);






                    TryStateOperation(() => State = State.Leave(), node, "");
                }
            });

        }
        public override void VisitDoStatement(DoStatementSyntax node)
        {
            visit(node, () =>
            {
                bool error = TryStateOperation(() => State = State.EnterLoopScope(), node, "");
                if (!error)
                {
                    TryStateOperation(() => State.LoopStatement(), node, "", false);
                    TryStateOperation(() => State.LoopContinuePoint(), node, "", false);

                    Visit(node.Statement);

                    Visit(node.Condition);

                    TryStateOperation(() => State.LoopCondition(), node, "", false);

                    TryStateOperation(() => State = State.Leave(), node, "");
                }

            });
        }
        public override void VisitForStatement(ForStatementSyntax node)
        {
            visit(node, () =>
            {

                bool error = TryStateOperation(() => State = State.EnterLoopScope(), node, "");
                if (!error)
                {
                    if (node.Declaration != null) Visit(node.Declaration);

                    foreach (var init in node.Initializers)
                    {
                        Visit(init);
                    }

                    TryStateOperation(() => State.LoopStatement(), node, "", false);

                    Visit(node.Condition);

                    TryStateOperation(() => State.LoopCondition(), node, "", false);

                    Visit(node.Statement);

                    TryStateOperation(() => State.LoopContinuePoint(), node, "", false);
                    State.ExpectingExpressionReturnValue = false; //!IMPORTANT
                    try
                    {
                        foreach (var incrementor in node.Incrementors)
                        {
                            Visit(incrementor);
                        }
                    }
                    finally
                    {
                        State.ExpectingExpressionReturnValue = true;
                    }


                    TryStateOperation(() => State = State.Leave(), node, "");
                }






            });
        }

        public override void VisitBlock(BlockSyntax node)
        {
            visit(node, () =>
            {

                bool error = TryStateOperation(() => State = State.EnterCodeBlockScope(), node, "");
                if (!error)
                {
                    base.VisitBlock(node);
                    TryStateOperation(() => State = State.Leave(), node, "");
                }
            });
        }

        public override void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            visit(node, () =>
            {
                if (node.ArgumentList.Arguments.Count > 1)
                {
                    throw new ErrorDiagnosticException("E066");
                }

                Visit(node.Expression);                     // get the actual value first...
                Visit(node.ArgumentList);                   // get the index

                var referenceSymbol = semanticModel
                           .GetSymbolInfo(node.Expression)
                           .Symbol;
                if (referenceSymbol != null)
                {
                    var expressionType = semanticModel.GetTypeInfo(node.Expression).Type;
                    TryStateOperation(() => State.LoadFromArrayElement(expressionType), node, "");
                }
            });
        }
        public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {

            visit(node, () =>
            {

                if (node.IsKind(SyntaxKind.SimpleAssignmentExpression))
                {

                    //Handle reference variables, like SmartContractReference (even though assignment is not permitted in current Algorand versions).
                    //TODO - check
                    if (node.Left is MemberAccessExpressionSyntax memberAccessSyntax)
                    {
                        if (memberAccessSyntax.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                            memberAccessSyntax.Expression is IdentifierNameSyntax referenceVariableSyntax)
                        {

                            var storageDeclaration = semanticModel
                                .GetSymbolInfo(memberAccessSyntax)
                                .Symbol
                                ?.GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                                .FirstOrDefault();

                            Core.StorageType storageType = Core.StorageType.Scratch;
                            if (storageDeclaration != null)
                            {
                                var st = storageDeclaration.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).First();
                                storageType = (Core.StorageType)st.Value;
                            }
                            else
                            {
                                var diagnostic = DiagnosticDescriptors.Create("E021", node.GetLocation(), messageArgs: node.Kind().ToString());
                                State.ReportDiagnostic(reportDiagnostic, diagnostic);
                            }


                            var referenceSymbol = semanticModel
                                .GetSymbolInfo(referenceVariableSyntax)
                                .Symbol;

                            if (referenceSymbol != null)
                            {

                                TryStateOperation(() => State.SaveToReferencedVariable(referenceSymbol, memberAccessSyntax.Name, storageType), memberAccessSyntax, memberAccessSyntax.Name.ToString(), false);
                            }




                        }
                        else
                        {
                            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                            State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        }
                    }

                    if (node.Left is IdentifierNameSyntax)
                    {

                        base.Visit(node.Right); //TODO - why base?
                      
                        var identifierSyntax = node.Left as IdentifierNameSyntax;
                        var leftSymbol = semanticModel.GetSymbolInfo(identifierSyntax).Symbol;
                        if (leftSymbol != null)
                        {
                            TryStateOperation(() => State.YieldExpressionResult(), node.Left, (node.Left as IdentifierNameSyntax).Identifier.ValueText, false);
                            TryStateOperation(() => State.StoreToVariable(leftSymbol), node.Left, (node.Left as IdentifierNameSyntax).Identifier.ValueText, false);
                        }
                    }

                    if (node.Left is ElementAccessExpressionSyntax elementAccessSyntax)
                    {
                        Visit(elementAccessSyntax.ArgumentList); //get the index
                        Visit(node.Right);                       //get the value to store
                       
                        var referenceSymbol = semanticModel
                            .GetSymbolInfo(elementAccessSyntax.Expression)
                            .Symbol;
                        if (referenceSymbol != null)
                        {
                            TryStateOperation(() => State.YieldExpressionResult(), node.Left,"", false);
                            TryStateOperation(() => State.InvokeFromReferencedVariable(referenceSymbol, semanticModel, null, "SetAtIndex", new List<IParameterSymbol>()), elementAccessSyntax, "array set", false);
                        }

                    }

                }
                else
                {
                    var diagnostic = DiagnosticDescriptors.Create("E026", node.GetLocation(), messageArgs: node.Kind().ToString());
                    State.ReportDiagnostic(reportDiagnostic, diagnostic);
                }
            });

        }
        public override void VisitCastExpression(CastExpressionSyntax node)
        {
            visit(node, () =>
            {
                if (node != null && node.Expression != null)
                {

                    //first visit what's being casted
                    Visit(node.Expression);

                    var expressionType = semanticModel.GetTypeInfo(node.Expression).Type?.ToString()?.ToLower();
                    var castType = semanticModel.GetTypeInfo(node).Type?.ToString()?.ToLower();

                    if (expressionType == null || castType == null)
                    {
                        var diagnostic = DiagnosticDescriptors.Create("E050", node.GetLocation(), messageArgs: node.Kind().ToString());
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                    }
                    else
                    {
                        TryStateOperation(() => State.Cast(fromType: expressionType, toType: castType), node, "", false);
                    }





                }



            });


        }
        public override void VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            visit(node, () =>
            {

                var symbol = semanticModel.GetDeclaredSymbol(node.Parent.Parent);
                switch (node.Kind())
                {
                    case SyntaxKind.ArrayInitializerExpression:
                        var typeInfo = semanticModel.GetTypeInfo(node);
                        if (typeInfo.ConvertedType is IArrayTypeSymbol arrayType)
                        {
                            if (arrayType.ElementType.SpecialType == SpecialType.System_Byte)
                            {
                                bool error = TryStateOperation(() => State = State.EnterByteArrayInitialiserScope(), node, "");
                                if (!error)
                                {
                                    foreach (var child in node.Expressions)
                                    {
                                        Visit(child);
                                        error = TryStateOperation(() => State.BuildValueFromStack(symbol), node, "", false);
                                        if (error) break;
                                    }

                                    TryStateOperation(() => State = State.Leave(), node, "");
                                }
                            }
                            else
                            {
                                var err = DiagnosticDescriptors.Create("E071", node.GetLocation(), messageArgs: node.Kind().ToString());
                                State.ReportDiagnostic(reportDiagnostic, err);
                            }
                        }





                        return;
                    default:
                        var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: node.Kind().ToString());
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        break;
                }
            });

        }
        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            visit(node, () =>
            {


                switch (node.Kind())
                {
                    case SyntaxKind.FalseLiteralExpression:
                    case SyntaxKind.TrueLiteralExpression:
                    case SyntaxKind.NumericLiteralExpression:
                        handleNumericLiteral(node, TealTypeUtils.UnaryModifier.None);
                        break;
                    case SyntaxKind.StringLiteralExpression:
                        handleStringLiteral(node);
                        break;
                    case SyntaxKind.CharacterLiteralExpression:
                        break;

                    case SyntaxKind.NullLiteralExpression:
                        var diagnostic = DiagnosticDescriptors.Create("E027", node.GetLocation(), messageArgs: node.Token.ValueText);
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        return;
                    default:
                        diagnostic = DiagnosticDescriptors.Create("W001", node.GetLocation(), messageArgs: node.Token.ValueText);
                        State.ReportDiagnostic(reportDiagnostic, diagnostic);
                        break;

                }

                base.VisitLiteralExpression(node);
            });





        }
        public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            visit(node, () =>
            {
                //Not sure if to implement lambdas, and if they can be treated as subroutines accessing the source scope...


                base.VisitParenthesizedLambdaExpression(node);


            });
        }

        #endregion

        #region Unsupported syntaxes
        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E016", node.GetLocation(), messageArgs: node.Kind().ToString());
            State.ReportDiagnostic(reportDiagnostic, diagnostic);

        }
        public override void VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E016", node.GetLocation(), messageArgs: node.Kind().ToString());
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }
        public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E016", node.GetLocation(), messageArgs: node.Kind().ToString());
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }
        public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E031", node.GetLocation(), messageArgs: "' array creation'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E031", node.GetLocation(), messageArgs: "'implicit array creation'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E031", node.GetLocation(), messageArgs: "'implicit stackalloc array creation'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E031", node.GetLocation(), messageArgs: "'stackalloc array creation'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }
        public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'fixed'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {

            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'property'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'event'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'event field'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'anonymous method'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'delegate'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'await'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            var diagnostic = DiagnosticDescriptors.Create("E006", node.GetLocation(), messageArgs: "'await'");
            State.ReportDiagnostic(reportDiagnostic, diagnostic);
        }
        public override void VisitThrowExpression(ThrowExpressionSyntax node)
        {
            visit(node, () =>
            {

                var diagnostic = DiagnosticDescriptors.Create("E051", node.GetLocation(), messageArgs: "");
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
            });
        }
        public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            visit(node, () =>
            {

                var diagnostic = DiagnosticDescriptors.Create("E051", node.GetLocation(), messageArgs: "");
                State.ReportDiagnostic(reportDiagnostic, diagnostic);
            });
        }




        #endregion

    }
}
