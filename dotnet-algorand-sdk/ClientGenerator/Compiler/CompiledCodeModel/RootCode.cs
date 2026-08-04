namespace AVM.ClientGenerator.Compiler.CompiledCodeModel
{
    internal abstract class RootCode : CodeBuilder
    {
        protected RootCode(Scope associatedScope)
        {
            AssociatedScope = associatedScope;
        }

        internal Scope AssociatedScope { get; set; }
    }
}
