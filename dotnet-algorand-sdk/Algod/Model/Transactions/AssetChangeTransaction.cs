using JsonSubTypes;
using MessagePack;
using Newtonsoft.Json;


namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(AssetUpdateTransaction),"apar")]
    [JsonSubtypes.FallBackSubType(typeof(AssetDestroyTransaction))]
    [MessagePackObject]
    [Union(0, typeof(AssetUpdateTransaction))]
    [Union(1, typeof(AssetDestroyTransaction))]
    public abstract partial class AssetChangeTransaction : AssetConfigurationTransaction
    { 
      
    }
}
