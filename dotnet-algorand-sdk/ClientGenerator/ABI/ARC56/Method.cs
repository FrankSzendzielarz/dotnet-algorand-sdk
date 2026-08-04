using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVM.ClientGenerator.ABI.ARC56
{
    /// <summary>
    /// Every method in the contract is described via a Method interface. This interface is an extension of the one defined in ARC-4.
    /// </summary>
    public class Method
    {
        /// <summary>
        /// The name of the method
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the method
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the method
        /// </summary>

        [JsonProperty("args")]
        public List<MethodArgument> Args { get; set; } = new List<MethodArgument>();
        /// <summary>
        /// Information about the method's return value
        /// </summary>
        [JsonProperty("returns")]
        public MethodReturn Returns { get; set; }
        /// <summary>
        /// an action is a combination of call/create and an OnComplete
        /// </summary>

        [JsonProperty("actions")]
        public MethodActions Actions { get; set; }
        /// <summary>
        /// If this method does not write anything to the ledger (ARC-22)
        /// </summary>

        [JsonProperty("readonly")]
        public bool? ReadOnly { get; set; }
        /// <summary>
        /// ARC-28 events that MAY be emitted by this method
        /// </summary>

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
        /// <summary>
        /// Information that clients can use when calling the method
        /// </summary>

        [JsonProperty("recommendations")]
        public Recommendations Recommendations { get; set; }

        public string ToARC4MethodSignature()
        {
            return $"{Name}({string.Join(",", Args.Select(a => a.Type))}){Returns.Type}";
        }
        public byte[] ToARC4MethodSelector()
        {
            var data = Encoding.ASCII.GetBytes(ToARC4MethodSignature());
            Sha512tDigest digest = new Sha512tDigest(256);
            digest.BlockUpdate(data, 0, data.Length);
            byte[] output = new byte[32];
            digest.DoFinal(output, 0);
            return output.Take(4).ToArray();
        }
    }
    public class MethodArgument
    {
        /// <summary>
        /// The type of the argument. The `struct` field should also be checked to determine if this arg is a struct. 
        /// 
        /// ABIType
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// If the type is a struct, the name of the struct
        /// 
        /// StructName
        /// </summary>
        [JsonProperty("struct")]
        public string Struct { get; set; }
        /// <summary>
        /// Optional, user-friendly name for the argument
        /// </summary>

        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the argument
        /// </summary>

        [JsonProperty("desc")]
        public string Description { get; set; }
        /// <summary>
        /// The default value that clients should use.
        /// </summary>

        [JsonProperty("defaultValue")]
        public DefaultValue DefaultValue { get; set; }


        internal bool IsAccountRef()
        {
            // The ARC4 foreign-reference type is literally "account" (a 1-byte index into Txn.Accounts[]),
            // distinct from the plain "address" value type (32 raw bytes) - conflating the two here would
            // misencode any method that takes a plain address value (see AVMTypes' account(address)address).
            return Type == "account";
        }

        internal bool IsApplicationRef()
        {
            return Type == "application";
        }

        internal bool IsAssetRef()
        {
            return Type == "asset";
        }

        internal bool IsTransaction()
        {
            return !string.IsNullOrWhiteSpace(TypeHelpers.TransactionReferenceToInnerTransaction(Type));
        }
    }
    public class DefaultValue
    {

        /// <summary>
        /// Where the default value is coming from
        /// - box: The data key signifies the box key to read the value from
        /// - global: The data key signifies the global state key to read the value from
        /// - local: The data key signifies the local state key to read the value from (for the sender)
        /// - literal: the value is a literal and should be passed directly as the argument
        /// - method: The utf8 signature of the method in this contract to call to get the default value. If the method has arguments, they all must have default values. The method **MUST** be readonly so simulate can be used to get the default value.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
        /// <summary>
        /// Base64 encoded bytes, base64 ARC4 encoded uint64, or UTF-8 method selector
        /// </summary>

        [JsonProperty("data")]
        public string Data { get; set; }
        /// <summary>
        /// How the data is encoded. This is the encoding for the data provided here, not the arg type. Undefined if the data is method selector
        /// ABIType | AVMType
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
    /// <summary>
    /// Information about the method's return value
    /// </summary>
    public class MethodReturn
    {
        /// <summary>
        /// The type of the return value, or "void" to indicate no return value. The `struct` field should also be checked to determine if this return value is a struct.
        /// ABIType
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// If the type is a struct, the name of the struct
        /// StructName
        /// </summary>

        [JsonProperty("struct")]
        public string Struct { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the return value
        /// </summary>

        [JsonProperty("desc")]
        public string Description { get; set; }
    }
    /// <summary>
    /// an action is a combination of call/create and an OnComplete
    /// </summary>
    public class MethodActions
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
    /// Information that clients can use when calling the method
    /// </summary>
    public class Recommendations
    {
        /// <summary>
        /// The number of inner transactions the caller should cover the fees for
        /// </summary>
        [JsonProperty("innerTransactionCount")]
        public int? InnerTransactionCount { get; set; }
        /// <summary>
        /// Recommended box references to include
        /// </summary>

        [JsonProperty("boxes")]
        public List<RecommendationBox> Boxes { get; set; }
        /// <summary>
        /// Recommended foreign accounts
        /// </summary>
        [JsonProperty("accounts")]
        public List<string> Accounts { get; set; }
        /// <summary>
        /// Recommended foreign apps
        /// </summary>
        [JsonProperty("apps")]
        public List<int> Apps { get; set; }
        /// <summary>
        /// Recommended foreign assets
        /// </summary>
        [JsonProperty("assets")]
        public List<int> Assets { get; set; }
    }

    public class RecommendationBox
    {
        /// <summary>
        /// The app ID for the box
        /// </summary>
        [JsonProperty("app")]
        public int? App { get; set; }
        /// <summary>
        /// The base64 encoded box key
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }
        /// <summary>
        /// The number of bytes being read from the box
        /// </summary>
        [JsonProperty("readBytes")]
        public int ReadBytes { get; set; }
        /// <summary>
        /// The number of bytes being written to the box
        /// </summary>
        [JsonProperty("writeBytes")]
        public int WriteBytes { get; set; }
    }


    public class Event
    {
        /// <summary>
        /// The name of the event
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the event
        /// </summary>

        [JsonProperty("desc")]
        public string Description { get; set; }
        /// <summary>
        /// The arguments of the event, in order
        /// </summary>

        [JsonProperty("args")]
        public List<EventArgument> Args { get; set; }
    }
    /// <summary>
    /// The arguments of the event, in order
    /// </summary>
    public class EventArgument
    {
        /// <summary>
        /// The type of the argument. The `struct` field should also be checked to determine if this arg is a struct.
        /// ABIType
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// If the type is a struct, the name of the struct
        /// StructName
        /// </summary>
        [JsonProperty("struct")]
        public string Struct { get; set; }
        /// <summary>
        /// Optional, user-friendly name for the argument
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Optional, user-friendly description for the argument
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }
    }
}