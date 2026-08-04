using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;

namespace AVM.ClientGenerator.Compiler.Operators
{
    internal class DecimalOperatorCodeGenerator : NumericCodeGenerator
    {
        
        public DecimalOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }

        internal override void Increment()
        {
            _code.byte_literal_constant("0x00000001000000000000000000000000");
            Addition();


        }

        internal override void Decrement()
        {
            _code.byte_literal_constant("0x00000001000000000000000080000000");
            Addition();

        }

        
        private void ConvertMantissaToUBigInteger()
        {
            (CodeBuilder code, string label) res=_code.RegisterPredefined(PredefinedSubroutines.ConvertDecimalMantissaToUBigInteger);
            if (res.code != null) {
                
                res.code.dup();                         //... D D
                res.code.dup();                         //... D D D
                res.code.int_literal_constant(8);       //... D D D 8
                res.code.int_literal_constant(12);      //... D D D 8 12
                res.code.substring3();                  //... D D Hi
                res.code.swap();                        //... D Hi D
                res.code.int_literal_constant(4);       //... D Hi D 4
                res.code.int_literal_constant(8);       //... D Hi D 4 8
                res.code.substring3();                  //... D Hi Mid
                res.code.uncover(2);                    //... Hi Mid D
                res.code.int_literal_constant(0);       //... Hi Mid D 0
                res.code.int_literal_constant(4);       //... Hi Mid D 0 4
                res.code.substring3();                  //... Hi Mid Lo
                res.code.concat();                      //... Hi MidLo
                res.code.concat();                      //... HiMidLo
                

                res.code.retsub();
            }
            _code.callsub(res.label);



        }



        private void ConvertDecimalToInteger()
        {
            (CodeBuilder code, string label) res = _code.RegisterPredefined(PredefinedSubroutines.ConvertDecimalToInteger);
            if (res.code != null)
            {
                var c = res.code;
                c.dup();                                                //D D
                c.dup();                                                //D D D
                c.dup();                                                //D D D D
                c.dup();                                                //D D D D D
                c.int_literal_constant(8);                              //D D D D D 8
                c.int_literal_constant(12);                             //D D D D D 8 12
                c.substring3();                                         //D D D D Hi
                c.swap();                                               //D D D Hi D
                c.int_literal_constant(4);                              //D D D Hi D 4
                c.int_literal_constant(8);                              //D D D Hi D 4 8
                c.substring3();                                         //D D D Hi Mid
                c.uncover(2);                                           //D D Hi Mid D
                c.int_literal_constant(0);                              //D D Hi Mid D 0
                c.int_literal_constant(4);                              //D D Hi Mid D 0 4
                c.substring3();                                         //D D Hi Mid Lo
                c.concat();                                             //D D Hi MidLo
                c.concat();                                             //D D HiMidLo
                c.swap();                                               //D decb D
                c.int_literal_constant(13);                             //D decb D 13
                c.getbyte();                                            //D decb scale
                c.int_literal_constant(10);                             //D decb scale 10
                c.swap();                                               //D decb 10 scale 
                c.expw();                                               //D decb 10^scale(hi) 10^scale(lo)
                c.itob();                                               //D decb 10^scale(hi) 10^scale(lo)bytes 
                c.swap();                                               //D decb 10^scale(lo)bytes 10^scale(hi)  
                c.itob();                                               //D decb 10^scale(lo)bytes 10^scale(hi)bytes
                c.swap();                                               //D decb 10^scale(hi)bytes 10^scale(lo)bytes 
                c.concat();                                             //D decb 10^scale(bytes)
                c.b_divide();                                           //D decb/(10^scale)
                c.btoi();                                               //D result ---should scream on overflow
                c.swap();                                               //result D
                c.int_literal_constant(12);                             //result D 12
                c.getbyte();                                            //result sign
                var noneg = _code.ReserveLabel();                            //now negate if necessary
                c.bz(noneg);                                            //result
                c.dup();                                                //result result
                c.bz(noneg);                                            //result
                c.bitwise_not();
                c.int_literal_constant(1);
                c.plus();
                c.AddLabel(noneg);
                c.retsub();
            }
            _code.callsub(res.label);

        }

