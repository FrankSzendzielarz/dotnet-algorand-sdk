using System;
using System.Linq;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class FixedArray<T> : Tuple where T : WireType
    {
        public uint Length { get; private set; }

        // The ABI type of a single element (e.g. "uint80", "ufixed64x2", "byte[16]"). Elements are built by
        // re-parsing this spec via WireType.FromABIDescription rather than `new T()`, because several wire types
        // (UInt with a non-standard bitwidth, UFixed, nested FixedArray/VariableArray) have no public parameterless
        // constructor - their encoding depends on values (bitwidth, precision, length) that only the spec string
        // carries, and a generic `where T : new()` constraint can't express that.
        public string ElementSpec { get; private set; }

        public FixedArray(uint length) : this(length, null)
        {
        }

        public FixedArray(uint length, string elementSpec)
        {
            Length = length;
            ElementSpec = elementSpec;
            Value = new System.Collections.Generic.List<WireType>((int)length);
            for (int i = 0; i < length; i++)
            {
                Value.Add(CreateElement());
            }
        }

        private WireType CreateElement()
        {
            if (!string.IsNullOrEmpty(ElementSpec))
            {
                var wireType = WireType.FromABIDescription(ElementSpec);
                if (wireType == null)
                {
                    throw new InvalidOperationException($"Unable to construct FixedArray element from ABI spec '{ElementSpec}'.");
                }
                return wireType;
            }
            // Backward-compatible fallback for callers that only know the length (e.g. Address(32)) - relies on
            // T having a public parameterless constructor, same as the previous `new T()`-based implementation.
            return (WireType)Activator.CreateInstance(typeof(T));
        }

        public override bool From(object instance)
        {
            return base.From(instance);
        }
        public byte[] ToByteArray()
        {
            if (Value.FirstOrDefault() is Byte b)
            {
                return Value.Select(b => (byte)b.ToValue()).ToArray();
            }
            return null;
        }
    }
}
