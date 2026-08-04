
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
    public partial class NodeStatusResponse
    {

        [Newtonsoft.Json.JsonProperty("catchpoint", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The current catchpoint that is being caught up to")]
    [field:InspectorName(@"Catchpoint")]
    public string Catchpoint {get;set;}
#else
        public string Catchpoint { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-acquired-blocks", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-acquired-blocks")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of blocks that have already been obtained by the node as part of the catchup")]
    [field:InspectorName(@"CatchpointAcquiredBlocks")]
    public ulong CatchpointAcquiredBlocks {get;set;}
#else
        public ulong? CatchpointAcquiredBlocks { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-processed-accounts", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-processed-accounts")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of accounts from the current catchpoint that have been processed so far as part of the catchup")]
    [field:InspectorName(@"CatchpointProcessedAccounts")]
    public ulong CatchpointProcessedAccounts {get;set;}
#else
        public ulong? CatchpointProcessedAccounts { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-processed-kvs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-processed-kvs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of key-values (KVs) from the current catchpoint that have been processed so far as part of the catchup")]
    [field:InspectorName(@"CatchpointProcessedKvs")]
    public ulong CatchpointProcessedKvs {get;set;}
#else
        public ulong? CatchpointProcessedKvs { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-total-accounts", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-total-accounts")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The total number of accounts included in the current catchpoint")]
    [field:InspectorName(@"CatchpointTotalAccounts")]
    public ulong CatchpointTotalAccounts {get;set;}
#else
        public ulong? CatchpointTotalAccounts { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-total-blocks", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-total-blocks")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The total number of blocks that are required to complete the current catchpoint catchup")]
    [field:InspectorName(@"CatchpointTotalBlocks")]
    public ulong CatchpointTotalBlocks {get;set;}
#else
        public ulong? CatchpointTotalBlocks { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-total-kvs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-total-kvs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The total number of key-values (KVs) included in the current catchpoint")]
    [field:InspectorName(@"CatchpointTotalKvs")]
    public ulong CatchpointTotalKvs {get;set;}
#else
        public ulong? CatchpointTotalKvs { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-verified-accounts", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-verified-accounts")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of accounts from the current catchpoint that have been verified so far as part of the catchup")]
    [field:InspectorName(@"CatchpointVerifiedAccounts")]
    public ulong CatchpointVerifiedAccounts {get;set;}
#else
        public ulong? CatchpointVerifiedAccounts { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchpoint-verified-kvs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("catchpoint-verified-kvs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of key-values (KVs) from the current catchpoint that have been verified so far as part of the catchup")]
    [field:InspectorName(@"CatchpointVerifiedKvs")]
    public ulong CatchpointVerifiedKvs {get;set;}
#else
        public ulong? CatchpointVerifiedKvs { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("catchup-time", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("catchup-time")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"CatchupTime in nanoseconds")]
    [field:InspectorName(@"CatchupTime")]
    public ulong CatchupTime {get;set;}
#else
        public ulong CatchupTime { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("last-catchpoint", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("last-catchpoint")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The last catchpoint seen by the node")]
    [field:InspectorName(@"LastCatchpoint")]
    public string LastCatchpoint {get;set;}
#else
        public string LastCatchpoint { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("last-round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("last-round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"LastRound indicates the last round seen")]
    [field:InspectorName(@"LastRound")]
    public ulong LastRound {get;set;}
#else
        public ulong LastRound { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("last-version", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("last-version")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"LastVersion indicates the last consensus version supported")]
    [field:InspectorName(@"LastVersion")]
    public string LastVersion {get;set;}
#else
        public string LastVersion { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("next-version", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("next-version")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"NextVersion of consensus protocol to use")]
    [field:InspectorName(@"NextVersion")]
    public string NextVersion {get;set;}
#else
        public string NextVersion { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("next-version-round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("next-version-round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"NextVersionRound is the round at which the next consensus version will apply")]
    [field:InspectorName(@"NextVersionRound")]
    public ulong NextVersionRound {get;set;}
#else
        public ulong NextVersionRound { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("next-version-supported", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("next-version-supported")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"NextVersionSupported indicates whether the next consensus version is supported by this node")]
    [field:InspectorName(@"NextVersionSupported")]
    public bool NextVersionSupported {get;set;}
#else
        public bool NextVersionSupported { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("stopped-at-unsupported-round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("stopped-at-unsupported-round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"StoppedAtUnsupportedRound indicates that the node does not support the new rounds and has stopped making progress")]
    [field:InspectorName(@"StoppedAtUnsupportedRound")]
    public bool StoppedAtUnsupportedRound {get;set;}
#else
        public bool StoppedAtUnsupportedRound { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("time-since-last-round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("time-since-last-round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"TimeSinceLastRound in nanoseconds")]
    [field:InspectorName(@"TimeSinceLastRound")]
    public ulong TimeSinceLastRound {get;set;}
#else
        public ulong TimeSinceLastRound { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-delay", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-delay")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Upgrade delay")]
    [field:InspectorName(@"UpgradeDelay")]
    public ulong UpgradeDelay {get;set;}
#else
        public ulong? UpgradeDelay { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-next-protocol-vote-before", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-next-protocol-vote-before")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Next protocol round")]
    [field:InspectorName(@"UpgradeNextProtocolVoteBefore")]
    public ulong UpgradeNextProtocolVoteBefore {get;set;}
#else
        public ulong? UpgradeNextProtocolVoteBefore { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-no-votes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-no-votes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"No votes cast for consensus upgrade")]
    [field:InspectorName(@"UpgradeNoVotes")]
    public ulong UpgradeNoVotes {get;set;}
#else
        public ulong? UpgradeNoVotes { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-node-vote", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-node-vote")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"This node's upgrade vote")]
    [field:InspectorName(@"UpgradeNodeVote")]
    public bool UpgradeNodeVote {get;set;}
#else
        public bool? UpgradeNodeVote { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-vote-rounds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-vote-rounds")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Total voting rounds for current upgrade")]
    [field:InspectorName(@"UpgradeVoteRounds")]
    public ulong UpgradeVoteRounds {get;set;}
#else
        public ulong? UpgradeVoteRounds { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-votes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-votes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Total votes cast for consensus upgrade")]
    [field:InspectorName(@"UpgradeVotes")]
    public ulong UpgradeVotes {get;set;}
#else
        public ulong? UpgradeVotes { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-votes-required", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-votes-required")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Yes votes required for consensus upgrade")]
    [field:InspectorName(@"UpgradeVotesRequired")]
    public ulong UpgradeVotesRequired {get;set;}
#else
        public ulong? UpgradeVotesRequired { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("upgrade-yes-votes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("upgrade-yes-votes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Yes votes cast for consensus upgrade")]
    [field:InspectorName(@"UpgradeYesVotes")]
    public ulong UpgradeYesVotes {get;set;}
#else
        public ulong? UpgradeYesVotes { get; set; }
#endif



    }


}