        private void RescaleDecimal()
        {
            (CodeBuilder code, string label) res = _code.RegisterPredefined(PredefinedSubroutines.RescaleDecimal);
            if (res.code != null)
            {
                var c = res.code;
                string exit = c.ReserveLabel();

                // ..resultscale sign result 
                
                
                //check result bitlen and if not in excess then just exit
                c.dup();                                                        // ..resultscale sign result result 
                c.bitlen();                                                     // ..resultscale sign result resultbitlen
                c.int_literal_constant(96);                                     // ..resultscale sign result resultbitlen 96
                c.less_than_or_equals();                                        // ..resultscale sign result 0/1
                c.bnz(exit);                                                    // ..resultscale sign result 

                //get the multiple of maxvalue that is in excess
                c.dup();                                                        // ..resultscale sign result result 
                c.dup();                                                        // ..resultscale sign result result result
                c.len();                                                        // ..resultscale sign result result resultlen
                c.int_literal_constant(12);                                     // ..resultscale sign result result resultlen 12
                c.minus();                                                      // ..resultscale sign result result resultlen-12
                c.int_literal_constant(0);                                      // ..resultscale sign result result resultlen-12 0
                c.swap();                                                       // ..resultscale sign result result 0 resultlen-12 
                c.extract3();                                                   // ..resultscale sign result excess (excess: number times maxvalue in excess of maxvalue)

                //get the total significant digits in excess
                c.dup();                                                        // ..resultscale sign result excess excess
                c.bitlen();                                                     // ..resultscale sign result excess excessbitlength
                c.int_literal_constant(77);                                     // ..resultscale sign result excess excessbitlength 77
                c.mulw();                                                       // ..resultscale sign result excess excessbitlength*77h l
                c.swap();                                                       // ..resultscale sign result excess excessbitlength*77l h
                c.pop();                                                        // ..resultscale sign result excess excessbitlength*77l
                c.int_literal_constant(8);                                      // ..resultscale sign result excess excessbitlength*77l 8
                c.shr();                                                        // ..resultscale sign result excess overscale
                //c.int_literal_constant(1);
                //c.plus();                                                       // ..resultscale sign result excess overscale (for unit excess we need to divide by 10, so the minimum rescale is 10)

                //get the power of ten for that scale
                c.dup();                                                        // ..resultscale sign result excess overscale overscale 
                c.int_literal_constant(10);                                     // ..resultscale sign result excess overscale overscale 10
                c.swap();                                                       // ..resultscale sign result excess overscale 10 overscale
                c.expw();                                                       // ..resultscale sign result excess overscale poweroftenhi powerfotenlo
                c.itob();                                                       // ..resultscale sign result excess overscale poweroftenhi powerfotenlob
                c.swap();                                                       // ..resultscale sign result excess overscale powerfotenlob poweroftenhi
                c.itob();                                                       // ..resultscale sign result excess overscale powerfotenlob poweroftenhib
                c.swap();                                                       // ..resultscale sign result excess overscale poweroftenhib powerfotenlob 
                c.concat();                                                     // ..resultscale sign result excess overscale poweroften

                c.dup();                                                        // ..resultscale sign result excess overscale poweroften poweroften 
                c.uncover(3);                                                   // ..resultscale sign result overscale poweroften poweroften excess
                
                var thisscale=c.ReserveLabel();
                c.b_greater_than();                                             // ..resultscale sign result overscale poweroften 0/1
                c.bnz(thisscale);                                               // ..resultscale sign result overscale poweroften 
                //excess scale is the higher of 2 possible values
          
                c.byte_literal_constant("0x0a");                                // ..resultscale sign result overscale poweroften 10
                c.b_multiply();                                                 // ..resultscale sign result overscale poweroften
                c.swap();                                                       // ..resultscale sign result poweroften overscale 
                c.int_literal_constant(1);                                      // ..resultscale sign result poweroften overscale 1
                c.plus();                                                       // ..resultscale sign result poweroften overscale
                c.swap();                                                       // ..resultscale sign result overscale poweroften

                c.AddLabel(thisscale);                                          // ..resultscale sign result overscale poweroften 

                //recalculate result with rounding
                c.uncover(2);                                                   // ..resultscale sign overscale poweroften result                                                
                c.byte_literal_constant("0x00");                                // ..resultscale sign overscale poweroften result 0
                c.concat();                                                     // ..resultscale sign overscale poweroften result*256
                c.swap();                                                       // ..resultscale sign overscale result*256 poweroften 
                c.b_divide();                                                   // ..resultscale sign overscale newresultwithroundingbyte
                c.dup();                                                        // ..resultscale sign overscale newresultwithroundingbyte newresultwithroundingbyte
                c.byte_literal_constant("0xff");                                // ..resultscale sign overscale newresultwithroundingbyte newresultwithroundingbyte ff
                c.b_bitwise_and();                                              // ..resultscale sign overscale newresultwithroundingbyte roundingbyte
                c.bitlen();                                                     // ..resultscale sign overscale newresultwithroundingbyte roundingbitlen
                c.int_literal_constant(7);                                    // ..resultscale sign overscale newresultwithroundingbyte roundingbyte 128
                c.greater_than_or_equals();                                     // ..resultscale sign overscale newresultwithroundingbyte 0/1
                string noRounding = c.ReserveLabel();
                c.bz(noRounding);                                               // ..resultscale sign overscale newresultwithroundingbyte 
                c.byte_literal_constant("0x0100");                               // ..resultscale sign overscale newresultwithroundingbyte 0x100
                c.b_plus();                                                     // ..resultscale sign overscale newresultwithroundingbyte    
                c.AddLabel(noRounding);                                         // ..resultscale sign overscale newresultwithroundingbyte 
                c.dup();                                                        // ..resultscale sign overscale newresultwithroundingbyte newresultwithroundingbyte
                c.len();                                                        // ..resultscale sign overscale newresultwithroundingbyte len
                c.int_literal_constant(1);                                      // ..resultscale sign overscale newresultwithroundingbyte len 1
                c.minus();                                                      // ..resultscale sign overscale newresultwithroundingbyte len-1
                c.int_literal_constant(0);                                      // ..resultscale sign overscale newresultwithroundingbyte len-1 0
                c.swap();                                                       // ..resultscale sign overscale newresultwithroundingbyte 0 len-1
                c.extract3();                                                   // ..resultscale sign overscale newresult

                //recalculate scale
                c.uncover(3);                                                   // ..sign overscale newresult resultscale
                c.uncover(2);                                                   // ..sign newresult resultscale overscale 
                c.minus();                                                      // ..sign newresult newscale
                c.cover(2);                                                     // ..newscale sign newresult 


                c.AddLabel(exit);                                               // ..resultscale sign result 
                c.retsub();
            }
            _code.callsub(res.label);

        }

        private void ConvertScaleSignAndResultToDecimal()
        {
            (CodeBuilder code, string label) res = _code.RegisterPredefined(PredefinedSubroutines.ConvertScaleSignAndResultToDecimal);
            if (res.code != null)
            {
              

                //scale sign(byte) result 
                res.code.dup();                                                         //scale sign(byte) result result
                res.code.len();                                                         //scale sign(byte) result len
                res.code.int_literal_constant(12);                                      //scale sign(byte) result len 12    
                res.code.swap();                                                        //scale sign(byte) result 12 len
                res.code.minus();                                                       //scale sign(byte) result 12-len
                res.code.swap();                                                        //scale sign(byte) 12-len result 
                res.code.byte_literal_constant("0x00000000000000000000000000000000");   //scale sign(byte) 12-len result zeros
                res.code.cover(2);                                                      //scale sign(byte) zeros 12-len result
                res.code.replace3();                                                    //scale sign(byte) resultpadded
                res.code.dup();                                                         //scale sign(byte) resultpadded resultpadded
                res.code.int_literal_constant(0);                                       //scale sign(byte) resultpadded resultpadded 0
                res.code.int_literal_constant(4);                                       //scale sign(byte) resultpadded resultpadded 0 4
                res.code.substring3();                                                  //scale sign(byte) resultpadded resulthi
                res.code.swap();                                                        //scale sign(byte) resulthi resultpadded 
                res.code.dup();                                                         //scale sign(byte) resulthi resultpadded resultpadded
                res.code.int_literal_constant(8);                                       //scale sign(byte) resulthi resultpadded resultpadded 8
                res.code.int_literal_constant(12);                                      //scale sign(byte) resulthi resultpadded resultpadded 8 12
                res.code.substring3();                                                  //scale sign(byte) resulthi resultpadded resultlo
                res.code.int_literal_constant(0);                                       //scale sign(byte) resulthi resultpadded resultlo 0
                res.code.swap();                                                        //scale sign(byte) resulthi resultpadded 0 resultlo        
                res.code.replace3();                                                    //scale sign(byte) resulthi resultpadded
                res.code.int_literal_constant(8);                                       //scale sign(byte) resulthi resultpadded 8
                res.code.uncover(2);                                                    //scale sign(byte) resultpadded 8 resulthi 
                res.code.replace3();                                                    //scale sign(byte) resultpadded
                res.code.swap();                                                        //scale resultpadded sign(byte)
                res.code.int_literal_constant(12);                                      //scale resultpadded sign(byte) 12
                res.code.swap();                                                        //scale resultpadded 12 sign(byte)                 
                res.code.setbyte();                                                     //scale resultpaddedwithsign
                res.code.swap();                                                        //resultpaddedwithsign scale 
                res.code.int_literal_constant(13);                                      //resultpaddedwithsign scale 13
                res.code.swap();                                                        //resultpaddedwithsign 13 scale 
                res.code.setbyte();                                                     //result 
                res.code.retsub();
            }
            _code.callsub(res.label);



        }


