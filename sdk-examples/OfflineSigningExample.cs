using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    // This SDK example shows a cold-wallet workflow:
    //  1. generate a brand new account and export its mnemonic,
    //  2. build an unsigned transaction online and serialize it to canonical msgpack,
    //  3. sign the transaction on an "offline device" (only bytes and the mnemonic cross the gap),
    //  4. bring the signed bytes back online and submit them.
    class OfflineSigningExample
    {
        public static async Task Main(params string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            // --- Key generation (do this on the offline device) -------------------------------
            var newAccount = new Account();
            Console.WriteLine("Generated address:  " + newAccount.Address);
            // Never print a mnemonic to the console/logs in real code — persist newAccount.ToMnemonic()
            // via a secure channel of your own choosing (e.g. an offline, encrypted secret store).

            // If you want to use this mnemonic, fund this account ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY using algokit first.
            var senderMnemonic = "arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken";
            var senderAddress = new Account(senderMnemonic).Address;

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            try
            {
                // --- Online: build the unsigned transaction ----------------------------------
                var transParams = await algod.TransactionParamsAsync();
                var amount = Utils.AlgosToMicroalgos(1);
                var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(senderAddress, newAccount.Address, amount, "offline signing demo", transParams);

                // Canonical msgpack bytes of the unsigned transaction - transfer these to the signer.
                byte[] unsignedBytes = Encoder.EncodeToMsgPackOrdered(tx);
                Console.WriteLine($"Unsigned transaction: {unsignedBytes.Length} bytes");

                // --- Offline: decode, verify, and sign ---------------------------------------
                var decodedTx = Encoder.DecodeFromMsgPack<Transaction>(unsignedBytes);
                Console.WriteLine($"Signing transaction with id: {decodedTx.TxID()}");

                var signer = new Account(senderMnemonic);
                var signedTx = decodedTx.Sign(signer);

                // Canonical msgpack bytes of the signed transaction - transfer these back online.
                byte[] signedBytes = Encoder.EncodeToMsgPackOrdered(signedTx);
                Console.WriteLine($"Signed transaction:   {signedBytes.Length} bytes");

                // --- Online: decode the signed bytes and submit -------------------------------
                var decodedSignedTx = Encoder.DecodeFromMsgPack<SignedTransaction>(signedBytes);
                var id = await Utils.SubmitTransaction(algod, decodedSignedTx);
                Console.WriteLine("Successfully sent tx with id: " + id.Txid);

                var resp = await Utils.WaitTransactionToComplete(algod, id.Txid);
                Console.WriteLine("Confirmed Round is: " + resp.ConfirmedRound);
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Error: " + e.Result.Message);
            }
        }
    }
}


