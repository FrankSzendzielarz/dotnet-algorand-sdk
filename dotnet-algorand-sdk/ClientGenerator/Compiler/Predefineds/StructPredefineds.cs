using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Structures;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class StructPredefineds : ITypePredefined
    {
        CodeBuilder code;
        CompilationGroup context;

        internal StructPredefineds(CodeBuilder code, CompilationGroup context)
        {
            this.code = code;
            this.context = context;
        }

        internal void Load(string structName, string fieldName)
        {
            if (context.ABIStructs.ContainsKey(structName))
            {
                EncodedStructureAccessors abiStruct = context.ABIStructs[structName];

                FieldAccessor fieldAccessor = abiStruct.FieldAccessors.Where(fa => fa.Name == fieldName).FirstOrDefault();
                if (fieldAccessor!=null)
                {
                    if (fieldAccessor.Encoding == ABIEncodingType.FixedInteger || fieldAccessor.Encoding== ABIEncodingType.FixedByteArray)
                    {
                        code.extract(fieldAccessor.Position, fieldAccessor.ByteWidth);
                        code.btoi();
                    }
                    else if (fieldAccessor.Encoding == ABIEncodingType.VariableByteArray)
                    {
                        if (fieldAccessor.Position > 0)
                        {
                            code.dupn(fieldAccessor.Position+1);                                    //eg: position 2 gives struct struct struct struct and each below iteration loses one struct                                    

                            code.int_literal_constant((ulong)abiStruct.CurrentFixedOffset);         // struct readPosition
                            for (int x = 0; x < fieldAccessor.Position; x++)  
                            {
                                code.dup();                                                         // struct readPosition readPosition
                                code.int_literal_constant(2);                                       // struct readPosition readPosition 2
                                code.plus();                                                        // struct readPosition readPosition+2
                                code.cover(2);                                                      // readPosition+2 struct readPosition 
                                code.extract_uint16();                                              // readPosition+2 length
                                code.plus();                                                        // newReadPosition
                            }
                            //at this point if field accessor was position 2 then the result should be struct struct newReadPosition
                        }
                        else
                        {
                            //first variable
                            code.dup();
                            code.int_literal_constant((ulong)abiStruct.CurrentFixedOffset);         //struct struct offsetToVariable
                        }

                        code.dup();                                                                 //struct struct offsetToVariable offsetToVariable
                        code.int_literal_constant(2);                                               //struct struct offsetToVariable offsetToVariable 2
                        code.plus();                                                                //struct struct offsetToVariable offsetToVariable+2
                        code.cover(2);                                                              //struct offsetToVariable+2 struct offsetToVariable 
                        code.extract_uint16();                                                      //struct offsetToVariable+2 length
                        code.extract3();                                                            //result




                    }
                   
                    
                }
                else
                {
                    //field not found
                    throw new ErrorDiagnosticException("E065");
                }
            }
            else
            {
                //struct not found
                throw new ErrorDiagnosticException("E064");
            }
        }
    }
}
