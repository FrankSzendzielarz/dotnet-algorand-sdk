using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public  class PaymentTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public byte[] Receiver;
        [Storage(StorageType.Protocol)]
        public ulong Amount;
        [Storage(StorageType.Protocol)]
        public byte[] CloseRemainderTo;
      


    }
}
