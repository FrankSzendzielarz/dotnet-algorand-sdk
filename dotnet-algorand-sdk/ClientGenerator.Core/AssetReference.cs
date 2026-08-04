using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public sealed class AssetReference 
    {

        [Storage(StorageType.Protocol)]
        public ulong Id;
        [Storage(StorageType.Protocol)]
        public ulong Total;
        [Storage(StorageType.Protocol)]
        public ulong Decimals;
        [Storage(StorageType.Protocol)]
        public ulong DefaultFrozen;
        [Storage(StorageType.Protocol)]
        public byte[] UnitName;
        [Storage(StorageType.Protocol)]
        public byte[] Name;
        [Storage(StorageType.Protocol)]
        public byte[] URL;
        [Storage(StorageType.Protocol)]
        public byte[] MetadataHash;
        [Storage(StorageType.Protocol)]
        public byte[] Manager;
        [Storage(StorageType.Protocol)]
        public byte[] Reserve;
        [Storage(StorageType.Protocol)]
        public byte[] Freeze;
        [Storage(StorageType.Protocol)]
        public byte[] Clawback;
        [Storage(StorageType.Protocol)]
        public byte[] Creator;


        public void Transfer(AccountReference recipientAddress, ulong amount) { throw new IntentionallyNotImplementedException(); }
      
        public void Update(byte[] Manager = null, byte[] Reserve = null, byte[] Freeze = null, byte[] Clawback=null) { throw new IntentionallyNotImplementedException(); }






    }
}
