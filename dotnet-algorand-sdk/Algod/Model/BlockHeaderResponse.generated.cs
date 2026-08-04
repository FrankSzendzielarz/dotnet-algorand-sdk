
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
public partial class BlockHeaderResponse{

    [Newtonsoft.Json.JsonProperty("blockHeader", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Block header data.")]
    [field:InspectorName(@"Blockheader")]
    public Block Blockheader {get;set;}
#else
    public Block Blockheader {get;set;}
#endif


    
}


}
