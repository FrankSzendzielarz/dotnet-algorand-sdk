using System.Numerics;

namespace AVM.ClientGenerator.Core
{

    public interface IAlgorandCommonPredefineds
    {

        byte[] Sha256(byte[] toHash);

        byte[] Keccak256(byte[] toHash);

        byte[] Sha512_256(byte[] toHash);

        bool Ed25519verify(byte[] data, byte[] signature, byte[] publicKey);

        bool Ecdsa_verify_secp256k1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y);

        void Ecdsa_pk_decompress_secp256k1(byte[] pk, out byte[] pubkey_X, out byte[] pubkey_Y);

        void Ecdsa_pk_recover_secp256k1(byte[] data, ulong recover_id, byte[] signature_R, byte[] signature_S, out byte[] pubkey_X, out byte[] pubkey_Y);



        public BigInteger BigIntegerFromByteArray(byte[] bytes);

    

        public byte[] BigIntegerToByteArray(BigInteger b);

        

        public BigInteger BigIntegerFromIntegral(long integral);

        

        #region Global F

        ulong MinTransactionFee { get; }
        ulong MinBalance { get; }
        ulong MaxTransactionLife { get; }
        byte[] ZeroAddress { get; }
        ulong GroupSize { get; }
        ulong LogicSigVersion { get; }

        byte[] GroupId { get; }


        #endregion

    }

    public interface ISmartSignaturePredefineds : IAlgorandCommonPredefineds
    {
        byte[] GetArgument(int n);

        void InvokeSmartSignatureMethod();
    }


    public interface ISmartContractPredefineds : IAlgorandCommonPredefineds
    {
        /// <summary>
        /// Change local account context to index in account array
        /// </summary>
        /// <param name="acccountIndex"></param>
        void SwitchLocalAccountContext(int acccountIndex);

        public ulong CreateAsset(ulong total, uint decimals, bool defaultFrozen, byte[] unitName = null, byte[] assetName = null, byte[] url = null, byte[] metaDataHash = null, byte[] manager = null, byte[] reserve = null, byte[] freeze = null, byte[] clawback = null);

        public void LogBytes(byte[] bytes);

        public void LogUInt(ulong integral);

        public void LogInt(long integral);


        /// <summary>
        /// ARC4 method router
        /// </summary>
        void InvokeSmartContractMethod();

        #region Global F

        
    
        ulong Round { get; }

        ulong LatestTimeStamp { get; }

        ulong CurrentApplicationID { get; }

        byte[] CreatorAddress { get; }

        byte[] CurrentApplicationAddress { get; }

        AssetReference[] AssetReferences { get; }

        #endregion

       

    }
}
