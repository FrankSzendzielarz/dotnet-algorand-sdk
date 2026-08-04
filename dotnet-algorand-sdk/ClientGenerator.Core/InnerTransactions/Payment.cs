namespace AVM.ClientGenerator.Core
{
    public  sealed class Payment : InnerTransaction
    {
        
        public byte[] Receiver { get; private set; }

        public ulong Amount { get; private set; }

        public byte[] CloseRemainderTo { get; private set; }

        private Payment() { }

        public Payment(AccountReference receiver, ulong amount, AccountReference closeRemainderTo = null, ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null, ulong? txType = 0, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base("pay",fee,firstValid,lastValid,sender,lease,note,rekeyTo)
        {
         
        }

   
    }
}
