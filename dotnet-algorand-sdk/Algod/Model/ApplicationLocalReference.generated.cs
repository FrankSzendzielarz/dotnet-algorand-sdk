
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
    public partial class ApplicationLocalReference
    {

        [Newtonsoft.Json.JsonProperty("account", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("account")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of the account with the local state.")]
    [field:InspectorName(@"Account")]
    public Address Account {get;set;}
#else
        public Address Account { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("app")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Application ID of the local state application.")]
    [field:InspectorName(@"App")]
    public ulong App {get;set;}
#else
        public ulong App { get; set; }
#endif



    }


}
