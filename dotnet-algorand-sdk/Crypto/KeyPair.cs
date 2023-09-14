using Algorand.Utils;
using Algorand.Utils.Crypto;
using BlazorSodium.Sodium.Models;
using System;

namespace Algorand.Crypto
{
    public class KeyPair
    {
        internal const int SK_SIZE = 32;
        internal const int SK_SIZE_BITS = SK_SIZE * 8;
        private const int PK_SIZE = 32;
        public byte[] ClearTextPrivateKey { get; private set; }
        public byte[] ClearTextPublicKey { get; private set; }

        public Ed25519KeyPair Pair { get; private set; }


        public KeyPair(byte[] privateKey)
        {
            if (privateKey.Length != SK_SIZE)
                throw new ArgumentException("Incorrect private key length");

            ClearTextPrivateKey = privateKey;
            ClearTextPublicKey = BlazorSodium.Sodium.ScalarMultiplication.Crypto_ScalarMult_Ed25519_Base(privateKey);

            Pair = new Ed25519KeyPair(ClearTextPrivateKey, ClearTextPublicKey);
        }


        public KeyPair(SecureRandom random)
        {

            // create a new key pair
            Pair = random.GenerateKey();

            // get the private and public keys
            ClearTextPrivateKey = Pair.PrivateKey;
            ClearTextPublicKey = Pair.PublicKey;

            

        }









        public string ToMnemonic()
        {

            return Mnemonic.FromKey(ClearTextPrivateKey);
        }
    }

}
