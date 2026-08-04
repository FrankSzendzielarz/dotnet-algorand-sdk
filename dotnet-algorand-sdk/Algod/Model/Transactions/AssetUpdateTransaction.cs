using JsonSubTypes;
using MessagePack;
using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(AssetUpdateTransaction),"apar")]
    [JsonSubtypes.FallBackSubType(typeof(AssetDestroyTransaction))]
    [MessagePackObject(AllowPrivate = true)]
    public partial class AssetUpdateTransaction : AssetChangeTransaction
    {
    


    }
}
