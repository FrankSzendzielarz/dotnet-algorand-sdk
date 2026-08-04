using JsonSubTypes;
using MessagePack;
using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.FallBackSubType(typeof(KeyRegisterOfflineTransaction))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(KeyRegisterOnlineTransaction), "votekey")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(KeyRegisterOnlineTransaction), "selkey")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(KeyRegisterOnlineTransaction), "votefst")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(KeyRegisterOnlineTransaction), "votelst")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(KeyRegisterOnlineTransaction), "votekd")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(KeyRegisterOnlineTransaction), "nonpart")]
    [MessagePackObject]
    [Union(0, typeof(KeyRegisterOnlineTransaction))]
    [Union(1, typeof(KeyRegisterOfflineTransaction))]
    public abstract partial class KeyRegistrationTransaction : Transaction
    {
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [MessagePack.Key("type")]
        public string type => "keyreg";
    }
}
