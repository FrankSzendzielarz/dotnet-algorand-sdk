
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class StateSchema
    {

        [Newtonsoft.Json.JsonProperty("nbs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("nbs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"NumByteSlice")]
    public ulong NumByteSlice {get;set;}
#else
        public ulong? NumByteSlice { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("nui", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("nui")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"NumUint")]
    public ulong NumUint {get;set;}
#else
        public ulong? NumUint { get; set; }
#endif



    }


}
