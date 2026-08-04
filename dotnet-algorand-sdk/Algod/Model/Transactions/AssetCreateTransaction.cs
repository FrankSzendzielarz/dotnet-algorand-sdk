using MessagePack;
using Newtonsoft.Json;
#if UNITY
using UnityEngine;
#endif

namespace Algorand.Algod.Model.Transactions
{
    [MessagePackObject(AllowPrivate = true)]
    public partial class AssetCreateTransaction : AssetConfigurationTransaction
    {
#if UNITY
        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"ConfirmedRound")]
        [JsonIgnore]
        [IgnoreMember]
        public ulong AssetIndex { get; internal set; }
#else
        [JsonIgnore]
        [IgnoreMember]
        public ulong? AssetIndex { get; internal set; }
#endif
    }
}
