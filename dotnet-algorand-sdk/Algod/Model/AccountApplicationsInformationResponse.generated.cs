
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
    public partial class AccountApplicationsInformationResponse
    {

        [Newtonsoft.Json.JsonProperty("application-resources", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("application-resources")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ApplicationResources")]
    public System.Collections.Generic.List<AccountApplicationResource> ApplicationResources {get;set;} = new System.Collections.Generic.List<AccountApplicationResource>();
#else
        public System.Collections.Generic.ICollection<AccountApplicationResource> ApplicationResources { get; set; } = new System.Collections.ObjectModel.Collection<AccountApplicationResource>();
#endif

        [Newtonsoft.Json.JsonProperty("next-token", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("next-token")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Used for pagination, when making another request provide this token with the next parameter. The next token is the next application ID to use as the pagination cursor.")]
    [field:InspectorName(@"NextToken")]
    public string NextToken {get;set;}
#else
        public string NextToken { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round for which this information is relevant.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



    }


}
