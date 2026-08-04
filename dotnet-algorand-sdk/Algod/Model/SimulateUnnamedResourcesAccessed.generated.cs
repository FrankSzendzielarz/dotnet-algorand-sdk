
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
    [MessagePack.MessagePackObject]
    public partial class SimulateUnnamedResourcesAccessed
    {

        [Newtonsoft.Json.JsonProperty("accounts", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("accounts")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The unnamed accounts that were referenced. The order of this array is arbitrary.")]
    [field:InspectorName(@"Accounts")]
    public System.Collections.Generic.List<Address> Accounts {get;set;} = new System.Collections.Generic.List<Address>();
#else
        public System.Collections.Generic.ICollection<Address> Accounts { get; set; } = new System.Collections.ObjectModel.Collection<Address>();
#endif

        [Newtonsoft.Json.JsonProperty("app-locals", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-locals")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The unnamed application local states that were referenced. The order of this array is arbitrary.")]
    [field:InspectorName(@"AppLocals")]
    public System.Collections.Generic.List<ApplicationLocalReference> AppLocals {get;set;} = new System.Collections.Generic.List<ApplicationLocalReference>();
#else
        public System.Collections.Generic.ICollection<ApplicationLocalReference> AppLocals { get; set; } = new System.Collections.ObjectModel.Collection<ApplicationLocalReference>();
#endif

        [Newtonsoft.Json.JsonProperty("apps", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The unnamed applications that were referenced. The order of this array is arbitrary.")]
    [field:InspectorName(@"Apps")]
    public System.Collections.Generic.List<ulong> Apps {get;set;} = new System.Collections.Generic.List<ulong>();
#else
        public System.Collections.Generic.ICollection<ulong> Apps { get; set; } = new System.Collections.ObjectModel.Collection<ulong>();
#endif

        [Newtonsoft.Json.JsonProperty("asset-holdings", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("asset-holdings")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The unnamed asset holdings that were referenced. The order of this array is arbitrary.")]
    [field:InspectorName(@"AssetHoldings")]
    public System.Collections.Generic.List<AssetHoldingReference> AssetHoldings {get;set;} = new System.Collections.Generic.List<AssetHoldingReference>();
#else
        public System.Collections.Generic.ICollection<AssetHoldingReference> AssetHoldings { get; set; } = new System.Collections.ObjectModel.Collection<AssetHoldingReference>();
#endif

        [Newtonsoft.Json.JsonProperty("assets", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("assets")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The unnamed assets that were referenced. The order of this array is arbitrary.")]
    [field:InspectorName(@"Assets")]
    public System.Collections.Generic.List<ulong> Assets {get;set;} = new System.Collections.Generic.List<ulong>();
#else
        public System.Collections.Generic.ICollection<ulong> Assets { get; set; } = new System.Collections.ObjectModel.Collection<ulong>();
#endif

        [Newtonsoft.Json.JsonProperty("boxes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("boxes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The unnamed boxes that were referenced. The order of this array is arbitrary.")]
    [field:InspectorName(@"Boxes")]
    public System.Collections.Generic.List<BoxReference> Boxes {get;set;} = new System.Collections.Generic.List<BoxReference>();
#else
        public System.Collections.Generic.ICollection<BoxReference> Boxes { get; set; } = new System.Collections.ObjectModel.Collection<BoxReference>();
#endif

        [Newtonsoft.Json.JsonProperty("extra-box-refs", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("extra-box-refs")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The number of extra box references used to increase the IO budget. This is in addition to the references defined in the input transaction group and any referenced to unnamed boxes.")]
    [field:InspectorName(@"ExtraBoxRefs")]
    public ulong ExtraBoxRefs {get;set;}
#else
        public ulong? ExtraBoxRefs { get; set; }
#endif



    }


}
