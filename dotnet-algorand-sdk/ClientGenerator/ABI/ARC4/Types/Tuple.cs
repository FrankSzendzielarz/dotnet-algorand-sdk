using System;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class Tuple : WireType
    {
        public override string GetDescription()
        {
            if (Value.Select(w => w.GetDescription()).Distinct().Count() == 1)
            {
                if (IsDynamic)
                {
                    return $"{Value.First().GetDescription()}[]";
                }
                return $"{Value.First().GetDescription()}[{Value.Count}]";
            }
            return $"({string.Join(",", Value.Select(w => w.GetDescription()))})";
        }
        public List<WireType> Value { get; set; } = new List<WireType>();

        public override bool IsDynamic => Value.Exists(x => x.IsDynamic);

        public override uint Decode(byte[] data)
        {
            // `data` is consumed head-first via a sliding window (mirrors the original static-only
            // implementation); `original` stays fixed at the tuple's own byte range, since dynamic elements are
            // read from an *absolute* offset into it rather than from the current window position.
            byte[] original = data;
            uint headOffset = 0;
            int boolCount = 0;
            var dynamicItems = new List<(WireType item, ushort offset)>();

            foreach (var item in Value)
            {
                if (item is Bool)
                {
                    if (boolCount == 0) headOffset++;
                    byte mask = (byte)(0x80 >> boolCount);
                    boolCount++;
                    (item as Bool).Value = (data[0] & mask) != 0;
                    if (boolCount == 8)
                    {
                        boolCount = 0;
                        data = data.Skip(1).ToArray();

                    }
                }
                else if (item.IsDynamic)
                {
                    // Dynamic elements aren't inlined: the head only carries a 2-byte big-endian offset into the
                    // tuple's own byte range, pointing at where the element's self-contained encoding actually
                    // starts (its "tail"). Defer decoding it until the head pass finishes.
                    ushort itemOffset = (ushort)((data[0] << 8) | data[1]);
                    dynamicItems.Add((item, itemOffset));
                    data = data.Skip(2).ToArray();
                    headOffset += 2;
                }
                else
                {
                    uint len = item.Decode(data);
                    data = data.Skip((int)len).ToArray();
                    headOffset += len;
                }


            }

            uint totalLength = headOffset;
            foreach (var (item, itemOffset) in dynamicItems)
            {
                uint len = item.Decode(original.Skip(itemOffset).ToArray());
                uint end = itemOffset + len;
                if (end > totalLength) totalLength = end;
            }

            return totalLength;
        }

        public override byte[] Encode()
        {
            List<byte[]> heads = new List<byte[]>();
            List<byte[]> tails = new List<byte[]>();
            // Consecutive Bool elements share a single packed head byte (only the first of a run adds a new
            // heads[] entry), so a plain per-item counter can't index heads[] correctly - track the actual
            // heads[] slot each item owns instead.
            List<int> headIndexForItem = new List<int>();


            int boolCount = 0;
            byte[] boolHead = new byte[1] { 0 };
            int currentBoolHeadIndex = -1;
            foreach (var item in Value)
            {
                byte[] encoded = item.Encode();
                if (item is Bool)
                {
                    if (boolCount % 8 == 0)
                    {
                        boolHead = encoded;
                        heads.Add(boolHead);
                        currentBoolHeadIndex = heads.Count - 1;
                        boolCount = 0;
                    }
                    else
                    {

                        boolHead[0] = (byte)(boolHead[0] | (encoded[0] >> boolCount));
                    }
                    boolCount++;
                    tails.Add(new byte[] { });
                    headIndexForItem.Add(currentBoolHeadIndex);

                }
                else
                {
                    boolCount = 0;
                    boolHead = new byte[1] { 0 };

                    if (item.IsDynamic)
                    {
                        heads.Add(new byte[2]); //reserve space for the offset
                        tails.Add(encoded);
                    }
                    else
                    {
                        heads.Add(encoded);
                        tails.Add(new byte[] { });
                    }
                    headIndexForItem.Add(heads.Count - 1);
                }
            }

            ushort offset = (ushort)heads.Sum(x => x.Length);
            //second pass to calculate the offsets
            for (int i = 0; i < Value.Count; i++)
            {
                var item = Value[i];
                if (item.IsDynamic)
                {
                    var bytes = BitConverter.GetBytes(offset);
                    if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
                    Buffer.BlockCopy(bytes, 0, heads[headIndexForItem[i]], 0, 2);
                }
                offset += (ushort)tails[i].Length;
            }

            //concatenate the heads and tails
            byte[] result = heads.SelectMany(x => x).Concat(tails.SelectMany(x => x)).ToArray();
            return result;
        }

        public override bool From(object instance)
        {
            if (instance is List<object> listV)
            {
                Value.Clear();
                foreach (var item in listV)
                {
                    var wiretype = WireType.FromABIDescription(TypeHelpers.CSTypeToAbiType(instance.GetType()));
                    wiretype.From(item);
                    Value.Add(wiretype);
                }
                return true;
            }
            if (instance is byte[] bytes)
            {
                Value.Clear();
                foreach (var item in bytes)
                {
                    var wiretype = WireType.FromABIDescription(TypeHelpers.CSTypeToAbiType(item.GetType()));
                    if (wiretype == null)
                    {
                        throw new Exception($"Unable to determine wiretype of {item.GetType()}");
                    }
                    wiretype.From(item);
                    Value.Add(wiretype);
                }
                return true;
            }
            // Any other enumerable of raw CLR values (e.g. ulong[], bool[] passed to a FixedArray<T>, whose
            // constructor already pre-sizes Value with one T per slot). If Value is already sized, fill each
            // existing slot positionally so the correct concrete WireType per element is preserved; otherwise
            // build it up fresh, inferring each element's WireType from its CLR type.
            if (instance is System.Collections.IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToList();
                if (Value.Count > 0)
                {
                    if (items.Count != Value.Count)
                    {
                        throw new ArgumentException($"Expected {Value.Count} elements but got {items.Count}.");
                    }
                    for (int i = 0; i < items.Count; i++)
                    {
                        Value[i].From(items[i]);
                    }
                }
                else
                {
                    foreach (var item in items)
                    {
                        var wiretype = WireType.FromABIDescription(TypeHelpers.CSTypeToAbiType(item.GetType()));
                        if (wiretype == null)
                        {
                            throw new Exception($"Unable to determine wiretype of {item.GetType()}");
                        }
                        wiretype.From(item);
                        Value.Add(wiretype);
                    }
                }
                return true;
            }
            return true;
        }

        public override object ToValue()
        {
            var list = new List<object>();
            foreach (var item in Value)
            {
                list.Add(item.ToValue());
            }
            return list;
        }
    }
}
