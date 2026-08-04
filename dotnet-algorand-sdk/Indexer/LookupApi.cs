









namespace Algorand.Indexer
{
    using Algorand.Indexer.Model;
    using System = global::System;
    using Newtonsoft.Msgpack;


    public partial interface ILookupApi
    {
       /// <summary>Lookup account information.
       /// </summary>
       /// <param name="exclude">Exclude additional items such as asset holdings, application local data stored
/// for this account, asset parameters created by this account, and application
/// parameters created by this account.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<AccountResponse> lookupAccountByIDAsync(string accountId, string? exclude=null, bool? includeAll=null, ulong? round=null);

       /// <param name="exclude">Exclude additional items such as asset holdings, application local data stored
/// for this account, asset parameters created by this account, and application
/// parameters created by this account.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<AccountResponse> lookupAccountByIDAsync(System.Threading.CancellationToken cancellationToken,string accountId,string? exclude=null,bool? includeAll=null,ulong? round=null);

       /// <summary>Lookup an account's asset holdings, optionally for a specific ID.
       /// </summary>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<AssetHoldingsResponse> lookupAccountAssetsAsync(string accountId, ulong? assetId=null, bool? includeAll=null, ulong? limit=null, string? next=null);

       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<AssetHoldingsResponse> lookupAccountAssetsAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? assetId=null,bool? includeAll=null,ulong? limit=null,string? next=null);

       /// <summary>Lookup an account's created asset parameters, optionally for a specific ID.
       /// </summary>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<AssetsResponse> lookupAccountCreatedAssetsAsync(string accountId, ulong? assetId=null, bool? includeAll=null, ulong? limit=null, string? next=null);

       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<AssetsResponse> lookupAccountCreatedAssetsAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? assetId=null,bool? includeAll=null,ulong? limit=null,string? next=null);

       /// <summary>Lookup an account's asset holdings, optionally for a specific ID.
       /// </summary>
       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<ApplicationLocalStatesResponse> lookupAccountAppLocalStatesAsync(string accountId, ulong? applicationId=null, bool? includeAll=null, ulong? limit=null, string? next=null);

       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<ApplicationLocalStatesResponse> lookupAccountAppLocalStatesAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? applicationId=null,bool? includeAll=null,ulong? limit=null,string? next=null);

       /// <summary>Lookup an account's created application parameters, optionally for a specific
/// ID.
       /// </summary>
       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<ApplicationsResponse> lookupAccountCreatedApplicationsAsync(string accountId, ulong? applicationId=null, bool? includeAll=null, ulong? limit=null, string? next=null);

       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<ApplicationsResponse> lookupAccountCreatedApplicationsAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? applicationId=null,bool? includeAll=null,ulong? limit=null,string? next=null);

       /// <summary>Lookup account transactions. Transactions are returned newest to oldest.
       /// </summary>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<TransactionsResponse> lookupAccountTransactionsAsync(string accountId, string? afterTime=null, ulong? assetId=null, string? beforeTime=null, ulong? currencyGreaterThan=null, ulong? currencyLessThan=null, ulong? limit=null, ulong? maxRound=null, ulong? minRound=null, string? next=null, string? notePrefix=null, bool? rekeyTo=null, ulong? round=null, string? sigType=null, string? txType=null, string? txid=null);

       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<TransactionsResponse> lookupAccountTransactionsAsync(System.Threading.CancellationToken cancellationToken,string accountId,string? afterTime=null,ulong? assetId=null,string? beforeTime=null,ulong? currencyGreaterThan=null,ulong? currencyLessThan=null,ulong? limit=null,ulong? maxRound=null,ulong? minRound=null,string? next=null,string? notePrefix=null,bool? rekeyTo=null,ulong? round=null,string? sigType=null,string? txType=null,string? txid=null);

       /// <summary>Lookup application.
       /// </summary>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="application-id"></param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<ApplicationResponse> lookupApplicationByIDAsync(ulong applicationId, bool? includeAll=null);

       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="application-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<ApplicationResponse> lookupApplicationByIDAsync(System.Threading.CancellationToken cancellationToken,ulong applicationId,bool? includeAll=null);

       /// <summary>Lookup application logs.
       /// </summary>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="sender-address">Only include transactions with this sender address.</param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="application-id"></param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<ApplicationLogsResponse> lookupApplicationLogsByIDAsync(ulong applicationId, ulong? limit=null, ulong? maxRound=null, ulong? minRound=null, string? next=null, Address? senderAddress=null, string? txid=null);

       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="sender-address">Only include transactions with this sender address.</param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="application-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<ApplicationLogsResponse> lookupApplicationLogsByIDAsync(System.Threading.CancellationToken cancellationToken,ulong applicationId,ulong? limit=null,ulong? maxRound=null,ulong? minRound=null,string? next=null,Address? senderAddress=null,string? txid=null);

       /// <summary>Lookup asset information.
       /// </summary>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="asset-id"></param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<AssetResponse> lookupAssetByIDAsync(ulong assetId, bool? includeAll=null);

       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="asset-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<AssetResponse> lookupAssetByIDAsync(System.Threading.CancellationToken cancellationToken,ulong assetId,bool? includeAll=null);

       /// <summary>Lookup the list of accounts who hold this asset
       /// </summary>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="asset-id"></param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<AssetBalancesResponse> lookupAssetBalancesAsync(ulong assetId, ulong? currencyGreaterThan=null, ulong? currencyLessThan=null, bool? includeAll=null, ulong? limit=null, string? next=null);

       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="asset-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<AssetBalancesResponse> lookupAssetBalancesAsync(System.Threading.CancellationToken cancellationToken,ulong assetId,ulong? currencyGreaterThan=null,ulong? currencyLessThan=null,bool? includeAll=null,ulong? limit=null,string? next=null);

       /// <summary>Lookup transactions for an asset. Transactions are returned oldest to newest.
       /// </summary>
       /// <param name="address">Only include transactions with this address in one of the transaction fields.</param>
       /// <param name="address-role">Combine with the address parameter to define what type of address to search for.</param>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="exclude-close-to">Combine with address and address-role parameters to define what type of address
