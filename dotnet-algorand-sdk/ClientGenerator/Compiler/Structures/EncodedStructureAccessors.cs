using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Structures
{

    internal class EncodedStructureAccessors
    {
        public int CurrentFixedOffset = 0;
        public int CurrentVariablesCount = 0;
        public List<FieldAccessor> FieldAccessors = new List<FieldAccessor>();

        internal void AddAccessor(EncodedStructureAccessors structureAccessors, ISymbol fieldSymbol, ABIEncodingType encoding, int byteWidth)
        {
            var accessor = new FieldAccessor()
            {
                ByteWidth = byteWidth,
                Encoding = encoding,
                Name = fieldSymbol.Name
            };

            switch (encoding)
            {
                case ABIEncodingType.FixedInteger:
                case ABIEncodingType.FixedByteArray:
                    accessor.Position = structureAccessors.CurrentFixedOffset;
                    accessor.Variable = false;
                    structureAccessors.CurrentFixedOffset += byteWidth;
                    structureAccessors.FieldAccessors.Add(accessor);
                    break;
                case ABIEncodingType.VariableByteArray:
                    accessor.Position = structureAccessors.CurrentVariablesCount;
                    accessor.Variable = true;
                    structureAccessors.CurrentVariablesCount++;
                    structureAccessors.FieldAccessors.Add(accessor);
                    break;
                default:
                    //TODO - throw exception?
                    break;
            }
        }
    }
}


