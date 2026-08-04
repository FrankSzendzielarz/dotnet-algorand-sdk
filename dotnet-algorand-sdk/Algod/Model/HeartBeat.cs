using MessagePack;

namespace Algorand.Algod.Model
{
    [MessagePackObject]
    public class HeartBeat
    {

        [Newtonsoft.Json.JsonProperty("a", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("a")]
        public Address? Account { get; set; }

        [Newtonsoft.Json.JsonProperty("kd", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("kd")]
        public ulong? KD { get; set; }

        [Newtonsoft.Json.JsonProperty("prf")]
        [MessagePack.Key("prf")]
        public BlockCertVoteSig? Proof { get; set; }
        [Newtonsoft.Json.JsonProperty("sd", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("sd")]
        public byte[]? SD { get; set; }
        [Newtonsoft.Json.JsonProperty("vid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("vid")]
        public byte[]? Vid { get; set; }
    }
}
