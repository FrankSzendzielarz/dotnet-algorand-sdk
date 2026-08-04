
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
    public partial class LedgerStateDeltaForTransactionGroup
    {

        [Newtonsoft.Json.JsonProperty("Delta", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("delta")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Ledger StateDelta object")]
    [field:InspectorName(@"Delta")]
    public byte[] Delta {get;set;}
#else
        public byte[] Delta { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("Ids", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("Ids")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Ids")]
    public System.Collections.Generic.List<string> Ids {get;set;} = new System.Collections.Generic.List<string>();
#else
        public System.Collections.Generic.ICollection<string> Ids { get; set; } = new System.Collections.ObjectModel.Collection<string>();
#endif

    }


}
