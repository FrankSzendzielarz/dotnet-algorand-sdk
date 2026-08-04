
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
    public partial class AppCallLogs
    {

        [Newtonsoft.Json.JsonProperty("application-index", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("application-index")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The application from which the logs were generated")]
    [field:InspectorName(@"ApplicationIndex")]
    public ulong ApplicationIndex {get;set;}
#else
        public ulong ApplicationIndex { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("logs", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("logs")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An array of logs")]
    [field:InspectorName(@"Logs")]
    public System.Collections.Generic.List<byte[]> Logs {get;set;} = new System.Collections.Generic.List<byte[]>();
#else
        public System.Collections.Generic.ICollection<byte[]> Logs { get; set; } = new System.Collections.ObjectModel.Collection<byte[]>();
#endif

        [Newtonsoft.Json.JsonProperty("txId", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txId")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The transaction ID of the outer app call that lead to these logs")]
    [field:InspectorName(@"Txid")]
    public string Txid {get;set;}
#else
        public string Txid { get; set; }
#endif



    }


}
