using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using System.Threading.Tasks;

namespace Algorand.Algod
{
    public class FundAccount
    {
        public static async Task<PostTransactionsResponse> PayTo(Address receiver, ulong amount, Account sender, DefaultApi defaultApi)
        {
            var tx = new PaymentTransaction() { amount = amount, Receiver = receiver, Sender = sender.Address };
            await tx.FillInParams(defaultApi);
            var signedTx = tx.Sign(sender);
            return await Algorand.Utils.Utils.SubmitTransaction(defaultApi, signedTx);
        }
    }
}
