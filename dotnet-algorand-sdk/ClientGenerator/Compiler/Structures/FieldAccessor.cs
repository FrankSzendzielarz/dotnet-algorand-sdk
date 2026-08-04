namespace AVM.ClientGenerator.Compiler.Structures
{

    internal class FieldAccessor
    {
        internal int Position;                //fixed offset or variables position
        internal string Name;
        internal ABIEncodingType Encoding;
        internal int ByteWidth;
        internal bool Variable;
    }

}
