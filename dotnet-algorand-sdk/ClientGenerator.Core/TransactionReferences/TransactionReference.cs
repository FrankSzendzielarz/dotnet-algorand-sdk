using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public class TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public ulong Fee;
        [Storage(StorageType.Protocol)]
        public ulong FirstValid;
        [Storage(StorageType.Protocol)]
        public ulong LastValid;
        [Storage(StorageType.Protocol)]
        public AccountReference Sender;
        [Storage(StorageType.Protocol)]
        public byte[] TxType;
        [Storage(StorageType.Protocol)]
        public byte[] Lease;
        [Storage(StorageType.Protocol)]
        public byte[] Note;
        [Storage(StorageType.Protocol)]
        public byte[] RekeyTo;
        [Storage(StorageType.Protocol)]
        public ulong GroupIndex;
        [Storage(StorageType.Protocol)]
        public byte[] TxID;

        //TODO - Accounts list?
        //[Storage(StorageType.Protocol)]
        //public byte[] Accounts;

        //TODO - App arguments list?
        //[Storage(StorageType.Protocol)]
        //public byte[] AppArgument;

        //TODO - Foreign apps list?
        //[Storage(StorageType.Protocol)]
        //public byte[] ForeignApps;

        //TODO - Foreign assets list?
        //[Storage(StorageType.Protocol)]
        //public byte[] ForeignApps;




    }


}
