
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class ApplicationOptInTransaction : ApplicationCallTransaction
    {

        [Newtonsoft.Json.JsonProperty("apid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apid")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ApplicationId")]
    public ulong ApplicationId {get;set;}
#else
        public ulong? ApplicationId { get; set; }
#endif



    }


}
