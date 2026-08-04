using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AVM.ClientGenerator.Core.Attributes;
using AVM.ClientGenerator.Compiler.Variables;
using Org.BouncyCastle.Crypto.Digests;
using AVM.ClientGenerator.ABI.ARC32;
using AVM.ClientGenerator.ABI.ARC4.Types;
using Algorand.AVM.ClientGenerator.Extensions;


namespace AVM.ClientGenerator.ABI.ARC4
{

    public class MethodDescription
    {
        [JsonRequired]
        public string Name { get; set; }

        public string Desc { get; set; }

        [JsonIgnore]
        public CallConfigSpec OnCompletion { get; set; } = new CallConfigSpec();
        [JsonIgnore]
        public Dictionary<string, DefaultArgumentSpec> Defaults { get; set; } = new Dictionary<string, DefaultArgumentSpec>();

        public List<ArgumentDescription> Args { get; set; } = new List<ArgumentDescription> { };

        public byte[] Selector { get; set; }

        [JsonIgnore]
        public string Identifier { get; set; }

        public ReturnTypeDescription Returns { get; set; }


        public static MethodDescription FromMethod(MethodDeclarationSyntax ms, SemanticModel model)
        {
            var methodSymbol = model.GetDeclaredSymbol(ms);

            MethodDescription md = null;
            var ABImethod = methodSymbol
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(SmartContractMethodAttribute))
                                .FirstOrDefault();
            if (ABImethod != null)
            {
                md = new MethodDescription();


                var callTypeConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.OnCompleteType)).First();
                var callType = (Core.OnCompleteType)callTypeConst.Value;

                md.OnCompletion = new CallConfigSpec();
                switch (callType)
                {
                    case Core.OnCompleteType.NoOp:
                        md.OnCompletion.No_op = CallConfig.CALL;
                        break;
                    case Core.OnCompleteType.OptIn:
                        md.OnCompletion.Opt_in = CallConfig.CALL;
                        break;
                    case Core.OnCompleteType.CloseOut:
                        md.OnCompletion.Close_out = CallConfig.CALL;
                        break;
                    case Core.OnCompleteType.UpdateApplication:
                        md.OnCompletion.Update_application = CallConfig.CALL;
                        break;
                    case Core.OnCompleteType.DeleteApplication:
                        md.OnCompletion.Delete_application = CallConfig.CALL;
                        break;

                }


                var returnType = methodSymbol.ReturnType;
                md.Returns = new ReturnTypeDescription()
                {
                    Type = TypeHelpers.CSTypeToAbiType(returnType),
                    TypeDetail = returnType.ToString()
                };

                md.Name = methodSymbol.Name;

                var refToCurrentAppCall = methodSymbol.Parameters.Where(p => TransactionRefVariable.IsTxRef(p.Type)).LastOrDefault();
                foreach (var parm in methodSymbol.Parameters)
                {
                    //the last transaction parameter is the current app call and this must not be part of the spec
                    if (refToCurrentAppCall == null || !refToCurrentAppCall.Equals(parm, SymbolEqualityComparer.Default))
                    {
                        //check if the parameter has an ABI type modifier and use that instead of the
                        //default 
                        var typeModifier = parm
                            .GetAttributes()
                            .Where(a => a.AttributeClass.Name == nameof(ABITypeDecoratorAttribute))
                            .FirstOrDefault();

                        string parameterTypeDescription = "unknown";
                        if (typeModifier == null)
                        {
                            parameterTypeDescription = TypeHelpers.CSTypeToAbiType(parm.Type);
                        }
                        else
                        {
                            if (typeModifier.ConstructorArguments.Where(kv => kv.Type.Name == "String").Any())
                            {
                                var parmTypeConst = typeModifier.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                                parameterTypeDescription = (string)parmTypeConst.Value;
                            }
                        }

                        md.Args.Add(new ArgumentDescription()
                        {
                            Name = parm.Name,
                            Type = parameterTypeDescription,
                            TypeDetail = parm.Type.ToString()
                        });

                    }
                }

                //Add any leading structured trivia
                if (ms.HasStructuredTrivia)
                {
                    var trivia = ms.GetLeadingTrivia()
                                                .Select(i => i.GetStructure())
                                                .OfType<DocumentationCommentTriviaSyntax>()
                                                .FirstOrDefault();

                    if (trivia != null)
                    {
                        var summary = trivia.ChildNodes()
                            .OfType<XmlElementSyntax>()
                            .Where(i => i.StartTag.Name.ToString().ToLower().Equals("summary"))
                            .FirstOrDefault();

                        if (summary != null && summary.Content != null)
                        {
                            md.Desc = summary.Content.FirstOrDefault().ToString().Trim().Replace("///", "");
                        }

                    }
                }

                var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();

