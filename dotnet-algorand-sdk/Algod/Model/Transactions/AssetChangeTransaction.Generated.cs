
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class AssetChangeTransaction : AssetConfigurationTransaction
    {
        [Newtonsoft.Json.JsonProperty("caid", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("caid")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetIndex")]
    public ulong AssetIndex {get;set;}
#else
        public ulong AssetIndex { get; set; }
#endif



    }


}
