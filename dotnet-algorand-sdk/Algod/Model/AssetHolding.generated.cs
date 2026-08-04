
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
    public partial class AssetHolding
    {

        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("amount")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[a\] number of units held.")]
    [field:InspectorName(@"Amount")]
    public ulong Amount {get;set;}
#else
        public ulong Amount { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset-id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("asset-id")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"Asset ID of the holding.")]
    [field:InspectorName(@"AssetId")]
    public ulong AssetId {get;set;}
#else
        public ulong AssetId { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("is-frozen", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("is-frozen")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[f\] whether or not the holding is frozen.")]
    [field:InspectorName(@"IsFrozen")]
    public bool IsFrozen {get;set;}
#else
        public bool IsFrozen { get; set; }
#endif



    }


}
