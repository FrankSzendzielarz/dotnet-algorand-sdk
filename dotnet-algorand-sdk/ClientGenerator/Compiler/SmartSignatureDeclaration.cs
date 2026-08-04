using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler
{
    internal class SmartSignatureDeclaration
    {
        internal ulong ProgramCost { get; set; }
        internal SmartSignatureCode code;
        internal string Name { get; set; }
        internal string Code { get; set; }

        internal string ToCSharp(string scope)
        {
            return $@"

using AVM.ClientGenerator.Core;
namespace {scope} {{
    public class {Name} : ICompiledSignature
    {{

        public string Program => @""
#pragma version 8
{ Code }"";

    
    }}
}}

";
        }

    }
}