        internal override void Addition()
        {
            //one two
            _code.dup2();                       //.. one two one two
            _code.dup2();                       //.. one two one two one two
            _code.int_literal_constant(12);     //.. one two one two one two 12
            _code.getbyte();                    //.. one two one two one twosign
            _code.swap();                       //.. one two one two twosign one
            _code.int_literal_constant(12);     //.. one two one two twosign one 12
            _code.getbyte();                    //.. one two one two twosign onesign
            _code.cover(5);                     //.. onesign one two one two twosign
            _code.cover(5);                     //.. [twosign onesign] one two one two  
            _code.int_literal_constant(13);     // ..one two one two 13
            _code.getbyte();                    // ..one two one twoscale
            _code.swap();                       // ..one two twoscale one
            _code.int_literal_constant(13);     // ..one two twoscale one 13
            _code.getbyte();                    // ..one two twoscale onescale
            _code.dup2();                       // ..one two twoscale onescale twoscale onescale
            _code.greater_than_or_equals();     // ..one two twoscale onescale 0/1
            string endofscalecheck=_code.ReserveLabel();
            string twoscalegreaterthancheck = _code.ReserveLabel();
            _code.bz(twoscalegreaterthancheck); // ..one two twoscale onescale 
            _code.dig(1);                       // ..one two twoscale onescale twoscale
            _code.cover(4);                     // ..twoscale one two twoscale onescale 
            _code.minus();                      // ..twoscale one two twoscale-onescale 
            _code.int_literal_constant(10);     // ..twoscale one two twoscale-onescale 10
            _code.swap();                       // ..twoscale one two 10 twoscale-onescale
            _code.expw();                       // ..twoscale one two 10^scalediffh 10^scalediffl
            _code.itob();                       // ..twoscale one two 10^scalediffh 10^scaledifflbytes
            _code.swap();                       // ..twoscale one two 10^scaledifflbytes 10^scalediffh 
            _code.itob();                       // ..twoscale one two 10^scaledifflbytes 10^scalediffhbytes
            _code.swap();                       // ..twoscale one two 10^scaledifflbytes 10^scalediffhbytes
            _code.concat();                     // ..twoscale one two 10^scalediffbytes
            _code.cover(2);                     // ..twoscale 10^scalediffbytes one two 
            ConvertMantissaToUBigInteger();             // ..twoscale 10^scalediffbytes one twomantissa
            _code.swap();                       // ..twoscale 10^scalediffbytes twomantissa one 
            ConvertMantissaToUBigInteger();             // ..twoscale 10^scalediffbytes twomantissa onemantissa
            _code.uncover(2);                   // ..twoscale twomantissa onemantissa 10^scalediffbytes 
            _code.b_multiply();                 // ..twoscale twomantissa onemantissascaled
            _code.swap();                       // ..twoscale one two
            _code.b(endofscalecheck);
            
            _code.AddLabel(twoscalegreaterthancheck); // ..one two twoscale onescale
            _code.dup2();                       // ..one two twoscale onescale twoscale onescale
            _code.less_than();                  // ..one two twoscale onescale 0/1
            string onescalegreaterthantwo=_code.ReserveLabel();
            _code.bnz(onescalegreaterthantwo);  // ..one two twoscale onescale 
            _code.cover(3);                     // ..onescale one two twoscale 
            _code.pop();                        // ..onescale one two 
            ConvertMantissaToUBigInteger();             // ..onescale one twomantissa
            _code.swap();                   
            ConvertMantissaToUBigInteger();             // ..onescale twomantissa onemantissa
            _code.swap();                       // ..onescale onemantissa twomantissa         
            _code.b(endofscalecheck);           // ..onescale one two

            _code.AddLabel(onescalegreaterthantwo);// ..one two twoscale onescale
            _code.dup();                        // ..one two twoscale onescale onescale
            _code.cover(4);                     // ..onescale one two twoscale onescale
            _code.swap();                       // ..onescale one two onescale twoscale 
            _code.minus();                      // ..onescale one two onescale-twoscale 
            _code.int_literal_constant(10);     // ..onescale one two onescale-twoscale 10
            _code.swap();                       // ..onescale one two 10 onescale-twoscale 
            _code.expw();                       // ..onescale one two 10^onescale-twoscaleh 10^onescale-twoscalel
            _code.itob();                       // ..onescale one two 10^onescale-twoscaleh 10^onescale-twoscalelbytes 
            _code.swap();                       // ..onescale one two 10^onescale-twoscalelbytes 10^onescale-twoscaleh 
            _code.itob();                       // ..onescale one two 10^onescale-twoscalelbytes 10^onescale-twoscalehbytes 
            _code.swap();                       // ..onescale one two 10^onescale-twoscalehbytes 10^onescale-twoscalelbytes 
            _code.concat();                     // ..onescale one two 10^scalediffbytes
            _code.swap();                       // ..onescale one 10^scalediffbytes two 
            ConvertMantissaToUBigInteger();             // ..onescale one 10^scalediffbytes twomantissa 
            _code.b_multiply();                 // ..onescale one twomantissa
            _code.swap();                       // ..onescale twomantissa one 
            ConvertMantissaToUBigInteger();             // ..onescale twomantissa onemantissa
            _code.swap();                       // ..onescale one two

            string exit = _code.ReserveLabel();
            _code.AddLabel(endofscalecheck);    // ..[twosign onesign] resultscale one two
            _code.dig(4);                       // ..[twosign onesign] resultscale one two twosign
            _code.dig(4);                       // ..[twosign onesign] resultscale one two twosign onesign
            _code.equals();                     // ..[twosign onesign] resultscale one two 0/1
            string differentSigns = _code.ReserveLabel();
            _code.bz(differentSigns);           // ..[twosign onesign] resultscale one two  
            _code.b_plus();                     // ..[twosign onesign] resultscale result
            _code.uncover(3);                   // ..onesign resultscale result twosign
            _code.pop();                        // ..onesign resultscale result 
            _code.b(exit);                      // ..onesign resultscale result 

            _code.AddLabel(differentSigns);     // ..[twosign onesign] resultscale one two 
            _code.dup2();                       // ..[twosign onesign] resultscale one two one two
            _code.b_greater_than();             // ..[twosign onesign] resultscale one two 0/1
            string twoishigher = _code.ReserveLabel();
            _code.bz(twoishigher);              // ..[twosign onesign] resultscale one two 
            _code.b_minus();                    // ..[twosign onesign] resultscale result
            _code.uncover(3);                   // ..[onesign] resultscale result twosign
            _code.pop();                        // ..[onesign] resultscale result 
            _code.b(exit);                      // ..sign resultscale result 
          
            _code.AddLabel(twoishigher);        // ..[twosign onesign] resultscale one two 
            _code.swap();                       // ..[twosign onesign] resultscale one two 
            _code.b_minus();                    // ..[twosign onesign] resultscale result
            _code.uncover(3);                   // ..onesign resultscale result twosign
            _code.pop();                        // ..onesign resultscale result
            _code.uncover(2);                   // ..resultscale result onesign
            _code.int_literal_constant(128);    // ..resultscale result onesign 128
            _code.bitwise_xor();                // ..resultscale result onesign^0x80
            _code.cover(2);                     // ..sign resultscale result 
          
            _code.AddLabel(exit);               // ..sign resultscale result 
            _code.cover(2);                     // ..result sign resultscale  
            _code.swap();                       // ..result resultscale sign  
            _code.uncover(2);                   // ..resultscale sign result 

            RescaleDecimal();                   //necessary for cases when scale is high and mantissa high bitwidth
           
            ConvertScaleSignAndResultToDecimal();


        }

