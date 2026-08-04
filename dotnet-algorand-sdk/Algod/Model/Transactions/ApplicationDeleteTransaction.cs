


using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [MessagePack.MessagePackObject(AllowPrivate = true)]
    public  partial class ApplicationDeleteTransaction : ApplicationCallTransaction
    {
        [JsonProperty(PropertyName = "apan")]
        [MessagePack.Key("apan")]
        public OnCompletion OnCompletion => OnCompletion.Delete;
    }
}
