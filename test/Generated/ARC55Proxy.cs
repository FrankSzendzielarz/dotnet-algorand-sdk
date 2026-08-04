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

namespace ARC55Regression
{


    public class ARC55Proxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public ARC55Proxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class TransactionGroup : AVMObjectType
            {
                public ulong Nonce { get; set; }

                public byte Index { get; set; }

                public byte SignerIndex { get; set; }

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
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSignerIndex = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vSignerIndex.From(SignerIndex);
                    ret.AddRange(vSignerIndex.Encode());
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
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSignerIndex = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vSignerIndex.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueSignerIndex = vSignerIndex.ToValue();
                    if (valueSignerIndex is byte vSignerIndexValue) { ret.SignerIndex = vSignerIndexValue; }
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
        ///Retrieve a transaction from a given transaction group for a specific signer
        ///</summary>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="transactionIndex">Index of transaction within group </param>
        /// <param name="signerIndex">Index of the signer (determines which encrypted version to retrieve) </param>
        public async Task<byte[]> Arc55GetTransaction(ulong transactionGroup, byte transactionIndex, byte signerIndex, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 60, 165, 17, 246 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var transactionIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); transactionIndexAbi.From(transactionIndex);
            var signerIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); signerIndexAbi.From(signerIndex);

            var result = await base.SimApp(new List<object> { abiHandle, transactionGroupAbi, transactionIndexAbi, signerIndexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> Arc55GetTransaction_Transactions(ulong transactionGroup, byte transactionIndex, byte signerIndex, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 60, 165, 17, 246 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var transactionIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); transactionIndexAbi.From(transactionIndex);
            var signerIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); signerIndexAbi.From(signerIndex);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionGroupAbi, transactionIndexAbi, signerIndexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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
        ///With signer_index included, each signer stores a different encrypted version
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
        ///Add a transaction to an existing group. Only one transaction should be included per call.
        ///For encrypted transactions, the admin encrypts the same transaction differently for each signer,
        ///so each signer stores their own version.
        ///</summary>
        /// <param name="costs">Minimum Balance Requirement for associated box storage costs </param>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="index">Transaction position within atomic group to add </param>
        /// <param name="signerIndex">Index of the signer (determines storage location for encrypted version) </param>
        /// <param name="transaction">Transaction to add (encrypted or unencrypted) </param>
        public async Task Arc55AddTransaction(PaymentTransaction costs, ulong transactionGroup, byte index, byte signerIndex, byte[] transaction, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { costs });
            byte[] abiHandle = { 187, 183, 207, 151 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);
            var signerIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); signerIndexAbi.From(signerIndex);
            var transactionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); transactionAbi.From(transaction);

            var result = await base.CallApp(new List<object> { abiHandle, costs, transactionGroupAbi, indexAbi, signerIndexAbi, transactionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55AddTransaction_Transactions(PaymentTransaction costs, ulong transactionGroup, byte index, byte signerIndex, byte[] transaction, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { costs });
            byte[] abiHandle = { 187, 183, 207, 151 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);
            var signerIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); signerIndexAbi.From(signerIndex);
            var transactionAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); transactionAbi.From(transaction);

            return await base.MakeTransactionList(new List<object> { abiHandle, costs, transactionGroupAbi, indexAbi, signerIndexAbi, transactionAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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
        ///Remove transaction from the app for a specific signer. The MBR associated with the transaction will be returned to the transaction sender.
        ///</summary>
        /// <param name="transactionGroup">Transaction Group nonce </param>
        /// <param name="index">Transaction position within atomic group to remove </param>
        /// <param name="signerIndex">Index of the signer whose encrypted version to remove </param>
        public async Task Arc55RemoveTransaction(ulong transactionGroup, byte index, byte signerIndex, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 237, 194, 109, 239 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);
            var signerIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); signerIndexAbi.From(signerIndex);

            var result = await base.CallApp(new List<object> { abiHandle, transactionGroupAbi, indexAbi, signerIndexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Arc55RemoveTransaction_Transactions(ulong transactionGroup, byte index, byte signerIndex, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 237, 194, 109, 239 };
            var transactionGroupAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); transactionGroupAbi.From(transactionGroup);
            var indexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); indexAbi.From(index);
            var signerIndexAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Byte(); signerIndexAbi.From(signerIndex);

            return await base.MakeTransactionList(new List<object> { abiHandle, transactionGroupAbi, indexAbi, signerIndexAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiQVJDNTUiLCJkZXNjIjpudWxsLCJuZXR3b3JrcyI6e30sInN0cnVjdHMiOnsiVHJhbnNhY3Rpb25Hcm91cCI6W3sibmFtZSI6Im5vbmNlIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImluZGV4IiwidHlwZSI6InVpbnQ4In0seyJuYW1lIjoic2lnbmVyX2luZGV4IiwidHlwZSI6InVpbnQ4In1dLCJUcmFuc2FjdGlvblNpZ25hdHVyZXMiOlt7Im5hbWUiOiJub25jZSIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJhZGRyZXNzIiwidHlwZSI6ImFkZHJlc3MifV19LCJNZXRob2RzIjpbeyJuYW1lIjoiYXJjNTVfZ2V0VGhyZXNob2xkIiwiZGVzYyI6IlJldHJpZXZlIHRoZSBzaWduYXR1cmUgdGhyZXNob2xkIHJlcXVpcmVkIGZvciB0aGUgbXVsdGlzaWduYXR1cmUgdG8gYmUgc3VibWl0dGVkIiwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6Ik11bHRpc2lnbmF0dXJlIHRocmVzaG9sZCJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfZ2V0QWRtaW4iLCJkZXNjIjoiUmV0cmlldmVzIHRoZSBhZG1pbiBhZGRyZXNzLCByZXNwb25zaWJsZSBmb3IgY2FsbGluZyBhcmM1NV9zZXR1cCIsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJkZXNjIjoiQWRtaW4gYWRkcmVzcyJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfbmV4dFRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjoiTmV4dCBleHBlY3RlZCBUcmFuc2FjdGlvbiBHcm91cCBub25jZSJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfZ2V0VHJhbnNhY3Rpb24iLCJkZXNjIjoiUmV0cmlldmUgYSB0cmFuc2FjdGlvbiBmcm9tIGEgZ2l2ZW4gdHJhbnNhY3Rpb24gZ3JvdXAgZm9yIGEgc3BlY2lmaWMgc2lnbmVyIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjoiVHJhbnNhY3Rpb24gR3JvdXAgbm9uY2UiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25JbmRleCIsImRlc2MiOiJJbmRleCBvZiB0cmFuc2FjdGlvbiB3aXRoaW4gZ3JvdXAiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVySW5kZXgiLCJkZXNjIjoiSW5kZXggb2YgdGhlIHNpZ25lciAoZGV0ZXJtaW5lcyB3aGljaCBlbmNyeXB0ZWQgdmVyc2lvbiB0byByZXRyaWV2ZSkiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJkZXNjIjoiQSBzaW5nbGUgdHJhbnNhY3Rpb24gYXQgdGhlIHNwZWNpZmllZCBpbmRleCBmb3IgdGhlIHRyYW5zYWN0aW9uIGdyb3VwIG5vbmNlIGFuZCBzaWduZXIifSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzU1X2dldFNpZ25hdHVyZXMiLCJkZXNjIjoiUmV0cmlldmUgYSBsaXN0IG9mIHNpZ25hdHVyZXMgZm9yIGEgZ2l2ZW4gdHJhbnNhY3Rpb24gZ3JvdXAgbm9uY2UgYW5kIGFkZHJlc3MiLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJUcmFuc2FjdGlvbiBHcm91cCBub25jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25lciIsImRlc2MiOiJBY2NvdW50IHlvdSB3YW50IHRvIHJldHJpZXZlIHNpZ25hdHVyZXMgZm9yIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbNjRdW10iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJBcnJheSBvZiBzaWduYXR1cmVzIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM1NV9nZXRTaWduZXJCeUluZGV4IiwiZGVzYyI6IkZpbmQgb3V0IHdoaWNoIGFkZHJlc3MgaXMgYXQgdGhpcyBpbmRleCBvZiB0aGUgbXVsdGlzaWduYXR1cmUiLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiaW5kZXgiLCJkZXNjIjoiQWNjb3VudCBhdCB0aGlzIGluZGV4IG9mIHRoZSBtdWx0aXNpZ25hdHVyZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJkZXNjIjoiQWNjb3VudCBhdCBpbmRleCJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfaXNTaWduZXIiLCJkZXNjIjoiQ2hlY2sgaWYgYW4gYWRkcmVzcyBpcyBhIG1lbWJlciBvZiB0aGUgbXVsdGlzaWduYXR1cmUiLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImFkZHJlc3MiLCJkZXNjIjoiQWNjb3VudCB0byBjaGVjayBpcyBhIHNpZ25lciIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjoiVHJ1ZSBpZiBhZGRyZXNzIGlzIGEgc2lnbmVyIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM1NV9tYnJTaWdJbmNyZWFzZSIsImRlc2MiOiJDYWxjdWxhdGUgdGhlIG1pbmltdW0gYmFsYW5jZSByZXF1aXJlbWVudCBmb3Igc3RvcmluZyBhIHNpZ25hdHVyZSIsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzaWduYXR1cmVzU2l6ZSIsImRlc2MiOiJTaXplIChpbiBieXRlcykgb2YgdGhlIHNpZ25hdHVyZXMgdG8gc3RvcmUiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJkZXNjIjoiTWluaW11bSBiYWxhbmNlIHJlcXVpcmVtZW50IGluY3JlYXNlIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmM1NV9tYnJUeG5JbmNyZWFzZSIsImRlc2MiOiJDYWxjdWxhdGUgdGhlIG1pbmltdW0gYmFsYW5jZSByZXF1aXJlbWVudCBmb3Igc3RvcmluZyBhIHRyYW5zYWN0aW9uXG5XaXRoIHNpZ25lcl9pbmRleCBpbmNsdWRlZCwgZWFjaCBzaWduZXIgc3RvcmVzIGEgZGlmZmVyZW50IGVuY3J5cHRlZCB2ZXJzaW9uIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uU2l6ZSIsImRlc2MiOiJTaXplIChpbiBieXRlcykgb2YgdGhlIHRyYW5zYWN0aW9uIHRvIHN0b3JlIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6Ik1pbmltdW0gYmFsYW5jZSByZXF1aXJlbWVudCBpbmNyZWFzZSJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfc2V0dXAiLCJkZXNjIjoiU2V0dXAgT24tQ2hhaW4gTXNpZyBBcHAuIFRoaXMgY2FuIG9ubHkgYmUgY2FsbGVkIHdoaWxzdCBubyB0cmFuc2FjdGlvbiBncm91cHMgaGF2ZSBiZWVuIGNyZWF0ZWQuIiwiYXJncyI6W3sidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidGhyZXNob2xkIiwiZGVzYyI6IkluaXRpYWwgbXVsdGlzaWcgdGhyZXNob2xkLCBtdXN0IGJlIGdyZWF0ZXIgdGhhbiAwIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyZXNzZXMiLCJkZXNjIjoiQXJyYXkgb2YgYWRkcmVzc2VzIHRoYXQgbWFrZSB1cCB0aGUgbXVsdGlzaWciLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfbmV3VHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJHZW5lcmF0ZSBhIG5ldyB0cmFuc2FjdGlvbiBncm91cCBub25jZSBmb3IgaG9sZGluZyBwZW5kaW5nIHRyYW5zYWN0aW9ucyIsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJ0cmFuc2FjdGlvbkdyb3VwIFRyYW5zYWN0aW9uIEdyb3VwIG5vbmNlIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfYWRkVHJhbnNhY3Rpb24iLCJkZXNjIjoiQWRkIGEgdHJhbnNhY3Rpb24gdG8gYW4gZXhpc3RpbmcgZ3JvdXAuIE9ubHkgb25lIHRyYW5zYWN0aW9uIHNob3VsZCBiZSBpbmNsdWRlZCBwZXIgY2FsbC5cbkZvciBlbmNyeXB0ZWQgdHJhbnNhY3Rpb25zLCB0aGUgYWRtaW4gZW5jcnlwdHMgdGhlIHNhbWUgdHJhbnNhY3Rpb24gZGlmZmVyZW50bHkgZm9yIGVhY2ggc2lnbmVyLFxuc28gZWFjaCBzaWduZXIgc3RvcmVzIHRoZWlyIG93biB2ZXJzaW9uLiIsImFyZ3MiOlt7InR5cGUiOiJwYXkiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJjb3N0cyIsImRlc2MiOiJNaW5pbXVtIEJhbGFuY2UgUmVxdWlyZW1lbnQgZm9yIGFzc29jaWF0ZWQgYm94IHN0b3JhZ2UgY29zdHMiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjoiVHJhbnNhY3Rpb24gR3JvdXAgbm9uY2UiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiaW5kZXgiLCJkZXNjIjoiVHJhbnNhY3Rpb24gcG9zaXRpb24gd2l0aGluIGF0b21pYyBncm91cCB0byBhZGQiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVySW5kZXgiLCJkZXNjIjoiSW5kZXggb2YgdGhlIHNpZ25lciAoZGV0ZXJtaW5lcyBzdG9yYWdlIGxvY2F0aW9uIGZvciBlbmNyeXB0ZWQgdmVyc2lvbikiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uIiwiZGVzYyI6IlRyYW5zYWN0aW9uIHRvIGFkZCAoZW5jcnlwdGVkIG9yIHVuZW5jcnlwdGVkKSIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiVHJhbnNhY3Rpb25BZGRlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkluZGV4IiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfYWRkVHJhbnNhY3Rpb25Db250aW51ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb24iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfcmVtb3ZlVHJhbnNhY3Rpb24iLCJkZXNjIjoiUmVtb3ZlIHRyYW5zYWN0aW9uIGZyb20gdGhlIGFwcCBmb3IgYSBzcGVjaWZpYyBzaWduZXIuIFRoZSBNQlIgYXNzb2NpYXRlZCB3aXRoIHRoZSB0cmFuc2FjdGlvbiB3aWxsIGJlIHJldHVybmVkIHRvIHRoZSB0cmFuc2FjdGlvbiBzZW5kZXIuIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjoiVHJhbnNhY3Rpb24gR3JvdXAgbm9uY2UiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiaW5kZXgiLCJkZXNjIjoiVHJhbnNhY3Rpb24gcG9zaXRpb24gd2l0aGluIGF0b21pYyBncm91cCB0byByZW1vdmUiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVySW5kZXgiLCJkZXNjIjoiSW5kZXggb2YgdGhlIHNpZ25lciB3aG9zZSBlbmNyeXB0ZWQgdmVyc2lvbiB0byByZW1vdmUiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6IlRyYW5zYWN0aW9uUmVtb3ZlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkluZGV4IiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfc2V0U2lnbmF0dXJlcyIsImRlc2MiOiJTZXQgc2lnbmF0dXJlcyBmb3IgYSBwYXJ0aWN1bGFyIHRyYW5zYWN0aW9uIGdyb3VwLiBTaWduYXR1cmVzIG11c3QgYmUgaW5jbHVkZWQgYXMgYW4gYXJyYXkgb2YgYnl0ZS1hcnJheXMiLCJhcmdzIjpbeyJ0eXBlIjoicGF5Iiwic3RydWN0IjpudWxsLCJuYW1lIjoiY29zdHMiLCJkZXNjIjoiTWluaW11bSBCYWxhbmNlIFJlcXVpcmVtZW50IGZvciBhc3NvY2lhdGVkIGJveCBzdG9yYWdlIGNvc3RzOiAoMjUwMCkgKyAoNDAwICogKDQwICsgc2lnbmF0dXJlcy5sZW5ndGgpKSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOiJUcmFuc2FjdGlvbiBHcm91cCBub25jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVs2NF1bXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25hdHVyZXMiLCJkZXNjIjoiQXJyYXkgb2Ygc2lnbmF0dXJlcyIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiU2lnbmF0dXJlU2V0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVyIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjNTVfY2xlYXJTaWduYXR1cmVzIiwiZGVzYyI6IkNsZWFyIHNpZ25hdHVyZXMgZm9yIGFuIGFkZHJlc3MuIEJlIGF3YXJlIHRoaXMgb25seSByZW1vdmVzIGl0IGZyb20gdGhlIGN1cnJlbnQgc3RhdGUgb2YgdGhlIGxlZGdlciwgYW5kIGluZGV4ZXJzIHdpbGwgc3RpbGwga25vdyBhbmQgY291bGQgdXNlIHlvdXIgc2lnbmF0dXJlIiwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjoiVHJhbnNhY3Rpb24gR3JvdXAgbm9uY2UiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyZXNzIiwiZGVzYyI6IkFjY291bnQgd2hvc2Ugc2lnbmF0dXJlcyB0byBjbGVhciIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiU2lnbmF0dXJlQ2xlYXJlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNpZ25lciIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19XSwic3RhdGUiOnsic2NoZW1hIjp7Imdsb2JhbCI6eyJpbnRzIjoyLCJieXRlcyI6MX0sImxvY2FsIjp7ImludHMiOjAsImJ5dGVzIjowfX0sImtleXMiOnsiZ2xvYmFsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJib3giOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9fSwibWFwcyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImxvY2FsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9LCJib3giOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH19fSwiYmFyZUFjdGlvbnMiOnsiY3JlYXRlIjpbIk5vT3AiXSwiY2FsbCI6W119LCJzb3VyY2VJbmZvIjp7ImFwcHJvdmFsIjp7InNvdXJjZUluZm8iOlt7InBjIjpbMjgzLDMyMCw4NzEsMTAzMV0sImVycm9yTWVzc2FnZSI6IkJveCBtdXN0IGhhdmUgdmFsdWUiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls4Ml0sImVycm9yTWVzc2FnZSI6Ik9uQ29tcGxldGlvbiBtdXN0IGJlIE5vT3AiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyMTFdLCJlcnJvck1lc3NhZ2UiOiJPbkNvbXBsZXRpb24gbXVzdCBiZSBOb09wICYmIGNhbiBvbmx5IGNhbGwgd2hlbiBjcmVhdGluZyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIxNywyMjksMjM5LDM0MCwzNTgsNDQ5LDQ5Niw1NzEsNjAwLDEwNzRdLCJlcnJvck1lc3NhZ2UiOiJjaGVjayBHbG9iYWxTdGF0ZSBleGlzdHMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls1NDNdLCJlcnJvck1lc3NhZ2UiOiJpbmRleCBhY2Nlc3MgaXMgb3V0IG9mIGJvdW5kcyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQzMyw2NTgsODAyLDkzM10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgYXJyYXkgbGVuZ3RoIGhlYWRlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ0NV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LmR5bmFtaWNfYXJyYXk8YWNjb3VudD4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2NjYsODA5XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuZHluYW1pY19hcnJheTxhcmM0LnVpbnQ4PiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6Wzk0NV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LmR5bmFtaWNfYXJyYXk8Ynl0ZXNbNjRdPiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzMxNCwzNTQsMTAxMV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnN0YXRpY19hcnJheTxhcmM0LnVpbnQ4LCAzMj4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyNTYsMzA1LDMzNCwzODEsNDAwLDYzNSw4MzAsOTI2LDEwMDJdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC51aW50NjQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyNjUsMjczLDQyNCw2NDQsNjUyLDgzOSw4NDddLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC51aW50OCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6Wzc1MV0sImVycm9yTWVzc2FnZSI6InRyYW5zYWN0aW9uIHR5cGUgaXMgYXBwbCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzYyNyw5MThdLCJlcnJvck1lc3NhZ2UiOiJ0cmFuc2FjdGlvbiB0eXBlIGlzIHBheSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH1dLCJwY09mZnNldE1ldGhvZCI6Im5vbmUifSwiY2xlYXIiOnsic291cmNlSW5mbyI6W10sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9fSwic291cmNlIjp7ImFwcHJvdmFsIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZWEpqTkM5cGJtUmxlQzVrTG5Sek9qcERiMjUwY21GamRDNWhjSEJ5YjNaaGJGQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0JwYm5SallteHZZMnNnTVNBd0lEZ2dNeklnTkRBd0lESTFNREFLSUNBZ0lHSjVkR1ZqWW14dlkyc2dNSGd4TlRGbU4yTTNOU0FpWVhKak5UVmZibTl1WTJVaUlDSmhjbU0xTlY5MGFISmxjMmh2YkdRaUlEQjROems0T0RJeVpURWdJbUZ5WXpVMVgyRmtiV2x1SWdvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUdKdWVpQnRZV2x1WDJGbWRHVnlYMmxtWDJWc2MyVkFNZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk56VUtJQ0FnSUM4dklHRnlZelUxWDNSb2NtVnphRzlzWkNBOUlFZHNiMkpoYkZOMFlYUmxQSFZwYm5RMk5ENG9leUJwYm1sMGFXRnNWbUZzZFdVNklGVnBiblEyTkNnd0tTQjlLVHNLSUNBZ0lHSjVkR1ZqWHpJZ0x5OGdJbUZ5WXpVMVgzUm9jbVZ6YUc5c1pDSUtJQ0FnSUdsdWRHTmZNU0F2THlBd0NpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TnpnS0lDQWdJQzh2SUdGeVl6VTFYMjV2Ym1ObElEMGdSMnh2WW1Gc1UzUmhkR1U4ZFdsdWREWTBQaWg3SUdsdWFYUnBZV3hXWVd4MVpUb2dWV2x1ZERZMEtEQXBJSDBwT3dvZ0lDQWdZbmwwWldOZk1TQXZMeUFpWVhKak5UVmZibTl1WTJVaUNpQWdJQ0JwYm5Salh6RWdMeThnTUFvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0NtMWhhVzVmWVdaMFpYSmZhV1pmWld4elpVQXlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5qa0tJQ0FnSUM4dklHVjRjRzl5ZENCamJHRnpjeUJCVWtNMU5TQmxlSFJsYm1SeklFTnZiblJ5WVdOMElIc0tJQ0FnSUhSNGJpQk9kVzFCY0hCQmNtZHpDaUFnSUNCaWVpQnRZV2x1WDE5ZllXeG5iM1J6WDE4dVpHVm1ZWFZzZEVOeVpXRjBaVUF5TlFvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRTl1UTI5dGNHeGxkR2x2YmlCdGRYTjBJR0psSUU1dlQzQUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RS0lDQWdJSEIxYzJoaWVYUmxjM01nTUhnMFpEQXhZemczT1NBd2VESXlORFUyTm1GbUlEQjRNMkprWkRrM1lXSWdNSGd6WTJFMU1URm1OaUF3ZURRek1qRTRNVEJsSURCNFlqRmhOV0k1WVRjZ01IaGpPR1JsTkRJMk5pQXdlREEzWVdaaE5XTmhJREI0WTJJME9UQmlaaklnTUhneU5EZ3hOakExTnlBd2VEQTJNVEU0TkRJeUlEQjRZbUppTjJObU9UY2dMeThnYldWMGFHOWtJQ0poY21NMU5WOW5aWFJVYUhKbGMyaHZiR1FvS1hWcGJuUTJOQ0lzSUcxbGRHaHZaQ0FpWVhKak5UVmZaMlYwUVdSdGFXNG9LV0ZrWkhKbGMzTWlMQ0J0WlhSb2IyUWdJbUZ5WXpVMVgyNWxlSFJVY21GdWMyRmpkR2x2YmtkeWIzVndLQ2wxYVc1ME5qUWlMQ0J0WlhSb2IyUWdJbUZ5WXpVMVgyZGxkRlJ5WVc1ellXTjBhVzl1S0hWcGJuUTJOQ3gxYVc1ME9DeDFhVzUwT0NsaWVYUmxXMTBpTENCdFpYUm9iMlFnSW1GeVl6VTFYMmRsZEZOcFoyNWhkSFZ5WlhNb2RXbHVkRFkwTEdGa1pISmxjM01wWW5sMFpWczJORjFiWFNJc0lHMWxkR2h2WkNBaVlYSmpOVFZmWjJWMFUybG5ibVZ5UW5sSmJtUmxlQ2gxYVc1ME5qUXBZV1JrY21WemN5SXNJRzFsZEdodlpDQWlZWEpqTlRWZmFYTlRhV2R1WlhJb1lXUmtjbVZ6Y3lsaWIyOXNJaXdnYldWMGFHOWtJQ0poY21NMU5WOXRZbkpUYVdkSmJtTnlaV0Z6WlNoMWFXNTBOalFwZFdsdWREWTBJaXdnYldWMGFHOWtJQ0poY21NMU5WOXRZbkpVZUc1SmJtTnlaV0Z6WlNoMWFXNTBOalFwZFdsdWREWTBJaXdnYldWMGFHOWtJQ0poY21NMU5WOXpaWFIxY0NoMWFXNTBPQ3hoWkdSeVpYTnpXMTBwZG05cFpDSXNJRzFsZEdodlpDQWlZWEpqTlRWZmJtVjNWSEpoYm5OaFkzUnBiMjVIY205MWNDZ3BkV2x1ZERZMElpd2diV1YwYUc5a0lDSmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZiaWh3WVhrc2RXbHVkRFkwTEhWcGJuUTRMSFZwYm5RNExHSjVkR1ZiWFNsMmIybGtJZ29nSUNBZ1lubDBaV05mTXlBdkx5QnRaWFJvYjJRZ0ltRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVRMjl1ZEdsdWRXVmtLR0o1ZEdWYlhTbDJiMmxrSWdvZ0lDQWdjSFZ6YUdKNWRHVnpjeUF3ZUdWa1l6STJaR1ZtSURCNE5tWmtPR1JsTURVZ01IZ3hNVEZpT0RreU5pQXZMeUJ0WlhSb2IyUWdJbUZ5WXpVMVgzSmxiVzkyWlZSeVlXNXpZV04wYVc5dUtIVnBiblEyTkN4MWFXNTBPQ3gxYVc1ME9DbDJiMmxrSWl3Z2JXVjBhRzlrSUNKaGNtTTFOVjl6WlhSVGFXZHVZWFIxY21WektIQmhlU3gxYVc1ME5qUXNZbmwwWlZzMk5GMWJYU2wyYjJsa0lpd2diV1YwYUc5a0lDSmhjbU0xTlY5amJHVmhjbE5wWjI1aGRIVnlaWE1vZFdsdWREWTBMR0ZrWkhKbGMzTXBkbTlwWkNJS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURBS0lDQWdJRzFoZEdOb0lHRnlZelUxWDJkbGRGUm9jbVZ6YUc5c1pDQmhjbU0xTlY5blpYUkJaRzFwYmlCaGNtTTFOVjl1WlhoMFZISmhibk5oWTNScGIyNUhjbTkxY0NCaGNtTTFOVjluWlhSVWNtRnVjMkZqZEdsdmJpQmhjbU0xTlY5blpYUlRhV2R1WVhSMWNtVnpJR0Z5WXpVMVgyZGxkRk5wWjI1bGNrSjVTVzVrWlhnZ1lYSmpOVFZmYVhOVGFXZHVaWElnWVhKak5UVmZiV0p5VTJsblNXNWpjbVZoYzJVZ1lYSmpOVFZmYldKeVZIaHVTVzVqY21WaGMyVWdZWEpqTlRWZmMyVjBkWEFnWVhKak5UVmZibVYzVkhKaGJuTmhZM1JwYjI1SGNtOTFjQ0JoY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2YmlCaGNtTTFOVjloWkdSVWNtRnVjMkZqZEdsdmJrTnZiblJwYm5WbFpDQmhjbU0xTlY5eVpXMXZkbVZVY21GdWMyRmpkR2x2YmlCaGNtTTFOVjl6WlhSVGFXZHVZWFIxY21WeklHRnlZelUxWDJOc1pXRnlVMmxuYm1GMGRYSmxjd29nSUNBZ1pYSnlDZ3B0WVdsdVgxOWZZV3huYjNSelgxOHVaR1ZtWVhWc2RFTnlaV0YwWlVBeU5Ub0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pZNUNpQWdJQ0F2THlCbGVIQnZjblFnWTJ4aGMzTWdRVkpETlRVZ1pYaDBaVzVrY3lCRGIyNTBjbUZqZENCN0NpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ0lRb2dJQ0FnSmlZS0lDQWdJR0Z6YzJWeWRDQXZMeUJQYmtOdmJYQnNaWFJwYjI0Z2JYVnpkQ0JpWlNCT2IwOXdJQ1ltSUdOaGJpQnZibXg1SUdOaGJHd2dkMmhsYmlCamNtVmhkR2x1WndvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJVa00xTlM1aGNtTTFOVjluWlhSVWFISmxjMmh2YkdSYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0xTlY5blpYUlVhSEpsYzJodmJHUTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE5Ea0tJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbUZ5WXpVMVgzUm9jbVZ6YUc5c1pDNTJZV3gxWlRzS0lDQWdJR2x1ZEdOZk1TQXZMeUF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNOUW9nSUNBZ0x5OGdZWEpqTlRWZmRHaHlaWE5vYjJ4a0lEMGdSMnh2WW1Gc1UzUmhkR1U4ZFdsdWREWTBQaWg3SUdsdWFYUnBZV3hXWVd4MVpUb2dWV2x1ZERZMEtEQXBJSDBwT3dvZ0lDQWdZbmwwWldOZk1pQXZMeUFpWVhKak5UVmZkR2h5WlhOb2IyeGtJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UUTVDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTVoY21NMU5WOTBhSEpsYzJodmJHUXVkbUZzZFdVN0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVFEzTFRFME9Bb2dJQ0FnTHk4Z1FISmxZV1J2Ym14NUNpQWdJQ0F2THlCaGNtTTFOVjluWlhSVWFISmxjMmh2YkdRb0tUb2dkV2x1ZERZMElIc0tJQ0FnSUdsMGIySUtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVZKRE5UVXVZWEpqTlRWZloyVjBRV1J0YVc1YmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0xTlY5blpYUkJaRzFwYmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakUxT0FvZ0lDQWdMeThnY21WMGRYSnVJSFJvYVhNdVlYSmpOVFZmWVdSdGFXNHVkbUZzZFdVN0NpQWdJQ0JwYm5Salh6RWdMeThnTUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPREVLSUNBZ0lDOHZJR0Z5WXpVMVgyRmtiV2x1SUQwZ1IyeHZZbUZzVTNSaGRHVThRV05qYjNWdWRENG9lMzBwT3dvZ0lDQWdZbmwwWldNZ05DQXZMeUFpWVhKak5UVmZZV1J0YVc0aUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TlRnS0lDQWdJQzh2SUhKbGRIVnliaUIwYUdsekxtRnlZelUxWDJGa2JXbHVMblpoYkhWbE93b2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakUxTmkweE5UY0tJQ0FnSUM4dklFQnlaV0ZrYjI1c2VRb2dJQ0FnTHk4Z1lYSmpOVFZmWjJWMFFXUnRhVzRvS1RvZ1FXTmpiM1Z1ZENCN0NpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0ZTUXpVMUxtRnlZelUxWDI1bGVIUlVjbUZ1YzJGamRHbHZia2R5YjNWd1czSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTlRWZmJtVjRkRlJ5WVc1ellXTjBhVzl1UjNKdmRYQTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE5qY0tJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbUZ5WXpVMVgyNXZibU5sTG5aaGJIVmxJQ3NnTVRzS0lDQWdJR2x1ZEdOZk1TQXZMeUF3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzNPQW9nSUNBZ0x5OGdZWEpqTlRWZmJtOXVZMlVnUFNCSGJHOWlZV3hUZEdGMFpUeDFhVzUwTmpRK0tIc2dhVzVwZEdsaGJGWmhiSFZsT2lCVmFXNTBOalFvTUNrZ2ZTazdDaUFnSUNCaWVYUmxZMTh4SUM4dklDSmhjbU0xTlY5dWIyNWpaU0lLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTJOd29nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11WVhKak5UVmZibTl1WTJVdWRtRnNkV1VnS3lBeE93b2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQXJDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveE5qVXRNVFkyQ2lBZ0lDQXZMeUJBY21WaFpHOXViSGtLSUNBZ0lDOHZJR0Z5WXpVMVgyNWxlSFJVY21GdWMyRmpkR2x2YmtkeWIzVndLQ2s2SUhWcGJuUTJOQ0I3Q2lBZ0lDQnBkRzlpQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRlNRelUxTG1GeVl6VTFYMmRsZEZSeVlXNXpZV04wYVc5dVczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTlRWZloyVjBWSEpoYm5OaFkzUnBiMjQ2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hOemN0TVRjNENpQWdJQ0F2THlCQWNtVmhaRzl1YkhrS0lDQWdJQzh2SUdGeVl6VTFYMmRsZEZSeVlXNXpZV04wYVc5dUtIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNklIVnBiblEyTkN3Z2RISmhibk5oWTNScGIyNUpibVJsZURvZ1ZXbHVkRGdzSUhOcFoyNWxja2x1WkdWNE9pQlZhVzUwT0NrNklHSjVkR1Z6SUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdPQW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzUxYVc1ME5qUUtJQ0FnSUdKMGIya0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBPQW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNd29nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblE0Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hOemt0TVRnekNpQWdJQ0F2THlCamIyNXpkQ0IwY21GdWMyRmpkR2x2YmtKdmVEb2dWSEpoYm5OaFkzUnBiMjVIY205MWNDQTlJSHNLSUNBZ0lDOHZJQ0FnYm05dVkyVTZJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQXNDaUFnSUNBdkx5QWdJR2x1WkdWNE9pQjBjbUZ1YzJGamRHbHZia2x1WkdWNExBb2dJQ0FnTHk4Z0lDQnphV2R1WlhKZmFXNWtaWGc2SUhOcFoyNWxja2x1WkdWNENpQWdJQ0F2THlCOU93b2dJQ0FnZFc1amIzWmxjaUF5Q2lBZ0lDQnBkRzlpQ2lBZ0lDQjFibU52ZG1WeUlESUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3hPRFVLSUNBZ0lDOHZJSEpsZEhWeWJpQjBhR2x6TG1GeVl6VTFYM1J5WVc1ellXTjBhVzl1Y3loMGNtRnVjMkZqZEdsdmJrSnZlQ2t1ZG1Gc2RXVTdDaUFnSUNCaWIzaGZaMlYwQ2lBZ0lDQmhjM05sY25RZ0x5OGdRbTk0SUcxMWMzUWdhR0YyWlNCMllXeDFaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UYzNMVEUzT0FvZ0lDQWdMeThnUUhKbFlXUnZibXg1Q2lBZ0lDQXZMeUJoY21NMU5WOW5aWFJVY21GdWMyRmpkR2x2YmloMGNtRnVjMkZqZEdsdmJrZHliM1Z3T2lCMWFXNTBOalFzSUhSeVlXNXpZV04wYVc5dVNXNWtaWGc2SUZWcGJuUTRMQ0J6YVdkdVpYSkpibVJsZURvZ1ZXbHVkRGdwT2lCaWVYUmxjeUI3Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FYUnZZZ29nSUNBZ1pYaDBjbUZqZENBMklESUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh3SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwQlVrTTFOUzVoY21NMU5WOW5aWFJUYVdkdVlYUjFjbVZ6VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpOVFZmWjJWMFUybG5ibUYwZFhKbGN6b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFNU5DMHhPVFVLSUNBZ0lDOHZJRUJ5WldGa2IyNXNlUW9nSUNBZ0x5OGdZWEpqTlRWZloyVjBVMmxuYm1GMGRYSmxjeWgwY21GdWMyRmpkR2x2YmtkeWIzVndPaUIxYVc1ME5qUXNJSE5wWjI1bGNqb2dRV05qYjNWdWRDazZJR0o1ZEdWelBEWTBQbHRkSUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpJZ0x5OGdPQW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzUxYVc1ME5qUUtJQ0FnSUdKMGIya0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6TWdMeThnTXpJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWMzUmhkR2xqWDJGeWNtRjVQR0Z5WXpRdWRXbHVkRGdzSURNeVBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TVRrMkxURTVPUW9nSUNBZ0x5OGdZMjl1YzNRZ2MybG5ibUYwZFhKbFFtOTRPaUJVY21GdWMyRmpkR2x2YmxOcFoyNWhkSFZ5WlhNZ1BTQjdDaUFnSUNBdkx5QWdJRzV2Ym1ObE9pQjBjbUZ1YzJGamRHbHZia2R5YjNWd0xBb2dJQ0FnTHk4Z0lDQmhaR1J5WlhOek9pQnphV2R1WlhJS0lDQWdJQzh2SUgwN0NpQWdJQ0J6ZDJGd0NpQWdJQ0JwZEc5aUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJd01Rb2dJQ0FnTHk4Z2NtVjBkWEp1SUhSb2FYTXVZWEpqTlRWZmMybG5ibUYwZFhKbGN5aHphV2R1WVhSMWNtVkNiM2dwTG5aaGJIVmxPd29nSUNBZ1ltOTRYMmRsZEFvZ0lDQWdZWE56WlhKMElDOHZJRUp2ZUNCdGRYTjBJR2hoZG1VZ2RtRnNkV1VLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRTVOQzB4T1RVS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZaMlYwVTJsbmJtRjBkWEpsY3loMGNtRnVjMkZqZEdsdmJrZHliM1Z3T2lCMWFXNTBOalFzSUhOcFoyNWxjam9nUVdOamIzVnVkQ2s2SUdKNWRHVnpQRFkwUGx0ZElIc0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVZKRE5UVXVZWEpqTlRWZloyVjBVMmxuYm1WeVFubEpibVJsZUZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZelUxWDJkbGRGTnBaMjVsY2tKNVNXNWtaWGc2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lNRGt0TWpFd0NpQWdJQ0F2THlCQWNtVmhaRzl1YkhrS0lDQWdJQzh2SUdGeVl6VTFYMmRsZEZOcFoyNWxja0o1U1c1a1pYZ29hVzVrWlhnNklIVnBiblEyTkNrNklFRmpZMjkxYm5RZ2V3b2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1pQXZMeUE0Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG5WcGJuUTJOQW9nSUNBZ1luUnZhUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UQXdDaUFnSUNBdkx5QnlaWFIxY200Z1IyeHZZbUZzVTNSaGRHVThRV05qYjNWdWRENG9leUJyWlhrNklHOXdMbWwwYjJJb2FXNWtaWGdwSUgwcENpQWdJQ0JwZEc5aUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TVRFS0lDQWdJQzh2SUhKbGRIVnliaUIwYUdsekxtRnlZelUxWDJsdVpHVjRWRzlCWkdSeVpYTnpLR2x1WkdWNEtTNTJZV3gxWlRzS0lDQWdJR2x1ZEdOZk1TQXZMeUF3Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qQTVMVEl4TUFvZ0lDQWdMeThnUUhKbFlXUnZibXg1Q2lBZ0lDQXZMeUJoY21NMU5WOW5aWFJUYVdkdVpYSkNlVWx1WkdWNEtHbHVaR1Y0T2lCMWFXNTBOalFwT2lCQlkyTnZkVzUwSUhzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzZRVkpETlRVdVlYSmpOVFZmYVhOVGFXZHVaWEpiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTTFOVjlwYzFOcFoyNWxjam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXhPUzB5TWpBS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZhWE5UYVdkdVpYSW9ZV1JrY21WemN6b2dRV05qYjNWdWRDazZJR0p2YjJ4bFlXNGdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNeUF2THlBek1nb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNXpkR0YwYVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0N3Z016SStDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU1qRUtJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbUZ5WXpVMVgyRmtaSEpsYzNORGIzVnVkQ2hoWkdSeVpYTnpLUzUyWVd4MVpTQWhQVDBnTURzS0lDQWdJR2x1ZEdOZk1TQXZMeUF3Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJRWRzYjJKaGJGTjBZWFJsSUdWNGFYTjBjd29nSUNBZ2FXNTBZMTh4SUM4dklEQUtJQ0FnSUNFOUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TVRrdE1qSXdDaUFnSUNBdkx5QkFjbVZoWkc5dWJIa0tJQ0FnSUM4dklHRnlZelUxWDJselUybG5ibVZ5S0dGa1pISmxjM002SUVGalkyOTFiblFwT2lCaWIyOXNaV0Z1SUhzS0lDQWdJSEIxYzJoaWVYUmxjeUF3ZURBd0NpQWdJQ0JwYm5Salh6RWdMeThnTUFvZ0lDQWdkVzVqYjNabGNpQXlDaUFnSUNCelpYUmlhWFFLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvNlFWSkROVFV1WVhKak5UVmZiV0p5VTJsblNXNWpjbVZoYzJWYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0xTlY5dFluSlRhV2RKYm1OeVpXRnpaVG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSXlPUzB5TXpBS0lDQWdJQzh2SUVCeVpXRmtiMjVzZVFvZ0lDQWdMeThnWVhKak5UVmZiV0p5VTJsblNXNWpjbVZoYzJVb2MybG5ibUYwZFhKbGMxTnBlbVU2SUhWcGJuUTJOQ2s2SUhWcGJuUTJOQ0I3Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklEZ0tJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVkV2x1ZERZMENpQWdJQ0JpZEc5cENpQWdJQ0JqWVd4c2MzVmlJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJVa00xTlM1aGNtTTFOVjl0WW5KVGFXZEpibU55WldGelpRb2dJQ0FnYVhSdllnb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCVWtNMU5TNWhjbU0xTlY5dFluSlVlRzVKYm1OeVpXRnpaVnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6VTFYMjFpY2xSNGJrbHVZM0psWVhObE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWpVekxUSTFOQW9nSUNBZ0x5OGdRSEpsWVdSdmJteDVDaUFnSUNBdkx5QmhjbU0xTlY5dFluSlVlRzVKYm1OeVpXRnpaU2gwY21GdWMyRmpkR2x2YmxOcGVtVTZJSFZwYm5RMk5DazZJSFZwYm5RMk5DQjdDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURnS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkRFkwQ2lBZ0lDQmlkRzlwQ2lBZ0lDQmpZV3hzYzNWaUlITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwQlVrTTFOUzVoY21NMU5WOXRZbkpVZUc1SmJtTnlaV0Z6WlFvZ0lDQWdhWFJ2WWdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJVa00xTlM1aGNtTTFOVjl6WlhSMWNGdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUZ5WXpVMVgzTmxkSFZ3T2dvZ0lDQWdhVzUwWTE4eElDOHZJREFLSUNBZ0lHUjFjRzRnTWdvZ0lDQWdjSFZ6YUdKNWRHVnpJQ0lpQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3lPRGt0TWpreUNpQWdJQ0F2THlCaGNtTTFOVjl6WlhSMWNDZ0tJQ0FnSUM4dklDQWdkR2h5WlhOb2IyeGtPaUJWYVc1ME9Dd0tJQ0FnSUM4dklDQWdZV1JrY21WemMyVnpPaUJCWTJOdmRXNTBXMTBLSUNBZ0lDOHZJQ2s2SUhadmFXUWdld29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblE0Q2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF5Q2lBZ0lDQmtkWEFLSUNBZ0lHTnZkbVZ5SURJS0lDQWdJR1IxY0FvZ0lDQWdhVzUwWTE4eElDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdNd29nSUNBZ2FXNTBZMTh6SUM4dklETXlDaUFnSUNBcUNpQWdJQ0J3ZFhOb2FXNTBJRElnTHk4Z01nb2dJQ0FnS3dvZ0lDQWdjM2RoY0FvZ0lDQWdiR1Z1Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG1SNWJtRnRhV05mWVhKeVlYazhZV05qYjNWdWRENEtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJNU13b2dJQ0FnTHk4Z1lYTnpaWEowS0NGMGFHbHpMbUZ5WXpVMVgyNXZibU5sTG5aaGJIVmxLVHNLSUNBZ0lHbHVkR05mTVNBdkx5QXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvM09Bb2dJQ0FnTHk4Z1lYSmpOVFZmYm05dVkyVWdQU0JIYkc5aVlXeFRkR0YwWlR4MWFXNTBOalErS0hzZ2FXNXBkR2xoYkZaaGJIVmxPaUJWYVc1ME5qUW9NQ2tnZlNrN0NpQWdJQ0JpZVhSbFkxOHhJQzh2SUNKaGNtTTFOVjl1YjI1alpTSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJNU13b2dJQ0FnTHk4Z1lYTnpaWEowS0NGMGFHbHpMbUZ5WXpVMVgyNXZibU5sTG5aaGJIVmxLVHNLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2dSMnh2WW1Gc1UzUmhkR1VnWlhocGMzUnpDaUFnSUNBaENpQWdJQ0JoYzNObGNuUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFeU5Bb2dJQ0FnTHk4Z1lYTnpaWEowS0ZSNGJpNXpaVzVrWlhJZ1BUMDlJRWRzYjJKaGJDNWpjbVZoZEc5eVFXUmtjbVZ6Y3lrN0NpQWdJQ0IwZUc0Z1UyVnVaR1Z5Q2lBZ0lDQm5iRzlpWVd3Z1EzSmxZWFJ2Y2tGa1pISmxjM01LSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSTVOZ29nSUNBZ0x5OGdZWE56WlhKMEtIUm9jbVZ6YUc5c1pDNWhjMVZwYm5RMk5DZ3BJRDRnTUNrN0NpQWdJQ0JpZEc5cENpQWdJQ0JrZFhBS0lDQWdJR0Z6YzJWeWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TnpVS0lDQWdJQzh2SUdGeVl6VTFYM1JvY21WemFHOXNaQ0E5SUVkc2IySmhiRk4wWVhSbFBIVnBiblEyTkQ0b2V5QnBibWwwYVdGc1ZtRnNkV1U2SUZWcGJuUTJOQ2d3S1NCOUtUc0tJQ0FnSUdKNWRHVmpYeklnTHk4Z0ltRnlZelUxWDNSb2NtVnphRzlzWkNJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakk1T0FvZ0lDQWdMeThnZEdocGN5NWhjbU0xTlY5MGFISmxjMmh2YkdRdWRtRnNkV1VnUFNCVmFXNTBOalFvZEdoeVpYTm9iMnhrTG1GelZXbHVkRFkwS0NrcE93b2dJQ0FnYzNkaGNBb2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pjNENpQWdJQ0F2THlCaGNtTTFOVjl1YjI1alpTQTlJRWRzYjJKaGJGTjBZWFJsUEhWcGJuUTJORDRvZXlCcGJtbDBhV0ZzVm1Gc2RXVTZJRlZwYm5RMk5DZ3dLU0I5S1RzS0lDQWdJR0o1ZEdWalh6RWdMeThnSW1GeVl6VTFYMjV2Ym1ObElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TWprNUNpQWdJQ0F2THlCMGFHbHpMbUZ5WXpVMVgyNXZibU5sTG5aaGJIVmxJRDBnTURzS0lDQWdJR2x1ZEdOZk1TQXZMeUF3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPREVLSUNBZ0lDOHZJR0Z5WXpVMVgyRmtiV2x1SUQwZ1IyeHZZbUZzVTNSaGRHVThRV05qYjNWdWRENG9lMzBwT3dvZ0lDQWdZbmwwWldNZ05DQXZMeUFpWVhKak5UVmZZV1J0YVc0aUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TURBS0lDQWdJQzh2SUhSb2FYTXVZWEpqTlRWZllXUnRhVzR1ZG1Gc2RXVWdQU0JVZUc0dWMyVnVaR1Z5T3dvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTXdOQW9nSUNBZ0x5OGdiR1YwSUhCSmJtUmxlRG9nZFdsdWREWTBJRDBnTURzS0lDQWdJR2x1ZEdOZk1TQXZMeUF3Q2dwaGNtTTFOVjl6WlhSMWNGOTNhR2xzWlY5MGIzQkFNam9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qRXdNQW9nSUNBZ0x5OGdjbVYwZFhKdUlFZHNiMkpoYkZOMFlYUmxQRUZqWTI5MWJuUStLSHNnYTJWNU9pQnZjQzVwZEc5aUtHbHVaR1Y0S1NCOUtRb2dJQ0FnWkhWd0NpQWdJQ0JwZEc5aUNpQWdJQ0JrZFhBS0lDQWdJR0oxY25rZ05nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpBMUNpQWdJQ0F2THlCM2FHbHNaU0FvZEdocGN5NWhjbU0xTlY5cGJtUmxlRlJ2UVdSa2NtVnpjeWh3U1c1a1pYZ3BMbWhoYzFaaGJIVmxLU0I3Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNQW9nSUNBZ2MzZGhjQW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0oxY25rZ01Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpBMUxUTXhOUW9nSUNBZ0x5OGdkMmhwYkdVZ0tIUm9hWE11WVhKak5UVmZhVzVrWlhoVWIwRmtaSEpsYzNNb2NFbHVaR1Y0S1M1b1lYTldZV3gxWlNrZ2V3b2dJQ0FnTHk4Z0lDQmpiMjV6ZENCaFpHUnlaWE56SUQwZ2RHaHBjeTVoY21NMU5WOXBibVJsZUZSdlFXUmtjbVZ6Y3lod1NXNWtaWGdwTG5aaGJIVmxPd29nSUNBZ0x5OGdJQ0F2THlCSmJpQndkWGxoTFhSeklIZGxJRzVsWldRZ2RHOGdZWE56WlhKMElIUm9aU0JyWlhrZ1pYaHBjM1J6SUdKbFptOXlaU0JrWld4bGRHbHVad29nSUNBZ0x5OGdJQ0JwWmlBb2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvWVdSa2NtVnpjeWt1YUdGelZtRnNkV1VwSUhzS0lDQWdJQzh2SUNBZ0lDQjBhR2x6TG1GeVl6VTFYMkZrWkhKbGMzTkRiM1Z1ZENoaFpHUnlaWE56S1M1a1pXeGxkR1VvS1RzS0lDQWdJQzh2SUNBZ0lDQjBhR2x6TG1GeVl6VTFYMmx1WkdWNFZHOUJaR1J5WlhOektIQkpibVJsZUNrdVpHVnNaWFJsS0NrN0NpQWdJQ0F2THlBZ0lIMEtJQ0FnSUM4dklBb2dJQ0FnTHk4Z0lDQWdJSEJKYm1SbGVDQXJQU0F4T3dvZ0lDQWdMeThnSUNBdkx5QjBhR2x6TG1GeVl6VTFYMkZrWkhKbGMzTkRiM1Z1ZENoaFpHUnlaWE56S1M1a1pXeGxkR1VvS1RzS0lDQWdJQzh2SUgwS0lDQWdJR0o2SUdGeVl6VTFYM05sZEhWd1gyRm1kR1Z5WDNkb2FXeGxRRFlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTXdOZ29nSUNBZ0x5OGdZMjl1YzNRZ1lXUmtjbVZ6Y3lBOUlIUm9hWE11WVhKak5UVmZhVzVrWlhoVWIwRmtaSEpsYzNNb2NFbHVaR1Y0S1M1MllXeDFaVHNLSUNBZ0lHbHVkR05mTVNBdkx5QXdDaUFnSUNCa2FXY2dOUW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJSE4zWVhBS0lDQWdJR1IxY0FvZ0lDQWdZMjkyWlhJZ01nb2dJQ0FnWW5WeWVTQTVDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnUjJ4dlltRnNVM1JoZEdVZ1pYaHBjM1J6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3pNRGdLSUNBZ0lDOHZJR2xtSUNoMGFHbHpMbUZ5WXpVMVgyRmtaSEpsYzNORGIzVnVkQ2hoWkdSeVpYTnpLUzVvWVhOV1lXeDFaU2tnZXdvZ0lDQWdhVzUwWTE4eElDOHZJREFLSUNBZ0lITjNZWEFLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JpZFhKNUlERUtJQ0FnSUdKNklHRnlZelUxWDNObGRIVndYMkZtZEdWeVgybG1YMlZzYzJWQU5Rb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpBNUNpQWdJQ0F2THlCMGFHbHpMbUZ5WXpVMVgyRmtaSEpsYzNORGIzVnVkQ2hoWkdSeVpYTnpLUzVrWld4bGRHVW9LVHNLSUNBZ0lHUnBaeUEyQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMlJsYkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNekV3Q2lBZ0lDQXZMeUIwYUdsekxtRnlZelUxWDJsdVpHVjRWRzlCWkdSeVpYTnpLSEJKYm1SbGVDa3VaR1ZzWlhSbEtDazdDaUFnSUNCa2FXY2dOQW9nSUNBZ1lYQndYMmRzYjJKaGJGOWtaV3dLQ21GeVl6VTFYM05sZEhWd1gyRm1kR1Z5WDJsbVgyVnNjMlZBTlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak14TXdvZ0lDQWdMeThnY0VsdVpHVjRJQ3M5SURFN0NpQWdJQ0JrZFhBS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQXJDaUFnSUNCaWRYSjVJREVLSUNBZ0lHSWdZWEpqTlRWZmMyVjBkWEJmZDJocGJHVmZkRzl3UURJS0NtRnlZelUxWDNObGRIVndYMkZtZEdWeVgzZG9hV3hsUURZNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TVRnS0lDQWdJQzh2SUd4bGRDQnVTVzVrWlhnNklIVnBiblEyTkNBOUlEQTdDaUFnSUNCcGJuUmpYekVnTHk4Z01Bb2dJQ0FnWW5WeWVTQTBDZ3BoY21NMU5WOXpaWFIxY0Y5M2FHbHNaVjkwYjNCQU56b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pNeU1Bb2dJQ0FnTHk4Z2QyaHBiR1VnS0c1SmJtUmxlQ0E4SUdGa1pISmxjM05sY3k1c1pXNW5kR2dwSUhzS0lDQWdJR1JwWnlBekNpQWdJQ0JrYVdjZ01nb2dJQ0FnUEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNekl3TFRNek5nb2dJQ0FnTHk4Z2QyaHBiR1VnS0c1SmJtUmxlQ0E4SUdGa1pISmxjM05sY3k1c1pXNW5kR2dwSUhzS0lDQWdJQzh2SUNBZ1lXUmtjbVZ6Y3lBOUlHRmtaSEpsYzNObGMxdHVTVzVrWlhoZE93b2dJQ0FnTHk4Z0NpQWdJQ0F2THlBZ0lDOHZJRk4wYjNKbElHMTFiSFJwYzJsbklHbHVaR1Y0SUdGeklHdGxlU0IzYVhSb0lHRmtaSEpsYzNNZ1lYTWdkbUZzZFdVS0lDQWdJQzh2SUNBZ2RHaHBjeTVoY21NMU5WOXBibVJsZUZSdlFXUmtjbVZ6Y3lodVNXNWtaWGdwTG5aaGJIVmxJRDBnWVdSa2NtVnpjenNLSUNBZ0lDOHZJQW9nSUNBZ0x5OGdJQ0F2THlCVGRHOXlaU0JoWkdSeVpYTnpJR0Z6SUd0bGVTQmhibVFnWTI5MWJuUmxjaUJoY3lCMllXeDFaU3dLSUNBZ0lDOHZJQ0FnTHk4Z2RHaHBjeUJwY3lCbWIzSWdaV0Z6WlNCdlppQmhkWFJvWlc1MGFXTmhkR2x2YmdvZ0lDQWdMeThnSUNBdkx5QkpSaUJtYVhKemRDQjBhVzFsTENCelpYUWdkRzhnTVN3Z1pXeHpaU0JwYm1OeVpXMWxiblFnWW5rZ01Rb2dJQ0FnTHk4Z0lDQnBaaUFvSVhSb2FYTXVZWEpqTlRWZllXUmtjbVZ6YzBOdmRXNTBLR0ZrWkhKbGMzTXBMbWhoYzFaaGJIVmxLU0I3Q2lBZ0lDQXZMeUFnSUNBZ2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvWVdSa2NtVnpjeWt1ZG1Gc2RXVWdQU0F3T3dvZ0lDQWdMeThnSUNCOUNpQWdJQ0F2THlBS0lDQWdJQzh2SUNBZ2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvWVdSa2NtVnpjeWt1ZG1Gc2RXVWdLejBnTVRzS0lDQWdJQzh2SUFvZ0lDQWdMeThnSUNCdVNXNWtaWGdnS3owZ01Uc0tJQ0FnSUM4dklIMEtJQ0FnSUdKNklHRnlZelUxWDNObGRIVndYMkZtZEdWeVgzZG9hV3hsUURFeENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TWpFS0lDQWdJQzh2SUdGa1pISmxjM01nUFNCaFpHUnlaWE56WlhOYmJrbHVaR1Y0WFRzS0lDQWdJR1JwWnlBeUNpQWdJQ0JsZUhSeVlXTjBJRElnTUFvZ0lDQWdaR2xuSURRS0lDQWdJR1IxY0FvZ0lDQWdZMjkyWlhJZ01nb2dJQ0FnYVc1MFkxOHpJQzh2SURNeUNpQWdJQ0FxQ2lBZ0lDQnBiblJqWHpNZ0x5OGdNeklLSUNBZ0lHVjRkSEpoWTNReklDOHZJRzl1SUdWeWNtOXlPaUJwYm1SbGVDQmhZMk5sYzNNZ2FYTWdiM1YwSUc5bUlHSnZkVzVrY3dvZ0lDQWdaSFZ3Q2lBZ0lDQmlkWEo1SURnS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakV3TUFvZ0lDQWdMeThnY21WMGRYSnVJRWRzYjJKaGJGTjBZWFJsUEVGalkyOTFiblErS0hzZ2EyVjVPaUJ2Y0M1cGRHOWlLR2x1WkdWNEtTQjlLUW9nSUNBZ2MzZGhjQW9nSUNBZ2FYUnZZZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16STBDaUFnSUNBdkx5QjBhR2x6TG1GeVl6VTFYMmx1WkdWNFZHOUJaR1J5WlhOektHNUpibVJsZUNrdWRtRnNkV1VnUFNCaFpHUnlaWE56T3dvZ0lDQWdaR2xuSURFS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmY0hWMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TWprS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1aGNtTTFOVjloWkdSeVpYTnpRMjkxYm5Rb1lXUmtjbVZ6Y3lrdWFHRnpWbUZzZFdVcElIc0tJQ0FnSUdsdWRHTmZNU0F2THlBd0NpQWdJQ0J6ZDJGd0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1luVnllU0F4Q2lBZ0lDQmlibm9nWVhKak5UVmZjMlYwZFhCZllXWjBaWEpmYVdaZlpXeHpaVUF4TUFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNek13Q2lBZ0lDQXZMeUIwYUdsekxtRnlZelUxWDJGa1pISmxjM05EYjNWdWRDaGhaR1J5WlhOektTNTJZV3gxWlNBOUlEQTdDaUFnSUNCa2FXY2dOUW9nSUNBZ2FXNTBZMTh4SUM4dklEQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDZ3BoY21NMU5WOXpaWFIxY0Y5aFpuUmxjbDlwWmw5bGJITmxRREV3T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNek16Q2lBZ0lDQXZMeUIwYUdsekxtRnlZelUxWDJGa1pISmxjM05EYjNWdWRDaGhaR1J5WlhOektTNTJZV3gxWlNBclBTQXhPd29nSUNBZ2FXNTBZMTh4SUM4dklEQUtJQ0FnSUdScFp5QTJDaUFnSUNCa2RYQUtJQ0FnSUdOdmRtVnlJRElLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2dSMnh2WW1Gc1UzUmhkR1VnWlhocGMzUnpDaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnS3dvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak16TlFvZ0lDQWdMeThnYmtsdVpHVjRJQ3M5SURFN0NpQWdJQ0JrYVdjZ013b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJQ3NLSUNBZ0lHSjFjbmtnTkFvZ0lDQWdZaUJoY21NMU5WOXpaWFIxY0Y5M2FHbHNaVjkwYjNCQU53b0tZWEpqTlRWZmMyVjBkWEJmWVdaMFpYSmZkMmhwYkdWQU1URTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pveU9Ea3RNamt5Q2lBZ0lDQXZMeUJoY21NMU5WOXpaWFIxY0NnS0lDQWdJQzh2SUNBZ2RHaHlaWE5vYjJ4a09pQlZhVzUwT0N3S0lDQWdJQzh2SUNBZ1lXUmtjbVZ6YzJWek9pQkJZMk52ZFc1MFcxMEtJQ0FnSUM4dklDazZJSFp2YVdRZ2V3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCVWtNMU5TNWhjbU0xTlY5dVpYZFVjbUZ1YzJGamRHbHZia2R5YjNWd1czSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTlRWZmJtVjNWSEpoYm5OaFkzUnBiMjVIY205MWNEb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFek9Bb2dJQ0FnTHk4Z2NtVjBkWEp1SUZSNGJpNXpaVzVrWlhJZ1BUMDlJRWRzYjJKaGJDNWpjbVZoZEc5eVFXUmtjbVZ6Y3pzS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHZHNiMkpoYkNCRGNtVmhkRzl5UVdSa2NtVnpjd29nSUNBZ1BUMEtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pNME5Bb2dJQ0FnTHk4Z2FXWWdLQ0YwYUdsekxtbHpRV1J0YVc0b0tTa2dld29nSUNBZ1ltNTZJR0Z5WXpVMVgyNWxkMVJ5WVc1ellXTjBhVzl1UjNKdmRYQmZZV1owWlhKZmFXWmZaV3h6WlVBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TkRVS0lDQWdJQzh2SUhSb2FYTXViMjVzZVZOcFoyNWxjaWdwT3dvZ0lDQWdZMkZzYkhOMVlpQnZibXg1VTJsbmJtVnlDZ3BoY21NMU5WOXVaWGRVY21GdWMyRmpkR2x2YmtkeWIzVndYMkZtZEdWeVgybG1YMlZzYzJWQU16b0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pFMk53b2dJQ0FnTHk4Z2NtVjBkWEp1SUhSb2FYTXVZWEpqTlRWZmJtOXVZMlV1ZG1Gc2RXVWdLeUF4T3dvZ0lDQWdhVzUwWTE4eElDOHZJREFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qYzRDaUFnSUNBdkx5QmhjbU0xTlY5dWIyNWpaU0E5SUVkc2IySmhiRk4wWVhSbFBIVnBiblEyTkQ0b2V5QnBibWwwYVdGc1ZtRnNkV1U2SUZWcGJuUTJOQ2d3S1NCOUtUc0tJQ0FnSUdKNWRHVmpYekVnTHk4Z0ltRnlZelUxWDI1dmJtTmxJZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UWTNDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTVoY21NMU5WOXViMjVqWlM1MllXeDFaU0FySURFN0NpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUVkc2IySmhiRk4wWVhSbElHVjRhWE4wY3dvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lDc0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pjNENpQWdJQ0F2THlCaGNtTTFOVjl1YjI1alpTQTlJRWRzYjJKaGJGTjBZWFJsUEhWcGJuUTJORDRvZXlCcGJtbDBhV0ZzVm1Gc2RXVTZJRlZwYm5RMk5DZ3dLU0I5S1RzS0lDQWdJR0o1ZEdWalh6RWdMeThnSW1GeVl6VTFYMjV2Ym1ObElnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpRNUNpQWdJQ0F2THlCMGFHbHpMbUZ5WXpVMVgyNXZibU5sTG5aaGJIVmxJRDBnYmpzS0lDQWdJR1JwWnlBeENpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TXpRekNpQWdJQ0F2THlCaGNtTTFOVjl1WlhkVWNtRnVjMkZqZEdsdmJrZHliM1Z3S0NrNklIVnBiblEyTkNCN0NpQWdJQ0JwZEc5aUNpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0ZTUXpVMUxtRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI0NkNpQWdJQ0JwYm5Salh6RWdMeThnTUFvZ0lDQWdaSFZ3YmlBeUNpQWdJQ0J3ZFhOb1lubDBaWE1nSWlJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak0yTkMwek56QUtJQ0FnSUM4dklHRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVLQW9nSUNBZ0x5OGdJQ0JqYjNOMGN6b2daM1I0Ymk1UVlYbHRaVzUwVkhodUxBb2dJQ0FnTHk4Z0lDQjBjbUZ1YzJGamRHbHZia2R5YjNWd09pQjFhVzUwTmpRc0NpQWdJQ0F2THlBZ0lHbHVaR1Y0T2lCVmFXNTBPQ3dLSUNBZ0lDOHZJQ0FnYzJsbmJtVnlTVzVrWlhnNklGVnBiblE0TEFvZ0lDQWdMeThnSUNCMGNtRnVjMkZqZEdsdmJqb2dZbmwwWlhNS0lDQWdJQzh2SUNrNklIWnZhV1FnZXdvZ0lDQWdkSGh1SUVkeWIzVndTVzVrWlhnS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQXRDaUFnSUNCbmRIaHVjeUJVZVhCbFJXNTFiUW9nSUNBZ2FXNTBZMTh3SUM4dklIQmhlUW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCMGNtRnVjMkZqZEdsdmJpQjBlWEJsSUdseklIQmhlUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNaUF2THlBNENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblEyTkFvZ0lDQWdZblJ2YVFvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTWdvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RNENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1ZFdsdWREZ0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklEUUtJQ0FnSUdSMWNBb2dJQ0FnYVc1MFkxOHhJQzh2SURBS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJR0Z5Y21GNUlHeGxibWQwYUNCb1pXRmtaWElLSUNBZ0lIQjFjMmhwYm5RZ01pQXZMeUF5Q2lBZ0lDQXJDaUFnSUNCa2FXY2dNUW9nSUNBZ2JHVnVDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbVI1Ym1GdGFXTmZZWEp5WVhrOFlYSmpOQzUxYVc1ME9ENEtJQ0FnSUdWNGRISmhZM1FnTWlBd0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TXpnS0lDQWdJQzh2SUhKbGRIVnliaUJVZUc0dWMyVnVaR1Z5SUQwOVBTQkhiRzlpWVd3dVkzSmxZWFJ2Y2tGa1pISmxjM003Q2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNCbmJHOWlZV3dnUTNKbFlYUnZja0ZrWkhKbGMzTUtJQ0FnSUQwOUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem96TnpFS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1cGMwRmtiV2x1S0NrcElIc0tJQ0FnSUdKdWVpQmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZibDloWm5SbGNsOXBabDlsYkhObFFETUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pNM01nb2dJQ0FnTHk4Z2RHaHBjeTV2Ym14NVUybG5ibVZ5S0NrN0NpQWdJQ0JqWVd4c2MzVmlJRzl1YkhsVGFXZHVaWElLQ21GeVl6VTFYMkZrWkZSeVlXNXpZV04wYVc5dVgyRm1kR1Z5WDJsbVgyVnNjMlZBTXpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak0zTlFvZ0lDQWdMeThnWVhOelpYSjBLSFJ5WVc1ellXTjBhVzl1UjNKdmRYQXBPd29nSUNBZ1pHbG5JRE1LSUNBZ0lHUjFjQW9nSUNBZ1lYTnpaWEowQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6b3pOemd0TXpneUNpQWdJQ0F2THlCamIyNXpkQ0IwY21GdWMyRmpkR2x2YmtKdmVEb2dWSEpoYm5OaFkzUnBiMjVIY205MWNDQTlJSHNLSUNBZ0lDOHZJQ0FnYm05dVkyVTZJSFJ5WVc1ellXTjBhVzl1UjNKdmRYQXNDaUFnSUNBdkx5QWdJR2x1WkdWNE9pQnBibVJsZUN3S0lDQWdJQzh2SUNBZ2MybG5ibVZ5WDJsdVpHVjRPaUJ6YVdkdVpYSkpibVJsZUFvZ0lDQWdMeThnZlRzS0lDQWdJR2wwYjJJS0lDQWdJR1JwWnlBekNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnWW5WeWVTQTVDaUFnSUNCa2FXY2dNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmlkWEo1SURjS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak00TndvZ0lDQWdMeThnYkdWMElHZHliM1Z3VUc5emFYUnBiMjQ2SUhWcGJuUTJOQ0E5SUZSNGJpNW5jbTkxY0VsdVpHVjRJQ3NnTVRzS0lDQWdJSFI0YmlCSGNtOTFjRWx1WkdWNENpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdLd29nSUNBZ1pIVndDaUFnSUNCaWRYSjVJRFlLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTTRPQW9nSUNBZ0x5OGdhV1lnS0dkeWIzVndVRzl6YVhScGIyNGdQQ0JIYkc5aVlXd3VaM0p2ZFhCVGFYcGxLU0I3Q2lBZ0lDQm5iRzlpWVd3Z1IzSnZkWEJUYVhwbENpQWdJQ0E4Q2lBZ0lDQmlibm9nWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI1ZmFXWmZZbTlrZVVBMENpQWdJQ0JrZFhBS0lDQWdJR0oxY25rZ05nb0tZWEpqTlRWZllXUmtWSEpoYm5OaFkzUnBiMjVmWVdaMFpYSmZhV1pmWld4elpVQXhORG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUXdNZ29nSUNBZ0x5OGdZMjl1YzNRZ2JXSnlWSGh1U1c1amNtVmhjMlVnUFNCMGFHbHpMbUZ5WXpVMVgyMWljbFI0YmtsdVkzSmxZWE5sS0hSeVlXNXpZV04wYVc5dVJHRjBZUzVzWlc1bmRHZ3BPd29nSUNBZ1pHbG5JRFVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCallXeHNjM1ZpSUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCVWtNMU5TNWhjbU0xTlY5dFluSlVlRzVKYm1OeVpXRnpaUW9nSUNBZ2NHOXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvME1UUUtJQ0FnSUM4dklIUm9hWE11WVhKak5UVmZkSEpoYm5OaFkzUnBiMjV6S0hSeVlXNXpZV04wYVc5dVFtOTRLUzUyWVd4MVpTQTlJSFJ5WVc1ellXTjBhVzl1UkdGMFlUc0tJQ0FnSUdScFp5QTNDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOWtaV3dLSUNBZ0lIQnZjQW9nSUNBZ2MzZGhjQW9nSUNBZ1ltOTRYM0IxZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOREUzTFRReU1Bb2dJQ0FnTHk4Z1pXMXBkRHhVY21GdWMyRmpkR2x2YmtGa1pHVmtQaWg3Q2lBZ0lDQXZMeUFnSUhSeVlXNXpZV04wYVc5dVIzSnZkWEE2SUhSeVlXNXpZV04wYVc5dVIzSnZkWEFzQ2lBZ0lDQXZMeUFnSUhSeVlXNXpZV04wYVc5dVNXNWtaWGc2SUdsdVpHVjRDaUFnSUNBdkx5QjlLUW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRNVGcwT1dFMU9UUWdMeThnYldWMGFHOWtJQ0pVY21GdWMyRmpkR2x2YmtGa1pHVmtLSFZwYm5RMk5DeDFhVzUwT0NraUNpQWdJQ0JrYVdjZ09Bb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pNMk5DMHpOekFLSUNBZ0lDOHZJR0Z5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1S0FvZ0lDQWdMeThnSUNCamIzTjBjem9nWjNSNGJpNVFZWGx0Wlc1MFZIaHVMQW9nSUNBZ0x5OGdJQ0IwY21GdWMyRmpkR2x2YmtkeWIzVndPaUIxYVc1ME5qUXNDaUFnSUNBdkx5QWdJR2x1WkdWNE9pQlZhVzUwT0N3S0lDQWdJQzh2SUNBZ2MybG5ibVZ5U1c1a1pYZzZJRlZwYm5RNExBb2dJQ0FnTHk4Z0lDQjBjbUZ1YzJGamRHbHZiam9nWW5sMFpYTUtJQ0FnSUM4dklDazZJSFp2YVdRZ2V3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tZWEpqTlRWZllXUmtWSEpoYm5OaFkzUnBiMjVmYVdaZlltOWtlVUEwT2dvZ0lDQWdaSFZ3Q2lBZ0lDQmlkWEo1SURZS0NtRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVYM2RvYVd4bFgzUnZjRUExT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNemt4Q2lBZ0lDQXZMeUJuZEhodUxrRndjR3hwWTJGMGFXOXVRMkZzYkZSNGJpaG5jbTkxY0ZCdmMybDBhVzl1S1M1aGNIQkpaQ0E5UFQwZ1ZIaHVMbUZ3Y0d4cFkyRjBhVzl1U1dRS0lDQWdJR1JwWnlBMENpQWdJQ0JrZFhBS0lDQWdJR2QwZUc1eklGUjVjR1ZGYm5WdENpQWdJQ0J3ZFhOb2FXNTBJRFlnTHk4Z1lYQndiQW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCMGNtRnVjMkZqZEdsdmJpQjBlWEJsSUdseklHRndjR3dLSUNBZ0lHZDBlRzV6SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQTlQUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk16a3hMVE01TXdvZ0lDQWdMeThnSUNCbmRIaHVMa0Z3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaWhuY205MWNGQnZjMmwwYVc5dUtTNWhjSEJKWkNBOVBUMGdWSGh1TG1Gd2NHeHBZMkYwYVc5dVNXUUtJQ0FnSUM4dklDOHZKaVlnZEdocGN5NTBlRzVIY205MWNGdG5jbTkxY0ZCdmMybDBhVzl1WFM1aGNIQnNhV05oZEdsdmJrRnlaM05iTUYwZ1BUMDlJRzFsZEdodlpDZ2lZWEpqTlRWZllXUmtWSEpoYm5OaFkzUnBiMjVEYjI1MGFXNTFaV1FvWW5sMFpWdGRLWFp2YVdRaUtRb2dJQ0FnTHk4Z0ppWWdaM1I0Ymk1QmNIQnNhV05oZEdsdmJrTmhiR3hVZUc0b1ozSnZkWEJRYjNOcGRHbHZiaWt1WVhCd1FYSm5jeWd3S1NBOVBUMGdiV1YwYUc5a1UyVnNaV04wYjNJb0ltRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVRMjl1ZEdsdWRXVmtLR0o1ZEdWYlhTbDJiMmxrSWlrS0lDQWdJR0o2SUdGeVl6VTFYMkZrWkZSeVlXNXpZV04wYVc5dVgyRm1kR1Z5WDJsbVgyVnNjMlZBT1FvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNemt6Q2lBZ0lDQXZMeUFtSmlCbmRIaHVMa0Z3Y0d4cFkyRjBhVzl1UTJGc2JGUjRiaWhuY205MWNGQnZjMmwwYVc5dUtTNWhjSEJCY21kektEQXBJRDA5UFNCdFpYUm9iMlJUWld4bFkzUnZjaWdpWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI1RGIyNTBhVzUxWldRb1lubDBaVnRkS1hadmFXUWlLUW9nSUNBZ1pHbG5JRFFLSUNBZ0lHbHVkR05mTVNBdkx5QXdDaUFnSUNCbmRIaHVjMkZ6SUVGd2NHeHBZMkYwYVc5dVFYSm5jd29nSUNBZ1lubDBaV05mTXlBdkx5QnRaWFJvYjJRZ0ltRnlZelUxWDJGa1pGUnlZVzV6WVdOMGFXOXVRMjl1ZEdsdWRXVmtLR0o1ZEdWYlhTbDJiMmxrSWdvZ0lDQWdQVDBLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qTTVNUzB6T1RNS0lDQWdJQzh2SUNBZ1ozUjRiaTVCY0hCc2FXTmhkR2x2YmtOaGJHeFVlRzRvWjNKdmRYQlFiM05wZEdsdmJpa3VZWEJ3U1dRZ1BUMDlJRlI0Ymk1aGNIQnNhV05oZEdsdmJrbGtDaUFnSUNBdkx5QXZMeVltSUhSb2FYTXVkSGh1UjNKdmRYQmJaM0p2ZFhCUWIzTnBkR2x2YmwwdVlYQndiR2xqWVhScGIyNUJjbWR6V3pCZElEMDlQU0J0WlhSb2IyUW9JbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1UTI5dWRHbHVkV1ZrS0dKNWRHVmJYU2wyYjJsa0lpa0tJQ0FnSUM4dklDWW1JR2QwZUc0dVFYQndiR2xqWVhScGIyNURZV3hzVkhodUtHZHliM1Z3VUc5emFYUnBiMjRwTG1Gd2NFRnlaM01vTUNrZ1BUMDlJRzFsZEdodlpGTmxiR1ZqZEc5eUtDSmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZia052Ym5ScGJuVmxaQ2hpZVhSbFcxMHBkbTlwWkNJcENpQWdJQ0JpZWlCaGNtTTFOVjloWkdSVWNtRnVjMkZqZEdsdmJsOWhablJsY2w5cFpsOWxiSE5sUURrS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPak01TmdvZ0lDQWdMeThnZEhKaGJuTmhZM1JwYjI1RVlYUmhJRDBnZEhKaGJuTmhZM1JwYjI1RVlYUmhMbU52Ym1OaGRDaG5kSGh1TGtGd2NHeHBZMkYwYVc5dVEyRnNiRlI0YmlobmNtOTFjRkJ2YzJsMGFXOXVLUzVoY0hCQmNtZHpLREVwS1RzS0lDQWdJR1JwWnlBMENpQWdJQ0JwYm5Salh6QWdMeThnTVFvZ0lDQWdaM1I0Ym5OaGN5QkJjSEJzYVdOaGRHbHZia0Z5WjNNS0lDQWdJR1JwWnlBMkNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUdKMWNua2dOZ29LWVhKak5UVmZZV1JrVkhKaGJuTmhZM1JwYjI1ZllXWjBaWEpmYVdaZlpXeHpaVUE1T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNems0Q2lBZ0lDQXZMeUJuY205MWNGQnZjMmwwYVc5dUlDczlJREU3Q2lBZ0lDQmthV2NnTkFvZ0lDQWdhVzUwWTE4d0lDOHZJREVLSUNBZ0lDc0tJQ0FnSUdSMWNBb2dJQ0FnWW5WeWVTQTJDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvek9Ua0tJQ0FnSUM4dklIMGdkMmhwYkdVZ0tHZHliM1Z3VUc5emFYUnBiMjRnUENCSGJHOWlZV3d1WjNKdmRYQlRhWHBsS1RzS0lDQWdJR2RzYjJKaGJDQkhjbTkxY0ZOcGVtVUtJQ0FnSUR3S0lDQWdJR0p1ZWlCaGNtTTFOVjloWkdSVWNtRnVjMkZqZEdsdmJsOTNhR2xzWlY5MGIzQkFOUW9nSUNBZ1lpQmhjbU0xTlY5aFpHUlVjbUZ1YzJGamRHbHZibDloWm5SbGNsOXBabDlsYkhObFFERTBDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02T2tGU1F6VTFMbUZ5WXpVMVgyRmtaRlJ5WVc1ellXTjBhVzl1UTI5dWRHbHVkV1ZrVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNURiMjUwYVc1MVpXUTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvME1qY3ROREk1Q2lBZ0lDQXZMeUJoY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2YmtOdmJuUnBiblZsWkNnS0lDQWdJQzh2SUNBZ2RISmhibk5oWTNScGIyNDZJR0o1ZEdWekNpQWdJQ0F2THlBcE9pQjJiMmxrSUhzS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdhVzUwWTE4eElDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUhCMWMyaHBiblFnTWlBdkx5QXlDaUFnSUNBckNpQWdJQ0J6ZDJGd0NpQWdJQ0JzWlc0S0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNVE00Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdWSGh1TG5ObGJtUmxjaUE5UFQwZ1IyeHZZbUZzTG1OeVpXRjBiM0pCWkdSeVpYTnpPd29nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnWjJ4dlltRnNJRU55WldGMGIzSkJaR1J5WlhOekNpQWdJQ0E5UFFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZORE13Q2lBZ0lDQXZMeUJwWmlBb0lYUm9hWE11YVhOQlpHMXBiaWdwS1NCN0NpQWdJQ0JpYm5vZ1lYSmpOVFZmWVdSa1ZISmhibk5oWTNScGIyNURiMjUwYVc1MVpXUmZZV1owWlhKZmFXWmZaV3h6WlVBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTXpFS0lDQWdJQzh2SUhSb2FYTXViMjVzZVZOcFoyNWxjaWdwT3dvZ0lDQWdZMkZzYkhOMVlpQnZibXg1VTJsbmJtVnlDZ3BoY21NMU5WOWhaR1JVY21GdWMyRmpkR2x2YmtOdmJuUnBiblZsWkY5aFpuUmxjbDlwWmw5bGJITmxRRE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzBNamN0TkRJNUNpQWdJQ0F2THlCaGNtTTFOVjloWkdSVWNtRnVjMkZqZEdsdmJrTnZiblJwYm5WbFpDZ0tJQ0FnSUM4dklDQWdkSEpoYm5OaFkzUnBiMjQ2SUdKNWRHVnpDaUFnSUNBdkx5QXBPaUIyYjJsa0lIc0tJQ0FnSUdsdWRHTmZNQ0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem82UVZKRE5UVXVZWEpqTlRWZmNtVnRiM1psVkhKaGJuTmhZM1JwYjI1YmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGhjbU0xTlY5eVpXMXZkbVZVY21GdWMyRmpkR2x2YmpvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalEwTVMwME5EVUtJQ0FnSUM4dklHRnlZelUxWDNKbGJXOTJaVlJ5WVc1ellXTjBhVzl1S0FvZ0lDQWdMeThnSUNCMGNtRnVjMkZqZEdsdmJrZHliM1Z3T2lCMWFXNTBOalFzQ2lBZ0lDQXZMeUFnSUdsdVpHVjRPaUJWYVc1ME9Dd0tJQ0FnSUM4dklDQWdjMmxuYm1WeVNXNWtaWGc2SUZWcGJuUTRDaUFnSUNBdkx5QXBPaUIyYjJsa0lIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnT0FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBOalFLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z01Rb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwT0FvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTXdvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RNENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem94TXpnS0lDQWdJQzh2SUhKbGRIVnliaUJVZUc0dWMyVnVaR1Z5SUQwOVBTQkhiRzlpWVd3dVkzSmxZWFJ2Y2tGa1pISmxjM003Q2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNCbmJHOWlZV3dnUTNKbFlYUnZja0ZrWkhKbGMzTUtJQ0FnSUQwOUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTkRZS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1cGMwRmtiV2x1S0NrcElIc0tJQ0FnSUdKdWVpQmhjbU0xTlY5eVpXMXZkbVZVY21GdWMyRmpkR2x2Ymw5aFpuUmxjbDlwWmw5bGJITmxRRE1LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUTBOd29nSUNBZ0x5OGdkR2hwY3k1dmJteDVVMmxuYm1WeUtDazdDaUFnSUNCallXeHNjM1ZpSUc5dWJIbFRhV2R1WlhJS0NtRnlZelUxWDNKbGJXOTJaVlJ5WVc1ellXTjBhVzl1WDJGbWRHVnlYMmxtWDJWc2MyVkFNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qUTFNQzAwTlRRS0lDQWdJQzh2SUdOdmJuTjBJSFJ5WVc1ellXTjBhVzl1UW05NE9pQlVjbUZ1YzJGamRHbHZia2R5YjNWd0lEMGdld29nSUNBZ0x5OGdJQ0J1YjI1alpUb2dkSEpoYm5OaFkzUnBiMjVIY205MWNDd0tJQ0FnSUM4dklDQWdhVzVrWlhnNklHbHVaR1Y0TEFvZ0lDQWdMeThnSUNCemFXZHVaWEpmYVc1a1pYZzZJSE5wWjI1bGNrbHVaR1Y0Q2lBZ0lDQXZMeUI5T3dvZ0lDQWdaR2xuSURJS0lDQWdJR2wwYjJJS0lDQWdJR1JwWnlBeUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnWkdsbklESUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5EVTJDaUFnSUNBdkx5QmpiMjV6ZENCMGVHNU1aVzVuZEdnZ1BTQjBhR2x6TG1GeVl6VTFYM1J5WVc1ellXTjBhVzl1Y3loMGNtRnVjMkZqZEdsdmJrSnZlQ2t1YkdWdVozUm9Pd29nSUNBZ1pIVndDaUFnSUNCaWIzaGZiR1Z1Q2lBZ0lDQmhjM05sY25RZ0x5OGdRbTk0SUcxMWMzUWdhR0YyWlNCMllXeDFaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5EVTNDaUFnSUNBdkx5QjBhR2x6TG1GeVl6VTFYM1J5WVc1ellXTjBhVzl1Y3loMGNtRnVjMkZqZEdsdmJrSnZlQ2t1WkdWc1pYUmxLQ2s3Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmliM2hmWkdWc0NpQWdJQ0J3YjNBS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalEyTWdvZ0lDQWdMeThnWTI5dWMzUWdiV0p5VkhodVJHVmpjbVZoYzJVNklIVnBiblEyTkNBOUlDZ3lOVEF3S1NBcklDZzBNREFnS2lBb01UQWdLeUIwZUc1TVpXNW5kR2dwS1RzS0lDQWdJSEIxYzJocGJuUWdNVEFnTHk4Z01UQUtJQ0FnSUNzS0lDQWdJR2x1ZEdNZ05DQXZMeUEwTURBS0lDQWdJQ29LSUNBZ0lHbHVkR01nTlNBdkx5QXlOVEF3Q2lBZ0lDQXJDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvME5qWXRORFk1Q2lBZ0lDQXZMeUJwZEhodUxuQmhlVzFsYm5Rb2V3b2dJQ0FnTHk4Z0lDQnlaV05sYVhabGNqb2dWSGh1TG5ObGJtUmxjaXdLSUNBZ0lDOHZJQ0FnWVcxdmRXNTBPaUJ0WW5KVWVHNUVaV055WldGelpRb2dJQ0FnTHk4Z2ZTa3VjM1ZpYldsMEtDazdDaUFnSUNCcGRIaHVYMkpsWjJsdUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTmpjS0lDQWdJQzh2SUhKbFkyVnBkbVZ5T2lCVWVHNHVjMlZ1WkdWeUxBb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdhWFI0Ymw5bWFXVnNaQ0JTWldObGFYWmxjZ29nSUNBZ2FYUjRibDltYVdWc1pDQkJiVzkxYm5RS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalEyTmkwME5qa0tJQ0FnSUM4dklHbDBlRzR1Y0dGNWJXVnVkQ2g3Q2lBZ0lDQXZMeUFnSUhKbFkyVnBkbVZ5T2lCVWVHNHVjMlZ1WkdWeUxBb2dJQ0FnTHk4Z0lDQmhiVzkxYm5RNklHMWljbFI0YmtSbFkzSmxZWE5sQ2lBZ0lDQXZMeUI5S1M1emRXSnRhWFFvS1RzS0lDQWdJR2x1ZEdOZk1DQXZMeUF4Q2lBZ0lDQnBkSGh1WDJacFpXeGtJRlI1Y0dWRmJuVnRDaUFnSUNCcGJuUmpYekVnTHk4Z01Bb2dJQ0FnYVhSNGJsOW1hV1ZzWkNCR1pXVUtJQ0FnSUdsMGVHNWZjM1ZpYldsMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8wTnpjdE5EZ3dDaUFnSUNBdkx5QmxiV2wwUEZSeVlXNXpZV04wYVc5dVVtVnRiM1psWkQ0b2V3b2dJQ0FnTHk4Z0lDQjBjbUZ1YzJGamRHbHZia2R5YjNWd09pQjBjbUZ1YzJGamRHbHZia2R5YjNWd0xBb2dJQ0FnTHk4Z0lDQjBjbUZ1YzJGamRHbHZia2x1WkdWNE9pQnBibVJsZUFvZ0lDQWdMeThnZlNrS0lDQWdJSEIxYzJoaWVYUmxjeUF3ZURObE9XSXlZMkUxSUM4dklHMWxkR2h2WkNBaVZISmhibk5oWTNScGIyNVNaVzF2ZG1Wa0tIVnBiblEyTkN4MWFXNTBPQ2tpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZORFF4TFRRME5Rb2dJQ0FnTHk4Z1lYSmpOVFZmY21WdGIzWmxWSEpoYm5OaFkzUnBiMjRvQ2lBZ0lDQXZMeUFnSUhSeVlXNXpZV04wYVc5dVIzSnZkWEE2SUhWcGJuUTJOQ3dLSUNBZ0lDOHZJQ0FnYVc1a1pYZzZJRlZwYm5RNExBb2dJQ0FnTHk4Z0lDQnphV2R1WlhKSmJtUmxlRG9nVldsdWREZ0tJQ0FnSUM4dklDazZJSFp2YVdRZ2V3b2dJQ0FnYVc1MFkxOHdJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCVWtNMU5TNWhjbU0xTlY5elpYUlRhV2R1WVhSMWNtVnpXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak5UVmZjMlYwVTJsbmJtRjBkWEpsY3pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalE1TkMwME9UZ0tJQ0FnSUM4dklHRnlZelUxWDNObGRGTnBaMjVoZEhWeVpYTW9DaUFnSUNBdkx5QWdJR052YzNSek9pQm5kSGh1TGxCaGVXMWxiblJVZUc0c0NpQWdJQ0F2THlBZ0lIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNklIVnBiblEyTkN3S0lDQWdJQzh2SUNBZ2MybG5ibUYwZFhKbGN6b2dZbmwwWlhNOE5qUStXMTBLSUNBZ0lDOHZJQ2s2SUhadmFXUWdld29nSUNBZ2RIaHVJRWR5YjNWd1NXNWtaWGdLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNBdENpQWdJQ0JrZFhBS0lDQWdJR2QwZUc1eklGUjVjR1ZGYm5WdENpQWdJQ0JwYm5Salh6QWdMeThnY0dGNUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJSFJ5WVc1ellXTjBhVzl1SUhSNWNHVWdhWE1nY0dGNUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eUlDOHZJRGdLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1ZFdsdWREWTBDaUFnSUNCaWRHOXBDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXlDaUFnSUNCa2RYQUtJQ0FnSUdsdWRHTmZNU0F2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQndkWE5vYVc1MElEWTBJQzh2SURZMENpQWdJQ0FxQ2lBZ0lDQmtkWEFLSUNBZ0lIQjFjMmhwYm5RZ01pQXZMeUF5Q2lBZ0lDQXJDaUFnSUNCa2FXY2dNZ29nSUNBZ2JHVnVDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbVI1Ym1GdGFXTmZZWEp5WVhrOFlubDBaWE5iTmpSZFBnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TkRrNUNpQWdJQ0F2THlCMGFHbHpMbTl1YkhsVGFXZHVaWElvS1RzS0lDQWdJR05oYkd4emRXSWdiMjVzZVZOcFoyNWxjZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5UQXhDaUFnSUNBdkx5QmpiMjV6ZENCdFluSlRhV2RKYm1OeVpXRnpaU0E5SUhSb2FYTXVZWEpqTlRWZmJXSnlVMmxuU1c1amNtVmhjMlVvYzJsbmJtRjBkWEpsY3k1c1pXNW5kR2dnS2lBMk5DazdDaUFnSUNCallXeHNjM1ZpSUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCVWtNMU5TNWhjbU0xTlY5dFluSlRhV2RKYm1OeVpXRnpaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5UQTBDaUFnSUNBdkx5QmhjM05sY25Rb1kyOXpkSE11Y21WalpXbDJaWElnUFQwOUlFZHNiMkpoYkM1amRYSnlaVzUwUVhCd2JHbGpZWFJwYjI1QlpHUnlaWE56S1FvZ0lDQWdaR2xuSURNS0lDQWdJR2QwZUc1eklGSmxZMlZwZG1WeUNpQWdJQ0JuYkc5aVlXd2dRM1Z5Y21WdWRFRndjR3hwWTJGMGFXOXVRV1JrY21WemN3b2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TlRBMUNpQWdJQ0F2THlCaGMzTmxjblFvWTI5emRITXVZVzF2ZFc1MElENDlJRzFpY2xOcFowbHVZM0psWVhObEtRb2dJQ0FnZFc1amIzWmxjaUF6Q2lBZ0lDQm5kSGh1Y3lCQmJXOTFiblFLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qVXhOQW9nSUNBZ0x5OGdZV1JrY21WemN6b2dWSGh1TG5ObGJtUmxjZ29nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TlRFeUxUVXhOUW9nSUNBZ0x5OGdZMjl1YzNRZ2MybG5ibUYwZFhKbFFtOTRPaUJVY21GdWMyRmpkR2x2YmxOcFoyNWhkSFZ5WlhNZ1BTQjdDaUFnSUNBdkx5QWdJRzV2Ym1ObE9pQjBjbUZ1YzJGamRHbHZia2R5YjNWd0xBb2dJQ0FnTHk4Z0lDQmhaR1J5WlhOek9pQlVlRzR1YzJWdVpHVnlDaUFnSUNBdkx5QjlPd29nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0JwZEc5aUNpQWdJQ0JrZFhBS0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFNVGdLSUNBZ0lDOHZJSFJvYVhNdVlYSmpOVFZmYzJsbmJtRjBkWEpsY3loemFXZHVZWFIxY21WQ2IzZ3BMblpoYkhWbElEMGdZMnh2Ym1Vb2MybG5ibUYwZFhKbGN5azdDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOWtaV3dLSUNBZ0lIQnZjQW9nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0JpYjNoZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvMU1qTUtJQ0FnSUM4dklITnBaMjVsY2pvZ1ZIaHVMbk5sYm1SbGNnb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOVEl4TFRVeU5Bb2dJQ0FnTHk4Z1pXMXBkRHhUYVdkdVlYUjFjbVZUWlhRK0tIc0tJQ0FnSUM4dklDQWdkSEpoYm5OaFkzUnBiMjVIY205MWNEb2dkSEpoYm5OaFkzUnBiMjVIY205MWNDd0tJQ0FnSUM4dklDQWdjMmxuYm1WeU9pQlVlRzR1YzJWdVpHVnlDaUFnSUNBdkx5QjlLUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGhsWTJaaVkySXpNeUF2THlCdFpYUm9iMlFnSWxOcFoyNWhkSFZ5WlZObGRDaDFhVzUwTmpRc1lXUmtjbVZ6Y3lraUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDIxMWJIUnBjMmxuTDJOdmJuUnlZV04wTG1Gc1oyOHVkSE02TkRrMExUUTVPQW9nSUNBZ0x5OGdZWEpqTlRWZmMyVjBVMmxuYm1GMGRYSmxjeWdLSUNBZ0lDOHZJQ0FnWTI5emRITTZJR2QwZUc0dVVHRjViV1Z1ZEZSNGJpd0tJQ0FnSUM4dklDQWdkSEpoYm5OaFkzUnBiMjVIY205MWNEb2dkV2x1ZERZMExBb2dJQ0FnTHk4Z0lDQnphV2R1WVhSMWNtVnpPaUJpZVhSbGN6dzJORDViWFFvZ0lDQWdMeThnS1RvZ2RtOXBaQ0I3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRlNRelUxTG1GeVl6VTFYMk5zWldGeVUybG5ibUYwZFhKbGMxdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUZ5WXpVMVgyTnNaV0Z5VTJsbmJtRjBkWEpsY3pvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPalV6TmkwMU16a0tJQ0FnSUM4dklHRnlZelUxWDJOc1pXRnlVMmxuYm1GMGRYSmxjeWdLSUNBZ0lDOHZJQ0FnZEhKaGJuTmhZM1JwYjI1SGNtOTFjRG9nZFdsdWREWTBMQW9nSUNBZ0x5OGdJQ0JoWkdSeVpYTnpPaUJCWTJOdmRXNTBDaUFnSUNBdkx5QXBPaUIyYjJsa0lIc0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6SWdMeThnT0FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBOalFLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYek1nTHk4Z016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVjM1JoZEdsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnc0lETXlQZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1UTTRDaUFnSUNBdkx5QnlaWFIxY200Z1ZIaHVMbk5sYm1SbGNpQTlQVDBnUjJ4dlltRnNMbU55WldGMGIzSkJaR1J5WlhOek93b2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdaMnh2WW1Gc0lFTnlaV0YwYjNKQlpHUnlaWE56Q2lBZ0lDQTlQUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5UUXdDaUFnSUNBdkx5QnBaaUFvSVhSb2FYTXVhWE5CWkcxcGJpZ3BLU0I3Q2lBZ0lDQmlibm9nWVhKak5UVmZZMnhsWVhKVGFXZHVZWFIxY21WelgyRm1kR1Z5WDJsbVgyVnNjMlZBTXdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZOVFF4Q2lBZ0lDQXZMeUIwYUdsekxtOXViSGxUYVdkdVpYSW9LVHNLSUNBZ0lHTmhiR3h6ZFdJZ2IyNXNlVk5wWjI1bGNnb0tZWEpqTlRWZlkyeGxZWEpUYVdkdVlYUjFjbVZ6WDJGbWRHVnlYMmxtWDJWc2MyVkFNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qVTBOQzAxTkRjS0lDQWdJQzh2SUdOdmJuTjBJSE5wWjI1aGRIVnlaVUp2ZURvZ1ZISmhibk5oWTNScGIyNVRhV2R1WVhSMWNtVnpJRDBnZXdvZ0lDQWdMeThnSUNCdWIyNWpaVG9nZEhKaGJuTmhZM1JwYjI1SGNtOTFjQ3dLSUNBZ0lDOHZJQ0FnWVdSa2NtVnpjem9nWVdSa2NtVnpjd29nSUNBZ0x5OGdmVHNLSUNBZ0lHUnBaeUF4Q2lBZ0lDQnBkRzlpQ2lBZ0lDQmthV2NnTVFvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8xTkRrS0lDQWdJQzh2SUdOdmJuTjBJSE5wWjB4bGJtZDBhRG9nZFdsdWREWTBJRDBnZEdocGN5NWhjbU0xTlY5emFXZHVZWFIxY21WektITnBaMjVoZEhWeVpVSnZlQ2t1YkdWdVozUm9Pd29nSUNBZ1pIVndDaUFnSUNCaWIzaGZiR1Z1Q2lBZ0lDQmhjM05sY25RZ0x5OGdRbTk0SUcxMWMzUWdhR0YyWlNCMllXeDFaUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk5UVXdDaUFnSUNBdkx5QjBhR2x6TG1GeVl6VTFYM05wWjI1aGRIVnlaWE1vYzJsbmJtRjBkWEpsUW05NEtTNWtaV3hsZEdVb0tUc0tJQ0FnSUdScFp5QXhDaUFnSUNCaWIzaGZaR1ZzQ2lBZ0lDQndiM0FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qVTFOUW9nSUNBZ0x5OGdZMjl1YzNRZ2JXSnlVMmxuUkdWamNtVmhjMlU2SUhWcGJuUTJOQ0E5SUNneU5UQXdLU0FySUNnME1EQWdLaUFvTkRBZ0t5QnphV2RNWlc1bmRHZ3BLVHNLSUNBZ0lIQjFjMmhwYm5RZ05EQWdMeThnTkRBS0lDQWdJQ3NLSUNBZ0lHbHVkR01nTkNBdkx5QTBNREFLSUNBZ0lDb0tJQ0FnSUdsdWRHTWdOU0F2THlBeU5UQXdDaUFnSUNBckNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem8xTlRndE5UWXhDaUFnSUNBdkx5QnBkSGh1TG5CaGVXMWxiblFvZXdvZ0lDQWdMeThnSUNCeVpXTmxhWFpsY2pvZ1ZIaHVMbk5sYm1SbGNpd0tJQ0FnSUM4dklDQWdZVzF2ZFc1ME9pQnRZbkpUYVdkRVpXTnlaV0Z6WlFvZ0lDQWdMeThnZlNrdWMzVmliV2wwS0NrN0NpQWdJQ0JwZEhodVgySmxaMmx1Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFOVGtLSUNBZ0lDOHZJSEpsWTJWcGRtVnlPaUJVZUc0dWMyVnVaR1Z5TEFvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ2FYUjRibDltYVdWc1pDQlNaV05sYVhabGNnb2dJQ0FnYVhSNGJsOW1hV1ZzWkNCQmJXOTFiblFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qVTFPQzAxTmpFS0lDQWdJQzh2SUdsMGVHNHVjR0Y1YldWdWRDaDdDaUFnSUNBdkx5QWdJSEpsWTJWcGRtVnlPaUJVZUc0dWMyVnVaR1Z5TEFvZ0lDQWdMeThnSUNCaGJXOTFiblE2SUcxaWNsTnBaMFJsWTNKbFlYTmxDaUFnSUNBdkx5QjlLUzV6ZFdKdGFYUW9LVHNLSUNBZ0lHbHVkR05mTUNBdkx5QXhDaUFnSUNCcGRIaHVYMlpwWld4a0lGUjVjR1ZGYm5WdENpQWdJQ0JwYm5Salh6RWdMeThnTUFvZ0lDQWdhWFI0Ymw5bWFXVnNaQ0JHWldVS0lDQWdJR2wwZUc1ZmMzVmliV2wwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZiWFZzZEdsemFXY3ZZMjl1ZEhKaFkzUXVZV3huYnk1MGN6bzFOamd0TlRjeENpQWdJQ0F2THlCbGJXbDBQRk5wWjI1aGRIVnlaVU5zWldGeVpXUStLSHNLSUNBZ0lDOHZJQ0FnZEhKaGJuTmhZM1JwYjI1SGNtOTFjRG9nZEhKaGJuTmhZM1JwYjI1SGNtOTFjQ3dLSUNBZ0lDOHZJQ0FnYzJsbmJtVnlPaUJoWkdSeVpYTnpDaUFnSUNBdkx5QjlLUW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRPRFV4WmpjMU16QWdMeThnYldWMGFHOWtJQ0pUYVdkdVlYUjFjbVZEYkdWaGNtVmtLSFZwYm5RMk5DeGhaR1J5WlhOektTSUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmJYVnNkR2x6YVdjdlkyOXVkSEpoWTNRdVlXeG5ieTUwY3pvMU16WXROVE01Q2lBZ0lDQXZMeUJoY21NMU5WOWpiR1ZoY2xOcFoyNWhkSFZ5WlhNb0NpQWdJQ0F2THlBZ0lIUnlZVzV6WVdOMGFXOXVSM0p2ZFhBNklIVnBiblEyTkN3S0lDQWdJQzh2SUNBZ1lXUmtjbVZ6Y3pvZ1FXTmpiM1Z1ZEFvZ0lDQWdMeThnS1RvZ2RtOXBaQ0I3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRlNRelUxTG05dWJIbFRhV2R1WlhJb0tTQXRQaUIyYjJsa09ncHZibXg1VTJsbmJtVnlPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1URXhDaUFnSUNBdkx5QmhjM05sY25Rb2RHaHBjeTVoY21NMU5WOWhaR1J5WlhOelEyOTFiblFvVkhodUxuTmxibVJsY2lrdWRtRnNkV1VnSVQwOUlEQXBPd29nSUNBZ2FXNTBZMTh4SUM4dklEQUtJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnUjJ4dlltRnNVM1JoZEdVZ1pYaHBjM1J6Q2lBZ0lDQmhjM05sY25RS0lDQWdJSEpsZEhOMVlnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPanBCVWtNMU5TNWhjbU0xTlY5dFluSlRhV2RKYm1OeVpXRnpaU2h6YVdkdVlYUjFjbVZ6VTJsNlpUb2dkV2x1ZERZMEtTQXRQaUIxYVc1ME5qUTZDbk50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJVa00xTlM1aGNtTTFOVjl0WW5KVGFXZEpibU55WldGelpUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJeU9TMHlNekFLSUNBZ0lDOHZJRUJ5WldGa2IyNXNlUW9nSUNBZ0x5OGdZWEpqTlRWZmJXSnlVMmxuU1c1amNtVmhjMlVvYzJsbmJtRjBkWEpsYzFOcGVtVTZJSFZwYm5RMk5DazZJSFZwYm5RMk5DQjdDaUFnSUNCd2NtOTBieUF4SURFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakl6TVFvZ0lDQWdMeThnWTI5dWMzUWdZM1Z5Y21WdWRFSmhiR0Z1WTJVNklIVnBiblEyTkNBOUlHOXdMbUpoYkdGdVkyVW9SMnh2WW1Gc0xtTjFjbkpsYm5SQmNIQnNhV05oZEdsdmJrRmtaSEpsYzNNcE93b2dJQ0FnWjJ4dlltRnNJRU4xY25KbGJuUkJjSEJzYVdOaGRHbHZia0ZrWkhKbGMzTUtJQ0FnSUdKaGJHRnVZMlVLSUNBZ0lHUjFjQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qTXlDaUFnSUNBdkx5QmpiMjV6ZENCdGFXNXBiWFZ0UW1Gc1lXNWpaVG9nZFdsdWREWTBJRDBnYjNBdWJXbHVRbUZzWVc1alpTaEhiRzlpWVd3dVkzVnljbVZ1ZEVGd2NHeHBZMkYwYVc5dVFXUmtjbVZ6Y3lrN0NpQWdJQ0JuYkc5aVlXd2dRM1Z5Y21WdWRFRndjR3hwWTJGMGFXOXVRV1JrY21WemN3b2dJQ0FnYldsdVgySmhiR0Z1WTJVS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTl0ZFd4MGFYTnBaeTlqYjI1MGNtRmpkQzVoYkdkdkxuUnpPakl6TndvZ0lDQWdMeThnWTI5dWMzUWdiV0p5VTJsblVtVnhkV2x5WldRNklIVnBiblEyTkNBOUlDZ3lOVEF3S1NBcklDZzBNREFnS2lBb05EQWdLeUF5SUNzZ2MybG5ibUYwZFhKbGMxTnBlbVVwS1RzS0lDQWdJSEIxYzJocGJuUWdORElnTHk4Z05ESUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnS3dvZ0lDQWdhVzUwWXlBMElDOHZJRFF3TUFvZ0lDQWdLZ29nSUNBZ2FXNTBZeUExSUM4dklESTFNREFLSUNBZ0lDc0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJek9Rb2dJQ0FnTHk4Z1kyOXVjM1FnYm1WM1RXbHVhVzExYlVKaGJHRnVZMlU2SUhWcGJuUTJOQ0E5SUcxcGJtbHRkVzFDWVd4aGJtTmxJQ3NnYldKeVUybG5VbVZ4ZFdseVpXUTdDaUFnSUNBckNpQWdJQ0JrZFhBS0lDQWdJR052ZG1WeUlESUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJME1Bb2dJQ0FnTHk4Z2FXWWdLR04xY25KbGJuUkNZV3hoYm1ObElENDlJRzVsZDAxcGJtbHRkVzFDWVd4aGJtTmxLU0I3Q2lBZ0lDQStQUW9nSUNBZ1lub2djMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk9rRlNRelUxTG1GeVl6VTFYMjFpY2xOcFowbHVZM0psWVhObFgyRm1kR1Z5WDJsbVgyVnNjMlZBTWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZNalF4Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdNRHNLSUNBZ0lHbHVkR05mTVNBdkx5QXdDaUFnSUNCbWNtRnRaVjlpZFhKNUlEQUtJQ0FnSUhKbGRITjFZZ29LYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0ZTUXpVMUxtRnlZelUxWDIxaWNsTnBaMGx1WTNKbFlYTmxYMkZtZEdWeVgybG1YMlZzYzJWQU1qb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJME5Bb2dJQ0FnTHk4Z2NtVjBkWEp1SUc1bGQwMXBibWx0ZFcxQ1lXeGhibU5sSUMwZ1kzVnljbVZ1ZEVKaGJHRnVZMlU3Q2lBZ0lDQm1jbUZ0WlY5a2FXY2dNUW9nSUNBZ1puSmhiV1ZmWkdsbklEQUtJQ0FnSUMwS0lDQWdJR1p5WVcxbFgySjFjbmtnTUFvZ0lDQWdjbVYwYzNWaUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMjExYkhScGMybG5MMk52Ym5SeVlXTjBMbUZzWjI4dWRITTZPa0ZTUXpVMUxtRnlZelUxWDIxaWNsUjRia2x1WTNKbFlYTmxLSFJ5WVc1ellXTjBhVzl1VTJsNlpUb2dkV2x1ZERZMEtTQXRQaUIxYVc1ME5qUTZDbk50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJVa00xTlM1aGNtTTFOVjl0WW5KVWVHNUpibU55WldGelpUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pJMU15MHlOVFFLSUNBZ0lDOHZJRUJ5WldGa2IyNXNlUW9nSUNBZ0x5OGdZWEpqTlRWZmJXSnlWSGh1U1c1amNtVmhjMlVvZEhKaGJuTmhZM1JwYjI1VGFYcGxPaUIxYVc1ME5qUXBPaUIxYVc1ME5qUWdld29nSUNBZ2NISnZkRzhnTVNBeENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TlRVS0lDQWdJQzh2SUdOdmJuTjBJR04xY25KbGJuUkNZV3hoYm1ObE9pQjFhVzUwTmpRZ1BTQnZjQzVpWVd4aGJtTmxLRWRzYjJKaGJDNWpkWEp5Wlc1MFFYQndiR2xqWVhScGIyNUJaR1J5WlhOektUc0tJQ0FnSUdkc2IySmhiQ0JEZFhKeVpXNTBRWEJ3YkdsallYUnBiMjVCWkdSeVpYTnpDaUFnSUNCaVlXeGhibU5sQ2lBZ0lDQmtkWEFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSTFOZ29nSUNBZ0x5OGdZMjl1YzNRZ2JXbHVhVzExYlVKaGJHRnVZMlU2SUhWcGJuUTJOQ0E5SUc5d0xtMXBia0poYkdGdVkyVW9SMnh2WW1Gc0xtTjFjbkpsYm5SQmNIQnNhV05oZEdsdmJrRmtaSEpsYzNNcE93b2dJQ0FnWjJ4dlltRnNJRU4xY25KbGJuUkJjSEJzYVdOaGRHbHZia0ZrWkhKbGMzTUtJQ0FnSUcxcGJsOWlZV3hoYm1ObENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TmpFS0lDQWdJQzh2SUdOdmJuTjBJRzFpY2xSNGJsSmxjWFZwY21Wa09pQjFhVzUwTmpRZ1BTQW9NalV3TUNrZ0t5QW9OREF3SUNvZ0tERXdJQ3NnZEhKaGJuTmhZM1JwYjI1VGFYcGxLU2s3Q2lBZ0lDQndkWE5vYVc1MElERXdJQzh2SURFd0NpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJQ3NLSUNBZ0lHbHVkR01nTkNBdkx5QTBNREFLSUNBZ0lDb0tJQ0FnSUdsdWRHTWdOU0F2THlBeU5UQXdDaUFnSUNBckNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TmpNS0lDQWdJQzh2SUdOdmJuTjBJRzVsZDAxcGJtbHRkVzFDWVd4aGJtTmxPaUIxYVc1ME5qUWdQU0J0YVc1cGJYVnRRbUZzWVc1alpTQXJJRzFpY2xSNGJsSmxjWFZwY21Wa093b2dJQ0FnS3dvZ0lDQWdaSFZ3Q2lBZ0lDQmpiM1psY2lBeUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TmpRS0lDQWdJQzh2SUdsbUlDaGpkWEp5Wlc1MFFtRnNZVzVqWlNBK1BTQnVaWGROYVc1cGJYVnRRbUZzWVc1alpTa2dld29nSUNBZ1BqMEtJQ0FnSUdKNklITnRZWEowWDJOdmJuUnlZV04wY3k5dGRXeDBhWE5wWnk5amIyNTBjbUZqZEM1aGJHZHZMblJ6T2pwQlVrTTFOUzVoY21NMU5WOXRZbkpVZUc1SmJtTnlaV0Z6WlY5aFpuUmxjbDlwWmw5bGJITmxRRElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qSTJOUW9nSUNBZ0x5OGdjbVYwZFhKdUlEQTdDaUFnSUNCcGJuUmpYekVnTHk4Z01Bb2dJQ0FnWm5KaGJXVmZZblZ5ZVNBd0NpQWdJQ0J5WlhSemRXSUtDbk50WVhKMFgyTnZiblJ5WVdOMGN5OXRkV3gwYVhOcFp5OWpiMjUwY21GamRDNWhiR2R2TG5Sek9qcEJVa00xTlM1aGNtTTFOVjl0WW5KVWVHNUpibU55WldGelpWOWhablJsY2w5cFpsOWxiSE5sUURJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YlhWc2RHbHphV2N2WTI5dWRISmhZM1F1WVd4bmJ5NTBjem95TmpnS0lDQWdJQzh2SUdOdmJuTjBJSEpsYzNWc2REb2dkV2x1ZERZMElEMGdibVYzVFdsdWFXMTFiVUpoYkdGdVkyVWdMU0JqZFhKeVpXNTBRbUZzWVc1alpUc0tJQ0FnSUdaeVlXMWxYMlJwWnlBeENpQWdJQ0JtY21GdFpWOWthV2NnTUFvZ0lDQWdMUW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyMTFiSFJwYzJsbkwyTnZiblJ5WVdOMExtRnNaMjh1ZEhNNk1qWTVDaUFnSUNBdkx5QnlaWFIxY200Z2NtVnpkV3gwT3dvZ0lDQWdabkpoYldWZlluVnllU0F3Q2lBZ0lDQnlaWFJ6ZFdJSyIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZbUZ6WlMxamIyNTBjbUZqZEM1a0xuUnpPanBDWVhObFEyOXVkSEpoWTNRdVkyeGxZWEpUZEdGMFpWQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0J3ZFhOb2FXNTBJREVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZz09In0sImJ5dGVDb2RlIjp7ImFwcHJvdmFsIjoiQ3lBR0FRQUlJSkFEeEJNbUJRUVZIM3gxQzJGeVl6VTFYMjV2Ym1ObEQyRnlZelUxWDNSb2NtVnphRzlzWkFSNWlDTGhDMkZ5WXpVMVgyRmtiV2x1TVJoQUFBWXFJMmNwSTJjeEcwRUFmVEVaRkVReEdFU0NEQVJOQWNoNUJDSkZacThFTzkyWHF3UThwUkgyQkVNaGdRNEVzYVc1cHdUSTNrSm1CQWV2cGNvRXkwa0w4Z1FrZ1dCWEJBWVJoQ0lFdTdmUGx5dUNBd1R0d20zdkJHL1kzZ1VFRVJ1SkpqWWFBSTRRQUFzQUZnQWhBQzRBWHdCOEFKQUFxd0MrQU5FQmZ3R2JBbElDYkFMQ0F4Z0FNUmtVTVJnVUVFUWlReU1xWlVRV0tFeFFzQ0pESXljRVpVUW9URkN3SWtNaktXVkVJZ2dXS0V4UXNDSkROaG9CU1JVa0VrUVhOaG9DU1JVaUVrUTJHZ05KRlNJU1JFOENGazhDVUV4UXZrUkpGUlpYQmdKTVVDaE1VTEFpUXpZYUFVa1ZKQkpFRnpZYUFra1ZKUkpFVEJaTVVMNUVLRXhRc0NKRE5ob0JTUlVrRWtRWEZpTk1aVVFvVEZDd0lrTTJHZ0ZKRlNVU1JDTk1aVVFqRTRBQkFDTlBBbFFvVEZDd0lrTTJHZ0ZKRlNRU1JCZUlBck1XS0V4UXNDSkROaG9CU1JVa0VrUVhpQUxKRmloTVVMQWlReU5IQW9BQU5ob0JTUlVpRWtRMkdnSkpUZ0pKSTFsSlRnTWxDNEVDQ0V3VkVrUWpLV1ZFRkVReEFESUpFa1FYU1VRcVRHY3BJMmNuQkRFQVp5TkpGa2xGQmlOTVpVVUJRUUFoSTBzRlpVeEpUZ0pGQ1VRalRHVkZBVUVBQmtzR2FVc0VhVWtpQ0VVQlF2L1NJMFVFU3dOTEFneEJBRFpMQWxjQ0FFc0VTVTRDSlFzbFdFbEZDRXdXU3dGbkkweGxSUUZBQUFSTEJTTm5JMHNHU1U0Q1pVUWlDR2RMQXlJSVJRUkMvOElpUXpFQU1na1NRQUFEaUFIWkl5bGxSQ0lJS1VzQlp4WW9URkN3SWtNalJ3S0FBREVXSWdrNEVDSVNSRFlhQVVrVkpCSkVGellhQWtrVkloSkVOaG9EU1JVaUVrUTJHZ1JKSTFtQkFnaExBUlVTUkZjQ0FERUFNZ2tTUUFBRGlBR0ZTd05KUkJaTEExQkpSUWxMQWxCRkJ6RVdJZ2hKUlFZeUJBeEFBQjVKUlFaTEJVa1ZpQUdPU0VzSFNieElUTCtBQkJoSnBaUkxDRkN3SWtOSlJRWkxCRWs0RUlFR0VrUTRHREVZRWtFQUZVc0VJOElhS3hKQkFBdExCQ0xDR2tzR1RGQkZCa3NFSWdoSlJRWXlCQXhBLzgxQy82dzJHZ0ZKSTFtQkFnaE1GUkpFTVFBeUNSSkFBQU9JQVBraVF6WWFBVWtWSkJKRUZ6WWFBa2tWSWhKRU5ob0RTUlVpRWtReEFESUpFa0FBQTRnQTAwc0NGa3NDVUVsTEFsQkp2VVJNdkVpQkNnZ2hCQXNoQlFpeE1RQ3lCN0lJSXJJUUk3SUJzNEFFUHBzc3BVeFFzQ0pETVJZaUNVazRFQ0lTUkRZYUFVa1ZKQkpFRnpZYUFra2pXWUZBQzBtQkFnaExBaFVTUklnQWVZZ0FmVXNET0FjeUNoSkVUd000Q0E1RU1RQlBBaFpKVHdKUVNieElUd0svTVFCUWdBVHMrOHN6VEZDd0lrTTJHZ0ZKRlNRU1JCYzJHZ0pKRlNVU1JERUFNZ2tTUUFBRGlBQXZTd0VXU3dGUVNiMUVTd0c4U0lFb0NDRUVDeUVGQ0xFeEFMSUhzZ2dpc2hBanNnR3pnQVNGSDNVd1RGQ3dJa01qTVFCbFJFU0ppZ0VCTWdwZ1NUSUtlSUVxaS84SUlRUUxJUVVJQ0VsT0FnOUJBQVFqakFDSml3R0xBQW1NQUltS0FRRXlDbUJKTWdwNGdRcUwvd2doQkFzaEJRZ0lTVTRDRDBFQUJDT01BSW1MQVlzQUNZd0FpUT09IiwiY2xlYXIiOiJDNEVCUXc9PSJ9LCJjb21waWxlckluZm8iOnsiY29tcGlsZXIiOiJwdXlhIiwiY29tcGlsZXJWZXJzaW9uIjp7Im1ham9yIjo1LCJtaW5vciI6MywicGF0Y2giOjIsImNvbW1pdEhhc2giOm51bGx9fSwiZXZlbnRzIjpbeyJuYW1lIjoiVHJhbnNhY3Rpb25BZGRlZCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkdyb3VwIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFuc2FjdGlvbkluZGV4IiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJUcmFuc2FjdGlvblJlbW92ZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25Hcm91cCIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQ4Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhbnNhY3Rpb25JbmRleCIsImRlc2MiOm51bGx9XX0seyJuYW1lIjoiU2lnbmF0dXJlU2V0IiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVyIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJTaWduYXR1cmVDbGVhcmVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYW5zYWN0aW9uR3JvdXAiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic2lnbmVyIiwiZGVzYyI6bnVsbH1dfV0sInRlbXBsYXRlVmFyaWFibGVzIjp7fSwic2NyYXRjaFZhcmlhYmxlcyI6e319";
    }

}
