using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AVM.ClientGenerator.ABI.ARC32
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ArgumentDefaultSource
    {
        [EnumMember(Value = "constant")]
        Constant,

        [EnumMember(Value = "global-state")]
        GlobalState,

        [EnumMember(Value = "local-state")]
        LocalState,

        [EnumMember(Value = "abi-method")]
        AbiMethod
    }


}

