
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
    public partial class ApplicationLocalState
    {

        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("id")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The application which this local state is for.")]
    [field:InspectorName(@"Id")]
    public ulong Id {get;set;}
#else
        public ulong Id { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("key-value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("key-value")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[tkv\] storage.")]
    [field:InspectorName(@"KeyValue")]
    public System.Collections.Generic.List<TealKeyValue> KeyValue {get;set;} = new System.Collections.Generic.List<TealKeyValue>();
#else
        public System.Collections.Generic.ICollection<TealKeyValue> KeyValue { get; set; } = new System.Collections.ObjectModel.Collection<TealKeyValue>();
#endif

        [Newtonsoft.Json.JsonProperty("schema", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("schema")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[hsch\] schema.")]
    [field:InspectorName(@"Schema")]
    public ApplicationStateSchema Schema {get;set;}
#else
        public ApplicationStateSchema Schema { get; set; }
#endif



    }


}
