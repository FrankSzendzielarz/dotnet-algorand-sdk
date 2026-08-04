
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
    public partial class DryrunTxnResult
    {

        [Newtonsoft.Json.JsonProperty("app-call-messages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-call-messages")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AppCallMessages")]
    public System.Collections.Generic.List<string> AppCallMessages {get;set;} = new System.Collections.Generic.List<string>();
#else
        public System.Collections.Generic.ICollection<string> AppCallMessages { get; set; } = new System.Collections.ObjectModel.Collection<string>();
#endif

        [Newtonsoft.Json.JsonProperty("app-call-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-call-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"AppCallTrace")]
    public System.Collections.Generic.List<DryrunState> AppCallTrace {get;set;} = new System.Collections.Generic.List<DryrunState>();
#else
        public System.Collections.Generic.ICollection<DryrunState> AppCallTrace { get; set; } = new System.Collections.ObjectModel.Collection<DryrunState>();
#endif

        [Newtonsoft.Json.JsonProperty("budget-added", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("budget-added")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Budget added during execution of app call transaction.")]
    [field:InspectorName(@"BudgetAdded")]
    public ulong BudgetAdded {get;set;}
#else
        public ulong? BudgetAdded { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("budget-consumed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("budget-consumed")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Budget consumed during execution of app call transaction.")]
    [field:InspectorName(@"BudgetConsumed")]
    public ulong BudgetConsumed {get;set;}
#else
        public ulong? BudgetConsumed { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("disassembly", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("disassembly")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Disassembled program line by line.")]
    [field:InspectorName(@"Disassembly")]
    public System.Collections.Generic.List<string> Disassembly {get;set;} = new System.Collections.Generic.List<string>();
#else
        public System.Collections.Generic.ICollection<string> Disassembly { get; set; } = new System.Collections.ObjectModel.Collection<string>();
#endif

        [Newtonsoft.Json.JsonProperty("global-delta", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("global-delta")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Application state delta.")]
    [field:InspectorName(@"GlobalDelta")]
    public System.Collections.Generic.List<EvalDeltaKeyValue> GlobalDelta {get;set;} = new System.Collections.Generic.List<EvalDeltaKeyValue>();
#else
        public System.Collections.Generic.ICollection<EvalDeltaKeyValue> GlobalDelta { get; set; } = new System.Collections.ObjectModel.Collection<EvalDeltaKeyValue>();
#endif

        [Newtonsoft.Json.JsonProperty("local-deltas", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("local-deltas")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"LocalDeltas")]
    public System.Collections.Generic.List<AccountStateDelta> LocalDeltas {get;set;} = new System.Collections.Generic.List<AccountStateDelta>();
#else
        public System.Collections.Generic.ICollection<AccountStateDelta> LocalDeltas { get; set; } = new System.Collections.ObjectModel.Collection<AccountStateDelta>();
#endif

        [Newtonsoft.Json.JsonProperty("logic-sig-disassembly", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logic-sig-disassembly")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Disassembled lsig program line by line.")]
    [field:InspectorName(@"LogicSigDisassembly")]
    public System.Collections.Generic.List<string> LogicSigDisassembly {get;set;} = new System.Collections.Generic.List<string>();
#else
        public System.Collections.Generic.ICollection<string> LogicSigDisassembly { get; set; } = new System.Collections.ObjectModel.Collection<string>();
#endif

        [Newtonsoft.Json.JsonProperty("logic-sig-messages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logic-sig-messages")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"LogicSigMessages")]
    public System.Collections.Generic.List<string> LogicSigMessages {get;set;} = new System.Collections.Generic.List<string>();
#else
        public System.Collections.Generic.ICollection<string> LogicSigMessages { get; set; } = new System.Collections.ObjectModel.Collection<string>();
#endif

        [Newtonsoft.Json.JsonProperty("logic-sig-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logic-sig-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"LogicSigTrace")]
    public System.Collections.Generic.List<DryrunState> LogicSigTrace {get;set;} = new System.Collections.Generic.List<DryrunState>();
#else
        public System.Collections.Generic.ICollection<DryrunState> LogicSigTrace { get; set; } = new System.Collections.ObjectModel.Collection<DryrunState>();
#endif

        [Newtonsoft.Json.JsonProperty("logs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Logs")]
    public System.Collections.Generic.List<byte[]> Logs {get;set;} = new System.Collections.Generic.List<byte[]>();
#else
        public System.Collections.Generic.ICollection<byte[]> Logs { get; set; } = new System.Collections.ObjectModel.Collection<byte[]>();
#endif

    }


}
