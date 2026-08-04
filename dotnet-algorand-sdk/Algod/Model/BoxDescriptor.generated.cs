
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
    public partial class BoxDescriptor
    {

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("name")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Base64 encoded box name")]
    [field:InspectorName(@"Name")]
    public byte[] Name {get;set;}
#else
        public byte[] Name { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("value")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Base64 encoded box value. Present only when the `values` query parameter is set to true.")]
    [field:InspectorName(@"Value")]
    public byte[] Value {get;set;}
#else
        public byte[] Value { get; set; }
#endif



    }


}
