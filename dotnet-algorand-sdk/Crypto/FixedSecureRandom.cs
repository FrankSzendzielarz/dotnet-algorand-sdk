using Org.BouncyCastle.Security;

namespace Algorand.Utils.Crypto
{
    public class FixedSecureRandom : SecureRandom
    {
        private byte[] fixedValue;
        private int index = 0;

        public FixedSecureRandom(byte[] fixedValue)
        {
            this.fixedValue = fixedValue;
        }
        public override void NextBytes(byte[] bytes)
        {
            if (this.index >= this.fixedValue.Length)
            {
                // no more data to copy
                return;
            }
            int len = bytes.Length;
            if (len > this.fixedValue.Length - this.index)
            {
                len = this.fixedValue.Length - this.index;
            }
            JavaHelper<byte>.SystemArrayCopy(this.fixedValue, this.index, bytes, 0, len);
            this.index += bytes.Length;
        }

        public override byte[] GenerateSeed(int numBytes)
        {
            byte[] bytes = new byte[numBytes];
            this.NextBytes(bytes);
            return bytes;
        }
    }
}
