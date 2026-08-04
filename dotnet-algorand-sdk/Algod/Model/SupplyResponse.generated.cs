
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
    public partial class SupplyResponse
    {

        [Newtonsoft.Json.JsonProperty("current_round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("current_round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Round")]
    [field:InspectorName(@"Current_round")]
    public ulong Current_round {get;set;}
#else
        public ulong Current_round { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("online-money", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("online-money")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"OnlineMoney")]
    [field:InspectorName(@"OnlineMoney")]
    public ulong OnlineMoney {get;set;}
#else
        public ulong OnlineMoney { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-money", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("total-money")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"TotalMoney")]
    [field:InspectorName(@"TotalMoney")]
    public ulong TotalMoney {get;set;}
#else
        public ulong TotalMoney { get; set; }
#endif



    }


}
