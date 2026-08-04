using Microsoft.CodeAnalysis;

namespace AVM.ClientGenerator.Compiler.CompiledCodeModel
{
    internal class SmartContractLibraryMethod : RootCode
    {
        internal SmartContractLibraryMethod(Scope associatedScope) : base(associatedScope)
        {
            stateless = false;
        }

        internal static string GetLibraryMethodLabel(IMethodSymbol methodSymbol)
        {

            string methodSignature = methodSymbol.ToString();
            int index = methodSignature.IndexOf('(');
            if (index > 0)
            {
                return methodSignature.Substring(0, index);
            
            }


            return methodSignature;
        }

        internal string GetLibraryMethodLabel()
        {
            IMethodSymbol methodSymbol = AssociatedScope.Label as IMethodSymbol;
            if (methodSymbol == null) return null;
            
            return GetLibraryMethodLabel(methodSymbol);

        }
    }
}
