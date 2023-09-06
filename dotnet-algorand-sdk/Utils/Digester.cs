using NSec.Cryptography;

namespace Algorand.Utils
{
    public class Digester
    {
        public static byte[] Digest(byte[] data)
        {
            var algorithm = HashAlgorithm.Sha256;

            // compute the hash of the data
            byte[] hash = algorithm.Hash(data);

            return hash;
        }
    }
}