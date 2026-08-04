using System;
using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AVM.ClientGenerator;
using AVM.ClientGenerator.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVM.ClientGenerator.ABI.ARC56;
using Algorand.AVM.ClientGenerator.ABI.ARC56;

namespace AVMTypes
{


    public class AvmTypesProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public AvmTypesProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class InnerStruct : AVMObjectType
            {
                public ulong Num { get; set; }

                public Structs.StructAddressUint256 Struct { get; set; } = new Structs.StructAddressUint256();

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNum = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vNum.From(Num);
                    ret.AddRange(vNum.Encode());
                    ret.AddRange(Struct.ToByteArray());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static InnerStruct Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new InnerStruct();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNum = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vNum.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueNum = vNum.ToValue();
                    if (valueNum is ulong vNumValue) { ret.Num = vNumValue; }
                    ret.Struct = Structs.StructAddressUint256.Parse(queue.ToArray());
                    { var consumedStruct = ret.Struct.ToByteArray().Length; for (int i = 0; i < consumedStruct && queue.Count > 0; i++) { queue.Dequeue(); } }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as InnerStruct);
                }
                public bool Equals(InnerStruct? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(InnerStruct left, InnerStruct right)
                {
                    return EqualityComparer<InnerStruct>.Default.Equals(left, right);
                }
                public static bool operator !=(InnerStruct left, InnerStruct right)
                {
                    return !(left == right);
                }

            }

            public class StructAddressUint256 : AVMObjectType
            {
                public Algorand.Address Address { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Uint256 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAddress = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vAddress.From(Address);
                    ret.AddRange(vAddress.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUint256 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vUint256.From(Uint256);
                    ret.AddRange(vUint256.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static StructAddressUint256 Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new StructAddressUint256();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAddress = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vAddress.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueAddress = vAddress.ToValue();
                    if (valueAddress is Algorand.Address vAddressValue) { ret.Address = vAddressValue; }
                    var vUint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vUint256.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Uint256 = vUint256;
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as StructAddressUint256);
                }
                public bool Equals(StructAddressUint256? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(StructAddressUint256 left, StructAddressUint256 right)
                {
                    return EqualityComparer<StructAddressUint256>.Default.Equals(left, right);
                }
                public static bool operator !=(StructAddressUint256 left, StructAddressUint256 right)
                {
                    return !(left == right);
                }

            }

            public class Arc4UintN82TupleArgData : AVMObjectType
            {
                public byte Field0 { get; set; }

                public byte Field1 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vField1.From(Field1);
                    ret.AddRange(vField1.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc4UintN82TupleArgData Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc4UintN82TupleArgData();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is byte vField1Value) { ret.Field1 = vField1Value; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc4UintN82TupleArgData);
                }
                public bool Equals(Arc4UintN82TupleArgData? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc4UintN82TupleArgData left, Arc4UintN82TupleArgData right)
                {
                    return EqualityComparer<Arc4UintN82TupleArgData>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc4UintN82TupleArgData left, Arc4UintN82TupleArgData right)
                {
                    return !(left == right);
                }

            }

            public class Arc4UintN83TupleArgData : AVMObjectType
            {
                public byte Field0 { get; set; }

                public byte Field1 { get; set; }

                public byte Field2 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vField1.From(Field1);
                    ret.AddRange(vField1.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vField2.From(Field2);
                    ret.AddRange(vField2.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc4UintN83TupleArgData Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc4UintN83TupleArgData();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is byte vField1Value) { ret.Field1 = vField1Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField2.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField2 = vField2.ToValue();
                    if (valueField2 is byte vField2Value) { ret.Field2 = vField2Value; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc4UintN83TupleArgData);
                }
                public bool Equals(Arc4UintN83TupleArgData? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc4UintN83TupleArgData left, Arc4UintN83TupleArgData right)
                {
                    return EqualityComparer<Arc4UintN83TupleArgData>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc4UintN83TupleArgData left, Arc4UintN83TupleArgData right)
                {
                    return !(left == right);
                }

            }

            public class Arc4ComplexTupleArgData : AVMObjectType
            {
                public Algorand.Address Field0 { get; set; }

                public Structs.Arc4ComplexTupleArgDataField1 Field1 { get; set; } = new Structs.Arc4ComplexTupleArgDataField1();

                public Structs.Arc4ComplexTupleArgDataField1 Field2 { get; set; } = new Structs.Arc4ComplexTupleArgDataField1();

                public Structs.Arc4ComplexTupleArgDataField1 Field3 { get; set; } = new Structs.Arc4ComplexTupleArgDataField1();

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    ret.AddRange(Field1.ToByteArray());
                    ret.AddRange(Field2.ToByteArray());
                    ret.AddRange(Field3.ToByteArray());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc4ComplexTupleArgData Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc4ComplexTupleArgData();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is Algorand.Address vField0Value) { ret.Field0 = vField0Value; }
                    ret.Field1 = Structs.Arc4ComplexTupleArgDataField1.Parse(queue.ToArray());
                    { var consumedField1 = ret.Field1.ToByteArray().Length; for (int i = 0; i < consumedField1 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    ret.Field2 = Structs.Arc4ComplexTupleArgDataField1.Parse(queue.ToArray());
                    { var consumedField2 = ret.Field2.ToByteArray().Length; for (int i = 0; i < consumedField2 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    ret.Field3 = Structs.Arc4ComplexTupleArgDataField1.Parse(queue.ToArray());
                    { var consumedField3 = ret.Field3.ToByteArray().Length; for (int i = 0; i < consumedField3 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc4ComplexTupleArgData);
                }
                public bool Equals(Arc4ComplexTupleArgData? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc4ComplexTupleArgData left, Arc4ComplexTupleArgData right)
                {
                    return EqualityComparer<Arc4ComplexTupleArgData>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc4ComplexTupleArgData left, Arc4ComplexTupleArgData right)
                {
                    return !(left == right);
                }

            }

            public class Arc4ComplexTupleArgDataField1 : AVMObjectType
            {
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Field0 { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Field1 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vField1.From(Field1);
                    ret.AddRange(vField1.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc4ComplexTupleArgDataField1 Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc4ComplexTupleArgDataField1();
                    uint count = 0;
                    var vField0 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Field0 = vField0;
                    var vField1 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Field1 = vField1;
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc4ComplexTupleArgDataField1);
                }
                public bool Equals(Arc4ComplexTupleArgDataField1? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc4ComplexTupleArgDataField1 left, Arc4ComplexTupleArgDataField1 right)
                {
                    return EqualityComparer<Arc4ComplexTupleArgDataField1>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc4ComplexTupleArgDataField1 left, Arc4ComplexTupleArgDataField1 right)
                {
                    return !(left == right);
                }

            }

            public class Arc4TupleArgData : AVMObjectType
            {
                public ulong Field0 { get; set; }

                public string Field1 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField1.From(Field1);
                    stringRef[ret.Count] = vField1.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc4TupleArgData Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new Arc4TupleArgData();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    var indexField1 = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField1.Decode(bytes.Skip(indexField1 + prefixOffset).ToArray());
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is string vField1Value) { ret.Field1 = vField1Value; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc4TupleArgData);
                }
                public bool Equals(Arc4TupleArgData? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc4TupleArgData left, Arc4TupleArgData right)
                {
                    return EqualityComparer<Arc4TupleArgData>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc4TupleArgData left, Arc4TupleArgData right)
                {
                    return !(left == right);
                }

            }

            public class NativeTupleArgData : AVMObjectType
            {
                public ulong Field0 { get; set; }

                public string Field1 { get; set; }

                public bool Field2 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField1.From(Field1);
                    stringRef[ret.Count] = vField1.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vField2.From(Field2);
                    ret.AddRange(vField2.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static NativeTupleArgData Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new NativeTupleArgData();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    var indexField1 = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField1.Decode(bytes.Skip(indexField1 + prefixOffset).ToArray());
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is string vField1Value) { ret.Field1 = vField1Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vField2.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField2 = vField2.ToValue();
                    if (valueField2 is bool vField2Value) { ret.Field2 = vField2Value; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as NativeTupleArgData);
                }
                public bool Equals(NativeTupleArgData? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(NativeTupleArgData left, NativeTupleArgData right)
                {
                    return EqualityComparer<NativeTupleArgData>.Default.Equals(left, right);
                }
                public static bool operator !=(NativeTupleArgData left, NativeTupleArgData right)
                {
                    return !(left == right);
                }

            }

        }

        public class Events
        {
            public class StringReadonlyEvent
            {
                public static readonly byte[] Selector = new byte[4] { 70, 75, 206, 120 };
                public const string Signature = "stringReadonly(string)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public string Field0 { get; set; }

                public static StringReadonlyEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new StringReadonlyEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField0.Decode(eventData.Skip(indexField0).ToArray());
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is string vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class StringEvent
            {
                public static readonly byte[] Selector = new byte[4] { 68, 231, 144, 227 };
                public const string Signature = "string(string)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public string Field0 { get; set; }

                public static StringEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new StringEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField0.Decode(eventData.Skip(indexField0).ToArray());
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is string vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class StringArrayEvent
            {
                public static readonly byte[] Selector = new byte[4] { 125, 42, 68, 191 };
                public const string Signature = "stringArray(string[])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public string[] Field0 { get; set; }

                public static StringArrayEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new StringArrayEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string[]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is string[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class BytesEvent
            {
                public static readonly byte[] Selector = new byte[4] { 195, 61, 74, 28 };
                public const string Signature = "bytes(byte[])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static BytesEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new BytesEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class BooleanEvent
            {
                public static readonly byte[] Selector = new byte[4] { 68, 188, 57, 95 };
                public const string Signature = "boolean(bool)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public bool Field0 { get; set; }

                public static BooleanEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new BooleanEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is bool vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class BiguintEvent
            {
                public static readonly byte[] Selector = new byte[4] { 116, 165, 144, 199 };
                public const string Signature = "biguint(uint512)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt512 Field0 { get; set; }

                public static BiguintEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new BiguintEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint512");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is AVM.ClientGenerator.ABI.ARC4.Types.UInt512 vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class StructEvent
            {
                public static readonly byte[] Selector = new byte[4] { 114, 185, 40, 251 };
                public const string Signature = "struct((address,uint256))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.StructAddressUint256 Field0 { get; set; }

                public static StructEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new StructEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    ret.Field0 = Structs.StructAddressUint256.Parse(queue.ToArray());
                    { var consumedField0 = ret.Field0.ToByteArray().Length; for (int i = 0; i < consumedField0 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    return ret;

                }

            }

            public class InnerStructEvent
            {
                public static readonly byte[] Selector = new byte[4] { 36, 147, 180, 24 };
                public const string Signature = "innerStruct((uint64,(address,uint256)))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.InnerStruct Field0 { get; set; }

                public static InnerStructEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new InnerStructEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    ret.Field0 = Structs.InnerStruct.Parse(queue.ToArray());
                    { var consumedField0 = ret.Field0.ToByteArray().Length; for (int i = 0; i < consumedField0 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    return ret;

                }

            }

            public class PaymentTxnEvent
            {
                public static readonly byte[] Selector = new byte[4] { 185, 175, 19, 166 };
                public const string Signature = "PaymentTxn(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static PaymentTxnEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new PaymentTxnEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class ApplicationCallTxnEvent
            {
                public static readonly byte[] Selector = new byte[4] { 185, 252, 134, 131 };
                public const string Signature = "ApplicationCallTxn(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static ApplicationCallTxnEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new ApplicationCallTxnEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class AssetTransferTxnEvent
            {
                public static readonly byte[] Selector = new byte[4] { 53, 215, 79, 59 };
                public const string Signature = "AssetTransferTxn(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static AssetTransferTxnEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new AssetTransferTxnEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class KeyRegistrationTxnEvent
            {
                public static readonly byte[] Selector = new byte[4] { 191, 144, 143, 90 };
                public const string Signature = "KeyRegistrationTxn(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static KeyRegistrationTxnEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new KeyRegistrationTxnEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class TransactionEvent
            {
                public static readonly byte[] Selector = new byte[4] { 249, 62, 6, 153 };
                public const string Signature = "Transaction(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static TransactionEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new TransactionEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class AccountEvent
            {
                public static readonly byte[] Selector = new byte[4] { 217, 220, 192, 241 };
                public const string Signature = "account(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static AccountEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new AccountEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class AssetEvent
            {
                public static readonly byte[] Selector = new byte[4] { 10, 35, 187, 80 };
                public const string Signature = "asset(uint64)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }

                public static AssetEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new AssetEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class ApplicationEvent
            {
                public static readonly byte[] Selector = new byte[4] { 202, 51, 198, 224 };
                public const string Signature = "application(uint64)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }

                public static ApplicationEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new ApplicationEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class AccountIndexedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 161, 18, 83, 215 };
                public const string Signature = "accountIndexed(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static AccountIndexedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new AccountIndexedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class AssetIndexedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 114, 13, 24, 39 };
                public const string Signature = "assetIndexed(uint64)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }

                public static AssetIndexedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new AssetIndexedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class ApplicationIndexedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 191, 148, 246, 198 };
                public const string Signature = "applicationIndexed(uint64)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }

                public static ApplicationIndexedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new ApplicationIndexedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4StaticBytes8Event
            {
                public static readonly byte[] Selector = new byte[4] { 82, 59, 99, 187 };
                public const string Signature = "arc4StaticBytes8(byte[8])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static Arc4StaticBytes8Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4StaticBytes8Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[8]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4StaticBytes32Event
            {
                public static readonly byte[] Selector = new byte[4] { 29, 42, 31, 214 };
                public const string Signature = "arc4StaticBytes32(byte[32])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static Arc4StaticBytes32Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4StaticBytes32Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4AddressEvent
            {
                public static readonly byte[] Selector = new byte[4] { 216, 248, 181, 140 };
                public const string Signature = "arc4Address(address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address Field0 { get; set; }

                public static Arc4AddressEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4AddressEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is Algorand.Address vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4BoolEvent
            {
                public static readonly byte[] Selector = new byte[4] { 119, 88, 50, 28 };
                public const string Signature = "arc4Bool(bool)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public bool Field0 { get; set; }

                public static Arc4BoolEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4BoolEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is bool vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4ByteEvent
            {
                public static readonly byte[] Selector = new byte[4] { 175, 186, 172, 231 };
                public const string Signature = "arc4Byte(byte)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte Field0 { get; set; }

                public static Arc4ByteEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4ByteEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4DynamicArrayOfStructEvent
            {
                public static readonly byte[] Selector = new byte[4] { 247, 180, 218, 98 };
                public const string Signature = "arc4DynamicArrayOfStruct((address,uint256)[])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.StructAddressUint256[] Field0 { get; set; }

                public static Arc4DynamicArrayOfStructEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4DynamicArrayOfStructEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrField0 = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrField0.Decode(eventData.Skip(indexField0).ToArray());
                    ret.Field0 = arrField0.Value.ToArray();
                    return ret;

                }

            }

            public class Arc4StaticArrayOf2BytesEvent
            {
                public static readonly byte[] Selector = new byte[4] { 169, 218, 5, 91 };
                public const string Signature = "arc4StaticArrayOf2Bytes(byte[2])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte[] Field0 { get; set; }

                public static Arc4StaticArrayOf2BytesEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4StaticArrayOf2BytesEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[2]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4StaticArrayOf2StructsEvent
            {
                public static readonly byte[] Selector = new byte[4] { 43, 187, 196, 39 };
                public const string Signature = "arc4StaticArrayOf2Structs((address,uint256)[2])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.StructAddressUint256[] Field0 { get; set; }

                public static Arc4StaticArrayOf2StructsEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4StaticArrayOf2StructsEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrField0 = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = true, FixedLength = 2 };
                    arrField0.Decode(eventData.Skip(indexField0).ToArray());
                    ret.Field0 = arrField0.Value.ToArray();
                    return ret;

                }

            }

            public class Arc4UFixed8X16Event
            {
                public static readonly byte[] Selector = new byte[4] { 138, 52, 76, 140 };
                public const string Signature = "arc4UFixed8x16(ufixed8x16)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public System.Numerics.BigInteger Field0 { get; set; }

                public static Arc4UFixed8X16Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UFixed8X16Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("ufixed8x16");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is System.Numerics.BigInteger vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UFixed512X160Event
            {
                public static readonly byte[] Selector = new byte[4] { 41, 198, 195, 206 };
                public const string Signature = "arc4UFixed512x160(ufixed512x160)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public System.Numerics.BigInteger Field0 { get; set; }

                public static Arc4UFixed512X160Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UFixed512X160Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("ufixed512x160");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is System.Numerics.BigInteger vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN8Event
            {
                public static readonly byte[] Selector = new byte[4] { 201, 27, 15, 95 };
                public const string Signature = "arc4UintN8(uint8)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte Field0 { get; set; }

                public static Arc4UintN8Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN8Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN512Event
            {
                public static readonly byte[] Selector = new byte[4] { 189, 128, 37, 205 };
                public const string Signature = "arc4UintN512(uint512)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt512 Field0 { get; set; }

                public static Arc4UintN512Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN512Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint512");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is AVM.ClientGenerator.ABI.ARC4.Types.UInt512 vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN128AliasEvent
            {
                public static readonly byte[] Selector = new byte[4] { 114, 233, 96, 224 };
                public const string Signature = "arc4UintN128Alias(uint128)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt128 Field0 { get; set; }

                public static Arc4UintN128AliasEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN128AliasEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint128");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is AVM.ClientGenerator.ABI.ARC4.Types.UInt128 vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN16AliasEvent
            {
                public static readonly byte[] Selector = new byte[4] { 86, 100, 114, 160 };
                public const string Signature = "arc4UintN16Alias(uint16)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ushort Field0 { get; set; }

                public static Arc4UintN16AliasEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN16AliasEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint16");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ushort vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN256AliasEvent
            {
                public static readonly byte[] Selector = new byte[4] { 15, 31, 1, 125 };
                public const string Signature = "arc4UintN256Alias(uint256)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Field0 { get; set; }

                public static Arc4UintN256AliasEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN256AliasEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is AVM.ClientGenerator.ABI.ARC4.Types.UInt256 vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN64AliasEvent
            {
                public static readonly byte[] Selector = new byte[4] { 208, 115, 142, 249 };
                public const string Signature = "arc4UintN64Alias(uint64)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }

                public static Arc4UintN64AliasEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN64AliasEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN8AliasEvent
            {
                public static readonly byte[] Selector = new byte[4] { 47, 146, 241, 138 };
                public const string Signature = "arc4UintN8Alias(uint8)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte Field0 { get; set; }

                public static Arc4UintN8AliasEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN8AliasEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4UintN82TupleEvent
            {
                public static readonly byte[] Selector = new byte[4] { 113, 153, 26, 48 };
                public const string Signature = "arc4UintN82Tuple(uint8,uint8)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte Field0 { get; set; }
                public byte Field1 { get; set; }

                public static Arc4UintN82TupleEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN82TupleEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is byte vField1Value) { ret.Field1 = vField1Value; }
                    return ret;

                }

            }

            public class Arc4UintN83TupleEvent
            {
                public static readonly byte[] Selector = new byte[4] { 47, 1, 236, 116 };
                public const string Signature = "arc4UintN83Tuple(uint8,uint8,uint8)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public byte Field0 { get; set; }
                public byte Field1 { get; set; }
                public byte Field2 { get; set; }

                public static Arc4UintN83TupleEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4UintN83TupleEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte vField0Value) { ret.Field0 = vField0Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is byte vField1Value) { ret.Field1 = vField1Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField2.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField2 = vField2.ToValue();
                    if (valueField2 is byte vField2Value) { ret.Field2 = vField2Value; }
                    return ret;

                }

            }

            public class Arc4ComplexTupleEvent
            {
                public static readonly byte[] Selector = new byte[4] { 237, 138, 159, 38 };
                public const string Signature = "arc4ComplexTuple(address,(uint256,uint256),(uint256,uint256),(uint256,uint256))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address Field0 { get; set; }
                public Structs.Arc4ComplexTupleArgDataField1 Field1 { get; set; }
                public Structs.Arc4ComplexTupleArgDataField1 Field2 { get; set; }
                public Structs.Arc4ComplexTupleArgDataField1 Field3 { get; set; }

                public static Arc4ComplexTupleEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4ComplexTupleEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is Algorand.Address vField0Value) { ret.Field0 = vField0Value; }
                    ret.Field1 = Structs.Arc4ComplexTupleArgDataField1.Parse(queue.ToArray());
                    { var consumedField1 = ret.Field1.ToByteArray().Length; for (int i = 0; i < consumedField1 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    ret.Field2 = Structs.Arc4ComplexTupleArgDataField1.Parse(queue.ToArray());
                    { var consumedField2 = ret.Field2.ToByteArray().Length; for (int i = 0; i < consumedField2 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    ret.Field3 = Structs.Arc4ComplexTupleArgDataField1.Parse(queue.ToArray());
                    { var consumedField3 = ret.Field3.ToByteArray().Length; for (int i = 0; i < consumedField3 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    return ret;

                }

            }

            public class Uint64Event
            {
                public static readonly byte[] Selector = new byte[4] { 18, 66, 119, 50 };
                public const string Signature = "uint64(uint64)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }

                public static Uint64Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Uint64Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Uint64ArrayEvent
            {
                public static readonly byte[] Selector = new byte[4] { 111, 52, 119, 209 };
                public const string Signature = "uint64Array(uint64[])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong[] Field0 { get; set; }

                public static Uint64ArrayEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Uint64ArrayEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64[]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class BooleanArrayEvent
            {
                public static readonly byte[] Selector = new byte[4] { 149, 75, 50, 50 };
                public const string Signature = "booleanArray(bool[])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public bool[] Field0 { get; set; }

                public static BooleanArrayEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new BooleanArrayEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool[]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is bool[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4StrEvent
            {
                public static readonly byte[] Selector = new byte[4] { 209, 175, 254, 166 };
                public const string Signature = "arc4Str(string)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public string Field0 { get; set; }

                public static Arc4StrEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4StrEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField0.Decode(eventData.Skip(indexField0).ToArray());
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is string vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

            public class Arc4TupleEvent
            {
                public static readonly byte[] Selector = new byte[4] { 108, 188, 227, 229 };
                public const string Signature = "arc4Tuple((uint64,string))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc4TupleArgData Field0 { get; set; }

                public static Arc4TupleEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc4TupleEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc4TupleArgData.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class NativeTupleEvent
            {
                public static readonly byte[] Selector = new byte[4] { 160, 43, 191, 24 };
                public const string Signature = "nativeTuple(uint64,string,bool)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong Field0 { get; set; }
                public string Field1 { get; set; }
                public bool Field2 { get; set; }

                public static NativeTupleEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new NativeTupleEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    var indexField1 = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField1.Decode(eventData.Skip(indexField1).ToArray());
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is string vField1Value) { ret.Field1 = vField1Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vField2.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField2 = vField2.ToValue();
                    if (valueField2 is bool vField2Value) { ret.Field2 = vField2Value; }
                    return ret;

                }

            }

            public class FixedArrayUint64Event
            {
                public static readonly byte[] Selector = new byte[4] { 27, 242, 64, 216 };
                public const string Signature = "fixedArrayUint64(uint64[3])";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong[] Field0 { get; set; }

                public static FixedArrayUint64Event Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new FixedArrayUint64Event();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64[3]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong[] vField0Value) { ret.Field0 = vField0Value; }
                    return ret;

                }

            }

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<string> StringReadonly(string data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 83, 255, 44, 46 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); dataAbi.From(data);

            var result = await base.SimApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.String();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToString();

        }

        public async Task<List<Transaction>> StringReadonly_Transactions(string data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 83, 255, 44, 46 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<string> String(string data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 145, 17, 196, 188 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.String();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToString();

        }

        public async Task<List<Transaction>> String_Transactions(string data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 145, 17, 196, 188 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<string[]> StringArray(string[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 229, 205, 129, 201 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.String>("string"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.String>("string");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToStringArray();

        }

        public async Task<List<Transaction>> StringArray_Transactions(string[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 229, 205, 129, 201 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.String>("string"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Bytes(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 200, 35, 15, 215 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Bytes_Transactions(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 200, 35, 15, 215 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<bool> Boolean(bool data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 228, 112, 230, 8 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Boolean_Transactions(bool data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 228, 112, 230, 8 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt512> Biguint(AVM.ClientGenerator.ABI.ARC4.Types.UInt512 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 133, 81, 239, 58 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt512();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Biguint_Transactions(AVM.ClientGenerator.ABI.ARC4.Types.UInt512 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 133, 81, 239, 58 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> structAddressUint256</param>
        public async Task<Structs.StructAddressUint256> Struct(Structs.StructAddressUint256 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 3, 7, 75, 25 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.StructAddressUint256.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Struct_Transactions(Structs.StructAddressUint256 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 3, 7, 75, 25 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> innerStruct</param>
        public async Task<Structs.InnerStruct> InnerStruct(Structs.InnerStruct data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 114, 64, 179, 102 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.InnerStruct.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> InnerStruct_Transactions(Structs.InnerStruct data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 114, 64, 179, 102 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> PaymentTxn(PaymentTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 162, 120, 249, 143 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> PaymentTxn_Transactions(PaymentTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 162, 120, 249, 143 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> ApplicationCallTxn(ApplicationCallTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 179, 119, 190, 109 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> ApplicationCallTxn_Transactions(ApplicationCallTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 179, 119, 190, 109 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> AssetTransferTxn(AssetTransferTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 164, 175, 142, 228 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> AssetTransferTxn_Transactions(AssetTransferTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 164, 175, 142, 228 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> KeyRegistrationTxn(KeyRegistrationTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 227, 32, 188, 94 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> KeyRegistrationTxn_Transactions(KeyRegistrationTransaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 227, 32, 188, 94 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Transaction(Transaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 189, 145, 34, 249 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Transaction_Transactions(Transaction data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { data });
            byte[] abiHandle = { 189, 145, 34, 249 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<Algorand.Address> Account(Algorand.Address data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 82, 186, 237, 146 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Account_Transactions(Algorand.Address data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 82, 186, 237, 146 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong> Asset(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 172, 182, 198, 159 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Asset_Transactions(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 172, 182, 198, 159 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong> Application(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 129, 190, 230, 34 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Application_Transactions(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 129, 190, 230, 34 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<Algorand.Address> AccountIndexed(Address data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            int _acctRefBase = _tx_accounts.Count;
            _tx_accounts.AddRange(new List<Address> { data });
            byte dataRefIndex = (byte)(_acctRefBase + 0 + 1);
            byte[] abiHandle = { 212, 157, 28, 93 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataRefIndex }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> AccountIndexed_Transactions(Address data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            int _acctRefBase = _tx_accounts.Count;
            _tx_accounts.AddRange(new List<Address> { data });
            byte dataRefIndex = (byte)(_acctRefBase + 0 + 1);
            byte[] abiHandle = { 212, 157, 28, 93 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataRefIndex }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong> AssetIndexed(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            int _assetRefBase = _tx_assets.Count;
            _tx_assets.AddRange(new List<ulong> { data });
            byte dataRefIndex = (byte)(_assetRefBase + 0);
            byte[] abiHandle = { 208, 12, 86, 175 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Asset(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataRefIndex }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> AssetIndexed_Transactions(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            int _assetRefBase = _tx_assets.Count;
            _tx_assets.AddRange(new List<ulong> { data });
            byte dataRefIndex = (byte)(_assetRefBase + 0);
            byte[] abiHandle = { 208, 12, 86, 175 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Asset(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataRefIndex }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong> ApplicationIndexed(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            int _appRefBase = _tx_apps.Count;
            _tx_apps.AddRange(new List<ulong> { data });
            byte dataRefIndex = (byte)(_appRefBase + 0);
            byte[] abiHandle = { 3, 149, 208, 158 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Application(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataRefIndex }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> ApplicationIndexed_Transactions(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            int _appRefBase = _tx_apps.Count;
            _tx_apps.AddRange(new List<ulong> { data });
            byte dataRefIndex = (byte)(_appRefBase + 0);
            byte[] abiHandle = { 3, 149, 208, 158 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Application(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataRefIndex }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Arc4StaticBytes8(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 181, 206, 142, 3 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(8, "byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(8, "byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc4StaticBytes8_Transactions(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 181, 206, 142, 3 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(8, "byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Arc4StaticBytes32(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 22, 133, 57, 116 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(32, "byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(32, "byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc4StaticBytes32_Transactions(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 22, 133, 57, 116 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(32, "byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Arc4StaticBytes1020(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 66, 197, 187, 143 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(1020, "byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(1020, "byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc4StaticBytes1020_Transactions(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 66, 197, 187, 143 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(1020, "byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Arc4DynamicBytes(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 170, 90, 193, 201 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc4DynamicBytes_Transactions(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 170, 90, 193, 201 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<Algorand.Address> Arc4Address(Algorand.Address data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 100, 224, 8, 240 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Arc4Address_Transactions(Algorand.Address data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 100, 224, 8, 240 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<bool> Arc4Bool(bool data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 170, 187, 236, 131 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc4Bool_Transactions(bool data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 170, 187, 236, 131 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte> Arc4Byte(byte data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 54, 249, 234, 173 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Byte();
            returnValueObj.Decode(lastLogReturnData);
            return ReverseIfLittleEndian(lastLogReturnData)[0];

        }

        public async Task<List<Transaction>> Arc4Byte_Transactions(byte data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 54, 249, 234, 173 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> structAddressUint256</param>
        public async Task<Structs.StructAddressUint256[]> Arc4DynamicArrayOfStruct(Structs.StructAddressUint256[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 54, 210, 65, 65 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = false, FixedLength = 0 }; dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value.ToArray();

        }

        public async Task<List<Transaction>> Arc4DynamicArrayOfStruct_Transactions(Structs.StructAddressUint256[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 54, 210, 65, 65 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = false, FixedLength = 0 }; dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte[]> Arc4StaticArrayOf2Bytes(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 206, 102, 242, 234 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(2, "byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(2, "byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc4StaticArrayOf2Bytes_Transactions(byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 206, 102, 242, 234 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(2, "byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> structAddressUint256</param>
        public async Task<Structs.StructAddressUint256[]> Arc4StaticArrayOf2Structs(Structs.StructAddressUint256[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 77, 72, 110, 153 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = true, FixedLength = 2 }; dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = true, FixedLength = 2 };
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value.ToArray();

        }

        public async Task<List<Transaction>> Arc4StaticArrayOf2Structs_Transactions(Structs.StructAddressUint256[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 77, 72, 110, 153 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.StructAddressUint256>(x => Structs.StructAddressUint256.Parse(x)) { IsFixedLength = true, FixedLength = 2 }; dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<System.Numerics.BigInteger> Arc4UFixed8X16(System.Numerics.BigInteger data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 105, 155, 42, 24 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UFixed(8, 16); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UFixed(8, 16);
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value;

        }

        public async Task<List<Transaction>> Arc4UFixed8X16_Transactions(System.Numerics.BigInteger data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 105, 155, 42, 24 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UFixed(8, 16); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<System.Numerics.BigInteger> Arc4UFixed512X160(System.Numerics.BigInteger data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 37, 143, 115, 172 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UFixed(512, 160); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UFixed(512, 160);
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value;

        }

        public async Task<List<Transaction>> Arc4UFixed512X160_Transactions(System.Numerics.BigInteger data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 37, 143, 115, 172 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UFixed(512, 160); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte> Arc4UintN8(byte data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 80, 226, 98, 126 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Byte();
            returnValueObj.Decode(lastLogReturnData);
            return ReverseIfLittleEndian(lastLogReturnData)[0];

        }

        public async Task<List<Transaction>> Arc4UintN8_Transactions(byte data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 80, 226, 98, 126 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt512> Arc4UintN512(AVM.ClientGenerator.ABI.ARC4.Types.UInt512 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 146, 80, 184, 86 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt512();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc4UintN512_Transactions(AVM.ClientGenerator.ABI.ARC4.Types.UInt512 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 146, 80, 184, 86 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt128> Arc4UintN128Alias(AVM.ClientGenerator.ABI.ARC4.Types.UInt128 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 40, 238, 36, 208 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt128();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc4UintN128Alias_Transactions(AVM.ClientGenerator.ABI.ARC4.Types.UInt128 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 40, 238, 36, 208 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ushort> Arc4UintN16Alias(ushort data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 199, 196, 232, 106 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt16(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt16();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt16(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc4UintN16Alias_Transactions(ushort data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 199, 196, 232, 106 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt16(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt256> Arc4UintN256Alias(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 27, 103, 164, 46 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc4UintN256Alias_Transactions(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 27, 103, 164, 46 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong> Arc4UintN64Alias(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 123, 131, 41, 152 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc4UintN64Alias_Transactions(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 123, 131, 41, 152 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<byte> Arc4UintN8Alias(byte data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 224, 218, 7, 14 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Byte();
            returnValueObj.Decode(lastLogReturnData);
            return ReverseIfLittleEndian(lastLogReturnData)[0];

        }

        public async Task<List<Transaction>> Arc4UintN8Alias_Transactions(byte data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 224, 218, 7, 14 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> Arc4UintN82TupleArgData</param>
        public async Task<Structs.Arc4UintN82TupleArgData> Arc4UintN82Tuple(Structs.Arc4UintN82TupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 218, 245, 108, 211 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.Arc4UintN82TupleArgData.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Arc4UintN82Tuple_Transactions(Structs.Arc4UintN82TupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 218, 245, 108, 211 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> Arc4UintN83TupleArgData</param>
        public async Task<Structs.Arc4UintN83TupleArgData> Arc4UintN83Tuple(Structs.Arc4UintN83TupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 185, 112, 147, 243 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.Arc4UintN83TupleArgData.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Arc4UintN83Tuple_Transactions(Structs.Arc4UintN83TupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 185, 112, 147, 243 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> Arc4ComplexTupleArgData</param>
        public async Task<Structs.Arc4ComplexTupleArgData> Arc4ComplexTuple(Structs.Arc4ComplexTupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 182, 22, 3, 242 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.Arc4ComplexTupleArgData.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Arc4ComplexTuple_Transactions(Structs.Arc4ComplexTupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 182, 22, 3, 242 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong> Uint64(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 163, 254, 194, 95 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Uint64_Transactions(ulong data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 163, 254, 194, 95 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong[]> Uint64Array(ulong[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 49, 174, 206, 241 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.UInt64>("uint64"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.UInt64>("uint64");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value.Select(v => (ulong)v.ToValue()).ToArray();

        }

        public async Task<List<Transaction>> Uint64Array_Transactions(ulong[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 49, 174, 206, 241 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.UInt64>("uint64"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<bool[]> BooleanArray(bool[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 138, 44, 103, 141 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Bool>("bool"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Bool>("bool");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value.Select(v => (bool)v.ToValue()).ToArray();

        }

        public async Task<List<Transaction>> BooleanArray_Transactions(bool[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 138, 44, 103, 141 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Bool>("bool"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<string> Arc4Str(string data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 130, 235, 255, 146 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.String();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToString();

        }

        public async Task<List<Transaction>> Arc4Str_Transactions(string data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 130, 235, 255, 146 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> Arc4TupleArgData</param>
        public async Task<Structs.Arc4TupleArgData> Arc4Tuple(Structs.Arc4TupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 144, 85, 147, 201 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.Arc4TupleArgData.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Arc4Tuple_Transactions(Structs.Arc4TupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 144, 85, 147, 201 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> NativeTupleArgData</param>
        public async Task<Structs.NativeTupleArgData> NativeTuple(Structs.NativeTupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 41, 87, 190, 201 };

            var result = await base.CallApp(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.NativeTupleArgData.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> NativeTuple_Transactions(Structs.NativeTupleArgData data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 41, 87, 190, 201 };

            return await base.MakeTransactionList(new List<object> { abiHandle, data }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="data"> </param>
        public async Task<ulong[]> FixedArrayUint64(ulong[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 104, 106, 50, 166 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.UInt64>(3, "uint64"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.UInt64>(3, "uint64");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.Value.Select(v => (ulong)v.ToValue()).ToArray();

        }

        public async Task<List<Transaction>> FixedArrayUint64_Transactions(ulong[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 104, 106, 50, 166 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.UInt64>(3, "uint64"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Constructor Bare Action
        ///</summary>
        public async Task CreateApplication(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.CreateApplication)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 0, 193, 250, 21 };

            var result = await base.CallApp(new List<object> { }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> CreateApplication_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.CreateApplication)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 0, 193, 250, 21 };

            return await base.MakeTransactionList(new List<object> { }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        protected override ulong? ExtraProgramPages { get; set; } = 0;
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiQXZtVHlwZXMiLCJkZXNjIjpudWxsLCJuZXR3b3JrcyI6e30sInN0cnVjdHMiOnsiaW5uZXJTdHJ1Y3QiOlt7Im5hbWUiOiJudW0iLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoic3RydWN0IiwidHlwZSI6InN0cnVjdEFkZHJlc3NVaW50MjU2In1dLCJzdHJ1Y3RBZGRyZXNzVWludDI1NiI6W3sibmFtZSI6ImFkZHJlc3MiLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6InVpbnQyNTYiLCJ0eXBlIjoidWludDI1NiJ9XSwiQXJjNFVpbnROODJUdXBsZUFyZ0RhdGEiOlt7Im5hbWUiOiJmaWVsZDAiLCJ0eXBlIjoidWludDgifSx7Im5hbWUiOiJmaWVsZDEiLCJ0eXBlIjoidWludDgifV0sIkFyYzRVaW50TjgzVHVwbGVBcmdEYXRhIjpbeyJuYW1lIjoiZmllbGQwIiwidHlwZSI6InVpbnQ4In0seyJuYW1lIjoiZmllbGQxIiwidHlwZSI6InVpbnQ4In0seyJuYW1lIjoiZmllbGQyIiwidHlwZSI6InVpbnQ4In1dLCJBcmM0Q29tcGxleFR1cGxlQXJnRGF0YSI6W3sibmFtZSI6ImZpZWxkMCIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoiZmllbGQxIiwidHlwZSI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhRmllbGQxIn0seyJuYW1lIjoiZmllbGQyIiwidHlwZSI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhRmllbGQxIn0seyJuYW1lIjoiZmllbGQzIiwidHlwZSI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhRmllbGQxIn1dLCJBcmM0Q29tcGxleFR1cGxlQXJnRGF0YUZpZWxkMSI6W3sibmFtZSI6ImZpZWxkMCIsInR5cGUiOiJ1aW50MjU2In0seyJuYW1lIjoiZmllbGQxIiwidHlwZSI6InVpbnQyNTYifV0sIkFyYzRUdXBsZUFyZ0RhdGEiOlt7Im5hbWUiOiJmaWVsZDAiLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiZmllbGQxIiwidHlwZSI6InN0cmluZyJ9XSwiTmF0aXZlVHVwbGVBcmdEYXRhIjpbeyJuYW1lIjoiZmllbGQwIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImZpZWxkMSIsInR5cGUiOiJzdHJpbmcifSx7Im5hbWUiOiJmaWVsZDIiLCJ0eXBlIjoiYm9vbCJ9XX0sIk1ldGhvZHMiOlt7Im5hbWUiOiJzdHJpbmdSZWFkb25seSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbeyJuYW1lIjoic3RyaW5nUmVhZG9ubHkiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoic3RyaW5nIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InN0cmluZyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6InN0cmluZyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoic3RyaW5nQXJyYXkiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoic3RyaW5nW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InN0cmluZ1tdIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoic3RyaW5nQXJyYXkiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoic3RyaW5nW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYnl0ZXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJieXRlcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYm9vbGVhbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYm9vbGVhbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImJpZ3VpbnQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDUxMiIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDUxMiIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImJpZ3VpbnQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDUxMiIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJzdHJ1Y3QiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGFkZHJlc3MsdWludDI1NikiLCJzdHJ1Y3QiOiJzdHJ1Y3RBZGRyZXNzVWludDI1NiIsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYpIiwic3RydWN0Ijoic3RydWN0QWRkcmVzc1VpbnQyNTYiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoic3RydWN0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYpIiwic3RydWN0Ijoic3RydWN0QWRkcmVzc1VpbnQyNTYiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImlubmVyU3RydWN0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50NjQsKGFkZHJlc3MsdWludDI1NikpIiwic3RydWN0IjoiaW5uZXJTdHJ1Y3QiLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIodWludDY0LChhZGRyZXNzLHVpbnQyNTYpKSIsInN0cnVjdCI6ImlubmVyU3RydWN0IiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImlubmVyU3RydWN0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50NjQsKGFkZHJlc3MsdWludDI1NikpIiwic3RydWN0IjoiaW5uZXJTdHJ1Y3QiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6IlBheW1lbnRUeG4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoicGF5Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJQYXltZW50VHhuIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbMzJdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6IkFwcGxpY2F0aW9uQ2FsbFR4biIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhcHBsIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJBcHBsaWNhdGlvbkNhbGxUeG4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiQXNzZXRUcmFuc2ZlclR4biIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJheGZlciIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiQXNzZXRUcmFuc2ZlclR4biIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJLZXlSZWdpc3RyYXRpb25UeG4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoia2V5cmVnIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJLZXlSZWdpc3RyYXRpb25UeG4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiVHJhbnNhY3Rpb24iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidHhuIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJUcmFuc2FjdGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhY2NvdW50IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhY2NvdW50IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbMzJdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFzc2V0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXNzZXQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFwcGxpY2F0aW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXBwbGljYXRpb24iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFjY291bnRJbmRleGVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFjY291bnQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhY2NvdW50SW5kZXhlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhc3NldEluZGV4ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYXNzZXQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFzc2V0SW5kZXhlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXBwbGljYXRpb25JbmRleGVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFwcGxpY2F0aW9uIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcHBsaWNhdGlvbkluZGV4ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRTdGF0aWNCeXRlczgiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVs4XSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVs4XSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRTdGF0aWNCeXRlczgiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVs4XSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM0U3RhdGljQnl0ZXMzMiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0U3RhdGljQnl0ZXMzMiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM0U3RhdGljQnl0ZXMxMDIwIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbMTAyMF0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbMTAyMF0iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzREeW5hbWljQnl0ZXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRBZGRyZXNzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0QWRkcmVzcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRCb29sIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0Qm9vbCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRCeXRlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGUiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJ5dGUiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0Qnl0ZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzREeW5hbWljQXJyYXlPZlN0cnVjdCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyx1aW50MjU2KVtdIiwic3RydWN0Ijoic3RydWN0QWRkcmVzc1VpbnQyNTYiLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIoYWRkcmVzcyx1aW50MjU2KVtdIiwic3RydWN0Ijoic3RydWN0QWRkcmVzc1VpbnQyNTYiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjNER5bmFtaWNBcnJheU9mU3RydWN0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYpW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNFN0YXRpY0FycmF5T2YyQnl0ZXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVsyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVsyXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRTdGF0aWNBcnJheU9mMkJ5dGVzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNFN0YXRpY0FycmF5T2YyU3RydWN0cyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyx1aW50MjU2KVsyXSIsInN0cnVjdCI6InN0cnVjdEFkZHJlc3NVaW50MjU2IiwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKGFkZHJlc3MsdWludDI1NilbMl0iLCJzdHJ1Y3QiOiJzdHJ1Y3RBZGRyZXNzVWludDI1NiIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0U3RhdGljQXJyYXlPZjJTdHJ1Y3RzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYpWzJdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRVRml4ZWQ4eDE2IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVmaXhlZDh4MTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVmaXhlZDh4MTYiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0VUZpeGVkOHgxNiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1Zml4ZWQ4eDE2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRVRml4ZWQ1MTJ4MTYwIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVmaXhlZDUxMngxNjAiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVmaXhlZDUxMngxNjAiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0VUZpeGVkNTEyeDE2MCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1Zml4ZWQ1MTJ4MTYwIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRVaW50TjgiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjNFVpbnROOCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM0VWludE41MTIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDUxMiIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDUxMiIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRVaW50TjUxMiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NTEyIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRVaW50TjEyOEFsaWFzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQxMjgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQxMjgiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0VWludE4xMjhBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50MTI4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRVaW50TjE2QWxpYXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDE2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50MTYiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0VWludE4xNkFsaWFzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQxNiIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM0VWludE4yNTZBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjNFVpbnROMjU2QWxpYXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM0VWludE42NEFsaWFzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjNFVpbnRONjRBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNFVpbnROOEFsaWFzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRVaW50TjhBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM0VWludE44MlR1cGxlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50OCx1aW50OCkiLCJzdHJ1Y3QiOiJBcmM0VWludE44MlR1cGxlQXJnRGF0YSIsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6Iih1aW50OCx1aW50OCkiLCJzdHJ1Y3QiOiJBcmM0VWludE44MlR1cGxlQXJnRGF0YSIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0VWludE44MlR1cGxlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMSIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRVaW50TjgzVHVwbGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKHVpbnQ4LHVpbnQ4LHVpbnQ4KSIsInN0cnVjdCI6IkFyYzRVaW50TjgzVHVwbGVBcmdEYXRhIiwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHVpbnQ4LHVpbnQ4LHVpbnQ4KSIsInN0cnVjdCI6IkFyYzRVaW50TjgzVHVwbGVBcmdEYXRhIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRVaW50TjgzVHVwbGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIxIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIyIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNENvbXBsZXhUdXBsZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcywodWludDI1Nix1aW50MjU2KSwodWludDI1Nix1aW50MjU2KSwodWludDI1Nix1aW50MjU2KSkiLCJzdHJ1Y3QiOiJBcmM0Q29tcGxleFR1cGxlQXJnRGF0YSIsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihhZGRyZXNzLCh1aW50MjU2LHVpbnQyNTYpLCh1aW50MjU2LHVpbnQyNTYpLCh1aW50MjU2LHVpbnQyNTYpKSIsInN0cnVjdCI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRDb21wbGV4VHVwbGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiIodWludDI1Nix1aW50MjU2KSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjEiLCJkZXNjIjpudWxsfSx7InR5cGUiOiIodWludDI1Nix1aW50MjU2KSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiIodWludDI1Nix1aW50MjU2KSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjMiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJ1aW50NjQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJ1aW50NjQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InVpbnQ2NEFycmF5IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NFtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjRbXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6InVpbnQ2NEFycmF5IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NFtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImJvb2xlYW5BcnJheSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2xbXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImJvb2xlYW5BcnJheSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNFN0ciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzRTdHIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoic3RyaW5nIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzRUdXBsZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIodWludDY0LHN0cmluZykiLCJzdHJ1Y3QiOiJBcmM0VHVwbGVBcmdEYXRhIiwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHVpbnQ2NCxzdHJpbmcpIiwic3RydWN0IjoiQXJjNFR1cGxlQXJnRGF0YSIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM0VHVwbGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKHVpbnQ2NCxzdHJpbmcpIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6Im5hdGl2ZVR1cGxlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50NjQsc3RyaW5nLGJvb2wpIiwic3RydWN0IjoiTmF0aXZlVHVwbGVBcmdEYXRhIiwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHVpbnQ2NCxzdHJpbmcsYm9vbCkiLCJzdHJ1Y3QiOiJOYXRpdmVUdXBsZUFyZ0RhdGEiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoibmF0aXZlVHVwbGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9LHsidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjEiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMiIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImZpeGVkQXJyYXlVaW50NjQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0WzNdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjRbM10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJmaXhlZEFycmF5VWludDY0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NFszXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fV0sInN0YXRlIjp7InNjaGVtYSI6eyJnbG9iYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjB9LCJsb2NhbCI6eyJpbnRzIjowLCJieXRlcyI6MH19LCJrZXlzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImxvY2FsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifSwiYm94Ijp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifX0sIm1hcHMiOnsiZ2xvYmFsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwiYm94Ijp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9fX0sImJhcmVBY3Rpb25zIjp7ImNyZWF0ZSI6WyJOb09wIl0sImNhbGwiOltdfSwic291cmNlSW5mbyI6eyJhcHByb3ZhbCI6eyJzb3VyY2VJbmZvIjpbeyJwYyI6WzI5XSwiZXJyb3JNZXNzYWdlIjoiT25Db21wbGV0aW9uIG11c3QgYmUgTm9PcCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM4NF0sImVycm9yTWVzc2FnZSI6Ik9uQ29tcGxldGlvbiBtdXN0IGJlIE5vT3AgJiYgY2FuIG9ubHkgY2FsbCB3aGVuIGNyZWF0aW5nIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDg1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBhcnJheSBlbmNvZGluZyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM5Miw0MjMsNDU1LDQ5OSw1NDYsMTA0MiwxMTM0LDE1OTIsMTYyNSwxNjYxLDE3MDgsMTc1NF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgYXJyYXkgbGVuZ3RoIGhlYWRlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzU3OSwxMDg4XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuYm9vbCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM5OSw0MzAsNTUzLDEwNDksMTY2OF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LmR5bmFtaWNfYXJyYXk8YXJjNC51aW50OD4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNjM3XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuZHluYW1pY19hcnJheTxib29sPiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUxOV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LmR5bmFtaWNfYXJyYXk8c3RyaW5nPiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzExNDRdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PHN0cnVjdEFkZHJlc3NVaW50MjU2PiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE2MDFdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PHVpbnQ2ND4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxMDMwXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuc3RhdGljX2FycmF5PGFyYzQudWludDgsIDEwMjA+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTE3MF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnN0YXRpY19hcnJheTxhcmM0LnVpbnQ4LCAyPiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzgyNCwxMDA0LDEwNjRdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5zdGF0aWNfYXJyYXk8YXJjNC51aW50OCwgMzI+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbOTc5XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuc3RhdGljX2FycmF5PGFyYzQudWludDgsIDg+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTE5Nl0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnN0YXRpY19hcnJheTxzdHJ1Y3RBZGRyZXNzVWludDI1NiwgMj4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxODEyXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuc3RhdGljX2FycmF5PHVpbnQ2NCwgMz4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNTE4XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudHVwbGU8YXJjNC5zdGF0aWNfYXJyYXk8YXJjNC51aW50OCwgMzI+LGFyYzQudHVwbGU8YXJjNC51aW50MjU2LGFyYzQudWludDI1Nj4sYXJjNC50dXBsZTxhcmM0LnVpbnQyNTYsYXJjNC51aW50MjU2PixhcmM0LnR1cGxlPGFyYzQudWludDI1NixhcmM0LnVpbnQyNTY+PiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE3MTNdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC50dXBsZTxhcmM0LnVpbnQ2NCxhcmM0LmR5bmFtaWNfYXJyYXk8YXJjNC51aW50OD4+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTQ3NF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnR1cGxlPGFyYzQudWludDgsYXJjNC51aW50OCxhcmM0LnVpbnQ4PiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE0NDBdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC50dXBsZTxhcmM0LnVpbnQ4LGFyYzQudWludDg+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTc2MV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnR1cGxlPHVpbnQ2NCxzdHJpbmcsYm9vbD4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxMjQ1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudWZpeGVkNTEyeDE2MCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzEyMjBdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC51Zml4ZWQ4eDE2IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTMxOV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQxMjgiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxMzQzXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudWludDE2IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTM2OF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQyNTYiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2MDUsMTI5NF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQ1MTIiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls4NDgsODcyLDEzOTIsMTU3MF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQ2NCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6Wzg5Niw5MjMsOTUxLDExMTIsMTI2OSwxNDE2XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudWludDgiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2NjNdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgaW5uZXJTdHJ1Y3QiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2MzhdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3Igc3RydWN0QWRkcmVzc1VpbnQyNTYiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNzAwXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMSBvZiAodWludDY0LChsZW4rdXRmOFtdKSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNzQ1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMSBvZiAodWludDY0LChsZW4rdXRmOFtdKSxib29sMSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0OTNdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBmb3IgKGxlbisobGVuK3V0ZjhbXSlbXSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNjk1LDE3NDBdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHR1cGxlIGVuY29kaW5nIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjA5XSwiZXJyb3JNZXNzYWdlIjoib3ZlcmZsb3ciLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls3MThdLCJlcnJvck1lc3NhZ2UiOiJ0cmFuc2FjdGlvbiB0eXBlIGlzIGFwcGwiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls3NDddLCJlcnJvck1lc3NhZ2UiOiJ0cmFuc2FjdGlvbiB0eXBlIGlzIGF4ZmVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNzc1XSwiZXJyb3JNZXNzYWdlIjoidHJhbnNhY3Rpb24gdHlwZSBpcyBrZXlyZWciLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2ODldLCJlcnJvck1lc3NhZ2UiOiJ0cmFuc2FjdGlvbiB0eXBlIGlzIHBheSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH1dLCJwY09mZnNldE1ldGhvZCI6Im5vbmUifSwiY2xlYXIiOnsic291cmNlSW5mbyI6W10sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9fSwic291cmNlIjp7ImFwcHJvdmFsIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZWEpqTkM5cGJtUmxlQzVrTG5Sek9qcERiMjUwY21GamRDNWhjSEJ5YjNaaGJGQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0JwYm5SallteHZZMnNnTVNBeUlEQWdPQW9nSUNBZ1lubDBaV05pYkc5amF5QXdlREUxTVdZM1l6YzFJREI0TVRVeFpqZGpOelV3TURJd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWpjS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJkbTFVZVhCbGN5QmxlSFJsYm1SeklFTnZiblJ5WVdOMElIc0tJQ0FnSUhSNGJpQk9kVzFCY0hCQmNtZHpDaUFnSUNCaWVpQnRZV2x1WDE5ZllXeG5iM1J6WDE4dVpHVm1ZWFZzZEVOeVpXRjBaVUExTlFvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCdGRYTjBJR0psSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RS0lDQWdJSEIxYzJoaWVYUmxjM01nTUhnMU0yWm1NbU15WlNBd2VEa3hNVEZqTkdKaklEQjRaVFZqWkRneFl6a2dNSGhqT0RJek1HWmtOeUF3ZUdVME56QmxOakE0SURCNE9EVTFNV1ZtTTJFZ01IZ3dNekEzTkdJeE9TQXdlRGN5TkRCaU16WTJJREI0WVRJM09HWTVPR1lnTUhoaU16YzNZbVUyWkNBd2VHRTBZV1k0WldVMElEQjRaVE15TUdKak5XVWdNSGhpWkRreE1qSm1PU0F3ZURVeVltRmxaRGt5SURCNFlXTmlObU0yT1dZZ01IZzRNV0psWlRZeU1pQXdlR1EwT1dReFl6VmtJREI0WkRBd1l6VTJZV1lnTUhnd016azFaREE1WlNBd2VHSTFZMlU0WlRBeklEQjRNVFk0TlRNNU56UWdNSGcwTW1NMVltSTRaaUF3ZUdGaE5XRmpNV001SURCNE5qUmxNREE0WmpBZ01IaGhZV0ppWldNNE15QXdlRE0yWmpsbFlXRmtJREI0TXpaa01qUXhOREVnTUhoalpUWTJaakpsWVNBd2VEUmtORGcyWlRrNUlEQjROams1WWpKaE1UZ2dNSGd5TlRobU56TmhZeUF3ZURVd1pUSTJNamRsSURCNE9USTFNR0k0TlRZZ01IZ3lPR1ZsTWpSa01DQXdlR00zWXpSbE9EWmhJREI0TVdJMk4yRTBNbVVnTUhnM1lqZ3pNams1T0NBd2VHVXdaR0V3TnpCbElEQjRaR0ZtTlRaalpETWdNSGhpT1Rjd09UTm1NeUF3ZUdJMk1UWXdNMll5SURCNFlUTm1aV015TldZZ01IZ3pNV0ZsWTJWbU1TQXdlRGhoTW1NMk56aGtJREI0T0RKbFltWm1PVElnTUhnNU1EVTFPVE5qT1NBd2VESTVOVGRpWldNNUlEQjROamcyWVRNeVlUWWdMeThnYldWMGFHOWtJQ0p6ZEhKcGJtZFNaV0ZrYjI1c2VTaHpkSEpwYm1jcGMzUnlhVzVuSWl3Z2JXVjBhRzlrSUNKemRISnBibWNvYzNSeWFXNW5LWE4wY21sdVp5SXNJRzFsZEdodlpDQWljM1J5YVc1blFYSnlZWGtvYzNSeWFXNW5XMTBwYzNSeWFXNW5XMTBpTENCdFpYUm9iMlFnSW1KNWRHVnpLR0o1ZEdWYlhTbGllWFJsVzEwaUxDQnRaWFJvYjJRZ0ltSnZiMnhsWVc0b1ltOXZiQ2xpYjI5c0lpd2diV1YwYUc5a0lDSmlhV2QxYVc1MEtIVnBiblExTVRJcGRXbHVkRFV4TWlJc0lHMWxkR2h2WkNBaWMzUnlkV04wS0NoaFpHUnlaWE56TEhWcGJuUXlOVFlwS1NoaFpHUnlaWE56TEhWcGJuUXlOVFlwSWl3Z2JXVjBhRzlrSUNKcGJtNWxjbE4wY25WamRDZ29kV2x1ZERZMExDaGhaR1J5WlhOekxIVnBiblF5TlRZcEtTa29kV2x1ZERZMExDaGhaR1J5WlhOekxIVnBiblF5TlRZcEtTSXNJRzFsZEdodlpDQWlVR0Y1YldWdWRGUjRiaWh3WVhrcFlubDBaVnRkSWl3Z2JXVjBhRzlrSUNKQmNIQnNhV05oZEdsdmJrTmhiR3hVZUc0b1lYQndiQ2xpZVhSbFcxMGlMQ0J0WlhSb2IyUWdJa0Z6YzJWMFZISmhibk5tWlhKVWVHNG9ZWGhtWlhJcFlubDBaVnRkSWl3Z2JXVjBhRzlrSUNKTFpYbFNaV2RwYzNSeVlYUnBiMjVVZUc0b2EyVjVjbVZuS1dKNWRHVmJYU0lzSUcxbGRHaHZaQ0FpVkhKaGJuTmhZM1JwYjI0b2RIaHVLV0o1ZEdWYlhTSXNJRzFsZEdodlpDQWlZV05qYjNWdWRDaGhaR1J5WlhOektXRmtaSEpsYzNNaUxDQnRaWFJvYjJRZ0ltRnpjMlYwS0hWcGJuUTJOQ2wxYVc1ME5qUWlMQ0J0WlhSb2IyUWdJbUZ3Y0d4cFkyRjBhVzl1S0hWcGJuUTJOQ2wxYVc1ME5qUWlMQ0J0WlhSb2IyUWdJbUZqWTI5MWJuUkpibVJsZUdWa0tHRmpZMjkxYm5RcFlXUmtjbVZ6Y3lJc0lHMWxkR2h2WkNBaVlYTnpaWFJKYm1SbGVHVmtLR0Z6YzJWMEtYVnBiblEyTkNJc0lHMWxkR2h2WkNBaVlYQndiR2xqWVhScGIyNUpibVJsZUdWa0tHRndjR3hwWTJGMGFXOXVLWFZwYm5RMk5DSXNJRzFsZEdodlpDQWlZWEpqTkZOMFlYUnBZMEo1ZEdWek9DaGllWFJsV3poZEtXSjVkR1ZiT0YwaUxDQnRaWFJvYjJRZ0ltRnlZelJUZEdGMGFXTkNlWFJsY3pNeUtHSjVkR1ZiTXpKZEtXSjVkR1ZiTXpKZElpd2diV1YwYUc5a0lDSmhjbU0wVTNSaGRHbGpRbmwwWlhNeE1ESXdLR0o1ZEdWYk1UQXlNRjBwWW5sMFpWc3hNREl3WFNJc0lHMWxkR2h2WkNBaVlYSmpORVI1Ym1GdGFXTkNlWFJsY3loaWVYUmxXMTBwWW5sMFpWdGRJaXdnYldWMGFHOWtJQ0poY21NMFFXUmtjbVZ6Y3loaFpHUnlaWE56S1dGa1pISmxjM01pTENCdFpYUm9iMlFnSW1GeVl6UkNiMjlzS0dKdmIyd3BZbTl2YkNJc0lHMWxkR2h2WkNBaVlYSmpORUo1ZEdVb1lubDBaU2xpZVhSbElpd2diV1YwYUc5a0lDSmhjbU0wUkhsdVlXMXBZMEZ5Y21GNVQyWlRkSEoxWTNRb0tHRmtaSEpsYzNNc2RXbHVkREkxTmlsYlhTa29ZV1JrY21WemN5eDFhVzUwTWpVMktWdGRJaXdnYldWMGFHOWtJQ0poY21NMFUzUmhkR2xqUVhKeVlYbFBaakpDZVhSbGN5aGllWFJsV3pKZEtXSjVkR1ZiTWwwaUxDQnRaWFJvYjJRZ0ltRnlZelJUZEdGMGFXTkJjbkpoZVU5bU1sTjBjblZqZEhNb0tHRmtaSEpsYzNNc2RXbHVkREkxTmlsYk1sMHBLR0ZrWkhKbGMzTXNkV2x1ZERJMU5pbGJNbDBpTENCdFpYUm9iMlFnSW1GeVl6UlZSbWw0WldRNGVERTJLSFZtYVhobFpEaDRNVFlwZFdacGVHVmtPSGd4TmlJc0lHMWxkR2h2WkNBaVlYSmpORlZHYVhobFpEVXhNbmd4TmpBb2RXWnBlR1ZrTlRFeWVERTJNQ2wxWm1sNFpXUTFNVEo0TVRZd0lpd2diV1YwYUc5a0lDSmhjbU0wVldsdWRFNDRLSFZwYm5RNEtYVnBiblE0SWl3Z2JXVjBhRzlrSUNKaGNtTTBWV2x1ZEU0MU1USW9kV2x1ZERVeE1pbDFhVzUwTlRFeUlpd2diV1YwYUc5a0lDSmhjbU0wVldsdWRFNHhNamhCYkdsaGN5aDFhVzUwTVRJNEtYVnBiblF4TWpnaUxDQnRaWFJvYjJRZ0ltRnlZelJWYVc1MFRqRTJRV3hwWVhNb2RXbHVkREUyS1hWcGJuUXhOaUlzSUcxbGRHaHZaQ0FpWVhKak5GVnBiblJPTWpVMlFXeHBZWE1vZFdsdWRESTFOaWwxYVc1ME1qVTJJaXdnYldWMGFHOWtJQ0poY21NMFZXbHVkRTQyTkVGc2FXRnpLSFZwYm5RMk5DbDFhVzUwTmpRaUxDQnRaWFJvYjJRZ0ltRnlZelJWYVc1MFRqaEJiR2xoY3loMWFXNTBPQ2wxYVc1ME9DSXNJRzFsZEdodlpDQWlZWEpqTkZWcGJuUk9PREpVZFhCc1pTZ29kV2x1ZERnc2RXbHVkRGdwS1NoMWFXNTBPQ3gxYVc1ME9Da2lMQ0J0WlhSb2IyUWdJbUZ5WXpSVmFXNTBUamd6VkhWd2JHVW9LSFZwYm5RNExIVnBiblE0TEhWcGJuUTRLU2tvZFdsdWREZ3NkV2x1ZERnc2RXbHVkRGdwSWl3Z2JXVjBhRzlrSUNKaGNtTTBRMjl0Y0d4bGVGUjFjR3hsS0NoaFpHUnlaWE56TENoMWFXNTBNalUyTEhWcGJuUXlOVFlwTENoMWFXNTBNalUyTEhWcGJuUXlOVFlwTENoMWFXNTBNalUyTEhWcGJuUXlOVFlwS1Nrb1lXUmtjbVZ6Y3l3b2RXbHVkREkxTml4MWFXNTBNalUyS1N3b2RXbHVkREkxTml4MWFXNTBNalUyS1N3b2RXbHVkREkxTml4MWFXNTBNalUyS1NraUxDQnRaWFJvYjJRZ0luVnBiblEyTkNoMWFXNTBOalFwZFdsdWREWTBJaXdnYldWMGFHOWtJQ0oxYVc1ME5qUkJjbkpoZVNoMWFXNTBOalJiWFNsMWFXNTBOalJiWFNJc0lHMWxkR2h2WkNBaVltOXZiR1ZoYmtGeWNtRjVLR0p2YjJ4YlhTbGliMjlzVzEwaUxDQnRaWFJvYjJRZ0ltRnlZelJUZEhJb2MzUnlhVzVuS1hOMGNtbHVaeUlzSUcxbGRHaHZaQ0FpWVhKak5GUjFjR3hsS0NoMWFXNTBOalFzYzNSeWFXNW5LU2tvZFdsdWREWTBMSE4wY21sdVp5a2lMQ0J0WlhSb2IyUWdJbTVoZEdsMlpWUjFjR3hsS0NoMWFXNTBOalFzYzNSeWFXNW5MR0p2YjJ3cEtTaDFhVzUwTmpRc2MzUnlhVzVuTEdKdmIyd3BJaXdnYldWMGFHOWtJQ0ptYVhobFpFRnljbUY1VldsdWREWTBLSFZwYm5RMk5Gc3pYU2wxYVc1ME5qUmJNMTBpQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF3Q2lBZ0lDQnRZWFJqYUNCemRISnBibWRTWldGa2IyNXNlU0J6ZEhKcGJtY2djM1J5YVc1blFYSnlZWGtnWW5sMFpYTWdZbTl2YkdWaGJpQmlhV2QxYVc1MElITjBjblZqZENCcGJtNWxjbE4wY25WamRDQlFZWGx0Wlc1MFZIaHVJRUZ3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaUJCYzNObGRGUnlZVzV6Wm1WeVZIaHVJRXRsZVZKbFoybHpkSEpoZEdsdmJsUjRiaUJVY21GdWMyRmpkR2x2YmlCaFkyTnZkVzUwSUdGemMyVjBJR0Z3Y0d4cFkyRjBhVzl1SUdGalkyOTFiblJKYm1SbGVHVmtJR0Z6YzJWMFNXNWtaWGhsWkNCaGNIQnNhV05oZEdsdmJrbHVaR1Y0WldRZ1lYSmpORk4wWVhScFkwSjVkR1Z6T0NCaGNtTTBVM1JoZEdsalFubDBaWE16TWlCaGNtTTBVM1JoZEdsalFubDBaWE14TURJd0lHRnlZelJFZVc1aGJXbGpRbmwwWlhNZ1lYSmpORUZrWkhKbGMzTWdZWEpqTkVKdmIyd2dZWEpqTkVKNWRHVWdZWEpqTkVSNWJtRnRhV05CY25KaGVVOW1VM1J5ZFdOMElHRnlZelJUZEdGMGFXTkJjbkpoZVU5bU1rSjVkR1Z6SUdGeVl6UlRkR0YwYVdOQmNuSmhlVTltTWxOMGNuVmpkSE1nWVhKak5GVkdhWGhsWkRoNE1UWWdZWEpqTkZWR2FYaGxaRFV4TW5neE5qQWdZWEpqTkZWcGJuUk9PQ0JoY21NMFZXbHVkRTQxTVRJZ1lYSmpORlZwYm5ST01USTRRV3hwWVhNZ1lYSmpORlZwYm5ST01UWkJiR2xoY3lCaGNtTTBWV2x1ZEU0eU5UWkJiR2xoY3lCaGNtTTBWV2x1ZEU0Mk5FRnNhV0Z6SUdGeVl6UlZhVzUwVGpoQmJHbGhjeUJoY21NMFZXbHVkRTQ0TWxSMWNHeGxJR0Z5WXpSVmFXNTBUamd6VkhWd2JHVWdZWEpqTkVOdmJYQnNaWGhVZFhCc1pTQjFhVzUwTmpRZ2RXbHVkRFkwUVhKeVlYa2dZbTl2YkdWaGJrRnljbUY1SUdGeVl6UlRkSElnWVhKak5GUjFjR3hsSUc1aGRHbDJaVlIxY0d4bElHWnBlR1ZrUVhKeVlYbFZhVzUwTmpRS0lDQWdJR1Z5Y2dvS2JXRnBibDlmWDJGc1oyOTBjMTlmTG1SbFptRjFiSFJEY21WaGRHVkFOVFU2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNamNLSUNBZ0lDOHZJR1Y0Y0c5eWRDQmpiR0Z6Y3lCQmRtMVVlWEJsY3lCbGVIUmxibVJ6SUVOdmJuUnlZV04wSUhzS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNBaENpQWdJQ0FtSmdvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCdGRYTjBJR0psSUU1dlQzQWdKaVlnWTJGdUlHOXViSGtnWTJGc2JDQjNhR1Z1SUdOeVpXRjBhVzVuQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJkbTFVZVhCbGN5NXpkSEpwYm1kU1pXRmtiMjVzZVZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNuTjBjbWx1WjFKbFlXUnZibXg1T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakk0Q2lBZ0lDQXZMeUJBWVdKcGJXVjBhRzlrS0hzZ2NtVmhaRzl1YkhrNklIUnlkV1VnZlNrS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdhVzUwWTE4eUlDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQmthV2NnTVFvZ0lDQWdiR1Z1Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG1SNWJtRnRhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPRDRLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3pNQW9nSUNBZ0x5OGdaVzFwZENnbmMzUnlhVzVuVW1WaFpHOXViSGtuTENCa1lYUmhLUW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRORFkwWW1ObE56Z3dNREF5Q2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95T0FvZ0lDQWdMeThnUUdGaWFXMWxkR2h2WkNoN0lISmxZV1J2Ym14NU9pQjBjblZsSUgwcENpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTV6ZEhKcGJtZGJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3B6ZEhKcGJtYzZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16TUtJQ0FnSUM4dklIQjFZbXhwWXlCemRISnBibWNvWkdGMFlUb2djM1J5YVc1bktUb2djM1J5YVc1bklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYVc1MFkxOHlJQzh2SURBS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJR0Z5Y21GNUlHeGxibWQwYUNCb1pXRmtaWElLSUNBZ0lHbHVkR05mTVNBdkx5QXlDaUFnSUNBckNpQWdJQ0JrYVdjZ01Rb2dJQ0FnYkdWdUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExtUjVibUZ0YVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0Q0S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TkFvZ0lDQWdMeThnWlcxcGRDZ25jM1J5YVc1bkp5d2daR0YwWVNrS0lDQWdJSEIxYzJoaWVYUmxjeUF3ZURRMFpUYzVNR1V6TURBd01nb2dJQ0FnWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16TUtJQ0FnSUM4dklIQjFZbXhwWXlCemRISnBibWNvWkdGMFlUb2djM1J5YVc1bktUb2djM1J5YVc1bklIc0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T2tGMmJWUjVjR1Z6TG5OMGNtbHVaMEZ5Y21GNVczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tjM1J5YVc1blFYSnlZWGs2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNemNLSUNBZ0lDOHZJSEIxWW14cFl5QnpkSEpwYm1kQmNuSmhlU2hrWVhSaE9pQnpkSEpwYm1kYlhTazZJSE4wY21sdVoxdGRJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjRzRnTWdvZ0lDQWdhVzUwWTE4eUlDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUNvS0lDQWdJSE4zWVhBS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmxlSFJ5WVdOMElESWdNQW9nSUNBZ2FXNTBZMTh5SUM4dklEQUtDbk4wY21sdVowRnljbUY1WDJadmNsOW9aV0ZrWlhKQU1Ub0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvek53b2dJQ0FnTHk4Z2NIVmliR2xqSUhOMGNtbHVaMEZ5Y21GNUtHUmhkR0U2SUhOMGNtbHVaMXRkS1RvZ2MzUnlhVzVuVzEwZ2V3b2dJQ0FnWkhWd0NpQWdJQ0JrYVdjZ05Rb2dJQ0FnUEFvZ0lDQWdZbm9nYzNSeWFXNW5RWEp5WVhsZllXWjBaWEpmWm05eVFEUUtJQ0FnSUdSMWNHNGdNZ29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUNvS0lDQWdJR1JwWnlBekNpQWdJQ0JrZFhBS0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnWlc1amIyUnBibWNLSUNBZ0lHUjFjQW9nSUNBZ1pHbG5JRGNLSUNBZ0lHUjFjQW9nSUNBZ1kyOTJaWElnTkFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lIUmhhV3dnY0c5cGJuUmxjaUJtYjNJZ0tHeGxiaXNvYkdWdUszVjBaamhiWFNsYlhTa0tJQ0FnSUdScFp5QXhDaUFnSUNCc1pXNEtJQ0FnSUhOMVluTjBjbWx1WnpNS0lDQWdJR2x1ZEdOZk1pQXZMeUF3Q2lBZ0lDQmxlSFJ5WVdOMFgzVnBiblF4TmlBdkx5QnZiaUJsY25KdmNqb2dhVzUyWVd4cFpDQmhjbkpoZVNCc1pXNW5kR2dnYUdWaFpHVnlDaUFnSUNCcGJuUmpYekVnTHk4Z01nb2dJQ0FnS3dvZ0lDQWdLd29nSUNBZ1luVnllU0ExQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ0t3b2dJQ0FnWW5WeWVTQXhDaUFnSUNCaUlITjBjbWx1WjBGeWNtRjVYMlp2Y2w5b1pXRmtaWEpBTVFvS2MzUnlhVzVuUVhKeVlYbGZZV1owWlhKZlptOXlRRFE2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNemNLSUNBZ0lDOHZJSEIxWW14cFl5QnpkSEpwYm1kQmNuSmhlU2hrWVhSaE9pQnpkSEpwYm1kYlhTazZJSE4wY21sdVoxdGRJSHNLSUNBZ0lHUnBaeUF6Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNZ29nSUNBZ0t3b2dJQ0FnWkdsbklETUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVaSGx1WVcxcFkxOWhjbkpoZVR4emRISnBibWMrQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNemdLSUNBZ0lDOHZJR1Z0YVhRb0ozTjBjbWx1WjBGeWNtRjVKeXdnWkdGMFlTa0tJQ0FnSUhCMWMyaGllWFJsY3lBd2VEZGtNbUUwTkdKbU1EQXdNZ29nSUNBZ1pHbG5JRFlLSUNBZ0lHUjFjQW9nSUNBZ1kyOTJaWElnTWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TndvZ0lDQWdMeThnY0hWaWJHbGpJSE4wY21sdVowRnljbUY1S0dSaGRHRTZJSE4wY21sdVoxdGRLVG9nYzNSeWFXNW5XMTBnZXdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZRWFp0Vkhsd1pYTXVZbmwwWlhOYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGllWFJsY3pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTVFvZ0lDQWdMeThnY0hWaWJHbGpJR0o1ZEdWektHUmhkR0U2SUdKNWRHVnpLVG9nWW5sMFpYTWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCcGJuUmpYeklnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QXhDaUFnSUNCc1pXNEtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVaSGx1WVcxcFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNFBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pReUNpQWdJQ0F2THlCbGJXbDBLQ2RpZVhSbGN5Y3NJR1JoZEdFcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhoak16TmtOR0V4WXpBd01ESUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUXhDaUFnSUNBdkx5QndkV0pzYVdNZ1lubDBaWE1vWkdGMFlUb2dZbmwwWlhNcE9pQmllWFJsY3lCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVpYjI5c1pXRnVXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWW05dmJHVmhiam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzBOUW9nSUNBZ0x5OGdjSFZpYkdsaklHSnZiMnhsWVc0b1pHRjBZVG9nWW05dmJHVmhiaWs2SUdKdmIyeGxZVzRnZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbUp2YjJ3S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTmdvZ0lDQWdMeThnWlcxcGRDZ25ZbTl2YkdWaGJpY3NJR1JoZEdFcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnME5HSmpNemsxWmlBdkx5QnRaWFJvYjJRZ0ltSnZiMnhsWVc0b1ltOXZiQ2tpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTlFvZ0lDQWdMeThnY0hWaWJHbGpJR0p2YjJ4bFlXNG9aR0YwWVRvZ1ltOXZiR1ZoYmlrNklHSnZiMnhsWVc0Z2V3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WW1sbmRXbHVkRnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21KcFozVnBiblE2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZORGtLSUNBZ0lDOHZJSEIxWW14cFl5QmlhV2QxYVc1MEtHUmhkR0U2SUdKcFozVnBiblFwT2lCaWFXZDFhVzUwSUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQmtkWEFLSUNBZ0lIQjFjMmhwYm5RZ05qUWdMeThnTmpRS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkRFV4TWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalV3Q2lBZ0lDQXZMeUJsYldsMEtDZGlhV2QxYVc1MEp5d2daR0YwWVNrS0lDQWdJSEIxYzJocGJuUWdOalFnTHk4Z05qUUtJQ0FnSUR3OUNpQWdJQ0JoYzNObGNuUWdMeThnYjNabGNtWnNiM2NLSUNBZ0lIQjFjMmhwYm5RZ05qUWdMeThnTmpRS0lDQWdJR0o2WlhKdkNpQWdJQ0JpZkFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TnpSaE5Ua3dZemNnTHk4Z2JXVjBhRzlrSUNKaWFXZDFhVzUwS0hWcGJuUTFNVElwSWdvZ0lDQWdaR2xuSURFS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TkRrS0lDQWdJQzh2SUhCMVlteHBZeUJpYVdkMWFXNTBLR1JoZEdFNklHSnBaM1ZwYm5RcE9pQmlhV2QxYVc1MElIc0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T2tGMmJWUjVjR1Z6TG5OMGNuVmpkRnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q25OMGNuVmpkRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFNd29nSUNBZ0x5OGdjSFZpYkdsaklITjBjblZqZENoa1lYUmhPaUJ6ZEhKMVkzUkJaR1J5WlhOelZXbHVkREkxTmlrNklITjBjblZqZEVGa1pISmxjM05WYVc1ME1qVTJJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCd2RYTm9hVzUwSURZMElDOHZJRFkwQ2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQnpkSEoxWTNSQlpHUnlaWE56VldsdWRESTFOZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qVTBDaUFnSUNBdkx5QmxiV2wwS0NkemRISjFZM1FuTENCa1lYUmhLUW9nSUNBZ2NIVnphR0o1ZEdWeklEQjROekppT1RJNFptSWdMeThnYldWMGFHOWtJQ0p6ZEhKMVkzUW9LR0ZrWkhKbGMzTXNkV2x1ZERJMU5pa3BJZ29nSUNBZ1pHbG5JREVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOVE1LSUNBZ0lDOHZJSEIxWW14cFl5QnpkSEoxWTNRb1pHRjBZVG9nYzNSeWRXTjBRV1JrY21WemMxVnBiblF5TlRZcE9pQnpkSEoxWTNSQlpHUnlaWE56VldsdWRESTFOaUI3Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJkbTFVZVhCbGN5NXBibTVsY2xOMGNuVmpkRnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21sdWJtVnlVM1J5ZFdOME9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pVM0NpQWdJQ0F2THlCd2RXSnNhV01nYVc1dVpYSlRkSEoxWTNRb1pHRjBZVG9nYVc1dVpYSlRkSEoxWTNRcE9pQnBibTVsY2xOMGNuVmpkQ0I3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2NIVnphR2x1ZENBM01pQXZMeUEzTWdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdhVzV1WlhKVGRISjFZM1FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFPQW9nSUNBZ0x5OGdaVzFwZENnbmFXNXVaWEpUZEhKMVkzUW5MQ0JrWVhSaEtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE1qUTVNMkkwTVRnZ0x5OGdiV1YwYUc5a0lDSnBibTVsY2xOMGNuVmpkQ2dvZFdsdWREWTBMQ2hoWkdSeVpYTnpMSFZwYm5ReU5UWXBLU2tpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8xTndvZ0lDQWdMeThnY0hWaWJHbGpJR2x1Ym1WeVUzUnlkV04wS0dSaGRHRTZJR2x1Ym1WeVUzUnlkV04wS1RvZ2FXNXVaWEpUZEhKMVkzUWdld29nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlFYWnRWSGx3WlhNdVVHRjViV1Z1ZEZSNGJsdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbEJoZVcxbGJuUlVlRzQ2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOakVLSUNBZ0lDOHZJSEIxWW14cFl5QlFZWGx0Wlc1MFZIaHVLR1JoZEdFNklHZDBlRzR1VUdGNWJXVnVkRlI0YmlrNklHSjVkR1Z6SUhzS0lDQWdJSFI0YmlCSGNtOTFjRWx1WkdWNENpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdMUW9nSUNBZ1pIVndDaUFnSUNCbmRIaHVjeUJVZVhCbFJXNTFiUW9nSUNBZ2FXNTBZMTh3SUM4dklIQmhlUW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCMGNtRnVjMkZqZEdsdmJpQjBlWEJsSUdseklIQmhlUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qWXlDaUFnSUNBdkx5QmxiV2wwS0NkUVlYbHRaVzUwVkhodUp5d2daR0YwWVM1MGVHNUpaQ2tLSUNBZ0lHZDBlRzV6SUZSNFNVUUtJQ0FnSUhCMWMyaGllWFJsY3lBd2VHSTVZV1l4TTJFMklDOHZJRzFsZEdodlpDQWlVR0Y1YldWdWRGUjRiaWhpZVhSbFd6TXlYU2tpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8yTVFvZ0lDQWdMeThnY0hWaWJHbGpJRkJoZVcxbGJuUlVlRzRvWkdGMFlUb2daM1I0Ymk1UVlYbHRaVzUwVkhodUtUb2dZbmwwWlhNZ2V3b2dJQ0FnWW5sMFpXTmZNU0F2THlBd2VERTFNV1kzWXpjMU1EQXlNQW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRjJiVlI1Y0dWekxrRndjR3hwWTJGMGFXOXVRMkZzYkZSNGJsdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDa0Z3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzJOUW9nSUNBZ0x5OGdjSFZpYkdsaklFRndjR3hwWTJGMGFXOXVRMkZzYkZSNGJpaGtZWFJoT2lCbmRIaHVMa0Z3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaWs2SUdKNWRHVnpJSHNLSUNBZ0lIUjRiaUJIY205MWNFbHVaR1Y0Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ0xRb2dJQ0FnWkhWd0NpQWdJQ0JuZEhodWN5QlVlWEJsUlc1MWJRb2dJQ0FnY0hWemFHbHVkQ0EySUM4dklHRndjR3dLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2RISmhibk5oWTNScGIyNGdkSGx3WlNCcGN5QmhjSEJzQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOallLSUNBZ0lDOHZJR1Z0YVhRb0owRndjR3hwWTJGMGFXOXVRMkZzYkZSNGJpY3NJR1JoZEdFdWRIaHVTV1FwQ2lBZ0lDQm5kSGh1Y3lCVWVFbEVDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IaGlPV1pqT0RZNE15QXZMeUJ0WlhSb2IyUWdJa0Z3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaWhpZVhSbFd6TXlYU2tpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8yTlFvZ0lDQWdMeThnY0hWaWJHbGpJRUZ3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaWhrWVhSaE9pQm5kSGh1TGtGd2NHeHBZMkYwYVc5dVEyRnNiRlI0YmlrNklHSjVkR1Z6SUhzS0lDQWdJR0o1ZEdWalh6RWdMeThnTUhneE5URm1OMk0zTlRBd01qQUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwQmRtMVVlWEJsY3k1QmMzTmxkRlJ5WVc1elptVnlWSGh1VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1FYTnpaWFJVY21GdWMyWmxjbFI0YmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8yT1FvZ0lDQWdMeThnY0hWaWJHbGpJRUZ6YzJWMFZISmhibk5tWlhKVWVHNG9aR0YwWVRvZ1ozUjRiaTVCYzNObGRGUnlZVzV6Wm1WeVZIaHVLVG9nWW5sMFpYTWdld29nSUNBZ2RIaHVJRWR5YjNWd1NXNWtaWGdLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNBdENpQWdJQ0JrZFhBS0lDQWdJR2QwZUc1eklGUjVjR1ZGYm5WdENpQWdJQ0J3ZFhOb2FXNTBJRFFnTHk4Z1lYaG1aWElLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2RISmhibk5oWTNScGIyNGdkSGx3WlNCcGN5QmhlR1psY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPamN3Q2lBZ0lDQXZMeUJsYldsMEtDZEJjM05sZEZSeVlXNXpabVZ5VkhodUp5d2daR0YwWVM1MGVHNUpaQ2tLSUNBZ0lHZDBlRzV6SUZSNFNVUUtJQ0FnSUhCMWMyaGllWFJsY3lBd2VETTFaRGMwWmpOaUlDOHZJRzFsZEdodlpDQWlRWE56WlhSVWNtRnVjMlpsY2xSNGJpaGllWFJsV3pNeVhTa2lDaUFnSUNCa2FXY2dNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzJPUW9nSUNBZ0x5OGdjSFZpYkdsaklFRnpjMlYwVkhKaGJuTm1aWEpVZUc0b1pHRjBZVG9nWjNSNGJpNUJjM05sZEZSeVlXNXpabVZ5VkhodUtUb2dZbmwwWlhNZ2V3b2dJQ0FnWW5sMFpXTmZNU0F2THlBd2VERTFNV1kzWXpjMU1EQXlNQW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRjJiVlI1Y0dWekxrdGxlVkpsWjJsemRISmhkR2x2YmxSNGJsdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDa3RsZVZKbFoybHpkSEpoZEdsdmJsUjRiam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNNd29nSUNBZ0x5OGdjSFZpYkdsaklFdGxlVkpsWjJsemRISmhkR2x2YmxSNGJpaGtZWFJoT2lCbmRIaHVMa3RsZVZKbFoybHpkSEpoZEdsdmJsUjRiaWs2SUdKNWRHVnpJSHNLSUNBZ0lIUjRiaUJIY205MWNFbHVaR1Y0Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ0xRb2dJQ0FnWkhWd0NpQWdJQ0JuZEhodWN5QlVlWEJsUlc1MWJRb2dJQ0FnYVc1MFkxOHhJQzh2SUd0bGVYSmxad29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCMGNtRnVjMkZqZEdsdmJpQjBlWEJsSUdseklHdGxlWEpsWndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPamMwQ2lBZ0lDQXZMeUJsYldsMEtDZExaWGxTWldkcGMzUnlZWFJwYjI1VWVHNG5MQ0JrWVhSaExuUjRia2xrS1FvZ0lDQWdaM1I0Ym5NZ1ZIaEpSQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRZbVk1TURobU5XRWdMeThnYldWMGFHOWtJQ0pMWlhsU1pXZHBjM1J5WVhScGIyNVVlRzRvWW5sMFpWc3pNbDBwSWdvZ0lDQWdaR2xuSURFS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TnpNS0lDQWdJQzh2SUhCMVlteHBZeUJMWlhsU1pXZHBjM1J5WVhScGIyNVVlRzRvWkdGMFlUb2daM1I0Ymk1TFpYbFNaV2RwYzNSeVlYUnBiMjVVZUc0cE9pQmllWFJsY3lCN0NpQWdJQ0JpZVhSbFkxOHhJQzh2SURCNE1UVXhaamRqTnpVd01ESXdDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlFYWnRWSGx3WlhNdVZISmhibk5oWTNScGIyNWJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3BVY21GdWMyRmpkR2x2YmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8zTndvZ0lDQWdMeThnY0hWaWJHbGpJRlJ5WVc1ellXTjBhVzl1S0dSaGRHRTZJR2QwZUc0dVZISmhibk5oWTNScGIyNHBPaUJpZVhSbGN5QjdDaUFnSUNCMGVHNGdSM0p2ZFhCSmJtUmxlQW9nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUMwS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8zT0FvZ0lDQWdMeThnWlcxcGRDZ25WSEpoYm5OaFkzUnBiMjRuTENCa1lYUmhMblI0Ymtsa0tRb2dJQ0FnWjNSNGJuTWdWSGhKUkFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0WmprelpUQTJPVGtnTHk4Z2JXVjBhRzlrSUNKVWNtRnVjMkZqZEdsdmJpaGllWFJsV3pNeVhTa2lDaUFnSUNCa2FXY2dNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNOd29nSUNBZ0x5OGdjSFZpYkdsaklGUnlZVzV6WVdOMGFXOXVLR1JoZEdFNklHZDBlRzR1VkhKaGJuTmhZM1JwYjI0cE9pQmllWFJsY3lCN0NpQWdJQ0JpZVhSbFkxOHhJQzh2SURCNE1UVXhaamRqTnpVd01ESXdDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlFYWnRWSGx3WlhNdVlXTmpiM1Z1ZEZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRmpZMjkxYm5RNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T0RFS0lDQWdJQzh2SUhCMVlteHBZeUJoWTJOdmRXNTBLR1JoZEdFNklFRmpZMjkxYm5RcE9pQkJZMk52ZFc1MElIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0J3ZFhOb2FXNTBJRE15SUM4dklETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbk4wWVhScFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNExDQXpNajRLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzRNZ29nSUNBZ0x5OGdaVzFwZENnbllXTmpiM1Z1ZENjc0lHUmhkR0V1WW5sMFpYTXBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IaGtPV1JqWXpCbU1TQXZMeUJ0WlhSb2IyUWdJbUZqWTI5MWJuUW9ZbmwwWlZzek1sMHBJZ29nSUNBZ1pHbG5JREVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPREVLSUNBZ0lDOHZJSEIxWW14cFl5QmhZMk52ZFc1MEtHUmhkR0U2SUVGalkyOTFiblFwT2lCQlkyTnZkVzUwSUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0YyYlZSNWNHVnpMbUZ6YzJWMFczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWE56WlhRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T0RVS0lDQWdJQzh2SUhCMVlteHBZeUJoYzNObGRDaGtZWFJoT2lCQmMzTmxkQ2s2SUVGemMyVjBJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYek1nTHk4Z09Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwTmpRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem80TmdvZ0lDQWdMeThnWlcxcGRDZ25ZWE56WlhRbkxDQmtZWFJoTG1sa0tRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE1HRXlNMkppTlRBZ0x5OGdiV1YwYUc5a0lDSmhjM05sZENoMWFXNTBOalFwSWdvZ0lDQWdaR2xuSURFS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T0RVS0lDQWdJQzh2SUhCMVlteHBZeUJoYzNObGRDaGtZWFJoT2lCQmMzTmxkQ2s2SUVGemMyVjBJSHNLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRjJiVlI1Y0dWekxtRndjR3hwWTJGMGFXOXVXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhCd2JHbGpZWFJwYjI0NkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T0RrS0lDQWdJQzh2SUhCMVlteHBZeUJoY0hCc2FXTmhkR2x2Ymloa1lYUmhPaUJCY0hCc2FXTmhkR2x2YmlrNklFRndjR3hwWTJGMGFXOXVJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYek1nTHk4Z09Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwTmpRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem81TUFvZ0lDQWdMeThnWlcxcGRDZ25ZWEJ3YkdsallYUnBiMjRuTENCa1lYUmhMbWxrS1FvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0WTJFek0yTTJaVEFnTHk4Z2JXVjBhRzlrSUNKaGNIQnNhV05oZEdsdmJpaDFhVzUwTmpRcElnb2dJQ0FnWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9Ea0tJQ0FnSUM4dklIQjFZbXhwWXlCaGNIQnNhV05oZEdsdmJpaGtZWFJoT2lCQmNIQnNhV05oZEdsdmJpazZJRUZ3Y0d4cFkyRjBhVzl1SUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0YyYlZSNWNHVnpMbUZqWTI5MWJuUkpibVJsZUdWa1czSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZV05qYjNWdWRFbHVaR1Y0WldRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T1RVS0lDQWdJQzh2SUVCaFltbHRaWFJvYjJRb2V5QnlaWE52ZFhKalpVVnVZMjlrYVc1bk9pQW5hVzVrWlhnbklIMHBDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkRGdLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUZ6SUVGalkyOTFiblJ6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPVGNLSUNBZ0lDOHZJR1Z0YVhRb0oyRmpZMjkxYm5SSmJtUmxlR1ZrSnl3Z1pHRjBZUzVpZVhSbGN5a0tJQ0FnSUhCMWMyaGllWFJsY3lBd2VHRXhNVEkxTTJRM0lDOHZJRzFsZEdodlpDQWlZV05qYjNWdWRFbHVaR1Y0WldRb1lubDBaVnN6TWwwcElnb2dJQ0FnWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9UVUtJQ0FnSUM4dklFQmhZbWx0WlhSb2IyUW9leUJ5WlhOdmRYSmpaVVZ1WTI5a2FXNW5PaUFuYVc1a1pYZ25JSDBwQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJkbTFVZVhCbGN5NWhjM05sZEVsdVpHVjRaV1JiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGMzTmxkRWx1WkdWNFpXUTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UQXdDaUFnSUNBdkx5QkFZV0pwYldWMGFHOWtLSHNnY21WemIzVnlZMlZGYm1OdlpHbHVaem9nSjJsdVpHVjRKeUI5S1FvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RNENpQWdJQ0JpZEc5cENpQWdJQ0IwZUc1aGN5QkJjM05sZEhNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TURJS0lDQWdJQzh2SUdWdGFYUW9KMkZ6YzJWMFNXNWtaWGhsWkNjc0lHUmhkR0V1YVdRcENpQWdJQ0JwZEc5aUNpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnM01qQmtNVGd5TnlBdkx5QnRaWFJvYjJRZ0ltRnpjMlYwU1c1a1pYaGxaQ2gxYVc1ME5qUXBJZ29nSUNBZ1pHbG5JREVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVEF3Q2lBZ0lDQXZMeUJBWVdKcGJXVjBhRzlrS0hzZ2NtVnpiM1Z5WTJWRmJtTnZaR2x1WnpvZ0oybHVaR1Y0SnlCOUtRb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WVhCd2JHbGpZWFJwYjI1SmJtUmxlR1ZrVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYQndiR2xqWVhScGIyNUpibVJsZUdWa09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFd05Rb2dJQ0FnTHk4Z1FHRmlhVzFsZEdodlpDaDdJSEpsYzI5MWNtTmxSVzVqYjJScGJtYzZJQ2RwYm1SbGVDY2dmU2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwT0FvZ0lDQWdZblJ2YVFvZ0lDQWdkSGh1WVhNZ1FYQndiR2xqWVhScGIyNXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UQTNDaUFnSUNBdkx5QmxiV2wwS0NkaGNIQnNhV05oZEdsdmJrbHVaR1Y0WldRbkxDQmtZWFJoTG1sa0tRb2dJQ0FnYVhSdllnb2dJQ0FnY0hWemFHSjVkR1Z6SURCNFltWTVOR1kyWXpZZ0x5OGdiV1YwYUc5a0lDSmhjSEJzYVdOaGRHbHZia2x1WkdWNFpXUW9kV2x1ZERZMEtTSUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRXdOUW9nSUNBZ0x5OGdRR0ZpYVcxbGRHaHZaQ2g3SUhKbGMyOTFjbU5sUlc1amIyUnBibWM2SUNkcGJtUmxlQ2NnZlNrS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0YyYlZSNWNHVnpMbUZ5WXpSVGRHRjBhV05DZVhSbGN6aGJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3BoY21NMFUzUmhkR2xqUW5sMFpYTTRPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRXhOQW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZelJUZEdGMGFXTkNlWFJsY3pnb1pHRjBZVG9nWVhKak5DNVRkR0YwYVdOQ2VYUmxjenc0UGlrNklHRnlZelF1VTNSaGRHbGpRbmwwWlhNOE9ENGdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNeUF2THlBNENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuTjBZWFJwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0TENBNFBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFeE5Rb2dJQ0FnTHk4Z1pXMXBkQ2duWVhKak5GTjBZWFJwWTBKNWRHVnpPQ2NzSUdSaGRHRXBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZzFNak5pTmpOaVlpQXZMeUJ0WlhSb2IyUWdJbUZ5WXpSVGRHRjBhV05DZVhSbGN6Z29ZbmwwWlZzNFhTa2lDaUFnSUNCa2FXY2dNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hNVFFLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU0wVTNSaGRHbGpRbmwwWlhNNEtHUmhkR0U2SUdGeVl6UXVVM1JoZEdsalFubDBaWE04T0Q0cE9pQmhjbU0wTGxOMFlYUnBZMEo1ZEdWelBEZytJSHNLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRjJiVlI1Y0dWekxtRnlZelJUZEdGMGFXTkNlWFJsY3pNeVczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTkZOMFlYUnBZMEo1ZEdWek16STZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1URTRDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpORk4wWVhScFkwSjVkR1Z6TXpJb1pHRjBZVG9nWVhKak5DNVRkR0YwYVdOQ2VYUmxjend6TWo0cE9pQmhjbU0wTGxOMFlYUnBZMEo1ZEdWelBETXlQaUI3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2NIVnphR2x1ZENBek1pQXZMeUF6TWdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1emRHRjBhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPQ3dnTXpJK0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRFNUNpQWdJQ0F2THlCbGJXbDBLQ2RoY21NMFUzUmhkR2xqUW5sMFpYTXpNaWNzSUdSaGRHRXBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZ3haREpoTVdaa05pQXZMeUJ0WlhSb2IyUWdJbUZ5WXpSVGRHRjBhV05DZVhSbGN6TXlLR0o1ZEdWYk16SmRLU0lLSUNBZ0lHUnBaeUF4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV4T0FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSVGRHRjBhV05DZVhSbGN6TXlLR1JoZEdFNklHRnlZelF1VTNSaGRHbGpRbmwwWlhNOE16SStLVG9nWVhKak5DNVRkR0YwYVdOQ2VYUmxjend6TWo0Z2V3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WVhKak5GTjBZWFJwWTBKNWRHVnpNVEF5TUZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZelJUZEdGMGFXTkNlWFJsY3pFd01qQTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1USXlDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpORk4wWVhScFkwSjVkR1Z6TVRBeU1DaGtZWFJoT2lCaGNtTTBMbE4wWVhScFkwSjVkR1Z6UERFd01qQStLVG9nWVhKak5DNVRkR0YwYVdOQ2VYUmxjend4TURJd1BpQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnY0hWemFHbHVkQ0F4TURJd0lDOHZJREV3TWpBS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWMzUmhkR2xqWDJGeWNtRjVQR0Z5WXpRdWRXbHVkRGdzSURFd01qQStDaUFnSUNCaWVYUmxZMTh3SUM4dklEQjRNVFV4Wmpkak56VUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwQmRtMVVlWEJsY3k1aGNtTTBSSGx1WVcxcFkwSjVkR1Z6VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpORVI1Ym1GdGFXTkNlWFJsY3pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TWpZS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NMFJIbHVZVzFwWTBKNWRHVnpLR1JoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWektUb2dZWEpqTkM1RWVXNWhiV2xqUW5sMFpYTWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCcGJuUmpYeklnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QXhDaUFnSUNCc1pXNEtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVaSGx1WVcxcFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNFBnb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WVhKak5FRmtaSEpsYzNOYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0wUVdSa2NtVnpjem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hNekFLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU0wUVdSa2NtVnpjeWhrWVhSaE9pQmhjbU0wTGtGa1pISmxjM01wT2lCaGNtTTBMa0ZrWkhKbGMzTWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUhCMWMyaHBiblFnTXpJZ0x5OGdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1YzNSaGRHbGpYMkZ5Y21GNVBHRnlZelF1ZFdsdWREZ3NJRE15UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV6TVFvZ0lDQWdMeThnWlcxcGRDZ25ZWEpqTkVGa1pISmxjM01uTENCa1lYUmhLUW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRaRGhtT0dJMU9HTWdMeThnYldWMGFHOWtJQ0poY21NMFFXUmtjbVZ6Y3loaFpHUnlaWE56S1NJS0lDQWdJR1JwWnlBeENpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFek1Bb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6UkJaR1J5WlhOektHUmhkR0U2SUdGeVl6UXVRV1JrY21WemN5azZJR0Z5WXpRdVFXUmtjbVZ6Y3lCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVoY21NMFFtOXZiRnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6UkNiMjlzT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV6TkFvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSQ2IyOXNLR1JoZEdFNklHRnlZelF1UW05dmJDazZJR0Z5WXpRdVFtOXZiQ0I3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVZbTl2YkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV6TlFvZ0lDQWdMeThnWlcxcGRDZ25ZWEpqTkVKdmIyd25MQ0JrWVhSaEtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE56YzFPRE15TVdNZ0x5OGdiV1YwYUc5a0lDSmhjbU0wUW05dmJDaGliMjlzS1NJS0lDQWdJR1JwWnlBeENpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFek5Bb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6UkNiMjlzS0dSaGRHRTZJR0Z5WXpRdVFtOXZiQ2s2SUdGeVl6UXVRbTl2YkNCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVoY21NMFFubDBaVnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6UkNlWFJsT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV6T0FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSQ2VYUmxLR1JoZEdFNklHRnlZelF1UW5sMFpTazZJR0Z5WXpRdVFubDBaU0I3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVkV2x1ZERnS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TXprS0lDQWdJQzh2SUdWdGFYUW9KMkZ5WXpSQ2VYUmxKeXdnWkdGMFlTa0tJQ0FnSUhCMWMyaGllWFJsY3lBd2VHRm1ZbUZoWTJVM0lDOHZJRzFsZEdodlpDQWlZWEpqTkVKNWRHVW9ZbmwwWlNraUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE16Z0tJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTTBRbmwwWlNoa1lYUmhPaUJoY21NMExrSjVkR1VwT2lCaGNtTTBMa0o1ZEdVZ2V3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WVhKak5FUjVibUZ0YVdOQmNuSmhlVTltVTNSeWRXTjBXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak5FUjVibUZ0YVdOQmNuSmhlVTltVTNSeWRXTjBPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTBNeTB4TkRVS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NMFJIbHVZVzFwWTBGeWNtRjVUMlpUZEhKMVkzUW9DaUFnSUNBdkx5QWdJR1JoZEdFNklHRnlZelF1UkhsdVlXMXBZMEZ5Y21GNVBITjBjblZqZEVGa1pISmxjM05WYVc1ME1qVTJQaXdLSUNBZ0lDOHZJQ2s2SUdGeVl6UXVSSGx1WVcxcFkwRnljbUY1UEhOMGNuVmpkRUZrWkhKbGMzTlZhVzUwTWpVMlBpQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUdsdWRHTmZNaUF2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQndkWE5vYVc1MElEWTBJQzh2SURZMENpQWdJQ0FxQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNZ29nSUNBZ0t3b2dJQ0FnWkdsbklERUtJQ0FnSUd4bGJnb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNWtlVzVoYldsalgyRnljbUY1UEhOMGNuVmpkRUZrWkhKbGMzTlZhVzUwTWpVMlBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFME5nb2dJQ0FnTHk4Z1pXMXBkQ2duWVhKak5FUjVibUZ0YVdOQmNuSmhlVTltVTNSeWRXTjBKeXdnWkdGMFlTa0tJQ0FnSUhCMWMyaGllWFJsY3lBd2VHWTNZalJrWVRZeU1EQXdNZ29nSUNBZ1pHbG5JREVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFF6TFRFME5Rb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6UkVlVzVoYldsalFYSnlZWGxQWmxOMGNuVmpkQ2dLSUNBZ0lDOHZJQ0FnWkdGMFlUb2dZWEpqTkM1RWVXNWhiV2xqUVhKeVlYazhjM1J5ZFdOMFFXUmtjbVZ6YzFWcGJuUXlOVFkrTEFvZ0lDQWdMeThnS1RvZ1lYSmpOQzVFZVc1aGJXbGpRWEp5WVhrOGMzUnlkV04wUVdSa2NtVnpjMVZwYm5ReU5UWStJSHNLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRjJiVlI1Y0dWekxtRnlZelJUZEdGMGFXTkJjbkpoZVU5bU1rSjVkR1Z6VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpORk4wWVhScFkwRnljbUY1VDJZeVFubDBaWE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFE1Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTkZOMFlYUnBZMEZ5Y21GNVQyWXlRbmwwWlhNb1pHRjBZVG9nWVhKak5DNVRkR0YwYVdOQmNuSmhlVHhoY21NMExrSjVkR1VzSURJK0tUb2dZWEpqTkM1VGRHRjBhV05CY25KaGVUeGhjbU0wTGtKNWRHVXNJREkrSUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNZ29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzV6ZEdGMGFXTmZZWEp5WVhrOFlYSmpOQzUxYVc1ME9Dd2dNajRLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hOVEFLSUNBZ0lDOHZJR1Z0YVhRb0oyRnlZelJUZEdGMGFXTkJjbkpoZVU5bU1rSjVkR1Z6Snl3Z1pHRjBZU2tLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlR0U1WkdFd05UVmlJQzh2SUcxbGRHaHZaQ0FpWVhKak5GTjBZWFJwWTBGeWNtRjVUMll5UW5sMFpYTW9ZbmwwWlZzeVhTa2lDaUFnSUNCa2FXY2dNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hORGtLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU0wVTNSaGRHbGpRWEp5WVhsUFpqSkNlWFJsY3loa1lYUmhPaUJoY21NMExsTjBZWFJwWTBGeWNtRjVQR0Z5WXpRdVFubDBaU3dnTWo0cE9pQmhjbU0wTGxOMFlYUnBZMEZ5Y21GNVBHRnlZelF1UW5sMFpTd2dNajRnZXdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZRWFp0Vkhsd1pYTXVZWEpqTkZOMFlYUnBZMEZ5Y21GNVQyWXlVM1J5ZFdOMGMxdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUZ5WXpSVGRHRjBhV05CY25KaGVVOW1NbE4wY25WamRITTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UVXpMVEUxTlFvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSVGRHRjBhV05CY25KaGVVOW1NbE4wY25WamRITW9DaUFnSUNBdkx5QWdJR1JoZEdFNklHRnlZelF1VTNSaGRHbGpRWEp5WVhrOGMzUnlkV04wUVdSa2NtVnpjMVZwYm5ReU5UWXNJREkrTEFvZ0lDQWdMeThnS1RvZ1lYSmpOQzVUZEdGMGFXTkJjbkpoZVR4emRISjFZM1JCWkdSeVpYTnpWV2x1ZERJMU5pd2dNajRnZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lIQjFjMmhwYm5RZ01USTRJQzh2SURFeU9Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNXpkR0YwYVdOZllYSnlZWGs4YzNSeWRXTjBRV1JrY21WemMxVnBiblF5TlRZc0lESStDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UVTJDaUFnSUNBdkx5QmxiV2wwS0NkaGNtTTBVM1JoZEdsalFYSnlZWGxQWmpKVGRISjFZM1J6Snl3Z1pHRjBZU2tLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlREppWW1Kak5ESTNJQzh2SUcxbGRHaHZaQ0FpWVhKak5GTjBZWFJwWTBGeWNtRjVUMll5VTNSeWRXTjBjeWdvWVdSa2NtVnpjeXgxYVc1ME1qVTJLVnN5WFNraUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE5UTXRNVFUxQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTkZOMFlYUnBZMEZ5Y21GNVQyWXlVM1J5ZFdOMGN5Z0tJQ0FnSUM4dklDQWdaR0YwWVRvZ1lYSmpOQzVUZEdGMGFXTkJjbkpoZVR4emRISjFZM1JCWkdSeVpYTnpWV2x1ZERJMU5pd2dNajRzQ2lBZ0lDQXZMeUFwT2lCaGNtTTBMbE4wWVhScFkwRnljbUY1UEhOMGNuVmpkRUZrWkhKbGMzTlZhVzUwTWpVMkxDQXlQaUI3Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJkbTFVZVhCbGN5NWhjbU0wVlVacGVHVmtPSGd4Tmx0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZelJWUm1sNFpXUTRlREUyT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakUxT1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSVlJtbDRaV1E0ZURFMktHUmhkR0U2SUdGeVl6UXVWVVpwZUdWa1BEZ3NJREUyUGlrNklHRnlZelF1VlVacGVHVmtQRGdzSURFMlBpQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXWnBlR1ZrT0hneE5nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMk1Bb2dJQ0FnTHk4Z1pXMXBkQ2duWVhKak5GVkdhWGhsWkRoNE1UWW5MQ0JrWVhSaEtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE9HRXpORFJqT0dNZ0x5OGdiV1YwYUc5a0lDSmhjbU0wVlVacGVHVmtPSGd4TmloMVptbDRaV1E0ZURFMktTSUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTFPUW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZelJWUm1sNFpXUTRlREUyS0dSaGRHRTZJR0Z5WXpRdVZVWnBlR1ZrUERnc0lERTJQaWs2SUdGeVl6UXVWVVpwZUdWa1BEZ3NJREUyUGlCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVoY21NMFZVWnBlR1ZrTlRFeWVERTJNRnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6UlZSbWw0WldRMU1USjRNVFl3T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakUyTXdvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSVlJtbDRaV1ExTVRKNE1UWXdLR1JoZEdFNklHRnlZelF1VlVacGVHVmtQRFV4TWl3Z01UWXdQaWs2SUdGeVl6UXVWVVpwZUdWa1BEVXhNaXdnTVRZd1BpQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnY0hWemFHbHVkQ0EyTkNBdkx5QTJOQW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzUxWm1sNFpXUTFNVEo0TVRZd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRZMENpQWdJQ0F2THlCbGJXbDBLQ2RoY21NMFZVWnBlR1ZrTlRFeWVERTJNQ2NzSUdSaGRHRXBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZ3lPV00yWXpOalpTQXZMeUJ0WlhSb2IyUWdJbUZ5WXpSVlJtbDRaV1ExTVRKNE1UWXdLSFZtYVhobFpEVXhNbmd4TmpBcElnb2dJQ0FnWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UWXpDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpORlZHYVhobFpEVXhNbmd4TmpBb1pHRjBZVG9nWVhKak5DNVZSbWw0WldROE5URXlMQ0F4TmpBK0tUb2dZWEpqTkM1VlJtbDRaV1E4TlRFeUxDQXhOakErSUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0YyYlZSNWNHVnpMbUZ5WXpSVmFXNTBUamhiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTBWV2x1ZEU0NE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMk53b2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6UlZhVzUwVGpnb1pHRjBZVG9nWVhKak5DNVZhVzUwUERnK0tUb2dZWEpqTkM1VmFXNTBQRGcrSUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzUxYVc1ME9Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMk9Bb2dJQ0FnTHk4Z1pXMXBkQ2duWVhKak5GVnBiblJPT0Njc0lHUmhkR0VwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGhqT1RGaU1HWTFaaUF2THlCdFpYUm9iMlFnSW1GeVl6UlZhVzUwVGpnb2RXbHVkRGdwSWdvZ0lDQWdaR2xuSURFS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRZM0NpQWdJQ0F2THlCd2RXSnNhV01nWVhKak5GVnBiblJPT0Noa1lYUmhPaUJoY21NMExsVnBiblE4T0Q0cE9pQmhjbU0wTGxWcGJuUThPRDRnZXdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZRWFp0Vkhsd1pYTXVZWEpqTkZWcGJuUk9OVEV5VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpORlZwYm5ST05URXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTNNUW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZelJWYVc1MFRqVXhNaWhrWVhSaE9pQmhjbU0wTGxWcGJuUThOVEV5UGlrNklHRnlZelF1VldsdWREdzFNVEkrSUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQndkWE5vYVc1MElEWTBJQzh2SURZMENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblExTVRJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TnpJS0lDQWdJQzh2SUdWdGFYUW9KMkZ5WXpSVmFXNTBUalV4TWljc0lHUmhkR0VwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGhpWkRnd01qVmpaQ0F2THlCdFpYUm9iMlFnSW1GeVl6UlZhVzUwVGpVeE1paDFhVzUwTlRFeUtTSUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTNNUW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZelJWYVc1MFRqVXhNaWhrWVhSaE9pQmhjbU0wTGxWcGJuUThOVEV5UGlrNklHRnlZelF1VldsdWREdzFNVEkrSUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0YyYlZSNWNHVnpMbUZ5WXpSVmFXNTBUakV5T0VGc2FXRnpXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak5GVnBiblJPTVRJNFFXeHBZWE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVGMxQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTkZWcGJuUk9NVEk0UVd4cFlYTW9aR0YwWVRvZ1lYSmpOQzVWYVc1ME1USTRLVG9nWVhKak5DNVZhVzUwTVRJNElIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0J3ZFhOb2FXNTBJREUySUM4dklERTJDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5ReE1qZ0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE56WUtJQ0FnSUM4dklHVnRhWFFvSjJGeVl6UlZhVzUwVGpFeU9FRnNhV0Z6Snl3Z1pHRjBZU2tLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlRGN5WlRrMk1HVXdJQzh2SUcxbGRHaHZaQ0FpWVhKak5GVnBiblJPTVRJNFFXeHBZWE1vZFdsdWRERXlPQ2tpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TnpVS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NMFZXbHVkRTR4TWpoQmJHbGhjeWhrWVhSaE9pQmhjbU0wTGxWcGJuUXhNamdwT2lCaGNtTTBMbFZwYm5ReE1qZ2dld29nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlFYWnRWSGx3WlhNdVlYSmpORlZwYm5ST01UWkJiR2xoYzF0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZelJWYVc1MFRqRTJRV3hwWVhNNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRjNUNpQWdJQ0F2THlCd2RXSnNhV01nWVhKak5GVnBiblJPTVRaQmJHbGhjeWhrWVhSaE9pQmhjbU0wTGxWcGJuUXhOaWs2SUdGeVl6UXVWV2x1ZERFMklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6RWdMeThnTWdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBNVFlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hPREFLSUNBZ0lDOHZJR1Z0YVhRb0oyRnlZelJWYVc1MFRqRTJRV3hwWVhNbkxDQmtZWFJoS1FvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TlRZMk5EY3lZVEFnTHk4Z2JXVjBhRzlrSUNKaGNtTTBWV2x1ZEU0eE5rRnNhV0Z6S0hWcGJuUXhOaWtpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TnprS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NMFZXbHVkRTR4TmtGc2FXRnpLR1JoZEdFNklHRnlZelF1VldsdWRERTJLVG9nWVhKak5DNVZhVzUwTVRZZ2V3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WVhKak5GVnBiblJPTWpVMlFXeHBZWE5iY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTBWV2x1ZEU0eU5UWkJiR2xoY3pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94T0RNS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NMFZXbHVkRTR5TlRaQmJHbGhjeWhrWVhSaE9pQmhjbU0wTGxWcGJuUXlOVFlwT2lCaGNtTTBMbFZwYm5ReU5UWWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUhCMWMyaHBiblFnTXpJZ0x5OGdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1ZFdsdWRESTFOZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTROQW9nSUNBZ0x5OGdaVzFwZENnbllYSmpORlZwYm5ST01qVTJRV3hwWVhNbkxDQmtZWFJoS1FvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TUdZeFpqQXhOMlFnTHk4Z2JXVjBhRzlrSUNKaGNtTTBWV2x1ZEU0eU5UWkJiR2xoY3loMWFXNTBNalUyS1NJS0lDQWdJR1JwWnlBeENpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFNE13b2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6UlZhVzUwVGpJMU5rRnNhV0Z6S0dSaGRHRTZJR0Z5WXpRdVZXbHVkREkxTmlrNklHRnlZelF1VldsdWRESTFOaUI3Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJkbTFVZVhCbGN5NWhjbU0wVldsdWRFNDJORUZzYVdGelczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTkZWcGJuUk9OalJCYkdsaGN6b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE9EY0tJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTTBWV2x1ZEU0Mk5FRnNhV0Z6S0dSaGRHRTZJR0Z5WXpRdVZXbHVkRFkwS1RvZ1lYSmpOQzVWYVc1ME5qUWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNeUF2THlBNENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblEyTkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakU0T0FvZ0lDQWdMeThnWlcxcGRDZ25ZWEpqTkZWcGJuUk9OalJCYkdsaGN5Y3NJR1JoZEdFcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhoa01EY3pPR1ZtT1NBdkx5QnRaWFJvYjJRZ0ltRnlZelJWYVc1MFRqWTBRV3hwWVhNb2RXbHVkRFkwS1NJS0lDQWdJR1JwWnlBeENpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFNE53b2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6UlZhVzUwVGpZMFFXeHBZWE1vWkdGMFlUb2dZWEpqTkM1VmFXNTBOalFwT2lCaGNtTTBMbFZwYm5RMk5DQjdDaUFnSUNCaWVYUmxZMTh3SUM4dklEQjRNVFV4Wmpkak56VUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwQmRtMVVlWEJsY3k1aGNtTTBWV2x1ZEU0NFFXeHBZWE5iY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTBWV2x1ZEU0NFFXeHBZWE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVGt4Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTkZWcGJuUk9PRUZzYVdGektHUmhkR0U2SUdGeVl6UXVWV2x1ZERncE9pQmhjbU0wTGxWcGJuUTRJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwT0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakU1TWdvZ0lDQWdMeThnWlcxcGRDZ25ZWEpqTkZWcGJuUk9PRUZzYVdGekp5d2daR0YwWVNrS0lDQWdJSEIxYzJoaWVYUmxjeUF3ZURKbU9USm1NVGhoSUM4dklHMWxkR2h2WkNBaVlYSmpORlZwYm5ST09FRnNhV0Z6S0hWcGJuUTRLU0lLSUNBZ0lHUnBaeUF4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakU1TVFvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSVmFXNTBUamhCYkdsaGN5aGtZWFJoT2lCaGNtTTBMbFZwYm5RNEtUb2dZWEpqTkM1VmFXNTBPQ0I3Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJkbTFVZVhCbGN5NWhjbU0wVldsdWRFNDRNbFIxY0d4bFczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTkZWcGJuUk9PREpVZFhCc1pUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE9UVUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTTBWV2x1ZEU0NE1sUjFjR3hsS0dSaGRHRTZJRnRoY21NMExsVnBiblE0TENCaGNtTTBMbFZwYm5RNFhTazZJRnRoY21NMExsVnBiblE0TENCaGNtTTBMbFZwYm5RNFhTQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHhJQzh2SURJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRIVndiR1U4WVhKak5DNTFhVzUwT0N4aGNtTTBMblZwYm5RNFBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFNU5nb2dJQ0FnTHk4Z1pXMXBkQ2duWVhKak5GVnBiblJPT0RKVWRYQnNaU2NzSUM0dUxtUmhkR0VwQ2lBZ0lDQmtkWEFLSUNBZ0lHVjRkSEpoWTNRZ01DQXhDaUFnSUNCa2FXY2dNUW9nSUNBZ1pYaDBjbUZqZENBeElERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjROekU1T1RGaE16QWdMeThnYldWMGFHOWtJQ0poY21NMFZXbHVkRTQ0TWxSMWNHeGxLSFZwYm5RNExIVnBiblE0S1NJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRrMUNpQWdJQ0F2THlCd2RXSnNhV01nWVhKak5GVnBiblJPT0RKVWRYQnNaU2hrWVhSaE9pQmJZWEpqTkM1VmFXNTBPQ3dnWVhKak5DNVZhVzUwT0YwcE9pQmJZWEpqTkM1VmFXNTBPQ3dnWVhKak5DNVZhVzUwT0YwZ2V3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVhadFZIbHdaWE11WVhKak5GVnBiblJPT0ROVWRYQnNaVnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6UlZhVzUwVGpnelZIVndiR1U2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVGs1Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTkZWcGJuUk9PRE5VZFhCc1pTaGtZWFJoT2lCYllYSmpOQzVWYVc1ME9Dd2dZWEpqTkM1VmFXNTBPQ3dnWVhKak5DNVZhVzUwT0YwcE9pQmJZWEpqTkM1VmFXNTBPQ3dnWVhKak5DNVZhVzUwT0N3Z1lYSmpOQzVWYVc1ME9GMGdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUhCMWMyaHBiblFnTXlBdkx5QXpDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblIxY0d4bFBHRnlZelF1ZFdsdWREZ3NZWEpqTkM1MWFXNTBPQ3hoY21NMExuVnBiblE0UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakl3TUFvZ0lDQWdMeThnWlcxcGRDZ25ZWEpqTkZWcGJuUk9PRE5VZFhCc1pTY3NJQzR1TG1SaGRHRXBDaUFnSUNCa2RYQUtJQ0FnSUdWNGRISmhZM1FnTUNBeENpQWdJQ0JrYVdjZ01Rb2dJQ0FnWlhoMGNtRmpkQ0F4SURFS0lDQWdJR1JwWnlBeUNpQWdJQ0JsZUhSeVlXTjBJRElnTVFvZ0lDQWdZMjkyWlhJZ01nb2dJQ0FnWTI5dVkyRjBDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlREptTURGbFl6YzBJQzh2SUcxbGRHaHZaQ0FpWVhKak5GVnBiblJPT0ROVWRYQnNaU2gxYVc1ME9DeDFhVzUwT0N4MWFXNTBPQ2tpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakU1T1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpSVmFXNTBUamd6VkhWd2JHVW9aR0YwWVRvZ1cyRnlZelF1VldsdWREZ3NJR0Z5WXpRdVZXbHVkRGdzSUdGeVl6UXVWV2x1ZERoZEtUb2dXMkZ5WXpRdVZXbHVkRGdzSUdGeVl6UXVWV2x1ZERnc0lHRnlZelF1VldsdWREaGRJSHNLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRjJiVlI1Y0dWekxtRnlZelJEYjIxd2JHVjRWSFZ3YkdWYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0wUTI5dGNHeGxlRlIxY0d4bE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJd015MHlNVEFLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU0wUTI5dGNHeGxlRlIxY0d4bEtBb2dJQ0FnTHk4Z0lDQmtZWFJoT2lCYkNpQWdJQ0F2THlBZ0lDQWdZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNBZ0lGdGhjbU0wTGxWcGJuUXlOVFlzSUdGeVl6UXVWV2x1ZERJMU5sMHNDaUFnSUNBdkx5QWdJQ0FnVzJGeVl6UXVWV2x1ZERJMU5pd2dZWEpqTkM1VmFXNTBNalUyWFN3S0lDQWdJQzh2SUNBZ0lDQmJZWEpqTkM1VmFXNTBNalUyTENCaGNtTTBMbFZwYm5ReU5UWmRMQW9nSUNBZ0x5OGdJQ0JkTEFvZ0lDQWdMeThnS1RvZ1cyRnlZelF1UVdSa2NtVnpjeXdnVzJGeVl6UXVWV2x1ZERJMU5pd2dZWEpqTkM1VmFXNTBNalUyWFN3Z1cyRnlZelF1VldsdWRESTFOaXdnWVhKak5DNVZhVzUwTWpVMlhTd2dXMkZ5WXpRdVZXbHVkREkxTml3Z1lYSmpOQzVWYVc1ME1qVTJYVjBnZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lIQjFjMmhwYm5RZ01qSTBJQzh2SURJeU5Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTBkWEJzWlR4aGNtTTBMbk4wWVhScFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNExDQXpNajRzWVhKak5DNTBkWEJzWlR4aGNtTTBMblZwYm5ReU5UWXNZWEpqTkM1MWFXNTBNalUyUGl4aGNtTTBMblIxY0d4bFBHRnlZelF1ZFdsdWRESTFOaXhoY21NMExuVnBiblF5TlRZK0xHRnlZelF1ZEhWd2JHVThZWEpqTkM1MWFXNTBNalUyTEdGeVl6UXVkV2x1ZERJMU5qNCtDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qRXhDaUFnSUNBdkx5QmxiV2wwS0NkaGNtTTBRMjl0Y0d4bGVGUjFjR3hsSnl3Z0xpNHVaR0YwWVNrS0lDQWdJR1IxY0FvZ0lDQWdaWGgwY21GamRDQXdJRE15Q2lBZ0lDQmthV2NnTVFvZ0lDQWdaWGgwY21GamRDQXpNaUEyTkFvZ0lDQWdaR2xuSURJS0lDQWdJR1Y0ZEhKaFkzUWdPVFlnTmpRS0lDQWdJR1JwWnlBekNpQWdJQ0JsZUhSeVlXTjBJREUyTUNBMk5Bb2dJQ0FnZFc1amIzWmxjaUF6Q2lBZ0lDQjFibU52ZG1WeUlETUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0JqYjI1allYUUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRaV1E0WVRsbU1qWWdMeThnYldWMGFHOWtJQ0poY21NMFEyOXRjR3hsZUZSMWNHeGxLR0ZrWkhKbGMzTXNLSFZwYm5ReU5UWXNkV2x1ZERJMU5pa3NLSFZwYm5ReU5UWXNkV2x1ZERJMU5pa3NLSFZwYm5ReU5UWXNkV2x1ZERJMU5pa3BJZ29nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lNRE10TWpFd0NpQWdJQ0F2THlCd2RXSnNhV01nWVhKak5FTnZiWEJzWlhoVWRYQnNaU2dLSUNBZ0lDOHZJQ0FnWkdGMFlUb2dXd29nSUNBZ0x5OGdJQ0FnSUdGeVl6UXVRV1JrY21WemN5d0tJQ0FnSUM4dklDQWdJQ0JiWVhKak5DNVZhVzUwTWpVMkxDQmhjbU0wTGxWcGJuUXlOVFpkTEFvZ0lDQWdMeThnSUNBZ0lGdGhjbU0wTGxWcGJuUXlOVFlzSUdGeVl6UXVWV2x1ZERJMU5sMHNDaUFnSUNBdkx5QWdJQ0FnVzJGeVl6UXVWV2x1ZERJMU5pd2dZWEpqTkM1VmFXNTBNalUyWFN3S0lDQWdJQzh2SUNBZ1hTd0tJQ0FnSUM4dklDazZJRnRoY21NMExrRmtaSEpsYzNNc0lGdGhjbU0wTGxWcGJuUXlOVFlzSUdGeVl6UXVWV2x1ZERJMU5sMHNJRnRoY21NMExsVnBiblF5TlRZc0lHRnlZelF1VldsdWRESTFObDBzSUZ0aGNtTTBMbFZwYm5ReU5UWXNJR0Z5WXpRdVZXbHVkREkxTmwxZElIc0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T2tGMmJWUjVjR1Z6TG5WcGJuUTJORnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q25WcGJuUTJORG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lNVFFLSUNBZ0lDOHZJSEIxWW14cFl5QjFhVzUwTmpRb1pHRjBZVG9nZFdsdWREWTBLVG9nZFdsdWREWTBJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYek1nTHk4Z09Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwTmpRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TVRVS0lDQWdJQzh2SUdWdGFYUW9KM1ZwYm5RMk5DY3NJR1JoZEdFcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhneE1qUXlOemN6TWlBdkx5QnRaWFJvYjJRZ0luVnBiblEyTkNoMWFXNTBOalFwSWdvZ0lDQWdaR2xuSURFS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWpFMENpQWdJQ0F2THlCd2RXSnNhV01nZFdsdWREWTBLR1JoZEdFNklIVnBiblEyTkNrNklIVnBiblEyTkNCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTUxYVc1ME5qUkJjbkpoZVZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNuVnBiblEyTkVGeWNtRjVPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXhPQW9nSUNBZ0x5OGdjSFZpYkdsaklIVnBiblEyTkVGeWNtRjVLR1JoZEdFNklIVnBiblEyTkZ0ZEtUb2dkV2x1ZERZMFcxMGdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCcGJuUmpYeklnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdhVzUwWTE4eklDOHZJRGdLSUNBZ0lDb0tJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQmthV2NnTVFvZ0lDQWdiR1Z1Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG1SNWJtRnRhV05mWVhKeVlYazhkV2x1ZERZMFBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGMmJWOTBlWEJsY3k5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJeE9Rb2dJQ0FnTHk4Z1pXMXBkQ2duZFdsdWREWTBRWEp5WVhrbkxDQmtZWFJoS1FvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0Tm1Zek5EYzNaREV3TURBeUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU1UZ0tJQ0FnSUM4dklIQjFZbXhwWXlCMWFXNTBOalJCY25KaGVTaGtZWFJoT2lCMWFXNTBOalJiWFNrNklIVnBiblEyTkZ0ZElIc0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T2tGMmJWUjVjR1Z6TG1KdmIyeGxZVzVCY25KaGVWdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUp2YjJ4bFlXNUJjbkpoZVRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloZG0xZmRIbHdaWE12WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TWpJS0lDQWdJQzh2SUhCMVlteHBZeUJpYjI5c1pXRnVRWEp5WVhrb1pHRjBZVG9nWW05dmJHVmhibHRkS1RvZ1ltOXZiR1ZoYmx0ZElIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYVc1MFkxOHlJQzh2SURBS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJR0Z5Y21GNUlHeGxibWQwYUNCb1pXRmtaWElLSUNBZ0lIQjFjMmhwYm5RZ055QXZMeUEzQ2lBZ0lDQXJDaUFnSUNCcGJuUmpYek1nTHk4Z09Bb2dJQ0FnTHdvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QXhDaUFnSUNCc1pXNEtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVaSGx1WVcxcFkxOWhjbkpoZVR4aWIyOXNQZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXlNd29nSUNBZ0x5OGdaVzFwZENnblltOXZiR1ZoYmtGeWNtRjVKeXdnWkdGMFlTa0tJQ0FnSUhCMWMyaGllWFJsY3lBd2VEazFOR0l6TWpNeU1EQXdNZ29nSUNBZ1pHbG5JREVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWFp0WDNSNWNHVnpMMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNakl5Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZbTl2YkdWaGJrRnljbUY1S0dSaGRHRTZJR0p2YjJ4bFlXNWJYU2s2SUdKdmIyeGxZVzViWFNCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVoY21NMFUzUnlXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak5GTjBjam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lNallLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU0wVTNSeUtHUmhkR0U2SUdGeVl6UXVVM1J5S1RvZ1lYSmpOQzVUZEhJZ2V3b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnWkhWd0NpQWdJQ0JwYm5Salh6SWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUNzS0lDQWdJR1JwWnlBeENpQWdJQ0JzWlc0S0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakl5TndvZ0lDQWdMeThnWlcxcGRDZ25ZWEpqTkZOMGNpY3NJR1JoZEdFcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhoa01XRm1abVZoTmpBd01ESUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXlOZ29nSUNBZ0x5OGdjSFZpYkdsaklHRnlZelJUZEhJb1pHRjBZVG9nWVhKak5DNVRkSElwT2lCaGNtTTBMbE4wY2lCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVoY21NMFZIVndiR1ZiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTBWSFZ3YkdVNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhadFgzUjVjR1Z6TDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWpNd0NpQWdJQ0F2THlCd2RXSnNhV01nWVhKak5GUjFjR3hsS0dSaGRHRTZJR0Z5WXpRdVZIVndiR1U4VzJGeVl6UXVWV2x1ZERZMExDQmhjbU0wTGxOMGNsMCtLVG9nWVhKak5DNVVkWEJzWlR4YllYSmpOQzVWYVc1ME5qUXNJR0Z5WXpRdVUzUnlYVDRnZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHUnBaeUF4Q2lBZ0lDQnBiblJqWHpNZ0x5OGdPQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ2RIVndiR1VnWlc1amIyUnBibWNLSUNBZ0lHUjFjQW9nSUNBZ2NIVnphR2x1ZENBeE1DQXZMeUF4TUFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lIUmhhV3dnY0c5cGJuUmxjaUJoZENCcGJtUmxlQ0F4SUc5bUlDaDFhVzUwTmpRc0tHeGxiaXQxZEdZNFcxMHBLUW9nSUNBZ1pHbG5JRElLSUNBZ0lITjNZWEFLSUNBZ0lHUnBaeUF5Q2lBZ0lDQnpkV0p6ZEhKcGJtY3pDaUFnSUNCcGJuUmpYeklnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdjSFZ6YUdsdWRDQXhNaUF2THlBeE1nb2dJQ0FnS3dvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MGRYQnNaVHhoY21NMExuVnBiblEyTkN4aGNtTTBMbVI1Ym1GdGFXTmZZWEp5WVhrOFlYSmpOQzUxYVc1ME9ENCtDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYWnRYM1I1Y0dWekwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qTXhDaUFnSUNBdkx5QmxiV2wwS0NkaGNtTTBWSFZ3YkdVbkxDQmtZWFJoS1FvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0Tm1OaVkyVXpaVFV3TURBeUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU16QUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTTBWSFZ3YkdVb1pHRjBZVG9nWVhKak5DNVVkWEJzWlR4YllYSmpOQzVWYVc1ME5qUXNJR0Z5WXpRdVUzUnlYVDRwT2lCaGNtTTBMbFIxY0d4bFBGdGhjbU0wTGxWcGJuUTJOQ3dnWVhKak5DNVRkSEpkUGlCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTV1WVhScGRtVlVkWEJzWlZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtNWhkR2wyWlZSMWNHeGxPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXpOQW9nSUNBZ0x5OGdjSFZpYkdsaklHNWhkR2wyWlZSMWNHeGxLR1JoZEdFNklGdDFhVzUwTmpRc0lITjBjbWx1Wnl3Z1ltOXZiR1ZoYmwwcE9pQmJkV2x1ZERZMExDQnpkSEpwYm1jc0lHSnZiMnhsWVc1ZElIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnYVc1MFkxOHpJQzh2SURnS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJSFIxY0d4bElHVnVZMjlrYVc1bkNpQWdJQ0JrZFhBS0lDQWdJSEIxYzJocGJuUWdNVEVnTHk4Z01URUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0IwWVdsc0lIQnZhVzUwWlhJZ1lYUWdhVzVrWlhnZ01TQnZaaUFvZFdsdWREWTBMQ2hzWlc0cmRYUm1PRnRkS1N4aWIyOXNNU2tLSUNBZ0lHUnBaeUF5Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmthV2NnTWdvZ0lDQWdjM1ZpYzNSeWFXNW5Nd29nSUNBZ1pIVndDaUFnSUNCcGJuUmpYeklnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdjSFZ6YUdsdWRDQXhNeUF2THlBeE13b2dJQ0FnS3dvZ0lDQWdkVzVqYjNabGNpQXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblIxY0d4bFBIVnBiblEyTkN4emRISnBibWNzWW05dmJENEtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU16VUtJQ0FnSUM4dklHVnRhWFFvSjI1aGRHbDJaVlIxY0d4bEp5d2dMaTR1WkdGMFlTa0tJQ0FnSUdScFp5QXhDaUFnSUNCbGVIUnlZV04wSURBZ09Bb2dJQ0FnWkdsbklESUtJQ0FnSUhCMWMyaHBiblFnT0RBZ0x5OGdPREFLSUNBZ0lHZGxkR0pwZEFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TURBS0lDQWdJR2x1ZEdOZk1pQXZMeUF3Q2lBZ0lDQjFibU52ZG1WeUlESUtJQ0FnSUhObGRHSnBkQW9nSUNBZ2MzZGhjQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRNREF3WWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRZVEF5WW1KbU1UZ2dMeThnYldWMGFHOWtJQ0p1WVhScGRtVlVkWEJzWlNoMWFXNTBOalFzYzNSeWFXNW5MR0p2YjJ3cElnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGRtMWZkSGx3WlhNdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU16UUtJQ0FnSUM4dklIQjFZbXhwWXlCdVlYUnBkbVZVZFhCc1pTaGtZWFJoT2lCYmRXbHVkRFkwTENCemRISnBibWNzSUdKdmIyeGxZVzVkS1RvZ1czVnBiblEyTkN3Z2MzUnlhVzVuTENCaWIyOXNaV0Z1WFNCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCZG0xVWVYQmxjeTVtYVhobFpFRnljbUY1VldsdWREWTBXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWm1sNFpXUkJjbkpoZVZWcGJuUTJORG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhkbTFmZEhsd1pYTXZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lNemdLSUNBZ0lDOHZJSEIxWW14cFl5Qm1hWGhsWkVGeWNtRjVWV2x1ZERZMEtHUmhkR0U2SUVacGVHVmtRWEp5WVhrOGRXbHVkRFkwTENBelBpazZJRVpwZUdWa1FYSnlZWGs4ZFdsdWREWTBMQ0F6UGlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdjSFZ6YUdsdWRDQXlOQ0F2THlBeU5Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNXpkR0YwYVdOZllYSnlZWGs4ZFdsdWREWTBMQ0F6UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkYyYlY5MGVYQmxjeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakl6T1FvZ0lDQWdMeThnWlcxcGRDZ25abWw0WldSQmNuSmhlVlZwYm5RMk5DY3NJR1JoZEdFcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhneFltWXlOREJrT0NBdkx5QnRaWFJvYjJRZ0ltWnBlR1ZrUVhKeVlYbFZhVzUwTmpRb2RXbHVkRFkwV3pOZEtTSUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRjJiVjkwZVhCbGN5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXpPQW9nSUNBZ0x5OGdjSFZpYkdsaklHWnBlR1ZrUVhKeVlYbFZhVzUwTmpRb1pHRjBZVG9nUm1sNFpXUkJjbkpoZVR4MWFXNTBOalFzSURNK0tUb2dSbWw0WldSQmNuSmhlVHgxYVc1ME5qUXNJRE0rSUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200SyIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZbUZ6WlMxamIyNTBjbUZqZEM1a0xuUnpPanBDWVhObFEyOXVkSEpoWTNRdVkyeGxZWEpUZEdGMFpWQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0J3ZFhOb2FXNTBJREVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZz09In0sImJ5dGVDb2RlIjp7ImFwcHJvdmFsIjoiQ3lBRUFRSUFDQ1lDQkJVZmZIVUdGUjk4ZFFBZ01SdEJBVjh4R1JSRU1SaEVnakFFVS84c0xnU1JFY1M4Qk9YTmdja0V5Q01QMXdUa2NPWUlCSVZSN3pvRUF3ZExHUVJ5UUxObUJLSjQrWThFczNlK2JRU2tyNDdrQk9NZ3ZGNEV2WkVpK1FSU3V1MlNCS3kyeHA4RWdiN21JZ1RVblJ4ZEJOQU1WcThFQTVYUW5nUzF6bzREQkJhRk9YUUVRc1c3andTcVdzSEpCR1RnQ1BBRXFydnNnd1EyK2VxdEJEYlNRVUVFem1ieTZnUk5TRzZaQkdtYktoZ0VKWTl6ckFSUTRtSitCSkpRdUZZRUtPNGswQVRIeE9ocUJCdG5wQzRFZTRNcG1BVGcyZ2NPQk5yMWJOTUV1WENUOHdTMkZnUHlCS1Ard2w4RU1hN084UVNLTEdlTkJJTHIvNUlFa0ZXVHlRUXBWNzdKQkdocU1xWTJHZ0NPTUFBTEFDb0FTUUNsQU1RQTNBRCtBUmNCTUFGTUFXa0JoZ0dpQWJnQjBRSHBBZ0VDSEFJNEFsUUNiQUtGQXBVQ3FBTEJBdGtDOFFNVEF5c0RSUU5kQTNZRGpnT25BOEFEMkFQeEJBa0VJUVJDQkcwRW93UzdCTndGQUFVZkJVd0ZsQUF4R1JReEdCUVFSQ0pETmhvQlNTUlpJd2hMQVJVU1JJQUdSa3ZPZUFBQ1N3RlFzQ2hNVUxBaVF6WWFBVWtrV1NNSVN3RVZFa1NBQmtUbmtPTUFBa3NCVUxBb1RGQ3dJa00yR2dGSEFpUlpTVTRDSXd0TVNSVk1Wd0lBSkVsTEJReEJBQ1JIQWlNTFN3TkpUd0paU1VzSFNVNEVFa1JMQVJWU0pGa2pDQWhGQlNJSVJRRkMvOVZMQXlNSVN3TVNSSUFHZlNwRXZ3QUNTd1pKVGdKUXNDaE1VTEFpUXpZYUFVa2tXU01JU3dFVkVrU0FCc005U2h3QUFrc0JVTEFvVEZDd0lrTTJHZ0ZKRlNJU1JJQUVSTHc1WDBzQlVMQW9URkN3SWtNMkdnRkpGVW1CUUJKRWdVQU9SSUZBcjZ1QUJIU2xrTWRMQVZDd0tFeFFzQ0pETmhvQlNSV0JRQkpFZ0FSeXVTajdTd0ZRc0NoTVVMQWlRellhQVVrVmdVZ1NSSUFFSkpPMEdFc0JVTEFvVEZDd0lrTXhGaUlKU1RnUUloSkVPQmVBQkxtdkU2WkxBVkN3S1V4UXNDSkRNUllpQ1VrNEVJRUdFa1E0RjRBRXVmeUdnMHNCVUxBcFRGQ3dJa014RmlJSlNUZ1FnUVFTUkRnWGdBUTExMDg3U3dGUXNDbE1VTEFpUXpFV0lnbEpPQkFqRWtRNEY0QUV2NUNQV2tzQlVMQXBURkN3SWtNeEZpSUpPQmVBQlBrK0JwbExBVkN3S1V4UXNDSkROaG9CU1JXQklCSkVnQVRaM01EeFN3RlFzQ2hNVUxBaVF6WWFBVWtWSlJKRWdBUUtJN3RRU3dGUXNDaE1VTEFpUXpZYUFVa1ZKUkpFZ0FUS004YmdTd0ZRc0NoTVVMQWlRellhQVVrVkloSkVGOEFjZ0FTaEVsUFhTd0ZRc0NoTVVMQWlRellhQVVrVkloSkVGOEF3Rm9BRWNnMFlKMHNCVUxBb1RGQ3dJa00yR2dGSkZTSVNSQmZBTWhhQUJMK1U5c1pMQVZDd0tFeFFzQ0pETmhvQlNSVWxFa1NBQkZJN1k3dExBVkN3S0V4UXNDSkROaG9CU1JXQklCSkVnQVFkS2gvV1N3RlFzQ2hNVUxBaVF6WWFBVWtWZ2Z3SEVrUW9URkN3SWtNMkdnRkpKRmtqQ0VzQkZSSkVLRXhRc0NKRE5ob0JTUldCSUJKRWdBVFkrTFdNU3dGUXNDaE1VTEFpUXpZYUFVa1ZJaEpFZ0FSM1dESWNTd0ZRc0NoTVVMQWlRellhQVVrVkloSkVnQVN2dXF6blN3RlFzQ2hNVUxBaVF6WWFBVWtrV1lGQUN5TUlTd0VWRWtTQUJ2ZTAybUlBQWtzQlVMQW9URkN3SWtNMkdnRkpGU01TUklBRXFkb0ZXMHNCVUxBb1RGQ3dJa00yR2dGSkZZR0FBUkpFZ0FRcnU4UW5Td0ZRc0NoTVVMQWlRellhQVVrVkloSkVnQVNLTkV5TVN3RlFzQ2hNVUxBaVF6WWFBVWtWZ1VBU1JJQUVLY2JEemtzQlVMQW9URkN3SWtNMkdnRkpGU0lTUklBRXlSc1BYMHNCVUxBb1RGQ3dJa00yR2dGSkZZRkFFa1NBQkwyQUpjMUxBVkN3S0V4UXNDSkROaG9CU1JXQkVCSkVnQVJ5NldEZ1N3RlFzQ2hNVUxBaVF6WWFBVWtWSXhKRWdBUldaSEtnU3dGUXNDaE1VTEFpUXpZYUFVa1ZnU0FTUklBRUR4OEJmVXNCVUxBb1RGQ3dJa00yR2dGSkZTVVNSSUFFMEhPTytVc0JVTEFvVEZDd0lrTTJHZ0ZKRlNJU1JJQUVMNUx4aWtzQlVMQW9URkN3SWtNMkdnRkpGU01TUkVsWEFBRkxBVmNCQVZDQUJIR1pHakJNVUxBb1RGQ3dJa00yR2dGSkZZRURFa1JKVndBQlN3RlhBUUZMQWxjQ0FVNENVRXhRZ0FRdkFleDBURkN3S0V4UXNDSkROaG9CU1JXQjRBRVNSRWxYQUNCTEFWY2dRRXNDVjJCQVN3TlhvRUJQQTA4RFVFOENVRXhRZ0FUdGlwOG1URkN3S0V4UXNDSkROaG9CU1JVbEVrU0FCQkpDZHpKTEFWQ3dLRXhRc0NKRE5ob0JTU1JaSlFzakNFc0JGUkpFZ0Fadk5IZlJBQUpMQVZDd0tFeFFzQ0pETmhvQlNTUlpnUWNJSlFvakNFc0JGUkpFZ0FhVlN6SXlBQUpMQVZDd0tFeFFzQ0pETmhvQlNTUlpJd2hMQVJVU1JJQUcwYS8rcGdBQ1N3RlFzQ2hNVUxBaVF6WWFBVWtWU3dFbFdVbUJDaEpFU3dKTVN3SlNKRm1CREFnU1JJQUdiTHpqNVFBQ1N3RlFzQ2hNVUxBaVF6WWFBVWtWU3dFbFdVbUJDeEpFU3dKTVN3SlNTU1JaZ1EwSVR3SVNSRXNCVndBSVN3S0JVRk9BQVFBa1R3SlVUSUFDQUF0UVRGQk1VSUFFb0N1L0dFeFFzQ2hNVUxBaVF6WWFBVWtWZ1JnU1JJQUVHL0pBMkVzQlVMQW9URkN3SWtNPSIsImNsZWFyIjoiQzRFQlF3PT0ifSwiY29tcGlsZXJJbmZvIjp7ImNvbXBpbGVyIjoicHV5YSIsImNvbXBpbGVyVmVyc2lvbiI6eyJtYWpvciI6NSwibWlub3IiOjMsInBhdGNoIjoyLCJjb21taXRIYXNoIjpudWxsfX0sImV2ZW50cyI6W3sibmFtZSI6InN0cmluZ1JlYWRvbmx5IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6InN0cmluZyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJzdHJpbmdBcnJheSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmdbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImJ5dGVzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImJvb2xlYW4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImJpZ3VpbnQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDUxMiIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6InN0cnVjdCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyx1aW50MjU2KSIsInN0cnVjdCI6InN0cnVjdEFkZHJlc3NVaW50MjU2IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImlubmVyU3RydWN0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50NjQsKGFkZHJlc3MsdWludDI1NikpIiwic3RydWN0IjoiaW5uZXJTdHJ1Y3QiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiUGF5bWVudFR4biIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6IkFwcGxpY2F0aW9uQ2FsbFR4biIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6IkFzc2V0VHJhbnNmZXJUeG4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJLZXlSZWdpc3RyYXRpb25UeG4iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJUcmFuc2FjdGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFjY291bnQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhc3NldCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcHBsaWNhdGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhY2NvdW50SW5kZXhlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFzc2V0SW5kZXhlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcHBsaWNhdGlvbkluZGV4ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFN0YXRpY0J5dGVzOCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlWzhdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFN0YXRpY0J5dGVzMzIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0QWRkcmVzcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNEJvb2wiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzRCeXRlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGUiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0RHluYW1pY0FycmF5T2ZTdHJ1Y3QiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGFkZHJlc3MsdWludDI1NilbXSIsInN0cnVjdCI6InN0cnVjdEFkZHJlc3NVaW50MjU2IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzRTdGF0aWNBcnJheU9mMkJ5dGVzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0U3RhdGljQXJyYXlPZjJTdHJ1Y3RzIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYpWzJdIiwic3RydWN0Ijoic3RydWN0QWRkcmVzc1VpbnQyNTYiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFVGaXhlZDh4MTYiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWZpeGVkOHgxNiIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzRVRml4ZWQ1MTJ4MTYwIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVmaXhlZDUxMngxNjAiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0VWludE44IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFVpbnRONTEyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ1MTIiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0VWludE4xMjhBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50MTI4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFVpbnROMTZBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50MTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0VWludE4yNTZBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFVpbnRONjRBbGlhcyIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0VWludE44QWxpYXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0VWludE44MlR1cGxlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiMSIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjNFVpbnROODNUdXBsZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjEiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjIiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzRDb21wbGV4VHVwbGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiIodWludDI1Nix1aW50MjU2KSIsInN0cnVjdCI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhRmllbGQxIiwibmFtZSI6IjEiLCJkZXNjIjpudWxsfSx7InR5cGUiOiIodWludDI1Nix1aW50MjU2KSIsInN0cnVjdCI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhRmllbGQxIiwibmFtZSI6IjIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiIodWludDI1Nix1aW50MjU2KSIsInN0cnVjdCI6IkFyYzRDb21wbGV4VHVwbGVBcmdEYXRhRmllbGQxIiwibmFtZSI6IjMiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6InVpbnQ2NCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJ1aW50NjRBcnJheSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjRbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImJvb2xlYW5BcnJheSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM0U3RyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzRUdXBsZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIodWludDY0LHN0cmluZykiLCJzdHJ1Y3QiOiJBcmM0VHVwbGVBcmdEYXRhIiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6Im5hdGl2ZVR1cGxlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiIxIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwibmFtZSI6IjIiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImZpeGVkQXJyYXlVaW50NjQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0WzNdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJ0ZW1wbGF0ZVZhcmlhYmxlcyI6e30sInNjcmF0Y2hWYXJpYWJsZXMiOnt9fQ==";
    }

}
