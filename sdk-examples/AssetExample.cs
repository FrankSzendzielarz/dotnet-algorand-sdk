using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace sdk_examples
{
    class AssetExample
    {
        public static async Task Main(params string[] args)
        {
            // If you want to use this mnemonic, fund this account 6IYAJY3KRIK5GZ326RDAY7ZATAOBY36BGUTYDDXAW44JPIGAY5GQZWY6NE using algokit first.
            // Find your account first `algokit goal account list`
            // If your account is `S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`, then run:
            //   `algokit goal clerk send -t 6IYAJY3KRIK5GZ326RDAY7ZATAOBY36BGUTYDDXAW44JPIGAY5GQZWY6NE -a 100000000 -f S2Z6G7MMDIIHXTYA4T63RLAZKVTTT4P2Q6VYDSE746YKGGMAVG5KWGQGJI`

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            // These will be our three accounts for the demonstration.
            //
            //   If using Sandbox, please use the following commands to replace the below mnemonics:
            //   ./sandbox goal account list
            //   ./sandbox goal account export -a <address>
            //   Repeat the second command foreach account.
            Account acct1 = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
            Account acct2 = new Account("pole pudding actor purpose spend agree erode account discover chapter adapt supreme excite lamp gospel guilt helmet wrestle meat sustain orphan certain mixture able disease");
            Account acct3 = new Account("cricket outside win obey swap useless spread detail shallow sunset birth fall innocent deal kiwi bounce okay rude social book brush lava correct abandon innocent");

            // Create a connection to our sandbox node
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            // PART 1: CREATE ASSET
            var ap = new AssetParams()
            {
                Name = "Asset Name",
                UnitName = "AST",
                Total = 10000,
                Manager = acct1.Address
            };
            var assetCreateTx = new AssetCreateTransaction()
            {
                AssetParams = ap
            };
            var tx = await MakeTransaction(assetCreateTx, acct1, algod) as AssetCreateTransaction;
            var assetId = (ulong)tx.AssetIndex;
            Console.WriteLine($"Asset id is {assetId}\n");
            var ast = await algod.GetAssetByIDAsync(assetId);

            // PART 2: UPDATE ASSET PARAMETERS
            // Now that we have a reference to our asset, we can update its parameters
            // Let's change the asset manager for example
            Console.WriteLine($"Current manager: {ast.Params.Manager}\n");
            ast.Params.Manager = acct2.Address;

            var assetUpdateTx = new AssetUpdateTransaction()
            {
                AssetParams = ast.Params
            };
            await MakeTransaction(assetUpdateTx, acct1, algod); 

            ast = await algod.GetAssetByIDAsync(assetId);
            Console.WriteLine($"Current manager: {ast.Params.Manager}\n");

            // PART 3: OPT INTO RECEIVING THE ASSET
            var assetOptInTx = new AssetAcceptTransaction()
            {
                XferAsset = assetId,
                AssetReceiver = acct3.Address
            };
            await MakeTransaction(assetOptInTx, acct3, algod);

            // PART 4: TRANSFER ASSET
            var assetTransferTx = new AssetTransferTransaction()
            {
                XferAsset = assetId,
                AssetReceiver = acct3.Address,
                AssetAmount = 100
            };
            await MakeTransaction(assetTransferTx, acct1, algod);

            // PART 5: FREEZE ASSET
            var assetFreezeTx = new AssetFreezeTransaction()
            {
                AssetFreezeId = assetId,
                FreezeState = true,
                FreezeTarget = acct3.Address,
            };
            await MakeTransaction(assetFreezeTx, acct2, algod);

            // PART 6: ASSET CLAWBACK
            var artx = new AssetClawbackTransaction()
            {
                XferAsset = assetId,
                AssetSender = acct3.Address,
                AssetReceiver = acct1.Address,
                AssetAmount = 10
            };
            await MakeTransaction(artx, acct2, algod);

            // PART 7: DESTROY ASSET
            var assetDestroyTx = new AssetDestroyTransaction()
            {
                AssetIndex = assetId,
            };
            await MakeTransaction(assetDestroyTx, acct2, algod);
        }

        static async Task<Transaction> MakeTransaction(Transaction tx, Account sender, DefaultApi apiInstance)
        {
            Console.WriteLine("Making transaction: " + tx.GetType().Name);

            try
            {
                // Get the prameters for constructing a new transaction.
                // Required before every transaction in order to get the latest parameters from the blockchain.
                var transParams = await apiInstance.TransactionParamsAsync();

                tx.SetFee(transParams.Fee);
                tx.FirstValid = transParams.LastRound;
                tx.LastValid = transParams.LastRound + 1000;
                tx.GenesisHash = new Digest(transParams.GenesisHash);
                tx.GenesisId = transParams.GenesisId;
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine("Could not get transaction parameters response: " + e.Message);
                Environment.Exit(1);
            }

            tx.Sender = sender.Address;

            var signedTx = tx.Sign(sender);

            try
            {
                var response = await Utils.SubmitTransaction(apiInstance, signedTx);
                var result = await Utils.WaitTransactionToComplete(apiInstance, response.Txid);
                Console.WriteLine($"Transaction ID: {response.Txid}\nConfirmed round: {result.ConfirmedRound}\n");

                if (result is AssetCreateTransaction)
                { 
                    return result as AssetCreateTransaction;
                }

                return result;
            }
            catch (ApiException<ErrorResponse> e)
            {
                Console.Error.WriteLine(e.Result.Message);
                Environment.Exit(1);
            }

            return null;
        }
    }
}

