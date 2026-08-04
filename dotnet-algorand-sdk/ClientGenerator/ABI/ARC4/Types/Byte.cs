namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class Byte : UInt
    {
        public Byte() : base(8)
        {
        }
        public Byte(byte data) : base(8)
        {
            base.From(data);
        }
        public override string GetDescription()
        {
            return "byte";
        }
    }
}
