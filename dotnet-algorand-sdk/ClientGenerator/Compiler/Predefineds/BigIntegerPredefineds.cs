using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class BigIntegerPredefineds : ITypePredefined
    {
        byte scratchIndex;
        CodeBuilder code;
        Scope scope;
        List<IParameterSymbol> nulledOptionals;
        Dictionary<string, string> literals;

        internal BigIntegerPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals)
        {
            this.code = code;
            this.scratchIndex = scratchIndex;
            this.nulledOptionals = nulledOptionals;
            this.scope = scope;
            this.literals = literals;
        }

        internal BigIntegerPredefineds(CodeBuilder code)
        {
            this.code = code;
        }





        public void BitLen()
        {
            code.bitlen();
        }
    }
}
