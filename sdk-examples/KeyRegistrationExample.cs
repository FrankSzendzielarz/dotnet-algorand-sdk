using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    // This SDK example shows how to register an account online for consensus participation
    // (KeyRegisterOnlineTransaction) and how to take it offline again (KeyRegisterOfflineTransaction).
    //
    // The participation keys (vote key, selection key, state proof key) must be generated on the
    // node that will participate in consensus, e.g.:
    //   algokit goal account addpartkey -a <address> --roundFirstValid <first> --roundLastValid <last>
    //   algokit goal account partkeyinfo
    // The values below are placeholders - replace them with the output of `partkeyinfo`.
    class KeyRegistrationExample
    {
        public static async Task Main(params string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            // If you want to use this mnemonic, fund this account ENOB5LVPJ7FZ6TO2DWET2DEBBV4NZUY5ZFQ6G2YX6SIER7UYLAM5FHE6TY using algokit first.
            var account = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            var transParams = await algod.TransactionParamsAsync();

            // Placeholder participation keys - replace with real values from `algokit goal account partkeyinfo`.
            // Vote and selection keys are 32 bytes; the state proof key is 64 bytes (all base64 in partkeyinfo output).
            // Note: the network accepts any well-formed keys, but the account will only actually
            // participate in consensus if these match participation keys installed on a node.
            var votePk = new byte[32]; votePk[0] = 1;
            var selectionPk = new byte[32]; selectionPk[0] = 1;
            var stateProofPk = new byte[64]; stateProofPk[0] = 1;

            var onlineTx = new KeyRegisterOnlineTransaction()
            {
                Sender = account.Address,
                Fee = transParams.MinFee,
                FirstValid = transParams.LastRound,
                LastValid = transParams.LastRound + 1000,
                GenesisId = transParams.GenesisId,
                GenesisHash = new Digest(transParams.GenesisHash),
                Votepk = new ParticipationPublicKey(votePk),
                SelectionPk = new VRFPublicKey(selectionPk),
                StateProofPK = stateProofPk,
                // The voting-key validity window - must match the rounds the partkey was generated for.
                VoteFirst = transParams.LastRound,
                VoteLast = transParams.LastRound + 3_000_000,
                VoteKeyDilution = 1_732 // typically sqrt(VoteLast - VoteFirst)
            };

            try
            {
                var signedTx = onlineTx.Sign(account);
                var id = await Utils.SubmitTransaction(algod, signedTx);
                var resp = await Utils.WaitTransactionToComplete(algod, id.Txid);
                Console.WriteLine($"Account registered online in round: {resp.ConfirmedRound}");
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Error registering online: " + e.Result.Message);
            }

            // Taking the account offline again only needs the sender - no participation keys.
            var offlineTx = new KeyRegisterOfflineTransaction()
            {
                Sender = account.Address,
                Fee = transParams.MinFee,
                FirstValid = transParams.LastRound,
                LastValid = transParams.LastRound + 1000,
                GenesisId = transParams.GenesisId,
                GenesisHash = new Digest(transParams.GenesisHash)
            };

            try
            {
                var signedTx = offlineTx.Sign(account);
                var id = await Utils.SubmitTransaction(algod, signedTx);
                var resp = await Utils.WaitTransactionToComplete(algod, id.Txid);
                Console.WriteLine($"Account registered offline in round: {resp.ConfirmedRound}");
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Error registering offline: " + e.Result.Message);
            }
        }
    }
}


