namespace Algorand.Algod.Model
{
    using MessagePack;
    using System.ComponentModel;
    using System = global::System;
#if UNITY
using UnityEngine;
#endif
    //TODO - modify codegen for this 

    /// <summary>AssetParams specifies the parameters for an asset.
    /// <br/>
    /// <br/>\[apar\] when part of an AssetConfig transaction.
    /// <br/>
    /// <br/>Definition:
    /// <br/>data/transactions/asset.go : AssetParams</summary>
#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject]
    public partial class AssetParams
    {

        [Newtonsoft.Json.JsonProperty("c", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("c")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The clawback address")]
    [field:InspectorName(@"Clawback")]  
#endif
        public Address Clawback { get; set; }

        /// <summary>\[c\] Address of account used to clawback holdings of this asset.  If empty, clawback is not permitted.</summary>
        [Newtonsoft.Json.JsonProperty("clawback", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private Address clawback { set { Clawback = value; } }


        [Newtonsoft.Json.JsonIgnore]
        [IgnoreMember]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The address that created this asset")]
    [field:InspectorName(@"Creator")]  
#endif
        public Address Creator { get; set; }
        /// <summary>The address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.</summary>
        [Newtonsoft.Json.JsonProperty("creator", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private Address creator { set { Creator = value; } }

        /// <summary>\[dc\] The number of digits to use after the decimal point when displaying this asset. If 0, the asset is not divisible. If 1, the base unit of the asset is in tenths. If 2, the base unit of the asset is in hundredths, and so on. This value must be between 0 and 19 (inclusive).</summary>
        [Newtonsoft.Json.JsonProperty("dc", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Range(0, 19)]
        [MessagePack.Key("dc")]

#if UNITY
    [field:SerializeField]
    [Tooltip(@"Decimals")]
    [field:InspectorName(@"Decimals")]  
#endif
        public ulong Decimals { get; set; }
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty("decimals", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private ulong decimals { set { Decimals = value; } }



        /// <summary>\[df\] Whether holdings of this asset are frozen by default.</summary>
        [Newtonsoft.Json.JsonProperty("df", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("df")]
        [DefaultValue(false)]
#if UNITY
        [field:SerializeField]
        [Tooltip(@"DefaultFrozen")]
        [field:InspectorName(@"DefaultFrozen")]  
        public bool DefaultFrozen { get; set; }
        /// <summary>\[df\] Whether holdings of this asset are frozen by default.</summary>
        [Newtonsoft.Json.JsonProperty("default-frozen", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private bool defaultFrozen { set { DefaultFrozen = value; } }
#else
        public bool? DefaultFrozen { get; set; }
        /// <summary>\[df\] Whether holdings of this asset are frozen by default.</summary>
        [Newtonsoft.Json.JsonProperty("default-frozen", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private bool? defaultFrozen { set { DefaultFrozen = value; } }
#endif


        /// <summary>\[f\] Address of account used to freeze holdings of this asset.  If empty, freezing is not permitted.</summary>
        [Newtonsoft.Json.JsonProperty("f", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("f")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of account used to freeze holdings of this asset. ")]
    [field:InspectorName(@"Freeze")]  
#endif
        public Address Freeze { get; set; }
        /// <summary>\[f\] Address of account used to freeze holdings of this asset.  If empty, freezing is not permitted.</summary>
        [Newtonsoft.Json.JsonProperty("freeze", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private Address freeze { set { Freeze = value; } }

        /// <summary>\[m\] Address of account used to manage the keys of this asset and to destroy it.</summary>
        [Newtonsoft.Json.JsonProperty("m", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("m")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of account used to manage the keys of this asset and to destroy it. ")]
    [field:InspectorName(@"Manager")]  
#endif
        public Address Manager { get; set; }
        /// <summary>\[m\] Address of account used to manage the keys of this asset and to destroy it.</summary>
        [Newtonsoft.Json.JsonProperty("manager", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private Address manager { set { Manager = value; } }

        /// <summary>\[am\] A commitment to some unspecified asset metadata. The format of this metadata is up to the application.</summary>
        [Newtonsoft.Json.JsonProperty("am", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("am")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"A commitment to some unspecified asset metadata.. ")]
    [field:InspectorName(@"MetadataHash")]  
#endif
        public byte[] MetadataHash { get; set; }
        /// <summary>\[am\] A commitment to some unspecified asset metadata. The format of this metadata is up to the application.</summary>
        [Newtonsoft.Json.JsonProperty("metadata-hash", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private byte[] metadataHash { set { MetadataHash = value; } }

        /// <summary>\[an\] Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.</summary>
        [Newtonsoft.Json.JsonProperty("an", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("an")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Name of this asset, as supplied by the creator. ")]
    [field:InspectorName(@"Name")]  
#endif
        public string Name { get; set; }
        /// <summary>\[an\] Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.</summary>
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private string name { set { Name = value; } }

        [Newtonsoft.Json.JsonIgnore]
        [IgnoreMember]
        public byte[] NameB64 { get; set; }
        /// <summary>Base64 encoded name of this asset, as supplied by the creator.</summary>
        [Newtonsoft.Json.JsonProperty("name-b64", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private byte[] nameB64 { set { NameB64 = value; } }

        /// <summary>\[r\] Address of account holding reserve (non-minted) units of this asset.</summary>
        [Newtonsoft.Json.JsonProperty("r", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("r")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Address of account holding reserve (non-minted) units of this asset. ")]
    [field:InspectorName(@"Reserve")]  
#endif
        public Address Reserve { get; set; }
        /// <summary>\[r\] Address of account holding reserve (non-minted) units of this asset.</summary>
        [Newtonsoft.Json.JsonProperty("reserve", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private Address reserve { set { Reserve = value; } }

        /// <summary>\[t\] The total number of units of this asset.</summary>
        [Newtonsoft.Json.JsonProperty("t", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("t")]
        [DefaultValue(0)]
#if UNITY
        [field:SerializeField]
        [Tooltip(@"Total amount. ")]
        [field:InspectorName(@"Total")]  
        public ulong Total { get;  set; }
        /// <summary>\[t\] The total number of units of this asset.</summary>
        [Newtonsoft.Json.JsonProperty("total", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private ulong total { set { Total = value; } }
#else
        public ulong? Total { get; set; }
        /// <summary>\[t\] The total number of units of this asset.</summary>
        [Newtonsoft.Json.JsonProperty("total", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private ulong? total { set { Total = value; } }
#endif



        /// <summary>\[un\] Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.</summary>
        [Newtonsoft.Json.JsonProperty("un", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("un")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Name of a unit of this asset. ")]
    [field:InspectorName(@"UnitName")]  
#endif
        public string UnitName { get; set; }
        /// <summary>\[un\] Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.</summary>
        [Newtonsoft.Json.JsonProperty("unit-name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private string unitName { set { UnitName = value; } }


        /// <summary>Base64 encoded name of a unit of this asset, as supplied by the creator.</summary>
        [Newtonsoft.Json.JsonIgnore]
        [IgnoreMember]
        public byte[] UnitNameB64 { get; set; }

        /// <summary>Base64 encoded name of a unit of this asset, as supplied by the creator.</summary>
        [Newtonsoft.Json.JsonProperty("unit-name-b64", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private byte[] unitNameB64 { set { UnitNameB64 = value; } }

        /// <summary>\[au\] URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.</summary>
        [Newtonsoft.Json.JsonProperty("au", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("au")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters ")]
    [field:InspectorName(@"Url")]  
#endif
        public string Url { get; set; }
        /// <summary>\[au\] URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.</summary>
        [Newtonsoft.Json.JsonProperty("url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private string url { set { Url = value; } }


        /// <summary>Base64 encoded URL where more information about the asset can be retrieved.</summary>
        [Newtonsoft.Json.JsonIgnore]
        [IgnoreMember]
        public byte[] UrlB64 { get; set; }
        /// <summary>Base64 encoded URL where more information about the asset can be retrieved.</summary>
        [Newtonsoft.Json.JsonProperty("url-b64", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        private byte[] urlB64 { set { UrlB64 = value; } }


    }
}
