
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
    public partial class Asset
    {

        [Newtonsoft.Json.JsonProperty("index", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("index")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"unique asset identifier")]
    [field:InspectorName(@"Index")]
    public ulong Index {get;set;}
#else
        public ulong Index { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("params", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("params")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"AssetParams specifies the parameters for an asset.

\[apar\] when part of an AssetConfig transaction.

Definition:
data/transactions/asset.go : AssetParams")]
    [field:InspectorName(@"Params")]
    public AssetParams Params {get;set;}
#else
        public AssetParams Params { get; set; }
#endif



    }


}
