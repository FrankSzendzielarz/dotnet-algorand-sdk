
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
    public partial class TealKeyValue
    {

        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("key")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Key")]
    public string Key {get;set;}
#else
        public string Key { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("value")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Represents a TEAL value.")]
    [field:InspectorName(@"Value")]
    public TealValue Value {get;set;}
#else
        public TealValue Value { get; set; }
#endif



    }


}
