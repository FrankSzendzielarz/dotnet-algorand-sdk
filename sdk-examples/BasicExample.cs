using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;


namespace sdk_examples
{
    class BasicExample
    {
        public static async Task Main(string[] args)
        {
            try
            {
                // This boilerplate creates an Account object with a private key represented by a mnemnonic.
                //
                //   If using Sandbox, please use the following commands to replace the below mnemonic:
                //   algokit goal account list
                //   algokit goal account export -a <address>
                //
                // If you want to use this mnemonic, fund this account ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY using algokit first.
                // Find your account first `algokit goal account list`
                // If your account is `S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`, then run:
                //   `algokit goal clerk send -t ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY -a 100000000 -f S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`

                var src = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");

                var DEST_ADDR = "5KFWCRTIJUMDBXELQGMRBGD2OQ2L3ZQ2MB54KT2XOQ3UWPKUU4Y7TQ4X7U";

                using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.DockerNet);
                var algod = new AlgodClient(httpClient);

                var supply = await algod.GetSupplyAsync();
                Console.WriteLine("Total Algorand Supply: " + supply.TotalMoney);
                Console.WriteLine("Online Algorand Supply: " + supply.OnlineMoney);

                var accountInfo = await algod.AccountInformationAsync(src.Address.ToString(), null, null);

                Console.WriteLine($"Account Balance: {accountInfo.Amount} microAlgos");

                var transParams = await algod.TransactionParamsAsync();

                var amount = Utils.AlgosToMicroalgos(1);
                var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(src.Address, new Address(DEST_ADDR), amount, "pay message", transParams);
                var signedTx = tx.Sign(src);

                Console.WriteLine("Signed transaction with transaction ID: " + signedTx.Tx.TxID());

                // send the transaction to the network
                var id = await Utils.SubmitTransaction(algod, signedTx);
                Console.WriteLine("Successfully sent tx with id: " + id.Txid);

                var resp = await Utils.WaitTransactionToComplete(algod, id.Txid);
                Console.WriteLine("Confirmed Round is: " + resp.ConfirmedRound);
            }
            catch (ApiException<ErrorResponse> exc)
            {
                Console.WriteLine("Error: " + exc.Result.Message);
            }
        }
    }
}

