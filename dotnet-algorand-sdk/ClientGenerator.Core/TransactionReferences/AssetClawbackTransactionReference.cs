using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public class AssetClawbackTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public ulong XferAsset;

        [Storage(StorageType.Protocol)]
        public ulong AssetAmount;

        [Storage(StorageType.Protocol)]
        public byte[] AssetSender;

        [Storage(StorageType.Protocol)]
        public byte[] AssetReceiver;

    }
}
