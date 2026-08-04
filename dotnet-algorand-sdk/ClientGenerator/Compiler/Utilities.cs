using AVM.ClientGenerator.Core.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace AVM.ClientGenerator.Compiler
{
    public static class Utilities
    {

        public static bool IsSmartContractLibrary(INamedTypeSymbol symbol)
        {
            if (symbol == null) return false;
            if (symbol.BaseType == null) return false;
            if (symbol.BaseType.Name == typeof(Core.SmartContractLibrary).Name) return true;
            return IsSmartContractLibrary(symbol.BaseType);

        }

        public static bool IsSmartContract(INamedTypeSymbol symbol)
        {
            if (symbol == null) return false;
            if (symbol.BaseType == null) return false;
            if (symbol.BaseType.Name == typeof(Core.SmartContract).Name) return true;
            return IsSmartContract(symbol.BaseType);

        }

        public static bool IsSmartSignature(INamedTypeSymbol symbol)
        {
            if (symbol == null) return false;
            if (symbol.BaseType == null) return false;
            if (symbol.BaseType.Name == typeof(Core.SmartSignature).Name) return true;
            return IsSmartSignature(symbol.BaseType);

        }
        public static bool IsAbiStruct(ITypeSymbol symbol)
        {
            if (symbol == null) return false;
            return symbol.GetAttributes().Any(a => a.AttributeClass.Name == nameof(ABIStructAttribute));

            

        }

        public static bool IsAbiStruct(Type symbol)
        {
            if (symbol == null) return false;
            return symbol.GetCustomAttributes(typeof(ABIStructAttribute),false).Any();



        }
    }
}
