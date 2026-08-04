
namespace Algorand.Algod.Model
{
    using Algorand.Algod.Model.Transactions;

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class EvalDelta
    {

        [Newtonsoft.Json.JsonProperty("action", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("action")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[at\] delta action.")]
    [field:InspectorName(@"Action")]
    public ulong Action {get;set;}
#else
        public ulong Action { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("bytes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("bytes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[bs\] bytes value.")]
    [field:InspectorName(@"Bytes")]
    public string Bytes {get;set;}
#else
        public string Bytes { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("uint", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("uint")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[ui\] uint value.")]
    [field:InspectorName(@"Uint")]
    public ulong Uint {get;set;}
#else
        public ulong? Uint { get; set; }
#endif



    }


}