                if (!string.IsNullOrWhiteSpace((string)selectorConst.Value))
                {
                    md.Selector = Encoding.UTF8.GetBytes((string)selectorConst.Value);
                    md.Identifier = (string)selectorConst.Value;

                }
                else
                {
                    md.Selector = md.ToARC4MethodSelector();
                    md.Identifier = md.ARC4MethodSignature;

                }
            }
            return md;
        }
        public string ARC4MethodSignature => $"{Name}({string.Join(",", Args.Select(a => a.Type))}){Returns.Type}";

        public byte[] ToARC4MethodSelector()
        {
            return ToARC4MethodSelector(ARC4MethodSignature);
        }
        /// <summary>
        /// Converts the specified ARC4 method signature into a 4-byte selector value.
        /// </summary>
        /// <remarks>The method computes the selector value by hashing the input string using a
        /// SHA-512/256 digest and returning the first 4 bytes of the resulting hash.</remarks>
        /// <param name="arc4MethodSignature">The ARC4 method signature to be converted. This must be a non-null, non-empty string.</param>
        /// <returns>A 4-byte array representing the selector value derived from the ARC4 method signature.</returns>
        public static byte[] ToARC4MethodSelector(string arc4MethodSignature)
        {
            var data = Encoding.ASCII.GetBytes(arc4MethodSignature);
            Sha512tDigest digest = new Sha512tDigest(256);
            digest.BlockUpdate(data, 0, data.Length);
            byte[] output = new byte[32];
            digest.DoFinal(output, 0);
            return output.Take(4).ToArray();
        }


        private static string SanitizeForComment(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            // Replace comment terminators (*/), new lines, and other potentially harmful characters
            return input.Replace("*/", "*\\/").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\\", "\\\\");
        }

        private static string GetFullGenericTypeName(Type type)
        {
            if (!type.IsGenericType)
                return type.FullName;

            var genericTypeName = type.GetGenericTypeDefinition().FullName;
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => GetFullGenericTypeName(t)));
            return genericTypeName + "<" + genericArgs + ">";
        }


        //Strictly ARC4 types
        public string ToARC4Caller()
        {
            StringBuilder arc4MethodCallerClass = new StringBuilder();
            List<string> invokerArgs = new List<string>();
            List<ArgumentDescription> transactionParameters = new List<ArgumentDescription>();
            List<ArgumentDescription> nonTransactionParameters = new List<ArgumentDescription>();
            arc4MethodCallerClass.AppendLine($"//{SanitizeForComment(Desc)}");
            arc4MethodCallerClass.AppendLine($"public class {Name}_Arc4GroupTransaction: ProxyBase");
            arc4MethodCallerClass.AppendLine("{");
            arc4MethodCallerClass.AppendLine($"\tpublic {Name}_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {{}}");
            arc4MethodCallerClass.AppendLine($"\tprivate {Name}_Arc4GroupTransaction() : base(null,0)  {{}} ");

            foreach (var arg in this.Args)
            {
                if (!arg.IsTransaction())
                {
                    WireType wt = WireType.FromABIDescription(arg.Type);
                    arc4MethodCallerClass.AppendLine($"//{SanitizeForComment(arg.Desc)}");
                    if (wt != null)
                    {
                        arc4MethodCallerClass.AppendLine($"\tpublic {GetFullGenericTypeName(wt.GetType())} {arg.Name} {{get;set;}}= ({GetFullGenericTypeName(wt.GetType())})AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{arg.Type}\");");
                    }
                    else
                    {
                        arc4MethodCallerClass.AppendLine($"\tAVM.ClientGenerator.ABI.ARC4.Types.WireType {arg.Name} {{get;set;}}");
                    }

                    nonTransactionParameters.Add(arg);
                }
                else
                {
                    transactionParameters.Add(arg);
                    invokerArgs.Add(defineTransactionParameter(arg));
                }
            }
            string txNameList;
            if (transactionParameters.Count > 0) txNameList = "new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => p.Name)) + "}";
            else
                txNameList = "null";

            string argNameList = "new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {" + string.Join(",", nonTransactionParameters.Select(p => $"{p.Name}")) + "}";


            string invokerArgsString;
            if (invokerArgs.Count > 0) invokerArgsString = string.Join(",", invokerArgs) + ",";
            else
                invokerArgsString = "";



            arc4MethodCallerClass.AppendLine($"\tpublic async Task<List<Transaction>> Invoke({invokerArgsString}ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)");
            arc4MethodCallerClass.AppendLine("\t{");
            arc4MethodCallerClass.AppendLine($"\t\t");
            arc4MethodCallerClass.AppendLine($"\t\tbyte[] abiHandle = {{{string.Join(",", Selector)}}};");
            //TODO: If the arg list length is >15, wrap up the 15th and remaining into a Tuple and encode like that.
            arc4MethodCallerClass.AppendLine($"return await base.MakeArc4TransactionList({txNameList}, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, {argNameList}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);");


            arc4MethodCallerClass.AppendLine("\t}");


            arc4MethodCallerClass.AppendLine("}");

            return arc4MethodCallerClass.ToString();

        }

        private static string defineTransactionParameter(ArgumentDescription p)
        {
            string parmType = p.Type.ToString();
            if (!string.IsNullOrWhiteSpace(p.TypeDetail)) parmType = p.TypeDetail;
            string outputParmType = TypeHelpers.determineTransactionType(parmType);

            return $"{outputParmType} {p.Name}";
        }


        internal void ToSmartContractReference(StringBuilder scr, List<string> structs)
        {
            var argsAndTransactionReferences = Args.Select(a =>
                {
                    var txRef = "";
                    if (a.TypeDetail != null)
                    {
                        txRef = TypeHelpers.TransactionReferenceToInnerTransaction(a.TypeDetail);
                    }
                    else
                    {
                        txRef = TypeHelpers.TransactionReferenceToInnerTransaction(a.Type);
                    }
                    return (refType: txRef, arg: a);
                })
                .ToList();

            var nonTxRefArgs = argsAndTransactionReferences.Where(a => string.IsNullOrWhiteSpace(a.refType)).ToList();
            var txRefArgs = argsAndTransactionReferences.Where(a => !string.IsNullOrWhiteSpace(a.refType)).ToList();


            scr.AppendLine(
$@"{"\t\t"}///<summary>
{"\t\t"}///{Desc?.Replace("\n", "\n///")}
{"\t\t"}///</summary>");
            foreach (var arg in nonTxRefArgs)
            {
                scr.AppendLine($@"{"\t\t"}///<param name=""{arg.arg.Name}"">{arg.arg.Desc}</param>");
            }
            scr.AppendLine($@"{"\t\t"}///<param name=""result"">{Returns.Desc}</param>");

            string retType;
            if (txRefArgs.Count == 0)
            {
                retType = "ValueTuple<AppCall>";
            }
            else
            if (txRefArgs.Count == 1)
            {
                retType = $"({txRefArgs[0].refType} {txRefArgs[0].arg.Name},AppCall)";
            }
            else
            {
                retType = $"({string.Join(",", txRefArgs.Select(a => $"{a.refType} {a.arg.Name}").Concat(new List<string> { "AppCall" }))})";
            }

            var t = TypeHelpers.GetCSType(FormatStructName($"{Name}Return"), Returns.Type, Returns.TypeDetail, structs, false);
            var retParm = $"out {t.type} result";
            if (t.type == "void") retParm = "";

            if (Enumerable.SequenceEqual(Selector, ToARC4MethodSelector()))
            {
                scr.AppendLine($"\t\t[SmartContractMethod(OnCompleteType.NoOp)]");
            }
            else
            {
                scr.AppendLine($"\t\t[SmartContractMethod(OnCompleteType.NoOp, \"{Encoding.UTF8.GetString(Selector)}\")]");
            }

            scr.Append($"\t\tpublic abstract {retType} {FormatMethodName(Name)}(");
            scr.Append(string.Join(",", txRefArgs
                .Select(s => $"{s.refType} {s.arg.Name}")
                ));
            if (txRefArgs.Count > 0) { scr.Append(","); }
            scr.Append(string.Join(",", nonTxRefArgs
                .Select(s => $"{TypeHelpers.GetCSType(MethodDescription.FormatStructName(Name + "_arg_" + s.arg.Name), s.arg.Type, s.arg.TypeDetail, structs, false).type} {s.arg.Name}")
                ));
            /// out var
            if (!string.IsNullOrEmpty(retParm))
            {
                if (nonTxRefArgs.Count > 0) scr.Append(",");
                scr.Append(retParm);
            }
            scr.AppendLine(");");
        }
        public static string FormatStructName(string name, bool appendStruct = false)
        {
            if (appendStruct)
            {
                return $"Structs.{name.ToPascalCase()}";
            }
            else
            {
                return name.ToPascalCase();
            }
        }
        public static string FormatMethodName(string name)
        {
            return name.ToPascalCase();
        }

        /// <summary>
        /// ARC56 method/event argument names are free-form and may collide with a C# reserved word (e.g. an ABI
        /// arg literally named "operator", as in ARC-1400's arc1410IsOperator). Since the generator otherwise
        /// emits arg.Name verbatim as a C# identifier (parameter declarations and in-body references), escape it
        /// with "@" whenever it is one, matching the C# verbatim-identifier syntax.
        /// </summary>
        public static string SafeIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            return SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None ? "@" + name : name;
        }
    }
}
