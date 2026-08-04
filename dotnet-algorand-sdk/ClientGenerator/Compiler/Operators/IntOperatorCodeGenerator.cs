using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;

namespace AVM.ClientGenerator.Compiler.Operators
{
    internal class IntOperatorCodeGenerator : NumericCodeGenerator
    {
        const ulong shiftOperatorCountLimiter = 0x_00_00_00_00_00_00_00_1f; //C# lang specification restricts int or int convertible types to lower 5 bits of shift count
        const ulong mask = 0x_00_00_00_00_ff_ff_ff_ff;
        const ulong signBit = 0x_00_00_00_00_80_00_00_00;
        const ulong int64NegativeMask = 0x_ffff_ffff_ffff_0000;
        public IntOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }





        internal override void Increment()
        {
            _code.int_literal_constant(1);
            _code.plus();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }

        internal override void Decrement()
        {
            _code.int_literal_constant(0x_01_00_00_00_00_00_00_00);
            _code.bitwise_or();
            _code.int_literal_constant(1);
            _code.minus();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }


        internal override void LogicalNegate()
        {
            throw new ErrorDiagnosticException("E017");
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
            _code.int_literal_constant(mask);
            _code.bitwise_and();
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
            _code.dup2();                                               //A B A B 
            _code.bitwise_xor();                                        //A B A^B
            _code.int_literal_constant(31);                            
            _code.getbit();                                            

            _code.cover(2);                                             //A^B A B
                                                                        //
            //divisor / denominator                                     //
            _code.dup();                                                //A^B A B B
            _code.int_literal_constant(31);                             //A^B A B B 31 
            _code.getbit();                                             //A^B A B 0
            string positiveDivisor = _code.ReserveLabel();              //
            _code.bz(positiveDivisor);                                  //A^B A B
            //negate divisor     
            _code.int_literal_constant(mask);//
            _code.bitwise_xor();                                        //
            _code.int_literal_constant(1);                              //
            _code.plus();                                           //
                                                                     //
            _code.AddLabel(positiveDivisor);                            //A^B A B
                                                                        //
            //dividend / numerator                                      //
            _code.dig(1);                                               //A^B A B A
            _code.int_literal_constant(31);                             //A^B A B A 31
            _code.getbit();                                             //A^B A B 0
            string positiveDividend = _code.ReserveLabel();             //
            _code.bz(positiveDividend);                                 //A^B A B 
            //negate divided                                            //
            _code.swap();
            _code.int_literal_constant(mask);
            _code.bitwise_xor();                                        //
            _code.int_literal_constant(1);                              //
            _code.plus();                                               //
            _code.swap();                                               //
                                                                        //
            _code.AddLabel(positiveDividend);                           //A^B A B
                                                                        //
            //do the division                                           //
            _code.divide();                                             //A^B A/B
                                                                        //
            //check if sign should be swapped                           //
            _code.swap();                                               //A/B A^B 
            string exit = _code.ReserveLabel();                         //
            _code.bz(exit);                                             //
            _code.dup();
            _code.bz(exit);                                             //skip negate if zero to avoid overflow
            _code.int_literal_constant(mask);
            _code.bitwise_xor();                                        //
            _code.int_literal_constant(1);                              //
            _code.plus();                                               //
            _code.AddLabel(exit);                                       //A/B
            _code.int_literal_constant(mask);         //
            _code.bitwise_and();
            



        }

        internal override void GreaterThan()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(31);
            _code.getbit();                       //A B sign
            string compare=_code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);                
            _code.greater_than();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(31);
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
            _code.int_literal_constant(31);
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
            _code.int_literal_constant(31);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.less_than_or_equals();
        }

        internal override void LeftShift()
        {
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();
            _code.shl();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }

        internal override void Multiplication()
        {
            _code.mulw();
            _code.swap();
            _code.pop();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
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

        //arithmetic shift right!
        internal override void RightShift()
        {
            
            var valueIsNeg = _code.ReserveLabel();
            var exit = _code.ReserveLabel();
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();

            _code.dig(1); //N B N
            _code.int_literal_constant(0x80000000);
            _code.bitwise_and();
            _code.bnz(valueIsNeg);
            _code.shr();
            _code.b(exit);
            
            _code.AddLabel(valueIsNeg);

            _code.dup();    //dup the bitshift count to get: N B B where B is the bitshift count and N is the value to be shifted
            _code.int_literal_constant(0xffffffff);  //N B B 0xffffffff
            _code.swap();                            //N B 0xffffffff B
            _code.shr();                             //N B (0xffffffff>>B)
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

            _code.int_literal_constant(mask);
            _code.bitwise_and();

            _code.AddLabel(exit);
            _code.pop();
        }

        internal override void CastToDecimal()
        {
            _code.dup();
            _code.int_literal_constant(0x80000000);
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
            //nop
        }

        internal override void CastToLong()
        {
            _code.dup();                                            
            _code.int_literal_constant(signBit);                    
            _code.bitwise_and();                                    
            var positive= _code.ReserveLabel();
            _code.bz(positive);                                     
            _code.int_literal_constant(int64NegativeMask);      
            _code.bitwise_or();                                     
            _code.AddLabel(positive);

            
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
            //nop
        }

        internal override void CastToULong()
        {
            CastToLong();
        }

        internal override void CastToUShort()
        {
            _code.int_literal_constant(0xffff);
            _code.bitwise_and();
        }
    }
}
