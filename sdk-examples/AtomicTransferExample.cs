using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace sdk_examples
{
    class AtomicTransferExample
    {
        public static async Task Main(params string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            string DEST_ADDR1 = "5KFWCRTIJUMDBXELQGMRBGD2OQ2L3ZQ2MB54KT2XOQ3UWPKUU4Y7TQ4X7U";
            string DEST_ADDR2 = "7AWVMMX56YR4IG62WRZMULUUQ7ZWQNAKVBGSWMXUYRRWEAOZ27XVRORTGM";

            var srcAccount = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");

            // Get a connection to the Sandbox node
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            var transParams = await algod.TransactionParamsAsync();

            // Create a transaction group
            var amount = Utils.AlgosToMicroalgos(1);
            var tx1 = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(srcAccount.Address, new Address(DEST_ADDR1), amount, "pay message", transParams);
            var tx2 = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(srcAccount.Address, new Address(DEST_ADDR2), amount, "pay message", transParams);

            var groupID = TxGroup.ComputeGroupID(new Transaction[] { tx1, tx2 });
            tx1.Group = groupID;
            tx2.Group = groupID;

            var signedTx = tx1.Sign(srcAccount);
            var signedTx2 = tx2.Sign(srcAccount);

            var signedTxGroup = new List<SignedTransaction>() { signedTx, signedTx2 };

            try
            {
                var response = await algod.TransactionsAsync(signedTxGroup);
                var round = Utils.WaitTransactionToComplete(algod, response.Txid).Result.ConfirmedRound;
                Console.WriteLine($"Transaction ID: {response.Txid}\nConfirmed round: {round}");
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.WriteLine(e.Result.Message);
            }
        }
    }
}

