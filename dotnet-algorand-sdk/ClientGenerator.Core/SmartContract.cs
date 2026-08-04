using System.Numerics;

#nullable enable

namespace AVM.ClientGenerator.Core
{
    public abstract class SmartContract : ISmartContractPredefineds
    {
        public ulong Balance => throw new IntentionallyNotImplementedException();
        public ulong MinTransactionFee => throw new IntentionallyNotImplementedException();

        public ulong MinBalance => throw new IntentionallyNotImplementedException();

        public ulong MaxTransactionLife => throw new IntentionallyNotImplementedException();

        public byte[] ZeroAddress => throw new IntentionallyNotImplementedException();

        public ulong GroupSize => throw new IntentionallyNotImplementedException();

        public ulong LogicSigVersion => throw new IntentionallyNotImplementedException();

        public ulong Round => throw new IntentionallyNotImplementedException();

        public ulong LatestTimeStamp => throw new IntentionallyNotImplementedException();

        public ulong CurrentApplicationID => throw new IntentionallyNotImplementedException();

        public byte[] CreatorAddress => throw new IntentionallyNotImplementedException();

        public byte[] CurrentApplicationAddress => throw new IntentionallyNotImplementedException();

        public byte[] GroupId => throw new IntentionallyNotImplementedException();

        public ulong OpcodeBudget => throw new IntentionallyNotImplementedException();
        public byte[] CallerApplicationID => throw new IntentionallyNotImplementedException();
        public byte[] CallerApplicationAddress => throw new IntentionallyNotImplementedException();
        public AssetReference[] AssetReferences => throw new IntentionallyNotImplementedException();
        public void Ecdsa_pk_decompress_secp256k1(byte[] pk, out byte[] pubkey_X, out byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public void Ecdsa_pk_recover_secp256k1(byte[] data, ulong recover_id, byte[] signature_R, byte[] signature_S, out byte[] pubkey_X, out byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public bool Ecdsa_verify_secp256k1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public bool Ecdsa_verify_secp256r1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public bool Ed25519verify(byte[] data, byte[] signature, byte[] publicKey) => throw new IntentionallyNotImplementedException();
        public byte[] EncodeString(string str) => throw new IntentionallyNotImplementedException();
        public byte[] Keccak256(byte[] toHash) => throw new IntentionallyNotImplementedException();
        public byte[] Sha256(byte[] toHash) => throw new IntentionallyNotImplementedException();
        public byte[] Sha512_256(byte[] toHash) => throw new IntentionallyNotImplementedException();
        public  void SwitchLocalAccountContext(int acccountIndex) => throw new IntentionallyNotImplementedException();
        public BigInteger BigIntegerFromByteArray(byte[] bytes) => throw new IntentionallyNotImplementedException();
        public byte[] BigIntegerToByteArray(BigInteger b) => throw new IntentionallyNotImplementedException();
        public BigInteger BigIntegerFromIntegral(long integral) => throw new IntentionallyNotImplementedException();
        public byte[] TealBytesFromDecimal(decimal dec) => throw new IntentionallyNotImplementedException();
        public decimal DecimalFromTealBytes(byte[] dec) => throw new IntentionallyNotImplementedException();
        public void Fail() { throw new IntentionallyNotImplementedException(); }
        public ulong CreateAsset(ulong total, uint decimals, bool defaultFrozen, byte[]? unitName = null, byte[]? assetName = null, byte[]? url = null, byte[]? metaDataHash = null, byte[]? manager = null, byte[]? reserve = null, byte[]? freeze = null, byte[]? clawback = null) => throw new IntentionallyNotImplementedException();
        public void InvokeSmartContractMethod() => throw new IntentionallyNotImplementedException();
        public void LogBytes(byte[] bytes) => throw new IntentionallyNotImplementedException();
        public void LogInt(long integral) => throw new IntentionallyNotImplementedException();
        public void LogUInt(ulong integral) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Create or update a box
        /// </summary>
        /// <param name="boxName">Name of box</param>
        public void BoxSet(byte[] boxName, byte[] data) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Delete box by name. 
        /// </summary>
        /// <param name="boxName">Name of box</param>
        /// <returns>False if box did not exist </returns>
        public void BoxDel(byte[] boxName) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Length of box by name, or -1 if the box did not exist.
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        /// <exception cref="IntentionallyNotImplementedException"></exception>
        public long BoxLen(byte[] boxName) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Get the box contents by name. Error if box does not exist.
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        public byte[] BoxGet(byte[] boxName) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Check if box exists
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        public bool BoxExists(byte[] boxName) => throw new IntentionallyNotImplementedException();
        public bool Error() => throw new IntentionallyNotImplementedException();
        protected abstract int ApprovalProgram(in AppCallTransactionReference transaction);
        protected abstract int ClearStateProgram(in AppCallTransactionReference transaction);

       

    }


}
