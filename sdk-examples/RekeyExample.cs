using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    class RekeyExample
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var ALGOD_API_ADDR = "http://localhost:4001/";
                var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

                // If you want to use this mnemonic, fund this account ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY using algokit first.
                // Find your account first `algokit goal account list`
                // If your account is `ADB66D3IQDQCH76LTTE7QG2YTPEHPQRUKZBYFP3KTOHD3JUIHYHBOMPCIU`, then run:
                //   `algokit goal clerk send -t ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY -a 100000000 -f ADB66D3IQDQCH76LTTE7QG2YTPEHPQRUKZBYFP3KTOHD3JUIHYHBOMPCIU`

                Account acct1 = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
                Account acct2 = new Account();
                Account acct3 = new Account();

                var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
                var algod = new AlgodClient(httpClient);

                await FundAccount.PayTo(acct3.Address, 1_000_000, acct1, algod);


                var transParams = await algod.TransactionParamsAsync();

                ulong amount = 200000;

                var rekeyAcct3ToAcct1tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct3.Address, acct2.Address, amount, "pay message", transParams);
                try
                {
                    // this uses the payment transaction to rekey to Acct3 to use Acct1 signing keys
                    rekeyAcct3ToAcct1tx.RekeyTo = acct2.Address;
                    var signedTx = rekeyAcct3ToAcct1tx.Sign(acct3);
                    await Utils.SubmitTransaction(algod, signedTx);

                    // let's try it again with the acct3 signing key
                    var failToExecuteTxWithAcct3Signature = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct3.Address, acct2.Address, amount, "pay message", transParams);
                    signedTx = failToExecuteTxWithAcct3Signature.Sign(acct3);
                    await Utils.SubmitTransaction(algod, signedTx); //this should fail

                }
                catch (ApiException<ErrorResponse> e)
                {
                    // we should get here as acct3 cannot sign for his address any more
                    Console.WriteLine($"Tx was correctly not submitted to the blockchain: {e.Result.Message}");
                }

                // another way is to set the rekey address on object account
                acct2.RekeyedTo = acct3.Address;

                // let's try it again with the acct3 signing key
                var signWithAcct2WithRekeyInfo = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct3.Address, acct2.Address, amount, "pay message", transParams);
                var signWithAcct2WithRekeyInfoSigned = signWithAcct2WithRekeyInfo.Sign(acct2);
                await Utils.SubmitTransaction(algod, signWithAcct2WithRekeyInfoSigned); //this will be ok, as acct2 acts now as acct3 public address with acct2 signing keys


                //rekey it back
                var rekeyBackTx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct3.Address, acct2.Address, amount, "pay message", transParams);
                rekeyBackTx.RekeyTo = acct3.Address;
                var rekeyBackTxSigned = rekeyBackTx.Sign(acct2);
                await Utils.SubmitTransaction(algod, rekeyBackTxSigned);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling Algod#TransactionParams: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}

