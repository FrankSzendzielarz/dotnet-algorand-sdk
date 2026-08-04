


using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [MessagePack.MessagePackObject(AllowPrivate = true)]
    public partial class ApplicationUpdateTransaction : ApplicationCallTransaction
    {
        [JsonProperty(PropertyName = "apan")]
        [MessagePack.Key("apan")]
        public OnCompletion OnCompletion => OnCompletion.Update;

        public bool ShouldSerializeGlobalStateSchema()
        {
            return GlobalStateSchema!=null && (GlobalStateSchema.NumByteSlice != 0 || GlobalStateSchema.NumUint != 0);
        }

        public bool ShouldSerializeLocalStateSchema()
        {
            return LocalStateSchema != null && (LocalStateSchema.NumByteSlice != 0 || LocalStateSchema.NumUint != 0);
        }

        public bool ShouldSerializeExtraProgramPages()
        {
            return ExtraProgramPages != 0;
        }

    }
}
