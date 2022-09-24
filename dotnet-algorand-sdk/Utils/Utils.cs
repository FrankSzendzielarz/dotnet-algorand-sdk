
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Algorand.Utils
{
    /// <summary>
    /// Convenience methods for algorand sdk.
    /// </summary>
    public class Utils
    {
      
      
        /// <summary>
        /// utility function to wait on a transaction to be confirmed using algod v2 API
        /// </summary>
        /// <param name="instance">The algod api instance using algod v2 API</param>
        /// <param name="txID">transaction ID</param>
        /// <param name="timeout">how many rounds do you wish to check pending transactions for</param>
        /// <returns>The pending transaction response</returns>
        public static async Task<Transaction> WaitTransactionToComplete(Algod.DefaultApi instance, string txID, ulong timeout = 3) 
        {

            if (instance == null || txID == null || txID.Length == 0 || timeout < 0)
            {
                throw new ArgumentException("Bad arguments for waitForConfirmation.");
            }
            NodeStatusResponse nodeStatusResponse = await instance.GetStatusAsync();            
            var startRound = nodeStatusResponse.LastRound + 1;
            var currentRound = startRound;
            while (currentRound < (startRound + timeout))
            {
                var pendingInfo = await instance.PendingTransactionInformationAsync(txID,null) as Transaction;

                if (pendingInfo != null)
                {
                    if (pendingInfo.ConfirmedRound != null && pendingInfo.ConfirmedRound > 0)
                    {
                        // Got the completed Transaction
                        return pendingInfo;
                    }
                    if (pendingInfo.PoolError != null && pendingInfo.PoolError.Length > 0)
                    {
                        // If there was a pool error, then the transaction has been rejected!
                        throw new Exception("The transaction has been rejected with a pool error: " + pendingInfo.PoolError);
                    }
                }
                await instance.WaitForBlockAsync(currentRound);
                currentRound++;
            }
            throw new Exception("Transaction not confirmed after " + timeout + " rounds!");
        }
      


        /// <summary>
        /// encode and submit signed transaction using algod v2 api
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="signedTx"></param>
        /// <returns></returns>
        public static async Task<PostTransactionsResponse> SubmitTransaction(Algod.DefaultApi instance, SignedTransaction signedTx) //throws Exception
        {
                    
                return await instance.TransactionsAsync(new List<SignedTransaction> { signedTx });
         
        }
        
        /// <summary>
        /// encode and submit signed transactions using algod v2 api
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="signedTx"></param>
        /// <returns></returns>
        public static async Task<PostTransactionsResponse> SubmitTransactions(Algod.DefaultApi instance, IEnumerable<SignedTransaction> signedTxs) //throws Exception
        {
            List<byte> byteList = new List<byte>();
            foreach (var signedTx in signedTxs)
            {
                byteList.AddRange(Algorand.Encoder.EncodeToMsgPack(signedTx));
            }
            using MemoryStream ms = new MemoryStream(byteList.ToArray());
            return await instance.TransactionsAsync(ms);
        }
        
        public static ulong AlgosToMicroalgos(double algos)
        {
            return Convert.ToUInt64(Math.Floor(algos * 1000000));
        }
        public static double MicroalgosToAlgos(ulong microAlgos)
        {
            return microAlgos / 1000000.0;
        }
        
       
        /// <summary>
        /// Generate a 32 bytes string for asset metadata hash
        /// </summary>
        /// <returns>a 32 bytes string</returns>
        public static string GetRandomAssetMetaHash()
        {
            Random rd = new Random();
            byte[] bts = new byte[32];
            rd.NextBytes(bts);
            //var base64 = Convert.ToBase64String(bts);
            return Convert.ToBase64String(bts);
        }
       
    
       
      

      
 

      

     
       
    

      
       
      
     
        public async static Task<Algod.Model.DryrunResponse> GetDryrunResponse(Algod.DefaultApi client, SignedTransaction stxn, byte[] source = null)
        {
            List<DryrunSource> sources = new List<DryrunSource>();
            List<SignedTransaction> stxns = new List<SignedTransaction>();
            //compiled 
            if (source is null)
            {
                stxns.Add(stxn);
            }
            // source
            else
            {
                sources.Add(new Algod.Model.DryrunSource(){
                    FieldName= "lsig",
                    Source= Encoding.UTF8.GetString(source), TxnIndex= 0 });
                stxns.Add(stxn);
            }
            if (sources.Count < 1) sources = null;
            return await client.TealDryrunAsync(new DryrunRequest() { Txns = stxns, Sources = sources });
        }

        internal static byte[] CombineBytes(byte[] b1, byte[] b2)
        {
            byte[] ret = new byte[b1.Length + b2.Length];
            Buffer.BlockCopy(b1, 0, ret, 0, b1.Length);
            Buffer.BlockCopy(b2, 0, ret, b1.Length, b2.Length);
            return ret;
        }
    }
}
