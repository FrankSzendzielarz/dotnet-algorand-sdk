
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
    public partial class SimulationEvalOverrides
    {

        [Newtonsoft.Json.JsonProperty("allow-empty-signatures", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("allow-empty-signatures")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If true, transactions without signatures are allowed and simulated as if they were properly signed.")]
    [field:InspectorName(@"AllowEmptySignatures")]
    public bool AllowEmptySignatures {get;set;}
#else
        public bool? AllowEmptySignatures { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("allow-unnamed-resources", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("allow-unnamed-resources")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If true, allows access to unnamed resources during simulation.")]
    [field:InspectorName(@"AllowUnnamedResources")]
    public bool AllowUnnamedResources {get;set;}
#else
        public bool? AllowUnnamedResources { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("extra-opcode-budget", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("extra-opcode-budget")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The extra opcode budget added to each transaction group during simulation")]
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



        [Newtonsoft.Json.JsonProperty("max-log-calls", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("max-log-calls")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The maximum log calls one can make during simulation")]
    [field:InspectorName(@"MaxLogCalls")]
    public ulong MaxLogCalls {get;set;}
#else
        public ulong? MaxLogCalls { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("max-log-size", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("max-log-size")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The maximum byte number to log during simulation")]
    [field:InspectorName(@"MaxLogSize")]
    public ulong MaxLogSize {get;set;}
#else
        public ulong? MaxLogSize { get; set; }
#endif



    }


}
