
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
    public partial class AccountAssetsInformationResponse
    {

        [Newtonsoft.Json.JsonProperty("asset-holdings", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("asset-holdings")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetHoldings")]
    public System.Collections.Generic.List<AccountAssetHolding> AssetHoldings {get;set;} = new System.Collections.Generic.List<AccountAssetHolding>();
#else
        public System.Collections.Generic.ICollection<AccountAssetHolding> AssetHoldings { get; set; } = new System.Collections.ObjectModel.Collection<AccountAssetHolding>();
#endif

        [Newtonsoft.Json.JsonProperty("next-token", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("next-token")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Used for pagination, when making another request provide this token with the next parameter.")]
    [field:InspectorName(@"NextToken")]
    public string NextToken {get;set;}
#else
        public string NextToken { get; set; }
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
