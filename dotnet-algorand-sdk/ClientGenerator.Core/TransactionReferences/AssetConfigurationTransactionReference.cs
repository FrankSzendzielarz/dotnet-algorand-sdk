using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public class AssetConfigurationTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public ulong ConfigAsset;

        [Storage(StorageType.Protocol)]
        public ulong ConfigAssetTotal;

        [Storage(StorageType.Protocol)]
        public uint ConfigAssetDecimals;

        [Storage(StorageType.Protocol)]
        public bool ConfigAssetDefaultFrozen;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetUnitName;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetName;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetURL;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetMetadataHash;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetManager;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetReserve;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetFreeze;

        [Storage(StorageType.Protocol)]
        public byte[] ConfigAssetClawback;



    }
}
