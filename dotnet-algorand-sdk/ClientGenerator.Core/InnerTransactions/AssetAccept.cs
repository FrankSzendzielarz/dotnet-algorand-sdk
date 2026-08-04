namespace AVM.ClientGenerator.Core
{
    public sealed class AssetAccept : InnerTransaction
    {
        
        public ulong XferAsset { get; private set; }

        public byte[] AssetSender { get; private set; }

        public byte[] AssetReceiver { get; private set; }
        private AssetAccept() { }
        public AssetAccept(ulong xferAsset, AccountReference sender, AccountReference assetReceiver,  ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0,  byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base("axfer", fee, firstValid, lastValid, sender, lease, note, rekeyTo)
        {
        
        }
    }
}
