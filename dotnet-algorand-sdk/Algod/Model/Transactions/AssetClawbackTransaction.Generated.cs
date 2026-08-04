
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject(AllowPrivate = true)]
    public partial class AssetClawbackTransaction : AssetTransferTransaction
    {

        [Newtonsoft.Json.JsonProperty("asnd", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("asnd")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetSender")]
    public Algorand.Address AssetSender {get;set;}
#else
        public Algorand.Address AssetSender { get; set; }
#endif
    }
}