        internal override void BitwiseAnd()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void BitwiseOr()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void BitwiseXor()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void Division()
        {
            //dd dr    


            //decimal dividend and divisor on stack
            _code.dup2();                       // ..dd dr dd dr
            _code.int_literal_constant(12);     // ..dd dr dd dr 12
            _code.getbyte();                    // ..dd dr dd drsign
            _code.cover(3);                     // ..drsign dd dr dd
            _code.int_literal_constant(12);     // ..drsign dd dr dd 12
            _code.getbyte();                    // ..drsign dd dr ddsign
            _code.cover(3);                     // ..ddsign drsign dd dr  <-----from hence on ddsign and drsign prefix
            _code.dup2();                       // ..dd dr dd dr
            _code.int_literal_constant(13);     // ..dd dr dd dr 13
            _code.getbyte();                    // ..dd dr dd drscale
            _code.swap();                       // ..dd dr drscale dd 
            _code.int_literal_constant(13);     // ..dd dr drscale dd 13
            _code.getbyte();                    // ..dd dr drscale ddscale
            _code.uncover(3);                   // ..dr drscale ddscale dd
            _code.uncover(3);                   // ..drscale ddscale dd dr

            // .. drscale ddscale dd dr
            ConvertMantissaToUBigInteger();             // ..drscale ddscale dd dr
            _code.swap();                       // ..drscale ddscale dr dd
            ConvertMantissaToUBigInteger();             // ..drscale ddscale dr dd
            _code.byte_literal_constant("0x00");   // ..drscale ddscale dr dd 0
            _code.concat();                     // ..drscale ddscale dr dd0

            
            _code.dup();                        // .. drscale ddscale dr dd0 dd0
            _code.int_literal_constant(28);     // .. drscale ddscale dr dd0 dd0 28
            _code.byte_literal_constant("0x204FCE5E3E2502611000000000"); // .. drscale ddscale dr dd0 dd0 28 10^28*256
            _code.uncover(2);                        // .. drscale ddscale dr dd0 28 10^28*256 dd0 
            _code.byte_literal_constant("0x204FCE5E3E2502611000000000"); // .. drscale ddscale dr dd0 28 10^28*256 dd0 10^28*256
            _code.b_greater_than();             // .. drscale ddscale dr dd0 28 10^28*256 0/1
            string sigDigits28 = _code.ReserveLabel();
            _code.bz(sigDigits28);              // .. drscale ddscale dr dd0 28 10^28*256
            _code.pop();                        // .. drscale ddscale dr dd0 28 
            _code.pop();                        // .. drscale ddscale dr dd0
            _code.int_literal_constant(29);     // .. drscale ddscale dr dd0 29
            _code.byte_literal_constant("0x01431e0fae6d7217caa000000000"); // .. drscale ddscale dr dd0 29 10^29*256
            _code.AddLabel(sigDigits28);
            _code.cover(5);                     // .. normaliser drscale ddscale dr dd0 29 
            _code.cover(2);                     // .. normaliser drscale ddscale sd dr dd0 

            // .. drscale ddscale sd dr dd0 
            _code.dig(2);                       // .. normaliser drscale ddscale sd dr dd0 sd
            _code.uncover(4);                   // .. normaliser drscale sd dr dd0 sd ddscale
            _code.minus();                      // .. normaliser drscale sd dr dd0 sd-ddscale
            _code.uncover(4);                   // .. normaliser sd dr dd0 sd-ddscale drscale
            _code.plus();                       // .. normaliser sd dr dd0 (sd-ddscale+drscale)
            _code.int_literal_constant(10);     // .. normaliser sd dr dd0 (sd-ddscale+drscale) 10
            _code.swap();                       // .. normaliser sd dr dd0 10 (sd-ddscale+drscale)
            _code.expw();                       // .. normaliser sd dr dd0 (10^(sd-ddscale+drscale))hi (10^(sd-ddscale+drscale))lo
            _code.itob();                       // .. normaliser sd dr dd0 (10^(sd-ddscale+drscale))hi (10^(sd-ddscale+drscale))lobytes
            _code.swap();                       // .. normaliser sd dr dd0 (10^(sd-ddscale+drscale))lobytes (10^(sd-ddscale+drscale))hi 
            _code.itob();                       // .. normaliser sd dr dd0 (10^(sd-ddscale+drscale))lobytes (10^(sd-ddscale+drscale))hibytes 
            _code.swap();                       // .. normaliser sd dr dd0 (10^(sd-ddscale+drscale))hibytes (10^(sd-ddscale+drscale))lobytes  
            _code.concat();                     // .. normaliser sd dr dd0 (10^(sd-ddscale+drscale))bytes
            _code.b_multiply();                 // .. normaliser sd dr dd0*(10^(sd-ddscale+drscale))bytes
            _code.swap();                       // .. normaliser sd dd0*(10^(sd-ddscale+drscale))bytes dr 
            _code.b_divide();                   // .. normaliser sd result*256bytes 
            _code.dup();                        // .. normaliser sd result*256bytes result*256bytes
            _code.uncover(3);                   // .. sd result*256bytes result*256bytes normaliser
            _code.b_divide();                   // .. sd result*256bytes wholepart
            _code.dup();                        // .. sd result*256bytes wholepart wholepart
            _code.bitlen();                        // .. sd result*256bytes wholepart wholepartlength
            string noWholePart = _code.ReserveLabel();
            _code.bz(noWholePart);              // .. sd result*256bytes wholepart
            _code.dup();                        // .. sd result*256bytes wholepart wholepart
            _code.bitlen();                     // .. sd result*256bytes wholepart wholepartbitlen
            _code.int_literal_constant(77);     // .. sd result*256bytes wholepart wholepartbitlen 77
            _code.mulw();                       // .. sd result*256bytes wholepart wholepartbitlen*77h l
            _code.swap();                       // .. sd result*256bytes wholepart wholepartbitlen*77l h
            _code.pop();                        // .. sd result*256bytes wholepart wholepartbitlen*77l
            _code.int_literal_constant(8);      // .. sd result*256bytes wholepart wholepartbitlen*77l 8
            _code.shr();                        // .. sd result*256bytes wholepart poweroften
            _code.dup();                        // .. sd result*256bytes wholepart poweroften poweroften
            _code.cover(2);                     // .. sd result*256bytes poweroften wholepart poweroften 
            _code.int_literal_constant(10);     // .. sd result*256bytes poweroften wholepart poweroften 10
            _code.swap();                       // .. sd result*256bytes poweroften wholepart 10 poweroften
            _code.expw();                       // .. sd result*256bytes poweroften wholepart 10^poweroftenh 10^poweroftenl
            _code.itob();                       // .. sd result*256bytes poweroften wholepart 10^poweroftenh 10^poweroftenlbytes
            _code.swap();                       // .. sd result*256bytes poweroften wholepart 10^poweroftenlbytes 10^poweroftenh 
            _code.itob();                       // .. sd result*256bytes poweroften wholepart 10^poweroftenlbytes 10^poweroftenhbytes
            _code.swap();                       // .. sd result*256bytes poweroften wholepart 10^poweroftenhbytes 10^poweroftenlbytes 
            _code.concat();                     // .. sd result*256bytes poweroften wholepart base10value
            string scaleShiftIsPowerOfTen = _code.ReserveLabel();
            _code.b_less_than();                // .. sd result*256bytes poweroften 0/1
            _code.bnz(scaleShiftIsPowerOfTen);  // .. sd result*256bytes poweroften
            _code.int_literal_constant(1);      // .. sd result*256bytes poweroften 1    
            _code.plus();                       // .. sd result*256bytes poweroften+1    
            _code.AddLabel(scaleShiftIsPowerOfTen);

            _code.uncover(2);                   // .. result*256bytes scaleshift sd 
            _code.dup2();                       // .. result*256bytes scaleshift sd scaleshift sd
            _code.less_than_or_equals();        // .. result*256bytes scaleshift sd 0/1
            string scaleshift29     = _code.ReserveLabel();
            string scaleCalculated  = _code.ReserveLabel();
            _code.bz(scaleshift29);             // .. result*256bytes scaleshift sd 
            _code.dig(1);                       // .. result*256bytes scaleshift sd scaleshift
            _code.minus();                      // .. result*256bytes scaleshift newscale
          //  _code.int_literal_constant(28);     // .. result*256bytes scaleshift newscale 
            _code.swap();                       // .. result*256bytes newscale scaleshift  
            _code.b(scaleCalculated);           // 

            _code.AddLabel(scaleshift29);       // .. result*256bytes scaleshift sd 
            _code.pop();                        // .. result*256bytes scaleshift 
            _code.pop();                        // .. result*256bytes 
            _code.int_literal_constant(0);      // .. result*256bytes scale
            _code.int_literal_constant(29);     // .. result*256bytes scale scaleshift29
            
            _code.AddLabel(scaleCalculated);
            // .. drscale ddscale result*256bytes scale scaleshift(28 or 29)
            _code.int_literal_constant(10);     // .. result*256bytes scale scaleshift 10
            _code.swap();                       // .. result*256bytes scale 10 scaleshift
            _code.expw();                       // .. result*256bytes scale 10^scaleshifthi lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     // .. result*256bytes scale 10^scaleshift
            _code.swap();                       // .. result*256bytes 10^scaleshift scale
            _code.cover(2);                     // .. scale result*256bytes 10^scaleshift 
            _code.b_divide();                   // .. scale result*256bytes/scaleshiftdp
            string endOfWholePart = _code.ReserveLabel();
            _code.b(endOfWholePart);
            
            _code.AddLabel(noWholePart);        // .. sd result*256bytes wholepart
            _code.pop();                        // .. sd result*256bytes
            _code.swap();                       // .. result*256bytes sd 
            _code.pop();                        // .. result*256bytes
            _code.int_literal_constant(28);     // .. result scale
            _code.swap();                       // .. scale result
            
            _code.AddLabel(endOfWholePart);     // .. scale result
          //  _code.pop();                        // .. ddsign drsign scale result   <-----sign is back in play
            _code.uncover(3);                   // .. drsign scale result  ddsign
            _code.uncover(3);                   // .. scale result ddsign drsign
            _code.bitwise_xor();                // .. scale result  sign(byte)
            _code.swap();                       // .. scale sign(byte) result  
            _code.dup();                        // .. scale sign(byte) result result
            _code.dup();                        // .. scale sign(byte) result result result
            //check if result is zero           
            _code.dup();                        // .. scale sign(byte) result result result result
            _code.byte_literal_constant("0x00");// .. scale sign(byte) result result result result 0
            _code.b_equals();                   // .. scale sign(byte) result result result 0/1
            var nonzero = _code.ReserveLabel();
            _code.bz(nonzero);                  // .. scale sign(byte) result result result 
            _code.pop();                        // .. scale sign(byte) result result
            _code.pop();                        // .. scale sign(byte) result
            string halfcheckover = _code.ReserveLabel();
            _code.b(halfcheckover);
            _code.AddLabel(nonzero);
            _code.len();                        // .. scale sign(byte) result result resultlen
            _code.int_literal_constant(1);      // .. scale sign(byte) result result resultlen 1
            _code.minus();                      // .. scale sign(byte) result result resultlen-1
            _code.dup();                        // .. scale sign(byte) result result resultlen-1 resultlen-1
            _code.cover(2);                     // .. scale sign(byte) result resultlen-1 result resultlen-1 
            _code.getbyte();                    // .. scale sign(byte) result resultlen-1 remainder
            _code.int_literal_constant(128);    // .. scale sign(byte) result resultlen-1 remainder 128
            _code.greater_than_or_equals();     // .. scale sign(byte) result resultlen-1 0/1
            string lessthanhalf = _code.ReserveLabel();
            
            _code.bz(lessthanhalf);             // .. scale sign(byte) result resultlen-1
            _code.int_literal_constant(0);      // .. scale sign(byte) result resultlen-1 0
            _code.swap();                       // .. scale sign(byte) result 0 resultlen-1
            _code.substring3();                 // .. scale sign(byte) result
            _code.int_literal_constant(1);      // .. scale sign(byte) result 1
            _code.itob();                       // .. scale sign(byte) result 1b
            _code.b_plus();                     // .. scale sign(byte) result+1
            _code.b(halfcheckover);             // .. scale sign(byte) result+1
            
            _code.AddLabel(lessthanhalf);       // .. scale sign(byte) result resultlen-1
            _code.int_literal_constant(0);      // .. scale sign(byte) result resultlen-1 0
            _code.swap();                       // .. scale sign(byte) result 0 resultlen-1
            _code.substring3();                 // .. scale sign(byte) result
            
            
            _code.AddLabel(halfcheckover);      // .. scale sign(byte) result
            ConvertScaleSignAndResultToDecimal();



            



        }

