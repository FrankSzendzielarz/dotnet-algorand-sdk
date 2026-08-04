
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
    public partial class ApplicationParams
    {

        [Newtonsoft.Json.JsonProperty("approval-program", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("approval-program")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[approv\] approval program.")]
    [field:InspectorName(@"ApprovalProgram")]
    public TEALProgram ApprovalProgram {get;set;}
#else
        public TEALProgram ApprovalProgram { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("clear-state-program", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("clear-state-program")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[clearp\] approval program.")]
    [field:InspectorName(@"ClearStateProgram")]
    public TEALProgram ClearStateProgram {get;set;}
#else
        public TEALProgram ClearStateProgram { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("creator", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("creator")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The address that created this application. This is the address where the parameters and global state for this application can be found.")]
    [field:InspectorName(@"Creator")]
    public Address Creator {get;set;}
#else
        public Address Creator { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("extra-program-pages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("extra-program-pages")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[epp\] the amount of extra program pages available to this app.")]
    [field:InspectorName(@"ExtraProgramPages")]
    public ulong ExtraProgramPages {get;set;}
#else
        public ulong? ExtraProgramPages { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("global-state", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("global-state")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[gs\] global state")]
    [field:InspectorName(@"GlobalState")]
    public System.Collections.Generic.List<TealKeyValue> GlobalState {get;set;} = new System.Collections.Generic.List<TealKeyValue>();
#else
        public System.Collections.Generic.ICollection<TealKeyValue> GlobalState { get; set; } = new System.Collections.ObjectModel.Collection<TealKeyValue>();
#endif

        [Newtonsoft.Json.JsonProperty("global-state-schema", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("global-state-schema")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[gsch\] global schema")]
    [field:InspectorName(@"GlobalStateSchema")]
    public ApplicationStateSchema GlobalStateSchema {get;set;}
#else
        public ApplicationStateSchema GlobalStateSchema { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("local-state-schema", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("local-state-schema")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[lsch\] local schema")]
    [field:InspectorName(@"LocalStateSchema")]
    public ApplicationStateSchema LocalStateSchema {get;set;}
#else
        public ApplicationStateSchema LocalStateSchema { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("version", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("version")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[v\] the number of updates to the application programs")]
    [field:InspectorName(@"Version")]
    public ulong Version {get;set;}
#else
        public ulong? Version { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("size-sponsor", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("size-sponsor")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[ss\] the account responsible for extra pages and global state MBR")]
    [field:InspectorName(@"SizeSponsor")]
    public Address SizeSponsor {get;set;}
#else
        public Address SizeSponsor { get; set; }
#endif



    }


}
