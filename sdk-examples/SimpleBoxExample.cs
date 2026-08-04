using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace sdk_examples
{
    class SimpleBoxExample
    {
        public static async Task Main(params string[] args)
        {
            string ALGOD_API_ADDR = "http://localhost:4001/";
            string ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Account creator = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
            Account user = new Account("pole pudding actor purpose spend agree erode account discover chapter adapt supreme excite lamp gospel guilt helmet wrestle meat sustain orphan certain mixture able disease");

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            // declare application state storage (immutable)
            ulong localInts = 0;
            ulong localBytes = 0;
            ulong globalInts = 0;
            ulong globalBytes =  0;

            // user declared approval program 
            string approvalProgramSource = TEALContractsForExamples.SimpleBox();
     
            // declare clear state program source
            string clearProgramSource = TEALContractsForExamples.SimpleBoxClear();

            CompileResponse approvalProgram;
            CompileResponse clearProgram;

            using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(approvalProgramSource)))
            {
                approvalProgram = await algod.TealCompileAsync(datams);
            }
            using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(clearProgramSource)))
            {
                clearProgram = await algod.TealCompileAsync(datams);
            }


            try
            {
                // create new application
                var appid = await CreateApp(algod, creator, new TEALProgram(approvalProgram.Result),
                    new TEALProgram(clearProgram.Result), globalInts, globalBytes, localInts, localBytes);

                // fund its MBR
                var transParams = await algod.TransactionParamsAsync();
                var amount = Utils.AlgosToMicroalgos(1);
                var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(creator.Address, Address.ForApplication(appid.Value), amount, "pay message", transParams);
                var signedTx = tx.Sign(creator);
                var id = await Utils.SubmitTransaction(algod, signedTx);
                var resp = await Utils.WaitTransactionToComplete(algod, id.Txid);
                Console.WriteLine("Successfully funded app with tx id: " + id.Txid+ " at round " + resp.ConfirmedRound);


                // call application without arguments
                await CallApp(algod, user, appid, null, new List<BoxRef>() {  new BoxRef() {  App= 0, Name=Encoding.UTF8.GetBytes("boxtest") } });

                // read box state of application 
                byte[] boxContents = await ReadBoxState(algod, appid.Value);

                string test = Encoding.UTF8.GetString(boxContents);

                Console.WriteLine("You have successefully arrived the end of this test, please press and key to exist.");
            }
            catch (ApiException e)
            {
                // This is generally expected, but should give us an informative error message.
                Console.WriteLine("Exception when calling algod#sendTransaction: " + e.Message);
            }
        }
    
  
        static async Task<ulong?> CreateApp(DefaultApi client, Account creator, TEALProgram approvalProgram,
            TEALProgram clearProgram, ulong globalInts, ulong globalBytes, ulong localInts, ulong localBytes)
        {
            try
            {
                var transParams = await client.TransactionParamsAsync();

                var tx = new ApplicationCreateTransaction()
                {
                    Sender = creator.Address,
                    Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000,
                    FirstValid = transParams.LastRound,
                    LastValid = transParams.LastRound + 1000,
                    GenesisId = transParams.GenesisId,
                    GenesisHash = new Digest(transParams.GenesisHash),
                    ApprovalProgram = approvalProgram,
                    ClearStateProgram = clearProgram,
                    GlobalStateSchema = new StateSchema() { NumUint = globalInts, NumByteSlice = globalBytes },
                    LocalStateSchema = new StateSchema() { NumUint = localInts, NumByteSlice = localBytes },
                    ExtraProgramPages =0
       
                };

                var dbg=Algorand.Utils.Encoder.EncodeToJson(tx);

                var signedTx = tx.Sign(creator);
                Console.WriteLine("Signed transaction with txid: " + signedTx.Tx.TxID());

                var id = await Utils.SubmitTransaction(client, signedTx);
                Console.WriteLine("Successfully sent tx with id: " + id.Txid);
                var resp = await Utils.WaitTransactionToComplete(client, id.Txid) as ApplicationCreateTransaction;
                Console.WriteLine("Application ID is: " + resp.ApplicationIndex.ToString());
                return resp.ApplicationIndex;
            }
            catch (Algorand.ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Exception when calling create application: " + e.Result.Message);
                return null;
            }
        }
        
        static async Task CallApp(DefaultApi client, Account sender, ulong? applicationId, List<byte[]> args, List<BoxRef> boxRefs )
        {
            try
            {
                var transParams = await client.TransactionParamsAsync();

                var tx = new ApplicationNoopTransaction()
                {
                    Sender = sender.Address,
                    Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000,
                    FirstValid = transParams.LastRound,
                    LastValid = transParams.LastRound + 1000,
                    GenesisId = transParams.GenesisId,
                    GenesisHash = new Digest(transParams.GenesisHash),
                    ApplicationId = applicationId.Value,
                    ApplicationArgs = args,

                    Boxes= boxRefs
                };

                var signedTx = tx.Sign(sender);
                Console.WriteLine("Signed transaction with txid: " + signedTx.Tx.TxID());
                var id = await Utils.SubmitTransaction(client, signedTx);
                Console.WriteLine("Successfully sent tx with id: " + id.Txid);
                var resp = await Utils.WaitTransactionToComplete(client, id.Txid) as ApplicationNoopTransaction;
                Console.WriteLine("Confirmed at round: " + resp.ConfirmedRound);
                Console.WriteLine(string.Format("Call Application({0}) success.", resp.ApplicationId));

            }
            catch (Algorand.ApiException<ErrorResponse> e)
            {
                Console.WriteLine("Exception when calling application: " + e.Result.Message);
            }
        }

        static public async Task<byte[]> ReadBoxState(DefaultApi client, ulong appId)
        {
            var boxResponse = await client.GetApplicationBoxByNameAsync(appId, "str:boxtest");

            return boxResponse.Value;
        }


    }
}


