
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
    public partial class AccountApplicationResource
    {

        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The application ID.")]
    [field:InspectorName(@"Id")]
    public ulong Id {get;set;}
#else
        public ulong Id { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("created-at-round", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("created-at-round")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Round when the account opted into or created the application.")]
    [field:InspectorName(@"CreatedAtRound")]
    public ulong CreatedAtRound {get;set;}
#else
        public ulong? CreatedAtRound { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("deleted", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("deleted")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Whether the application has been deleted.")]
    [field:InspectorName(@"Deleted")]
    public bool Deleted {get;set;}
#else
        public bool? Deleted { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-local-state", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-local-state")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appl\] the application local data stored in this account.

The raw account uses `AppLocalState` for this type.")]
    [field:InspectorName(@"AppLocalState")]
    public ApplicationLocalState AppLocalState {get;set;}
#else
        public ApplicationLocalState AppLocalState { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("params", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("params")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appp\] parameters of the application created by this account including app global data.

The raw account uses `AppParams` for this type.
Only present if the account is the creator and `include=params` is specified.")]
    [field:InspectorName(@"Params")]
    public ApplicationParams Params {get;set;}
#else
        public ApplicationParams Params { get; set; }
#endif



    }


}
