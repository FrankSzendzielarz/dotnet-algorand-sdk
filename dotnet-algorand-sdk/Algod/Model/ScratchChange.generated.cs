
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
    public partial class ScratchChange
    {

        [Newtonsoft.Json.JsonProperty("new-value", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("new-value")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Represents an AVM value.")]
    [field:InspectorName(@"NewValue")]
    public AvmValue NewValue {get;set;}
#else
        public AvmValue NewValue { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("slot", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("slot")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The scratch slot written.")]
    [field:InspectorName(@"Slot")]
    public ulong Slot {get;set;}
#else
        public ulong Slot { get; set; }
#endif



    }


}
