namespace Algorand.Algod.Model.Agreement
{

    /// <summary>
    /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/fuzzer/messageDecoderFilter_test.go#L73
    /// </summary>
    [MessagePack.MessagePackObject]

    public class ProposalValue
    {
        [Newtonsoft.Json.JsonProperty("oper", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("oper")]
        public ulong OriginalPeriod { get; set; }

        [Newtonsoft.Json.JsonProperty("oprop", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("oprop")]
        public Address OriginalProposer { get; set; }

        [Newtonsoft.Json.JsonProperty("dig", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("dig")]
        public Digest EntryDigest { get; set; }

        [Newtonsoft.Json.JsonProperty("encdig", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("encdig")]
        public Digest EncodingDigest { get; set; }

    }
}
