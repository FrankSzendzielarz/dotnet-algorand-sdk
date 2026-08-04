
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
    public partial class DryrunRequest
    {

        [Newtonsoft.Json.JsonProperty("accounts", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("accounts")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Accounts")]
    public System.Collections.Generic.List<Account> Accounts {get;set;} = new System.Collections.Generic.List<Account>();
#else
        public System.Collections.Generic.ICollection<Account> Accounts { get; set; } = new System.Collections.ObjectModel.Collection<Account>();
#endif

        [Newtonsoft.Json.JsonProperty("apps", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("apps")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Apps")]
    public System.Collections.Generic.List<Application> Apps {get;set;} = new System.Collections.Generic.List<Application>();
#else
        public System.Collections.Generic.ICollection<Application> Apps { get; set; } = new System.Collections.ObjectModel.Collection<Application>();
#endif

        [Newtonsoft.Json.JsonProperty("latest-timestamp", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("latest-timestamp")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"LatestTimestamp is available to some TEAL scripts. Defaults to the latest confirmed timestamp this algod is attached to.")]
    [field:InspectorName(@"LatestTimestamp")]
    public ulong LatestTimestamp {get;set;}
#else
        public ulong LatestTimestamp { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("protocol-version", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("protocol-version")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"ProtocolVersion specifies a specific version string to operate under, otherwise whatever the current protocol of the network this algod is running in.")]
    [field:InspectorName(@"ProtocolVersion")]
    public string ProtocolVersion {get;set;}
#else
        public string ProtocolVersion { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Round is available to some TEAL scripts. Defaults to the current round on the network this algod is attached to.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("sources", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("sources")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Sources")]
    public System.Collections.Generic.List<DryrunSource> Sources {get;set;} = new System.Collections.Generic.List<DryrunSource>();
#else
        public System.Collections.Generic.ICollection<DryrunSource> Sources { get; set; } = new System.Collections.ObjectModel.Collection<DryrunSource>();
#endif

        [Newtonsoft.Json.JsonProperty("txns", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txns")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Txns")]
    public System.Collections.Generic.List<SignedTransaction> Txns {get;set;} = new System.Collections.Generic.List<SignedTransaction>();
#else
        public System.Collections.Generic.ICollection<SignedTransaction> Txns { get; set; } = new System.Collections.ObjectModel.Collection<SignedTransaction>();
#endif

    }


}
