namespace AVM.ClientGenerator.Core
{
    public sealed class AssetClawback : InnerTransaction
    {
        
        public ulong XferAsset { get; private set; }

        public ulong AssetAmount { get; private set; }

        public byte[] AssetSender { get; private set; }

        public byte[] AssetReceiver { get; private set; }

        private AssetClawback() { }
        public AssetClawback(ulong xferAsset, ulong assetAmount, AccountReference assetSender, AccountReference assetReceiver, ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null, ulong? txType = 0, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base("axfer", fee, firstValid, lastValid, sender, lease, note, rekeyTo)
        {
        
        }
    }
}
