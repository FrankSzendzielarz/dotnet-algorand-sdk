using MessagePack;
using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [MessagePackObject(AllowPrivate = true)]
    public class StateProofTransaction : Transaction
    {
        [JsonProperty(PropertyName = "type")]
        [MessagePack.Key("type")]
        public string type => "stpf";
    }
}
