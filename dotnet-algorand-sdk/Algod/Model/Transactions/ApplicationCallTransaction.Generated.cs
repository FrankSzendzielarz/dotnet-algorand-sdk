
namespace Algorand.Algod.Model.Transactions
{

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    public partial class ApplicationCallTransaction : Transaction
    {

        [Newtonsoft.Json.JsonProperty("apaa", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apaa")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ApplicationArgs")]
    public System.Collections.Generic.List<byte[]> ApplicationArgs {get;set;} = new System.Collections.Generic.List<byte[]>();
#else
        public System.Collections.Generic.ICollection<byte[]> ApplicationArgs { get; set; } = new System.Collections.ObjectModel.Collection<byte[]>();
#endif

        [Newtonsoft.Json.JsonProperty("apas", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apas")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ForeignAssets")]
    public System.Collections.Generic.List<ulong> ForeignAssets {get;set;} = new System.Collections.Generic.List<ulong>();
#else
        public System.Collections.Generic.ICollection<ulong> ForeignAssets { get; set; } = new System.Collections.ObjectModel.Collection<ulong>();
#endif

        [Newtonsoft.Json.JsonProperty("apat", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apat")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Accounts")]
    public System.Collections.Generic.List<Algorand.Address> Accounts {get;set;} = new System.Collections.Generic.List<Algorand.Address>();
#else
        public System.Collections.Generic.ICollection<Algorand.Address> Accounts { get; set; } = new System.Collections.ObjectModel.Collection<Algorand.Address>();
#endif

        [Newtonsoft.Json.JsonProperty("apfa", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apfa")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"ForeignApps")]
    public System.Collections.Generic.List<ulong> ForeignApps {get;set;} = new System.Collections.Generic.List<ulong>();
#else
        public System.Collections.Generic.ICollection<ulong> ForeignApps { get; set; } = new System.Collections.ObjectModel.Collection<ulong>();
#endif

    }


}
