using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    class LogicSignatureExample
    {
        public static async Task Main(params string[] args)
        {
            // If you want to use this mnemonic, fund this account ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY using algokit first.
            // Find your account first `algokit goal account list`
            // If your account is `S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`, then run:
            //   `algokit goal clerk send -t ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY -a 100000000 -f S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`

            string ALGOD_API_ADDR = "http://localhost:4001/";
            string ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Account acct1 = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
            Account acct2 = new Account("pole pudding actor purpose spend agree erode account discover chapter adapt supreme excite lamp gospel guilt helmet wrestle meat sustain orphan certain mixture able disease");

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            byte[] program = Convert.FromBase64String("ASABASI=");
            var lsig1 = new LogicsigSignature(program);
            lsig1.Sign(acct1);

            var contractSig = Convert.ToBase64String(lsig1.Sig.Bytes);
            var lsig2 = new LogicsigSignature(program, null, Convert.FromBase64String(contractSig));
            
            var transParams = await algod.TransactionParamsAsync();
            var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct1.Address, acct2.Address, 1000000, "draw algo with logic signature", transParams);

            var signedTx = tx.Sign(lsig2);

            try
            {
                var response = await Utils.SubmitTransaction(algod, signedTx);
                Console.WriteLine("Successfully sent tx logic sig tx id: " + response.Txid);
                Console.WriteLine("Confirmed Round is: " +
                        Utils.WaitTransactionToComplete(algod, response.Txid).Result.ConfirmedRound);
            }
            catch (ApiException e)
            {
                Console.WriteLine("Exception when calling algod#rawTransaction: " + e.Message);
            }
        }
    }
}


