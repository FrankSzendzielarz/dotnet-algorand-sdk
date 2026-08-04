
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
    public partial class Application
    {

        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appidx\] application index.")]
    [field:InspectorName(@"Id")]
    public ulong Id {get;set;}
#else
        public ulong Id { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("params", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("params")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appparams\] application parameters.")]
    [field:InspectorName(@"Params")]
    public ApplicationParams Params {get;set;}
#else
        public ApplicationParams Params { get; set; }
#endif



    }


}
