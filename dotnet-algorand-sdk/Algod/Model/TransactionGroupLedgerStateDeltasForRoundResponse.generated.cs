
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
    public partial class TransactionGroupLedgerStateDeltasForRoundResponse
    {

        [Newtonsoft.Json.JsonProperty("Deltas", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("Deltas")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Deltas")]
    public System.Collections.Generic.List<LedgerStateDeltaForTransactionGroup> Deltas {get;set;} = new System.Collections.Generic.List<LedgerStateDeltaForTransactionGroup>();
#else
        public System.Collections.Generic.ICollection<LedgerStateDeltaForTransactionGroup> Deltas { get; set; } = new System.Collections.ObjectModel.Collection<LedgerStateDeltaForTransactionGroup>();
#endif

    }


}
