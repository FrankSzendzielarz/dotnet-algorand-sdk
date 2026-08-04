
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
    public partial class TransactionProofResponse
    {

        [Newtonsoft.Json.JsonProperty("hashtype", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("hashtype")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The type of hash function used to create the proof, must be one of: 
* sha512_256 
* sha256")]
    [field:InspectorName(@"Hashtype")]
    public string Hashtype {get;set;}
#else
        public string Hashtype { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("idx", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("idx")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Index of the transaction in the block's payset.")]
    [field:InspectorName(@"Idx")]
    public ulong Idx {get;set;}
#else
        public ulong Idx { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("proof", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("proof")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Proof of transaction membership.")]
    [field:InspectorName(@"Proof")]
    public byte[] Proof {get;set;}
#else
        public byte[] Proof { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("stibhash", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("stibhash")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Hash of SignedTxnInBlock for verifying proof.")]
    [field:InspectorName(@"Stibhash")]
    public byte[] Stibhash {get;set;}
#else
        public byte[] Stibhash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("treedepth", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("treedepth")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Represents the depth of the tree that is being proven, i.e. the number of edges from a leaf to the root.")]
    [field:InspectorName(@"Treedepth")]
    public ulong Treedepth {get;set;}
#else
        public ulong Treedepth { get; set; }
#endif



    }


}
