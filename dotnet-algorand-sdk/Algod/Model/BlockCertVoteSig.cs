namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]

    public class BlockCertVoteSig
    {
        [Newtonsoft.Json.JsonProperty("p")]
        [MessagePack.Key("p")]
        public byte[]? P { get; set; }
        [Newtonsoft.Json.JsonProperty("p1s")]
        [MessagePack.Key("p1s")]
        public byte[]? P1S { get; set; }
        [Newtonsoft.Json.JsonProperty("p2")]
        [MessagePack.Key("p2")]
        public byte[]? P2 { get; set; }
        [Newtonsoft.Json.JsonProperty("p2s")]
        [MessagePack.Key("p2s")]
        public byte[]? P2S { get; set; }
        [Newtonsoft.Json.JsonProperty("ps")]
        [MessagePack.Key("ps")]
        public byte[]? PS { get; set; }
        [Newtonsoft.Json.JsonProperty("s")]
        [MessagePack.Key("s")]
        public byte[]? S { get; set; }
    }
}
