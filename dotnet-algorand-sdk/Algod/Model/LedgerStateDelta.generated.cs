
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
    public partial class LedgerStateDelta
    {

        [Newtonsoft.Json.JsonProperty("accts", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("accts")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"AccountDeltas object")]
    [field:InspectorName(@"Accts")]
    public AccountDeltas Accts {get;set;}
#else
        public AccountDeltas Accts { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("kv-mods", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("kv-mods")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Array of KV Deltas")]
    [field:InspectorName(@"KvMods")]
    public System.Collections.Generic.List<KvDelta> KvMods {get;set;} = new System.Collections.Generic.List<KvDelta>();
#else
        public System.Collections.Generic.ICollection<KvDelta> KvMods { get; set; } = new System.Collections.ObjectModel.Collection<KvDelta>();
#endif

        [Newtonsoft.Json.JsonProperty("modified-apps", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("modified-apps")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"List of modified Apps")]
    [field:InspectorName(@"ModifiedApps")]
    public System.Collections.Generic.List<ModifiedApp> ModifiedApps {get;set;} = new System.Collections.Generic.List<ModifiedApp>();
#else
        public System.Collections.Generic.ICollection<ModifiedApp> ModifiedApps { get; set; } = new System.Collections.ObjectModel.Collection<ModifiedApp>();
#endif

        [Newtonsoft.Json.JsonProperty("modified-assets", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("modified-assets")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"List of modified Assets")]
    [field:InspectorName(@"ModifiedAssets")]
    public System.Collections.Generic.List<ModifiedAsset> ModifiedAssets {get;set;} = new System.Collections.Generic.List<ModifiedAsset>();
#else
        public System.Collections.Generic.ICollection<ModifiedAsset> ModifiedAssets { get; set; } = new System.Collections.ObjectModel.Collection<ModifiedAsset>();
#endif

        [Newtonsoft.Json.JsonProperty("prev-timestamp", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("prev-timestamp")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Previous block timestamp")]
    [field:InspectorName(@"PrevTimestamp")]
    public ulong PrevTimestamp {get;set;}
#else
        public ulong? PrevTimestamp { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("state-proof-next", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("state-proof-next")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Next round for which we expect a state proof")]
    [field:InspectorName(@"StateProofNext")]
    public ulong StateProofNext {get;set;}
#else
        public ulong? StateProofNext { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("totals", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("totals")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Account Totals")]
    [field:InspectorName(@"Totals")]
    public AccountTotals Totals {get;set;}
#else
        public AccountTotals Totals { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("tx-leases", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("tx-leases")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"List of transaction leases")]
    [field:InspectorName(@"TxLeases")]
    public System.Collections.Generic.List<TxLease> TxLeases {get;set;} = new System.Collections.Generic.List<TxLease>();
#else
        public System.Collections.Generic.ICollection<TxLease> TxLeases { get; set; } = new System.Collections.ObjectModel.Collection<TxLease>();
#endif

    }


}
