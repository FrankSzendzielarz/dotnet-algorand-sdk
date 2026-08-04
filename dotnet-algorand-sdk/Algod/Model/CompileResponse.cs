
namespace Algorand.Algod.Model
{
    using Newtonsoft.Json.Linq;
#if UNITY
    using UnityEngine;
#endif
    [MessagePack.MessagePackObject]
    public partial class CompileResponse
    {

        [Newtonsoft.Json.JsonProperty("sourcemap", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("sourcemap")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"JSON of the source map")]
    [field:InspectorName(@"Sourcemap")]
    public byte[] Sourcemap {get;set;}
#else
        public JObject Sourcemap { get; set; }
#endif



    }


}
