#nullable enable annotations
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
[MessagePack.MessagePackObject]
public partial class CertifiedBlock{

    [Newtonsoft.Json.JsonProperty("block", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    [MessagePack.Key("block")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Block header data.")]
    [field:InspectorName(@"Block")]
    public Block Block {get;set;}
#else
    public Block? Block {get;set;}
#endif



    [Newtonsoft.Json.JsonProperty("cert", Required = Newtonsoft.Json.Required.Default,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [MessagePack.Key("cert")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Optional certificate object. This is only included when the format is set to message pack.")]
    [field:InspectorName(@"Cert")]
    public byte[] Cert {get;set;}
#else
    public BlockCert? Cert {get;set;}
#endif


    
}


}