        internal override void GreaterThan()
        {
            _code.swap();
            LessThan();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.swap();
            LessThanOrEqual();
        }

        internal override void LeftShift()
        {
              throw new ErrorDiagnosticException("E006");
        
        }

        internal override void LessThan()
        {
            string end = _code.ReserveLabel();
            //one two   
            //first check for edge case of both zeros:
            _code.dup2();                       //.. one two one two
            _code.b_bitwise_or();               //.. one two or
            _code.byte_literal_constant("0xffffffffffffffffffffffff00000000"); //.. one two 0xffffffffffffffffffffffff00000000
            _code.b_bitwise_and();              //.. one two 0/1
            _code.bitlen();
            var nonzeros = _code.ReserveLabel();
            _code.bnz(nonzeros);
            _code.pop();
            _code.pop();
            _code.int_literal_constant(0);
            _code.b(end);
            _code.AddLabel(nonzeros);

            //first get signs
            _code.dup2();                       //.. one two one two 
            _code.b_bitwise_xor();              //.. one two xord
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two signsame?
            var sameSign = _code.ReserveLabel();
            _code.bz(sameSign);                 //.. one two
            //different sign - if the second is 0(+) and the first 1(-) then the result is 1
            _code.dup();                        //.. one two two
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two twosign
            _code.not();
            _code.cover(2);
            _code.pop();
            _code.pop();
            _code.b(end);//exit with if second is negative then result is 0 

            _code.AddLabel(sameSign);
            //same sign, but if both are negative then we need to invert the result, so just swap:
            _code.dup();                        //.. one two two
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two twosign
            var bothPositive = _code.ReserveLabel();
            _code.bz(bothPositive);             //.. one two
            _code.swap();                       //.. two one
            //values are now inverted so the result is the same as if they were different signs


            _code.AddLabel(bothPositive);
            _code.dup2();                     //.. one two one two 
            ConvertMantissaToUBigInteger();             //.. one two one twomantissa
            _code.swap();                       //.. one two twomantissa one 
            ConvertMantissaToUBigInteger();             //.. one two twomantissa onemantissa
            _code.uncover(3);                   //.. two twomantissa onemantissa one
            _code.uncover(3);                   //.. twomantissa onemantissa one two
            _code.int_literal_constant(13);     //.. twomantissa onemantissa one two 13
            _code.getbyte();                    //.. twomantissa onemantissa one 2scale
            _code.swap();                       //.. twomantissa onemantissa 2scale one 
            _code.int_literal_constant(13);     //.. twomantissa onemantissa 2scale one 13
            _code.getbyte();                    //.. twomantissa onemantissa 2scale 1scale
            _code.dup2();                       //.. twomantissa onemantissa 2scale 1scale 2scale 1scale
            _code.greater_than();               //.. twomantissa onemantissa 2scale 1scale 0/1    
            string scale2lowerorequal = _code.ReserveLabel();
            _code.bz(scale2lowerorequal);       //.. twomantissa onemantissa 2scale 1scale 
            _code.minus();                      //.. twomantissa onemantissa scalediff
            _code.int_literal_constant(10);     //.. twomantissa onemantissa scalediff 10
            _code.swap();                       //.. twomantissa onemantissa 10 scalediff
            _code.expw();                       //.. twomantissa onemantissa 10^scalediffhi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. twomantissa onemantissa 10^scalediff
            _code.b_multiply();                 //.. twomantissa scaleduponemantissa
            _code.swap();                       //.. onemantissa(scaled) twomantissa
            string docomparison = _code.ReserveLabel();
            _code.b(docomparison);              //.. onemantissa(scaled) twomantissa        

            _code.AddLabel(scale2lowerorequal); //.. twomantissa onemantissa 2scale 1scale 
            _code.minus();                      //.. twomantissa onemantissa scalediff
            _code.dup();                        //.. twomantissa onemantissa scalediff scalediff
            string scalesequal = _code.ReserveLabel();
            _code.bz(scalesequal);              //.. twomantissa onemantissa scalediff 
            _code.int_literal_constant(10);     //.. twomantissa onemantissa scalediff 10
            _code.swap();                       //.. twomantissa onemantissa 10 scalediff
            _code.expw();                       //.. twomantissa onemantissa 10^scalediffhi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. twomantissa onemantissa 10^scalediff
            _code.uncover(2);                   //.. onemantissa 10^scalediff twomantissa 
            _code.b_multiply();                 //.. onemantissa twomantissascaled
            _code.b(docomparison);              //

            _code.AddLabel(scalesequal);        //.. twomantissa onemantissa scalediff 
            _code.pop();
            _code.swap();

            _code.AddLabel(docomparison);       //..  onemantissa twomantissa        
            _code.b_less_than();

            _code.AddLabel(end);
        }


