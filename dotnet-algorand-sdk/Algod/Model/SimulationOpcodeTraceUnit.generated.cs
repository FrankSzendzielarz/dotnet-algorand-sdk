
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
    public partial class SimulationOpcodeTraceUnit
    {

        [Newtonsoft.Json.JsonProperty("pc", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("pc")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The program counter of the current opcode being evaluated.")]
    [field:InspectorName(@"Pc")]
    public ulong Pc {get;set;}
#else
        public ulong Pc { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("scratch-changes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("scratch-changes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The writes into scratch slots.")]
    [field:InspectorName(@"ScratchChanges")]
    public System.Collections.Generic.List<ScratchChange> ScratchChanges {get;set;} = new System.Collections.Generic.List<ScratchChange>();
#else
        public System.Collections.Generic.ICollection<ScratchChange> ScratchChanges { get; set; } = new System.Collections.ObjectModel.Collection<ScratchChange>();
#endif

        [Newtonsoft.Json.JsonProperty("spawned-inners", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("spawned-inners")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The indexes of the traces for inner transactions spawned by this opcode, if any.")]
    [field:InspectorName(@"SpawnedInners")]
    public System.Collections.Generic.List<ulong> SpawnedInners {get;set;} = new System.Collections.Generic.List<ulong>();
#else
        public System.Collections.Generic.ICollection<ulong> SpawnedInners { get; set; } = new System.Collections.ObjectModel.Collection<ulong>();
#endif

        [Newtonsoft.Json.JsonProperty("stack-additions", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("stack-additions")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The values added by this opcode to the stack.")]
    [field:InspectorName(@"StackAdditions")]
    public System.Collections.Generic.List<AvmValue> StackAdditions {get;set;} = new System.Collections.Generic.List<AvmValue>();
#else
        public System.Collections.Generic.ICollection<AvmValue> StackAdditions { get; set; } = new System.Collections.ObjectModel.Collection<AvmValue>();
#endif

        [Newtonsoft.Json.JsonProperty("stack-pop-count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("stack-pop-count")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of deleted stack values by this opcode.")]
    [field:InspectorName(@"StackPopCount")]
    public ulong StackPopCount {get;set;}
#else
        public ulong? StackPopCount { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("state-changes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("state-changes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The operations against the current application's states.")]
    [field:InspectorName(@"StateChanges")]
    public System.Collections.Generic.List<ApplicationStateOperation> StateChanges {get;set;} = new System.Collections.Generic.List<ApplicationStateOperation>();
#else
        public System.Collections.Generic.ICollection<ApplicationStateOperation> StateChanges { get; set; } = new System.Collections.ObjectModel.Collection<ApplicationStateOperation>();
#endif

    }


}
