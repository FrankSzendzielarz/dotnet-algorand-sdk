
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
    public partial class AccountParticipation
    {

        [Newtonsoft.Json.JsonProperty("selection-participation-key", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("selection-participation-key")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[sel\] Selection public key (if any) currently registered for this round.")]
    [field:InspectorName(@"SelectionParticipationKey")]
    public byte[] SelectionParticipationKey {get;set;}
#else
        public byte[] SelectionParticipationKey { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("state-proof-key", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("state-proof-key")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[stprf\] Root of the state proof key (if any)")]
    [field:InspectorName(@"StateProofKey")]
    public byte[] StateProofKey {get;set;}
#else
        public byte[] StateProofKey { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("vote-first-valid", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("vote-first-valid")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[voteFst\] First round for which this participation is valid.")]
    [field:InspectorName(@"VoteFirstValid")]
    public ulong VoteFirstValid {get;set;}
#else
        public ulong VoteFirstValid { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("vote-key-dilution", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("vote-key-dilution")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[voteKD\] Number of subkeys in each batch of participation keys.")]
    [field:InspectorName(@"VoteKeyDilution")]
    public ulong VoteKeyDilution {get;set;}
#else
        public ulong VoteKeyDilution { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("vote-last-valid", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("vote-last-valid")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[voteLst\] Last round for which this participation is valid.")]
    [field:InspectorName(@"VoteLastValid")]
    public ulong VoteLastValid {get;set;}
#else
        public ulong VoteLastValid { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("vote-participation-key", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("vote-participation-key")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[vote\] root participation public key (if any) currently registered for this round.")]
    [field:InspectorName(@"VoteParticipationKey")]
    public byte[] VoteParticipationKey {get;set;}
#else
        public byte[] VoteParticipationKey { get; set; }
#endif



    }


}
