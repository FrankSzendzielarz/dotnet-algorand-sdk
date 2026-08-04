using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace sdk_examples
{
    class MultisigExample
    {
        public static async Task Main(string[] args)
        {
            try
            {
                // If you want to use this mnemonic, fund this account I3QJVFAQW6IY5SNXXSRQRGZPHZIH2MJHP6ZGGD27VI6CCHDCERRDHEPTOU using algokit first.
                // Find your account first `algokit goal account list`
                // If your account is `S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`, then run:
                //   `algokit goal clerk send -t I3QJVFAQW6IY5SNXXSRQRGZPHZIH2MJHP6ZGGD27VI6CCHDCERRDHEPTOU -a 100000000 -f S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`

                var ALGOD_API_ADDR = "http://localhost:4001/";
                var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

                // This boilerplate creates an Account object with a private key represented by a mnemnonic.
                //
                //   If using Sandbox, please use the following commands to replace the below mnemonic:
                //   ./sandbox goal account list
                //   ./sandbox goal account export -a <address>
                var acc1 = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
                Account acc2 = new Account("pole pudding actor purpose spend agree erode account discover chapter adapt supreme excite lamp gospel guilt helmet wrestle meat sustain orphan certain mixture able disease");
                Account acc3 = new Account("cricket outside win obey swap useless spread detail shallow sunset birth fall innocent deal kiwi bounce okay rude social book brush lava correct abandon innocent");
                var randomAccount = new Account();

                var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
                var algod = new AlgodClient(httpClient);

                // A multisig address is the hash of the following information
                // Note that the second argument (2) means in this case "2 of 3 signatures are required"
                MultisigAddress multiAddress = new MultisigAddress(1, 2, new List<byte[]> { acc1.Address.Bytes, acc2.Address.Bytes, acc3.Address.Bytes });

                // Send *to* the multisig address
                var transParams = await algod.TransactionParamsAsync();
                var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acc1.Address, multiAddress.ToAddress(), 110000, "to multsig", transParams);
                var signedTx = payment.Sign(acc1);
                var tx = await Utils.SubmitTransaction(algod, signedTx);
                await Utils.WaitTransactionToComplete(algod,tx.Txid);

                // now to send *from* the multi-address we need a certain number of signatures specified by the threshold
                transParams = await algod.TransactionParamsAsync();
                var payment2 = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(multiAddress.ToAddress(),randomAccount.Address, 110000, "from multisig", transParams);

                // sign with 2 addresses (2 of 3 threshold)
                var signedTx1 = payment2.Sign(multiAddress,acc1);
                var signedTx2 = payment2.Sign(multiAddress,acc2);
                signedTx = SignedTransaction.MergeMultisigTransactions(signedTx1, signedTx2);
               
                tx = await Utils.SubmitTransaction(algod, signedTx);
                var result= await Utils.WaitTransactionToComplete(algod, tx.Txid);

                // now let's check the account received the amount
                var accountInfo = await algod.AccountInformationAsync(randomAccount.Address.ToString(), null, null);
                Console.WriteLine($"For account address {randomAccount.Address} the account balance is {accountInfo.Amount}");



            }
            catch (ApiException<ErrorResponse> apiException)
            {
                Console.WriteLine($"An error was returned by the Sandbox: {apiException.Result.Message}");
            }
        }
    }
}


