
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
    public partial class AppResourceRecord
    {

        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("address")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"App account address")]
    [field:InspectorName(@"Address")]
    public string Address {get;set;}
#else
        public string Address { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-deleted", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("app-deleted")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Whether the app was deleted")]
    [field:InspectorName(@"AppDeleted")]
    public bool AppDeleted {get;set;}
#else
        public bool AppDeleted { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-index", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("app-index")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"App index")]
    [field:InspectorName(@"AppIndex")]
    public ulong AppIndex {get;set;}
#else
        public ulong AppIndex { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-local-state", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-local-state")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"App local state")]
    [field:InspectorName(@"AppLocalState")]
    public ApplicationLocalState AppLocalState {get;set;}
#else
        public ApplicationLocalState AppLocalState { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-local-state-deleted", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("app-local-state-deleted")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Whether the app local state was deleted")]
    [field:InspectorName(@"AppLocalStateDeleted")]
    public bool AppLocalStateDeleted {get;set;}
#else
        public bool AppLocalStateDeleted { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-params", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-params")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"App params")]
    [field:InspectorName(@"AppParams")]
    public ApplicationParams AppParams {get;set;}
#else
        public ApplicationParams AppParams { get; set; }
#endif



    }


}
