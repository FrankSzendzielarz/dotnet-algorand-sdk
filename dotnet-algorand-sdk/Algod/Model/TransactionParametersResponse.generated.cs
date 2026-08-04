
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
    public partial class TransactionParametersResponse
    {

        [Newtonsoft.Json.JsonProperty("consensus-version", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("consensus-version")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"ConsensusVersion indicates the consensus protocol version
as of LastRound.")]
    [field:InspectorName(@"ConsensusVersion")]
    public string ConsensusVersion {get;set;}
#else
        public string ConsensusVersion { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("fee", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("fee")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Fee is the suggested transaction fee
Fee is in units of micro-Algos per byte.
Fee may fall to zero but transactions must still have a fee of
at least MinTxnFee for the current network protocol.")]
    [field:InspectorName(@"Fee")]
    public ulong Fee {get;set;}
#else
        public ulong Fee { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("genesis-hash", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("genesis-hash")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"GenesisHash is the hash of the genesis block.")]
    [field:InspectorName(@"GenesisHash")]
    public byte[] GenesisHash {get;set;}
#else
        public byte[] GenesisHash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("genesis-id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("genesis-id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"GenesisID is an ID listed in the genesis block.")]
    [field:InspectorName(@"GenesisId")]
    public string GenesisId {get;set;}
#else
        public string GenesisId { get; set; }
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



        [Newtonsoft.Json.JsonProperty("min-fee", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("min-fee")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The minimum transaction fee (not per byte) required for the
txn to validate for the current network protocol.")]
    [field:InspectorName(@"MinFee")]
    public ulong MinFee {get;set;}
#else
        public ulong MinFee { get; set; }
#endif



    }


}
