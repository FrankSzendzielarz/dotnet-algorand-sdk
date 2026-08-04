namespace AVM.ClientGenerator.Core
{
    public sealed class AssetTransfer : InnerTransaction
    {
        public ulong XferAsset { get; private set; }
        public ulong AssetAmount { get; private set; }
        public byte[] AssetSender { get; private set; }
        public byte[] AssetReceiver { get; private set; }
        public byte[] AssetCloseTo { get; private set; }

        private AssetTransfer() { }

        public AssetTransfer(ulong xferAsset, ulong assetAmount, AccountReference assetReceiver, AccountReference assetSender=null, AccountReference assetCloseTo = null , ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null,  byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base("axfer", fee, firstValid, lastValid, sender, lease, note, rekeyTo)
        {
        }
    }
}
