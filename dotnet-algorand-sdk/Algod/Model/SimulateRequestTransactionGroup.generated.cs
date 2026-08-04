
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
    public partial class SimulateRequestTransactionGroup
    {

        [Newtonsoft.Json.JsonProperty("txns", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txns")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An atomic transaction group.")]
    [field:InspectorName(@"Txns")]
    public System.Collections.Generic.List<SignedTransaction> Txns {get;set;} = new System.Collections.Generic.List<SignedTransaction>();
#else
        public System.Collections.Generic.ICollection<SignedTransaction> Txns { get; set; } = new System.Collections.ObjectModel.Collection<SignedTransaction>();
#endif

    }


}
