
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
public partial class BlockHashResponse{

    [Newtonsoft.Json.JsonProperty("blockHash", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Block header hash.")]
    [field:InspectorName(@"Blockhash")]
    public string Blockhash {get;set;}
#else
    public string Blockhash {get;set;}
#endif


    
}


}
