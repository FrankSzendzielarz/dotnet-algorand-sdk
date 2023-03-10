
namespace Algorand.Algod.Model.Transactions
{

using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
public partial class ApplicationClearStateTransaction : ApplicationCallTransaction{

    [Newtonsoft.Json.JsonProperty("apid", Required = Newtonsoft.Json.Required.Default,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ApplicationId")]
    public ulong ApplicationId {get;set;}
#else
    public ulong? ApplicationId {get;set;}
#endif


    
}


}
