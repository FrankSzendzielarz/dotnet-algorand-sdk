
namespace Algorand.Algod.Model.Transactions
{
    using Algorand.Algod.Model.Converters.MsgPack;
    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject]
    [MessagePack.MessagePackFormatter(typeof(NoDefaultsFormatter<SignedTransaction>))]
    public partial class SignedTransaction
    {

        [Newtonsoft.Json.JsonProperty("sig", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Sig")]
    public Algorand.Signature Sig {get;set;}
#else
        [MessagePack.Key("sig")]
        public Algorand.Signature Sig { get; set; }
#endif

        [Newtonsoft.Json.JsonProperty("txn", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Tx")]
    public Transaction Tx {get;set;}
#else
        [MessagePack.Key("txn")]
        public Transaction Tx { get; set; }
#endif

        [Newtonsoft.Json.JsonProperty("msig", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"MSig")]
    public Algorand.MultisigSignature MSig {get;set;}
#else
        [MessagePack.Key("msig")]
        public Algorand.MultisigSignature MSig { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("lsig", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"LSig")]
    public Algorand.LogicsigSignature LSig {get;set;}
#else
        [MessagePack.Key("lsig")]
        public Algorand.LogicsigSignature LSig { get; set; }
#endif

        [Newtonsoft.Json.JsonProperty("sgnr", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AuthAddr")]
    public Algorand.Address AuthAddr {get;set;}
#else
        [MessagePack.Key("sgnr")]
        public Algorand.Address AuthAddr { get; set; }
#endif


    }


}
