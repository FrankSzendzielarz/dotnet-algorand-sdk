namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]
    public class BlockCertVote
    {

        [Newtonsoft.Json.JsonProperty("cred")]
        [MessagePack.Key("cred")]
        public BlockCertVoteCred? Cred { get; set; }
        [Newtonsoft.Json.JsonProperty("sig")]
        [MessagePack.Key("sig")]
        public BlockCertVoteSig? Sig { get; set; }
        [Newtonsoft.Json.JsonProperty("snd")]
        [MessagePack.Key("snd")]
        public Address? Sender { get; set; }
    }
}
