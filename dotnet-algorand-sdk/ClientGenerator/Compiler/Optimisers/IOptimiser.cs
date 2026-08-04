using System.Collections.Generic;

namespace AVM.ClientGenerator.Optimisers
{
    public interface IOptimiser
    {
        void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler);

        void ChildScopeEntered();

        void ChildScopeExited();

    }
}
