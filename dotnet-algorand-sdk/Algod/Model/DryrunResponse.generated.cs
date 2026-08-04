
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
    public partial class DryrunResponse
    {

        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("error")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Error")]
    public string Error {get;set;}
#else
        public string Error { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("protocol-version", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("protocol-version")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Protocol version is the protocol version Dryrun was operated under.")]
    [field:InspectorName(@"ProtocolVersion")]
    public string ProtocolVersion {get;set;}
#else
        public string ProtocolVersion { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("txns", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txns")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Txns")]
    public System.Collections.Generic.List<DryrunTxnResult> Txns {get;set;} = new System.Collections.Generic.List<DryrunTxnResult>();
#else
        public System.Collections.Generic.ICollection<DryrunTxnResult> Txns { get; set; } = new System.Collections.ObjectModel.Collection<DryrunTxnResult>();
#endif

    }


}
