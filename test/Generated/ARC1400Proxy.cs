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

namespace ARC1400
{


    public class Arc1644Proxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public Arc1644Proxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class ApprovalStruct : AVMObjectType
            {
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 ApprovalAmount { get; set; }

                public Algorand.Address Owner { get; set; }

                public Algorand.Address Spender { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApprovalAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vApprovalAmount.From(ApprovalAmount);
                    ret.AddRange(vApprovalAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vOwner.From(Owner);
                    ret.AddRange(vOwner.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSpender = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vSpender.From(Spender);
                    ret.AddRange(vSpender.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ApprovalStruct Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new ApprovalStruct();
                    uint count = 0;
                    var vApprovalAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vApprovalAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.ApprovalAmount = vApprovalAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOwner = vOwner.ToValue();
                    if (valueOwner is Algorand.Address vOwnerValue) { ret.Owner = vOwnerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSpender = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vSpender.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueSpender = vSpender.ToValue();
                    if (valueSpender is Algorand.Address vSpenderValue) { ret.Spender = vSpenderValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ApprovalStruct);
                }
                public bool Equals(ApprovalStruct? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ApprovalStruct left, ApprovalStruct right)
                {
                    return EqualityComparer<ApprovalStruct>.Default.Equals(left, right);
                }
                public static bool operator !=(ApprovalStruct left, ApprovalStruct right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410HoldingPartitionsPaginatedKey : AVMObjectType
            {
                public Algorand.Address Holder { get; set; }

                public ulong Page { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vHolder.From(Holder);
                    ret.AddRange(vHolder.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPage = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPage.From(Page);
                    ret.AddRange(vPage.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410HoldingPartitionsPaginatedKey Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410HoldingPartitionsPaginatedKey();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vHolder.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHolder = vHolder.ToValue();
                    if (valueHolder is Algorand.Address vHolderValue) { ret.Holder = vHolderValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPage = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPage.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePage = vPage.ToValue();
                    if (valuePage is ulong vPageValue) { ret.Page = vPageValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410HoldingPartitionsPaginatedKey);
                }
                public bool Equals(Arc1410HoldingPartitionsPaginatedKey? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410HoldingPartitionsPaginatedKey left, Arc1410HoldingPartitionsPaginatedKey right)
                {
                    return EqualityComparer<Arc1410HoldingPartitionsPaginatedKey>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410HoldingPartitionsPaginatedKey left, Arc1410HoldingPartitionsPaginatedKey right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410OperatorKey : AVMObjectType
            {
                public Algorand.Address Holder { get; set; }

                public Algorand.Address Operator { get; set; }

                public Algorand.Address Partition { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vHolder.From(Holder);
                    ret.AddRange(vHolder.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperator = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vOperator.From(Operator);
                    ret.AddRange(vOperator.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vPartition.From(Partition);
                    ret.AddRange(vPartition.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410OperatorKey Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410OperatorKey();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vHolder.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHolder = vHolder.ToValue();
                    if (valueHolder is Algorand.Address vHolderValue) { ret.Holder = vHolderValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperator = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vOperator.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOperator = vOperator.ToValue();
                    if (valueOperator is Algorand.Address vOperatorValue) { ret.Operator = vOperatorValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePartition = vPartition.ToValue();
                    if (valuePartition is Algorand.Address vPartitionValue) { ret.Partition = vPartitionValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410OperatorKey);
                }
                public bool Equals(Arc1410OperatorKey? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410OperatorKey left, Arc1410OperatorKey right)
                {
                    return EqualityComparer<Arc1410OperatorKey>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410OperatorKey left, Arc1410OperatorKey right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410OperatorPortionKey : AVMObjectType
            {
                public Algorand.Address Holder { get; set; }

                public Algorand.Address Operator { get; set; }

                public Algorand.Address Partition { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vHolder.From(Holder);
                    ret.AddRange(vHolder.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperator = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vOperator.From(Operator);
                    ret.AddRange(vOperator.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vPartition.From(Partition);
                    ret.AddRange(vPartition.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410OperatorPortionKey Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410OperatorPortionKey();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vHolder.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHolder = vHolder.ToValue();
                    if (valueHolder is Algorand.Address vHolderValue) { ret.Holder = vHolderValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperator = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vOperator.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOperator = vOperator.ToValue();
                    if (valueOperator is Algorand.Address vOperatorValue) { ret.Operator = vOperatorValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePartition = vPartition.ToValue();
                    if (valuePartition is Algorand.Address vPartitionValue) { ret.Partition = vPartitionValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410OperatorPortionKey);
                }
                public bool Equals(Arc1410OperatorPortionKey? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410OperatorPortionKey left, Arc1410OperatorPortionKey right)
                {
                    return EqualityComparer<Arc1410OperatorPortionKey>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410OperatorPortionKey left, Arc1410OperatorPortionKey right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410PartitionKey : AVMObjectType
            {
                public Algorand.Address Holder { get; set; }

                public Algorand.Address Partition { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vHolder.From(Holder);
                    ret.AddRange(vHolder.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vPartition.From(Partition);
                    ret.AddRange(vPartition.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410PartitionKey Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410PartitionKey();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHolder = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vHolder.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHolder = vHolder.ToValue();
                    if (valueHolder is Algorand.Address vHolderValue) { ret.Holder = vHolderValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePartition = vPartition.ToValue();
                    if (valuePartition is Algorand.Address vPartitionValue) { ret.Partition = vPartitionValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410PartitionKey);
                }
                public bool Equals(Arc1410PartitionKey? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410PartitionKey left, Arc1410PartitionKey right)
                {
                    return EqualityComparer<Arc1410PartitionKey>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410PartitionKey left, Arc1410PartitionKey right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410CanTransferByPartitionReturn : AVMObjectType
            {
                public byte Code { get; set; }

                public string Status { get; set; }

                public Algorand.Address ReceiverPartition { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCode = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    vCode.From(Code);
                    ret.AddRange(vCode.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vStatus = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vStatus.From(Status);
                    stringRef[ret.Count] = vStatus.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vReceiverPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vReceiverPartition.From(ReceiverPartition);
                    ret.AddRange(vReceiverPartition.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410CanTransferByPartitionReturn Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new Arc1410CanTransferByPartitionReturn();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCode = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    count = vCode.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCode = vCode.ToValue();
                    if (valueCode is byte vCodeValue) { ret.Code = vCodeValue; }
                    var indexStatus = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vStatus = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vStatus.Decode(bytes.Skip(indexStatus + prefixOffset).ToArray());
                    var valueStatus = vStatus.ToValue();
                    if (valueStatus is string vStatusValue) { ret.Status = vStatusValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vReceiverPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vReceiverPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueReceiverPartition = vReceiverPartition.ToValue();
                    if (valueReceiverPartition is Algorand.Address vReceiverPartitionValue) { ret.ReceiverPartition = vReceiverPartitionValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410CanTransferByPartitionReturn);
                }
                public bool Equals(Arc1410CanTransferByPartitionReturn? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410CanTransferByPartitionReturn left, Arc1410CanTransferByPartitionReturn right)
                {
                    return EqualityComparer<Arc1410CanTransferByPartitionReturn>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410CanTransferByPartitionReturn left, Arc1410CanTransferByPartitionReturn right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410PartitionIssue : AVMObjectType
            {
                public Algorand.Address To { get; set; }

                public Algorand.Address Partition { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte[] Data { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vTo.From(To);
                    ret.AddRange(vTo.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vPartition.From(Partition);
                    ret.AddRange(vPartition.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vData.From(Data);
                    ret.AddRange(vData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410PartitionIssue Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410PartitionIssue();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vTo.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTo = vTo.ToValue();
                    if (valueTo is Algorand.Address vToValue) { ret.To = vToValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePartition = vPartition.ToValue();
                    if (valuePartition is Algorand.Address vPartitionValue) { ret.Partition = vPartitionValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueData = vData.ToValue();
                    if (valueData is byte[] vDataValue) { ret.Data = vDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410PartitionIssue);
                }
                public bool Equals(Arc1410PartitionIssue? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410PartitionIssue left, Arc1410PartitionIssue right)
                {
                    return EqualityComparer<Arc1410PartitionIssue>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410PartitionIssue left, Arc1410PartitionIssue right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410PartitionRedeem : AVMObjectType
            {
                public Algorand.Address From { get; set; }

                public Algorand.Address Partition { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte[] Data { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vFrom.From(From);
                    ret.AddRange(vFrom.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vPartition.From(Partition);
                    ret.AddRange(vPartition.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vData.From(Data);
                    ret.AddRange(vData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410PartitionRedeem Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410PartitionRedeem();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vFrom.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFrom = vFrom.ToValue();
                    if (valueFrom is Algorand.Address vFromValue) { ret.From = vFromValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePartition = vPartition.ToValue();
                    if (valuePartition is Algorand.Address vPartitionValue) { ret.Partition = vPartitionValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueData = vData.ToValue();
                    if (valueData is byte[] vDataValue) { ret.Data = vDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410PartitionRedeem);
                }
                public bool Equals(Arc1410PartitionRedeem? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410PartitionRedeem left, Arc1410PartitionRedeem right)
                {
                    return EqualityComparer<Arc1410PartitionRedeem>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410PartitionRedeem left, Arc1410PartitionRedeem right)
                {
                    return !(left == right);
                }

            }

            public class Arc1410PartitionTransfer : AVMObjectType
            {
                public Algorand.Address From { get; set; }

                public Algorand.Address To { get; set; }

                public Algorand.Address Partition { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte[] Data { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vFrom.From(From);
                    ret.AddRange(vFrom.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vTo.From(To);
                    ret.AddRange(vTo.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vPartition.From(Partition);
                    ret.AddRange(vPartition.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vData.From(Data);
                    ret.AddRange(vData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1410PartitionTransfer Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1410PartitionTransfer();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vFrom.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFrom = vFrom.ToValue();
                    if (valueFrom is Algorand.Address vFromValue) { ret.From = vFromValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vTo.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTo = vTo.ToValue();
                    if (valueTo is Algorand.Address vToValue) { ret.To = vToValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPartition = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPartition.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePartition = vPartition.ToValue();
                    if (valuePartition is Algorand.Address vPartitionValue) { ret.Partition = vPartitionValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueData = vData.ToValue();
                    if (valueData is byte[] vDataValue) { ret.Data = vDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1410PartitionTransfer);
                }
                public bool Equals(Arc1410PartitionTransfer? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1410PartitionTransfer left, Arc1410PartitionTransfer right)
                {
                    return EqualityComparer<Arc1410PartitionTransfer>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1410PartitionTransfer left, Arc1410PartitionTransfer right)
                {
                    return !(left == right);
                }

            }

            public class Arc1594IssueEvent : AVMObjectType
            {
                public Algorand.Address To { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte[] Data { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vTo.From(To);
                    ret.AddRange(vTo.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vData.From(Data);
                    ret.AddRange(vData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1594IssueEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1594IssueEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vTo.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTo = vTo.ToValue();
                    if (valueTo is Algorand.Address vToValue) { ret.To = vToValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueData = vData.ToValue();
                    if (valueData is byte[] vDataValue) { ret.Data = vDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1594IssueEvent);
                }
                public bool Equals(Arc1594IssueEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1594IssueEvent left, Arc1594IssueEvent right)
                {
                    return EqualityComparer<Arc1594IssueEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1594IssueEvent left, Arc1594IssueEvent right)
                {
                    return !(left == right);
                }

            }

            public class Arc1594RedeemEvent : AVMObjectType
            {
                public Algorand.Address From { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte[] Data { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vFrom.From(From);
                    ret.AddRange(vFrom.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vData.From(Data);
                    ret.AddRange(vData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1594RedeemEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1594RedeemEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vFrom.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFrom = vFrom.ToValue();
                    if (valueFrom is Algorand.Address vFromValue) { ret.From = vFromValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueData = vData.ToValue();
                    if (valueData is byte[] vDataValue) { ret.Data = vDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1594RedeemEvent);
                }
                public bool Equals(Arc1594RedeemEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1594RedeemEvent left, Arc1594RedeemEvent right)
                {
                    return EqualityComparer<Arc1594RedeemEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1594RedeemEvent left, Arc1594RedeemEvent right)
                {
                    return !(left == right);
                }

            }

            public class Arc1643DocumentRecord : AVMObjectType
            {
                public string Uri { get; set; }

                public byte[] Hash { get; set; }

                public ulong Timestamp { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUri = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vUri.From(Uri);
                    stringRef[ret.Count] = vUri.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vHash.From(Hash);
                    ret.AddRange(vHash.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTimestamp = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vTimestamp.From(Timestamp);
                    ret.AddRange(vTimestamp.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1643DocumentRecord Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new Arc1643DocumentRecord();
                    uint count = 0;
                    var indexUri = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUri = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vUri.Decode(bytes.Skip(indexUri + prefixOffset).ToArray());
                    var valueUri = vUri.ToValue();
                    if (valueUri is string vUriValue) { ret.Uri = vUriValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vHash.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHash = vHash.ToValue();
                    if (valueHash is byte[] vHashValue) { ret.Hash = vHashValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTimestamp = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vTimestamp.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTimestamp = vTimestamp.ToValue();
                    if (valueTimestamp is ulong vTimestampValue) { ret.Timestamp = vTimestampValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1643DocumentRecord);
                }
                public bool Equals(Arc1643DocumentRecord? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1643DocumentRecord left, Arc1643DocumentRecord right)
                {
                    return EqualityComparer<Arc1643DocumentRecord>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1643DocumentRecord left, Arc1643DocumentRecord right)
                {
                    return !(left == right);
                }

            }

            public class Arc1643DocumentRemovedEvent : AVMObjectType
            {
                public byte[] Name { get; set; }

                public string Uri { get; set; }

                public byte[] Hash { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vName.From(Name);
                    ret.AddRange(vName.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUri = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vUri.From(Uri);
                    stringRef[ret.Count] = vUri.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vHash.From(Hash);
                    ret.AddRange(vHash.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1643DocumentRemovedEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new Arc1643DocumentRemovedEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vName.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueName = vName.ToValue();
                    if (valueName is byte[] vNameValue) { ret.Name = vNameValue; }
                    var indexUri = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUri = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vUri.Decode(bytes.Skip(indexUri + prefixOffset).ToArray());
                    var valueUri = vUri.ToValue();
                    if (valueUri is string vUriValue) { ret.Uri = vUriValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vHash.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHash = vHash.ToValue();
                    if (valueHash is byte[] vHashValue) { ret.Hash = vHashValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1643DocumentRemovedEvent);
                }
                public bool Equals(Arc1643DocumentRemovedEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1643DocumentRemovedEvent left, Arc1643DocumentRemovedEvent right)
                {
                    return EqualityComparer<Arc1643DocumentRemovedEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1643DocumentRemovedEvent left, Arc1643DocumentRemovedEvent right)
                {
                    return !(left == right);
                }

            }

            public class Arc1643DocumentUpdatedEvent : AVMObjectType
            {
                public byte[] Name { get; set; }

                public string Uri { get; set; }

                public byte[] Hash { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vName.From(Name);
                    ret.AddRange(vName.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUri = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vUri.From(Uri);
                    stringRef[ret.Count] = vUri.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vHash.From(Hash);
                    ret.AddRange(vHash.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1643DocumentUpdatedEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new Arc1643DocumentUpdatedEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vName.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueName = vName.ToValue();
                    if (valueName is byte[] vNameValue) { ret.Name = vNameValue; }
                    var indexUri = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUri = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vUri.Decode(bytes.Skip(indexUri + prefixOffset).ToArray());
                    var valueUri = vUri.ToValue();
                    if (valueUri is string vUriValue) { ret.Uri = vUriValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vHash.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueHash = vHash.ToValue();
                    if (valueHash is byte[] vHashValue) { ret.Hash = vHashValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1643DocumentUpdatedEvent);
                }
                public bool Equals(Arc1643DocumentUpdatedEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1643DocumentUpdatedEvent left, Arc1643DocumentUpdatedEvent right)
                {
                    return EqualityComparer<Arc1643DocumentUpdatedEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1643DocumentUpdatedEvent left, Arc1643DocumentUpdatedEvent right)
                {
                    return !(left == right);
                }

            }

            public class Arc1644ControllerChangedEvent : AVMObjectType
            {
                public Algorand.Address Old { get; set; }

                public Algorand.Address Neu { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOld = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vOld.From(Old);
                    ret.AddRange(vOld.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNeu = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vNeu.From(Neu);
                    ret.AddRange(vNeu.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1644ControllerChangedEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1644ControllerChangedEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOld = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vOld.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOld = vOld.ToValue();
                    if (valueOld is Algorand.Address vOldValue) { ret.Old = vOldValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNeu = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vNeu.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueNeu = vNeu.ToValue();
                    if (valueNeu is Algorand.Address vNeuValue) { ret.Neu = vNeuValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1644ControllerChangedEvent);
                }
                public bool Equals(Arc1644ControllerChangedEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1644ControllerChangedEvent left, Arc1644ControllerChangedEvent right)
                {
                    return EqualityComparer<Arc1644ControllerChangedEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1644ControllerChangedEvent left, Arc1644ControllerChangedEvent right)
                {
                    return !(left == right);
                }

            }

            public class Arc1644ControllerRedeemEvent : AVMObjectType
            {
                public Algorand.Address Controller { get; set; }

                public Algorand.Address From { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte Code { get; set; }

                public byte[] OperatorData { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vController = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vController.From(Controller);
                    ret.AddRange(vController.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vFrom.From(From);
                    ret.AddRange(vFrom.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCode = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    vCode.From(Code);
                    ret.AddRange(vCode.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperatorData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vOperatorData.From(OperatorData);
                    ret.AddRange(vOperatorData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1644ControllerRedeemEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1644ControllerRedeemEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vController = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vController.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueController = vController.ToValue();
                    if (valueController is Algorand.Address vControllerValue) { ret.Controller = vControllerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vFrom.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFrom = vFrom.ToValue();
                    if (valueFrom is Algorand.Address vFromValue) { ret.From = vFromValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCode = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    count = vCode.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCode = vCode.ToValue();
                    if (valueCode is byte vCodeValue) { ret.Code = vCodeValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperatorData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vOperatorData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOperatorData = vOperatorData.ToValue();
                    if (valueOperatorData is byte[] vOperatorDataValue) { ret.OperatorData = vOperatorDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1644ControllerRedeemEvent);
                }
                public bool Equals(Arc1644ControllerRedeemEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1644ControllerRedeemEvent left, Arc1644ControllerRedeemEvent right)
                {
                    return EqualityComparer<Arc1644ControllerRedeemEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1644ControllerRedeemEvent left, Arc1644ControllerRedeemEvent right)
                {
                    return !(left == right);
                }

            }

            public class Arc1644ControllerTransferEvent : AVMObjectType
            {
                public Algorand.Address Controller { get; set; }

                public Algorand.Address From { get; set; }

                public Algorand.Address To { get; set; }

                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Amount { get; set; }

                public byte Code { get; set; }

                public byte[] Data { get; set; }

                public byte[] OperatorData { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vController = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vController.From(Controller);
                    ret.AddRange(vController.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vFrom.From(From);
                    ret.AddRange(vFrom.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vTo.From(To);
                    ret.AddRange(vTo.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAmount = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    vAmount.From(Amount);
                    ret.AddRange(vAmount.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCode = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    vCode.From(Code);
                    ret.AddRange(vCode.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vData.From(Data);
                    ret.AddRange(vData.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperatorData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vOperatorData.From(OperatorData);
                    ret.AddRange(vOperatorData.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static Arc1644ControllerTransferEvent Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new Arc1644ControllerTransferEvent();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vController = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vController.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueController = vController.ToValue();
                    if (valueController is Algorand.Address vControllerValue) { ret.Controller = vControllerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vFrom.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFrom = vFrom.ToValue();
                    if (valueFrom is Algorand.Address vFromValue) { ret.From = vFromValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vTo.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTo = vTo.ToValue();
                    if (valueTo is Algorand.Address vToValue) { ret.To = vToValue; }
                    var vAmount = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
                    count = vAmount.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    ret.Amount = vAmount;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCode = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte");
                    count = vCode.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCode = vCode.ToValue();
                    if (valueCode is byte vCodeValue) { ret.Code = vCodeValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueData = vData.ToValue();
                    if (valueData is byte[] vDataValue) { ret.Data = vDataValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOperatorData = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vOperatorData.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOperatorData = vOperatorData.ToValue();
                    if (valueOperatorData is byte[] vOperatorDataValue) { ret.OperatorData = vOperatorDataValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as Arc1644ControllerTransferEvent);
                }
                public bool Equals(Arc1644ControllerTransferEvent? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(Arc1644ControllerTransferEvent left, Arc1644ControllerTransferEvent right)
                {
                    return EqualityComparer<Arc1644ControllerTransferEvent>.Default.Equals(left, right);
                }
                public static bool operator !=(Arc1644ControllerTransferEvent left, Arc1644ControllerTransferEvent right)
                {
                    return !(left == right);
                }

            }

        }

        public class Events
        {
            public class ControllerChangedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 64, 156, 197, 112 };
                public const string Signature = "ControllerChanged((address,address))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1644ControllerChangedEvent Field0 { get; set; }

                public static ControllerChangedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new ControllerChangedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    ret.Field0 = Structs.Arc1644ControllerChangedEvent.Parse(queue.ToArray());
                    { var consumedField0 = ret.Field0.ToByteArray().Length; for (int i = 0; i < consumedField0 && queue.Count > 0; i++) { queue.Dequeue(); } }
                    return ret;

                }

            }

            public class ControllerTransferEvent
            {
                public static readonly byte[] Selector = new byte[4] { 52, 110, 167, 149 };
                public const string Signature = "ControllerTransfer((address,address,address,uint256,byte,byte[],byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1644ControllerTransferEvent Field0 { get; set; }

                public static ControllerTransferEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new ControllerTransferEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1644ControllerTransferEvent.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class ControllerRedeemEvent
            {
                public static readonly byte[] Selector = new byte[4] { 13, 238, 20, 245 };
                public const string Signature = "ControllerRedeem((address,address,uint256,byte,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1644ControllerRedeemEvent Field0 { get; set; }

                public static ControllerRedeemEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new ControllerRedeemEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1644ControllerRedeemEvent.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class DocumentUpdatedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 45, 192, 60, 54 };
                public const string Signature = "DocumentUpdated((byte[],string,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1643DocumentUpdatedEvent Field0 { get; set; }

                public static DocumentUpdatedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new DocumentUpdatedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1643DocumentUpdatedEvent.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class DocumentRemovedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 174, 122, 79, 160 };
                public const string Signature = "DocumentRemoved((byte[],string,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1643DocumentRemovedEvent Field0 { get; set; }

                public static DocumentRemovedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new DocumentRemovedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1643DocumentRemovedEvent.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class IssueEvent
            {
                public static readonly byte[] Selector = new byte[4] { 250, 68, 59, 27 };
                public const string Signature = "Issue((address,address,uint256,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1410PartitionIssue Field0 { get; set; }

                public static IssueEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new IssueEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1410PartitionIssue.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class IssueEvent2
            {
                public static readonly byte[] Selector = new byte[4] { 242, 233, 152, 175 };
                public const string Signature = "Issue((address,uint256,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1594IssueEvent Field0 { get; set; }

                public static IssueEvent2 Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new IssueEvent2();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1594IssueEvent.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class RedeemEvent
            {
                public static readonly byte[] Selector = new byte[4] { 215, 252, 74, 152 };
                public const string Signature = "Redeem((address,uint256,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1594RedeemEvent Field0 { get; set; }

                public static RedeemEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new RedeemEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1594RedeemEvent.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class TransferEvent
            {
                public static readonly byte[] Selector = new byte[4] { 32, 107, 121, 64 };
                public const string Signature = "Transfer((address,address,address,uint256,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1410PartitionTransfer Field0 { get; set; }

                public static TransferEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new TransferEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1410PartitionTransfer.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class Arc200TransferEvent
            {
                public static readonly byte[] Selector = new byte[4] { 121, 131, 195, 92 };
                public const string Signature = "arc200_Transfer(address,address,uint256)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address From { get; set; }
                public Algorand.Address To { get; set; }
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Value { get; set; }

                public static Arc200TransferEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc200TransferEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFrom = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vFrom.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFrom = vFrom.ToValue();
                    if (valueFrom is Algorand.Address vFromValue) { ret.From = vFromValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTo = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vTo.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTo = vTo.ToValue();
                    if (valueTo is Algorand.Address vToValue) { ret.To = vToValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vValue = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    count = vValue.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueValue = vValue.ToValue();
                    if (valueValue is AVM.ClientGenerator.ABI.ARC4.Types.UInt256 vValueValue) { ret.Value = vValueValue; }
                    return ret;

                }

            }

            public class Arc200ApprovalEvent
            {
                public static readonly byte[] Selector = new byte[4] { 25, 105, 248, 101 };
                public const string Signature = "arc200_Approval(address,address,uint256)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address Owner { get; set; }
                public Algorand.Address Spender { get; set; }
                public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 Value { get; set; }

                public static Arc200ApprovalEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc200ApprovalEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueOwner = vOwner.ToValue();
                    if (valueOwner is Algorand.Address vOwnerValue) { ret.Owner = vOwnerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSpender = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vSpender.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueSpender = vSpender.ToValue();
                    if (valueSpender is Algorand.Address vSpenderValue) { ret.Spender = vSpenderValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vValue = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
                    count = vValue.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueValue = vValue.ToValue();
                    if (valueValue is AVM.ClientGenerator.ABI.ARC4.Types.UInt256 vValueValue) { ret.Value = vValueValue; }
                    return ret;

                }

            }

            public class RedeemEvent2
            {
                public static readonly byte[] Selector = new byte[4] { 92, 39, 180, 252 };
                public const string Signature = "Redeem((address,address,uint256,byte[]))";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Structs.Arc1410PartitionRedeem Field0 { get; set; }

                public static RedeemEvent2 Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new RedeemEvent2();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    var indexField0 = queue.Dequeue() * 256 + queue.Dequeue();
                    ret.Field0 = Structs.Arc1410PartitionRedeem.Parse(eventData.Skip(indexField0).ToArray());
                    return ret;

                }

            }

            public class Arc88OwnershipTransferredEvent
            {
                public static readonly byte[] Selector = new byte[4] { 67, 85, 210, 173 };
                public const string Signature = "arc88_OwnershipTransferred(address,address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address PreviousOwner { get; set; }
                public Algorand.Address NewOwner { get; set; }

                public static Arc88OwnershipTransferredEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc88OwnershipTransferredEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPreviousOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPreviousOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePreviousOwner = vPreviousOwner.ToValue();
                    if (valuePreviousOwner is Algorand.Address vPreviousOwnerValue) { ret.PreviousOwner = vPreviousOwnerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNewOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vNewOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueNewOwner = vNewOwner.ToValue();
                    if (valueNewOwner is Algorand.Address vNewOwnerValue) { ret.NewOwner = vNewOwnerValue; }
                    return ret;

                }

            }

            public class Arc88OwnershipRenouncedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 52, 106, 161, 102 };
                public const string Signature = "arc88_OwnershipRenounced(address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address PreviousOwner { get; set; }

                public static Arc88OwnershipRenouncedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc88OwnershipRenouncedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPreviousOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPreviousOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePreviousOwner = vPreviousOwner.ToValue();
                    if (valuePreviousOwner is Algorand.Address vPreviousOwnerValue) { ret.PreviousOwner = vPreviousOwnerValue; }
                    return ret;

                }

            }

            public class Arc88OwnershipTransferRequestedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 22, 191, 31, 145 };
                public const string Signature = "arc88_OwnershipTransferRequested(address,address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address PreviousOwner { get; set; }
                public Algorand.Address PendingOwner { get; set; }

                public static Arc88OwnershipTransferRequestedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc88OwnershipTransferRequestedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPreviousOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPreviousOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePreviousOwner = vPreviousOwner.ToValue();
                    if (valuePreviousOwner is Algorand.Address vPreviousOwnerValue) { ret.PreviousOwner = vPreviousOwnerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPendingOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPendingOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePendingOwner = vPendingOwner.ToValue();
                    if (valuePendingOwner is Algorand.Address vPendingOwnerValue) { ret.PendingOwner = vPendingOwnerValue; }
                    return ret;

                }

            }

            public class Arc88OwnershipTransferAcceptedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 247, 227, 107, 55 };
                public const string Signature = "arc88_OwnershipTransferAccepted(address,address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public Algorand.Address PreviousOwner { get; set; }
                public Algorand.Address NewOwner { get; set; }

                public static Arc88OwnershipTransferAcceptedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new Arc88OwnershipTransferAcceptedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPreviousOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vPreviousOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePreviousOwner = vPreviousOwner.ToValue();
                    if (valuePreviousOwner is Algorand.Address vPreviousOwnerValue) { ret.PreviousOwner = vPreviousOwnerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNewOwner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vNewOwner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueNewOwner = vNewOwner.ToValue();
                    if (valueNewOwner is Algorand.Address vNewOwnerValue) { ret.NewOwner = vNewOwnerValue; }
                    return ret;

                }

            }

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="new_controller"> </param>
        public async Task Arc1644SetController(Algorand.Address new_controller, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 4, 84, 114, 208 };
            var new_controllerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); new_controllerAbi.From(new_controller);

            var result = await base.CallApp(new List<object> { abiHandle, new_controllerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1644SetController_Transactions(Algorand.Address new_controller, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 4, 84, 114, 208 };
            var new_controllerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); new_controllerAbi.From(new_controller);

            return await base.MakeTransactionList(new List<object> { abiHandle, new_controllerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="flag"> </param>
        public async Task Arc1644SetControllable(bool flag, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 125, 121, 4, 164 };
            var flagAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); flagAbi.From(flag);

            var result = await base.CallApp(new List<object> { abiHandle, flagAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1644SetControllable_Transactions(bool flag, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 125, 121, 4, 164 };
            var flagAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); flagAbi.From(flag);

            return await base.MakeTransactionList(new List<object> { abiHandle, flagAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="flag"> </param>
        public async Task Arc1644SetRequireJustification(bool flag, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 230, 244, 248, 97 };
            var flagAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); flagAbi.From(flag);

            var result = await base.CallApp(new List<object> { abiHandle, flagAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1644SetRequireJustification_Transactions(bool flag, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 230, 244, 248, 97 };
            var flagAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); flagAbi.From(flag);

            return await base.MakeTransactionList(new List<object> { abiHandle, flagAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="interval"> </param>
        public async Task Arc1644SetMinActionInterval(ulong interval, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 46, 189, 45, 52 };
            var intervalAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); intervalAbi.From(interval);

            var result = await base.CallApp(new List<object> { abiHandle, intervalAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1644SetMinActionInterval_Transactions(ulong interval, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 46, 189, 45, 52 };
            var intervalAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); intervalAbi.From(interval);

            return await base.MakeTransactionList(new List<object> { abiHandle, intervalAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<ulong> Arc1644IsControllable(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 238, 111, 45, 14 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1644IsControllable_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 238, 111, 45, 14 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        /// <param name="operator_data"> </param>
        public async Task<ulong> Arc1644ControllerTransfer(Algorand.Address from, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, byte[] operator_data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 29, 92, 122, 23 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);
            var operator_dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); operator_dataAbi.From(operator_data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, toAbi, amount, dataAbi, operator_dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1644ControllerTransfer_Transactions(Algorand.Address from, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, byte[] operator_data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 29, 92, 122, 23 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);
            var operator_dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); operator_dataAbi.From(operator_data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, toAbi, amount, dataAbi, operator_dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="amount"> </param>
        /// <param name="operator_data"> </param>
        public async Task<ulong> Arc1644ControllerRedeem(Algorand.Address from, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] operator_data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 229, 122, 110, 24 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var operator_dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); operator_dataAbi.From(operator_data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, amount, operator_dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1644ControllerRedeem_Transactions(Algorand.Address from, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] operator_data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 229, 122, 110, 24 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var operator_dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); operator_dataAbi.From(operator_data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, amount, operator_dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="name"> </param>
        /// <param name="uri"> </param>
        /// <param name="hash"> </param>
        public async Task Arc1643SetDocument(byte[] name, string uri, byte[] hash, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 17, 203, 54, 245 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);
            var uriAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); uriAbi.From(uri);
            var hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); hashAbi.From(hash);

            var result = await base.CallApp(new List<object> { abiHandle, nameAbi, uriAbi, hashAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1643SetDocument_Transactions(byte[] name, string uri, byte[] hash, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 17, 203, 54, 245 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);
            var uriAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); uriAbi.From(uri);
            var hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); hashAbi.From(hash);

            return await base.MakeTransactionList(new List<object> { abiHandle, nameAbi, uriAbi, hashAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="name"> </param>
        public async Task<Structs.Arc1643DocumentRecord> Arc1643GetDocument(byte[] name, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 177, 109, 122, 140 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);

            var result = await base.SimApp(new List<object> { abiHandle, nameAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.Arc1643DocumentRecord.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Arc1643GetDocument_Transactions(byte[] name, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 177, 109, 122, 140 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);

            return await base.MakeTransactionList(new List<object> { abiHandle, nameAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="name"> </param>
        public async Task Arc1643RemoveDocument(byte[] name, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 167, 203, 52, 130 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);

            var result = await base.CallApp(new List<object> { abiHandle, nameAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1643RemoveDocument_Transactions(byte[] name, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 167, 203, 52, 130 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);

            return await base.MakeTransactionList(new List<object> { abiHandle, nameAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<byte[][]> Arc1643GetAllDocuments(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 236, 182, 54, 200 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            throw new Exception("Conversion not implemented"); // <unknown return conversion>

        }

        public async Task<List<Transaction>> Arc1643GetAllDocuments_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 236, 182, 54, 200 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="flag"> </param>
        public async Task Arc1594SetIssuable(bool flag, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 101, 177, 104, 42 };
            var flagAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); flagAbi.From(flag);

            var result = await base.CallApp(new List<object> { abiHandle, flagAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1594SetIssuable_Transactions(bool flag, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 101, 177, 104, 42 };
            var flagAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Bool(); flagAbi.From(flag);

            return await base.MakeTransactionList(new List<object> { abiHandle, flagAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task Arc1594Issue(Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 1, 48, 89, 155 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1594Issue_Transactions(Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 1, 48, 89, 155 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task Arc1594RedeemFrom(Algorand.Address from, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 20, 43, 95, 203 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1594RedeemFrom_Transactions(Algorand.Address from, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 20, 43, 95, 203 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task Arc1594Redeem(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 248, 131, 142, 185 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1594Redeem_Transactions(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 248, 131, 142, 185 };
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task<bool> Arc1594TransferWithData(Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 49, 136, 43, 250 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1594TransferWithData_Transactions(Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 49, 136, 43, 250 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task<bool> Arc1594TransferFromWithData(Algorand.Address from, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 169, 204, 161, 111 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1594TransferFromWithData_Transactions(Algorand.Address from, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 169, 204, 161, 111 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<bool> Arc1594IsIssuable(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 38, 101, 151, 192 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1594IsIssuable_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 38, 101, 151, 192 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="partition"> </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt256> Arc1410BalanceOfPartition(Algorand.Address holder, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 53, 248, 19, 95 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            var result = await base.SimApp(new List<object> { abiHandle, holderAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc1410BalanceOfPartition_Transactions(Algorand.Address holder, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 53, 248, 19, 95 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="to"> </param>
        /// <param name="value"> </param>
        public async Task<bool> Arc200Transfer(Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 218, 112, 37, 185 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);

            var result = await base.CallApp(new List<object> { abiHandle, toAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc200Transfer_Transactions(Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 218, 112, 37, 185 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);

            return await base.MakeTransactionList(new List<object> { abiHandle, toAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Transfer an amount of tokens from partition to receiver. Sender must be msg.sender or authorized operator.
        ///</summary>
        /// <param name="partition"> </param>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task<Algorand.Address> Arc1410TransferByPartition(Algorand.Address partition, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 63, 37, 103, 19 };
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, partitionAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Arc1410TransferByPartition_Transactions(Algorand.Address partition, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 63, 37, 103, 19 };
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, partitionAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="page"> </param>
        public async Task<Algorand.Address[]> Arc1410PartitionsOf(Algorand.Address holder, ulong page, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 149, 180, 249, 227 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var pageAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); pageAbi.From(page);

            var result = await base.CallApp(new List<object> { abiHandle, holderAbi, pageAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            throw new Exception("Conversion not implemented"); // <unknown return conversion>

        }

        public async Task<List<Transaction>> Arc1410PartitionsOf_Transactions(Algorand.Address holder, ulong page, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 149, 180, 249, 227 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var pageAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); pageAbi.From(page);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, pageAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="operator"> </param>
        /// <param name="partition"> </param>
        public async Task<bool> Arc1410IsOperator(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 128, 204, 73, 171 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            var result = await base.SimApp(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1410IsOperator_Transactions(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 128, 204, 73, 171 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="operator"> </param>
        /// <param name="partition"> </param>
        public async Task Arc1410AuthorizeOperator(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 7, 150, 33, 101 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            var result = await base.CallApp(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1410AuthorizeOperator_Transactions(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 7, 150, 33, 101 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="operator"> </param>
        /// <param name="partition"> </param>
        public async Task Arc1410RevokeOperator(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 231, 137, 97, 218 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            var result = await base.CallApp(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1410RevokeOperator_Transactions(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 231, 137, 97, 218 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="partition"> </param>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task<Algorand.Address> Arc1410OperatorTransferByPartition(Algorand.Address from, Algorand.Address partition, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 253, 148, 128, 215 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, partitionAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Arc1410OperatorTransferByPartition_Transactions(Algorand.Address from, Algorand.Address partition, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 253, 148, 128, 215 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, partitionAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="partition"> </param>
        /// <param name="to"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task<Structs.Arc1410CanTransferByPartitionReturn> Arc1410CanTransferByPartition(Algorand.Address from, Algorand.Address partition, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 177, 177, 214, 154 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, partitionAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.Arc1410CanTransferByPartitionReturn.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> Arc1410CanTransferByPartition_Transactions(Algorand.Address from, Algorand.Address partition, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 177, 177, 214, 154 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, partitionAbi, toAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="operator"> </param>
        /// <param name="partition"> </param>
        /// <param name="amount"> </param>
        public async Task Arc1410AuthorizeOperatorByPortion(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 193, 190, 215, 137 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            var result = await base.CallApp(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi, amount }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1410AuthorizeOperatorByPortion_Transactions(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 193, 190, 215, 137 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi, amount }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="holder"> </param>
        /// <param name="operator"> </param>
        /// <param name="partition"> </param>
        public async Task<bool> Arc1410IsOperatorByPortion(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 59, 254, 24, 51 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            var result = await base.SimApp(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc1410IsOperatorByPortion_Transactions(Algorand.Address holder, Algorand.Address @operator, Algorand.Address partition, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 59, 254, 24, 51 };
            var holderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); holderAbi.From(holder);
            var operatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); operatorAbi.From(@operator);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);

            return await base.MakeTransactionList(new List<object> { abiHandle, holderAbi, operatorAbi, partitionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="to"> </param>
        /// <param name="partition"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task Arc1410IssueByPartition(Algorand.Address to, Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 89, 156, 209, 165 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, toAbi, partitionAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1410IssueByPartition_Transactions(Algorand.Address to, Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 89, 156, 209, 165 };
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, toAbi, partitionAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="partition"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task Arc1410RedeemByPartition(Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 109, 233, 65, 102 };
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, partitionAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1410RedeemByPartition_Transactions(Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 109, 233, 65, 102 };
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, partitionAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="from"> </param>
        /// <param name="partition"> </param>
        /// <param name="amount"> </param>
        /// <param name="data"> </param>
        public async Task Arc1410OperatorRedeemByPartition(Algorand.Address from, Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 40, 240, 35, 215 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, partitionAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc1410OperatorRedeemByPartition_Transactions(Algorand.Address from, Algorand.Address partition, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 amount, byte[] data, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 40, 240, 35, 215 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var partitionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); partitionAbi.From(partition);
            var dataAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); dataAbi.From(data);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, partitionAbi, amount, dataAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="name"> </param>
        /// <param name="symbol"> </param>
        /// <param name="decimals"> </param>
        /// <param name="totalSupply"> </param>
        public async Task<bool> Bootstrap(byte[] name, byte[] symbol, byte decimals, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 totalSupply, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 151, 83, 130, 226 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);
            var symbolAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); symbolAbi.From(symbol);
            var decimalsAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); decimalsAbi.From(decimals);

            var result = await base.CallApp(new List<object> { abiHandle, nameAbi, symbolAbi, decimalsAbi, totalSupply }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Bootstrap_Transactions(byte[] name, byte[] symbol, byte decimals, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 totalSupply, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 151, 83, 130, 226 };
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); nameAbi.From(name);
            var symbolAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); symbolAbi.From(symbol);
            var decimalsAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); decimalsAbi.From(decimals);

            return await base.MakeTransactionList(new List<object> { abiHandle, nameAbi, symbolAbi, decimalsAbi, totalSupply }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Returns the name of the token
        ///</summary>
        public async Task<byte[]> Arc200Name(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 101, 125, 19, 236 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(32, "byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc200Name_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 101, 125, 19, 236 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Returns the symbol of the token
        ///</summary>
        public async Task<byte[]> Arc200Symbol(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 182, 174, 26, 37 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(8, "byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc200Symbol_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 182, 174, 26, 37 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Returns the decimals of the token
        ///</summary>
        public async Task<byte> Arc200Decimals(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 132, 236, 19, 213 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Byte();
            returnValueObj.Decode(lastLogReturnData);
            return ReverseIfLittleEndian(lastLogReturnData)[0];

        }

        public async Task<List<Transaction>> Arc200Decimals_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 132, 236, 19, 213 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Returns the total supply of the token
        ///</summary>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt256> Arc200TotalSupply(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 236, 153, 96, 65 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc200TotalSupply_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 236, 153, 96, 65 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Returns the current balance of the owner of the token
        ///</summary>
        /// <param name="owner">The address of the owner of the token </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt256> Arc200BalanceOf(Algorand.Address owner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 130, 229, 115, 196 };
            var ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); ownerAbi.From(owner);

            var result = await base.SimApp(new List<object> { abiHandle, ownerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc200BalanceOf_Transactions(Algorand.Address owner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 130, 229, 115, 196 };
            var ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); ownerAbi.From(owner);

            return await base.MakeTransactionList(new List<object> { abiHandle, ownerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Transfers tokens from source to destination as approved spender
        ///</summary>
        /// <param name="from">The source of the transfer </param>
        /// <param name="to">The destination of the transfer </param>
        /// <param name="value">Amount of tokens to transfer </param>
        public async Task<bool> Arc200TransferFrom(Algorand.Address from, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 74, 150, 143, 143 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);

            var result = await base.CallApp(new List<object> { abiHandle, fromAbi, toAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc200TransferFrom_Transactions(Algorand.Address from, Algorand.Address to, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 74, 150, 143, 143 };
            var fromAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); fromAbi.From(from);
            var toAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); toAbi.From(to);

            return await base.MakeTransactionList(new List<object> { abiHandle, fromAbi, toAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Approve spender for a token
        ///</summary>
        /// <param name="spender">Who is allowed to take tokens on owner's behalf </param>
        /// <param name="value">Amount of tokens to be taken by spender </param>
        public async Task<bool> Arc200Approve(Algorand.Address spender, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 181, 66, 33, 37 };
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            var result = await base.CallApp(new List<object> { abiHandle, spenderAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc200Approve_Transactions(Algorand.Address spender, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 181, 66, 33, 37 };
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            return await base.MakeTransactionList(new List<object> { abiHandle, spenderAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Returns the current allowance of the spender of the tokens of the owner
        ///</summary>
        /// <param name="owner">Owner's account </param>
        /// <param name="spender">Who is allowed to take tokens on owner's behalf </param>
        public async Task<AVM.ClientGenerator.ABI.ARC4.Types.UInt256> Arc200Allowance(Algorand.Address owner, Algorand.Address spender, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 187, 179, 25, 243 };
            var ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); ownerAbi.From(owner);
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            var result = await base.SimApp(new List<object> { abiHandle, ownerAbi, spenderAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj;

        }

        public async Task<List<Transaction>> Arc200Allowance_Transactions(Algorand.Address owner, Algorand.Address spender, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 187, 179, 25, 243 };
            var ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); ownerAbi.From(owner);
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            return await base.MakeTransactionList(new List<object> { abiHandle, ownerAbi, spenderAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<Algorand.Address> Arc88Owner(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 7, 2, 101, 78 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Arc88Owner_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 7, 2, 101, 78 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="query"> </param>
        public async Task<bool> Arc88IsOwner(Algorand.Address query, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 208, 21, 114, 78 };
            var queryAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); queryAbi.From(query);

            var result = await base.SimApp(new List<object> { abiHandle, queryAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc88IsOwner_Transactions(Algorand.Address query, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 208, 21, 114, 78 };
            var queryAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); queryAbi.From(query);

            return await base.MakeTransactionList(new List<object> { abiHandle, queryAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Explicit initialization override (creation group recommended). Fails if already initialized.
        ///</summary>
        /// <param name="new_owner"> </param>
        public async Task Arc88InitializeOwner(Algorand.Address new_owner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 2, 159, 236, 192 };
            var new_ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); new_ownerAbi.From(new_owner);

            var result = await base.CallApp(new List<object> { abiHandle, new_ownerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc88InitializeOwner_Transactions(Algorand.Address new_owner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 2, 159, 236, 192 };
            var new_ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); new_ownerAbi.From(new_owner);

            return await base.MakeTransactionList(new List<object> { abiHandle, new_ownerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="new_owner"> </param>
        public async Task Arc88TransferOwnership(Algorand.Address new_owner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 115, 73, 51, 78 };
            var new_ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); new_ownerAbi.From(new_owner);

            var result = await base.CallApp(new List<object> { abiHandle, new_ownerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc88TransferOwnership_Transactions(Algorand.Address new_owner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 115, 73, 51, 78 };
            var new_ownerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); new_ownerAbi.From(new_owner);

            return await base.MakeTransactionList(new List<object> { abiHandle, new_ownerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task Arc88RenounceOwnership(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 219, 124, 130, 239 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc88RenounceOwnership_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 219, 124, 130, 239 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="pending"> </param>
        public async Task Arc88TransferOwnershipRequest(Algorand.Address pending, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 253, 44, 44, 110 };
            var pendingAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); pendingAbi.From(pending);

            var result = await base.CallApp(new List<object> { abiHandle, pendingAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc88TransferOwnershipRequest_Transactions(Algorand.Address pending, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 253, 44, 44, 110 };
            var pendingAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); pendingAbi.From(pending);

            return await base.MakeTransactionList(new List<object> { abiHandle, pendingAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task Arc88AcceptOwnership(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 66, 165, 240, 101 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc88AcceptOwnership_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 66, 165, 240, 101 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task Arc88CancelOwnershipRequest(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 173, 79, 104, 234 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc88CancelOwnershipRequest_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 173, 79, 104, 234 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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

        protected override ulong? ExtraProgramPages { get; set; } = 2;
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiQXJjMTY0NCIsImRlc2MiOm51bGwsIm5ldHdvcmtzIjp7fSwic3RydWN0cyI6eyJBcHByb3ZhbFN0cnVjdCI6W3sibmFtZSI6ImFwcHJvdmFsQW1vdW50IiwidHlwZSI6InVpbnQyNTYifSx7Im5hbWUiOiJvd25lciIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoic3BlbmRlciIsInR5cGUiOiJhZGRyZXNzIn1dLCJhcmMxNDEwX0hvbGRpbmdQYXJ0aXRpb25zUGFnaW5hdGVkS2V5IjpbeyJuYW1lIjoiaG9sZGVyIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJwYWdlIiwidHlwZSI6InVpbnQ2NCJ9XSwiYXJjMTQxMF9PcGVyYXRvcktleSI6W3sibmFtZSI6ImhvbGRlciIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoib3BlcmF0b3IiLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6InBhcnRpdGlvbiIsInR5cGUiOiJhZGRyZXNzIn1dLCJhcmMxNDEwX09wZXJhdG9yUG9ydGlvbktleSI6W3sibmFtZSI6ImhvbGRlciIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoib3BlcmF0b3IiLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6InBhcnRpdGlvbiIsInR5cGUiOiJhZGRyZXNzIn1dLCJhcmMxNDEwX1BhcnRpdGlvbktleSI6W3sibmFtZSI6ImhvbGRlciIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoicGFydGl0aW9uIiwidHlwZSI6ImFkZHJlc3MifV0sImFyYzE0MTBfY2FuX3RyYW5zZmVyX2J5X3BhcnRpdGlvbl9yZXR1cm4iOlt7Im5hbWUiOiJjb2RlIiwidHlwZSI6ImJ5dGUifSx7Im5hbWUiOiJzdGF0dXMiLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoicmVjZWl2ZXJQYXJ0aXRpb24iLCJ0eXBlIjoiYWRkcmVzcyJ9XSwiYXJjMTQxMF9wYXJ0aXRpb25faXNzdWUiOlt7Im5hbWUiOiJ0byIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoicGFydGl0aW9uIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJhbW91bnQiLCJ0eXBlIjoidWludDI1NiJ9LHsibmFtZSI6ImRhdGEiLCJ0eXBlIjoiYnl0ZVtdIn1dLCJhcmMxNDEwX3BhcnRpdGlvbl9yZWRlZW0iOlt7Im5hbWUiOiJmcm9tIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJwYXJ0aXRpb24iLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6ImFtb3VudCIsInR5cGUiOiJ1aW50MjU2In0seyJuYW1lIjoiZGF0YSIsInR5cGUiOiJieXRlW10ifV0sImFyYzE0MTBfcGFydGl0aW9uX3RyYW5zZmVyIjpbeyJuYW1lIjoiZnJvbSIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoidG8iLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6InBhcnRpdGlvbiIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoiYW1vdW50IiwidHlwZSI6InVpbnQyNTYifSx7Im5hbWUiOiJkYXRhIiwidHlwZSI6ImJ5dGVbXSJ9XSwiYXJjMTU5NF9pc3N1ZV9ldmVudCI6W3sibmFtZSI6InRvIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJhbW91bnQiLCJ0eXBlIjoidWludDI1NiJ9LHsibmFtZSI6ImRhdGEiLCJ0eXBlIjoiYnl0ZVtdIn1dLCJhcmMxNTk0X3JlZGVlbV9ldmVudCI6W3sibmFtZSI6ImZyb20iLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6ImFtb3VudCIsInR5cGUiOiJ1aW50MjU2In0seyJuYW1lIjoiZGF0YSIsInR5cGUiOiJieXRlW10ifV0sImFyYzE2NDNfZG9jdW1lbnRfcmVjb3JkIjpbeyJuYW1lIjoidXJpIiwidHlwZSI6InN0cmluZyJ9LHsibmFtZSI6Imhhc2giLCJ0eXBlIjoiYnl0ZVtdIn0seyJuYW1lIjoidGltZXN0YW1wIiwidHlwZSI6InVpbnQ2NCJ9XSwiYXJjMTY0M19kb2N1bWVudF9yZW1vdmVkX2V2ZW50IjpbeyJuYW1lIjoibmFtZSIsInR5cGUiOiJieXRlW10ifSx7Im5hbWUiOiJ1cmkiLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoiaGFzaCIsInR5cGUiOiJieXRlW10ifV0sImFyYzE2NDNfZG9jdW1lbnRfdXBkYXRlZF9ldmVudCI6W3sibmFtZSI6Im5hbWUiLCJ0eXBlIjoiYnl0ZVtdIn0seyJuYW1lIjoidXJpIiwidHlwZSI6InN0cmluZyJ9LHsibmFtZSI6Imhhc2giLCJ0eXBlIjoiYnl0ZVtdIn1dLCJhcmMxNjQ0X2NvbnRyb2xsZXJfY2hhbmdlZF9ldmVudCI6W3sibmFtZSI6Im9sZCIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoibmV1IiwidHlwZSI6ImFkZHJlc3MifV0sImFyYzE2NDRfY29udHJvbGxlcl9yZWRlZW1fZXZlbnQiOlt7Im5hbWUiOiJjb250cm9sbGVyIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJmcm9tIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJhbW91bnQiLCJ0eXBlIjoidWludDI1NiJ9LHsibmFtZSI6ImNvZGUiLCJ0eXBlIjoiYnl0ZSJ9LHsibmFtZSI6Im9wZXJhdG9yX2RhdGEiLCJ0eXBlIjoiYnl0ZVtdIn1dLCJhcmMxNjQ0X2NvbnRyb2xsZXJfdHJhbnNmZXJfZXZlbnQiOlt7Im5hbWUiOiJjb250cm9sbGVyIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJmcm9tIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJ0byIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoiYW1vdW50IiwidHlwZSI6InVpbnQyNTYifSx7Im5hbWUiOiJjb2RlIiwidHlwZSI6ImJ5dGUifSx7Im5hbWUiOiJkYXRhIiwidHlwZSI6ImJ5dGVbXSJ9LHsibmFtZSI6Im9wZXJhdG9yX2RhdGEiLCJ0eXBlIjoiYnl0ZVtdIn1dfSwiTWV0aG9kcyI6W3sibmFtZSI6ImFyYzE2NDRfc2V0X2NvbnRyb2xsZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5ld19jb250cm9sbGVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJDb250cm9sbGVyQ2hhbmdlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyxhZGRyZXNzKSIsInN0cnVjdCI6ImFyYzE2NDRfY29udHJvbGxlcl9jaGFuZ2VkX2V2ZW50IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNjQ0X3NldF9jb250cm9sbGFibGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZsYWciLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTY0NF9zZXRfcmVxdWlyZV9qdXN0aWZpY2F0aW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmbGFnIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE2NDRfc2V0X21pbl9hY3Rpb25faW50ZXJ2YWwiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiaW50ZXJ2YWwiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTY0NF9pc19jb250cm9sbGFibGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE2NDRfY29udHJvbGxlcl90cmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZnJvbSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiYW1vdW50IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJvcGVyYXRvcl9kYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IkNvbnRyb2xsZXJUcmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyxhZGRyZXNzLGFkZHJlc3MsdWludDI1NixieXRlLGJ5dGVbXSxieXRlW10pIiwic3RydWN0IjoiYXJjMTY0NF9jb250cm9sbGVyX3RyYW5zZmVyX2V2ZW50IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNjQ0X2NvbnRyb2xsZXJfcmVkZWVtIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiYW1vdW50IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJvcGVyYXRvcl9kYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IkNvbnRyb2xsZXJSZWRlZW0iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGUsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE2NDRfY29udHJvbGxlcl9yZWRlZW1fZXZlbnQiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE2NDNfc2V0X2RvY3VtZW50IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5hbWUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InN0cmluZyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InVyaSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiaGFzaCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiRG9jdW1lbnRVcGRhdGVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihieXRlW10sc3RyaW5nLGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNjQzX2RvY3VtZW50X3VwZGF0ZWRfZXZlbnQiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE2NDNfZ2V0X2RvY3VtZW50IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5hbWUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHN0cmluZyxieXRlW10sdWludDY0KSIsInN0cnVjdCI6ImFyYzE2NDNfZG9jdW1lbnRfcmVjb3JkIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNjQzX3JlbW92ZV9kb2N1bWVudCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJuYW1lIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJEb2N1bWVudFJlbW92ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGJ5dGVbXSxzdHJpbmcsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE2NDNfZG9jdW1lbnRfcmVtb3ZlZF9ldmVudCIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTY0M19nZXRfYWxsX2RvY3VtZW50cyIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW11bXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNTk0X3NldF9pc3N1YWJsZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZmxhZyIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNTk0X2lzc3VlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0byIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImFtb3VudCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiSXNzdWUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNDEwX3BhcnRpdGlvbl9pc3N1ZSIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJJc3N1ZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNTk0X2lzc3VlX2V2ZW50IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNTk0X3JlZGVlbUZyb20iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZyb20iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhbW91bnQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IlJlZGVlbSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNTk0X3JlZGVlbV9ldmVudCIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTU5NF9yZWRlZW0iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImFtb3VudCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiUmVkZWVtIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE1OTRfcmVkZWVtX2V2ZW50IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNTk0X3RyYW5zZmVyX3dpdGhfZGF0YSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhbW91bnQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IlRyYW5zZmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNDEwX3BhcnRpdGlvbl90cmFuc2ZlciIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmMyMDBfVHJhbnNmZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZyb20iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNTk0X3RyYW5zZmVyX2Zyb21fd2l0aF9kYXRhIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhbW91bnQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzIwMF9BcHByb3ZhbCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3duZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3BlbmRlciIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjMjAwX1RyYW5zZmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InZhbHVlIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTU5NF9pc19pc3N1YWJsZSIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfYmFsYW5jZV9vZl9wYXJ0aXRpb24iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImhvbGRlciIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InBhcnRpdGlvbiIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzIwMF90cmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiVHJhbnNmZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGFkZHJlc3MsYWRkcmVzcyxhZGRyZXNzLHVpbnQyNTYsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE0MTBfcGFydGl0aW9uX3RyYW5zZmVyIiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzIwMF9UcmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZnJvbSIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0byIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfdHJhbnNmZXJfYnlfcGFydGl0aW9uIiwiZGVzYyI6IlRyYW5zZmVyIGFuIGFtb3VudCBvZiB0b2tlbnMgZnJvbSBwYXJ0aXRpb24gdG8gcmVjZWl2ZXIuIFNlbmRlciBtdXN0IGJlIG1zZy5zZW5kZXIgb3IgYXV0aG9yaXplZCBvcGVyYXRvci4iLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InBhcnRpdGlvbiIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiYW1vdW50IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJUcmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyxhZGRyZXNzLGFkZHJlc3MsdWludDI1NixieXRlW10pIiwic3RydWN0IjoiYXJjMTQxMF9wYXJ0aXRpb25fdHJhbnNmZXIiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfcGFydGl0aW9uc19vZiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiaG9sZGVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwYWdlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImFkZHJlc3NbXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTQxMF9pc19vcGVyYXRvciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiaG9sZGVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3BlcmF0b3IiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwYXJ0aXRpb24iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNDEwX2F1dGhvcml6ZV9vcGVyYXRvciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiaG9sZGVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3BlcmF0b3IiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwYXJ0aXRpb24iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTQxMF9yZXZva2Vfb3BlcmF0b3IiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImhvbGRlciIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im9wZXJhdG9yIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfb3BlcmF0b3JfdHJhbnNmZXJfYnlfcGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhbW91bnQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IlRyYW5zZmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNDEwX3BhcnRpdGlvbl90cmFuc2ZlciIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMTQxMF9jYW5fdHJhbnNmZXJfYnlfcGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhbW91bnQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKGJ5dGUsc3RyaW5nLGFkZHJlc3MpIiwic3RydWN0IjoiYXJjMTQxMF9jYW5fdHJhbnNmZXJfYnlfcGFydGl0aW9uX3JldHVybiIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfYXV0aG9yaXplX29wZXJhdG9yX2J5X3BvcnRpb24iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImhvbGRlciIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im9wZXJhdG9yIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiYW1vdW50IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfaXNfb3BlcmF0b3JfYnlfcG9ydGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiaG9sZGVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3BlcmF0b3IiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwYXJ0aXRpb24iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMxNDEwX2lzc3VlX2J5X3BhcnRpdGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwYXJ0aXRpb24iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhbW91bnQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRhdGEiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6Iklzc3VlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsdWludDI1NixieXRlW10pIiwic3RydWN0IjoiYXJjMTQxMF9wYXJ0aXRpb25faXNzdWUiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfcmVkZWVtX2J5X3BhcnRpdGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicGFydGl0aW9uIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiYW1vdW50IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkYXRhIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJSZWRlZW0iLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNDEwX3BhcnRpdGlvbl9yZWRlZW0iLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzE0MTBfb3BlcmF0b3JfcmVkZWVtX2J5X3BhcnRpdGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZnJvbSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InBhcnRpdGlvbiIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImFtb3VudCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZGF0YSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiUmVkZWVtIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsdWludDI1NixieXRlW10pIiwic3RydWN0IjoiYXJjMTQxMF9wYXJ0aXRpb25fcmVkZWVtIiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJib290c3RyYXAiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoibmFtZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3ltYm9sIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImRlY2ltYWxzIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidG90YWxTdXBwbHkiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzIwMF9UcmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZnJvbSIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0byIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzIwMF9uYW1lIiwiZGVzYyI6IlJldHVybnMgdGhlIG5hbWUgb2YgdGhlIHRva2VuIiwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbMzJdIiwic3RydWN0IjpudWxsLCJkZXNjIjoiVGhlIG5hbWUgb2YgdGhlIHRva2VuIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfc3ltYm9sIiwiZGVzYyI6IlJldHVybnMgdGhlIHN5bWJvbCBvZiB0aGUgdG9rZW4iLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVs4XSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlRoZSBzeW1ib2wgb2YgdGhlIHRva2VuIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfZGVjaW1hbHMiLCJkZXNjIjoiUmV0dXJucyB0aGUgZGVjaW1hbHMgb2YgdGhlIHRva2VuIiwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJkZXNjIjoiVGhlIGRlY2ltYWxzIG9mIHRoZSB0b2tlbiJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX3RvdGFsU3VwcGx5IiwiZGVzYyI6IlJldHVybnMgdGhlIHRvdGFsIHN1cHBseSBvZiB0aGUgdG9rZW4iLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlRoZSB0b3RhbCBzdXBwbHkgb2YgdGhlIHRva2VuIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfYmFsYW5jZU9mIiwiZGVzYyI6IlJldHVybnMgdGhlIGN1cnJlbnQgYmFsYW5jZSBvZiB0aGUgb3duZXIgb2YgdGhlIHRva2VuIiwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJvd25lciIsImRlc2MiOiJUaGUgYWRkcmVzcyBvZiB0aGUgb3duZXIgb2YgdGhlIHRva2VuIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJUaGUgY3VycmVudCBiYWxhbmNlIG9mIHRoZSBob2xkZXIgb2YgdGhlIHRva2VuIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfdHJhbnNmZXJGcm9tIiwiZGVzYyI6IlRyYW5zZmVycyB0b2tlbnMgZnJvbSBzb3VyY2UgdG8gZGVzdGluYXRpb24gYXMgYXBwcm92ZWQgc3BlbmRlciIsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZnJvbSIsImRlc2MiOiJUaGUgc291cmNlIG9mIHRoZSB0cmFuc2ZlciIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvIiwiZGVzYyI6IlRoZSBkZXN0aW5hdGlvbiBvZiB0aGUgdHJhbnNmZXIiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOiJBbW91bnQgb2YgdG9rZW5zIHRvIHRyYW5zZmVyIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJTdWNjZXNzIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzIwMF9BcHByb3ZhbCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3duZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3BlbmRlciIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjMjAwX1RyYW5zZmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InZhbHVlIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX2FwcHJvdmUiLCJkZXNjIjoiQXBwcm92ZSBzcGVuZGVyIGZvciBhIHRva2VuIiwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzcGVuZGVyIiwiZGVzYyI6IldobyBpcyBhbGxvd2VkIHRvIHRha2UgdG9rZW5zIG9uIG93bmVyJ3MgYmVoYWxmIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjoiQW1vdW50IG9mIHRva2VucyB0byBiZSB0YWtlbiBieSBzcGVuZGVyIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJTdWNjZXNzIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzIwMF9BcHByb3ZhbCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3duZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3BlbmRlciIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzIwMF9hbGxvd2FuY2UiLCJkZXNjIjoiUmV0dXJucyB0aGUgY3VycmVudCBhbGxvd2FuY2Ugb2YgdGhlIHNwZW5kZXIgb2YgdGhlIHRva2VucyBvZiB0aGUgb3duZXIiLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im93bmVyIiwiZGVzYyI6Ik93bmVyJ3MgYWNjb3VudCIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNwZW5kZXIiLCJkZXNjIjoiV2hvIGlzIGFsbG93ZWQgdG8gdGFrZSB0b2tlbnMgb24gb3duZXIncyBiZWhhbGYiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlRoZSByZW1haW5pbmcgYWxsb3dhbmNlIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM4OF9vd25lciIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzg4X2lzX293bmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJxdWVyeSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzg4X2luaXRpYWxpemVfb3duZXIiLCJkZXNjIjoiRXhwbGljaXQgaW5pdGlhbGl6YXRpb24gb3ZlcnJpZGUgKGNyZWF0aW9uIGdyb3VwIHJlY29tbWVuZGVkKS4gRmFpbHMgaWYgYWxyZWFkeSBpbml0aWFsaXplZC4iLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5ld19vd25lciIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM4OF90cmFuc2Zlcl9vd25lcnNoaXAiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5ld19vd25lciIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjODhfT3duZXJzaGlwVHJhbnNmZXJyZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InByZXZpb3VzX293bmVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5ld19vd25lciIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzg4X3Jlbm91bmNlX293bmVyc2hpcCIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjODhfT3duZXJzaGlwUmVub3VuY2VkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwcmV2aW91c19vd25lciIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzg4X3RyYW5zZmVyX293bmVyc2hpcF9yZXF1ZXN0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwZW5kaW5nIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmM4OF9Pd25lcnNoaXBUcmFuc2ZlclJlcXVlc3RlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicHJldmlvdXNfb3duZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicGVuZGluZ19vd25lciIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzg4X2FjY2VwdF9vd25lcnNoaXAiLCJkZXNjIjpudWxsLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzg4X093bmVyc2hpcFRyYW5zZmVyQWNjZXB0ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InByZXZpb3VzX293bmVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5ld19vd25lciIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiYXJjODhfT3duZXJzaGlwVHJhbnNmZXJyZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InByZXZpb3VzX293bmVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5ld19vd25lciIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzg4X2NhbmNlbF9vd25lcnNoaXBfcmVxdWVzdCIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fV0sInN0YXRlIjp7InNjaGVtYSI6eyJnbG9iYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjEzfSwibG9jYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjB9fSwia2V5cyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn19LCJtYXBzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfX19LCJiYXJlQWN0aW9ucyI6eyJjcmVhdGUiOlsiTm9PcCJdLCJjYWxsIjpbXX0sInNvdXJjZUluZm8iOnsiYXBwcm92YWwiOnsic291cmNlSW5mbyI6W3sicGMiOlsyNDIwLDI0NzMsMjQ5MywyNTcyLDI3MjEsMjczNCwyODI0LDI4MzcsMjk0MCwzMDIxLDMwNzAsMzEwMiwzMjI2LDMzNjgsMzU2MywzNzQ4LDM4MTEsMzg2OSwzODg0LDQwMDEsNDA5MCw0MTczLDQyMzgsNDI3Niw0MzYyLDQzNjksNDM5OSw0NDEyLDQ1MjAsNDU2Nyw0NTc0LDQ2MDcsNDYyMCw0OTA2LDUwMzhdLCJlcnJvck1lc3NhZ2UiOiJCb3ggbXVzdCBoYXZlIHZhbHVlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzgzNyw1MDM5XSwiZXJyb3JNZXNzYWdlIjoiSW5kZXggYWNjZXNzIGlzIG91dCBvZiBib3VuZHMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0NDA3LDQ2MTVdLCJlcnJvck1lc3NhZ2UiOiJJbnN1ZmZpY2llbnQgYmFsYW5jZSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ5MjddLCJlcnJvck1lc3NhZ2UiOiJJbnN1ZmZpY2llbnQgYmFsYW5jZSBhdCB0aGUgc2VuZGVyIGFjY291bnQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MzY2LDQ1NzFdLCJlcnJvck1lc3NhZ2UiOiJJbnN1ZmZpY2llbnQgcGFydGl0aW9uIGJhbGFuY2UiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlszOTc2LDQyMDUsNDM0M10sImVycm9yTWVzc2FnZSI6IkludmFsaWQgYW1vdW50IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDY5M10sImVycm9yTWVzc2FnZSI6Ik5hbWUgb2YgdGhlIGFzc2V0IG11c3QgYmUgbG9uZ2VyIG9yIGVxdWFsIHRvIDEgY2hhcmFjdGVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDY5Nl0sImVycm9yTWVzc2FnZSI6Ik5hbWUgb2YgdGhlIGFzc2V0IG11c3QgYmUgc2hvcnRlciBvciBlcXVhbCB0byAzMiBjaGFyYWN0ZXJzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzI1Myw0NTQ3XSwiZXJyb3JNZXNzYWdlIjoiTm90IGF1dGhvcml6ZWQgb3BlcmF0b3IiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2NDEsNjUzLDY2NSw2ODAsNjkyLDcwNyw3MjIsNzQxLDc1Nyw3NzksODAxLDgyNiw4NDUsODYxLDg3Nyw4OTMsOTA5LDkzNyw5NjEsOTgyLDEwMDYsMTAzMSwxMDU1LDEwODYsMTExNywxMTM4LDExNTksMTE4NCwxMjA2LDEyMzQsMTI1NiwxMjc4LDEyOTQsMTMyMiwxMzQ3LDEzNjUsMTM4NiwxNDA3LDE0MjIsMTQzOCwxNDUzLDE0NzIsMTQ5MywxNTE4LDE1NDksMTU2NSwxNTgwLDE1OTUsMTYxMF0sImVycm9yTWVzc2FnZSI6Ik9uQ29tcGxldGlvbiBpcyBub3QgTm9PcCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ2ODVdLCJlcnJvck1lc3NhZ2UiOiJPbmx5IGRlcGxveWVyIG9mIHRoaXMgc21hcnQgY29udHJhY3QgY2FuIGNhbGwgYm9vdHN0cmFwIG1ldGhvZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzMxMjddLCJlcnJvck1lc3NhZ2UiOiJPbmx5IGhvbGRlciBjYW4gYXV0aG9yaXplIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDExMl0sImVycm9yTWVzc2FnZSI6Ik9ubHkgaG9sZGVyIGNhbiBhdXRob3JpemUgcG9ydGlvbiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzMxNTJdLCJlcnJvck1lc3NhZ2UiOiJPbmx5IGhvbGRlciBjYW4gcmV2b2tlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDM1OSw0NTY0XSwiZXJyb3JNZXNzYWdlIjoiUGFydGl0aW9uIGJhbGFuY2UgbWlzc2luZyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzMyMzEsNDUyNV0sImVycm9yTWVzc2FnZSI6IlBvcnRpb24gYWxsb3dhbmNlIGV4Y2VlZGVkIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDcwNF0sImVycm9yTWVzc2FnZSI6IlN5bWJvbCBvZiB0aGUgYXNzZXQgbXVzdCBiZSBsb25nZXIgb3IgZXF1YWwgdG8gMSBjaGFyYWN0ZXIiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0NzA4XSwiZXJyb3JNZXNzYWdlIjoiU3ltYm9sIG9mIHRoZSBhc3NldCBtdXN0IGJlIHNob3J0ZXIgb3IgZXF1YWwgdG8gOCBjaGFyYWN0ZXJzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDcxNV0sImVycm9yTWVzc2FnZSI6IlRoaXMgbWV0aG9kIGNhbiBiZSBjYWxsZWQgb25seSBvbmNlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTIwMl0sImVycm9yTWVzc2FnZSI6ImFscmVhZHlfaW5pdGlhbGl6ZWQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNjMwXSwiZXJyb3JNZXNzYWdlIjoiY2FuIG9ubHkgY2FsbCB3aGVuIGNyZWF0aW5nIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjQ0LDY1Niw2NjgsNjgzLDY5NSw3MTAsNzI1LDc0NCw3NjAsNzgyLDgwNCw4MjksODQ4LDg2NCw4ODAsODk2LDkxMiw5NDAsOTY0LDk4NSwxMDA5LDEwMzQsMTA1OCwxMDg5LDExMjAsMTE0MSwxMTYyLDExODcsMTIwOSwxMjM3LDEyNTksMTI4MSwxMjk3LDEzMjUsMTM1MCwxMzY4LDEzODksMTQxMCwxNDI1LDE0NDEsMTQ1NiwxNDc1LDE0OTYsMTUyMSwxNTUyLDE1NjgsMTU4MywxNTk4LDE2MTNdLCJlcnJvck1lc3NhZ2UiOiJjYW4gb25seSBjYWxsIHdoZW4gbm90IGNyZWF0aW5nIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTc2NCwxNzgwLDE4MTEsMTg0MCwxODU4LDE4NjQsMTkwMywxOTc4LDIwMjksMjIzNSwyNjIyLDI3NTYsMjg1OSwyOTI1LDQyOTUsNDQzMSw0NjM5LDQ3NjgsNDc4Myw0Nzk5LDQ4MDQsNTEwMyw1MTMyLDUxNTQsNTE2Niw1MTkzLDUyMzMsNTI0NCw1MjY2LDUyNzIsNTI5Nyw1MzE4LDUzMzEsNTM2MSw1MzY5LDU0MDRdLCJlcnJvck1lc3NhZ2UiOiJjaGVjayBHbG9iYWxTdGF0ZSBleGlzdHMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNzg5XSwiZXJyb3JNZXNzYWdlIjoiY29udHJvbGxlcl9kaXNhYmxlZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIzMDZdLCJlcnJvck1lc3NhZ2UiOiJlbXB0eV9uYW1lIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjA4OSwyMjEwXSwiZXJyb3JNZXNzYWdlIjoiaW5zdWZmaWNpZW50IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDgzMV0sImVycm9yTWVzc2FnZSI6Imluc3VmZmljaWVudCBhcHByb3ZhbCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI3MjksMjgzMl0sImVycm9yTWVzc2FnZSI6Imluc3VmZmljaWVudF9iYWxhbmNlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDc3Niw0NzkyLDUwMDhdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHNpemUiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyNjA4LDI3MDMsMjgwOV0sImVycm9yTWVzc2FnZSI6ImludmFsaWRfYW1vdW50IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjYzMV0sImVycm9yTWVzc2FnZSI6Imlzc3VhbmNlX2Rpc2FibGVkIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTgyNV0sImVycm9yTWVzc2FnZSI6Imp1c3RpZmljYXRpb25fcmVxdWlyZWQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNzU3XSwiZXJyb3JNZXNzYWdlIjoibm9fY29udHJvbGxlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI2OTddLCJlcnJvck1lc3NhZ2UiOiJub3RfYXV0aCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE3NjZdLCJlcnJvck1lc3NhZ2UiOiJub3RfY29udHJvbGxlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI0NzEsMjQ5MF0sImVycm9yTWVzc2FnZSI6Im5vdF9mb3VuZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUyMzUsNTI2OCw1Mjk5LDU0MDZdLCJlcnJvck1lc3NhZ2UiOiJub3Rfb3duZXIiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls1MzU0LDUzNjVdLCJlcnJvck1lc3NhZ2UiOiJub3RfcGVuZGluZ19vd25lciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE3NDksMjI5NCwyNTgzLDQxOTldLCJlcnJvck1lc3NhZ2UiOiJvbmx5X293bmVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjA5NywyMTIzLDIyMTgsMjI0MywyNzQyLDI3NjQsMjg0NSwyODY3LDMyNDIsNDAwOSw0MDk4LDQyNDYsNDI4NCw0MzAzLDQzNzcsNDQyMCw0NDM5LDQ1MzYsNDU4Miw0NjI4LDQ2NDcsNDgzOSw0OTQ1LDQ5NjhdLCJlcnJvck1lc3NhZ2UiOiJvdmVyZmxvdyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUzMjJdLCJlcnJvck1lc3NhZ2UiOiJwZW5kaW5nX3RyYW5zZmVyX2V4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE4NzRdLCJlcnJvck1lc3NhZ2UiOiJyYXRlX2xpbWl0ZWQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyMDc5XSwiZXJyb3JNZXNzYWdlIjoic2FtZV9hZGRyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTIwNyw1MjQwLDUzMDRdLCJlcnJvck1lc3NhZ2UiOiJ6ZXJvX2FkZHJlc3Nfbm90X2FsbG93ZWQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9XSwicGNPZmZzZXRNZXRob2QiOiJub25lIn0sImNsZWFyIjp7InNvdXJjZUluZm8iOltdLCJwY09mZnNldE1ldGhvZCI6Im5vbmUifX0sInNvdXJjZSI6eyJhcHByb3ZhbCI6IkkzQnlZV2R0WVNCMlpYSnphVzl1SURFd0NpTndjbUZuYldFZ2RIbHdaWFJ5WVdOcklHWmhiSE5sQ2dvdkx5QkFZV3huYjNKaGJtUm1iM1Z1WkdGMGFXOXVMMkZzWjI5eVlXNWtMWFI1Y0dWelkzSnBjSFF2WVhKak5DOXBibVJsZUM1a0xuUnpPanBEYjI1MGNtRmpkQzVoY0hCeWIzWmhiRkJ5YjJkeVlXMG9LU0F0UGlCMWFXNTBOalE2Q20xaGFXNDZDaUFnSUNCcGJuUmpZbXh2WTJzZ01DQXhJRE15SURJS0lDQWdJR0o1ZEdWallteHZZMnNnTUhneE5URm1OMk0zTlNBd2VEQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQWdJbUZ5WXpnNFgyOGlJQ0owSWlBaVlpSWdNSGd3TURBeUlDSmhjbU14TmpRMFgyTjBjbXhsYmlJZ01IZzRNQ0FpWVhKak1UUXhNRjl3SWlBaVlYSmpNVFkwTTE5a2IyTnpJaUFpWVhKak9EaGZjRzhpSUNKaGNtTXhOalEwWDJOMGNtd2lJREI0TURBZ0ltRnlZemc0WDI5cElpQWlZWEpqTVRReE1GOXZjR0VpSUNKaGNtTXhOalEwWDIxallXa2lJREI0TURBd01DQWlZWEpqTVRVNU5GOXBjM01pSUNKaGNtTXhOREV3WDJod1gyRWlJQ0poY21NeE5ERXdYMjl3SWlBaVlYSmpNVFkwTkY5eWFuVnpkQ0lnSW1GeVl6RTJORFJmYkdOaGNpSWdNSGd3TURBd01EQXdNREF3TURBd01EQXdJQ0poY21NeE5qUXpYMlJ2WXlJZ01IZ3dNRFF5SURCNE1ERWdNSGd3TURBeElEQjRNREEyTWlBd2VEQXdNRFlnTUhoa04yWmpOR0U1T0NBd2VEVmpNamRpTkdaaklEQjROems0TTJNek5XTWdNSGcwTXpVMVpESmhaQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem95T1FvZ0lDQWdMeThnWlhod2IzSjBJR05zWVhOeklFRnlZekUyTkRRZ1pYaDBaVzVrY3lCQmNtTXhOalF6SUhzS0lDQWdJSFI0YmlCT2RXMUJjSEJCY21kekNpQWdJQ0JpZWlCdFlXbHVYMkpoY21WZmNtOTFkR2x1WjBBMU5nb2dJQ0FnY0hWemFHSjVkR1Z6Y3lBd2VEQTBOVFEzTW1Rd0lEQjROMlEzT1RBMFlUUWdNSGhsTm1ZMFpqZzJNU0F3ZURKbFltUXlaRE0wSURCNFpXVTJaakprTUdVZ01IZ3haRFZqTjJFeE55QXdlR1UxTjJFMlpURTRJREI0TVRGallqTTJaalVnTUhoaU1UWmtOMkU0WXlBd2VHRTNZMkl6TkRneUlEQjRaV05pTmpNMll6Z2dNSGcyTldJeE5qZ3lZU0F3ZURBeE16QTFPVGxpSURCNE1UUXlZalZtWTJJZ01IaG1PRGd6T0dWaU9TQXdlRE14T0RneVltWmhJREI0WVRsalkyRXhObVlnTUhneU5qWTFPVGRqTUNBd2VETTFaamd4TXpWbUlEQjRaR0UzTURJMVlqa2dNSGd6WmpJMU5qY3hNeUF3ZURrMVlqUm1PV1V6SURCNE9EQmpZelE1WVdJZ01IZ3dOemsyTWpFMk5TQXdlR1UzT0RrMk1XUmhJREI0Wm1RNU5EZ3daRGNnTUhoaU1XSXhaRFk1WVNBd2VHTXhZbVZrTnpnNUlEQjRNMkptWlRFNE16TWdNSGcxT1RsalpERmhOU0F3ZURaa1pUazBNVFkySURCNE1qaG1NREl6WkRjZ01IZzVOelV6T0RKbE1pQXdlRFkxTjJReE0yVmpJREI0WWpaaFpURmhNalVnTUhnNE5HVmpNVE5rTlNBd2VHVmpPVGsyTURReElEQjRPREpsTlRjell6UWdNSGcwWVRrMk9HWTRaaUF3ZUdJMU5ESXlNVEkxSURCNFltSmlNekU1WmpNZ01IZ3dOekF5TmpVMFpTQXdlR1F3TVRVM01qUmxJREI0TURJNVptVmpZekFnTUhnM016UTVNek0wWlNBd2VHUmlOMk00TW1WbUlEQjRabVF5WXpKak5tVWdNSGcwTW1FMVpqQTJOU0F3ZUdGa05HWTJPR1ZoSUM4dklHMWxkR2h2WkNBaVlYSmpNVFkwTkY5elpYUmZZMjl1ZEhKdmJHeGxjaWhoWkdSeVpYTnpLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltRnlZekUyTkRSZmMyVjBYMk52Ym5SeWIyeHNZV0pzWlNoaWIyOXNLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltRnlZekUyTkRSZmMyVjBYM0psY1hWcGNtVmZhblZ6ZEdsbWFXTmhkR2x2YmloaWIyOXNLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltRnlZekUyTkRSZmMyVjBYMjFwYmw5aFkzUnBiMjVmYVc1MFpYSjJZV3dvZFdsdWREWTBLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltRnlZekUyTkRSZmFYTmZZMjl1ZEhKdmJHeGhZbXhsS0NsMWFXNTBOalFpTENCdFpYUm9iMlFnSW1GeVl6RTJORFJmWTI5dWRISnZiR3hsY2w5MGNtRnVjMlpsY2loaFpHUnlaWE56TEdGa1pISmxjM01zZFdsdWRESTFOaXhpZVhSbFcxMHNZbmwwWlZ0ZEtYVnBiblEyTkNJc0lHMWxkR2h2WkNBaVlYSmpNVFkwTkY5amIyNTBjbTlzYkdWeVgzSmxaR1ZsYlNoaFpHUnlaWE56TEhWcGJuUXlOVFlzWW5sMFpWdGRLWFZwYm5RMk5DSXNJRzFsZEdodlpDQWlZWEpqTVRZME0xOXpaWFJmWkc5amRXMWxiblFvWW5sMFpWdGRMSE4wY21sdVp5eGllWFJsVzEwcGRtOXBaQ0lzSUcxbGRHaHZaQ0FpWVhKak1UWTBNMTluWlhSZlpHOWpkVzFsYm5Rb1lubDBaVnRkS1NoemRISnBibWNzWW5sMFpWdGRMSFZwYm5RMk5Da2lMQ0J0WlhSb2IyUWdJbUZ5WXpFMk5ETmZjbVZ0YjNabFgyUnZZM1Z0Wlc1MEtHSjVkR1ZiWFNsMmIybGtJaXdnYldWMGFHOWtJQ0poY21NeE5qUXpYMmRsZEY5aGJHeGZaRzlqZFcxbGJuUnpLQ2xpZVhSbFcxMWJYU0lzSUcxbGRHaHZaQ0FpWVhKak1UVTVORjl6WlhSZmFYTnpkV0ZpYkdVb1ltOXZiQ2wyYjJsa0lpd2diV1YwYUc5a0lDSmhjbU14TlRrMFgybHpjM1ZsS0dGa1pISmxjM01zZFdsdWRESTFOaXhpZVhSbFcxMHBkbTlwWkNJc0lHMWxkR2h2WkNBaVlYSmpNVFU1TkY5eVpXUmxaVzFHY205dEtHRmtaSEpsYzNNc2RXbHVkREkxTml4aWVYUmxXMTBwZG05cFpDSXNJRzFsZEdodlpDQWlZWEpqTVRVNU5GOXlaV1JsWlcwb2RXbHVkREkxTml4aWVYUmxXMTBwZG05cFpDSXNJRzFsZEdodlpDQWlZWEpqTVRVNU5GOTBjbUZ1YzJabGNsOTNhWFJvWDJSaGRHRW9ZV1JrY21WemN5eDFhVzUwTWpVMkxHSjVkR1ZiWFNsaWIyOXNJaXdnYldWMGFHOWtJQ0poY21NeE5UazBYM1J5WVc1elptVnlYMlp5YjIxZmQybDBhRjlrWVhSaEtHRmtaSEpsYzNNc1lXUmtjbVZ6Y3l4MWFXNTBNalUyTEdKNWRHVmJYU2xpYjI5c0lpd2diV1YwYUc5a0lDSmhjbU14TlRrMFgybHpYMmx6YzNWaFlteGxLQ2xpYjI5c0lpd2diV1YwYUc5a0lDSmhjbU14TkRFd1gySmhiR0Z1WTJWZmIyWmZjR0Z5ZEdsMGFXOXVLR0ZrWkhKbGMzTXNZV1JrY21WemN5bDFhVzUwTWpVMklpd2diV1YwYUc5a0lDSmhjbU15TURCZmRISmhibk5tWlhJb1lXUmtjbVZ6Y3l4MWFXNTBNalUyS1dKdmIyd2lMQ0J0WlhSb2IyUWdJbUZ5WXpFME1UQmZkSEpoYm5ObVpYSmZZbmxmY0dGeWRHbDBhVzl1S0dGa1pISmxjM01zWVdSa2NtVnpjeXgxYVc1ME1qVTJMR0o1ZEdWYlhTbGhaR1J5WlhOeklpd2diV1YwYUc5a0lDSmhjbU14TkRFd1gzQmhjblJwZEdsdmJuTmZiMllvWVdSa2NtVnpjeXgxYVc1ME5qUXBZV1JrY21WemMxdGRJaXdnYldWMGFHOWtJQ0poY21NeE5ERXdYMmx6WDI5d1pYSmhkRzl5S0dGa1pISmxjM01zWVdSa2NtVnpjeXhoWkdSeVpYTnpLV0p2YjJ3aUxDQnRaWFJvYjJRZ0ltRnlZekUwTVRCZllYVjBhRzl5YVhwbFgyOXdaWEpoZEc5eUtHRmtaSEpsYzNNc1lXUmtjbVZ6Y3l4aFpHUnlaWE56S1hadmFXUWlMQ0J0WlhSb2IyUWdJbUZ5WXpFME1UQmZjbVYyYjJ0bFgyOXdaWEpoZEc5eUtHRmtaSEpsYzNNc1lXUmtjbVZ6Y3l4aFpHUnlaWE56S1hadmFXUWlMQ0J0WlhSb2IyUWdJbUZ5WXpFME1UQmZiM0JsY21GMGIzSmZkSEpoYm5ObVpYSmZZbmxmY0dGeWRHbDBhVzl1S0dGa1pISmxjM01zWVdSa2NtVnpjeXhoWkdSeVpYTnpMSFZwYm5ReU5UWXNZbmwwWlZ0ZEtXRmtaSEpsYzNNaUxDQnRaWFJvYjJRZ0ltRnlZekUwTVRCZlkyRnVYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2YmloaFpHUnlaWE56TEdGa1pISmxjM01zWVdSa2NtVnpjeXgxYVc1ME1qVTJMR0o1ZEdWYlhTa29ZbmwwWlN4emRISnBibWNzWVdSa2NtVnpjeWtpTENCdFpYUm9iMlFnSW1GeVl6RTBNVEJmWVhWMGFHOXlhWHBsWDI5d1pYSmhkRzl5WDJKNVgzQnZjblJwYjI0b1lXUmtjbVZ6Y3l4aFpHUnlaWE56TEdGa1pISmxjM01zZFdsdWRESTFOaWwyYjJsa0lpd2diV1YwYUc5a0lDSmhjbU14TkRFd1gybHpYMjl3WlhKaGRHOXlYMko1WDNCdmNuUnBiMjRvWVdSa2NtVnpjeXhoWkdSeVpYTnpMR0ZrWkhKbGMzTXBZbTl2YkNJc0lHMWxkR2h2WkNBaVlYSmpNVFF4TUY5cGMzTjFaVjlpZVY5d1lYSjBhWFJwYjI0b1lXUmtjbVZ6Y3l4aFpHUnlaWE56TEhWcGJuUXlOVFlzWW5sMFpWdGRLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltRnlZekUwTVRCZmNtVmtaV1Z0WDJKNVgzQmhjblJwZEdsdmJpaGhaR1J5WlhOekxIVnBiblF5TlRZc1lubDBaVnRkS1hadmFXUWlMQ0J0WlhSb2IyUWdJbUZ5WXpFME1UQmZiM0JsY21GMGIzSmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZiaWhoWkdSeVpYTnpMR0ZrWkhKbGMzTXNkV2x1ZERJMU5peGllWFJsVzEwcGRtOXBaQ0lzSUcxbGRHaHZaQ0FpWW05dmRITjBjbUZ3S0dKNWRHVmJYU3hpZVhSbFcxMHNkV2x1ZERnc2RXbHVkREkxTmlsaWIyOXNJaXdnYldWMGFHOWtJQ0poY21NeU1EQmZibUZ0WlNncFlubDBaVnN6TWwwaUxDQnRaWFJvYjJRZ0ltRnlZekl3TUY5emVXMWliMndvS1dKNWRHVmJPRjBpTENCdFpYUm9iMlFnSW1GeVl6SXdNRjlrWldOcGJXRnNjeWdwZFdsdWREZ2lMQ0J0WlhSb2IyUWdJbUZ5WXpJd01GOTBiM1JoYkZOMWNIQnNlU2dwZFdsdWRESTFOaUlzSUcxbGRHaHZaQ0FpWVhKak1qQXdYMkpoYkdGdVkyVlBaaWhoWkdSeVpYTnpLWFZwYm5ReU5UWWlMQ0J0WlhSb2IyUWdJbUZ5WXpJd01GOTBjbUZ1YzJabGNrWnliMjBvWVdSa2NtVnpjeXhoWkdSeVpYTnpMSFZwYm5ReU5UWXBZbTl2YkNJc0lHMWxkR2h2WkNBaVlYSmpNakF3WDJGd2NISnZkbVVvWVdSa2NtVnpjeXgxYVc1ME1qVTJLV0p2YjJ3aUxDQnRaWFJvYjJRZ0ltRnlZekl3TUY5aGJHeHZkMkZ1WTJVb1lXUmtjbVZ6Y3l4aFpHUnlaWE56S1hWcGJuUXlOVFlpTENCdFpYUm9iMlFnSW1GeVl6ZzRYMjkzYm1WeUtDbGhaR1J5WlhOeklpd2diV1YwYUc5a0lDSmhjbU00T0Y5cGMxOXZkMjVsY2loaFpHUnlaWE56S1dKdmIyd2lMQ0J0WlhSb2IyUWdJbUZ5WXpnNFgybHVhWFJwWVd4cGVtVmZiM2R1WlhJb1lXUmtjbVZ6Y3lsMmIybGtJaXdnYldWMGFHOWtJQ0poY21NNE9GOTBjbUZ1YzJabGNsOXZkMjVsY25Ob2FYQW9ZV1JrY21WemN5bDJiMmxrSWl3Z2JXVjBhRzlrSUNKaGNtTTRPRjl5Wlc1dmRXNWpaVjl2ZDI1bGNuTm9hWEFvS1hadmFXUWlMQ0J0WlhSb2IyUWdJbUZ5WXpnNFgzUnlZVzV6Wm1WeVgyOTNibVZ5YzJocGNGOXlaWEYxWlhOMEtHRmtaSEpsYzNNcGRtOXBaQ0lzSUcxbGRHaHZaQ0FpWVhKak9EaGZZV05qWlhCMFgyOTNibVZ5YzJocGNDZ3BkbTlwWkNJc0lHMWxkR2h2WkNBaVlYSmpPRGhmWTJGdVkyVnNYMjkzYm1WeWMyaHBjRjl5WlhGMVpYTjBLQ2wyYjJsa0lnb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Bb2dJQ0FnYldGMFkyZ2diV0ZwYmw5aGNtTXhOalEwWDNObGRGOWpiMjUwY205c2JHVnlYM0p2ZFhSbFFEVWdiV0ZwYmw5aGNtTXhOalEwWDNObGRGOWpiMjUwY205c2JHRmliR1ZmY205MWRHVkFOaUJ0WVdsdVgyRnlZekUyTkRSZmMyVjBYM0psY1hWcGNtVmZhblZ6ZEdsbWFXTmhkR2x2Ymw5eWIzVjBaVUEzSUcxaGFXNWZZWEpqTVRZME5GOXpaWFJmYldsdVgyRmpkR2x2Ymw5cGJuUmxjblpoYkY5eWIzVjBaVUE0SUcxaGFXNWZZWEpqTVRZME5GOXBjMTlqYjI1MGNtOXNiR0ZpYkdWZmNtOTFkR1ZBT1NCdFlXbHVYMkZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjbDkwY21GdWMyWmxjbDl5YjNWMFpVQXhNQ0J0WVdsdVgyRnlZekUyTkRSZlkyOXVkSEp2Ykd4bGNsOXlaV1JsWlcxZmNtOTFkR1ZBTVRFZ2JXRnBibDloY21NeE5qUXpYM05sZEY5a2IyTjFiV1Z1ZEY5eWIzVjBaVUF4TWlCdFlXbHVYMkZ5WXpFMk5ETmZaMlYwWDJSdlkzVnRaVzUwWDNKdmRYUmxRREV6SUcxaGFXNWZZWEpqTVRZME0xOXlaVzF2ZG1WZlpHOWpkVzFsYm5SZmNtOTFkR1ZBTVRRZ2JXRnBibDloY21NeE5qUXpYMmRsZEY5aGJHeGZaRzlqZFcxbGJuUnpYM0p2ZFhSbFFERTFJRzFoYVc1ZllYSmpNVFU1TkY5elpYUmZhWE56ZFdGaWJHVmZjbTkxZEdWQU1UWWdiV0ZwYmw5aGNtTXhOVGswWDJsemMzVmxYM0p2ZFhSbFFERTNJRzFoYVc1ZllYSmpNVFU1TkY5eVpXUmxaVzFHY205dFgzSnZkWFJsUURFNElHMWhhVzVmWVhKak1UVTVORjl5WldSbFpXMWZjbTkxZEdWQU1Ua2diV0ZwYmw5aGNtTXhOVGswWDNSeVlXNXpabVZ5WDNkcGRHaGZaR0YwWVY5eWIzVjBaVUF5TUNCdFlXbHVYMkZ5WXpFMU9UUmZkSEpoYm5ObVpYSmZabkp2YlY5M2FYUm9YMlJoZEdGZmNtOTFkR1ZBTWpFZ2JXRnBibDloY21NeE5UazBYMmx6WDJsemMzVmhZbXhsWDNKdmRYUmxRREl5SUcxaGFXNWZZWEpqTVRReE1GOWlZV3hoYm1ObFgyOW1YM0JoY25ScGRHbHZibDl5YjNWMFpVQXlNeUJ0WVdsdVgyRnlZekl3TUY5MGNtRnVjMlpsY2w5eWIzVjBaVUF5TkNCdFlXbHVYMkZ5WXpFME1UQmZkSEpoYm5ObVpYSmZZbmxmY0dGeWRHbDBhVzl1WDNKdmRYUmxRREkxSUcxaGFXNWZZWEpqTVRReE1GOXdZWEowYVhScGIyNXpYMjltWDNKdmRYUmxRREkySUcxaGFXNWZZWEpqTVRReE1GOXBjMTl2Y0dWeVlYUnZjbDl5YjNWMFpVQXlOeUJ0WVdsdVgyRnlZekUwTVRCZllYVjBhRzl5YVhwbFgyOXdaWEpoZEc5eVgzSnZkWFJsUURJNElHMWhhVzVmWVhKak1UUXhNRjl5WlhadmEyVmZiM0JsY21GMGIzSmZjbTkxZEdWQU1qa2diV0ZwYmw5aGNtTXhOREV3WDI5d1pYSmhkRzl5WDNSeVlXNXpabVZ5WDJKNVgzQmhjblJwZEdsdmJsOXliM1YwWlVBek1DQnRZV2x1WDJGeVl6RTBNVEJmWTJGdVgzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZibDl5YjNWMFpVQXpNU0J0WVdsdVgyRnlZekUwTVRCZllYVjBhRzl5YVhwbFgyOXdaWEpoZEc5eVgySjVYM0J2Y25ScGIyNWZjbTkxZEdWQU16SWdiV0ZwYmw5aGNtTXhOREV3WDJselgyOXdaWEpoZEc5eVgySjVYM0J2Y25ScGIyNWZjbTkxZEdWQU16TWdiV0ZwYmw5aGNtTXhOREV3WDJsemMzVmxYMko1WDNCaGNuUnBkR2x2Ymw5eWIzVjBaVUF6TkNCdFlXbHVYMkZ5WXpFME1UQmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZibDl5YjNWMFpVQXpOU0J0WVdsdVgyRnlZekUwTVRCZmIzQmxjbUYwYjNKZmNtVmtaV1Z0WDJKNVgzQmhjblJwZEdsdmJsOXliM1YwWlVBek5pQnRZV2x1WDJKdmIzUnpkSEpoY0Y5eWIzVjBaVUF6TnlCdFlXbHVYMkZ5WXpJd01GOXVZVzFsWDNKdmRYUmxRRE00SUcxaGFXNWZZWEpqTWpBd1gzTjViV0p2YkY5eWIzVjBaVUF6T1NCdFlXbHVYMkZ5WXpJd01GOWtaV05wYldGc2MxOXliM1YwWlVBME1DQnRZV2x1WDJGeVl6SXdNRjkwYjNSaGJGTjFjSEJzZVY5eWIzVjBaVUEwTVNCdFlXbHVYMkZ5WXpJd01GOWlZV3hoYm1ObFQyWmZjbTkxZEdWQU5ESWdiV0ZwYmw5aGNtTXlNREJmZEhKaGJuTm1aWEpHY205dFgzSnZkWFJsUURReklHMWhhVzVmWVhKak1qQXdYMkZ3Y0hKdmRtVmZjbTkxZEdWQU5EUWdiV0ZwYmw5aGNtTXlNREJmWVd4c2IzZGhibU5sWDNKdmRYUmxRRFExSUcxaGFXNWZZWEpqT0RoZmIzZHVaWEpmY205MWRHVkFORFlnYldGcGJsOWhjbU00T0Y5cGMxOXZkMjVsY2w5eWIzVjBaVUEwTnlCdFlXbHVYMkZ5WXpnNFgybHVhWFJwWVd4cGVtVmZiM2R1WlhKZmNtOTFkR1ZBTkRnZ2JXRnBibDloY21NNE9GOTBjbUZ1YzJabGNsOXZkMjVsY25Ob2FYQmZjbTkxZEdWQU5Ea2diV0ZwYmw5aGNtTTRPRjl5Wlc1dmRXNWpaVjl2ZDI1bGNuTm9hWEJmY205MWRHVkFOVEFnYldGcGJsOWhjbU00T0Y5MGNtRnVjMlpsY2w5dmQyNWxjbk5vYVhCZmNtVnhkV1Z6ZEY5eWIzVjBaVUExTVNCdFlXbHVYMkZ5WXpnNFgyRmpZMlZ3ZEY5dmQyNWxjbk5vYVhCZmNtOTFkR1ZBTlRJZ2JXRnBibDloY21NNE9GOWpZVzVqWld4ZmIzZHVaWEp6YUdsd1gzSmxjWFZsYzNSZmNtOTFkR1ZBTlRNS0NtMWhhVzVmWVdaMFpYSmZhV1pmWld4elpVQTJNRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdjbVYwZFhKdUNncHRZV2x1WDJGeVl6ZzRYMk5oYm1ObGJGOXZkMjVsY25Ob2FYQmZjbVZ4ZFdWemRGOXliM1YwWlVBMU16b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qRXdNd29nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCUGJrTnZiWEJzWlhScGIyNGdhWE1nYm05MElFNXZUM0FLSUNBZ0lIUjRiaUJCY0hCc2FXTmhkR2x2YmtsRUNpQWdJQ0JoYzNObGNuUWdMeThnWTJGdUlHOXViSGtnWTJGc2JDQjNhR1Z1SUc1dmRDQmpjbVZoZEdsdVp3b2dJQ0FnWTJGc2JITjFZaUJoY21NNE9GOWpZVzVqWld4ZmIzZHVaWEp6YUdsd1gzSmxjWFZsYzNRS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWVhKak9EaGZZV05qWlhCMFgyOTNibVZ5YzJocGNGOXliM1YwWlVBMU1qb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qa3dDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0JqWVd4c2MzVmlJR0Z5WXpnNFgyRmpZMlZ3ZEY5dmQyNWxjbk5vYVhBS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWVhKak9EaGZkSEpoYm5ObVpYSmZiM2R1WlhKemFHbHdYM0psY1hWbGMzUmZjbTkxZEdWQU5URTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6bzNPQW9nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCUGJrTnZiWEJzWlhScGIyNGdhWE1nYm05MElFNXZUM0FLSUNBZ0lIUjRiaUJCY0hCc2FXTmhkR2x2YmtsRUNpQWdJQ0JoYzNObGNuUWdMeThnWTJGdUlHOXViSGtnWTJGc2JDQjNhR1Z1SUc1dmRDQmpjbVZoZEdsdVp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3lPUW9nSUNBZ0x5OGdaWGh3YjNKMElHTnNZWE56SUVGeVl6RTJORFFnWlhoMFpXNWtjeUJCY21NeE5qUXpJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPamM0Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ1kyRnNiSE4xWWlCaGNtTTRPRjkwY21GdWMyWmxjbDl2ZDI1bGNuTm9hWEJmY21WeGRXVnpkQW9nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LYldGcGJsOWhjbU00T0Y5eVpXNXZkVzVqWlY5dmQyNWxjbk5vYVhCZmNtOTFkR1ZBTlRBNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvMk9Bb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdZMkZzYkhOMVlpQmhjbU00T0Y5eVpXNXZkVzVqWlY5dmQyNWxjbk5vYVhBS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWVhKak9EaGZkSEpoYm5ObVpYSmZiM2R1WlhKemFHbHdYM0p2ZFhSbFFEUTVPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZOVGdLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCMGVHNGdUMjVEYjIxd2JHVjBhVzl1Q2lBZ0lDQWhDaUFnSUNCaGMzTmxjblFnTHk4Z1QyNURiMjF3YkdWMGFXOXVJR2x6SUc1dmRDQk9iMDl3Q2lBZ0lDQjBlRzRnUVhCd2JHbGpZWFJwYjI1SlJBb2dJQ0FnWVhOelpYSjBJQzh2SUdOaGJpQnZibXg1SUdOaGJHd2dkMmhsYmlCdWIzUWdZM0psWVhScGJtY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNamtLSUNBZ0lDOHZJR1Y0Y0c5eWRDQmpiR0Z6Y3lCQmNtTXhOalEwSUdWNGRHVnVaSE1nUVhKak1UWTBNeUI3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem8xT0FvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpPRGhmZEhKaGJuTm1aWEpmYjNkdVpYSnphR2x3Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpnNFgybHVhWFJwWVd4cGVtVmZiM2R1WlhKZmNtOTFkR1ZBTkRnNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvMU1Bb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qVXdDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJoY21NNE9GOXBibWwwYVdGc2FYcGxYMjkzYm1WeUNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNncHRZV2x1WDJGeVl6ZzRYMmx6WDI5M2JtVnlYM0p2ZFhSbFFEUTNPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZOREVLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaDdJSEpsWVdSdmJteDVPaUIwY25WbElIMHBDaUFnSUNCMGVHNGdUMjVEYjIxd2JHVjBhVzl1Q2lBZ0lDQWhDaUFnSUNCaGMzTmxjblFnTHk4Z1QyNURiMjF3YkdWMGFXOXVJR2x6SUc1dmRDQk9iMDl3Q2lBZ0lDQjBlRzRnUVhCd2JHbGpZWFJwYjI1SlJBb2dJQ0FnWVhOelpYSjBJQzh2SUdOaGJpQnZibXg1SUdOaGJHd2dkMmhsYmlCdWIzUWdZM0psWVhScGJtY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNamtLSUNBZ0lDOHZJR1Y0Y0c5eWRDQmpiR0Z6Y3lCQmNtTXhOalEwSUdWNGRHVnVaSE1nUVhKak1UWTBNeUI3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem8wTVFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpPRGhmYVhOZmIzZHVaWElLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqT0RoZmIzZHVaWEpmY205MWRHVkFORFk2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem96TlFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBia052YlhCc1pYUnBiMjRnYVhNZ2JtOTBJRTV2VDNBS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaGMzTmxjblFnTHk4Z1kyRnVJRzl1YkhrZ1kyRnNiQ0IzYUdWdUlHNXZkQ0JqY21WaGRHbHVad29nSUNBZ1kyRnNiSE4xWWlCaGNtTTRPRjl2ZDI1bGNnb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tiV0ZwYmw5aGNtTXlNREJmWVd4c2IzZGhibU5sWDNKdmRYUmxRRFExT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFM053b2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0hzZ2NtVmhaRzl1YkhrNklIUnlkV1VnZlNrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3hOemNLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaDdJSEpsWVdSdmJteDVPaUIwY25WbElIMHBDaUFnSUNCallXeHNjM1ZpSUdGeVl6SXdNRjloYkd4dmQyRnVZMlVLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTWpBd1gyRndjSEp2ZG1WZmNtOTFkR1ZBTkRRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1UWTFDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qSTVDaUFnSUNBdkx5QmxlSEJ2Y25RZ1kyeGhjM01nUVhKak1UWTBOQ0JsZUhSbGJtUnpJRUZ5WXpFMk5ETWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPakUyTlFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNakF3WDJGd2NISnZkbVVLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTWpBd1gzUnlZVzV6Wm1WeVJuSnZiVjl5YjNWMFpVQTBNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem94TkRnS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1UUTRDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJoY21NeU1EQmZkSEpoYm5ObVpYSkdjbTl0Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpJd01GOWlZV3hoYm1ObFQyWmZjbTkxZEdWQU5ESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZNVEl6Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9leUJ5WldGa2IyNXNlVG9nZEhKMVpTQjlLUW9nSUNBZ2RIaHVJRTl1UTI5dGNHeGxkR2x2YmdvZ0lDQWdJUW9nSUNBZ1lYTnpaWEowSUM4dklFOXVRMjl0Y0d4bGRHbHZiaUJwY3lCdWIzUWdUbTlQY0FvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUdGemMyVnlkQ0F2THlCallXNGdiMjVzZVNCallXeHNJSGRvWlc0Z2JtOTBJR055WldGMGFXNW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakk1Q2lBZ0lDQXZMeUJsZUhCdmNuUWdZMnhoYzNNZ1FYSmpNVFkwTkNCbGVIUmxibVJ6SUVGeVl6RTJORE1nZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFeU13b2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0hzZ2NtVmhaRzl1YkhrNklIUnlkV1VnZlNrS0lDQWdJR05oYkd4emRXSWdZWEpqTWpBd1gySmhiR0Z1WTJWUFpnb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tiV0ZwYmw5aGNtTXlNREJmZEc5MFlXeFRkWEJ3YkhsZmNtOTFkR1ZBTkRFNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1URXlDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0JqWVd4c2MzVmlJR0Z5WXpJd01GOTBiM1JoYkZOMWNIQnNlUW9nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LYldGcGJsOWhjbU15TURCZlpHVmphVzFoYkhOZmNtOTFkR1ZBTkRBNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1UQXlDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0JqWVd4c2MzVmlJR0Z5WXpJd01GOWtaV05wYldGc2N3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tiV0ZwYmw5aGNtTXlNREJmYzNsdFltOXNYM0p2ZFhSbFFETTVPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPamt5Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9leUJ5WldGa2IyNXNlVG9nZEhKMVpTQjlLUW9nSUNBZ2RIaHVJRTl1UTI5dGNHeGxkR2x2YmdvZ0lDQWdJUW9nSUNBZ1lYTnpaWEowSUM4dklFOXVRMjl0Y0d4bGRHbHZiaUJwY3lCdWIzUWdUbTlQY0FvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUdGemMyVnlkQ0F2THlCallXNGdiMjVzZVNCallXeHNJSGRvWlc0Z2JtOTBJR055WldGMGFXNW5DaUFnSUNCallXeHNjM1ZpSUdGeVl6SXdNRjl6ZVcxaWIyd0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDbTFoYVc1ZllYSmpNakF3WDI1aGJXVmZjbTkxZEdWQU16ZzZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZPRElLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaDdJSEpsWVdSdmJteDVPaUIwY25WbElIMHBDaUFnSUNCMGVHNGdUMjVEYjIxd2JHVjBhVzl1Q2lBZ0lDQWhDaUFnSUNCaGMzTmxjblFnTHk4Z1QyNURiMjF3YkdWMGFXOXVJR2x6SUc1dmRDQk9iMDl3Q2lBZ0lDQjBlRzRnUVhCd2JHbGpZWFJwYjI1SlJBb2dJQ0FnWVhOelpYSjBJQzh2SUdOaGJpQnZibXg1SUdOaGJHd2dkMmhsYmlCdWIzUWdZM0psWVhScGJtY0tJQ0FnSUdOaGJHeHpkV0lnWVhKak1qQXdYMjVoYldVS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWW05dmRITjBjbUZ3WDNKdmRYUmxRRE0zT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pVMkNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCcGN5QnViM1FnVG05UGNBb2dJQ0FnZEhodUlFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lHRnpjMlZ5ZENBdkx5QmpZVzRnYjI1c2VTQmpZV3hzSUhkb1pXNGdibTkwSUdOeVpXRjBhVzVuQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pJNUNpQWdJQ0F2THlCbGVIQnZjblFnWTJ4aGMzTWdRWEpqTVRZME5DQmxlSFJsYm1SeklFRnlZekUyTkRNZ2V3b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01nb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ013b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ05Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qVTJDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJpYjI5MGMzUnlZWEFLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTVRReE1GOXZjR1Z5WVhSdmNsOXlaV1JsWlcxZllubGZjR0Z5ZEdsMGFXOXVYM0p2ZFhSbFFETTJPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTWpNS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qUXlNd29nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUdOaGJHeHpkV0lnWVhKak1UUXhNRjl2Y0dWeVlYUnZjbDl5WldSbFpXMWZZbmxmY0dGeWRHbDBhVzl1Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpFME1UQmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZibDl5YjNWMFpVQXpOVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TkRBNENpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCcGN5QnViM1FnVG05UGNBb2dJQ0FnZEhodUlFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lHRnpjMlZ5ZENBdkx5QmpZVzRnYjI1c2VTQmpZV3hzSUhkb1pXNGdibTkwSUdOeVpXRjBhVzVuQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pJNUNpQWdJQ0F2THlCbGVIQnZjblFnWTJ4aGMzTWdRWEpqTVRZME5DQmxlSFJsYm1SeklFRnlZekUyTkRNZ2V3b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01nb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ013b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzBNRGdLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCallXeHNjM1ZpSUdGeVl6RTBNVEJmY21Wa1pXVnRYMko1WDNCaGNuUnBkR2x2YmdvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS2JXRnBibDloY21NeE5ERXdYMmx6YzNWbFgySjVYM0JoY25ScGRHbHZibDl5YjNWMFpVQXpORG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TXpnekNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCcGN5QnViM1FnVG05UGNBb2dJQ0FnZEhodUlFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lHRnpjMlZ5ZENBdkx5QmpZVzRnYjI1c2VTQmpZV3hzSUhkb1pXNGdibTkwSUdOeVpXRjBhVzVuQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pJNUNpQWdJQ0F2THlCbGVIQnZjblFnWTJ4aGMzTWdRWEpqTVRZME5DQmxlSFJsYm1SeklFRnlZekUyTkRNZ2V3b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01nb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ013b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ05Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3pPRE1LSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCallXeHNjM1ZpSUdGeVl6RTBNVEJmYVhOemRXVmZZbmxmY0dGeWRHbDBhVzl1Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpFME1UQmZhWE5mYjNCbGNtRjBiM0pmWW5sZmNHOXlkR2x2Ymw5eWIzVjBaVUF6TXpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk16Y3hDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qSTVDaUFnSUNBdkx5QmxlSEJ2Y25RZ1kyeGhjM01nUVhKak1UWTBOQ0JsZUhSbGJtUnpJRUZ5WXpFMk5ETWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96TnpFS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQmpZV3hzYzNWaUlHRnlZekUwTVRCZmFYTmZiM0JsY21GMGIzSmZZbmxmY0c5eWRHbHZiZ29nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LYldGcGJsOWhjbU14TkRFd1gyRjFkR2h2Y21sNlpWOXZjR1Z5WVhSdmNsOWllVjl3YjNKMGFXOXVYM0p2ZFhSbFFETXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96TlRrS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qTTFPUW9nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUdOaGJHeHpkV0lnWVhKak1UUXhNRjloZFhSb2IzSnBlbVZmYjNCbGNtRjBiM0pmWW5sZmNHOXlkR2x2YmdvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS2JXRnBibDloY21NeE5ERXdYMk5oYmw5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNWZjbTkxZEdWQU16RTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakUzTkFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBia052YlhCc1pYUnBiMjRnYVhNZ2JtOTBJRTV2VDNBS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaGMzTmxjblFnTHk4Z1kyRnVJRzl1YkhrZ1kyRnNiQ0IzYUdWdUlHNXZkQ0JqY21WaGRHbHVad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem95T1FvZ0lDQWdMeThnWlhod2IzSjBJR05zWVhOeklFRnlZekUyTkRRZ1pYaDBaVzVrY3lCQmNtTXhOalF6SUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURJS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURNS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURRS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1UYzBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJoY21NeE5ERXdYMk5oYmw5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNEtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDbTFoYVc1ZllYSmpNVFF4TUY5dmNHVnlZWFJ2Y2w5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNWZjbTkxZEdWQU16QTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakUwTkFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBia052YlhCc1pYUnBiMjRnYVhNZ2JtOTBJRTV2VDNBS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaGMzTmxjblFnTHk4Z1kyRnVJRzl1YkhrZ1kyRnNiQ0IzYUdWdUlHNXZkQ0JqY21WaGRHbHVad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem95T1FvZ0lDQWdMeThnWlhod2IzSjBJR05zWVhOeklFRnlZekUyTkRRZ1pYaDBaVzVrY3lCQmNtTXhOalF6SUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURJS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURNS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURRS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1UUTBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJoY21NeE5ERXdYMjl3WlhKaGRHOXlYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2YmdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS2JXRnBibDloY21NeE5ERXdYM0psZG05clpWOXZjR1Z5WVhSdmNsOXliM1YwWlVBeU9Ub0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVE0xQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ2RIaHVJRTl1UTI5dGNHeGxkR2x2YmdvZ0lDQWdJUW9nSUNBZ1lYTnpaWEowSUM4dklFOXVRMjl0Y0d4bGRHbHZiaUJwY3lCdWIzUWdUbTlQY0FvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUdGemMyVnlkQ0F2THlCallXNGdiMjVzZVNCallXeHNJSGRvWlc0Z2JtOTBJR055WldGMGFXNW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakk1Q2lBZ0lDQXZMeUJsZUhCdmNuUWdZMnhoYzNNZ1FYSmpNVFkwTkNCbGVIUmxibVJ6SUVGeVl6RTJORE1nZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTWdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTXdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveE16VUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0JqWVd4c2MzVmlJR0Z5WXpFME1UQmZjbVYyYjJ0bFgyOXdaWEpoZEc5eUNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNncHRZV2x1WDJGeVl6RTBNVEJmWVhWMGFHOXlhWHBsWDI5d1pYSmhkRzl5WDNKdmRYUmxRREk0T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveE1qZ0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnVDI1RGIyMXdiR1YwYVc5dUlHbHpJRzV2ZENCT2IwOXdDaUFnSUNCMGVHNGdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdZWE56WlhKMElDOHZJR05oYmlCdmJteDVJR05oYkd3Z2QyaGxiaUJ1YjNRZ1kzSmxZWFJwYm1jS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1qa0tJQ0FnSUM4dklHVjRjRzl5ZENCamJHRnpjeUJCY21NeE5qUTBJR1Y0ZEdWdVpITWdRWEpqTVRZME15QjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXlDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakV5T0FvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNVFF4TUY5aGRYUm9iM0pwZW1WZmIzQmxjbUYwYjNJS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWVhKak1UUXhNRjlwYzE5dmNHVnlZWFJ2Y2w5eWIzVjBaVUF5TnpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1URTBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qSTVDaUFnSUNBdkx5QmxlSEJ2Y25RZ1kyeGhjM01nUVhKak1UWTBOQ0JsZUhSbGJtUnpJRUZ5WXpFMk5ETWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TVRRS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQmpZV3hzYzNWaUlHRnlZekUwTVRCZmFYTmZiM0JsY21GMGIzSUtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDbTFoYVc1ZllYSmpNVFF4TUY5d1lYSjBhWFJwYjI1elgyOW1YM0p2ZFhSbFFESTJPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TURjS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qRXdOd29nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUdOaGJHeHpkV0lnWVhKak1UUXhNRjl3WVhKMGFYUnBiMjV6WDI5bUNpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNncHRZV2x1WDJGeVl6RTBNVEJmZEhKaGJuTm1aWEpmWW5sZmNHRnlkR2wwYVc5dVgzSnZkWFJsUURJMU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzVNd29nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCUGJrTnZiWEJzWlhScGIyNGdhWE1nYm05MElFNXZUM0FLSUNBZ0lIUjRiaUJCY0hCc2FXTmhkR2x2YmtsRUNpQWdJQ0JoYzNObGNuUWdMeThnWTJGdUlHOXViSGtnWTJGc2JDQjNhR1Z1SUc1dmRDQmpjbVZoZEdsdVp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3lPUW9nSUNBZ0x5OGdaWGh3YjNKMElHTnNZWE56SUVGeVl6RTJORFFnWlhoMFpXNWtjeUJCY21NeE5qUXpJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRE1LSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02T1RNS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQmpZV3hzYzNWaUlHRnlZekUwTVRCZmRISmhibk5tWlhKZllubGZjR0Z5ZEdsMGFXOXVDaUFnSUNCaWVYUmxZMTh3SUM4dklEQjRNVFV4Wmpkak56VUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ3B0WVdsdVgyRnlZekl3TUY5MGNtRnVjMlpsY2w5eWIzVjBaVUF5TkRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk56Z0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnVDI1RGIyMXdiR1YwYVc5dUlHbHpJRzV2ZENCT2IwOXdDaUFnSUNCMGVHNGdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdZWE56WlhKMElDOHZJR05oYmlCdmJteDVJR05oYkd3Z2QyaGxiaUJ1YjNRZ1kzSmxZWFJwYm1jS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1qa0tJQ0FnSUM4dklHVjRjRzl5ZENCamJHRnpjeUJCY21NeE5qUTBJR1Y0ZEdWdVpITWdRWEpqTVRZME15QjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPamM0Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ1kyRnNiSE4xWWlCaGNtTXlNREJmZEhKaGJuTm1aWElLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTVRReE1GOWlZV3hoYm1ObFgyOW1YM0JoY25ScGRHbHZibDl5YjNWMFpVQXlNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TmprS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qWTVDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnWTJGc2JITjFZaUJoY21NeE5ERXdYMkpoYkdGdVkyVmZiMlpmY0dGeWRHbDBhVzl1Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpFMU9UUmZhWE5mYVhOemRXRmliR1ZmY205MWRHVkFNakk2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pnMkNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvZXlCeVpXRmtiMjVzZVRvZ2RISjFaU0I5S1FvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCcGN5QnViM1FnVG05UGNBb2dJQ0FnZEhodUlFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lHRnpjMlZ5ZENBdkx5QmpZVzRnYjI1c2VTQmpZV3hzSUhkb1pXNGdibTkwSUdOeVpXRjBhVzVuQ2lBZ0lDQmpZV3hzYzNWaUlHRnlZekUxT1RSZmFYTmZhWE56ZFdGaWJHVUtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDbTFoYVc1ZllYSmpNVFU1TkY5MGNtRnVjMlpsY2w5bWNtOXRYM2RwZEdoZlpHRjBZVjl5YjNWMFpVQXlNVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOVGswTG1Gc1oyOHVkSE02TnpRS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qYzBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJoY21NeE5UazBYM1J5WVc1elptVnlYMlp5YjIxZmQybDBhRjlrWVhSaENpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNncHRZV2x1WDJGeVl6RTFPVFJmZEhKaGJuTm1aWEpmZDJsMGFGOWtZWFJoWDNKdmRYUmxRREl3T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUxT1RRdVlXeG5ieTUwY3pvMk5nb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklETUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5UazBMbUZzWjI4dWRITTZOallLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCallXeHNjM1ZpSUdGeVl6RTFPVFJmZEhKaGJuTm1aWEpmZDJsMGFGOWtZWFJoQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpFMU9UUmZjbVZrWldWdFgzSnZkWFJsUURFNU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMU9UUXVZV3huYnk1MGN6bzFOUW9nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCUGJrTnZiWEJzWlhScGIyNGdhWE1nYm05MElFNXZUM0FLSUNBZ0lIUjRiaUJCY0hCc2FXTmhkR2x2YmtsRUNpQWdJQ0JoYzNObGNuUWdMeThnWTJGdUlHOXViSGtnWTJGc2JDQjNhR1Z1SUc1dmRDQmpjbVZoZEdsdVp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3lPUW9nSUNBZ0x5OGdaWGh3YjNKMElHTnNZWE56SUVGeVl6RTJORFFnWlhoMFpXNWtjeUJCY21NeE5qUXpJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOVGswTG1Gc1oyOHVkSE02TlRVS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQmpZV3hzYzNWaUlHRnlZekUxT1RSZmNtVmtaV1Z0Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpFMU9UUmZjbVZrWldWdFJuSnZiVjl5YjNWMFpVQXhPRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOVGswTG1Gc1oyOHVkSE02TkRRS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUdseklHNXZkQ0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowSUM4dklHTmhiaUJ2Ym14NUlHTmhiR3dnZDJobGJpQnViM1FnWTNKbFlYUnBibWNLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TWprS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU14TmpRMElHVjRkR1Z1WkhNZ1FYSmpNVFkwTXlCN0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qUTBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnWTJGc2JITjFZaUJoY21NeE5UazBYM0psWkdWbGJVWnliMjBLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTVRVNU5GOXBjM04xWlY5eWIzVjBaVUF4TnpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk16UUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnVDI1RGIyMXdiR1YwYVc5dUlHbHpJRzV2ZENCT2IwOXdDaUFnSUNCMGVHNGdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdZWE56WlhKMElDOHZJR05oYmlCdmJteDVJR05oYkd3Z2QyaGxiaUJ1YjNRZ1kzSmxZWFJwYm1jS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1qa0tJQ0FnSUM4dklHVjRjRzl5ZENCamJHRnpjeUJCY21NeE5qUTBJR1Y0ZEdWdVpITWdRWEpqTVRZME15QjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXlDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPak0wQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ1kyRnNiSE4xWWlCaGNtTXhOVGswWDJsemMzVmxDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ3B0WVdsdVgyRnlZekUxT1RSZmMyVjBYMmx6YzNWaFlteGxYM0p2ZFhSbFFERTJPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem95TndvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBia052YlhCc1pYUnBiMjRnYVhNZ2JtOTBJRTV2VDNBS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaGMzTmxjblFnTHk4Z1kyRnVJRzl1YkhrZ1kyRnNiQ0IzYUdWdUlHNXZkQ0JqY21WaGRHbHVad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem95T1FvZ0lDQWdMeThnWlhod2IzSjBJR05zWVhOeklFRnlZekUyTkRRZ1pYaDBaVzVrY3lCQmNtTXhOalF6SUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk1qY0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0JqWVd4c2MzVmlJR0Z5WXpFMU9UUmZjMlYwWDJsemMzVmhZbXhsQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwdFlXbHVYMkZ5WXpFMk5ETmZaMlYwWDJGc2JGOWtiMk4xYldWdWRITmZjbTkxZEdWQU1UVTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBNeTVoYkdkdkxuUnpPalkxQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9leUJ5WldGa2IyNXNlVG9nZEhKMVpTQjlLUW9nSUNBZ2RIaHVJRTl1UTI5dGNHeGxkR2x2YmdvZ0lDQWdJUW9nSUNBZ1lYTnpaWEowSUM4dklFOXVRMjl0Y0d4bGRHbHZiaUJwY3lCdWIzUWdUbTlQY0FvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUdGemMyVnlkQ0F2THlCallXNGdiMjVzZVNCallXeHNJSGRvWlc0Z2JtOTBJR055WldGMGFXNW5DaUFnSUNCallXeHNjM1ZpSUdGeVl6RTJORE5mWjJWMFgyRnNiRjlrYjJOMWJXVnVkSE1LSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTVRZME0xOXlaVzF2ZG1WZlpHOWpkVzFsYm5SZmNtOTFkR1ZBTVRRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qVTBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qSTVDaUFnSUNBdkx5QmxlSEJ2Y25RZ1kyeGhjM01nUVhKak1UWTBOQ0JsZUhSbGJtUnpJRUZ5WXpFMk5ETWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORE11WVd4bmJ5NTBjem8xTkFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNVFkwTTE5eVpXMXZkbVZmWkc5amRXMWxiblFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTVRZME0xOW5aWFJmWkc5amRXMWxiblJmY205MWRHVkFNVE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pRNENpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvZXlCeVpXRmtiMjVzZVRvZ2RISjFaU0I5S1FvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCcGN5QnViM1FnVG05UGNBb2dJQ0FnZEhodUlFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lHRnpjMlZ5ZENBdkx5QmpZVzRnYjI1c2VTQmpZV3hzSUhkb1pXNGdibTkwSUdOeVpXRjBhVzVuQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pJNUNpQWdJQ0F2THlCbGVIQnZjblFnWTJ4aGMzTWdRWEpqTVRZME5DQmxlSFJsYm1SeklFRnlZekUyTkRNZ2V3b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5ETXVZV3huYnk1MGN6bzBPQW9nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tIc2djbVZoWkc5dWJIazZJSFJ5ZFdVZ2ZTa0tJQ0FnSUdOaGJHeHpkV0lnWVhKak1UWTBNMTluWlhSZlpHOWpkVzFsYm5RS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWVhKak1UWTBNMTl6WlhSZlpHOWpkVzFsYm5SZmNtOTFkR1ZBTVRJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qTTBDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qSTVDaUFnSUNBdkx5QmxlSEJ2Y25RZ1kyeGhjM01nUVhKak1UWTBOQ0JsZUhSbGJtUnpJRUZ5WXpFMk5ETWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORE11WVd4bmJ5NTBjem96TkFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNVFkwTTE5elpYUmZaRzlqZFcxbGJuUUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDbTFoYVc1ZllYSmpNVFkwTkY5amIyNTBjbTlzYkdWeVgzSmxaR1ZsYlY5eWIzVjBaVUF4TVRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1UVTVDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnZEhodUlFOXVRMjl0Y0d4bGRHbHZiZ29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnBjeUJ1YjNRZ1RtOVBjQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0Z6YzJWeWRDQXZMeUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnYm05MElHTnlaV0YwYVc1bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qSTVDaUFnSUNBdkx5QmxlSEJ2Y25RZ1kyeGhjM01nUVhKak1UWTBOQ0JsZUhSbGJtUnpJRUZ5WXpFMk5ETWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem94TlRrS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQmpZV3hzYzNWaUlHRnlZekUyTkRSZlkyOXVkSEp2Ykd4bGNsOXlaV1JsWlcwS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NtMWhhVzVmWVhKak1UWTBORjlqYjI1MGNtOXNiR1Z5WDNSeVlXNXpabVZ5WDNKdmRYUmxRREV3T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveE1qUUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnVDI1RGIyMXdiR1YwYVc5dUlHbHpJRzV2ZENCT2IwOXdDaUFnSUNCMGVHNGdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdZWE56WlhKMElDOHZJR05oYmlCdmJteDVJR05oYkd3Z2QyaGxiaUJ1YjNRZ1kzSmxZWFJwYm1jS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1qa0tJQ0FnSUM4dklHVjRjRzl5ZENCamJHRnpjeUJCY21NeE5qUTBJR1Y0ZEdWdVpITWdRWEpqTVRZME15QjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXlDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXpDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QTBDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QTFDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakV5TkFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNVFkwTkY5amIyNTBjbTlzYkdWeVgzUnlZVzV6Wm1WeUNpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNncHRZV2x1WDJGeVl6RTJORFJmYVhOZlkyOXVkSEp2Ykd4aFlteGxYM0p2ZFhSbFFEazZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakV4TWdvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBia052YlhCc1pYUnBiMjRnYVhNZ2JtOTBJRTV2VDNBS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaGMzTmxjblFnTHk4Z1kyRnVJRzl1YkhrZ1kyRnNiQ0IzYUdWdUlHNXZkQ0JqY21WaGRHbHVad29nSUNBZ1kyRnNiSE4xWWlCaGNtTXhOalEwWDJselgyTnZiblJ5YjJ4c1lXSnNaUW9nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LYldGcGJsOWhjbU14TmpRMFgzTmxkRjl0YVc1ZllXTjBhVzl1WDJsdWRHVnlkbUZzWDNKdmRYUmxRRGc2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pFd05Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVEExQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ1kyRnNiSE4xWWlCaGNtTXhOalEwWDNObGRGOXRhVzVmWVdOMGFXOXVYMmx1ZEdWeWRtRnNDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ3B0WVdsdVgyRnlZekUyTkRSZmMyVjBYM0psY1hWcGNtVmZhblZ6ZEdsbWFXTmhkR2x2Ymw5eWIzVjBaVUEzT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvNU9Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZPVGtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCallXeHNjM1ZpSUdGeVl6RTJORFJmYzJWMFgzSmxjWFZwY21WZmFuVnpkR2xtYVdOaGRHbHZiZ29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LYldGcGJsOWhjbU14TmpRMFgzTmxkRjlqYjI1MGNtOXNiR0ZpYkdWZmNtOTFkR1ZBTmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk9EVUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnVDI1RGIyMXdiR1YwYVc5dUlHbHpJRzV2ZENCT2IwOXdDaUFnSUNCMGVHNGdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdZWE56WlhKMElDOHZJR05oYmlCdmJteDVJR05oYkd3Z2QyaGxiaUJ1YjNRZ1kzSmxZWFJwYm1jS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1qa0tJQ0FnSUM4dklHVjRjRzl5ZENCamJHRnpjeUJCY21NeE5qUTBJR1Y0ZEdWdVpITWdRWEpqTVRZME15QjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPamcxQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ1kyRnNiSE4xWWlCaGNtTXhOalEwWDNObGRGOWpiMjUwY205c2JHRmliR1VLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ20xaGFXNWZZWEpqTVRZME5GOXpaWFJmWTI5dWRISnZiR3hsY2w5eWIzVjBaVUExT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvM05Bb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2FYTWdibTkwSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RZ0x5OGdZMkZ1SUc5dWJIa2dZMkZzYkNCM2FHVnVJRzV2ZENCamNtVmhkR2x1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZOelFLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCallXeHNjM1ZpSUdGeVl6RTJORFJmYzJWMFgyTnZiblJ5YjJ4c1pYSUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDbTFoYVc1ZlltRnlaVjl5YjNWMGFXNW5RRFUyT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZ5WXpFMk5EUWdaWGgwWlc1a2N5QkJjbU14TmpReklIc0tJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lHSnVlaUJ0WVdsdVgyRm1kR1Z5WDJsbVgyVnNjMlZBTmpBS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnWTJGdUlHOXViSGtnWTJGc2JDQjNhR1Z1SUdOeVpXRjBhVzVuQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdYM0IxZVdGZmJHbGlMbUZ5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVjlqYjI1allYUmZZbmwwWlY5c1pXNW5kR2hmYUdWaFpDaGhjbkpoZVRvZ1lubDBaWE1zSUc1bGQxOXBkR1Z0YzE5aWVYUmxjem9nWW5sMFpYTXNJRzVsZDE5cGRHVnRjMTlqYjNWdWREb2dkV2x1ZERZMEtTQXRQaUJpZVhSbGN6b0taSGx1WVcxcFkxOWhjbkpoZVY5amIyNWpZWFJmWW5sMFpWOXNaVzVuZEdoZmFHVmhaRG9LSUNBZ0lIQnliM1J2SURNZ01Rb2dJQ0FnWm5KaGJXVmZaR2xuSUMwekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZS0lDQWdJR1IxY0FvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQXJDaUFnSUNCemQyRndDaUFnSUNCcGJuUmpYek1nTHk4Z01nb2dJQ0FnS2dvZ0lDQWdhVzUwWTE4eklDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QXhDaUFnSUNCcGRHOWlDaUFnSUNCbGVIUnlZV04wSURZZ01nb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNCcGJuUmpYek1nTHk4Z01nb2dJQ0FnWkdsbklESUtJQ0FnSUhOMVluTjBjbWx1WnpNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdhVzUwWTE4eklDOHZJRElLSUNBZ0lDb0tJQ0FnSUdKNlpYSnZDaUFnSUNCamIyNWpZWFFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNd29nSUNBZ2JHVnVDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUTUtJQ0FnSUhWdVkyOTJaWElnTXdvZ0lDQWdkVzVqYjNabGNpQXlDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JqYjI1allYUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWTI5dVkyRjBDaUFnSUNCemQyRndDaUFnSUNCcGJuUmpYek1nTHk4Z01nb2dJQ0FnS2dvZ0lDQWdaSFZ3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2MzZGhjQW9LWkhsdVlXMXBZMTloY25KaGVWOWpiMjVqWVhSZllubDBaVjlzWlc1bmRHaGZhR1ZoWkY5bWIzSmZhR1ZoWkdWeVFESTZDaUFnSUNCbWNtRnRaVjlrYVdjZ013b2dJQ0FnWm5KaGJXVmZaR2xuSURJS0lDQWdJRHdLSUNBZ0lHSjZJR1I1Ym1GdGFXTmZZWEp5WVhsZlkyOXVZMkYwWDJKNWRHVmZiR1Z1WjNSb1gyaGxZV1JmWVdaMFpYSmZabTl5UURVS0lDQWdJR1p5WVcxbFgyUnBaeUEwQ2lBZ0lDQmtkWEFLSUNBZ0lHbDBiMklLSUNBZ0lHVjRkSEpoWTNRZ05pQXlDaUFnSUNCbWNtRnRaVjlrYVdjZ01Rb2dJQ0FnWm5KaGJXVmZaR2xuSURNS0lDQWdJR1IxY0FvZ0lDQWdZMjkyWlhJZ05Bb2dJQ0FnZFc1amIzWmxjaUF5Q2lBZ0lDQnlaWEJzWVdObE13b2dJQ0FnWkhWd0NpQWdJQ0JtY21GdFpWOWlkWEo1SURFS0lDQWdJR1JwWnlBeENpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5nb2dJQ0FnYVc1MFkxOHpJQzh2SURJS0lDQWdJQ3NLSUNBZ0lDc0tJQ0FnSUdaeVlXMWxYMkoxY25rZ05Bb2dJQ0FnYVc1MFkxOHpJQzh2SURJS0lDQWdJQ3NLSUNBZ0lHWnlZVzFsWDJKMWNua2dNd29nSUNBZ1lpQmtlVzVoYldsalgyRnljbUY1WDJOdmJtTmhkRjlpZVhSbFgyeGxibWQwYUY5b1pXRmtYMlp2Y2w5b1pXRmtaWEpBTWdvS1pIbHVZVzFwWTE5aGNuSmhlVjlqYjI1allYUmZZbmwwWlY5c1pXNW5kR2hmYUdWaFpGOWhablJsY2w5bWIzSkFOVG9LSUNBZ0lHWnlZVzFsWDJScFp5QXdDaUFnSUNCbWNtRnRaVjlrYVdjZ01Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCbWNtRnRaVjlpZFhKNUlEQUtJQ0FnSUhKbGRITjFZZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZPa0Z5WXpFMk5EUXVYMjl1YkhsUGQyNWxjaWdwSUMwK0lIWnZhV1E2Q2w5dmJteDVUM2R1WlhJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qUXpDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVoY21NNE9GOXBjMTl2ZDI1bGNpaHVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBLUzV1WVhScGRtVWdQVDA5SUhSeWRXVXNJQ2R2Ym14NVgyOTNibVZ5SnlrS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpPRGhmYVhOZmIzZHVaWElLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCblpYUmlhWFFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5dWJIbGZiM2R1WlhJS0lDQWdJSEpsZEhOMVlnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk9rRnlZekUyTkRRdVgyOXViSGxEYjI1MGNtOXNiR1Z5S0NrZ0xUNGdkbTlwWkRvS1gyOXViSGxEYjI1MGNtOXNiR1Z5T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvek1Rb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6RTJORFJmWTI5dWRISnZiR3hsY2lBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFXUmtjbVZ6Y3o0b2V5QnJaWGs2SUNkaGNtTXhOalEwWDJOMGNtd25JSDBwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV01nTVRFZ0x5OGdJbUZ5WXpFMk5EUmZZM1J5YkNJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk5EY0tJQ0FnSUM4dklHRnpjMlZ5ZENoMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjaTVvWVhOV1lXeDFaU3dnSjI1dlgyTnZiblJ5YjJ4c1pYSW5LUW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWVhOelpYSjBJQzh2SUc1dlgyTnZiblJ5YjJ4c1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZORGdLSUNBZ0lDOHZJR0Z6YzJWeWRDaHVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBJRDA5UFNCMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjaTUyWVd4MVpTd2dKMjV2ZEY5amIyNTBjbTlzYkdWeUp5a0tJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk16RUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOalEwWDJOdmJuUnliMnhzWlhJZ1BTQkhiRzlpWVd4VGRHRjBaVHhoY21NMExrRmtaSEpsYzNNK0tIc2dhMlY1T2lBbllYSmpNVFkwTkY5amRISnNKeUI5S1FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqSURFeElDOHZJQ0poY21NeE5qUTBYMk4wY213aUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvME9Bb2dJQ0FnTHk4Z1lYTnpaWEowS0c1bGR5QmhjbU0wTGtGa1pISmxjM01vVkhodUxuTmxibVJsY2lrZ1BUMDlJSFJvYVhNdVlYSmpNVFkwTkY5amIyNTBjbTlzYkdWeUxuWmhiSFZsTENBbmJtOTBYMk52Ym5SeWIyeHNaWEluS1FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnViM1JmWTI5dWRISnZiR3hsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvek1nb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6RTJORFJmWTI5dWRISnZiR3hoWW14bElEMGdSMnh2WW1Gc1UzUmhkR1U4WVhKak5DNUNiMjlzUGloN0lHdGxlVG9nSjJGeVl6RTJORFJmWTNSeWJHVnVKeUI5S1FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqSURZZ0x5OGdJbUZ5WXpFMk5EUmZZM1J5YkdWdUlnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6bzBPUW9nSUNBZ0x5OGdZWE56WlhKMEtIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR0ZpYkdVdWFHRnpWbUZzZFdVZ0ppWWdkR2hwY3k1aGNtTXhOalEwWDJOdmJuUnliMnhzWVdKc1pTNTJZV3gxWlM1dVlYUnBkbVVnUFQwOUlIUnlkV1VzSUNkamIyNTBjbTlzYkdWeVgyUnBjMkZpYkdWa0p5a0tJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmlkWEo1SURFS0lDQWdJR0o2SUY5dmJteDVRMjl1ZEhKdmJHeGxjbDlpYjI5c1gyWmhiSE5sUURNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk16SUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOalEwWDJOdmJuUnliMnhzWVdKc1pTQTlJRWRzYjJKaGJGTjBZWFJsUEdGeVl6UXVRbTl2YkQ0b2V5QnJaWGs2SUNkaGNtTXhOalEwWDJOMGNteGxiaWNnZlNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWXlBMklDOHZJQ0poY21NeE5qUTBYMk4wY214bGJpSUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ1IyeHZZbUZzVTNSaGRHVWdaWGhwYzNSekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvME9Rb2dJQ0FnTHk4Z1lYTnpaWEowS0hSb2FYTXVZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1V1YUdGelZtRnNkV1VnSmlZZ2RHaHBjeTVoY21NeE5qUTBYMk52Ym5SeWIyeHNZV0pzWlM1MllXeDFaUzV1WVhScGRtVWdQVDA5SUhSeWRXVXNJQ2RqYjI1MGNtOXNiR1Z5WDJScGMyRmliR1ZrSnlrS0lDQWdJR2RsZEdKcGRBb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmllaUJmYjI1c2VVTnZiblJ5YjJ4c1pYSmZZbTl2YkY5bVlXeHpaVUF6Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9LWDI5dWJIbERiMjUwY205c2JHVnlYMkp2YjJ4ZmJXVnlaMlZBTkRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk5Ea0tJQ0FnSUM4dklHRnpjMlZ5ZENoMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGhZbXhsTG1oaGMxWmhiSFZsSUNZbUlIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR0ZpYkdVdWRtRnNkV1V1Ym1GMGFYWmxJRDA5UFNCMGNuVmxMQ0FuWTI5dWRISnZiR3hsY2w5a2FYTmhZbXhsWkNjcENpQWdJQ0JoYzNObGNuUWdMeThnWTI5dWRISnZiR3hsY2w5a2FYTmhZbXhsWkFvZ0lDQWdjbVYwYzNWaUNncGZiMjVzZVVOdmJuUnliMnhzWlhKZlltOXZiRjltWVd4elpVQXpPZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdJZ1gyOXViSGxEYjI1MGNtOXNiR1Z5WDJKdmIyeGZiV1Z5WjJWQU5Bb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk9rRnlZekUyTkRRdVgyTm9aV05yU25WemRHbG1hV05oZEdsdmJpaHZjR1Z5WVhSdmNsOWtZWFJoT2lCaWVYUmxjeWtnTFQ0Z2RtOXBaRG9LWDJOb1pXTnJTblZ6ZEdsbWFXTmhkR2x2YmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk5USUtJQ0FnSUM4dklIQnliM1JsWTNSbFpDQmZZMmhsWTJ0S2RYTjBhV1pwWTJGMGFXOXVLRzl3WlhKaGRHOXlYMlJoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWektUb2dkbTlwWkNCN0NpQWdJQ0J3Y205MGJ5QXhJREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TXpNS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYM0psY1hWcGNtVktkWE4wYVdacFkyRjBhVzl1SUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1Q2IyOXNQaWg3SUd0bGVUb2dKMkZ5WXpFMk5EUmZjbXAxYzNRbklIMHBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdNakFnTHk4Z0ltRnlZekUyTkRSZmNtcDFjM1FpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pVekNpQWdJQ0F2THlCcFppQW9kR2hwY3k1aGNtTXhOalEwWDNKbGNYVnBjbVZLZFhOMGFXWnBZMkYwYVc5dUxtaGhjMVpoYkhWbElDWW1JSFJvYVhNdVlYSmpNVFkwTkY5eVpYRjFhWEpsU25WemRHbG1hV05oZEdsdmJpNTJZV3gxWlM1dVlYUnBkbVVnUFQwOUlIUnlkV1VwSUhzS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaWRYSjVJREVLSUNBZ0lHSjZJRjlqYUdWamEwcDFjM1JwWm1sallYUnBiMjVmWVdaMFpYSmZhV1pmWld4elpVQXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPak16Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRZME5GOXlaWEYxYVhKbFNuVnpkR2xtYVdOaGRHbHZiaUE5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW05dmJENG9leUJyWlhrNklDZGhjbU14TmpRMFgzSnFkWE4wSnlCOUtRb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWaklESXdJQzh2SUNKaGNtTXhOalEwWDNKcWRYTjBJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qVXpDaUFnSUNBdkx5QnBaaUFvZEdocGN5NWhjbU14TmpRMFgzSmxjWFZwY21WS2RYTjBhV1pwWTJGMGFXOXVMbWhoYzFaaGJIVmxJQ1ltSUhSb2FYTXVZWEpqTVRZME5GOXlaWEYxYVhKbFNuVnpkR2xtYVdOaGRHbHZiaTUyWVd4MVpTNXVZWFJwZG1VZ1BUMDlJSFJ5ZFdVcElIc0tJQ0FnSUdkbGRHSnBkQW9nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUQwOUNpQWdJQ0JpZWlCZlkyaGxZMnRLZFhOMGFXWnBZMkYwYVc5dVgyRm1kR1Z5WDJsbVgyVnNjMlZBTXdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvMU5Bb2dJQ0FnTHk4Z1lYTnpaWEowS0c5d1pYSmhkRzl5WDJSaGRHRXVibUYwYVhabExteGxibWQwYUNBK0lEQXNJQ2RxZFhOMGFXWnBZMkYwYVc5dVgzSmxjWFZwY21Wa0p5a0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWlhoMGNtRmpkQ0F5SURBS0lDQWdJR3hsYmdvZ0lDQWdZWE56WlhKMElDOHZJR3AxYzNScFptbGpZWFJwYjI1ZmNtVnhkV2x5WldRS0NsOWphR1ZqYTBwMWMzUnBabWxqWVhScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBek9nb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6bzZRWEpqTVRZME5DNWZjbUYwWlV4cGJXbDBLQ2tnTFQ0Z2RtOXBaRG9LWDNKaGRHVk1hVzFwZERvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk16VUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOalEwWDIxcGJrTnZiblJ5YjJ4c1pYSkJZM1JwYjI1SmJuUmxjblpoYkNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVZXbHVkRTQyTkQ0b2V5QnJaWGs2SUNkaGNtTXhOalEwWDIxallXa25JSDBwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV01nTVRVZ0x5OGdJbUZ5WXpFMk5EUmZiV05oYVNJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk5qQUtJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjl0YVc1RGIyNTBjbTlzYkdWeVFXTjBhVzl1U1c1MFpYSjJZV3d1YUdGelZtRnNkV1VnSmlZS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaWRYSjVJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TmpBdE5qRUtJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjl0YVc1RGIyNTBjbTlzYkdWeVFXTjBhVzl1U1c1MFpYSjJZV3d1YUdGelZtRnNkV1VnSmlZS0lDQWdJQzh2SUhSb2FYTXVZWEpqTVRZME5GOXRhVzVEYjI1MGNtOXNiR1Z5UVdOMGFXOXVTVzUwWlhKMllXd3VkbUZzZFdVdWJtRjBhWFpsSUQ0Z01HNEtJQ0FnSUdKNklGOXlZWFJsVEdsdGFYUmZZV1owWlhKZmFXWmZaV3h6WlVBMUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qTTFDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFkwTkY5dGFXNURiMjUwY205c2JHVnlRV04wYVc5dVNXNTBaWEoyWVd3Z1BTQkhiRzlpWVd4VGRHRjBaVHhoY21NMExsVnBiblJPTmpRK0tIc2dhMlY1T2lBbllYSmpNVFkwTkY5dFkyRnBKeUI5S1FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqSURFMUlDOHZJQ0poY21NeE5qUTBYMjFqWVdraUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvMk1Rb2dJQ0FnTHk4Z2RHaHBjeTVoY21NeE5qUTBYMjFwYmtOdmJuUnliMnhzWlhKQlkzUnBiMjVKYm5SbGNuWmhiQzUyWVd4MVpTNXVZWFJwZG1VZ1BpQXdiZ29nSUNBZ1luUnZhUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem8yTUMwMk1Rb2dJQ0FnTHk4Z2RHaHBjeTVoY21NeE5qUTBYMjFwYmtOdmJuUnliMnhzWlhKQlkzUnBiMjVKYm5SbGNuWmhiQzVvWVhOV1lXeDFaU0FtSmdvZ0lDQWdMeThnZEdocGN5NWhjbU14TmpRMFgyMXBia052Ym5SeWIyeHNaWEpCWTNScGIyNUpiblJsY25aaGJDNTJZV3gxWlM1dVlYUnBkbVVnUGlBd2Jnb2dJQ0FnWW5vZ1gzSmhkR1ZNYVcxcGRGOWhablJsY2w5cFpsOWxiSE5sUURVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk16UUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOalEwWDJ4aGMzUkRiMjUwY205c2JHVnlRV04wYVc5dVVtOTFibVFnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGxWcGJuUk9OalErS0hzZ2EyVjVPaUFuWVhKak1UWTBORjlzWTJGeUp5QjlLU0F2THlCdmNIUnBiMjVoYkNCeVlYUmxJR3hwYldsMElIUnlZV05yYVc1bkNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdZbmwwWldNZ01qRWdMeThnSW1GeVl6RTJORFJmYkdOaGNpSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZOak1LSUNBZ0lDOHZJR2xtSUNoMGFHbHpMbUZ5WXpFMk5EUmZiR0Z6ZEVOdmJuUnliMnhzWlhKQlkzUnBiMjVTYjNWdVpDNW9ZWE5XWVd4MVpTa2dld29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ1gzSmhkR1ZNYVcxcGRGOWhablJsY2w5cFpsOWxiSE5sUURRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk16UUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOalEwWDJ4aGMzUkRiMjUwY205c2JHVnlRV04wYVc5dVVtOTFibVFnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGxWcGJuUk9OalErS0hzZ2EyVjVPaUFuWVhKak1UWTBORjlzWTJGeUp5QjlLU0F2THlCdmNIUnBiMjVoYkNCeVlYUmxJR3hwYldsMElIUnlZV05yYVc1bkNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdZbmwwWldNZ01qRWdMeThnSW1GeVl6RTJORFJmYkdOaGNpSUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ1IyeHZZbUZzVTNSaGRHVWdaWGhwYzNSekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qWTBDaUFnSUNBdkx5QmpiMjV6ZENCc1lYTjBJRDBnZEdocGN5NWhjbU14TmpRMFgyeGhjM1JEYjI1MGNtOXNiR1Z5UVdOMGFXOXVVbTkxYm1RdWRtRnNkV1V1Ym1GMGFYWmxDaUFnSUNCaWRHOXBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPak0xQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRZME5GOXRhVzVEYjI1MGNtOXNiR1Z5UVdOMGFXOXVTVzUwWlhKMllXd2dQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMbFZwYm5ST05qUStLSHNnYTJWNU9pQW5ZWEpqTVRZME5GOXRZMkZwSnlCOUtRb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWaklERTFJQzh2SUNKaGNtTXhOalEwWDIxallXa2lDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6bzJOUW9nSUNBZ0x5OGdZMjl1YzNRZ2JXbHVSMkZ3SUQwZ2RHaHBjeTVoY21NeE5qUTBYMjFwYmtOdmJuUnliMnhzWlhKQlkzUnBiMjVKYm5SbGNuWmhiQzUyWVd4MVpTNXVZWFJwZG1VS0lDQWdJR0owYjJrS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk5qWUtJQ0FnSUM4dklHTnZibk4wSUdOMWNuSmxiblFnUFNCdVpYY2dZWEpqTkM1VmFXNTBUalkwS0Vkc2IySmhiQzV5YjNWdVpDa3VibUYwYVhabENpQWdJQ0JuYkc5aVlXd2dVbTkxYm1RS0lDQWdJR2wwYjJJS0lDQWdJR0owYjJrS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk5qY0tJQ0FnSUM4dklHRnpjMlZ5ZENoamRYSnlaVzUwSUQ0OUlHeGhjM1FnS3lCdGFXNUhZWEFzSUNkeVlYUmxYMnhwYldsMFpXUW5LUW9nSUNBZ1kyOTJaWElnTWdvZ0lDQWdLd29nSUNBZ1BqMEtJQ0FnSUdGemMyVnlkQ0F2THlCeVlYUmxYMnhwYldsMFpXUUtDbDl5WVhSbFRHbHRhWFJmWVdaMFpYSmZhV1pmWld4elpVQTBPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem8yT1FvZ0lDQWdMeThnZEdocGN5NWhjbU14TmpRMFgyeGhjM1JEYjI1MGNtOXNiR1Z5UVdOMGFXOXVVbTkxYm1RdWRtRnNkV1VnUFNCdVpYY2dZWEpqTkM1VmFXNTBUalkwS0Vkc2IySmhiQzV5YjNWdVpDa0tJQ0FnSUdkc2IySmhiQ0JTYjNWdVpBb2dJQ0FnYVhSdllnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3pOQW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZekUyTkRSZmJHRnpkRU52Ym5SeWIyeHNaWEpCWTNScGIyNVNiM1Z1WkNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVZXbHVkRTQyTkQ0b2V5QnJaWGs2SUNkaGNtTXhOalEwWDJ4allYSW5JSDBwSUM4dklHOXdkR2x2Ym1Gc0lISmhkR1VnYkdsdGFYUWdkSEpoWTJ0cGJtY0tJQ0FnSUdKNWRHVmpJREl4SUM4dklDSmhjbU14TmpRMFgyeGpZWElpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pZNUNpQWdJQ0F2THlCMGFHbHpMbUZ5WXpFMk5EUmZiR0Z6ZEVOdmJuUnliMnhzWlhKQlkzUnBiMjVTYjNWdVpDNTJZV3gxWlNBOUlHNWxkeUJoY21NMExsVnBiblJPTmpRb1IyeHZZbUZzTG5KdmRXNWtLUW9nSUNBZ2MzZGhjQW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLQ2w5eVlYUmxUR2x0YVhSZllXWjBaWEpmYVdaZlpXeHpaVUExT2dvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvNlFYSmpNVFkwTkM1aGNtTXhOalEwWDNObGRGOWpiMjUwY205c2JHVnlLRzVsZDE5amIyNTBjbTlzYkdWeU9pQmllWFJsY3lrZ0xUNGdkbTlwWkRvS1lYSmpNVFkwTkY5elpYUmZZMjl1ZEhKdmJHeGxjam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TnpRdE56VUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UWTBORjl6WlhSZlkyOXVkSEp2Ykd4bGNpaHVaWGRmWTI5dWRISnZiR3hsY2pvZ1lYSmpOQzVCWkdSeVpYTnpLVG9nZG05cFpDQjdDaUFnSUNCd2NtOTBieUF4SURBS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pjMkNpQWdJQ0F2THlCMGFHbHpMbDl2Ym14NVQzZHVaWElvS1FvZ0lDQWdZMkZzYkhOMVlpQmZiMjVzZVU5M2JtVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPak14Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRZME5GOWpiMjUwY205c2JHVnlJRDBnUjJ4dlltRnNVM1JoZEdVOFlYSmpOQzVCWkdSeVpYTnpQaWg3SUd0bGVUb2dKMkZ5WXpFMk5EUmZZM1J5YkNjZ2ZTa0tJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFl5QXhNU0F2THlBaVlYSmpNVFkwTkY5amRISnNJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem8zTndvZ0lDQWdMeThnWTI5dWMzUWdiMnhrSUQwZ2RHaHBjeTVoY21NeE5qUTBYMk52Ym5SeWIyeHNaWEl1YUdGelZtRnNkV1VnUHlCMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjaTUyWVd4MVpTQTZJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9LUW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ1lYSmpNVFkwTkY5elpYUmZZMjl1ZEhKdmJHeGxjbDkwWlhKdVlYSjVYMlpoYkhObFFESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNekVLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU14TmpRMFgyTnZiblJ5YjJ4c1pYSWdQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMa0ZrWkhKbGMzTStLSHNnYTJWNU9pQW5ZWEpqTVRZME5GOWpkSEpzSnlCOUtRb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWaklERXhJQzh2SUNKaGNtTXhOalEwWDJOMGNtd2lDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnWm5KaGJXVmZZblZ5ZVNBd0NncGhjbU14TmpRMFgzTmxkRjlqYjI1MGNtOXNiR1Z5WDNSbGNtNWhjbmxmYldWeVoyVkFNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TXpFS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYMk52Ym5SeWIyeHNaWElnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtGa1pISmxjM00rS0hzZ2EyVjVPaUFuWVhKak1UWTBORjlqZEhKc0p5QjlLUW9nSUNBZ1lubDBaV01nTVRFZ0x5OGdJbUZ5WXpFMk5EUmZZM1J5YkNJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk56Z0tJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR1Z5TG5aaGJIVmxJRDBnYm1WM1gyTnZiblJ5YjJ4c1pYSUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNeklLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU14TmpRMFgyTnZiblJ5YjJ4c1lXSnNaU0E5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW05dmJENG9leUJyWlhrNklDZGhjbU14TmpRMFgyTjBjbXhsYmljZ2ZTa0tJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFl5QTJJQzh2SUNKaGNtTXhOalEwWDJOMGNteGxiaUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TnprS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1aGNtTXhOalEwWDJOdmJuUnliMnhzWVdKc1pTNW9ZWE5XWVd4MVpTa2dld29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW01NklHRnlZekUyTkRSZmMyVjBYMk52Ym5SeWIyeHNaWEpmWVdaMFpYSmZhV1pmWld4elpVQTFDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPak15Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1VnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtKdmIydytLSHNnYTJWNU9pQW5ZWEpqTVRZME5GOWpkSEpzWlc0bklIMHBDaUFnSUNCaWVYUmxZeUEySUM4dklDSmhjbU14TmpRMFgyTjBjbXhsYmlJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk9EQUtJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR0ZpYkdVdWRtRnNkV1VnUFNCdVpYY2dZWEpqTkM1Q2IyOXNLSFJ5ZFdVcENpQWdJQ0JpZVhSbFl5QTNJQzh2SURCNE9EQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDZ3BoY21NeE5qUTBYM05sZEY5amIyNTBjbTlzYkdWeVgyRm1kR1Z5WDJsbVgyVnNjMlZBTlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk9ESUtJQ0FnSUM4dklHVnRhWFFvSjBOdmJuUnliMnhzWlhKRGFHRnVaMlZrSnl3Z2JtVjNJR0Z5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjbDlqYUdGdVoyVmtYMlYyWlc1MEtIc2diMnhrTENCdVpYVTZJRzVsZDE5amIyNTBjbTlzYkdWeUlIMHBLUW9nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZzBNRGxqWXpVM01DQXZMeUJ0WlhSb2IyUWdJa052Ym5SeWIyeHNaWEpEYUdGdVoyVmtLQ2hoWkdSeVpYTnpMR0ZrWkhKbGMzTXBLU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnlaWFJ6ZFdJS0NtRnlZekUyTkRSZmMyVjBYMk52Ym5SeWIyeHNaWEpmZEdWeWJtRnllVjltWVd4elpVQXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem8zTndvZ0lDQWdMeThnWTI5dWMzUWdiMnhrSUQwZ2RHaHBjeTVoY21NeE5qUTBYMk52Ym5SeWIyeHNaWEl1YUdGelZtRnNkV1VnUHlCMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjaTUyWVd4MVpTQTZJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9LUW9nSUNBZ1lubDBaV05mTVNBdkx5QmhaR1J5SUVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZaTlVoR1MxRUtJQ0FnSUdaeVlXMWxYMkoxY25rZ01Bb2dJQ0FnWWlCaGNtTXhOalEwWDNObGRGOWpiMjUwY205c2JHVnlYM1JsY201aGNubGZiV1Z5WjJWQU13b0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk9rRnlZekUyTkRRdVlYSmpNVFkwTkY5elpYUmZZMjl1ZEhKdmJHeGhZbXhsS0dac1lXYzZJR0o1ZEdWektTQXRQaUIyYjJsa09ncGhjbU14TmpRMFgzTmxkRjlqYjI1MGNtOXNiR0ZpYkdVNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qZzFMVGcyQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZekUyTkRSZmMyVjBYMk52Ym5SeWIyeHNZV0pzWlNobWJHRm5PaUJoY21NMExrSnZiMndwT2lCMmIybGtJSHNLSUNBZ0lIQnliM1J2SURFZ01Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6bzROd29nSUNBZ0x5OGdkR2hwY3k1ZmIyNXNlVTkzYm1WeUtDa0tJQ0FnSUdOaGJHeHpkV0lnWDI5dWJIbFBkMjVsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvNE9Rb2dJQ0FnTHk4Z2FXWWdLR1pzWVdjdWJtRjBhWFpsSUQwOVBTQm1ZV3h6WlNrZ2V3b2dJQ0FnWm5KaGJXVmZaR2xuSUMweENpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaMlYwWW1sMENpQWdJQ0JpYm5vZ1lYSmpNVFkwTkY5elpYUmZZMjl1ZEhKdmJHeGhZbXhsWDJWc2MyVmZZbTlrZVVBeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qTXlDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFkwTkY5amIyNTBjbTlzYkdGaWJHVWdQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMa0p2YjJ3K0tIc2dhMlY1T2lBbllYSmpNVFkwTkY5amRISnNaVzRuSUgwcENpQWdJQ0JpZVhSbFl5QTJJQzh2SUNKaGNtTXhOalEwWDJOMGNteGxiaUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02T1RBS0lDQWdJQzh2SUhSb2FYTXVZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1V1ZG1Gc2RXVWdQU0JtYkdGbkNpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmY0hWMENncGhjbU14TmpRMFgzTmxkRjlqYjI1MGNtOXNiR0ZpYkdWZllXWjBaWEpmYVdaZlpXeHpaVUEyT2dvZ0lDQWdjbVYwYzNWaUNncGhjbU14TmpRMFgzTmxkRjlqYjI1MGNtOXNiR0ZpYkdWZlpXeHpaVjlpYjJSNVFESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPak15Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1VnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtKdmIydytLSHNnYTJWNU9pQW5ZWEpqTVRZME5GOWpkSEpzWlc0bklIMHBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdOaUF2THlBaVlYSmpNVFkwTkY5amRISnNaVzRpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2prekNpQWdJQ0F2THlCcFppQW9JWFJvYVhNdVlYSmpNVFkwTkY5amIyNTBjbTlzYkdGaWJHVXVhR0Z6Vm1Gc2RXVWdmSHdnZEdocGN5NWhjbU14TmpRMFgyTnZiblJ5YjJ4c1lXSnNaUzUyWVd4MVpTNXVZWFJwZG1VZ1BUMDlJSFJ5ZFdVcElIc0tJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmlkWEo1SURFS0lDQWdJR0o2SUdGeVl6RTJORFJmYzJWMFgyTnZiblJ5YjJ4c1lXSnNaVjlwWmw5aWIyUjVRRFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TXpJS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYMk52Ym5SeWIyeHNZV0pzWlNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFtOXZiRDRvZXlCclpYazZJQ2RoY21NeE5qUTBYMk4wY214bGJpY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZeUEySUM4dklDSmhjbU14TmpRMFgyTjBjbXhsYmlJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnUjJ4dlltRnNVM1JoZEdVZ1pYaHBjM1J6Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem81TXdvZ0lDQWdMeThnYVdZZ0tDRjBhR2x6TG1GeVl6RTJORFJmWTI5dWRISnZiR3hoWW14bExtaGhjMVpoYkhWbElIeDhJSFJvYVhNdVlYSmpNVFkwTkY5amIyNTBjbTlzYkdGaWJHVXVkbUZzZFdVdWJtRjBhWFpsSUQwOVBTQjBjblZsS1NCN0NpQWdJQ0JuWlhSaWFYUUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0E5UFFvZ0lDQWdZbm9nWVhKak1UWTBORjl6WlhSZlkyOXVkSEp2Ykd4aFlteGxYMkZtZEdWeVgybG1YMlZzYzJWQU5nb0tZWEpqTVRZME5GOXpaWFJmWTI5dWRISnZiR3hoWW14bFgybG1YMkp2WkhsQU5Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNeklLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU14TmpRMFgyTnZiblJ5YjJ4c1lXSnNaU0E5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW05dmJENG9leUJyWlhrNklDZGhjbU14TmpRMFgyTjBjbXhsYmljZ2ZTa0tJQ0FnSUdKNWRHVmpJRFlnTHk4Z0ltRnlZekUyTkRSZlkzUnliR1Z1SWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvNU5Bb2dJQ0FnTHk4Z2RHaHBjeTVoY21NeE5qUTBYMk52Ym5SeWIyeHNZV0pzWlM1MllXeDFaU0E5SUdac1lXY0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUhKbGRITjFZZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZPa0Z5WXpFMk5EUXVZWEpqTVRZME5GOXpaWFJmY21WeGRXbHlaVjlxZFhOMGFXWnBZMkYwYVc5dUtHWnNZV2M2SUdKNWRHVnpLU0F0UGlCMmIybGtPZ3BoY21NeE5qUTBYM05sZEY5eVpYRjFhWEpsWDJwMWMzUnBabWxqWVhScGIyNDZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPams1TFRFd01Bb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYM05sZEY5eVpYRjFhWEpsWDJwMWMzUnBabWxqWVhScGIyNG9abXhoWnpvZ1lYSmpOQzVDYjI5c0tUb2dkbTlwWkNCN0NpQWdJQ0J3Y205MGJ5QXhJREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TVRBeENpQWdJQ0F2THlCMGFHbHpMbDl2Ym14NVQzZHVaWElvS1FvZ0lDQWdZMkZzYkhOMVlpQmZiMjVzZVU5M2JtVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPak16Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRZME5GOXlaWEYxYVhKbFNuVnpkR2xtYVdOaGRHbHZiaUE5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW05dmJENG9leUJyWlhrNklDZGhjbU14TmpRMFgzSnFkWE4wSnlCOUtRb2dJQ0FnWW5sMFpXTWdNakFnTHk4Z0ltRnlZekUyTkRSZmNtcDFjM1FpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pFd01nb2dJQ0FnTHk4Z2RHaHBjeTVoY21NeE5qUTBYM0psY1hWcGNtVktkWE4wYVdacFkyRjBhVzl1TG5aaGJIVmxJRDBnWm14aFp3b2dJQ0FnWm5KaGJXVmZaR2xuSUMweENpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6bzZRWEpqTVRZME5DNWhjbU14TmpRMFgzTmxkRjl0YVc1ZllXTjBhVzl1WDJsdWRHVnlkbUZzS0dsdWRHVnlkbUZzT2lCaWVYUmxjeWtnTFQ0Z2RtOXBaRG9LWVhKak1UWTBORjl6WlhSZmJXbHVYMkZqZEdsdmJsOXBiblJsY25aaGJEb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVEExTFRFd05nb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYM05sZEY5dGFXNWZZV04wYVc5dVgybHVkR1Z5ZG1Gc0tHbHVkR1Z5ZG1Gc09pQmhjbU0wTGxWcGJuUk9OalFwT2lCMmIybGtJSHNLSUNBZ0lIQnliM1J2SURFZ01Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hNRGNLSUNBZ0lDOHZJSFJvYVhNdVgyOXViSGxQZDI1bGNpZ3BDaUFnSUNCallXeHNjM1ZpSUY5dmJteDVUM2R1WlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk16VUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOalEwWDIxcGJrTnZiblJ5YjJ4c1pYSkJZM1JwYjI1SmJuUmxjblpoYkNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVZXbHVkRTQyTkQ0b2V5QnJaWGs2SUNkaGNtTXhOalEwWDIxallXa25JSDBwQ2lBZ0lDQmllWFJsWXlBeE5TQXZMeUFpWVhKak1UWTBORjl0WTJGcElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hNRGdLSUNBZ0lDOHZJSFJvYVhNdVlYSmpNVFkwTkY5dGFXNURiMjUwY205c2JHVnlRV04wYVc5dVNXNTBaWEoyWVd3dWRtRnNkV1VnUFNCcGJuUmxjblpoYkFvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvNlFYSmpNVFkwTkM1aGNtTXhOalEwWDJselgyTnZiblJ5YjJ4c1lXSnNaU2dwSUMwK0lHSjVkR1Z6T2dwaGNtTXhOalEwWDJselgyTnZiblJ5YjJ4c1lXSnNaVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TXpJS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYMk52Ym5SeWIyeHNZV0pzWlNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFtOXZiRDRvZXlCclpYazZJQ2RoY21NeE5qUTBYMk4wY214bGJpY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZeUEySUM4dklDSmhjbU14TmpRMFgyTjBjbXhsYmlJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1URTFDaUFnSUNBdkx5QjBhR2x6TG1GeVl6RTJORFJmWTI5dWRISnZiR3hoWW14bExtaGhjMVpoYkhWbElDWW1DaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZblZ5ZVNBeENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qRXhOUzB4TVRZS0lDQWdJQzh2SUhSb2FYTXVZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1V1YUdGelZtRnNkV1VnSmlZS0lDQWdJQzh2SUhSb2FYTXVZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1V1ZG1Gc2RXVXVibUYwYVhabElEMDlQU0IwY25WbElDWW1DaUFnSUNCaWVpQmhjbU14TmpRMFgybHpYMk52Ym5SeWIyeHNZV0pzWlY5aFpuUmxjbDlwWmw5bGJITmxRRFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TXpJS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5qUTBYMk52Ym5SeWIyeHNZV0pzWlNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFtOXZiRDRvZXlCclpYazZJQ2RoY21NeE5qUTBYMk4wY214bGJpY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZeUEySUM4dklDSmhjbU14TmpRMFgyTjBjbXhsYmlJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnUjJ4dlltRnNVM1JoZEdVZ1pYaHBjM1J6Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem94TVRZS0lDQWdJQzh2SUhSb2FYTXVZWEpqTVRZME5GOWpiMjUwY205c2JHRmliR1V1ZG1Gc2RXVXVibUYwYVhabElEMDlQU0IwY25WbElDWW1DaUFnSUNCblpYUmlhWFFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hNVFV0TVRFMkNpQWdJQ0F2THlCMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGhZbXhsTG1oaGMxWmhiSFZsSUNZbUNpQWdJQ0F2THlCMGFHbHpMbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGhZbXhsTG5aaGJIVmxMbTVoZEdsMlpTQTlQVDBnZEhKMVpTQW1KZ29nSUNBZ1lub2dZWEpqTVRZME5GOXBjMTlqYjI1MGNtOXNiR0ZpYkdWZllXWjBaWEpmYVdaZlpXeHpaVUEwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pNeENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UWTBORjlqYjI1MGNtOXNiR1Z5SUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1QlpHUnlaWE56UGloN0lHdGxlVG9nSjJGeVl6RTJORFJmWTNSeWJDY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZeUF4TVNBdkx5QWlZWEpqTVRZME5GOWpkSEpzSWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveE1UY0tJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR1Z5TG1oaGMxWmhiSFZsQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWW5WeWVTQXhDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakV4TlMweE1UY0tJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR0ZpYkdVdWFHRnpWbUZzZFdVZ0ppWUtJQ0FnSUM4dklIUm9hWE11WVhKak1UWTBORjlqYjI1MGNtOXNiR0ZpYkdVdWRtRnNkV1V1Ym1GMGFYWmxJRDA5UFNCMGNuVmxJQ1ltQ2lBZ0lDQXZMeUIwYUdsekxtRnlZekUyTkRSZlkyOXVkSEp2Ykd4bGNpNW9ZWE5XWVd4MVpRb2dJQ0FnWW5vZ1lYSmpNVFkwTkY5cGMxOWpiMjUwY205c2JHRmliR1ZmWVdaMFpYSmZhV1pmWld4elpVQTBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakV4T1FvZ0lDQWdMeThnY21WMGRYSnVJRzVsZHlCaGNtTTBMbFZwYm5ST05qUW9NVzRwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGd3TURBd01EQXdNREF3TURBd01EQXhDaUFnSUNCeVpYUnpkV0lLQ21GeVl6RTJORFJmYVhOZlkyOXVkSEp2Ykd4aFlteGxYMkZtZEdWeVgybG1YMlZzYzJWQU5Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVEl4Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdibVYzSUdGeVl6UXVWV2x1ZEU0Mk5DZ3diaWtLSUNBZ0lHSjVkR1ZqSURJeUlDOHZJREI0TURBd01EQXdNREF3TURBd01EQXdNQW9nSUNBZ2NtVjBjM1ZpQ2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem82UVhKak1UWTBOQzVoY21NeE5qUTBYMk52Ym5SeWIyeHNaWEpmZEhKaGJuTm1aWElvWm5KdmJUb2dZbmwwWlhNc0lIUnZPaUJpZVhSbGN5d2dZVzF2ZFc1ME9pQmllWFJsY3l3Z1pHRjBZVG9nWW5sMFpYTXNJRzl3WlhKaGRHOXlYMlJoZEdFNklHSjVkR1Z6S1NBdFBpQmllWFJsY3pvS1lYSmpNVFkwTkY5amIyNTBjbTlzYkdWeVgzUnlZVzV6Wm1WeU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hNalF0TVRNeENpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjbDkwY21GdWMyWmxjaWdLSUNBZ0lDOHZJQ0FnWm5KdmJUb2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCMGJ6b2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCaGJXOTFiblE2SUdGeVl6UXVWV2x1ZEU0eU5UWXNDaUFnSUNBdkx5QWdJR1JoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWekxBb2dJQ0FnTHk4Z0lDQnZjR1Z5WVhSdmNsOWtZWFJoT2lCaGNtTTBMa1I1Ym1GdGFXTkNlWFJsY3l3S0lDQWdJQzh2SUNrNklHRnlZelF1VldsdWRFNDJOQ0I3Q2lBZ0lDQndjbTkwYnlBMUlERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVE15Q2lBZ0lDQXZMeUIwYUdsekxsOXZibXg1UTI5dWRISnZiR3hsY2lncENpQWdJQ0JqWVd4c2MzVmlJRjl2Ym14NVEyOXVkSEp2Ykd4bGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hNek1LSUNBZ0lDOHZJSFJvYVhNdVgyTm9aV05yU25WemRHbG1hV05oZEdsdmJpaHZjR1Z5WVhSdmNsOWtZWFJoS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpZV3hzYzNWaUlGOWphR1ZqYTBwMWMzUnBabWxqWVhScGIyNEtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVE0wQ2lBZ0lDQXZMeUIwYUdsekxsOXlZWFJsVEdsdGFYUW9LUW9nSUNBZ1kyRnNiSE4xWWlCZmNtRjBaVXhwYldsMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qRXpOZ29nSUNBZ0x5OGdZWE56WlhKMEtHWnliMjBnSVQwOUlIUnZMQ0FuYzJGdFpWOWhaR1J5SnlrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TlFvZ0lDQWdabkpoYldWZlpHbG5JQzAwQ2lBZ0lDQWhQUW9nSUNBZ1lYTnpaWEowSUM4dklITmhiV1ZmWVdSa2Nnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hNemNLSUNBZ0lDOHZJR052Ym5OMElHWnliMjFDWVd3Z1BTQjBhR2x6TGw5aVlXeGhibU5sVDJZb1puSnZiU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXROUW9nSUNBZ1kyRnNiSE4xWWlCZlltRnNZVzVqWlU5bUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qRXpPQW9nSUNBZ0x5OGdZWE56WlhKMEtHWnliMjFDWVd3dWJtRjBhWFpsSUQ0OUlHRnRiM1Z1ZEM1dVlYUnBkbVVzSUNkcGJuTjFabVpwWTJsbGJuUW5LUW9nSUNBZ1pIVndDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUTUtJQ0FnSUdJK1BRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWMzVm1abWxqYVdWdWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hOREFLSUNBZ0lDOHZJSFJvYVhNdVltRnNZVzVqWlhNb1puSnZiU2t1ZG1Gc2RXVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqSTFOaWhtY205dFFtRnNMbTVoZEdsMlpTQXRJR0Z0YjNWdWRDNXVZWFJwZG1VcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR0l0Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNBOFBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5MlpYSm1iRzkzQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lHSjZaWEp2Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmthV2NnTVFvZ0lDQWdZbndLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem8xTXdvZ0lDQWdMeThnY0hWaWJHbGpJR0poYkdGdVkyVnpJRDBnUW05NFRXRndQRUZrWkhKbGMzTXNJRlZwYm5ST01qVTJQaWg3SUd0bGVWQnlaV1pwZURvZ0oySW5JSDBwQ2lBZ0lDQmllWFJsWXlBMElDOHZJQ0ppSWdvZ0lDQWdabkpoYldWZlpHbG5JQzAxQ2lBZ0lDQmpiMjVqWVhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1UUXdDaUFnSUNBdkx5QjBhR2x6TG1KaGJHRnVZMlZ6S0daeWIyMHBMblpoYkhWbElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0eU5UWW9abkp2YlVKaGJDNXVZWFJwZG1VZ0xTQmhiVzkxYm5RdWJtRjBhWFpsS1FvZ0lDQWdjM2RoY0FvZ0lDQWdZbTk0WDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hOREVLSUNBZ0lDOHZJR052Ym5OMElIUnZRbUZzSUQwZ2RHaHBjeTVmWW1Gc1lXNWpaVTltS0hSdktRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMENpQWdJQ0JqWVd4c2MzVmlJRjlpWVd4aGJtTmxUMllLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TVRReUNpQWdJQ0F2THlCMGFHbHpMbUpoYkdGdVkyVnpLSFJ2S1M1MllXeDFaU0E5SUc1bGR5QmhjbU0wTGxWcGJuUk9NalUyS0hSdlFtRnNMbTVoZEdsMlpTQXJJR0Z0YjNWdWRDNXVZWFJwZG1VcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR0lyQ2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNBOFBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5MlpYSm1iRzkzQ2lBZ0lDQmlmQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPalV6Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZbUZzWVc1alpYTWdQU0JDYjNoTllYQThRV1JrY21WemN5d2dWV2x1ZEU0eU5UWStLSHNnYTJWNVVISmxabWw0T2lBbllpY2dmU2tLSUNBZ0lHSjVkR1ZqSURRZ0x5OGdJbUlpQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVFFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pveE5ESUtJQ0FnSUM4dklIUm9hWE11WW1Gc1lXNWpaWE1vZEc4cExuWmhiSFZsSUQwZ2JtVjNJR0Z5WXpRdVZXbHVkRTR5TlRZb2RHOUNZV3d1Ym1GMGFYWmxJQ3NnWVcxdmRXNTBMbTVoZEdsMlpTa0tJQ0FnSUhOM1lYQUtJQ0FnSUdKdmVGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalEwTG1Gc1oyOHVkSE02TVRRM0NpQWdJQ0F2THlCamIyNTBjbTlzYkdWeU9pQnVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBMQW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hORFl0TVRVMENpQWdJQ0F2THlCdVpYY2dZWEpqTVRZME5GOWpiMjUwY205c2JHVnlYM1J5WVc1elptVnlYMlYyWlc1MEtIc0tJQ0FnSUM4dklDQWdZMjl1ZEhKdmJHeGxjam9nYm1WM0lHRnlZelF1UVdSa2NtVnpjeWhVZUc0dWMyVnVaR1Z5S1N3S0lDQWdJQzh2SUNBZ1puSnZiU3dLSUNBZ0lDOHZJQ0FnZEc4c0NpQWdJQ0F2THlBZ0lHRnRiM1Z1ZEN3S0lDQWdJQzh2SUNBZ1kyOWtaU3dLSUNBZ0lDOHZJQ0FnWkdGMFlTd0tJQ0FnSUM4dklDQWdiM0JsY21GMGIzSmZaR0YwWVN3S0lDQWdJQzh2SUgwcExBb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE5Bb2dJQ0FnWTI5dVkyRjBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUTUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem95TlFvZ0lDQWdMeThnWTI5dWMzUWdRMDlFUlY5VFZVTkRSVk5USUQwZ2JtVjNJR0Z5WXpRdVFubDBaU2d3ZURVeEtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE5URUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVFEyTFRFMU5Bb2dJQ0FnTHk4Z2JtVjNJR0Z5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjbDkwY21GdWMyWmxjbDlsZG1WdWRDaDdDaUFnSUNBdkx5QWdJR052Ym5SeWIyeHNaWEk2SUc1bGR5QmhjbU0wTGtGa1pISmxjM01vVkhodUxuTmxibVJsY2lrc0NpQWdJQ0F2THlBZ0lHWnliMjBzQ2lBZ0lDQXZMeUFnSUhSdkxBb2dJQ0FnTHk4Z0lDQmhiVzkxYm5Rc0NpQWdJQ0F2THlBZ0lHTnZaR1VzQ2lBZ0lDQXZMeUFnSUdSaGRHRXNDaUFnSUNBdkx5QWdJRzl3WlhKaGRHOXlYMlJoZEdFc0NpQWdJQ0F2THlCOUtTd0tJQ0FnSUdOdmJtTmhkQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRNREE0TlFvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR3hsYmdvZ0lDQWdjSFZ6YUdsdWRDQXhNek1nTHk4Z01UTXpDaUFnSUNBckNpQWdJQ0JwZEc5aUNpQWdJQ0JsZUhSeVlXTjBJRFlnTWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR052Ym1OaGRBb2dJQ0FnWm5KaGJXVmZaR2xuSUMweENpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVFEwTFRFMU5Rb2dJQ0FnTHk4Z1pXMXBkQ2dLSUNBZ0lDOHZJQ0FnSjBOdmJuUnliMnhzWlhKVWNtRnVjMlpsY2ljc0NpQWdJQ0F2THlBZ0lHNWxkeUJoY21NeE5qUTBYMk52Ym5SeWIyeHNaWEpmZEhKaGJuTm1aWEpmWlhabGJuUW9ld29nSUNBZ0x5OGdJQ0FnSUdOdmJuUnliMnhzWlhJNklHNWxkeUJoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa3NDaUFnSUNBdkx5QWdJQ0FnWm5KdmJTd0tJQ0FnSUM4dklDQWdJQ0IwYnl3S0lDQWdJQzh2SUNBZ0lDQmhiVzkxYm5Rc0NpQWdJQ0F2THlBZ0lDQWdZMjlrWlN3S0lDQWdJQzh2SUNBZ0lDQmtZWFJoTEFvZ0lDQWdMeThnSUNBZ0lHOXdaWEpoZEc5eVgyUmhkR0VzQ2lBZ0lDQXZMeUFnSUgwcExBb2dJQ0FnTHk4Z0tRb2dJQ0FnWW5sMFpXTWdOU0F2THlBd2VEQXdNRElLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TXpRMlpXRTNPVFVnTHk4Z2JXVjBhRzlrSUNKRGIyNTBjbTlzYkdWeVZISmhibk5tWlhJb0tHRmtaSEpsYzNNc1lXUmtjbVZ6Y3l4aFpHUnlaWE56TEhWcGJuUXlOVFlzWW5sMFpTeGllWFJsVzEwc1lubDBaVnRkS1NraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hOVFlLSUNBZ0lDOHZJSEpsZEhWeWJpQnVaWGNnWVhKak5DNVZhVzUwVGpZMEtHTnZaR1V1Ym1GMGFYWmxLUW9nSUNBZ2NIVnphR2x1ZENBNE1TQXZMeUE0TVFvZ0lDQWdhWFJ2WWdvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRRdVlXeG5ieTUwY3pvNlFYSmpNVFkwTkM1aGNtTXhOalEwWDJOdmJuUnliMnhzWlhKZmNtVmtaV1Z0S0daeWIyMDZJR0o1ZEdWekxDQmhiVzkxYm5RNklHSjVkR1Z6TENCdmNHVnlZWFJ2Y2w5a1lYUmhPaUJpZVhSbGN5a2dMVDRnWW5sMFpYTTZDbUZ5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjbDl5WldSbFpXMDZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakUxT1MweE5qUUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UWTBORjlqYjI1MGNtOXNiR1Z5WDNKbFpHVmxiU2dLSUNBZ0lDOHZJQ0FnWm5KdmJUb2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCaGJXOTFiblE2SUdGeVl6UXVWV2x1ZEU0eU5UWXNDaUFnSUNBdkx5QWdJRzl3WlhKaGRHOXlYMlJoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWekxBb2dJQ0FnTHk4Z0tUb2dZWEpqTkM1VmFXNTBUalkwSUhzS0lDQWdJSEJ5YjNSdklETWdNUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem94TmpVS0lDQWdJQzh2SUhSb2FYTXVYMjl1YkhsRGIyNTBjbTlzYkdWeUtDa0tJQ0FnSUdOaGJHeHpkV0lnWDI5dWJIbERiMjUwY205c2JHVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakUyTmdvZ0lDQWdMeThnZEdocGN5NWZZMmhsWTJ0S2RYTjBhV1pwWTJGMGFXOXVLRzl3WlhKaGRHOXlYMlJoZEdFcENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR05oYkd4emRXSWdYMk5vWldOclNuVnpkR2xtYVdOaGRHbHZiZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem94TmpjS0lDQWdJQzh2SUhSb2FYTXVYM0poZEdWTWFXMXBkQ2dwQ2lBZ0lDQmpZV3hzYzNWaUlGOXlZWFJsVEdsdGFYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVFk0Q2lBZ0lDQXZMeUJqYjI1emRDQm1jbTl0UW1Gc0lEMGdkR2hwY3k1ZlltRnNZVzVqWlU5bUtHWnliMjBwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVE1LSUNBZ0lHTmhiR3h6ZFdJZ1gySmhiR0Z1WTJWUFpnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3hOamtLSUNBZ0lDOHZJR0Z6YzJWeWRDaG1jbTl0UW1Gc0xtNWhkR2wyWlNBK1BTQmhiVzkxYm5RdWJtRjBhWFpsTENBbmFXNXpkV1ptYVdOcFpXNTBKeWtLSUNBZ0lHUjFjQW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCaVBqMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuTjFabVpwWTJsbGJuUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVGN3Q2lBZ0lDQXZMeUIwYUdsekxtSmhiR0Z1WTJWektHWnliMjBwTG5aaGJIVmxJRDBnYm1WM0lHRnlZelF1VldsdWRFNHlOVFlvWm5KdmJVSmhiQzV1WVhScGRtVWdMU0JoYlc5MWJuUXVibUYwYVhabEtRb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JpTFFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTWlBdkx5QXpNZ29nSUNBZ1BEMEtJQ0FnSUdGemMyVnlkQ0F2THlCdmRtVnlabXh2ZHdvZ0lDQWdhVzUwWTE4eUlDOHZJRE15Q2lBZ0lDQmllbVZ5YndvZ0lDQWdjM2RoY0FvZ0lDQWdaR2xuSURFS0lDQWdJR0o4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TlRNS0lDQWdJQzh2SUhCMVlteHBZeUJpWVd4aGJtTmxjeUE5SUVKdmVFMWhjRHhCWkdSeVpYTnpMQ0JWYVc1MFRqSTFOajRvZXlCclpYbFFjbVZtYVhnNklDZGlKeUI5S1FvZ0lDQWdZbmwwWldNZ05DQXZMeUFpWWlJS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME5DNWhiR2R2TG5Sek9qRTNNQW9nSUNBZ0x5OGdkR2hwY3k1aVlXeGhibU5sY3lobWNtOXRLUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLR1p5YjIxQ1lXd3VibUYwYVhabElDMGdZVzF2ZFc1MExtNWhkR2wyWlNrS0lDQWdJSE4zWVhBS0lDQWdJR0p2ZUY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzFNUW9nSUNBZ0x5OGdjSFZpYkdsaklIUnZkR0ZzVTNWd2NHeDVJRDBnUjJ4dlltRnNVM1JoZEdVOFZXbHVkRTR5TlRZK0tIc2dhMlY1T2lBbmRDY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh6SUM4dklDSjBJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVGN4Q2lBZ0lDQXZMeUIwYUdsekxuUnZkR0ZzVTNWd2NHeDVMblpoYkhWbElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0eU5UWW9kR2hwY3k1MGIzUmhiRk4xY0hCc2VTNTJZV3gxWlM1dVlYUnBkbVVnTFNCaGJXOTFiblF1Ym1GMGFYWmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCaUxRb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdQRDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnZkbVZ5Wm14dmR3b2dJQ0FnWW53S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pvMU1Rb2dJQ0FnTHk4Z2NIVmliR2xqSUhSdmRHRnNVM1Z3Y0d4NUlEMGdSMnh2WW1Gc1UzUmhkR1U4VldsdWRFNHlOVFkrS0hzZ2EyVjVPaUFuZENjZ2ZTa0tJQ0FnSUdKNWRHVmpYek1nTHk4Z0luUWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBOQzVoYkdkdkxuUnpPakUzTVFvZ0lDQWdMeThnZEdocGN5NTBiM1JoYkZOMWNIQnNlUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLSFJvYVhNdWRHOTBZV3hUZFhCd2JIa3VkbUZzZFdVdWJtRjBhWFpsSUMwZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lITjNZWEFLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTkM1aGJHZHZMblJ6T2pFM05nb2dJQ0FnTHk4Z1kyOXVkSEp2Ykd4bGNqb2dibVYzSUdGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLU3dLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUTBMbUZzWjI4dWRITTZNVGMxTFRFNE1Rb2dJQ0FnTHk4Z2JtVjNJR0Z5WXpFMk5EUmZZMjl1ZEhKdmJHeGxjbDl5WldSbFpXMWZaWFpsYm5Rb2V3b2dJQ0FnTHk4Z0lDQmpiMjUwY205c2JHVnlPaUJ1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwTEFvZ0lDQWdMeThnSUNCbWNtOXRMQW9nSUNBZ0x5OGdJQ0JoYlc5MWJuUXNDaUFnSUNBdkx5QWdJR052WkdVc0NpQWdJQ0F2THlBZ0lHOXdaWEpoZEc5eVgyUmhkR0VzQ2lBZ0lDQXZMeUI5S1N3S0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5EUXVZV3huYnk1MGN6b3lOUW9nSUNBZ0x5OGdZMjl1YzNRZ1EwOUVSVjlUVlVORFJWTlRJRDBnYm1WM0lHRnlZelF1UW5sMFpTZ3dlRFV4S1FvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TlRFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRMExtRnNaMjh1ZEhNNk1UYzFMVEU0TVFvZ0lDQWdMeThnYm1WM0lHRnlZekUyTkRSZlkyOXVkSEp2Ykd4bGNsOXlaV1JsWlcxZlpYWmxiblFvZXdvZ0lDQWdMeThnSUNCamIyNTBjbTlzYkdWeU9pQnVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBMQW9nSUNBZ0x5OGdJQ0JtY205dExBb2dJQ0FnTHk4Z0lDQmhiVzkxYm5Rc0NpQWdJQ0F2THlBZ0lHTnZaR1VzQ2lBZ0lDQXZMeUFnSUc5d1pYSmhkRzl5WDJSaGRHRXNDaUFnSUNBdkx5QjlLU3dLSUNBZ0lHTnZibU5oZEFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TURBMk13b2dJQ0FnWTI5dVkyRjBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem94TnpNdE1UZ3lDaUFnSUNBdkx5QmxiV2wwS0FvZ0lDQWdMeThnSUNBblEyOXVkSEp2Ykd4bGNsSmxaR1ZsYlNjc0NpQWdJQ0F2THlBZ0lHNWxkeUJoY21NeE5qUTBYMk52Ym5SeWIyeHNaWEpmY21Wa1pXVnRYMlYyWlc1MEtIc0tJQ0FnSUM4dklDQWdJQ0JqYjI1MGNtOXNiR1Z5T2lCdVpYY2dZWEpqTkM1QlpHUnlaWE56S0ZSNGJpNXpaVzVrWlhJcExBb2dJQ0FnTHk4Z0lDQWdJR1p5YjIwc0NpQWdJQ0F2THlBZ0lDQWdZVzF2ZFc1MExBb2dJQ0FnTHk4Z0lDQWdJR052WkdVc0NpQWdJQ0F2THlBZ0lDQWdiM0JsY21GMGIzSmZaR0YwWVN3S0lDQWdJQzh2SUNBZ2ZTa3NDaUFnSUNBdkx5QXBDaUFnSUNCaWVYUmxZeUExSUM4dklEQjRNREF3TWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnd1pHVmxNVFJtTlNBdkx5QnRaWFJvYjJRZ0lrTnZiblJ5YjJ4c1pYSlNaV1JsWlcwb0tHRmtaSEpsYzNNc1lXUmtjbVZ6Y3l4MWFXNTBNalUyTEdKNWRHVXNZbmwwWlZ0ZEtTa2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORFF1WVd4bmJ5NTBjem94T0RNS0lDQWdJQzh2SUhKbGRIVnliaUJ1WlhjZ1lYSmpOQzVWYVc1MFRqWTBLR052WkdVdWJtRjBhWFpsS1FvZ0lDQWdjSFZ6YUdsdWRDQTRNU0F2THlBNE1Rb2dJQ0FnYVhSdllnb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5ETXVZV3huYnk1MGN6bzZRWEpqTVRZME15NWZiMjVzZVU5M2JtVnlLQ2tnTFQ0Z2RtOXBaRG9LYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRNdVlXeG5ieTUwY3pvNlFYSmpNVFkwTXk1ZmIyNXNlVTkzYm1WeU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5ETXVZV3huYnk1MGN6b3pNQW9nSUNBZ0x5OGdZWE56WlhKMEtIUm9hWE11WVhKak9EaGZhWE5mYjNkdVpYSW9ibVYzSUdGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLU2t1Ym1GMGFYWmxJRDA5UFNCMGNuVmxMQ0FuYjI1c2VWOXZkMjVsY2ljcENpQWdJQ0IwZUc0Z1UyVnVaR1Z5Q2lBZ0lDQmpZV3hzYzNWaUlHRnlZemc0WDJselgyOTNibVZ5Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1oyVjBZbWwwQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCdmJteDVYMjkzYm1WeUNpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qcEJjbU14TmpRekxtRnlZekUyTkROZmMyVjBYMlJ2WTNWdFpXNTBLRzVoYldVNklHSjVkR1Z6TENCMWNtazZJR0o1ZEdWekxDQm9ZWE5vT2lCaWVYUmxjeWtnTFQ0Z2RtOXBaRG9LWVhKak1UWTBNMTl6WlhSZlpHOWpkVzFsYm5RNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qTTBMVE0xQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZekUyTkROZmMyVjBYMlJ2WTNWdFpXNTBLRzVoYldVNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWekxDQjFjbWs2SUdGeVl6UXVVM1J5TENCb1lYTm9PaUJoY21NMExrUjVibUZ0YVdOQ2VYUmxjeWs2SUhadmFXUWdld29nSUNBZ2NISnZkRzhnTXlBd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qTTJDaUFnSUNBdkx5QjBhR2x6TGw5dmJteDVUM2R1WlhJb0tRb2dJQ0FnWTJGc2JITjFZaUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pwQmNtTXhOalF6TGw5dmJteDVUM2R1WlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRekxtRnNaMjh1ZEhNNk16Y0tJQ0FnSUM4dklHRnpjMlZ5ZENodVlXMWxMbUo1ZEdWekxteGxibWQwYUNBK0lEQXNJQ2RsYlhCMGVWOXVZVzFsSnlrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdiR1Z1Q2lBZ0lDQmtkWEFLSUNBZ0lHRnpjMlZ5ZENBdkx5QmxiWEIwZVY5dVlXMWxDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBNeTVoYkdkdkxuUnpPak00Q2lBZ0lDQXZMeUJqYjI1emRDQnlaV01nUFNCdVpYY2dZWEpqTVRZME0xOWtiMk4xYldWdWRGOXlaV052Y21Rb2V5QjFjbWtzSUdoaGMyZ3NJSFJwYldWemRHRnRjRG9nYm1WM0lHRnlZelF1VldsdWRFNDJOQ2hIYkc5aVlXd3VjbTkxYm1RcElIMHBDaUFnSUNCbmJHOWlZV3dnVW05MWJtUUtJQ0FnSUdsMGIySUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnYkdWdUNpQWdJQ0JrZFhBS0lDQWdJR052ZG1WeUlESUtJQ0FnSUhCMWMyaHBiblFnTVRJZ0x5OGdNVElLSUNBZ0lDc0tJQ0FnSUdsMGIySUtJQ0FnSUdWNGRISmhZM1FnTmlBeUNpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnd01EQmpDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qSTJDaUFnSUNBdkx5QndkV0pzYVdNZ1pHOWpkVzFsYm5SeklEMGdRbTk0VFdGd1BHRnlZelF1UkhsdVlXMXBZMEo1ZEdWekxDQmhjbU14TmpRelgyUnZZM1Z0Wlc1MFgzSmxZMjl5WkQ0b2V5QnJaWGxRY21WbWFYZzZJQ2RoY21NeE5qUXpYMlJ2WXljZ2ZTa0tJQ0FnSUdKNWRHVmpJREl6SUM4dklDSmhjbU14TmpRelgyUnZZeUlLSUNBZ0lHWnlZVzFsWDJScFp5QXRNd29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pNNUNpQWdJQ0F2THlCMGFHbHpMbVJ2WTNWdFpXNTBjeWh1WVcxbEtTNTJZV3gxWlNBOUlISmxZeTVqYjNCNUtDa0tJQ0FnSUdSMWNBb2dJQ0FnWW05NFgyUmxiQW9nSUNBZ2NHOXdDaUFnSUNCemQyRndDaUFnSUNCaWIzaGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pJM0NpQWdJQ0F2THlCd2RXSnNhV01nWkc5amRXMWxiblJMWlhseklEMGdRbTk0UEdGeVl6UXVSSGx1WVcxcFkwSjVkR1Z6VzEwK0tIc2dhMlY1T2lBbllYSmpNVFkwTTE5a2IyTnpKeUI5S1FvZ0lDQWdZbmwwWldNZ09TQXZMeUFpWVhKak1UWTBNMTlrYjJOeklnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5ETXVZV3huYnk1MGN6bzBNQW9nSUNBZ0x5OGdhV1lnS0NGMGFHbHpMbVJ2WTNWdFpXNTBTMlY1Y3k1bGVHbHpkSE1wSUhzS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1ltNTZJR0Z5WXpFMk5ETmZjMlYwWDJSdlkzVnRaVzUwWDJWc2MyVmZZbTlrZVVBeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qUXhDaUFnSUNBdkx5QjBhR2x6TG1SdlkzVnRaVzUwUzJWNWN5NTJZV3gxWlNBOUlGdHVZVzFsWFFvZ0lDQWdZbmwwWldNZ01UWWdMeThnTUhnd01EQXdDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUTUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0JqWVd4c2MzVmlJR1I1Ym1GdGFXTmZZWEp5WVhsZlkyOXVZMkYwWDJKNWRHVmZiR1Z1WjNSb1gyaGxZV1FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalF6TG1Gc1oyOHVkSE02TWpjS0lDQWdJQzh2SUhCMVlteHBZeUJrYjJOMWJXVnVkRXRsZVhNZ1BTQkNiM2c4WVhKak5DNUVlVzVoYldsalFubDBaWE5iWFQ0b2V5QnJaWGs2SUNkaGNtTXhOalF6WDJSdlkzTW5JSDBwQ2lBZ0lDQmllWFJsWXlBNUlDOHZJQ0poY21NeE5qUXpYMlJ2WTNNaUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qUXhDaUFnSUNBdkx5QjBhR2x6TG1SdlkzVnRaVzUwUzJWNWN5NTJZV3gxWlNBOUlGdHVZVzFsWFFvZ0lDQWdZbTk0WDJSbGJBb2dJQ0FnY0c5d0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qSTNDaUFnSUNBdkx5QndkV0pzYVdNZ1pHOWpkVzFsYm5STFpYbHpJRDBnUW05NFBHRnlZelF1UkhsdVlXMXBZMEo1ZEdWelcxMCtLSHNnYTJWNU9pQW5ZWEpqTVRZME0xOWtiMk56SnlCOUtRb2dJQ0FnWW5sMFpXTWdPU0F2THlBaVlYSmpNVFkwTTE5a2IyTnpJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORE11WVd4bmJ5NTBjem8wTVFvZ0lDQWdMeThnZEdocGN5NWtiMk4xYldWdWRFdGxlWE11ZG1Gc2RXVWdQU0JiYm1GdFpWMEtJQ0FnSUhOM1lYQUtJQ0FnSUdKdmVGOXdkWFFLQ21GeVl6RTJORE5mYzJWMFgyUnZZM1Z0Wlc1MFgyRm1kR1Z5WDJsbVgyVnNjMlZBTXpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRekxtRnNaMjh1ZEhNNk5EVUtJQ0FnSUM4dklHVnRhWFFvSjBSdlkzVnRaVzUwVlhCa1lYUmxaQ2NzSUc1bGR5QmhjbU14TmpRelgyUnZZM1Z0Wlc1MFgzVndaR0YwWldSZlpYWmxiblFvZXlCdVlXMWxMQ0IxY21rc0lHaGhjMmdnZlNrcENpQWdJQ0J3ZFhOb2FXNTBJRFlnTHk4Z05nb2dJQ0FnWm5KaGJXVmZaR2xuSURBS0lDQWdJQ3NLSUNBZ0lHUjFjQW9nSUNBZ2FYUnZZZ29nSUNBZ1pYaDBjbUZqZENBMklESUtJQ0FnSUdKNWRHVmpJREk0SUM4dklEQjRNREF3TmdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0J6ZDJGd0NpQWdJQ0JtY21GdFpWOWthV2NnTVFvZ0lDQWdLd29nSUNBZ2FYUnZZZ29nSUNBZ1pYaDBjbUZqZENBMklESUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNCamIyNWpZWFFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdZbmwwWldNZ05TQXZMeUF3ZURBd01ESUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRNbVJqTUROak16WWdMeThnYldWMGFHOWtJQ0pFYjJOMWJXVnVkRlZ3WkdGMFpXUW9LR0o1ZEdWYlhTeHpkSEpwYm1jc1lubDBaVnRkS1NraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnY21WMGMzVmlDZ3BoY21NeE5qUXpYM05sZEY5a2IyTjFiV1Z1ZEY5bGJITmxYMkp2WkhsQU1qb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUXpMbUZzWjI4dWRITTZNamNLSUNBZ0lDOHZJSEIxWW14cFl5QmtiMk4xYldWdWRFdGxlWE1nUFNCQ2IzZzhZWEpqTkM1RWVXNWhiV2xqUW5sMFpYTmJYVDRvZXlCclpYazZJQ2RoY21NeE5qUXpYMlJ2WTNNbklIMHBDaUFnSUNCaWVYUmxZeUE1SUM4dklDSmhjbU14TmpRelgyUnZZM01pQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pRekNpQWdJQ0F2THlCMGFHbHpMbVJ2WTNWdFpXNTBTMlY1Y3k1MllXeDFaU0E5SUZzdUxpNTBhR2x6TG1SdlkzVnRaVzUwUzJWNWN5NTJZV3gxWlN3Z2JtRnRaVjBLSUNBZ0lHSnZlRjluWlhRS0lDQWdJR0Z6YzJWeWRDQXZMeUJDYjNnZ2JYVnpkQ0JvWVhabElIWmhiSFZsQ2lBZ0lDQmllWFJsWXlBeE5pQXZMeUF3ZURBd01EQUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJR05oYkd4emRXSWdaSGx1WVcxcFkxOWhjbkpoZVY5amIyNWpZWFJmWW5sMFpWOXNaVzVuZEdoZmFHVmhaQW9nSUNBZ1pIVndDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWUtJQ0FnSUhOM1lYQUtJQ0FnSUdWNGRISmhZM1FnTWlBd0NpQWdJQ0JrYVdjZ01Rb2dJQ0FnYVc1MFkxOHpJQzh2SURJS0lDQWdJQ29LSUNBZ0lHUnBaeUF4Q2lBZ0lDQnNaVzRLSUNBZ0lITjFZbk4wY21sdVp6TUtJQ0FnSUhOM1lYQUtJQ0FnSUdOaGJHeHpkV0lnWkhsdVlXMXBZMTloY25KaGVWOWpiMjVqWVhSZllubDBaVjlzWlc1bmRHaGZhR1ZoWkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRNdVlXeG5ieTUwY3pveU53b2dJQ0FnTHk4Z2NIVmliR2xqSUdSdlkzVnRaVzUwUzJWNWN5QTlJRUp2ZUR4aGNtTTBMa1I1Ym1GdGFXTkNlWFJsYzF0ZFBpaDdJR3RsZVRvZ0oyRnlZekUyTkROZlpHOWpjeWNnZlNrS0lDQWdJR0o1ZEdWaklEa2dMeThnSW1GeVl6RTJORE5mWkc5amN5SUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUXpMbUZzWjI4dWRITTZORE1LSUNBZ0lDOHZJSFJvYVhNdVpHOWpkVzFsYm5STFpYbHpMblpoYkhWbElEMGdXeTR1TG5Sb2FYTXVaRzlqZFcxbGJuUkxaWGx6TG5aaGJIVmxMQ0J1WVcxbFhRb2dJQ0FnWW05NFgyUmxiQW9nSUNBZ2NHOXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBNeTVoYkdkdkxuUnpPakkzQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdaRzlqZFcxbGJuUkxaWGx6SUQwZ1FtOTRQR0Z5WXpRdVJIbHVZVzFwWTBKNWRHVnpXMTArS0hzZ2EyVjVPaUFuWVhKak1UWTBNMTlrYjJOekp5QjlLUW9nSUNBZ1lubDBaV01nT1NBdkx5QWlZWEpqTVRZME0xOWtiMk56SWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRNdVlXeG5ieTUwY3pvME13b2dJQ0FnTHk4Z2RHaHBjeTVrYjJOMWJXVnVkRXRsZVhNdWRtRnNkV1VnUFNCYkxpNHVkR2hwY3k1a2IyTjFiV1Z1ZEV0bGVYTXVkbUZzZFdVc0lHNWhiV1ZkQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmliM2hmY0hWMENpQWdJQ0JpSUdGeVl6RTJORE5mYzJWMFgyUnZZM1Z0Wlc1MFgyRm1kR1Z5WDJsbVgyVnNjMlZBTXdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalF6TG1Gc1oyOHVkSE02T2tGeVl6RTJORE11WVhKak1UWTBNMTluWlhSZlpHOWpkVzFsYm5Rb2JtRnRaVG9nWW5sMFpYTXBJQzArSUdKNWRHVnpPZ3BoY21NeE5qUXpYMmRsZEY5a2IyTjFiV1Z1ZERvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRekxtRnNaMjh1ZEhNNk5EZ3RORGtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaDdJSEpsWVdSdmJteDVPaUIwY25WbElIMHBDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFkwTTE5blpYUmZaRzlqZFcxbGJuUW9ibUZ0WlRvZ1lYSmpOQzVFZVc1aGJXbGpRbmwwWlhNcE9pQmhjbU14TmpRelgyUnZZM1Z0Wlc1MFgzSmxZMjl5WkNCN0NpQWdJQ0J3Y205MGJ5QXhJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalF6TG1Gc1oyOHVkSE02TWpZS0lDQWdJQzh2SUhCMVlteHBZeUJrYjJOMWJXVnVkSE1nUFNCQ2IzaE5ZWEE4WVhKak5DNUVlVzVoYldsalFubDBaWE1zSUdGeVl6RTJORE5mWkc5amRXMWxiblJmY21WamIzSmtQaWg3SUd0bGVWQnlaV1pwZURvZ0oyRnlZekUyTkROZlpHOWpKeUI5S1FvZ0lDQWdZbmwwWldNZ01qTWdMeThnSW1GeVl6RTJORE5mWkc5aklnb2dJQ0FnWm5KaGJXVmZaR2xuSUMweENpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5qUXpMbUZzWjI4dWRITTZOVEFLSUNBZ0lDOHZJR0Z6YzJWeWRDaDBhR2x6TG1SdlkzVnRaVzUwY3lodVlXMWxLUzVsZUdsemRITXNJQ2R1YjNSZlptOTFibVFuS1FvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmYkdWdUNpQWdJQ0JpZFhKNUlERUtJQ0FnSUdGemMyVnlkQ0F2THlCdWIzUmZabTkxYm1RS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRekxtRnNaMjh1ZEhNNk5URUtJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbVJ2WTNWdFpXNTBjeWh1WVcxbEtTNTJZV3gxWlM1amIzQjVLQ2tLSUNBZ0lHSnZlRjluWlhRS0lDQWdJR0Z6YzJWeWRDQXZMeUJDYjNnZ2JYVnpkQ0JvWVhabElIWmhiSFZsQ2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pwQmNtTXhOalF6TG1GeVl6RTJORE5mY21WdGIzWmxYMlJ2WTNWdFpXNTBLRzVoYldVNklHSjVkR1Z6S1NBdFBpQjJiMmxrT2dwaGNtTXhOalF6WDNKbGJXOTJaVjlrYjJOMWJXVnVkRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOalF6TG1Gc1oyOHVkSE02TlRRdE5UVUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UWTBNMTl5WlcxdmRtVmZaRzlqZFcxbGJuUW9ibUZ0WlRvZ1lYSmpOQzVFZVc1aGJXbGpRbmwwWlhNcE9pQjJiMmxrSUhzS0lDQWdJSEJ5YjNSdklERWdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTJORE11WVd4bmJ5NTBjem8xTmdvZ0lDQWdMeThnZEdocGN5NWZiMjVzZVU5M2JtVnlLQ2tLSUNBZ0lHTmhiR3h6ZFdJZ2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5ETXVZV3huYnk1MGN6bzZRWEpqTVRZME15NWZiMjVzZVU5M2JtVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBNeTVoYkdkdkxuUnpPakkyQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdaRzlqZFcxbGJuUnpJRDBnUW05NFRXRndQR0Z5WXpRdVJIbHVZVzFwWTBKNWRHVnpMQ0JoY21NeE5qUXpYMlJ2WTNWdFpXNTBYM0psWTI5eVpENG9leUJyWlhsUWNtVm1hWGc2SUNkaGNtTXhOalF6WDJSdll5Y2dmU2tLSUNBZ0lHSjVkR1ZqSURJeklDOHZJQ0poY21NeE5qUXpYMlJ2WXlJS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRZME15NWhiR2R2TG5Sek9qVTNDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVrYjJOMWJXVnVkSE1vYm1GdFpTa3VaWGhwYzNSekxDQW5ibTkwWDJadmRXNWtKeWtLSUNBZ0lHUjFjQW9nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JoYzNObGNuUWdMeThnYm05MFgyWnZkVzVrQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pVNENpQWdJQ0F2THlCamIyNXpkQ0J3Y21sdmNpQTlJSFJvYVhNdVpHOWpkVzFsYm5SektHNWhiV1VwTG5aaGJIVmxMbU52Y0hrb0tRb2dJQ0FnWkhWd0NpQWdJQ0JpYjNoZloyVjBDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMk5ETXVZV3huYnk1MGN6bzFPUW9nSUNBZ0x5OGdkR2hwY3k1a2IyTjFiV1Z1ZEhNb2JtRnRaU2t1WkdWc1pYUmxLQ2tLSUNBZ0lITjNZWEFLSUNBZ0lHSnZlRjlrWld3S0lDQWdJSEJ2Y0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUyTkRNdVlXeG5ieTUwY3pvMk1nb2dJQ0FnTHk4Z1pXMXBkQ2duUkc5amRXMWxiblJTWlcxdmRtVmtKeXdnYm1WM0lHRnlZekUyTkROZlpHOWpkVzFsYm5SZmNtVnRiM1psWkY5bGRtVnVkQ2g3SUc1aGJXVXNJSFZ5YVRvZ2NISnBiM0l1ZFhKcExDQm9ZWE5vT2lCd2NtbHZjaTVvWVhOb0lIMHBLUW9nSUNBZ1pIVndDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWUtJQ0FnSUdScFp5QXhDaUFnSUNCcGJuUmpYek1nTHk4Z01nb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWUtJQ0FnSUdScFp5QXlDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJR1JwWnlBeUNpQWdJQ0J6ZFdKemRISnBibWN6Q2lBZ0lDQmthV2NnTWdvZ0lDQWdiR1Z1Q2lBZ0lDQjFibU52ZG1WeUlETUtJQ0FnSUhWdVkyOTJaWElnTXdvZ0lDQWdkVzVqYjNabGNpQXlDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR3hsYmdvZ0lDQWdjSFZ6YUdsdWRDQTJJQzh2SURZS0lDQWdJQ3NLSUNBZ0lHUjFjQW9nSUNBZ2FYUnZZZ29nSUNBZ1pYaDBjbUZqZENBMklESUtJQ0FnSUdKNWRHVmpJREk0SUM4dklEQjRNREF3TmdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JrYVdjZ013b2dJQ0FnYkdWdUNpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lDc0tJQ0FnSUdsMGIySUtJQ0FnSUdWNGRISmhZM1FnTmlBeUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJR052Ym1OaGRBb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCaWVYUmxZeUExSUM4dklEQjRNREF3TWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhoaFpUZGhOR1poTUNBdkx5QnRaWFJvYjJRZ0lrUnZZM1Z0Wlc1MFVtVnRiM1psWkNnb1lubDBaVnRkTEhOMGNtbHVaeXhpZVhSbFcxMHBLU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFkwTXk1aGJHZHZMblJ6T2pwQmNtTXhOalF6TG1GeVl6RTJORE5mWjJWMFgyRnNiRjlrYjJOMWJXVnVkSE1vS1NBdFBpQmllWFJsY3pvS1lYSmpNVFkwTTE5blpYUmZZV3hzWDJSdlkzVnRaVzUwY3pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TmpRekxtRnNaMjh1ZEhNNk1qY0tJQ0FnSUM4dklIQjFZbXhwWXlCa2IyTjFiV1Z1ZEV0bGVYTWdQU0JDYjNnOFlYSmpOQzVFZVc1aGJXbGpRbmwwWlhOYlhUNG9leUJyWlhrNklDZGhjbU14TmpRelgyUnZZM01uSUgwcENpQWdJQ0JpZVhSbFl5QTVJQzh2SUNKaGNtTXhOalF6WDJSdlkzTWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UWTBNeTVoYkdkdkxuUnpPalk1Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdkR2hwY3k1a2IyTjFiV1Z1ZEV0bGVYTXVkbUZzZFdVS0lDQWdJR0p2ZUY5blpYUUtJQ0FnSUdGemMyVnlkQ0F2THlCQ2IzZ2diWFZ6ZENCb1lYWmxJSFpoYkhWbENpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qcEJjbU14TlRrMExsOXZibXg1VDNkdVpYSW9LU0F0UGlCMmIybGtPZ3B6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pwQmNtTXhOVGswTGw5dmJteDVUM2R1WlhJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qSXlDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVoY21NNE9GOXBjMTl2ZDI1bGNpaHVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBLUzV1WVhScGRtVWdQVDA5SUhSeWRXVXNJQ2R2Ym14NVgyOTNibVZ5SnlrS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpPRGhmYVhOZmIzZHVaWElLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCblpYUmlhWFFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5dWJIbGZiM2R1WlhJS0lDQWdJSEpsZEhOMVlnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk9rRnlZekUxT1RRdVlYSmpNVFU1TkY5elpYUmZhWE56ZFdGaWJHVW9abXhoWnpvZ1lubDBaWE1wSUMwK0lIWnZhV1E2Q21GeVl6RTFPVFJmYzJWMFgybHpjM1ZoWW14bE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMU9UUXVZV3huYnk1MGN6b3lOeTB5T0FvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU14TlRrMFgzTmxkRjlwYzNOMVlXSnNaU2htYkdGbk9pQmhjbU0wTGtKdmIyd3BPaUIyYjJsa0lIc0tJQ0FnSUhCeWIzUnZJREVnTUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUxT1RRdVlXeG5ieTUwY3pveU9Rb2dJQ0FnTHk4Z2RHaHBjeTVmYjI1c2VVOTNibVZ5S0NrS0lDQWdJR05oYkd4emRXSWdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem82UVhKak1UVTVOQzVmYjI1c2VVOTNibVZ5Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pFMENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UVTVORjlwYzNOMVlXSnNaU0E5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW05dmJENG9leUJyWlhrNklDZGhjbU14TlRrMFgybHpjeWNnZlNrZ0x5OGdWSEoxWlNBOUlHbHpjM1ZoWW14bENpQWdJQ0JpZVhSbFl5QXhOeUF2THlBaVlYSmpNVFU1TkY5cGMzTWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPak13Q2lBZ0lDQXZMeUIwYUdsekxtRnlZekUxT1RSZmFYTnpkV0ZpYkdVdWRtRnNkV1VnUFNCbWJHRm5DaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNCeVpYUnpkV0lLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPanBCY21NeE5UazBMbUZ5WXpFMU9UUmZhWE56ZFdVb2RHODZJR0o1ZEdWekxDQmhiVzkxYm5RNklHSjVkR1Z6TENCa1lYUmhPaUJpZVhSbGN5a2dMVDRnZG05cFpEb0tZWEpqTVRVNU5GOXBjM04xWlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk16UXRNelVLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFU1TkY5cGMzTjFaU2gwYnpvZ1lYSmpOQzVCWkdSeVpYTnpMQ0JoYlc5MWJuUTZJR0Z5WXpRdVZXbHVkRTR5TlRZc0lHUmhkR0U2SUdGeVl6UXVSSGx1WVcxcFkwSjVkR1Z6S1RvZ2RtOXBaQ0I3Q2lBZ0lDQndjbTkwYnlBeklEQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5UazBMbUZzWjI4dWRITTZNellLSUNBZ0lDOHZJSFJvYVhNdVgyOXViSGxQZDI1bGNpZ3BDaUFnSUNCallXeHNjM1ZpSUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk9rRnlZekUxT1RRdVgyOXViSGxQZDI1bGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMU9UUXVZV3huYnk1MGN6b3pOd29nSUNBZ0x5OGdZWE56WlhKMEtHRnRiM1Z1ZEM1dVlYUnBkbVVnUGlBd2Jpd2dKMmx1ZG1Gc2FXUmZZVzF2ZFc1MEp5a0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnY0hWemFHSjVkR1Z6SURCNENpQWdJQ0JpUGdvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUmZZVzF2ZFc1MENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qRTBDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFU1TkY5cGMzTjFZV0pzWlNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFtOXZiRDRvZXlCclpYazZJQ2RoY21NeE5UazBYMmx6Y3ljZ2ZTa2dMeThnVkhKMVpTQTlJR2x6YzNWaFlteGxDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdNVGNnTHk4Z0ltRnlZekUxT1RSZmFYTnpJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem96T0FvZ0lDQWdMeThnWVhOelpYSjBLSFJvYVhNdVlYSmpNVFU1TkY5cGMzTjFZV0pzWlM1b1lYTldZV3gxWlNBbUppQjBhR2x6TG1GeVl6RTFPVFJmYVhOemRXRmliR1V1ZG1Gc2RXVXVibUYwYVhabElEMDlQU0IwY25WbExDQW5hWE56ZFdGdVkyVmZaR2x6WVdKc1pXUW5LUW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ1lYSmpNVFU1TkY5cGMzTjFaVjlpYjI5c1gyWmhiSE5sUURNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk1UUUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOVGswWDJsemMzVmhZbXhsSUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1Q2IyOXNQaWg3SUd0bGVUb2dKMkZ5WXpFMU9UUmZhWE56SnlCOUtTQXZMeUJVY25WbElEMGdhWE56ZFdGaWJHVUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFl5QXhOeUF2THlBaVlYSmpNVFU1TkY5cGMzTWlDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk16Z0tJQ0FnSUM4dklHRnpjMlZ5ZENoMGFHbHpMbUZ5WXpFMU9UUmZhWE56ZFdGaWJHVXVhR0Z6Vm1Gc2RXVWdKaVlnZEdocGN5NWhjbU14TlRrMFgybHpjM1ZoWW14bExuWmhiSFZsTG01aGRHbDJaU0E5UFQwZ2RISjFaU3dnSjJsemMzVmhibU5sWDJScGMyRmliR1ZrSnlrS0lDQWdJR2RsZEdKcGRBb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmllaUJoY21NeE5UazBYMmx6YzNWbFgySnZiMnhmWm1Gc2MyVkFNd29nSUNBZ2FXNTBZMTh4SUM4dklERUtDbUZ5WXpFMU9UUmZhWE56ZFdWZlltOXZiRjl0WlhKblpVQTBPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem96T0FvZ0lDQWdMeThnWVhOelpYSjBLSFJvYVhNdVlYSmpNVFU1TkY5cGMzTjFZV0pzWlM1b1lYTldZV3gxWlNBbUppQjBhR2x6TG1GeVl6RTFPVFJmYVhOemRXRmliR1V1ZG1Gc2RXVXVibUYwYVhabElEMDlQU0IwY25WbExDQW5hWE56ZFdGdVkyVmZaR2x6WVdKc1pXUW5LUW9nSUNBZ1lYTnpaWEowSUM4dklHbHpjM1ZoYm1ObFgyUnBjMkZpYkdWa0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qUXdDaUFnSUNBdkx5QjBhR2x6TG1GeVl6RTBNVEJmYVhOemRXVmZZbmxmY0dGeWRHbDBhVzl1S0hSdkxDQnVaWGNnWVhKak5DNUJaR1J5WlhOektDa3NJR0Z0YjNWdWRDd2daR0YwWVNrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdZbmwwWldOZk1TQXZMeUJoWkdSeUlFRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGWk5VaEdTMUVLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1puSmhiV1ZmWkdsbklDMHhDaUFnSUNCallXeHNjM1ZpSUdGeVl6RTBNVEJmYVhOemRXVmZZbmxmY0dGeWRHbDBhVzl1Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pReENpQWdJQ0F2THlCbGJXbDBLQ2RKYzNOMVpTY3NJRzVsZHlCaGNtTXhOVGswWDJsemMzVmxYMlYyWlc1MEtIc2dkRzhzSUdGdGIzVnVkQ3dnWkdGMFlTQjlLU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXRNd29nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCamIyNWpZWFFLSUNBZ0lHSjVkR1ZqSURJMElDOHZJREI0TURBME1nb2dJQ0FnWTI5dVkyRjBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1lubDBaV01nTlNBdkx5QXdlREF3TURJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnY0hWemFHSjVkR1Z6SURCNFpqSmxPVGs0WVdZZ0x5OGdiV1YwYUc5a0lDSkpjM04xWlNnb1lXUmtjbVZ6Y3l4MWFXNTBNalUyTEdKNWRHVmJYU2twSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJSEpsZEhOMVlnb0tZWEpqTVRVNU5GOXBjM04xWlY5aWIyOXNYMlpoYkhObFFETTZDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWWlCaGNtTXhOVGswWDJsemMzVmxYMkp2YjJ4ZmJXVnlaMlZBTkFvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOVGswTG1Gc1oyOHVkSE02T2tGeVl6RTFPVFF1WVhKak1UVTVORjl5WldSbFpXMUdjbTl0S0daeWIyMDZJR0o1ZEdWekxDQmhiVzkxYm5RNklHSjVkR1Z6TENCa1lYUmhPaUJpZVhSbGN5a2dMVDRnZG05cFpEb0tZWEpqTVRVNU5GOXlaV1JsWlcxR2NtOXRPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem8wTkMwME5Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5UazBYM0psWkdWbGJVWnliMjBvWm5KdmJUb2dZWEpqTkM1QlpHUnlaWE56TENCaGJXOTFiblE2SUdGeVl6UXVWV2x1ZEU0eU5UWXNJR1JoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWektUb2dkbTlwWkNCN0NpQWdJQ0J3Y205MGJ5QXpJREFLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPalEyQ2lBZ0lDQXZMeUJqYjI1emRDQnpaVzVrWlhJZ1BTQnVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBDaUFnSUNCMGVHNGdVMlZ1WkdWeUNpQWdJQ0JrZFhBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk5EY0tJQ0FnSUM4dklHRnpjMlZ5ZENoelpXNWtaWElnUFQwOUlHWnliMjBnZkh3Z2RHaHBjeTVoY21NNE9GOXBjMTl2ZDI1bGNpaHpaVzVrWlhJcExtNWhkR2wyWlNBOVBUMGdkSEoxWlN3Z0oyNXZkRjloZFhSb0p5a0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnUFQwS0lDQWdJR0p1ZWlCaGNtTXhOVGswWDNKbFpHVmxiVVp5YjIxZlltOXZiRjkwY25WbFFESUtJQ0FnSUdaeVlXMWxYMlJwWnlBeENpQWdJQ0JqWVd4c2MzVmlJR0Z5WXpnNFgybHpYMjkzYm1WeUNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaMlYwWW1sMENpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdQVDBLSUNBZ0lHSjZJR0Z5WXpFMU9UUmZjbVZrWldWdFJuSnZiVjlpYjI5c1gyWmhiSE5sUURNS0NtRnlZekUxT1RSZmNtVmtaV1Z0Um5KdmJWOWliMjlzWDNSeWRXVkFNam9LSUNBZ0lHbHVkR05mTVNBdkx5QXhDZ3BoY21NeE5UazBYM0psWkdWbGJVWnliMjFmWW05dmJGOXRaWEpuWlVBME9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMU9UUXVZV3huYnk1MGN6bzBOd29nSUNBZ0x5OGdZWE56WlhKMEtITmxibVJsY2lBOVBUMGdabkp2YlNCOGZDQjBhR2x6TG1GeVl6ZzRYMmx6WDI5M2JtVnlLSE5sYm1SbGNpa3VibUYwYVhabElEMDlQU0IwY25WbExDQW5ibTkwWDJGMWRHZ25LUW9nSUNBZ1lYTnpaWEowSUM4dklHNXZkRjloZFhSb0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qUTRDaUFnSUNBdkx5QmhjM05sY25Rb1lXMXZkVzUwTG01aGRHbDJaU0ErSURCdUxDQW5hVzUyWVd4cFpGOWhiVzkxYm5RbktRb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnS0lDQWdJR0krQ2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpGOWhiVzkxYm5RS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pvMU13b2dJQ0FnTHk4Z2NIVmliR2xqSUdKaGJHRnVZMlZ6SUQwZ1FtOTRUV0Z3UEVGa1pISmxjM01zSUZWcGJuUk9NalUyUGloN0lHdGxlVkJ5WldacGVEb2dKMkluSUgwcENpQWdJQ0JpZVhSbFl5QTBJQzh2SUNKaUlnb2dJQ0FnWm5KaGJXVmZaR2xuSUMwekNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qUTVDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVpWVd4aGJtTmxjeWhtY205dEtTNWxlR2x6ZEhNZ0ppWWdkR2hwY3k1aVlXeGhibU5sY3lobWNtOXRLUzUyWVd4MVpTNXVZWFJwZG1VZ1BqMGdZVzF2ZFc1MExtNWhkR2wyWlN3Z0oybHVjM1ZtWm1samFXVnVkRjlpWVd4aGJtTmxKeWtLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ1lYSmpNVFU1TkY5eVpXUmxaVzFHY205dFgySnZiMnhmWm1Gc2MyVkFOd29nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUdKdmVGOW5aWFFLSUNBZ0lHRnpjMlZ5ZENBdkx5QkNiM2dnYlhWemRDQm9ZWFpsSUhaaGJIVmxDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdJK1BRb2dJQ0FnWW5vZ1lYSmpNVFU1TkY5eVpXUmxaVzFHY205dFgySnZiMnhmWm1Gc2MyVkFOd29nSUNBZ2FXNTBZMTh4SUM4dklERUtDbUZ5WXpFMU9UUmZjbVZrWldWdFJuSnZiVjlpYjI5c1gyMWxjbWRsUURnNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qUTVDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVpWVd4aGJtTmxjeWhtY205dEtTNWxlR2x6ZEhNZ0ppWWdkR2hwY3k1aVlXeGhibU5sY3lobWNtOXRLUzUyWVd4MVpTNXVZWFJwZG1VZ1BqMGdZVzF2ZFc1MExtNWhkR2wyWlN3Z0oybHVjM1ZtWm1samFXVnVkRjlpWVd4aGJtTmxKeWtLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBibk4xWm1acFkybGxiblJmWW1Gc1lXNWpaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem8xTUFvZ0lDQWdMeThnZEdocGN5NWlZV3hoYm1ObGN5aG1jbTl0S1M1MllXeDFaU0E5SUc1bGR5QmhjbU0wTGxWcGJuUk9NalUyS0hSb2FYTXVZbUZzWVc1alpYTW9abkp2YlNrdWRtRnNkV1V1Ym1GMGFYWmxJQzBnWVcxdmRXNTBMbTVoZEdsMlpTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBd0NpQWdJQ0JrZFhBS0lDQWdJR0p2ZUY5blpYUUtJQ0FnSUdGemMyVnlkQ0F2THlCQ2IzZ2diWFZ6ZENCb1lYWmxJSFpoYkhWbENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR0l0Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNBOFBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5MlpYSm1iRzkzQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lHSjZaWEp2Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmthV2NnTVFvZ0lDQWdZbndLSUNBZ0lIVnVZMjkyWlhJZ01nb2dJQ0FnYzNkaGNBb2dJQ0FnWW05NFgzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPalV4Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdkRzkwWVd4VGRYQndiSGtnUFNCSGJHOWlZV3hUZEdGMFpUeFZhVzUwVGpJMU5qNG9leUJyWlhrNklDZDBKeUI5S1FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqWHpNZ0x5OGdJblFpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem8xTVFvZ0lDQWdMeThnZEdocGN5NTBiM1JoYkZOMWNIQnNlUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLSFJvYVhNdWRHOTBZV3hUZFhCd2JIa3VkbUZzZFdVdWJtRjBhWFpsSUMwZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1lpMEtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnTXpJS0lDQWdJRHc5Q2lBZ0lDQmhjM05sY25RZ0x5OGdiM1psY21ac2IzY0tJQ0FnSUdKOENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5URUtJQ0FnSUM4dklIQjFZbXhwWXlCMGIzUmhiRk4xY0hCc2VTQTlJRWRzYjJKaGJGTjBZWFJsUEZWcGJuUk9NalUyUGloN0lHdGxlVG9nSjNRbklIMHBDaUFnSUNCaWVYUmxZMTh6SUM4dklDSjBJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTFPVFF1WVd4bmJ5NTBjem8xTVFvZ0lDQWdMeThnZEdocGN5NTBiM1JoYkZOMWNIQnNlUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLSFJvYVhNdWRHOTBZV3hUZFhCd2JIa3VkbUZzZFdVdWJtRjBhWFpsSUMwZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lITjNZWEFLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pVeUNpQWdJQ0F2THlCbGJXbDBLQ2RTWldSbFpXMG5MQ0J1WlhjZ1lYSmpNVFU1TkY5eVpXUmxaVzFmWlhabGJuUW9leUJtY205dExDQmhiVzkxYm5Rc0lHUmhkR0VnZlNrcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JpZVhSbFl5QXlOQ0F2THlBd2VEQXdORElLSUNBZ0lHTnZibU5oZEFvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR0o1ZEdWaklEVWdMeThnTUhnd01EQXlDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHSjVkR1ZqSURJNUlDOHZJRzFsZEdodlpDQWlVbVZrWldWdEtDaGhaR1J5WlhOekxIVnBiblF5TlRZc1lubDBaVnRkS1NraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnY21WMGMzVmlDZ3BoY21NeE5UazBYM0psWkdWbGJVWnliMjFmWW05dmJGOW1ZV3h6WlVBM09nb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0lnWVhKak1UVTVORjl5WldSbFpXMUdjbTl0WDJKdmIyeGZiV1Z5WjJWQU9Bb0tZWEpqTVRVNU5GOXlaV1JsWlcxR2NtOXRYMkp2YjJ4ZlptRnNjMlZBTXpvS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmlJR0Z5WXpFMU9UUmZjbVZrWldWdFJuSnZiVjlpYjI5c1gyMWxjbWRsUURRS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pwQmNtTXhOVGswTG1GeVl6RTFPVFJmY21Wa1pXVnRLR0Z0YjNWdWREb2dZbmwwWlhNc0lHUmhkR0U2SUdKNWRHVnpLU0F0UGlCMmIybGtPZ3BoY21NeE5UazBYM0psWkdWbGJUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5UazBMbUZzWjI4dWRITTZOVFV0TlRZS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTVRVNU5GOXlaV1JsWlcwb1lXMXZkVzUwT2lCaGNtTTBMbFZwYm5ST01qVTJMQ0JrWVhSaE9pQmhjbU0wTGtSNWJtRnRhV05DZVhSbGN5azZJSFp2YVdRZ2V3b2dJQ0FnY0hKdmRHOGdNaUF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pVM0NpQWdJQ0F2THlCamIyNXpkQ0JtY205dElEMGdibVYzSUdGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLUW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnWkhWd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qVTRDaUFnSUNBdkx5QmhjM05sY25Rb1lXMXZkVzUwTG01aGRHbDJaU0ErSURCdUxDQW5hVzUyWVd4cFpGOWhiVzkxYm5RbktRb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnS0lDQWdJR0krQ2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpGOWhiVzkxYm5RS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pvMU13b2dJQ0FnTHk4Z2NIVmliR2xqSUdKaGJHRnVZMlZ6SUQwZ1FtOTRUV0Z3UEVGa1pISmxjM01zSUZWcGJuUk9NalUyUGloN0lHdGxlVkJ5WldacGVEb2dKMkluSUgwcENpQWdJQ0JpZVhSbFl5QTBJQzh2SUNKaUlnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCa2RYQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5UazBMbUZzWjI4dWRITTZOVGtLSUNBZ0lDOHZJR0Z6YzJWeWRDaDBhR2x6TG1KaGJHRnVZMlZ6S0daeWIyMHBMbVY0YVhOMGN5QW1KaUIwYUdsekxtSmhiR0Z1WTJWektHWnliMjBwTG5aaGJIVmxMbTVoZEdsMlpTQStQU0JoYlc5MWJuUXVibUYwYVhabExDQW5hVzV6ZFdabWFXTnBaVzUwWDJKaGJHRnVZMlVuS1FvZ0lDQWdZbTk0WDJ4bGJnb2dJQ0FnWW5WeWVTQXhDaUFnSUNCaWVpQmhjbU14TlRrMFgzSmxaR1ZsYlY5aWIyOXNYMlpoYkhObFFETUtJQ0FnSUdaeVlXMWxYMlJwWnlBeENpQWdJQ0JpYjNoZloyVjBDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JpUGowS0lDQWdJR0o2SUdGeVl6RTFPVFJmY21Wa1pXVnRYMkp2YjJ4ZlptRnNjMlZBTXdvZ0lDQWdhVzUwWTE4eElDOHZJREVLQ21GeVl6RTFPVFJmY21Wa1pXVnRYMkp2YjJ4ZmJXVnlaMlZBTkRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TlRrMExtRnNaMjh1ZEhNNk5Ua0tJQ0FnSUM4dklHRnpjMlZ5ZENoMGFHbHpMbUpoYkdGdVkyVnpLR1p5YjIwcExtVjRhWE4wY3lBbUppQjBhR2x6TG1KaGJHRnVZMlZ6S0daeWIyMHBMblpoYkhWbExtNWhkR2wyWlNBK1BTQmhiVzkxYm5RdWJtRjBhWFpsTENBbmFXNXpkV1ptYVdOcFpXNTBYMkpoYkdGdVkyVW5LUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVjM1ZtWm1samFXVnVkRjlpWVd4aGJtTmxDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPall3Q2lBZ0lDQXZMeUIwYUdsekxtSmhiR0Z1WTJWektHWnliMjBwTG5aaGJIVmxJRDBnYm1WM0lHRnlZelF1VldsdWRFNHlOVFlvZEdocGN5NWlZV3hoYm1ObGN5aG1jbTl0S1M1MllXeDFaUzV1WVhScGRtVWdMU0JoYlc5MWJuUXVibUYwYVhabEtRb2dJQ0FnWm5KaGJXVmZaR2xuSURFS0lDQWdJR1IxY0FvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnWVhOelpYSjBJQzh2SUVKdmVDQnRkWE4wSUdoaGRtVWdkbUZzZFdVS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZaTBLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYeklnTHk4Z016SUtJQ0FnSUR3OUNpQWdJQ0JoYzNObGNuUWdMeThnYjNabGNtWnNiM2NLSUNBZ0lHbHVkR05mTWlBdkx5QXpNZ29nSUNBZ1lucGxjbThLSUNBZ0lITjNZWEFLSUNBZ0lHUnBaeUF4Q2lBZ0lDQmlmQW9nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0J6ZDJGd0NpQWdJQ0JpYjNoZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZOVEVLSUNBZ0lDOHZJSEIxWW14cFl5QjBiM1JoYkZOMWNIQnNlU0E5SUVkc2IySmhiRk4wWVhSbFBGVnBiblJPTWpVMlBpaDdJR3RsZVRvZ0ozUW5JSDBwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV05mTXlBdkx5QWlkQ0lLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2dSMnh2WW1Gc1UzUmhkR1VnWlhocGMzUnpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPall4Q2lBZ0lDQXZMeUIwYUdsekxuUnZkR0ZzVTNWd2NHeDVMblpoYkhWbElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0eU5UWW9kR2hwY3k1MGIzUmhiRk4xY0hCc2VTNTJZV3gxWlM1dVlYUnBkbVVnTFNCaGJXOTFiblF1Ym1GMGFYWmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCaUxRb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdQRDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnZkbVZ5Wm14dmR3b2dJQ0FnWW53S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pvMU1Rb2dJQ0FnTHk4Z2NIVmliR2xqSUhSdmRHRnNVM1Z3Y0d4NUlEMGdSMnh2WW1Gc1UzUmhkR1U4VldsdWRFNHlOVFkrS0hzZ2EyVjVPaUFuZENjZ2ZTa0tJQ0FnSUdKNWRHVmpYek1nTHk4Z0luUWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPall4Q2lBZ0lDQXZMeUIwYUdsekxuUnZkR0ZzVTNWd2NHeDVMblpoYkhWbElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0eU5UWW9kR2hwY3k1MGIzUmhiRk4xY0hCc2VTNTJZV3gxWlM1dVlYUnBkbVVnTFNCaGJXOTFiblF1Ym1GMGFYWmxLUW9nSUNBZ2MzZGhjQW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOVGswTG1Gc1oyOHVkSE02TmpJS0lDQWdJQzh2SUdWdGFYUW9KMUpsWkdWbGJTY3NJRzVsZHlCaGNtTXhOVGswWDNKbFpHVmxiVjlsZG1WdWRDaDdJR1p5YjIwc0lHRnRiM1Z1ZEN3Z1pHRjBZU0I5S1NrS0lDQWdJR1p5WVcxbFgyUnBaeUF3Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHTnZibU5oZEFvZ0lDQWdZbmwwWldNZ01qUWdMeThnTUhnd01EUXlDaUFnSUNCamIyNWpZWFFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmllWFJsWXlBMUlDOHZJREI0TURBd01nb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCaWVYUmxZeUF5T1NBdkx5QnRaWFJvYjJRZ0lsSmxaR1ZsYlNnb1lXUmtjbVZ6Y3l4MWFXNTBNalUyTEdKNWRHVmJYU2twSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJSEpsZEhOMVlnb0tZWEpqTVRVNU5GOXlaV1JsWlcxZlltOXZiRjltWVd4elpVQXpPZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdJZ1lYSmpNVFU1TkY5eVpXUmxaVzFmWW05dmJGOXRaWEpuWlVBMENnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUxT1RRdVlXeG5ieTUwY3pvNlFYSmpNVFU1TkM1aGNtTXhOVGswWDNSeVlXNXpabVZ5WDNkcGRHaGZaR0YwWVNoMGJ6b2dZbmwwWlhNc0lHRnRiM1Z1ZERvZ1lubDBaWE1zSUdSaGRHRTZJR0o1ZEdWektTQXRQaUJpZVhSbGN6b0tZWEpqTVRVNU5GOTBjbUZ1YzJabGNsOTNhWFJvWDJSaGRHRTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPalkyTFRZM0NpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpFMU9UUmZkSEpoYm5ObVpYSmZkMmwwYUY5a1lYUmhLSFJ2T2lCaGNtTTBMa0ZrWkhKbGMzTXNJR0Z0YjNWdWREb2dZWEpqTkM1VmFXNTBUakkxTml3Z1pHRjBZVG9nWVhKak5DNUVlVzVoYldsalFubDBaWE1wT2lCaGNtTTBMa0p2YjJ3Z2V3b2dJQ0FnY0hKdmRHOGdNeUF4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pZNUNpQWdJQ0F2THlCamIyNXpkQ0J5WlhNZ1BTQjBhR2x6TG1GeVl6SXdNRjkwY21GdWMyWmxjaWgwYnl3Z1lXMXZkVzUwS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB6Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNakF3WDNSeVlXNXpabVZ5Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFU1TkM1aGJHZHZMblJ6T2pjeENpQWdJQ0F2THlCeVpYUjFjbTRnY21WekNpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qcEJjbU14TlRrMExtRnlZekUxT1RSZmRISmhibk5tWlhKZlpuSnZiVjkzYVhSb1gyUmhkR0VvWm5KdmJUb2dZbmwwWlhNc0lIUnZPaUJpZVhSbGN5d2dZVzF2ZFc1ME9pQmllWFJsY3l3Z1pHRjBZVG9nWW5sMFpYTXBJQzArSUdKNWRHVnpPZ3BoY21NeE5UazBYM1J5WVc1elptVnlYMlp5YjIxZmQybDBhRjlrWVhSaE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMU9UUXVZV3huYnk1MGN6bzNOQzA0TUFvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU14TlRrMFgzUnlZVzV6Wm1WeVgyWnliMjFmZDJsMGFGOWtZWFJoS0FvZ0lDQWdMeThnSUNCbWNtOXRPaUJoY21NMExrRmtaSEpsYzNNc0NpQWdJQ0F2THlBZ0lIUnZPaUJoY21NMExrRmtaSEpsYzNNc0NpQWdJQ0F2THlBZ0lHRnRiM1Z1ZERvZ1lYSmpOQzVWYVc1MFRqSTFOaXdLSUNBZ0lDOHZJQ0FnWkdGMFlUb2dZWEpqTkM1RWVXNWhiV2xqUW5sMFpYTXNDaUFnSUNBdkx5QXBPaUJoY21NMExrSnZiMndnZXdvZ0lDQWdjSEp2ZEc4Z05DQXhDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UVTVOQzVoYkdkdkxuUnpPamd4Q2lBZ0lDQXZMeUJqYjI1emRDQnlaWE1nUFNCMGFHbHpMbUZ5WXpJd01GOTBjbUZ1YzJabGNrWnliMjBvWm5KdmJTd2dkRzhzSUdGdGIzVnVkQ2tLSUNBZ0lHWnlZVzFsWDJScFp5QXROQW9nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdOaGJHeHpkV0lnWVhKak1qQXdYM1J5WVc1elptVnlSbkp2YlFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUxT1RRdVlXeG5ieTUwY3pvNE1nb2dJQ0FnTHk4Z2NtVjBkWEp1SUhKbGN3b2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFMU9UUXVZV3huYnk1MGN6bzZRWEpqTVRVNU5DNWhjbU14TlRrMFgybHpYMmx6YzNWaFlteGxLQ2tnTFQ0Z1lubDBaWE02Q21GeVl6RTFPVFJmYVhOZmFYTnpkV0ZpYkdVNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRVNU5DNWhiR2R2TG5Sek9qRTBDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFU1TkY5cGMzTjFZV0pzWlNBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFtOXZiRDRvZXlCclpYazZJQ2RoY21NeE5UazBYMmx6Y3ljZ2ZTa2dMeThnVkhKMVpTQTlJR2x6YzNWaFlteGxDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdNVGNnTHk4Z0ltRnlZekUxT1RSZmFYTnpJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5UazBMbUZzWjI4dWRITTZPRGdLSUNBZ0lDOHZJSEpsZEhWeWJpQjBhR2x6TG1GeVl6RTFPVFJmYVhOemRXRmliR1V1ZG1Gc2RXVUtJQ0FnSUhKbGRITjFZZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZPa0Z5WXpFME1UQXVZWEpqTVRReE1GOWlZV3hoYm1ObFgyOW1YM0JoY25ScGRHbHZiaWhvYjJ4a1pYSTZJR0o1ZEdWekxDQndZWEowYVhScGIyNDZJR0o1ZEdWektTQXRQaUJpZVhSbGN6b0tZWEpqTVRReE1GOWlZV3hoYm1ObFgyOW1YM0JoY25ScGRHbHZiam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TmprdE56QUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNoN0lISmxZV1J2Ym14NU9pQjBjblZsSUgwcENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UUXhNRjlpWVd4aGJtTmxYMjltWDNCaGNuUnBkR2x2Ymlob2IyeGtaWEk2SUdGeVl6UXVRV1JrY21WemN5d2djR0Z5ZEdsMGFXOXVPaUJoY21NMExrRmtaSEpsYzNNcE9pQmhjbU0wTGxWcGJuUk9NalUySUhzS0lDQWdJSEJ5YjNSdklESWdNUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8zTVMwM05Bb2dJQ0FnTHk4Z1kyOXVjM1FnYTJWNUlEMGdibVYzSUdGeVl6RTBNVEJmVUdGeWRHbDBhVzl1UzJWNUtIc0tJQ0FnSUM4dklDQWdhRzlzWkdWeU9pQm9iMnhrWlhJc0NpQWdJQ0F2THlBZ0lIQmhjblJwZEdsdmJqb2djR0Z5ZEdsMGFXOXVMQW9nSUNBZ0x5OGdmU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1puSmhiV1ZmWkdsbklDMHhDaUFnSUNCamIyNWpZWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TlRjS0lDQWdJQzh2SUhCMVlteHBZeUJ3WVhKMGFYUnBiMjV6SUQwZ1FtOTRUV0Z3UEdGeVl6RTBNVEJmVUdGeWRHbDBhVzl1UzJWNUxDQmhjbU0wTGxWcGJuUk9NalUyUGloN0lHdGxlVkJ5WldacGVEb2dKMkZ5WXpFME1UQmZjQ2NnZlNrS0lDQWdJR0o1ZEdWaklEZ2dMeThnSW1GeVl6RTBNVEJmY0NJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzNOUW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11Y0dGeWRHbDBhVzl1Y3loclpYa3BMblpoYkhWbENpQWdJQ0JpYjNoZloyVjBDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzZRWEpqTVRReE1DNWhjbU15TURCZmRISmhibk5tWlhJb2RHODZJR0o1ZEdWekxDQjJZV3gxWlRvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwaGNtTXlNREJmZEhKaGJuTm1aWEk2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pjNExUYzVDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnTHk4Z2NIVmliR2xqSUc5MlpYSnlhV1JsSUdGeVl6SXdNRjkwY21GdWMyWmxjaWgwYnpvZ1lYSmpOQzVCWkdSeVpYTnpMQ0IyWVd4MVpUb2dZWEpqTkM1VmFXNTBUakkxTmlrNklHRnlZelF1UW05dmJDQjdDaUFnSUNCd2NtOTBieUF5SURFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk9ERUtJQ0FnSUM4dklHNWxkeUJoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa3NDaUFnSUNCMGVHNGdVMlZ1WkdWeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qZ3lDaUFnSUNBdkx5QnVaWGNnWVhKak5DNUJaR1J5WlhOektDa3NDaUFnSUNCaWVYUmxZMTh4SUM4dklHRmtaSElnUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVmsxU0VaTFVRb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzRNQzA0TndvZ0lDQWdMeThnZEdocGN5NWZkSEpoYm5ObVpYSmZjR0Z5ZEdsMGFXOXVLQW9nSUNBZ0x5OGdJQ0J1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwTEFvZ0lDQWdMeThnSUNCdVpYY2dZWEpqTkM1QlpHUnlaWE56S0Nrc0NpQWdJQ0F2THlBZ0lIUnZMQW9nSUNBZ0x5OGdJQ0J1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLQ2tzQ2lBZ0lDQXZMeUFnSUhaaGJIVmxMQW9nSUNBZ0x5OGdJQ0J1WlhjZ1lYSmpOQzVFZVc1aGJXbGpRbmwwWlhNb0tTd0tJQ0FnSUM4dklDa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzROQW9nSUNBZ0x5OGdibVYzSUdGeVl6UXVRV1JrY21WemN5Z3BMQW9nSUNBZ1lubDBaV05mTVNBdkx5QmhaR1J5SUVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZaTlVoR1MxRUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZPREF0T0RjS0lDQWdJQzh2SUhSb2FYTXVYM1J5WVc1elptVnlYM0JoY25ScGRHbHZiaWdLSUNBZ0lDOHZJQ0FnYm1WM0lHRnlZelF1UVdSa2NtVnpjeWhVZUc0dWMyVnVaR1Z5S1N3S0lDQWdJQzh2SUNBZ2JtVjNJR0Z5WXpRdVFXUmtjbVZ6Y3lncExBb2dJQ0FnTHk4Z0lDQjBieXdLSUNBZ0lDOHZJQ0FnYm1WM0lHRnlZelF1UVdSa2NtVnpjeWdwTEFvZ0lDQWdMeThnSUNCMllXeDFaU3dLSUNBZ0lDOHZJQ0FnYm1WM0lHRnlZelF1UkhsdVlXMXBZMEo1ZEdWektDa3NDaUFnSUNBdkx5QXBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZPRFlLSUNBZ0lDOHZJRzVsZHlCaGNtTTBMa1I1Ym1GdGFXTkNlWFJsY3lncExBb2dJQ0FnWW5sMFpXTWdNVFlnTHk4Z01IZ3dNREF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pnd0xUZzNDaUFnSUNBdkx5QjBhR2x6TGw5MGNtRnVjMlpsY2w5d1lYSjBhWFJwYjI0b0NpQWdJQ0F2THlBZ0lHNWxkeUJoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa3NDaUFnSUNBdkx5QWdJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9LU3dLSUNBZ0lDOHZJQ0FnZEc4c0NpQWdJQ0F2THlBZ0lHNWxkeUJoY21NMExrRmtaSEpsYzNNb0tTd0tJQ0FnSUM4dklDQWdkbUZzZFdVc0NpQWdJQ0F2THlBZ0lHNWxkeUJoY21NMExrUjVibUZ0YVdOQ2VYUmxjeWdwTEFvZ0lDQWdMeThnS1FvZ0lDQWdZMkZzYkhOMVlpQmZkSEpoYm5ObVpYSmZjR0Z5ZEdsMGFXOXVDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPamc0Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdkR2hwY3k1ZmRISmhibk5tWlhJb2JtVjNJR0Z5WXpRdVFXUmtjbVZ6Y3loVWVHNHVjMlZ1WkdWeUtTd2dkRzhzSUhaaGJIVmxLUW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR05oYkd4emRXSWdYM1J5WVc1elptVnlDaUFnSUNCeVpYUnpkV0lLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPanBCY21NeE5ERXdMbUZ5WXpFME1UQmZkSEpoYm5ObVpYSmZZbmxmY0dGeWRHbDBhVzl1S0hCaGNuUnBkR2x2YmpvZ1lubDBaWE1zSUhSdk9pQmllWFJsY3l3Z1lXMXZkVzUwT2lCaWVYUmxjeXdnWkdGMFlUb2dZbmwwWlhNcElDMCtJR0o1ZEdWek9ncGhjbU14TkRFd1gzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZiam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02T1RNdE9Ua0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UUXhNRjkwY21GdWMyWmxjbDlpZVY5d1lYSjBhWFJwYjI0b0NpQWdJQ0F2THlBZ0lIQmhjblJwZEdsdmJqb2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCMGJ6b2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCaGJXOTFiblE2SUdGeVl6UXVWV2x1ZEU0eU5UWXNDaUFnSUNBdkx5QWdJR1JoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWekxBb2dJQ0FnTHk4Z0tUb2dZWEpqTkM1QlpHUnlaWE56SUhzS0lDQWdJSEJ5YjNSdklEUWdNUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TURBS0lDQWdJQzh2SUdOdmJuTjBJSE5sYm1SbGNpQTlJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9WSGh1TG5ObGJtUmxjaWtLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVEF5Q2lBZ0lDQXZMeUJzWlhRZ2NtVmpaV2wyWlhKUVlYSjBhWFJwYjI0Z1BTQjBhR2x6TGw5eVpXTmxhWFpsY2xCaGNuUnBkR2x2YmloMGJ5d2djR0Z5ZEdsMGFXOXVLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUUUtJQ0FnSUdOaGJHeHpkV0lnWDNKbFkyVnBkbVZ5VUdGeWRHbDBhVzl1Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFd013b2dJQ0FnTHk4Z2RHaHBjeTVmZEhKaGJuTm1aWEpmY0dGeWRHbDBhVzl1S0hObGJtUmxjaXdnY0dGeWRHbDBhVzl1TENCMGJ5d2djbVZqWldsMlpYSlFZWEowYVhScGIyNHNJR0Z0YjNWdWRDd2daR0YwWVNrS0lDQWdJSE4zWVhBS0lDQWdJR1p5WVcxbFgyUnBaeUF0TkFvZ0lDQWdabkpoYldWZlpHbG5JQzB6Q2lBZ0lDQmthV2NnTXdvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHTmhiR3h6ZFdJZ1gzUnlZVzV6Wm1WeVgzQmhjblJwZEdsdmJnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hNRFFLSUNBZ0lDOHZJSEpsZEhWeWJpQnlaV05sYVhabGNsQmhjblJwZEdsdmJnb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzZRWEpqTVRReE1DNWhjbU14TkRFd1gzQmhjblJwZEdsdmJuTmZiMllvYUc5c1pHVnlPaUJpZVhSbGN5d2djR0ZuWlRvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwaGNtTXhOREV3WDNCaGNuUnBkR2x2Ym5OZmIyWTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakV3TnkweE1EZ0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0F2THlCd2RXSnNhV01nWVhKak1UUXhNRjl3WVhKMGFYUnBiMjV6WDI5bUtHaHZiR1JsY2pvZ1lYSmpOQzVCWkdSeVpYTnpMQ0J3WVdkbE9pQmhjbU0wTGxWcGJuUk9OalFwT2lCaGNtTTBMa0ZrWkhKbGMzTmJYU0I3Q2lBZ0lDQndjbTkwYnlBeUlERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVEE1Q2lBZ0lDQXZMeUJqYjI1emRDQnJaWGtnUFNCdVpYY2dZWEpqTVRReE1GOUliMnhrYVc1blVHRnlkR2wwYVc5dWMxQmhaMmx1WVhSbFpFdGxlU2g3SUdodmJHUmxjam9nYUc5c1pHVnlMQ0J3WVdkbE9pQndZV2RsSUgwcENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qWXdDaUFnSUNBdkx5QnJaWGxRY21WbWFYZzZJQ2RoY21NeE5ERXdYMmh3WDJFbkxBb2dJQ0FnWW5sMFpXTWdNVGdnTHk4Z0ltRnlZekUwTVRCZmFIQmZZU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFeE1Bb2dJQ0FnTHk4Z2FXWWdLQ0YwYUdsekxtaHZiR1JsY2xCaGNuUnBkR2x2Ym5OQlpHUnlaWE56WlhNb2EyVjVLUzVsZUdsemRITXBJSEpsZEhWeWJpQmJYUW9nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JpYm5vZ1lYSmpNVFF4TUY5d1lYSjBhWFJwYjI1elgyOW1YMkZtZEdWeVgybG1YMlZzYzJWQU1nb2dJQ0FnWW5sMFpXTWdNVFlnTHk4Z01IZ3dNREF3Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQnlaWFJ6ZFdJS0NtRnlZekUwTVRCZmNHRnlkR2wwYVc5dWMxOXZabDloWm5SbGNsOXBabDlsYkhObFFESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakV4TVFvZ0lDQWdMeThnY21WMGRYSnVJSFJvYVhNdWFHOXNaR1Z5VUdGeWRHbDBhVzl1YzBGa1pISmxjM05sY3loclpYa3BMblpoYkhWbENpQWdJQ0JtY21GdFpWOWthV2NnTUFvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnWVhOelpYSjBJQzh2SUVKdmVDQnRkWE4wSUdoaGRtVWdkbUZzZFdVS0lDQWdJSE4zWVhBS0lDQWdJSEpsZEhOMVlnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk9rRnlZekUwTVRBdVlYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNpaG9iMnhrWlhJNklHSjVkR1Z6TENCdmNHVnlZWFJ2Y2pvZ1lubDBaWE1zSUhCaGNuUnBkR2x2YmpvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwaGNtTXhOREV3WDJselgyOXdaWEpoZEc5eU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hNVFF0TVRFMUNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvZXlCeVpXRmtiMjVzZVRvZ2RISjFaU0I5S1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpFME1UQmZhWE5mYjNCbGNtRjBiM0lvYUc5c1pHVnlPaUJoY21NMExrRmtaSEpsYzNNc0lHOXdaWEpoZEc5eU9pQmhjbU0wTGtGa1pISmxjM01zSUhCaGNuUnBkR2x2YmpvZ1lYSmpOQzVCWkdSeVpYTnpLVG9nWVhKak5DNUNiMjlzSUhzS0lDQWdJSEJ5YjNSdklETWdNUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdSMWNHNGdNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TVRZS0lDQWdJQzh2SUdsbUlDaHZjR1Z5WVhSdmNpQTlQVDBnYUc5c1pHVnlLU0J5WlhSMWNtNGdibVYzSUdGeVl6UXVRbTl2YkNoMGNuVmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUTUtJQ0FnSUQwOUNpQWdJQ0JpZWlCaGNtTXhOREV3WDJselgyOXdaWEpoZEc5eVgyRm1kR1Z5WDJsbVgyVnNjMlZBTWdvZ0lDQWdZbmwwWldNZ055QXZMeUF3ZURnd0NpQWdJQ0JtY21GdFpWOWlkWEo1SURBS0lDQWdJSEpsZEhOMVlnb0tZWEpqTVRReE1GOXBjMTl2Y0dWeVlYUnZjbDloWm5SbGNsOXBabDlsYkhObFFESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakV4TndvZ0lDQWdMeThnWTI5dWMzUWdjM0JsWTJsbWFXTWdQU0J1WlhjZ1lYSmpNVFF4TUY5UGNHVnlZWFJ2Y2t0bGVTaDdJR2h2YkdSbGNqb2dhRzlzWkdWeUxDQnZjR1Z5WVhSdmNqb2diM0JsY21GMGIzSXNJSEJoY25ScGRHbHZiam9nY0dGeWRHbDBhVzl1SUgwcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JrZFhBS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5qSUtJQ0FnSUM4dklIQjFZbXhwWXlCdmNHVnlZWFJ2Y25NZ1BTQkNiM2hOWVhBOFlYSmpNVFF4TUY5UGNHVnlZWFJ2Y2t0bGVTd2dZWEpqTkM1Q2VYUmxQaWg3SUd0bGVWQnlaV1pwZURvZ0oyRnlZekUwTVRCZmIzQW5JSDBwSUM4dklIWmhiSFZsSUQwZ01TQmhkWFJvYjNKcGVtVmtDaUFnSUNCaWVYUmxZeUF4T1NBdkx5QWlZWEpqTVRReE1GOXZjQ0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQm1jbUZ0WlY5aWRYSjVJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TVRFNENpQWdJQ0F2THlCcFppQW9kR2hwY3k1dmNHVnlZWFJ2Y25Nb2MzQmxZMmxtYVdNcExtVjRhWE4wY3lBbUppQjBhR2x6TG05d1pYSmhkRzl5Y3loemNHVmphV1pwWXlrdWRtRnNkV1V1Ym1GMGFYWmxJRDA5UFNBeEtTQjdDaUFnSUNCaWIzaGZiR1Z1Q2lBZ0lDQmlkWEo1SURFS0lDQWdJR0o2SUdGeVl6RTBNVEJmYVhOZmIzQmxjbUYwYjNKZllXWjBaWEpmYVdaZlpXeHpaVUExQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dNUW9nSUNBZ1ltOTRYMmRsZEFvZ0lDQWdZWE56WlhKMElDOHZJRUp2ZUNCdGRYTjBJR2hoZG1VZ2RtRnNkV1VLSUNBZ0lHSjBiMmtLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWW5vZ1lYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNsOWhablJsY2w5cFpsOWxiSE5sUURVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1URTVDaUFnSUNBdkx5QnlaWFIxY200Z2JtVjNJR0Z5WXpRdVFtOXZiQ2gwY25WbEtRb2dJQ0FnWW5sMFpXTWdOeUF2THlBd2VEZ3dDaUFnSUNCbWNtRnRaVjlpZFhKNUlEQUtJQ0FnSUhKbGRITjFZZ29LWVhKak1UUXhNRjlwYzE5dmNHVnlZWFJ2Y2w5aFpuUmxjbDlwWmw5bGJITmxRRFU2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFeU1Rb2dJQ0FnTHk4Z1kyOXVjM1FnWjJ4dlltRnNTMlY1SUQwZ2JtVjNJR0Z5WXpFME1UQmZUM0JsY21GMGIzSkxaWGtvZXlCb2IyeGtaWEk2SUdodmJHUmxjaXdnYjNCbGNtRjBiM0k2SUc5d1pYSmhkRzl5TENCd1lYSjBhWFJwYjI0NklHNWxkeUJoY21NMExrRmtaSEpsYzNNb0tTQjlLUW9nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUdKNWRHVmpYekVnTHk4Z1lXUmtjaUJCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJXVFZJUmt0UkNpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZOaklLSUNBZ0lDOHZJSEIxWW14cFl5QnZjR1Z5WVhSdmNuTWdQU0JDYjNoTllYQThZWEpqTVRReE1GOVBjR1Z5WVhSdmNrdGxlU3dnWVhKak5DNUNlWFJsUGloN0lHdGxlVkJ5WldacGVEb2dKMkZ5WXpFME1UQmZiM0FuSUgwcElDOHZJSFpoYkhWbElEMGdNU0JoZFhSb2IzSnBlbVZrQ2lBZ0lDQmllWFJsWXlBeE9TQXZMeUFpWVhKak1UUXhNRjl2Y0NJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnWkhWd0NpQWdJQ0JtY21GdFpWOWlkWEo1SURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1USXlDaUFnSUNBdkx5QnBaaUFvZEdocGN5NXZjR1Z5WVhSdmNuTW9aMnh2WW1Gc1MyVjVLUzVsZUdsemRITWdKaVlnZEdocGN5NXZjR1Z5WVhSdmNuTW9aMnh2WW1Gc1MyVjVLUzUyWVd4MVpTNXVZWFJwZG1VZ1BUMDlJREVwSUhzS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1lub2dZWEpqTVRReE1GOXBjMTl2Y0dWeVlYUnZjbDloWm5SbGNsOXBabDlsYkhObFFEZ0tJQ0FnSUdaeVlXMWxYMlJwWnlBeUNpQWdJQ0JpYjNoZloyVjBDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnWW5SdmFRb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmllaUJoY21NeE5ERXdYMmx6WDI5d1pYSmhkRzl5WDJGbWRHVnlYMmxtWDJWc2MyVkFPQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TWpNS0lDQWdJQzh2SUhKbGRIVnliaUJ1WlhjZ1lYSmpOQzVDYjI5c0tIUnlkV1VwQ2lBZ0lDQmllWFJsWXlBM0lDOHZJREI0T0RBS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdjbVYwYzNWaUNncGhjbU14TkRFd1gybHpYMjl3WlhKaGRHOXlYMkZtZEdWeVgybG1YMlZzYzJWQU9Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVEkxQ2lBZ0lDQXZMeUJ5WlhSMWNtNGdibVYzSUdGeVl6UXVRbTl2YkNobVlXeHpaU2tLSUNBZ0lHSjVkR1ZqSURFeUlDOHZJREI0TURBS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvNlFYSmpNVFF4TUM1aGNtTXhOREV3WDJGMWRHaHZjbWw2WlY5dmNHVnlZWFJ2Y2lob2IyeGtaWEk2SUdKNWRHVnpMQ0J2Y0dWeVlYUnZjam9nWW5sMFpYTXNJSEJoY25ScGRHbHZiam9nWW5sMFpYTXBJQzArSUhadmFXUTZDbUZ5WXpFME1UQmZZWFYwYUc5eWFYcGxYMjl3WlhKaGRHOXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TWpndE1USTVDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6RTBNVEJmWVhWMGFHOXlhWHBsWDI5d1pYSmhkRzl5S0dodmJHUmxjam9nWVhKak5DNUJaR1J5WlhOekxDQnZjR1Z5WVhSdmNqb2dZWEpqTkM1QlpHUnlaWE56TENCd1lYSjBhWFJwYjI0NklHRnlZelF1UVdSa2NtVnpjeWs2SUhadmFXUWdld29nSUNBZ2NISnZkRzhnTXlBd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qRXpNQW9nSUNBZ0x5OGdZWE56WlhKMEtHNWxkeUJoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa2dQVDA5SUdodmJHUmxjaXdnSjA5dWJIa2dhRzlzWkdWeUlHTmhiaUJoZFhSb2IzSnBlbVVuS1FvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dWJIa2dhRzlzWkdWeUlHTmhiaUJoZFhSb2IzSnBlbVVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TVRNeENpQWdJQ0F2THlCamIyNXpkQ0JyWlhrZ1BTQnVaWGNnWVhKak1UUXhNRjlQY0dWeVlYUnZja3RsZVNoN0lHaHZiR1JsY2pvZ2FHOXNaR1Z5TENCdmNHVnlZWFJ2Y2pvZ2IzQmxjbUYwYjNJc0lIQmhjblJwZEdsdmJqb2djR0Z5ZEdsMGFXOXVJSDBwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVE1LSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvMk1nb2dJQ0FnTHk4Z2NIVmliR2xqSUc5d1pYSmhkRzl5Y3lBOUlFSnZlRTFoY0R4aGNtTXhOREV3WDA5d1pYSmhkRzl5UzJWNUxDQmhjbU0wTGtKNWRHVStLSHNnYTJWNVVISmxabWw0T2lBbllYSmpNVFF4TUY5dmNDY2dmU2tnTHk4Z2RtRnNkV1VnUFNBeElHRjFkR2h2Y21sNlpXUUtJQ0FnSUdKNWRHVmpJREU1SUM4dklDSmhjbU14TkRFd1gyOXdJZ29nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFek1nb2dJQ0FnTHk4Z2RHaHBjeTV2Y0dWeVlYUnZjbk1vYTJWNUtTNTJZV3gxWlNBOUlHNWxkeUJoY21NMExrSjVkR1VvTVNrS0lDQWdJR0o1ZEdWaklESTFJQzh2SURCNE1ERUtJQ0FnSUdKdmVGOXdkWFFLSUNBZ0lISmxkSE4xWWdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02T2tGeVl6RTBNVEF1WVhKak1UUXhNRjl5WlhadmEyVmZiM0JsY21GMGIzSW9hRzlzWkdWeU9pQmllWFJsY3l3Z2IzQmxjbUYwYjNJNklHSjVkR1Z6TENCd1lYSjBhWFJwYjI0NklHSjVkR1Z6S1NBdFBpQjJiMmxrT2dwaGNtTXhOREV3WDNKbGRtOXJaVjl2Y0dWeVlYUnZjam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TVRNMUxURXpOZ29nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXhOREV3WDNKbGRtOXJaVjl2Y0dWeVlYUnZjaWhvYjJ4a1pYSTZJR0Z5WXpRdVFXUmtjbVZ6Y3l3Z2IzQmxjbUYwYjNJNklHRnlZelF1UVdSa2NtVnpjeXdnY0dGeWRHbDBhVzl1T2lCaGNtTTBMa0ZrWkhKbGMzTXBPaUIyYjJsa0lIc0tJQ0FnSUhCeWIzUnZJRE1nTUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveE16Y0tJQ0FnSUM4dklHRnpjMlZ5ZENodVpYY2dZWEpqTkM1QlpHUnlaWE56S0ZSNGJpNXpaVzVrWlhJcElEMDlQU0JvYjJ4a1pYSXNJQ2RQYm14NUlHaHZiR1JsY2lCallXNGdjbVYyYjJ0bEp5a0tJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBibXg1SUdodmJHUmxjaUJqWVc0Z2NtVjJiMnRsQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFek9Bb2dJQ0FnTHk4Z1kyOXVjM1FnYTJWNUlEMGdibVYzSUdGeVl6RTBNVEJmVDNCbGNtRjBiM0pMWlhrb2V5Qm9iMnhrWlhJNklHaHZiR1JsY2l3Z2IzQmxjbUYwYjNJNklHOXdaWEpoZEc5eUxDQndZWEowYVhScGIyNDZJSEJoY25ScGRHbHZiaUI5S1FvZ0lDQWdabkpoYldWZlpHbG5JQzB6Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHTnZibU5oZEFvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5qSUtJQ0FnSUM4dklIQjFZbXhwWXlCdmNHVnlZWFJ2Y25NZ1BTQkNiM2hOWVhBOFlYSmpNVFF4TUY5UGNHVnlZWFJ2Y2t0bGVTd2dZWEpqTkM1Q2VYUmxQaWg3SUd0bGVWQnlaV1pwZURvZ0oyRnlZekUwTVRCZmIzQW5JSDBwSUM4dklIWmhiSFZsSUQwZ01TQmhkWFJvYjNKcGVtVmtDaUFnSUNCaWVYUmxZeUF4T1NBdkx5QWlZWEpqTVRReE1GOXZjQ0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFek9Rb2dJQ0FnTHk4Z2FXWWdLSFJvYVhNdWIzQmxjbUYwYjNKektHdGxlU2t1WlhocGMzUnpLU0I3Q2lBZ0lDQmliM2hmYkdWdUNpQWdJQ0JpZFhKNUlERUtJQ0FnSUdKNklHRnlZekUwTVRCZmNtVjJiMnRsWDI5d1pYSmhkRzl5WDJGbWRHVnlYMmxtWDJWc2MyVkFNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TkRBS0lDQWdJQzh2SUhSb2FYTXViM0JsY21GMGIzSnpLR3RsZVNrdVpHVnNaWFJsS0NrS0lDQWdJR1p5WVcxbFgyUnBaeUF3Q2lBZ0lDQmliM2hmWkdWc0NpQWdJQ0J3YjNBS0NtRnlZekUwTVRCZmNtVjJiMnRsWDI5d1pYSmhkRzl5WDJGbWRHVnlYMmxtWDJWc2MyVkFNam9LSUNBZ0lISmxkSE4xWWdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02T2tGeVl6RTBNVEF1WVhKak1UUXhNRjl2Y0dWeVlYUnZjbDkwY21GdWMyWmxjbDlpZVY5d1lYSjBhWFJwYjI0b1puSnZiVG9nWW5sMFpYTXNJSEJoY25ScGRHbHZiam9nWW5sMFpYTXNJSFJ2T2lCaWVYUmxjeXdnWVcxdmRXNTBPaUJpZVhSbGN5d2daR0YwWVRvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwaGNtTXhOREV3WDI5d1pYSmhkRzl5WDNSeVlXNXpabVZ5WDJKNVgzQmhjblJwZEdsdmJqb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVFEwTFRFMU1Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5ERXdYMjl3WlhKaGRHOXlYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2YmlnS0lDQWdJQzh2SUNBZ1puSnZiVG9nWVhKak5DNUJaR1J5WlhOekxBb2dJQ0FnTHk4Z0lDQndZWEowYVhScGIyNDZJR0Z5WXpRdVFXUmtjbVZ6Y3l3S0lDQWdJQzh2SUNBZ2RHODZJR0Z5WXpRdVFXUmtjbVZ6Y3l3S0lDQWdJQzh2SUNBZ1lXMXZkVzUwT2lCaGNtTTBMbFZwYm5ST01qVTJMQW9nSUNBZ0x5OGdJQ0JrWVhSaE9pQmhjbU0wTGtSNWJtRnRhV05DZVhSbGN5d0tJQ0FnSUM4dklDazZJR0Z5WXpRdVFXUmtjbVZ6Y3lCN0NpQWdJQ0J3Y205MGJ5QTFJREVLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakUxTWdvZ0lDQWdMeThnWTI5dWMzUWdjMlZ1WkdWeUlEMGdibVYzSUdGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLUW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hOVFFLSUNBZ0lDOHZJR3hsZENCaGRYUm9iM0pwZW1Wa0lEMGdkR2hwY3k1aGNtTXhOREV3WDJselgyOXdaWEpoZEc5eUtHWnliMjBzSUhObGJtUmxjaXdnY0dGeWRHbDBhVzl1S1M1dVlYUnBkbVVnUFQwOUlIUnlkV1VLSUNBZ0lHWnlZVzFsWDJScFp5QXROUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem94TlRJS0lDQWdJQzh2SUdOdmJuTjBJSE5sYm1SbGNpQTlJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9WSGh1TG5ObGJtUmxjaWtLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVFUwQ2lBZ0lDQXZMeUJzWlhRZ1lYVjBhRzl5YVhwbFpDQTlJSFJvYVhNdVlYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNpaG1jbTl0TENCelpXNWtaWElzSUhCaGNuUnBkR2x2YmlrdWJtRjBhWFpsSUQwOVBTQjBjblZsQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVFFLSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNnb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR2RsZEdKcGRBb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmtkWEJ1SURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1UVTJDaUFnSUNBdkx5QnBaaUFvSVdGMWRHaHZjbWw2WldRcElIc0tJQ0FnSUdKdWVpQmhjbU14TkRFd1gyOXdaWEpoZEc5eVgzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFEUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVFU0Q2lBZ0lDQXZMeUJqYjI1emRDQndTMlY1SUQwZ2JtVjNJR0Z5WXpFME1UQmZUM0JsY21GMGIzSlFiM0owYVc5dVMyVjVLSHNnYUc5c1pHVnlPaUJtY205dExDQnZjR1Z5WVhSdmNqb2djMlZ1WkdWeUxDQndZWEowYVhScGIyNGdmU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXROUW9nSUNBZ1puSmhiV1ZmWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1puSmhiV1ZmWkdsbklDMDBDaUFnSUNCamIyNWpZWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TmpNS0lDQWdJQzh2SUhCMVlteHBZeUJ2Y0dWeVlYUnZjbEJ2Y25ScGIyNUJiR3h2ZDJGdVkyVnpJRDBnUW05NFRXRndQR0Z5WXpFME1UQmZUM0JsY21GMGIzSlFiM0owYVc5dVMyVjVMQ0JoY21NMExsVnBiblJPTWpVMlBpaDdJR3RsZVZCeVpXWnBlRG9nSjJGeVl6RTBNVEJmYjNCaEp5QjlLUW9nSUNBZ1lubDBaV01nTVRRZ0x5OGdJbUZ5WXpFME1UQmZiM0JoSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JrZFhBS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveE5Ua0tJQ0FnSUM4dklHbG1JQ2gwYUdsekxtOXdaWEpoZEc5eVVHOXlkR2x2YmtGc2JHOTNZVzVqWlhNb2NFdGxlU2t1WlhocGMzUnpLU0I3Q2lBZ0lDQmliM2hmYkdWdUNpQWdJQ0JpZFhKNUlERUtJQ0FnSUdKNklHRnlZekUwTVRCZmIzQmxjbUYwYjNKZmRISmhibk5tWlhKZllubGZjR0Z5ZEdsMGFXOXVYMkZtZEdWeVgybG1YMlZzYzJWQU13b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hOakFLSUNBZ0lDOHZJR052Ym5OMElISmxiV0ZwYm1sdVp5QTlJSFJvYVhNdWIzQmxjbUYwYjNKUWIzSjBhVzl1UVd4c2IzZGhibU5sY3lod1MyVjVLUzUyWVd4MVpRb2dJQ0FnWm5KaGJXVmZaR2xuSURBS0lDQWdJR1IxY0FvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnWVhOelpYSjBJQzh2SUVKdmVDQnRkWE4wSUdoaGRtVWdkbUZzZFdVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1UWXhDaUFnSUNBdkx5QmhjM05sY25Rb2NtVnRZV2x1YVc1bkxtNWhkR2wyWlNBK1BTQmhiVzkxYm5RdWJtRjBhWFpsTENBblVHOXlkR2x2YmlCaGJHeHZkMkZ1WTJVZ1pYaGpaV1ZrWldRbktRb2dJQ0FnWkhWd0NpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR0krUFFvZ0lDQWdZWE56WlhKMElDOHZJRkJ2Y25ScGIyNGdZV3hzYjNkaGJtTmxJR1Y0WTJWbFpHVmtDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakUyTWdvZ0lDQWdMeThnWVhWMGFHOXlhWHBsWkNBOUlIUnlkV1VLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCbWNtRnRaVjlpZFhKNUlESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVFkxQ2lBZ0lDQXZMeUIwYUdsekxtOXdaWEpoZEc5eVVHOXlkR2x2YmtGc2JHOTNZVzVqWlhNb2NFdGxlU2t1ZG1Gc2RXVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqSTFOaWh5WlcxaGFXNXBibWN1Ym1GMGFYWmxJQzBnWVcxdmRXNTBMbTVoZEdsMlpTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWWkwS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFnTHk4Z2IzWmxjbVpzYjNjS0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdZbnBsY204S0lDQWdJR0o4Q2lBZ0lDQmliM2hmY0hWMENncGhjbU14TkRFd1gyOXdaWEpoZEc5eVgzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFETTZDaUFnSUNCbWNtRnRaVjlrYVdjZ01nb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBekNncGhjbU14TkRFd1gyOXdaWEpoZEc5eVgzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFEUTZDaUFnSUNCbWNtRnRaVjlrYVdjZ013b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hOamdLSUNBZ0lDOHZJR0Z6YzJWeWRDaGhkWFJvYjNKcGVtVmtMQ0FuVG05MElHRjFkR2h2Y21sNlpXUWdiM0JsY21GMGIzSW5LUW9nSUNBZ1lYTnpaWEowSUM4dklFNXZkQ0JoZFhSb2IzSnBlbVZrSUc5d1pYSmhkRzl5Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFMk9Rb2dJQ0FnTHk4Z2JHVjBJSEpsWTJWcGRtVnlVR0Z5ZEdsMGFXOXVJRDBnZEdocGN5NWZjbVZqWldsMlpYSlFZWEowYVhScGIyNG9kRzhzSUhCaGNuUnBkR2x2YmlrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdabkpoYldWZlpHbG5JQzAwQ2lBZ0lDQmpZV3hzYzNWaUlGOXlaV05sYVhabGNsQmhjblJwZEdsdmJnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hOekFLSUNBZ0lDOHZJSFJvYVhNdVgzUnlZVzV6Wm1WeVgzQmhjblJwZEdsdmJpaG1jbTl0TENCd1lYSjBhWFJwYjI0c0lIUnZMQ0J5WldObGFYWmxjbEJoY25ScGRHbHZiaXdnWVcxdmRXNTBMQ0JrWVhSaEtRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMUNpQWdJQ0JtY21GdFpWOWthV2NnTFRRS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdaR2xuSURNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpZV3hzYzNWaUlGOTBjbUZ1YzJabGNsOXdZWEowYVhScGIyNEtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVGN4Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdjbVZqWldsMlpYSlFZWEowYVhScGIyNEtJQ0FnSUdaeVlXMWxYMkoxY25rZ01Bb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzZRWEpqTVRReE1DNWhjbU14TkRFd1gyTmhibDkwY21GdWMyWmxjbDlpZVY5d1lYSjBhWFJwYjI0b1puSnZiVG9nWW5sMFpYTXNJSEJoY25ScGRHbHZiam9nWW5sMFpYTXNJSFJ2T2lCaWVYUmxjeXdnWVcxdmRXNTBPaUJpZVhSbGN5d2daR0YwWVRvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwaGNtTXhOREV3WDJOaGJsOTBjbUZ1YzJabGNsOWllVjl3WVhKMGFYUnBiMjQ2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pFM05DMHhPREVLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFF4TUY5allXNWZkSEpoYm5ObVpYSmZZbmxmY0dGeWRHbDBhVzl1S0FvZ0lDQWdMeThnSUNCbWNtOXRPaUJoY21NMExrRmtaSEpsYzNNc0NpQWdJQ0F2THlBZ0lIQmhjblJwZEdsdmJqb2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCMGJ6b2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCaGJXOTFiblE2SUdGeVl6UXVWV2x1ZEU0eU5UWXNDaUFnSUNBdkx5QWdJR1JoZEdFNklHRnlZelF1UkhsdVlXMXBZMEo1ZEdWekxBb2dJQ0FnTHk4Z0tUb2dZWEpqTVRReE1GOWpZVzVmZEhKaGJuTm1aWEpmWW5sZmNHRnlkR2wwYVc5dVgzSmxkSFZ5YmlCN0NpQWdJQ0J3Y205MGJ5QTFJREVLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCa2RYQUtJQ0FnSUhCMWMyaGllWFJsY3lBaUlnb2dJQ0FnWkhWd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTFNUW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11Y0dGeWRHbDBhVzl1Y3lodVpYY2dZWEpqTVRReE1GOVFZWEowYVhScGIyNUxaWGtvZXlCb2IyeGtaWEk2SUdodmJHUmxjaXdnY0dGeWRHbDBhVzl1T2lCd1lYSjBhWFJwYjI0Z2ZTa3BMbVY0YVhOMGN3b2dJQ0FnWm5KaGJXVmZaR2xuSUMwMUNpQWdJQ0JtY21GdFpWOWthV2NnTFRRS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzFOd29nSUNBZ0x5OGdjSFZpYkdsaklIQmhjblJwZEdsdmJuTWdQU0JDYjNoTllYQThZWEpqTVRReE1GOVFZWEowYVhScGIyNUxaWGtzSUdGeVl6UXVWV2x1ZEU0eU5UWStLSHNnYTJWNVVISmxabWw0T2lBbllYSmpNVFF4TUY5d0p5QjlLUW9nSUNBZ1lubDBaV01nT0NBdkx5QWlZWEpqTVRReE1GOXdJZ29nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmtkWEFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpVeENpQWdJQ0F2THlCeVpYUjFjbTRnZEdocGN5NXdZWEowYVhScGIyNXpLRzVsZHlCaGNtTXhOREV3WDFCaGNuUnBkR2x2Ymt0bGVTaDdJR2h2YkdSbGNqb2dhRzlzWkdWeUxDQndZWEowYVhScGIyNDZJSEJoY25ScGRHbHZiaUI5S1NrdVpYaHBjM1J6Q2lBZ0lDQmliM2hmYkdWdUNpQWdJQ0JpZFhKNUlERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNVGd5Q2lBZ0lDQXZMeUJwWmlBb0lYUm9hWE11WDNaaGJHbGtVR0Z5ZEdsMGFXOXVLR1p5YjIwc0lIQmhjblJwZEdsdmJpa3BJSHNLSUNBZ0lHSnVlaUJoY21NeE5ERXdYMk5oYmw5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qRTRNeTB4T0RjS0lDQWdJQzh2SUhKbGRIVnliaUJ1WlhjZ1lYSmpNVFF4TUY5allXNWZkSEpoYm5ObVpYSmZZbmxmY0dGeWRHbDBhVzl1WDNKbGRIVnliaWg3Q2lBZ0lDQXZMeUFnSUdOdlpHVTZJRzVsZHlCaGNtTTBMa0o1ZEdVb01IZzFNQ2tzQ2lBZ0lDQXZMeUFnSUhOMFlYUjFjem9nYm1WM0lHRnlZelF1VTNSeUtDZFFZWEowYVhScGIyNGdibTkwSUdWNGFYTjBjeWNwTEFvZ0lDQWdMeThnSUNCeVpXTmxhWFpsY2xCaGNuUnBkR2x2YmpvZ2JtVjNJR0Z5WXpRdVFXUmtjbVZ6Y3lncExBb2dJQ0FnTHk4Z2ZTa0tJQ0FnSUhCMWMyaGllWFJsY3lCaVlYTmxNeklvUzBGQlEwZEJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVdEdRVmxNVTA5U1ZWaEpNa3hRVGxsUlJ6UXpNMVZGUWxOWVVUSk1WRTlTV2xFcENpQWdJQ0JtY21GdFpWOWlkWEo1SURBS0lDQWdJSEpsZEhOMVlnb0tZWEpqTVRReE1GOWpZVzVmZEhKaGJuTm1aWEpmWW5sZmNHRnlkR2wwYVc5dVgyRm1kR1Z5WDJsbVgyVnNjMlZBTWpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1Ua3dDaUFnSUNBdkx5QjBhR2x6TG5CaGNuUnBkR2x2Ym5Nb2JtVjNJR0Z5WXpFME1UQmZVR0Z5ZEdsMGFXOXVTMlY1S0hzZ2FHOXNaR1Z5T2lCbWNtOXRMQ0J3WVhKMGFYUnBiMjQ2SUhCaGNuUnBkR2x2YmlCOUtTa3VkbUZzZFdVdWJtRjBhWFpsSUR3Z1lXMXZkVzUwTG01aGRHbDJaUW9nSUNBZ1puSmhiV1ZmWkdsbklEUUtJQ0FnSUdKdmVGOW5aWFFLSUNBZ0lHRnpjMlZ5ZENBdkx5QkNiM2dnYlhWemRDQm9ZWFpsSUhaaGJIVmxDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdJOENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qRTRPUzB4T1RFS0lDQWdJQzh2SUdsbUlDZ0tJQ0FnSUM4dklDQWdkR2hwY3k1d1lYSjBhWFJwYjI1ektHNWxkeUJoY21NeE5ERXdYMUJoY25ScGRHbHZia3RsZVNoN0lHaHZiR1JsY2pvZ1puSnZiU3dnY0dGeWRHbDBhVzl1T2lCd1lYSjBhWFJwYjI0Z2ZTa3BMblpoYkhWbExtNWhkR2wyWlNBOElHRnRiM1Z1ZEM1dVlYUnBkbVVLSUNBZ0lDOHZJQ2tnZXdvZ0lDQWdZbm9nWVhKak1UUXhNRjlqWVc1ZmRISmhibk5tWlhKZllubGZjR0Z5ZEdsMGFXOXVYMkZtZEdWeVgybG1YMlZzYzJWQU5Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3hPVEl0TVRrMkNpQWdJQ0F2THlCeVpYUjFjbTRnYm1WM0lHRnlZekUwTVRCZlkyRnVYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2Ymw5eVpYUjFjbTRvZXdvZ0lDQWdMeThnSUNCamIyUmxPaUJ1WlhjZ1lYSmpOQzVDZVhSbEtEQjROVElwTEFvZ0lDQWdMeThnSUNCemRHRjBkWE02SUc1bGR5QmhjbU0wTGxOMGNpZ25TVzV6ZFdabWFXTnBaVzUwSUdKaGJHRnVZMlVuS1N3S0lDQWdJQzh2SUNBZ2NtVmpaV2wyWlhKUVlYSjBhWFJwYjI0NklHNWxkeUJoY21NMExrRmtaSEpsYzNNb0tTd0tJQ0FnSUM4dklIMHBDaUFnSUNCd2RYTm9ZbmwwWlhNZ1ltRnpaVE15S0V0SlFVTkhRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkxSVk16VkZSUFZsUkhUVEpNUkU1R1UxYzBOVUpCVFVwUlYxbFpURTlOVGxOUktRb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBd0NpQWdJQ0J5WlhSemRXSUtDbUZ5WXpFME1UQmZZMkZ1WDNSeVlXNXpabVZ5WDJKNVgzQmhjblJwZEdsdmJsOWhablJsY2w5cFpsOWxiSE5sUURRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qRTVPUW9nSUNBZ0x5OGdhV1lnS0hSdklEMDlQU0J1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLQ2twSUhzS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdZbmwwWldOZk1TQXZMeUJoWkdSeUlFRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGWk5VaEdTMUVLSUNBZ0lEMDlDaUFnSUNCaWVpQmhjbU14TkRFd1gyTmhibDkwY21GdWMyWmxjbDlpZVY5d1lYSjBhWFJwYjI1ZllXWjBaWEpmYVdaZlpXeHpaVUEyQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJd01DMHlNRFFLSUNBZ0lDOHZJSEpsZEhWeWJpQnVaWGNnWVhKak1UUXhNRjlqWVc1ZmRISmhibk5tWlhKZllubGZjR0Z5ZEdsMGFXOXVYM0psZEhWeWJpaDdDaUFnSUNBdkx5QWdJR052WkdVNklHNWxkeUJoY21NMExrSjVkR1VvTUhnMU55a3NDaUFnSUNBdkx5QWdJSE4wWVhSMWN6b2dibVYzSUdGeVl6UXVVM1J5S0NkSmJuWmhiR2xrSUhKbFkyVnBkbVZ5Snlrc0NpQWdJQ0F2THlBZ0lISmxZMlZwZG1WeVVHRnlkR2wwYVc5dU9pQnVaWGNnWVhKak5DNUJaR1J5WlhOektDa3NDaUFnSUNBdkx5QjlLUW9nSUNBZ2NIVnphR0o1ZEdWeklHSmhjMlV6TWloTE5FRkRSMEZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJTVVZUTTFSWFRVWlhSMU5hUWtGUFNsTlhSMXBNU2s5YVUxaEZLUW9nSUNBZ1puSmhiV1ZmWW5WeWVTQXdDaUFnSUNCeVpYUnpkV0lLQ21GeVl6RTBNVEJmWTJGdVgzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFEWTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakl3T0FvZ0lDQWdMeThnWTI5dWMzUWdjMlZ1WkdWeVFXUmtjaUE5SUc1bGR5QmhjbU0wTGtGa1pISmxjM01vVkhodUxuTmxibVJsY2lrS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHUjFjQW9nSUNBZ1puSmhiV1ZmWW5WeWVTQXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakl3T1FvZ0lDQWdMeThnYVdZZ0tITmxibVJsY2tGa1pISWdJVDA5SUdaeWIyMHBJSHNLSUNBZ0lHWnlZVzFsWDJScFp5QXROUW9nSUNBZ0lUMEtJQ0FnSUdKNklHRnlZekUwTVRCZlkyRnVYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRREUyQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJeE1Bb2dJQ0FnTHk4Z2JHVjBJR0YxZEdodmNtbDZaV1FnUFNCMGFHbHpMbUZ5WXpFME1UQmZhWE5mYjNCbGNtRjBiM0lvWm5KdmJTd2djMlZ1WkdWeVFXUmtjaXdnY0dGeWRHbDBhVzl1S1M1dVlYUnBkbVVnUFQwOUlIUnlkV1VLSUNBZ0lHWnlZVzFsWDJScFp5QXROUW9nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE5Bb2dJQ0FnWTJGc2JITjFZaUJoY21NeE5ERXdYMmx6WDI5d1pYSmhkRzl5Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1oyVjBZbWwwQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ1BUMEtJQ0FnSUdSMWNBb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBeUNpQWdJQ0JrZFhBS0lDQWdJR1p5WVcxbFgySjFjbmtnTXdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveU1URUtJQ0FnSUM4dklHbG1JQ2doWVhWMGFHOXlhWHBsWkNrZ2V3b2dJQ0FnWW01NklHRnlZekUwTVRCZlkyRnVYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRREV6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJeE1nb2dJQ0FnTHk4Z1kyOXVjM1FnY0V0bGVTQTlJRzVsZHlCaGNtTXhOREV3WDA5d1pYSmhkRzl5VUc5eWRHbHZia3RsZVNoN0lHaHZiR1JsY2pvZ1puSnZiU3dnYjNCbGNtRjBiM0k2SUhObGJtUmxja0ZrWkhJc0lIQmhjblJwZEdsdmJpQjlLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMDFDaUFnSUNCbWNtRnRaVjlrYVdjZ01Bb2dJQ0FnWTI5dVkyRjBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUUUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8yTXdvZ0lDQWdMeThnY0hWaWJHbGpJRzl3WlhKaGRHOXlVRzl5ZEdsdmJrRnNiRzkzWVc1alpYTWdQU0JDYjNoTllYQThZWEpqTVRReE1GOVBjR1Z5WVhSdmNsQnZjblJwYjI1TFpYa3NJR0Z5WXpRdVZXbHVkRTR5TlRZK0tIc2dhMlY1VUhKbFptbDRPaUFuWVhKak1UUXhNRjl2Y0dFbklIMHBDaUFnSUNCaWVYUmxZeUF4TkNBdkx5QWlZWEpqTVRReE1GOXZjR0VpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1IxY0FvZ0lDQWdabkpoYldWZlluVnllU0F4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJeE13b2dJQ0FnTHk4Z2FXWWdLSFJvYVhNdWIzQmxjbUYwYjNKUWIzSjBhVzl1UVd4c2IzZGhibU5sY3lod1MyVjVLUzVsZUdsemRITXBJSHNLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWm5KaGJXVmZaR2xuSURJS0lDQWdJR1p5WVcxbFgySjFjbmtnTXdvZ0lDQWdZbm9nWVhKak1UUXhNRjlqWVc1ZmRISmhibk5tWlhKZllubGZjR0Z5ZEdsMGFXOXVYMkZtZEdWeVgybG1YMlZzYzJWQU1UTUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNakUwQ2lBZ0lDQXZMeUJqYjI1emRDQnlaVzFoYVc1cGJtY2dQU0IwYUdsekxtOXdaWEpoZEc5eVVHOXlkR2x2YmtGc2JHOTNZVzVqWlhNb2NFdGxlU2t1ZG1Gc2RXVUtJQ0FnSUdaeVlXMWxYMlJwWnlBeENpQWdJQ0JpYjNoZloyVjBDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lNVFVLSUNBZ0lDOHZJR2xtSUNoeVpXMWhhVzVwYm1jdWJtRjBhWFpsSUQ0OUlHRnRiM1Z1ZEM1dVlYUnBkbVVwSUhzS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZajQ5Q2lBZ0lDQmllaUJoY21NeE5ERXdYMk5oYmw5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBeE1Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lNVFlLSUNBZ0lDOHZJR0YxZEdodmNtbDZaV1FnUFNCMGNuVmxDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBeUNncGhjbU14TkRFd1gyTmhibDkwY21GdWMyWmxjbDlpZVY5d1lYSjBhWFJwYjI1ZllXWjBaWEpmYVdaZlpXeHpaVUF4TVRvS0lDQWdJR1p5WVcxbFgyUnBaeUF5Q2lBZ0lDQm1jbUZ0WlY5aWRYSjVJRE1LQ21GeVl6RTBNVEJmWTJGdVgzUnlZVzV6Wm1WeVgySjVYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFERXpPZ29nSUNBZ1puSmhiV1ZmWkdsbklETUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNakl3Q2lBZ0lDQXZMeUJwWmlBb0lXRjFkR2h2Y21sNlpXUXBJSHNLSUNBZ0lHSnVlaUJoY21NeE5ERXdYMk5oYmw5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBeE5nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lNakV0TWpJMUNpQWdJQ0F2THlCeVpYUjFjbTRnYm1WM0lHRnlZekUwTVRCZlkyRnVYM1J5WVc1elptVnlYMko1WDNCaGNuUnBkR2x2Ymw5eVpYUjFjbTRvZXdvZ0lDQWdMeThnSUNCamIyUmxPaUJ1WlhjZ1lYSmpOQzVDZVhSbEtEQjROVGdwTEFvZ0lDQWdMeThnSUNCemRHRjBkWE02SUc1bGR5QmhjbU0wTGxOMGNpZ25UM0JsY21GMGIzSWdibTkwSUdGMWRHaHZjbWw2WldRbktTd0tJQ0FnSUM4dklDQWdjbVZqWldsMlpYSlFZWEowYVhScGIyNDZJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9LU3dLSUNBZ0lDOHZJSDBwQ2lBZ0lDQndkWE5vWW5sMFpYTWdZbUZ6WlRNeUtFeEJRVU5IUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZNVlRZMFJFWlBTbEZZU1RNelUwVkNXRWMyTlVKQlRVWXlXRWt5UkZCUFNsVllWVnBNUlNrS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdjbVYwYzNWaUNncGhjbU14TkRFd1gyTmhibDkwY21GdWMyWmxjbDlpZVY5d1lYSjBhWFJwYjI1ZllXWjBaWEpmYVdaZlpXeHpaVUF4TmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1qSTVDaUFnSUNBdkx5QnNaWFFnY21WalpXbDJaWEpRWVhKMGFYUnBiMjRnUFNCMGFHbHpMbDl5WldObGFYWmxjbEJoY25ScGRHbHZiaWgwYnl3Z2NHRnlkR2wwYVc5dUtRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwekNpQWdJQ0JtY21GdFpWOWthV2NnTFRRS0lDQWdJR05oYkd4emRXSWdYM0psWTJWcGRtVnlVR0Z5ZEdsMGFXOXVDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakl6TVMweU16VUtJQ0FnSUM4dklISmxkSFZ5YmlCdVpYY2dZWEpqTVRReE1GOWpZVzVmZEhKaGJuTm1aWEpmWW5sZmNHRnlkR2wwYVc5dVgzSmxkSFZ5YmloN0NpQWdJQ0F2THlBZ0lHTnZaR1U2SUc1bGR5QmhjbU0wTGtKNWRHVW9NSGcxTVNrc0NpQWdJQ0F2THlBZ0lITjBZWFIxY3pvZ2JtVjNJR0Z5WXpRdVUzUnlLQ2RUZFdOalpYTnpKeWtzQ2lBZ0lDQXZMeUFnSUhKbFkyVnBkbVZ5VUdGeWRHbDBhVzl1T2lCeVpXTmxhWFpsY2xCaGNuUnBkR2x2Yml3S0lDQWdJQzh2SUgwcENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnMU1UQXdNak1LSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveU16TUtJQ0FnSUM4dklITjBZWFIxY3pvZ2JtVjNJR0Z5WXpRdVUzUnlLQ2RUZFdOalpYTnpKeWtzQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGd3TURBM05UTTNOVFl6TmpNMk5UY3pOek1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpNeExUSXpOUW9nSUNBZ0x5OGdjbVYwZFhKdUlHNWxkeUJoY21NeE5ERXdYMk5oYmw5MGNtRnVjMlpsY2w5aWVWOXdZWEowYVhScGIyNWZjbVYwZFhKdUtIc0tJQ0FnSUM4dklDQWdZMjlrWlRvZ2JtVjNJR0Z5WXpRdVFubDBaU2d3ZURVeEtTd0tJQ0FnSUM4dklDQWdjM1JoZEhWek9pQnVaWGNnWVhKak5DNVRkSElvSjFOMVkyTmxjM01uS1N3S0lDQWdJQzh2SUNBZ2NtVmpaV2wyWlhKUVlYSjBhWFJwYjI0NklISmxZMlZwZG1WeVVHRnlkR2wwYVc5dUxBb2dJQ0FnTHk4Z2ZTa0tJQ0FnSUdOdmJtTmhkQW9nSUNBZ1puSmhiV1ZmWW5WeWVTQXdDaUFnSUNCeVpYUnpkV0lLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPanBCY21NeE5ERXdMbDl5WldObGFYWmxjbEJoY25ScGRHbHZiaWh5WldObGFYWmxjam9nWW5sMFpYTXNJSEJoY25ScGRHbHZiam9nWW5sMFpYTXBJQzArSUdKNWRHVnpPZ3BmY21WalpXbDJaWEpRWVhKMGFYUnBiMjQ2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJME13b2dJQ0FnTHk4Z2NISnZkR1ZqZEdWa0lGOXlaV05sYVhabGNsQmhjblJwZEdsdmJpaHlaV05sYVhabGNqb2dZWEpqTkM1QlpHUnlaWE56TENCd1lYSjBhWFJwYjI0NklHRnlZelF1UVdSa2NtVnpjeWs2SUdGeVl6UXVRV1JrY21WemN5QjdDaUFnSUNCd2NtOTBieUF5SURFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1qUTBDaUFnSUNBdkx5QnNaWFFnY21WalpXbDJaWEpRWVhKMGFYUnBiMjRnUFNCdVpYY2dZWEpqTkM1QlpHUnlaWE56S0NrS0lDQWdJR0o1ZEdWalh6RWdMeThnWVdSa2NpQkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQldUVklSa3RSQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJME5Rb2dJQ0FnTHk4Z2FXWWdLSFJvYVhNdWNHRnlkR2wwYVc5dWN5aHVaWGNnWVhKak1UUXhNRjlRWVhKMGFYUnBiMjVMWlhrb2V5Qm9iMnhrWlhJNklISmxZMlZwZG1WeUxDQndZWEowYVhScGIyNDZJSEJoY25ScGRHbHZiaUI5S1NrdVpYaHBjM1J6S1NCN0NpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qVTNDaUFnSUNBdkx5QndkV0pzYVdNZ2NHRnlkR2wwYVc5dWN5QTlJRUp2ZUUxaGNEeGhjbU14TkRFd1gxQmhjblJwZEdsdmJrdGxlU3dnWVhKak5DNVZhVzUwVGpJMU5qNG9leUJyWlhsUWNtVm1hWGc2SUNkaGNtTXhOREV3WDNBbklIMHBDaUFnSUNCaWVYUmxZeUE0SUM4dklDSmhjbU14TkRFd1gzQWlDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpRMUNpQWdJQ0F2THlCcFppQW9kR2hwY3k1d1lYSjBhWFJwYjI1ektHNWxkeUJoY21NeE5ERXdYMUJoY25ScGRHbHZia3RsZVNoN0lHaHZiR1JsY2pvZ2NtVmpaV2wyWlhJc0lIQmhjblJwZEdsdmJqb2djR0Z5ZEdsMGFXOXVJSDBwS1M1bGVHbHpkSE1wSUhzS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1lub2dYM0psWTJWcGRtVnlVR0Z5ZEdsMGFXOXVYMkZtZEdWeVgybG1YMlZzYzJWQU1nb2dJQ0FnWm5KaGJXVmZaR2xuSUMweENpQWdJQ0JtY21GdFpWOWlkWEo1SURBS0NsOXlaV05sYVhabGNsQmhjblJwZEdsdmJsOWhablJsY2w5cFpsOWxiSE5sUURJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTBPQW9nSUNBZ0x5OGdjbVYwZFhKdUlISmxZMlZwZG1WeVVHRnlkR2wwYVc5dUNpQWdJQ0JtY21GdFpWOWthV2NnTUFvZ0lDQWdjM2RoY0FvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvNlFYSmpNVFF4TUM1ZllXUmtYM0JoY25ScFkybHdZWFJwYjI1ZmRHOWZhRzlzWkdWeUtHaHZiR1JsY2pvZ1lubDBaWE1zSUhCaGNuUnBZMmx3WVhScGIyNDZJR0o1ZEdWektTQXRQaUIyYjJsa09ncGZZV1JrWDNCaGNuUnBZMmx3WVhScGIyNWZkRzlmYUc5c1pHVnlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem95TmpRS0lDQWdJQzh2SUhCeWIzUmxZM1JsWkNCZllXUmtYM0JoY25ScFkybHdZWFJwYjI1ZmRHOWZhRzlzWkdWeUtHaHZiR1JsY2pvZ1lYSmpOQzVCWkdSeVpYTnpMQ0J3WVhKMGFXTnBjR0YwYVc5dU9pQmhjbU0wTGtGa1pISmxjM01wT2lCMmIybGtJSHNLSUNBZ0lIQnliM1J2SURJZ01Bb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR1IxY0c0Z05Bb2dJQ0FnY0hWemFHSjVkR1Z6SUNJaUNpQWdJQ0JrZFhCdUlEUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZOVGdLSUNBZ0lDOHZJSEIxWW14cFl5Qm9iMnhrWlhKUVlYSjBhWFJwYjI1elEzVnljbVZ1ZEZCaFoyVWdQU0JDYjNoTllYQThZWEpqTkM1QlpHUnlaWE56TENCaGNtTTBMbFZwYm5ST05qUStLSHNnYTJWNVVISmxabWw0T2lBbllYSmpNVFF4TUY5b2NGOXdKeUI5S1FvZ0lDQWdjSFZ6YUdKNWRHVnpJQ0poY21NeE5ERXdYMmh3WDNBaUNpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR052Ym1OaGRBb2dJQ0FnWkhWd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTJOZ29nSUNBZ0x5OGdhV1lnS0NGMGFHbHpMbWh2YkdSbGNsQmhjblJwZEdsdmJuTkRkWEp5Wlc1MFVHRm5aU2hvYjJ4a1pYSXBMbVY0YVhOMGN5a2dld29nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JpYm5vZ1gyRmtaRjl3WVhKMGFXTnBjR0YwYVc5dVgzUnZYMmh2YkdSbGNsOWhablJsY2w5cFpsOWxiSE5sUURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1qWTNDaUFnSUNBdkx5QjBhR2x6TG1odmJHUmxjbEJoY25ScGRHbHZibk5EZFhKeVpXNTBVR0ZuWlNob2IyeGtaWElwTG5aaGJIVmxJRDBnY0dGblpRb2dJQ0FnWm5KaGJXVmZaR2xuSURFd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTJOUW9nSUNBZ0x5OGdiR1YwSUhCaFoyVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqWTBLREFwQ2lBZ0lDQmllWFJsWXlBeU1pQXZMeUF3ZURBd01EQXdNREF3TURBd01EQXdNREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpZM0NpQWdJQ0F2THlCMGFHbHpMbWh2YkdSbGNsQmhjblJwZEdsdmJuTkRkWEp5Wlc1MFVHRm5aU2hvYjJ4a1pYSXBMblpoYkhWbElEMGdjR0ZuWlFvZ0lDQWdZbTk0WDNCMWRBb0tYMkZrWkY5d1lYSjBhV05wY0dGMGFXOXVYM1J2WDJodmJHUmxjbDloWm5SbGNsOXBabDlsYkhObFFESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakkyT1FvZ0lDQWdMeThnWTI5dWMzUWdiR0Z6ZEZCaFoyVWdQU0IwYUdsekxtaHZiR1JsY2xCaGNuUnBkR2x2Ym5ORGRYSnlaVzUwVUdGblpTaG9iMnhrWlhJcExuWmhiSFZsQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dNVEFLSUNBZ0lHSnZlRjluWlhRS0lDQWdJSE4zWVhBS0lDQWdJR1p5WVcxbFgySjFjbmtnTWdvZ0lDQWdZWE56WlhKMElDOHZJRUp2ZUNCdGRYTjBJR2hoZG1VZ2RtRnNkV1VLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpjd0NpQWdJQ0F2THlCc1pYUWdabTkxYm1RZ1BTQm1ZV3h6WlFvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHWnlZVzFsWDJKMWNua2dOZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem95TmpVS0lDQWdJQzh2SUd4bGRDQndZV2RsSUQwZ2JtVjNJR0Z5WXpRdVZXbHVkRTQyTkNnd0tRb2dJQ0FnWW5sMFpXTWdNaklnTHk4Z01IZ3dNREF3TURBd01EQXdNREF3TURBd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTNNUW9nSUNBZ0x5OGdabTl5SUNoc1pYUWdZM1Z5VUdGblpTQTlJSEJoWjJVN0lHTjFjbEJoWjJVdWJtRjBhWFpsSUR3Z2JHRnpkRkJoWjJVdWJtRjBhWFpsT3lCamRYSlFZV2RsSUQwZ2JtVjNJR0Z5WXpRdVZXbHVkRTQyTkNoamRYSlFZV2RsTG01aGRHbDJaU0FySURFcEtTQjdDaUFnSUNCbWNtRnRaVjlpZFhKNUlERUtDbDloWkdSZmNHRnlkR2xqYVhCaGRHbHZibDkwYjE5b2IyeGtaWEpmZDJocGJHVmZkRzl3UURNNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTNNUW9nSUNBZ0x5OGdabTl5SUNoc1pYUWdZM1Z5VUdGblpTQTlJSEJoWjJVN0lHTjFjbEJoWjJVdWJtRjBhWFpsSUR3Z2JHRnpkRkJoWjJVdWJtRjBhWFpsT3lCamRYSlFZV2RsSUQwZ2JtVjNJR0Z5WXpRdVZXbHVkRTQyTkNoamRYSlFZV2RsTG01aGRHbDJaU0FySURFcEtTQjdDaUFnSUNCbWNtRnRaVjlrYVdjZ01Rb2dJQ0FnWW5SdmFRb2dJQ0FnWkhWd0NpQWdJQ0JtY21GdFpWOWlkWEo1SURnS0lDQWdJR1p5WVcxbFgyUnBaeUF5Q2lBZ0lDQmlkRzlwQ2lBZ0lDQmtkWEFLSUNBZ0lHWnlZVzFsWDJKMWNua2dPUW9nSUNBZ1BBb2dJQ0FnWW5vZ1gyRmtaRjl3WVhKMGFXTnBjR0YwYVc5dVgzUnZYMmh2YkdSbGNsOWliRzlqYTBBeE1Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lOekl0TWpjMUNpQWdJQ0F2THlCamIyNXpkQ0J3WVdkcGJtRjBaV1JMWlhrZ1BTQnVaWGNnWVhKak1UUXhNRjlJYjJ4a2FXNW5VR0Z5ZEdsMGFXOXVjMUJoWjJsdVlYUmxaRXRsZVNoN0NpQWdJQ0F2THlBZ0lHaHZiR1JsY2pvZ2FHOXNaR1Z5TEFvZ0lDQWdMeThnSUNCd1lXZGxPaUJqZFhKUVlXZGxMQW9nSUNBZ0x5OGdmU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1puSmhiV1ZmWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8yTUFvZ0lDQWdMeThnYTJWNVVISmxabWw0T2lBbllYSmpNVFF4TUY5b2NGOWhKeXdLSUNBZ0lHSjVkR1ZqSURFNElDOHZJQ0poY21NeE5ERXdYMmh3WDJFaUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTNOZ29nSUNBZ0x5OGdhV1lnS0NGMGFHbHpMbWh2YkdSbGNsQmhjblJwZEdsdmJuTkJaR1J5WlhOelpYTW9jR0ZuYVc1aGRHVmtTMlY1S1M1bGVHbHpkSE1wSUhzS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1ltNTZJRjloWkdSZmNHRnlkR2xqYVhCaGRHbHZibDkwYjE5b2IyeGtaWEpmWVdaMFpYSmZhV1pmWld4elpVQTJDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakkzTndvZ0lDQWdMeThnZEdocGN5NW9iMnhrWlhKUVlYSjBhWFJwYjI1elFXUmtjbVZ6YzJWektIQmhaMmx1WVhSbFpFdGxlU2t1ZG1Gc2RXVWdQU0JiY0dGeWRHbGphWEJoZEdsdmJsMEtJQ0FnSUdKNWRHVmpJREkySUM4dklEQjRNREF3TVFvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1p5WVcxbFgyUnBaeUEwQ2lBZ0lDQmtkWEFLSUNBZ0lHSnZlRjlrWld3S0lDQWdJSEJ2Y0FvZ0lDQWdjM2RoY0FvZ0lDQWdZbTk0WDNCMWRBb0tYMkZrWkY5d1lYSjBhV05wY0dGMGFXOXVYM1J2WDJodmJHUmxjbDloWm5SbGNsOXBabDlsYkhObFFEWTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakk0TUFvZ0lDQWdMeThnYVdZZ0tIUm9hWE11WTI5dWRHRnBibk5CWkdSeVpYTnpLSFJvYVhNdWFHOXNaR1Z5VUdGeWRHbDBhVzl1YzBGa1pISmxjM05sY3lod1lXZHBibUYwWldSTFpYa3BMblpoYkhWbExDQndZWEowYVdOcGNHRjBhVzl1S1NrZ2V3b2dJQ0FnWm5KaGJXVmZaR2xuSURRS0lDQWdJR0p2ZUY5blpYUUtJQ0FnSUhOM1lYQUtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ1puSmhiV1ZmWW5WeWVTQXdDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lOVFFLSUNBZ0lDOHZJR1p2Y2lBb1kyOXVjM1FnZGlCdlppQmhLU0I3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlLSUNBZ0lHWnlZVzFsWDJKMWNua2dOUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdaeVlXMWxYMkoxY25rZ053b0tYMkZrWkY5d1lYSjBhV05wY0dGMGFXOXVYM1J2WDJodmJHUmxjbDltYjNKZmFHVmhaR1Z5UURFM09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lOVFFLSUNBZ0lDOHZJR1p2Y2lBb1kyOXVjM1FnZGlCdlppQmhLU0I3Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dOd29nSUNBZ1puSmhiV1ZmWkdsbklEVUtJQ0FnSUR3S0lDQWdJR0o2SUY5aFpHUmZjR0Z5ZEdsamFYQmhkR2x2Ymw5MGIxOW9iMnhrWlhKZllXWjBaWEpmWm05eVFESXhDaUFnSUNCbWNtRnRaVjlrYVdjZ01Bb2dJQ0FnWlhoMGNtRmpkQ0F5SURBS0lDQWdJR1p5WVcxbFgyUnBaeUEzQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lDb0tJQ0FnSUdsdWRHTmZNaUF2THlBek1nb2dJQ0FnWlhoMGNtRmpkRE1nTHk4Z2IyNGdaWEp5YjNJNklFbHVaR1Y0SUdGalkyVnpjeUJwY3lCdmRYUWdiMllnWW05MWJtUnpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakkxTlFvZ0lDQWdMeThnYVdZZ0tIWWdQVDA5SUhncElISmxkSFZ5YmlCMGNuVmxDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUQwOUNpQWdJQ0JpZWlCZllXUmtYM0JoY25ScFkybHdZWFJwYjI1ZmRHOWZhRzlzWkdWeVgyRm1kR1Z5WDJsbVgyVnNjMlZBTWpBS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2dwZllXUmtYM0JoY25ScFkybHdZWFJwYjI1ZmRHOWZhRzlzWkdWeVgyRm1kR1Z5WDJsdWJHbHVaV1JmYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvNlFYSmpNVFF4TUM1amIyNTBZV2x1YzBGa1pISmxjM05BTWpJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTRNQW9nSUNBZ0x5OGdhV1lnS0hSb2FYTXVZMjl1ZEdGcGJuTkJaR1J5WlhOektIUm9hWE11YUc5c1pHVnlVR0Z5ZEdsMGFXOXVjMEZrWkhKbGMzTmxjeWh3WVdkcGJtRjBaV1JMWlhrcExuWmhiSFZsTENCd1lYSjBhV05wY0dGMGFXOXVLU2tnZXdvZ0lDQWdZbm9nWDJGa1pGOXdZWEowYVdOcGNHRjBhVzl1WDNSdlgyaHZiR1JsY2w5aFpuUmxjbDlwWmw5bGJITmxRRGdLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpneENpQWdJQ0F2THlCbWIzVnVaQ0E5SUhSeWRXVUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0JtY21GdFpWOWlkWEo1SURZS0NsOWhaR1JmY0dGeWRHbGphWEJoZEdsdmJsOTBiMTlvYjJ4a1pYSmZZbXh2WTJ0QU1UQTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPakk0TlFvZ0lDQWdMeThnYVdZZ0tDRm1iM1Z1WkNrZ2V3b2dJQ0FnWm5KaGJXVmZaR2xuSURZS0lDQWdJR0p1ZWlCZllXUmtYM0JoY25ScFkybHdZWFJwYjI1ZmRHOWZhRzlzWkdWeVgyRm1kR1Z5WDJsbVgyVnNjMlZBTVRVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1qZzJMVEk0T1FvZ0lDQWdMeThnWTI5dWMzUWdjR0ZuYVc1aGRHVmtTMlY1SUQwZ2JtVjNJR0Z5WXpFME1UQmZTRzlzWkdsdVoxQmhjblJwZEdsdmJuTlFZV2RwYm1GMFpXUkxaWGtvZXdvZ0lDQWdMeThnSUNCb2IyeGtaWEk2SUdodmJHUmxjaXdLSUNBZ0lDOHZJQ0FnY0dGblpUb2diR0Z6ZEZCaFoyVXNDaUFnSUNBdkx5QjlLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCbWNtRnRaVjlrYVdjZ01nb2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPall3Q2lBZ0lDQXZMeUJyWlhsUWNtVm1hWGc2SUNkaGNtTXhOREV3WDJod1gyRW5MQW9nSUNBZ1lubDBaV01nTVRnZ0x5OGdJbUZ5WXpFME1UQmZhSEJmWVNJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnWkhWd0NpQWdJQ0JtY21GdFpWOWlkWEo1SURNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk1qa3dDaUFnSUNBdkx5QmpiMjV6ZENCcGRHVnRjME52ZFc1MElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0Mk5DaDBhR2x6TG1odmJHUmxjbEJoY25ScGRHbHZibk5CWkdSeVpYTnpaWE1vY0dGbmFXNWhkR1ZrUzJWNUtTNTJZV3gxWlM1c1pXNW5kR2dwQ2lBZ0lDQmliM2hmWjJWMENpQWdJQ0JoYzNObGNuUWdMeThnUW05NElHMTFjM1FnYUdGMlpTQjJZV3gxWlFvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUyQ2lBZ0lDQnBkRzlpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJNU1Rb2dJQ0FnTHk4Z2FXWWdLR2wwWlcxelEyOTFiblF1Ym1GMGFYWmxJRHdnTVRBcElIc0tJQ0FnSUdKMGIya0tJQ0FnSUhCMWMyaHBiblFnTVRBZ0x5OGdNVEFLSUNBZ0lEd0tJQ0FnSUdKNklGOWhaR1JmY0dGeWRHbGphWEJoZEdsdmJsOTBiMTlvYjJ4a1pYSmZaV3h6WlY5aWIyUjVRREV6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJNU5Bb2dJQ0FnTHk4Z0xpNHVkR2hwY3k1b2IyeGtaWEpRWVhKMGFYUnBiMjV6UVdSa2NtVnpjMlZ6S0hCaFoybHVZWFJsWkV0bGVTa3VkbUZzZFdVc0NpQWdJQ0JtY21GdFpWOWthV2NnTXdvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmWjJWMENpQWdJQ0JoYzNObGNuUWdMeThnUW05NElHMTFjM1FnYUdGMlpTQjJZV3gxWlFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pveU9UTXRNamsyQ2lBZ0lDQXZMeUIwYUdsekxtaHZiR1JsY2xCaGNuUnBkR2x2Ym5OQlpHUnlaWE56WlhNb2NHRm5hVzVoZEdWa1MyVjVLUzUyWVd4MVpTQTlJRnNLSUNBZ0lDOHZJQ0FnTGk0dWRHaHBjeTVvYjJ4a1pYSlFZWEowYVhScGIyNXpRV1JrY21WemMyVnpLSEJoWjJsdVlYUmxaRXRsZVNrdWRtRnNkV1VzQ2lBZ0lDQXZMeUFnSUhCaGNuUnBZMmx3WVhScGIyNHNDaUFnSUNBdkx5QmRDaUFnSUNCbGVIUnlZV04wSURJZ01Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3lPVFVLSUNBZ0lDOHZJSEJoY25ScFkybHdZWFJwYjI0c0NpQWdJQ0JpZVhSbFl5QXlOaUF2THlBd2VEQXdNREVLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJNU15MHlPVFlLSUNBZ0lDOHZJSFJvYVhNdWFHOXNaR1Z5VUdGeWRHbDBhVzl1YzBGa1pISmxjM05sY3lod1lXZHBibUYwWldSTFpYa3BMblpoYkhWbElEMGdXd29nSUNBZ0x5OGdJQ0F1TGk1MGFHbHpMbWh2YkdSbGNsQmhjblJwZEdsdmJuTkJaR1J5WlhOelpYTW9jR0ZuYVc1aGRHVmtTMlY1S1M1MllXeDFaU3dLSUNBZ0lDOHZJQ0FnY0dGeWRHbGphWEJoZEdsdmJpd0tJQ0FnSUM4dklGMEtJQ0FnSUdWNGRISmhZM1FnTWlBd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnTXpJS0lDQWdJQzhLSUNBZ0lHbDBiMklLSUNBZ0lHVjRkSEpoWTNRZ05pQXlDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHUnBaeUF4Q2lBZ0lDQmliM2hmWkdWc0NpQWdJQ0J3YjNBS0lDQWdJR0p2ZUY5d2RYUUtDbDloWkdSZmNHRnlkR2xqYVhCaGRHbHZibDkwYjE5b2IyeGtaWEpmWVdaMFpYSmZhV1pmWld4elpVQXhOVG9LSUNBZ0lISmxkSE4xWWdvS1gyRmtaRjl3WVhKMGFXTnBjR0YwYVc5dVgzUnZYMmh2YkdSbGNsOWxiSE5sWDJKdlpIbEFNVE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pJNU9Bb2dJQ0FnTHk4Z1kyOXVjM1FnYm1WM1RHRnpkRkJoWjJVZ1BTQnVaWGNnWVhKak5DNVZhVzUwVGpZMEtHeGhjM1JRWVdkbExtNWhkR2wyWlNBcklERXBDaUFnSUNCbWNtRnRaVjlrYVdjZ09Rb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJQ3NLSUNBZ0lHbDBiMklLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWprNUNpQWdJQ0F2THlCMGFHbHpMbWh2YkdSbGNsQmhjblJwZEdsdmJuTkRkWEp5Wlc1MFVHRm5aU2hvYjJ4a1pYSXBMblpoYkhWbElEMGdibVYzVEdGemRGQmhaMlVLSUNBZ0lHWnlZVzFsWDJScFp5QXhNQW9nSUNBZ1pHbG5JREVLSUNBZ0lHSnZlRjl3ZFhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk16QXdMVE13TXdvZ0lDQWdMeThnWTI5dWMzUWdibVYzVUdGbmFXNWhkR1ZrUzJWNUlEMGdibVYzSUdGeVl6RTBNVEJmU0c5c1pHbHVaMUJoY25ScGRHbHZibk5RWVdkcGJtRjBaV1JMWlhrb2V3b2dJQ0FnTHk4Z0lDQm9iMnhrWlhJNklHaHZiR1JsY2l3S0lDQWdJQzh2SUNBZ2NHRm5aVG9nYm1WM1RHRnpkRkJoWjJVc0NpQWdJQ0F2THlCOUtRb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNekEwQ2lBZ0lDQXZMeUIwYUdsekxtaHZiR1JsY2xCaGNuUnBkR2x2Ym5OQlpHUnlaWE56WlhNb2JtVjNVR0ZuYVc1aGRHVmtTMlY1S1M1MllXeDFaU0E5SUZ0d1lYSjBhV05wY0dGMGFXOXVYUW9nSUNBZ1lubDBaV01nTWpZZ0x5OGdNSGd3TURBeENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzJNQW9nSUNBZ0x5OGdhMlY1VUhKbFptbDRPaUFuWVhKak1UUXhNRjlvY0Y5aEp5d0tJQ0FnSUdKNWRHVmpJREU0SUM4dklDSmhjbU14TkRFd1gyaHdYMkVpQ2lBZ0lDQjFibU52ZG1WeUlESUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96TURRS0lDQWdJQzh2SUhSb2FYTXVhRzlzWkdWeVVHRnlkR2wwYVc5dWMwRmtaSEpsYzNObGN5aHVaWGRRWVdkcGJtRjBaV1JMWlhrcExuWmhiSFZsSUQwZ1czQmhjblJwWTJsd1lYUnBiMjVkQ2lBZ0lDQmtkWEFLSUNBZ0lHSnZlRjlrWld3S0lDQWdJSEJ2Y0FvZ0lDQWdjM2RoY0FvZ0lDQWdZbTk0WDNCMWRBb2dJQ0FnY21WMGMzVmlDZ3BmWVdSa1gzQmhjblJwWTJsd1lYUnBiMjVmZEc5ZmFHOXNaR1Z5WDJGbWRHVnlYMmxtWDJWc2MyVkFPRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TWpjeENpQWdJQ0F2THlCbWIzSWdLR3hsZENCamRYSlFZV2RsSUQwZ2NHRm5aVHNnWTNWeVVHRm5aUzV1WVhScGRtVWdQQ0JzWVhOMFVHRm5aUzV1WVhScGRtVTdJR04xY2xCaFoyVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqWTBLR04xY2xCaFoyVXVibUYwYVhabElDc2dNU2twSUhzS0lDQWdJR1p5WVcxbFgyUnBaeUE0Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ0t3b2dJQ0FnYVhSdllnb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBeENpQWdJQ0JpSUY5aFpHUmZjR0Z5ZEdsamFYQmhkR2x2Ymw5MGIxOW9iMnhrWlhKZmQyaHBiR1ZmZEc5d1FETUtDbDloWkdSZmNHRnlkR2xqYVhCaGRHbHZibDkwYjE5b2IyeGtaWEpmWVdaMFpYSmZhV1pmWld4elpVQXlNRG9LSUNBZ0lHWnlZVzFsWDJScFp5QTNDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnS3dvZ0lDQWdabkpoYldWZlluVnllU0EzQ2lBZ0lDQmlJRjloWkdSZmNHRnlkR2xqYVhCaGRHbHZibDkwYjE5b2IyeGtaWEpmWm05eVgyaGxZV1JsY2tBeE53b0tYMkZrWkY5d1lYSjBhV05wY0dGMGFXOXVYM1J2WDJodmJHUmxjbDloWm5SbGNsOW1iM0pBTWpFNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qSTFOd29nSUNBZ0x5OGdjbVYwZFhKdUlHWmhiSE5sQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem95T0RBS0lDQWdJQzh2SUdsbUlDaDBhR2x6TG1OdmJuUmhhVzV6UVdSa2NtVnpjeWgwYUdsekxtaHZiR1JsY2xCaGNuUnBkR2x2Ym5OQlpHUnlaWE56WlhNb2NHRm5hVzVoZEdWa1MyVjVLUzUyWVd4MVpTd2djR0Z5ZEdsamFYQmhkR2x2YmlrcElIc0tJQ0FnSUdJZ1gyRmtaRjl3WVhKMGFXTnBjR0YwYVc5dVgzUnZYMmh2YkdSbGNsOWhablJsY2w5cGJteHBibVZrWDNOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk9rRnlZekUwTVRBdVkyOXVkR0ZwYm5OQlpHUnlaWE56UURJeUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvNlFYSmpNVFF4TUM1ZmRISmhibk5tWlhKZmNHRnlkR2wwYVc5dUtHWnliMjA2SUdKNWRHVnpMQ0JtY205dFVHRnlkR2wwYVc5dU9pQmllWFJsY3l3Z2RHODZJR0o1ZEdWekxDQjBiMUJoY25ScGRHbHZiam9nWW5sMFpYTXNJR0Z0YjNWdWREb2dZbmwwWlhNc0lHUmhkR0U2SUdKNWRHVnpLU0F0UGlCMmIybGtPZ3BmZEhKaGJuTm1aWEpmY0dGeWRHbDBhVzl1T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvek1UZ3RNekkxQ2lBZ0lDQXZMeUJ3Y205MFpXTjBaV1FnWDNSeVlXNXpabVZ5WDNCaGNuUnBkR2x2YmlnS0lDQWdJQzh2SUNBZ1puSnZiVG9nWVhKak5DNUJaR1J5WlhOekxBb2dJQ0FnTHk4Z0lDQm1jbTl0VUdGeWRHbDBhVzl1T2lCaGNtTTBMa0ZrWkhKbGMzTXNDaUFnSUNBdkx5QWdJSFJ2T2lCaGNtTTBMa0ZrWkhKbGMzTXNDaUFnSUNBdkx5QWdJSFJ2VUdGeWRHbDBhVzl1T2lCaGNtTTBMa0ZrWkhKbGMzTXNDaUFnSUNBdkx5QWdJR0Z0YjNWdWREb2dZWEpqTkM1VmFXNTBUakkxTml3S0lDQWdJQzh2SUNBZ1pHRjBZVG9nWVhKak5DNUVlVzVoYldsalFubDBaWE1zQ2lBZ0lDQXZMeUFwT2lCMmIybGtJSHNLSUNBZ0lIQnliM1J2SURZZ01Bb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR1IxY0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvek1qWUtJQ0FnSUM4dklHRnpjMlZ5ZENoaGJXOTFiblF1Ym1GMGFYWmxJRDRnTUN3Z0owbHVkbUZzYVdRZ1lXMXZkVzUwSnlrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0Q2lBZ0lDQmlQZ29nSUNBZ1lYTnpaWEowSUM4dklFbHVkbUZzYVdRZ1lXMXZkVzUwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pNeU9Bb2dJQ0FnTHk4Z1kyOXVjM1FnWm5KdmJVdGxlU0E5SUc1bGR5QmhjbU14TkRFd1gxQmhjblJwZEdsdmJrdGxlU2g3SUdodmJHUmxjam9nWm5KdmJTd2djR0Z5ZEdsMGFXOXVPaUJtY205dFVHRnlkR2wwYVc5dUlIMHBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUWUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE5Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPalUzQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdjR0Z5ZEdsMGFXOXVjeUE5SUVKdmVFMWhjRHhoY21NeE5ERXdYMUJoY25ScGRHbHZia3RsZVN3Z1lYSmpOQzVWYVc1MFRqSTFOajRvZXlCclpYbFFjbVZtYVhnNklDZGhjbU14TkRFd1gzQW5JSDBwQ2lBZ0lDQmllWFJsWXlBNElDOHZJQ0poY21NeE5ERXdYM0FpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1IxY0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvek1qa0tJQ0FnSUM4dklHbG1JQ2doZEdocGN5NXdZWEowYVhScGIyNXpLR1p5YjIxTFpYa3BMbVY0YVhOMGN5a2dld29nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JpYm5vZ1gzUnlZVzV6Wm1WeVgzQmhjblJwZEdsdmJsOWhablJsY2w5cFpsOWxiSE5sUURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk16TXdDaUFnSUNBdkx5QjBhR2x6TG5CaGNuUnBkR2x2Ym5Nb1puSnZiVXRsZVNrdWRtRnNkV1VnUFNCdVpYY2dZWEpqTkM1VmFXNTBUakkxTmlnd0tRb2dJQ0FnWm5KaGJXVmZaR2xuSURJS0lDQWdJR0o1ZEdWalh6RWdMeThnTUhnd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd0NpQWdJQ0JpYjNoZmNIVjBDZ3BmZEhKaGJuTm1aWEpmY0dGeWRHbDBhVzl1WDJGbWRHVnlYMmxtWDJWc2MyVkFNam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TXpNeUNpQWdJQ0F2THlCMGFHbHpMbkJoY25ScGRHbHZibk1vWm5KdmJVdGxlU2t1ZG1Gc2RXVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqSTFOaWgwYUdsekxuQmhjblJwZEdsdmJuTW9abkp2YlV0bGVTa3VkbUZzZFdVdWJtRjBhWFpsSUMwZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXlDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOW5aWFFLSUNBZ0lHRnpjMlZ5ZENBdkx5QkNiM2dnYlhWemRDQm9ZWFpsSUhaaGJIVmxDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdJdENpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eUlDOHZJRE15Q2lBZ0lDQThQUW9nSUNBZ1lYTnpaWEowSUM4dklHOTJaWEptYkc5M0NpQWdJQ0JwYm5Salh6SWdMeThnTXpJS0lDQWdJR0o2WlhKdkNpQWdJQ0JrZFhBS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdZbndLSUNBZ0lHSnZlRjl3ZFhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk16TTNMVE0wTXdvZ0lDQWdMeThnYm1WM0lHRnlZekUwTVRCZmNHRnlkR2wwYVc5dVgzUnlZVzV6Wm1WeUtIc0tJQ0FnSUM4dklDQWdabkp2YlRvZ1puSnZiU3dLSUNBZ0lDOHZJQ0FnZEc4NklIUnZMQW9nSUNBZ0x5OGdJQ0J3WVhKMGFYUnBiMjQ2SUdaeWIyMVFZWEowYVhScGIyNHNDaUFnSUNBdkx5QWdJR0Z0YjNWdWREb2dZVzF2ZFc1MExBb2dJQ0FnTHk4Z0lDQmtZWFJoT2lCa1lYUmhMQW9nSUNBZ0x5OGdmU2tzQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVFlLSUNBZ0lHWnlZVzFsWDJScFp5QXROQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVFVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJSEIxYzJoaWVYUmxjeUF3ZURBd09ESUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1puSmhiV1ZmWkdsbklDMHhDaUFnSUNCamIyNWpZWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TXpNMUxUTTBOQW9nSUNBZ0x5OGdaVzFwZENnS0lDQWdJQzh2SUNBZ0oxUnlZVzV6Wm1WeUp5d0tJQ0FnSUM4dklDQWdibVYzSUdGeVl6RTBNVEJmY0dGeWRHbDBhVzl1WDNSeVlXNXpabVZ5S0hzS0lDQWdJQzh2SUNBZ0lDQm1jbTl0T2lCbWNtOXRMQW9nSUNBZ0x5OGdJQ0FnSUhSdk9pQjBieXdLSUNBZ0lDOHZJQ0FnSUNCd1lYSjBhWFJwYjI0NklHWnliMjFRWVhKMGFYUnBiMjRzQ2lBZ0lDQXZMeUFnSUNBZ1lXMXZkVzUwT2lCaGJXOTFiblFzQ2lBZ0lDQXZMeUFnSUNBZ1pHRjBZVG9nWkdGMFlTd0tJQ0FnSUM4dklDQWdmU2tzQ2lBZ0lDQXZMeUFwQ2lBZ0lDQmllWFJsWXlBMUlDOHZJREI0TURBd01nb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZ3lNRFppTnprME1DQXZMeUJ0WlhSb2IyUWdJbFJ5WVc1elptVnlLQ2hoWkdSeVpYTnpMR0ZrWkhKbGMzTXNZV1JrY21WemN5eDFhVzUwTWpVMkxHSjVkR1ZiWFNrcElnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNelEzQ2lBZ0lDQXZMeUJwWmlBb2RHOVFZWEowYVhScGIyNGdJVDA5SUdaeWIyMVFZWEowYVhScGIyNHBJSHNLSUNBZ0lHWnlZVzFsWDJScFp5QXRNd29nSUNBZ1puSmhiV1ZmWkdsbklDMDFDaUFnSUNBaFBRb2dJQ0FnWW5vZ1gzUnlZVzV6Wm1WeVgzQmhjblJwZEdsdmJsOWhablJsY2w5cFpsOWxiSE5sUURRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk16UTRDaUFnSUNBdkx5QjBhR2x6TGw5aFpHUmZjR0Z5ZEdsamFYQmhkR2x2Ymw5MGIxOW9iMnhrWlhJb2RHOHNJSFJ2VUdGeWRHbDBhVzl1S1FvZ0lDQWdabkpoYldWZlpHbG5JQzAwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVE1LSUNBZ0lHTmhiR3h6ZFdJZ1gyRmtaRjl3WVhKMGFXTnBjR0YwYVc5dVgzUnZYMmh2YkdSbGNnb0tYM1J5WVc1elptVnlYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFEUTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPak0xTWdvZ0lDQWdMeThnWTI5dWMzUWdkRzlMWlhrZ1BTQnVaWGNnWVhKak1UUXhNRjlRWVhKMGFYUnBiMjVMWlhrb2V5Qm9iMnhrWlhJNklIUnZMQ0J3WVhKMGFYUnBiMjQ2SUhSdlVHRnlkR2wwYVc5dUlIMHBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPalUzQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdjR0Z5ZEdsMGFXOXVjeUE5SUVKdmVFMWhjRHhoY21NeE5ERXdYMUJoY25ScGRHbHZia3RsZVN3Z1lYSmpOQzVWYVc1MFRqSTFOajRvZXlCclpYbFFjbVZtYVhnNklDZGhjbU14TkRFd1gzQW5JSDBwQ2lBZ0lDQmllWFJsWXlBNElDOHZJQ0poY21NeE5ERXdYM0FpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1IxY0FvZ0lDQWdabkpoYldWZlluVnllU0F4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pNMU13b2dJQ0FnTHk4Z2FXWWdLQ0YwYUdsekxuQmhjblJwZEdsdmJuTW9kRzlMWlhrcExtVjRhWE4wY3lrZ2V3b2dJQ0FnWW05NFgyeGxiZ29nSUNBZ1luVnllU0F4Q2lBZ0lDQmlibm9nWDNSeVlXNXpabVZ5WDNCaGNuUnBkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRRFlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TXpVMENpQWdJQ0F2THlCMGFHbHpMbkJoY25ScGRHbHZibk1vZEc5TFpYa3BMblpoYkhWbElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0eU5UWW9NQ2tLSUNBZ0lHWnlZVzFsWDJScFp5QXhDaUFnSUNCaWVYUmxZMTh4SUM4dklEQjRNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNQW9nSUNBZ1ltOTRYM0IxZEFvS1gzUnlZVzV6Wm1WeVgzQmhjblJwZEdsdmJsOWhablJsY2w5cFpsOWxiSE5sUURZNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qTTFOZ29nSUNBZ0x5OGdkR2hwY3k1d1lYSjBhWFJwYjI1ektIUnZTMlY1S1M1MllXeDFaU0E5SUc1bGR5QmhjbU0wTGxWcGJuUk9NalUyS0hSb2FYTXVjR0Z5ZEdsMGFXOXVjeWgwYjB0bGVTa3VkbUZzZFdVdWJtRjBhWFpsSUNzZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXhDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOW5aWFFLSUNBZ0lHRnpjMlZ5ZENBdkx5QkNiM2dnYlhWemRDQm9ZWFpsSUhaaGJIVmxDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdJckNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eUlDOHZJRE15Q2lBZ0lDQThQUW9nSUNBZ1lYTnpaWEowSUM4dklHOTJaWEptYkc5M0NpQWdJQ0JtY21GdFpWOWthV2NnTUFvZ0lDQWdZbndLSUNBZ0lHSnZlRjl3ZFhRS0lDQWdJSEpsZEhOMVlnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk9rRnlZekUwTVRBdVlYSmpNVFF4TUY5aGRYUm9iM0pwZW1WZmIzQmxjbUYwYjNKZllubGZjRzl5ZEdsdmJpaG9iMnhrWlhJNklHSjVkR1Z6TENCdmNHVnlZWFJ2Y2pvZ1lubDBaWE1zSUhCaGNuUnBkR2x2YmpvZ1lubDBaWE1zSUdGdGIzVnVkRG9nWW5sMFpYTXBJQzArSUhadmFXUTZDbUZ5WXpFME1UQmZZWFYwYUc5eWFYcGxYMjl3WlhKaGRHOXlYMko1WDNCdmNuUnBiMjQ2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pNMU9TMHpOalVLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpNVFF4TUY5aGRYUm9iM0pwZW1WZmIzQmxjbUYwYjNKZllubGZjRzl5ZEdsdmJpZ0tJQ0FnSUM4dklDQWdhRzlzWkdWeU9pQmhjbU0wTGtGa1pISmxjM01zQ2lBZ0lDQXZMeUFnSUc5d1pYSmhkRzl5T2lCaGNtTTBMa0ZrWkhKbGMzTXNDaUFnSUNBdkx5QWdJSEJoY25ScGRHbHZiam9nWVhKak5DNUJaR1J5WlhOekxBb2dJQ0FnTHk4Z0lDQmhiVzkxYm5RNklHRnlZelF1VldsdWRFNHlOVFlzQ2lBZ0lDQXZMeUFwT2lCMmIybGtJSHNLSUNBZ0lIQnliM1J2SURRZ01Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6b3pOallLSUNBZ0lDOHZJR0Z6YzJWeWRDaHVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBJRDA5UFNCb2IyeGtaWElzSUNkUGJteDVJR2h2YkdSbGNpQmpZVzRnWVhWMGFHOXlhWHBsSUhCdmNuUnBiMjRuS1FvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ1puSmhiV1ZmWkdsbklDMDBDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dWJIa2dhRzlzWkdWeUlHTmhiaUJoZFhSb2IzSnBlbVVnY0c5eWRHbHZiZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96TmpjS0lDQWdJQzh2SUdOdmJuTjBJR3RsZVNBOUlHNWxkeUJoY21NeE5ERXdYMDl3WlhKaGRHOXlVRzl5ZEdsdmJrdGxlU2g3SUdodmJHUmxjaXdnYjNCbGNtRjBiM0lzSUhCaGNuUnBkR2x2YmlCOUtRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR052Ym1OaGRBb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZOak1LSUNBZ0lDOHZJSEIxWW14cFl5QnZjR1Z5WVhSdmNsQnZjblJwYjI1QmJHeHZkMkZ1WTJWeklEMGdRbTk0VFdGd1BHRnlZekUwTVRCZlQzQmxjbUYwYjNKUWIzSjBhVzl1UzJWNUxDQmhjbU0wTGxWcGJuUk9NalUyUGloN0lHdGxlVkJ5WldacGVEb2dKMkZ5WXpFME1UQmZiM0JoSnlCOUtRb2dJQ0FnWW5sMFpXTWdNVFFnTHk4Z0ltRnlZekUwTVRCZmIzQmhJZ29nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pNMk9Bb2dJQ0FnTHk4Z2RHaHBjeTV2Y0dWeVlYUnZjbEJ2Y25ScGIyNUJiR3h2ZDJGdVkyVnpLR3RsZVNrdWRtRnNkV1VnUFNCaGJXOTFiblFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1ltOTRYM0IxZEFvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvNlFYSmpNVFF4TUM1aGNtTXhOREV3WDJselgyOXdaWEpoZEc5eVgySjVYM0J2Y25ScGIyNG9hRzlzWkdWeU9pQmllWFJsY3l3Z2IzQmxjbUYwYjNJNklHSjVkR1Z6TENCd1lYSjBhWFJwYjI0NklHSjVkR1Z6S1NBdFBpQmllWFJsY3pvS1lYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNsOWllVjl3YjNKMGFXOXVPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96TnpFdE16YzJDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6RTBNVEJmYVhOZmIzQmxjbUYwYjNKZllubGZjRzl5ZEdsdmJpZ0tJQ0FnSUM4dklDQWdhRzlzWkdWeU9pQmhjbU0wTGtGa1pISmxjM01zQ2lBZ0lDQXZMeUFnSUc5d1pYSmhkRzl5T2lCaGNtTTBMa0ZrWkhKbGMzTXNDaUFnSUNBdkx5QWdJSEJoY25ScGRHbHZiam9nWVhKak5DNUJaR1J5WlhOekxBb2dJQ0FnTHk4Z0tUb2dZWEpqTkM1Q2IyOXNJSHNLSUNBZ0lIQnliM1J2SURNZ01Rb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk16YzNDaUFnSUNBdkx5QnBaaUFvYjNCbGNtRjBiM0lnUFQwOUlHaHZiR1JsY2lrZ2NtVjBkWEp1SUc1bGR5QmhjbU0wTGtKdmIyd29kSEoxWlNrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdabkpoYldWZlpHbG5JQzB6Q2lBZ0lDQTlQUW9nSUNBZ1lub2dZWEpqTVRReE1GOXBjMTl2Y0dWeVlYUnZjbDlpZVY5d2IzSjBhVzl1WDJGbWRHVnlYMmxtWDJWc2MyVkFNZ29nSUNBZ1lubDBaV01nTnlBdkx5QXdlRGd3Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQnlaWFJ6ZFdJS0NtRnlZekUwTVRCZmFYTmZiM0JsY21GMGIzSmZZbmxmY0c5eWRHbHZibDloWm5SbGNsOXBabDlsYkhObFFESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPak0zT0FvZ0lDQWdMeThnWTI5dWMzUWdhMlY1SUQwZ2JtVjNJR0Z5WXpFME1UQmZUM0JsY21GMGIzSlFiM0owYVc5dVMyVjVLSHNnYUc5c1pHVnlMQ0J2Y0dWeVlYUnZjaXdnY0dGeWRHbDBhVzl1SUgwcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzJNd29nSUNBZ0x5OGdjSFZpYkdsaklHOXdaWEpoZEc5eVVHOXlkR2x2YmtGc2JHOTNZVzVqWlhNZ1BTQkNiM2hOWVhBOFlYSmpNVFF4TUY5UGNHVnlZWFJ2Y2xCdmNuUnBiMjVMWlhrc0lHRnlZelF1VldsdWRFNHlOVFkrS0hzZ2EyVjVVSEpsWm1sNE9pQW5ZWEpqTVRReE1GOXZjR0VuSUgwcENpQWdJQ0JpZVhSbFl5QXhOQ0F2THlBaVlYSmpNVFF4TUY5dmNHRWlDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHUjFjQW9nSUNBZ1puSmhiV1ZmWW5WeWVTQXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPak0zT1FvZ0lDQWdMeThnYVdZZ0tDRjBhR2x6TG05d1pYSmhkRzl5VUc5eWRHbHZia0ZzYkc5M1lXNWpaWE1vYTJWNUtTNWxlR2x6ZEhNcElISmxkSFZ5YmlCdVpYY2dZWEpqTkM1Q2IyOXNLR1poYkhObEtRb2dJQ0FnWW05NFgyeGxiZ29nSUNBZ1luVnllU0F4Q2lBZ0lDQmlibm9nWVhKak1UUXhNRjlwYzE5dmNHVnlZWFJ2Y2w5aWVWOXdiM0owYVc5dVgyRm1kR1Z5WDJsbVgyVnNjMlZBTkFvZ0lDQWdZbmwwWldNZ01USWdMeThnTUhnd01Bb2dJQ0FnYzNkaGNBb2dJQ0FnY21WMGMzVmlDZ3BoY21NeE5ERXdYMmx6WDI5d1pYSmhkRzl5WDJKNVgzQnZjblJwYjI1ZllXWjBaWEpmYVdaZlpXeHpaVUEwT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvek9EQUtJQ0FnSUM4dklISmxkSFZ5YmlCdVpYY2dZWEpqTkM1Q2IyOXNLSFJvYVhNdWIzQmxjbUYwYjNKUWIzSjBhVzl1UVd4c2IzZGhibU5sY3loclpYa3BMblpoYkhWbExtNWhkR2wyWlNBK0lEQXBDaUFnSUNCbWNtRnRaVjlrYVdjZ01Bb2dJQ0FnWW05NFgyZGxkQW9nSUNBZ1lYTnpaWEowSUM4dklFSnZlQ0J0ZFhOMElHaGhkbVVnZG1Gc2RXVUtJQ0FnSUhCMWMyaGllWFJsY3lBd2VBb2dJQ0FnWWo0S0lDQWdJR0o1ZEdWaklERXlJQzh2SURCNE1EQUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lITmxkR0pwZEFvZ0lDQWdjM2RoY0FvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvNlFYSmpNVFF4TUM1aGNtTXhOREV3WDJsemMzVmxYMko1WDNCaGNuUnBkR2x2YmloMGJ6b2dZbmwwWlhNc0lIQmhjblJwZEdsdmJqb2dZbmwwWlhNc0lHRnRiM1Z1ZERvZ1lubDBaWE1zSUdSaGRHRTZJR0o1ZEdWektTQXRQaUIyYjJsa09ncGhjbU14TkRFd1gybHpjM1ZsWDJKNVgzQmhjblJwZEdsdmJqb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNemd6TFRNNE9Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0NrS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NeE5ERXdYMmx6YzNWbFgySjVYM0JoY25ScGRHbHZiaWdLSUNBZ0lDOHZJQ0FnZEc4NklHRnlZelF1UVdSa2NtVnpjeXdLSUNBZ0lDOHZJQ0FnY0dGeWRHbDBhVzl1T2lCaGNtTTBMa0ZrWkhKbGMzTXNDaUFnSUNBdkx5QWdJR0Z0YjNWdWREb2dZWEpqTkM1VmFXNTBUakkxTml3S0lDQWdJQzh2SUNBZ1pHRjBZVG9nWVhKak5DNUVlVzVoYldsalFubDBaWE1zQ2lBZ0lDQXZMeUFwT2lCMmIybGtJSHNLSUNBZ0lIQnliM1J2SURRZ01Bb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR1IxY0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvek9UQUtJQ0FnSUM4dklHRnpjMlZ5ZENoMGFHbHpMbUZ5WXpnNFgybHpYMjkzYm1WeUtHNWxkeUJoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa3BMbTVoZEdsMlpTQTlQVDBnZEhKMVpTd2dKMjl1YkhsZmIzZHVaWEluS1FvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ1kyRnNiSE4xWWlCaGNtTTRPRjlwYzE5dmQyNWxjZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdkbGRHSnBkQW9nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYjI1c2VWOXZkMjVsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvek9URUtJQ0FnSUM4dklHRnpjMlZ5ZENoaGJXOTFiblF1Ym1GMGFYWmxJRDRnTUN3Z0owbHVkbUZzYVdRZ1lXMXZkVzUwSnlrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0Q2lBZ0lDQmlQZ29nSUNBZ1lYTnpaWEowSUM4dklFbHVkbUZzYVdRZ1lXMXZkVzUwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pNNU13b2dJQ0FnTHk4Z1kyOXVjM1FnZEc5TFpYa2dQU0J1WlhjZ1lYSmpNVFF4TUY5UVlYSjBhWFJwYjI1TFpYa29leUJvYjJ4a1pYSTZJSFJ2TENCd1lYSjBhWFJwYjI0Z2ZTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE5Bb2dJQ0FnWm5KaGJXVmZaR2xuSUMwekNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzFOd29nSUNBZ0x5OGdjSFZpYkdsaklIQmhjblJwZEdsdmJuTWdQU0JDYjNoTllYQThZWEpqTVRReE1GOVFZWEowYVhScGIyNUxaWGtzSUdGeVl6UXVWV2x1ZEU0eU5UWStLSHNnYTJWNVVISmxabWw0T2lBbllYSmpNVFF4TUY5d0p5QjlLUW9nSUNBZ1lubDBaV01nT0NBdkx5QWlZWEpqTVRReE1GOXdJZ29nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmtkWEFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TXprMENpQWdJQ0F2THlCcFppQW9JWFJvYVhNdWNHRnlkR2wwYVc5dWN5aDBiMHRsZVNrdVpYaHBjM1J6S1NCN0NpQWdJQ0JpYjNoZmJHVnVDaUFnSUNCaWRYSjVJREVLSUNBZ0lHSnVlaUJoY21NeE5ERXdYMmx6YzNWbFgySjVYM0JoY25ScGRHbHZibDloWm5SbGNsOXBabDlsYkhObFFESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZNemsxQ2lBZ0lDQXZMeUIwYUdsekxuQmhjblJwZEdsdmJuTW9kRzlMWlhrcExuWmhiSFZsSUQwZ2JtVjNJR0Z5WXpRdVZXbHVkRTR5TlRZb01Da0tJQ0FnSUdaeVlXMWxYMlJwWnlBekNpQWdJQ0JpZVhSbFkxOHhJQzh2SURCNE1EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01Bb2dJQ0FnWW05NFgzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96T1RZS0lDQWdJQzh2SUhSb2FYTXVYMkZrWkY5d1lYSjBhV05wY0dGMGFXOXVYM1J2WDJodmJHUmxjaWgwYnl3Z2NHRnlkR2wwYVc5dUtRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR05oYkd4emRXSWdYMkZrWkY5d1lYSjBhV05wY0dGMGFXOXVYM1J2WDJodmJHUmxjZ29LWVhKak1UUXhNRjlwYzNOMVpWOWllVjl3WVhKMGFYUnBiMjVmWVdaMFpYSmZhV1pmWld4elpVQXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem96T1RnS0lDQWdJQzh2SUhSb2FYTXVjR0Z5ZEdsMGFXOXVjeWgwYjB0bGVTa3VkbUZzZFdVZ1BTQnVaWGNnWVhKak5DNVZhVzUwVGpJMU5paDBhR2x6TG5CaGNuUnBkR2x2Ym5Nb2RHOUxaWGtwTG5aaGJIVmxMbTVoZEdsMlpTQXJJR0Z0YjNWdWRDNXVZWFJwZG1VcENpQWdJQ0JtY21GdFpWOWthV2NnTXdvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmWjJWMENpQWdJQ0JoYzNObGNuUWdMeThnUW05NElHMTFjM1FnYUdGMlpTQjJZV3gxWlFvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmlLd29nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNaUF2THlBek1nb2dJQ0FnUEQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJ2ZG1WeVpteHZkd29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNCaWVtVnlid29nSUNBZ1pIVndDaUFnSUNCbWNtRnRaVjlpZFhKNUlEQUtJQ0FnSUdKOENpQWdJQ0JpYjNoZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZOVE1LSUNBZ0lDOHZJSEIxWW14cFl5QmlZV3hoYm1ObGN5QTlJRUp2ZUUxaGNEeEJaR1J5WlhOekxDQlZhVzUwVGpJMU5qNG9leUJyWlhsUWNtVm1hWGc2SUNkaUp5QjlLUW9nSUNBZ1lubDBaV01nTkNBdkx5QWlZaUlLSUNBZ0lHWnlZVzFsWDJScFp5QXROQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmtkWEFLSUNBZ0lHWnlZVzFsWDJKMWNua2dNUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTURBS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1aVlXeGhibU5sY3loMGJ5a3VaWGhwYzNSektTQjdDaUFnSUNCaWIzaGZiR1Z1Q2lBZ0lDQmlkWEo1SURFS0lDQWdJR0p1ZWlCaGNtTXhOREV3WDJsemMzVmxYMko1WDNCaGNuUnBkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRRFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TkRBeENpQWdJQ0F2THlCMGFHbHpMbUpoYkdGdVkyVnpLSFJ2S1M1MllXeDFaU0E5SUc1bGR5QmhjbU0wTGxWcGJuUk9NalUyS0RBcENpQWdJQ0JtY21GdFpWOWthV2NnTVFvZ0lDQWdZbmwwWldOZk1TQXZMeUF3ZURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBS0lDQWdJR0p2ZUY5d2RYUUtDbUZ5WXpFME1UQmZhWE56ZFdWZllubGZjR0Z5ZEdsMGFXOXVYMkZtZEdWeVgybG1YMlZzYzJWQU5Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZOREF6Q2lBZ0lDQXZMeUIwYUdsekxtSmhiR0Z1WTJWektIUnZLUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLSFJvYVhNdVltRnNZVzVqWlhNb2RHOHBMblpoYkhWbExtNWhkR2wyWlNBcklHRnRiM1Z1ZEM1dVlYUnBkbVVwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dNUW9nSUNBZ1pIVndDaUFnSUNCaWIzaGZaMlYwQ2lBZ0lDQmhjM05sY25RZ0x5OGdRbTk0SUcxMWMzUWdhR0YyWlNCMllXeDFaUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCaUt3b2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdQRDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnZkbVZ5Wm14dmR3b2dJQ0FnWm5KaGJXVmZaR2xuSURBS0lDQWdJR1IxY0FvZ0lDQWdZMjkyWlhJZ013b2dJQ0FnWW53S0lDQWdJR0p2ZUY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzFNUW9nSUNBZ0x5OGdjSFZpYkdsaklIUnZkR0ZzVTNWd2NHeDVJRDBnUjJ4dlltRnNVM1JoZEdVOFZXbHVkRTR5TlRZK0tIc2dhMlY1T2lBbmRDY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh6SUM4dklDSjBJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZOREEwQ2lBZ0lDQXZMeUIwYUdsekxuUnZkR0ZzVTNWd2NHeDVMblpoYkhWbElEMGdibVYzSUdGeVl6UXVWV2x1ZEU0eU5UWW9kR2hwY3k1MGIzUmhiRk4xY0hCc2VTNTJZV3gxWlM1dVlYUnBkbVVnS3lCaGJXOTFiblF1Ym1GMGFYWmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCaUt3b2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdQRDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnZkbVZ5Wm14dmR3b2dJQ0FnWW53S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pvMU1Rb2dJQ0FnTHk4Z2NIVmliR2xqSUhSdmRHRnNVM1Z3Y0d4NUlEMGdSMnh2WW1Gc1UzUmhkR1U4VldsdWRFNHlOVFkrS0hzZ2EyVjVPaUFuZENjZ2ZTa0tJQ0FnSUdKNWRHVmpYek1nTHk4Z0luUWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPalF3TkFvZ0lDQWdMeThnZEdocGN5NTBiM1JoYkZOMWNIQnNlUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLSFJvYVhNdWRHOTBZV3hUZFhCd2JIa3VkbUZzZFdVdWJtRjBhWFpsSUNzZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lITjNZWEFLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pRd05Rb2dJQ0FnTHk4Z1pXMXBkQ2duU1hOemRXVW5MQ0J1WlhjZ1lYSmpNVFF4TUY5d1lYSjBhWFJwYjI1ZmFYTnpkV1VvZXlCMGJ5d2djR0Z5ZEdsMGFXOXVMQ0JoYlc5MWJuUXNJR1JoZEdFZ2ZTa3BDaUFnSUNCbWNtRnRaVjlrYVdjZ01nb2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdKNWRHVmpJREkzSUM4dklEQjRNREEyTWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR052Ym1OaGRBb2dJQ0FnWW5sMFpXTWdOU0F2THlBd2VEQXdNRElLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0Wm1FME5ETmlNV0lnTHk4Z2JXVjBhRzlrSUNKSmMzTjFaU2dvWVdSa2NtVnpjeXhoWkdSeVpYTnpMSFZwYm5ReU5UWXNZbmwwWlZ0ZEtTa2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2NtVjBjM1ZpQ2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem82UVhKak1UUXhNQzVoY21NeE5ERXdYM0psWkdWbGJWOWllVjl3WVhKMGFYUnBiMjRvY0dGeWRHbDBhVzl1T2lCaWVYUmxjeXdnWVcxdmRXNTBPaUJpZVhSbGN5d2daR0YwWVRvZ1lubDBaWE1wSUMwK0lIWnZhV1E2Q21GeVl6RTBNVEJmY21Wa1pXVnRYMko1WDNCaGNuUnBkR2x2YmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5EQTRMVFF3T1FvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lDOHZJSEIxWW14cFl5QmhjbU14TkRFd1gzSmxaR1ZsYlY5aWVWOXdZWEowYVhScGIyNG9jR0Z5ZEdsMGFXOXVPaUJoY21NMExrRmtaSEpsYzNNc0lHRnRiM1Z1ZERvZ1lYSmpOQzVWYVc1MFRqSTFOaXdnWkdGMFlUb2dZWEpqTkM1RWVXNWhiV2xqUW5sMFpYTXBPaUIyYjJsa0lIc0tJQ0FnSUhCeWIzUnZJRE1nTUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvME1UQUtJQ0FnSUM4dklHTnZibk4wSUdaeWIyMGdQU0J1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwQ2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPalF4TVFvZ0lDQWdMeThnWVhOelpYSjBLR0Z0YjNWdWRDNXVZWFJwZG1VZ1BpQXdMQ0FuU1c1MllXeHBaQ0JoYlc5MWJuUW5LUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHlDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZ0tJQ0FnSUdJK0NpQWdJQ0JoYzNObGNuUWdMeThnU1c1MllXeHBaQ0JoYlc5MWJuUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZOREV5Q2lBZ0lDQXZMeUJqYjI1emRDQm1jbTl0UzJWNUlEMGdibVYzSUdGeVl6RTBNVEJmVUdGeWRHbDBhVzl1UzJWNUtIc2dhRzlzWkdWeU9pQm1jbTl0TENCd1lYSjBhWFJwYjI0Z2ZTa0tJQ0FnSUdSMWNBb2dJQ0FnWm5KaGJXVmZaR2xuSUMwekNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8xTndvZ0lDQWdMeThnY0hWaWJHbGpJSEJoY25ScGRHbHZibk1nUFNCQ2IzaE5ZWEE4WVhKak1UUXhNRjlRWVhKMGFYUnBiMjVMWlhrc0lHRnlZelF1VldsdWRFNHlOVFkrS0hzZ2EyVjVVSEpsWm1sNE9pQW5ZWEpqTVRReE1GOXdKeUI5S1FvZ0lDQWdZbmwwWldNZ09DQXZMeUFpWVhKak1UUXhNRjl3SWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qUXhNd29nSUNBZ0x5OGdZWE56WlhKMEtIUm9hWE11Y0dGeWRHbDBhVzl1Y3lobWNtOXRTMlY1S1M1bGVHbHpkSE1zSUNkUVlYSjBhWFJwYjI0Z1ltRnNZVzVqWlNCdGFYTnphVzVuSnlrS0lDQWdJR1IxY0FvZ0lDQWdZbTk0WDJ4bGJnb2dJQ0FnWW5WeWVTQXhDaUFnSUNCaGMzTmxjblFnTHk4Z1VHRnlkR2wwYVc5dUlHSmhiR0Z1WTJVZ2JXbHpjMmx1WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvME1UUUtJQ0FnSUM4dklHRnpjMlZ5ZENoMGFHbHpMbkJoY25ScGRHbHZibk1vWm5KdmJVdGxlU2t1ZG1Gc2RXVXVibUYwYVhabElENDlJR0Z0YjNWdWRDNXVZWFJwZG1Vc0lDZEpibk4xWm1acFkybGxiblFnY0dGeWRHbDBhVzl1SUdKaGJHRnVZMlVuS1FvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmWjJWMENpQWdJQ0JoYzNObGNuUWdMeThnUW05NElHMTFjM1FnYUdGMlpTQjJZV3gxWlFvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmlQajBLSUNBZ0lHRnpjMlZ5ZENBdkx5Qkpibk4xWm1acFkybGxiblFnY0dGeWRHbDBhVzl1SUdKaGJHRnVZMlVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TkRFMUNpQWdJQ0F2THlCMGFHbHpMbkJoY25ScGRHbHZibk1vWm5KdmJVdGxlU2t1ZG1Gc2RXVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqSTFOaWgwYUdsekxuQmhjblJwZEdsdmJuTW9abkp2YlV0bGVTa3VkbUZzZFdVdWJtRjBhWFpsSUMwZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lHUjFjQW9nSUNBZ1ltOTRYMmRsZEFvZ0lDQWdZWE56WlhKMElDOHZJRUp2ZUNCdGRYTjBJR2hoZG1VZ2RtRnNkV1VLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1lpMEtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnTXpJS0lDQWdJRHc5Q2lBZ0lDQmhjM05sY25RZ0x5OGdiM1psY21ac2IzY0tJQ0FnSUdsdWRHTmZNaUF2THlBek1nb2dJQ0FnWW5wbGNtOEtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdOQW9nSUNBZ1lud0tJQ0FnSUdKdmVGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem8xTXdvZ0lDQWdMeThnY0hWaWJHbGpJR0poYkdGdVkyVnpJRDBnUW05NFRXRndQRUZrWkhKbGMzTXNJRlZwYm5ST01qVTJQaWg3SUd0bGVWQnlaV1pwZURvZ0oySW5JSDBwQ2lBZ0lDQmllWFJsWXlBMElDOHZJQ0ppSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JrZFhBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5ERTNDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVpWVd4aGJtTmxjeWhtY205dEtTNWxlR2x6ZEhNZ0ppWWdkR2hwY3k1aVlXeGhibU5sY3lobWNtOXRLUzUyWVd4MVpTNXVZWFJwZG1VZ1BqMGdZVzF2ZFc1MExtNWhkR2wyWlN3Z0owbHVjM1ZtWm1samFXVnVkQ0JpWVd4aGJtTmxKeWtLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ1lYSmpNVFF4TUY5eVpXUmxaVzFmWW5sZmNHRnlkR2wwYVc5dVgySnZiMnhmWm1Gc2MyVkFNd29nSUNBZ1puSmhiV1ZmWkdsbklESUtJQ0FnSUdKdmVGOW5aWFFLSUNBZ0lHRnpjMlZ5ZENBdkx5QkNiM2dnYlhWemRDQm9ZWFpsSUhaaGJIVmxDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdJK1BRb2dJQ0FnWW5vZ1lYSmpNVFF4TUY5eVpXUmxaVzFmWW5sZmNHRnlkR2wwYVc5dVgySnZiMnhmWm1Gc2MyVkFNd29nSUNBZ2FXNTBZMTh4SUM4dklERUtDbUZ5WXpFME1UQmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZibDlpYjI5c1gyMWxjbWRsUURRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qUXhOd29nSUNBZ0x5OGdZWE56WlhKMEtIUm9hWE11WW1Gc1lXNWpaWE1vWm5KdmJTa3VaWGhwYzNSeklDWW1JSFJvYVhNdVltRnNZVzVqWlhNb1puSnZiU2t1ZG1Gc2RXVXVibUYwYVhabElENDlJR0Z0YjNWdWRDNXVZWFJwZG1Vc0lDZEpibk4xWm1acFkybGxiblFnWW1Gc1lXNWpaU2NwQ2lBZ0lDQmhjM05sY25RZ0x5OGdTVzV6ZFdabWFXTnBaVzUwSUdKaGJHRnVZMlVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TkRFNENpQWdJQ0F2THlCMGFHbHpMbUpoYkdGdVkyVnpLR1p5YjIwcExuWmhiSFZsSUQwZ2JtVjNJR0Z5WXpRdVZXbHVkRTR5TlRZb2RHaHBjeTVpWVd4aGJtTmxjeWhtY205dEtTNTJZV3gxWlM1dVlYUnBkbVVnTFNCaGJXOTFiblF1Ym1GMGFYWmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklESUtJQ0FnSUdSMWNBb2dJQ0FnWW05NFgyZGxkQW9nSUNBZ1lYTnpaWEowSUM4dklFSnZlQ0J0ZFhOMElHaGhkbVVnZG1Gc2RXVUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWWkwS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFnTHk4Z2IzWmxjbVpzYjNjS0lDQWdJR1p5WVcxbFgyUnBaeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHTnZkbVZ5SURNS0lDQWdJR0o4Q2lBZ0lDQmliM2hmY0hWMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5URUtJQ0FnSUM4dklIQjFZbXhwWXlCMGIzUmhiRk4xY0hCc2VTQTlJRWRzYjJKaGJGTjBZWFJsUEZWcGJuUk9NalUyUGloN0lHdGxlVG9nSjNRbklIMHBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTmZNeUF2THlBaWRDSUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ1IyeHZZbUZzVTNSaGRHVWdaWGhwYzNSekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qUXhPUW9nSUNBZ0x5OGdkR2hwY3k1MGIzUmhiRk4xY0hCc2VTNTJZV3gxWlNBOUlHNWxkeUJoY21NMExsVnBiblJPTWpVMktIUm9hWE11ZEc5MFlXeFRkWEJ3YkhrdWRtRnNkV1V1Ym1GMGFYWmxJQzBnWVcxdmRXNTBMbTVoZEdsMlpTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWWkwS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFnTHk4Z2IzWmxjbVpzYjNjS0lDQWdJR0o4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TlRFS0lDQWdJQzh2SUhCMVlteHBZeUIwYjNSaGJGTjFjSEJzZVNBOUlFZHNiMkpoYkZOMFlYUmxQRlZwYm5ST01qVTJQaWg3SUd0bGVUb2dKM1FuSUgwcENpQWdJQ0JpZVhSbFkxOHpJQzh2SUNKMElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzBNVGtLSUNBZ0lDOHZJSFJvYVhNdWRHOTBZV3hUZFhCd2JIa3VkbUZzZFdVZ1BTQnVaWGNnWVhKak5DNVZhVzUwVGpJMU5paDBhR2x6TG5SdmRHRnNVM1Z3Y0d4NUxuWmhiSFZsTG01aGRHbDJaU0F0SUdGdGIzVnVkQzV1WVhScGRtVXBDaUFnSUNCemQyRndDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTWpBS0lDQWdJQzh2SUdWdGFYUW9KMUpsWkdWbGJTY3NJRzVsZHlCaGNtTXhOREV3WDNCaGNuUnBkR2x2Ymw5eVpXUmxaVzBvZXlCbWNtOXRMQ0J3WVhKMGFYUnBiMjRzSUdGdGIzVnVkQ3dnWkdGMFlTQjlLU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXdDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1lubDBaV01nTWpjZ0x5OGdNSGd3TURZeUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCaWVYUmxZeUExSUM4dklEQjRNREF3TWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JpZVhSbFl5QXpNQ0F2THlCdFpYUm9iMlFnSWxKbFpHVmxiU2dvWVdSa2NtVnpjeXhoWkdSeVpYTnpMSFZwYm5ReU5UWXNZbmwwWlZ0ZEtTa2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2NtVjBjM1ZpQ2dwaGNtTXhOREV3WDNKbFpHVmxiVjlpZVY5d1lYSjBhWFJwYjI1ZlltOXZiRjltWVd4elpVQXpPZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdJZ1lYSmpNVFF4TUY5eVpXUmxaVzFmWW5sZmNHRnlkR2wwYVc5dVgySnZiMnhmYldWeVoyVkFOQW9LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZPa0Z5WXpFME1UQXVZWEpqTVRReE1GOXZjR1Z5WVhSdmNsOXlaV1JsWlcxZllubGZjR0Z5ZEdsMGFXOXVLR1p5YjIwNklHSjVkR1Z6TENCd1lYSjBhWFJwYjI0NklHSjVkR1Z6TENCaGJXOTFiblE2SUdKNWRHVnpMQ0JrWVhSaE9pQmllWFJsY3lrZ0xUNGdkbTlwWkRvS1lYSmpNVFF4TUY5dmNHVnlZWFJ2Y2w5eVpXUmxaVzFmWW5sZmNHRnlkR2wwYVc5dU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzBNak10TkRJNUNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpFME1UQmZiM0JsY21GMGIzSmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZiaWdLSUNBZ0lDOHZJQ0FnWm5KdmJUb2dZWEpqTkM1QlpHUnlaWE56TEFvZ0lDQWdMeThnSUNCd1lYSjBhWFJwYjI0NklHRnlZelF1UVdSa2NtVnpjeXdLSUNBZ0lDOHZJQ0FnWVcxdmRXNTBPaUJoY21NMExsVnBiblJPTWpVMkxBb2dJQ0FnTHk4Z0lDQmtZWFJoT2lCaGNtTTBMa1I1Ym1GdGFXTkNlWFJsY3l3S0lDQWdJQzh2SUNrNklIWnZhV1FnZXdvZ0lDQWdjSEp2ZEc4Z05DQXdDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWkhWd2JpQXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPalF6TUFvZ0lDQWdMeThnWTI5dWMzUWdjMlZ1WkdWeUlEMGdibVYzSUdGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLUW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzBNeklLSUNBZ0lDOHZJR3hsZENCaGRYUm9iM0pwZW1Wa0lEMGdkR2hwY3k1aGNtTXhOREV3WDJselgyOXdaWEpoZEc5eUtHWnliMjBzSUhObGJtUmxjaXdnY0dGeWRHbDBhVzl1S1M1dVlYUnBkbVVnUFQwOUlIUnlkV1VLSUNBZ0lHWnlZVzFsWDJScFp5QXROQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTXpBS0lDQWdJQzh2SUdOdmJuTjBJSE5sYm1SbGNpQTlJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9WSGh1TG5ObGJtUmxjaWtLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZORE15Q2lBZ0lDQXZMeUJzWlhRZ1lYVjBhRzl5YVhwbFpDQTlJSFJvYVhNdVlYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNpaG1jbTl0TENCelpXNWtaWElzSUhCaGNuUnBkR2x2YmlrdWJtRjBhWFpsSUQwOVBTQjBjblZsQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVE1LSUNBZ0lHTmhiR3h6ZFdJZ1lYSmpNVFF4TUY5cGMxOXZjR1Z5WVhSdmNnb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR2RsZEdKcGRBb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmtkWEJ1SURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5ETXpDaUFnSUNBdkx5QnBaaUFvSVdGMWRHaHZjbWw2WldRcElIc0tJQ0FnSUdKdWVpQmhjbU14TkRFd1gyOXdaWEpoZEc5eVgzSmxaR1ZsYlY5aWVWOXdZWEowYVhScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qUXpOQW9nSUNBZ0x5OGdZMjl1YzNRZ2NFdGxlU0E5SUc1bGR5QmhjbU14TkRFd1gwOXdaWEpoZEc5eVVHOXlkR2x2Ymt0bGVTaDdJR2h2YkdSbGNqb2dabkp2YlN3Z2IzQmxjbUYwYjNJNklITmxibVJsY2l3Z2NHRnlkR2wwYVc5dUlIMHBDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUUUtJQ0FnSUdaeVlXMWxYMlJwWnlBMENpQWdJQ0JqYjI1allYUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPall6Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdiM0JsY21GMGIzSlFiM0owYVc5dVFXeHNiM2RoYm1ObGN5QTlJRUp2ZUUxaGNEeGhjbU14TkRFd1gwOXdaWEpoZEc5eVVHOXlkR2x2Ymt0bGVTd2dZWEpqTkM1VmFXNTBUakkxTmo0b2V5QnJaWGxRY21WbWFYZzZJQ2RoY21NeE5ERXdYMjl3WVNjZ2ZTa0tJQ0FnSUdKNWRHVmpJREUwSUM4dklDSmhjbU14TkRFd1gyOXdZU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQm1jbUZ0WlY5aWRYSjVJRE1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TkRNMUNpQWdJQ0F2THlCcFppQW9kR2hwY3k1dmNHVnlZWFJ2Y2xCdmNuUnBiMjVCYkd4dmQyRnVZMlZ6S0hCTFpYa3BMbVY0YVhOMGN5a2dld29nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JpZWlCaGNtTXhOREV3WDI5d1pYSmhkRzl5WDNKbFpHVmxiVjlpZVY5d1lYSjBhWFJwYjI1ZllXWjBaWEpmYVdaZlpXeHpaVUF6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pRek5nb2dJQ0FnTHk4Z1kyOXVjM1FnY21WdFlXbHVhVzVuSUQwZ2RHaHBjeTV2Y0dWeVlYUnZjbEJ2Y25ScGIyNUJiR3h2ZDJGdVkyVnpLSEJMWlhrcExuWmhiSFZsQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dNd29nSUNBZ1pIVndDaUFnSUNCaWIzaGZaMlYwQ2lBZ0lDQmhjM05sY25RZ0x5OGdRbTk0SUcxMWMzUWdhR0YyWlNCMllXeDFaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTXpjS0lDQWdJQzh2SUdGemMyVnlkQ2h5WlcxaGFXNXBibWN1Ym1GMGFYWmxJRDQ5SUdGdGIzVnVkQzV1WVhScGRtVXNJQ2RRYjNKMGFXOXVJR0ZzYkc5M1lXNWpaU0JsZUdObFpXUmxaQ2NwQ2lBZ0lDQmtkWEFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNZ29nSUNBZ1lqNDlDaUFnSUNCaGMzTmxjblFnTHk4Z1VHOXlkR2x2YmlCaGJHeHZkMkZ1WTJVZ1pYaGpaV1ZrWldRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5ETTRDaUFnSUNBdkx5QmhkWFJvYjNKcGVtVmtJRDBnZEhKMVpRb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJR1p5WVcxbFgySjFjbmtnTlFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvME16a0tJQ0FnSUM4dklIUm9hWE11YjNCbGNtRjBiM0pRYjNKMGFXOXVRV3hzYjNkaGJtTmxjeWh3UzJWNUtTNTJZV3gxWlNBOUlHNWxkeUJoY21NMExsVnBiblJPTWpVMktISmxiV0ZwYm1sdVp5NXVZWFJwZG1VZ0xTQmhiVzkxYm5RdWJtRjBhWFpsS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmlMUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNaUF2THlBek1nb2dJQ0FnUEQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJ2ZG1WeVpteHZkd29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNCaWVtVnlid29nSUNBZ1lud0tJQ0FnSUdKdmVGOXdkWFFLQ21GeVl6RTBNVEJmYjNCbGNtRjBiM0pmY21Wa1pXVnRYMko1WDNCaGNuUnBkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRRE02Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dOUW9nSUNBZ1puSmhiV1ZmWW5WeWVTQTJDZ3BoY21NeE5ERXdYMjl3WlhKaGRHOXlYM0psWkdWbGJWOWllVjl3WVhKMGFYUnBiMjVmWVdaMFpYSmZhV1pmWld4elpVQTBPZ29nSUNBZ1puSmhiV1ZmWkdsbklEWUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeE5ERXdMbUZzWjI4dWRITTZORFF5Q2lBZ0lDQXZMeUJoYzNObGNuUW9ZWFYwYUc5eWFYcGxaQ3dnSjA1dmRDQmhkWFJvYjNKcGVtVmtJRzl3WlhKaGRHOXlKeWtLSUNBZ0lHRnpjMlZ5ZENBdkx5Qk9iM1FnWVhWMGFHOXlhWHBsWkNCdmNHVnlZWFJ2Y2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekUwTVRBdVlXeG5ieTUwY3pvME5EUUtJQ0FnSUM4dklHTnZibk4wSUdaeWIyMUxaWGtnUFNCdVpYY2dZWEpqTVRReE1GOVFZWEowYVhScGIyNUxaWGtvZXlCb2IyeGtaWEk2SUdaeWIyMHNJSEJoY25ScGRHbHZiaUI5S1FvZ0lDQWdabkpoYldWZlpHbG5JQzAwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVE1LSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQm1jbUZ0WlY5aWRYSjVJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TlRjS0lDQWdJQzh2SUhCMVlteHBZeUJ3WVhKMGFYUnBiMjV6SUQwZ1FtOTRUV0Z3UEdGeVl6RTBNVEJmVUdGeWRHbDBhVzl1UzJWNUxDQmhjbU0wTGxWcGJuUk9NalUyUGloN0lHdGxlVkJ5WldacGVEb2dKMkZ5WXpFME1UQmZjQ2NnZlNrS0lDQWdJR0o1ZEdWaklEZ2dMeThnSW1GeVl6RTBNVEJmY0NJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpFME1UQXVZV3huYnk1MGN6bzBORFVLSUNBZ0lDOHZJR0Z6YzJWeWRDaDBhR2x6TG5CaGNuUnBkR2x2Ym5Nb1puSnZiVXRsZVNrdVpYaHBjM1J6TENBblVHRnlkR2wwYVc5dUlHSmhiR0Z1WTJVZ2JXbHpjMmx1WnljcENpQWdJQ0JrZFhBS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1lYTnpaWEowSUM4dklGQmhjblJwZEdsdmJpQmlZV3hoYm1ObElHMXBjM05wYm1jS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5EUTJDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTV3WVhKMGFYUnBiMjV6S0daeWIyMUxaWGtwTG5aaGJIVmxMbTVoZEdsMlpTQStQU0JoYlc5MWJuUXVibUYwYVhabExDQW5TVzV6ZFdabWFXTnBaVzUwSUhCaGNuUnBkR2x2YmlCaVlXeGhibU5sSnlrS0lDQWdJR1IxY0FvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnWVhOelpYSjBJQzh2SUVKdmVDQnRkWE4wSUdoaGRtVWdkbUZzZFdVS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZajQ5Q2lBZ0lDQmhjM05sY25RZ0x5OGdTVzV6ZFdabWFXTnBaVzUwSUhCaGNuUnBkR2x2YmlCaVlXeGhibU5sQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNVFF4TUM1aGJHZHZMblJ6T2pRME53b2dJQ0FnTHk4Z2RHaHBjeTV3WVhKMGFYUnBiMjV6S0daeWIyMUxaWGtwTG5aaGJIVmxJRDBnYm1WM0lHRnlZelF1VldsdWRFNHlOVFlvZEdocGN5NXdZWEowYVhScGIyNXpLR1p5YjIxTFpYa3BMblpoYkhWbExtNWhkR2wyWlNBdElHRnRiM1Z1ZEM1dVlYUnBkbVVwQ2lBZ0lDQmtkWEFLSUNBZ0lHSnZlRjluWlhRS0lDQWdJR0Z6YzJWeWRDQXZMeUJDYjNnZ2JYVnpkQ0JvWVhabElIWmhiSFZsQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHSXRDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURNeUNpQWdJQ0E4UFFvZ0lDQWdZWE56WlhKMElDOHZJRzkyWlhKbWJHOTNDaUFnSUNCcGJuUmpYeklnTHk4Z016SUtJQ0FnSUdKNlpYSnZDaUFnSUNCa2RYQUtJQ0FnSUdaeVlXMWxYMkoxY25rZ01Bb2dJQ0FnWW53S0lDQWdJR0p2ZUY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzFNd29nSUNBZ0x5OGdjSFZpYkdsaklHSmhiR0Z1WTJWeklEMGdRbTk0VFdGd1BFRmtaSEpsYzNNc0lGVnBiblJPTWpVMlBpaDdJR3RsZVZCeVpXWnBlRG9nSjJJbklIMHBDaUFnSUNCaWVYUmxZeUEwSUM4dklDSmlJZ29nSUNBZ1puSmhiV1ZmWkdsbklDMDBDaUFnSUNCamIyNWpZWFFLSUNBZ0lHUjFjQW9nSUNBZ1puSmhiV1ZmWW5WeWVTQXlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1UUXhNQzVoYkdkdkxuUnpPalEwT0FvZ0lDQWdMeThnWVhOelpYSjBLSFJvYVhNdVltRnNZVzVqWlhNb1puSnZiU2t1WlhocGMzUnpJQ1ltSUhSb2FYTXVZbUZzWVc1alpYTW9abkp2YlNrdWRtRnNkV1V1Ym1GMGFYWmxJRDQ5SUdGdGIzVnVkQzV1WVhScGRtVXNJQ2RKYm5OMVptWnBZMmxsYm5RZ1ltRnNZVzVqWlNjcENpQWdJQ0JpYjNoZmJHVnVDaUFnSUNCaWRYSjVJREVLSUNBZ0lHSjZJR0Z5WXpFME1UQmZiM0JsY21GMGIzSmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZibDlpYjI5c1gyWmhiSE5sUURjS0lDQWdJR1p5WVcxbFgyUnBaeUF5Q2lBZ0lDQmliM2hmWjJWMENpQWdJQ0JoYzNObGNuUWdMeThnUW05NElHMTFjM1FnYUdGMlpTQjJZV3gxWlFvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmlQajBLSUNBZ0lHSjZJR0Z5WXpFME1UQmZiM0JsY21GMGIzSmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZibDlpYjI5c1gyWmhiSE5sUURjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2dwaGNtTXhOREV3WDI5d1pYSmhkRzl5WDNKbFpHVmxiVjlpZVY5d1lYSjBhWFJwYjI1ZlltOXZiRjl0WlhKblpVQTRPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTkRnS0lDQWdJQzh2SUdGemMyVnlkQ2gwYUdsekxtSmhiR0Z1WTJWektHWnliMjBwTG1WNGFYTjBjeUFtSmlCMGFHbHpMbUpoYkdGdVkyVnpLR1p5YjIwcExuWmhiSFZsTG01aGRHbDJaU0ErUFNCaGJXOTFiblF1Ym1GMGFYWmxMQ0FuU1c1emRXWm1hV05wWlc1MElHSmhiR0Z1WTJVbktRb2dJQ0FnWVhOelpYSjBJQzh2SUVsdWMzVm1abWxqYVdWdWRDQmlZV3hoYm1ObENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTVRReE1DNWhiR2R2TG5Sek9qUTBPUW9nSUNBZ0x5OGdkR2hwY3k1aVlXeGhibU5sY3lobWNtOXRLUzUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMbFZwYm5ST01qVTJLSFJvYVhNdVltRnNZVzVqWlhNb1puSnZiU2t1ZG1Gc2RXVXVibUYwYVhabElDMGdZVzF2ZFc1MExtNWhkR2wyWlNrS0lDQWdJR1p5WVcxbFgyUnBaeUF5Q2lBZ0lDQmtkWEFLSUNBZ0lHSnZlRjluWlhRS0lDQWdJR0Z6YzJWeWRDQXZMeUJDYjNnZ2JYVnpkQ0JvWVhabElIWmhiSFZsQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHSXRDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURNeUNpQWdJQ0E4UFFvZ0lDQWdZWE56WlhKMElDOHZJRzkyWlhKbWJHOTNDaUFnSUNCbWNtRnRaVjlrYVdjZ01Bb2dJQ0FnWkhWd0NpQWdJQ0JqYjNabGNpQXpDaUFnSUNCaWZBb2dJQ0FnWW05NFgzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPalV4Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdkRzkwWVd4VGRYQndiSGtnUFNCSGJHOWlZV3hUZEdGMFpUeFZhVzUwVGpJMU5qNG9leUJyWlhrNklDZDBKeUI5S1FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqWHpNZ0x5OGdJblFpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6RTBNVEF1WVd4bmJ5NTBjem8wTlRBS0lDQWdJQzh2SUhSb2FYTXVkRzkwWVd4VGRYQndiSGt1ZG1Gc2RXVWdQU0J1WlhjZ1lYSmpOQzVWYVc1MFRqSTFOaWgwYUdsekxuUnZkR0ZzVTNWd2NHeDVMblpoYkhWbExtNWhkR2wyWlNBdElHRnRiM1Z1ZEM1dVlYUnBkbVVwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHSXRDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURNeUNpQWdJQ0E4UFFvZ0lDQWdZWE56WlhKMElDOHZJRzkyWlhKbWJHOTNDaUFnSUNCaWZBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qVXhDaUFnSUNBdkx5QndkV0pzYVdNZ2RHOTBZV3hUZFhCd2JIa2dQU0JIYkc5aVlXeFRkR0YwWlR4VmFXNTBUakkxTmo0b2V5QnJaWGs2SUNkMEp5QjlLUW9nSUNBZ1lubDBaV05mTXlBdkx5QWlkQ0lLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXhOREV3TG1Gc1oyOHVkSE02TkRVd0NpQWdJQ0F2THlCMGFHbHpMblJ2ZEdGc1UzVndjR3g1TG5aaGJIVmxJRDBnYm1WM0lHRnlZelF1VldsdWRFNHlOVFlvZEdocGN5NTBiM1JoYkZOMWNIQnNlUzUyWVd4MVpTNXVZWFJwZG1VZ0xTQmhiVzkxYm5RdWJtRjBhWFpsS1FvZ0lDQWdjM2RoY0FvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU14TkRFd0xtRnNaMjh1ZEhNNk5EVXhDaUFnSUNBdkx5QmxiV2wwS0NkU1pXUmxaVzBuTENCdVpYY2dZWEpqTVRReE1GOXdZWEowYVhScGIyNWZjbVZrWldWdEtIc2dabkp2YlN3Z2NHRnlkR2wwYVc5dUxDQmhiVzkxYm5Rc0lHUmhkR0VnZlNrcENpQWdJQ0JtY21GdFpWOWthV2NnTVFvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR0o1ZEdWaklESTNJQzh2SURCNE1EQTJNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHTnZibU5oZEFvZ0lDQWdZbmwwWldNZ05TQXZMeUF3ZURBd01ESUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1lubDBaV01nTXpBZ0x5OGdiV1YwYUc5a0lDSlNaV1JsWlcwb0tHRmtaSEpsYzNNc1lXUmtjbVZ6Y3l4MWFXNTBNalUyTEdKNWRHVmJYU2twSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJSEpsZEhOMVlnb0tZWEpqTVRReE1GOXZjR1Z5WVhSdmNsOXlaV1JsWlcxZllubGZjR0Z5ZEdsMGFXOXVYMkp2YjJ4ZlptRnNjMlZBTnpvS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmlJR0Z5WXpFME1UQmZiM0JsY21GMGIzSmZjbVZrWldWdFgySjVYM0JoY25ScGRHbHZibDlpYjI5c1gyMWxjbWRsUURnS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02T2tGeVl6SXdNQzVpYjI5MGMzUnlZWEFvYm1GdFpUb2dZbmwwWlhNc0lITjViV0p2YkRvZ1lubDBaWE1zSUdSbFkybHRZV3h6T2lCaWVYUmxjeXdnZEc5MFlXeFRkWEJ3YkhrNklHSjVkR1Z6S1NBdFBpQmllWFJsY3pvS1ltOXZkSE4wY21Gd09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qVTJMVFUzQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ0x5OGdjSFZpYkdsaklHSnZiM1J6ZEhKaGNDaHVZVzFsT2lCRWVXNWhiV2xqUW5sMFpYTXNJSE41YldKdmJEb2dSSGx1WVcxcFkwSjVkR1Z6TENCa1pXTnBiV0ZzY3pvZ1ZXbHVkRTQ0TENCMGIzUmhiRk4xY0hCc2VUb2dWV2x1ZEU0eU5UWXBPaUJDYjI5c0lIc0tJQ0FnSUhCeWIzUnZJRFFnTVFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pVNENpQWdJQ0F2THlCaGMzTmxjblFvVkhodUxuTmxibVJsY2lBOVBUMGdSMnh2WW1Gc0xtTnlaV0YwYjNKQlpHUnlaWE56TENBblQyNXNlU0JrWlhCc2IzbGxjaUJ2WmlCMGFHbHpJSE50WVhKMElHTnZiblJ5WVdOMElHTmhiaUJqWVd4c0lHSnZiM1J6ZEhKaGNDQnRaWFJvYjJRbktRb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdaMnh2WW1Gc0lFTnlaV0YwYjNKQlpHUnlaWE56Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklFOXViSGtnWkdWd2JHOTVaWElnYjJZZ2RHaHBjeUJ6YldGeWRDQmpiMjUwY21GamRDQmpZVzRnWTJGc2JDQmliMjkwYzNSeVlYQWdiV1YwYUc5a0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5Ua0tJQ0FnSUM4dklHRnpjMlZ5ZENodVlXMWxMbTVoZEdsMlpTNXNaVzVuZEdnZ1BpQXdMQ0FuVG1GdFpTQnZaaUIwYUdVZ1lYTnpaWFFnYlhWemRDQmlaU0JzYjI1blpYSWdiM0lnWlhGMVlXd2dkRzhnTVNCamFHRnlZV04wWlhJbktRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMENpQWdJQ0JsZUhSeVlXTjBJRElnTUFvZ0lDQWdiR1Z1Q2lBZ0lDQmtkWEFLSUNBZ0lHRnpjMlZ5ZENBdkx5Qk9ZVzFsSUc5bUlIUm9aU0JoYzNObGRDQnRkWE4wSUdKbElHeHZibWRsY2lCdmNpQmxjWFZoYkNCMGJ5QXhJR05vWVhKaFkzUmxjZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPall3Q2lBZ0lDQXZMeUJoYzNObGNuUW9ibUZ0WlM1dVlYUnBkbVV1YkdWdVozUm9JRHc5SURNeUxDQW5UbUZ0WlNCdlppQjBhR1VnWVhOelpYUWdiWFZ6ZENCaVpTQnphRzl5ZEdWeUlHOXlJR1Z4ZFdGc0lIUnZJRE15SUdOb1lYSmhZM1JsY25NbktRb2dJQ0FnYVc1MFkxOHlJQzh2SURNeUNpQWdJQ0E4UFFvZ0lDQWdZWE56WlhKMElDOHZJRTVoYldVZ2IyWWdkR2hsSUdGemMyVjBJRzExYzNRZ1ltVWdjMmh2Y25SbGNpQnZjaUJsY1hWaGJDQjBieUF6TWlCamFHRnlZV04wWlhKekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5qRUtJQ0FnSUM4dklHRnpjMlZ5ZENoemVXMWliMnd1Ym1GMGFYWmxMbXhsYm1kMGFDQStJREFzSUNkVGVXMWliMndnYjJZZ2RHaGxJR0Z6YzJWMElHMTFjM1FnWW1VZ2JHOXVaMlZ5SUc5eUlHVnhkV0ZzSUhSdklERWdZMmhoY21GamRHVnlKeWtLSUNBZ0lHWnlZVzFsWDJScFp5QXRNd29nSUNBZ1pYaDBjbUZqZENBeUlEQUtJQ0FnSUd4bGJnb2dJQ0FnWkhWd0NpQWdJQ0JoYzNObGNuUWdMeThnVTNsdFltOXNJRzltSUhSb1pTQmhjM05sZENCdGRYTjBJR0psSUd4dmJtZGxjaUJ2Y2lCbGNYVmhiQ0IwYnlBeElHTm9ZWEpoWTNSbGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qWXlDaUFnSUNBdkx5QmhjM05sY25Rb2MzbHRZbTlzTG01aGRHbDJaUzVzWlc1bmRHZ2dQRDBnT0N3Z0oxTjViV0p2YkNCdlppQjBhR1VnWVhOelpYUWdiWFZ6ZENCaVpTQnphRzl5ZEdWeUlHOXlJR1Z4ZFdGc0lIUnZJRGdnWTJoaGNtRmpkR1Z5Y3ljcENpQWdJQ0J3ZFhOb2FXNTBJRGdnTHk4Z09Bb2dJQ0FnUEQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJUZVcxaWIyd2diMllnZEdobElHRnpjMlYwSUcxMWMzUWdZbVVnYzJodmNuUmxjaUJ2Y2lCbGNYVmhiQ0IwYnlBNElHTm9ZWEpoWTNSbGNuTUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzFNUW9nSUNBZ0x5OGdjSFZpYkdsaklIUnZkR0ZzVTNWd2NHeDVJRDBnUjJ4dlltRnNVM1JoZEdVOFZXbHVkRTR5TlRZK0tIc2dhMlY1T2lBbmRDY2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh6SUM4dklDSjBJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPall6Q2lBZ0lDQXZMeUJoYzNObGNuUW9JWFJvYVhNdWRHOTBZV3hUZFhCd2JIa3VhR0Z6Vm1Gc2RXVXNJQ2RVYUdseklHMWxkR2h2WkNCallXNGdZbVVnWTJGc2JHVmtJRzl1YkhrZ2IyNWpaU2NwQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWW5WeWVTQXhDaUFnSUNBaENpQWdJQ0JoYzNObGNuUWdMeThnVkdocGN5QnRaWFJvYjJRZ1kyRnVJR0psSUdOaGJHeGxaQ0J2Ym14NUlHOXVZMlVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem96T1FvZ0lDQWdMeThnY0hWaWJHbGpJRzVoYldVZ1BTQkhiRzlpWVd4VGRHRjBaVHhFZVc1aGJXbGpRbmwwWlhNK0tIc2dhMlY1T2lBbmJpY2dmU2tLSUNBZ0lIQjFjMmhpZVhSbGN5QWliaUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem8yTlFvZ0lDQWdMeThnZEdocGN5NXVZVzFsTG5aaGJIVmxJRDBnYm1GdFpRb2dJQ0FnWm5KaGJXVmZaR2xuSUMwMENpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qUXpDaUFnSUNBdkx5QndkV0pzYVdNZ2MzbHRZbTlzSUQwZ1IyeHZZbUZzVTNSaGRHVThSSGx1WVcxcFkwSjVkR1Z6UGloN0lHdGxlVG9nSjNNbklIMHBDaUFnSUNCd2RYTm9ZbmwwWlhNZ0luTWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZOallLSUNBZ0lDOHZJSFJvYVhNdWMzbHRZbTlzTG5aaGJIVmxJRDBnYzNsdFltOXNDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUTUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZOVEVLSUNBZ0lDOHZJSEIxWW14cFl5QjBiM1JoYkZOMWNIQnNlU0E5SUVkc2IySmhiRk4wWVhSbFBGVnBiblJPTWpVMlBpaDdJR3RsZVRvZ0ozUW5JSDBwQ2lBZ0lDQmllWFJsWTE4eklDOHZJQ0owSWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pZM0NpQWdJQ0F2THlCMGFHbHpMblJ2ZEdGc1UzVndjR3g1TG5aaGJIVmxJRDBnZEc5MFlXeFRkWEJ3YkhrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pvME53b2dJQ0FnTHk4Z2NIVmliR2xqSUdSbFkybHRZV3h6SUQwZ1IyeHZZbUZzVTNSaGRHVThWV2x1ZEU0NFBpaDdJR3RsZVRvZ0oyUW5JSDBwQ2lBZ0lDQndkWE5vWW5sMFpYTWdJbVFpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TmpnS0lDQWdJQzh2SUhSb2FYTXVaR1ZqYVcxaGJITXVkbUZzZFdVZ1BTQmtaV05wYldGc2N3b2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qWTVDaUFnSUNBdkx5QmpiMjV6ZENCelpXNWtaWElnUFNCdVpYY2dRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLUW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qVXpDaUFnSUNBdkx5QndkV0pzYVdNZ1ltRnNZVzVqWlhNZ1BTQkNiM2hOWVhBOFFXUmtjbVZ6Y3l3Z1ZXbHVkRTR5TlRZK0tIc2dhMlY1VUhKbFptbDRPaUFuWWljZ2ZTa0tJQ0FnSUdKNWRHVmpJRFFnTHk4Z0ltSWlDaUFnSUNCa2FXY2dNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TnpFS0lDQWdJQzh2SUhSb2FYTXVZbUZzWVc1alpYTW9jMlZ1WkdWeUtTNTJZV3gxWlNBOUlIUnZkR0ZzVTNWd2NHeDVDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdKdmVGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem8zTXdvZ0lDQWdMeThnWlcxcGRDaHVaWGNnWVhKak1qQXdYMVJ5WVc1elptVnlLSHNnWm5KdmJUb2dibVYzSUVGa1pISmxjM01vUjJ4dlltRnNMbnBsY205QlpHUnlaWE56S1N3Z2RHODZJSE5sYm1SbGNpd2dkbUZzZFdVNklIUnZkR0ZzVTNWd2NHeDVJSDBwS1FvZ0lDQWdaMnh2WW1Gc0lGcGxjbTlCWkdSeVpYTnpDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmllWFJsWXlBek1TQXZMeUJ0WlhSb2IyUWdJbUZ5WXpJd01GOVVjbUZ1YzJabGNpaGhaR1J5WlhOekxHRmtaSEpsYzNNc2RXbHVkREkxTmlraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qYzBDaUFnSUNBdkx5QnlaWFIxY200Z2JtVjNJRUp2YjJ3b2RISjFaU2tLSUNBZ0lHSjVkR1ZqSURjZ0x5OGdNSGc0TUFvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pwQmNtTXlNREF1WVhKak1qQXdYMjVoYldVb0tTQXRQaUJpZVhSbGN6b0tZWEpqTWpBd1gyNWhiV1U2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TXprS0lDQWdJQzh2SUhCMVlteHBZeUJ1WVcxbElEMGdSMnh2WW1Gc1UzUmhkR1U4UkhsdVlXMXBZMEo1ZEdWelBpaDdJR3RsZVRvZ0oyNG5JSDBwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2NIVnphR0o1ZEdWeklDSnVJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzROQW9nSUNBZ0x5OGdjbVYwZFhKdUlHNWxkeUJUZEdGMGFXTkNlWFJsY3p3ek1qNG9kR2hwY3k1dVlXMWxMblpoYkhWbExtNWhkR2wyWlNrS0lDQWdJR1Y0ZEhKaFkzUWdNaUF3Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYzJsNlpRb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qcEJjbU15TURBdVlYSmpNakF3WDNONWJXSnZiQ2dwSUMwK0lHSjVkR1Z6T2dwaGNtTXlNREJmYzNsdFltOXNPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPalF6Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdjM2x0WW05c0lEMGdSMnh2WW1Gc1UzUmhkR1U4UkhsdVlXMXBZMEo1ZEdWelBpaDdJR3RsZVRvZ0ozTW5JSDBwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2NIVnphR0o1ZEdWeklDSnpJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzVOQW9nSUNBZ0x5OGdjbVYwZFhKdUlHNWxkeUJUZEdGMGFXTkNlWFJsY3p3NFBpaDBhR2x6TG5ONWJXSnZiQzUyWVd4MVpTNXVZWFJwZG1VcENpQWdJQ0JsZUhSeVlXTjBJRElnTUFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lIQjFjMmhwYm5RZ09DQXZMeUE0Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2MybDZaUW9nSUNBZ2NtVjBjM1ZpQ2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPanBCY21NeU1EQXVZWEpqTWpBd1gyUmxZMmx0WVd4ektDa2dMVDRnWW5sMFpYTTZDbUZ5WXpJd01GOWtaV05wYldGc2N6b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzBOd29nSUNBZ0x5OGdjSFZpYkdsaklHUmxZMmx0WVd4eklEMGdSMnh2WW1Gc1UzUmhkR1U4VldsdWRFNDRQaWg3SUd0bGVUb2dKMlFuSUgwcENpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdjSFZ6YUdKNWRHVnpJQ0prSWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJIYkc5aVlXeFRkR0YwWlNCbGVHbHpkSE1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem94TURRS0lDQWdJQzh2SUhKbGRIVnliaUIwYUdsekxtUmxZMmx0WVd4ekxuWmhiSFZsQ2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02T2tGeVl6SXdNQzVoY21NeU1EQmZkRzkwWVd4VGRYQndiSGtvS1NBdFBpQmllWFJsY3pvS1lYSmpNakF3WDNSdmRHRnNVM1Z3Y0d4NU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qVXhDaUFnSUNBdkx5QndkV0pzYVdNZ2RHOTBZV3hUZFhCd2JIa2dQU0JIYkc5aVlXeFRkR0YwWlR4VmFXNTBUakkxTmo0b2V5QnJaWGs2SUNkMEp5QjlLUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdKNWRHVmpYek1nTHk4Z0luUWlDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRXhOQW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11ZEc5MFlXeFRkWEJ3YkhrdWRtRnNkV1VLSUNBZ0lISmxkSE4xWWdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem82UVhKak1qQXdMbUZ5WXpJd01GOWlZV3hoYm1ObFQyWW9iM2R1WlhJNklHSjVkR1Z6S1NBdFBpQmllWFJsY3pvS1lYSmpNakF3WDJKaGJHRnVZMlZQWmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pveE1qTXRNVEkwQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9leUJ5WldGa2IyNXNlVG9nZEhKMVpTQjlLUW9nSUNBZ0x5OGdjSFZpYkdsaklHRnlZekl3TUY5aVlXeGhibU5sVDJZb2IzZHVaWEk2SUVGa1pISmxjM01wT2lCaGNtTTBMbFZwYm5ST01qVTJJSHNLSUNBZ0lIQnliM1J2SURFZ01Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRXlOUW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11WDJKaGJHRnVZMlZQWmlodmQyNWxjaWtLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1kyRnNiSE4xWWlCZlltRnNZVzVqWlU5bUNpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9rRnlZekl3TUM1aGNtTXlNREJmZEhKaGJuTm1aWEpHY205dEtHWnliMjA2SUdKNWRHVnpMQ0IwYnpvZ1lubDBaWE1zSUhaaGJIVmxPaUJpZVhSbGN5a2dMVDRnWW5sMFpYTTZDbUZ5WXpJd01GOTBjbUZ1YzJabGNrWnliMjA2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TVRRNExURTBPUW9nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUM4dklIQjFZbXhwWXlCaGNtTXlNREJmZEhKaGJuTm1aWEpHY205dEtHWnliMjA2SUVGa1pISmxjM01zSUhSdk9pQkJaR1J5WlhOekxDQjJZV3gxWlRvZ1lYSmpOQzVWYVc1MFRqSTFOaWs2SUdGeVl6UXVRbTl2YkNCN0NpQWdJQ0J3Y205MGJ5QXpJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem94TlRBS0lDQWdJQzh2SUdOdmJuTjBJSE53Wlc1a1pYSWdQU0J1WlhjZ1FXUmtjbVZ6Y3loVWVHNHVjMlZ1WkdWeUtRb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFMU1Rb2dJQ0FnTHk4Z1kyOXVjM1FnYzNCbGJtUmxjbDloYkd4dmQyRnVZMlVnUFNCMGFHbHpMbDloYkd4dmQyRnVZMlVvWm5KdmJTd2djM0JsYm1SbGNpa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnWkdsbklERUtJQ0FnSUdOaGJHeHpkV0lnWDJGc2JHOTNZVzVqWlFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFMU1nb2dJQ0FnTHk4Z1lYTnpaWEowS0hOd1pXNWtaWEpmWVd4c2IzZGhibU5sTG01aGRHbDJaU0ErUFNCMllXeDFaUzV1WVhScGRtVXNJQ2RwYm5OMVptWnBZMmxsYm5RZ1lYQndjbTkyWVd3bktRb2dJQ0FnWkhWd0NpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR0krUFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1YzNWbVptbGphV1Z1ZENCaGNIQnliM1poYkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFMU13b2dJQ0FnTHk4Z1kyOXVjM1FnYm1WM1gzTndaVzVrWlhKZllXeHNiM2RoYm1ObElEMGdibVYzSUZWcGJuUk9NalUyS0hOd1pXNWtaWEpmWVd4c2IzZGhibU5sTG01aGRHbDJaU0F0SUhaaGJIVmxMbTVoZEdsMlpTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWWkwS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFnTHk4Z2IzWmxjbVpzYjNjS0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdZbnBsY204S0lDQWdJR0o4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TVRVMENpQWdJQ0F2THlCMGFHbHpMbDloY0hCeWIzWmxLR1p5YjIwc0lITndaVzVrWlhJc0lHNWxkMTl6Y0dWdVpHVnlYMkZzYkc5M1lXNWpaU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXRNd29nSUNBZ1kyOTJaWElnTWdvZ0lDQWdZMkZzYkhOMVlpQmZZWEJ3Y205MlpRb2dJQ0FnY0c5d0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1UVTFDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTVmZEhKaGJuTm1aWElvWm5KdmJTd2dkRzhzSUhaaGJIVmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNCbWNtRnRaVjlrYVdjZ0xUSUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWTJGc2JITjFZaUJmZEhKaGJuTm1aWElLSUNBZ0lISmxkSE4xWWdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem82UVhKak1qQXdMbUZ5WXpJd01GOWhjSEJ5YjNabEtITndaVzVrWlhJNklHSjVkR1Z6TENCMllXeDFaVG9nWW5sMFpYTXBJQzArSUdKNWRHVnpPZ3BoY21NeU1EQmZZWEJ3Y205MlpUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3hOalV0TVRZMkNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvS1FvZ0lDQWdMeThnY0hWaWJHbGpJR0Z5WXpJd01GOWhjSEJ5YjNabEtITndaVzVrWlhJNklFRmtaSEpsYzNNc0lIWmhiSFZsT2lCaGNtTTBMbFZwYm5ST01qVTJLVG9nUW05dmJDQjdDaUFnSUNCd2NtOTBieUF5SURFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pveE5qY0tJQ0FnSUM4dklHTnZibk4wSUc5M2JtVnlJRDBnYm1WM0lFRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa0tJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pveE5qZ0tJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbDloY0hCeWIzWmxLRzkzYm1WeUxDQnpjR1Z1WkdWeUxDQjJZV3gxWlNrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpZV3hzYzNWaUlGOWhjSEJ5YjNabENpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9rRnlZekl3TUM1aGNtTXlNREJmWVd4c2IzZGhibU5sS0c5M2JtVnlPaUJpZVhSbGN5d2djM0JsYm1SbGNqb2dZbmwwWlhNcElDMCtJR0o1ZEdWek9ncGhjbU15TURCZllXeHNiM2RoYm1ObE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTNOeTB4TnpnS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZWEpqTWpBd1gyRnNiRzkzWVc1alpTaHZkMjVsY2pvZ1FXUmtjbVZ6Y3l3Z2MzQmxibVJsY2pvZ1FXUmtjbVZ6Y3lrNklHRnlZelF1VldsdWRFNHlOVFlnZXdvZ0lDQWdjSEp2ZEc4Z01pQXhDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZNVGM1Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdkR2hwY3k1ZllXeHNiM2RoYm1ObEtHOTNibVZ5TENCemNHVnVaR1Z5S1FvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHTmhiR3h6ZFdJZ1gyRnNiRzkzWVc1alpRb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qcEJjbU15TURBdVgySmhiR0Z1WTJWUFppaHZkMjVsY2pvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwZlltRnNZVzVqWlU5bU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTRNZ29nSUNBZ0x5OGdjSEp2ZEdWamRHVmtJRjlpWVd4aGJtTmxUMllvYjNkdVpYSTZJRUZrWkhKbGMzTXBPaUJWYVc1MFRqSTFOaUI3Q2lBZ0lDQndjbTkwYnlBeElERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6bzFNd29nSUNBZ0x5OGdjSFZpYkdsaklHSmhiR0Z1WTJWeklEMGdRbTk0VFdGd1BFRmtaSEpsYzNNc0lGVnBiblJPTWpVMlBpaDdJR3RsZVZCeVpXWnBlRG9nSjJJbklIMHBDaUFnSUNCaWVYUmxZeUEwSUM4dklDSmlJZ29nSUNBZ1puSmhiV1ZmWkdsbklDMHhDaUFnSUNCamIyNWpZWFFLSUNBZ0lHUjFjQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPakU0TXdvZ0lDQWdMeThnYVdZZ0tDRjBhR2x6TG1KaGJHRnVZMlZ6S0c5M2JtVnlLUzVsZUdsemRITXBJSEpsZEhWeWJpQnVaWGNnVldsdWRFNHlOVFlvTUNrS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1ltNTZJRjlpWVd4aGJtTmxUMlpmWVdaMFpYSmZhV1pmWld4elpVQXlDaUFnSUNCaWVYUmxZMTh4SUM4dklEQjRNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNQW9nSUNBZ2MzZGhjQW9nSUNBZ2NtVjBjM1ZpQ2dwZlltRnNZVzVqWlU5bVgyRm1kR1Z5WDJsbVgyVnNjMlZBTWpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pveE9EUUtJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbUpoYkdGdVkyVnpLRzkzYm1WeUtTNTJZV3gxWlFvZ0lDQWdabkpoYldWZlpHbG5JREFLSUNBZ0lHSnZlRjluWlhRS0lDQWdJR0Z6YzJWeWRDQXZMeUJDYjNnZ2JYVnpkQ0JvWVhabElIWmhiSFZsQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02T2tGeVl6SXdNQzVmZEhKaGJuTm1aWElvYzJWdVpHVnlPaUJpZVhSbGN5d2djbVZqYVhCcFpXNTBPaUJpZVhSbGN5d2dZVzF2ZFc1ME9pQmllWFJsY3lrZ0xUNGdZbmwwWlhNNkNsOTBjbUZ1YzJabGNqb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3hPRGNLSUNBZ0lDOHZJSEJ5YjNSbFkzUmxaQ0JmZEhKaGJuTm1aWElvYzJWdVpHVnlPaUJCWkdSeVpYTnpMQ0J5WldOcGNHbGxiblE2SUVGa1pISmxjM01zSUdGdGIzVnVkRG9nVldsdWRFNHlOVFlwT2lCQ2IyOXNJSHNLSUNBZ0lIQnliM1J2SURNZ01Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTRPQW9nSUNBZ0x5OGdZMjl1YzNRZ2MyVnVaR1Z5WDJKaGJHRnVZMlVnUFNCMGFHbHpMbDlpWVd4aGJtTmxUMllvYzJWdVpHVnlLUW9nSUNBZ1puSmhiV1ZmWkdsbklDMHpDaUFnSUNCallXeHNjM1ZpSUY5aVlXeGhibU5sVDJZS0lDQWdJR1IxY0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFNE9Rb2dJQ0FnTHk4Z1kyOXVjM1FnY21WamFYQnBaVzUwWDJKaGJHRnVZMlVnUFNCMGFHbHpMbDlpWVd4aGJtTmxUMllvY21WamFYQnBaVzUwS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmpZV3hzYzNWaUlGOWlZV3hoYm1ObFQyWUtJQ0FnSUhOM1lYQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3hPVEFLSUNBZ0lDOHZJR0Z6YzJWeWRDaHpaVzVrWlhKZlltRnNZVzVqWlM1dVlYUnBkbVVnUGowZ1lXMXZkVzUwTG01aGRHbDJaU3dnSjBsdWMzVm1abWxqYVdWdWRDQmlZV3hoYm1ObElHRjBJSFJvWlNCelpXNWtaWElnWVdOamIzVnVkQ2NwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHSStQUW9nSUNBZ1lYTnpaWEowSUM4dklFbHVjM1ZtWm1samFXVnVkQ0JpWVd4aGJtTmxJR0YwSUhSb1pTQnpaVzVrWlhJZ1lXTmpiM1Z1ZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pFNU1nb2dJQ0FnTHk4Z2FXWWdLSE5sYm1SbGNpQWhQVDBnY21WamFYQnBaVzUwS1NCN0NpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdJVDBLSUNBZ0lHSjZJRjkwY21GdWMyWmxjbDloWm5SbGNsOXBabDlsYkhObFFESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3hPVFFLSUNBZ0lDOHZJSFJvYVhNdVltRnNZVzVqWlhNb2MyVnVaR1Z5S1M1MllXeDFaU0E5SUc1bGR5QlZhVzUwVGpJMU5paHpaVzVrWlhKZlltRnNZVzVqWlM1dVlYUnBkbVVnTFNCaGJXOTFiblF1Ym1GMGFYWmxLUW9nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWWkwS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdNeklLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFnTHk4Z2IzWmxjbVpzYjNjS0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdZbnBsY204S0lDQWdJSE4zWVhBS0lDQWdJR1JwWnlBeENpQWdJQ0JpZkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pVekNpQWdJQ0F2THlCd2RXSnNhV01nWW1Gc1lXNWpaWE1nUFNCQ2IzaE5ZWEE4UVdSa2NtVnpjeXdnVldsdWRFNHlOVFkrS0hzZ2EyVjVVSEpsWm1sNE9pQW5ZaWNnZlNrS0lDQWdJR0o1ZEdWaklEUWdMeThnSW1JaUNpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTVOQW9nSUNBZ0x5OGdkR2hwY3k1aVlXeGhibU5sY3loelpXNWtaWElwTG5aaGJIVmxJRDBnYm1WM0lGVnBiblJPTWpVMktITmxibVJsY2w5aVlXeGhibU5sTG01aGRHbDJaU0F0SUdGdGIzVnVkQzV1WVhScGRtVXBDaUFnSUNCemQyRndDaUFnSUNCaWIzaGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TVRrMUNpQWdJQ0F2THlCMGFHbHpMbUpoYkdGdVkyVnpLSEpsWTJsd2FXVnVkQ2t1ZG1Gc2RXVWdQU0J1WlhjZ1ZXbHVkRTR5TlRZb2NtVmphWEJwWlc1MFgySmhiR0Z1WTJVdWJtRjBhWFpsSUNzZ1lXMXZkVzUwTG01aGRHbDJaU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXhDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdJckNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eUlDOHZJRE15Q2lBZ0lDQThQUW9nSUNBZ1lYTnpaWEowSUM4dklHOTJaWEptYkc5M0NpQWdJQ0JpZkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pVekNpQWdJQ0F2THlCd2RXSnNhV01nWW1Gc1lXNWpaWE1nUFNCQ2IzaE5ZWEE4UVdSa2NtVnpjeXdnVldsdWRFNHlOVFkrS0hzZ2EyVjVVSEpsWm1sNE9pQW5ZaWNnZlNrS0lDQWdJR0o1ZEdWaklEUWdMeThnSW1JaUNpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTVOUW9nSUNBZ0x5OGdkR2hwY3k1aVlXeGhibU5sY3loeVpXTnBjR2xsYm5RcExuWmhiSFZsSUQwZ2JtVjNJRlZwYm5ST01qVTJLSEpsWTJsd2FXVnVkRjlpWVd4aGJtTmxMbTVoZEdsMlpTQXJJR0Z0YjNWdWRDNXVZWFJwZG1VcENpQWdJQ0J6ZDJGd0NpQWdJQ0JpYjNoZmNIVjBDZ3BmZEhKaGJuTm1aWEpmWVdaMFpYSmZhV1pmWld4elpVQXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPakU1TndvZ0lDQWdMeThnWlcxcGRDaHVaWGNnWVhKak1qQXdYMVJ5WVc1elptVnlLSHNnWm5KdmJUb2djMlZ1WkdWeUxDQjBiem9nY21WamFYQnBaVzUwTENCMllXeDFaVG9nWVcxdmRXNTBJSDBwS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB6Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVElLSUNBZ0lHTnZibU5oZEFvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR0o1ZEdWaklETXhJQzh2SUcxbGRHaHZaQ0FpWVhKak1qQXdYMVJ5WVc1elptVnlLR0ZrWkhKbGMzTXNZV1JrY21WemN5eDFhVzUwTWpVMktTSUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZNVGs0Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdibVYzSUVKdmIyd29kSEoxWlNrS0lDQWdJR0o1ZEdWaklEY2dMeThnTUhnNE1Bb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBd0NpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9rRnlZekl3TUM1ZllYQndjbTkyWVd4TFpYa29iM2R1WlhJNklHSjVkR1Z6TENCemNHVnVaR1Z5T2lCaWVYUmxjeWtnTFQ0Z1lubDBaWE02Q2w5aGNIQnliM1poYkV0bGVUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3lNREFLSUNBZ0lDOHZJSEJ5YjNSbFkzUmxaQ0JmWVhCd2NtOTJZV3hMWlhrb2IzZHVaWEk2SUVGa1pISmxjM01zSUhOd1pXNWtaWEk2SUVGa1pISmxjM01wT2lCVGRHRjBhV05DZVhSbGN6d3pNajRnZXdvZ0lDQWdjSEp2ZEc4Z01pQXhDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZNakF4Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdibVYzSUZOMFlYUnBZMEo1ZEdWelBETXlQaWh2Y0M1emFHRXlOVFlvYjNBdVkyOXVZMkYwS0c5M2JtVnlMbUo1ZEdWekxDQnpjR1Z1WkdWeUxtSjVkR1Z6S1NrcENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0J6YUdFeU5UWUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnTXpJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnphWHBsQ2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02T2tGeVl6SXdNQzVmWVd4c2IzZGhibU5sS0c5M2JtVnlPaUJpZVhSbGN5d2djM0JsYm1SbGNqb2dZbmwwWlhNcElDMCtJR0o1ZEdWek9ncGZZV3hzYjNkaGJtTmxPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6SXdNQzVoYkdkdkxuUnpPakl3TkFvZ0lDQWdMeThnY0hKdmRHVmpkR1ZrSUY5aGJHeHZkMkZ1WTJVb2IzZHVaWEk2SUVGa1pISmxjM01zSUhOd1pXNWtaWEk2SUVGa1pISmxjM01wT2lCVmFXNTBUakkxTmlCN0NpQWdJQ0J3Y205MGJ5QXlJREVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem95TURVS0lDQWdJQzh2SUdOdmJuTjBJR3RsZVNBOUlIUm9hWE11WDJGd2NISnZkbUZzUzJWNUtHOTNibVZ5TENCemNHVnVaR1Z5S1FvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHTmhiR3h6ZFdJZ1gyRndjSEp2ZG1Gc1MyVjVDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZOVFVLSUNBZ0lDOHZJSEIxWW14cFl5QmhjSEJ5YjNaaGJITWdQU0JDYjNoTllYQThVM1JoZEdsalFubDBaWE04TXpJK0xDQkJjSEJ5YjNaaGJGTjBjblZqZEQ0b2V5QnJaWGxRY21WbWFYZzZJQ2RoSnlCOUtRb2dJQ0FnY0hWemFHSjVkR1Z6SUNKaElnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCa2RYQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NeU1EQXVZV3huYnk1MGN6b3lNRFlLSUNBZ0lDOHZJR2xtSUNnaGRHaHBjeTVoY0hCeWIzWmhiSE1vYTJWNUtTNWxlR2x6ZEhNcElISmxkSFZ5YmlCdVpYY2dWV2x1ZEU0eU5UWW9NQ2tLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW01NklGOWhiR3h2ZDJGdVkyVmZZV1owWlhKZmFXWmZaV3h6WlVBeUNpQWdJQ0JpZVhSbFkxOHhJQzh2SURCNE1EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01Bb2dJQ0FnYzNkaGNBb2dJQ0FnY21WMGMzVmlDZ3BmWVd4c2IzZGhibU5sWDJGbWRHVnlYMmxtWDJWc2MyVkFNam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem95TURjS0lDQWdJQzh2SUhKbGRIVnliaUIwYUdsekxtRndjSEp2ZG1Gc2N5aHJaWGtwTG5aaGJIVmxMbUZ3Y0hKdmRtRnNRVzF2ZFc1MENpQWdJQ0JtY21GdFpWOWthV2NnTUFvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnWVhOelpYSjBJQzh2SUVKdmVDQnRkWE4wSUdoaGRtVWdkbUZzZFdVS0lDQWdJR1Y0ZEhKaFkzUWdNQ0F6TWlBdkx5QnZiaUJsY25KdmNqb2dTVzVrWlhnZ1lXTmpaWE56SUdseklHOTFkQ0J2WmlCaWIzVnVaSE1LSUNBZ0lITjNZWEFLSUNBZ0lISmxkSE4xWWdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem82UVhKak1qQXdMbDloY0hCeWIzWmxLRzkzYm1WeU9pQmllWFJsY3l3Z2MzQmxibVJsY2pvZ1lubDBaWE1zSUdGdGIzVnVkRG9nWW5sMFpYTXBJQzArSUdKNWRHVnpPZ3BmWVhCd2NtOTJaVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTXlNREF1WVd4bmJ5NTBjem95TVRBS0lDQWdJQzh2SUhCeWIzUmxZM1JsWkNCZllYQndjbTkyWlNodmQyNWxjam9nUVdSa2NtVnpjeXdnYzNCbGJtUmxjam9nUVdSa2NtVnpjeXdnWVcxdmRXNTBPaUJWYVc1MFRqSTFOaWs2SUVKdmIyd2dld29nSUNBZ2NISnZkRzhnTXlBeENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1qRXhDaUFnSUNBdkx5QmpiMjV6ZENCclpYa2dQU0IwYUdsekxsOWhjSEJ5YjNaaGJFdGxlU2h2ZDI1bGNpd2djM0JsYm1SbGNpa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JqWVd4c2MzVmlJRjloY0hCeWIzWmhiRXRsZVFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZekl3TUM1aGJHZHZMblJ6T2pJeE1pMHlNVFlLSUNBZ0lDOHZJR052Ym5OMElHRndjSEp2ZG1Gc1FtOTRPaUJCY0hCeWIzWmhiRk4wY25WamRDQTlJRzVsZHlCQmNIQnliM1poYkZOMGNuVmpkQ2g3Q2lBZ0lDQXZMeUFnSUdGd2NISnZkbUZzUVcxdmRXNTBPaUJoYlc5MWJuUXNDaUFnSUNBdkx5QWdJRzkzYm1WeU9pQnZkMjVsY2l3S0lDQWdJQzh2SUNBZ2MzQmxibVJsY2pvZ2MzQmxibVJsY2l3S0lDQWdJQzh2SUgwcENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRJS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpJd01DNWhiR2R2TG5Sek9qVTFDaUFnSUNBdkx5QndkV0pzYVdNZ1lYQndjbTkyWVd4eklEMGdRbTk0VFdGd1BGTjBZWFJwWTBKNWRHVnpQRE15UGl3Z1FYQndjbTkyWVd4VGRISjFZM1ErS0hzZ2EyVjVVSEpsWm1sNE9pQW5ZU2NnZlNrS0lDQWdJSEIxYzJoaWVYUmxjeUFpWVNJS0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpNakF3TG1Gc1oyOHVkSE02TWpFM0NpQWdJQ0F2THlCMGFHbHpMbUZ3Y0hKdmRtRnNjeWhyWlhrcExuWmhiSFZsSUQwZ1lYQndjbTkyWVd4Q2IzZ3VZMjl3ZVNncENpQWdJQ0J6ZDJGd0NpQWdJQ0JpYjNoZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak1qQXdMbUZzWjI4dWRITTZNakU0Q2lBZ0lDQXZMeUJsYldsMEtHNWxkeUJoY21NeU1EQmZRWEJ3Y205MllXd29leUJ2ZDI1bGNqb2diM2R1WlhJc0lITndaVzVrWlhJNklITndaVzVrWlhJc0lIWmhiSFZsT2lCaGJXOTFiblFnZlNrcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR052Ym1OaGRBb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE1UazJPV1k0TmpVZ0x5OGdiV1YwYUc5a0lDSmhjbU15TURCZlFYQndjbTkyWVd3b1lXUmtjbVZ6Y3l4aFpHUnlaWE56TEhWcGJuUXlOVFlwSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU15TURBdVlXeG5ieTUwY3pveU1Ua0tJQ0FnSUM4dklISmxkSFZ5YmlCdVpYY2dRbTl2YkNoMGNuVmxLUW9nSUNBZ1lubDBaV01nTnlBdkx5QXdlRGd3Q2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem82UVhKak9EZ3VYMlZ1YzNWeVpVUmxabUYxYkhSUGQyNWxjaWdwSUMwK0lIWnZhV1E2Q2w5bGJuTjFjbVZFWldaaGRXeDBUM2R1WlhJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pveE9Rb2dJQ0FnTHk4Z2NIVmliR2xqSUdsdWFYUnBZV3hwZW1Wa0lEMGdSMnh2WW1Gc1UzUmhkR1U4WVhKak5DNUNlWFJsUGloN0lHdGxlVG9nSjJGeVl6ZzRYMjlwSnlCOUtTQXZMeUF4SUdsbUlHbHVhWFJwWVd4cGVtVmtJQ2hsZUhCc2FXTnBkQ0J2Y2lCcGJYQnNhV05wZENrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWXlBeE15QXZMeUFpWVhKak9EaGZiMmtpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem95TndvZ0lDQWdMeThnYVdZZ0tDRjBhR2x6TG1sdWFYUnBZV3hwZW1Wa0xtaGhjMVpoYkhWbElIeDhJSFJvYVhNdWFXNXBkR2xoYkdsNlpXUXVkbUZzZFdVdWJtRjBhWFpsSUQwOVBTQXdLU0I3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWW5WeWVTQXhDaUFnSUNCaWVpQmZaVzV6ZFhKbFJHVm1ZWFZzZEU5M2JtVnlYMmxtWDJKdlpIbEFNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZNVGtLSUNBZ0lDOHZJSEIxWW14cFl5QnBibWwwYVdGc2FYcGxaQ0E5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW5sMFpUNG9leUJyWlhrNklDZGhjbU00T0Y5dmFTY2dmU2tnTHk4Z01TQnBaaUJwYm1sMGFXRnNhWHBsWkNBb1pYaHdiR2xqYVhRZ2IzSWdhVzF3YkdsamFYUXBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdNVE1nTHk4Z0ltRnlZemc0WDI5cElnb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pJM0NpQWdJQ0F2THlCcFppQW9JWFJvYVhNdWFXNXBkR2xoYkdsNlpXUXVhR0Z6Vm1Gc2RXVWdmSHdnZEdocGN5NXBibWwwYVdGc2FYcGxaQzUyWVd4MVpTNXVZWFJwZG1VZ1BUMDlJREFwSUhzS0lDQWdJR0owYjJrS0lDQWdJR0p1ZWlCZlpXNXpkWEpsUkdWbVlYVnNkRTkzYm1WeVgyRm1kR1Z5WDJsbVgyVnNjMlZBTlFvS1gyVnVjM1Z5WlVSbFptRjFiSFJQZDI1bGNsOXBabDlpYjJSNVFESTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6b3hOd29nSUNBZ0x5OGdjSFZpYkdsaklHOTNibVZ5SUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1QlpHUnlaWE56UGloN0lHdGxlVG9nSjJGeVl6ZzRYMjhuSUgwcENpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdZbmwwWldOZk1pQXZMeUFpWVhKak9EaGZieUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPakk0Q2lBZ0lDQXZMeUJwWmlBb0lYUm9hWE11YjNkdVpYSXVhR0Z6Vm1Gc2RXVXBJSHNLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JpZFhKNUlERUtJQ0FnSUdKdWVpQmZaVzV6ZFhKbFJHVm1ZWFZzZEU5M2JtVnlYMkZtZEdWeVgybG1YMlZzYzJWQU5Bb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk1UY0tJQ0FnSUM4dklIQjFZbXhwWXlCdmQyNWxjaUE5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UVdSa2NtVnpjejRvZXlCclpYazZJQ2RoY21NNE9GOXZKeUI5S1FvZ0lDQWdZbmwwWldOZk1pQXZMeUFpWVhKak9EaGZieUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPakk1Q2lBZ0lDQXZMeUIwYUdsekxtOTNibVZ5TG5aaGJIVmxJRDBnYm1WM0lHRnlZelF1UVdSa2NtVnpjeWhIYkc5aVlXd3VZM0psWVhSdmNrRmtaSEpsYzNNcENpQWdJQ0JuYkc5aVlXd2dRM0psWVhSdmNrRmtaSEpsYzNNS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmY0hWMENncGZaVzV6ZFhKbFJHVm1ZWFZzZEU5M2JtVnlYMkZtZEdWeVgybG1YMlZzYzJWQU5Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qRTVDaUFnSUNBdkx5QndkV0pzYVdNZ2FXNXBkR2xoYkdsNlpXUWdQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMa0o1ZEdVK0tIc2dhMlY1T2lBbllYSmpPRGhmYjJrbklIMHBJQzh2SURFZ2FXWWdhVzVwZEdsaGJHbDZaV1FnS0dWNGNHeHBZMmwwSUc5eUlHbHRjR3hwWTJsMEtRb2dJQ0FnWW5sMFpXTWdNVE1nTHk4Z0ltRnlZemc0WDI5cElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk16RUtJQ0FnSUM4dklIUm9hWE11YVc1cGRHbGhiR2w2WldRdWRtRnNkV1VnUFNCdVpYY2dZWEpqTkM1Q2VYUmxLREVwQ2lBZ0lDQmllWFJsWXlBeU5TQXZMeUF3ZURBeENpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb0tYMlZ1YzNWeVpVUmxabUYxYkhSUGQyNWxjbDloWm5SbGNsOXBabDlsYkhObFFEVTZDaUFnSUNCeVpYUnpkV0lLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6bzZRWEpqT0RndVlYSmpPRGhmYjNkdVpYSW9LU0F0UGlCaWVYUmxjem9LWVhKak9EaGZiM2R1WlhJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvek53b2dJQ0FnTHk4Z2RHaHBjeTVmWlc1emRYSmxSR1ZtWVhWc2RFOTNibVZ5S0NrS0lDQWdJR05oYkd4emRXSWdYMlZ1YzNWeVpVUmxabUYxYkhSUGQyNWxjZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZNVGNLSUNBZ0lDOHZJSEIxWW14cFl5QnZkMjVsY2lBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFXUmtjbVZ6Y3o0b2V5QnJaWGs2SUNkaGNtTTRPRjl2SnlCOUtRb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWalh6SWdMeThnSW1GeVl6ZzRYMjhpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZNemdLSUNBZ0lDOHZJSEpsZEhWeWJpQjBhR2x6TG05M2JtVnlMblpoYkhWbENpQWdJQ0J5WlhSemRXSUtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvNlFYSmpPRGd1WVhKak9EaGZhWE5mYjNkdVpYSW9jWFZsY25rNklHSjVkR1Z6S1NBdFBpQmllWFJsY3pvS1lYSmpPRGhmYVhOZmIzZHVaWEk2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem8wTVMwME1nb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0hzZ2NtVmhaRzl1YkhrNklIUnlkV1VnZlNrS0lDQWdJQzh2SUhCMVlteHBZeUJoY21NNE9GOXBjMTl2ZDI1bGNpaHhkV1Z5ZVRvZ1lYSmpOQzVCWkdSeVpYTnpLVG9nWVhKak5DNUNiMjlzSUhzS0lDQWdJSEJ5YjNSdklERWdNUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZORE1LSUNBZ0lDOHZJSFJvYVhNdVgyVnVjM1Z5WlVSbFptRjFiSFJQZDI1bGNpZ3BDaUFnSUNCallXeHNjM1ZpSUY5bGJuTjFjbVZFWldaaGRXeDBUM2R1WlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pFM0NpQWdJQ0F2THlCd2RXSnNhV01nYjNkdVpYSWdQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMa0ZrWkhKbGMzTStLSHNnYTJWNU9pQW5ZWEpqT0RoZmJ5Y2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh5SUM4dklDSmhjbU00T0Y5dklnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk5EUUtJQ0FnSUM4dklHbG1JQ2doZEdocGN5NXZkMjVsY2k1b1lYTldZV3gxWlNrZ2NtVjBkWEp1SUc1bGR5QmhjbU0wTGtKdmIyd29abUZzYzJVcENpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1luVnllU0F4Q2lBZ0lDQmlibm9nWVhKak9EaGZhWE5mYjNkdVpYSmZZV1owWlhKZmFXWmZaV3h6WlVBeUNpQWdJQ0JpZVhSbFl5QXhNaUF2THlBd2VEQXdDaUFnSUNCeVpYUnpkV0lLQ21GeVl6ZzRYMmx6WDI5M2JtVnlYMkZtZEdWeVgybG1YMlZzYzJWQU1qb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qRTNDaUFnSUNBdkx5QndkV0pzYVdNZ2IzZHVaWElnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtGa1pISmxjM00rS0hzZ2EyVjVPaUFuWVhKak9EaGZieWNnZlNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eUlDOHZJQ0poY21NNE9GOXZJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qUTFDaUFnSUNBdkx5QnBaaUFvZEdocGN5NXZkMjVsY2k1MllXeDFaU0E5UFQwZ2JtVjNJR0Z5WXpRdVFXUmtjbVZ6Y3lncEtTQnlaWFIxY200Z2JtVjNJR0Z5WXpRdVFtOXZiQ2htWVd4elpTa0tJQ0FnSUdKNWRHVmpYekVnTHk4Z1lXUmtjaUJCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJXVFZJUmt0UkNpQWdJQ0E5UFFvZ0lDQWdZbm9nWVhKak9EaGZhWE5mYjNkdVpYSmZZV1owWlhKZmFXWmZaV3h6WlVBMENpQWdJQ0JpZVhSbFl5QXhNaUF2THlBd2VEQXdDaUFnSUNCeVpYUnpkV0lLQ21GeVl6ZzRYMmx6WDI5M2JtVnlYMkZtZEdWeVgybG1YMlZzYzJWQU5Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qRTNDaUFnSUNBdkx5QndkV0pzYVdNZ2IzZHVaWElnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtGa1pISmxjM00rS0hzZ2EyVjVPaUFuWVhKak9EaGZieWNnZlNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eUlDOHZJQ0poY21NNE9GOXZJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qUTJDaUFnSUNBdkx5QnlaWFIxY200Z2JtVjNJR0Z5WXpRdVFtOXZiQ2gwYUdsekxtOTNibVZ5TG5aaGJIVmxJRDA5UFNCeGRXVnllU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1BUMEtJQ0FnSUdKNWRHVmpJREV5SUM4dklEQjRNREFLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJSE5sZEdKcGRBb2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk9rRnlZemc0TG1GeVl6ZzRYMmx1YVhScFlXeHBlbVZmYjNkdVpYSW9ibVYzWDI5M2JtVnlPaUJpZVhSbGN5a2dMVDRnZG05cFpEb0tZWEpqT0RoZmFXNXBkR2xoYkdsNlpWOXZkMjVsY2pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pVd0xUVXhDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6ZzRYMmx1YVhScFlXeHBlbVZmYjNkdVpYSW9ibVYzWDI5M2JtVnlPaUJoY21NMExrRmtaSEpsYzNNcE9pQjJiMmxrSUhzS0lDQWdJSEJ5YjNSdklERWdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZNVGtLSUNBZ0lDOHZJSEIxWW14cFl5QnBibWwwYVdGc2FYcGxaQ0E5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW5sMFpUNG9leUJyWlhrNklDZGhjbU00T0Y5dmFTY2dmU2tnTHk4Z01TQnBaaUJwYm1sMGFXRnNhWHBsWkNBb1pYaHdiR2xqYVhRZ2IzSWdhVzF3YkdsamFYUXBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdNVE1nTHk4Z0ltRnlZemc0WDI5cElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk5USUtJQ0FnSUM4dklHRnpjMlZ5ZENnaEtIUm9hWE11YVc1cGRHbGhiR2w2WldRdWFHRnpWbUZzZFdVZ0ppWWdkR2hwY3k1cGJtbDBhV0ZzYVhwbFpDNTJZV3gxWlM1dVlYUnBkbVVnUFQwOUlERXBMQ0FuWVd4eVpXRmtlVjlwYm1sMGFXRnNhWHBsWkNjcENpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1luVnllU0F4Q2lBZ0lDQmllaUJoY21NNE9GOXBibWwwYVdGc2FYcGxYMjkzYm1WeVgySnZiMnhmWm1Gc2MyVkFNd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZNVGtLSUNBZ0lDOHZJSEIxWW14cFl5QnBibWwwYVdGc2FYcGxaQ0E5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UW5sMFpUNG9leUJyWlhrNklDZGhjbU00T0Y5dmFTY2dmU2tnTHk4Z01TQnBaaUJwYm1sMGFXRnNhWHBsWkNBb1pYaHdiR2xqYVhRZ2IzSWdhVzF3YkdsamFYUXBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdNVE1nTHk4Z0ltRnlZemc0WDI5cElnb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pVeUNpQWdJQ0F2THlCaGMzTmxjblFvSVNoMGFHbHpMbWx1YVhScFlXeHBlbVZrTG1oaGMxWmhiSFZsSUNZbUlIUm9hWE11YVc1cGRHbGhiR2w2WldRdWRtRnNkV1V1Ym1GMGFYWmxJRDA5UFNBeEtTd2dKMkZzY21WaFpIbGZhVzVwZEdsaGJHbDZaV1FuS1FvZ0lDQWdZblJ2YVFvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lEMDlDaUFnSUNCaWVpQmhjbU00T0Y5cGJtbDBhV0ZzYVhwbFgyOTNibVZ5WDJKdmIyeGZabUZzYzJWQU13b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0NtRnlZemc0WDJsdWFYUnBZV3hwZW1WZmIzZHVaWEpmWW05dmJGOXRaWEpuWlVBME9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk5USUtJQ0FnSUM4dklHRnpjMlZ5ZENnaEtIUm9hWE11YVc1cGRHbGhiR2w2WldRdWFHRnpWbUZzZFdVZ0ppWWdkR2hwY3k1cGJtbDBhV0ZzYVhwbFpDNTJZV3gxWlM1dVlYUnBkbVVnUFQwOUlERXBMQ0FuWVd4eVpXRmtlVjlwYm1sMGFXRnNhWHBsWkNjcENpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RZ0x5OGdZV3h5WldGa2VWOXBibWwwYVdGc2FYcGxaQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZOVE1LSUNBZ0lDOHZJR0Z6YzJWeWRDaHVaWGRmYjNkdVpYSWdJVDA5SUc1bGR5QmhjbU0wTGtGa1pISmxjM01vS1N3Z0ozcGxjbTlmWVdSa2NtVnpjMTl1YjNSZllXeHNiM2RsWkNjcENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR0o1ZEdWalh6RWdMeThnWVdSa2NpQkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQldUVklSa3RSQ2lBZ0lDQWhQUW9nSUNBZ1lYTnpaWEowSUM4dklIcGxjbTlmWVdSa2NtVnpjMTl1YjNSZllXeHNiM2RsWkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZemc0TG1Gc1oyOHVkSE02TVRjS0lDQWdJQzh2SUhCMVlteHBZeUJ2ZDI1bGNpQTlJRWRzYjJKaGJGTjBZWFJsUEdGeVl6UXVRV1JrY21WemN6NG9leUJyWlhrNklDZGhjbU00T0Y5dkp5QjlLUW9nSUNBZ1lubDBaV05mTWlBdkx5QWlZWEpqT0RoZmJ5SUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qVTBDaUFnSUNBdkx5QjBhR2x6TG05M2JtVnlMblpoYkhWbElEMGdibVYzWDI5M2JtVnlDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6b3hPUW9nSUNBZ0x5OGdjSFZpYkdsaklHbHVhWFJwWVd4cGVtVmtJRDBnUjJ4dlltRnNVM1JoZEdVOFlYSmpOQzVDZVhSbFBpaDdJR3RsZVRvZ0oyRnlZemc0WDI5cEp5QjlLU0F2THlBeElHbG1JR2x1YVhScFlXeHBlbVZrSUNobGVIQnNhV05wZENCdmNpQnBiWEJzYVdOcGRDa0tJQ0FnSUdKNWRHVmpJREV6SUM4dklDSmhjbU00T0Y5dmFTSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qVTFDaUFnSUNBdkx5QjBhR2x6TG1sdWFYUnBZV3hwZW1Wa0xuWmhiSFZsSUQwZ2JtVjNJR0Z5WXpRdVFubDBaU2d4S1FvZ0lDQWdZbmwwWldNZ01qVWdMeThnTUhnd01Rb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUhKbGRITjFZZ29LWVhKak9EaGZhVzVwZEdsaGJHbDZaVjl2ZDI1bGNsOWliMjlzWDJaaGJITmxRRE02Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lpQmhjbU00T0Y5cGJtbDBhV0ZzYVhwbFgyOTNibVZ5WDJKdmIyeGZiV1Z5WjJWQU5Bb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pwQmNtTTRPQzVoY21NNE9GOTBjbUZ1YzJabGNsOXZkMjVsY25Ob2FYQW9ibVYzWDI5M2JtVnlPaUJpZVhSbGN5a2dMVDRnZG05cFpEb0tZWEpqT0RoZmRISmhibk5tWlhKZmIzZHVaWEp6YUdsd09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk5UZ3ROVGtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNBdkx5QndkV0pzYVdNZ1lYSmpPRGhmZEhKaGJuTm1aWEpmYjNkdVpYSnphR2x3S0c1bGQxOXZkMjVsY2pvZ1lYSmpOQzVCWkdSeVpYTnpLVG9nZG05cFpDQjdDaUFnSUNCd2NtOTBieUF4SURBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pZd0NpQWdJQ0F2THlCMGFHbHpMbDlsYm5OMWNtVkVaV1poZFd4MFQzZHVaWElvS1FvZ0lDQWdZMkZzYkhOMVlpQmZaVzV6ZFhKbFJHVm1ZWFZzZEU5M2JtVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6bzJNUW9nSUNBZ0x5OGdZWE56WlhKMEtHNWxkeUJoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpa2dQVDA5SUhSb2FYTXViM2R1WlhJdWRtRnNkV1VzSUNkdWIzUmZiM2R1WlhJbktRb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZemc0TG1Gc1oyOHVkSE02TVRjS0lDQWdJQzh2SUhCMVlteHBZeUJ2ZDI1bGNpQTlJRWRzYjJKaGJGTjBZWFJsUEdGeVl6UXVRV1JrY21WemN6NG9leUJyWlhrNklDZGhjbU00T0Y5dkp5QjlLUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdKNWRHVmpYeklnTHk4Z0ltRnlZemc0WDI4aUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZemc0TG1Gc1oyOHVkSE02TmpFS0lDQWdJQzh2SUdGemMyVnlkQ2h1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwSUQwOVBTQjBhR2x6TG05M2JtVnlMblpoYkhWbExDQW5ibTkwWDI5M2JtVnlKeWtLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2JtOTBYMjkzYm1WeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvMk1nb2dJQ0FnTHk4Z1lYTnpaWEowS0c1bGQxOXZkMjVsY2lBaFBUMGdibVYzSUdGeVl6UXVRV1JrY21WemN5Z3BMQ0FuZW1WeWIxOWhaR1J5WlhOelgyNXZkRjloYkd4dmQyVmtKeWtLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1lubDBaV05mTVNBdkx5QmhaR1J5SUVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZaTlVoR1MxRUtJQ0FnSUNFOUNpQWdJQ0JoYzNObGNuUWdMeThnZW1WeWIxOWhaR1J5WlhOelgyNXZkRjloYkd4dmQyVmtDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6b3hOd29nSUNBZ0x5OGdjSFZpYkdsaklHOTNibVZ5SUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1QlpHUnlaWE56UGloN0lHdGxlVG9nSjJGeVl6ZzRYMjhuSUgwcENpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdZbmwwWldOZk1pQXZMeUFpWVhKak9EaGZieUlLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2dSMnh2WW1Gc1UzUmhkR1VnWlhocGMzUnpDaUFnSUNCaWVYUmxZMTh5SUM4dklDSmhjbU00T0Y5dklnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk5qUUtJQ0FnSUM4dklIUm9hWE11YjNkdVpYSXVkbUZzZFdVZ1BTQnVaWGRmYjNkdVpYSUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qWTFDaUFnSUNBdkx5QmxiV2wwS0c1bGR5QmhjbU00T0Y5UGQyNWxjbk5vYVhCVWNtRnVjMlpsY25KbFpDaDdJSEJ5WlhacGIzVnpYMjkzYm1WeU9pQndjbVYyYVc5MWN5d2dibVYzWDI5M2JtVnlJSDBwS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR0o1ZEdWaklETXlJQzh2SUcxbGRHaHZaQ0FpWVhKak9EaGZUM2R1WlhKemFHbHdWSEpoYm5ObVpYSnlaV1FvWVdSa2NtVnpjeXhoWkdSeVpYTnpLU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem82UVhKak9EZ3VZWEpqT0RoZmNtVnViM1Z1WTJWZmIzZHVaWEp6YUdsd0tDa2dMVDRnZG05cFpEb0tZWEpqT0RoZmNtVnViM1Z1WTJWZmIzZHVaWEp6YUdsd09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk56QUtJQ0FnSUM4dklIUm9hWE11WDJWdWMzVnlaVVJsWm1GMWJIUlBkMjVsY2lncENpQWdJQ0JqWVd4c2MzVmlJRjlsYm5OMWNtVkVaV1poZFd4MFQzZHVaWElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPamN4Q2lBZ0lDQXZMeUJoYzNObGNuUW9ibVYzSUdGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLU0E5UFQwZ2RHaHBjeTV2ZDI1bGNpNTJZV3gxWlN3Z0oyNXZkRjl2ZDI1bGNpY3BDaUFnSUNCMGVHNGdVMlZ1WkdWeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pveE53b2dJQ0FnTHk4Z2NIVmliR2xqSUc5M2JtVnlJRDBnUjJ4dlltRnNVM1JoZEdVOFlYSmpOQzVCWkdSeVpYTnpQaWg3SUd0bGVUb2dKMkZ5WXpnNFgyOG5JSDBwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV05mTWlBdkx5QWlZWEpqT0RoZmJ5SUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ1IyeHZZbUZzVTNSaGRHVWdaWGhwYzNSekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvM01Rb2dJQ0FnTHk4Z1lYTnpaWEowS0c1bGR5QmhjbU0wTGtGa1pISmxjM01vVkhodUxuTmxibVJsY2lrZ1BUMDlJSFJvYVhNdWIzZHVaWEl1ZG1Gc2RXVXNJQ2R1YjNSZmIzZHVaWEluS1FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnViM1JmYjNkdVpYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qRTNDaUFnSUNBdkx5QndkV0pzYVdNZ2IzZHVaWElnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtGa1pISmxjM00rS0hzZ2EyVjVPaUFuWVhKak9EaGZieWNnZlNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eUlDOHZJQ0poY21NNE9GOXZJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUdKNWRHVmpYeklnTHk4Z0ltRnlZemc0WDI4aUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvM013b2dJQ0FnTHk4Z2RHaHBjeTV2ZDI1bGNpNTJZV3gxWlNBOUlHNWxkeUJoY21NMExrRmtaSEpsYzNNb0tRb2dJQ0FnWW5sMFpXTmZNU0F2THlCaFpHUnlJRUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRlpOVWhHUzFFS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmY0hWMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvM05Bb2dJQ0FnTHk4Z1pXMXBkQ2h1WlhjZ1lYSmpPRGhmVDNkdVpYSnphR2x3VW1WdWIzVnVZMlZrS0hzZ2NISmxkbWx2ZFhOZmIzZHVaWEk2SUhCeVpYWnBiM1Z6SUgwcEtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE16UTJZV0V4TmpZZ0x5OGdiV1YwYUc5a0lDSmhjbU00T0Y5UGQyNWxjbk5vYVhCU1pXNXZkVzVqWldRb1lXUmtjbVZ6Y3lraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk9rRnlZemc0TG1GeVl6ZzRYM1J5WVc1elptVnlYMjkzYm1WeWMyaHBjRjl5WlhGMVpYTjBLSEJsYm1ScGJtYzZJR0o1ZEdWektTQXRQaUIyYjJsa09ncGhjbU00T0Y5MGNtRnVjMlpsY2w5dmQyNWxjbk5vYVhCZmNtVnhkV1Z6ZERvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pjNExUYzVDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnTHk4Z2NIVmliR2xqSUdGeVl6ZzRYM1J5WVc1elptVnlYMjkzYm1WeWMyaHBjRjl5WlhGMVpYTjBLSEJsYm1ScGJtYzZJR0Z5WXpRdVFXUmtjbVZ6Y3lrNklIWnZhV1FnZXdvZ0lDQWdjSEp2ZEc4Z01TQXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6bzRNQW9nSUNBZ0x5OGdkR2hwY3k1ZlpXNXpkWEpsUkdWbVlYVnNkRTkzYm1WeUtDa0tJQ0FnSUdOaGJHeHpkV0lnWDJWdWMzVnlaVVJsWm1GMWJIUlBkMjVsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZemc0TG1Gc1oyOHVkSE02T0RFS0lDQWdJQzh2SUdGemMyVnlkQ2h1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwSUQwOVBTQjBhR2x6TG05M2JtVnlMblpoYkhWbExDQW5ibTkwWDI5M2JtVnlKeWtLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qRTNDaUFnSUNBdkx5QndkV0pzYVdNZ2IzZHVaWElnUFNCSGJHOWlZV3hUZEdGMFpUeGhjbU0wTGtGa1pISmxjM00rS0hzZ2EyVjVPaUFuWVhKak9EaGZieWNnZlNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eUlDOHZJQ0poY21NNE9GOXZJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qZ3hDaUFnSUNBdkx5QmhjM05sY25Rb2JtVjNJR0Z5WXpRdVFXUmtjbVZ6Y3loVWVHNHVjMlZ1WkdWeUtTQTlQVDBnZEdocGN5NXZkMjVsY2k1MllXeDFaU3dnSjI1dmRGOXZkMjVsY2ljcENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJRzV2ZEY5dmQyNWxjZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZPRElLSUNBZ0lDOHZJR0Z6YzJWeWRDaHdaVzVrYVc1bklDRTlQU0J1WlhjZ1lYSmpOQzVCWkdSeVpYTnpLQ2tzSUNkNlpYSnZYMkZrWkhKbGMzTmZibTkwWDJGc2JHOTNaV1FuS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmllWFJsWTE4eElDOHZJR0ZrWkhJZ1FVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVZrMVNFWkxVUW9nSUNBZ0lUMEtJQ0FnSUdGemMyVnlkQ0F2THlCNlpYSnZYMkZrWkhKbGMzTmZibTkwWDJGc2JHOTNaV1FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPakU0Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdjR1Z1WkdsdVowOTNibVZ5SUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1QlpHUnlaWE56UGloN0lHdGxlVG9nSjJGeVl6ZzRYM0J2SnlCOUtTQXZMeUJ2Y0hScGIyNWhiQ0IwZDI4dGMzUmxjQW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdKNWRHVmpJREV3SUM4dklDSmhjbU00T0Y5d2J5SUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qZ3pDaUFnSUNBdkx5QnBaaUFvZEdocGN5NXdaVzVrYVc1blQzZHVaWEl1YUdGelZtRnNkV1VnSmlZZ2RHaHBjeTV3Wlc1a2FXNW5UM2R1WlhJdWRtRnNkV1VnSVQwOUlHNWxkeUJoY21NMExrRmtaSEpsYzNNb0tTa2dld29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ1lYSmpPRGhmZEhKaGJuTm1aWEpmYjNkdVpYSnphR2x3WDNKbGNYVmxjM1JmWVdaMFpYSmZhV1pmWld4elpVQXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6b3hPQW9nSUNBZ0x5OGdjSFZpYkdsaklIQmxibVJwYm1kUGQyNWxjaUE5SUVkc2IySmhiRk4wWVhSbFBHRnlZelF1UVdSa2NtVnpjejRvZXlCclpYazZJQ2RoY21NNE9GOXdieWNnZlNrZ0x5OGdiM0IwYVc5dVlXd2dkSGR2TFhOMFpYQUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFl5QXhNQ0F2THlBaVlYSmpPRGhmY0c4aUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZemc0TG1Gc1oyOHVkSE02T0RNS0lDQWdJQzh2SUdsbUlDaDBhR2x6TG5CbGJtUnBibWRQZDI1bGNpNW9ZWE5XWVd4MVpTQW1KaUIwYUdsekxuQmxibVJwYm1kUGQyNWxjaTUyWVd4MVpTQWhQVDBnYm1WM0lHRnlZelF1UVdSa2NtVnpjeWdwS1NCN0NpQWdJQ0JpZVhSbFkxOHhJQzh2SUdGa1pISWdRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFWazFTRVpMVVFvZ0lDQWdJVDBLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCd1pXNWthVzVuWDNSeVlXNXpabVZ5WDJWNGFYTjBjd29LWVhKak9EaGZkSEpoYm5ObVpYSmZiM2R1WlhKemFHbHdYM0psY1hWbGMzUmZZV1owWlhKZmFXWmZaV3h6WlVBek9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk1UZ0tJQ0FnSUM4dklIQjFZbXhwWXlCd1pXNWthVzVuVDNkdVpYSWdQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMa0ZrWkhKbGMzTStLSHNnYTJWNU9pQW5ZWEpqT0RoZmNHOG5JSDBwSUM4dklHOXdkR2x2Ym1Gc0lIUjNieTF6ZEdWd0NpQWdJQ0JpZVhSbFl5QXhNQ0F2THlBaVlYSmpPRGhmY0c4aUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pvNE5nb2dJQ0FnTHk4Z2RHaHBjeTV3Wlc1a2FXNW5UM2R1WlhJdWRtRnNkV1VnUFNCd1pXNWthVzVuQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVEVLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem94TndvZ0lDQWdMeThnY0hWaWJHbGpJRzkzYm1WeUlEMGdSMnh2WW1Gc1UzUmhkR1U4WVhKak5DNUJaR1J5WlhOelBpaDdJR3RsZVRvZ0oyRnlZemc0WDI4bklIMHBDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTmZNaUF2THlBaVlYSmpPRGhmYnlJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnUjJ4dlltRnNVM1JoZEdVZ1pYaHBjM1J6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem80TndvZ0lDQWdMeThnWlcxcGRDaHVaWGNnWVhKak9EaGZUM2R1WlhKemFHbHdWSEpoYm5ObVpYSlNaWEYxWlhOMFpXUW9leUJ3Y21WMmFXOTFjMTl2ZDI1bGNqb2dkR2hwY3k1dmQyNWxjaTUyWVd4MVpTd2djR1Z1WkdsdVoxOXZkMjVsY2pvZ2NHVnVaR2x1WnlCOUtTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZ3hObUptTVdZNU1TQXZMeUJ0WlhSb2IyUWdJbUZ5WXpnNFgwOTNibVZ5YzJocGNGUnlZVzV6Wm1WeVVtVnhkV1Z6ZEdWa0tHRmtaSEpsYzNNc1lXUmtjbVZ6Y3lraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnY21WMGMzVmlDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk9rRnlZemc0TG1GeVl6ZzRYMkZqWTJWd2RGOXZkMjVsY25Ob2FYQW9LU0F0UGlCMmIybGtPZ3BoY21NNE9GOWhZMk5sY0hSZmIzZHVaWEp6YUdsd09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNObFkzVnlhWFI1WDNSdmEyVnVMMkZ5WXpnNExtRnNaMjh1ZEhNNk9USUtJQ0FnSUM4dklIUm9hWE11WDJWdWMzVnlaVVJsWm1GMWJIUlBkMjVsY2lncENpQWdJQ0JqWVd4c2MzVmlJRjlsYm5OMWNtVkVaV1poZFd4MFQzZHVaWElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPakU0Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdjR1Z1WkdsdVowOTNibVZ5SUQwZ1IyeHZZbUZzVTNSaGRHVThZWEpqTkM1QlpHUnlaWE56UGloN0lHdGxlVG9nSjJGeVl6ZzRYM0J2SnlCOUtTQXZMeUJ2Y0hScGIyNWhiQ0IwZDI4dGMzUmxjQW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdKNWRHVmpJREV3SUM4dklDSmhjbU00T0Y5d2J5SUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5elpXTjFjbWwwZVY5MGIydGxiaTloY21NNE9DNWhiR2R2TG5Sek9qa3pDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTV3Wlc1a2FXNW5UM2R1WlhJdWFHRnpWbUZzZFdVc0lDZHViM1JmY0dWdVpHbHVaMTl2ZDI1bGNpY3BDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JoYzNObGNuUWdMeThnYm05MFgzQmxibVJwYm1kZmIzZHVaWElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPamswQ2lBZ0lDQXZMeUJqYjI1emRDQnpaVzVrWlhJZ1BTQnVaWGNnWVhKak5DNUJaR1J5WlhOektGUjRiaTV6Wlc1a1pYSXBDaUFnSUNCMGVHNGdVMlZ1WkdWeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pveE9Bb2dJQ0FnTHk4Z2NIVmliR2xqSUhCbGJtUnBibWRQZDI1bGNpQTlJRWRzYjJKaGJGTjBZWFJsUEdGeVl6UXVRV1JrY21WemN6NG9leUJyWlhrNklDZGhjbU00T0Y5d2J5Y2dmU2tnTHk4Z2IzQjBhVzl1WVd3Z2RIZHZMWE4wWlhBS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWXlBeE1DQXZMeUFpWVhKak9EaGZjRzhpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZPVFVLSUNBZ0lDOHZJR0Z6YzJWeWRDaHpaVzVrWlhJZ1BUMDlJSFJvYVhNdWNHVnVaR2x1WjA5M2JtVnlMblpoYkhWbExDQW5ibTkwWDNCbGJtUnBibWRmYjNkdVpYSW5LUW9nSUNBZ1pHbG5JREVLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2JtOTBYM0JsYm1ScGJtZGZiM2R1WlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pFM0NpQWdJQ0F2THlCd2RXSnNhV01nYjNkdVpYSWdQU0JIYkc5aVlXeFRkR0YwWlR4aGNtTTBMa0ZrWkhKbGMzTStLSHNnYTJWNU9pQW5ZWEpqT0RoZmJ5Y2dmU2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh5SUM4dklDSmhjbU00T0Y5dklnb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJR0o1ZEdWalh6SWdMeThnSW1GeVl6ZzRYMjhpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem81TndvZ0lDQWdMeThnZEdocGN5NXZkMjVsY2k1MllXeDFaU0E5SUhObGJtUmxjZ29nSUNBZ1pHbG5JRElLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem94T0FvZ0lDQWdMeThnY0hWaWJHbGpJSEJsYm1ScGJtZFBkMjVsY2lBOUlFZHNiMkpoYkZOMFlYUmxQR0Z5WXpRdVFXUmtjbVZ6Y3o0b2V5QnJaWGs2SUNkaGNtTTRPRjl3YnljZ2ZTa2dMeThnYjNCMGFXOXVZV3dnZEhkdkxYTjBaWEFLSUNBZ0lHSjVkR1ZqSURFd0lDOHZJQ0poY21NNE9GOXdieUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPams0Q2lBZ0lDQXZMeUIwYUdsekxuQmxibVJwYm1kUGQyNWxjaTUyWVd4MVpTQTlJRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9LUW9nSUNBZ1lubDBaV05mTVNBdkx5QmhaR1J5SUVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZaTlVoR1MxRUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6bzVPUW9nSUNBZ0x5OGdaVzFwZENodVpYY2dZWEpqT0RoZlQzZHVaWEp6YUdsd1ZISmhibk5tWlhKQlkyTmxjSFJsWkNoN0lIQnlaWFpwYjNWelgyOTNibVZ5T2lCd2NtVjJhVzkxY3l3Z2JtVjNYMjkzYm1WeU9pQnpaVzVrWlhJZ2ZTa3BDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlR1kzWlRNMllqTTNJQzh2SUcxbGRHaHZaQ0FpWVhKak9EaGZUM2R1WlhKemFHbHdWSEpoYm5ObVpYSkJZMk5sY0hSbFpDaGhaR1J5WlhOekxHRmtaSEpsYzNNcElnb2dJQ0FnWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmMyVmpkWEpwZEhsZmRHOXJaVzR2WVhKak9EZ3VZV3huYnk1MGN6b3hNREFLSUNBZ0lDOHZJR1Z0YVhRb2JtVjNJR0Z5WXpnNFgwOTNibVZ5YzJocGNGUnlZVzV6Wm1WeWNtVmtLSHNnY0hKbGRtbHZkWE5mYjNkdVpYSTZJSEJ5WlhacGIzVnpMQ0J1WlhkZmIzZHVaWEk2SUhObGJtUmxjaUI5S1NrS0lDQWdJR0o1ZEdWaklETXlJQzh2SUcxbGRHaHZaQ0FpWVhKak9EaGZUM2R1WlhKemFHbHdWSEpoYm5ObVpYSnlaV1FvWVdSa2NtVnpjeXhoWkdSeVpYTnpLU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZjMlZqZFhKcGRIbGZkRzlyWlc0dllYSmpPRGd1WVd4bmJ5NTBjem82UVhKak9EZ3VZWEpqT0RoZlkyRnVZMlZzWDI5M2JtVnljMmhwY0Y5eVpYRjFaWE4wS0NrZ0xUNGdkbTlwWkRvS1lYSmpPRGhmWTJGdVkyVnNYMjkzYm1WeWMyaHBjRjl5WlhGMVpYTjBPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzTmxZM1Z5YVhSNVgzUnZhMlZ1TDJGeVl6ZzRMbUZzWjI4dWRITTZNVEExQ2lBZ0lDQXZMeUIwYUdsekxsOWxibk4xY21WRVpXWmhkV3gwVDNkdVpYSW9LUW9nSUNBZ1kyRnNiSE4xWWlCZlpXNXpkWEpsUkdWbVlYVnNkRTkzYm1WeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YzJWamRYSnBkSGxmZEc5clpXNHZZWEpqT0RndVlXeG5ieTUwY3pveE1EWUtJQ0FnSUM4dklHRnpjMlZ5ZENodVpYY2dZWEpqTkM1QlpHUnlaWE56S0ZSNGJpNXpaVzVrWlhJcElEMDlQU0IwYUdsekxtOTNibVZ5TG5aaGJIVmxMQ0FuYm05MFgyOTNibVZ5SnlrS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPakUzQ2lBZ0lDQXZMeUJ3ZFdKc2FXTWdiM2R1WlhJZ1BTQkhiRzlpWVd4VGRHRjBaVHhoY21NMExrRmtaSEpsYzNNK0tIc2dhMlY1T2lBbllYSmpPRGhmYnljZ2ZTa0tJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFkxOHlJQzh2SUNKaGNtTTRPRjl2SWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJIYkc5aVlXeFRkR0YwWlNCbGVHbHpkSE1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXpaV04xY21sMGVWOTBiMnRsYmk5aGNtTTRPQzVoYkdkdkxuUnpPakV3TmdvZ0lDQWdMeThnWVhOelpYSjBLRzVsZHlCaGNtTTBMa0ZrWkhKbGMzTW9WSGh1TG5ObGJtUmxjaWtnUFQwOUlIUm9hWE11YjNkdVpYSXVkbUZzZFdVc0lDZHViM1JmYjNkdVpYSW5LUW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCdWIzUmZiM2R1WlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl6WldOMWNtbDBlVjkwYjJ0bGJpOWhjbU00T0M1aGJHZHZMblJ6T2pFNENpQWdJQ0F2THlCd2RXSnNhV01nY0dWdVpHbHVaMDkzYm1WeUlEMGdSMnh2WW1Gc1UzUmhkR1U4WVhKak5DNUJaR1J5WlhOelBpaDdJR3RsZVRvZ0oyRnlZemc0WDNCdkp5QjlLU0F2THlCdmNIUnBiMjVoYkNCMGQyOHRjM1JsY0FvZ0lDQWdZbmwwWldNZ01UQWdMeThnSW1GeVl6ZzRYM0J2SWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM05sWTNWeWFYUjVYM1J2YTJWdUwyRnlZemc0TG1Gc1oyOHVkSE02TVRBM0NpQWdJQ0F2THlCMGFHbHpMbkJsYm1ScGJtZFBkMjVsY2k1MllXeDFaU0E5SUc1bGR5QmhjbU0wTGtGa1pISmxjM01vS1FvZ0lDQWdZbmwwWldOZk1TQXZMeUJoWkdSeUlFRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGQlFVRkJRVUZCUVVGWk5VaEdTMUVLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQnlaWFJ6ZFdJSyIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV3Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZbUZ6WlMxamIyNTBjbUZqZEM1a0xuUnpPanBDWVhObFEyOXVkSEpoWTNRdVkyeGxZWEpUZEdGMFpWQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0J3ZFhOb2FXNTBJREVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZz09In0sImJ5dGVDb2RlIjp7ImFwcHJvdmFsIjoiQ2lBRUFBRWdBaVloQkJVZmZIVWdBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBSFlYSmpPRGhmYndGMEFXSUNBQUlPWVhKak1UWTBORjlqZEhKc1pXNEJnQWxoY21NeE5ERXdYM0FNWVhKak1UWTBNMTlrYjJOekNHRnlZemc0WDNCdkRHRnlZekUyTkRSZlkzUnliQUVBQ0dGeVl6ZzRYMjlwQzJGeVl6RTBNVEJmYjNCaERHRnlZekUyTkRSZmJXTmhhUUlBQUF0aGNtTXhOVGswWDJsemN3eGhjbU14TkRFd1gyaHdYMkVLWVhKak1UUXhNRjl2Y0ExaGNtTXhOalEwWDNKcWRYTjBER0Z5WXpFMk5EUmZiR05oY2dnQUFBQUFBQUFBQUF0aGNtTXhOalF6WDJSdll3SUFRZ0VCQWdBQkFnQmlBZ0FHQk5mOFNwZ0VYQ2UwL0FSNWc4TmNCRU5WMHEweEcwRUZPSUl4QkFSVWN0QUVmWGtFcEFUbTlQaGhCQzY5TFRRRTdtOHREZ1FkWEhvWEJPVjZiaGdFRWNzMjlRU3hiWHFNQktmTE5JSUU3TFkyeUFSbHNXZ3FCQUV3V1pzRUZDdGZ5d1Q0ZzQ2NUJER0lLL29FcWN5aGJ3UW1aWmZBQkRYNEUxOEUybkFsdVFRL0pXY1RCSlcwK2VNRWdNeEpxd1FIbGlGbEJPZUpZZG9FL1pTQTF3U3hzZGFhQk1HKzE0a0VPLzRZTXdSWm5OR2xCRzNwUVdZRUtQQWoxd1NYVTRMaUJHVjlFK3dFdHE0YUpRU0U3QlBWQk95WllFRUVndVZ6eEFSS2xvK1BCTFZDSVNVRXU3TVo4d1FIQW1WT0JOQVZjazRFQXAvc3dBUnpTVE5PQk50OGd1OEUvU3dzYmdSQ3BmQmxCSzFQYU9vMkdnQ09NUVBMQTd3RHJRT2VBNDREYndOV0EwRURMZ01mQXc4REFBTHJBdFlDeEFLckFvOENmd0pwQWxNQ053SWhBZ2dCOHdIZUFiOEJvQUdJQVc4QlZ3RkNBU29CRGdEK0FPNEEzZ0RPQUxzQW9nQ01BSFlBWmdCVEFFUUFOUUFwQUJvQURnQUNJa014R1JSRU1SaEVpQktNSTBNeEdSUkVNUmhFaUJKTkkwTXhHUlJFTVJoRU5ob0JpQklESTBNeEdSUkVNUmhFaUJIYkkwTXhHUlJFTVJoRU5ob0JpQkdvSTBNeEdSUkVNUmhFTmhvQmlCRnNJME14R1JSRU1SaEVOaG9CaUJFeUtFeFFzQ05ETVJrVVJERVlSSWdSR2loTVVMQWpRekVaRkVReEdFUTJHZ0UyR2dLSUVBZ29URkN3STBNeEdSUkVNUmhFTmhvQk5ob0NpQS9sS0V4UXNDTkRNUmtVUkRFWVJEWWFBVFlhQWpZYUE0Z1BuaWhNVUxBalF6RVpGRVF4R0VRMkdnR0lENElvVEZDd0kwTXhHUlJFTVJoRWlBOXRLRXhRc0NORE1Sa1VSREVZUklnUFZpaE1VTEFqUXpFWkZFUXhHRVNJRHpZb1RGQ3dJME14R1JSRU1SaEVpQThYS0V4UXNDTkRNUmtVUkRFWVJEWWFBVFlhQWpZYUF6WWFCSWdPcFNoTVVMQWpRekVaRkVReEdFUTJHZ0UyR2dJMkdnTTJHZ1NJRGJralF6RVpGRVF4R0VRMkdnRTJHZ0kyR2dPSURSd2pRekVaRkVReEdFUTJHZ0UyR2dJMkdnTTJHZ1NJREhBalF6RVpGRVF4R0VRMkdnRTJHZ0kyR2dPSURDTW9URkN3STBNeEdSUkVNUmhFTmhvQk5ob0NOaG9ETmhvRWlBdnVJME14R1JSRU1SaEVOaG9CTmhvQ05ob0ROaG9FTmhvRmlBaWFLRXhRc0NORE1Sa1VSREVZUkRZYUFUWWFBallhQXpZYUJEWWFCWWdJRlNoTVVMQWpRekVaRkVReEdFUTJHZ0UyR2dJMkdnT0lCOXNqUXpFWkZFUXhHRVEyR2dFMkdnSTJHZ09JQjYwalF6RVpGRVF4R0VRMkdnRTJHZ0kyR2dPSUJ6a29URkN3STBNeEdSUkVNUmhFTmhvQk5ob0NpQWNHS0V4UXNDTkRNUmtVUkRFWVJEWWFBVFlhQWpZYUF6WWFCSWdHenloTVVMQWpRekVaRkVReEdFUTJHZ0UyR2dLSUJwOG9URkN3STBNeEdSUkVNUmhFTmhvQk5ob0NpQVo2S0V4UXNDTkRNUmtVUkRFWVJJZ0daQ2hNVUxBalF6RVpGRVF4R0VRMkdnRTJHZ0kyR2dNMkdnU0lCanNvVEZDd0kwTXhHUlJFTVJoRU5ob0JOaG9DTmhvRGlBWVhLRXhRc0NORE1Sa1VSREVZUkRZYUFUWWFBb2dGbmlORE1Sa1VSREVZUkRZYUFUWWFBallhQTRnRkNpTkRNUmtVUkRFWVJEWWFBVFlhQWpZYUE0Z0VxeU5ETVJrVVJERVlSRFlhQVlnRWtDTkRNUmtVUkRFWVJJZ0VkQ2hNVUxBalF6RVpGRVF4R0VRMkdnR0lCQU1qUXpFWkZFUXhHRVEyR2dHSUErUW9URkN3STBNeEdSUkVNUmhFTmhvQk5ob0NOaG9EaUFNb0kwTXhHUlJFTVJoRU5ob0JOaG9DTmhvRGlBS21LRXhRc0NORE1Sa1VSREVZUkRZYUFUWWFBallhQXpZYUJEWWFCWWdDQ0NoTVVMQWpRekVaRkVReEdFU0lBY3dvVEZDd0kwTXhHUlJFTVJoRU5ob0JpQUd0STBNeEdSUkVNUmhFTmhvQmlBR1NJME14R1JSRU1SaEVOaG9CaUFGVkkwTXhHUlJFTVJoRU5ob0JpQUVISTBNeEdVRDhJVEVZRkVRalE0b0RBWXY5SWxsSmkvOElUQ1VMSlFoTEFSWlhCZ0pPQW92OUpVc0NVb3YvSlF1dlVJdjlGWXY5VHdOUEFsSlFpLzVRVENVTFNTSk1pd09MQWd4QkFDT0xCRWtXVndZQ2l3R0xBMGxPQkU4Q1hVbU1BVXNCV1NVSUNJd0VKUWlNQTBMLzFZc0Fpd0ZRakFDSk1RQ0lEVDBpVXlNU1JJa2lKd3RsUlFGRU1RQWlKd3RsUkJKRUlpY0daVVVCUVFBUElpY0daVVFpVXlNU1FRQURJMFNKSWtMLytvb0JBQ0luRkdWRkFVRUFFeUluRkdWRUlsTWpFa0VBQjR2L1Z3SUFGVVNKSWljUFpVVUJRUUF1SWljUFpVUVhRUUFsSWljVlpVVUJRUUFWSWljVlpVUVhJaWNQWlVRWE1nWVdGMDRDQ0E5RU1nWVdKeFZNWjRtS0FRQWlpUDlxSWljTFpVVUJRUUFwSWljTFpVU01BQ2NMaS85bklpY0daVVVCUUFBRkp3WW5CMmVMQUl2L1VJQUVRSnpGY0V4UXNJa3BqQUJDLzlpS0FRQ0kveXlML3lKVFFBQUdKd2FMLzJlSklpY0daVVVCUVFBTUlpY0daVVFpVXlNU1FmL3FKd2FMLzJlSmlnRUFpUDcrSnhTTC8yZUppZ0VBaVA3eUp3K0wvMmVKSWljR1pVVUJRUUFnSWljR1pVUWlVeU1TUVFBVUlpY0xaVVVCUVFBTGdBZ0FBQUFBQUFBQUFZa25Gb21LQlFHSS9zV0wvNGorN0lqL0NZdjdpL3dUUkl2N2lBcndTWXY5cDBTTC9hRkpGU1FPUkNTdlRFc0JxeWNFaS90UVRMK0wvSWdLMFl2OW9Fa1ZKQTVFcXljRWkveFFUTDh4QUl2N1VJdjhVSXY5VUlBQlVWQ0FBZ0NGVUl2K0ZZR0ZBUWdXVndZQ1VJditVSXYvVUNjRlRGQ0FCRFJ1cDVWTVVMQ0JVUmFKaWdNQmlQNUdpLytJL20ySS9vcUwvWWdLZDBtTC9xZEVpLzZoU1JVa0RrUWtyMHhMQWFzbkJJdjlVRXkvSWl0bFJJditvVWtWSkE1RXF5dE1aekVBaS8xUWkvNVFnQUZSVUlBQ0FHTlFpLzlRSndWTVVJQUVEZTRVOVV4UXNJRlJGb2t4QUlnTEhDSlRJeEpFaVlvREFJai83NHY5RlVsRU1nWVdpLzRWU1U0Q2dRd0lGbGNHQW9BQ0FBeE1VRXhRaS81UWkvOVFKeGVML1ZCSnZFaE12eWNKdlVVQlFBQStKeENML1NPSS9TWW5DYnhJSndsTXY0RUdpd0FJU1JaWEJnSW5IRXhRVElzQkNCWlhCZ0pRaS8xUWkvNVFpLzlRSndWTVVJQUVMY0E4Tmt4UXNJa25DYjVFSnhDTC9TT0kvT1JKSWxsTVZ3SUFTd0VsQzBzQkZWSk1pUHpSSndtOFNDY0pUTDlDLzZpS0FRRW5GNHYvVUVtOVJRRkV2a1NKaWdFQWlQODhKeGVMLzFCSnZVVUJSRW0rUkV5OFNFa2lXVXNCSlZsTEFrOENTd0pTU3dJVlR3TlBBMDhDVW92L0ZZRUdDRWtXVndZQ0p4eE1VRXNERlU4Q0NCWlhCZ0pRaS85UVR3SlFURkFuQlV4UWdBU3VlaytnVEZDd2lTY0p2a1NKTVFDSUNmc2lVeU1TUkltS0FRQ0kvKzhuRVl2L1o0bUtBd0NJLytPTC9vQUFwVVFpSnhGbFJRRkJBREVpSnhGbFJDSlRJeEpCQUNValJJdjlLWXYraS8rSUJnZUwvWXYrVUNjWVVJdi9VQ2NGVEZDQUJQTHBtSzlNVUxDSklrTC8ySW9EQUNJeEFFbUwvUkpBQUF5TEFZZ0pqU0pUSXhKQkFHSWpSSXYrZ0FDbFJDY0VpLzFRU1l3QXZVVUJRUUJJaXdDK1JJditwMEVBUGlORWl3Qkp2a1NML3FGSkZTUU9SQ1N2VEVzQnEwOENUTDhpSzJWRWkvNmhTUlVrRGtTcksweG5pLzJML2xBbkdGQ0wvMUFuQlV4UUp4MU1VTENKSWtML3Z5SkMvNXVLQWdBeEFFbUwvb0FBcFVRbkJFeFFTYjFGQVVFQVNJc0J2a1NML3FkQkFENGpSSXNCU2I1RWkvNmhTUlVrRGtRa3IweExBYXRQQWt5L0lpdGxSSXYrb1VrVkpBNUVxeXRNWjRzQWkvNVFKeGhRaS85UUp3Vk1VQ2NkVEZDd2lTSkMvNytLQXdHTC9ZditpQUFqaVlvRUFZdjhpLzJML29nSFo0a2lKeEZsUkltS0FnR0wvb3YvVUNjSVRGQytSSW1LQWdFeEFDbUwvaW1ML3ljUWlBUHdNUUNML292L2lBZVdpWW9FQVRFQWkvMkwvSWdDdUV5TC9JdjlTd09ML292L2lBUE1pWW9DQVl2K2kvOVFKeEpNVUVtOVJRRkFBQVFuRUV5Sml3QytSRXlKaWdNQklrY0NpLzZML1JKQkFBVW5CNHdBaVl2OWkvNVFTWXdBaS85UUp4Tk1VRW1NQWIxRkFVRUFENHNCdmtRWEl4SkJBQVVuQjR3QWlZc0FLVkFuRTB4UVNZd0N2VVVCUVFBUGl3SytSQmNqRWtFQUJTY0hqQUNKSnd5TUFJbUtBd0F4QUl2OUVrU0wvWXYrVUl2L1VDY1RURkFuR2IrSmlnTUFNUUNML1JKRWkvMkwvbENMLzFBbkUweFFTYjFGQVVFQUJJc0F2RWlKaWdVQklqRUFpL3N4QUl2OGlQOVlJbE1qRWtjQ1FBQXlpL3VMQVZDTC9GQW5Ea3hRU1l3QXZVVUJRUUFaaXdCSnZrUkppLzZuUkNPTUFvditvVWtWSkE1RUpLK3J2NHNDakFPTEEwU0wvWXY4aUFHZmkvdUwvSXY5U3dPTC9vdi9pQUt5akFDSmlnVUJJa21BQUVtTCs0djhVQ2NJVEZCSnZVVUJRQUErZ0RsUUFDTUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBVVVHRnlkR2wwYVc5dUlHNXZkQ0JsZUdsemRIT01BSW1MQkw1RWkvNmtRUUErZ0RsU0FDTUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBVVNXNXpkV1ptYVdOcFpXNTBJR0poYkdGdVkyV01BSW1ML1NrU1FRQTZnRFZYQUNNQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFRU1c1MllXeHBaQ0J5WldObGFYWmxjb3dBaVRFQVNZd0FpL3NUUVFDR2kvdUxBSXY4aVA0T0lsTWpFa21NQWttTUEwQUFLb3Y3aXdCUWkveFFKdzVNVUVtTUFiMUZBWXNDakFOQkFCR0xBYjVFaS82blFRQURJNHdDaXdLTUE0c0RRQUJCZ0R4WUFDTUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBWFQzQmxjbUYwYjNJZ2JtOTBJR0YxZEdodmNtbDZaV1NNQUltTC9ZdjhpQUFXZ0FOUkFDTk1VSUFKQUFkVGRXTmpaWE56VUl3QWlZb0NBU21ML292L1VDY0lURkM5UlFGQkFBU0wvNHdBaXdCTWlZb0NBQ0pIQklBQVJ3U0FER0Z5WXpFME1UQmZhSEJmY0l2K1VFbTlSUUZBQUFXTENpY1d2NHNLdmt5TUFrUWlqQVluRm93Qml3RVhTWXdJaXdJWFNZd0pERUVBVDR2K2l3RlFKeEpNVUVtTUJMMUZBVUFBRENjYWkvOVFpd1JKdkVoTXY0c0V2a3hKVGdLTUFFUWlXWXdGSW93SGl3ZUxCUXhCQUllTEFGY0NBSXNISkFza1dJdi9Fa0VBYlNOQkFGOGpqQWFMQmtBQU9Jditpd0pRSnhKTVVFbU1BNzVFSWxrV0Y0RUtERUVBSVlzRFNiNUVWd0lBSnhxTC8xQlhBZ0JRU1JVa0NoWlhCZ0pNVUVzQnZFaS9pWXNKSXdnV2l3cExBYitML2t4UUp4cUwvMUFuRWs4Q1VFbThTRXkvaVlzSUl3Z1dqQUZDL3p1TEJ5TUlqQWRDLzNFaVF2K0hpZ1lBSWttTC9vQUFwVVNMK292N1VDY0lURkJKdlVVQlFBQUVpd0lwdjRzQ1NiNUVpLzZoU1JVa0RrUWtyMG1NQUt1L2kvcUwvRkNMKzFDTC9sQ0FBZ0NDVUl2L1VDY0ZURkNBQkNCcmVVQk1VTENML1l2N0UwRUFCNHY4aS8ySS9wZUwvSXY5VUNjSVRGQkpqQUc5UlFGQUFBU0xBU20vaXdGSnZrU0wvcUJKRlNRT1JJc0FxNytKaWdRQU1RQ0wvQkpFaS95TC9WQ0wvbEFuRGt4UWkvKy9pWW9EQVNLTC9vdjlFa0VBQkNjSFRJbUwvWXYrVUl2L1VDY09URkJKakFDOVJRRkFBQVFuREV5Sml3QytSSUFBcFNjTUlrOENWRXlKaWdRQUlra3hBSWdEcXlKVEl4SkVpLzZBQUtWRWkveUwvVkJKSndoTVVFbTlSUUZBQUF1TEF5bS9pL3lML1lqOTdZc0RTYjVFaS82Z1NSVWtEa1FrcjBtTUFLdS9Kd1NML0ZCSmpBRzlSUUZBQUFTTEFTbS9pd0ZKdmtTTC9xQkpGU1FPUklzQVNVNERxNzhpSzJWRWkvNmdTUlVrRGtTcksweG5pd0tML2xBbkcxQ0wvMUFuQlV4UWdBVDZSRHNiVEZDd2lZb0RBREVBaS82QUFLVkVTWXY5VUVsT0FpY0lURkJKdlVVQlJFbStSSXYrcDBSSnZrU0wvcUZKRlNRT1JDU3ZTVTRFcTc4bkJFeFFTYjFGQVVFQVJZc0N2a1NML3FkQkFEc2pSSXNDU2I1RWkvNmhTUlVrRGtTTEFVbE9BNnUvSWl0bFJJditvVWtWSkE1RXF5dE1aNHNBaS81UUp4dFFpLzlRSndWTVVDY2VURkN3aVNKQy84S0tCQUFpUndNeEFJdjhNUUNML1lqNlNpSlRJeEpIQWtBQU1vdjhpd1JRaS8xUUp3NU1VRW1NQTcxRkFVRUFHWXNEU2I1RVNZditwMFFqakFXTC9xRkpGU1FPUkNTdnE3K0xCWXdHaXdaRWkveUwvVkJKakFFbkNFeFFTYjFGQVVSSnZrU0wvcWRFU2I1RWkvNmhTUlVrRGtRa3IwbU1BS3UvSndTTC9GQkpqQUs5UlFGQkFFV0xBcjVFaS82blFRQTdJMFNMQWttK1JJditvVWtWSkE1RWl3QkpUZ09ydnlJclpVU0wvcUZKRlNRT1JLc3JUR2VMQVl2K1VDY2JVSXYvVUNjRlRGQW5Ia3hRc0lraVF2L0NpZ1FCTVFBeUNSSkVpL3hYQWdBVlNVUWtEa1NML1ZjQ0FCVkpSSUVJRGtRaUsyVkZBUlJFZ0FGdWkveG5nQUZ6aS8xbks0di9aNEFCWkl2K1p6RUFKd1JMQVZDTC83OHlBMHhRaS85UUp4OU1VTEFuQjRraWdBRnVaVVJYQWdCSkZTUVNSSWtpZ0FGelpVUlhBZ0JKRllFSUVrU0pJb0FCWkdWRWlTSXJaVVNKaWdFQmkvK0lBRWVKaWdNQk1RQ0wvVXNCaUFDM1NZdi9wMFNMLzZGSkZTUU9SQ1N2cTR2OVRnS0lBTUpJaS8yTC9vdi9pQUF4aVlvQ0FURUFpLzZMLzRnQXE0bUtBZ0dML292L2lBQitpWW9CQVNjRWkvOVFTYjFGQVVBQUF5bE1pWXNBdmtSTWlZb0RBWXY5aVAvZ1NZditpUC9hVEl2L3AwU0wvWXYrRTBFQUtZc0FpLytoU1JVa0RrUWtyMHhMQWFzbkJJdjlVRXkvaXdHTC82QkpGU1FPUktzbkJJditVRXkvaS8yTC9sQ0wvMUFuSDB4UXNDY0hqQUNKaWdJQmkvNkwvMUFCU1JVa0VrU0ppZ0lCaS82TC80ai81NEFCWVV4UVNiMUZBVUFBQXlsTWlZc0F2a1JYQUNCTWlZb0RBWXY5aS82SS84V0wvNHY5VUl2K1VJQUJZVThDVUV5L2kvMkwvbENMLzFDQUJCbHArR1ZNVUxBbkI0a2lKdzFsUlFGQkFBa2lKdzFsUkJkQUFCRWlLbVZGQVVBQUJDb3lDV2NuRFNjWlo0bUkvOWtpS21WRWlZb0JBWWovemlJcVpVVUJRQUFESnd5SklpcGxSQ2tTUVFBREp3eUpJaXBsUkl2L0VpY01JazhDVkltS0FRQWlKdzFsUlFGQkFCMGlKdzFsUkJjakVrRUFFaU1VUkl2L0tSTkVLb3YvWnljTkp4bG5pU0pDLyt1S0FRQ0kvM1l4QUNJcVpVUVNSSXYvS1JORUlpcGxSQ3FMLzJlTC8xQW5JRXhRc0ltSS8xVXhBQ0lxWlVRU1JDSXFaVVFxS1dlQUJEUnFvV1pNVUxDSmlnRUFpUDgyTVFBaUttVkVFa1NML3lrVFJDSW5DbVZGQVVFQUNTSW5DbVZFS1JNVVJDY0tpLzluSWlwbFJJdi9VSUFFRnI4ZmtVeFFzSW1JL3Y0aUp3cGxSUUZFTVFBaUp3cGxSRXNCRWtRaUttVkVLa3NDWnljS0tXZE1VSUFFOStOck4wc0JVTEFuSUV4UXNJbUkvc3N4QUNJcVpVUVNSQ2NLS1dlSiIsImNsZWFyIjoiQ29FQlF3PT0ifSwiY29tcGlsZXJJbmZvIjp7ImNvbXBpbGVyIjoicHV5YSIsImNvbXBpbGVyVmVyc2lvbiI6eyJtYWpvciI6NCwibWlub3IiOjcsInBhdGNoIjowLCJjb21taXRIYXNoIjpudWxsfX0sImV2ZW50cyI6W3sibmFtZSI6IkNvbnRyb2xsZXJDaGFuZ2VkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MpIiwic3RydWN0IjoiYXJjMTY0NF9jb250cm9sbGVyX2NoYW5nZWRfZXZlbnQiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiQ29udHJvbGxlclRyYW5zZmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGUsYnl0ZVtdLGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNjQ0X2NvbnRyb2xsZXJfdHJhbnNmZXJfZXZlbnQiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiQ29udHJvbGxlclJlZGVlbSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyxhZGRyZXNzLHVpbnQyNTYsYnl0ZSxieXRlW10pIiwic3RydWN0IjoiYXJjMTY0NF9jb250cm9sbGVyX3JlZGVlbV9ldmVudCIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJEb2N1bWVudFVwZGF0ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGJ5dGVbXSxzdHJpbmcsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE2NDNfZG9jdW1lbnRfdXBkYXRlZF9ldmVudCIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJEb2N1bWVudFJlbW92ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKGJ5dGVbXSxzdHJpbmcsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE2NDNfZG9jdW1lbnRfcmVtb3ZlZF9ldmVudCIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJJc3N1ZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYWRkcmVzcyxhZGRyZXNzLHVpbnQyNTYsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE0MTBfcGFydGl0aW9uX2lzc3VlIiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6Iklzc3VlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE1OTRfaXNzdWVfZXZlbnQiLCJuYW1lIjoiMCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiUmVkZWVtIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLHVpbnQyNTYsYnl0ZVtdKSIsInN0cnVjdCI6ImFyYzE1OTRfcmVkZWVtX2V2ZW50IiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6IlRyYW5zZmVyIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyx1aW50MjU2LGJ5dGVbXSkiLCJzdHJ1Y3QiOiJhcmMxNDEwX3BhcnRpdGlvbl90cmFuc2ZlciIsIm5hbWUiOiIwIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmMyMDBfVHJhbnNmZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZyb20iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzIwMF9BcHByb3ZhbCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3duZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3BlbmRlciIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiUmVkZWVtIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihhZGRyZXNzLGFkZHJlc3MsdWludDI1NixieXRlW10pIiwic3RydWN0IjoiYXJjMTQxMF9wYXJ0aXRpb25fcmVkZWVtIiwibmFtZSI6IjAiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzg4X093bmVyc2hpcFRyYW5zZmVycmVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwcmV2aW91c19vd25lciIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJuZXdfb3duZXIiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzg4X093bmVyc2hpcFJlbm91bmNlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoicHJldmlvdXNfb3duZXIiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzg4X093bmVyc2hpcFRyYW5zZmVyUmVxdWVzdGVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwcmV2aW91c19vd25lciIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwZW5kaW5nX293bmVyIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmM4OF9Pd25lcnNoaXBUcmFuc2ZlckFjY2VwdGVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwcmV2aW91c19vd25lciIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJuZXdfb3duZXIiLCJkZXNjIjpudWxsfV19XSwidGVtcGxhdGVWYXJpYWJsZXMiOnt9LCJzY3JhdGNoVmFyaWFibGVzIjp7fX0=";
    }

}
