
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
    public partial class ApplicationStateSchema
    {

        [Newtonsoft.Json.JsonProperty("num-byte-slice", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("num-byte-slice")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[nbs\] num of byte slices.")]
    [field:InspectorName(@"NumByteSlice")]
    public ulong NumByteSlice {get;set;}
#else
        public ulong NumByteSlice { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("num-uint", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("num-uint")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[nui\] num of uints.")]
    [field:InspectorName(@"NumUint")]
    public ulong NumUint {get;set;}
#else
        public ulong NumUint { get; set; }
#endif



    }


}
