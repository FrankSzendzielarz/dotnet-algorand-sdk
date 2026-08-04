using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace sdk_examples
{
    // This SDK example shows how to read node and chain state: node status, waiting for
    // new rounds, and fetching blocks with their transactions, hashes and transaction ids.
    class NodeAndBlockExample
    {
        public static async Task Main(params string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            try
            {
                // Current node status
                var status = await algod.GetStatusAsync();
                Console.WriteLine($"Last round:        {status.LastRound}");
                Console.WriteLine($"Consensus version: {status.LastVersion}");
                Console.WriteLine($"Time since last round: {status.TimeSinceLastRound / 1_000_000} ms");

                // Network / genesis information
                var transParams = await algod.TransactionParamsAsync();
                Console.WriteLine($"Genesis ID:   {transParams.GenesisId}");
                Console.WriteLine($"Genesis hash: {Convert.ToBase64String(transParams.GenesisHash)}");
                Console.WriteLine($"Min fee:      {transParams.MinFee} microAlgos");

                // Block until the next round is produced (long-polls the node)
                var next = await algod.WaitForBlockAsync(status.LastRound);
                Console.WriteLine($"New round arrived: {next.LastRound}");

                // Fetch a whole block, including its transactions
                var round = next.LastRound;
                var certifiedBlock = await algod.GetBlockAsync(round);
                var block = certifiedBlock.Block;
                Console.WriteLine($"Block {block.Round} contains {block.Transactions?.Count ?? 0} transaction(s)");

                // Block hash and the ids of the transactions inside the block
                var blockHash = await algod.GetBlockHashAsync(round);
                Console.WriteLine($"Block hash: {blockHash.Blockhash}");

                var txids = await algod.GetBlockTxidsAsync(round);
                foreach (var txid in txids.Blocktxids.Take(10))
                    Console.WriteLine($"  txid: {txid}");
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Error calling algod: " + e.Result.Message);
            }
        }
    }
}

