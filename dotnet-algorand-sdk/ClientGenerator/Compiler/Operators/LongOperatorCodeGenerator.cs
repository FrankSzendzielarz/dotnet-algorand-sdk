using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;

namespace AVM.ClientGenerator.Compiler.Operators
{
    internal class LongOperatorCodeGenerator : NumericCodeGenerator
    {
        const ulong shiftOperatorCountLimiter = 0x_00_00_00_00_00_00_00_3f; //C# lang specification restricts long convertible types to lower 6 bits of shift count
        



        public LongOperatorCodeGenerator(CodeBuilder code) : base(code)
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
            var overflow=_code.ReserveLabel();
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
            //simple two's complement addition
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
            //TODO make this a PredefinedSubroutine
            //check signs
            _code.dup2();
            _code.bitwise_xor();
            _code.int_literal_constant(63);
            _code.getbit();
            _code.cover(2);

            //divisor / denominator
            _code.dup();
            _code.int_literal_constant(63);
            _code.getbit();
            string positiveDivisor = _code.ReserveLabel();
            _code.bz(positiveDivisor);
            //negate divisor
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.addw();
            _code.swap();
            _code.pop();

            _code.AddLabel(positiveDivisor);

            //dividend / numerator
            _code.dig(1);
            _code.int_literal_constant(63);
            _code.getbit();
            string positiveDividend = _code.ReserveLabel();
            _code.bz(positiveDividend);
            //negate divided
            _code.swap();
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.addw();
            _code.swap();
            _code.pop();
            _code.swap();

            _code.AddLabel(positiveDividend);

            //do the division
            _code.divide();

            //check if sign should be swapped
            _code.swap();
            string exit = _code.ReserveLabel();
            _code.bz(exit);
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.addw();
            _code.swap();
            _code.pop();

            _code.AddLabel(exit);
        }

        internal override void LeftShift()
        {
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();
            _code.shl();
        }

        internal override void Multiplication()
        {
            _code.mulw();
            _code.swap();
            _code.pop();
        }


        internal override void GreaterThan()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(63);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.greater_than();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(63);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.greater_than_or_equals();
        }
        internal override void LessThan()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(63);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.less_than();
        }

        internal override void LessThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(63);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.less_than_or_equals();
        }

        internal override void Remainder()
        {
            //dup A%B 
            _code.dup2();                                           //A B A B <-HEAD
            Division();                                             //A B (A/B) <-HEAD
            _code.dup();                                            //A B (A/B) (A/B) <-HEAD
            string exitAsZero = _code.ReserveLabel();
            string exit = _code.ReserveLabel();
            //A must have been zero
            _code.bz(exitAsZero);                                   //A B (A/B) <-HEAD

            //multiply result by B, 
            _code.mulw();                                           //A OVERFLOW (B*(A/B)) <-HEAD
            _code.swap();                                           //A (B*(A/B)) OVERFLOW  <-HEAD
            _code.pop();                                            //A (B*(A/B))   <-HEAD

            //A-B to get remainder, so get -B
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.plus();                                           //A -(B*(A/B))   <-HEAD

            _code.addw();                                           //OVERFLOW (A-(B*(A/B)) <-HEAD
            _code.swap();
            _code.pop();
            _code.b(exit);

            _code.AddLabel(exitAsZero);
            _code.pop();                                            //A B  <-HEAD
            _code.pop();                                            //A <-HEAD (where A must be zero)


            _code.AddLabel(exit);
        }

        internal override void RightShift()
        {
            var valueIsNeg = _code.ReserveLabel();
            var exit = _code.ReserveLabel();
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();

            _code.dig(1); //N B N
            _code.int_literal_constant(0x8000000000000000);
            _code.bitwise_and();
            _code.bnz(valueIsNeg);
            _code.shr();
            _code.b(exit);

            _code.AddLabel(valueIsNeg);

            _code.dup();    //dup the bitshift count to get: N B B where B is the bitshift count and N is the value to be shifted
            _code.int_literal_constant(0xffffffffffffffff);  //N B B 0xffffffffffffffff
            _code.swap();                            //N B 0xffffffffffffffff B
            _code.shr();                             //N B (0xffffffffffffffff>>B)
            _code.bitwise_not();                     //N B ormask  
            _code.cover(2);                          //ormask N B 
            _code.shr();                             //ormask N
            _code.bitwise_or();                      //result

            _code.AddLabel(exit);
        }

        internal override void Subtraction()
        {
            string exit = _code.ReserveLabel();
            _code.dup();
            _code.bz(exit);

            //two's complement
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.plus();

            _code.addw();
            _code.swap();

            _code.AddLabel(exit);
            _code.pop();
        }

        internal override void CastToDecimal()
        {
            _code.dup();
            _code.int_literal_constant(0x8000000000000000);
            _code.bitwise_and();

            CastLongAndSignByteToDecimal();
        }


        internal override void CastToObject()
        {
            //nop
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
