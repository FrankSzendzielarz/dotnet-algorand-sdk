

using Algorand.Algod.Model.Transactions;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
#if UNITY
using UnityEngine;
#endif
namespace Algorand.Algod.Model
{
#if UNITY
    [System.Serializable]
#endif
    [MessagePack.MessagePackObject(AllowPrivate = true)]
    public class ReturnedTransaction
    {

        [JsonProperty("txn")]
        [MessagePack.Key("txn")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"The raw signed transaction.")]
        [field: InspectorName(@"Transaction")]
        internal SignedTransaction Transaction { get; set; }
#else
        public SignedTransaction Transaction { get; set; }
#endif

        /// <summary>The round where this transaction was confirmed, if present.</summary>
        [JsonProperty("confirmed-round", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("confirmed-round")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"The round where this transaction was confirmed, if present.")]
        [field: InspectorName(@"ConfirmedRound")]
        internal ulong ConfirmedRound { get; set; }
#else
        public ulong? ConfirmedRound { get; set; }
#endif

        /// <summary>Indicates that the transaction was kicked out of this node's transaction pool (and specifies why that happened).  An empty string indicates the transaction wasn't kicked out of this node's txpool due to an error.
        /// <br/></summary>
        [JsonProperty("pool-error", Required = Required.Always)]
        [MessagePack.Key("pool-error")]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"Indicates that the transaction was kicked out of this node's transaction pool (and specifies why that happened).  An empty string indicates the transaction wasn't kicked out of this node's txpool due to an error.
")]
        [field: InspectorName(@"PoolError")]
        internal string PoolError { get; set; }
#else
        public string PoolError { get; set; }
#endif


        /// <summary>Rewards in microalgos applied to the receiver account.</summary>
        [JsonProperty("receiver-rewards", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("receiver-rewards")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"Rewards in microalgos applied to the receiver account.")]
        [field: InspectorName(@"ReceiverRewards")]
        internal ulong ReceiverRewards { get; set; }
#else
        public ulong? ReceiverRewards { get; set; }
#endif

        /// <summary>Rewards in microalgos applied to the sender account.</summary>
        [JsonProperty("sender-rewards", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("sender-rewards")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"Rewards in microalgos applied to the sender account.")]
        [field: InspectorName(@"SenderRewards")]
        internal ulong SenderRewards { get; set; }
#else
        public ulong? SenderRewards { get; set; }
#endif

        /// <summary>Rewards in microalgos applied to the close remainder to account.</summary>
        [JsonProperty("close-rewards", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("close-rewards")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"Rewards in microalgos applied to the close remainder to account.")]
        [field: InspectorName(@"CloseRewards")]
        internal ulong CloseRewards { get; set; }
#else
        public ulong? CloseRewards { get; set; }
#endif

        /// <summary>\[ld\] Local state key/value changes for the application being executed by this transaction.</summary>

        [JsonProperty("local-state-delta", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("local-state-delta")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"\[ld\] Local state key/value changes for the application being executed by this transaction.")]
        [field: InspectorName(@"LocalStateDelta")]
        internal System.Collections.Generic.List<AccountStateDelta> LocalStateDelta { get; set; } = new System.Collections.Generic.List<AccountStateDelta>();
#else
        public System.Collections.Generic.ICollection<AccountStateDelta> LocalStateDelta { get; set; } = new System.Collections.ObjectModel.Collection<AccountStateDelta>();
#endif

        /// <summary>\[gd\] Global state key/value changes for the application being executed by this transaction.</summary>
        [JsonProperty("global-state-delta", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        //[MessagePack.Key("global-state-delta")]
        [IgnoreMember]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"\[gd\] Global state key/value changes for the application being executed by this transaction.")]
        [field: InspectorName(@"GlobalStateDelta")]
#endif
        public StateDelta GlobalStateDelta { get; set; }


        [JsonProperty("logs", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("logs")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"\[lg\] Logs for the application being executed by this transaction.")]
        [field: InspectorName(@"Logs")]
        internal System.Collections.Generic.List<byte[]> Logs { get; set; } = new System.Collections.Generic.List<byte[]>();
#else
        public System.Collections.Generic.ICollection<byte[]> Logs { get; set; } = new System.Collections.ObjectModel.Collection<byte[]>();
#endif


        /// <summary>Inner transactions produced by application execution.</summary>
        [JsonProperty("inner-txns", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("inner-txns")]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"Inner transactions produced by application execution.")]
        [field: InspectorName(@"InnerTxns")]
        internal System.Collections.Generic.List<IReturnableTransaction> InnerTxns { get; set; } = new System.Collections.Generic.List<IReturnableTransaction>();
#else
        public System.Collections.Generic.ICollection<IReturnableTransaction> InnerTxns { get; set; } = new System.Collections.ObjectModel.Collection<IReturnableTransaction>();
#endif


        /// <summary>The round where this transaction was confirmed, if present.</summary>
        [JsonProperty("application-index", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("application-index")]
        [DefaultValue(0)]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"The application index if the transaction was found and it created an application.")]
        [field: InspectorName(@"ApplicationIndex")]
        internal ulong ApplicationIndex { get; set; }
#else
        public ulong? ApplicationIndex { get; set; }
#endif

        /// <summary>The number of the asset's unit that were transferred to the close-to address..</summary>
        [JsonProperty("asset-index", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("asset-index")]
        [DefaultValue(0)]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"The asset index if the transaction was found and it created an asset.")]
        [field: InspectorName(@"AssetIndex")]
        internal ulong AssetIndex { get; set; }
#else
        public ulong? AssetIndex { get; set; }
#endif



        /// <summary>The number of the asset's unit that were transferred to the close-to address..</summary>
        [JsonProperty("asset-closing-amount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [MessagePack.Key("asset-closing-amount")]
        [DefaultValue(0)]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"The number of the asset's unit that were transferred to the close-to address.")]
        [field: InspectorName(@"AssetClosingAmount")]
        internal ulong AssetClosingAmount { get; set; }
#else
        public ulong? AssetClosingAmount { get; set; }
#endif




    }
}
