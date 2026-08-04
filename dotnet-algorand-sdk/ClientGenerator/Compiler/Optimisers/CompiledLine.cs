using AVM.ClientGenerator.Compiler;
using System.Linq;


namespace AVM.ClientGenerator.Optimisers
{
    public struct CompiledLine
    {
        public string Opcode;
        internal ulong costOverride;
        public ulong Size { get { if (costOverride > 0) return costOverride; else { var opcode = Opcode.Trim().ToLower(); return (ulong)(LangSpec.Instance.Ops.Where(o => o.Name.Trim().ToLower() == opcode).FirstOrDefault()?.Size ?? 0); } } 
                            set { costOverride = value; }                    
        }
        
        public string[] Parameters;
        public string Line;
        public bool Optimisable;

    }
}
