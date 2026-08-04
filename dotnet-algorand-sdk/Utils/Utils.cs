
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using System;
using System.Collections.Generic;
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
                    if (pendingInfo.ConfirmedRound > 0)
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
            byte[] bts = new byte[32];
            System.Security.Cryptography.RandomNumberGenerator.Fill(bts);
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


        /// <summary>
        /// Converts gossip delta state value string to byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DeltaValueStringToBytes(string data)
        {
            //return data.Select(c => (byte)c).ToArray();  // returns 83f77166d10 instead of 8a677166d10 
            //return Encoding.ASCII.GetBytes(data); // returns 83f77166d10 instead of 8a677166d10 
            //return Encoding.Latin1.GetBytes(data); // returns 83f77166d10 instead of 8a677166d10 

            return Encoding.ASCII.GetBytes(data);
        }
    }
}
