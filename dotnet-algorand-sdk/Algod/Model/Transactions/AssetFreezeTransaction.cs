

using MessagePack;
using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [MessagePackObject(AllowPrivate = true)]
    public partial class AssetFreezeTransaction : Transaction
    {
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [MessagePack.Key("type")]
        public string type => "afrz";

       
    }
}
