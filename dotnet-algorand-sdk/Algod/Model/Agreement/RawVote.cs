namespace Algorand.Algod.Model.Agreement
{
    /// <summary>
    /// rawVote is the inner struct which is authenticated with keys
    /// </summary>
    [MessagePack.MessagePackObject]
    public class RawVote
    {
        /// <summary>
        /// Sender
        /// 
        /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/vote.go#L45
        /// </summary>
        [Newtonsoft.Json.JsonProperty("snd", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("snd")]
        public Address Sender { get; set; }

        /// <summary>
        /// Round
        /// 
        /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/vote.go#L45
        /// </summary>
        [Newtonsoft.Json.JsonProperty("rnd", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("rnd")]
        public ulong Round { get; set; }


        /// <summary>
        /// Period
        /// 
        /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/vote.go#L45
        /// </summary>
        [Newtonsoft.Json.JsonProperty("per", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("per")]
        public ulong Period { get; set; }


        /// <summary>
        /// Step
        /// 
        /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/vote.go#L45
        /// </summary>
        [Newtonsoft.Json.JsonProperty("step", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("step")]
        public ulong Step { get; set; }


        /// <summary>
        /// Proposal
        /// 
        /// https://github.com/algorand/go-algorand/blob/515915252eebf336cb1f09ea7533d5b48c5b01ac/agreement/vote.go#L45
        /// </summary>
        [Newtonsoft.Json.JsonProperty("prop", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("prop")]
        public ProposalValue Proposal { get; set; }


    }
}
