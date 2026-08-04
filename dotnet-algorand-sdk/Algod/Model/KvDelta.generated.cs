
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
    public partial class KvDelta
    {

        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("key")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The key, base64 encoded.")]
    [field:InspectorName(@"Key")]
    public byte[] Key {get;set;}
#else
        public byte[] Key { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("value")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The new value of the KV store entry, base64 encoded.")]
    [field:InspectorName(@"Value")]
    public byte[] Value {get;set;}
#else
        public byte[] Value { get; set; }
#endif



    }


}
