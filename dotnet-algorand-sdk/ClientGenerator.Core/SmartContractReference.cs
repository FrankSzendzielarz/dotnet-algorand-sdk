using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{

   

    public  class SmartContractReference
    {
 
        [Storage(StorageType.Protocol)]
        public ulong Id;
        [Storage(StorageType.Protocol)]
        public byte[] ApprovalProgram;
        [Storage(StorageType.Protocol)]
        public byte[] ClearStateProgram;
        [Storage(StorageType.Protocol)]
        public ulong GlobalNumUint;
        [Storage(StorageType.Protocol)]
        public ulong GlobalNumByteSlice;
        [Storage(StorageType.Protocol)]
        public ulong LocalNumUint;
        [Storage(StorageType.Protocol)]
        public ulong LocalNumByteSlice;
        [Storage(StorageType.Protocol)]
        public ulong ExtraProgramPages;
        [Storage(StorageType.Protocol)]
        public byte[] Creator;
        [Storage(StorageType.Protocol)]
        public byte[] Address;
        [Storage(StorageType.Protocol)]
        public byte[] Balance;



    }
}
