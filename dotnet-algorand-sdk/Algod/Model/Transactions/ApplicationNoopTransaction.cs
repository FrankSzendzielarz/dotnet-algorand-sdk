


using JsonSubTypes;
using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ApplicationCreateTransaction), "apap")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ApplicationCreateTransaction), "apsu")]
    [JsonSubtypes.FallBackSubType(typeof(ApplicationNoopTransaction))]
    [MessagePack.MessagePackObject(AllowPrivate = true)]

    public partial class ApplicationNoopTransaction : ApplicationCallTransaction
    {
        [JsonProperty(PropertyName = "apan")]
        [MessagePack.Key("apan")]
        public OnCompletion OnCompletion => OnCompletion.Noop;

    }
}
