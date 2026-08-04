using System.Numerics;

namespace AVM.ClientGenerator.Core
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Integer extensions.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <exception cref="IntentionallyNotImplementedException"></exception>
        public static ulong Pow(this ulong a, ulong b) { throw new IntentionallyNotImplementedException();  }

        public static BigInteger BigPow(this ulong a,ulong b) { throw new IntentionallyNotImplementedException(); }

        public static ulong Sqrt(this ulong a) { throw new IntentionallyNotImplementedException(); }

        public static int BitLen(this ulong a) { throw new IntentionallyNotImplementedException(); }

        public static byte[] ToTealBytes(this ulong a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this long a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this uint a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this int a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this ushort a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this short a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this byte a) { throw new IntentionallyNotImplementedException(); }
        public static byte[] ToTealBytes(this sbyte a) { throw new IntentionallyNotImplementedException(); }





    }
}
