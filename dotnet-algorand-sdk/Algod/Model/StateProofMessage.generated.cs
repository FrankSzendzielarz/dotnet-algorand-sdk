
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
    public partial class StateProofMessage
    {

        [Newtonsoft.Json.JsonProperty("BlockHeadersCommitment", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("BlockHeadersCommitment")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The vector commitment root on all light block headers within a state proof interval.")]
    [field:InspectorName(@"Blockheaderscommitment")]
    public byte[] Blockheaderscommitment {get;set;}
#else
        public byte[] Blockheaderscommitment { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("FirstAttestedRound", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("FirstAttestedRound")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The first round the message attests to.")]
    [field:InspectorName(@"Firstattestedround")]
    public ulong Firstattestedround {get;set;}
#else
        public ulong Firstattestedround { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("LastAttestedRound", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("LastAttestedRound")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The last round the message attests to.")]
    [field:InspectorName(@"Lastattestedround")]
    public ulong Lastattestedround {get;set;}
#else
        public ulong Lastattestedround { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("LnProvenWeight", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("LnProvenWeight")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An integer value representing the natural log of the proven weight with 16 bits of precision. This value would be used to verify the next state proof.")]
    [field:InspectorName(@"Lnprovenweight")]
    public ulong Lnprovenweight {get;set;}
#else
        public ulong Lnprovenweight { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("VotersCommitment", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("VotersCommitment")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The vector commitment root of the top N accounts to sign the next StateProof.")]
    [field:InspectorName(@"Voterscommitment")]
    public byte[] Voterscommitment {get;set;}
#else
        public byte[] Voterscommitment { get; set; }
#endif



    }


}
