using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC56
{
    /// <summary>
    /// Represents an ARC32 app description
    /// </summary>
    public class AppDescriptionArc56
    {

        #region Members
        /// <summary>
        /// The ARCs used and/or supported by this contract. All contracts implicitly support ARC4 and ARC56
        /// </summary>
        [Newtonsoft.Json.JsonProperty("arcs")]
        public int[] ARCs { get; set; }
        /// <summary>
        /// A user-friendly name for the contract
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the interface
        /// </summary>
        [Newtonsoft.Json.JsonProperty("desc")]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// Optional object listing the contract instances across different networks.
        /// The key is the base64 genesis hash of the network, and the value contains
        /// information about the deployed contract in the network indicated by the
        /// key. A key containing the human-readable name of the network MAY be
        /// included, but the corresponding genesis hash key MUST also be defined
        /// </summary>
        [Newtonsoft.Json.JsonProperty("networks")]
        public Dictionary<string, NetworkInfo> Networks { get; set; } = new Dictionary<string, NetworkInfo>();
        /// <summary>
        /// Named structs used by the application. Each struct field appears in the same order as ABI encoding.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("structs")]
        public Dictionary<string, List<StructField>> Structs { get; set; } = new Dictionary<string, List<StructField>>();
        /// <summary>
        /// All of the methods that the contract implements
        /// </summary>
        public List<Method> Methods { get; set; } = new List<Method>();
        /// <summary>
        /// Contract state.
        /// </summary>
        [JsonProperty("state")]
        public ContractState State { get; set; }

        /// <summary>
        /// Supported bare actions for the contract.
        /// </summary>
        [JsonProperty("bareActions")]
        public BareActions BareActions { get; set; }

        /// <summary>
        /// Information about the TEAL programs.
        /// </summary>
        [JsonProperty("sourceInfo")]
        public SourceInfoArgument SourceInfo { get; set; }

        /// <summary>
        /// The pre-compiled TEAL that may contain template variables.
        /// </summary>
        [JsonProperty("source")]
        public ProgramSource Source { get; set; }
        /// <summary>
        /// The compiled bytecode for the application.
        /// </summary>
        [JsonProperty("byteCode")]
        public ProgramSource ByteCode { get; set; }

        /// <summary>
        /// Information used to get the given byteCode and/or PC values in sourceInfo.
        /// </summary>
        [JsonProperty("compilerInfo")]
        public CompilerInfo CompilerInfo { get; set; }
        /// <summary>
        /// ARC-28 events that may be emitted by this contract.
        /// </summary>
        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        /// <summary>
        /// A mapping of template variable names to their respective types and values.
        /// </summary>
        [JsonProperty("templateVariables")]
        public Dictionary<string, TemplateVariable> TemplateVariables { get; set; } = new Dictionary<string, TemplateVariable>();

        /// <summary>
        /// The scratch variables used during runtime.
        /// </summary>
        [JsonProperty("scratchVariables")]
        public Dictionary<string, ScratchVariable> ScratchVariables { get; set; } = new Dictionary<string, ScratchVariable>();
        #endregion
    }
    /// <summary>
    /// Netowork data structure
    /// </summary>
    public class NetworkInfo
    {
        /// <summary>
        /// The app ID of the deployed contract in this network
        /// </summary>
        [Newtonsoft.Json.JsonProperty("appID")]
        public ulong AppID { get; set; }
    }
    /// <summary>
    /// StructField data structure
    /// </summary>
    public class StructField
    {
        /// <summary>
        /// The name of the struct field
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// The type of the struct field's value
        /// 
        /// ABIType | StructName | StructField[]
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }
    }
    /// <summary>
    /// Defines the values that should be used for GlobalNumUint, GlobalNumByteSlice, LocalNumUint, and LocalNumByteSlice when creating the application
    /// </summary>
    public class ContractState
    {
        /// <summary>
        /// Defines the values that should be used for GlobalNumUint, GlobalNumByteSlice, LocalNumUint, and LocalNumByteSlice when creating the application
        /// </summary>
        [JsonProperty("schema")]
        public Schema Schema { get; set; } = new Schema();
        /// <summary>
        /// Mapping of human-readable names to StorageKey objects
        /// </summary>
        [JsonProperty("keys")]
        public Dictionary<string, StorageKey> Keys { get; set; } = new Dictionary<string, StorageKey>();
        /// <summary>
        /// Mapping of human-readable names to StorageMap objects
        /// </summary>
        [JsonProperty("maps")]
        public Dictionary<string, StorageMap> Maps { get; set; } = new Dictionary<string, StorageMap>();
    }

    public class Schema
    {
        [JsonProperty("global")]
        public SchemaValues Global { get; set; }
        [JsonProperty("local")]
        public SchemaValues Local { get; set; }
    }
    public class SchemaValues
    {
        [JsonProperty("ints")]
        public ulong Ints { get; set; } = 0;

        [JsonProperty("bytes")]
        public ulong Bytes { get; set; } = 0;
    }
    /// <summary>
    /// Supported bare actions for the contract. An action is a combination of call/create and an OnComplete
    /// </summary>
    public class BareActions
    {
        /// <summary>
        /// OnCompletes this method allows when appID === 0
        /// </summary>
        [JsonProperty("create")]
        public List<string> Create { get; set; }
        /// <summary>
        /// OnCompletes this method allows when appID !== 0
        /// </summary>
        [JsonProperty("call")]
        public List<string> Call { get; set; }
    }
    /// <summary>
    /// Information about the TEAL programs
    /// </summary>
    public class SourceInfoArgument
    {
        /// <summary>
        /// Approval program information
        /// </summary>
        [JsonProperty("approval")]
        public ProgramSourceInfo Approval { get; set; } = new ProgramSourceInfo();
        /// <summary>
        /// Clear program information
        /// </summary>
        [JsonProperty("clear")]
        public ProgramSourceInfo Clear { get; set; } = new ProgramSourceInfo();
    }

    public class ProgramSource
    {
        [JsonProperty("approval")]
        public string Approval { get; set; }

        [JsonProperty("clear")]
        public string Clear { get; set; }
    }

    public class CompilerInfo
    {
        [JsonProperty("compiler")]
        public string Compiler { get; set; }

        [JsonProperty("compilerVersion")]
        public CompilerVersion CompilerVersion { get; set; } = new CompilerVersion();
    }

    public class CompilerVersion
    {
        [JsonProperty("major")]
        public int Major { get; set; }

        [JsonProperty("minor")]
        public int Minor { get; set; }

        [JsonProperty("patch")]
        public int Patch { get; set; }

        [JsonProperty("commitHash")]
        public string CommitHash { get; set; }
    }

    public class TemplateVariable
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ScratchVariable
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Describes a single key in app storage.
    /// </summary>
    public class StorageKey
    {
        /// <summary>
        /// Description of what this storage key holds.
        /// </summary>
        [JsonProperty("desc")]
        public string? Description { get; set; }

        /// <summary>
        /// The type of the key.
        /// </summary>
        [JsonProperty("keyType")]
        public string KeyType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the value.
        /// </summary>
        [JsonProperty("valueType")]
        public string ValueType { get; set; } = string.Empty;

        /// <summary>
        /// The bytes of the key encoded as base64.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;
    }

    /// <summary>
    /// Describes a mapping of key-value pairs in storage.
    /// </summary>
    public class StorageMap
    {
        /// <summary>
        /// Description of what the key-value pairs in this mapping hold.
        /// </summary>
        [JsonProperty("desc")]
        public string? Description { get; set; }

        /// <summary>
        /// The type of the keys in the map.
        /// </summary>
        [JsonProperty("keyType")]
        public string KeyType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the values in the map.
        /// </summary>
        [JsonProperty("valueType")]
        public string ValueType { get; set; } = string.Empty;

        /// <summary>
        /// The base64-encoded prefix of the map keys.
        /// </summary>
        [JsonProperty("prefix")]
        public string? Prefix { get; set; }
    }

    /// <summary>
    /// The source information for the program.
    /// </summary>
    public class ProgramSourceInfo
    {
        /// <summary>
        /// The source information for the program.
        /// </summary>
        [JsonProperty("sourceInfo")]
        public List<SourceInfo> SourceInfo { get; set; } = new List<SourceInfo>();

        /// <summary>
        /// How the program counter offset is calculated.
        /// - none: The pc values in sourceInfo are not offset.
        /// - cblocks: The pc values in sourceInfo are offset by the PC of the first op following the last cblock at the top of the program.
        /// </summary>
        [JsonProperty("pcOffsetMethod")]
        public string PcOffsetMethod { get; set; }
    }

    /// <summary>
    /// The source information details.
    /// </summary>
    public class SourceInfo
    {
        /// <summary>
        /// The program counter value(s). Could be offset if pcOffsetMethod is not "none".
        /// </summary>
        [JsonProperty("pc")]
        public List<int> Pc { get; set; } = new List<int>();

        /// <summary>
        /// A human-readable string that describes the error when the program fails at the given PC.
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The TEAL line number that corresponds to the given PC. RECOMMENDED to be used for development purposes, but not required for clients.
        /// </summary>
        [JsonProperty("teal")]
        public int? Teal { get; set; }

        /// <summary>
        /// The original source file and line number that corresponds to the given PC. RECOMMENDED to be used for development purposes, but not required for clients.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
    }

}

