
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class PaymentTransaction : Transaction
    {

        [Newtonsoft.Json.JsonProperty("amt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("amt")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Amount")]
    public ulong Amount {get;set;}
#else
        public ulong? Amount { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("close", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("close")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"CloseRemainderTo")]
    public Algorand.Address CloseRemainderTo {get;set;}
#else
        public Algorand.Address CloseRemainderTo { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("rcv", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("rcv")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Receiver")]
    public Algorand.Address Receiver {get;set;}
#else
        public Algorand.Address Receiver { get; set; }
#endif



    }


}
