using System.Numerics;

namespace AVM.ClientGenerator.Core
{


    public class SmartContractLibrary 
    {
        public static ulong Balance => throw new IntentionallyNotImplementedException();
        public static ulong MinTransactionFee => throw new IntentionallyNotImplementedException();
        public static ulong MinBalance => throw new IntentionallyNotImplementedException();
        public static ulong MaxTransactionLife => throw new IntentionallyNotImplementedException();
        public static byte[] ZeroAddress => throw new IntentionallyNotImplementedException();
        public static ulong GroupSize => throw new IntentionallyNotImplementedException();

        public static ulong LogicSigVersion => throw new IntentionallyNotImplementedException();
        public static ulong Round => throw new IntentionallyNotImplementedException();
        public static ulong LatestTimeStamp => throw new IntentionallyNotImplementedException();
        public static ulong CurrentApplicationID => throw new IntentionallyNotImplementedException();
        public static byte[] CreatorAddress => throw new IntentionallyNotImplementedException();
        public static byte[] CurrentApplicationAddress => throw new IntentionallyNotImplementedException();
        public static byte[] GroupId => throw new IntentionallyNotImplementedException();
        public static ulong OpcodeBudget => throw new IntentionallyNotImplementedException();
        public static byte[] CallerApplicationID => throw new IntentionallyNotImplementedException();
        public static byte[] CallerApplicationAddress => throw new IntentionallyNotImplementedException();
        public static AssetReference[] AssetReferences => throw new IntentionallyNotImplementedException();
        public static void Ecdsa_pk_decompress_secp256k1(byte[] pk, out byte[] pubkey_X, out byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public static void Ecdsa_pk_recover_secp256k1(byte[] data, ulong recover_id, byte[] signature_R, byte[] signature_S, out byte[] pubkey_X, out byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public static bool Ecdsa_verify_secp256k1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public static bool Ecdsa_verify_secp256r1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y) => throw new IntentionallyNotImplementedException();
        public static bool Ed25519verify(byte[] data, byte[] signature, byte[] publicKey) => throw new IntentionallyNotImplementedException();
        public static byte[] EncodeString(string str) => throw new IntentionallyNotImplementedException();
        public static byte[] Keccak256(byte[] toHash) => throw new IntentionallyNotImplementedException();
        public static byte[] Sha256(byte[] toHash) => throw new IntentionallyNotImplementedException();
        public static byte[] Sha512_256(byte[] toHash) => throw new IntentionallyNotImplementedException();
        public static void SwitchLocalAccountContext(int acccountIndex) => throw new IntentionallyNotImplementedException();
        public static BigInteger BigIntegerFromByteArray(byte[] bytes) => throw new IntentionallyNotImplementedException();
        public static byte[] BigIntegerToByteArray(BigInteger b) => throw new IntentionallyNotImplementedException();
        public static BigInteger BigIntegerFromIntegral(long integral) => throw new IntentionallyNotImplementedException();
        public static byte[] TealBytesFromDecimal(decimal dec) => throw new IntentionallyNotImplementedException();
        public static decimal DecimalFromTealBytes(byte[] dec) => throw new IntentionallyNotImplementedException();
        public static void Fail() { throw new IntentionallyNotImplementedException(); }
        public static ulong CreateAsset(ulong total, uint decimals, bool defaultFrozen, byte[]? unitName = null, byte[]? assetName = null, byte[]? url = null, byte[]? metaDataHash = null, byte[] manager = null, byte[]? reserve = null, byte[]? freeze = null, byte[]? clawback = null) => throw new IntentionallyNotImplementedException();
        public static void InvokeSmartContractMethod() => throw new IntentionallyNotImplementedException();
        public static void LogBytes(byte[] bytes) => throw new IntentionallyNotImplementedException();
        public static void LogInt(long integral) => throw new IntentionallyNotImplementedException();
        public static void LogUInt(ulong integral) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Create or update a box
        /// </summary>
        /// <param name="boxName">Name of box</param>
        public static void BoxSet(byte[] boxName, byte[] data) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Delete box by name. 
        /// </summary>
        /// <param name="boxName">Name of box</param>
        /// <returns>False if box did not exist </returns>
        public static void BoxDel(byte[] boxName) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Length of box by name, or -1 if the box did not exist.
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        /// <exception cref="IntentionallyNotImplementedException"></exception>
        public static long BoxLen(byte[] boxName) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Get the box contents by name. Error if box does not exist.
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        public static byte[] BoxGet(byte[] boxName) => throw new IntentionallyNotImplementedException();
        /// <summary>
        /// Check if box exists
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        public static bool BoxExists(byte[] boxName) => throw new IntentionallyNotImplementedException();
        public static bool Error() => throw new IntentionallyNotImplementedException();
    }
}
