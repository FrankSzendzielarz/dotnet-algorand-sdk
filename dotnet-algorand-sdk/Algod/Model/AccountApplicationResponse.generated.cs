
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
    public partial class AccountApplicationResponse
    {

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



        [Newtonsoft.Json.JsonProperty("created-app", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("created-app")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appp\] parameters of the application created by this account including app global data.

The raw account uses `AppParams` for this type.")]
    [field:InspectorName(@"CreatedApp")]
    public ApplicationParams CreatedApp {get;set;}
#else
        public ApplicationParams CreatedApp { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round for which this information is relevant.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



    }


}
