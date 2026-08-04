using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    class DryrunDebuggingExample
    {
        public static async Task Main(params string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            // This boilerplate creates Account objects with a private key represented by a mnemnonic.
            //
            //   If using Sandbox, please use the following commands to replace the below mnemonic:
            //   ./sandbox goal account list
            //   ./sandbox goal account export -a <address>
            var acct1 = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
            var acct2 = new Account("pole pudding actor purpose spend agree erode account discover chapter adapt supreme excite lamp gospel guilt helmet wrestle meat sustain orphan certain mixture able disease");

            // Create a connection to our sandbox node
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            // The byte code of a very simple teal program. The TEAL
            // disassembly will appear in the dryrun call.
            byte[] program = Convert.FromBase64String("ASABASI=");

            // Use the program as a logic signature and sign the logic signature with an Account
            LogicsigSignature lsig = new LogicsigSignature(program, null);
            lsig.Sign(acct1);
            
            try
            {
                // Get a payment transaction based on current network parameters
                TransactionParametersResponse transParams = await algod.TransactionParamsAsync();
                var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct1.Address, acct2.Address, 1000000, "tx using in dryrun", transParams);

                // Sign the transaction using a logic signature
                var signedTx = tx.Sign(lsig);

                // "Dry run" the transaction and show what would have happened during logic sig evalution.
                // The output also includes the TEAL disassembly.
                var dryrunResponse2 = await Utils.GetDryrunResponse(algod, signedTx);
                Console.WriteLine("Dryrun source response : " + dryrunResponse2.ToJson());

            }
            catch (ApiException<ErrorResponse> apiException)
            {
                Console.WriteLine($"An error was returned by the Sandbox: {apiException.Result.Message}");
             
            }

           

            
              
       
        }
    }
}


