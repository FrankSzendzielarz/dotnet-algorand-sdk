namespace Algorand.Algod.Model.Agreement
{
    /// <summary>
    /// A vote is an endorsement of a particular proposal in Algorand
    /// 
    /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/fuzzer/messageDecoderFilter_test.go#L127
    /// </summary>
    [MessagePack.MessagePackObject]
    public class Vote
    {
        /// <summary>
        /// RawVote
        /// </summary>
        [Newtonsoft.Json.JsonProperty("r", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("r")]
        public RawVote R { get; set; }
        /// <summary>
        /// committee.Credential 
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cred")]
        [MessagePack.Key("cred")]
        public BlockCertVoteCred? Cred { get; set; }
        /// <summary>
        /// crypto.OneTimeSignature
        /// </summary>
        [Newtonsoft.Json.JsonProperty("sig")]
        [MessagePack.Key("sig")]
        public BlockCertVoteSig? Sig { get; set; }
    }
}
