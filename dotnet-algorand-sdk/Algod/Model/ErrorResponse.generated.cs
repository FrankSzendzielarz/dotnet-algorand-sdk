
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
    public partial class ErrorResponse
    {

        [Newtonsoft.Json.JsonProperty("message", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("message")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Message")]
    public string Message {get;set;}
#else
        public string Message { get; set; }
#endif



    }


}
