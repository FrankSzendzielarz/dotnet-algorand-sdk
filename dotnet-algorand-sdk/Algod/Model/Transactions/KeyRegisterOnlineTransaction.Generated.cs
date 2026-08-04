
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class KeyRegisterOnlineTransaction : KeyRegistrationTransaction
    {

        [Newtonsoft.Json.JsonProperty("nonpart", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("nonpart")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"NonParticipation")]
    public bool NonParticipation {get;set;}
#else
        public bool? NonParticipation { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("selkey", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("selkey")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"SelectionPk")]
    public Algorand.VRFPublicKey SelectionPk {get;set;}
#else
        public Algorand.VRFPublicKey SelectionPk { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("votefst", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("votefst")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"VoteFirst")]
    public ulong VoteFirst {get;set;}
#else
        public ulong? VoteFirst { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("votekd", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("votekd")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"VoteKeyDilution")]
    public ulong VoteKeyDilution {get;set;}
#else
        public ulong? VoteKeyDilution { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("votekey", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("votekey")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Votepk")]
    public Algorand.ParticipationPublicKey Votepk {get;set;}
#else
        public Algorand.ParticipationPublicKey Votepk { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("votelst", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("votelst")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"VoteLast")]
    public ulong VoteLast {get;set;}
#else
        public ulong? VoteLast { get; set; }
#endif



    }


}
