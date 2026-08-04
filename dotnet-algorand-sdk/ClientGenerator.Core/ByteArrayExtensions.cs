using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Conc
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <exception cref="IntentionallyNotImplementedException"></exception>
        public static byte[] Concat(this byte[] a, byte[] b) { throw new IntentionallyNotImplementedException();  }

        public static AccountReference ToAccountReference(this byte[] a) { throw new IntentionallyNotImplementedException(); }

        public static byte[] Part(this byte[] a,uint start, uint end) { throw new IntentionallyNotImplementedException(); }

        public static void Init(this byte[] a, [Literal]byte b, [Literal] uint l) { throw new IntentionallyNotImplementedException(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b">Position to replace from</param>
        /// <param name="c">Bytes to replace with</param>
        /// <exception cref="IntentionallyNotImplementedException"></exception>
        public static void Replace(this byte[] a,  byte b, byte[] c) { throw new IntentionallyNotImplementedException(); }

        public static string ToString(this byte[] a) { throw new IntentionallyNotImplementedException(); }


        public static int BitLen(this byte[] a) { throw new IntentionallyNotImplementedException(); }
     

        public static int GetBit(this byte[] a, ulong b) { throw new IntentionallyNotImplementedException(); }

        public static ulong ToTealUlong(this byte[] a) { throw new IntentionallyNotImplementedException(); }




    }
}
