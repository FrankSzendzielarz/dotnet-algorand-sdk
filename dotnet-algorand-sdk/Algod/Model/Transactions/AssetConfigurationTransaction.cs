using JsonSubTypes;
using MessagePack;
using Newtonsoft.Json;


namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(AssetUpdateTransaction), "caid")]
    [JsonSubtypes.FallBackSubType(typeof(AssetCreateTransaction))]
    [MessagePackObject]
    [Union(0, typeof(AssetUpdateTransaction))]
    [Union(1, typeof(AssetCreateTransaction))]
    public abstract partial class AssetConfigurationTransaction : Transaction
    {
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [MessagePack.Key("type")]
        public string type => "acfg";
    }
}
