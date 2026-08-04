
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
    public partial class StateProof
    {

        [Newtonsoft.Json.JsonProperty("Message", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("Message")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Represents the message that the state proofs are attesting to.")]
    [field:InspectorName(@"Message")]
    public StateProofMessage Message {get;set;}
#else
        public StateProofMessage Message { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("StateProof", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("StateProof")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The encoded StateProof for the message.")]
    [field:InspectorName(@"Stateproof")]
    public byte[] Stateproof {get;set;}
#else
        public byte[] Stateproof { get; set; }
#endif



    }


}
