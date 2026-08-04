
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
    public partial class PendingTransactions
    {

        [Newtonsoft.Json.JsonProperty("top-transactions", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("top-transactions")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An array of signed transaction objects.")]
    [field:InspectorName(@"TopTransactions")]
    public System.Collections.Generic.List<SignedTransaction> TopTransactions {get;set;} = new System.Collections.Generic.List<SignedTransaction>();
#else
        public System.Collections.Generic.ICollection<SignedTransaction> TopTransactions { get; set; } = new System.Collections.ObjectModel.Collection<SignedTransaction>();
#endif

        [Newtonsoft.Json.JsonProperty("total-transactions", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("total-transactions")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Total number of transactions in the pool.")]
    [field:InspectorName(@"TotalTransactions")]
    public ulong TotalTransactions {get;set;}
#else
        public ulong TotalTransactions { get; set; }
#endif



    }


}
