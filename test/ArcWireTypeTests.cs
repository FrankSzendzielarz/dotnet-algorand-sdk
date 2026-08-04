using AVM.ClientGenerator.ABI.ARC4.Types;
using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace test
{
    /// <summary>
    /// Fast, network-free unit tests for the ARC4 WireType encode/decode primitives used by the ARC56 client
    /// generator. These specifically guard against regressions in bugs found and fixed while extending AVMTypes
    /// coverage: VariableArray&lt;T&gt;.From() only handling a few hardcoded input shapes, Tuple.From() clobbering
    /// FixedArray&lt;T&gt;'s pre-sized Value list, and StructArray&lt;T&gt;'s encode/decode round trip.
    /// </summary>
    [TestFixture]
    public class ArcWireTypeTests
    {
        [Test]
        public void String_IsDynamic_IsAlwaysTrue()
        {
            // ARC4 "string" is always dynamic regardless of content, unlike VariableArray<T>.IsDynamic's default
            // (element-dynamism-based) computation, which for Byte elements (always static) would incorrectly
            // report false. Getting this wrong breaks any Tuple/array that nests a String, since Tuple.Encode()
            // decides between inlining (static) and offset/tail-addressing (dynamic) based on this flag.
            var s = new String();
            s.From("hi");
            Assert.That(s.IsDynamic, Is.True);

            var empty = new String();
            empty.From("");
            Assert.That(empty.IsDynamic, Is.True);
        }

        [Test]
        public void VariableArray_StringArray_RoundTrips()
        {
            var abi = new VariableArray<String>();
            abi.From(new[] { "Hello", "world" });

            var encoded = abi.Encode();

            var decoded = new VariableArray<String>();
            decoded.Decode(encoded);

            var values = decoded.Value.Select(v => v.ToString()).ToArray();
            Assert.That(values, Is.EqualTo(new[] { "Hello", "world" }));
        }

        [Test]
        public void Tuple_MixedStaticAndDynamicElements_RoundTrips()
        {
            // Regression test for Tuple.Decode(): it used to ignore IsDynamic entirely and decode every element
            // sequentially/inline, which happened to work for all-static tuples (the only case exercised until
            // string[] support was added) but corrupts anything with a genuinely dynamic element - the head
            // holds a 2-byte offset for those, not their inline bytes.
            var uintEl = new UInt64();
            uintEl.From(777UL);
            var strEl = new String();
            strEl.From("dynamic tail");
            var boolEl = new Bool();
            boolEl.From(true);

            var tuple = new Tuple();
            tuple.Value.AddRange(new WireType[] { uintEl, strEl, boolEl });

            var encoded = tuple.Encode();

            var decodeUint = new UInt64();
            var decodeStr = new String();
            var decodeBool = new Bool();
            var decodeTuple = new Tuple();
            decodeTuple.Value.AddRange(new WireType[] { decodeUint, decodeStr, decodeBool });
            var consumed = decodeTuple.Decode(encoded);

            Assert.That((ulong)decodeUint.ToValue(), Is.EqualTo(777UL));
            Assert.That(decodeStr.ToString(), Is.EqualTo("dynamic tail"));
            Assert.That((bool)decodeBool.ToValue(), Is.True);
            Assert.That(consumed, Is.EqualTo((uint)encoded.Length));
        }

        [Test]
        public void VariableArray_UInt64_FromRawArray_RoundTrips()
        {
            var abi = new VariableArray<UInt64>();
            abi.From(new ulong[] { 1, 2, 3, ulong.MaxValue });

            var encoded = abi.Encode();

            var decoded = new VariableArray<UInt64>();
            decoded.Decode(encoded);

            var values = decoded.Value.Select(v => (ulong)v.ToValue()).ToArray();
            Assert.That(values, Is.EqualTo(new ulong[] { 1, 2, 3, ulong.MaxValue }));
        }

        [Test]
        public void VariableArray_Bool_FromRawArray_RoundTrips()
        {
            var abi = new VariableArray<Bool>();
            abi.From(new bool[] { true, false, true, true, false, false, false, true, true });

            var encoded = abi.Encode();

            var decoded = new VariableArray<Bool>();
            decoded.Decode(encoded);

            var values = decoded.Value.Select(v => (bool)v.ToValue()).ToArray();
            Assert.That(values, Is.EqualTo(new bool[] { true, false, true, true, false, false, false, true, true }));
        }

        [Test]
        public void VariableArray_Empty_RoundTrips()
        {
            var abi = new VariableArray<UInt64>();
            abi.From(System.Array.Empty<ulong>());

            var encoded = abi.Encode();
            Assert.That(encoded.Length, Is.EqualTo(2)); // just the 2-byte length prefix

            var decoded = new VariableArray<UInt64>();
            decoded.Decode(encoded);
            Assert.That(decoded.Value, Is.Empty);
        }

        [Test]
        public void FixedArray_UInt64_FromRawArray_RoundTrips()
        {
            // FixedArray<T>'s constructor pre-populates Value with `length` fresh T() instances; From() must fill
            // those existing slots positionally rather than discarding them (the original bug: Tuple.From()
            // unconditionally cleared Value and only recognized List<object>/byte[] inputs, silently dropping
            // ulong[] input and leaving an empty, 0-byte-encoding array).
            var abi = new FixedArray<UInt64>(3);
            abi.From(new ulong[] { 10, 20, 30 });

            var encoded = abi.Encode();
            Assert.That(encoded.Length, Is.EqualTo(24)); // 3 * 8 bytes, no length prefix (static array)

            var decodeTuple = new Tuple();
            decodeTuple.Value.AddRange(new WireType[] { new UInt64(), new UInt64(), new UInt64() });
            decodeTuple.Decode(encoded);
            var values = decodeTuple.Value.Select(v => (ulong)v.ToValue()).ToArray();
            Assert.That(values, Is.EqualTo(new ulong[] { 10, 20, 30 }));
        }

        [Test]
        public void FixedArray_WrongElementCount_Throws()
        {
            var abi = new FixedArray<UInt64>(3);
            Assert.Throws<System.ArgumentException>(() => abi.From(new ulong[] { 1, 2 }));
        }

        [Test]
        public void Tuple_NestedStaticTuple_IsInlinedWithoutOffsetIndirection()
        {
            // Regression test for the static/dynamic ARC4 nesting rule: a tuple composed entirely of static
            // elements (here two uint64s) must itself be encoded as a flat, non-offset-indirected member when
            // nested inside another tuple, matching real ARC4 encoding (see AVMTypes.arc4ComplexTuple).
            var inner = new Tuple();
            inner.Value.AddRange(new WireType[] { new UInt64(), new UInt64() });
            inner.From(new object[] { 111UL, 222UL });

            var outer = new Tuple();
            outer.Value.Add(new UInt64());
            outer.Value.Add(inner);
            ((UInt64)outer.Value[0]).From(999UL);

            var encoded = outer.Encode();
            // uint64 (8 bytes) + inlined nested tuple of 2 uint64s (16 bytes) = 24 bytes, no 2-byte offset anywhere.
            Assert.That(encoded.Length, Is.EqualTo(24));
        }

        [Test]
        public void StructArray_VariableLength_EncodesCountPrefixAndConcatenatedElements()
        {
            var elements = new[]
            {
                MakeAddressUint256(1, new BigInteger(11)),
                MakeAddressUint256(2, new BigInteger(22)),
            };

            var abi = new StructArray<FakeAddressUint256>(FakeAddressUint256.Parse) { IsFixedLength = false };
            abi.From(elements.ToList());

            var encoded = abi.Encode();
            // 2-byte count prefix + 2 elements * 64 bytes (32-byte address + 32-byte uint256) each.
            Assert.That(encoded.Length, Is.EqualTo(2 + 2 * 64));
            Assert.That(encoded[0], Is.EqualTo(0));
            Assert.That(encoded[1], Is.EqualTo(2));

            var decoded = new StructArray<FakeAddressUint256>(FakeAddressUint256.Parse) { IsFixedLength = false };
            decoded.Decode(encoded);
            Assert.That(decoded.Value.Count, Is.EqualTo(2));
            Assert.That(decoded.Value[0].Uint256, Is.EqualTo(new BigInteger(11)));
            Assert.That(decoded.Value[1].Uint256, Is.EqualTo(new BigInteger(22)));
        }

        [Test]
        public void StructArray_FixedLength_EncodesWithoutCountPrefix()
        {
            var elements = new[]
            {
                MakeAddressUint256(1, new BigInteger(5)),
                MakeAddressUint256(2, new BigInteger(6)),
            };

            var abi = new StructArray<FakeAddressUint256>(FakeAddressUint256.Parse) { IsFixedLength = true, FixedLength = 2 };
            abi.From(elements.ToList());

            var encoded = abi.Encode();
            Assert.That(encoded.Length, Is.EqualTo(2 * 64)); // no count prefix

            var decoded = new StructArray<FakeAddressUint256>(FakeAddressUint256.Parse) { IsFixedLength = true, FixedLength = 2 };
            decoded.Decode(encoded);
            Assert.That(decoded.Value.Count, Is.EqualTo(2));
            Assert.That(decoded.Value[0].Uint256, Is.EqualTo(new BigInteger(5)));
            Assert.That(decoded.Value[1].Uint256, Is.EqualTo(new BigInteger(6)));
        }

        private static FakeAddressUint256 MakeAddressUint256(byte addressFiller, BigInteger uint256)
        {
            var address = new byte[32];
            for (int i = 0; i < address.Length; i++) address[i] = addressFiller;
            return new FakeAddressUint256 { Address = address, Uint256 = uint256 };
        }

        /// <summary>
        /// Minimal stand-in for a generated ARC56 struct (AVMObjectType) shaped like AVMTypes' structAddressUint256
        /// (address + uint256, both static), used to unit test StructArray&lt;T&gt; without depending on any
        /// generated proxy code.
        /// </summary>
        private class FakeAddressUint256 : Algorand.AVM.ClientGenerator.ABI.ARC56.AVMObjectType
        {
            public byte[] Address { get; set; } = new byte[32];
            public BigInteger Uint256 { get; set; }

            public byte[] ToByteArray()
            {
                var addressWire = new Address();
                addressWire.From(new Algorand.Address(Address));
                var uintWire = new UInt256();
                uintWire.From(Uint256);
                return addressWire.Encode().Concat(uintWire.Encode()).ToArray();
            }

            public static FakeAddressUint256 Parse(byte[] bytes)
            {
                var addressWire = new Address();
                addressWire.Decode(bytes);
                var uintWire = new UInt256();
                uintWire.Decode(bytes.Skip(32).ToArray());
                return new FakeAddressUint256
                {
                    Address = addressWire.Encode(),
                    Uint256 = (BigInteger)uintWire.ToValue()
                };
            }
        }
    }
}
