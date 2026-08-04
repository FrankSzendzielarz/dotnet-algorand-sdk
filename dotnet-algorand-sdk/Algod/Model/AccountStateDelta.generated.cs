
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
    public partial class AccountStateDelta
    {

        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("address")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Address")]
    public string Address {get;set;}
#else
        public string Address { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("delta", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("delta")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Application state delta.")]
    [field:InspectorName(@"Delta")]
    public System.Collections.Generic.List<EvalDeltaKeyValue> Delta {get;set;} = new System.Collections.Generic.List<EvalDeltaKeyValue>();
#else
        public System.Collections.Generic.ICollection<EvalDeltaKeyValue> Delta { get; set; } = new System.Collections.ObjectModel.Collection<EvalDeltaKeyValue>();
#endif

    }


}
