
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
    public partial class ApplicationStateOperation
    {

        [Newtonsoft.Json.JsonProperty("account", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("account")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"For local state changes, the address of the account associated with the local state.")]
    [field:InspectorName(@"Account")]
    public Address Account {get;set;}
#else
        public Address Account { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("app-state-type", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("app-state-type")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Type of application state. Value `g` is **global state**, `l` is **local state**, `b` is **boxes**.")]
    [field:InspectorName(@"AppStateType")]
    public string AppStateType {get;set;}
#else
        public string AppStateType { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("key")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The key (name) of the global/local/box state.")]
    [field:InspectorName(@"Key")]
    public byte[] Key {get;set;}
#else
        public byte[] Key { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("new-value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("new-value")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Represents an AVM value.")]
    [field:InspectorName(@"NewValue")]
    public AvmValue NewValue {get;set;}
#else
        public AvmValue NewValue { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("operation", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("operation")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Operation type. Value `w` is **write**, `d` is **delete**.")]
    [field:InspectorName(@"Operation")]
    public string Operation {get;set;}
#else
        public string Operation { get; set; }
#endif



    }


}
