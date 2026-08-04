
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
    public partial class AccountBalanceRecord
    {

        [Newtonsoft.Json.JsonProperty("account-data", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("account-data")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Updated account data.")]
    [field:InspectorName(@"AccountData")]
    public Account AccountData {get;set;}
#else
        public Account AccountData { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("address")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of the updated account.")]
    [field:InspectorName(@"Address")]
    public string Address {get;set;}
#else
        public string Address { get; set; }
#endif



    }


}
