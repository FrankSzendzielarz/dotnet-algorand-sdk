
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
    public partial class GetBlockTimeStampOffsetResponse
    {

        [Newtonsoft.Json.JsonProperty("offset", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("offset")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Timestamp offset in seconds.")]
    [field:InspectorName(@"Offset")]
    public ulong Offset {get;set;}
#else
        public ulong Offset { get; set; }
#endif



    }


}
