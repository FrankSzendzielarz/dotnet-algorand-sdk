
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
    public partial class CompileResponse
    {

        [Newtonsoft.Json.JsonProperty("hash", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("hash")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"base32 SHA512_256 of program bytes (Address style)")]
    [field:InspectorName(@"Hash")]
    public string Hash {get;set;}
#else
        public string Hash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("result", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("result")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"base64 encoded program bytes")]
    [field:InspectorName(@"Result")]
    public string Result {get;set;}
#else
        public string Result { get; set; }
#endif



    }


}
