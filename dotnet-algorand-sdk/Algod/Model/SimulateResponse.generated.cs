
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
    public partial class SimulateResponse
    {

        [Newtonsoft.Json.JsonProperty("eval-overrides", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("eval-overrides")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The set of parameters and limits override during simulation. If this set of parameters is present, then evaluation parameters may differ from standard evaluation in certain ways.")]
    [field:InspectorName(@"EvalOverrides")]
    public SimulationEvalOverrides EvalOverrides {get;set;}
#else
        public SimulationEvalOverrides EvalOverrides { get; set; }
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



        [Newtonsoft.Json.JsonProperty("initial-states", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("initial-states")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Initial states of resources that were accessed during simulation.")]
    [field:InspectorName(@"InitialStates")]
    public SimulateInitialStates InitialStates {get;set;}
#else
        public SimulateInitialStates InitialStates { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("last-round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("last-round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round immediately preceding this simulation. State changes through this round were used to run this simulation.")]
    [field:InspectorName(@"LastRound")]
    public ulong LastRound {get;set;}
#else
        public ulong LastRound { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("txn-groups", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txn-groups")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"A result object for each transaction group that was simulated.")]
    [field:InspectorName(@"TxnGroups")]
    public System.Collections.Generic.List<SimulateTransactionGroupResult> TxnGroups {get;set;} = new System.Collections.Generic.List<SimulateTransactionGroupResult>();
#else
        public System.Collections.Generic.ICollection<SimulateTransactionGroupResult> TxnGroups { get; set; } = new System.Collections.ObjectModel.Collection<SimulateTransactionGroupResult>();
#endif

        [Newtonsoft.Json.JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("versions")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The version of this response object.")]
    [field:InspectorName(@"Version")]
    public ulong Version {get;set;}
#else
        public ulong Version { get; set; }
#endif



    }


}
