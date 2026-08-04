
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
    public partial class GetSyncRoundResponse
    {

        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The minimum sync round for the ledger.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



    }


}
