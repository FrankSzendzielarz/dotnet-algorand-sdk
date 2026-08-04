
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
    public partial class DryrunState
    {

        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("error")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Evaluation error if any")]
    [field:InspectorName(@"Error")]
    public string Error {get;set;}
#else
        public string Error { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("line", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("line")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Line number")]
    [field:InspectorName(@"Line")]
    public ulong Line {get;set;}
#else
        public ulong Line { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("pc", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("pc")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Program counter")]
    [field:InspectorName(@"Pc")]
    public ulong Pc {get;set;}
#else
        public ulong Pc { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("scratch", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("scratch")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Scratch")]
    public System.Collections.Generic.List<TealValue> Scratch {get;set;} = new System.Collections.Generic.List<TealValue>();
#else
        public System.Collections.Generic.ICollection<TealValue> Scratch { get; set; } = new System.Collections.ObjectModel.Collection<TealValue>();
#endif

        [Newtonsoft.Json.JsonProperty("stack", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("stack")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Stack")]
    public System.Collections.Generic.List<TealValue> Stack {get;set;} = new System.Collections.Generic.List<TealValue>();
#else
        public System.Collections.Generic.ICollection<TealValue> Stack { get; set; } = new System.Collections.ObjectModel.Collection<TealValue>();
#endif

    }


}
