using System.Numerics;

namespace AVM.ClientGenerator.Core
{
    public abstract class SmartSignature : ISmartSignaturePredefineds
    {
        public ulong MinTransactionFee => throw new IntentionallyNotImplementedException();

        public ulong MinBalance => throw new IntentionallyNotImplementedException();

        public ulong MaxTransactionLife => throw new IntentionallyNotImplementedException();

        public byte[] ZeroAddress => throw new IntentionallyNotImplementedException();

        public ulong GroupSize => throw new IntentionallyNotImplementedException();

        public ulong LogicSigVersion => throw new IntentionallyNotImplementedException();

        public byte[] GroupId => throw new IntentionallyNotImplementedException();

        public BigInteger BigIntegerFromByteArray(byte[] bytes)
        {
            throw new IntentionallyNotImplementedException();
        }

        public BigInteger BigIntegerFromIntegral(long integral)
        {
            throw new IntentionallyNotImplementedException();
        }

        public byte[] BigIntegerToByteArray(BigInteger b)
        {
            throw new IntentionallyNotImplementedException();
        }

        public void Ecdsa_pk_decompress_secp256k1(byte[] pk, out byte[] pubkey_X, out byte[] pubkey_Y)
        {
            throw new IntentionallyNotImplementedException();
        }

      

        public void Ecdsa_pk_recover_secp256k1(byte[] data, ulong recover_id, byte[] signature_R, byte[] signature_S, out byte[] pubkey_X, out byte[] pubkey_Y)
        {
            throw new IntentionallyNotImplementedException();
        }

        public bool Ecdsa_verify_secp256k1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y)
        {
            throw new IntentionallyNotImplementedException();
        }

        public bool Ecdsa_verify_secp256r1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y)
        {
            throw new IntentionallyNotImplementedException();
        }

        public bool Ed25519verify(byte[] data, byte[] signature, byte[] publicKey)
        {
            throw new IntentionallyNotImplementedException();
        }

        public byte[] GetArgument(int n)
        {
            throw new IntentionallyNotImplementedException();
        }

        public byte[] Keccak256(byte[] toHash)
        {
            throw new IntentionallyNotImplementedException();
        }

        public byte[] Sha256(byte[] toHash)
        {
            throw new IntentionallyNotImplementedException();
        }

        public byte[] Sha512_256(byte[] toHash)
        {
            throw new IntentionallyNotImplementedException();
        }

      

       
        public void InvokeSmartSignatureMethod()
        {
            throw new IntentionallyNotImplementedException();
        }

        public abstract int Program();

     
    }

    


}
