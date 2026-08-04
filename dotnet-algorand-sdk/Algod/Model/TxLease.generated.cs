
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
    public partial class TxLease
    {

        [Newtonsoft.Json.JsonProperty("expiration", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("expiration")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Round that the lease expires")]
    [field:InspectorName(@"Expiration")]
    public ulong Expiration {get;set;}
#else
        public ulong Expiration { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("lease", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("lease")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Lease data")]
    [field:InspectorName(@"Lease")]
    public byte[] Lease {get;set;}
#else
        public byte[] Lease { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("sender", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("sender")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of the lease sender")]
    [field:InspectorName(@"Sender")]
    public string Sender {get;set;}
#else
        public string Sender { get; set; }
#endif



    }


}
