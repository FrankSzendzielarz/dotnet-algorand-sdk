
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
    public partial class ModifiedApp
    {

        [Newtonsoft.Json.JsonProperty("created", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("created")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Created if true, deleted if false")]
    [field:InspectorName(@"Created")]
    public bool Created {get;set;}
#else
        public bool Created { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("creator", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("creator")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of the creator.")]
    [field:InspectorName(@"Creator")]
    public string Creator {get;set;}
#else
        public string Creator { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"App Id")]
    [field:InspectorName(@"Id")]
    public ulong Id {get;set;}
#else
        public ulong Id { get; set; }
#endif



    }


}
