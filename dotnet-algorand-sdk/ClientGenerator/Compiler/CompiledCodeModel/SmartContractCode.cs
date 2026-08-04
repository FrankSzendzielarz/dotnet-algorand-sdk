using Microsoft.CodeAnalysis;
using System.Linq;




namespace AVM.ClientGenerator.Compiler.CompiledCodeModel
{
    internal class SmartContractCode : RootCode
    {
        internal SmartContractCode(Scope associatedScope) : base(associatedScope)
        {
            stateless = false;
        }


        internal int GetGlobalByteCount()
        {

            return AssociatedScope.GetGlobals(Core.VariableType.ByteSlice).Count() + AssociatedScope.GetGlobals(Core.VariableType.ByteArrayReference).Count();
        }

        internal int GetGlobalUintCount()
        {
            return AssociatedScope.GetGlobals(Core.VariableType.UInt64).Count() + AssociatedScope.GetGlobals(Core.VariableType.UlongReference).Count();
        }

        internal int GetLocalByteCount()
        {
            return AssociatedScope.GetLocals(Core.VariableType.ByteSlice).Count() + AssociatedScope.GetLocals(Core.VariableType.ByteArrayReference).Count();
        }

        internal int GetLocalUintCount()
        {
            return AssociatedScope.GetLocals(Core.VariableType.UInt64).Count() + AssociatedScope.GetLocals(Core.VariableType.UlongReference).Count();
        }

    }
}
