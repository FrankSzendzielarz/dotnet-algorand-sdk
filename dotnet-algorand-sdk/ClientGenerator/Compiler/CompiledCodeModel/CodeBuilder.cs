using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Optimisers;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace AVM.ClientGenerator.Compiler.CompiledCodeModel
{
    internal class CodeBuilder : ICompilerMemento
    {
        private static int instance = 0;
        private string instanceId = "";
        private int label = 0;
        private CodeBuilder parent;
        private bool suppressInners = false;
        protected bool? stateless = null;
        private List<CodeBuilder> childCodeBlocks = new List<CodeBuilder>();
        private List<IMethodSymbol> libraryDependencies = new List<IMethodSymbol>();
        private List<Func<CompiledLine>> codeBuilder = new List<Func<CompiledLine>>();
        private Dictionary<ISymbol, int> labeledStatements = new Dictionary<ISymbol, int>(SymbolEqualityComparer.Default);
        private Dictionary<PredefinedSubroutines, string> registeredPredefineds = new Dictionary<PredefinedSubroutines, string>();

        internal string Name { get; set; }
        internal const int CodeLineWidth = 50;

        

        internal CodeBuilder()
        { instanceId = instance.ToString(); instance++; }




        internal void AddChildCode(CodeBuilder code)
        {

            childCodeBlocks.Add(code);
            code.parent = this;
            if (!code.stateless.HasValue) code.stateless = stateless;
        }



        internal IEnumerable<CodeBuilder> allChildren()
        {
            return
                childCodeBlocks.SelectMany(c => c.allChildren()).Append(this);
        }

        internal List<ContractDeclaration> ContractDeclarations =>
            allChildren()
            .Where(c => c is SmartContractCode sc)
            .Select(c =>
                new ContractDeclaration()
                {
                    code = (SmartContractCode)c,
                    Name = ((SmartContractCode)c).Name,
                    GlobalByteCount = ((SmartContractCode)c).GetGlobalByteCount(),
                    GlobalUintCount = ((SmartContractCode)c).GetGlobalUintCount(),
                    LocalByteCount = ((SmartContractCode)c).GetLocalByteCount(),
                    LocalUintCount = ((SmartContractCode)c).GetLocalUintCount(),
                }
             )
            .ToList();

        internal List<SmartSignatureDeclaration> SigDeclarations =>
           allChildren()
           .Where(c => c is SmartSignatureCode sc)
           .Select(c =>
               new SmartSignatureDeclaration()
               {
                   code = (SmartSignatureCode)c,
                   Name = ((SmartSignatureCode)c).Name
               }
            )
           .ToList();

        /// <summary>
        /// Suppress the code block from itxn_begin commands. This is required for 
        /// the scenario that a reference to an inner transaction has been declared
        /// in an innertransactionlocalfunction scope. After that declaration no 
        /// further innertransactions are permitted (because the declaration would
        /// become invalid). Compiler states prevent entering new inners, but
        /// some predefined methods on the Account or Asset reference classes invoke
        /// itxn_begin, and these can only be suppressed at the level of TEAL generation
        /// </summary>
        internal void SuppressInnerTransactions()
        {
            suppressInners = true;
        }

        private void AddOpcode(string opcode, params object[] parameters)
        {

            codeBuilder.Add(() =>
            {
                var parms = string.Join(" ", parameters);


                CompiledLine oci = new CompiledLine()
                {
                    Line = $"\t{opcode} {parms}",

                    Opcode = opcode,
                    Parameters = parameters.Select(p => p.ToString()).ToArray(),
                    Optimisable = true
                };


                return oci;
            });

        }


        /// <summary>
        /// Get the clear state program code
        /// </summary>
        internal (string code, ulong cost) GetClearStateCode(CompilationGroup compilationGroup)
        {
            StringBuilder code = new StringBuilder();
            ulong cost = 0;

            foreach (var child in childCodeBlocks) //possible eventual partials support?
            {
                if (child is SmartContractProgramCode sc && sc.Name == "ClearStateProgram")
                {
                   // code.AppendLine($"{new string(' ', CodeLineWidth)}//Opcode size");
                    var c = child.GetApprovalProgramCode(compilationGroup,new List<CodeBuilder>());
                    code.Append(c.code);
                    cost += c.cost;
                }
            }

            return (code.ToString(), cost);
        }



        private List<CodeBuilder> childCodeBlocksAndLibraryDependencies(CompilationGroup compilationGroup, List<CodeBuilder> knownCode)
        {
            //get a list of SmartContractLibraryMethod codebuilders from the compilationGroup identified by libraryDependencies 
            List<SmartContractLibraryMethod> libraryMethods = new List<SmartContractLibraryMethod>();
            foreach (var library in libraryDependencies)
            {
                var lib = compilationGroup.LibraryMethods.Where(l => l.AssociatedScope.IsSymbolisedBy(library)).FirstOrDefault();
                if (lib != null && !knownCode.Contains(lib)) libraryMethods.Add(lib);
            }

            List<CodeBuilder> ret = libraryMethods.Concat(childCodeBlocks).ToList();
            return ret;

        }


        /// <summary>
        /// Get all code including subroutines
        /// </summary>
        internal (string code, ulong cost) GetApprovalProgramCode(CompilationGroup compilationGroup, List<CodeBuilder> knownCode)
        {
            StringBuilder code = new StringBuilder();
            ulong cost = 0;
            
            

           // code.AppendLine($"{new string(' ', CodeLineWidth)}//Opcode size");


            foreach (var codeStep in optimisedLines)
            {
                cost += codeStep.Size;
                int width = codeStep.Line.Length;
                int spacer = Math.Max(CodeLineWidth - width, 0);
                code.AppendLine($"{codeStep.Line}");// {new string(' ', spacer)}//{cost}       ");
            }

            var children = childCodeBlocksAndLibraryDependencies(compilationGroup, knownCode);
            knownCode.Add(this);

            foreach (var child in children)
            {
                if (child is SmartContractProgramCode sc)
                {
                    if (sc.Name != "ClearStateProgram")
                    {
                        var c = child.GetApprovalProgramCode(compilationGroup,knownCode);
                        code.Append(c.code);
                        cost += c.cost;
                        
                    }
                }
                else
                {
                    var c = child.GetApprovalProgramCode(compilationGroup,knownCode );
                    code.Append(c.code);
                    cost += c.cost;
                    
                }
            }

            return (code.ToString(), cost);
        }


        private List<CompiledLine> optimisedLines = new List<CompiledLine>();
        bool optimised = false;
        internal void OptimiseCode(List<IOptimiser> optimisers)
        {
            foreach (var codeStep in codeBuilder)
            {
                optimisedLines.Add(codeStep());
                do
                {
                    optimised = false;
                    foreach (var optimiser in optimisers) optimiser.LineAdded(optimisedLines.AsReadOnly().AsEnumerable(), this);

                } while (optimised);

            }
            foreach (var child in childCodeBlocks)
            {
                foreach (var optimiser in optimisers) optimiser.ChildScopeEntered();
                child.OptimiseCode(optimisers);
                foreach (var optimiser in optimisers) optimiser.ChildScopeExited();
            }
        }

        public void RemoveLineAt(int index)
        {
            optimised = true;
            //ignore potential exceptions, they will be caught by the optimisation process
            // and the optimisation cancelled for the given 'malicious' optimiser.
            optimisedLines.RemoveAt(index);
        }

        public void ReplaceLineAt(int index, CompiledLine line)
        {
            optimised = true;
            CompiledLine l = new CompiledLine()
            {

                Line = line.Line,
                Opcode = line.Opcode,
                Optimisable = line.Optimisable,
                Parameters = line.Parameters,
            };

            optimisedLines[index] = l;
        }



        public void InsertLineAt(int index, CompiledLine line)
        {
            optimised = true;
            CompiledLine l = new CompiledLine()
            {

                Line = line.Line,
                Opcode = line.Opcode,
                Optimisable = line.Optimisable,
                Parameters = line.Parameters,
            };

            optimisedLines.Insert(index, l);
        }

        public void AddLine(CompiledLine line)
        {
            optimised = true;
            optimisedLines.Add(line);
        }

        public void RemoveTopLine()
        {
            optimised = true;
            if (optimisedLines.Count > 0)
                optimisedLines.RemoveAt(optimisedLines.Count - 1);
        }






        internal void InvokeSmartContractMethod()
        {
            CheckSmartContract();
            codeBuilder.Add(() => new CompiledLine() { Size = 12, Line = invokeSmartContractMethod(), Opcode = null, Optimisable = false, Parameters = null }); ; ;
        }
        internal void InvokeSmartSignatureMethod()
        {
            CheckStateless();
            codeBuilder.Add(() => new CompiledLine() { Size = 12, Line = invokeSmartSignatureMethod(), Opcode = null, Optimisable = false, Parameters = null });
        }

        private CodeBuilder parentSmartSignature()
        {
            if (parent == null) return null;
            if (parent is SmartSignatureCode) return parent;
            return parent.parentSmartSignature();
        }

        private CodeBuilder parentSmartContract()
        {
            if (parent == null) return null;
            if (parent is SmartContractCode) return parent;
            return parent.parentSmartContract();
        }

        private CodeBuilder parentSmartContractLibraryMethod()
        {
            if (this is SmartContractLibraryMethod) return this;
            if (parent == null) return null;
            return parent.parentSmartContractLibraryMethod();
        }

        private string invokeSmartContractMethod()
        {
            StringBuilder boilerplate = new StringBuilder();

            var parentContract = parentSmartContract();

            if (parentContract != null)
            {
                string exitLabel = ReserveLabel();

                boilerplate.AppendLine($"\tint 0");
                boilerplate.AppendLine("\ttxn NumAppArgs");
                boilerplate.AppendLine($"\t==");
                boilerplate.AppendLine($"\tbnz {exitLabel}");

                //scan this code body for sub-codes that are smart contract methods
                foreach (SmartContractMethodCode child in parentContract.allChildren().Where(c => c is SmartContractMethodCode))
                {
                    string occheckLabel = ReserveLabel();
                    boilerplate.AppendLine("\ttxn OnCompletion");
                    boilerplate.AppendLine($"\tint {child.OnCompletion}");
                    boilerplate.AppendLine("\t==");
                    boilerplate.AppendLine($"\tbz {occheckLabel} ");
                    boilerplate.AppendLine("\ttxna ApplicationArgs 0");
                    boilerplate.AppendLine($"\tbyte {child.Name}");
                    boilerplate.AppendLine("\t==");
                    boilerplate.AppendLine($"\tbnz {child.Label}");
                    boilerplate.AppendLine($"\t{occheckLabel}:");
                    boilerplate.AppendLine();
                }

                boilerplate.AppendLine($"\t{exitLabel}:");

                return boilerplate.ToString();
            }
            else
            {
                throw new ErrorDiagnosticException("E019");
            }



        }

        private string invokeSmartSignatureMethod()
        {
            StringBuilder boilerplate = new StringBuilder();

            var parentSignature = parentSmartSignature();

            if (parentSignature != null)
            {


                //scan this code body for sub-codes that are smart sig methods
                foreach (SmartSignatureMethodCode child in parentSignature.allChildren().Where(c => c is SmartSignatureMethodCode))
                {

                    boilerplate.AppendLine("\targ 0");
                    boilerplate.AppendLine($"\tbyte {child.Name}");
                    boilerplate.AppendLine("\t==");
                    boilerplate.AppendLine($"\tbnz {child.Label}");

                    boilerplate.AppendLine();
                }



                return boilerplate.ToString();
            }
            else
            {
                throw new ErrorDiagnosticException("E054");
            }



        }

        private int CurrentLabel
        {
            get
            {


                var root = parentSmartContract() ?? parentSmartSignature()??parentSmartContractLibraryMethod();
                if (root == null)
                {
                    throw new Exception("Code not attached to root code.");
                }
                return root.label;


            }
        }

        private void AdvanceLabel()
        {

            var root = parentSmartContract() ?? parentSmartSignature() ?? parentSmartContractLibraryMethod();
            if (root == null)
            {
                throw new Exception("Code not attached to root code.");
            }
            root.label++;
        }

        internal string ReserveLabel()
        {
            string lab = $"Label{CurrentLabel}";
            var smartContractLibraryMethod = parentSmartContractLibraryMethod();
            if (smartContractLibraryMethod != null)
            {
                lab= smartContractLibraryMethod.Name + "_" + label;
            }
            AdvanceLabel();
            return lab;
        }



        internal (CodeBuilder code, string label) RegisterPredefined(PredefinedSubroutines predefined)
        {
            if (parent != null && !(parent is SmartContractCode || parent is SmartSignatureCode)) return parent.RegisterPredefined(predefined);

            if (registeredPredefineds.ContainsKey(predefined)) return (null, registeredPredefineds[predefined]);

            var label = $"{predefined}{instanceId}";
            CodeBuilder predefinedCode = new CodeBuilder();
            AddChildCode(predefinedCode);
            predefinedCode.AddLabel(label);
            registeredPredefineds[predefined] = label;

            return (predefinedCode, label);

        }

        internal void AddLabel(string label)
        {
            codeBuilder.Add(() => new CompiledLine() { Line = $"{label}:", Opcode = "Label", Optimisable = false, Parameters = null });
        }

        internal void AddComment(string comment)
        {
            codeBuilder.Add(() => new CompiledLine() { Line = $"//{comment}\t", Opcode = "", Optimisable = false, Parameters = null });

        }

        internal void AddEmptyLine()
        {
            codeBuilder.Add(() => new CompiledLine() { Line = "", Opcode = "", Optimisable = false, Parameters = null });
        }



        internal void byte_string_literal(string constant) { AddOpcode("byte", $"\"\"{constant}\"\""); }
        internal void byte_literal_constant(string constant) { AddOpcode("byte", constant); }
        internal void int_literal_constant(ulong constant) { AddOpcode("int", constant); }
        internal void int_literal_constant(string constant) { AddOpcode("int", constant); }

        internal string GetLabeledStatement(ISymbol symbol)
        {
            string prefix="labeled";
            var scm = parentSmartContractLibraryMethod();
            if (scm!=null)
            {
                prefix = scm.Name;
            }

            if (labeledStatements.TryGetValue(symbol, out int l))
            {
                return $"{prefix}{l}";
            }
            else
            {

                labeledStatements.Add(symbol, CurrentLabel);
                var lab = $"{prefix}{CurrentLabel}";
                AdvanceLabel();
                return lab;
            }
        }

        internal void err() { AddOpcode("err"); }
        internal void sha256() { AddOpcode("sha256"); }
        internal void keccak256() { AddOpcode("keccak256"); }
        internal void sha512_256() { AddOpcode("sha512_256"); }
        internal void ed25519verify() { AddOpcode("ed25519verify"); }
        internal void ecdsa_verify(string curve) { AddOpcode("ecdsa_verify", curve); }
        internal void ecdsa_pk_decompress(string curve) { AddOpcode("ecdsa_pk_decompress", curve); }
        internal void ecdsa_pk_recover(string curve) { AddOpcode("ecdsa_pk_recover", curve); }

        internal void plus() { AddOpcode("+"); }
        internal void minus() { AddOpcode("-"); }
        internal void divide() { AddOpcode("/"); }
        internal void multiply() { AddOpcode("*"); }
        internal void less_than() { AddOpcode("<"); }
        internal void greater_than() { AddOpcode(">"); }
        internal void less_than_or_equals() { AddOpcode("<="); }
        internal void greater_than_or_equals() { AddOpcode(">="); }
        internal void and() { AddOpcode("&&"); }   //these aren't used
        internal void or() { AddOpcode("||"); }   // these aren't used
        internal void equals() { AddOpcode("=="); }
        internal void not_equals() { AddOpcode("!="); }
        internal void not() { AddOpcode("!"); } // not used
        internal void len() { AddOpcode("len"); }
        internal void itob() { AddOpcode("itob"); }
        internal void btoi() { AddOpcode("btoi"); }
        internal void modulo() { AddOpcode("%"); }
        internal void bitwise_or() { AddOpcode("|"); }
        internal void bitwise_and() { AddOpcode("&"); }
        internal void bitwise_xor() { AddOpcode("^"); }
        internal void bitwise_not() { AddOpcode("~"); }
        internal void mulw() { AddOpcode("mulw"); }
        internal void addw() { AddOpcode("addw"); }
        internal void divmodw() { }
        internal void intcblock() { }
        internal void intc() { }
        internal void intc_0() { }
        internal void intc_1() { }
        internal void intc_2() { }
        internal void intc_3() { }
        internal void bytecblock() { }
        internal void bytec() { }
        internal void bytec_0() { }
        internal void bytec_1() { }
        internal void bytec_2() { }
        internal void bytec_3() { }
        internal void arg(int i) { AddOpcode("arg", i); }
        internal void arg_0() { AddOpcode("arg_0"); }
        internal void arg_1() { AddOpcode("arg_1"); }
        internal void arg_2() { AddOpcode("arg_2"); }
        internal void arg_3() { AddOpcode("arg_3"); }
        internal void txn(string i) { AddOpcode("txn", i); }
        internal void txn_arrayfield(string arrayfield, int i) { AddOpcode("txn", arrayfield, i); }
        internal void global(byte i) { AddOpcode("global", i); }
        internal void global(string i) { AddOpcode("global", i); }
        internal void gtxn(byte i, string field) { AddOpcode("gtxn", i, field); }

        internal void gtxns(string field) { AddOpcode("gtxns", field); }
        internal void gtxnsa(string field, byte i) { AddOpcode("gtxnsa", field, i); }


        internal void load(byte index, Scope scope)
        {
            var p = (index + scope.ScratchSpaceStart).ToString();
            codeBuilder.Add(() => new CompiledLine() { Opcode = "load", Optimisable = true, Parameters = new string[] { p }, Line = $"\tload {index + scope.ScratchSpaceStart}" });
        }
        internal void store(byte index, Scope scope)
        {
            var p = (index + scope.ScratchSpaceStart).ToString();
            codeBuilder.Add(() => new CompiledLine() { Opcode = "store", Optimisable = true, Parameters = new string[] { p }, Line = $"\tstore {index + scope.ScratchSpaceStart}" });
        }

        internal void loadabsolute(byte index)
        {
            codeBuilder.Add(() => new CompiledLine() { Opcode = "load", Optimisable = true, Parameters = new string[] { index.ToString() }, Line = $"\tload {index}" });
        }
        internal void storeabsolute(byte index)
        {
            codeBuilder.Add(() => new CompiledLine() { Opcode = "store", Optimisable = true, Parameters = new string[] { index.ToString() }, Line = $"\tstore {index}" });
        }
        internal void txna(string array, byte index)
        {

            AddOpcode("txna", array, index);

        }
        internal void gtxna(byte i, string arr, string field) { AddOpcode("gtxn", i, arr, field); }

        internal void gitxn(byte t, string field) { CheckSmartContract(); AddOpcode("gitxn", t, field); }
        internal void gitxna(byte t, string field, byte index) { CheckSmartContract(); AddOpcode("gitxn", t, field, index); }

        internal void gload() { }
        internal void gloads() { }
        internal void gaid() { }
        internal void gaids() { }
        internal void loads() { AddOpcode("loads"); }
        internal void stores() { AddOpcode("stores"); }
        internal void bnz(string target) { AddOpcode("bnz", target); }
        internal void bz(string target) { AddOpcode("bz", target); }
        internal void b(string target) { AddOpcode("b", target); }

        internal void ret() { AddOpcode("return"); }
        internal void assert() { }
        internal void pop() { AddOpcode("pop"); }
        internal void dup() { AddOpcode("dup"); }
        internal void dupn(int n) { AddOpcode("dupn", n); }
        internal void dup2() { AddOpcode("dup2"); }
        internal void dig(int n) { AddOpcode("dig", n); }
        internal void swap() { AddOpcode("swap"); }
        internal void select() { }
        internal void cover(int n) { AddOpcode("cover", n); }
        internal void uncover(int n) { AddOpcode("uncover", n); }
        internal void concat() { AddOpcode("concat"); }
        internal void substring(int s, int e) { AddOpcode("substring", s, e); }
        internal void substring3() { AddOpcode("substring3"); }
        internal void getbit() { AddOpcode("getbit"); }
        internal void setbit() { }
        internal void getbyte() { AddOpcode("getbyte"); }
        internal void setbyte() { AddOpcode("setbyte"); }
        internal void extract(int s, int l) { AddOpcode("extract", s, l); }
        internal void extract3() { AddOpcode("extract3"); }

        internal void replace2(int s) { AddOpcode("replace2", s); }
        internal void replace3() { AddOpcode("replace3"); }
        internal void extract_uint16() { AddOpcode("extract_uint16"); }
        internal void extract_uint32() { AddOpcode("extract_uint32"); }
        internal void extract_uint64() { AddOpcode("extract_uint64"); }
        internal void balance() { CheckSmartContract(); AddOpcode("balance"); }
        internal void app_opted_in() { CheckSmartContract(); AddOpcode("app_opted_in"); }
        internal void app_local_get() { CheckSmartContract(); AddOpcode("app_local_get"); }
        internal void app_local_get_ex() { CheckSmartContract(); AddOpcode("app_local_get_ex"); }
        internal void app_global_get() { CheckSmartContract(); AddOpcode("app_global_get"); }
        internal void app_global_get_ex() { CheckSmartContract(); AddOpcode("app_global_get_ex"); }
        internal void app_local_put() { CheckSmartContract(); AddOpcode("app_local_put"); }
        internal void app_global_put() { CheckSmartContract(); AddOpcode("app_global_put"); }
        internal void app_local_del() { CheckSmartContract(); }           //TODO ROADMAP
        internal void app_global_del() { CheckSmartContract(); }          //TODO ROADMAP
        internal void asset_holding_get(string i) { CheckSmartContract(); AddOpcode("asset_holding_get", i); }
        internal void asset_params_get(string i) { CheckSmartContract(); AddOpcode("asset_params_get", i); }
        internal void app_params_get(string i) { CheckSmartContract(); AddOpcode("app_params_get", i); }
        internal void pushbytes(string bytes) { AddOpcode("pushbytes", $"\"\"{bytes}\"\""); }
        internal void pushint() { }
        internal void callsub(string name) { AddOpcode("callsub", name); }
        internal void retsub() { AddOpcode("retsub"); }
        internal void shl() { AddOpcode("shl"); }
        internal void shr() { AddOpcode("shr"); }
        internal void sqrt() { AddOpcode("sqrt"); }
        internal void exp() { AddOpcode("exp"); }
        internal void expw() { AddOpcode("expw"); }
        internal void b_plus() { AddOpcode("b+"); }
        internal void b_minus() { AddOpcode("b-"); }

        internal void b_divide() { AddOpcode("b/"); }
        internal void b_multiply() { AddOpcode("b*"); }
        internal void b_less_than() { AddOpcode("b<"); }
        internal void b_greater_than() { AddOpcode("b>"); }
        internal void b_less_than_or_equal() { AddOpcode("b<="); }
        internal void b_greater_than_or_equal() { AddOpcode("b>="); }
        internal void b_equals() { AddOpcode("b=="); }
        internal void b_not_equals() { AddOpcode("b!="); }
        internal void b_modulo() { AddOpcode("b%"); }
        internal void b_bitwise_or() { AddOpcode("b|"); }
        internal void b_bitwise_and() { AddOpcode("b&"); }
        internal void b_bitwise_xor() { AddOpcode("b^"); }
        internal void b_bitwise_not() { AddOpcode("b~"); }
        internal void bzero() { AddOpcode("bzero"); }
        internal void log() { CheckSmartContract(); AddOpcode("log"); }
        internal void itxn_begin() { CheckSmartContract(); if (!suppressInners) AddOpcode("itxn_begin"); else throw new ErrorDiagnosticException("E044"); }

        internal void itxn_next() { CheckSmartContract(); AddOpcode("itxn_next"); }
        internal void itxn_field(string f) { CheckSmartContract(); AddOpcode("itxn_field", f); }
        internal void itxn_submit() { CheckSmartContract(); AddOpcode("itxn_submit"); }
        internal void itxn(string f) { CheckSmartContract(); AddOpcode("itxn", f); }
        internal void itxna() { }
        internal void txnas(string f) { CheckSmartContract(); AddOpcode("txnas", f); }
        internal void gtxnas() { }
        internal void gtxnsas() { }

        internal void box_del() { CheckSmartContract(); AddOpcode("box_del"); }
        internal void box_put() { CheckSmartContract(); AddOpcode("box_put"); }
        internal void box_len() { CheckSmartContract(); AddOpcode("box_len"); }
        internal void box_get() { CheckSmartContract(); AddOpcode("box_get"); }
        internal void args() { CheckStateless(); AddOpcode("args"); }

        private void CheckStateless()
        {
            if (stateless != null && !stateless.Value) throw new ErrorDiagnosticException("E053");
        }
        private void CheckSmartContract()
        {
            if (stateless != null && stateless.Value) throw new ErrorDiagnosticException("E052");
        }

        internal void bitlen() { AddOpcode("bitlen"); }

        internal void MinBalance() { CheckSmartContract(); AddOpcode("min_balance"); }

        internal void RegisterLibraryDependency(IMethodSymbol invocationSymbol)
        {
            if (this.libraryDependencies.Any(l => l.Equals(invocationSymbol,SymbolEqualityComparer.Default))) return;

            this.libraryDependencies.Add(invocationSymbol);
        }
    }
}
