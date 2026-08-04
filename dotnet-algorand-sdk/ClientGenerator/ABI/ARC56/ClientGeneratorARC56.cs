using AVM.ClientGenerator.ABI;
using AVM.ClientGenerator.ABI.ARC4;
using AVM.ClientGenerator.ABI.ARC56;
using AVM.ClientGenerator.Clients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Algorand.AVM.ClientGenerator.Extensions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using AVM.ClientGenerator.ABI.ARC4.Types;

namespace Algorand.AVM.ClientGenerator.ABI.ARC56
{
    public class ClientGeneratorARC56
    {
        private AppDescriptionArc56 Contract { get; set; } = null;
        /// <summary>
        /// Load app description from file
        /// 
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool LoadFromByteArray(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            Contract = JsonConvert.DeserializeObject<AppDescriptionArc56>(json, new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            // Some real-world ARC-56 artifacts (e.g. from older/nonstandard build tooling) emit the compiled
            // bytecode under a snake_case "byte_code" key instead of the spec's camelCase "byteCode" - fall back
            // to it so ExtraProgramPages (ToProxy below) can still be computed instead of silently staying null.
            if (Contract != null && Contract.ByteCode == null)
            {
                var root = Newtonsoft.Json.Linq.JObject.Parse(json);
                var byteCodeToken = root["byte_code"];
                if (byteCodeToken != null)
                {
                    Contract.ByteCode = byteCodeToken.ToObject<ProgramSource>();
                }
            }

            return true;
        }
        public string GetOutputFileName()
        {
            return Contract.Name + ".cs";
        }


        public async Task<string> ToProxy(string namespaceName)
        {
            if (Contract == null) throw new Exception("Load the contract first");

            ResolveAnonymousTuples();

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
            code.AddOpeningLine("using AVM.ClientGenerator.ABI.ARC56;");
            code.AddOpeningLine("using Algorand.AVM.ClientGenerator.ABI.ARC56;");



            code.AddOpeningLine("");
            code.AddOpeningLine($"namespace {namespaceName}");
            code.AddOpeningLine("{");
            code.AddOpeningLine("");
            code.AddClosingLine("}");
            var proxyBody = code.AddChild();
            proxyBody.AddOpeningLine("");


            //Add any leading structured trivia

            if (!string.IsNullOrEmpty(Contract.Description))
            {
                proxyBody.AddOpeningLine("//");
                proxyBody.AddOpeningLine($"// {Contract.Description?.Replace("\n", "\n//")}");
                proxyBody.AddOpeningLine("//");
            }


            // ExtraProgramPages is derived from the compiled approval program size, but ByteCode is optional in
            // ARC-56 (some artifacts only ship source/sourceInfo) - fall back to 0 rather than crash when it or
            // its Approval program is missing.
            ulong extraProgramPages = string.IsNullOrEmpty(Contract.ByteCode?.Approval)
                ? 0
                : (ulong)(Convert.FromBase64String(Contract.ByteCode.Approval).Length / 2048);

            proxyBody.AddOpeningLine($"public class {className} : ProxyBase");
            proxyBody.AddOpeningLine("{");
            proxyBody.AddClosingLine($"protected override ulong? ExtraProgramPages {{get; set; }} = {extraProgramPages};");
            proxyBody.AddClosingLine($"protected string _ARC56DATA = \"{Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Contract)))}\";");
            proxyBody.AddClosingLine("}");

            var ctor = proxyBody.AddChild();
            ctor.AddOpeningLine("public override AppDescriptionArc56 App { get; set; }");
            ctor.AddOpeningLine("");
            ctor.AddOpeningLine($"public {className}(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) ");
            ctor.AddOpeningLine("{");
            ctor.AddClosingLine("}");

            var ctorInner = ctor.AddChild();
            ctorInner.AddOpeningLine("App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception(\"Error reading ARC56 data\");");

            defineStructs(proxyBody);
            defineEvents(proxyBody);
            defineMethods(proxyBody, structs);

            return await FormatCode(code.ToString());
        }

        /// <summary>
        /// ARC56 methods/events may reference raw ABI tuple types (e.g. "(uint64,string)" or "(address,uint256)[]")
        /// directly, without a corresponding entry in Contract.Structs. The rest of the generator only knows how
        /// to encode/decode named structs, so this walks every method arg/return (and struct field, recursively)
        /// and synthesizes a Contract.Structs entry for any such anonymous tuple, reusing an existing struct when
        /// its shape already matches one (e.g. an array of "(address,uint256)" reuses "structAddressUint256").
        /// </summary>
        private void ResolveAnonymousTuples()
        {
            var shapeCache = new Dictionary<string, string>();
            foreach (var structName in Contract.Structs.Keys.ToList())
            {
                var sig = BuildTupleSignature(structName, new HashSet<string>());
                if (sig != null && !shapeCache.ContainsKey(sig))
                {
                    shapeCache[sig] = structName;
                }
            }

            foreach (var method in Contract.Methods)
            {
                foreach (var arg in method.Args)
                {
                    if (string.IsNullOrEmpty(arg.Struct) && !string.IsNullOrEmpty(arg.Type))
                    {
                        var resolved = ResolveTupleType(arg.Type, $"{method.Name}_arg_{arg.Name}", shapeCache);
                        if (resolved != null) arg.Struct = resolved;
                    }
                }
                if (method.Returns != null && string.IsNullOrEmpty(method.Returns.Struct) && !string.IsNullOrEmpty(method.Returns.Type))
                {
                    var resolved = ResolveTupleType(method.Returns.Type, $"{method.Name}Return", shapeCache);
                    if (resolved != null) method.Returns.Struct = resolved;
                }
            }

            if (Contract.Events != null)
            {
                foreach (var evt in Contract.Events)
                {
                    if (evt.Args == null) continue;
                    foreach (var arg in evt.Args)
                    {
                        if (string.IsNullOrEmpty(arg.Struct) && !string.IsNullOrEmpty(arg.Type))
                        {
                            var resolved = ResolveTupleType(arg.Type, $"{evt.Name}_event_{arg.Name}", shapeCache);
                            if (resolved != null) arg.Struct = resolved;
                        }
                    }
                }
            }

            // Unlike MethodArgument/EventArgument, StructField has no separate "Struct" annotation - per ARC56 its
            // Type is either a raw ABI type, a struct name (optionally array-suffixed), or a raw anonymous tuple
            // (optionally array-suffixed). Rewrite that last case in place to the resolved struct name(+array
            // suffix), same as arg.Struct above, so defineStructs's "is this a struct field?" check
            // (WireType.FromABIDescription(structItem.Type) == null) sees a struct name instead of raw tuple text.
            foreach (var structName in Contract.Structs.Keys.ToList())
            {
                var fields = Contract.Structs[structName];
                for (int i = 0; i < fields.Count; i++)
                {
                    var field = fields[i];
                    if (string.IsNullOrEmpty(field.Type)) continue;
                    string bare = SplitTrailingArrayComponent(field.Type, out string arrayComponent);
                    if (Contract.Structs.ContainsKey(bare)) continue; // already a named-struct reference

                    var resolved = ResolveTupleType(field.Type, $"{structName}_{field.Name}", shapeCache);
                    if (resolved != null) field.Type = resolved + arrayComponent;
                }
            }
        }

        private static string SplitTrailingArrayComponent(string abiType, out string arrayComponent)
        {
            arrayComponent = "";
            string bare = abiType.Trim();
            if (bare.EndsWith("]"))
            {
                int idx = bare.LastIndexOf('[');
                if (idx > 0 && bare[idx - 1] == ')')
                {
                    arrayComponent = bare.Substring(idx);
                    bare = bare.Substring(0, idx);
                }
            }
            return bare;
        }

        /// <summary>
        /// If abiType is a raw tuple (optionally array-suffixed), resolves/creates the named struct for its
        /// element shape and returns the struct name. Returns null if abiType isn't a tuple at all.
        /// </summary>
        private string ResolveTupleType(string abiType, string nameHint, Dictionary<string, string> shapeCache)
        {
            string bare = SplitTrailingArrayComponent(abiType, out _);
            if (!bare.StartsWith("(") || !bare.EndsWith(")")) return null;

            return ResolveOrCreateStruct(bare, nameHint, shapeCache);
        }

        private string ResolveOrCreateStruct(string tupleAbiType, string nameHint, Dictionary<string, string> shapeCache)
        {
            string canonical = tupleAbiType.Trim().ToLowerInvariant();
            if (shapeCache.TryGetValue(canonical, out var existing)) return existing;

            string structName = MethodDescription.FormatStructName(nameHint);
            string baseName = structName;
            int suffix = 1;
            while (Contract.Structs.ContainsKey(structName))
            {
                structName = baseName + suffix;
                suffix++;
            }
            // Reserve the name immediately so nested tuples of the same shape (e.g. a self-referential
            // structure) can't recurse forever, and so later lookups see it even before fields are filled in.
            var fields = new List<StructField>();
            Contract.Structs[structName] = fields;
            shapeCache[canonical] = structName;

            string inner = tupleAbiType.Substring(1, tupleAbiType.Length - 2);
            var subTypes = new TypeHelpers.TupleSubTypes(inner);
            int i = 0;
            foreach (var subType in subTypes)
            {
                string trimmed = subType.Trim();
                string subBare = SplitTrailingArrayComponent(trimmed, out string subArrayComponent);

                string fieldType;
                if (subBare.StartsWith("(") && subBare.EndsWith(")"))
                {
                    string nestedStructName = ResolveOrCreateStruct(subBare, $"{nameHint}_field{i}", shapeCache);
                    fieldType = nestedStructName + subArrayComponent;
                }
                else
                {
                    fieldType = trimmed;
                }
                fields.Add(new StructField { Name = $"field{i}", Type = fieldType });
                i++;
            }

            return structName;
        }

        /// <summary>
        /// Reconstructs the canonical ABI tuple signature of a named struct (recursively expanding any nested
        /// struct-typed fields), so anonymous tuples encountered later can be matched against it by shape.
        /// </summary>
        private string BuildTupleSignature(string structName, HashSet<string> visiting)
        {
            if (!Contract.Structs.TryGetValue(structName, out var fields)) return null;
            if (!visiting.Add(structName)) return null;

            var parts = new List<string>();
            foreach (var field in fields)
            {
                string bare = SplitTrailingArrayComponent(field.Type ?? "", out string arrayComponent);
                if (Contract.Structs.ContainsKey(bare))
                {
                    string nested = BuildTupleSignature(bare, visiting);
                    parts.Add((nested ?? bare) + arrayComponent);
                }
                else
                {
                    parts.Add((field.Type ?? "").Trim());
                }
            }

            visiting.Remove(structName);
            return "(" + string.Join(",", parts) + ")";
        }

        /// <summary>
        /// True if the given ABI field type must be offset/tail-addressed when nested inside another tuple,
        /// struct, or ARC-28 event argument list, per ARC4 rules: strings and variable-length arrays are always
        /// dynamic; a fixed-length array is dynamic iff its element type is; a struct (named or synthesized from
        /// a plain ABI tuple) is dynamic only if one of its own fields is, checked recursively; everything else
        /// (uintN, byte, bool, address, asset, application, ufixed) is static.
        /// </summary>
        private bool IsFieldTypeDynamic(string abiType)
        {
            string trimmed = (abiType ?? "").Trim();
            if (trimmed.Equals("string", StringComparison.OrdinalIgnoreCase)) return true;

            if (trimmed.EndsWith("]"))
            {
                int idx = trimmed.LastIndexOf('[');
                if (idx >= 0)
                {
                    string arrayComponent = trimmed.Substring(idx);
                    string bare = trimmed.Substring(0, idx);
                    if (arrayComponent == "[]") return true;
                    return IsFieldTypeDynamic(bare);
                }
            }

            if (Contract.Structs.ContainsKey(trimmed))
            {
                // Empirically (see ARC-28 event log encoding), Algorand Python/Puya treats a struct as dynamic
                // only if it actually contains a dynamic component, the same rule as plain tuples - not "any
                // named struct is always dynamic" as one might expect from the type system alone.
                return Contract.Structs[trimmed].Any(f => IsFieldTypeDynamic(f.Type));
            }

            return false;
        }

        static async Task<string> FormatCode(string sourceCode)
        {
            var workspace = new AdhocWorkspace();
            var options = workspace.Options
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInMethods, true)
                .WithChangedOption(CSharpFormattingOptions.IndentBraces, false);

            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = await syntaxTree.GetRootAsync();
            var formattedRoot = Formatter.Format(root, workspace, options);
            return formattedRoot.ToFullString();
        }
        /// <summary>
        /// True if fieldType is an array of a named struct (e.g. "SomeStruct[]" or "SomeStruct[5]") - the one
        /// struct-field shape defineStructs' encode/decode can't handle via the plain "abiDescriptor == null ->
        /// scalar struct usage" path below, since WireType.FromABIDescription also returns null for it (its
        /// element isn't a raw ABI type either) but a plain Parse()/ToByteArray() call is a scalar
        /// StructType.Parse(...), not the StructType[] the property is actually declared as.
        /// </summary>
        private bool TryGetStructArrayField(string fieldType, out string structTypeName, out bool isFixedLength, out string fixedLength)
        {
            structTypeName = null; isFixedLength = false; fixedLength = "0";
            string trimmed = (fieldType ?? "").Trim();
            if (!trimmed.EndsWith("]")) return false;
            int idx = trimmed.LastIndexOf('[');
            if (idx < 0) return false;
            string bare = trimmed.Substring(0, idx);
            string arrayComponent = trimmed.Substring(idx);
            if (!Contract.Structs.ContainsKey(bare)) return false;

            structTypeName = $"Structs.{MethodDescription.FormatStructName(bare)}";
            isFixedLength = arrayComponent.Length > 2;
            fixedLength = isFixedLength ? arrayComponent.TrimStart('[').TrimEnd(']') : "0";
            return true;
        }

        private void defineStructs(Code proxyBody)
        {
            var structsObj = proxyBody.AddChild();
            structsObj.AddOpeningLine($"public class Structs");
            structsObj.AddOpeningLine("{");
            structsObj.AddClosingLine("}");

            foreach (var item in Contract.Structs)
            {
                var structObj = structsObj.AddChild();
                structObj.AddOpeningLine($"public class {item.Key.ToPascalCase()} : AVMObjectType");
                structObj.AddOpeningLine("{");
                structObj.AddClosingLine("}");

                foreach (var structItem in item.Value)
                {
                    var structItemObj = structObj.AddChild();
                    // By this point ResolveAnonymousTuples has already rewritten any raw-tuple structItem.Type
                    // (e.g. "(byte[4],uint64)[]") into a named-struct reference, so parentStructName only actually
                    // matters here for ABITypeToCSType's "unknown type -> reference an existing struct" fallback -
                    // which reconstructs the struct name from parentStructName, not from methodABIType. Passing
                    // structItem.Type itself keeps that fallback resolving to the right struct.
                    var p = TypeHelpers.ABITypeToCSType(structItem.Type, structItem.Type, new List<string>(), false);
                    var appendInitiator = p.useNew ? $" = new {p.Item2}();" : "";
                    structItemObj.AddOpeningLine($"public {p.Item2} {structItem.Name.ToPascalCase()} {{get; set;}}{appendInitiator}");
                }
                // ToByteArray()
                var structF1 = structObj.AddChild();
                structF1.AddOpeningLine("public byte[] ToByteArray()");
                structF1.AddOpeningLine("{");
                structF1.AddClosingLine("}");

                var structF1In = structF1.AddChild();

                structF1.AddOpeningLine("var ret = new List<byte>();");
                structF1.AddOpeningLine("var stringRef = new Dictionary<int, byte[]>();");

                foreach (var structItem in item.Value)
                {
                    var abiDescriptor = WireType.FromABIDescription(structItem.Type);
                    if (TryGetStructArrayField(structItem.Type, out string arrStructType, out bool arrIsFixedLength, out string arrFixedLength))
                    {
                        string fieldName = structItem.Name.ToPascalCase();
                        structF1.AddOpeningLine($"var arr{fieldName} = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<{arrStructType}>(x => {arrStructType}.Parse(x)) {{ IsFixedLength = {(arrIsFixedLength ? "true" : "false")}, FixedLength = {arrFixedLength} }};");
                        structF1.AddOpeningLine($"arr{fieldName}.Value = ({fieldName} ?? Array.Empty<{arrStructType}>()).ToList();");
                        if (IsFieldTypeDynamic(structItem.Type))
                        {
                            structF1.AddOpeningLine($"stringRef[ret.Count] = arr{fieldName}.Encode();");
                            structF1.AddOpeningLine($"ret.AddRange(new byte[2]);");
                        }
                        else
                        {
                            structF1.AddOpeningLine($"ret.AddRange(arr{fieldName}.Encode());");
                        }
                    }
                    else if (abiDescriptor == null)
                    {
                        // struct usage
                        if (IsFieldTypeDynamic(structItem.Type))
                        {
                            structF1.AddOpeningLine($"stringRef[ret.Count] = {structItem.Name.ToPascalCase()}.ToByteArray();");
                            structF1.AddOpeningLine($"ret.AddRange(new byte[2]);");
                        }
                        else
                        {
                            // A static (all-fields-static) synthesized struct is inlined directly, matching how
                            // ARC4 encodes a static nested tuple - no offset/tail indirection.
                            structF1.AddOpeningLine($"ret.AddRange({structItem.Name.ToPascalCase()}.ToByteArray());");
                        }
                    }
                    else
                    {
                        if (structItem.Type == "string")
                        {
                            structF1.AddOpeningLine($"AVM.ClientGenerator.ABI.ARC4.Types.WireType v{structItem.Name.ToPascalCase()} = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{structItem.Type}\");");
                            structF1.AddOpeningLine($"v{structItem.Name.ToPascalCase()}.From({structItem.Name.ToPascalCase()});");
                            structF1.AddOpeningLine($"stringRef[ret.Count] = v{structItem.Name.ToPascalCase()}.Encode();");
                            structF1.AddOpeningLine($"ret.AddRange(new byte[2]);");
                        }
                        else
                        {

                            structF1.AddOpeningLine($"AVM.ClientGenerator.ABI.ARC4.Types.WireType v{structItem.Name.ToPascalCase()} = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{structItem.Type}\");");
                            structF1.AddOpeningLine($"v{structItem.Name.ToPascalCase()}.From({structItem.Name.ToPascalCase()});");
                            structF1.AddOpeningLine($"ret.AddRange(v{structItem.Name.ToPascalCase()}.Encode());");
                        }

                    }
                }
                structF1.AddOpeningLine("foreach (var item in stringRef)\r\n                {\r\n                    var b1 = ret.Count;\r\n                    ret[item.Key] = Convert.ToByte(b1 / 256);\r\n                    ret[item.Key + 1] = Convert.ToByte(b1 % 256);\r\n                    ret.AddRange(item.Value);\r\n                }");
                structF1.AddOpeningLine("return ret.ToArray();");


                // Parse()
                var structF2 = structObj.AddChild();
                structF2.AddOpeningLine($"public static {item.Key.ToPascalCase()} Parse(byte[] bytes)");
                structF2.AddOpeningLine("{");
                structF2.AddClosingLine("}");

                var structF2In = structF2.AddChild();

                // prefixOffset/count are only emitted when a field's decode path actually reads them below -
                // declaring them unconditionally produced CS0219 "assigned but never used" for structs whose
                // fields never hit those paths (e.g. all-static-struct or all-string-free-of-struct fields).
                bool needsPrefixOffset = item.Value.Any(structItem =>
                {
                    var abiDescriptor = WireType.FromABIDescription(structItem.Type);
                    return abiDescriptor == null
                        ? IsFieldTypeDynamic(structItem.Type)
                        : structItem.Type == "string";
                });
                bool needsCount = item.Value.Any(structItem =>
                {
                    var abiDescriptor = WireType.FromABIDescription(structItem.Type);
                    return abiDescriptor != null && structItem.Type != "string";
                });

                structF2.AddOpeningLine($"var queue = new Queue<byte>(bytes);");
                if (needsPrefixOffset)
                {
                    structF2.AddOpeningLine($"var prefixOffset = 0;");
                    structF2.AddOpeningLine($"var retPrefix = new byte[4] {{ bytes[0], bytes[1], bytes[2], bytes[3] }};\r\n                if (retPrefix.SequenceEqual(Constants.RetPrefix))\r\n                {{\r\n                    prefixOffset=4;\r\n                    for (int i = 0; i < 4 && queue.Count > 0; i++){{queue.Dequeue();}}\r\n                }}");
                }
                structF2.AddOpeningLine($"var ret = new {item.Key.ToPascalCase()}();");
                if (needsCount)
                {
                    structF2.AddOpeningLine($"uint count = 0;");
                }

                foreach (var structItem in item.Value)
                {
                    var abiDescriptor = WireType.FromABIDescription(structItem.Type);
                    if (TryGetStructArrayField(structItem.Type, out string arrStructType, out bool arrIsFixedLength, out string arrFixedLength))
                    {
                        string fieldName = structItem.Name.ToPascalCase();
                        if (IsFieldTypeDynamic(structItem.Type))
                        {
                            structF2.AddOpeningLine($"var index{fieldName} = queue.Dequeue() * 256 + queue.Dequeue();");
                            structF2.AddOpeningLine($"var arr{fieldName} = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<{arrStructType}>(x => {arrStructType}.Parse(x)) {{ IsFixedLength = {(arrIsFixedLength ? "true" : "false")}, FixedLength = {arrFixedLength} }};");
                            structF2.AddOpeningLine($"arr{fieldName}.Decode(bytes.Skip(index{fieldName} + prefixOffset).ToArray());");
                            structF2.AddOpeningLine($"ret.{fieldName} = arr{fieldName}.Value.ToArray();");
                        }
                        else
                        {
                            // A static (fixed-length, static-element) struct array is inlined directly, matching
                            // how ARC4 encodes a static array - no offset/tail indirection.
                            structF2.AddOpeningLine($"var arr{fieldName} = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<{arrStructType}>(x => {arrStructType}.Parse(x)) {{ IsFixedLength = {(arrIsFixedLength ? "true" : "false")}, FixedLength = {arrFixedLength} }};");
                            structF2.AddOpeningLine($"{{ var consumed{fieldName} = (int)arr{fieldName}.Decode(queue.ToArray()); for (int i = 0; i < consumed{fieldName} && queue.Count > 0; i++){{queue.Dequeue();}} }}");
                            structF2.AddOpeningLine($"ret.{fieldName} = arr{fieldName}.Value.ToArray();");
                        }
                    }
                    else if (abiDescriptor == null)
                    {
                        // struct usage
                        var name = MethodDescription.FormatStructName(structItem.Type, true);
                        if (IsFieldTypeDynamic(structItem.Type))
                        {
                            structF2.AddOpeningLine($"var index{structItem.Name.ToPascalCase()} = queue.Dequeue() * 256 + queue.Dequeue();");
                            structF2.AddOpeningLine($"ret.{structItem.Name.ToPascalCase()} = {name}.Parse(bytes.Skip(index{structItem.Name.ToPascalCase()} + prefixOffset).ToArray());");
                        }
                        else
                        {
                            // A static synthesized struct is inlined directly: parse it off the front of what's
                            // left in the queue, then measure how many bytes it actually consumed (via a
                            // re-encode round-trip) to advance the queue past it.
                            structF2.AddOpeningLine($"ret.{structItem.Name.ToPascalCase()} = {name}.Parse(queue.ToArray());");
                            structF2.AddOpeningLine($"{{ var consumed{structItem.Name.ToPascalCase()} = ret.{structItem.Name.ToPascalCase()}.ToByteArray().Length; for (int i = 0; i < consumed{structItem.Name.ToPascalCase()} && queue.Count > 0; i++){{queue.Dequeue();}} }}");
                        }
                    }
                    else
                    {
                        var p = TypeHelpers.ABITypeToCSType(structItem.Type, structItem.Type, new List<string>(), false);
                        if (abiDescriptor.GetType().ToString() == p.dotnetArgInputType)
                        {
                            structF2.AddOpeningLine($"var v{structItem.Name.ToPascalCase()} = new {p.dotnetArgInputType}();");
                            structF2.AddOpeningLine($"count = v{structItem.Name.ToPascalCase()}.Decode(queue.ToArray());");
                            structF2.AddOpeningLine($"for (int i = 0; i < Convert.ToInt32(count); i++){{queue.Dequeue();}}");
                            structF2.AddOpeningLine($"ret.{structItem.Name.ToPascalCase()} = v{structItem.Name.ToPascalCase()};");

                        }
                        else  if (structItem.Type == "string")
                        {
                            structF2.AddOpeningLine($"var index{structItem.Name.ToPascalCase()} = queue.Dequeue() * 256 + queue.Dequeue();");
                            structF2.AddOpeningLine($"AVM.ClientGenerator.ABI.ARC4.Types.WireType v{structItem.Name.ToPascalCase()} = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{structItem.Type}\");");
                            structF2.AddOpeningLine($"v{structItem.Name.ToPascalCase()}.Decode(bytes.Skip(index{structItem.Name.ToPascalCase()} + prefixOffset).ToArray());");
                            structF2.AddOpeningLine($"var value{structItem.Name.ToPascalCase()} = v{structItem.Name.ToPascalCase()}.ToValue();");
                            structF2.AddOpeningLine($"if (value{structItem.Name.ToPascalCase()} is string v{structItem.Name.ToPascalCase()}Value) {{ ret.{structItem.Name.ToPascalCase()} = v{structItem.Name.ToPascalCase()}Value; }}");
                        }
                        else
                        {
                            structF2.AddOpeningLine($"AVM.ClientGenerator.ABI.ARC4.Types.WireType v{structItem.Name.ToPascalCase()} = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{structItem.Type}\");");
                            structF2.AddOpeningLine($"count = v{structItem.Name.ToPascalCase()}.Decode(queue.ToArray());");
                            structF2.AddOpeningLine($"for (int i = 0; i < Convert.ToInt32(count); i++){{queue.Dequeue();}}");
                            structF2.AddOpeningLine($"var value{structItem.Name.ToPascalCase()} = v{structItem.Name.ToPascalCase()}.ToValue();");
                            structF2.AddOpeningLine($"if (value{structItem.Name.ToPascalCase()} is {p.dotnetArgInputType} v{structItem.Name.ToPascalCase()}Value){{ret.{structItem.Name.ToPascalCase()} = v{structItem.Name.ToPascalCase()}Value;}}");
                        }
                    }
                }
                structF2.AddOpeningLine("return ret;");

                var structF3 = structObj.AddChild();
                structF3.AddOpeningLine("public override string ToString()");
                structF3.AddOpeningLine("{");
                structF3.AddOpeningLine("return $\"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace(\"-\", \"\")}\";");
                structF3.AddOpeningLine("}");
                structF3.AddOpeningLine("public override bool Equals(object? obj)");
                structF3.AddOpeningLine("{");
                structF3.AddOpeningLine($"return Equals(obj as {item.Key.ToPascalCase()});");
                structF3.AddOpeningLine("}");
                structF3.AddOpeningLine($"public bool Equals({item.Key.ToPascalCase()}? other)");
                structF3.AddOpeningLine("{");
                structF3.AddOpeningLine("return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());");
                structF3.AddOpeningLine("}");
                structF3.AddOpeningLine("public override int GetHashCode()");
                structF3.AddOpeningLine("{");
                structF3.AddOpeningLine("return ToByteArray().GetHashCode();");
                structF3.AddOpeningLine("}");
                structF3.AddOpeningLine($"public static bool operator ==({item.Key.ToPascalCase()} left, {item.Key.ToPascalCase()} right)");
                structF3.AddOpeningLine("{");
                structF3.AddOpeningLine($"return EqualityComparer<{item.Key.ToPascalCase()}>.Default.Equals(left, right);");
                structF3.AddOpeningLine("}");
                structF3.AddOpeningLine($"public static bool operator !=({item.Key.ToPascalCase()} left, {item.Key.ToPascalCase()} right)");
                structF3.AddOpeningLine("{");
                structF3.AddOpeningLine("return !(left == right);");
                structF3.AddOpeningLine("}");
            }
        }

        /// <summary>
        /// Generates a nested Events class with one decode-only class per ARC-28 event, mirroring the way
        /// defineStructs generates the Structs class. Each generated event class exposes a 4-byte ARC-28
        /// selector (the same sha512/256-based selector algorithm as ARC4 method selectors, computed over
        /// "EventName(argtype1,argtype2,...)" with no return type), a Matches(log) helper, and a static
        /// Decode(log) that parses the ARC4-tuple-encoded argument list following the selector.
        /// </summary>
        private void defineEvents(Code proxyBody)
        {
            if (Contract.Events == null || Contract.Events.Count == 0) return;

            var eventsObj = proxyBody.AddChild();
            eventsObj.AddOpeningLine("public class Events");
            eventsObj.AddOpeningLine("{");
            eventsObj.AddClosingLine("}");

            // ARC-28 allows overloaded events - multiple entries sharing the same name but distinguished by their
            // arg types/selector (e.g. ARC-1400's "Issue" and "Redeem" events are each declared twice). Generating
            // one class per entry under the same FormatStructName(evt.Name + "Event") would collide, so append a
            // numeric suffix to any name reused by a later overload.
            var usedEventClassNames = new HashSet<string>();

            foreach (var evt in Contract.Events)
            {
                string className = MethodDescription.FormatStructName(evt.Name + "Event");
                string baseClassName = className;
                int classNameSuffix = 2;
                while (!usedEventClassNames.Add(className))
                {
                    className = baseClassName + classNameSuffix;
                    classNameSuffix++;
                }
                string signature = $"{evt.Name}({string.Join(",", evt.Args?.Select(a => a.Type) ?? Enumerable.Empty<string>())})";
                byte[] selector = MethodDescription.ToARC4MethodSelector(signature);

                var eventObj = eventsObj.AddChild();
                eventObj.AddOpeningLine($"public class {className}");
                eventObj.AddOpeningLine("{");
                eventObj.AddClosingLine("}");

                var eventTop = eventObj.AddChild();
                eventTop.AddOpeningLine($"public static readonly byte[] Selector = new byte[4] {{ {string.Join(",", selector)} }};");
                eventTop.AddOpeningLine($"public const string Signature = \"{signature.Replace("\"", "\\\"")}\";");
                eventTop.AddOpeningLine("public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }");

                var args = evt.Args ?? new List<EventArgument>();
                var propNames = new List<string>();
                // Keep the per-arg dotnetArgInputType computed here (with a proper, unique struct name) so the
                // decode body below can reuse it instead of re-deriving it from arg.Type directly - re-deriving it
                // there passed arg.Type itself as the parentStructName, which for a tuple/array event arg (e.g.
                // "(byte[4],uint64)[]") produced the raw ABI type text as a "type name" (invalid C# syntax) rather
                // than the synthesized struct name TypeToStructs actually registered.
                var argCsTypes = new List<string>();
                for (int i = 0; i < args.Count; i++)
                {
                    var arg = args[i];
                    string propName = string.IsNullOrEmpty(arg.Name) || char.IsDigit(arg.Name[0]) ? $"Field{i}" : arg.Name.ToPascalCase();
                    propNames.Add(propName);
                    var p = TypeHelpers.GetCSType(MethodDescription.FormatStructName($"{evt.Name}_evtarg_{i}"), arg.Type, arg.Struct, new List<string>(), false);
                    argCsTypes.Add(p.type);
                    eventTop.AddOpeningLine($"public {p.type} {propName} {{ get; set; }}");
                }

                var decodeMethod = eventObj.AddChild();
                decodeMethod.AddOpeningLine($"public static {className} Decode(byte[] log)");
                decodeMethod.AddOpeningLine("{");
                decodeMethod.AddClosingLine("}");
                var decodeBody = decodeMethod.AddChild();
                decodeBody.AddOpeningLine("if (!Matches(log)) throw new Exception(\"Log does not match event selector\");");
                decodeBody.AddOpeningLine($"var ret = new {className}();");
                decodeBody.AddOpeningLine("var eventData = log.Skip(4).ToArray();");

                if (args.Count > 0)
                {
                    // Events are ARC4-tuple-encoded (selector + head/tail-encoded arg list), the same shape as a
                    // struct's fields, so each arg is decoded using the same convention as defineStructs's field
                    // decode: static args are consumed directly off the queue, dynamic ones (string, nested
                    // struct) are read as a 2-byte offset into eventData.
                    decodeBody.AddOpeningLine("var queue = new Queue<byte>(eventData);");
                    // Only declared when an arg actually falls through to the plain-scalar decode path below -
                    // events made up solely of struct/string args never read it, which produced CS0219.
                    bool needsCount = args.Any(a => string.IsNullOrEmpty(a.Struct) && a.Type.Trim().ToLowerInvariant() != "string");
                    if (needsCount)
                    {
                        decodeBody.AddOpeningLine("uint count = 0;");
                    }
                    for (int i = 0; i < args.Count; i++)
                    {
                        var arg = args[i];
                        string propName = propNames[i];
                        if (!string.IsNullOrEmpty(arg.Struct))
                        {
                            string structType = $"Structs.{MethodDescription.FormatStructName(arg.Struct)}";
                            string trimmedArgType = arg.Type.Trim();
                            string arrayComponent = "";
                            if (trimmedArgType.EndsWith("]"))
                            {
                                int arrIdx = trimmedArgType.LastIndexOf('[');
                                if (arrIdx >= 0) arrayComponent = trimmedArgType.Substring(arrIdx);
                            }

                            if (string.IsNullOrEmpty(arrayComponent) && !IsFieldTypeDynamic(arg.Struct))
                            {
                                // A static synthesized struct arg is inlined directly, matching ARC4's static
                                // nested-tuple rule (see IsFieldTypeDynamic).
                                decodeBody.AddOpeningLine($"ret.{propName} = {structType}.Parse(queue.ToArray());");
                                decodeBody.AddOpeningLine($"{{ var consumed{propName} = ret.{propName}.ToByteArray().Length; for (int i = 0; i < consumed{propName} && queue.Count > 0; i++){{queue.Dequeue();}} }}");
                            }
                            else if (string.IsNullOrEmpty(arrayComponent))
                            {
                                decodeBody.AddOpeningLine($"var index{propName} = queue.Dequeue() * 256 + queue.Dequeue();");
                                decodeBody.AddOpeningLine($"ret.{propName} = {structType}.Parse(eventData.Skip(index{propName}).ToArray());");
                            }
                            else
                            {
                                bool isFixedLength = arrayComponent.Length > 2;
                                string fixedLength = isFixedLength ? arrayComponent.TrimStart('[').TrimEnd(']') : "0";
                                decodeBody.AddOpeningLine($"var index{propName} = queue.Dequeue() * 256 + queue.Dequeue();");
                                decodeBody.AddOpeningLine($"var arr{propName} = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<{structType}>(x => {structType}.Parse(x)) {{ IsFixedLength = {(isFixedLength ? "true" : "false")}, FixedLength = {fixedLength} }};");
                                decodeBody.AddOpeningLine($"arr{propName}.Decode(eventData.Skip(index{propName}).ToArray());");
                                decodeBody.AddOpeningLine($"ret.{propName} = arr{propName}.Value.ToArray();");
                            }
                        }
                        else if (arg.Type.Trim().ToLowerInvariant() == "string")
                        {
                            decodeBody.AddOpeningLine($"var index{propName} = queue.Dequeue() * 256 + queue.Dequeue();");
                            decodeBody.AddOpeningLine($"AVM.ClientGenerator.ABI.ARC4.Types.WireType v{propName} = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{arg.Type}\");");
                            decodeBody.AddOpeningLine($"v{propName}.Decode(eventData.Skip(index{propName}).ToArray());");
                            decodeBody.AddOpeningLine($"var value{propName} = v{propName}.ToValue();");
                            decodeBody.AddOpeningLine($"if (value{propName} is string v{propName}Value) {{ ret.{propName} = v{propName}Value; }}");
                        }
                        else
                        {
                            decodeBody.AddOpeningLine($"AVM.ClientGenerator.ABI.ARC4.Types.WireType v{propName} = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription(\"{arg.Type}\");");
                            decodeBody.AddOpeningLine($"count = v{propName}.Decode(queue.ToArray());");
                            decodeBody.AddOpeningLine($"for (int i = 0; i < Convert.ToInt32(count); i++){{queue.Dequeue();}}");
                            decodeBody.AddOpeningLine($"var value{propName} = v{propName}.ToValue();");
                            decodeBody.AddOpeningLine($"if (value{propName} is {argCsTypes[i]} v{propName}Value){{ret.{propName} = v{propName}Value;}}");
                        }
                    }
                }
                decodeBody.AddOpeningLine("return ret;");
            }
        }

