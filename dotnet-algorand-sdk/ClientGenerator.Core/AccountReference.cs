namespace AVM.ClientGenerator.Core
{
    public sealed class AccountReference
    {
    
        public byte[] Address() { throw new IntentionallyNotImplementedException(); }
        
        public ulong Balance() { throw new IntentionallyNotImplementedException(); }

        public ulong AssetBalance(AssetReference asset) { throw new IntentionallyNotImplementedException(); }

        public bool IsAssetFrozen(AssetReference asset) { throw new IntentionallyNotImplementedException(); }

        public bool OptedIn(SmartContractReference contract) { throw new IntentionallyNotImplementedException(); }

        public void SetAsLocalAccountContext() { throw new IntentionallyNotImplementedException(); }

        public void Freeze(AssetReference asset) { throw new IntentionallyNotImplementedException(); }

        public void UnFreeze(AssetReference asset) { throw new IntentionallyNotImplementedException(); }

        public void Clawback(AssetReference asset,ulong Amount) { throw new IntentionallyNotImplementedException(); }
    }
}
