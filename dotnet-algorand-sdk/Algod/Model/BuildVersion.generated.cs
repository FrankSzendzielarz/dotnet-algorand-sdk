
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
    public partial class BuildVersion
    {

        [Newtonsoft.Json.JsonProperty("branch", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("branch")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Branch")]
    public string Branch {get;set;}
#else
        public string Branch { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("build_number", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("build_number")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Build_number")]
    public ulong Build_number {get;set;}
#else
        public ulong Build_number { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("channel", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("channel")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Channel")]
    public string Channel {get;set;}
#else
        public string Channel { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("commit_hash", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("commit_hash")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Commit_hash")]
    public string Commit_hash {get;set;}
#else
        public string Commit_hash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("major", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("major")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Major")]
    public ulong Major {get;set;}
#else
        public ulong Major { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("minor", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("minor")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Minor")]
    public ulong Minor {get;set;}
#else
        public ulong Minor { get; set; }
#endif



    }


}
