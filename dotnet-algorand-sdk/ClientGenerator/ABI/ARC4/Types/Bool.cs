using System;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class Bool : WireType
    {
        public override string GetDescription()
        {
            return "bool";
        }
        public bool Value { get; set; }

        public override bool IsDynamic => false;

        public override uint Decode(byte[] data)
        {
            if (data.Length == 0)
                throw new ArgumentException("Invalid data length");
            Value = data[0] == 0x80;
            return 1;
        }

        public override byte[] Encode()
        {
            return new byte[] { (byte)(Value ? 0x80 : 0) };
        }

        public override bool From(object instance)
        {
            if (instance is bool boolV)
            {
                Value = boolV;
                return true;
            }
            throw new NotImplementedException();
        }

        public override object ToValue()
        {
            return Value;
        }
    }

}
