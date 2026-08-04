using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public class AppCallTransactionReference : TransactionReference
    {

        [Storage(StorageType.Protocol)]
        public ulong NumAppArgs;

        [Storage(StorageType.Protocol)]
        public ulong NumAccounts;

        [Storage(StorageType.Protocol)]
        public ulong NumAssets;

        [Storage(StorageType.Protocol)]
        public ulong NumApplications;

        [Storage(StorageType.Protocol)]
        public ulong GlobalNumUint;

        [Storage(StorageType.Protocol)]
        public ulong GlobalNumByteSlice;

        [Storage(StorageType.Protocol)]
        public ulong LocalNumUint;

        [Storage(StorageType.Protocol)]
        public ulong LocalNumByteSlice;

        [Storage(StorageType.Protocol)]
        public ulong ApplicationID;

        [Storage(StorageType.Protocol)]
        public ulong OnCompletion;

        [Storage(StorageType.Protocol)]
        public byte[] ApprovalProgram;

        [Storage(StorageType.Protocol)]
        public byte[] ClearStateProgram;

        [Storage(StorageType.Protocol)]
        public ulong ExtraProgramPages;
      
        








    }


}
