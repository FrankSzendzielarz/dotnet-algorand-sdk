namespace Algorand.Algod
{
    using Algorand.Algod.Model;
    using Algorand.Algod.Model.Transactions;
    using Algorand.Utils;
    using System.Collections.Generic;
    using System = global::System;
    using Newtonsoft.Msgpack;


    public partial interface IDefaultApi
    {
        /// <summary>Returns OK if healthy.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> HealthCheckAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> HealthCheckAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Returns OK if healthy and fully caught up.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> GetReadyAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> GetReadyAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Return metrics about algod functioning.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> MetricsAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> MetricsAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Returns the entire genesis file in json.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Genesis> GetGenesisAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Genesis> GetGenesisAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Returns the entire swagger spec in json.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> SwaggerJSONAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> SwaggerJSONAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Retrieves the supported API versions, binary build versions, and genesis
        /// information.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Version> GetVersionAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Version> GetVersionAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Given a specific account public key, this call returns the account's status,
        /// balance and spendable amounts
        /// </summary>
        /// <param name="exclude">When set to `all` will exclude asset holdings, application local state, created
        /// asset parameters, any created application parameters. Defaults to `none`.</param>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Account> AccountInformationAsync(string address, string? exclude = null, Format? format = null);

        /// <param name="exclude">When set to `all` will exclude asset holdings, application local state, created
        /// asset parameters, any created application parameters. Defaults to `none`.</param>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Account> AccountInformationAsync(System.Threading.CancellationToken cancellationToken, string address, string? exclude = null, Format? format = null);

        /// <summary>Given a specific account public key and asset ID, this call returns the
        /// account's asset holding and asset parameters (if either exist). Asset parameters
        /// will only be returned if the provided address is the asset's creator.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="asset-id">An asset identifier</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AccountAssetResponse> AccountAssetInformationAsync(string address, ulong assetId, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="asset-id">An asset identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AccountAssetResponse> AccountAssetInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong assetId, Format? format = null);

        /// <summary>Lookup an account's asset holdings.
        /// </summary>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AccountAssetsInformationResponse> AccountAssetsInformationAsync(string address, ulong? limit = null, string? next = null);

        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AccountAssetsInformationResponse> AccountAssetsInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong? limit = null, string? next = null);

        /// <summary>Lookup an account's application holdings (local state and params if the account is the creator).
        /// </summary>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="include">Include additional items in the response. Use `params` to include full application parameters (global state, schema, etc.). Multiple values can be comma-separated. Defaults to returning only application IDs and local state.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AccountApplicationsInformationResponse> AccountApplicationsInformationAsync(string address, ulong? limit = null, string? next = null, string? include = null);

        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="include">Include additional items in the response. Use `params` to include full application parameters (global state, schema, etc.). Multiple values can be comma-separated. Defaults to returning only application IDs and local state.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AccountApplicationsInformationResponse> AccountApplicationsInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong? limit = null, string? next = null, string? include = null);

        /// <summary>Given a specific account public key and application ID, this call returns the
        /// account's application local state and global state (AppLocalState and AppParams,
        /// if either exists). Global state will only be returned if the provided address is
        /// the application's creator.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AccountApplicationResponse> AccountApplicationInformationAsync(string address, ulong applicationId, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<AccountApplicationResponse> AccountApplicationInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong applicationId, Format? format = null);

        /// <summary>Get the list of pending transactions by address, sorted by priority, in
        /// decreasing order, truncated at the end at MAX. If MAX = 0, returns all pending
        /// transactions.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsByAddressAsync(string address, Format? format = null, ulong? max = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsByAddressAsync(System.Threading.CancellationToken cancellationToken, string address, Format? format = null, ulong? max = null);

        /// <summary>Get the block for the given round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="header-only">If true, only the block header (exclusive of payset or certificate) may be
        /// included in response.</param>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<CertifiedBlock> GetBlockAsync(ulong round, Format? format = null, bool? headerOnly = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="header-only">If true, only the block header (exclusive of payset or certificate) may be
        /// included in response.</param>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<CertifiedBlock> GetBlockAsync(System.Threading.CancellationToken cancellationToken, ulong round, Format? format = null, bool? headerOnly = null);

        /// <summary>Get the top level transaction IDs for the block on the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block transaction IDs.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BlockTxidsResponse> GetBlockTxidsAsync(ulong round);

        /// <param name="round">The round from which to fetch block transaction IDs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<BlockTxidsResponse> GetBlockTxidsAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Get the block hash for the block on the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block hash information.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BlockHashResponse> GetBlockHashAsync(ulong round);

        /// <param name="round">The round from which to fetch block hash information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<BlockHashResponse> GetBlockHashAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Get a proof for a transaction in a block.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="hashtype">The type of hash function used to create the proof, must be one of:
        /// * sha512_256
        /// * sha256</param>
        /// <param name="round">The round in which the transaction appears.</param>
        /// <param name="txid">The transaction ID for which to generate a proof.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<TransactionProofResponse> GetTransactionProofAsync(ulong round, string txid, Format? format = null, string? hashtype = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="hashtype">The type of hash function used to create the proof, must be one of:
        /// * sha512_256
        /// * sha256</param>
        /// <param name="round">The round in which the transaction appears.</param>
        /// <param name="txid">The transaction ID for which to generate a proof.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<TransactionProofResponse> GetTransactionProofAsync(System.Threading.CancellationToken cancellationToken, ulong round, string txid, Format? format = null, string? hashtype = null);

        /// <summary>Get all of the logs from outer and inner app calls in the given round
        /// </summary>
        /// <param name="round">The round from which to fetch block log information.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BlockLogsResponse> GetBlockLogsAsync(ulong round);

        /// <param name="round">The round from which to fetch block log information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<BlockLogsResponse> GetBlockLogsAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Get the current supply reported by the ledger.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<SupplyResponse> GetSupplyAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<SupplyResponse> GetSupplyAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Gets the current node status.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<NodeStatusResponse> GetStatusAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<NodeStatusResponse> GetStatusAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Waits for a block to appear after round {round} and returns the node's status at
        /// the time. There is a 1 minute timeout, when reached the current status is
        /// returned regardless of whether or not it is the round after the given round.
        /// </summary>
        /// <param name="round">The round to wait until returning status</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<NodeStatusResponse> WaitForBlockAsync(ulong round);

        /// <param name="round">The round to wait until returning status</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<NodeStatusResponse> WaitForBlockAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Broadcasts a raw transaction or transaction group to the network.
        /// </summary>
        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PostTransactionsResponse> TransactionsAsync(List<SignedTransaction> rawtxn);

        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<PostTransactionsResponse> TransactionsAsync(System.Threading.CancellationToken cancellationToken, List<SignedTransaction> rawtxn);

        /// <summary>Fast track for broadcasting a raw transaction or transaction group to the
        /// network through the tx handler without performing most of the checks and
        /// reporting detailed errors. Should be only used for development and performance
        /// testing.
        /// </summary>
        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> RawTransactionAsyncAsync(List<SignedTransaction> rawtxn);

        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> RawTransactionAsyncAsync(System.Threading.CancellationToken cancellationToken, List<SignedTransaction> rawtxn);

        /// <summary>Simulates a raw transaction or transaction group as it would be evaluated on the
        /// network. The simulation will use blockchain state from the latest committed
        /// round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="request">The transactions to simulate, along with any other inputs.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<SimulateResponse> SimulateTransactionAsync(SimulateRequest request, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="request">The transactions to simulate, along with any other inputs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<SimulateResponse> SimulateTransactionAsync(System.Threading.CancellationToken cancellationToken, SimulateRequest request, Format? format = null);

        /// <summary>Get parameters for constructing a new transaction
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<TransactionParametersResponse> TransactionParamsAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<TransactionParametersResponse> TransactionParamsAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Get the list of pending transactions, sorted by priority, in decreasing order,
        /// truncated at the end at MAX. If MAX = 0, returns all pending transactions.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsAsync(Format? format = null, ulong? max = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsAsync(System.Threading.CancellationToken cancellationToken, Format? format = null, ulong? max = null);

        /// <summary>Given a transaction ID of a recently submitted transaction, it returns
        /// information about it. There are several cases when this might succeed:
        /// - transaction committed (committed round > 0)
        /// - transaction still in the pool (committed round = 0, pool error = "")
        /// - transaction removed from pool due to error (committed round = 0, pool error !=
        /// "")
        /// Or the transaction may have happened sufficiently long ago that the node no
        /// longer remembers it, and this will return an error.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="txid">A transaction ID</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<IReturnableTransaction> PendingTransactionInformationAsync(string txid, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="txid">A transaction ID</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<IReturnableTransaction> PendingTransactionInformationAsync(System.Threading.CancellationToken cancellationToken, string txid, Format? format = null);

        /// <summary>Get ledger deltas for a round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaAsync(ulong round, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaAsync(System.Threading.CancellationToken cancellationToken, ulong round, Format? format = null);

        /// <summary>Get ledger deltas for transaction groups in a given round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<TransactionGroupLedgerStateDeltasForRoundResponse> GetTransactionGroupLedgerStateDeltasForRoundAsync(ulong round, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<TransactionGroupLedgerStateDeltasForRoundResponse> GetTransactionGroupLedgerStateDeltasForRoundAsync(System.Threading.CancellationToken cancellationToken, ulong round, Format? format = null);

        /// <summary>Get a ledger delta for a given transaction group.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="id">A transaction ID, or transaction group ID</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaForTransactionGroupAsync(string id, Format? format = null);

        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="id">A transaction ID, or transaction group ID</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaForTransactionGroupAsync(System.Threading.CancellationToken cancellationToken, string id, Format? format = null);

        /// <summary>Get a state proof that covers a given round
        /// </summary>
        /// <param name="round">The round for which a state proof is desired.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StateProof> GetStateProofAsync(ulong round);

        /// <param name="round">The round for which a state proof is desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<StateProof> GetStateProofAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Gets a proof for a given light block header inside a state proof commitment
        /// </summary>
        /// <param name="round">The round to which the light block header belongs.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<LightBlockHeaderProof> GetLightBlockHeaderProofAsync(ulong round);

        /// <param name="round">The round to which the light block header belongs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<LightBlockHeaderProof> GetLightBlockHeaderProofAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Given a application ID, it returns application information including creator,
        /// approval and clear programs, global and local schemas, and global state.
        /// </summary>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Application> GetApplicationByIDAsync(ulong applicationId);

        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Application> GetApplicationByIDAsync(System.Threading.CancellationToken cancellationToken, ulong applicationId);

        /// <summary>Given an application ID, return all Box names. No particular ordering is
        /// guaranteed. Request fails when client or server-side configured limits prevent
        /// returning all Box names.
        /// </summary>
        /// <param name="max">Max number of box names to return. If max is not set, or max == 0, returns all
        /// box-names.</param>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BoxesResponse> GetApplicationBoxesAsync(ulong applicationId, ulong? max = null);

        /// <param name="max">Max number of box names to return. If max is not set, or max == 0, returns all
        /// box-names.</param>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<BoxesResponse> GetApplicationBoxesAsync(System.Threading.CancellationToken cancellationToken, ulong applicationId, ulong? max = null);

        /// <summary>Given an application ID and box name, it returns the round, box name, and value
        /// (each base64 encoded). Box names must be in the goal app call arg encoding form
        /// 'encoding:value'. For ints, use the form 'int:1234'. For raw bytes, use the form
        /// 'b64:A=='. For printable strings, use the form 'str:hello'. For addresses, use
        /// the form 'addr:XYZ...'.
        /// </summary>
        /// <param name="name">A box name, in the goal app call arg form 'encoding:value'. For ints, use the
        /// form 'int:1234'. For raw bytes, use the form 'b64:A=='. For printable strings,
        /// use the form 'str:hello'. For addresses, use the form 'addr:XYZ...'.</param>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Box> GetApplicationBoxByNameAsync(ulong applicationId, string? name = null);

        /// <param name="name">A box name, in the goal app call arg form 'encoding:value'. For ints, use the
        /// form 'int:1234'. For raw bytes, use the form 'b64:A=='. For printable strings,
        /// use the form 'str:hello'. For addresses, use the form 'addr:XYZ...'.</param>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Box> GetApplicationBoxByNameAsync(System.Threading.CancellationToken cancellationToken, ulong applicationId, string? name = null);

        /// <summary>Given a asset ID, it returns asset information including creator, name, total
        /// supply and special addresses.
        /// </summary>
        /// <param name="asset-id">An asset identifier</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Asset> GetAssetByIDAsync(ulong assetId);

        /// <param name="asset-id">An asset identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Asset> GetAssetByIDAsync(System.Threading.CancellationToken cancellationToken, ulong assetId);

        /// <summary>Unset the ledger sync round.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> UnsetSyncRoundAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> UnsetSyncRoundAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Gets the minimum sync round for the ledger.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<GetSyncRoundResponse> GetSyncRoundAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<GetSyncRoundResponse> GetSyncRoundAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Sets the minimum sync round on the ledger.
        /// </summary>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> SetSyncRoundAsync(ulong round);

        /// <param name="round">The round for which the deltas are desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> SetSyncRoundAsync(System.Threading.CancellationToken cancellationToken, ulong round);

        /// <summary>Given TEAL source code in plain text, return base64 encoded program bytes and
        /// base32 SHA512_256 hash of program bytes (Address style). This endpoint is only
        /// enabled when a node's configuration file sets EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="sourcemap">When set to `true`, returns the source map of the program as a JSON. Defaults to
        /// `false`.</param>
        /// <param name="source">TEAL source code to be compiled</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<CompileResponse> TealCompileAsync(System.IO.Stream source, bool? sourcemap = null);

        /// <param name="sourcemap">When set to `true`, returns the source map of the program as a JSON. Defaults to
        /// `false`.</param>
        /// <param name="source">TEAL source code to be compiled</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<CompileResponse> TealCompileAsync(System.Threading.CancellationToken cancellationToken, System.IO.Stream source, bool? sourcemap = null);

        /// <summary>Given the program bytes, return the TEAL source code in plain text. This
        /// endpoint is only enabled when a node's configuration file sets
        /// EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="source">TEAL program binary to be disassembled</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<DisassembleResponse> TealDisassembleAsync(System.IO.Stream source);

        /// <param name="source">TEAL program binary to be disassembled</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<DisassembleResponse> TealDisassembleAsync(System.Threading.CancellationToken cancellationToken, System.IO.Stream source);

        /// <summary>Executes TEAL program(s) in context and returns debugging information about the
        /// execution. This endpoint is only enabled when a node's configuration file sets
        /// EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="request">Transaction (or group) and any accompanying state-simulation data.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<DryrunResponse> TealDryrunAsync(DryrunRequest request);

        /// <param name="request">Transaction (or group) and any accompanying state-simulation data.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<DryrunResponse> TealDryrunAsync(System.Threading.CancellationToken cancellationToken, DryrunRequest request);

        /// <summary>Returns OK if experimental API is enabled.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> ExperimentalCheckAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> ExperimentalCheckAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Gets the current timestamp offset.
        /// </summary>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<GetBlockTimeStampOffsetResponse> GetBlockTimeStampOffsetAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<GetBlockTimeStampOffsetResponse> GetBlockTimeStampOffsetAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>Sets the timestamp offset (seconds) for blocks in dev mode. Providing an offset
        /// of 0 will unset this value and try to use the real clock for the timestamp.
        /// </summary>
        /// <param name="offset">The timestamp offset for blocks in dev mode.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        System.Threading.Tasks.Task<string> SetBlockTimeStampOffsetAsync(ulong offset);

        /// <param name="offset">The timestamp offset for blocks in dev mode.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<string> SetBlockTimeStampOffsetAsync(System.Threading.CancellationToken cancellationToken, ulong offset);

    }

    public partial class DefaultApi : IDefaultApi
    {
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public DefaultApi(System.Net.Http.HttpClient httpClient)
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



        /// <summary>Returns OK if healthy.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> HealthCheckAsync()
        {
            return HealthCheckAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Returns OK if healthy.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> HealthCheckAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("health");
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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Returns OK if healthy and fully caught up.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> GetReadyAsync()
        {
            return GetReadyAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Returns OK if healthy and fully caught up.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> GetReadyAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("ready");
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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Return metrics about algod functioning.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> MetricsAsync()
        {
            return MetricsAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Return metrics about algod functioning.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> MetricsAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("metrics");
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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Returns the entire genesis file in json.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Genesis> GetGenesisAsync()
        {
            return GetGenesisAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Returns the entire genesis file in json.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<Genesis> GetGenesisAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("genesis");
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
                            var objectResponse_ = await ReadObjectResponseAsync<Genesis>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Returns the entire swagger spec in json.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> SwaggerJSONAsync()
        {
            return SwaggerJSONAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Returns the entire swagger spec in json.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> SwaggerJSONAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("swagger.json");
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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Retrieves the supported API versions, binary build versions, and genesis
        /// information.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Version> GetVersionAsync()
        {
            return GetVersionAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Retrieves the supported API versions, binary build versions, and genesis
        /// information.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<Version> GetVersionAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("versions");
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
                            var objectResponse_ = await ReadObjectResponseAsync<Version>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given a specific account public key, this call returns the account's status,
        /// balance and spendable amounts
        /// </summary>
        /// <param name="exclude">When set to `all` will exclude asset holdings, application local state, created
        /// asset parameters, any created application parameters. Defaults to `none`.</param>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Account> AccountInformationAsync(string address, string? exclude = null, Format? format = null)
        {
            return AccountInformationAsync(System.Threading.CancellationToken.None, address, exclude, format);
        }

        /// <summary>>Given a specific account public key, this call returns the account's status,
        /// balance and spendable amounts
        /// </summary>
        /// <param name="exclude">When set to `all` will exclude asset holdings, application local state, created
        /// asset parameters, any created application parameters. Defaults to `none`.</param>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<Account> AccountInformationAsync(System.Threading.CancellationToken cancellationToken, string address, string? exclude = null, Format? format = null)
        {
            if (address == null) throw new System.ArgumentNullException("address");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/accounts/{address}?");
            urlBuilder_.Replace("{address}", System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture)));
            if (exclude != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("exclude") + "=").Append(System.Uri.EscapeDataString(ConvertToString(exclude, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<Account>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given a specific account public key and asset ID, this call returns the
        /// account's asset holding and asset parameters (if either exist). Asset parameters
        /// will only be returned if the provided address is the asset's creator.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="asset-id">An asset identifier</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<AccountAssetResponse> AccountAssetInformationAsync(string address, ulong assetId, Format? format = null)
        {
            return AccountAssetInformationAsync(System.Threading.CancellationToken.None, address, assetId, format);
        }

        /// <summary>>Given a specific account public key and asset ID, this call returns the
        /// account's asset holding and asset parameters (if either exist). Asset parameters
        /// will only be returned if the provided address is the asset's creator.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="asset-id">An asset identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<AccountAssetResponse> AccountAssetInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong assetId, Format? format = null)
        {
            if (address == null) throw new System.ArgumentNullException("address");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/accounts/{address}/assets/{asset-id}?");
            urlBuilder_.Replace("{address}", System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{asset-id}", System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<AccountAssetResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Lookup an account's asset holdings.
        /// </summary>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<AccountAssetsInformationResponse> AccountAssetsInformationAsync(string address, ulong? limit = null, string? next = null)
        {
            return AccountAssetsInformationAsync(System.Threading.CancellationToken.None, address, limit, next);
        }

        /// <summary>>Lookup an account's asset holdings.
        /// </summary>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<AccountAssetsInformationResponse> AccountAssetsInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong? limit = null, string? next = null)
        {
            if (address == null) throw new System.ArgumentNullException("address");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/accounts/{address}/assets?");
            urlBuilder_.Replace("{address}", System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<AccountAssetsInformationResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Lookup an account's application holdings (local state and params if the account is the creator).
        /// </summary>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="include">Include additional items in the response. Use `params` to include full application parameters (global state, schema, etc.). Multiple values can be comma-separated. Defaults to returning only application IDs and local state.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<AccountApplicationsInformationResponse> AccountApplicationsInformationAsync(string address, ulong? limit = null, string? next = null, string? include = null)
        {
            return AccountApplicationsInformationAsync(System.Threading.CancellationToken.None, address, limit, next, include);
        }

        /// <summary>>Lookup an account's application holdings (local state and params if the account is the creator).
        /// </summary>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="include">Include additional items in the response. Use `params` to include full application parameters (global state, schema, etc.). Multiple values can be comma-separated. Defaults to returning only application IDs and local state.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<AccountApplicationsInformationResponse> AccountApplicationsInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong? limit = null, string? next = null, string? include = null)
        {
            if (address == null) throw new System.ArgumentNullException("address");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/accounts/{address}/applications?");
            urlBuilder_.Replace("{address}", System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture)));
            if (limit != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (next != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("next") + "=").Append(System.Uri.EscapeDataString(ConvertToString(next, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (include != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("include") + "=").Append(System.Uri.EscapeDataString(ConvertToString(include, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<AccountApplicationsInformationResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given a specific account public key and application ID, this call returns the
        /// account's application local state and global state (AppLocalState and AppParams,
        /// if either exists). Global state will only be returned if the provided address is
        /// the application's creator.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<AccountApplicationResponse> AccountApplicationInformationAsync(string address, ulong applicationId, Format? format = null)
        {
            return AccountApplicationInformationAsync(System.Threading.CancellationToken.None, address, applicationId, format);
        }

        /// <summary>>Given a specific account public key and application ID, this call returns the
        /// account's application local state and global state (AppLocalState and AppParams,
        /// if either exists). Global state will only be returned if the provided address is
        /// the application's creator.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="address">An account public key</param>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<AccountApplicationResponse> AccountApplicationInformationAsync(System.Threading.CancellationToken cancellationToken, string address, ulong applicationId, Format? format = null)
        {
            if (address == null) throw new System.ArgumentNullException("address");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/accounts/{address}/applications/{application-id}?");
            urlBuilder_.Replace("{address}", System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{application-id}", System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<AccountApplicationResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get the list of pending transactions by address, sorted by priority, in
        /// decreasing order, truncated at the end at MAX. If MAX = 0, returns all pending
        /// transactions.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <param name="address">An account public key</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsByAddressAsync(string address, Format? format = null, ulong? max = null)
        {
            return GetPendingTransactionsByAddressAsync(System.Threading.CancellationToken.None, address, format, max);
        }

        /// <summary>>Get the list of pending transactions by address, sorted by priority, in
        /// decreasing order, truncated at the end at MAX. If MAX = 0, returns all pending
        /// transactions.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <param name="address">An account public key</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsByAddressAsync(System.Threading.CancellationToken cancellationToken, string address, Format? format = null, ulong? max = null)
        {
            if (address == null) throw new System.ArgumentNullException("address");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/accounts/{address}/transactions/pending?");
            urlBuilder_.Replace("{address}", System.Uri.EscapeDataString(ConvertToString(address, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (max != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("max") + "=").Append(System.Uri.EscapeDataString(ConvertToString(max, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<PendingTransactions>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get the block for the given round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="header-only">If true, only the block header (exclusive of payset or certificate) may be
        /// included in response.</param>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<CertifiedBlock> GetBlockAsync(ulong round, Format? format = null, bool? headerOnly = null)
        {
            return GetBlockAsync(System.Threading.CancellationToken.None, round, format, headerOnly);
        }

        /// <summary>>Get the block for the given round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="header-only">If true, only the block header (exclusive of payset or certificate) may be
        /// included in response.</param>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<CertifiedBlock> GetBlockAsync(System.Threading.CancellationToken cancellationToken, ulong round, Format? format = null, bool? headerOnly = null)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/blocks/{round}?");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (headerOnly != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("header-only") + "=").Append(System.Uri.EscapeDataString(ConvertToString(headerOnly, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<CertifiedBlock>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get the top level transaction IDs for the block on the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block transaction IDs.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<BlockTxidsResponse> GetBlockTxidsAsync(ulong round)
        {
            return GetBlockTxidsAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Get the top level transaction IDs for the block on the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block transaction IDs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<BlockTxidsResponse> GetBlockTxidsAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/blocks/{round}/txids");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<BlockTxidsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get the block hash for the block on the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block hash information.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<BlockHashResponse> GetBlockHashAsync(ulong round)
        {
            return GetBlockHashAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Get the block hash for the block on the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block hash information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<BlockHashResponse> GetBlockHashAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/blocks/{round}/hash");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<BlockHashResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get a proof for a transaction in a block.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="hashtype">The type of hash function used to create the proof, must be one of:
        /// * sha512_256
        /// * sha256</param>
        /// <param name="round">The round in which the transaction appears.</param>
        /// <param name="txid">The transaction ID for which to generate a proof.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<TransactionProofResponse> GetTransactionProofAsync(ulong round, string txid, Format? format = null, string? hashtype = null)
        {
            return GetTransactionProofAsync(System.Threading.CancellationToken.None, round, txid, format, hashtype);
        }

        /// <summary>>Get a proof for a transaction in a block.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="hashtype">The type of hash function used to create the proof, must be one of:
        /// * sha512_256
        /// * sha256</param>
        /// <param name="round">The round in which the transaction appears.</param>
        /// <param name="txid">The transaction ID for which to generate a proof.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<TransactionProofResponse> GetTransactionProofAsync(System.Threading.CancellationToken cancellationToken, ulong round, string txid, Format? format = null, string? hashtype = null)
        {
            if (txid == null) throw new System.ArgumentNullException("txid");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/blocks/{round}/transactions/{txid}/proof?");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{txid}", System.Uri.EscapeDataString(ConvertToString(txid, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (hashtype != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("hashtype") + "=").Append(System.Uri.EscapeDataString(ConvertToString(hashtype, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<TransactionProofResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get all of the logs from outer and inner app calls in the given round
        /// </summary>
        /// <param name="round">The round from which to fetch block log information.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<BlockLogsResponse> GetBlockLogsAsync(ulong round)
        {
            return GetBlockLogsAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Get all of the logs from outer and inner app calls in the given round
        /// </summary>
        /// <param name="round">The round from which to fetch block log information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<BlockLogsResponse> GetBlockLogsAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/blocks/{round}/logs");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<BlockLogsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get the current supply reported by the ledger.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SupplyResponse> GetSupplyAsync()
        {
            return GetSupplyAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Get the current supply reported by the ledger.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SupplyResponse> GetSupplyAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/ledger/supply");
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
                            var objectResponse_ = await ReadObjectResponseAsync<SupplyResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Gets the current node status.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<NodeStatusResponse> GetStatusAsync()
        {
            return GetStatusAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Gets the current node status.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<NodeStatusResponse> GetStatusAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/status");
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
                            var objectResponse_ = await ReadObjectResponseAsync<NodeStatusResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Waits for a block to appear after round {round} and returns the node's status at
        /// the time. There is a 1 minute timeout, when reached the current status is
        /// returned regardless of whether or not it is the round after the given round.
        /// </summary>
        /// <param name="round">The round to wait until returning status</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<NodeStatusResponse> WaitForBlockAsync(ulong round)
        {
            return WaitForBlockAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Waits for a block to appear after round {round} and returns the node's status at
        /// the time. There is a 1 minute timeout, when reached the current status is
        /// returned regardless of whether or not it is the round after the given round.
        /// </summary>
        /// <param name="round">The round to wait until returning status</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<NodeStatusResponse> WaitForBlockAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/status/wait-for-block-after/{round}");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<NodeStatusResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Broadcasts a raw transaction or transaction group to the network.
        /// </summary>
        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<PostTransactionsResponse> TransactionsAsync(List<SignedTransaction> rawtxn)
        {
            return TransactionsAsync(System.Threading.CancellationToken.None, rawtxn);
        }

        /// <summary>>Broadcasts a raw transaction or transaction group to the network.
        /// </summary>
        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<PostTransactionsResponse> TransactionsAsync(System.Threading.CancellationToken cancellationToken, List<SignedTransaction> rawtxn)
        {
            if (rawtxn == null) throw new System.ArgumentNullException("rawtxn");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/transactions");
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    System.Net.Http.ByteArrayContent content_ = new System.Net.Http.ByteArrayContent(Encoder.EncodeToMsgPackOrdered(rawtxn));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/msgpack");
                    request_.Content = content_;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<PostTransactionsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Fast track for broadcasting a raw transaction or transaction group to the
        /// network through the tx handler without performing most of the checks and
        /// reporting detailed errors. Should be only used for development and performance
        /// testing.
        /// </summary>
        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> RawTransactionAsyncAsync(List<SignedTransaction> rawtxn)
        {
            return RawTransactionAsyncAsync(System.Threading.CancellationToken.None, rawtxn);
        }

        /// <summary>>Fast track for broadcasting a raw transaction or transaction group to the
        /// network through the tx handler without performing most of the checks and
        /// reporting detailed errors. Should be only used for development and performance
        /// testing.
        /// </summary>
        /// <param name="rawtxn">The byte encoded signed transaction to broadcast to network</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> RawTransactionAsyncAsync(System.Threading.CancellationToken cancellationToken, List<SignedTransaction> rawtxn)
        {
            if (rawtxn == null) throw new System.ArgumentNullException("rawtxn");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/transactions/async");
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Simulates a raw transaction or transaction group as it would be evaluated on the
        /// network. The simulation will use blockchain state from the latest committed
        /// round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="request">The transactions to simulate, along with any other inputs.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SimulateResponse> SimulateTransactionAsync(SimulateRequest request, Format? format = null)
        {
            return SimulateTransactionAsync(System.Threading.CancellationToken.None, request, format);
        }

        /// <summary>>Simulates a raw transaction or transaction group as it would be evaluated on the
        /// network. The simulation will use blockchain state from the latest committed
        /// round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="request">The transactions to simulate, along with any other inputs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SimulateResponse> SimulateTransactionAsync(System.Threading.CancellationToken cancellationToken, SimulateRequest request, Format? format = null)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/transactions/simulate?");
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    System.Net.Http.ByteArrayContent content_ = new System.Net.Http.ByteArrayContent(Encoder.EncodeToMsgPackOrdered(request));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/msgpack");
                    request_.Content = content_;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<SimulateResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get parameters for constructing a new transaction
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<TransactionParametersResponse> TransactionParamsAsync()
        {
            return TransactionParamsAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Get parameters for constructing a new transaction
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<TransactionParametersResponse> TransactionParamsAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/transactions/params");
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
                            var objectResponse_ = await ReadObjectResponseAsync<TransactionParametersResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get the list of pending transactions, sorted by priority, in decreasing order,
        /// truncated at the end at MAX. If MAX = 0, returns all pending transactions.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsAsync(Format? format = null, ulong? max = null)
        {
            return GetPendingTransactionsAsync(System.Threading.CancellationToken.None, format, max);
        }

        /// <summary>>Get the list of pending transactions, sorted by priority, in decreasing order,
        /// truncated at the end at MAX. If MAX = 0, returns all pending transactions.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<PendingTransactions> GetPendingTransactionsAsync(System.Threading.CancellationToken cancellationToken, Format? format = null, ulong? max = null)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/transactions/pending?");
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (max != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("max") + "=").Append(System.Uri.EscapeDataString(ConvertToString(max, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<PendingTransactions>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given a transaction ID of a recently submitted transaction, it returns
        /// information about it. There are several cases when this might succeed:
        /// - transaction committed (committed round > 0)
        /// - transaction still in the pool (committed round = 0, pool error = "")
        /// - transaction removed from pool due to error (committed round = 0, pool error !=
        /// "")
        /// Or the transaction may have happened sufficiently long ago that the node no
        /// longer remembers it, and this will return an error.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="txid">A transaction ID</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<IReturnableTransaction> PendingTransactionInformationAsync(string txid, Format? format = null)
        {
            return PendingTransactionInformationAsync(System.Threading.CancellationToken.None, txid, format);
        }

        /// <summary>>Given a transaction ID of a recently submitted transaction, it returns
        /// information about it. There are several cases when this might succeed:
        /// - transaction committed (committed round > 0)
        /// - transaction still in the pool (committed round = 0, pool error = "")
        /// - transaction removed from pool due to error (committed round = 0, pool error !=
        /// "")
        /// Or the transaction may have happened sufficiently long ago that the node no
        /// longer remembers it, and this will return an error.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="txid">A transaction ID</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<IReturnableTransaction> PendingTransactionInformationAsync(System.Threading.CancellationToken cancellationToken, string txid, Format? format = null)
        {
            if (txid == null) throw new System.ArgumentNullException("txid");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/transactions/pending/{txid}?");
            urlBuilder_.Replace("{txid}", System.Uri.EscapeDataString(ConvertToString(txid, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<IReturnableTransaction>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get ledger deltas for a round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaAsync(ulong round, Format? format = null)
        {
            return GetLedgerStateDeltaAsync(System.Threading.CancellationToken.None, round, format);
        }

        /// <summary>>Get ledger deltas for a round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaAsync(System.Threading.CancellationToken cancellationToken, ulong round, Format? format = null)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/deltas/{round}?");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<LedgerStateDelta>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get ledger deltas for transaction groups in a given round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<TransactionGroupLedgerStateDeltasForRoundResponse> GetTransactionGroupLedgerStateDeltasForRoundAsync(ulong round, Format? format = null)
        {
            return GetTransactionGroupLedgerStateDeltasForRoundAsync(System.Threading.CancellationToken.None, round, format);
        }

        /// <summary>>Get ledger deltas for transaction groups in a given round.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<TransactionGroupLedgerStateDeltasForRoundResponse> GetTransactionGroupLedgerStateDeltasForRoundAsync(System.Threading.CancellationToken cancellationToken, ulong round, Format? format = null)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/deltas/{round}/txn/group?");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<TransactionGroupLedgerStateDeltasForRoundResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get a ledger delta for a given transaction group.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="id">A transaction ID, or transaction group ID</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaForTransactionGroupAsync(string id, Format? format = null)
        {
            return GetLedgerStateDeltaForTransactionGroupAsync(System.Threading.CancellationToken.None, id, format);
        }

        /// <summary>>Get a ledger delta for a given transaction group.
        /// </summary>
        /// <param name="format">Configures whether the response object is JSON or MessagePack encoded. If not
        /// provided, defaults to JSON.</param>
        /// <param name="id">A transaction ID, or transaction group ID</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<LedgerStateDelta> GetLedgerStateDeltaForTransactionGroupAsync(System.Threading.CancellationToken cancellationToken, string id, Format? format = null)
        {
            if (id == null) throw new System.ArgumentNullException("id");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/deltas/txn/group/{id}?");
            urlBuilder_.Replace("{id}", System.Uri.EscapeDataString(ConvertToString(id, System.Globalization.CultureInfo.InvariantCulture)));
            if (format != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("format") + "=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<LedgerStateDelta>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Get a state proof that covers a given round
        /// </summary>
        /// <param name="round">The round for which a state proof is desired.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<StateProof> GetStateProofAsync(ulong round)
        {
            return GetStateProofAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Get a state proof that covers a given round
        /// </summary>
        /// <param name="round">The round for which a state proof is desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<StateProof> GetStateProofAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/stateproofs/{round}");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<StateProof>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Gets a proof for a given light block header inside a state proof commitment
        /// </summary>
        /// <param name="round">The round to which the light block header belongs.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<LightBlockHeaderProof> GetLightBlockHeaderProofAsync(ulong round)
        {
            return GetLightBlockHeaderProofAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Gets a proof for a given light block header inside a state proof commitment
        /// </summary>
        /// <param name="round">The round to which the light block header belongs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<LightBlockHeaderProof> GetLightBlockHeaderProofAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/blocks/{round}/lightheader/proof");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<LightBlockHeaderProof>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given a application ID, it returns application information including creator,
        /// approval and clear programs, global and local schemas, and global state.
        /// </summary>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Application> GetApplicationByIDAsync(ulong applicationId)
        {
            return GetApplicationByIDAsync(System.Threading.CancellationToken.None, applicationId);
        }

        /// <summary>>Given a application ID, it returns application information including creator,
        /// approval and clear programs, global and local schemas, and global state.
        /// </summary>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<Application> GetApplicationByIDAsync(System.Threading.CancellationToken cancellationToken, ulong applicationId)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/applications/{application-id}");
            urlBuilder_.Replace("{application-id}", System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<Application>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given an application ID, return all Box names. No particular ordering is
        /// guaranteed. Request fails when client or server-side configured limits prevent
        /// returning all Box names.
        /// </summary>
        /// <param name="max">Max number of box names to return. If max is not set, or max == 0, returns all
        /// box-names.</param>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<BoxesResponse> GetApplicationBoxesAsync(ulong applicationId, ulong? max = null)
        {
            return GetApplicationBoxesAsync(System.Threading.CancellationToken.None, applicationId, max);
        }

        /// <summary>>Given an application ID, return all Box names. No particular ordering is
        /// guaranteed. Request fails when client or server-side configured limits prevent
        /// returning all Box names.
        /// </summary>
        /// <param name="max">Max number of box names to return. If max is not set, or max == 0, returns all
        /// box-names.</param>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<BoxesResponse> GetApplicationBoxesAsync(System.Threading.CancellationToken cancellationToken, ulong applicationId, ulong? max = null)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/applications/{application-id}/boxes?");
            urlBuilder_.Replace("{application-id}", System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture)));
            if (max != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("max") + "=").Append(System.Uri.EscapeDataString(ConvertToString(max, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<BoxesResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given an application ID and box name, it returns the round, box name, and value
        /// (each base64 encoded). Box names must be in the goal app call arg encoding form
        /// 'encoding:value'. For ints, use the form 'int:1234'. For raw bytes, use the form
        /// 'b64:A=='. For printable strings, use the form 'str:hello'. For addresses, use
        /// the form 'addr:XYZ...'.
        /// </summary>
        /// <param name="name">A box name, in the goal app call arg form 'encoding:value'. For ints, use the
        /// form 'int:1234'. For raw bytes, use the form 'b64:A=='. For printable strings,
        /// use the form 'str:hello'. For addresses, use the form 'addr:XYZ...'.</param>
        /// <param name="application-id">An application identifier</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Box> GetApplicationBoxByNameAsync(ulong applicationId, string? name = null)
        {
            return GetApplicationBoxByNameAsync(System.Threading.CancellationToken.None, applicationId, name);
        }

        /// <summary>>Given an application ID and box name, it returns the round, box name, and value
        /// (each base64 encoded). Box names must be in the goal app call arg encoding form
        /// 'encoding:value'. For ints, use the form 'int:1234'. For raw bytes, use the form
        /// 'b64:A=='. For printable strings, use the form 'str:hello'. For addresses, use
        /// the form 'addr:XYZ...'.
        /// </summary>
        /// <param name="name">A box name, in the goal app call arg form 'encoding:value'. For ints, use the
        /// form 'int:1234'. For raw bytes, use the form 'b64:A=='. For printable strings,
        /// use the form 'str:hello'. For addresses, use the form 'addr:XYZ...'.</param>
        /// <param name="application-id">An application identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<Box> GetApplicationBoxByNameAsync(System.Threading.CancellationToken cancellationToken, ulong applicationId, string? name = null)
        {
            if (name == null) throw new System.ArgumentNullException("name");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/applications/{application-id}/box?");
            urlBuilder_.Replace("{application-id}", System.Uri.EscapeDataString(ConvertToString(applicationId, System.Globalization.CultureInfo.InvariantCulture)));
            if (name != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("name") + "=").Append(System.Uri.EscapeDataString(ConvertToString(name, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
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
                            var objectResponse_ = await ReadObjectResponseAsync<Box>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given a asset ID, it returns asset information including creator, name, total
        /// supply and special addresses.
        /// </summary>
        /// <param name="asset-id">An asset identifier</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Asset> GetAssetByIDAsync(ulong assetId)
        {
            return GetAssetByIDAsync(System.Threading.CancellationToken.None, assetId);
        }

        /// <summary>>Given a asset ID, it returns asset information including creator, name, total
        /// supply and special addresses.
        /// </summary>
        /// <param name="asset-id">An asset identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<Asset> GetAssetByIDAsync(System.Threading.CancellationToken cancellationToken, ulong assetId)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/assets/{asset-id}");
            urlBuilder_.Replace("{asset-id}", System.Uri.EscapeDataString(ConvertToString(assetId, System.Globalization.CultureInfo.InvariantCulture)));
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
                            var objectResponse_ = await ReadObjectResponseAsync<Asset>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Unset the ledger sync round.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> UnsetSyncRoundAsync()
        {
            return UnsetSyncRoundAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Unset the ledger sync round.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> UnsetSyncRoundAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/ledger/sync");
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("DELETE");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Gets the minimum sync round for the ledger.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<GetSyncRoundResponse> GetSyncRoundAsync()
        {
            return GetSyncRoundAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Gets the minimum sync round for the ledger.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<GetSyncRoundResponse> GetSyncRoundAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/ledger/sync");
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
                            var objectResponse_ = await ReadObjectResponseAsync<GetSyncRoundResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Sets the minimum sync round on the ledger.
        /// </summary>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> SetSyncRoundAsync(ulong round)
        {
            return SetSyncRoundAsync(System.Threading.CancellationToken.None, round);
        }

        /// <summary>>Sets the minimum sync round on the ledger.
        /// </summary>
        /// <param name="round">The round for which the deltas are desired.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> SetSyncRoundAsync(System.Threading.CancellationToken cancellationToken, ulong round)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/ledger/sync/{round}");
            urlBuilder_.Replace("{round}", System.Uri.EscapeDataString(ConvertToString(round, System.Globalization.CultureInfo.InvariantCulture)));
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given TEAL source code in plain text, return base64 encoded program bytes and
        /// base32 SHA512_256 hash of program bytes (Address style). This endpoint is only
        /// enabled when a node's configuration file sets EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="sourcemap">When set to `true`, returns the source map of the program as a JSON. Defaults to
        /// `false`.</param>
        /// <param name="source">TEAL source code to be compiled</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<CompileResponse> TealCompileAsync(System.IO.Stream source, bool? sourcemap = null)
        {
            return TealCompileAsync(System.Threading.CancellationToken.None, source, sourcemap);
        }

        /// <summary>>Given TEAL source code in plain text, return base64 encoded program bytes and
        /// base32 SHA512_256 hash of program bytes (Address style). This endpoint is only
        /// enabled when a node's configuration file sets EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="sourcemap">When set to `true`, returns the source map of the program as a JSON. Defaults to
        /// `false`.</param>
        /// <param name="source">TEAL source code to be compiled</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<CompileResponse> TealCompileAsync(System.Threading.CancellationToken cancellationToken, System.IO.Stream source, bool? sourcemap = null)
        {
            if (source == null) throw new System.ArgumentNullException("source");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/teal/compile?");
            if (sourcemap != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("sourcemap") + "=").Append(System.Uri.EscapeDataString(ConvertToString(sourcemap, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    var content_ = new System.Net.Http.StreamContent(source);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain");
                    request_.Content = content_;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<CompileResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Given the program bytes, return the TEAL source code in plain text. This
        /// endpoint is only enabled when a node's configuration file sets
        /// EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="source">TEAL program binary to be disassembled</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<DisassembleResponse> TealDisassembleAsync(System.IO.Stream source)
        {
            return TealDisassembleAsync(System.Threading.CancellationToken.None, source);
        }

        /// <summary>>Given the program bytes, return the TEAL source code in plain text. This
        /// endpoint is only enabled when a node's configuration file sets
        /// EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="source">TEAL program binary to be disassembled</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<DisassembleResponse> TealDisassembleAsync(System.Threading.CancellationToken cancellationToken, System.IO.Stream source)
        {
            if (source == null) throw new System.ArgumentNullException("source");
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/teal/disassemble");
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    var content_ = new System.Net.Http.StreamContent(source);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain");
                    request_.Content = content_;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<DisassembleResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Executes TEAL program(s) in context and returns debugging information about the
        /// execution. This endpoint is only enabled when a node's configuration file sets
        /// EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="request">Transaction (or group) and any accompanying state-simulation data.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<DryrunResponse> TealDryrunAsync(DryrunRequest request)
        {
            return TealDryrunAsync(System.Threading.CancellationToken.None, request);
        }

        /// <summary>>Executes TEAL program(s) in context and returns debugging information about the
        /// execution. This endpoint is only enabled when a node's configuration file sets
        /// EnableDeveloperAPI to true.
        /// </summary>
        /// <param name="request">Transaction (or group) and any accompanying state-simulation data.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<DryrunResponse> TealDryrunAsync(System.Threading.CancellationToken cancellationToken, DryrunRequest request)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/teal/dryrun");
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    System.Net.Http.ByteArrayContent content_ = new System.Net.Http.ByteArrayContent(Encoder.EncodeToMsgPackOrdered(request));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/msgpack");
                    request_.Content = content_;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<DryrunResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Returns OK if experimental API is enabled.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> ExperimentalCheckAsync()
        {
            return ExperimentalCheckAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Returns OK if experimental API is enabled.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> ExperimentalCheckAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/experimental");
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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Gets the current timestamp offset.
        /// </summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<GetBlockTimeStampOffsetResponse> GetBlockTimeStampOffsetAsync()
        {
            return GetBlockTimeStampOffsetAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>>Gets the current timestamp offset.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<GetBlockTimeStampOffsetResponse> GetBlockTimeStampOffsetAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/devmode/blocks/offset");
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
                            var objectResponse_ = await ReadObjectResponseAsync<GetBlockTimeStampOffsetResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
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




        /// <summary>Sets the timestamp offset (seconds) for blocks in dev mode. Providing an offset
        /// of 0 will unset this value and try to use the real clock for the timestamp.
        /// </summary>
        /// <param name="offset">The timestamp offset for blocks in dev mode.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> SetBlockTimeStampOffsetAsync(ulong offset)
        {
            return SetBlockTimeStampOffsetAsync(System.Threading.CancellationToken.None, offset);
        }

        /// <summary>>Sets the timestamp offset (seconds) for blocks in dev mode. Providing an offset
        /// of 0 will unset this value and try to use the real clock for the timestamp.
        /// </summary>
        /// <param name="offset">The timestamp offset for blocks in dev mode.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> SetBlockTimeStampOffsetAsync(System.Threading.CancellationToken cancellationToken, ulong offset)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("v2/devmode/blocks/offset/{offset}");
            urlBuilder_.Replace("{offset}", System.Uri.EscapeDataString(ConvertToString(offset, System.Globalization.CultureInfo.InvariantCulture)));
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();

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
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_, cancellationToken).ConfigureAwait(false);
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
                    // A handful of algod endpoints (e.g. /health, /ready, /metrics) are typed as returning a
                    // plain string but actually respond 200 with an empty (or literal "null") body on success -
                    // that's a valid "no content" success, not a deserialization failure, so callers shouldn't
                    // see it as Object == null (which every generated method treats as an error).
                    if (typedBody == null && typeof(T) == typeof(string))
                    {
                        typedBody = (T)(object)string.Empty;
                    }
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
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        var typedBody = Algorand.Utils.Encoder.DecodeFromMsgPack<T>(bytes);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                        //using Stream responseStream = new System.IO.MemoryStream(bytes);
                        //using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        //using var reader = new MessagePackReader(responseStream);
                        //var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        //var typedBody = serializer.Deserialize<T>(reader);
                        //return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                    else
                    {
                        // var bytes = await response.Content.ReadAsByteArrayAsync();

                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var streamReader = new System.IO.StreamReader(responseStream))
                        using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                        {
                            var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                            var typedBody = serializer.Deserialize<T>(jsonTextReader);
                            // See the ReadResponseAsString branch above: a null body for a string-typed endpoint
                            // (e.g. /health, /ready, /metrics) is a valid empty-body success, not an error.
                            if (typedBody == null && typeof(T) == typeof(string))
                            {
                                typedBody = (T)(object)string.Empty;
                            }
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
