
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
    public partial class AssetResourceRecord
    {

        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("address")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Account address of the asset")]
    [field:InspectorName(@"Address")]
    public string Address {get;set;}
#else
        public string Address { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset-deleted", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("asset-deleted")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"Whether the asset was deleted")]
    [field:InspectorName(@"AssetDeleted")]
    public bool AssetDeleted {get;set;}
#else
        public bool AssetDeleted { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset-holding", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("asset-holding")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The asset holding")]
    [field:InspectorName(@"AssetHolding")]
    public AssetHolding AssetHolding {get;set;}
#else
        public AssetHolding AssetHolding { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset-holding-deleted", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("asset-holding-deleted")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Whether the asset holding was deleted")]
    [field:InspectorName(@"AssetHoldingDeleted")]
    public bool AssetHoldingDeleted {get;set;}
#else
        public bool AssetHoldingDeleted { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset-index", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("asset-index")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Index of the asset")]
    [field:InspectorName(@"AssetIndex")]
    public ulong AssetIndex {get;set;}
#else
        public ulong AssetIndex { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset-params", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("asset-params")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Asset params")]
    [field:InspectorName(@"AssetParams")]
    public AssetParams AssetParams {get;set;}
#else
        public AssetParams AssetParams { get; set; }
#endif

    }
}