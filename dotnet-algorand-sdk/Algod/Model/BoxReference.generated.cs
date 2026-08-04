
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
    public partial class BoxReference
    {

        [Newtonsoft.Json.JsonProperty("app", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("app")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Application ID which this box belongs to")]
    [field:InspectorName(@"App")]
    public ulong App {get;set;}
#else
        public ulong App { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("name")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Base64 encoded box name")]
    [field:InspectorName(@"Name")]
    public byte[] Name {get;set;}
#else
        public byte[] Name { get; set; }
#endif



    }


}
