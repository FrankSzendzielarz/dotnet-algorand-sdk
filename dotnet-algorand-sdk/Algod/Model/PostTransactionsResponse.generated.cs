
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
    public partial class PostTransactionsResponse
    {

        [Newtonsoft.Json.JsonProperty("txId", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txId")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"encoding of the transaction hash.")]
    [field:InspectorName(@"Txid")]
    public string Txid {get;set;}
#else
        public string Txid { get; set; }
#endif



    }


}
