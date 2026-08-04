using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler
{
    internal class Scope
    {
        //#BUG
        //#TODO Problem with multiple local functions in one smart contract sharing same name
        private Scope parent = null;
        private List<Scope> childScopes = new List<Scope>();
        private Dictionary<ISymbol, Variable> scopeVariables = new Dictionary<ISymbol, Variable>(SymbolEqualityComparer.Default);
        private int scratchSpaceCounter = 0;
        
        internal ISymbol Label { get; set; }

        private Scope() { }

        public static Scope NewRootScope()
        {
            Scope scope = new Scope();

            return scope;
        }

        internal Scope NewChildScope()
        {
            var newScope = new Scope();
            newScope.parent = this;

            childScopes.Add(newScope);

            return newScope;
        }

        internal bool IsSymbolisedBy(ISymbol label)
        {
            return label.Equals(Label, SymbolEqualityComparer.Default);
        }


        internal IEnumerable<Scope> AllSubScopes
        {
            get{
                return childScopes.SelectMany(child => child.AllSubScopes).Concat(childScopes);
            }
        }

        internal Scope Root
        {
            get
            {
                return parent?.Root ?? this;
            }
        }

        internal IEnumerable<Scope> AllVisibleSuperScopes
        {
            get
            {
                if (parent != null)
                {
                    return parent.childScopes.Concat(parent.AllVisibleSuperScopes);
                }
                return new List<Scope>() { this };
            }
        }

        

        internal (Variable,Scope)? FindVariable(ISymbol symbol)
        {

            //access the variable declared in this or parent scopes,
            //sibling scopes are not permitted to declare globals/locals
            bool foundVariable = scopeVariables.TryGetValue(symbol, out Variable variable);
            if (foundVariable)
            { 
                return (variable,this);
            }
            else
            {
                return parent?.FindVariable(symbol);
            }

        }



        internal AccountRefVariable AddAccountRefVariable(IParameterSymbol param)
        {
            var variable = new AccountRefVariable(scratchSpaceCounter.ToString());
            scopeVariables.Add(param, variable);
            scratchSpaceCounter++;
            return variable;
        }

        internal ApplicationRefVariable AddApplicationRefVariable(IParameterSymbol param)
        {
            var variable = new ApplicationRefVariable(scratchSpaceCounter.ToString());
            scopeVariables.Add(param, variable);
            scratchSpaceCounter++;
            return variable;
        }


        internal TransactionRefVariable AddTransactionRefVariable(IParameterSymbol param)
        {

            var variable = new TransactionRefVariable(scratchSpaceCounter.ToString());
            scopeVariables.Add(param, variable);
            scratchSpaceCounter++;
            return variable;
        }

        internal InnerTransactionVariable AddInnerTransactionVariable(ISymbol param)
        {
            var variable = new InnerTransactionVariable("");
            
            scopeVariables.Add(param, variable);
            scratchSpaceCounter++;

            return variable;
        }

        internal AssetRefVariable AddAssetRefVariable(IParameterSymbol param)
        {
            var variable = new AssetRefVariable(scratchSpaceCounter.ToString());
            scopeVariables.Add(param, variable);
            scratchSpaceCounter++;
            return variable;
        }

        internal int ScratchSpaceStart
        {
            get
            {
                if (parent != null) return parent.scratchSpaceCounter + parent.ScratchSpaceStart;
                else return 0;
            }

        }
        private ITypeSymbol GetSymbolType(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Local:
                    return ((ILocalSymbol)symbol).Type;
                case SymbolKind.Parameter:
                    return ((IParameterSymbol)symbol).Type;
                case SymbolKind.Field:
                    return ((IFieldSymbol)symbol).Type;
                case SymbolKind.Property:
                    return ((IPropertySymbol)symbol).Type;
                case SymbolKind.NamedType:
                    return (INamedTypeSymbol)symbol;
                default:
                    return null;
            }
        }

        internal ScratchVariable AddScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            ScratchVariable variable;
            ITypeSymbol typeSymbol = GetSymbolType(symbol); 


            if (valueType == Core.VariableType.ABIStruct)
            {
                variable = new ABIStructScratchVariable(scratchSpaceCounter.ToString());
            }
            else
            {
                //TODO - check the ongoing rearchitecture -  is there a need anymore for different scratch variables types?
                string symbolString = typeSymbol?.ToString().ToLower();
                switch (symbolString)
                {
                    case "system.numerics.biginteger":
                        variable = new BigIntegerScratchVariable(scratchSpaceCounter.ToString());
                        break;

                    case "ulong":
                    case "long":
                    case "uint":
                    case "int":
                    case "ushort":
                    case "short":
                    case "sbyte":
                    case "byte":
                        variable = new IntegerScratchVariable(scratchSpaceCounter.ToString());

                        break;
                    case "byte[]":
                        variable = new ByteArrayScratchVariable(scratchSpaceCounter.ToString());

                        break;
                    case "string":
                        variable = new StringScratchVariable(scratchSpaceCounter.ToString());
                        break;

                    default:
                        if (AssetRefVariable.IsAssetRef(typeSymbol))
                        {
                            variable = new AssetRefVariable(scratchSpaceCounter.ToString());
                        }
                        else
                        if (AccountRefVariable.IsAccountRef(typeSymbol))
                        {
                            variable = new AccountRefVariable(scratchSpaceCounter.ToString());

                        }
                        else
                        if (ApplicationRefVariable.IsApplicationRef(typeSymbol))
                        {
                            variable = new ApplicationRefVariable(scratchSpaceCounter.ToString());
                        }
                        else
                            variable = new ScratchVariable(scratchSpaceCounter.ToString(), Core.VariableType.UInt64);
                        //if (TransactionRefVariable.IsTxRef(typeSymbol))
                        //{
                        //TODO - does this need doing?
                        //}

                        break;
                }
            }

            scopeVariables.Add(symbol, variable);
            scratchSpaceCounter++;
            
            return variable;
        }

        internal GlobalVariable AddGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            var variable = new GlobalVariable(symbol.Name,valueType);
            scopeVariables.Add(symbol, variable);
            return variable;
        }

        internal LocalVariable AddLocalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            var variable = new LocalVariable(symbol.Name,valueType);
            scopeVariables.Add(symbol, variable);
            return variable;
        }

        internal void ReadVariable(ISymbol symbol,CodeBuilder code)
        {
            var varAndScope = FindVariable(symbol);
            if (varAndScope != null)
            {
                varAndScope.Value.Item1.AddLoadCode(code,varAndScope.Value.Item2);
            }
            else
            {
                throw new ErrorDiagnosticException("E004");
            }
        }

        internal void StoreVariable(ISymbol symbol, CodeBuilder code)
        {
            var varAndScope = FindVariable(symbol);
            if (varAndScope != null)
            {
                varAndScope.Value.Item1.AddSaveCode(code, varAndScope.Value.Item2);
            }
            else
            {
                throw new ErrorDiagnosticException("E004");
            }
        }

        private int PushScratchVars(CodeBuilder code)
        {
            int count = 0;
            foreach (var localvar in scopeVariables.Select(kv => kv.Value as ScratchVariable).Where(v => v != null))
            {
                if (byte.TryParse(localvar.Name, out var scratchIndex))
                {
                    code.load(scratchIndex,this);
                    count++;
                }
                else
                {
                    throw new ErrorDiagnosticException("E012");
                }
            }
            return count;
        }

        private void PopScratchVars(CodeBuilder code)
        {
            var vars = scopeVariables.Select(kv => kv.Value as ScratchVariable).Where(v => v != null).Reverse().ToList();
            foreach (var localvar in vars)
            {
                if (byte.TryParse(localvar.Name, out var scratchIndex))
                {
                    code.store(scratchIndex,this);
                }
                else
                {
                    throw new ErrorDiagnosticException("E012");
                }
            }
            return;
        }




        /// <summary>
        /// Save the locals onto the stack because of a scope switch
        /// </summary>
        internal int PushAllScratchVars(CodeBuilder code, Scope targetScope, int count)
        {
            if (childScopes.Contains(targetScope)) return count; //we are calling from a child scope into a child of this scope, so this scope and upwards are common to both target and calling scope

            count += PushScratchVars(code);

            if (parent != null)
            {
                count = parent.PushAllScratchVars(code,targetScope, count);
            }

            return count;
        }

        /// <summary>
        /// Retrieve pushed local vars
        /// </summary>
        internal void PopAllScratchVars(CodeBuilder code, Scope targetScope)
        {
            if (childScopes.Contains(targetScope)) return; //we are calling from a child scope into a child of this scope, so this scope and upwards are common to both target and calling scope

            if (parent != null)
            {
                parent.PopAllScratchVars(code,targetScope);
            }

            PopScratchVars(code);


        }

        internal void StoreReferencedVariable(ISymbol referenceSymbol, SyntaxToken identifier, Core.StorageType storageType, CodeBuilder code)
        {
            var varAndScope = FindVariable(referenceSymbol);
            if (varAndScope != null)
            {
                varAndScope.Value.Item1.AddReferencedSaveCode(code, varAndScope.Value.Item2, identifier, storageType);
            }
            else
            {
                throw new ErrorDiagnosticException("E004");
            }
        }


        internal void LoadReferencedVariable(ISymbol referenceSymbol, SyntaxToken identifier, Core.StorageType storageType, CodeBuilder code)
        {
            var varAndScope = FindVariable(referenceSymbol);
            if (varAndScope != null)
            {
                varAndScope.Value.Item1.AddReferencedLoadCode(code, varAndScope.Value.Item2, identifier, storageType);
            }
            else
            {
                throw new ErrorDiagnosticException("E004");
            }
        }

        internal void InvokeReferencedVariable(ISymbol referenceSymbol, SemanticModel model, InvocationExpressionSyntax node, string identifier, CodeBuilder code, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals)
        {
            var varAndScope = FindVariable(referenceSymbol);
            if (varAndScope != null)
            {
                
                varAndScope.Value.Item1.InvokeMethod(code, varAndScope.Value.Item2, identifier, nulledOptionals, literals, node);
            }
            else
            {
                throw new ErrorDiagnosticException("E004");
            }
        }

        internal IEnumerable<GlobalVariable> GetGlobals(Core.VariableType vt)
        {
            return scopeVariables.Values.Where(v => v is GlobalVariable gv && gv.ValueType == vt).Select(v=>(GlobalVariable)v);
        }

        internal IEnumerable<LocalVariable> GetLocals(Core.VariableType vt)
        {
            return scopeVariables.Values.Where(v => v is LocalVariable gv && gv.ValueType == vt).Select(v => (LocalVariable)v);
        }
    }
}
