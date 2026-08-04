
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class Transaction
    {

        [Newtonsoft.Json.JsonProperty("fee", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("fee")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Fee")]
    public ulong Fee {get;set;}
#else
        public ulong Fee { get; set; } = 0;
#endif



        [Newtonsoft.Json.JsonProperty("fv", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("fv")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"FirstValid")]
    public ulong FirstValid {get;set;}
#else
        public ulong FirstValid { get; set; } = 0;
#endif



        [Newtonsoft.Json.JsonProperty("gen", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("gen")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"GenesisId")]
    public string GenesisId {get;set;}
#else
        public string GenesisId { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("gh", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("gh")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"GenesisHash")]
    public Algorand.Digest GenesisHash {get;set;}
#else
        public Algorand.Digest GenesisHash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("grp", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("grp")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Group")]
    public Algorand.Digest Group {get;set;}
#else
        public Algorand.Digest Group { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("lv", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("lv")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"LastValid")]
    public ulong LastValid {get;set;}
#else
        public ulong LastValid { get; set; } = 0;
#endif



        [Newtonsoft.Json.JsonProperty("rekey", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("rekey")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"RekeyTo")]
    public Algorand.Address RekeyTo {get;set;}
#else
        public Algorand.Address RekeyTo { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("snd", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("snd")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Sender")]
    public Algorand.Address Sender {get;set;}
#else
        public Algorand.Address Sender { get; set; }
#endif
    }
}
