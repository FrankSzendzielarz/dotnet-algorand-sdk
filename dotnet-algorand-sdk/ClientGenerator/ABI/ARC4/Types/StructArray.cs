using Algorand.AVM.ClientGenerator.ABI.ARC56;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    /// <summary>
    /// Wraps a fixed- or variable-length ARC4 array of ARC56 generated struct elements (AVMObjectType) for encoding/decoding.
    /// Named ARC56 structs are not WireType instances themselves (they have their own hand-generated ToByteArray()/Parse()),
    /// so this adapter lets them participate in the same argument/return encoding pipeline as the primitive ARC4 types.
    /// Only static (fixed-size) element structs are supported: each element is inlined directly, matching the ARC4
    /// encoding rule that arrays of static elements have no per-element offset table.
    /// </summary>
    public class StructArray<T> : WireType where T : AVMObjectType
    {
        private readonly Func<byte[], T> parse;

        public List<T> Value { get; set; } = new List<T>();
        public bool IsFixedLength { get; set; }
        public uint FixedLength { get; set; }

        public StructArray(Func<byte[], T> parse)
        {
            this.parse = parse;
        }

        public override bool IsDynamic => !IsFixedLength;

        public override string GetDescription()
        {
            return IsFixedLength ? $"struct[{FixedLength}]" : "struct[]";
        }

        public override byte[] Encode()
        {
            var bytes = new List<byte>();
            if (!IsFixedLength)
            {
                bytes.Add((byte)(Value.Count / 256));
                bytes.Add((byte)(Value.Count % 256));
            }
            foreach (var item in Value)
            {
                bytes.AddRange(item.ToByteArray());
            }
            return bytes.ToArray();
        }

        public override uint Decode(byte[] data)
        {
            uint offset = 0;
            int count;
            byte[] remaining;
            if (IsFixedLength)
            {
                count = (int)FixedLength;
                remaining = data;
            }
            else
            {
                if (data.Length < 2) throw new ArgumentException("Invalid data length");
                count = (data[0] << 8) | data[1];
                remaining = data.Skip(2).ToArray();
                offset += 2;
            }

            Value = new List<T>();
            for (int i = 0; i < count; i++)
            {
                var item = parse(remaining);
                Value.Add(item);
                var consumed = item.ToByteArray().Length;
                remaining = remaining.Skip(consumed).ToArray();
                offset += (uint)consumed;
            }
            return offset;
        }

        public override bool From(object instance)
        {
            if (instance is List<T> listV)
            {
                Value = listV;
                return true;
            }
            if (instance is T[] arrayV)
            {
                Value = arrayV.ToList();
                return true;
            }
            if (instance is IEnumerable<T> enumerableV)
            {
                Value = enumerableV.ToList();
                return true;
            }
            return false;
        }

        public override object ToValue()
        {
            return Value;
        }
    }
}
