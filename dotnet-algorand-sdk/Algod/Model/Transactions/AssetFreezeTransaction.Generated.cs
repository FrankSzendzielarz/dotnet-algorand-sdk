
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class AssetFreezeTransaction : Transaction
    {

        [Newtonsoft.Json.JsonProperty("afrz", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("afrz")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"FreezeState")]
    public bool FreezeState {get;set;}
#else
        public bool? FreezeState { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("fadd", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("fadd")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"FreezeTarget")]
    public Algorand.Address FreezeTarget {get;set;}
#else
        public Algorand.Address FreezeTarget { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("faid", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("faid")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AssetFreezeId")]
    public ulong AssetFreezeId {get;set;}
#else
        public ulong AssetFreezeId { get; set; }
#endif



    }


}
