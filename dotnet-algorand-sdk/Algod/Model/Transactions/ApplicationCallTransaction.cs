using JsonSubTypes;
using MessagePack;
using Newtonsoft.Json;
using System.Collections.Generic;
#if UNITY
using UnityEngine;
#endif

namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes), "apan")]
    [JsonSubtypes.KnownSubType(typeof(ApplicationClearStateTransaction), OnCompletion.Clear)]
    [JsonSubtypes.KnownSubType(typeof(ApplicationOptInTransaction), OnCompletion.Optin)]
    [JsonSubtypes.KnownSubType(typeof(ApplicationUpdateTransaction), OnCompletion.Update)]
    [JsonSubtypes.KnownSubType(typeof(ApplicationCloseOutTransaction), OnCompletion.Closeout)]
    [JsonSubtypes.KnownSubType(typeof(ApplicationDeleteTransaction), OnCompletion.Delete)]
    [JsonSubtypes.FallBackSubType(typeof(ApplicationNoopTransaction))]

    [MessagePack.MessagePackObject]
    [Union(0, typeof(ApplicationClearStateTransaction))]
    [Union(1, typeof(ApplicationOptInTransaction))]
    [Union(2, typeof(ApplicationUpdateTransaction))]
    [Union(3, typeof(ApplicationCloseOutTransaction))]
    [Union(4, typeof(ApplicationDeleteTransaction))]
    [Union(5, typeof(ApplicationNoopTransaction))]
    public abstract partial class ApplicationCallTransaction : Transaction
    {
        [JsonProperty(PropertyName = "type")]
        [MessagePack.Key("type")]
        public string type => "appl";

        public bool ShouldSerializeAccounts() { return Accounts?.Count > 0; }
        public bool ShouldSerializeApplicationArgs() { return ApplicationArgs?.Count > 0; }
        public bool ShouldSerializeForeignApps() { return ForeignApps?.Count > 0; }
        public bool ShouldSerializeForeignAssets() { return ForeignAssets?.Count > 0; }

        public bool ShouldSerializeBoxes() { return Boxes?.Count > 0; }


        [JsonIgnore]
        [IgnoreMember]
        public ICollection<byte[]> Logs { get; internal set; }

        [JsonIgnore]
        [IgnoreMember]
        public StateDelta GlobalStateDelta { get; internal set; }
        [JsonIgnore]
        [IgnoreMember]
        public ICollection<AccountStateDelta> LocalStateDelta { get; internal set; }

        //TEMP: Modify the JSON in the Api-generator folder instead

        [Newtonsoft.Json.JsonProperty("apbx", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apbx")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Boxes")]
    public System.Collections.Generic.List<BoxRef> Boxes {get;set;} = new System.Collections.Generic.List<BoxRef>();
#else
        public System.Collections.Generic.ICollection<BoxRef> Boxes { get; set; } = new System.Collections.ObjectModel.Collection<BoxRef>();
#endif




    }


}