        internal override void EqualsExpression()
        {
            string end = _code.ReserveLabel();

            //one two   
            _code.dup2();                       //.. one two one two 
            ConvertMantissaToUBigInteger();     //.. one two one twomantissa
            _code.swap();                       //.. one two twomantissa one 
            ConvertMantissaToUBigInteger();     //.. one two twomantissa onemantissa
            _code.dup2();                       //.. one two twomantissa onemantissa twomantissa onemantissa
            _code.b_bitwise_or();               //.. one two twomantissa onemantissa ordmantissas
            _code.bitlen();
            var nonzeros = _code.ReserveLabel();
            _code.bnz(nonzeros);                //.. one two twomantissa onemantissa
            _code.pop();
            _code.pop();
            _code.pop();
            _code.pop();
            _code.int_literal_constant(1);      // both zero so exit with true
            _code.b(end);

            _code.AddLabel(nonzeros);
            
            _code.dig(3);                       //.. one two twomantissa onemantissa one
            _code.dig(3);                       //.. one two twomantissa onemantissa one two
            _code.b_bitwise_xor();              //.. one two twomantissa onemantissa xord
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two twomantissa onemantissa xordsignbyte
            string signsAreEqual = _code.ReserveLabel(); 
            _code.bz(signsAreEqual);           //.. one two twomantissa onemantissa 
            _code.pop();
            _code.pop();
            _code.pop();
            _code.pop();
            _code.int_literal_constant(0);
            _code.b(end);   

_code.AddLabel(signsAreEqual);                  //.. one two twomantissa onemantissa 
         

            _code.uncover(3);                   //.. two twomantissa onemantissa one
            _code.uncover(3);                   //.. twomantissa onemantissa one two
            _code.int_literal_constant(13);     //.. twomantissa onemantissa one two 13
            _code.getbyte();                    //.. twomantissa onemantissa one 2scale
            _code.swap();                       //.. twomantissa onemantissa 2scale one 
            _code.int_literal_constant(13);     //.. twomantissa onemantissa 2scale one 13
            _code.getbyte();                    //.. twomantissa onemantissa 2scale 1scale
            _code.dup2();                       //.. twomantissa onemantissa 2scale 1scale 2scale 1scale
            _code.greater_than();               //.. twomantissa onemantissa 2scale 1scale 0/1    
            string scale2lowerorequal = _code.ReserveLabel();
            _code.bz(scale2lowerorequal);       //.. twomantissa onemantissa 2scale 1scale 
         
            //scale 2 is higher
            _code.minus();                      //.. twomantissa onemantissa scalediff
            _code.int_literal_constant(10);     //.. twomantissa onemantissa scalediff 10
            _code.swap();                       //.. twomantissa onemantissa 10 scalediff
            _code.expw();                       //.. twomantissa onemantissa 10^scalediffhi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. twomantissa onemantissa 10^scalediff
            _code.b_multiply();                 //.. twomantissa scaleduponemantissa
            string docomparison = _code.ReserveLabel();
            _code.b(docomparison);              //.. onemantissa(scaled) twomantissa        

            //scale 2 is lower or equal
            _code.AddLabel(scale2lowerorequal); //.. twomantissa onemantissa 2scale 1scale 
            _code.swap();
            _code.minus();                      //.. twomantissa onemantissa scalediff
            _code.dup();                        //.. twomantissa onemantissa scalediff scalediff
            string scalesequal = _code.ReserveLabel();
            _code.bz(scalesequal);              //.. twomantissa onemantissa scalediff 
           
            //scale 2 is lower
            _code.int_literal_constant(10);     //.. twomantissa onemantissa scalediff 10
            _code.swap();                       //.. twomantissa onemantissa 10 scalediff
            _code.expw();                       //.. twomantissa onemantissa 10^scalediffhi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. twomantissa onemantissa 10^scalediff
            _code.uncover(2);                   //.. onemantissa 10^scalediff twomantissa 
            _code.b_multiply();                 //.. onemantissa twomantissascaled
            _code.b(docomparison);              //

            _code.AddLabel(scalesequal);        //.. twomantissa onemantissa scalediff 
            _code.pop();
            

            _code.AddLabel(docomparison);       //..  onemantissa twomantissa        
            _code.b_equals();                   //.. 0/1

            _code.AddLabel(end);

        }

        internal override void NotEquals()
        {
            EqualsExpression();
            _code.int_literal_constant(1);
            _code.bitwise_xor();
        }

