using Algorand.Algod;
using Algorand.AVM.ClientGenerator.ABI.ARC32;
using AVM.ClientGenerator.ABI.ARC4;
using AVM.ClientGenerator.Clients;
using AVM.ClientGenerator.Compiler;
using AVM.ClientGenerator.Core.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVM.ClientGenerator.ABI.ARC32
{


    /// <summary>
    /// Represents an ARC32 app description
    /// </summary>
    [JsonConverter(typeof(AppDescriptionConverter))]
    public class AppDescription
    {
        #region Members
        public ContractDescription Contract { get; set; } = new ContractDescription();

        public StateDescription State { get; set; } = new StateDescription();

        public CallConfigSpec Bare_call_config { get; set; }

        public Dictionary<string, HintSpec> Hints = new Dictionary<string, HintSpec>();
        /// <summary>
        /// The ARC32 source attribute
        /// </summary>
        [Newtonsoft.Json.JsonProperty("source")]
        public SourceDescription Source { get; set; } = new SourceDescription();
        #endregion

        #region Methods



        public void SaveToFile(string filename)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            File.WriteAllText(filename, JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            }));
        }

        public static AppDescription LoadFromFile(string fileName)
        {

            if (File.Exists(fileName))
            {
                var jsonFile = File.ReadAllText(fileName);
                try
                {
                    AppDescription cd = JsonConvert.DeserializeObject<AppDescription>(jsonFile, new JsonSerializerSettings()
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                    return cd;
                }
                catch
                {
                    System.Diagnostics.Trace.TraceError("Unable to deserialise file to ContractDescription");
                }

            }
            return null;
        }
        public static async Task<AppDescription> LoadFromByteArray(byte[] data, IDefaultApi? algodClient = null)
        {
            try
            {
                AppDescription cd = JsonConvert.DeserializeObject<AppDescription>(Encoding.UTF8.GetString(data), new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

                if (string.IsNullOrEmpty(cd.Source.ApprovalAVM))
                {
                    var teal = Convert.FromBase64String(cd.Source.Approval);
                    var compiled = await algodClient.TealCompileAsync(new MemoryStream(teal));
                    cd.Source.ApprovalAVM = compiled.Result;
                }

                if (string.IsNullOrEmpty(cd.Source.ClearAVM))
                {
                    var teal = Convert.FromBase64String(cd.Source.Clear);
                    var compiled = await algodClient.TealCompileAsync(new MemoryStream(teal));
                    cd.Source.ClearAVM = compiled.Result;
                }

                return cd;
            }
            catch
            {
                System.Diagnostics.Trace.TraceError("Unable to deserialise file to ContractDescription");
                return null;
            }
        }




        #region Smart Contract to App Json
        public static AppDescription GenerateContractDescription(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass)
        {
            semanticModel = semanticModel ?? throw new ArgumentNullException(nameof(semanticModel));
            smartContractClass = smartContractClass ?? throw new ArgumentNullException(nameof(smartContractClass));

            AppDescription contractDescription = null;
            var classSymbol = semanticModel.GetDeclaredSymbol(smartContractClass);
            if (classSymbol != null && Utilities.IsSmartContract(classSymbol as INamedTypeSymbol))
            {

                contractDescription = new AppDescription();
                contractDescription.Contract.Name = smartContractClass.Identifier.Text;

                if (smartContractClass.HasStructuredTrivia)
                {
                    var classTrivia = smartContractClass.GetLeadingTrivia()
                                                      .Select(i => i.GetStructure())
                                                      .OfType<DocumentationCommentTriviaSyntax>()
                                                      .FirstOrDefault();

                    if (classTrivia != null)
                    {
                        var summary = classTrivia.ChildNodes()
                            .OfType<XmlElementSyntax>()
                            .Where(i => i.StartTag.Name.ToString().ToLower().Equals("summary"))
                            .FirstOrDefault();

                        if (summary != null && summary.Content != null)
                        {
                            contractDescription.Contract.Desc = summary.Content.FirstOrDefault().ToString().Trim().Replace("///", "");
                        }

                    }
                }

                defineContractDescriptionMethods(semanticModel, smartContractClass, contractDescription);
                defineContractDescriptionFields(semanticModel, smartContractClass, contractDescription);


            }

            return contractDescription;

        }

        private static void defineContractDescriptionFields(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, AppDescription contractDescription)
        {
            var fieldDeclarations = smartContractClass
                                              .DescendantNodes()
                                              .OfType<VariableDeclarationSyntax>()
                                              .SelectMany(s => s.Variables)
                                              .Select(s => (syntax: s, symbol: semanticModel.GetDeclaredSymbol(s), attribute: semanticModel.GetDeclaredSymbol(s)?
                                                            .GetAttributes()
                                                            .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
                                                            .FirstOrDefault())
                                                     )
                                              .Where(s => s.attribute != null);

            foreach (var field in fieldDeclarations)
            {
                var st = field.attribute.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).First();
                Core.StorageType storageType = (Core.StorageType)st.Value;

                switch (storageType)
                {
                    case Core.StorageType.Global: addStateVarToContractDescription(field.syntax, field.symbol, contractDescription, semanticModel, false); break;
                    case Core.StorageType.Local: addStateVarToContractDescription(field.syntax, field.symbol, contractDescription, semanticModel, true); break;
                    default:
                        throw new Exception("Unsupported field type");

                }
            }
        }

        private static void defineContractDescriptionMethods(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, AppDescription contractDescription)
        {

            var methodSyntaxes = smartContractClass
                                    .DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>();


            foreach (var methodSyntax in methodSyntaxes)
            {
                MethodDescription md = MethodDescription.FromMethod(methodSyntax, semanticModel);
                if (md != null)
                {
                    contractDescription.Contract.Methods.Add(md);
                    //also split out the arc32 hints 


                    contractDescription.Hints.Add(md.Identifier,
                        new HintSpec()
                        {
                            Call_config = md.OnCompletion,

                        });
                }

            }
        }

        private static void addStateVarToContractDescription(VariableDeclaratorSyntax syntax, ISymbol symbol, AppDescription contractDescription, SemanticModel semanticModel, bool local)
        {
            StorageElement storageElement = new StorageElement();

            if (syntax.HasStructuredTrivia)
            {
                var trivia = syntax.GetLeadingTrivia()
                                            .Select(i => i.GetStructure())
                                            .OfType<DocumentationCommentTriviaSyntax>()
                                            .FirstOrDefault();
                if (trivia != null)
                    storageElement.Descr = trivia.ToFullString().Trim();
            }

            storageElement.Type = TypeHelpers.CSTypeToAbiType((syntax.Parent as VariableDeclarationSyntax).Type, semanticModel);
            storageElement.TypeDetail = (syntax.Parent as VariableDeclarationSyntax).Type.ToString();
            storageElement.Key = syntax.Identifier.ValueText;
            string name = syntax.Identifier.ValueText;



            if (local)
            {
                if (contractDescription.State.Local == null) contractDescription.State.Local = new StorageSection();
                contractDescription.State.Local.Declared.Add(name, storageElement);
            }
            else
            {
                if (contractDescription.State.Global == null) contractDescription.State.Global = new StorageSection();
                contractDescription.State.Global.Declared.Add(name, storageElement);
            }






        }
        #endregion Smart Contract to App Json



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="nameOverride"></param>
        /// <returns></returns>
        public string ToSmartContractReference(string nameSpace, string nameOverride)
        {

            if (string.IsNullOrWhiteSpace(nameSpace)) nameSpace = "Algorand.Imports";

            string name = Contract.Name;
            if (!string.IsNullOrWhiteSpace(nameOverride)) { name = nameOverride; }

            StringBuilder crb = new StringBuilder();
            crb.AppendLine("using Algorand;");
            crb.AppendLine("using AVM.ClientGenerator.Core;");
            crb.AppendLine("using AVM.ClientGenerator.Core.Attributes; ");
            crb.AppendLine("using System; ");
            crb.AppendLine();
            crb.AppendLine($"namespace {nameSpace}");
            crb.AppendLine("{");

            if (!string.IsNullOrWhiteSpace(Contract.Desc))
            {
                crb.AppendLine(
$@"{"\t"}///<summary>
{"\t"}///{Contract.Desc?.Replace("\n", "\n///")}
{"\t"}///</summary>");
            }

            crb.AppendLine($"\tpublic abstract class {name}Reference : SmartContractReference");
            crb.AppendLine("\t{");

            List<string> structs = new List<string>();

            //declare state:
            StringBuilder stateBuilder = new StringBuilder();
            State.ToSmartContractReference(stateBuilder, structs);

            //declare methods:
            StringBuilder methodBuilder = new StringBuilder();
            foreach (var method in Contract.Methods)
            {
                methodBuilder.AppendLine();

                method.ToSmartContractReference(methodBuilder, structs);
            }

            //extract types here and declare them as local classes
            //we generate types because tuples don't allow their individual declared elements to be decorated with attributes
            //for bitwidth
            //and we don't want to make a special "bitwidthsfortuple" attribute, which would be messy.
            //cleaner to declare structs to represent tuples

            foreach (var st in structs)
            {
                crb.AppendLine(st);
            }
            crb.Append(stateBuilder.ToString());
            crb.Append(methodBuilder.ToString());
            crb.AppendLine("\t}");
            crb.AppendLine("}");

            return crb.ToString();

        }


        public string ToProxy(string namespaceName)
        {
            List<string> structs = new List<string>();


            string className = $"{Contract.Name}Proxy";

            Code code = new Code(indent: 0);
            code.AddOpeningLine("using System;");
            code.AddOpeningLine("using Algorand;");
            code.AddOpeningLine("using Algorand.Algod;");
            code.AddOpeningLine("using Algorand.Algod.Model;");
            code.AddOpeningLine("using Algorand.Algod.Model.Transactions;");
            code.AddOpeningLine("using AVM.ClientGenerator;");
            code.AddOpeningLine("using AVM.ClientGenerator.Core;");
            code.AddOpeningLine("using System.Collections.Generic;");
            code.AddOpeningLine("using System.Linq;");
            code.AddOpeningLine("using System.Text;");
            code.AddOpeningLine("using System.Threading.Tasks;");



            code.AddOpeningLine("");
            code.AddOpeningLine($"namespace {namespaceName}");
            code.AddOpeningLine("{");
            code.AddOpeningLine("");
            code.AddClosingLine("}");
            var proxyBody = code.AddChild();
            proxyBody.AddOpeningLine("");


            //Add any leading structured trivia

            if (!string.IsNullOrEmpty(Contract.Desc))
            {
                proxyBody.AddOpeningLine("//");
                proxyBody.AddOpeningLine($"// {Contract.Desc?.Replace("\n", "\n//")}");
                proxyBody.AddOpeningLine("//");
            }


            proxyBody.AddOpeningLine($"public class {className} : ProxyBase");
            proxyBody.AddOpeningLine("{");
            proxyBody.AddClosingLine("}");

            var ctor = proxyBody.AddChild();
            ctor.AddOpeningLine("");
            ctor.AddOpeningLine($"public {className}(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) ");
            ctor.AddOpeningLine("{");
            ctor.AddClosingLine("}");


            State.ToProxy(proxyBody, structs);

            defineMethods(proxyBody, structs);
            defineArc4Callers(proxyBody);

            // defineMethods (and State.ToProxy above) populate `structs` with tuple/struct declarations
            // referenced by parameter and return types (e.g. DoAppCallArgAppCallParams); those types must
            // be emitted into the proxy class body itself, since the proxy is a standalone class that does
            // not share scope with the XxxReference class that ToSmartContractReference declares them in.
            var structsBody = proxyBody.AddChild();
            foreach (var st in structs)
            {
                structsBody.AddOpeningLine(st);
            }


            var sourceVars = proxyBody.AddChild();
            sourceVars.AddOpeningLine($"protected override string SourceApproval {{ get; set; }}= \"{Source.Approval}\";");
            sourceVars.AddOpeningLine($"protected override string SourceClear {{ get; set; }} = \"{Source.Clear}\";");
            sourceVars.AddOpeningLine($"protected override string SourceApprovalAVM {{ get; set; }}= \"{Source.ApprovalAVM}\";");
            sourceVars.AddOpeningLine($"protected override string SourceClearAVM {{ get; set; }} = \"{Source.ClearAVM}\";");
            sourceVars.AddOpeningLine($"protected override ulong? GlobalNumByteSlices {{ get; set; }}={State.Global.NumByteSlices};");
            sourceVars.AddOpeningLine($"protected override ulong? GlobalNumUints {{ get; set; }}={State.Global.NumUints};");
            sourceVars.AddOpeningLine($"protected override ulong? LocalNumByteSlices {{ get; set; }}={State.Local.NumByteSlices};");
            sourceVars.AddOpeningLine($"protected override ulong? LocalNumUints {{ get; set; }}={State.Local.NumUints};");
            sourceVars.AddOpeningLine($"protected override ulong? ExtraProgramPages {{ get; set; }}={ComputeExtraProgramPages()};");

            return code.ToString();
        }

        // AVM consensus caps a single compiled program at MaxAppProgramLen (2048) bytes per page, and algod
        // rejects app creation with "approval program too long" if either compiled program exceeds
        // MaxAppProgramLen * (1 + ExtraProgramPages). goal/algod itself derives ExtraProgramPages from the
        // compiled program sizes at creation time; the generated proxy has no such runtime step (it submits a
        // fixed, pre-compiled program), so the required page count must be computed once here, at generation
        // time, from the actual compiled AVM bytecode - leaving it hardcoded at 0 silently breaks creation for
        // any contract whose compiled approval or clear program grows past a single page.
        private const int MaxAppProgramLenBytes = 2048;
        private const int MaxExtraAppProgramPages = 3;
        private ulong ComputeExtraProgramPages()
        {
            int approvalLen = string.IsNullOrEmpty(Source.ApprovalAVM) ? 0 : Convert.FromBase64String(Source.ApprovalAVM).Length;
            int clearLen = string.IsNullOrEmpty(Source.ClearAVM) ? 0 : Convert.FromBase64String(Source.ClearAVM).Length;
            int largest = Math.Max(approvalLen, clearLen);
            int extraPages = Math.Max(0, (int)Math.Ceiling(largest / (double)MaxAppProgramLenBytes) - 1);
            return (ulong)Math.Min(extraPages, MaxExtraAppProgramPages);
        }

        private void defineArc4Callers(Code proxyBody)
        {
            foreach (var method in this.Contract.Methods)
            {
                var caller = proxyBody.AddChild();
                string callerText = method.ToARC4Caller();
                caller.AddOpeningLine(callerText);
            }
        }

        private void defineMethods(Code proxyBody, List<string> structs)
        {

            foreach (var method in this.Contract.Methods)
            {



                var returnType = method.Returns;
                var methodName = method.Name;
                List<ArgumentDescription> transactionParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> appRefParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> acctRefParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> assetRefParameters = new List<ArgumentDescription>();
                List<ArgumentDescription> argParameters = new List<ArgumentDescription>();

                foreach (var parm in method.Args)
                {
                    var parmType = parm.Type;
                    if (parm.IsTransaction())
                    {
                        transactionParameters.Add(parm);
                        continue;
                    }

                    if (parm.IsApplicationRef())
                    {
                        appRefParameters.Add(parm);
                        continue;
                    }

                    if (parm.IsAccountRef())
                    {
                        acctRefParameters.Add(parm);
                        continue;
                    }

                    if (parm.IsAssetRef())
                    {
                        assetRefParameters.Add(parm);
                        continue;
                    }

                    argParameters.Add(parm);

                }

                var transactionParameterDefinitions = transactionParameters.Select(p => defineTransactionParameter(p));
                var appRefParameterDefinitions = appRefParameters.Select(p => defineAppRefParameter(p));
                var accountRefParameterDefinitions = acctRefParameters.Select(p => defineAcctRefParameter(p));
                var assetRefParameterDefinitions = assetRefParameters.Select(p => defineAssetRefParameter(p));
                var argParameterDefinitions = argParameters.Select(p => defineArgParameter(p, methodName, structs));
                var allParameters = transactionParameterDefinitions.Concat(appRefParameterDefinitions).Concat(accountRefParameterDefinitions).Concat(assetRefParameterDefinitions).Concat(argParameterDefinitions);

                string parameters = string.Join(",", allParameters);

                // ARC4 account/asset/application reference args are not ABI-encoded by value: the actual
                // account/asset/app ID goes into the txn's foreign-reference arrays (_tx_accounts/_tx_assets/
                // _tx_apps, populated below via the RefIndex block), and the ABI arg itself is just a 1-byte
                // index into that array (accounts are 1-based, since index 0 implicitly means the sender;
                // assets/apps are 0-based). Transaction-typed args carry no app-arg byte at all - they're
                // satisfied by a preceding grouped transaction instead - so they're passed through as-is here;
                // ProxyBase's argument encoder (TealTypeUtils.EncodeArgument/EncodeElement) recognizes
                // Transaction values and omits them from the encoded app-args bytes.
                string argsList = "new List<object> {" + string.Join(",", new List<string> { "abiHandle" }.Concat(method.Args.Select(p =>
                {
                    if (p.IsAccountRef() || p.IsAssetRef() || p.IsApplicationRef()) return p.Name + "RefIndex";
                    return p.Name;
                }))) + "}";

                var t = TypeHelpers.GetCSType(MethodDescription.FormatStructName(methodName + "return"), returnType.Type, returnType.TypeDetail, structs, false);
                string methodReturnType;
                if (t.type != "void")
                {
                    methodReturnType = $"Task<{t.type}>";
                }
                else
                {
                    methodReturnType = "Task";
                }

                var abiMethod = proxyBody.AddChild();
                var abiMethodForTransactions = proxyBody.AddChild();

                abiMethod.AddOpeningLine(
$@"///<summary>
        ///{method.Desc?.Replace("\n", "\n///") ?? ""}
        ///{method.OnCompletion.Summary?.Replace("\n", "\n///")}
        ///</summary>");

                foreach (var parm in method.Args)
                {
                    string parmDefaultSummary = "";
                    if (method.Defaults.ContainsKey(parm.Name))
                    {
                        parmDefaultSummary = method.Defaults[parm.Name].Summary;
                    }
                    abiMethod.AddOpeningLine($@"/// <param name=""{parm.Name}"">{parm.Desc?.Replace("\n", "\n///")} {parm.Summary?.Replace("\n", "\n///")} {parmDefaultSummary} </param>");

                }

                if (!string.IsNullOrEmpty(parameters))
                {
                    parameters += ", ";
                }

                abiMethod.AddOpeningLine($"public async {methodReturnType} {methodName} ({parameters}Account _tx_sender, ulong? _tx_fee = null,string _tx_note = \"\", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )".Replace(",,", ",").Replace(", ,", ","));
                abiMethod.AddOpeningLine("{");
                abiMethod.AddClosingLine("}");

                abiMethodForTransactions.AddOpeningLine($"public async Task<List<Transaction>> {methodName}_Transactions ({parameters}Account _tx_sender, ulong? _tx_fee = null, string _tx_note = \"\", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )".Replace(",,", ",").Replace(", ,", ","));
                abiMethodForTransactions.AddOpeningLine("{");
                abiMethodForTransactions.AddClosingLine("}");

                var abiMethodBody = abiMethod.AddChild();

                abiMethodBody.AddOpeningLine($"_tx_boxes ??= new List<BoxRef>();");
                abiMethodBody.AddOpeningLine($"_tx_transactions ??= new List<Transaction>();");
                abiMethodBody.AddOpeningLine($"_tx_assets ??= new List<ulong>();");
                abiMethodBody.AddOpeningLine($"_tx_apps ??= new List<ulong>();");
                abiMethodBody.AddOpeningLine($"_tx_accounts ??= new List<Address>();");

                if (transactionParameters.Count > 0)
                {
                    abiMethodBody.AddOpeningLine("_tx_transactions.AddRange(new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => p.Name)) + "});");
                }

                EmitRefIndexAssignments(abiMethodBody, appRefParameters, assetRefParameters, acctRefParameters);

                abiMethodBody.AddOpeningLine($"byte[] abiHandle = {{{String.Join(",", method.Selector)}}};");
                abiMethodBody.AddOpeningLine($"var result = await base.CallApp({argsList}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);");

                if (t.type != "void")
                {
                    if (ProxyGenerator.returnTypeConversions.TryGetValue(t.type, out string retline))
                    {
                        abiMethodBody.AddOpeningLine("var lastLogBytes = result.Last();");
                        abiMethodBody.AddOpeningLine("if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception(\"Invalid ABI handle\");");
                        abiMethodBody.AddOpeningLine("var lastLogReturnData = lastLogBytes.Skip(4).ToArray();");
                        if (retline.Contains("returnValueObj"))
                        {
                            abiMethodBody.AddOpeningLine($"var returnValueObj = new {t.abiType};");
                            abiMethodBody.AddOpeningLine("returnValueObj.Decode(lastLogReturnData);");
                        }
                        abiMethodBody.AddOpeningLine(retline);
                    }
                    else
                    {
                        abiMethodBody.AddOpeningLine("throw new Exception(\"Conversion not implemented\"); // <unknown return conversion>");
                    }
                }

                var abiMethodBodyForTransactions = abiMethodForTransactions.AddChild();
                abiMethodBodyForTransactions.AddOpeningLine($"_tx_boxes ??= new List<BoxRef>();");
                abiMethodBodyForTransactions.AddOpeningLine($"_tx_transactions ??= new List<Transaction>();");
                abiMethodBodyForTransactions.AddOpeningLine($"_tx_assets ??= new List<ulong>();");
                abiMethodBodyForTransactions.AddOpeningLine($"_tx_apps ??= new List<ulong>();");
                abiMethodBodyForTransactions.AddOpeningLine($"_tx_accounts ??= new List<Address>();");

                if (transactionParameters.Count > 0)
                {
                    abiMethodBodyForTransactions.AddOpeningLine("_tx_transactions.AddRange(new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => p.Name)) + "});");
                }

                EmitRefIndexAssignments(abiMethodBodyForTransactions, appRefParameters, assetRefParameters, acctRefParameters);
                abiMethodBodyForTransactions.AddOpeningLine($"byte[] abiHandle = {{{String.Join(",", method.Selector)}}};");
                abiMethodBodyForTransactions.AddOpeningLine($"return await base.MakeTransactionList({argsList}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);");
            }
        }

        /// <summary>
        /// Appends account/asset/application reference args to their respective _tx_accounts/_tx_assets/_tx_apps
        /// foreign-reference arrays, and emits a "{name}RefIndex" byte variable for each - the 1-byte ARC4-encoded
        /// index into that array that actually gets sent as the ABI argument (see the argsList construction in
        /// defineMethods). Per ARC4: asset/application reference indices are 0-based directly into
        /// Txn.Assets[]/Txn.Applications[]; account reference indices are 1-based into Txn.Accounts[], since
        /// index 0 implicitly refers to the transaction sender.
        /// </summary>
        private void EmitRefIndexAssignments(Code body, List<ArgumentDescription> appRefParameters, List<ArgumentDescription> assetRefParameters, List<ArgumentDescription> acctRefParameters)
        {
            if (appRefParameters.Count > 0)
            {
                body.AddOpeningLine("int _appRefBase = _tx_apps.Count;");
                body.AddOpeningLine("_tx_apps.AddRange(new List<ulong> {" + string.Join(",", appRefParameters.Select(p => p.Name)) + "});");
                for (int i = 0; i < appRefParameters.Count; i++)
                {
                    body.AddOpeningLine($"byte {appRefParameters[i].Name}RefIndex = (byte)(_appRefBase + {i});");
                }
            }

            if (assetRefParameters.Count > 0)
            {
                body.AddOpeningLine("int _assetRefBase = _tx_assets.Count;");
                body.AddOpeningLine("_tx_assets.AddRange(new List<ulong> {" + string.Join(",", assetRefParameters.Select(p => p.Name)) + "});");
                for (int i = 0; i < assetRefParameters.Count; i++)
                {
                    body.AddOpeningLine($"byte {assetRefParameters[i].Name}RefIndex = (byte)(_assetRefBase + {i});");
                }
            }

            if (acctRefParameters.Count > 0)
            {
                body.AddOpeningLine("int _acctRefBase = _tx_accounts.Count;");
                body.AddOpeningLine("_tx_accounts.AddRange(new List<Address> {" + string.Join(",", acctRefParameters.Select(p => p.Name)) + "});");
                for (int i = 0; i < acctRefParameters.Count; i++)
                {
                    body.AddOpeningLine($"byte {acctRefParameters[i].Name}RefIndex = (byte)(_acctRefBase + {i} + 1);");
                }
            }
        }

        private static string defineArgParameter(ArgumentDescription p, string methodName, List<string> structs)
        {
            var type = TypeHelpers.GetCSType(MethodDescription.FormatStructName(methodName + "_arg_" + p.Name), p.Type, p.TypeDetail, structs, false).type;
            return $"{type} {p.Name}";
        }

        private static string defineAssetRefParameter(ArgumentDescription p)
        {
            return $"ulong {p.Name}";
        }

        private static string defineAcctRefParameter(ArgumentDescription p)
        {
            return $"Address {p.Name}";
        }

        private static string defineTransactionParameter(ArgumentDescription p)
        {
            string parmType = p.Type.ToString();
            if (!string.IsNullOrWhiteSpace(p.TypeDetail)) parmType = p.TypeDetail;
            string outputParmType = TypeHelpers.determineTransactionType(parmType);

            return $"{outputParmType} {p.Name}";
        }


        private static string defineAppRefParameter(ArgumentDescription p)
        {
            return $"ulong {p.Name}";
        }
        #endregion
    }
}
