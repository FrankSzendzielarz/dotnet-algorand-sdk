
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
    public partial class ApplicationInitialStates
    {

        [Newtonsoft.Json.JsonProperty("app-boxes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-boxes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An application's global/local/box state.")]
    [field:InspectorName(@"AppBoxes")]
    public ApplicationKVStorage AppBoxes {get;set;}
#else
        public ApplicationKVStorage AppBoxes { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-globals", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-globals")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An application's global/local/box state.")]
    [field:InspectorName(@"AppGlobals")]
    public ApplicationKVStorage AppGlobals {get;set;}
#else
        public ApplicationKVStorage AppGlobals { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-locals", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-locals")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An application's initial local states tied to different accounts.")]
    [field:InspectorName(@"AppLocals")]
    public System.Collections.Generic.List<ApplicationKVStorage> AppLocals {get;set;} = new System.Collections.Generic.List<ApplicationKVStorage>();
#else
        public System.Collections.Generic.ICollection<ApplicationKVStorage> AppLocals { get; set; } = new System.Collections.ObjectModel.Collection<ApplicationKVStorage>();
#endif

        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Application index.")]
    [field:InspectorName(@"Id")]
    public ulong Id {get;set;}
#else
        public ulong Id { get; set; }
#endif



    }


}
