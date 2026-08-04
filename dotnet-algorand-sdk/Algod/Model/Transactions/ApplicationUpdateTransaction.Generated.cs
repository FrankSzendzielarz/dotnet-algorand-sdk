
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class ApplicationUpdateTransaction : ApplicationCallTransaction
    {

        [Newtonsoft.Json.JsonProperty("apap", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apap")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ApprovalProgram")]
    public Algorand.TEALProgram ApprovalProgram {get;set;}
#else
        public Algorand.TEALProgram ApprovalProgram { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apep", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apep")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ExtraProgramPages")]
    public ulong ExtraProgramPages {get;set;}
#else
        public ulong? ExtraProgramPages { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apgs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apgs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"GlobalStateSchema")]
    public StateSchema GlobalStateSchema {get;set;}
#else
        public StateSchema GlobalStateSchema { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apid")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ApplicationId")]
    public ulong ApplicationId {get;set;}
#else
        public ulong? ApplicationId { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apls", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apls")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"LocalStateSchema")]
    public StateSchema LocalStateSchema {get;set;}
#else
        public StateSchema LocalStateSchema { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apsu", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apsu")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ClearStateProgram")]
    public Algorand.TEALProgram ClearStateProgram {get;set;}
#else
        public Algorand.TEALProgram ClearStateProgram { get; set; }
#endif



    }


}
