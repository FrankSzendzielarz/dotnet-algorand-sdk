
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
    public partial class LightBlockHeaderProof
    {

        [Newtonsoft.Json.JsonProperty("index", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("index")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The index of the light block header in the vector commitment tree")]
    [field:InspectorName(@"Index")]
    public ulong Index {get;set;}
#else
        public ulong Index { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("proof", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("proof")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The encoded proof.")]
    [field:InspectorName(@"Proof")]
    public byte[] Proof {get;set;}
#else
        public byte[] Proof { get; set; }
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
