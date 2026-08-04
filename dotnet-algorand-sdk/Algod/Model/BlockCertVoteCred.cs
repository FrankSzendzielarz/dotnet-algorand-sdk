namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]
    public class BlockCertVoteCred
    {
        [Newtonsoft.Json.JsonProperty("pf")]
        [MessagePack.Key("pf")]
        public byte[]? PF { get; set; }
    }
}
