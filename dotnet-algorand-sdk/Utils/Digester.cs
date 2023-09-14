using BlazorSodium.Sodium;


namespace Algorand.Utils
{
    public class Digester
    {
        public static byte[] Digest(byte[] data)
        {
            Sha512256.Compute(data, out byte[] hash);
            return hash;
        }
    }
}