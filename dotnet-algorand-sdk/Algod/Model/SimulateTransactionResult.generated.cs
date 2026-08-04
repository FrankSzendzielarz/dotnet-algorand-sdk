
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
    public partial class SimulateTransactionResult
    {

        [Newtonsoft.Json.JsonProperty("app-budget-consumed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-budget-consumed")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Budget used during execution of an app call transaction. This value includes budged used by inner app calls spawned by this transaction.")]
    [field:InspectorName(@"AppBudgetConsumed")]
    public ulong AppBudgetConsumed {get;set;}
#else
        public ulong? AppBudgetConsumed { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("exec-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("exec-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The execution trace of calling an app or a logic sig, containing the inner app call trace in a recursive way.")]
    [field:InspectorName(@"ExecTrace")]
    public SimulationTransactionExecTrace ExecTrace {get;set;}
#else
        public SimulationTransactionExecTrace ExecTrace { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("fixed-signer", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("fixed-signer")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The account that needed to sign this transaction when no signature was provided and the provided signer was incorrect.")]
    [field:InspectorName(@"FixedSigner")]
    public Address FixedSigner {get;set;}
#else
        public Address FixedSigner { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("logic-sig-budget-consumed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logic-sig-budget-consumed")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Budget used during execution of a logic sig transaction.")]
    [field:InspectorName(@"LogicSigBudgetConsumed")]
    public ulong LogicSigBudgetConsumed {get;set;}
#else
        public ulong? LogicSigBudgetConsumed { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("txn-result", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txn-result")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeReference]
    [Tooltip(@"Details about a pending transaction. If the transaction was recently confirmed, includes confirmation details like the round and reward details.")]
    [field:InspectorName(@"TxnResult")]
    public IReturnableTransaction TxnResult {get;set;}
#else
        public IReturnableTransaction TxnResult { get; set; }
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
