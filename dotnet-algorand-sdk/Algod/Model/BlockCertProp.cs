namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]
    public class BlockCertProp
    {
        [Newtonsoft.Json.JsonProperty("dig")]
        [MessagePack.Key("dig")]
        public byte[]? Dig { get; set; }
        [Newtonsoft.Json.JsonProperty("encdig")]
        [MessagePack.Key("encdig")]
        public byte[]? EncDig { get; set; }
        [Newtonsoft.Json.JsonProperty("oprop")]
        [MessagePack.Key("oprop")]
        public byte[]? Oprop { get; set; }
    }
}
