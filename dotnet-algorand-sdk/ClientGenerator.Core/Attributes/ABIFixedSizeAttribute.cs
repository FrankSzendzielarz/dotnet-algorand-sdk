using System;


namespace AVM.ClientGenerator.Core.Attributes
{
    [AttributeUsage( AttributeTargets.Field , AllowMultiple = false)]
    public class ABIFixedSizeAttribute : Attribute
    {
        public int Bytes;

        public ABIFixedSizeAttribute(Int32 bytes)
        {
           Bytes = bytes;
        }
    }
}
