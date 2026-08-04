using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Operators
{
    internal abstract class NumericCodeGenerator : TypeCodeGenerator
    {
        internal NumericCodeGenerator(CodeBuilder code) : base(code) { }

        protected void  CastUnsignedLongToDecimal()
        {
            (CodeBuilder code, string label) res = _code.RegisterPredefined(PredefinedSubroutines.CastUnsignedLongToDecimal);
            if (res.code != null)
            {
                var c = res.code;
                c.dup();
                c.int_literal_constant(32);
                c.shr();   //get the top bytes
                c.swap();
                c.int_literal_constant(32);
                c.shl();
                c.bitwise_or();
                c.itob();
                c.byte_literal_constant("0x0000000000000000");
                c.concat();
                c.retsub();
            }
            _code.callsub(res.label);

        }
        protected void CastLongAndSignByteToDecimal()
        {
            (CodeBuilder code, string label) res = _code.RegisterPredefined(PredefinedSubroutines.CastLongAndSignByteToDecimal);
            if (res.code != null)
            {
                var c = res.code;
                c.dup();
                var positive = _code.ReserveLabel();
                c.bz(positive);
                c.bitwise_not();                                                               //negate
                c.int_literal_constant(1);                                                     //
                c.plus();                                                                      //
                c.AddLabel(positive);
                c.swap();
                c.itob();                                                                      //sign ib
                c.dup();                                                                       //sign ib ib
                c.int_literal_constant(4);                                                     //sign ib ib 4
                c.int_literal_constant(8);                                                     //sign ib ib 4 8
                c.substring3();                                                                //sign ib lo
                c.swap();                                                                      //sign lo ib
                c.int_literal_constant(0);                                                     //sign lo ib 0
                c.int_literal_constant(4);                                                     //sign lo ib 0 4
                c.substring3();                                                                //sign lo mid
                c.byte_literal_constant("0x0000000000000000");                                 //sign lo mid 0000000000000000
                c.concat();                                                                    //sign lo mid0000000000000000
                c.concat();                                                                    //sign lomid0000000000000000
                c.swap();                                                                      // lomid0000000000000000 sign 
                c.int_literal_constant(12);                                                    // lomid0000000000000000 sign 12
                c.swap();                                                                      // lomid0000000000000000 12 sign
                c.setbyte();                                                                   // 0xllllllllmmmmmmmm00000000ss000000
                c.retsub();
            }
            _code.callsub(res.label);
        }

        internal abstract void CastToDecimal();
        internal abstract void CastToByte();
        internal abstract void CastToInt();
        internal abstract void CastToLong();
        internal abstract void CastToSbyte();
        internal abstract void CastToShort();
        internal abstract void CastToUInt();
        internal abstract void CastToULong();
        internal abstract void CastToUShort();
        internal abstract void CastToObject();




    }
}
