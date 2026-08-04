namespace Algorand.Algod.Model
{
    using Newtonsoft.Json.Linq;
    using System = global::System;
#if UNITY
using UnityEngine;
#endif


    /// <summary>An error response with optional data field.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.5.2.0 (Newtonsoft.Json v12.0.0.0)")]
    [MessagePack.MessagePackObject]
    public partial class ErrorResponse
    {
        [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("data")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Data")]
    public JObject Data {get;set;}
#else
        public JObject Data { get; set; }
#endif

    }
}
