using System;
using System.Collections.Generic;
using System.Numerics;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Operators
{
    /// <summary>
    /// Gives a code generator for arithmetic operators, depending on the *destination type*
    /// </summary>
    internal static class TypeCodeGeneratorFactory
    {

        private static Dictionary<string, Func<CodeBuilder, TypeCodeGenerator>> generatorFactories =
            new Dictionary<string, Func<CodeBuilder, TypeCodeGenerator>>()
            {
                    {   "bool",                 (code)=> {  return new BooleanOperatorCodeGenerator(code);              } },
                    {   typeof(Boolean).Name.ToLower(),   (code)=> {  return new BooleanOperatorCodeGenerator(code);              } },
                    {   "byte",                 (code)=> {  return new ByteOperatorCodeGenerator(code);                 } },

                    {   "sbyte",                (code)=> {  return new SByteOperatorCodeGenerator(code) ;               } },

                    {   "int",                  (code)=> {  return new IntOperatorCodeGenerator(code);                  } },
                    {   typeof(Int32).Name.ToLower(),     (code)=> {  return new IntOperatorCodeGenerator(code);                  } },
                    {   "uint",                 (code)=> {  return new UIntOperatorCodeGenerator(code);                 } },
                    {   typeof(UInt32).Name.ToLower(),    (code)=> {  return new UIntOperatorCodeGenerator(code);                 } },
                    {   "long",                 (code)=> {  return new LongOperatorCodeGenerator(code);                 } },
                    {   typeof(Int64).Name.ToLower(),     (code)=> {  return new LongOperatorCodeGenerator(code);                 } },
                    {   "ulong",                (code)=> {  return new ULongOperatorCodeGenerator(code);                } },
                    {   typeof(UInt64).Name.ToLower(),    (code)=> {  return new ULongOperatorCodeGenerator(code);                } },
                    {   "short",                (code)=> {  return new ShortOperatorCodeGenerator(code);                } },
                    {   typeof(Int16).Name.ToLower(),     (code)=> {  return new ShortOperatorCodeGenerator(code);                } },
                    {   "ushort",               (code)=> {  return new UShortOperatorCodeGenerator(code);               } },
                    {   typeof(UInt16).Name.ToLower(),    (code)=> {  return new UShortOperatorCodeGenerator(code);               } },
                    {   typeof(BigInteger).Name.ToLower(),    (code)=> {  return new BigIntegerOperatorCodeGenerator(code);       } },
                    {   "object",    (code)=> {  return new ObjectOperatorCodeGenerator(code);       } },
                    {   "decimal",               (code)=> {  return new DecimalOperatorCodeGenerator(code);               } }
            };

        internal static TypeCodeGenerator GetOperatorCodeGenerator(string type, CodeBuilder code)
        {

            if (generatorFactories.TryGetValue(type.ToLower(), out Func<CodeBuilder, TypeCodeGenerator> generatorFactory))
            {
                return generatorFactory(code);
            }
            else
            {
                throw new NotSupportedException();
            }



        }
    }
}