        internal override void LessThanOrEqual()
        {
            string end = _code.ReserveLabel();
            //one two   

            //first check for edge case of both zeros:
            _code.dup2();                       //.. one two one two
            _code.b_bitwise_or();               //.. one two or
            _code.byte_literal_constant("0xffffffffffffffffffffffff00000000"); //.. one two 0xffffffffffffffffffffffff00000000
            _code.b_bitwise_and();              //.. one two 0/1
            _code.bitlen();
            var nonzeros = _code.ReserveLabel();
            _code.bnz(nonzeros);
            _code.pop();
            _code.pop();
            _code.int_literal_constant(1);
            _code.b(end);
            

            _code.AddLabel(nonzeros);
            //get signs
            _code.dup2();                       //.. one two one two 
            _code.b_bitwise_xor();              //.. one two xord
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two signsame?
            var sameSign = _code.ReserveLabel();
            _code.bz(sameSign);                 //.. one two
            //different sign - if the second is 0(+) and the first 1(-) then the result is 1
            _code.dup();                        //.. one two two
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two twosign
            _code.not();
            _code.cover(2);
            _code.pop();
            _code.pop();
            _code.b(end);//exit with if second is negative then result is 0 

            _code.AddLabel(sameSign);
            //same sign, but if both are negative then we need to invert the result, so just swap:
            _code.dup();                        //.. one two two
            _code.int_literal_constant(12);
            _code.getbyte();                    //.. one two twosign
            var bothPositive = _code.ReserveLabel();
            _code.bz(bothPositive);             //.. one two
            _code.swap();                       //.. two one
            //values are now inverted so the result is the same as if they were different signs


            _code.AddLabel(bothPositive);
            _code.dup2();                     //.. one two one two 
            ConvertMantissaToUBigInteger();             //.. one two one twomantissa
            _code.swap();                       //.. one two twomantissa one 
            ConvertMantissaToUBigInteger();             //.. one two twomantissa onemantissa
            _code.uncover(3);                   //.. two twomantissa onemantissa one
            _code.uncover(3);                   //.. twomantissa onemantissa one two
            _code.int_literal_constant(13);     //.. twomantissa onemantissa one two 13
            _code.getbyte();                    //.. twomantissa onemantissa one 2scale
            _code.swap();                       //.. twomantissa onemantissa 2scale one 
            _code.int_literal_constant(13);     //.. twomantissa onemantissa 2scale one 13
            _code.getbyte();                    //.. twomantissa onemantissa 2scale 1scale
            _code.dup2();                       //.. twomantissa onemantissa 2scale 1scale 2scale 1scale
            _code.greater_than();               //.. twomantissa onemantissa 2scale 1scale 0/1    
            string scale2lowerorequal = _code.ReserveLabel();
            _code.bz(scale2lowerorequal);       //.. twomantissa onemantissa 2scale 1scale 
            _code.minus();                      //.. twomantissa onemantissa scalediff
            _code.int_literal_constant(10);     //.. twomantissa onemantissa scalediff 10
            _code.swap();                       //.. twomantissa onemantissa 10 scalediff
            _code.expw();                       //.. twomantissa onemantissa 10^scalediffhi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. twomantissa onemantissa 10^scalediff
            _code.b_multiply();                 //.. twomantissa scaleduponemantissa
            _code.swap();                       //.. onemantissa(scaled) twomantissa
            string docomparison = _code.ReserveLabel();
            _code.b(docomparison);              //.. onemantissa(scaled) twomantissa        

            _code.AddLabel(scale2lowerorequal); //.. twomantissa onemantissa 2scale 1scale 
            _code.minus();                      //.. twomantissa onemantissa scalediff
            _code.dup();                        //.. twomantissa onemantissa scalediff scalediff
            string scalesequal = _code.ReserveLabel();
            _code.bz(scalesequal);              //.. twomantissa onemantissa scalediff 
            _code.int_literal_constant(10);     //.. twomantissa onemantissa scalediff 10
            _code.swap();                       //.. twomantissa onemantissa 10 scalediff
            _code.expw();                       //.. twomantissa onemantissa 10^scalediffhi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. twomantissa onemantissa 10^scalediff
            _code.uncover(2);                   //.. onemantissa 10^scalediff twomantissa 
            _code.b_multiply();                 //.. onemantissa twomantissascaled
            _code.b(docomparison);              //

            _code.AddLabel(scalesequal);        //.. twomantissa onemantissa scalediff 
            _code.pop();
            _code.swap();

            _code.AddLabel(docomparison);       //..  onemantissa twomantissa        
            _code.b_less_than_or_equal();

            _code.AddLabel(end);
        }

