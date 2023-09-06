using NSec.Cryptography;

namespace Algorand.Utils.Crypto
{
    public class SecureRandom
    {
        protected RandomGenerator randomGenerator = RandomGenerator.Default;

        public Key GenerateKey(Algorithm algorithm, in KeyCreationParameters creationParameters = default(KeyCreationParameters))
        {
            return randomGenerator.GenerateKey(algorithm, in creationParameters);
        }

    }

    public class FixedSecureRandom : SecureRandom
    {
        private byte[] seed;
        public FixedSecureRandom(byte[] seed)
        {
            this.seed = seed;
        }
        public Key GenerateKey(Algorithm algorithm, in KeyCreationParameters creationParameters = default(KeyCreationParameters))
        {
            return Key.Import(algorithm, seed, KeyBlobFormat.RawPrivateKey);
        }


    }
}
