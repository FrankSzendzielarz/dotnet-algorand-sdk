
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
    public partial class ApplicationKVStorage
    {

        [Newtonsoft.Json.JsonProperty("account", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("account")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The address of the account associated with the local state.")]
    [field:InspectorName(@"Account")]
    public Address Account {get;set;}
#else
        public Address Account { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("kvs", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("kvs")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Key-Value pairs representing application states.")]
    [field:InspectorName(@"Kvs")]
    public System.Collections.Generic.List<AvmKeyValue> Kvs {get;set;} = new System.Collections.Generic.List<AvmKeyValue>();
#else
        public System.Collections.Generic.ICollection<AvmKeyValue> Kvs { get; set; } = new System.Collections.ObjectModel.Collection<AvmKeyValue>();
#endif

    }


}
