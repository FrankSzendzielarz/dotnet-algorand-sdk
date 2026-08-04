namespace AVM.ClientGenerator.Core
{
    public  abstract class InnerTransaction
    {
        public ulong Fee { get; private set; }
        public ulong FirstValid { get; private set; }
        public ulong LastValid { get; private set; }
        public byte[] Sender { get; private set; }
        public string TxType { get; private set; }
        public byte[] Lease { get; private set; }
        public byte[] Note { get; private set; }
        public byte[] RekeyTo { get; private set; }
        public ulong GroupIndex { get; private set; }
        public byte[] TxID { get; private set; }
        protected InnerTransaction() { }
        public InnerTransaction(string txType, ulong? fee=0, ulong? firstValid=0, ulong? lastValid = 0, AccountReference sender = null, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null)
        {
         
        }

   
    }


}
