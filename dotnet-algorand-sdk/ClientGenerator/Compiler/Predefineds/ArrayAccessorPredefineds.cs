using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class ArrayAccessorPredefineds : ITypePredefined, IArrayTypePredefined
    {
        CodeBuilder code;
        IArrayTypeSymbol arts;

        //read access ctor
        internal ArrayAccessorPredefineds(CodeBuilder code, IArrayTypeSymbol arts)
        {
            this.code = code;
            this.arts = arts;
        }

        //Accessors
        public void GetAtIndex()
        {
            //get the element type and use TealTypeUtils.TypeEncodings to get the fixed size too
            var elementEncoding = TealTypeUtils.DetermineEncodingType(arts.ElementType);
            if (elementEncoding.Encoding != ABIEncodingType.Unsupported)
            {
                // stack:
                //      data
                //      index

                //if it's fixed then:
                if (elementEncoding.Encoding == ABIEncodingType.FixedByteArray || elementEncoding.Encoding == ABIEncodingType.FixedInteger)
                {
                    if (elementEncoding.ByteWidth == 1)
                    {
                        //data
                        //index
                        code.getbyte();
                        //byte (not sure as yet if byte or int)
                    }
                    else
                    {
                        //if the type is something else then multiply the index by the size and get the value there by extracting the right amount of bytes...
                        //data
                        //index
                        code.int_literal_constant((ulong)elementEncoding.ByteWidth);
                        //elementsize
                        code.multiply();
                        code.int_literal_constant((ulong)elementEncoding.ByteWidth);
                        //data
                        //index_in_bytes
                        //len to extract
                        code.extract3();
                    }
                    //btoi if necessary - could be array of fixed sized arrays
                    if (elementEncoding.Encoding == ABIEncodingType.FixedInteger)
                    {
                        code.btoi();
                    }
                }
                else
                {
                    //if it's variable then:
                    //inputs
                    code.int_literal_constant(0); //read position
                    //  data
                    //  index
                    //  read position 
                    //
                    //  if index is zero
                    //      read the length N
                    //      read the bytes of length N from the read position plus +2
                    //  else
                    //      decrement index
                    //      read the length N
                    //      add the length N to the read Position

                    string loopEnd = code.ReserveLabel();
                    string loopStart = code.ReserveLabel();
                    //data index readpos

                    code.AddLabel(loopStart);

                    code.dig(1);                                                //data index readpos index
                    code.bz(loopEnd);                                           //data index readpos 
                    code.dig(2);                                                //data index readpos data
                    code.dig(1);                                                //data index readpos data readpos
                    code.extract_uint16();                                      //data index readpos lengthofelement
                    code.swap();                                                //data index lengthofelement readpos
                    code.int_literal_constant(2);                               //data index lengthofelement readpos 2
                    code.plus();                                                //data index lengthofelement start
                    code.plus();                                                //data index newreadpos
                    code.swap();                                                //data newreadpos index
                    code.int_literal_constant(1);                               //data newreadpos index 1
                    code.minus();                                               //data newreadpos index-1
                    code.swap();                                                //data index-1 newreadpos
                    code.b(loopStart);


                    code.AddLabel(loopEnd);
                    code.swap();                                                //data newreadpos 0
                    code.pop();                                                 //data newreadpos            
                    //data readpos
                    code.dup2();                                                //data newreadpos  data newreadpos          
                    code.extract_uint16();                                      //data newreadpos length        
                    //data readpos length
                    code.swap();
                    code.int_literal_constant(2);
                    code.plus();
                    code.swap();
                    code.extract3();                                            //result    


                }




                //Once this is done, above, then we just need to test....it's done. Setters and wotnot come later.

            }
            else
            {
                throw new ErrorDiagnosticException("E067");
            }


        }

        public void SetAtIndex()
        {
            throw new NotImplementedException();
        }
    }
}