/// to search for. The close to fields are normally treated as a receiver, if you
/// would like to exclude them set this parameter to true.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="asset-id"></param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<TransactionsResponse> lookupAssetTransactionsAsync(ulong assetId, Address? address=null, string? addressRole=null, string? afterTime=null, string? beforeTime=null, ulong? currencyGreaterThan=null, ulong? currencyLessThan=null, bool? excludeCloseTo=null, ulong? limit=null, ulong? maxRound=null, ulong? minRound=null, string? next=null, string? notePrefix=null, bool? rekeyTo=null, ulong? round=null, string? sigType=null, string? txType=null, string? txid=null);

       /// <param name="address">Only include transactions with this address in one of the transaction fields.</param>
       /// <param name="address-role">Combine with the address parameter to define what type of address to search for.</param>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="exclude-close-to">Combine with address and address-role parameters to define what type of address
/// to search for. The close to fields are normally treated as a receiver, if you
/// would like to exclude them set this parameter to true.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="asset-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<TransactionsResponse> lookupAssetTransactionsAsync(System.Threading.CancellationToken cancellationToken,ulong assetId,Address? address=null,string? addressRole=null,string? afterTime=null,string? beforeTime=null,ulong? currencyGreaterThan=null,ulong? currencyLessThan=null,bool? excludeCloseTo=null,ulong? limit=null,ulong? maxRound=null,ulong? minRound=null,string? next=null,string? notePrefix=null,bool? rekeyTo=null,ulong? round=null,string? sigType=null,string? txType=null,string? txid=null);

       /// <summary>Lookup block.
       /// </summary>
       /// <param name="round-number">Round number</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<Block> lookupBlockAsync(ulong roundNumber);

       /// <param name="round-number">Round number</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<Block> lookupBlockAsync(System.Threading.CancellationToken cancellationToken,ulong roundNumber);

       /// <summary>Lookup a single transaction.
       /// </summary>
       /// <param name="txid"></param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<TransactionResponse> lookupTransactionAsync(string txid);

       /// <param name="txid"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<TransactionResponse> lookupTransactionAsync(System.Threading.CancellationToken cancellationToken,string txid);

    }

    public partial class LookupApi : ILookupApi
    {
       private System.Net.Http.HttpClient _httpClient;
       private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

       public LookupApi(System.Net.Http.HttpClient httpClient)
       {
              _httpClient = httpClient;
              _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
       }
       
       private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
       {
              var settings = new Newtonsoft.Json.JsonSerializerSettings();
              UpdateJsonSerializerSettings(settings);
              return settings;
       }  

       protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

       partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
       partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
       partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
       partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

       

       /// <summary>Lookup account information.
       /// </summary>
       /// <param name="exclude">Exclude additional items such as asset holdings, application local data stored
/// for this account, asset parameters created by this account, and application
/// parameters created by this account.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<AccountResponse> lookupAccountByIDAsync(string accountId, string? exclude=null, bool? includeAll=null, ulong? round=null)
       {
              return lookupAccountByIDAsync(System.Threading.CancellationToken.None,accountId,exclude,includeAll,round);
       }

       /// <summary>>Lookup account information.
       /// </summary>
       /// <param name="exclude">Exclude additional items such as asset holdings, application local data stored
/// for this account, asset parameters created by this account, and application
/// parameters created by this account.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<AccountResponse> lookupAccountByIDAsync(System.Threading.CancellationToken cancellationToken,string accountId,string? exclude=null,bool? includeAll=null,ulong? round=null)
       {
              if (accountId == null) throw new System.ArgumentNullException("accountId");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/accounts/{account-id}?");
              urlBuilder_.Replace("{account-id}", System.Uri.EscapeDataString(ConvertToString(accountId, System.Globalization.CultureInfo.InvariantCulture)));
              if (exclude != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("exclude") + "=").Append(System.Uri.EscapeDataString(ConvertToString(exclude, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (round != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AccountResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup an account's asset holdings, optionally for a specific ID.
       /// </summary>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<AssetHoldingsResponse> lookupAccountAssetsAsync(string accountId, ulong? assetId=null, bool? includeAll=null, ulong? limit=null, string? next=null)
       {
              return lookupAccountAssetsAsync(System.Threading.CancellationToken.None,accountId,assetId,includeAll,limit,next);
       }

       /// <summary>>Lookup an account's asset holdings, optionally for a specific ID.
       /// </summary>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<AssetHoldingsResponse> lookupAccountAssetsAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? assetId=null,bool? includeAll=null,ulong? limit=null,string? next=null)
       {
              if (accountId == null) throw new System.ArgumentNullException("accountId");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/accounts/{account-id}/assets?");
              urlBuilder_.Replace("{account-id}", System.Uri.EscapeDataString(ConvertToString(accountId, System.Globalization.CultureInfo.InvariantCulture)));
              if (assetId != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("asset-id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AssetHoldingsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup an account's created asset parameters, optionally for a specific ID.
       /// </summary>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<AssetsResponse> lookupAccountCreatedAssetsAsync(string accountId, ulong? assetId=null, bool? includeAll=null, ulong? limit=null, string? next=null)
       {
              return lookupAccountCreatedAssetsAsync(System.Threading.CancellationToken.None,accountId,assetId,includeAll,limit,next);
       }

       /// <summary>>Lookup an account's created asset parameters, optionally for a specific ID.
       /// </summary>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<AssetsResponse> lookupAccountCreatedAssetsAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? assetId=null,bool? includeAll=null,ulong? limit=null,string? next=null)
       {
              if (accountId == null) throw new System.ArgumentNullException("accountId");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/accounts/{account-id}/created-assets?");
              urlBuilder_.Replace("{account-id}", System.Uri.EscapeDataString(ConvertToString(accountId, System.Globalization.CultureInfo.InvariantCulture)));
              if (assetId != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("asset-id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AssetsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup an account's asset holdings, optionally for a specific ID.
       /// </summary>
       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<ApplicationLocalStatesResponse> lookupAccountAppLocalStatesAsync(string accountId, ulong? applicationId=null, bool? includeAll=null, ulong? limit=null, string? next=null)
       {
              return lookupAccountAppLocalStatesAsync(System.Threading.CancellationToken.None,accountId,applicationId,includeAll,limit,next);
       }

       /// <summary>>Lookup an account's asset holdings, optionally for a specific ID.
       /// </summary>
       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<ApplicationLocalStatesResponse> lookupAccountAppLocalStatesAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? applicationId=null,bool? includeAll=null,ulong? limit=null,string? next=null)
       {
              if (accountId == null) throw new System.ArgumentNullException("accountId");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/accounts/{account-id}/apps-local-state?");
              urlBuilder_.Replace("{account-id}", System.Uri.EscapeDataString(ConvertToString(accountId, System.Globalization.CultureInfo.InvariantCulture)));
              if (applicationId != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("application-id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ApplicationLocalStatesResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup an account's created application parameters, optionally for a specific
/// ID.
       /// </summary>
       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<ApplicationsResponse> lookupAccountCreatedApplicationsAsync(string accountId, ulong? applicationId=null, bool? includeAll=null, ulong? limit=null, string? next=null)
       {
              return lookupAccountCreatedApplicationsAsync(System.Threading.CancellationToken.None,accountId,applicationId,includeAll,limit,next);
       }

       /// <summary>>Lookup an account's created application parameters, optionally for a specific
/// ID.
       /// </summary>
       /// <param name="application-id">Application ID</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<ApplicationsResponse> lookupAccountCreatedApplicationsAsync(System.Threading.CancellationToken cancellationToken,string accountId,ulong? applicationId=null,bool? includeAll=null,ulong? limit=null,string? next=null)
       {
              if (accountId == null) throw new System.ArgumentNullException("accountId");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/accounts/{account-id}/created-applications?");
              urlBuilder_.Replace("{account-id}", System.Uri.EscapeDataString(ConvertToString(accountId, System.Globalization.CultureInfo.InvariantCulture)));
              if (applicationId != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("application-id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ApplicationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup account transactions. Transactions are returned newest to oldest.
       /// </summary>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="account-id">account string</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<TransactionsResponse> lookupAccountTransactionsAsync(string accountId, string? afterTime=null, ulong? assetId=null, string? beforeTime=null, ulong? currencyGreaterThan=null, ulong? currencyLessThan=null, ulong? limit=null, ulong? maxRound=null, ulong? minRound=null, string? next=null, string? notePrefix=null, bool? rekeyTo=null, ulong? round=null, string? sigType=null, string? txType=null, string? txid=null)
       {
              return lookupAccountTransactionsAsync(System.Threading.CancellationToken.None,accountId,afterTime,assetId,beforeTime,currencyGreaterThan,currencyLessThan,limit,maxRound,minRound,next,notePrefix,rekeyTo,round,sigType,txType,txid);
       }

       /// <summary>>Lookup account transactions. Transactions are returned newest to oldest.
       /// </summary>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="asset-id">Asset ID</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="account-id">account string</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<TransactionsResponse> lookupAccountTransactionsAsync(System.Threading.CancellationToken cancellationToken,string accountId,string? afterTime=null,ulong? assetId=null,string? beforeTime=null,ulong? currencyGreaterThan=null,ulong? currencyLessThan=null,ulong? limit=null,ulong? maxRound=null,ulong? minRound=null,string? next=null,string? notePrefix=null,bool? rekeyTo=null,ulong? round=null,string? sigType=null,string? txType=null,string? txid=null)
       {
              if (accountId == null) throw new System.ArgumentNullException("accountId");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/accounts/{account-id}/transactions?");
              urlBuilder_.Replace("{account-id}", System.Uri.EscapeDataString(ConvertToString(accountId, System.Globalization.CultureInfo.InvariantCulture)));
              if (afterTime != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("after-time") + "=").Append(System.Uri.EscapeDataString(ConvertToString(afterTime, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (assetId != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("asset-id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (beforeTime != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("before-time") + "=").Append(System.Uri.EscapeDataString(ConvertToString(beforeTime, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (currencyGreaterThan != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("currency-greater-than") + "=").Append(System.Uri.EscapeDataString(ConvertToString(currencyGreaterThan, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (currencyLessThan != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("currency-less-than") + "=").Append(System.Uri.EscapeDataString(ConvertToString(currencyLessThan, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (maxRound != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("max-round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(maxRound, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (minRound != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("min-round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(minRound, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (notePrefix != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("note-prefix") + "=").Append(System.Uri.EscapeDataString(ConvertToString(notePrefix, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (rekeyTo != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("rekey-to") + "=").Append(System.Uri.EscapeDataString(ConvertToString(rekeyTo, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (round != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (sigType != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("sig-type") + "=").Append(System.Uri.EscapeDataString(ConvertToString(sigType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (txType != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("tx-type") + "=").Append(System.Uri.EscapeDataString(ConvertToString(txType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (txid != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("txid") + "=").Append(System.Uri.EscapeDataString(ConvertToString(txid, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<TransactionsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup application.
       /// </summary>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="application-id"></param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<ApplicationResponse> lookupApplicationByIDAsync(ulong applicationId, bool? includeAll=null)
       {
              return lookupApplicationByIDAsync(System.Threading.CancellationToken.None,applicationId,includeAll);
       }

       /// <summary>>Lookup application.
       /// </summary>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="application-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<ApplicationResponse> lookupApplicationByIDAsync(System.Threading.CancellationToken cancellationToken,ulong applicationId,bool? includeAll=null)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/applications/{application-id}?");
              urlBuilder_.Replace("{application-id}", System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture)));
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ApplicationResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup application logs.
       /// </summary>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="sender-address">Only include transactions with this sender address.</param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="application-id"></param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<ApplicationLogsResponse> lookupApplicationLogsByIDAsync(ulong applicationId, ulong? limit=null, ulong? maxRound=null, ulong? minRound=null, string? next=null, Address? senderAddress=null, string? txid=null)
       {
              return lookupApplicationLogsByIDAsync(System.Threading.CancellationToken.None,applicationId,limit,maxRound,minRound,next,senderAddress,txid);
       }

       /// <summary>>Lookup application logs.
       /// </summary>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="sender-address">Only include transactions with this sender address.</param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="application-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<ApplicationLogsResponse> lookupApplicationLogsByIDAsync(System.Threading.CancellationToken cancellationToken,ulong applicationId,ulong? limit=null,ulong? maxRound=null,ulong? minRound=null,string? next=null,Address? senderAddress=null,string? txid=null)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/applications/{application-id}/logs?");
              urlBuilder_.Replace("{application-id}", System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture)));
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (maxRound != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("max-round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(maxRound, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (minRound != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("min-round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(minRound, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (senderAddress != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("sender-address") + "=").Append(System.Uri.EscapeDataString(ConvertToString(senderAddress, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (txid != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("txid") + "=").Append(System.Uri.EscapeDataString(ConvertToString(txid, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ApplicationLogsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup asset information.
       /// </summary>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="asset-id"></param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<AssetResponse> lookupAssetByIDAsync(ulong assetId, bool? includeAll=null)
       {
              return lookupAssetByIDAsync(System.Threading.CancellationToken.None,assetId,includeAll);
       }

       /// <summary>>Lookup asset information.
       /// </summary>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="asset-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<AssetResponse> lookupAssetByIDAsync(System.Threading.CancellationToken cancellationToken,ulong assetId,bool? includeAll=null)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/assets/{asset-id}?");
              urlBuilder_.Replace("{asset-id}", System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture)));
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AssetResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup the list of accounts who hold this asset
       /// </summary>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="asset-id"></param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<AssetBalancesResponse> lookupAssetBalancesAsync(ulong assetId, ulong? currencyGreaterThan=null, ulong? currencyLessThan=null, bool? includeAll=null, ulong? limit=null, string? next=null)
       {
              return lookupAssetBalancesAsync(System.Threading.CancellationToken.None,assetId,currencyGreaterThan,currencyLessThan,includeAll,limit,next);
       }

       /// <summary>>Lookup the list of accounts who hold this asset
       /// </summary>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="include-all">Include all items including closed accounts, deleted applications, destroyed
/// assets, opted-out asset holdings, and closed-out application localstates.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="asset-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<AssetBalancesResponse> lookupAssetBalancesAsync(System.Threading.CancellationToken cancellationToken,ulong assetId,ulong? currencyGreaterThan=null,ulong? currencyLessThan=null,bool? includeAll=null,ulong? limit=null,string? next=null)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/assets/{asset-id}/balances?");
              urlBuilder_.Replace("{asset-id}", System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture)));
              if (currencyGreaterThan != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("currency-greater-than") + "=").Append(System.Uri.EscapeDataString(ConvertToString(currencyGreaterThan, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (currencyLessThan != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("currency-less-than") + "=").Append(System.Uri.EscapeDataString(ConvertToString(currencyLessThan, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (includeAll != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("include-all") + "=").Append(System.Uri.EscapeDataString(ConvertToString(includeAll, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AssetBalancesResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup transactions for an asset. Transactions are returned oldest to newest.
       /// </summary>
       /// <param name="address">Only include transactions with this address in one of the transaction fields.</param>
       /// <param name="address-role">Combine with the address parameter to define what type of address to search for.</param>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="exclude-close-to">Combine with address and address-role parameters to define what type of address
/// to search for. The close to fields are normally treated as a receiver, if you
/// would like to exclude them set this parameter to true.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="asset-id"></param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<TransactionsResponse> lookupAssetTransactionsAsync(ulong assetId, Address? address=null, string? addressRole=null, string? afterTime=null, string? beforeTime=null, ulong? currencyGreaterThan=null, ulong? currencyLessThan=null, bool? excludeCloseTo=null, ulong? limit=null, ulong? maxRound=null, ulong? minRound=null, string? next=null, string? notePrefix=null, bool? rekeyTo=null, ulong? round=null, string? sigType=null, string? txType=null, string? txid=null)
       {
              return lookupAssetTransactionsAsync(System.Threading.CancellationToken.None,assetId,address,addressRole,afterTime,beforeTime,currencyGreaterThan,currencyLessThan,excludeCloseTo,limit,maxRound,minRound,next,notePrefix,rekeyTo,round,sigType,txType,txid);
       }

       /// <summary>>Lookup transactions for an asset. Transactions are returned oldest to newest.
       /// </summary>
       /// <param name="address">Only include transactions with this address in one of the transaction fields.</param>
       /// <param name="address-role">Combine with the address parameter to define what type of address to search for.</param>
       /// <param name="after-time">Include results after the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="before-time">Include results before the given time. Must be an RFC 3339 formatted string.</param>
       /// <param name="currency-greater-than">Results should have an amount greater than this value. MicroAlgos are the
/// default currency unless an asset-id is provided, in which case the asset will be
/// used.</param>
       /// <param name="currency-less-than">Results should have an amount less than this value. MicroAlgos are the default
/// currency unless an asset-id is provided, in which case the asset will be used.</param>
       /// <param name="exclude-close-to">Combine with address and address-role parameters to define what type of address
/// to search for. The close to fields are normally treated as a receiver, if you
/// would like to exclude them set this parameter to true.</param>
       /// <param name="limit">Maximum number of results to return. There could be additional pages even if the
/// limit is not reached.</param>
       /// <param name="max-round">Include results at or before the specified max-round.</param>
       /// <param name="min-round">Include results at or after the specified min-round.</param>
       /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
       /// <param name="note-prefix">Specifies a prefix which must be contained in the note field.</param>
       /// <param name="rekey-to">Include results which include the rekey-to field.</param>
       /// <param name="round">Include results for the specified round.</param>
       /// <param name="sig-type">SigType filters just results using the specified type of signature:
/// * sig - Standard
/// * msig - MultiSig
/// * lsig - LogicSig</param>
       /// <param name="tx-type"></param>
       /// <param name="txid">Lookup the specific transaction by ID.</param>
       /// <param name="asset-id"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<TransactionsResponse> lookupAssetTransactionsAsync(System.Threading.CancellationToken cancellationToken,ulong assetId,Address? address=null,string? addressRole=null,string? afterTime=null,string? beforeTime=null,ulong? currencyGreaterThan=null,ulong? currencyLessThan=null,bool? excludeCloseTo=null,ulong? limit=null,ulong? maxRound=null,ulong? minRound=null,string? next=null,string? notePrefix=null,bool? rekeyTo=null,ulong? round=null,string? sigType=null,string? txType=null,string? txid=null)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/assets/{asset-id}/transactions?");
              urlBuilder_.Replace("{asset-id}", System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture)));
              if (address != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("address") + "=").Append(System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (addressRole != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("address-role") + "=").Append(System.Uri.EscapeDataString(ConvertToString(addressRole, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (afterTime != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("after-time") + "=").Append(System.Uri.EscapeDataString(ConvertToString(afterTime, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (beforeTime != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("before-time") + "=").Append(System.Uri.EscapeDataString(ConvertToString(beforeTime, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (currencyGreaterThan != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("currency-greater-than") + "=").Append(System.Uri.EscapeDataString(ConvertToString(currencyGreaterThan, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (currencyLessThan != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("currency-less-than") + "=").Append(System.Uri.EscapeDataString(ConvertToString(currencyLessThan, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (excludeCloseTo != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("exclude-close-to") + "=").Append(System.Uri.EscapeDataString(ConvertToString(excludeCloseTo, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (limit != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (maxRound != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("max-round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(maxRound, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (minRound != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("min-round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(minRound, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (next != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (notePrefix != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("note-prefix") + "=").Append(System.Uri.EscapeDataString(ConvertToString(notePrefix, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (rekeyTo != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("rekey-to") + "=").Append(System.Uri.EscapeDataString(ConvertToString(rekeyTo, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (round != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("round") + "=").Append(System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (sigType != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("sig-type") + "=").Append(System.Uri.EscapeDataString(ConvertToString(sigType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (txType != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("tx-type") + "=").Append(System.Uri.EscapeDataString(ConvertToString(txType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              if (txid != null)
              {
                     urlBuilder_.Append(System.Uri.EscapeDataString("txid") + "=").Append(System.Uri.EscapeDataString(ConvertToString(txid, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
              }
              urlBuilder_.Length--;
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<TransactionsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup block.
       /// </summary>
       /// <param name="round-number">Round number</param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<Block> lookupBlockAsync(ulong roundNumber)
       {
              return lookupBlockAsync(System.Threading.CancellationToken.None,roundNumber);
       }

       /// <summary>>Lookup block.
       /// </summary>
       /// <param name="round-number">Round number</param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<Block> lookupBlockAsync(System.Threading.CancellationToken cancellationToken,ulong roundNumber)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/blocks/{round-number}");
              urlBuilder_.Replace("{round-number}", System.Uri.EscapeDataString(ConvertToString(roundNumber, System.Globalization.CultureInfo.InvariantCulture)));
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Block>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }


       

       /// <summary>Lookup a single transaction.
       /// </summary>
       /// <param name="txid"></param>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<TransactionResponse> lookupTransactionAsync(string txid)
       {
              return lookupTransactionAsync(System.Threading.CancellationToken.None,txid);
       }

       /// <summary>>Lookup a single transaction.
       /// </summary>
       /// <param name="txid"></param>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<TransactionResponse> lookupTransactionAsync(System.Threading.CancellationToken cancellationToken,string txid)
       {
              if (txid == null) throw new System.ArgumentNullException("txid");
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/v2/transactions/{txid}");
              urlBuilder_.Replace("{txid}", System.Uri.EscapeDataString(ConvertToString(txid, System.Globalization.CultureInfo.InvariantCulture)));
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<TransactionResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }



       protected struct ObjectResponseResult<T>
       {
       public ObjectResponseResult(T responseObject, string responseText)
       {
              this.Object = responseObject;
              this.Text = responseText;
       }

       public T Object { get; }

       public string Text { get; }
       }

       public bool ReadResponseAsString { get; set; }

       protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
       {
       if (response == null || response.Content == null)
       {
              return new ObjectResponseResult<T>(default(T), string.Empty);
       }

       if (ReadResponseAsString)
       {
              string responseText;
                if (response.Content.Headers.ContentType.MediaType == "application/msgpack")
                {
                    using (MessagePackReader reader = new MessagePackReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false)))
                    {
                        responseText = reader.ReadAsString();
                    }
                }
                else
                {
                    responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                
              try
              {
                     var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                     return new ObjectResponseResult<T>(typedBody, responseText);
              }
              catch (Newtonsoft.Json.JsonException exception)
              {
                     var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                     throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
              }
       }
       else
       {
                 try
                {
                    if (response.Content.Headers.ContentType.MediaType == "application/msgpack")
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var reader = new MessagePackReader(responseStream))
                        {
                            var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                            var typedBody = serializer.Deserialize<T>(reader);
                            return new ObjectResponseResult<T>(typedBody, string.Empty);
                        }
                    }
                    else
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var streamReader = new System.IO.StreamReader(responseStream))
                        using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                        {
                            var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                            var typedBody = serializer.Deserialize<T>(jsonTextReader);
                            return new ObjectResponseResult<T>(typedBody, string.Empty);
                        }
                    }
                                      
                }
              catch (Newtonsoft.Json.JsonException exception)
              {
              var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
              throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
              }
       }
       }

       private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
       {
              if (value == null)
              {
                     return "";
              }

              if (value is System.Enum)
              {
                     var name = System.Enum.GetName(value.GetType(), value);
                     if (name != null)
                     {
                     var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                     if (field != null)
                     {
                            var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                            if (attribute != null)
                            {
                            return attribute.Value != null ? attribute.Value : name;
                            }
                     }

                     var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                     return converted == null ? string.Empty : converted;
                     }
              }
              else if (value is bool)
              {
                     return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
              }
              else if (value is byte[])
              {
                     return System.Convert.ToBase64String((byte[])value);
              }
              else if (value.GetType().IsArray)
              {
                     var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                     return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
              }

              var result = System.Convert.ToString(value, cultureInfo);
              return result == null ? "" : result;
              
       }

    }

}
