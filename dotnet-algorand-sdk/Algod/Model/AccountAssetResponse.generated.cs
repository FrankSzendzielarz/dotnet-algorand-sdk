
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
    public partial class AccountAssetResponse
    {

        [Newtonsoft.Json.JsonProperty("asset-holding", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("asset-holding")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[asset\] Details about the asset held by this account.

The raw account uses `AssetHolding` for this type.")]
    [field:InspectorName(@"AssetHolding")]
    public AssetHolding AssetHolding {get;set;}
#else
        public AssetHolding AssetHolding { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("created-asset", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("created-asset")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[apar\] parameters of the asset created by this account.

The raw account uses `AssetParams` for this type.")]
    [field:InspectorName(@"CreatedAsset")]
    public AssetParams CreatedAsset {get;set;}
#else
        public AssetParams CreatedAsset { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round for which this information is relevant.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



    }


}
