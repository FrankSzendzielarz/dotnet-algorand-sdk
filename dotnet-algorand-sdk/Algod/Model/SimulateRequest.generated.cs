
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
    public partial class SimulateRequest
    {

        [Newtonsoft.Json.JsonProperty("allow-empty-signatures", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("allow-empty-signatures")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Allows transactions without signatures to be simulated as if they had correct signatures.")]
    [field:InspectorName(@"AllowEmptySignatures")]
    public bool AllowEmptySignatures {get;set;}
#else
        public bool? AllowEmptySignatures { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("allow-more-logging", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("allow-more-logging")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Lifts limits on log opcode usage during simulation.")]
    [field:InspectorName(@"AllowMoreLogging")]
    public bool AllowMoreLogging {get;set;}
#else
        public bool? AllowMoreLogging { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("allow-unnamed-resources", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("allow-unnamed-resources")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Allows access to unnamed resources during simulation.")]
    [field:InspectorName(@"AllowUnnamedResources")]
    public bool AllowUnnamedResources {get;set;}
#else
        public bool? AllowUnnamedResources { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("exec-trace-config", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("exec-trace-config")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An object that configures simulation execution trace.")]
    [field:InspectorName(@"ExecTraceConfig")]
    public SimulateTraceConfig ExecTraceConfig {get;set;}
#else
        public SimulateTraceConfig ExecTraceConfig { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("extra-opcode-budget", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("extra-opcode-budget")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Applies extra opcode budget during simulation for each transaction group.")]
    [field:InspectorName(@"ExtraOpcodeBudget")]
    public ulong ExtraOpcodeBudget {get;set;}
#else
        public ulong? ExtraOpcodeBudget { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("fix-signers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("fix-signers")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If true, signers for transactions that are missing signatures will be fixed during evaluation.")]
    [field:InspectorName(@"FixSigners")]
    public bool FixSigners {get;set;}
#else
        public bool? FixSigners { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("round")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If provided, specifies the round preceding the simulation. State changes through this round will be used to run this simulation. Usually only the 4 most recent rounds will be available (controlled by the node config value MaxAcctLookback). If not specified, defaults to the latest available round.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong? Round { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("txn-groups", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txn-groups")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The transaction groups to simulate.")]
    [field:InspectorName(@"TxnGroups")]
    public System.Collections.Generic.List<SimulateRequestTransactionGroup> TxnGroups {get;set;} = new System.Collections.Generic.List<SimulateRequestTransactionGroup>();
#else
        public System.Collections.Generic.ICollection<SimulateRequestTransactionGroup> TxnGroups { get; set; } = new System.Collections.ObjectModel.Collection<SimulateRequestTransactionGroup>();
#endif

    }


}
