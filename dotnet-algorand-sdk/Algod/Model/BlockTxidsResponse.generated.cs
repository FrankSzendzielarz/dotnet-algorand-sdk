
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
public partial class BlockTxidsResponse{

    [Newtonsoft.Json.JsonProperty("blockTxids", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Block transaction IDs.")]
    [field:InspectorName(@"Blocktxids")]
    public System.Collections.Generic.List<string> Blocktxids {get;set;} = new System.Collections.Generic.List<string>();
#else
    public System.Collections.Generic.ICollection<string> Blocktxids {get;set;} = new System.Collections.ObjectModel.Collection<string>();
#endif
    
}


}
