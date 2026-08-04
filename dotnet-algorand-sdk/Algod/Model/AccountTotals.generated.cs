
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
    public partial class AccountTotals
    {

        [Newtonsoft.Json.JsonProperty("not-participating", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("not-participating")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Amount of stake in non-participating accounts")]
    [field:InspectorName(@"NotParticipating")]
    public ulong NotParticipating {get;set;}
#else
        public ulong NotParticipating { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("offline", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("offline")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Amount of stake in offline accounts")]
    [field:InspectorName(@"Offline")]
    public ulong Offline {get;set;}
#else
        public ulong Offline { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("online", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("online")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Amount of stake in online accounts")]
    [field:InspectorName(@"Online")]
    public ulong Online {get;set;}
#else
        public ulong Online { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("rewards-level", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("rewards-level")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Total number of algos received per reward unit since genesis")]
    [field:InspectorName(@"RewardsLevel")]
    public ulong RewardsLevel {get;set;}
#else
        public ulong RewardsLevel { get; set; }
#endif



    }


}
