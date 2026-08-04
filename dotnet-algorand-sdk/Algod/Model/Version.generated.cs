
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
    public partial class Version
    {

        [Newtonsoft.Json.JsonProperty("build", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("build")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Build")]
    public BuildVersion Build {get;set;}
#else
        public BuildVersion Build { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("genesis_hash_b64", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("genesis_hash_b64")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Genesis_hash_b64")]
    public byte[] Genesis_hash_b64 {get;set;}
#else
        public byte[] Genesis_hash_b64 { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("genesis_id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("genesis_id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Genesis_id")]
    public string Genesis_id {get;set;}
#else
        public string Genesis_id { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("versions", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("versions")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Versions")]
    public System.Collections.Generic.List<string> Versions {get;set;} = new System.Collections.Generic.List<string>();
#else
        public System.Collections.Generic.ICollection<string> Versions { get; set; } = new System.Collections.ObjectModel.Collection<string>();
#endif

    }


}
