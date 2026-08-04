
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
    public partial class SimulateTransactionGroupResult
    {

        [Newtonsoft.Json.JsonProperty("app-budget-added", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-budget-added")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Total budget added during execution of app calls in the transaction group.")]
    [field:InspectorName(@"AppBudgetAdded")]
    public ulong AppBudgetAdded {get;set;}
#else
        public ulong? AppBudgetAdded { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-budget-consumed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-budget-consumed")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Total budget consumed during execution of app calls in the transaction group.")]
    [field:InspectorName(@"AppBudgetConsumed")]
    public ulong AppBudgetConsumed {get;set;}
#else
        public ulong? AppBudgetConsumed { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("failed-at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("failed-at")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If present, indicates which transaction in this group caused the failure. This array represents the path to the failing transaction. Indexes are zero based, the first element indicates the top-level transaction, and successive elements indicate deeper inner transactions.")]
    [field:InspectorName(@"FailedAt")]
    public System.Collections.Generic.List<ulong> FailedAt {get;set;} = new System.Collections.Generic.List<ulong>();
#else
        public System.Collections.Generic.ICollection<ulong> FailedAt { get; set; } = new System.Collections.ObjectModel.Collection<ulong>();
#endif

        [Newtonsoft.Json.JsonProperty("failure-message", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("failure-message")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If present, indicates that the transaction group failed and specifies why that happened")]
    [field:InspectorName(@"FailureMessage")]
    public string FailureMessage {get;set;}
#else
        public string FailureMessage { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("txn-results", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txn-results")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Simulation result for individual transactions")]
    [field:InspectorName(@"TxnResults")]
    public System.Collections.Generic.List<SimulateTransactionResult> TxnResults {get;set;} = new System.Collections.Generic.List<SimulateTransactionResult>();
#else
        public System.Collections.Generic.ICollection<SimulateTransactionResult> TxnResults { get; set; } = new System.Collections.ObjectModel.Collection<SimulateTransactionResult>();
#endif

        [Newtonsoft.Json.JsonProperty("unnamed-resources-accessed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("unnamed-resources-accessed")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"These are resources that were accessed by this group that would normally have caused failure, but were allowed in simulation. Depending on where this object is in the response, the unnamed resources it contains may or may not qualify for group resource sharing. If this is a field in SimulateTransactionGroupResult, the resources do qualify, but if this is a field in SimulateTransactionResult, they do not qualify. In order to make this group valid for actual submission, resources that qualify for group sharing can be made available by any transaction of the group; otherwise, resources must be placed in the same transaction which accessed them.")]
    [field:InspectorName(@"UnnamedResourcesAccessed")]
    public SimulateUnnamedResourcesAccessed UnnamedResourcesAccessed {get;set;}
#else
        public SimulateUnnamedResourcesAccessed UnnamedResourcesAccessed { get; set; }
#endif



    }


}
