using Algorand.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Algorand.Algod.Model.Transactions;

namespace SerialisationTests
{
    [TestClass]
    public class PaymentTransactionTest
    {
        [TestMethod]
        public void PayTransaction()
        {
            PaymentTransaction txn = new PaymentTransaction(new Algorand.Address(), new Algorand.Address(), 3, "hi", 0, 1, "sandnet", "wGHE2Pwdvd7S12BL5FaOP20EGYesN73ktiC1qzkkit8=");
            

            var txnJson = Encoder.EncodeToJson(txn);

            var transaction = JsonConvert.DeserializeObject<Transaction>(txnJson);

            Assert.IsInstanceOfType(transaction, typeof(PaymentTransaction));
        }

      
    }
}
