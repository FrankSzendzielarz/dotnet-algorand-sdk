using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AVM.ClientGenerator.Clients
{
    public static class ContractUtilities
    {


        private static async Task<ulong?> CreateApp(DefaultApi client, Account creator, TEALProgram approvalProgram,
         TEALProgram clearProgram, ulong globalInts, ulong globalBytes, ulong localInts, ulong localBytes)
        {

            ulong extraPages = ((ulong)approvalProgram.Bytes.Length + (ulong)clearProgram.Bytes.Length) / 2048;

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
                ExtraProgramPages = extraPages,
            };


            var signedTx = tx.Sign(creator);
            var id = await Utils.SubmitTransaction(client, signedTx);
            var resp = await Utils.WaitTransactionToComplete(client, id.Txid) as ApplicationCreateTransaction;
            return resp.ApplicationIndex;

        }

        /// <summary>
        /// Deploy a Smart Contract on the Algorand Network represented by the Algod node
        /// </summary>
        /// <param name="contract">The contract to deploy</param>
        /// <param name="creator">The Account deploying the Smart Contract</param>
        /// <param name="api">The client reprsenting the Algod node</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<ulong?> Deploy(this Core.ICompiledContract contract, Account creator, DefaultApi api)
        {
            string approvalProgram = "";
            string clearStateProgram = "";

            CompileResponse response;
            if (!String.IsNullOrWhiteSpace(contract.ApprovalProgram))
            {
                using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(contract.ApprovalProgram)))
                {

                    response = await api.TealCompileAsync(datams);
                    approvalProgram = response.Result;
                }
            }
            if (!String.IsNullOrWhiteSpace(contract.ClearState))
            {
                using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(contract.ClearState)))
                {

                    response = await api.TealCompileAsync(datams);
                    clearStateProgram = response.Result;
                }
            }
            else
            {
                using (var datams = new MemoryStream(Encoding.UTF8.GetBytes("#pragma version 7 \n int 1")))
                {

                    response = await api.TealCompileAsync(datams);
                    clearStateProgram = response.Result;
                }
            }


            var a = new TEALProgram(approvalProgram);
            var c = new TEALProgram(clearStateProgram);
            var aid = await CreateApp(api, creator, a, c, (ulong)contract.NumberOfGlobalUInts, (ulong)contract.NumberOfGlobalByteSlices, (ulong)contract.NumberOfLocalUInts, (ulong)contract.NumberOfLocalByteSlices);
            if (aid == null) throw new Exception("Failed to deploy.");

            return aid;

        }

        /// <summary>
        /// Fund a contract
        /// </summary>
        /// <param name="account">Account to fund from </param>
        /// <param name="microAlgos">Microalgos to fund</param>
        public static async Task FundContract(this Account account, ulong appId, ulong microAlgos, DefaultApi api)
        {

            var transParams = await api.TransactionParamsAsync();
            var appToFund = Address.ForApplication(appId);

            var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(account.Address, appToFund, microAlgos, $"fund {appId}", transParams);
            var signedTx = tx.Sign(account);

            var id = await Utils.SubmitTransaction(api, signedTx);
            var resp = await Utils.WaitTransactionToComplete(api, id.Txid);

            if (!resp.Committed) { throw new Exception("FundContract did not complete."); }

        }


        /// <summary>
        /// Opt In an Account to a Smart Contract, establishing a relationship between
        /// the Account and the Smart Contract containing "Local State"
        /// </summary>
        /// <param name="account">The account to opt in to the contract</param>
        /// <param name="appId">The contract id</param>
        /// <param name="api">The client to the Algod node</param>
        /// <returns></returns>
        public static async Task ContractOptIn(this Account account, ulong appId, DefaultApi api)
        {

            var transParams = await api.TransactionParamsAsync();
            var tx = new ApplicationOptInTransaction()
            {
                Sender = account.Address,
                Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000,
                FirstValid = transParams.LastRound,
                LastValid = transParams.LastRound + 1000,
                GenesisId = transParams.GenesisId,
                GenesisHash = new Digest(transParams.GenesisHash),
                ApplicationId = appId
            };
            var signedTx = tx.Sign(account);

            var id = await Utils.SubmitTransaction(api, signedTx);
            var resp = await Utils.WaitTransactionToComplete(api, id.Txid);

            if (!resp.Committed) { throw new Exception("OptIn did not complete."); }


        }

        public static async Task DeleteApp(this Account sender, ulong applicationId, DefaultApi api)
        {
            var transParams = await api.TransactionParamsAsync();
            var tx = new ApplicationDeleteTransaction()
            {
                Sender = sender.Address,
                Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000,
                FirstValid = transParams.LastRound,
                LastValid = transParams.LastRound + 1000,
                GenesisId = transParams.GenesisId,
                GenesisHash = new Digest(transParams.GenesisHash),
                ApplicationId = applicationId
            };

            var signedTx = tx.Sign(sender);
            var id = await Utils.SubmitTransaction(api, signedTx);
            var resp = await Utils.WaitTransactionToComplete(api, id.Txid);

            if (!resp.Committed) { throw new Exception("Deletion did not complete."); }
        }
        public static async Task ClearApp(this Account sender, ulong applicationId, DefaultApi api)
        {
            var transParams = await api.TransactionParamsAsync();
            var tx = new ApplicationClearStateTransaction()
            {
                Sender = sender.Address,
                Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000,
                FirstValid = transParams.LastRound,
                LastValid = transParams.LastRound + 1000,
                GenesisId = transParams.GenesisId,
                GenesisHash = new Digest(transParams.GenesisHash),
                ApplicationId = applicationId,
            };

            var signedTx = tx.Sign(sender);
            var id = await Utils.SubmitTransaction(api, signedTx);
            var resp = await Utils.WaitTransactionToComplete(api, id.Txid) as ApplicationClearStateTransaction;

            if (!resp.Committed) { throw new Exception("Deletion did not complete."); }

        }
    }


}
