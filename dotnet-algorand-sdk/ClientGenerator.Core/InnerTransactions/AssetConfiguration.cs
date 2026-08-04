namespace AVM.ClientGenerator.Core
{
    public sealed class AssetConfiguration : InnerTransaction
    {

        public ulong ConfigAsset { get; private set; }
        public ulong ConfigAssetTotal { get; private set; }
        public uint ConfigAssetDecimals { get; private set; }
        public bool ConfigAssetDefaultFrozen { get; private set; }
        public byte[] ConfigAssetUnitName { get; private set; }
        public byte[] ConfigAssetName { get; private set; }
        public byte[] ConfigAssetURL { get; private set; }
        public byte[] ConfigAssetMetadataHash { get; private set; }
        public byte[] ConfigAssetManager { get; private set; }
        public byte[] ConfigAssetReserve { get; private set; }
        public byte[] ConfigAssetFreeze { get; private set; }
        public byte[] ConfigAssetClawback { get; private set; }

        private AssetConfiguration() { }
        public AssetConfiguration(ulong configAsset, ulong? configAssetTotal=0, uint? configAssetDecimals=0, bool? configAssetDefaultFrozen=null, byte[] configAssetUnitName=null, byte[] configAssetName = null, byte[] configAssetURL = null, byte[] configAssetMetadataHash = null, AccountReference configAssetManager = null, AccountReference configAssetReserve = null, AccountReference configAssetFreeze = null, AccountReference configAssetClawback = null,  ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null, ulong? txType = 0, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base("acfg", fee, firstValid, lastValid, sender, lease, note, rekeyTo)
        {
            
        }
    }
}
