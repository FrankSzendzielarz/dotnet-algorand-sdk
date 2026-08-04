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

namespace MultisigRegression
{


    public class MultisigProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public MultisigProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class TransactionGroup : AVMObjectType
            {
                public ulong Nonce { get; set; }

                public byte Index { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNonce = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vNonce.From(Nonce);
                    ret.AddRange(vNonce.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vIndex = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vIndex.From(Index);
                    ret.AddRange(vIndex.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static TransactionGroup Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new TransactionGroup();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNonce = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vNonce.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueNonce = vNonce.ToValue();
                    if (valueNonce is ulong vNonceValue) { ret.Nonce = vNonceValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vIndex = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vIndex.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueIndex = vIndex.ToValue();
                    if (valueIndex is byte vIndexValue) { ret.Index = vIndexValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as TransactionGroup);
                }
                public bool Equals(TransactionGroup? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(TransactionGroup left, TransactionGroup right)
                {
                    return EqualityComparer<TransactionGroup>.Default.Equals(left, right);
                }
                public static bool operator !=(TransactionGroup left, TransactionGroup right)
                {
                    return !(left == right);
                }

            }

            public class TransactionSignatures : AVMObjectType
            {
                public ulong Nonce { get; set; }

                public Algorand.Address Address { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNonce = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vNonce.From(Nonce);
                    ret.AddRange(vNonce.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAddress = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vAddress.From(Address);
                    ret.AddRange(vAddress.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static TransactionSignatures Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new TransactionSignatures();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vNonce = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vNonce.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueNonce = vNonce.ToValue();
                    if (valueNonce is ulong vNonceValue) { ret.Nonce = vNonceValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAddress = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vAddress.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueAddress = vAddress.ToValue();
                    if (valueAddress is Algorand.Address vAddressValue) { ret.Address = vAddressValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as TransactionSignatures);
                }
                public bool Equals(TransactionSignatures? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(TransactionSignatures left, TransactionSignatures right)
                {
                    return EqualityComparer<TransactionSignatures>.Default.Equals(left, right);
                }
                public static bool operator !=(TransactionSignatures left, TransactionSignatures right)
                {
                    return !(left == right);
                }

            }

        }

        public class Events
        {
            public class TransactionAddedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 24, 73, 165, 148 };
                public const string Signature = "TransactionAdded(uint64,uint8)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong TransactionGroup { get; set; }
                public byte TransactionIndex { get; set; }

                public static TransactionAddedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new TransactionAddedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTransactionGroup = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vTransactionGroup.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTransactionGroup = vTransactionGroup.ToValue();
                    if (valueTransactionGroup is ulong vTransactionGroupValue) { ret.TransactionGroup = vTransactionGroupValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTransactionIndex = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vTransactionIndex.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTransactionIndex = vTransactionIndex.ToValue();
                    if (valueTransactionIndex is byte vTransactionIndexValue) { ret.TransactionIndex = vTransactionIndexValue; }
                    return ret;

                }

            }

            public class TransactionRemovedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 62, 155, 44, 165 };
                public const string Signature = "TransactionRemoved(uint64,uint8)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong TransactionGroup { get; set; }
                public byte TransactionIndex { get; set; }

                public static TransactionRemovedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new TransactionRemovedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTransactionGroup = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vTransactionGroup.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTransactionGroup = vTransactionGroup.ToValue();
                    if (valueTransactionGroup is ulong vTransactionGroupValue) { ret.TransactionGroup = vTransactionGroupValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTransactionIndex = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vTransactionIndex.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTransactionIndex = vTransactionIndex.ToValue();
                    if (valueTransactionIndex is byte vTransactionIndexValue) { ret.TransactionIndex = vTransactionIndexValue; }
                    return ret;

                }

            }

            public class SignatureSetEvent
            {
                public static readonly byte[] Selector = new byte[4] { 236, 251, 203, 51 };
                public const string Signature = "SignatureSet(uint64,address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong TransactionGroup { get; set; }
                public Algorand.Address Signer { get; set; }

                public static SignatureSetEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new SignatureSetEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTransactionGroup = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vTransactionGroup.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTransactionGroup = vTransactionGroup.ToValue();
                    if (valueTransactionGroup is ulong vTransactionGroupValue) { ret.TransactionGroup = vTransactionGroupValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSigner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vSigner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueSigner = vSigner.ToValue();
                    if (valueSigner is Algorand.Address vSignerValue) { ret.Signer = vSignerValue; }
                    return ret;

                }

            }

            public class SignatureClearedEvent
            {
                public static readonly byte[] Selector = new byte[4] { 133, 31, 117, 48 };
                public const string Signature = "SignatureCleared(uint64,address)";
                public static bool Matches(byte[] log) { return log != null && log.Length >= 4 && log[0] == Selector[0] && log[1] == Selector[1] && log[2] == Selector[2] && log[3] == Selector[3]; }
                public ulong TransactionGroup { get; set; }
                public Algorand.Address Signer { get; set; }

                public static SignatureClearedEvent Decode(byte[] log)
                {
                    if (!Matches(log)) throw new Exception("Log does not match event selector");
                    var ret = new SignatureClearedEvent();
                    var eventData = log.Skip(4).ToArray();
                    var queue = new Queue<byte>(eventData);
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vTransactionGroup = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vTransactionGroup.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueTransactionGroup = vTransactionGroup.ToValue();
                    if (valueTransactionGroup is ulong vTransactionGroupValue) { ret.TransactionGroup = vTransactionGroupValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSigner = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vSigner.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueSigner = vSigner.ToValue();
                    if (valueSigner is Algorand.Address vSignerValue) { ret.Signer = vSignerValue; }
                    return ret;

                }

            }

        }

        ///<summary>
        ///Retrieve the signature threshold required for the multisignature to be submitted
        ///</summary>
        public async Task<ulong> Arc55GetThreshold(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 77, 1, 200, 121 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc55GetThreshold_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 77, 1, 200, 121 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Retrieves the admin address, responsible for calling arc55_setup
        ///</summary>
        public async Task<Algorand.Address> Arc55GetAdmin(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 34, 69, 102, 175 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Arc55GetAdmin_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 34, 69, 102, 175 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<ulong> Arc55NextTransactionGroup(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 59, 221, 151, 171 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc55NextTransactionGroup_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 59, 221, 151, 171 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Retrieve a transaction from a given transaction group
        ///</summary>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="transactionIndex">Index of transaction within group </param>
        public async Task<byte[]> Arc55GetTransaction(ulong transactionGroup, byte transactionIndex, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 126, 207, 37, 192 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var transactionIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); transactionIndexAbi.From(transactionIndex);

            var result = await base.SimApp(new List<object> { abiHandle, transactionGroupAbi, transactionIndexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc55GetTransaction_Transactions(ulong transactionGroup, byte transactionIndex, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 126, 207, 37, 192 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var transactionIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); transactionIndexAbi.From(transactionIndex);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionGroupAbi, transactionIndexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Retrieve a list of signatures for a given transaction group nonce and address
        ///</summary>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="signer">Account you want to retrieve signatures for </param>
        public async Task<byte[][]> Arc55GetSignatures(ulong transactionGroup, Algorand.Address signer, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 67, 33, 129, 14 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var signerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); signerAbi.From(signer);

            var result = await base.SimApp(new List<object> { abiHandle, transactionGroupAbi, signerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            throw new Exception("Conversion not implemented"); // <unknown return conversion>

        }

        public async Task<List<Transaction>> Arc55GetSignatures_Transactions(ulong transactionGroup, Algorand.Address signer, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 67, 33, 129, 14 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var signerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); signerAbi.From(signer);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionGroupAbi, signerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Find out which address is at this index of the multisignature
        ///</summary>
        /// <param name="index">Account at this index of the multisignature </param>
        public async Task<Algorand.Address> Arc55GetSignerByIndex(ulong index, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 177, 165, 185, 167 };
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); indexAbi.From(index);

            var result = await base.SimApp(new List<object> { abiHandle, indexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Address();
            returnValueObj.Decode(lastLogReturnData);
            return new Algorand.Address(returnValueObj.ToByteArray());

        }

        public async Task<List<Transaction>> Arc55GetSignerByIndex_Transactions(ulong index, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 177, 165, 185, 167 };
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); indexAbi.From(index);

            return await base.MakeTransactionList(new List<object> { abiHandle, indexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Check if an address is a member of the multisignature
        ///</summary>
        /// <param name="address">Account to check is a signer </param>
        public async Task<bool> Arc55IsSigner(Algorand.Address address, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 200, 222, 66, 102 };
            var addressAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addressAbi.From(address);

            var result = await base.SimApp(new List<object> { abiHandle, addressAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc55IsSigner_Transactions(Algorand.Address address, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 200, 222, 66, 102 };
            var addressAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addressAbi.From(address);

            return await base.MakeTransactionList(new List<object> { abiHandle, addressAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Calculate the minimum balance requirement for storing a signature
        ///</summary>
        /// <param name="signaturesSize">Size (in bytes) of the signatures to store </param>
        public async Task<ulong> Arc55MbrSigIncrease(ulong signaturesSize, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 7, 175, 165, 202 };
            var signaturesSizeAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); signaturesSizeAbi.From(signaturesSize);

            var result = await base.SimApp(new List<object> { abiHandle, signaturesSizeAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc55MbrSigIncrease_Transactions(ulong signaturesSize, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 7, 175, 165, 202 };
            var signaturesSizeAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); signaturesSizeAbi.From(signaturesSize);

            return await base.MakeTransactionList(new List<object> { abiHandle, signaturesSizeAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Calculate the minimum balance requirement for storing a transaction
        ///</summary>
        /// <param name="transactionSize">Size (in bytes) of the transaction to store </param>
        public async Task<ulong> Arc55MbrTxnIncrease(ulong transactionSize, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 203, 73, 11, 242 };
            var transactionSizeAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionSizeAbi.From(transactionSize);

            var result = await base.SimApp(new List<object> { abiHandle, transactionSizeAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc55MbrTxnIncrease_Transactions(ulong transactionSize, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 203, 73, 11, 242 };
            var transactionSizeAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionSizeAbi.From(transactionSize);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionSizeAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Setup On-Chain Msig App. This can only be called whilst no transaction groups have been created.
        ///</summary>
        /// <param name="threshold">Initial multisig threshold, must be greater than 0 </param>
        /// <param name="addresses">Array of addresses that make up the multisig </param>
        public async Task Arc55Setup(byte threshold, Algorand.Address[] addresses, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 36, 129, 96, 87 };
            var thresholdAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); thresholdAbi.From(threshold);
            var addressesAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Address>("address"); addressesAbi.From(addresses);

            var result = await base.CallApp(new List<object> { abiHandle, thresholdAbi, addressesAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55Setup_Transactions(byte threshold, Algorand.Address[] addresses, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 36, 129, 96, 87 };
            var thresholdAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); thresholdAbi.From(threshold);
            var addressesAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Address>("address"); addressesAbi.From(addresses);

            return await base.MakeTransactionList(new List<object> { abiHandle, thresholdAbi, addressesAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Generate a new transaction group nonce for holding pending transactions
        ///</summary>
        public async Task<ulong> Arc55NewTransactionGroup(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 6, 17, 132, 34 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc55NewTransactionGroup_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 6, 17, 132, 34 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Add a transaction to an existing group. Only one transaction should be included per call
        ///</summary>
        /// <param name="costs">Minimum Balance Requirement for associated box storage costs: (2500) + (400 * (9 + transaction.length)) </param>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="index">Transaction position within atomic group to add </param>
        /// <param name="transaction">Transaction to add </param>
        public async Task Arc55AddTransaction(PaymentTransaction costs, ulong transactionGroup, byte index, byte[] transaction, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { costs });
            byte[] abiHandle = { 74, 236, 188, 163 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);
            var transactionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); transactionAbi.From(transaction);

            var result = await base.CallApp(new List<object> { abiHandle, costs, transactionGroupAbi, indexAbi, transactionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55AddTransaction_Transactions(PaymentTransaction costs, ulong transactionGroup, byte index, byte[] transaction, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { costs });
            byte[] abiHandle = { 74, 236, 188, 163 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);
            var transactionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); transactionAbi.From(transaction);

            return await base.MakeTransactionList(new List<object> { abiHandle, costs, transactionGroupAbi, indexAbi, transactionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="transaction"> </param>
        public async Task Arc55AddTransactionContinued(byte[] transaction, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 121, 136, 34, 225 };
            var transactionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); transactionAbi.From(transaction);

            var result = await base.CallApp(new List<object> { abiHandle, transactionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55AddTransactionContinued_Transactions(byte[] transaction, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 121, 136, 34, 225 };
            var transactionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); transactionAbi.From(transaction);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Remove transaction from the app. The MBR associated with the transaction will be returned to the transaction sender.
        ///</summary>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="index">Transaction position within atomic group to remove </param>
        public async Task Arc55RemoveTransaction(ulong transactionGroup, byte index, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 240, 134, 126, 241 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);

            var result = await base.CallApp(new List<object> { abiHandle, transactionGroupAbi, indexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55RemoveTransaction_Transactions(ulong transactionGroup, byte index, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 240, 134, 126, 241 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionGroupAbi, indexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Set signatures for a particular transaction group. Signatures must be included as an array of byte-arrays
        ///</summary>
        /// <param name="costs">Minimum Balance Requirement for associated box storage costs: (2500) + (400 * (40 + signatures.length)) </param>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="signatures">Array of signatures </param>
        public async Task Arc55SetSignatures(PaymentTransaction costs, ulong transactionGroup, byte[][] signatures, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { costs });
            byte[] abiHandle = { 111, 216, 222, 5 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var signaturesAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>>("byte[64]"); signaturesAbi.From(signatures);

            var result = await base.CallApp(new List<object> { abiHandle, costs, transactionGroupAbi, signaturesAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55SetSignatures_Transactions(PaymentTransaction costs, ulong transactionGroup, byte[][] signatures, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { costs });
            byte[] abiHandle = { 111, 216, 222, 5 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var signaturesAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>>("byte[64]"); signaturesAbi.From(signatures);

            return await base.MakeTransactionList(new List<object> { abiHandle, costs, transactionGroupAbi, signaturesAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Clear signatures for an address. Be aware this only removes it from the current state of the ledger, and indexers will still know and could use your signature
        ///</summary>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="address">Account whose signatures to clear </param>
        public async Task Arc55ClearSignatures(ulong transactionGroup, Algorand.Address address, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 17, 27, 137, 38 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var addressAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addressAbi.From(address);

            var result = await base.CallApp(new List<object> { abiHandle, transactionGroupAbi, addressAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55ClearSignatures_Transactions(ulong transactionGroup, Algorand.Address address, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 17, 27, 137, 38 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var addressAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addressAbi.From(address);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionGroupAbi, addressAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiTXVsdGlzaWciLCJkZXNjIjpudWxsLCJuZXR3b3JrcyI6e30sInN0cnVjdHMiOnsiVHJhbnNhY3Rpb25Hcm91cCI6W3sibmFtZSI6Im5vbmNlIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImluZGV4IiwidHlwZSI6InVpbnQ4In1dLCJUcmFuc2FjdGlvblNpZ25hdHVyZXMiOlt7Im5hbWUiOiJub25jZSIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJhZGRyZXNzIiwidHlwZSI6ImFkZHJlc3MifV19LCJNZXRob2RzIjpbeyJuYW1lIjoiYXJjNTVfZ2V0VGhyZXNob2xkIiwiZGVzYyI6IlJldHJpZXZlIHRoZSBzaWduYXR1cmUgdGhyZXNob2xkIHJlcXVpcmVkIGZvciB0aGUgbXVsdGlzaWduYXR1cmUgdG8gYmUgc3VibWl0dGVkIiwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6Ik11bHRpc2lnbmF0dXJlIHRocmVzaG9sZCJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfZ2V0QWRtaW4iLCJkZXNjIjoiUmV0cmlldmVzIHRoZSBhZG1pbiBhZGRyZXNzLCByZXNwb25zaWJsZSBmb3IgY2FsbGluZyBhcmM1NV9zZXR1cCIsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJkZXNjIjoiQWRtaW4gYWRkcmVzcyJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfbmV4dFRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjoiTmV4dCBleHBlY3RlZCBUcmFuc2FjdGlvbiBHcm91cCBub25jZSJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfZ2V0VHJhbnNhY3Rpb24iLCJkZXNjIjoiUmV0cmlldmUgYSB0cmFuc2FjdGlvbiBmcm9tIGEgZ2l2ZW4gdHJhbnNhY3Rpb24gZ3JvdXAiLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJUcmFuc2FjdGlvbiBHcm91cCBub25jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkluZGV4IiwiZGVzYyI6IkluZGV4IG9mIHRyYW5zYWN0aW9uIHdpdGhpbiBncm91cCIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJBIHNpbmdsZSB0cmFuc2FjdGlvbiBhdCB0aGUgc3BlY2lmaWVkIGluZGV4IGZvciB0aGUgdHJhbnNhY3Rpb24gZ3JvdXAgbm9uY2UifSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzU1X2dldFNpZ25hdHVyZXMiLCJkZXNjIjoiUmV0cmlldmUgYSBsaXN0IG9mIHNpZ25hdHVyZXMgZm9yIGEgZ2l2ZW4gdHJhbnNhY3Rpb24gZ3JvdXAgbm9uY2UgYW5kIGFkZHJlc3MiLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJUcmFuc2FjdGlvbiBHcm91cCBub25jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25lciIsImRlc2MiOiJBY2NvdW50IHlvdSB3YW50IHRvIHJldHJpZXZlIHNpZ25hdHVyZXMgZm9yIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbNjRdW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJBcnJheSBvZiBzaWduYXR1cmVzIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM1NV9nZXRTaWduZXJCeUluZGV4IiwiZGVzYyI6IkZpbmQgb3V0IHdoaWNoIGFkZHJlc3MgaXMgYXQgdGhpcyBpbmRleCBvZiB0aGUgbXVsdGlzaWduYXR1cmUiLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiaW5kZXgiLCJkZXNjIjoiQWNjb3VudCBhdCB0aGlzIGluZGV4IG9mIHRoZSBtdWx0aXNpZ25hdHVyZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJkZXNjIjoiQWNjb3VudCBhdCBpbmRleCJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfaXNTaWduZXIiLCJkZXNjIjoiQ2hlY2sgaWYgYW4gYWRkcmVzcyBpcyBhIG1lbWJlciBvZiB0aGUgbXVsdGlzaWduYXR1cmUiLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImFkZHJlc3MiLCJkZXNjIjoiQWNjb3VudCB0byBjaGVjayBpcyBhIHNpZ25lciIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjoiVHJ1ZSBpZiBhZGRyZXNzIGlzIGEgc2lnbmVyIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM1NV9tYnJTaWdJbmNyZWFzZSIsImRlc2MiOiJDYWxjdWxhdGUgdGhlIG1pbmltdW0gYmFsYW5jZSByZXF1aXJlbWVudCBmb3Igc3RvcmluZyBhIHNpZ25hdHVyZSIsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzaWduYXR1cmVzU2l6ZSIsImRlc2MiOiJTaXplIChpbiBieXRlcykgb2YgdGhlIHNpZ25hdHVyZXMgdG8gc3RvcmUiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjoiTWluaW11bSBiYWxhbmNlIHJlcXVpcmVtZW50IGluY3JlYXNlIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM1NV9tYnJUeG5JbmNyZWFzZSIsImRlc2MiOiJDYWxjdWxhdGUgdGhlIG1pbmltdW0gYmFsYW5jZSByZXF1aXJlbWVudCBmb3Igc3RvcmluZyBhIHRyYW5zYWN0aW9uIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uU2l6ZSIsImRlc2MiOiJTaXplIChpbiBieXRlcykgb2YgdGhlIHRyYW5zYWN0aW9uIHRvIHN0b3JlIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6Ik1pbmltdW0gYmFsYW5jZSByZXF1aXJlbWVudCBpbmNyZWFzZSJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfc2V0dXAiLCJkZXNjIjoiU2V0dXAgT24tQ2hhaW4gTXNpZyBBcHAuIFRoaXMgY2FuIG9ubHkgYmUgY2FsbGVkIHdoaWxzdCBubyB0cmFuc2FjdGlvbiBncm91cHMgaGF2ZSBiZWVuIGNyZWF0ZWQuIiwiYXJncyI6W3sidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidGhyZXNob2xkIiwiZGVzYyI6IkluaXRpYWwgbXVsdGlzaWcgdGhyZXNob2xkLCBtdXN0IGJlIGdyZWF0ZXIgdGhhbiAwIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyZXNzZXMiLCJkZXNjIjoiQXJyYXkgb2YgYWRkcmVzc2VzIHRoYXQgbWFrZSB1cCB0aGUgbXVsdGlzaWciLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfbmV3VHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJHZW5lcmF0ZSBhIG5ldyB0cmFuc2FjdGlvbiBncm91cCBub25jZSBmb3IgaG9sZGluZyBwZW5kaW5nIHRyYW5zYWN0aW9ucyIsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJ0cmFuc2FjdGlvbkdyb3VwIFRyYW5zYWN0aW9uIEdyb3VwIG5vbmNlIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfYWRkVHJhbnNhY3Rpb24iLCJkZXNjIjoiQWRkIGEgdHJhbnNhY3Rpb24gdG8gYW4gZXhpc3RpbmcgZ3JvdXAuIE9ubHkgb25lIHRyYW5zYWN0aW9uIHNob3VsZCBiZSBpbmNsdWRlZCBwZXIgY2FsbCIsImFyZ3MiOlt7InR5cGUiOiJwYXkiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJjb3N0cyIsImRlc2MiOiJNaW5pbXVtIEJhbGFuY2UgUmVxdWlyZW1lbnQgZm9yIGFzc29jaWF0ZWQgYm94IHN0b3JhZ2UgY29zdHM6ICgyNTAwKSArICg0MDAgKiAoOSArIHRyYW5zYWN0aW9uLmxlbmd0aCkpIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6IlRyYW5zYWN0aW9uIEdyb3VwIG5vbmNlIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImluZGV4IiwiZGVzYyI6IlRyYW5zYWN0aW9uIHBvc2l0aW9uIHdpdGhpbiBhdG9taWMgZ3JvdXAgdG8gYWRkIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbiIsImRlc2MiOiJUcmFuc2FjdGlvbiB0byBhZGQiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IlRyYW5zYWN0aW9uQWRkZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25JbmRleCIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzU1X2FkZFRyYW5zYWN0aW9uQ29udGludWVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzU1X3JlbW92ZVRyYW5zYWN0aW9uIiwiZGVzYyI6IlJlbW92ZSB0cmFuc2FjdGlvbiBmcm9tIHRoZSBhcHAuIFRoZSBNQlIgYXNzb2NpYXRlZCB3aXRoIHRoZSB0cmFuc2FjdGlvbiB3aWxsIGJlIHJldHVybmVkIHRvIHRoZSB0cmFuc2FjdGlvbiBzZW5kZXIuIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjoiVHJhbnNhY3Rpb24gR3JvdXAgbm9uY2UiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiaW5kZXgiLCJkZXNjIjoiVHJhbnNhY3Rpb24gcG9zaXRpb24gd2l0aGluIGF0b21pYyBncm91cCB0byByZW1vdmUiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IlRyYW5zYWN0aW9uUmVtb3ZlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkluZGV4IiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfc2V0U2lnbmF0dXJlcyIsImRlc2MiOiJTZXQgc2lnbmF0dXJlcyBmb3IgYSBwYXJ0aWN1bGFyIHRyYW5zYWN0aW9uIGdyb3VwLiBTaWduYXR1cmVzIG11c3QgYmUgaW5jbHVkZWQgYXMgYW4gYXJyYXkgb2YgYnl0ZS1hcnJheXMiLCJhcmdzIjpbeyJ0eXBlIjoicGF5Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiY29zdHMiLCJkZXNjIjoiTWluaW11bSBCYWxhbmNlIFJlcXVpcmVtZW50IGZvciBhc3NvY2lhdGVkIGJveCBzdG9yYWdlIGNvc3RzOiAoMjUwMCkgKyAoNDAwICogKDQwICsgc2lnbmF0dXJlcy5sZW5ndGgpKSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJUcmFuc2FjdGlvbiBHcm91cCBub25jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVs2NF1bXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25hdHVyZXMiLCJkZXNjIjoiQXJyYXkgb2Ygc2lnbmF0dXJlcyIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiU2lnbmF0dXJlU2V0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVyIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfY2xlYXJTaWduYXR1cmVzIiwiZGVzYyI6IkNsZWFyIHNpZ25hdHVyZXMgZm9yIGFuIGFkZHJlc3MuIEJlIGF3YXJlIHRoaXMgb25seSByZW1vdmVzIGl0IGZyb20gdGhlIGN1cnJlbnQgc3RhdGUgb2YgdGhlIGxlZGdlciwgYW5kIGluZGV4ZXJzIHdpbGwgc3RpbGwga25vdyBhbmQgY291bGQgdXNlIHlvdXIgc2lnbmF0dXJlIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjoiVHJhbnNhY3Rpb24gR3JvdXAgbm9uY2UiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyZXNzIiwiZGVzYyI6IkFjY291bnQgd2hvc2Ugc2lnbmF0dXJlcyB0byBjbGVhciIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiU2lnbmF0dXJlQ2xlYXJlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25lciIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19XSwic3RhdGUiOnsic2NoZW1hIjp7Imdsb2JhbCI6eyJpbnRzIjo2LCJieXRlcyI6Nn0sImxvY2FsIjp7ImludHMiOjEsImJ5dGVzIjowfX0sImtleXMiOnsiZ2xvYmFsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJib3giOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9fSwibWFwcyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImxvY2FsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9LCJib3giOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH19fSwiYmFyZUFjdGlvbnMiOnsiY3JlYXRlIjpbIk5vT3AiXSwiY2FsbCI6W119LCJzb3VyY2VJbmZvIjp7ImFwcHJvdmFsIjp7InNvdXJjZUluZm8iOlt7InBjIjpbMjcxLDMwOCw4NTMsMTAxMl0sImVycm9yTWVzc2FnZSI6IkJveCBtdXN0IGhhdmUgdmFsdWUiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls4Ml0sImVycm9yTWVzc2FnZSI6Ik9uQ29tcGxldGlvbiBtdXN0IGJlIE5vT3AiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyMTJdLCJlcnJvck1lc3NhZ2UiOiJPbkNvbXBsZXRpb24gbXVzdCBiZSBOb09wICYmIGNhbiBvbmx5IGNhbGwgd2hlbiBjcmVhdGluZyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIxOCwyMjksMjM5LDMyOCwzNDYsNDM2LDQ1Miw0OTMsNTY5LDYwNSw2ODIsMTA1NSwxMDcxXSwiZXJyb3JNZXNzYWdlIjoiY2hlY2sgR2xvYmFsU3RhdGUgZXhpc3RzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTQxXSwiZXJyb3JNZXNzYWdlIjoiaW5kZXggYWNjZXNzIGlzIG91dCBvZiBib3VuZHMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MjAsNjU0LDgwMCw5MTZdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIGFycmF5IGxlbmd0aCBoZWFkZXIiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MzJdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PGFjY291bnQ+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjYyLDgwN10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LmR5bmFtaWNfYXJyYXk8YXJjNC51aW50OD4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls5MjhdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PGJ5dGVzWzY0XT4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlszMDIsMzQyLDk5NF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnN0YXRpY19hcnJheTxhcmM0LnVpbnQ4LCAzMj4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyNTYsMjkzLDMyMiwzNjksMzg4LDYzOSw4MjYsOTA5LDk4NV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQ2NCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI2NSw0MTMsNjQ4LDgzNV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQ4IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNzQ4XSwiZXJyb3JNZXNzYWdlIjoidHJhbnNhY3Rpb24gdHlwZSBpcyBhcHBsIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjMxLDkwMV0sImVycm9yTWVzc2FnZSI6InRyYW5zYWN0aW9uIHR5cGUgaXMgcGF5IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfV0sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9LCJjbGVhciI6eyJzb3VyY2VJbmZvIjpbXSwicGNPZmZzZXRNZXRob2QiOiJub25lIn19LCJzb3VyY2UiOnsiYXBwcm92YWwiOiJJM0J5WVdkdFlTQjJaWEp6YVc5dUlERXhDaU53Y21GbmJXRWdkSGx3WlhSeVlXTnJJR1poYkhObENnb3ZMeUJBWVd4bmIzSmhibVJtYjNWdVpHRjBhVzl1TDJGc1oyOXlZVzVrTFhSNWNHVnpZM0pwY0hRdllYSmpOQzlwYm1SbGVDNWtMblJ6T2pwRGIyNTBjbUZqZEM1aGNIQnliM1poYkZCeWIyZHlZVzBvS1NBdFBpQjFhVzUwTmpRNkNtMWhhVzQ2Q2lBZ0lDQnBiblJqWW14dlkyc2dNQ0F4SURnZ016SWdOREF3SURJMU1EQUtJQ0FnSUdKNWRHVmpZbXh2WTJzZ01IZ3hOVEZtTjJNM05TQWlZWEpqTlRWZmJtOXVZMlVpSUNKaGNtTTFOVjloWkcxcGJpSWdJbUZ5WXpVMVgzUm9jbVZ6YUc5c1pDSWdNSGczT1RnNE1qSmxNUW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0p1ZWlCdFlXbHVYMkZtZEdWeVgybG1YMlZzYzJWQU1nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TmpnS0lDQWdJQzh2SUdGeVl6VTFYM1JvY21WemFHOXNaQ0E5SUVkc2IySmhiRk4wWVhSbFBIVnBiblEyTkQ0b2V5QnBibWwwYVdGc1ZtRnNkV1U2SUZWcGJuUTJOQ2d3S1NCOUtUc0tJQ0FnSUdKNWRHVmpYek1nTHk4Z0ltRnlZelUxWDNSb2NtVnphRzlzWkNJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOekVLSUNBZ0lDOHZJR0Z5WXpVMVgyNXZibU5sSUQwZ1IyeHZZbUZzVTNSaGRHVThkV2x1ZERZMFBpaDdJR2x1YVhScFlXeFdZV3gxWlRvZ1ZXbHVkRFkwS0RBcElIMHBPd29nSUNBZ1lubDBaV05mTVNBdkx5QWlZWEpqTlRWZmJtOXVZMlVpQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLQ20xaGFXNWZZV1owWlhKZmFXWmZaV3h6WlVBeU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TlRVdE5qSUtJQ0FnSUM4dklFQmpiMjUwY21GamRDaDdDaUFnSUNBdkx5QWdJSE4wWVhSbFZHOTBZV3h6T2lCN0NpQWdJQ0F2THlBZ0lDQWdaMnh2WW1Gc1ZXbHVkSE02SURZc0NpQWdJQ0F2THlBZ0lDQWdaMnh2WW1Gc1FubDBaWE02SURZc0NpQWdJQ0F2THlBZ0lDQWdiRzlqWVd4VmFXNTBjem9nTVN3S0lDQWdJQzh2SUNBZ0lDQnNiMk5oYkVKNWRHVnpPaUF3TEFvZ0lDQWdMeThnSUNCOUxBb2dJQ0FnTHk4Z2ZTbGxlSEJ2Y25RZ1kyeGhjM01nVFhWc2RHbHphV2NnWlhoMFpXNWtjeUJEYjI1MGNtRmpkQ0I3Q2lBZ0lDQjBlRzRnVG5WdFFYQndRWEpuY3dvZ0lDQWdZbm9nYldGcGJsOWZYMkZzWjI5MGMxOWZMbVJsWm1GMWJIUkRjbVZoZEdWQU1qVUtJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCUGJrTnZiWEJzWlhScGIyNGdiWFZ6ZENCaVpTQk9iMDl3Q2lBZ0lDQjBlRzRnUVhCd2JHbGpZWFJwYjI1SlJBb2dJQ0FnWVhOelpYSjBDaUFnSUNCd2RYTm9ZbmwwWlhOeklEQjROR1F3TVdNNE56a2dNSGd5TWpRMU5qWmhaaUF3ZUROaVpHUTVOMkZpSURCNE4yVmpaakkxWXpBZ01IZzBNekl4T0RFd1pTQXdlR0l4WVRWaU9XRTNJREI0WXpoa1pUUXlOallnTUhnd04yRm1ZVFZqWVNBd2VHTmlORGt3WW1ZeUlEQjRNalE0TVRZd05UY2dNSGd3TmpFeE9EUXlNaUF3ZURSaFpXTmlZMkV6SUM4dklHMWxkR2h2WkNBaVlYSmpOVFZmWjJWMFZHaHlaWE5vYjJ4a0tDbDFhVzUwTmpRaUxDQnRaWFJvYjJRZ0ltRnlZelUxWDJkbGRFRmtiV2x1S0NsaFpHUnlaWE56SWl3Z2JXVjBhRzlrSUNKaGNtTTFOVjl1WlhoMFZISmhibk5oWTNScGIyNUhjbTkxY0NncGRXbHVkRFkwSWl3Z2JXVjBhRzlrSUNKaGNtTTFOVjluWlhSVWNtRnVjMkZqZEdsdmJpaDFhVzUwTmpRc2RXbHVkRGdwWW5sMFpWdGRJaXdnYldWMGFHOWtJQ0poY21NMU5WOW5aWFJUYVdkdVlYUjFjbVZ6S0hWcGJuUTJOQ3hoWkdSeVpYTnpLV0o1ZEdWYk5qUmRXMTBpTENCdFpYUm9iMlFnSW1GeVl6VTFYMmRsZEZOcFoyNWxja0o1U1c1a1pYZ29kV2x1ZERZMEtXRmtaSEpsYzNNaUxDQnRaWFJvYjJRZ0ltRnlZelUxWDJselUybG5ibVZ5S0dGa1pISmxjM01wWW05dmJDSXNJRzFsZEdodlpDQWlZWEpqTlRWZmJXSnlVMmxuU1c1amNtVmhjMlVvZFdsdWREWTBLWFZwYm5RMk5DSXNJRzFsZEdodlpDQWlZWEpqTlRWZmJXSnlWSGh1U1c1amNtVmhjMlVvZFdsdWREWTBLWFZwYm5RMk5DSXNJRzFsZEdodlpDQWlZWEpqTlRWZmMyVjBkWEFvZFdsdWREZ3NZV1JrY21WemMxdGRLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltRnlZelUxWDI1bGQxUnlZVzV6WVdOMGFXOXVSM0p2ZFhBb0tYVnBiblEyTkNJc0lHMWxkR2h2WkNBaVlYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNG9jR0Y1TEhWcGJuUTJOQ3gxYVc1ME9DeGllWFJsVzEwcGRtOXBaQ0lLSUNBZ0lHSjVkR1ZqSURRZ0x5OGdiV1YwYUc5a0lDSmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZia052Ym5ScGJuVmxaQ2hpZVhSbFcxMHBkbTlwWkNJS0lDQWdJSEIxYzJoaWVYUmxjM01nTUhobU1EZzJOMlZtTVNBd2VEWm1aRGhrWlRBMUlEQjRNVEV4WWpnNU1qWWdMeThnYldWMGFHOWtJQ0poY21NMU5WOXlaVzF2ZG1WVWNtRnVjMkZqZEdsdmJpaDFhVzUwTmpRc2RXbHVkRGdwZG05cFpDSXNJRzFsZEdodlpDQWlZWEpqTlRWZmMyVjBVMmxuYm1GMGRYSmxjeWh3WVhrc2RXbHVkRFkwTEdKNWRHVmJOalJkVzEwcGRtOXBaQ0lzSUcxbGRHaHZaQ0FpWVhKak5UVmZZMnhsWVhKVGFXZHVZWFIxY21WektIVnBiblEyTkN4aFpHUnlaWE56S1hadmFXUWlDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXdDaUFnSUNCdFlYUmphQ0JoY21NMU5WOW5aWFJVYUhKbGMyaHZiR1FnWVhKak5UVmZaMlYwUVdSdGFXNGdZWEpqTlRWZmJtVjRkRlJ5WVc1ellXTjBhVzl1UjNKdmRYQWdZWEpqTlRWZloyVjBWSEpoYm5OaFkzUnBiMjRnWVhKak5UVmZaMlYwVTJsbmJtRjBkWEpsY3lCaGNtTTFOVjluWlhSVGFXZHVaWEpDZVVsdVpHVjRJR0Z5WXpVMVgybHpVMmxuYm1WeUlHRnlZelUxWDIxaWNsTnBaMGx1WTNKbFlYTmxJR0Z5WXpVMVgyMWljbFI0YmtsdVkzSmxZWE5sSUdGeVl6VTFYM05sZEhWd0lHRnlZelUxWDI1bGQxUnlZVzV6WVdOMGFXOXVSM0p2ZFhBZ1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNGdZWEpqTlRWZllXUmtWSEpoYm5OaFkzUnBiMjVEYjI1MGFXNTFaV1FnWVhKak5UVmZjbVZ0YjNabFZISmhibk5oWTNScGIyNGdZWEpqTlRWZmMyVjBVMmxuYm1GMGRYSmxjeUJoY21NMU5WOWpiR1ZoY2xOcFoyNWhkSFZ5WlhNS0lDQWdJR1Z5Y2dvS2JXRnBibDlmWDJGc1oyOTBjMTlmTG1SbFptRjFiSFJEY21WaGRHVkFNalU2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFOUzAyTWdvZ0lDQWdMeThnUUdOdmJuUnlZV04wS0hzS0lDQWdJQzh2SUNBZ2MzUmhkR1ZVYjNSaGJITTZJSHNLSUNBZ0lDOHZJQ0FnSUNCbmJHOWlZV3hWYVc1MGN6b2dOaXdLSUNBZ0lDOHZJQ0FnSUNCbmJHOWlZV3hDZVhSbGN6b2dOaXdLSUNBZ0lDOHZJQ0FnSUNCc2IyTmhiRlZwYm5Sek9pQXhMQW9nSUNBZ0x5OGdJQ0FnSUd4dlkyRnNRbmwwWlhNNklEQXNDaUFnSUNBdkx5QWdJSDBzQ2lBZ0lDQXZMeUI5S1dWNGNHOXlkQ0JqYkdGemN5Qk5kV3gwYVhOcFp5QmxlSFJsYm1SeklFTnZiblJ5WVdOMElIc0tJQ0FnSUhSNGJpQlBia052YlhCc1pYUnBiMjRLSUNBZ0lDRUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQWhDaUFnSUNBbUpnb2dJQ0FnWVhOelpYSjBJQzh2SUU5dVEyOXRjR3hsZEdsdmJpQnRkWE4wSUdKbElFNXZUM0FnSmlZZ1kyRnVJRzl1YkhrZ1kyRnNiQ0IzYUdWdUlHTnlaV0YwYVc1bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyZGxkRlJvY21WemFHOXNaRnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6VTFYMmRsZEZSb2NtVnphRzlzWkRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV6T0FvZ0lDQWdMeThnY21WMGRYSnVJSFJvYVhNdVlYSmpOVFZmZEdoeVpYTm9iMnhrTG5aaGJIVmxPd29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pZNENpQWdJQ0F2THlCaGNtTTFOVjkwYUhKbGMyaHZiR1FnUFNCSGJHOWlZV3hUZEdGMFpUeDFhVzUwTmpRK0tIc2dhVzVwZEdsaGJGWmhiSFZsT2lCVmFXNTBOalFvTUNrZ2ZTazdDaUFnSUNCaWVYUmxZMTh6SUM4dklDSmhjbU0xTlY5MGFISmxjMmh2YkdRaUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TXpnS0lDQWdJQzh2SUhKbGRIVnliaUIwYUdsekxtRnlZelUxWDNSb2NtVnphRzlzWkM1MllXeDFaVHNLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2dSMnh2WW1Gc1UzUmhkR1VnWlhocGMzUnpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE16WXRNVE0zQ2lBZ0lDQXZMeUJBY21WaFpHOXViSGtLSUNBZ0lDOHZJR0Z5WXpVMVgyZGxkRlJvY21WemFHOXNaQ2dwT2lCMWFXNTBOalFnZXdvZ0lDQWdhWFJ2WWdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcE5kV3gwYVhOcFp5NWhjbU0xTlY5blpYUkJaRzFwYmx0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZelUxWDJkbGRFRmtiV2x1T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFEzQ2lBZ0lDQXZMeUJ5WlhSMWNtNGdkR2hwY3k1aGNtTTFOVjloWkcxcGJpNTJZV3gxWlRzS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNOQW9nSUNBZ0x5OGdZWEpqTlRWZllXUnRhVzRnUFNCSGJHOWlZV3hUZEdGMFpUeEJZMk52ZFc1MFBpaDdmU2s3Q2lBZ0lDQmllWFJsWTE4eUlDOHZJQ0poY21NMU5WOWhaRzFwYmlJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakUwTndvZ0lDQWdMeThnY21WMGRYSnVJSFJvYVhNdVlYSmpOVFZmWVdSdGFXNHVkbUZzZFdVN0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFExTFRFME5nb2dJQ0FnTHk4Z1FISmxZV1J2Ym14NUNpQWdJQ0F2THlCaGNtTTFOVjluWlhSQlpHMXBiaWdwT2lCQlkyTnZkVzUwSUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZUWFZzZEdsemFXY3VZWEpqTlRWZmJtVjRkRlJ5WVc1ellXTjBhVzl1UjNKdmRYQmJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3BoY21NMU5WOXVaWGgwVkhKaGJuTmhZM1JwYjI1SGNtOTFjRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTFOZ29nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11WVhKak5UVmZibTl1WTJVdWRtRnNkV1VnS3lBeE93b2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPamN4Q2lBZ0lDQXZMeUJoY21NMU5WOXViMjVqWlNBOUlFZHNiMkpoYkZOMFlYUmxQSFZwYm5RMk5ENG9leUJwYm1sMGFXRnNWbUZzZFdVNklGVnBiblEyTkNnd0tTQjlLVHNLSUNBZ0lHSjVkR1ZqWHpFZ0x5OGdJbUZ5WXpVMVgyNXZibU5sSWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFUyQ2lBZ0lDQXZMeUJ5WlhSMWNtNGdkR2hwY3k1aGNtTTFOVjl1YjI1alpTNTJZV3gxWlNBcklERTdDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJQ3NLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTFOQzB4TlRVS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZibVY0ZEZSeVlXNXpZV04wYVc5dVIzSnZkWEFvS1RvZ2RXbHVkRFkwSUhzS0lDQWdJR2wwYjJJS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZUWFZzZEdsemFXY3VZWEpqTlRWZloyVjBWSEpoYm5OaFkzUnBiMjViY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTFOVjluWlhSVWNtRnVjMkZqZEdsdmJqb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMk5TMHhOallLSUNBZ0lDOHZJRUJ5WldGa2IyNXNlUW9nSUNBZ0x5OGdZWEpqTlRWZloyVjBWSEpoYm5OaFkzUnBiMjRvZEhKaGJuTmhZM1JwYjI1SGNtOTFjRG9nZFdsdWREWTBMQ0IwY21GdWMyRmpkR2x2YmtsdVpHVjRPaUJWYVc1ME9DazZJR0o1ZEdWeklIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnT0FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBOalFLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwT0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFkzTFRFM01Bb2dJQ0FnTHk4Z1kyOXVjM1FnZEhKaGJuTmhZM1JwYjI1Q2IzZzZJRlJ5WVc1ellXTjBhVzl1UjNKdmRYQWdQU0I3Q2lBZ0lDQXZMeUFnSUc1dmJtTmxPaUIwY21GdWMyRmpkR2x2YmtkeWIzVndMQW9nSUNBZ0x5OGdJQ0JwYm1SbGVEb2dkSEpoYm5OaFkzUnBiMjVKYm1SbGVBb2dJQ0FnTHk4Z2ZUc0tJQ0FnSUhOM1lYQUtJQ0FnSUdsMGIySUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UY3lDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTVoY21NMU5WOTBjbUZ1YzJGamRHbHZibk1vZEhKaGJuTmhZM1JwYjI1Q2IzZ3BMblpoYkhWbE93b2dJQ0FnWW05NFgyZGxkQW9nSUNBZ1lYTnpaWEowSUM4dklFSnZlQ0J0ZFhOMElHaGhkbVVnZG1Gc2RXVUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMk5TMHhOallLSUNBZ0lDOHZJRUJ5WldGa2IyNXNlUW9nSUNBZ0x5OGdZWEpqTlRWZloyVjBWSEpoYm5OaFkzUnBiMjRvZEhKaGJuTmhZM1JwYjI1SGNtOTFjRG9nZFdsdWREWTBMQ0IwY21GdWMyRmpkR2x2YmtsdVpHVjRPaUJWYVc1ME9DazZJR0o1ZEdWeklIc0tJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwZEc5aUNpQWdJQ0JsZUhSeVlXTjBJRFlnTWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyZGxkRk5wWjI1aGRIVnlaWE5iY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTFOVjluWlhSVGFXZHVZWFIxY21Wek9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRneExURTRNZ29nSUNBZ0x5OGdRSEpsWVdSdmJteDVDaUFnSUNBdkx5QmhjbU0xTlY5blpYUlRhV2R1WVhSMWNtVnpLSFJ5WVc1ellXTjBhVzl1UjNKdmRYQTZJSFZwYm5RMk5Dd2djMmxuYm1WeU9pQkJZMk52ZFc1MEtUb2dZbmwwWlhNOE5qUStXMTBnZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTWlBdkx5QTRDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RMk5Bb2dJQ0FnWW5SdmFRb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01nb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk15QXZMeUF6TWdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1emRHRjBhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPQ3dnTXpJK0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94T0RNdE1UZzJDaUFnSUNBdkx5QmpiMjV6ZENCemFXZHVZWFIxY21WQ2IzZzZJRlJ5WVc1ellXTjBhVzl1VTJsbmJtRjBkWEpsY3lBOUlIc0tJQ0FnSUM4dklDQWdibTl1WTJVNklIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBc0NpQWdJQ0F2THlBZ0lHRmtaSEpsYzNNNklITnBaMjVsY2dvZ0lDQWdMeThnZlRzS0lDQWdJSE4zWVhBS0lDQWdJR2wwYjJJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRnNENpQWdJQ0F2THlCeVpYUjFjbTRnZEdocGN5NWhjbU0xTlY5emFXZHVZWFIxY21WektITnBaMjVoZEhWeVpVSnZlQ2t1ZG1Gc2RXVTdDaUFnSUNCaWIzaGZaMlYwQ2lBZ0lDQmhjM05sY25RZ0x5OGdRbTk0SUcxMWMzUWdhR0YyWlNCMllXeDFaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UZ3hMVEU0TWdvZ0lDQWdMeThnUUhKbFlXUnZibXg1Q2lBZ0lDQXZMeUJoY21NMU5WOW5aWFJUYVdkdVlYUjFjbVZ6S0hSeVlXNXpZV04wYVc5dVIzSnZkWEE2SUhWcGJuUTJOQ3dnYzJsbmJtVnlPaUJCWTJOdmRXNTBLVG9nWW5sMFpYTThOalErVzEwZ2V3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBOZFd4MGFYTnBaeTVoY21NMU5WOW5aWFJUYVdkdVpYSkNlVWx1WkdWNFczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTlRWZloyVjBVMmxuYm1WeVFubEpibVJsZURvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakU1TmkweE9UY0tJQ0FnSUM4dklFQnlaV0ZrYjI1c2VRb2dJQ0FnTHk4Z1lYSmpOVFZmWjJWMFUybG5ibVZ5UW5sSmJtUmxlQ2hwYm1SbGVEb2dkV2x1ZERZMEtUb2dRV05qYjNWdWRDQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURnS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkRFkwQ2lBZ0lDQmlkRzlwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzVNd29nSUNBZ0x5OGdjbVYwZFhKdUlFZHNiMkpoYkZOMFlYUmxQRUZqWTI5MWJuUStLSHNnYTJWNU9pQnZjQzVwZEc5aUtHbHVaR1Y0S1NCOUtRb2dJQ0FnYVhSdllnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRrNENpQWdJQ0F2THlCeVpYUjFjbTRnZEdocGN5NWhjbU0xTlY5cGJtUmxlRlJ2UVdSa2NtVnpjeWhwYm1SbGVDa3VkbUZzZFdVN0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdjM2RoY0FvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJIYkc5aVlXeFRkR0YwWlNCbGVHbHpkSE1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTVOaTB4T1RjS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZaMlYwVTJsbmJtVnlRbmxKYm1SbGVDaHBibVJsZURvZ2RXbHVkRFkwS1RvZ1FXTmpiM1Z1ZENCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgybHpVMmxuYm1WeVczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTlRWZmFYTlRhV2R1WlhJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TURZdE1qQTNDaUFnSUNBdkx5QkFjbVZoWkc5dWJIa0tJQ0FnSUM4dklHRnlZelUxWDJselUybG5ibVZ5S0dGa1pISmxjM002SUVGalkyOTFiblFwT2lCaWIyOXNaV0Z1SUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpNZ0x5OGdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1YzNSaGRHbGpYMkZ5Y21GNVBHRnlZelF1ZFdsdWREZ3NJRE15UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNakE0Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdkR2hwY3k1aGNtTTFOVjloWkdSeVpYTnpRMjkxYm5Rb1lXUmtjbVZ6Y3lrdWRtRnNkV1VnSVQwOUlEQTdDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnYzNkaGNBb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQWhQUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qQTJMVEl3TndvZ0lDQWdMeThnUUhKbFlXUnZibXg1Q2lBZ0lDQXZMeUJoY21NMU5WOXBjMU5wWjI1bGNpaGhaR1J5WlhOek9pQkJZMk52ZFc1MEtUb2dZbTl2YkdWaGJpQjdDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZ3dNQW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUhWdVkyOTJaWElnTWdvZ0lDQWdjMlYwWW1sMENpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyMWljbE5wWjBsdVkzSmxZWE5sVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpOVFZmYldKeVUybG5TVzVqY21WaGMyVTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU1UWXRNakUzQ2lBZ0lDQXZMeUJBY21WaFpHOXViSGtLSUNBZ0lDOHZJR0Z5WXpVMVgyMWljbE5wWjBsdVkzSmxZWE5sS0hOcFoyNWhkSFZ5WlhOVGFYcGxPaUIxYVc1ME5qUXBPaUIxYVc1ME5qUWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNaUF2THlBNENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblEyTkFvZ0lDQWdZblJ2YVFvZ0lDQWdZMkZzYkhOMVlpQnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlRYVnNkR2x6YVdjdVlYSmpOVFZmYldKeVUybG5TVzVqY21WaGMyVUtJQ0FnSUdsMGIySUtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82VFhWc2RHbHphV2N1WVhKak5UVmZiV0p5VkhodVNXNWpjbVZoYzJWYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0xTlY5dFluSlVlRzVKYm1OeVpXRnpaVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXpPUzB5TkRBS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZiV0p5VkhodVNXNWpjbVZoYzJVb2RISmhibk5oWTNScGIyNVRhWHBsT2lCMWFXNTBOalFwT2lCMWFXNTBOalFnZXdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTWlBdkx5QTRDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RMk5Bb2dJQ0FnWW5SdmFRb2dJQ0FnWTJGc2JITjFZaUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZUWFZzZEdsemFXY3VZWEpqTlRWZmJXSnlWSGh1U1c1amNtVmhjMlVLSUNBZ0lHbDBiMklLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlRYVnNkR2x6YVdjdVlYSmpOVFZmYzJWMGRYQmJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3BoY21NMU5WOXpaWFIxY0RvS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmtkWEJ1SURJS0lDQWdJSEIxYzJoaWVYUmxjeUFpSWdvZ0lDQWdaSFZ3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lOelV0TWpjNENpQWdJQ0F2THlCaGNtTTFOVjl6WlhSMWNDZ0tJQ0FnSUM4dklDQWdkR2h5WlhOb2IyeGtPaUJWYVc1ME9Dd0tJQ0FnSUM4dklDQWdZV1JrY21WemMyVnpPaUJCWTJOdmRXNTBXMTBLSUNBZ0lDOHZJQ2s2SUhadmFXUWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblE0Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF5Q2lBZ0lDQmtkWEJ1SURJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmxlSFJ5WVdOMFgzVnBiblF4TmlBdkx5QnZiaUJsY25KdmNqb2dhVzUyWVd4cFpDQmhjbkpoZVNCc1pXNW5kR2dnYUdWaFpHVnlDaUFnSUNCa2RYQUtJQ0FnSUdOdmRtVnlJRElLSUNBZ0lHbHVkR05mTXlBdkx5QXpNZ29nSUNBZ0tnb2dJQ0FnY0hWemFHbHVkQ0F5SUM4dklESUtJQ0FnSUNzS0lDQWdJSE4zWVhBS0lDQWdJR3hsYmdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1a2VXNWhiV2xqWDJGeWNtRjVQR0ZqWTI5MWJuUStDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU56a0tJQ0FnSUM4dklHRnpjMlZ5ZENnaGRHaHBjeTVoY21NMU5WOXViMjVqWlM1MllXeDFaU2s3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk56RUtJQ0FnSUM4dklHRnlZelUxWDI1dmJtTmxJRDBnUjJ4dlltRnNVM1JoZEdVOGRXbHVkRFkwUGloN0lHbHVhWFJwWVd4V1lXeDFaVG9nVldsdWREWTBLREFwSUgwcE93b2dJQ0FnWW5sMFpXTmZNU0F2THlBaVlYSmpOVFZmYm05dVkyVWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU56a0tJQ0FnSUM4dklHRnpjMlZ5ZENnaGRHaHBjeTVoY21NMU5WOXViMjVqWlM1MllXeDFaU2s3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ0lRb2dJQ0FnWVhOelpYSjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE1URUtJQ0FnSUM4dklHbG1JQ2gwYUdsekxtRnlZelUxWDJGa2JXbHVMbWhoYzFaaGJIVmxLU0I3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk56UUtJQ0FnSUM4dklHRnlZelUxWDJGa2JXbHVJRDBnUjJ4dlltRnNVM1JoZEdVOFFXTmpiM1Z1ZEQ0b2UzMHBPd29nSUNBZ1lubDBaV05mTWlBdkx5QWlZWEpqTlRWZllXUnRhVzRpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hNVEVLSUNBZ0lDOHZJR2xtSUNoMGFHbHpMbUZ5WXpVMVgyRmtiV2x1TG1oaGMxWmhiSFZsS1NCN0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1luVnllU0F4Q2lBZ0lDQmllaUJoY21NMU5WOXpaWFIxY0Y5bGJITmxYMkp2WkhsQU1qWUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFeE1nb2dJQ0FnTHk4Z1lYTnpaWEowS0ZSNGJpNXpaVzVrWlhJZ1BUMDlJSFJvYVhNdVlYSmpOVFZmWVdSdGFXNHVkbUZzZFdVcE93b2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qYzBDaUFnSUNBdkx5QmhjbU0xTlY5aFpHMXBiaUE5SUVkc2IySmhiRk4wWVhSbFBFRmpZMjkxYm5RK0tIdDlLVHNLSUNBZ0lHSjVkR1ZqWHpJZ0x5OGdJbUZ5WXpVMVgyRmtiV2x1SWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVEV5Q2lBZ0lDQXZMeUJoYzNObGNuUW9WSGh1TG5ObGJtUmxjaUE5UFQwZ2RHaHBjeTVoY21NMU5WOWhaRzFwYmk1MllXeDFaU2s3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQW9LWVhKak5UVmZjMlYwZFhCZllXWjBaWEpmYVdaZlpXeHpaVUF5TnpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakk0TWdvZ0lDQWdMeThnWVhOelpYSjBLSFJvY21WemFHOXNaQzVoYzFWcGJuUTJOQ2dwSUQ0Z01DazdDaUFnSUNCa2FXY2dNZ29nSUNBZ1luUnZhUW9nSUNBZ1pIVndDaUFnSUNCaGMzTmxjblFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qWTRDaUFnSUNBdkx5QmhjbU0xTlY5MGFISmxjMmh2YkdRZ1BTQkhiRzlpWVd4VGRHRjBaVHgxYVc1ME5qUStLSHNnYVc1cGRHbGhiRlpoYkhWbE9pQlZhVzUwTmpRb01Da2dmU2s3Q2lBZ0lDQmllWFJsWTE4eklDOHZJQ0poY21NMU5WOTBhSEpsYzJodmJHUWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU9EUUtJQ0FnSUM4dklIUm9hWE11WVhKak5UVmZkR2h5WlhOb2IyeGtMblpoYkhWbElEMGdWV2x1ZERZMEtIUm9jbVZ6YUc5c1pDNWhjMVZwYm5RMk5DZ3BLVHNLSUNBZ0lITjNZWEFLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNNUW9nSUNBZ0x5OGdZWEpqTlRWZmJtOXVZMlVnUFNCSGJHOWlZV3hUZEdGMFpUeDFhVzUwTmpRK0tIc2dhVzVwZEdsaGJGWmhiSFZsT2lCVmFXNTBOalFvTUNrZ2ZTazdDaUFnSUNCaWVYUmxZMTh4SUM4dklDSmhjbU0xTlY5dWIyNWpaU0lLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSTROUW9nSUNBZ0x5OGdkR2hwY3k1aGNtTTFOVjl1YjI1alpTNTJZV3gxWlNBOUlEQTdDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJNE9Rb2dJQ0FnTHk4Z2JHVjBJSEJKYm1SbGVEb2dkV2x1ZERZMElEMGdNRHNLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWRYSjVJRFFLQ21GeVl6VTFYM05sZEhWd1gzZG9hV3hsWDNSdmNFQXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9UTUtJQ0FnSUM4dklISmxkSFZ5YmlCSGJHOWlZV3hUZEdGMFpUeEJZMk52ZFc1MFBpaDdJR3RsZVRvZ2IzQXVhWFJ2WWlocGJtUmxlQ2tnZlNrS0lDQWdJR1JwWnlBekNpQWdJQ0JwZEc5aUNpQWdJQ0JrZFhBS0lDQWdJR0oxY25rZ053b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWprd0NpQWdJQ0F2THlCM2FHbHNaU0FvZEdocGN5NWhjbU0xTlY5cGJtUmxlRlJ2UVdSa2NtVnpjeWh3U1c1a1pYZ3BMbWhoYzFaaGJIVmxLU0I3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2MzZGhjQW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWprd0xUTXdNQW9nSUNBZ0x5OGdkMmhwYkdVZ0tIUm9hWE11WVhKak5UVmZhVzVrWlhoVWIwRmtaSEpsYzNNb2NFbHVaR1Y0S1M1b1lYTldZV3gxWlNrZ2V3b2dJQ0FnTHk4Z0lDQmpiMjV6ZENCaFpHUnlaWE56SUQwZ2RHaHBjeTVoY21NMU5WOXBibVJsZUZSdlFXUmtjbVZ6Y3lod1NXNWtaWGdwTG5aaGJIVmxPd29nSUNBZ0x5OGdJQ0F2THlCSmJpQndkWGxoTFhSeklIZGxJRzVsWldRZ2RHOGdZWE56WlhKMElIUm9aU0JyWlhrZ1pYaHBjM1J6SUdKbFptOXlaU0JrWld4bGRHbHVad29nSUNBZ0x5OGdJQ0JwWmlBb2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvWVdSa2NtVnpjeWt1YUdGelZtRnNkV1VwSUhzS0lDQWdJQzh2SUNBZ0lDQjBhR2x6TG1GeVl6VTFYMkZrWkhKbGMzTkRiM1Z1ZENoaFpHUnlaWE56S1M1a1pXeGxkR1VvS1RzS0lDQWdJQzh2SUNBZ0lDQjBhR2x6TG1GeVl6VTFYMmx1WkdWNFZHOUJaR1J5WlhOektIQkpibVJsZUNrdVpHVnNaWFJsS0NrN0NpQWdJQ0F2THlBZ0lIMEtJQ0FnSUM4dklBb2dJQ0FnTHk4Z0lDQWdJSEJKYm1SbGVDQXJQU0F4T3dvZ0lDQWdMeThnSUNBdkx5QjBhR2x6TG1GeVl6VTFYMkZrWkhKbGMzTkRiM1Z1ZENoaFpHUnlaWE56S1M1a1pXeGxkR1VvS1RzS0lDQWdJQzh2SUgwS0lDQWdJR0o2SUdGeVl6VTFYM05sZEhWd1gyRm1kR1Z5WDNkb2FXeGxRRFlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSTVNUW9nSUNBZ0x5OGdZMjl1YzNRZ1lXUmtjbVZ6Y3lBOUlIUm9hWE11WVhKak5UVmZhVzVrWlhoVWIwRmtaSEpsYzNNb2NFbHVaR1Y0S1M1MllXeDFaVHNLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCa2FXY2dOZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJSE4zWVhBS0lDQWdJR1IxY0FvZ0lDQWdZMjkyWlhJZ01nb2dJQ0FnWW5WeWVTQXhNQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNamt6Q2lBZ0lDQXZMeUJwWmlBb2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvWVdSa2NtVnpjeWt1YUdGelZtRnNkV1VwSUhzS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWW5WeWVTQXhDaUFnSUNCaWVpQmhjbU0xTlY5elpYUjFjRjloWm5SbGNsOXBabDlsYkhObFFEVUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJNU5Bb2dJQ0FnTHk4Z2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvWVdSa2NtVnpjeWt1WkdWc1pYUmxLQ2s3Q2lBZ0lDQmthV2NnTndvZ0lDQWdZWEJ3WDJkc2IySmhiRjlrWld3S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakk1TlFvZ0lDQWdMeThnZEdocGN5NWhjbU0xTlY5cGJtUmxlRlJ2UVdSa2NtVnpjeWh3U1c1a1pYZ3BMbVJsYkdWMFpTZ3BPd29nSUNBZ1pHbG5JRFVLSUNBZ0lHRndjRjluYkc5aVlXeGZaR1ZzQ2dwaGNtTTFOVjl6WlhSMWNGOWhablJsY2w5cFpsOWxiSE5sUURVNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95T1RnS0lDQWdJQzh2SUhCSmJtUmxlQ0FyUFNBeE93b2dJQ0FnWkdsbklETUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0FyQ2lBZ0lDQmlkWEo1SURRS0lDQWdJR0lnWVhKak5UVmZjMlYwZFhCZmQyaHBiR1ZmZEc5d1FESUtDbUZ5WXpVMVgzTmxkSFZ3WDJGbWRHVnlYM2RvYVd4bFFEWTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvek1ETUtJQ0FnSUM4dklHeGxkQ0J1U1c1a1pYZzZJSFZwYm5RMk5DQTlJREE3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1luVnllU0ExQ2dwaGNtTTFOVjl6WlhSMWNGOTNhR2xzWlY5MGIzQkFOem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTXdOUW9nSUNBZ0x5OGdkMmhwYkdVZ0tHNUpibVJsZUNBOElHRmtaSEpsYzNObGN5NXNaVzVuZEdncElIc0tJQ0FnSUdScFp5QTBDaUFnSUNCa2FXY2dNUW9nSUNBZ1BBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpBMUxUTXlNUW9nSUNBZ0x5OGdkMmhwYkdVZ0tHNUpibVJsZUNBOElHRmtaSEpsYzNObGN5NXNaVzVuZEdncElIc0tJQ0FnSUM4dklDQWdZV1JrY21WemN5QTlJR0ZrWkhKbGMzTmxjMXR1U1c1a1pYaGRPd29nSUNBZ0x5OGdDaUFnSUNBdkx5QWdJQzh2SUZOMGIzSmxJRzExYkhScGMybG5JR2x1WkdWNElHRnpJR3RsZVNCM2FYUm9JR0ZrWkhKbGMzTWdZWE1nZG1Gc2RXVUtJQ0FnSUM4dklDQWdkR2hwY3k1aGNtTTFOVjlwYm1SbGVGUnZRV1JrY21WemN5aHVTVzVrWlhncExuWmhiSFZsSUQwZ1lXUmtjbVZ6Y3pzS0lDQWdJQzh2SUFvZ0lDQWdMeThnSUNBdkx5QlRkRzl5WlNCaFpHUnlaWE56SUdGeklHdGxlU0JoYm1RZ1kyOTFiblJsY2lCaGN5QjJZV3gxWlN3S0lDQWdJQzh2SUNBZ0x5OGdkR2hwY3lCcGN5Qm1iM0lnWldGelpTQnZaaUJoZFhSb1pXNTBhV05oZEdsdmJnb2dJQ0FnTHk4Z0lDQXZMeUJKUmlCbWFYSnpkQ0IwYVcxbExDQnpaWFFnZEc4Z01Td2daV3h6WlNCcGJtTnlaVzFsYm5RZ1lua2dNUW9nSUNBZ0x5OGdJQ0JwWmlBb0lYUm9hWE11WVhKak5UVmZZV1JrY21WemMwTnZkVzUwS0dGa1pISmxjM01wTG1oaGMxWmhiSFZsS1NCN0NpQWdJQ0F2THlBZ0lDQWdkR2hwY3k1aGNtTTFOVjloWkdSeVpYTnpRMjkxYm5Rb1lXUmtjbVZ6Y3lrdWRtRnNkV1VnUFNBd093b2dJQ0FnTHk4Z0lDQjlDaUFnSUNBdkx5QUtJQ0FnSUM4dklDQWdkR2hwY3k1aGNtTTFOVjloWkdSeVpYTnpRMjkxYm5Rb1lXUmtjbVZ6Y3lrdWRtRnNkV1VnS3owZ01Uc0tJQ0FnSUM4dklBb2dJQ0FnTHk4Z0lDQnVTVzVrWlhnZ0t6MGdNVHNLSUNBZ0lDOHZJSDBLSUNBZ0lHSjZJR0Z5WXpVMVgzTmxkSFZ3WDJGbWRHVnlYM2RvYVd4bFFERXhDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvek1EWUtJQ0FnSUM4dklHRmtaSEpsYzNNZ1BTQmhaR1J5WlhOelpYTmJia2x1WkdWNFhUc0tJQ0FnSUdScFp5QXhDaUFnSUNCbGVIUnlZV04wSURJZ01Bb2dJQ0FnWkdsbklEVUtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ2FXNTBZMTh6SUM4dklETXlDaUFnSUNBcUNpQWdJQ0JwYm5Salh6TWdMeThnTXpJS0lDQWdJR1Y0ZEhKaFkzUXpJQzh2SUc5dUlHVnljbTl5T2lCcGJtUmxlQ0JoWTJObGMzTWdhWE1nYjNWMElHOW1JR0p2ZFc1a2N3b2dJQ0FnWkhWd0NpQWdJQ0JpZFhKNUlEa0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2prekNpQWdJQ0F2THlCeVpYUjFjbTRnUjJ4dlltRnNVM1JoZEdVOFFXTmpiM1Z1ZEQ0b2V5QnJaWGs2SUc5d0xtbDBiMklvYVc1a1pYZ3BJSDBwQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQnBkRzlpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3pNRGtLSUNBZ0lDOHZJSFJvYVhNdVlYSmpOVFZmYVc1a1pYaFViMEZrWkhKbGMzTW9ia2x1WkdWNEtTNTJZV3gxWlNBOUlHRmtaSEpsYzNNN0NpQWdJQ0JrYVdjZ01Rb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pNeE5Bb2dJQ0FnTHk4Z2FXWWdLQ0YwYUdsekxtRnlZelUxWDJGa1pISmxjM05EYjNWdWRDaGhaR1J5WlhOektTNW9ZWE5XWVd4MVpTa2dld29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUhOM1lYQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmlkWEo1SURFS0lDQWdJR0p1ZWlCaGNtTTFOVjl6WlhSMWNGOWhablJsY2w5cFpsOWxiSE5sUURFd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TVRVS0lDQWdJQzh2SUhSb2FYTXVZWEpqTlRWZllXUmtjbVZ6YzBOdmRXNTBLR0ZrWkhKbGMzTXBMblpoYkhWbElEMGdNRHNLSUNBZ0lHUnBaeUEyQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLQ21GeVl6VTFYM05sZEhWd1gyRm1kR1Z5WDJsbVgyVnNjMlZBTVRBNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TVRnS0lDQWdJQzh2SUhSb2FYTXVZWEpqTlRWZllXUmtjbVZ6YzBOdmRXNTBLR0ZrWkhKbGMzTXBMblpoYkhWbElDczlJREU3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pHbG5JRGNLSUNBZ0lHUjFjQW9nSUNBZ1kyOTJaWElnTWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJIYkc5aVlXeFRkR0YwWlNCbGVHbHpkSE1LSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNBckNpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpJd0NpQWdJQ0F2THlCdVNXNWtaWGdnS3owZ01Uc0tJQ0FnSUdScFp5QTBDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnS3dvZ0lDQWdZblZ5ZVNBMUNpQWdJQ0JpSUdGeVl6VTFYM05sZEhWd1gzZG9hV3hsWDNSdmNFQTNDZ3BoY21NMU5WOXpaWFIxY0Y5aFpuUmxjbDkzYUdsc1pVQXhNVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSTNOUzB5TnpnS0lDQWdJQzh2SUdGeVl6VTFYM05sZEhWd0tBb2dJQ0FnTHk4Z0lDQjBhSEpsYzJodmJHUTZJRlZwYm5RNExBb2dJQ0FnTHk4Z0lDQmhaR1J5WlhOelpYTTZJRUZqWTI5MWJuUmJYUW9nSUNBZ0x5OGdLVG9nZG05cFpDQjdDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ3BoY21NMU5WOXpaWFIxY0Y5bGJITmxYMkp2WkhsQU1qWTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE1UUUtJQ0FnSUM4dklHRnpjMlZ5ZENoVWVHNHVjMlZ1WkdWeUlEMDlQU0JIYkc5aVlXd3VZM0psWVhSdmNrRmtaSEpsYzNNcE93b2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdaMnh2WW1Gc0lFTnlaV0YwYjNKQlpHUnlaWE56Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowQ2lBZ0lDQmlJR0Z5WXpVMVgzTmxkSFZ3WDJGbWRHVnlYMmxtWDJWc2MyVkFNamNLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlRYVnNkR2x6YVdjdVlYSmpOVFZmYm1WM1ZISmhibk5oWTNScGIyNUhjbTkxY0Z0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZelUxWDI1bGQxUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TWprS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1cGMwRmtiV2x1S0NrcElIc0tJQ0FnSUdOaGJHeHpkV0lnYVhOQlpHMXBiZ29nSUNBZ1ltNTZJR0Z5WXpVMVgyNWxkMVJ5WVc1ellXTjBhVzl1UjNKdmRYQmZZV1owWlhKZmFXWmZaV3h6WlVBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TXpBS0lDQWdJQzh2SUhSb2FYTXViMjVzZVZOcFoyNWxjaWdwT3dvZ0lDQWdZMkZzYkhOMVlpQnZibXg1VTJsbmJtVnlDZ3BoY21NMU5WOXVaWGRVY21GdWMyRmpkR2x2YmtkeWIzVndYMkZtZEdWeVgybG1YMlZzYzJWQU16b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMU5nb2dJQ0FnTHk4Z2NtVjBkWEp1SUhSb2FYTXVZWEpqTlRWZmJtOXVZMlV1ZG1Gc2RXVWdLeUF4T3dvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qY3hDaUFnSUNBdkx5QmhjbU0xTlY5dWIyNWpaU0E5SUVkc2IySmhiRk4wWVhSbFBIVnBiblEyTkQ0b2V5QnBibWwwYVdGc1ZtRnNkV1U2SUZWcGJuUTJOQ2d3S1NCOUtUc0tJQ0FnSUdKNWRHVmpYekVnTHk4Z0ltRnlZelUxWDI1dmJtTmxJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UVTJDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTVoY21NMU5WOXViMjVqWlM1MllXeDFaU0FySURFN0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lDc0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pjeENpQWdJQ0F2THlCaGNtTTFOVjl1YjI1alpTQTlJRWRzYjJKaGJGTjBZWFJsUEhWcGJuUTJORDRvZXlCcGJtbDBhV0ZzVm1Gc2RXVTZJRlZwYm5RMk5DZ3dLU0I5S1RzS0lDQWdJR0o1ZEdWalh6RWdMeThnSW1GeVl6VTFYMjV2Ym1ObElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpNMENpQWdJQ0F2THlCMGFHbHpMbUZ5WXpVMVgyNXZibU5sTG5aaGJIVmxJRDBnYmpzS0lDQWdJR1JwWnlBeENpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpJNENpQWdJQ0F2THlCaGNtTTFOVjl1WlhkVWNtRnVjMkZqZEdsdmJrZHliM1Z3S0NrNklIVnBiblEyTkNCN0NpQWdJQ0JwZEc5aUNpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNDZDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWkhWd0NpQWdJQ0J3ZFhOb1lubDBaWE1nSWlJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak0wTmkwek5URUtJQ0FnSUM4dklHRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVLQW9nSUNBZ0x5OGdJQ0JqYjNOMGN6b2daM1I0Ymk1UVlYbHRaVzUwVkhodUxBb2dJQ0FnTHk4Z0lDQjBjbUZ1YzJGamRHbHZia2R5YjNWd09pQjFhVzUwTmpRc0NpQWdJQ0F2THlBZ0lHbHVaR1Y0T2lCVmFXNTBPQ3dLSUNBZ0lDOHZJQ0FnZEhKaGJuTmhZM1JwYjI0NklHSjVkR1Z6Q2lBZ0lDQXZMeUFwT2lCMmIybGtJSHNLSUNBZ0lIUjRiaUJIY205MWNFbHVaR1Y0Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ0xRb2dJQ0FnWjNSNGJuTWdWSGx3WlVWdWRXMEtJQ0FnSUdsdWRHTmZNU0F2THlCd1lYa0tJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnZEhKaGJuTmhZM1JwYjI0Z2RIbHdaU0JwY3lCd1lYa0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnT0FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBOalFLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwT0FvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTXdvZ0lDQWdaSFZ3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnY0hWemFHbHVkQ0F5SUM4dklESUtJQ0FnSUNzS0lDQWdJR1JwWnlBeENpQWdJQ0JzWlc0S0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdaWGgwY21GamRDQXlJREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTTFNZ29nSUNBZ0x5OGdhV1lnS0NGMGFHbHpMbWx6UVdSdGFXNG9LU2tnZXdvZ0lDQWdZMkZzYkhOMVlpQnBjMEZrYldsdUNpQWdJQ0JpYm5vZ1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TlRNS0lDQWdJQzh2SUhSb2FYTXViMjVzZVZOcFoyNWxjaWdwT3dvZ0lDQWdZMkZzYkhOMVlpQnZibXg1VTJsbmJtVnlDZ3BoY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRRE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3pOVFlLSUNBZ0lDOHZJR0Z6YzJWeWRDaDBjbUZ1YzJGamRHbHZia2R5YjNWd0tUc0tJQ0FnSUdScFp5QXlDaUFnSUNCa2RYQUtJQ0FnSUdGemMyVnlkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16VTNDaUFnSUNBdkx5QmhjM05sY25Rb2RISmhibk5oWTNScGIyNUhjbTkxY0NBOFBTQjBhR2x6TG1GeVl6VTFYMjV2Ym1ObExuWmhiSFZsS1RzS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNNUW9nSUNBZ0x5OGdZWEpqTlRWZmJtOXVZMlVnUFNCSGJHOWlZV3hUZEdGMFpUeDFhVzUwTmpRK0tIc2dhVzVwZEdsaGJGWmhiSFZsT2lCVmFXNTBOalFvTUNrZ2ZTazdDaUFnSUNCaWVYUmxZMTh4SUM4dklDSmhjbU0xTlY5dWIyNWpaU0lLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTTFOd29nSUNBZ0x5OGdZWE56WlhKMEtIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBZ1BEMGdkR2hwY3k1aGNtTTFOVjl1YjI1alpTNTJZV3gxWlNrN0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdaR2xuSURFS0lDQWdJRDQ5Q2lBZ0lDQmhjM05sY25RS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak0xT1Mwek5qSUtJQ0FnSUM4dklHTnZibk4wSUhSeVlXNXpZV04wYVc5dVFtOTRPaUJVY21GdWMyRmpkR2x2YmtkeWIzVndJRDBnZXdvZ0lDQWdMeThnSUNCdWIyNWpaVG9nZEhKaGJuTmhZM1JwYjI1SGNtOTFjQ3dLSUNBZ0lDOHZJQ0FnYVc1a1pYZzZJR2x1WkdWNExBb2dJQ0FnTHk4Z2ZUc0tJQ0FnSUdsMGIySUtJQ0FnSUdScFp5QXlDaUFnSUNCamIyNWpZWFFLSUNBZ0lHSjFjbmtnTmdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNelkzQ2lBZ0lDQXZMeUJzWlhRZ1ozSnZkWEJRYjNOcGRHbHZiam9nZFdsdWREWTBJRDBnVkhodUxtZHliM1Z3U1c1a1pYZ2dLeUF4T3dvZ0lDQWdkSGh1SUVkeWIzVndTVzVrWlhnS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQXJDaUFnSUNCa2RYQUtJQ0FnSUdKMWNua2dOUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16WTRDaUFnSUNBdkx5QnBaaUFvWjNKdmRYQlFiM05wZEdsdmJpQThJRWRzYjJKaGJDNW5jbTkxY0ZOcGVtVXBJSHNLSUNBZ0lHZHNiMkpoYkNCSGNtOTFjRk5wZW1VS0lDQWdJRHdLSUNBZ0lHSnVlaUJoY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2Ymw5cFpsOWliMlI1UURRS0lDQWdJR1IxY0FvZ0lDQWdZblZ5ZVNBMUNncGhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZibDloWm5SbGNsOXBabDlsYkhObFFERTBPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16Z3lDaUFnSUNBdkx5QmpiMjV6ZENCdFluSlVlRzVKYm1OeVpXRnpaU0E5SUhSb2FYTXVZWEpqTlRWZmJXSnlWSGh1U1c1amNtVmhjMlVvZEhKaGJuTmhZM1JwYjI1RVlYUmhMbXhsYm1kMGFDazdDaUFnSUNCa2FXY2dOQW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdOaGJHeHpkV0lnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyMWljbFI0YmtsdVkzSmxZWE5sQ2lBZ0lDQndiM0FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTTVOQW9nSUNBZ0x5OGdkR2hwY3k1aGNtTTFOVjkwY21GdWMyRmpkR2x2Ym5Nb2RISmhibk5oWTNScGIyNUNiM2dwTG5aaGJIVmxJRDBnZEhKaGJuTmhZM1JwYjI1RVlYUmhPd29nSUNBZ1pHbG5JRFlLSUNBZ0lHUjFjQW9nSUNBZ1ltOTRYMlJsYkFvZ0lDQWdjRzl3Q2lBZ0lDQmtkWEFLSUNBZ0lIVnVZMjkyWlhJZ01nb2dJQ0FnWW05NFgzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16azNMVFF3TUFvZ0lDQWdMeThnWlcxcGREeFVjbUZ1YzJGamRHbHZia0ZrWkdWa1BpaDdDaUFnSUNBdkx5QWdJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQTZJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQXNDaUFnSUNBdkx5QWdJSFJ5WVc1ellXTjBhVzl1U1c1a1pYZzZJR2x1WkdWNENpQWdJQ0F2THlCOUtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE1UZzBPV0UxT1RRZ0x5OGdiV1YwYUc5a0lDSlVjbUZ1YzJGamRHbHZia0ZrWkdWa0tIVnBiblEyTkN4MWFXNTBPQ2tpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNelEyTFRNMU1Rb2dJQ0FnTHk4Z1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNG9DaUFnSUNBdkx5QWdJR052YzNSek9pQm5kSGh1TGxCaGVXMWxiblJVZUc0c0NpQWdJQ0F2THlBZ0lIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNklIVnBiblEyTkN3S0lDQWdJQzh2SUNBZ2FXNWtaWGc2SUZWcGJuUTRMQW9nSUNBZ0x5OGdJQ0IwY21GdWMyRmpkR2x2YmpvZ1lubDBaWE1LSUNBZ0lDOHZJQ2s2SUhadmFXUWdld29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI1ZmFXWmZZbTlrZVVBME9nb2dJQ0FnWkhWd0NpQWdJQ0JpZFhKNUlEVUtDbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1WDNkb2FXeGxYM1J2Y0VBMU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpjeENpQWdJQ0F2THlCbmRIaHVMa0Z3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaWhuY205MWNGQnZjMmwwYVc5dUtTNWhjSEJKWkNBOVBUMGdWSGh1TG1Gd2NHeHBZMkYwYVc5dVNXUUtJQ0FnSUdScFp5QXpDaUFnSUNCa2RYQUtJQ0FnSUdkMGVHNXpJRlI1Y0dWRmJuVnRDaUFnSUNCd2RYTm9hVzUwSURZZ0x5OGdZWEJ3YkFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QjBjbUZ1YzJGamRHbHZiaUIwZVhCbElHbHpJR0Z3Y0d3S0lDQWdJR2QwZUc1eklFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lIUjRiaUJCY0hCc2FXTmhkR2x2YmtsRUNpQWdJQ0E5UFFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNemN4TFRNM013b2dJQ0FnTHk4Z0lDQm5kSGh1TGtGd2NHeHBZMkYwYVc5dVEyRnNiRlI0YmlobmNtOTFjRkJ2YzJsMGFXOXVLUzVoY0hCSlpDQTlQVDBnVkhodUxtRndjR3hwWTJGMGFXOXVTV1FLSUNBZ0lDOHZJQzh2SmlZZ2RHaHBjeTUwZUc1SGNtOTFjRnRuY205MWNGQnZjMmwwYVc5dVhTNWhjSEJzYVdOaGRHbHZia0Z5WjNOYk1GMGdQVDA5SUcxbGRHaHZaQ2dpWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI1RGIyNTBhVzUxWldRb1lubDBaVnRkS1hadmFXUWlLUW9nSUNBZ0x5OGdKaVlnWjNSNGJpNUJjSEJzYVdOaGRHbHZia05oYkd4VWVHNG9aM0p2ZFhCUWIzTnBkR2x2YmlrdVlYQndRWEpuY3lnd0tTQTlQVDBnYldWMGFHOWtVMlZzWldOMGIzSW9JbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1UTI5dWRHbHVkV1ZrS0dKNWRHVmJYU2wyYjJsa0lpa0tJQ0FnSUdKNklHRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVYMkZtZEdWeVgybG1YMlZzYzJWQU9Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpjekNpQWdJQ0F2THlBbUppQm5kSGh1TGtGd2NHeHBZMkYwYVc5dVEyRnNiRlI0YmlobmNtOTFjRkJ2YzJsMGFXOXVLUzVoY0hCQmNtZHpLREFwSUQwOVBTQnRaWFJvYjJSVFpXeGxZM1J2Y2lnaVlYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNURiMjUwYVc1MVpXUW9ZbmwwWlZ0ZEtYWnZhV1FpS1FvZ0lDQWdaR2xuSURNS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQm5kSGh1YzJGeklFRndjR3hwWTJGMGFXOXVRWEpuY3dvZ0lDQWdZbmwwWldNZ05DQXZMeUJ0WlhSb2IyUWdJbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1UTI5dWRHbHVkV1ZrS0dKNWRHVmJYU2wyYjJsa0lnb2dJQ0FnUFQwS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak0zTVMwek56TUtJQ0FnSUM4dklDQWdaM1I0Ymk1QmNIQnNhV05oZEdsdmJrTmhiR3hVZUc0b1ozSnZkWEJRYjNOcGRHbHZiaWt1WVhCd1NXUWdQVDA5SUZSNGJpNWhjSEJzYVdOaGRHbHZia2xrQ2lBZ0lDQXZMeUF2THlZbUlIUm9hWE11ZEhodVIzSnZkWEJiWjNKdmRYQlFiM05wZEdsdmJsMHVZWEJ3YkdsallYUnBiMjVCY21keld6QmRJRDA5UFNCdFpYUm9iMlFvSW1GeVl6VTFYMkZrWkZSeVlXNXpZV04wYVc5dVEyOXVkR2x1ZFdWa0tHSjVkR1ZiWFNsMmIybGtJaWtLSUNBZ0lDOHZJQ1ltSUdkMGVHNHVRWEJ3YkdsallYUnBiMjVEWVd4c1ZIaHVLR2R5YjNWd1VHOXphWFJwYjI0cExtRndjRUZ5WjNNb01Da2dQVDA5SUcxbGRHaHZaRk5sYkdWamRHOXlLQ0poY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2YmtOdmJuUnBiblZsWkNoaWVYUmxXMTBwZG05cFpDSXBDaUFnSUNCaWVpQmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZibDloWm5SbGNsOXBabDlsYkhObFFEa0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pNM05nb2dJQ0FnTHk4Z2RISmhibk5oWTNScGIyNUVZWFJoSUQwZ2RISmhibk5oWTNScGIyNUVZWFJoTG1OdmJtTmhkQ2huZEhodUxrRndjR3hwWTJGMGFXOXVRMkZzYkZSNGJpaG5jbTkxY0ZCdmMybDBhVzl1S1M1aGNIQkJjbWR6S0RFcEtUc0tJQ0FnSUdScFp5QXpDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnWjNSNGJuTmhjeUJCY0hCc2FXTmhkR2x2YmtGeVozTUtJQ0FnSUdScFp5QTFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHSjFjbmtnTlFvS1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNWZZV1owWlhKZmFXWmZaV3h6WlVBNU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpjNENpQWdJQ0F2THlCbmNtOTFjRkJ2YzJsMGFXOXVJQ3M5SURFN0NpQWdJQ0JrYVdjZ013b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJQ3NLSUNBZ0lHUjFjQW9nSUNBZ1luVnllU0ExQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3pOemtLSUNBZ0lDOHZJSDBnZDJocGJHVWdLR2R5YjNWd1VHOXphWFJwYjI0Z1BDQkhiRzlpWVd3dVozSnZkWEJUYVhwbEtUc0tJQ0FnSUdkc2IySmhiQ0JIY205MWNGTnBlbVVLSUNBZ0lEd0tJQ0FnSUdKdWVpQmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZibDkzYUdsc1pWOTBiM0JBTlFvZ0lDQWdZaUJoY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRREUwQ2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rMTFiSFJwYzJsbkxtRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVRMjl1ZEdsdWRXVmtXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI1RGIyNTBhVzUxWldRNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTURjdE5EQTVDaUFnSUNBdkx5QmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZia052Ym5ScGJuVmxaQ2dLSUNBZ0lDOHZJQ0FnZEhKaGJuTmhZM1JwYjI0NklHSjVkR1Z6Q2lBZ0lDQXZMeUFwT2lCMmIybGtJSHNLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUdGeWNtRjVJR3hsYm1kMGFDQm9aV0ZrWlhJS0lDQWdJSEIxYzJocGJuUWdNaUF2THlBeUNpQWdJQ0FyQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQnNaVzRLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1WkhsdVlXMXBZMTloY25KaGVUeGhjbU0wTG5WcGJuUTRQZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5ERXdDaUFnSUNBdkx5QnBaaUFvSVhSb2FYTXVhWE5CWkcxcGJpZ3BLU0I3Q2lBZ0lDQmpZV3hzYzNWaUlHbHpRV1J0YVc0S0lDQWdJR0p1ZWlCaGNtTTFOVjloWkdSVWNtRnVjMkZqZEdsdmJrTnZiblJwYm5WbFpGOWhablJsY2w5cFpsOWxiSE5sUURNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalF4TVFvZ0lDQWdMeThnZEdocGN5NXZibXg1VTJsbmJtVnlLQ2s3Q2lBZ0lDQmpZV3hzYzNWaUlHOXViSGxUYVdkdVpYSUtDbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1UTI5dWRHbHVkV1ZrWDJGbWRHVnlYMmxtWDJWc2MyVkFNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUXdOeTAwTURrS0lDQWdJQzh2SUdGeVl6VTFYMkZrWkZSeVlXNXpZV04wYVc5dVEyOXVkR2x1ZFdWa0tBb2dJQ0FnTHk4Z0lDQjBjbUZ1YzJGamRHbHZiam9nWW5sMFpYTUtJQ0FnSUM4dklDazZJSFp2YVdRZ2V3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBOZFd4MGFYTnBaeTVoY21NMU5WOXlaVzF2ZG1WVWNtRnVjMkZqZEdsdmJsdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUZ5WXpVMVgzSmxiVzkyWlZSeVlXNXpZV04wYVc5dU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TkRJd0xUUXlNd29nSUNBZ0x5OGdZWEpqTlRWZmNtVnRiM1psVkhKaGJuTmhZM1JwYjI0b0NpQWdJQ0F2THlBZ0lIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNklIVnBiblEyTkN3S0lDQWdJQzh2SUNBZ2FXNWtaWGc2SUZWcGJuUTRDaUFnSUNBdkx5QXBPaUIyYjJsa0lIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnT0FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBOalFLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwT0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOREkwQ2lBZ0lDQXZMeUJwWmlBb0lYUm9hWE11YVhOQlpHMXBiaWdwS1NCN0NpQWdJQ0JqWVd4c2MzVmlJR2x6UVdSdGFXNEtJQ0FnSUdKdWVpQmhjbU0xTlY5eVpXMXZkbVZVY21GdWMyRmpkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRRE1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUXlOUW9nSUNBZ0x5OGdkR2hwY3k1dmJteDVVMmxuYm1WeUtDazdDaUFnSUNCallXeHNjM1ZpSUc5dWJIbFRhV2R1WlhJS0NtRnlZelUxWDNKbGJXOTJaVlJ5WVc1ellXTjBhVzl1WDJGbWRHVnlYMmxtWDJWc2MyVkFNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUXlPQzAwTXpFS0lDQWdJQzh2SUdOdmJuTjBJSFJ5WVc1ellXTjBhVzl1UW05NE9pQlVjbUZ1YzJGamRHbHZia2R5YjNWd0lEMGdld29nSUNBZ0x5OGdJQ0J1YjI1alpUb2dkSEpoYm5OaFkzUnBiMjVIY205MWNDd0tJQ0FnSUM4dklDQWdhVzVrWlhnNklHbHVaR1Y0TEFvZ0lDQWdMeThnZlRzS0lDQWdJR1JwWnlBeENpQWdJQ0JwZEc5aUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvME16TUtJQ0FnSUM4dklHTnZibk4wSUhSNGJreGxibWQwYUNBOUlIUm9hWE11WVhKak5UVmZkSEpoYm5OaFkzUnBiMjV6S0hSeVlXNXpZV04wYVc5dVFtOTRLUzVzWlc1bmRHZzdDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOXNaVzRLSUNBZ0lHRnpjMlZ5ZENBdkx5QkNiM2dnYlhWemRDQm9ZWFpsSUhaaGJIVmxDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvME16UUtJQ0FnSUM4dklIUm9hWE11WVhKak5UVmZkSEpoYm5OaFkzUnBiMjV6S0hSeVlXNXpZV04wYVc5dVFtOTRLUzVrWld4bGRHVW9LVHNLSUNBZ0lHUnBaeUF4Q2lBZ0lDQmliM2hmWkdWc0NpQWdJQ0J3YjNBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalF6T1FvZ0lDQWdMeThnWTI5dWMzUWdiV0p5VkhodVJHVmpjbVZoYzJVNklIVnBiblEyTkNBOUlDZ3lOVEF3S1NBcklDZzBNREFnS2lBb09TQXJJSFI0Ymt4bGJtZDBhQ2twT3dvZ0lDQWdjSFZ6YUdsdWRDQTVJQzh2SURrS0lDQWdJQ3NLSUNBZ0lHbHVkR01nTkNBdkx5QTBNREFLSUNBZ0lDb0tJQ0FnSUdsdWRHTWdOU0F2THlBeU5UQXdDaUFnSUNBckNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTkRNdE5EUTJDaUFnSUNBdkx5QnBkSGh1TG5CaGVXMWxiblFvZXdvZ0lDQWdMeThnSUNCeVpXTmxhWFpsY2pvZ1ZIaHVMbk5sYm1SbGNpd0tJQ0FnSUM4dklDQWdZVzF2ZFc1ME9pQnRZbkpVZUc1RVpXTnlaV0Z6WlFvZ0lDQWdMeThnZlNrdWMzVmliV2wwS0NrN0NpQWdJQ0JwZEhodVgySmxaMmx1Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzBORFFLSUNBZ0lDOHZJSEpsWTJWcGRtVnlPaUJVZUc0dWMyVnVaR1Z5TEFvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ2FYUjRibDltYVdWc1pDQlNaV05sYVhabGNnb2dJQ0FnYVhSNGJsOW1hV1ZzWkNCQmJXOTFiblFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUTBNeTAwTkRZS0lDQWdJQzh2SUdsMGVHNHVjR0Y1YldWdWRDaDdDaUFnSUNBdkx5QWdJSEpsWTJWcGRtVnlPaUJVZUc0dWMyVnVaR1Z5TEFvZ0lDQWdMeThnSUNCaGJXOTFiblE2SUcxaWNsUjRia1JsWTNKbFlYTmxDaUFnSUNBdkx5QjlLUzV6ZFdKdGFYUW9LVHNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCcGRIaHVYMlpwWld4a0lGUjVjR1ZGYm5WdENpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdhWFI0Ymw5bWFXVnNaQ0JHWldVS0lDQWdJR2wwZUc1ZmMzVmliV2wwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzBOVFF0TkRVM0NpQWdJQ0F2THlCbGJXbDBQRlJ5WVc1ellXTjBhVzl1VW1WdGIzWmxaRDRvZXdvZ0lDQWdMeThnSUNCMGNtRnVjMkZqZEdsdmJrZHliM1Z3T2lCMGNtRnVjMkZqZEdsdmJrZHliM1Z3TEFvZ0lDQWdMeThnSUNCMGNtRnVjMkZqZEdsdmJrbHVaR1Y0T2lCcGJtUmxlQW9nSUNBZ0x5OGdmU2tLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlRE5sT1dJeVkyRTFJQzh2SUcxbGRHaHZaQ0FpVkhKaGJuTmhZM1JwYjI1U1pXMXZkbVZrS0hWcGJuUTJOQ3gxYVc1ME9Da2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5ESXdMVFF5TXdvZ0lDQWdMeThnWVhKak5UVmZjbVZ0YjNabFZISmhibk5oWTNScGIyNG9DaUFnSUNBdkx5QWdJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQTZJSFZwYm5RMk5Dd0tJQ0FnSUM4dklDQWdhVzVrWlhnNklGVnBiblE0Q2lBZ0lDQXZMeUFwT2lCMmIybGtJSHNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlRYVnNkR2x6YVdjdVlYSmpOVFZmYzJWMFUybG5ibUYwZFhKbGMxdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUZ5WXpVMVgzTmxkRk5wWjI1aGRIVnlaWE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzBOekV0TkRjMUNpQWdJQ0F2THlCaGNtTTFOVjl6WlhSVGFXZHVZWFIxY21WektBb2dJQ0FnTHk4Z0lDQmpiM04wY3pvZ1ozUjRiaTVRWVhsdFpXNTBWSGh1TEFvZ0lDQWdMeThnSUNCMGNtRnVjMkZqZEdsdmJrZHliM1Z3T2lCMWFXNTBOalFzQ2lBZ0lDQXZMeUFnSUhOcFoyNWhkSFZ5WlhNNklHSjVkR1Z6UERZMFBsdGRDaUFnSUNBdkx5QXBPaUIyYjJsa0lIc0tJQ0FnSUhSNGJpQkhjbTkxY0VsdVpHVjRDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnTFFvZ0lDQWdaSFZ3Q2lBZ0lDQm5kSGh1Y3lCVWVYQmxSVzUxYlFvZ0lDQWdhVzUwWTE4eElDOHZJSEJoZVFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QjBjbUZ1YzJGamRHbHZiaUIwZVhCbElHbHpJSEJoZVFvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTWlBdkx5QTRDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RMk5Bb2dJQ0FnWW5SdmFRb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01nb2dJQ0FnWkhWd0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2NIVnphR2x1ZENBMk5DQXZMeUEyTkFvZ0lDQWdLZ29nSUNBZ1pIVndDaUFnSUNCd2RYTm9hVzUwSURJZ0x5OGdNZ29nSUNBZ0t3b2dJQ0FnWkdsbklESUtJQ0FnSUd4bGJnb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNWtlVzVoYldsalgyRnljbUY1UEdKNWRHVnpXelkwWFQ0S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalEzTmdvZ0lDQWdMeThnZEdocGN5NXZibXg1VTJsbmJtVnlLQ2s3Q2lBZ0lDQmpZV3hzYzNWaUlHOXViSGxUYVdkdVpYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pRM09Bb2dJQ0FnTHk4Z1kyOXVjM1FnYldKeVUybG5TVzVqY21WaGMyVWdQU0IwYUdsekxtRnlZelUxWDIxaWNsTnBaMGx1WTNKbFlYTmxLSE5wWjI1aGRIVnlaWE11YkdWdVozUm9JQ29nTmpRcE93b2dJQ0FnWTJGc2JITjFZaUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZUWFZzZEdsemFXY3VZWEpqTlRWZmJXSnlVMmxuU1c1amNtVmhjMlVLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUTRNUW9nSUNBZ0x5OGdZWE56WlhKMEtHTnZjM1J6TG5KbFkyVnBkbVZ5SUQwOVBTQkhiRzlpWVd3dVkzVnljbVZ1ZEVGd2NHeHBZMkYwYVc5dVFXUmtjbVZ6Y3lrS0lDQWdJR1JwWnlBekNpQWdJQ0JuZEhodWN5QlNaV05sYVhabGNnb2dJQ0FnWjJ4dlltRnNJRU4xY25KbGJuUkJjSEJzYVdOaGRHbHZia0ZrWkhKbGMzTUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pRNE1nb2dJQ0FnTHk4Z1lYTnpaWEowS0dOdmMzUnpMbUZ0YjNWdWRDQStQU0J0WW5KVGFXZEpibU55WldGelpTa0tJQ0FnSUhWdVkyOTJaWElnTXdvZ0lDQWdaM1I0Ym5NZ1FXMXZkVzUwQ2lBZ0lDQThQUW9nSUNBZ1lYTnpaWEowQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzBPVEVLSUNBZ0lDOHZJR0ZrWkhKbGMzTTZJRlI0Ymk1elpXNWtaWElLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pRNE9TMDBPVElLSUNBZ0lDOHZJR052Ym5OMElITnBaMjVoZEhWeVpVSnZlRG9nVkhKaGJuTmhZM1JwYjI1VGFXZHVZWFIxY21WeklEMGdld29nSUNBZ0x5OGdJQ0J1YjI1alpUb2dkSEpoYm5OaFkzUnBiMjVIY205MWNDd0tJQ0FnSUM4dklDQWdZV1JrY21WemN6b2dWSGh1TG5ObGJtUmxjZ29nSUNBZ0x5OGdmVHNLSUNBZ0lIVnVZMjkyWlhJZ01nb2dJQ0FnYVhSdllnb2dJQ0FnWkhWd0NpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lHTnZibU5oZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZORGsxQ2lBZ0lDQXZMeUIwYUdsekxtRnlZelUxWDNOcFoyNWhkSFZ5WlhNb2MybG5ibUYwZFhKbFFtOTRLUzUyWVd4MVpTQTlJR05zYjI1bEtITnBaMjVoZEhWeVpYTXBPd29nSUNBZ1pIVndDaUFnSUNCaWIzaGZaR1ZzQ2lBZ0lDQndiM0FLSUNBZ0lIVnVZMjkyWlhJZ01nb2dJQ0FnWW05NFgzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5UQXdDaUFnSUNBdkx5QnphV2R1WlhJNklGUjRiaTV6Wlc1a1pYSUtJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalE1T0MwMU1ERUtJQ0FnSUM4dklHVnRhWFE4VTJsbmJtRjBkWEpsVTJWMFBpaDdDaUFnSUNBdkx5QWdJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQTZJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQXNDaUFnSUNBdkx5QWdJSE5wWjI1bGNqb2dWSGh1TG5ObGJtUmxjZ29nSUNBZ0x5OGdmU2tLSUNBZ0lHTnZibU5oZEFvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0WldObVltTmlNek1nTHk4Z2JXVjBhRzlrSUNKVGFXZHVZWFIxY21WVFpYUW9kV2x1ZERZMExHRmtaSEpsYzNNcElnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pRM01TMDBOelVLSUNBZ0lDOHZJR0Z5WXpVMVgzTmxkRk5wWjI1aGRIVnlaWE1vQ2lBZ0lDQXZMeUFnSUdOdmMzUnpPaUJuZEhodUxsQmhlVzFsYm5SVWVHNHNDaUFnSUNBdkx5QWdJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQTZJSFZwYm5RMk5Dd0tJQ0FnSUM4dklDQWdjMmxuYm1GMGRYSmxjem9nWW5sMFpYTThOalErVzEwS0lDQWdJQzh2SUNrNklIWnZhV1FnZXdvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcE5kV3gwYVhOcFp5NWhjbU0xTlY5amJHVmhjbE5wWjI1aGRIVnlaWE5iY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTFOVjlqYkdWaGNsTnBaMjVoZEhWeVpYTTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvMU1UTXROVEUyQ2lBZ0lDQXZMeUJoY21NMU5WOWpiR1ZoY2xOcFoyNWhkSFZ5WlhNb0NpQWdJQ0F2THlBZ0lIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNklIVnBiblEyTkN3S0lDQWdJQzh2SUNBZ1lXUmtjbVZ6Y3pvZ1FXTmpiM1Z1ZEFvZ0lDQWdMeThnS1RvZ2RtOXBaQ0I3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklEZ0tJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVkV2x1ZERZMENpQWdJQ0JpZEc5cENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeUNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eklDOHZJRE15Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG5OMFlYUnBZMTloY25KaGVUeGhjbU0wTG5WcGJuUTRMQ0F6TWo0S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalV4TndvZ0lDQWdMeThnYVdZZ0tDRjBhR2x6TG1selFXUnRhVzRvS1NrZ2V3b2dJQ0FnWTJGc2JITjFZaUJwYzBGa2JXbHVDaUFnSUNCaWJub2dZWEpqTlRWZlkyeGxZWEpUYVdkdVlYUjFjbVZ6WDJGbWRHVnlYMmxtWDJWc2MyVkFNd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5URTRDaUFnSUNBdkx5QjBhR2x6TG05dWJIbFRhV2R1WlhJb0tUc0tJQ0FnSUdOaGJHeHpkV0lnYjI1c2VWTnBaMjVsY2dvS1lYSmpOVFZmWTJ4bFlYSlRhV2R1WVhSMWNtVnpYMkZtZEdWeVgybG1YMlZzYzJWQU16b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pVeU1TMDFNalFLSUNBZ0lDOHZJR052Ym5OMElITnBaMjVoZEhWeVpVSnZlRG9nVkhKaGJuTmhZM1JwYjI1VGFXZHVZWFIxY21WeklEMGdld29nSUNBZ0x5OGdJQ0J1YjI1alpUb2dkSEpoYm5OaFkzUnBiMjVIY205MWNDd0tJQ0FnSUM4dklDQWdZV1JrY21WemN6b2dZV1JrY21WemN3b2dJQ0FnTHk4Z2ZUc0tJQ0FnSUdScFp5QXhDaUFnSUNCcGRHOWlDaUFnSUNCa2FXY2dNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFNallLSUNBZ0lDOHZJR052Ym5OMElITnBaMHhsYm1kMGFEb2dkV2x1ZERZMElEMGdkR2hwY3k1aGNtTTFOVjl6YVdkdVlYUjFjbVZ6S0hOcFoyNWhkSFZ5WlVKdmVDa3ViR1Z1WjNSb093b2dJQ0FnWkhWd0NpQWdJQ0JpYjNoZmJHVnVDaUFnSUNCaGMzTmxjblFnTHk4Z1FtOTRJRzExYzNRZ2FHRjJaU0IyWVd4MVpRb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TlRJM0NpQWdJQ0F2THlCMGFHbHpMbUZ5WXpVMVgzTnBaMjVoZEhWeVpYTW9jMmxuYm1GMGRYSmxRbTk0S1M1a1pXeGxkR1VvS1RzS0lDQWdJR1JwWnlBeENpQWdJQ0JpYjNoZlpHVnNDaUFnSUNCd2IzQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pVek1nb2dJQ0FnTHk4Z1kyOXVjM1FnYldKeVUybG5SR1ZqY21WaGMyVTZJSFZwYm5RMk5DQTlJQ2d5TlRBd0tTQXJJQ2cwTURBZ0tpQW9OREFnS3lCemFXZE1aVzVuZEdncEtUc0tJQ0FnSUhCMWMyaHBiblFnTkRBZ0x5OGdOREFLSUNBZ0lDc0tJQ0FnSUdsdWRHTWdOQ0F2THlBME1EQUtJQ0FnSUNvS0lDQWdJR2x1ZEdNZ05TQXZMeUF5TlRBd0NpQWdJQ0FyQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFNelV0TlRNNENpQWdJQ0F2THlCcGRIaHVMbkJoZVcxbGJuUW9ld29nSUNBZ0x5OGdJQ0J5WldObGFYWmxjam9nVkhodUxuTmxibVJsY2l3S0lDQWdJQzh2SUNBZ1lXMXZkVzUwT2lCdFluSlRhV2RFWldOeVpXRnpaUW9nSUNBZ0x5OGdmU2t1YzNWaWJXbDBLQ2s3Q2lBZ0lDQnBkSGh1WDJKbFoybHVDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvMU16WUtJQ0FnSUM4dklISmxZMlZwZG1WeU9pQlVlRzR1YzJWdVpHVnlMQW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnYVhSNGJsOW1hV1ZzWkNCU1pXTmxhWFpsY2dvZ0lDQWdhWFI0Ymw5bWFXVnNaQ0JCYlc5MWJuUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pVek5TMDFNemdLSUNBZ0lDOHZJR2wwZUc0dWNHRjViV1Z1ZENoN0NpQWdJQ0F2THlBZ0lISmxZMlZwZG1WeU9pQlVlRzR1YzJWdVpHVnlMQW9nSUNBZ0x5OGdJQ0JoYlc5MWJuUTZJRzFpY2xOcFowUmxZM0psWVhObENpQWdJQ0F2THlCOUtTNXpkV0p0YVhRb0tUc0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0JwZEhodVgyWnBaV3hrSUZSNWNHVkZiblZ0Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2FYUjRibDltYVdWc1pDQkdaV1VLSUNBZ0lHbDBlRzVmYzNWaWJXbDBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvMU5EVXROVFE0Q2lBZ0lDQXZMeUJsYldsMFBGTnBaMjVoZEhWeVpVTnNaV0Z5WldRK0tIc0tJQ0FnSUM4dklDQWdkSEpoYm5OaFkzUnBiMjVIY205MWNEb2dkSEpoYm5OaFkzUnBiMjVIY205MWNDd0tJQ0FnSUM4dklDQWdjMmxuYm1WeU9pQmhaR1J5WlhOekNpQWdJQ0F2THlCOUtRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE9EVXhaamMxTXpBZ0x5OGdiV1YwYUc5a0lDSlRhV2R1WVhSMWNtVkRiR1ZoY21Wa0tIVnBiblEyTkN4aFpHUnlaWE56S1NJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8xTVRNdE5URTJDaUFnSUNBdkx5QmhjbU0xTlY5amJHVmhjbE5wWjI1aGRIVnlaWE1vQ2lBZ0lDQXZMeUFnSUhSeVlXNXpZV04wYVc5dVIzSnZkWEE2SUhWcGJuUTJOQ3dLSUNBZ0lDOHZJQ0FnWVdSa2NtVnpjem9nUVdOamIzVnVkQW9nSUNBZ0x5OGdLVG9nZG05cFpDQjdDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T2sxMWJIUnBjMmxuTG05dWJIbFRhV2R1WlhJb0tTQXRQaUIyYjJsa09ncHZibXg1VTJsbmJtVnlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UQTBDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvVkhodUxuTmxibVJsY2lrdWRtRnNkV1VnSVQwOUlEQXBPd29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnUjJ4dlltRnNVM1JoZEdVZ1pYaHBjM1J6Q2lBZ0lDQmhjM05sY25RS0lDQWdJSEpsZEhOMVlnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBOZFd4MGFYTnBaeTVwYzBGa2JXbHVLQ2tnTFQ0Z2RXbHVkRFkwT2dwcGMwRmtiV2x1T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVEl6Q2lBZ0lDQXZMeUJwWmlBb2RHaHBjeTVoY21NMU5WOWhaRzFwYmk1b1lYTldZV3gxWlNrZ2V3b2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPamMwQ2lBZ0lDQXZMeUJoY21NMU5WOWhaRzFwYmlBOUlFZHNiMkpoYkZOMFlYUmxQRUZqWTI5MWJuUStLSHQ5S1RzS0lDQWdJR0o1ZEdWalh6SWdMeThnSW1GeVl6VTFYMkZrYldsdUlnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRJekNpQWdJQ0F2THlCcFppQW9kR2hwY3k1aGNtTTFOVjloWkcxcGJpNW9ZWE5XWVd4MVpTa2dld29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW5vZ2FYTkJaRzFwYmw5bGJITmxYMkp2WkhsQU1nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRJMENpQWdJQ0F2THlCeVpYUjFjbTRnVkhodUxuTmxibVJsY2lBOVBUMGdkR2hwY3k1aGNtTTFOVjloWkcxcGJpNTJZV3gxWlRzS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvM05Bb2dJQ0FnTHk4Z1lYSmpOVFZmWVdSdGFXNGdQU0JIYkc5aVlXeFRkR0YwWlR4QlkyTnZkVzUwUGloN2ZTazdDaUFnSUNCaWVYUmxZMTh5SUM4dklDSmhjbU0xTlY5aFpHMXBiaUlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRXlOQW9nSUNBZ0x5OGdjbVYwZFhKdUlGUjRiaTV6Wlc1a1pYSWdQVDA5SUhSb2FYTXVZWEpqTlRWZllXUnRhVzR1ZG1Gc2RXVTdDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnUFQwS0lDQWdJSEpsZEhOMVlnb0thWE5CWkcxcGJsOWxiSE5sWDJKdlpIbEFNam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRXlOZ29nSUNBZ0x5OGdjbVYwZFhKdUlGUjRiaTV6Wlc1a1pYSWdQVDA5SUVkc2IySmhiQzVqY21WaGRHOXlRV1JrY21WemN6c0tJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJR2RzYjJKaGJDQkRjbVZoZEc5eVFXUmtjbVZ6Y3dvZ0lDQWdQVDBLSUNBZ0lISmxkSE4xWWdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcE5kV3gwYVhOcFp5NWhjbU0xTlY5dFluSlRhV2RKYm1OeVpXRnpaU2h6YVdkdVlYUjFjbVZ6VTJsNlpUb2dkV2x1ZERZMEtTQXRQaUIxYVc1ME5qUTZDbk50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcE5kV3gwYVhOcFp5NWhjbU0xTlY5dFluSlRhV2RKYm1OeVpXRnpaVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXhOaTB5TVRjS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZiV0p5VTJsblNXNWpjbVZoYzJVb2MybG5ibUYwZFhKbGMxTnBlbVU2SUhWcGJuUTJOQ2s2SUhWcGJuUTJOQ0I3Q2lBZ0lDQndjbTkwYnlBeElERUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJeE9Bb2dJQ0FnTHk4Z1kyOXVjM1FnWTNWeWNtVnVkRUpoYkdGdVkyVTZJSFZwYm5RMk5DQTlJRzl3TG1KaGJHRnVZMlVvUjJ4dlltRnNMbU4xY25KbGJuUkJjSEJzYVdOaGRHbHZia0ZrWkhKbGMzTXBPd29nSUNBZ1oyeHZZbUZzSUVOMWNuSmxiblJCY0hCc2FXTmhkR2x2YmtGa1pISmxjM01LSUNBZ0lHSmhiR0Z1WTJVS0lDQWdJR1IxY0FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNakU1Q2lBZ0lDQXZMeUJqYjI1emRDQnRhVzVwYlhWdFFtRnNZVzVqWlRvZ2RXbHVkRFkwSUQwZ2IzQXViV2x1UW1Gc1lXNWpaU2hIYkc5aVlXd3VZM1Z5Y21WdWRFRndjR3hwWTJGMGFXOXVRV1JrY21WemN5azdDaUFnSUNCbmJHOWlZV3dnUTNWeWNtVnVkRUZ3Y0d4cFkyRjBhVzl1UVdSa2NtVnpjd29nSUNBZ2JXbHVYMkpoYkdGdVkyVUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJeU5Bb2dJQ0FnTHk4Z1kyOXVjM1FnYldKeVUybG5VbVZ4ZFdseVpXUTZJSFZwYm5RMk5DQTlJQ2d5TlRBd0tTQXJJQ2cwTURBZ0tpQW9OREFnS3lBeUlDc2djMmxuYm1GMGRYSmxjMU5wZW1VcEtUc0tJQ0FnSUhCMWMyaHBiblFnTkRJZ0x5OGdORElLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ0t3b2dJQ0FnYVc1MFl5QTBJQzh2SURRd01Bb2dJQ0FnS2dvZ0lDQWdhVzUwWXlBMUlDOHZJREkxTURBS0lDQWdJQ3NLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXlOZ29nSUNBZ0x5OGdZMjl1YzNRZ2JtVjNUV2x1YVcxMWJVSmhiR0Z1WTJVNklIVnBiblEyTkNBOUlHMXBibWx0ZFcxQ1lXeGhibU5sSUNzZ2JXSnlVMmxuVW1WeGRXbHlaV1E3Q2lBZ0lDQXJDaUFnSUNCa2RYQUtJQ0FnSUdOdmRtVnlJRElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXlOd29nSUNBZ0x5OGdhV1lnS0dOMWNuSmxiblJDWVd4aGJtTmxJRDQ5SUc1bGQwMXBibWx0ZFcxQ1lXeGhibU5sS1NCN0NpQWdJQ0ErUFFvZ0lDQWdZbm9nYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyMWljbE5wWjBsdVkzSmxZWE5sWDJGbWRHVnlYMmxtWDJWc2MyVkFNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qSTRDaUFnSUNBdkx5QnlaWFIxY200Z01Ec0tJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JtY21GdFpWOWlkWEo1SURBS0lDQWdJSEpsZEhOMVlnb0tjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rMTFiSFJwYzJsbkxtRnlZelUxWDIxaWNsTnBaMGx1WTNKbFlYTmxYMkZtZEdWeVgybG1YMlZzYzJWQU1qb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJek1Rb2dJQ0FnTHk4Z2NtVjBkWEp1SUc1bGQwMXBibWx0ZFcxQ1lXeGhibU5sSUMwZ1kzVnljbVZ1ZEVKaGJHRnVZMlU3Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dNUW9nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUMwS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPazExYkhScGMybG5MbUZ5WXpVMVgyMWljbFI0YmtsdVkzSmxZWE5sS0hSeVlXNXpZV04wYVc5dVUybDZaVG9nZFdsdWREWTBLU0F0UGlCMWFXNTBOalE2Q25OdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBOZFd4MGFYTnBaeTVoY21NMU5WOXRZbkpVZUc1SmJtTnlaV0Z6WlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakl6T1MweU5EQUtJQ0FnSUM4dklFQnlaV0ZrYjI1c2VRb2dJQ0FnTHk4Z1lYSmpOVFZmYldKeVZIaHVTVzVqY21WaGMyVW9kSEpoYm5OaFkzUnBiMjVUYVhwbE9pQjFhVzUwTmpRcE9pQjFhVzUwTmpRZ2V3b2dJQ0FnY0hKdmRHOGdNU0F4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lOREVLSUNBZ0lDOHZJR052Ym5OMElHTjFjbkpsYm5SQ1lXeGhibU5sT2lCMWFXNTBOalFnUFNCdmNDNWlZV3hoYm1ObEtFZHNiMkpoYkM1amRYSnlaVzUwUVhCd2JHbGpZWFJwYjI1QlpHUnlaWE56S1RzS0lDQWdJR2RzYjJKaGJDQkRkWEp5Wlc1MFFYQndiR2xqWVhScGIyNUJaR1J5WlhOekNpQWdJQ0JpWVd4aGJtTmxDaUFnSUNCa2RYQUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJME1nb2dJQ0FnTHk4Z1kyOXVjM1FnYldsdWFXMTFiVUpoYkdGdVkyVTZJSFZwYm5RMk5DQTlJRzl3TG0xcGJrSmhiR0Z1WTJVb1IyeHZZbUZzTG1OMWNuSmxiblJCY0hCc2FXTmhkR2x2YmtGa1pISmxjM01wT3dvZ0lDQWdaMnh2WW1Gc0lFTjFjbkpsYm5SQmNIQnNhV05oZEdsdmJrRmtaSEpsYzNNS0lDQWdJRzFwYmw5aVlXeGhibU5sQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lORGNLSUNBZ0lDOHZJR052Ym5OMElHMWljbFI0YmxKbGNYVnBjbVZrT2lCMWFXNTBOalFnUFNBb01qVXdNQ2tnS3lBb05EQXdJQ29nS0RrZ0t5QjBjbUZ1YzJGamRHbHZibE5wZW1VcEtUc0tJQ0FnSUhCMWMyaHBiblFnT1NBdkx5QTVDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUNzS0lDQWdJR2x1ZEdNZ05DQXZMeUEwTURBS0lDQWdJQ29LSUNBZ0lHbHVkR01nTlNBdkx5QXlOVEF3Q2lBZ0lDQXJDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU5Ea0tJQ0FnSUM4dklHTnZibk4wSUc1bGQwMXBibWx0ZFcxQ1lXeGhibU5sT2lCMWFXNTBOalFnUFNCdGFXNXBiWFZ0UW1Gc1lXNWpaU0FySUcxaWNsUjRibEpsY1hWcGNtVmtPd29nSUNBZ0t3b2dJQ0FnWkhWd0NpQWdJQ0JqYjNabGNpQXlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU5UQUtJQ0FnSUM4dklHbG1JQ2hqZFhKeVpXNTBRbUZzWVc1alpTQStQU0J1WlhkTmFXNXBiWFZ0UW1Gc1lXNWpaU2tnZXdvZ0lDQWdQajBLSUNBZ0lHSjZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcE5kV3gwYVhOcFp5NWhjbU0xTlY5dFluSlVlRzVKYm1OeVpXRnpaVjloWm5SbGNsOXBabDlsYkhObFFESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJMU1Rb2dJQ0FnTHk4Z2NtVjBkWEp1SURBN0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdabkpoYldWZlluVnllU0F3Q2lBZ0lDQnlaWFJ6ZFdJS0NuTnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwTmRXeDBhWE5wWnk1aGNtTTFOVjl0WW5KVWVHNUpibU55WldGelpWOWhablJsY2w5cFpsOWxiSE5sUURJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TlRRS0lDQWdJQzh2SUdOdmJuTjBJSEpsYzNWc2REb2dkV2x1ZERZMElEMGdibVYzVFdsdWFXMTFiVUpoYkdGdVkyVWdMU0JqZFhKeVpXNTBRbUZzWVc1alpUc0tJQ0FnSUdaeVlXMWxYMlJwWnlBeENpQWdJQ0JtY21GdFpWOWthV2NnTUFvZ0lDQWdMUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qVTFDaUFnSUNBdkx5QnlaWFIxY200Z2NtVnpkV3gwT3dvZ0lDQWdabkpoYldWZlluVnllU0F3Q2lBZ0lDQnlaWFJ6ZFdJSyIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZbUZ6WlMxamIyNTBjbUZqZEM1a0xuUnpPanBDWVhObFEyOXVkSEpoWTNRdVkyeGxZWEpUZEdGMFpWQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0J3ZFhOb2FXNTBJREVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZz09In0sImJ5dGVDb2RlIjp7ImFwcHJvdmFsIjoiQ3lBR0FBRUlJSkFEeEJNbUJRUVZIM3gxQzJGeVl6VTFYMjV2Ym1ObEMyRnlZelUxWDJGa2JXbHVEMkZ5WXpVMVgzUm9jbVZ6YUc5c1pBUjVpQ0xoTVJoQUFBWXJJbWNwSW1jeEcwRUFmakVaRkVReEdFU0NEQVJOQWNoNUJDSkZacThFTzkyWHF3Uit6eVhBQkVNaGdRNEVzYVc1cHdUSTNrSm1CQWV2cGNvRXkwa0w4Z1FrZ1dCWEJBWVJoQ0lFU3V5OG95Y0VnZ01FOElaKzhRUnYyTjRGQkJFYmlTWTJHZ0NPRUFBTEFCWUFJQUF0QUZJQWJ3Q0RBSjRBc1FERUFZVUJud0pQQW1jQ3NBTUdBREVaRkRFWUZCQkVJME1pSzJWRUZpaE1VTEFqUXlJcVpVUW9URkN3STBNaUtXVkVJd2dXS0V4UXNDTkROaG9CU1JVa0VrUVhOaG9DU1JVakVrUk1Ga3hRdmtSSkZSWlhCZ0pNVUNoTVVMQWpRellhQVVrVkpCSkVGellhQWtrVkpSSkVUQlpNVUw1RUtFeFFzQ05ETmhvQlNSVWtFa1FYRmlKTVpVUW9URkN3STBNMkdnRkpGU1VTUkNKTVpVUWlFNEFCQUNKUEFsUW9URkN3STBNMkdnRkpGU1FTUkJlSUFzSVdLRXhRc0NORE5ob0JTUlVrRWtRWGlBTFlGaWhNVUxBalF5SkhBb0FBU1RZYUFVa1ZJeEpFTmhvQ1J3SWlXVWxPQWlVTGdRSUlUQlVTUkNJcFpVUVVSQ0lxWlVVQlFRQ0pNUUFpS21WRUVrUkxBaGRKUkN0TVp5a2laeUpGQkVzREZrbEZCeUpNWlVVQlFRQWlJa3NHWlV4SlRnSkZDa1FpVEdWRkFVRUFCa3NIYVVzRmFVc0RJd2hGQkVMLzBDSkZCVXNFU3dFTVFRQTJTd0ZYQWdCTEJVbE9BaVVMSlZoSlJRbE1Ga3NCWnlKTVpVVUJRQUFFU3dZaVp5SkxCMGxPQW1WRUl3aG5Td1FqQ0VVRlF2L0NJME14QURJSkVrUkMvM2FJQWM1QUFBT0lBY0VpS1dWRUl3Z3BTd0ZuRmloTVVMQWpReUpKZ0FBeEZpTUpPQkFqRWtRMkdnRkpGU1FTUkJjMkdnSkpGU01TUkRZYUEwa2lXWUVDQ0VzQkZSSkVWd0lBaUFHRlFBQURpQUY0U3dKSlJDSXBaVVJMQVE5RUZrc0NVRVVHTVJZakNFbEZCVElFREVBQUgwbEZCVXNFU1JXSUFaVklTd1pKdkVoSlR3Sy9nQVFZU2FXVVRGQ3dJME5KUlFWTEEwazRFSUVHRWtRNEdERVlFa0VBRmtzRElzSWFKd1FTUVFBTFN3TWp3aHBMQlV4UVJRVkxBeU1JU1VVRk1nUU1RUC9NUXYrcU5ob0JTU0paZ1FJSVRCVVNSSWdBOTBBQUE0Z0E2aU5ETmhvQlNSVWtFa1FYTmhvQ1NSVWpFa1NJQU50QUFBT0lBTTVMQVJaTEFWQkp2VVJMQWJ4SWdRa0lJUVFMSVFVSXNURUFzZ2V5Q0NPeUVDS3lBYk9BQkQ2YkxLVk1VTEFqUXpFV0l3bEpPQkFqRWtRMkdnRkpGU1FTUkJjMkdnSkpJbG1CUUF0SmdRSUlTd0lWRWtTSUFIZUlBSkZMQXpnSE1nb1NSRThET0FnT1JERUFUd0lXU1U4Q1VFbThTRThDdnpFQVVJQUU3UHZMTTB4UXNDTkROaG9CU1JVa0VrUVhOaG9DU1JVbEVrU0lBRHhBQUFPSUFDOUxBUlpMQVZCSnZVUkxBYnhJZ1NnSUlRUUxJUVVJc1RFQXNnZXlDQ095RUNLeUFiT0FCSVVmZFRCTVVMQWpReUl4QUdWRVJJa2lLbVZGQVVFQUNERUFJaXBsUkJLSk1RQXlDUktKaWdFQk1ncGdTVElLZUlFcWkvOElJUVFMSVFVSUNFbE9BZzlCQUFRaWpBQ0ppd0dMQUFtTUFJbUtBUUV5Q21CSk1ncDRnUW1ML3dnaEJBc2hCUWdJU1U0Q0QwRUFCQ0tNQUltTEFZc0FDWXdBaVE9PSIsImNsZWFyIjoiQzRFQlF3PT0ifSwiY29tcGlsZXJJbmZvIjp7ImNvbXBpbGVyIjoicHV5YSIsImNvbXBpbGVyVmVyc2lvbiI6eyJtYWpvciI6NSwibWlub3IiOjMsInBhdGNoIjoyLCJjb21taXRIYXNoIjpudWxsfX0sImV2ZW50cyI6W3sibmFtZSI6IlRyYW5zYWN0aW9uQWRkZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25JbmRleCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiVHJhbnNhY3Rpb25SZW1vdmVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uSW5kZXgiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6IlNpZ25hdHVyZVNldCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25lciIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiU2lnbmF0dXJlQ2xlYXJlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25lciIsImRlc2MiOm51bGx9XX1dLCJ0ZW1wbGF0ZVZhcmlhYmxlcyI6e30sInNjcmF0Y2hWYXJpYWJsZXMiOnt9fQ==";
    }

}
