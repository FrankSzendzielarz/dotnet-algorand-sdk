
using JsonSubTypes;
using Newtonsoft.Json;
#if UNITY
using UnityEngine;
#endif

namespace Algorand.Algod.Model.Transactions
{

    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(AssetCloseTransaction), "aclose", StopLookupOnMatch = true)]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(AssetTransferTransaction), "aamt", StopLookupOnMatch = true)]
    [JsonSubtypes.FallBackSubType(typeof(AssetAcceptTransaction))]

    public abstract partial class AssetMovementsTransaction : Transaction
    {
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [MessagePack.Key("type")]
        public string type => "axfer";


        [Newtonsoft.Json.JsonProperty("aamt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("aamt")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetAmount")]
    public ulong AssetAmount {get;set;}
#else
        public ulong AssetAmount { get; set; }
#endif
        [Newtonsoft.Json.JsonProperty("arcv", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("arcv")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetReceiver")]
    public Algorand.Address AssetReceiver {get;set;}
#else
        public Algorand.Address AssetReceiver { get; set; }
#endif

    }
}
