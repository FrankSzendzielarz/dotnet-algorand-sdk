namespace AVM.ClientGenerator.Core
{
    public static class Constants
    {
        public const uint TEALVersion = 1;
        public const uint StackDepth = 1000;
        public const uint ScratchSpaceSize = 256;
        public const uint MaxByteArraySize = 4096;
        public static byte[] RetPrefix = new byte[4] { 21, 31, 124, 117 };
    }
}
