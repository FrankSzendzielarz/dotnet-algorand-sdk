
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
    public partial class Box
    {

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("name")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[name\] box name, base64 encoded")]
    [field:InspectorName(@"Name")]
    public byte[] Name {get;set;}
#else
        public byte[] Name { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("round")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round for which this information is relevant")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("value")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[value\] box value, base64 encoded.")]
    [field:InspectorName(@"Value")]
    public byte[] Value {get;set;}
#else
        public byte[] Value { get; set; }
#endif



    }


}
