
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
    public partial class GenesisAllocation
    {

        [Newtonsoft.Json.JsonProperty("addr", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("addr")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Addr")]
    public string Addr {get;set;}
#else
        public string Addr { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("comment", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("comment")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Comment")]
    public string Comment {get;set;}
#else
        public string Comment { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("state", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("state")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"State")]
    public byte[] State {get;set;}
#else
        public byte[] State { get; set; }
#endif



    }


}
