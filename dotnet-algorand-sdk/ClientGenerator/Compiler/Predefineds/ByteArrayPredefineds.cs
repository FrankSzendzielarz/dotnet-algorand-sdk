using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class ByteArrayPredefineds : ITypePredefined, IArrayTypePredefined
    {

        byte scratchIndex;
        CodeBuilder code;
        Scope scope;
        List<IParameterSymbol> nulledOptionals;
        Dictionary<string, string> literals;

        internal ByteArrayPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals , Dictionary<string,string> literals)
        {
            this.code = code;
            this.scratchIndex = scratchIndex;
            this.nulledOptionals = nulledOptionals;
            this.scope = scope;
            this.literals = literals;
        }

        //read access ctor
        internal ByteArrayPredefineds(CodeBuilder code)
        {
            this.code = code;
        }

        //Accessors
        public void GetAtIndex()
        {
         
            //code.swap();
            code.getbyte();
        }

        public void SetAtIndex()
        {
            // relies on syntax walker doing this:
            //  visit index 
            //  visit right (val to store)
            //  dup result for expression return
            // so stacks is:
            // ..INDEX, VALUE, VALUE, ARRAY
       
            code.cover(3);              // ..ARRAY, INDEX, VALUE, VALUE
            code.cover(3);              // ..VALUE, ARRAY, INDEX, VALUE
            code.setbyte();
            code.store(scratchIndex, scope);
           
        }

        public void ToAccountReference()
        {
            //do nothing
        }

        //Array type implementations 
        public void Length()
        {
            code.len();
        }

        public void IsFixedSize()
        {
            throw new ErrorDiagnosticException("E032");
        }

        public void IsReadOnly()
        {
            code.int_literal_constant(0);
        }

        public void IsSynchronized()
        {
            throw new ErrorDiagnosticException("E032");
        }

        public void LongLength()
        {
           
            code.len();
        }

        public void MaxLength()
        {
            code.int_literal_constant(Core.Constants.MaxByteArraySize);
        }
        public void Rank()
        {
            code.int_literal_constant(1);
        }

        public void SyncRoot()
        {
            throw new ErrorDiagnosticException("E032");
        }
     
       
        //extension method
        public void Concat()
        {
         
            code.swap();
            code.concat();
       
        }

        public void Part()
        {
          
            code.cover(2);
            code.substring3();

        }

        public void Replace()
        {
           
            code.cover(2);
            code.replace3();
            code.store(scratchIndex, scope);
        }

        public void BitLen()
        {
            code.bitlen();
        }

        public void GetBit()
        {
            code.swap();
            code.getbit();
        }

        public void ToTealUlong()
        {
            code.btoi();
        }

        public void Init()
        {
            try
            {
                byte b= byte.Parse(literals["b"]);
                uint l = uint.Parse(literals["l"]);

                string s = new StringBuilder(2*(int)l).Insert(0,b.ToString("X2"),(int)l).ToString();

                code.byte_literal_constant($"0x{s}");


            }
            catch
            {
                throw new Exception("Unexpected arguments to ByteArray Init method.");
            }
            code.store(scratchIndex, scope);
        }

        public new void ToString()
        {

            //do nothing

        }

     









    }
}
