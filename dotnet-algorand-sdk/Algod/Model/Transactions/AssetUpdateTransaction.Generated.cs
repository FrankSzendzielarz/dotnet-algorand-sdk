
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class AssetUpdateTransaction : AssetChangeTransaction
    {

        [Newtonsoft.Json.JsonProperty("apar", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apar")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetParams")]
    public Algorand.Algod.Model.AssetParams AssetParams {get;set;}
#else
        public Algorand.Algod.Model.AssetParams AssetParams { get; set; }
#endif
    }
}
