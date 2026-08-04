using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class StringPredefineds : ITypePredefined
    {

        byte scratchIndex;
        CodeBuilder code;
        Scope scope;
        List<IParameterSymbol> nulledOptionals;
        Dictionary<string, string> literals;

        internal StringPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals)
        {
            this.code = code;
            this.scratchIndex = scratchIndex;
            this.nulledOptionals = nulledOptionals;
            this.scope = scope;
            this.literals = literals;
        }

        internal StringPredefineds(CodeBuilder code)
        {
            this.code = code;
        }


        public void ToByteArray()
        {

            //do nothing
        }







    }
}