        private void defineMethods(Code proxyBody, List<string> structs)
        {
            if (!this.Contract.Methods.Any(m => m.Name.ToLower() == "createapplication"))
            {
                // make custom deploy method
                this.Contract.Methods.Add(new Method()
                {
                    Name = "CreateApplication",
                    Actions = new MethodActions() { Create = new List<string>() { }, },
                    Args = new List<MethodArgument>() { },
                    Description = "Constructor Bare Action",
                    ReadOnly = false,
                    Events = new List<Event>() { },
                    Recommendations = new Recommendations() { },
                    Returns = new MethodReturn() { Type = "void" }
                });
            }

            foreach (var method in this.Contract.Methods)
            {
                var returnType = method.Returns;
                var methodName = method.Name;
                List<MethodArgument> transactionParameters = new List<MethodArgument>();
                List<MethodArgument> appRefParameters = new List<MethodArgument>();
                List<MethodArgument> acctRefParameters = new List<MethodArgument>();
                List<MethodArgument> assetRefParameters = new List<MethodArgument>();
                List<MethodArgument> argParameters = new List<MethodArgument>();

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
                var prependArgs = "";
                var convertedToAbi = new HashSet<string>();
                foreach (var arg in method.Args)
                {
                    var type = TypeHelpers.GetCSType(arg.Name, arg.Type, arg.Struct, structs, false);
                    if (type.abiType.StartsWith("AVM.") && !type.abiType.StartsWith(type.type))
                    {
                        prependArgs += $"var {arg.Name}Abi = new {type.abiType};{arg.Name}Abi.From({MethodDescription.SafeIdentifier(arg.Name)});\n";
                        convertedToAbi.Add(arg.Name);
                    }
                }
                string argsList = "new List<object> {" + string.Join(",", new List<string> { "abiHandle" }.Concat(method.Args.Select(p =>
                {
                    // ARC4 account/asset/application reference args are not ABI-encoded by value: the actual
                    // account/asset/app ID goes into the txn's foreign-reference arrays (_tx_accounts/_tx_assets/_tx_apps,
                    // populated below), and the ABI arg itself is just a 1-byte index into that array (accounts are
                    // 1-based, since index 0 implicitly means the sender; assets/apps are 0-based). See EmitRefIndexAssignments.
                    if (p.IsAccountRef() || p.IsAssetRef() || p.IsApplicationRef()) return p.Name + "RefIndex";
                    return convertedToAbi.Contains(p.Name) ? p.Name + "Abi" : MethodDescription.SafeIdentifier(p.Name);
                }))) + "}";
                if (method.Description == "Constructor Bare Action")
                {
                    argsList = "new List<object> {}";
                }


                var t = TypeHelpers.GetCSType(MethodDescription.FormatStructName(methodName + "Return"), returnType.Type, returnType.Struct, structs, false);
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
        ///{method.Description?.Replace("\n", "\n///") ?? ""}
        ///</summary>");

                foreach (var parm in method.Args)
                {
                    abiMethod.AddOpeningLine($@"/// <param name=""{parm.Name}"">{parm.Description?.Replace("\n", "\n///")} {parm.Struct?.Replace("\n", "\n///")}</param>");
                }

                if (!string.IsNullOrEmpty(parameters))
                {
                    parameters += ", ";
                }
                var defaultOp = "AVM.ClientGenerator.Core.OnCompleteType.NoOp";
                if (method.Name == "CreateApplication")
                {
                    defaultOp = "AVM.ClientGenerator.Core.OnCompleteType.CreateApplication";
                }
                abiMethod.AddOpeningLine($"public async {methodReturnType} {methodName.ToPascalCase()} ({parameters}Account _tx_sender, ulong? _tx_fee,string _tx_note = \"\", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = {defaultOp} )".Replace(",,", ",").Replace(", ,", ","));
                abiMethod.AddOpeningLine("{");
                abiMethod.AddClosingLine("}");

                abiMethodForTransactions.AddOpeningLine($"public async Task<List<Transaction>> {methodName.ToPascalCase()}_Transactions ({parameters}Account _tx_sender, ulong? _tx_fee, string _tx_note = \"\", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = {defaultOp} )".Replace(",,", ",").Replace(", ,", ","));
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
                    abiMethodBody.AddOpeningLine("_tx_transactions.AddRange(new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => MethodDescription.SafeIdentifier(p.Name))) + "});");
                }

                EmitRefIndexAssignments(abiMethodBody, appRefParameters, assetRefParameters, acctRefParameters);


                abiMethodBody.AddOpeningLine($"byte[] abiHandle = {{{string.Join(",", method.ToARC4MethodSelector())}}};");
                abiMethodBody.AddOpeningLine(prependArgs);
                var simOrCall = "CallApp";
                if (method.ReadOnly == true)
                {
                    simOrCall = "SimApp";
                }

                abiMethodBody.AddOpeningLine($"var result = await base.{simOrCall}({argsList}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);");

                if (t.type != "void")
                {
                    string scalarStructType = !string.IsNullOrEmpty(returnType.Struct) ? $"Structs.{MethodDescription.FormatStructName(returnType.Struct)}" : null;
                    if (scalarStructType != null && t.type == scalarStructType)
                    {
                        abiMethodBody.AddOpeningLine("var lastLogBytes = result.Last();");
                        abiMethodBody.AddOpeningLine("if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception(\"Invalid ABI handle\");");
                        abiMethodBody.AddOpeningLine($"return {t.type}.Parse(lastLogBytes.Skip(4).ToArray());");
                    }
                    else if (scalarStructType != null && t.type == scalarStructType + "[]")
                    {
                        abiMethodBody.AddOpeningLine("var lastLogBytes = result.Last();");
                        abiMethodBody.AddOpeningLine("if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception(\"Invalid ABI handle\");");
                        abiMethodBody.AddOpeningLine("var lastLogReturnData = lastLogBytes.Skip(4).ToArray();");
                        abiMethodBody.AddOpeningLine($"var returnValueObj = new {t.abiType};");
                        abiMethodBody.AddOpeningLine("returnValueObj.Decode(lastLogReturnData);");
                        abiMethodBody.AddOpeningLine("return returnValueObj.Value.ToArray();");
                    }
                    else
                    if (ProxyGenerator.returnTypeConversions.TryGetValue(t.type, out string retline))
                    {
                        abiMethodBody.AddOpeningLine("var lastLogBytes = result.Last();");
                        abiMethodBody.AddOpeningLine("if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception(\"Invalid ABI handle\");");
                        abiMethodBody.AddOpeningLine("var lastLogReturnData = lastLogBytes.Skip(4).ToArray();");
                        abiMethodBody.AddOpeningLine($"var returnValueObj = new {t.abiType};");
                        abiMethodBody.AddOpeningLine($"returnValueObj.Decode(lastLogReturnData);");
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
                    abiMethodBodyForTransactions.AddOpeningLine("_tx_transactions.AddRange(new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => MethodDescription.SafeIdentifier(p.Name))) + "});");
                }

                EmitRefIndexAssignments(abiMethodBodyForTransactions, appRefParameters, assetRefParameters, acctRefParameters);
                abiMethodBodyForTransactions.AddOpeningLine($"byte[] abiHandle = {{{string.Join(",", method.ToARC4MethodSelector())}}};");
                abiMethodBodyForTransactions.AddOpeningLine(prependArgs);
                abiMethodBodyForTransactions.AddOpeningLine($"return await base.MakeTransactionList({argsList}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);");
            }
        }

        /// <summary>
        /// Appends account/asset/application reference args to their respective _tx_accounts/_tx_assets/_tx_apps
        /// foreign-reference arrays, and emits a "{name}RefIndex" byte variable for each - the 1-byte ARC4-encoded
        /// index into that array that actually gets sent as the ABI argument (see the argsList construction
        /// above). Per ARC4: asset/application reference indices are 0-based directly into Txn.Assets[]/
        /// Txn.Applications[]; account reference indices are 1-based into Txn.Accounts[], since index 0 implicitly
        /// refers to the transaction sender.
        /// </summary>
        private void EmitRefIndexAssignments(Code body, List<MethodArgument> appRefParameters, List<MethodArgument> assetRefParameters, List<MethodArgument> acctRefParameters)
        {
            if (appRefParameters.Count > 0)
            {
                body.AddOpeningLine("int _appRefBase = _tx_apps.Count;");
                body.AddOpeningLine("_tx_apps.AddRange(new List<ulong> {" + string.Join(",", appRefParameters.Select(p => MethodDescription.SafeIdentifier(p.Name))) + "});");
                for (int i = 0; i < appRefParameters.Count; i++)
                {
                    body.AddOpeningLine($"byte {appRefParameters[i].Name}RefIndex = (byte)(_appRefBase + {i});");
                }
            }

            if (assetRefParameters.Count > 0)
            {
                body.AddOpeningLine("int _assetRefBase = _tx_assets.Count;");
                body.AddOpeningLine("_tx_assets.AddRange(new List<ulong> {" + string.Join(",", assetRefParameters.Select(p => MethodDescription.SafeIdentifier(p.Name))) + "});");
                for (int i = 0; i < assetRefParameters.Count; i++)
                {
                    body.AddOpeningLine($"byte {assetRefParameters[i].Name}RefIndex = (byte)(_assetRefBase + {i});");
                }
            }

            if (acctRefParameters.Count > 0)
            {
                body.AddOpeningLine("int _acctRefBase = _tx_accounts.Count;");
                body.AddOpeningLine("_tx_accounts.AddRange(new List<Address> {" + string.Join(",", acctRefParameters.Select(p => MethodDescription.SafeIdentifier(p.Name))) + "});");
                for (int i = 0; i < acctRefParameters.Count; i++)
                {
                    body.AddOpeningLine($"byte {acctRefParameters[i].Name}RefIndex = (byte)(_acctRefBase + {i} + 1);");
                }
            }
        }

        private string defineArgParameter(MethodArgument p, string methodName, List<string> structs)
        {
            var type = TypeHelpers.GetCSType(MethodDescription.FormatStructName(methodName + "_arg_" + p.Name), p.Type, p.Struct, structs, false).type;
            return $"{type} {MethodDescription.SafeIdentifier(p.Name)}";
        }

        private string defineAssetRefParameter(MethodArgument p)
        {
            return $"ulong {MethodDescription.SafeIdentifier(p.Name)}";
        }

        private string defineAcctRefParameter(MethodArgument p)
        {
            return $"Address {MethodDescription.SafeIdentifier(p.Name)}";
        }

        private string defineTransactionParameter(MethodArgument p)
        {
            string parmType = p.Type.ToString();
            if (!string.IsNullOrWhiteSpace(p.Struct)) parmType = p.Struct;
            string outputParmType = TypeHelpers.determineTransactionType(parmType);
            return $"{outputParmType} {MethodDescription.SafeIdentifier(p.Name)}";
        }


        private string defineAppRefParameter(MethodArgument p)
        {
            return $"ulong {MethodDescription.SafeIdentifier(p.Name)}";
        }
    }
}