        internal override void Multiplication()
        {
            _code.dup2();                       //.. one two one two
            ConvertMantissaToUBigInteger();             //.. one two one twomantissa
            _code.swap();                       //.. one two twomantissa one 
            ConvertMantissaToUBigInteger();             //.. one two twomantissa onemantissa
            _code.b_multiply();                 //.. one two result
            _code.byte_literal_constant("0x204FCE5E3E2502611000000000"); //.. one two result scaler (nb scaler is 10^28 <<8 for the fraction byte)
            _code.b_multiply();                 //.. one two scaledresult

            _code.dig(2);                       //.. one two scaledresult one
            _code.int_literal_constant(13);     //.. one two scaledresult one 13
            _code.getbyte();                    //.. one two scaledresult onescale
            _code.dig(2);                       //.. one two scaledresult onescale two
            _code.int_literal_constant(13);     //.. one two scaledresult onescale two 13
            _code.getbyte();                    //.. one two scaledresult onescale twoscale
            _code.plus();                       //.. one two scaledresult onescale+twoscale
            _code.int_literal_constant(10);     //.. one two scaledresult onescale+twoscale 10
            _code.swap();                       //.. one two scaledresult 10 onescale+twoscale 
            _code.expw();                       //.. one two scaledresult 10^onescale+twoscalehi and lo
            _code.itob();                       //.. one two scaledresult 10^onescale+twoscalehi and lobytes
            _code.swap();                       //.. one two scaledresult normaliserlobytes normaliserhi
            _code.itob();                       //.. one two scaledresult normaliserlobytes normaliserhibytes
            _code.swap();                       //.. one two scaledresult normaliserhibytes normaliserlobytes     
            _code.concat();                     //.. one two scaledresult normaliser
            _code.b_divide();                   //.. one two scalednormalisedresult

            _code.dup();                        //.. one two scalednormalisedresult scalednormalisedresult
            _code.byte_literal_constant("0x204FCE5E3E2502611000000000"); //.. one two scalednormalisedresult scalednormalisedresult scaler
            _code.b_divide();                   //.. one two scalednormalisedresult wholepart
          
    //        _code.dup();                        //.. one two scalednormalisedresult wholepart wholepart
    //        _code.bitlen();                        //.. one two scalednormalisedresult wholepart wholepartlen
    //        string leniszero = _code.ReserveLabel();
            string endrescale = _code.ReserveLabel();
  //          _code.bz(leniszero);                //.. one two scalednormalisedresult wholepart 

            _code.dup();                        //.. one two scalednormalisedresult wholepart wholepart 
            _code.bitlen();                     //.. one two scalednormalisedresult wholepart wholepartbitlen
            _code.int_literal_constant(77);     //.. one two scalednormalisedresult wholepart wholepartbitlen 77
            _code.multiply();                   //.. one two scalednormalisedresult wholepart wholepartbitlen*77
            _code.int_literal_constant(8);      //.. one two scalednormalisedresult wholepart wholepartbitlen*77 8
            _code.shr();                        //.. one two scalednormalisedresult wholepart poweroften
            _code.dup();                        //.. one two scalednormalisedresult wholepart poweroften poweroften
            _code.cover(2);                     //.. one two scalednormalisedresult poweroften wholepart poweroften 
            _code.int_literal_constant(10);     //.. one two scalednormalisedresult poweroften wholepart poweroften 10
            _code.swap();                       //.. one two scalednormalisedresult poweroften wholepart 10 poweroften 
            _code.expw();                       //.. one two scalednormalisedresult poweroften wholepart 10^poweroftenh and lo 
            _code.itob();                       //.. one two scalednormalisedresult poweroften wholepart 10^poweroftenh and lobytes 
            _code.swap();                       //.. one two scalednormalisedresult poweroften wholepart 10^poweroftenlobytes and hi
            _code.itob();                       //.. one two scalednormalisedresult poweroften wholepart 10^poweroftenlobytes and hibytes
            _code.swap();                       //.. one two scalednormalisedresult poweroften wholepart 10^poweroftenhi and lo bytes
            _code.concat();                     //.. one two scalednormalisedresult poweroften wholepart 10^poweroften 
            string sigdigitsend=_code.ReserveLabel();
            _code.b_less_than();                //.. one two scalednormalisedresult poweroften 0/1
            _code.bnz(sigdigitsend);            //.. one two scalednormalisedresult poweroften  (sigdigits is power of 10)
            _code.int_literal_constant(1);      //.. one two scalednormalisedresult poweroften 1
            _code.plus();                       //.. one two scalednormalisedresult poweroften+1
            
            _code.AddLabel(sigdigitsend);       //.. one two scalednormalisedresult sigdigits
            _code.dup();                        //.. one two scalednormalisedresult sigdigits sigdigits
            _code.int_literal_constant(28);     //.. one two scalednormalisedresult sigdigits sigdigits 28
            _code.greater_than();               //.. one two scalednormalisedresult sigdigits 0/1
            string lowsigdigits = _code.ReserveLabel();
            _code.bz(lowsigdigits);             //.. one two scalednormalisedresult sigdigits
            _code.pop();                        //.. one two scalednormalisedresult 
            _code.byte_literal_constant("0x204FCE5E3E25026110000000"); //.. one two scalednormalisedresult 10^28
            _code.b_divide();                   //.. one two result
            _code.int_literal_constant(0);      //.. one two result scale    
            _code.b(endrescale);
           
            _code.AddLabel(lowsigdigits);       //.. one two scalednormalisedresult sigdigits
            _code.dup();                        //.. one two scalednormalisedresult sigdigits sigdigits
            _code.int_literal_constant(28);     //.. one two scalednormalisedresult sigdigits sigdigits 28
            _code.swap();                       //.. one two scalednormalisedresult sigdigits 28 sigdigits
            _code.minus();                      //.. one two scalednormalisedresult sigdigits scale
            _code.swap();                       //.. one two scalednormalisedresult scale sigdigits
            _code.int_literal_constant(10);     //.. one two scalednormalisedresult scale sigdigits 10
            _code.swap();                       //.. one two scalednormalisedresult scale 10 sigdigits 
            _code.expw();                       //.. one two scalednormalisedresult scale 10^sigdigitshi and lo
            _code.itob();
            _code.swap();
            _code.itob();
            _code.swap();
            _code.concat();                     //.. one two scalednormalisedresult scale 10^sigdigits
            _code.uncover(2);                   //.. one two scale 10^sigdigits scalednormalisedresult 
            _code.swap();                       //.. one two scale scalednormalisedresult 10^sigdigits 
            _code.b_divide();                   //.. one two scale result
            _code.swap();                       //.. one two result scale
            _code.b(endrescale);

          //  _code.AddLabel(leniszero);          //.. one two scalednormalisedresult wholepart 
            _code.pop();
            _code.byte_literal_constant("0x204FCE5E3E25026110000000"); //.. one two scalednormalisedresult 10^28
            _code.b_divide();                   //.. one two result
            _code.int_literal_constant(28);     //.. one two result scale

            _code.AddLabel(endrescale);         //.. one two result scale
            _code.uncover(3);                   //.. two result scale one
            _code.int_literal_constant(12);     //.. two result scale one 12
            _code.getbyte();                    //.. two result scale signone
            _code.uncover(3);                   //.. result scale signone two
            _code.int_literal_constant(12);     //.. result scale signone two 12
            _code.getbyte();                    //.. result scale signone signtwo
            _code.bitwise_xor();                //.. result scale resultsign
            _code.uncover(2);                   //..  scale resultsign result

            _code.dup();                        // .. scale sign(byte) result result
            _code.dup();                        // .. scale sign(byte) result result result
            string halfcheckover = _code.ReserveLabel();
            //check if the result is zero
            _code.dup();
            _code.bitlen();
            string nonzero= _code.ReserveLabel();
            _code.bnz(nonzero);
            _code.pop();
            _code.pop();
            _code.b(halfcheckover);

            _code.AddLabel(nonzero);
            _code.len();                        // .. scale sign(byte) result result resultlen
            _code.int_literal_constant(1);      // .. scale sign(byte) result result resultlen 1
            _code.minus();                      // .. scale sign(byte) result result resultlen-1
            _code.dup();                        // .. scale sign(byte) result result resultlen-1 resultlen-1
            _code.cover(2);                     // .. scale sign(byte) result resultlen-1 result resultlen-1 
            _code.getbyte();                    // .. scale sign(byte) result resultlen-1 remainder
            _code.int_literal_constant(128);    // .. scale sign(byte) result resultlen-1 remainder 128
            _code.greater_than_or_equals();     // .. scale sign(byte) result resultlen-1 0/1
            string lessthanhalf = _code.ReserveLabel();
          
            _code.bz(lessthanhalf);             // .. scale sign(byte) result resultlen-1
            _code.int_literal_constant(0);      // .. scale sign(byte) result resultlen-1 0
            _code.swap();                       // .. scale sign(byte) result 0 resultlen-1
            _code.substring3();                 // .. scale sign(byte) result
            _code.int_literal_constant(1);      // .. scale sign(byte) result 1
            _code.itob();                       // .. scale sign(byte) result 1b
            _code.b_plus();                     // .. scale sign(byte) result+1
            _code.b(halfcheckover);             // .. scale sign(byte) result+1
            _code.AddLabel(lessthanhalf);       // .. scale sign(byte) result resultlen-1
            _code.int_literal_constant(0);      // .. scale sign(byte) result resultlen-1 0
            _code.swap();                       // .. scale sign(byte) result 0 resultlen-1
            _code.substring3();                 // .. scale sign(byte) result
            _code.AddLabel(halfcheckover);      // .. scale sign(byte) result


            ConvertScaleSignAndResultToDecimal();


        }

        internal override void Remainder()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void RightShift()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void Subtraction()
        {
            //one two
            _code.byte_literal_constant("0x00000000000000000000000080000000"); //one two signbyte
            _code.b_bitwise_xor();
            Addition();
            
            
        }


        internal override void Negate()
        {
            _code.byte_literal_constant("0x00000000000000000000000080000000"); 
            _code.b_bitwise_xor();
        }

        internal override void OnesComplement()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void LogicalNegate()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void CastToObject()
        {
            //nop
        }

        internal override void CastToDecimal()
        {
            //nop
        }

        internal override void CastToByte()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xff);
            _code.bitwise_and();
        }

        internal override void CastToInt()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xffffffff);
            _code.bitwise_and();
        }

        internal override void CastToLong()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xffffffffffffffff);
            _code.bitwise_and();
        }

        internal override void CastToSbyte()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xff);
            _code.bitwise_and();
        }

        internal override void CastToShort()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xffff);
            _code.bitwise_and();
        }

        internal override void CastToUInt()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xffffffff);
            _code.bitwise_and();
        }

        internal override void CastToULong()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xffffffffffffffff);
            _code.bitwise_and();
        }

        internal override void CastToUShort()
        {
            ConvertDecimalToInteger();
            _code.int_literal_constant(0xffff);
            _code.bitwise_and();
        }
    }
}
