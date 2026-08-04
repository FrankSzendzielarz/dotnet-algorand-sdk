using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;

namespace AVM.ClientGenerator.Compiler.Operators
{
    internal class ULongOperatorCodeGenerator : NumericCodeGenerator
    {
        const ulong shiftOperatorCountLimiter = 0x_00_00_00_00_00_00_00_3f; //C# lang specification restricts int or int convertible types to lower 5 bits of shift count

        public ULongOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }
        internal override void LogicalNegate()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void Increment()
        {
            _code.int_literal_constant(1);
            _code.addw();
            _code.swap();
            _code.pop();


        }

        internal override void Decrement()
        {
            var overflow = _code.ReserveLabel();
            var end = _code.ReserveLabel();
            _code.dup();
            _code.bz(overflow);
            _code.int_literal_constant(1);
            _code.minus();
            _code.b(end);
            _code.AddLabel(overflow);
            _code.int_literal_constant(0xffffffffffffffff);
            _code.AddLabel(end);

        }

        internal override void Negate()
        {
            string ignoreZeroLabel = _code.ReserveLabel();
            _code.dup();                         //keep the value on the stack
            _code.bz(ignoreZeroLabel);           //pop value and skip if zero, to avoid overflow
            _code.bitwise_not();                 //one's complement (invert) all bits
            _code.int_literal_constant(1);       //push a one
            _code.plus();                        //add the 1 and the inverted value - this yields the 2s complement negation - no overflow should occur
            _code.AddLabel(ignoreZeroLabel);     //add the reserved label here for the above branch
        }

        internal override void OnesComplement()
        {
            _code.bitwise_not();
        }
        internal override void Addition()
        {
            _code.addw();
            _code.swap();
            _code.pop();
            
            
        }

        internal override void BitwiseAnd()
        {
            _code.bitwise_and();
        }

        internal override void BitwiseOr()
        {
            _code.bitwise_or();
        }
        internal override void BitwiseXor()
        {
            _code.bitwise_xor();

        }
        internal override void Division()
        {
            _code.divide();
        }

        internal override void LeftShift()
        {
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();
            _code.shl();
        }
        internal override void GreaterThan()
        {
            _code.greater_than();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.greater_than_or_equals();
        }
        internal override void LessThan()
        {
            _code.less_than();
        }

        internal override void LessThanOrEqual()
        {
            _code.less_than_or_equals();
        }
        internal override void Multiplication()
        {
            _code.mulw();
            _code.swap();
            _code.pop();
            
            
        }

        internal override void Remainder()
        {
            _code.modulo();
        }

        internal override void RightShift()
        {
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();
            _code.shr();
        }

        internal override void Subtraction()
        {
            string exit = _code.ReserveLabel();

            //Is B zero? If yes, go to special case where the result is A.
            string rightOperandIsNotZero = _code.ReserveLabel();
            _code.dup();
            _code.bnz(rightOperandIsNotZero);
            _code.pop();            //lose B and return A
            _code.b(exit);

            _code.AddLabel(rightOperandIsNotZero);
            //two's complement
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.plus();

            //add the two
            _code.addw();
            _code.swap();
            _code.pop();


            _code.AddLabel(exit);
        }

        internal override void CastToObject()
        {
            //noop
        }

        internal override void CastToDecimal()
        {
            CastUnsignedLongToDecimal();
        }

        internal override void CastToByte()
        {
            _code.int_literal_constant(0xff);
            _code.bitwise_and();
        }

        internal override void CastToInt()
        {
            _code.int_literal_constant(0xffffffff);
            _code.bitwise_and();
        }

        internal override void CastToLong()
        {
            //nop
        }

        internal override void CastToSbyte()
        {
            _code.int_literal_constant(0xff);
            _code.bitwise_and();
        }

        internal override void CastToShort()
        {
            _code.int_literal_constant(0xffff);
            _code.bitwise_and();
        }

        internal override void CastToUInt()
        {
            _code.int_literal_constant(0xffffffff);
            _code.bitwise_and();
        }

        internal override void CastToULong()
        {
            //nop
        }

        internal override void CastToUShort()
        {
            _code.int_literal_constant(0xffff);
            _code.bitwise_and();
        }
    }
}
