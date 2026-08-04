
using Algorand.Algod.Model.Converters.MsgPack;
using JsonSubTypes;
using MessagePack;
using Newtonsoft.Json;
#if UNITY
using UnityEngine;
#endif

namespace Algorand.Algod.Model.Transactions
{

    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(AssetClawbackTransaction), "asnd")]
    [JsonSubtypes.FallBackSubType(typeof(AssetTransferTransaction))]
    [MessagePackObject]
    [MessagePackFormatter(typeof(NoDefaultsFormatter<AssetTransferTransaction>))]

    public partial class AssetTransferTransaction : AssetMovementsTransaction
    {
#if UNITY
        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"AssetClosingAmount")]
        [JsonIgnore]
        [IgnoreMember]
        public ulong AssetClosingAmount { get; internal set; }
#else
        [JsonIgnore]
        [IgnoreMember]
        public ulong? AssetClosingAmount { get; internal set; }
#endif
    }
}
