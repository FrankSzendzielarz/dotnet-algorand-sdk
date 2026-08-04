
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
    public partial class DryrunSource
    {

        [Newtonsoft.Json.JsonProperty("app-index", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("app-index")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AppIndex")]
    public ulong AppIndex {get;set;}
#else
        public ulong AppIndex { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("field-name", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("field-name")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"FieldName is what kind of sources this is. If lsig then it goes into the transactions[this.TxnIndex].LogicSig. If approv or clearp it goes into the Approval Program or Clear State Program of application[this.AppIndex].")]
    [field:InspectorName(@"FieldName")]
    public string FieldName {get;set;}
#else
        public string FieldName { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("source", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("source")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Source")]
    public string Source {get;set;}
#else
        public string Source { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("txn-index", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("txn-index")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"TxnIndex")]
    public ulong TxnIndex {get;set;}
#else
        public ulong TxnIndex { get; set; }
#endif



    }


}
