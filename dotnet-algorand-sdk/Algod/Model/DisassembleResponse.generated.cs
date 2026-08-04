
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
    public partial class DisassembleResponse
    {

        [Newtonsoft.Json.JsonProperty("result", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("result")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"disassembled Teal code")]
    [field:InspectorName(@"Result")]
    public string Result {get;set;}
#else
        public string Result { get; set; }
#endif



    }


}
