
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
    [MessagePack.MessagePackObject]
    public partial class AvmKeyValue
    {

        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("key")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Key")]
    public byte[] Key {get;set;}
#else
        public byte[] Key { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("value")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Represents an AVM value.")]
    [field:InspectorName(@"Value")]
    public AvmValue Value {get;set;}
#else
        public AvmValue Value { get; set; }
#endif

    }
}