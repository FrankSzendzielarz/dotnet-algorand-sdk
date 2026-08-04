namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class Address : FixedArray<Byte>
    {
        public override bool IsDynamic => false;
        public Address() : base(32)
        {
        }
        public override bool From(object instance)
        {

            if (instance is Algorand.Address b)
            {
                return base.From(b.Bytes);
            }
            return base.From(instance);
        }
        public override object ToValue()
        {
            return new Algorand.Address(base.ToByteArray());
        }
    }
}
