namespace AVM.ClientGenerator.Optimisers
{
    public interface ICompilerMemento
    {
        void RemoveLineAt(int index);

        void ReplaceLineAt(int index, CompiledLine line);

        void InsertLineAt(int index,CompiledLine line);

        void AddLine( CompiledLine line);

        void RemoveTopLine();

    }
}
