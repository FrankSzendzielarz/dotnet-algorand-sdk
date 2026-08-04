#nullable enable annotations
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
    public partial class Account
    {
        /// <summary>
        /// The account public address. When rekeyed, this is the original account address, and signing is done using the private key in the address.
        /// </summary>
        protected Address _address;
        /// <summary>
        /// The account public address. When rekeyed, this is the original account address, and signing is done using the private key in the address.
        /// </summary>
        protected Address? _rekeyedTo;


        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("address")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"the account public key")]
    [field:InspectorName(@"Address")]
    public Address Address {get;set;}
#else
        public Address Address
        {
            get { return _rekeyedTo ?? _address; }
            set { _address = value; }
        }
#endif
        [Newtonsoft.Json.JsonIgnore]
        [MessagePack.IgnoreMember]
        public Address? RekeyedTo
        {
            get { return _rekeyedTo; }
            set { _rekeyedTo = value; }
        }
        [Newtonsoft.Json.JsonIgnore]
        [MessagePack.IgnoreMember]
        public Address? OriginalAddress
        {
            get { return _address; }
        }

        [Newtonsoft.Json.JsonProperty("auth-addr", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("auth-addr")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[spend\] the address against which signing should be checked. If empty, the address of the current account is used. This field can be updated in any transaction by setting the RekeyTo field.")]
    [field:InspectorName(@"AuthAddr")]
    public Address AuthAddr {get;set;}
#else
        public Address AuthAddr
        {
            get { return _rekeyedTo; }
            set { _rekeyedTo = value; }
        }
#endif

        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("amount")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[algo\] total number of MicroAlgos in the account")]
    [field:InspectorName(@"Amount")]
    public ulong Amount {get;set;}
#else
        public ulong Amount { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("amount-without-pending-rewards", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("amount-without-pending-rewards")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"specifies the amount of MicroAlgos in the account, without the pending rewards.")]
    [field:InspectorName(@"AmountWithoutPendingRewards")]
    public ulong AmountWithoutPendingRewards {get;set;}
#else
        public ulong AmountWithoutPendingRewards { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apps-local-state", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apps-local-state")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appl\] applications local data stored in this account.

Note the raw object uses `map[int] -> AppLocalState` for this type.")]
    [field:InspectorName(@"AppsLocalState")]
    public System.Collections.Generic.List<ApplicationLocalState> AppsLocalState {get;set;} = new System.Collections.Generic.List<ApplicationLocalState>();
#else
        public System.Collections.Generic.ICollection<ApplicationLocalState> AppsLocalState { get; set; } = new System.Collections.ObjectModel.Collection<ApplicationLocalState>();
#endif

        [Newtonsoft.Json.JsonProperty("apps-total-extra-pages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apps-total-extra-pages")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[teap\] the sum of all extra application program pages for this account.")]
    [field:InspectorName(@"AppsTotalExtraPages")]
    public ulong AppsTotalExtraPages {get;set;}
#else
        public ulong? AppsTotalExtraPages { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("apps-total-schema", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("apps-total-schema")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[tsch\] stores the sum of all of the local schemas and global schemas in this account.

Note: the raw account uses `StateSchema` for this type.")]
    [field:InspectorName(@"AppsTotalSchema")]
    public ApplicationStateSchema AppsTotalSchema {get;set;}
#else
        public ApplicationStateSchema AppsTotalSchema { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("assets", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("assets")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[asset\] assets held by this account.

Note the raw object uses `map[int] -> AssetHolding` for this type.")]
    [field:InspectorName(@"Assets")]
    public System.Collections.Generic.List<AssetHolding> Assets {get;set;} = new System.Collections.Generic.List<AssetHolding>();
#else
        public System.Collections.Generic.ICollection<AssetHolding> Assets { get; set; } = new System.Collections.ObjectModel.Collection<AssetHolding>();
#endif




        [Newtonsoft.Json.JsonProperty("created-apps", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("created-apps")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[appp\] parameters of applications created by this account including app global data.

Note: the raw account uses `map[int] -> AppParams` for this type.")]
    [field:InspectorName(@"CreatedApps")]
    public System.Collections.Generic.List<Application> CreatedApps {get;set;} = new System.Collections.Generic.List<Application>();
#else
        public System.Collections.Generic.ICollection<Application> CreatedApps { get; set; } = new System.Collections.ObjectModel.Collection<Application>();
#endif

        [Newtonsoft.Json.JsonProperty("created-assets", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("created-assets")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[apar\] parameters of assets created by this account.

Note: the raw account uses `map[int] -> Asset` for this type.")]
    [field:InspectorName(@"CreatedAssets")]
    public System.Collections.Generic.List<Asset> CreatedAssets {get;set;} = new System.Collections.Generic.List<Asset>();
#else
        public System.Collections.Generic.ICollection<Asset> CreatedAssets { get; set; } = new System.Collections.ObjectModel.Collection<Asset>();
#endif

        [Newtonsoft.Json.JsonProperty("incentive-eligible", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("incentive-eligible")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Whether or not the account can receive block incentives if its balance is in range at proposal time.")]
    [field:InspectorName(@"IncentiveEligible")]
    public bool IncentiveEligible {get;set;}
#else
        public bool? IncentiveEligible { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("last-heartbeat", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("last-heartbeat")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round in which this account last went online, or explicitly renewed their online status.")]
    [field:InspectorName(@"LastHeartbeat")]
    public ulong LastHeartbeat {get;set;}
#else
        public ulong? LastHeartbeat { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("last-proposed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("last-proposed")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round in which this account last proposed the block.")]
    [field:InspectorName(@"LastProposed")]
    public ulong LastProposed {get;set;}
#else
        public ulong? LastProposed { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("min-balance", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("min-balance")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"MicroAlgo balance required by the account.

The requirement grows based on asset and application usage.")]
    [field:InspectorName(@"MinBalance")]
    public ulong MinBalance {get;set;}
#else
        public ulong MinBalance { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("participation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("participation")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"AccountParticipation describes the parameters used by this account in consensus protocol.")]
    [field:InspectorName(@"Participation")]
    public AccountParticipation Participation {get;set;}
#else
        public AccountParticipation Participation { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("pending-rewards", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("pending-rewards")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"amount of MicroAlgos of pending rewards in this account.")]
    [field:InspectorName(@"PendingRewards")]
    public ulong PendingRewards {get;set;}
#else
        public ulong PendingRewards { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("reward-base", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("reward-base")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[ebase\] used as part of the rewards computation. Only applicable to accounts which are participating.")]
    [field:InspectorName(@"RewardBase")]
    public ulong RewardBase {get;set;}
#else
        public ulong? RewardBase { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("rewards", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("rewards")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[ern\] total rewards of MicroAlgos the account has received, including pending rewards.")]
    [field:InspectorName(@"Rewards")]
    public ulong Rewards {get;set;}
#else
        public ulong Rewards { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("round", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("round")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The round for which this information is relevant.")]
    [field:InspectorName(@"Round")]
    public ulong Round {get;set;}
#else
        public ulong Round { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("status")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[onl\] delegation status of the account's MicroAlgos
* Offline - indicates that the associated account is delegated.
*  Online  - indicates that the associated account used as part of the delegation pool.
*   NotParticipating - indicates that the associated account is neither a delegator nor a delegate.")]
    [field:InspectorName(@"Status")]
    public string Status {get;set;}
#else
        public string Status { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-apps-opted-in", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("total-apps-opted-in")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The count of all applications that have been opted in, equivalent to the count of application local data (AppLocalState objects) stored in this account.")]
    [field:InspectorName(@"TotalAppsOptedIn")]
    public ulong TotalAppsOptedIn {get;set;}
#else
        public ulong TotalAppsOptedIn { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-assets-opted-in", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("total-assets-opted-in")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The count of all assets that have been opted in, equivalent to the count of AssetHolding objects held by this account.")]
    [field:InspectorName(@"TotalAssetsOptedIn")]
    public ulong TotalAssetsOptedIn {get;set;}
#else
        public ulong TotalAssetsOptedIn { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-box-bytes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("total-box-bytes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[tbxb\] The total number of bytes used by this account's app's box keys and values.")]
    [field:InspectorName(@"TotalBoxBytes")]
    public ulong TotalBoxBytes {get;set;}
#else
        public ulong? TotalBoxBytes { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-boxes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("total-boxes")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[tbx\] The number of existing boxes created by this account's app.")]
    [field:InspectorName(@"TotalBoxes")]
    public ulong TotalBoxes {get;set;}
#else
        public ulong? TotalBoxes { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-created-apps", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("total-created-apps")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The count of all apps (AppParams objects) created by this account.")]
    [field:InspectorName(@"TotalCreatedApps")]
    public ulong TotalCreatedApps {get;set;}
#else
        public ulong TotalCreatedApps { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("total-created-assets", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("total-created-assets")]
        [System.ComponentModel.DataAnnotations.Required]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The count of all assets (AssetParams objects) created by this account.")]
    [field:InspectorName(@"TotalCreatedAssets")]
    public ulong TotalCreatedAssets {get;set;}
#else
        public ulong TotalCreatedAssets { get; set; }
#endif
    }
}
