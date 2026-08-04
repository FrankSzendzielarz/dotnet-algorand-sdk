
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
public partial class BlockLogsResponse{

    [Newtonsoft.Json.JsonProperty("logs", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Logs")]
    public System.Collections.Generic.List<AppCallLogs> Logs {get;set;} = new System.Collections.Generic.List<AppCallLogs>();
#else
    public System.Collections.Generic.ICollection<AppCallLogs> Logs {get;set;} = new System.Collections.ObjectModel.Collection<AppCallLogs>();
#endif
    
}


}
