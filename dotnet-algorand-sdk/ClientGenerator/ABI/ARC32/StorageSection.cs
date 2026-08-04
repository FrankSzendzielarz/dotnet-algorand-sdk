using AVM.ClientGenerator.Clients;
using AVM.ClientGenerator.Compiler;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVM.ClientGenerator.ABI.ARC32
{
    public class StorageSection
    {
        [Newtonsoft.Json.JsonProperty("num_byte_slices")]
        public ulong? NumByteSlices { get; set; } = 0;
        [Newtonsoft.Json.JsonProperty("num_uints")]
        public ulong? NumUints { get; set; } = 0;
        public Dictionary<string, StorageElement> Declared { get; set; } = new Dictionary<string, StorageElement>();

        internal void ToProxy(Code proxyBody, List<string> structs, bool local)
        {
            foreach (var kv in Declared)
            {
                Code code = proxyBody.AddChild();
                if (!string.IsNullOrWhiteSpace(kv.Value.Descr))
                {
                    code.AddOpeningLine(
                    $@"{"\t"}///<summary>
                        {"\t"}///{kv.Value.Descr?.Replace("\n", "\n///")}
                        {"\t"}///</summary>");


                }
                var returnType = TypeHelpers.GetCSType(kv.Key + "Type", kv.Value.Type, kv.Value.TypeDetail, structs, false);
                var fieldName = kv.Key;
                var fieldKey = kv.Value.Key;

                Core.VariableType machineValueType = Core.VariableType.Unsupported;
                if (TealTypeUtils.PredefinedByteSliceStoreTypes.Contains(returnType.type)) machineValueType = Core.VariableType.ByteSlice;
                if (TealTypeUtils.PredefinedIntegerStoredTypes.Contains(returnType.type)) machineValueType = Core.VariableType.UInt64;
                string typeCall;
                string retline = $"return ({returnType.type})result;"; ;
                switch (machineValueType)
                {
                    case Core.VariableType.ByteSlice:
                        typeCall = local ? "GetLocalByteSlice(caller,key)" : "GetGlobalByteSlice(key)";
                        if (ProxyGenerator.storageConversions.TryGetValue(returnType.type, out retline))
                        {

                        }
                        else
                        {
                            retline = "return; // <unknown return conversion>";
                        }

                        break;
                    case Core.VariableType.UInt64: typeCall = local ? "GetLocalUInt(caller,key)" : "GetGlobalUInt(key)"; break;
                    default:
                        throw new Exception("Unsupported field type.");
                }

                if (local)
                {
                    code.AddOpeningLine($"public async Task<{returnType.type}> {fieldName}(Account caller)");
                }
                else
                {
                    code.AddOpeningLine($"public async Task<{returnType.type}> {fieldName}()");
                }
                code.AddOpeningLine("{");
                var body = code.AddChild();
                body.AddOpeningLine($"var key=\"{fieldName}\";");
                body.AddOpeningLine($"var result= await base.{typeCall};");
                body.AddOpeningLine(retline);
                code.AddClosingLine("}");
            }
        }




        internal void ToSmartContractReference(StringBuilder stateBuilder, string storageAttribute, List<string> structs)
        {
            foreach (var kv in Declared)
            {
                stateBuilder.AppendLine($"\t\t{storageAttribute}");

                var t = TypeHelpers.GetCSType(kv.Key + "Type", kv.Value.Type, kv.Value.TypeDetail, structs, false);

                if (!string.IsNullOrWhiteSpace(t.bitwidthAttrib)) stateBuilder.AppendLine($"\t\t{t.bitwidthAttrib}");
                stateBuilder.AppendLine($"\t\tpublic {t.bitwidthAttrib}{t.type} {kv.Value.Key};");

                stateBuilder.AppendLine();
            }
        }


    }
}
