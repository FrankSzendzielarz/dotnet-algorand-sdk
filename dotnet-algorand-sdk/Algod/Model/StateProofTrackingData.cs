#if UNITY
using UnityEngine;
#endif

namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]
    public partial class StateProofTrackingData
    {
        /// <summary>\[v\] StateProofVotersCommitment.</summary>
        [Newtonsoft.Json.JsonProperty("v")] //, Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"StateProofVotersCommitment.")]
    [field:InspectorName(@"StateProofVotersCommitment")]  
#endif
        [MessagePack.Key("v")]
        public byte[]? StateProofVotersCommitment { get; set; }
        /// <summary>\[t\] StateProofOnlineTotalWeight.</summary>
        [Newtonsoft.Json.JsonProperty("t")] //, Required = Newtonsoft.Json.Required.Always)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"StateProofOnlineTotalWeight.")]
    [field:InspectorName(@"StateProofOnlineTotalWeight")]  
#endif
        [MessagePack.Key("t")]
        public ulong? StateProofOnlineTotalWeight { get; set; }
        /// <summary>\[n\] StateProofNextRound.</summary>
        [Newtonsoft.Json.JsonProperty("n")] //, Required = Newtonsoft.Json.Required.Always)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"StateProofNextRound.")]
    [field:InspectorName(@"StateProofNextRound")]  
#endif
        [MessagePack.Key("n")]
        public ulong? StateProofNextRound { get; set; }
    }
}
