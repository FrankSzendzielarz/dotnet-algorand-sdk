namespace AVM.ClientGenerator.Core
{
    public sealed class AssetFreeze : InnerTransaction
    {
        public byte[] FreezeAssetAccount { get; private set; }
        public ulong FreezeAsset { get; private set; }
        public bool FreezeAssetFrozen { get; private set; }

        private AssetFreeze() { }
        public AssetFreeze(AccountReference freezeAssetAccount, ulong freezeAsset, bool freezeAssetFrozen, ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null, ulong? txType = 0, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base("afrz", fee, firstValid, lastValid, sender, lease, note, rekeyTo)
        {
        }
    }
}
