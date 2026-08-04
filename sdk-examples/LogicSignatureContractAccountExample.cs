using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    class LogicSignatureContractAccountExample
    {
        public static async Task Main(params string[] args)
        {
            // If you want to use this mnemonic, fund this account NTJZXOAERQEWRQEDGD2HO4I67EYA6HICDRHPUWNUKQZUQCGM65G7SKZA4U using algokit first.
            // Find your account first `algokit goal account list`
            // If your account is `S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`, then run:
            //   `algokit goal clerk send -t NTJZXOAERQEWRQEDGD2HO4I67EYA6HICDRHPUWNUKQZUQCGM65G7SKZA4U -a 100000000 -f S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";


            var destAcc = new Account("pole pudding actor purpose spend agree erode account discover chapter adapt supreme excite lamp gospel guilt helmet wrestle meat sustain orphan certain mixture able disease");
            
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            var transParams = await algod.TransactionParamsAsync();
            

            // logic sig that simply approves
            byte[] program = Convert.FromBase64String("ASABASI=");
            LogicsigSignature lsig = new LogicsigSignature(program);
            Console.WriteLine("Escrow address: " + lsig.Address.ToString());

            //pay to the escrow address
            var payEscrow = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(destAcc.Address, lsig.Address, 11000000, "send algo to contract", transParams);
            var payEscrowTx = payEscrow.Sign(destAcc);

            // pay from the escrow address
            var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(lsig.Address, destAcc.Address, 10000000, "draw algo from contract", transParams);
            if (!lsig.Verify(tx.Sender))
            {
                Console.WriteLine("Verification failed");
                Environment.Exit(0);
            }

            try
            {
                await Utils.SubmitTransaction(algod, payEscrowTx);//Fund the escrow

                var signedTx = tx.Sign(lsig); // sign the payment from the logic signature address with the logic sig itself
                var id = await Utils.SubmitTransaction(algod, signedTx);
                Console.WriteLine("Successfully sent tx logic sig tx id: " + id);
                Console.WriteLine("Confirmed Round is: " +
                    Utils.WaitTransactionToComplete(algod, id.Txid).Result.ConfirmedRound);
            }
            catch (ApiException<ErrorResponse> e)
            {
                // This is expected, but should give us an informative error message.
                Console.WriteLine("Exception when calling algod#sendTransaction: " + e.Result.Message);
            }
        }
    }
}


