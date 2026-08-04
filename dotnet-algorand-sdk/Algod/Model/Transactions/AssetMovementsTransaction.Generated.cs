
namespace Algorand.Algod.Model.Transactions
{
    using MessagePack;
    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject]
    [Union(0, typeof(AssetAcceptTransaction))]
    [Union(1, typeof(AssetClawbackTransaction))]
    [Union(2, typeof(AssetTransferTransaction))]
    [Union(3, typeof(AssetCloseTransaction))]
    public partial class AssetMovementsTransaction : Transaction
    {
        [Newtonsoft.Json.JsonProperty("xaid", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("xaid")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"XferAsset")]
    public ulong XferAsset {get;set;}
#else
        public ulong XferAsset { get; set; }
#endif
    }
}
