using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdk_examples
{
    // This SDK example shows how to use the algod simulate endpoint to dry-run a transaction
    // group without submitting it to the network. Simulation reports whether the group would
    // succeed, and (for app calls) how much opcode budget it consumes - useful for validating
    // transactions and debugging smart contract calls before spending fees.
    class SimulateTransactionExample
    {
        public static async Task Main(params string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            string DEST_ADDR = "5KFWCRTIJUMDBXELQGMRBGD2OQ2L3ZQ2MB54KT2XOQ3UWPKUU4Y7TQ4X7U";

            // If you want to use this mnemonic, fund this account ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY using algokit first.
            var srcAccount = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            var transParams = await algod.TransactionParamsAsync();

            var amount = Utils.AlgosToMicroalgos(1);
            var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(srcAccount.Address, new Address(DEST_ADDR), amount, "simulated payment", transParams);

            // Simulate a properly signed transaction.
            var signedTx = tx.Sign(srcAccount);

            var request = new SimulateRequest()
            {
                TxnGroups = new List<SimulateRequestTransactionGroup>()
                {
                    new SimulateRequestTransactionGroup() { Txns = new List<SignedTransaction>() { signedTx } }
                },
                // With AllowEmptySignatures you can simulate transactions before signing them
                // (wrap the unsigned transaction in a SignedTransaction without a signature).
                AllowEmptySignatures = true,
                // AllowMoreLogging = true,     // lift log limits when debugging app calls
                // ExtraOpcodeBudget = 20000,   // extra AVM budget per group while testing
            };

            try
            {
                var response = await algod.SimulateTransactionAsync(request);

                Console.WriteLine($"Simulated at round: {response.LastRound}");
                foreach (var group in response.TxnGroups)
                {
                    if (string.IsNullOrEmpty(group.FailureMessage))
                    {
                        Console.WriteLine($"Group would succeed. Transactions in group: {group.TxnResults.Count}");
                        var budget = group.TxnResults.Sum(r => (decimal)(r.AppBudgetConsumed ?? 0));
                        if (budget > 0)
                            Console.WriteLine($"App opcode budget consumed: {budget}");
                    }
                    else
                    {
                        Console.WriteLine($"Group would fail: {group.FailureMessage}");
                    }
                }

                // Nothing was submitted - the network state is unchanged.
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Error simulating transaction: " + e.Result.Message);
            }
        }
    }
}

