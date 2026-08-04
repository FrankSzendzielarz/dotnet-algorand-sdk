using Algorand.Algod.Model.Transactions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Algorand.Algod.Model
{
    public partial class Account
    {
        public async Task<PostTransactionsResponse> MakePaymentTo(Address address, ulong amount, string note, DefaultApi algodApi)
        {
            TransactionParametersResponse trans = await algodApi.TransactionParamsAsync();
            var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(Address, address, amount, note, trans);
            var signed = tx.Sign(this);
            return await algodApi.TransactionsAsync(new List<SignedTransaction>() { signed });
        }
        public async Task<PostTransactionsResponse> MakeAssetTransferTo(Address address, ulong amount, ulong assetId, string note, DefaultApi algodApi)
        {
            TransactionParametersResponse trans = await algodApi.TransactionParamsAsync();
            var tx = new AssetTransferTransaction() { AssetAmount = amount, XferAsset = assetId, Note = Encoding.UTF8.GetBytes(note), AssetReceiver = address };
            tx.FillInParams(trans);
            var signed = tx.Sign(this);
            return await algodApi.TransactionsAsync(new List<SignedTransaction>() { signed });
        }
    }
}
