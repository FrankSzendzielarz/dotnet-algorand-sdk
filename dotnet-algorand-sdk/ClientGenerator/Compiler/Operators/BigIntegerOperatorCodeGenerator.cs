using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using System;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.Operators
{
    internal class BigIntegerOperatorCodeGenerator : TypeCodeGenerator
    {
        internal static string maxBigIntegerMask = "0x" + String.Concat(Enumerable.Repeat("00", 64));
        internal static string negativeOne = "0x" + String.Concat(Enumerable.Repeat("ff", 64));


        public BigIntegerOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }

        internal override void Addition()
        {
            //simple two's complement addition
            _code.b_plus();
        }

        internal override void BitwiseAnd()
        {
            _code.b_bitwise_and();
        }

        internal override void BitwiseOr()
        {
            _code.b_bitwise_or();
        }
        internal override void BitwiseXor()
        {
            _code.b_bitwise_xor();
        }

        internal override void Division()
        {
      

            //check signs
            _code.dup2();
            
            _code.b_bitwise_xor(); //indicates if signs were different
            //pump to max size
            _code.byte_literal_constant(maxBigIntegerMask);
            _code.b_bitwise_or();
            _code.int_literal_constant(0);
            _code.getbit();

            _code.cover(2);

            //divisor / denominator
            _code.dup();
            _code.int_literal_constant(0);
            _code.getbit();                                  //sign bit
            string positiveDivisor = _code.ReserveLabel();
            _code.bz(positiveDivisor);
            //negate divisor
            _code.b_bitwise_not();
            _code.byte_literal_constant("0x01");
            _code.b_plus();

            _code.AddLabel(positiveDivisor);

            //dividend / numerator
            _code.dig(1);
            _code.int_literal_constant(0);
            _code.getbit();
            string positiveDividend = _code.ReserveLabel();
            _code.bz(positiveDividend);
            //negate dividend
            _code.swap();
            _code.b_bitwise_not();
            _code.byte_literal_constant("0x01");
            _code.b_plus();
            _code.swap();

            _code.AddLabel(positiveDividend);

            //do the division
            _code.b_divide();

            //check if sign should be swapped
            _code.swap();
            string exit = _code.ReserveLabel();
   
            _code.bz(exit); //xor from above
            _code.dup();
            _code.byte_literal_constant("0x00");
            _code.b_equals();
            _code.bnz(exit);//avoid negating zero and causing overflow
            _code.byte_literal_constant(maxBigIntegerMask);
            _code.b_bitwise_or();
            _code.b_bitwise_not();
            _code.byte_literal_constant("0x01");
            _code.b_plus();
            
            _code.AddLabel(exit);
            
           
            

        }

        internal override void LeftShift()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void Multiplication()
        {
            _code.b_multiply();
          
        }


        internal override void GreaterThan()
        {
            _code.dup2();                           //A B A B
            _code.b_bitwise_xor();                  //A B sign-in-top-bit
            _code.int_literal_constant(0);
            _code.getbit();                      //A B sign
            string compare = _code.ReserveLabel();  //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.b_greater_than();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.b_bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(0);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();  //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.b_greater_than_or_equal();
        }
        internal override void LessThan()
        {
            _code.dup2();                           //A B A B
            _code.b_bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(0);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.b_less_than();
        }

        internal override void LessThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.b_bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(0);
            _code.getbit();                      //A B sign
            string compare = _code.ReserveLabel();  //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.b_less_than_or_equal();
        }
  
        internal override void Remainder()
        {
                                                                        //
            _code.dup2();                                               // a b a b
            Division();                                                 // a b a/b
            _code.dup();                                                // a b a/b a/b
            string exitAsZero = _code.ReserveLabel();                   //
            string exit = _code.ReserveLabel();                         //
                                                                        //         
                                                                        
            _code.byte_literal_constant("0x00");                        // a b a/b a/b 0  
            _code.b_equals();                                           // a b a/b 1
            _code.bnz(exitAsZero);                                      // a b a/b
                                                                        //
                                                                        //
            //multiply result by B,                                     //
            _code.b_multiply();                                         // ... A (B * C)
            _code.dup();                                                // b*c b*c
            _code.len();                                                // b*c 67
            _code.dup();                                                // b*c 67 67
            _code.int_literal_constant(64);                             // b*c 67 67 64
            var smallnum=_code.ReserveLabel();                          // 
            _code.less_than_or_equals();                                // b*c 67 1
            _code.bnz(smallnum);                                        // b*c 67
            _code.int_literal_constant(64);                             // b*c 67 64
            _code.minus();                                              // b*c 3
            _code.int_literal_constant(64);                             //b*c 3 64
            _code.extract3();                                           // b*c
            _code.dup();                                                // b*c b*c


            _code.AddLabel(smallnum);
            _code.pop();                                                    //b*c 
            //A-B to get remainder, so get -B                           //
            _code.byte_literal_constant(maxBigIntegerMask);
            _code.b_bitwise_or();                                                               // a b*c
            
            _code.b_bitwise_not();                                      // a !B*c
            _code.byte_literal_constant("0x01");                           //  a !B*c 1
            _code.b_plus();                                             // ... a -b*c
            _code.b_plus();                                             // ... A-(B*C)
            _code.b(exit);                                              // 
                                                                        //
            _code.AddLabel(exitAsZero);                                 //a b a/b
            _code.pop();                                                //
            _code.pop();                                                //


            _code.AddLabel(exit);
            
            
        }

        internal override void RightShift()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void Subtraction()
        {
            string exit = _code.ReserveLabel();                         //       
            _code.dup();                                                // ... A B B
            _code.byte_literal_constant("0x00");                                             // ... A B B 0
            _code.b_equals();                                           // ... A B 0/1
            _code.bnz(exit);                                            // ... A B
                                                                        //
                                                                        //two's complement                                          //
            _code.byte_literal_constant(maxBigIntegerMask);
            _code.b_bitwise_or();
            _code.b_bitwise_not();                                      // ... A !B
            _code.byte_literal_constant("0x01");                           // ... A !B 1
            _code.b_plus();                                             // ... A -B
                                                                        //
            _code.b_plus();                                             // ... A-B
            _code.dup();                                                // ... A-B A-B
                                                                        //
            _code.AddLabel(exit);                                       //
            _code.pop();                                                // ... result

           
        }

        internal override void Negate()
        {
            _code.byte_literal_constant(maxBigIntegerMask);
            _code.b_bitwise_or();
            _code.b_bitwise_not();
            _code.byte_literal_constant("0x01");
            _code.b_plus();
        }

        internal override void OnesComplement()
        {
            _code.byte_literal_constant(maxBigIntegerMask);
            _code.b_bitwise_or();
            _code.b_bitwise_not();
        }

        internal override void LogicalNegate()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void Increment()
        {
            _code.byte_literal_constant("0x01");
            _code.b_plus();

        }

        internal override void Decrement()
        {
            var exit = _code.ReserveLabel();
            _code.dup();
            _code.byte_literal_constant("0x00");
            _code.b_equals();
            var notZero=_code.ReserveLabel();
            _code.bz(notZero);
            _code.byte_literal_constant(negativeOne);
            _code.b(exit);
            _code.AddLabel(notZero);
            _code.byte_literal_constant("0x01");
            _code.b_minus();
            _code.AddLabel(exit);
        }
    }
}
