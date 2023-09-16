using BlazorSodium;
using BlazorSodium.Services;
using BlazorSodium.Sodium;
using BlazorSodium.Sodium.Models;

namespace Algorand.Utils.Crypto
{
    public class SecureRandom
    {
        public virtual Ed25519KeyPair GenerateKey()
        {
            return PublicKeySignature.Crypto_Sign_KeyPair();
        }
    }

    public class FixedSecureRandom : SecureRandom
    {
        private byte[] seed;
        public FixedSecureRandom(byte[] seed)
        {
            this.seed = seed;
        }
        public override Ed25519KeyPair GenerateKey()
        {
            return PublicKeySignature.Crypto_Sign_Seed_KeyPair(seed);
        }


    }
}
