
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
    public partial class SimulateTraceConfig
    {

        [Newtonsoft.Json.JsonProperty("enable", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("enable")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"A boolean option for opting in execution trace features simulation endpoint.")]
    [field:InspectorName(@"Enable")]
    public bool Enable {get;set;}
#else
        public bool? Enable { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("scratch-change", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("scratch-change")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"A boolean option enabling returning scratch slot changes together with execution trace during simulation.")]
    [field:InspectorName(@"ScratchChange")]
    public bool ScratchChange {get;set;}
#else
        public bool? ScratchChange { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("stack-change", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("stack-change")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"A boolean option enabling returning stack changes together with execution trace during simulation.")]
    [field:InspectorName(@"StackChange")]
    public bool StackChange {get;set;}
#else
        public bool? StackChange { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("state-change", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("state-change")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"A boolean option enabling returning application state changes (global, local, and box changes) with the execution trace during simulation.")]
    [field:InspectorName(@"StateChange")]
    public bool StateChange {get;set;}
#else
        public bool? StateChange { get; set; }
#endif



    }


}
