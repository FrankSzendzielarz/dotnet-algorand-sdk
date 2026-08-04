
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
    [MessagePack.MessagePackObject]
    public partial class AssetHoldingReference
    {

        [Newtonsoft.Json.JsonProperty("account", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("account")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of the account holding the asset.")]
    [field:InspectorName(@"Account")]
    public Address Account {get;set;}
#else
        public Address Account { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("asset", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("asset")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"Asset ID of the holding.")]
    [field:InspectorName(@"Asset")]
    public ulong Asset {get;set;}
#else
        public ulong Asset { get; set; }
#endif



    }


}
