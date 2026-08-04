using System.Collections.Generic;

namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]
    public partial class BlockCert
    {
        [Newtonsoft.Json.JsonProperty("prop")]
        [MessagePack.Key("prop")]
        public BlockCertProp? Prop { get; set; }
        [Newtonsoft.Json.JsonProperty("rnd")]
        [MessagePack.Key("rnd")]
        public ulong? Round { get; set; }
        [Newtonsoft.Json.JsonProperty("step")]
        [MessagePack.Key("step")]
        public ulong? Step { get; set; }

        [Newtonsoft.Json.JsonProperty("vote")]
        [MessagePack.Key("vote")]
        public ICollection<BlockCertVote> Votes { get; set; }
    }
}
