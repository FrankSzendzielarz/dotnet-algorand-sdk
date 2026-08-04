using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class IntegerPredefineds : ITypePredefined
    {
        byte scratchIndex;
        CodeBuilder code;
        Scope scope;
        List<IParameterSymbol> nulledOptionals;
        Dictionary<string, string> literals;

        internal IntegerPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals)
        {
            this.code = code;
            this.scratchIndex = scratchIndex;
            this.nulledOptionals = nulledOptionals;
            this.scope = scope;
            this.literals = literals;
        }

        internal IntegerPredefineds(CodeBuilder code)
        {
            this.code = code;
        }




        public void Pow()
        {
            //    code.load(scratchIndex, scope);
            code.swap();
            code.exp();
        }

        public void BigPow()
        {
            code.swap();
            code.expw();
            code.itob();
            code.swap();
            code.itob();
            code.swap();
            code.concat();
        }

        public void Sqrt()
        {
            code.sqrt();
        }

        public void BitLen()
        {
            code.bitlen();
        }

        public void ToTealBytes()
        {
            code.itob();
        }
    }
}
