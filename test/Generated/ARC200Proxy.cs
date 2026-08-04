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

namespace ARC200
{


    //
    // Smart Contract Token Base Interface
    //
    public class Arc200Proxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public Arc200Proxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
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

        }

        public class Events
        {
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
        ///Transfers tokens
        ///</summary>
        /// <param name="to">The destination of the transfer </param>
        /// <param name="value">Amount of tokens to transfer </param>
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
        ///Increases the allowance of spender by value, avoiding the classic approve() front-running
        ///race condition where an in-flight transferFrom can consume both the old and new allowance.
        ///</summary>
        /// <param name="spender">Who is allowed to take tokens on owner's behalf </param>
        /// <param name="value">Amount to add to the current allowance </param>
        public async Task<bool> Arc200IncreaseAllowance(Algorand.Address spender, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 144, 240, 38, 205 };
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            var result = await base.CallApp(new List<object> { abiHandle, spenderAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc200IncreaseAllowance_Transactions(Algorand.Address spender, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 144, 240, 38, 205 };
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            return await base.MakeTransactionList(new List<object> { abiHandle, spenderAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Decreases the allowance of spender by value, avoiding the classic approve() front-running
        ///race condition where an in-flight transferFrom can consume both the old and new allowance.
        ///</summary>
        /// <param name="spender">Who is allowed to take tokens on owner's behalf </param>
        /// <param name="value">Amount to subtract from the current allowance </param>
        public async Task<bool> Arc200DecreaseAllowance(Algorand.Address spender, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 184, 190, 243, 205 };
            var spenderAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); spenderAbi.From(spender);

            var result = await base.CallApp(new List<object> { abiHandle, spenderAbi, value }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> Arc200DecreaseAllowance_Transactions(Algorand.Address spender, AVM.ClientGenerator.ABI.ARC4.Types.UInt256 value, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 184, 190, 243, 205 };
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
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiQXJjMjAwIiwiZGVzYyI6IlNtYXJ0IENvbnRyYWN0IFRva2VuIEJhc2UgSW50ZXJmYWNlIiwibmV0d29ya3MiOnt9LCJzdHJ1Y3RzIjp7IkFwcHJvdmFsU3RydWN0IjpbeyJuYW1lIjoiYXBwcm92YWxBbW91bnQiLCJ0eXBlIjoidWludDI1NiJ9LHsibmFtZSI6Im93bmVyIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJzcGVuZGVyIiwidHlwZSI6ImFkZHJlc3MifV19LCJNZXRob2RzIjpbeyJuYW1lIjoiYm9vdHN0cmFwIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im5hbWUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InN5bWJvbCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDgiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkZWNpbWFscyIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvdGFsU3VwcGx5IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmMyMDBfVHJhbnNmZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZyb20iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfbmFtZSIsImRlc2MiOiJSZXR1cm5zIHRoZSBuYW1lIG9mIHRoZSB0b2tlbiIsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJieXRlWzMyXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlRoZSBuYW1lIG9mIHRoZSB0b2tlbiJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX3N5bWJvbCIsImRlc2MiOiJSZXR1cm5zIHRoZSBzeW1ib2wgb2YgdGhlIHRva2VuIiwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbOF0iLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJUaGUgc3ltYm9sIG9mIHRoZSB0b2tlbiJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX2RlY2ltYWxzIiwiZGVzYyI6IlJldHVybnMgdGhlIGRlY2ltYWxzIG9mIHRoZSB0b2tlbiIsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50OCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlRoZSBkZWNpbWFscyBvZiB0aGUgdG9rZW4ifSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzIwMF90b3RhbFN1cHBseSIsImRlc2MiOiJSZXR1cm5zIHRoZSB0b3RhbCBzdXBwbHkgb2YgdGhlIHRva2VuIiwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJUaGUgdG90YWwgc3VwcGx5IG9mIHRoZSB0b2tlbiJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX2JhbGFuY2VPZiIsImRlc2MiOiJSZXR1cm5zIHRoZSBjdXJyZW50IGJhbGFuY2Ugb2YgdGhlIG93bmVyIG9mIHRoZSB0b2tlbiIsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3duZXIiLCJkZXNjIjoiVGhlIGFkZHJlc3Mgb2YgdGhlIG93bmVyIG9mIHRoZSB0b2tlbiIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJkZXNjIjoiVGhlIGN1cnJlbnQgYmFsYW5jZSBvZiB0aGUgaG9sZGVyIG9mIHRoZSB0b2tlbiJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX3RyYW5zZmVyIiwiZGVzYyI6IlRyYW5zZmVycyB0b2tlbnMiLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRvIiwiZGVzYyI6IlRoZSBkZXN0aW5hdGlvbiBvZiB0aGUgdHJhbnNmZXIiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOiJBbW91bnQgb2YgdG9rZW5zIHRvIHRyYW5zZmVyIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJTdWNjZXNzIn0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W3sibmFtZSI6ImFyYzIwMF9UcmFuc2ZlciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiZnJvbSIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0byIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX1dLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImFyYzIwMF90cmFuc2ZlckZyb20iLCJkZXNjIjoiVHJhbnNmZXJzIHRva2VucyBmcm9tIHNvdXJjZSB0byBkZXN0aW5hdGlvbiBhcyBhcHByb3ZlZCBzcGVuZGVyIiwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJmcm9tIiwiZGVzYyI6IlRoZSBzb3VyY2Ugb2YgdGhlIHRyYW5zZmVyIiwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjoiVGhlIGRlc3RpbmF0aW9uIG9mIHRoZSB0cmFuc2ZlciIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InZhbHVlIiwiZGVzYyI6IkFtb3VudCBvZiB0b2tlbnMgdG8gdHJhbnNmZXIiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlN1Y2Nlc3MifSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjMjAwX0FwcHJvdmFsIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJvd25lciIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzcGVuZGVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InZhbHVlIiwiZGVzYyI6bnVsbH1dfSx7Im5hbWUiOiJhcmMyMDBfVHJhbnNmZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZyb20iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfYXBwcm92ZSIsImRlc2MiOiJBcHByb3ZlIHNwZW5kZXIgZm9yIGEgdG9rZW4iLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNwZW5kZXIiLCJkZXNjIjoiV2hvIGlzIGFsbG93ZWQgdG8gdGFrZSB0b2tlbnMgb24gb3duZXIncyBiZWhhbGYiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOiJBbW91bnQgb2YgdG9rZW5zIHRvIGJlIHRha2VuIGJ5IHNwZW5kZXIiLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6IlN1Y2Nlc3MifSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbeyJuYW1lIjoiYXJjMjAwX0FwcHJvdmFsIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJvd25lciIsImRlc2MiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzcGVuZGVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InZhbHVlIiwiZGVzYyI6bnVsbH1dfV0sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiYXJjMjAwX2luY3JlYXNlQWxsb3dhbmNlIiwiZGVzYyI6IkluY3JlYXNlcyB0aGUgYWxsb3dhbmNlIG9mIHNwZW5kZXIgYnkgdmFsdWUsIGF2b2lkaW5nIHRoZSBjbGFzc2ljIGFwcHJvdmUoKSBmcm9udC1ydW5uaW5nXG5yYWNlIGNvbmRpdGlvbiB3aGVyZSBhbiBpbi1mbGlnaHQgdHJhbnNmZXJGcm9tIGNhbiBjb25zdW1lIGJvdGggdGhlIG9sZCBhbmQgbmV3IGFsbG93YW5jZS4iLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNwZW5kZXIiLCJkZXNjIjoiV2hvIGlzIGFsbG93ZWQgdG8gdGFrZSB0b2tlbnMgb24gb3duZXIncyBiZWhhbGYiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOiJBbW91bnQgdG8gYWRkIHRvIHRoZSBjdXJyZW50IGFsbG93YW5jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjoiU3VjY2VzcyJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmMyMDBfQXBwcm92YWwiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im93bmVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNwZW5kZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfZGVjcmVhc2VBbGxvd2FuY2UiLCJkZXNjIjoiRGVjcmVhc2VzIHRoZSBhbGxvd2FuY2Ugb2Ygc3BlbmRlciBieSB2YWx1ZSwgYXZvaWRpbmcgdGhlIGNsYXNzaWMgYXBwcm92ZSgpIGZyb250LXJ1bm5pbmdcbnJhY2UgY29uZGl0aW9uIHdoZXJlIGFuIGluLWZsaWdodCB0cmFuc2ZlckZyb20gY2FuIGNvbnN1bWUgYm90aCB0aGUgb2xkIGFuZCBuZXcgYWxsb3dhbmNlLiIsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3BlbmRlciIsImRlc2MiOiJXaG8gaXMgYWxsb3dlZCB0byB0YWtlIHRva2VucyBvbiBvd25lcidzIGJlaGFsZiIsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDI1NiIsInN0cnVjdCI6bnVsbCwibmFtZSI6InZhbHVlIiwiZGVzYyI6IkFtb3VudCB0byBzdWJ0cmFjdCBmcm9tIHRoZSBjdXJyZW50IGFsbG93YW5jZSIsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJib29sIiwic3RydWN0IjpudWxsLCJkZXNjIjoiU3VjY2VzcyJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOlt7Im5hbWUiOiJhcmMyMDBfQXBwcm92YWwiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6Im93bmVyIiwiZGVzYyI6bnVsbH0seyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNwZW5kZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19XSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcmMyMDBfYWxsb3dhbmNlIiwiZGVzYyI6IlJldHVybnMgdGhlIGN1cnJlbnQgYWxsb3dhbmNlIG9mIHRoZSBzcGVuZGVyIG9mIHRoZSB0b2tlbnMgb2YgdGhlIG93bmVyIiwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJvd25lciIsImRlc2MiOiJPd25lcidzIGFjY291bnQiLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzcGVuZGVyIiwiZGVzYyI6IldobyBpcyBhbGxvd2VkIHRvIHRha2UgdG9rZW5zIG9uIG93bmVyJ3MgYmVoYWxmIiwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOiJUaGUgcmVtYWluaW5nIGFsbG93YW5jZSJ9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX1dLCJzdGF0ZSI6eyJzY2hlbWEiOnsiZ2xvYmFsIjp7ImludHMiOjAsImJ5dGVzIjo0fSwibG9jYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjB9fSwia2V5cyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn19LCJtYXBzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfX19LCJiYXJlQWN0aW9ucyI6eyJjcmVhdGUiOlsiTm9PcCJdLCJjYWxsIjpbXX0sInNvdXJjZUluZm8iOnsiYXBwcm92YWwiOnsic291cmNlSW5mbyI6W3sicGMiOls4NDVdLCJlcnJvck1lc3NhZ2UiOiJBIHplcm8tdmFsdWUgYXBwcm92YWwgY2Fubm90IGJlIHVzZWQgdG8gY3JlYXRlIGEgbmV3IGFwcHJvdmFsIGJveCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzcyOF0sImVycm9yTWVzc2FnZSI6IkEgemVyby12YWx1ZSB0cmFuc2ZlciBjYW5ub3QgYmUgdXNlZCB0byBjcmVhdGUgYSBuZXcgYmFsYW5jZSBib3giLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2NzQsODEzXSwiZXJyb3JNZXNzYWdlIjoiQm94IG11c3QgaGF2ZSB2YWx1ZSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzcxMF0sImVycm9yTWVzc2FnZSI6IkNhbm5vdCB0cmFuc2ZlciB0byB0aGUgemVybyBhZGRyZXNzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjA0XSwiZXJyb3JNZXNzYWdlIjoiRGVjcmVhc2UgZXhjZWVkcyBjdXJyZW50IGFsbG93YW5jZSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzY5Nl0sImVycm9yTWVzc2FnZSI6Ikluc3VmZmljaWVudCBiYWxhbmNlIGF0IHRoZSBzZW5kZXIgYWNjb3VudCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIyNl0sImVycm9yTWVzc2FnZSI6Ik5hbWUgb2YgdGhlIGFzc2V0IG11c3QgYmUgbG9uZ2VyIG9yIGVxdWFsIHRvIDEgY2hhcmFjdGVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjI5XSwiZXJyb3JNZXNzYWdlIjoiTmFtZSBvZiB0aGUgYXNzZXQgbXVzdCBiZSBzaG9ydGVyIG9yIGVxdWFsIHRvIDMyIGNoYXJhY3RlcnMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2NV0sImVycm9yTWVzc2FnZSI6Ik9uQ29tcGxldGlvbiBtdXN0IGJlIE5vT3AiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNjhdLCJlcnJvck1lc3NhZ2UiOiJPbkNvbXBsZXRpb24gbXVzdCBiZSBOb09wICYmIGNhbiBvbmx5IGNhbGwgd2hlbiBjcmVhdGluZyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIxOF0sImVycm9yTWVzc2FnZSI6Ik9ubHkgZGVwbG95ZXIgb2YgdGhpcyBzbWFydCBjb250cmFjdCBjYW4gY2FsbCBib290c3RyYXAgbWV0aG9kIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjM3XSwiZXJyb3JNZXNzYWdlIjoiU3ltYm9sIG9mIHRoZSBhc3NldCBtdXN0IGJlIGxvbmdlciBvciBlcXVhbCB0byAxIGNoYXJhY3RlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI0MV0sImVycm9yTWVzc2FnZSI6IlN5bWJvbCBvZiB0aGUgYXNzZXQgbXVzdCBiZSBzaG9ydGVyIG9yIGVxdWFsIHRvIDggY2hhcmFjdGVycyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI0OF0sImVycm9yTWVzc2FnZSI6IlRoaXMgbWV0aG9kIGNhbiBiZSBjYWxsZWQgb25seSBvbmNlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzA2LDMyNiwzNDcsMzU3XSwiZXJyb3JNZXNzYWdlIjoiY2hlY2sgR2xvYmFsU3RhdGUgZXhpc3RzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDU3XSwiZXJyb3JNZXNzYWdlIjoiaW5zdWZmaWNpZW50IGFwcHJvdmFsIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMTc2LDE4OV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgYXJyYXkgbGVuZ3RoIGhlYWRlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE4MywxOTZdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PGFyYzQudWludDg+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzcxLDM4OCw0MTgsNDI4LDUwNyw1MzYsNTgzLDYzNSw2NDNdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5zdGF0aWNfYXJyYXk8YXJjNC51aW50OCwgMzI+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjEyLDM5Niw0MzksNTE1LDU0NCw1OTFdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC51aW50MjU2IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjA0XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudWludDgiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlszMTQsMzM1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBzaXplIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDcxLDU2MCw2MTIsNzM4LDc2MF0sImVycm9yTWVzc2FnZSI6Im92ZXJmbG93IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfV0sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9LCJjbGVhciI6eyJzb3VyY2VJbmZvIjpbXSwicGNPZmZzZXRNZXRob2QiOiJub25lIn19LCJzb3VyY2UiOnsiYXBwcm92YWwiOiJJM0J5WVdkdFlTQjJaWEp6YVc5dUlERXhDaU53Y21GbmJXRWdkSGx3WlhSeVlXTnJJR1poYkhObENnb3ZMeUJBWVd4bmIzSmhibVJtYjNWdVpHRjBhVzl1TDJGc1oyOXlZVzVrTFhSNWNHVnpZM0pwY0hRdllYSmpOQzlwYm1SbGVDNWtMblJ6T2pwRGIyNTBjbUZqZEM1aGNIQnliM1poYkZCeWIyZHlZVzBvS1NBdFBpQjFhVzUwTmpRNkNtMWhhVzQ2Q2lBZ0lDQnBiblJqWW14dlkyc2dNeklnTVNBd0lESUtJQ0FnSUdKNWRHVmpZbXh2WTJzZ01IZ3hOVEZtTjJNM05TQWlZaUlnSW5RaUlEQjRJREI0TnprNE0yTXpOV01nTUhnd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TXpnS0lDQWdJQzh2SUdWNGNHOXlkQ0JqYkdGemN5QkJjbU15TURBZ1pYaDBaVzVrY3lCRGIyNTBjbUZqZENCN0NpQWdJQ0IwZUc0Z1RuVnRRWEJ3UVhKbmN3b2dJQ0FnWW5vZ2JXRnBibDlmWDJGc1oyOTBjMTlmTG1SbFptRjFiSFJEY21WaGRHVkFNVGtLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZENBdkx5QlBia052YlhCc1pYUnBiMjRnYlhWemRDQmlaU0JPYjA5d0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowQ2lBZ0lDQndkWE5vWW5sMFpYTnpJREI0T1RjMU16Z3laVElnTUhnMk5UZGtNVE5sWXlBd2VHSTJZV1V4WVRJMUlEQjRPRFJsWXpFelpEVWdNSGhsWXprNU5qQTBNU0F3ZURneVpUVTNNMk0wSURCNFpHRTNNREkxWWprZ01IZzBZVGsyT0dZNFppQXdlR0kxTkRJeU1USTFJREI0T1RCbU1ESTJZMlFnTUhoaU9HSmxaak5qWkNBd2VHSmlZak14T1dZeklDOHZJRzFsZEdodlpDQWlZbTl2ZEhOMGNtRndLR0o1ZEdWYlhTeGllWFJsVzEwc2RXbHVkRGdzZFdsdWRESTFOaWxpYjI5c0lpd2diV1YwYUc5a0lDSmhjbU15TURCZmJtRnRaU2dwWW5sMFpWc3pNbDBpTENCdFpYUm9iMlFnSW1GeVl6SXdNRjl6ZVcxaWIyd29LV0o1ZEdWYk9GMGlMQ0J0WlhSb2IyUWdJbUZ5WXpJd01GOWtaV05wYldGc2N5Z3BkV2x1ZERnaUxDQnRaWFJvYjJRZ0ltRnlZekl3TUY5MGIzUmhiRk4xY0hCc2VTZ3BkV2x1ZERJMU5pSXNJRzFsZEdodlpDQWlZWEpqTWpBd1gySmhiR0Z1WTJWUFppaGhaR1J5WlhOektYVnBiblF5TlRZaUxDQnRaWFJvYjJRZ0ltRnlZekl3TUY5MGNtRnVjMlpsY2loaFpHUnlaWE56TEhWcGJuUXlOVFlwWW05dmJDSXNJRzFsZEdodlpDQWlZWEpqTWpBd1gzUnlZVzV6Wm1WeVJuSnZiU2hoWkdSeVpYTnpMR0ZrWkhKbGMzTXNkV2x1ZERJMU5pbGliMjlzSWl3Z2JXVjBhRzlrSUNKaGNtTXlNREJmWVhCd2NtOTJaU2hoWkdSeVpYTnpMSFZwYm5ReU5UWXBZbTl2YkNJc0lHMWxkR2h2WkNBaVlYSmpNakF3WDJsdVkzSmxZWE5sUVd4c2IzZGhibU5sS0dGa1pISmxjM01zZFdsdWRESTFOaWxpYjI5c0lpd2diV1YwYUc5a0lDSmhjbU15TURCZlpHVmpjbVZoYzJWQmJHeHZkMkZ1WTJVb1lXUmtjbVZ6Y3l4MWFXNTBNalUyS1dKdmIyd2lMQ0J0WlhSb2IyUWdJbUZ5WXpJd01GOWhiR3h2ZDJGdVkyVW9ZV1JrY21WemN5eGhaR1J5WlhOektYVnBiblF5TlRZaUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBd0NpQWdJQ0J0WVhSamFDQmliMjkwYzNSeVlYQWdZWEpqTWpBd1gyNWhiV1VnWVhKak1qQXdYM041YldKdmJDQmhjbU15TURCZlpHVmphVzFoYkhNZ1lYSmpNakF3WDNSdmRHRnNVM1Z3Y0d4NUlHRnlZekl3TUY5aVlXeGhibU5sVDJZZ1lYSmpNakF3WDNSeVlXNXpabVZ5SUdGeVl6SXdNRjkwY21GdWMyWmxja1p5YjIwZ1lYSmpNakF3WDJGd2NISnZkbVVnWVhKak1qQXdYMmx1WTNKbFlYTmxRV3hzYjNkaGJtTmxJR0Z5WXpJd01GOWtaV055WldGelpVRnNiRzkzWVc1alpTQmhjbU15TURCZllXeHNiM2RoYm1ObENpQWdJQ0JsY25JS0NtMWhhVzVmWDE5aGJHZHZkSE5mWHk1a1pXWmhkV3gwUTNKbFlYUmxRREU1T2dvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPak00Q2lBZ0lDQXZMeUJsZUhCdmNuUWdZMnhoYzNNZ1FYSmpNakF3SUdWNGRHVnVaSE1nUTI5dWRISmhZM1FnZXdvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUNFS0lDQWdJQ1ltQ2lBZ0lDQmhjM05sY25RZ0x5OGdUMjVEYjIxd2JHVjBhVzl1SUcxMWMzUWdZbVVnVG05UGNDQW1KaUJqWVc0Z2IyNXNlU0JqWVd4c0lIZG9aVzRnWTNKbFlYUnBibWNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9rRnlZekl3TUM1aWIyOTBjM1J5WVhCYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGliMjkwYzNSeVlYQTZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5qTUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0JrZFhBS0lDQWdJR2x1ZEdOZk1pQXZMeUF3Q2lBZ0lDQmxlSFJ5WVdOMFgzVnBiblF4TmlBdkx5QnZiaUJsY25KdmNqb2dhVzUyWVd4cFpDQmhjbkpoZVNCc1pXNW5kR2dnYUdWaFpHVnlDaUFnSUNCcGJuUmpYek1nTHk4Z01nb2dJQ0FnS3dvZ0lDQWdaR2xuSURFS0lDQWdJR3hsYmdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1a2VXNWhiV2xqWDJGeWNtRjVQR0Z5WXpRdWRXbHVkRGcrQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF5Q2lBZ0lDQmtkWEFLSUNBZ0lHbHVkR05mTWlBdkx5QXdDaUFnSUNCbGVIUnlZV04wWDNWcGJuUXhOaUF2THlCdmJpQmxjbkp2Y2pvZ2FXNTJZV3hwWkNCaGNuSmhlU0JzWlc1bmRHZ2dhR1ZoWkdWeUNpQWdJQ0JwYm5Salh6TWdMeThnTWdvZ0lDQWdLd29nSUNBZ1pHbG5JREVLSUNBZ0lHeGxiZ29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzVrZVc1aGJXbGpYMkZ5Y21GNVBHRnlZelF1ZFdsdWREZytDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXpDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkRGdLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRFFLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVkV2x1ZERJMU5nb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pZMUNpQWdJQ0F2THlCaGMzTmxjblFvVkhodUxuTmxibVJsY2lBOVBUMGdSMnh2WW1Gc0xtTnlaV0YwYjNKQlpHUnlaWE56TENBblQyNXNlU0JrWlhCc2IzbGxjaUJ2WmlCMGFHbHpJSE50WVhKMElHTnZiblJ5WVdOMElHTmhiaUJqWVd4c0lHSnZiM1J6ZEhKaGNDQnRaWFJvYjJRbktRb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdaMnh2WW1Gc0lFTnlaV0YwYjNKQlpHUnlaWE56Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklFOXViSGtnWkdWd2JHOTVaWElnYjJZZ2RHaHBjeUJ6YldGeWRDQmpiMjUwY21GamRDQmpZVzRnWTJGc2JDQmliMjkwYzNSeVlYQWdiV1YwYUc5a0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TmpZS0lDQWdJQzh2SUdGemMyVnlkQ2h1WVcxbExtNWhkR2wyWlM1c1pXNW5kR2dnUGlBd0xDQW5UbUZ0WlNCdlppQjBhR1VnWVhOelpYUWdiWFZ6ZENCaVpTQnNiMjVuWlhJZ2IzSWdaWEYxWVd3Z2RHOGdNU0JqYUdGeVlXTjBaWEluS1FvZ0lDQWdaR2xuSURNS0lDQWdJR1Y0ZEhKaFkzUWdNaUF3Q2lBZ0lDQnNaVzRLSUNBZ0lHUjFjQW9nSUNBZ1lYTnpaWEowSUM4dklFNWhiV1VnYjJZZ2RHaGxJR0Z6YzJWMElHMTFjM1FnWW1VZ2JHOXVaMlZ5SUc5eUlHVnhkV0ZzSUhSdklERWdZMmhoY21GamRHVnlDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5qY0tJQ0FnSUM4dklHRnpjMlZ5ZENodVlXMWxMbTVoZEdsMlpTNXNaVzVuZEdnZ1BEMGdNeklzSUNkT1lXMWxJRzltSUhSb1pTQmhjM05sZENCdGRYTjBJR0psSUhOb2IzSjBaWElnYjNJZ1pYRjFZV3dnZEc4Z016SWdZMmhoY21GamRHVnljeWNwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNeklLSUNBZ0lEdzlDaUFnSUNCaGMzTmxjblFnTHk4Z1RtRnRaU0J2WmlCMGFHVWdZWE56WlhRZ2JYVnpkQ0JpWlNCemFHOXlkR1Z5SUc5eUlHVnhkV0ZzSUhSdklETXlJR05vWVhKaFkzUmxjbk1LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6bzJPQW9nSUNBZ0x5OGdZWE56WlhKMEtITjViV0p2YkM1dVlYUnBkbVV1YkdWdVozUm9JRDRnTUN3Z0oxTjViV0p2YkNCdlppQjBhR1VnWVhOelpYUWdiWFZ6ZENCaVpTQnNiMjVuWlhJZ2IzSWdaWEYxWVd3Z2RHOGdNU0JqYUdGeVlXTjBaWEluS1FvZ0lDQWdaR2xuSURJS0lDQWdJR1Y0ZEhKaFkzUWdNaUF3Q2lBZ0lDQnNaVzRLSUNBZ0lHUjFjQW9nSUNBZ1lYTnpaWEowSUM4dklGTjViV0p2YkNCdlppQjBhR1VnWVhOelpYUWdiWFZ6ZENCaVpTQnNiMjVuWlhJZ2IzSWdaWEYxWVd3Z2RHOGdNU0JqYUdGeVlXTjBaWElLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6bzJPUW9nSUNBZ0x5OGdZWE56WlhKMEtITjViV0p2YkM1dVlYUnBkbVV1YkdWdVozUm9JRHc5SURnc0lDZFRlVzFpYjJ3Z2IyWWdkR2hsSUdGemMyVjBJRzExYzNRZ1ltVWdjMmh2Y25SbGNpQnZjaUJsY1hWaGJDQjBieUE0SUdOb1lYSmhZM1JsY25NbktRb2dJQ0FnY0hWemFHbHVkQ0E0SUM4dklEZ0tJQ0FnSUR3OUNpQWdJQ0JoYzNObGNuUWdMeThnVTNsdFltOXNJRzltSUhSb1pTQmhjM05sZENCdGRYTjBJR0psSUhOb2IzSjBaWElnYjNJZ1pYRjFZV3dnZEc4Z09DQmphR0Z5WVdOMFpYSnpDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk56QUtJQ0FnSUM4dklHRnpjMlZ5ZENnaGRHaHBjeTUwYjNSaGJGTjFjSEJzZVM1b1lYTldZV3gxWlN3Z0oxUm9hWE1nYldWMGFHOWtJR05oYmlCaVpTQmpZV3hzWldRZ2IyNXNlU0J2Ym1ObEp5a0tJQ0FnSUdsdWRHTmZNaUF2THlBd0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TlRjS0lDQWdJQzh2SUhCMVlteHBZeUIwYjNSaGJGTjFjSEJzZVNBOUlFZHNiMkpoYkZOMFlYUmxQRlZwYm5ReU5UWStLSHNnYTJWNU9pQW5kQ2NnZlNrS0lDQWdJR0o1ZEdWalh6SWdMeThnSW5RaUNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TnpBS0lDQWdJQzh2SUdGemMyVnlkQ2doZEdocGN5NTBiM1JoYkZOMWNIQnNlUzVvWVhOV1lXeDFaU3dnSjFSb2FYTWdiV1YwYUc5a0lHTmhiaUJpWlNCallXeHNaV1FnYjI1c2VTQnZibU5sSnlrS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaWRYSjVJREVLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCVWFHbHpJRzFsZEdodlpDQmpZVzRnWW1VZ1kyRnNiR1ZrSUc5dWJIa2diMjVqWlFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPalF5Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdibUZ0WlNBOUlFZHNiMkpoYkZOMFlYUmxQRVI1Ym1GdGFXTkNlWFJsY3o0b2V5QnJaWGs2SUNkdUp5QjlLUW9nSUNBZ2NIVnphR0o1ZEdWeklDSnVJZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qY3lDaUFnSUNBdkx5QjBhR2x6TG01aGJXVXVkbUZzZFdVZ1BTQnVZVzFsQ2lBZ0lDQjFibU52ZG1WeUlEUUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5EY0tJQ0FnSUM4dklIQjFZbXhwWXlCemVXMWliMndnUFNCSGJHOWlZV3hUZEdGMFpUeEVlVzVoYldsalFubDBaWE0rS0hzZ2EyVjVPaUFuY3ljZ2ZTa0tJQ0FnSUhCMWMyaGllWFJsY3lBaWN5SUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pvM013b2dJQ0FnTHk4Z2RHaHBjeTV6ZVcxaWIyd3VkbUZzZFdVZ1BTQnplVzFpYjJ3S0lDQWdJSFZ1WTI5MlpYSWdNd29nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6bzFOd29nSUNBZ0x5OGdjSFZpYkdsaklIUnZkR0ZzVTNWd2NHeDVJRDBnUjJ4dlltRnNVM1JoZEdVOFZXbHVkREkxTmo0b2V5QnJaWGs2SUNkMEp5QjlLUW9nSUNBZ1lubDBaV05mTWlBdkx5QWlkQ0lLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6bzNOQW9nSUNBZ0x5OGdkR2hwY3k1MGIzUmhiRk4xY0hCc2VTNTJZV3gxWlNBOUlIUnZkR0ZzVTNWd2NHeDVDaUFnSUNCa2FXY2dNUW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6bzFNZ29nSUNBZ0x5OGdjSFZpYkdsaklHUmxZMmx0WVd4eklEMGdSMnh2WW1Gc1UzUmhkR1U4VldsdWREZytLSHNnYTJWNU9pQW5aQ2NnZlNrS0lDQWdJSEIxYzJoaWVYUmxjeUFpWkNJS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem8zTlFvZ0lDQWdMeThnZEdocGN5NWtaV05wYldGc2N5NTJZV3gxWlNBOUlHUmxZMmx0WVd4ekNpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZOellLSUNBZ0lDOHZJR052Ym5OMElITmxibVJsY2lBOUlHNWxkeUJCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwQ2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5Ua0tJQ0FnSUM4dklIQjFZbXhwWXlCaVlXeGhibU5sY3lBOUlFSnZlRTFoY0R4QlpHUnlaWE56TENCVmFXNTBNalUyUGloN0lHdGxlVkJ5WldacGVEb2dKMkluSUgwcENpQWdJQ0JpZVhSbFkxOHhJQzh2SUNKaUlnb2dJQ0FnWkdsbklERUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qYzRDaUFnSUNBdkx5QjBhR2x6TG1KaGJHRnVZMlZ6S0hObGJtUmxjaWt1ZG1Gc2RXVWdQU0IwYjNSaGJGTjFjSEJzZVFvZ0lDQWdaR2xuSURJS0lDQWdJR0p2ZUY5d2RYUUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pvNE1Bb2dJQ0FnTHk4Z1pXMXBkQ2h1WlhjZ1lYSmpNakF3WDFSeVlXNXpabVZ5S0hzZ1puSnZiVG9nYm1WM0lFRmtaSEpsYzNNb1IyeHZZbUZzTG5wbGNtOUJaR1J5WlhOektTd2dkRzg2SUhObGJtUmxjaXdnZG1Gc2RXVTZJSFJ2ZEdGc1UzVndjR3g1SUgwcEtRb2dJQ0FnWjJ4dlltRnNJRnBsY205QlpHUnlaWE56Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnWW5sMFpXTWdOQ0F2THlCdFpYUm9iMlFnSW1GeVl6SXdNRjlVY21GdWMyWmxjaWhoWkdSeVpYTnpMR0ZrWkhKbGMzTXNkV2x1ZERJMU5pa2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qWXpDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb0tRb2dJQ0FnY0hWemFHSjVkR1Z6SURCNE1UVXhaamRqTnpVNE1Bb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPanBCY21NeU1EQXVZWEpqTWpBd1gyNWhiV1ZiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTXlNREJmYm1GdFpUb0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pvNU1Rb2dJQ0FnTHk4Z2NtVjBkWEp1SUc1bGR5QlRkR0YwYVdOQ2VYUmxjend6TWo0b2RHaHBjeTV1WVcxbExuWmhiSFZsTG01aGRHbDJaU2tLSUNBZ0lHbHVkR05mTWlBdkx5QXdDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5ESUtJQ0FnSUM4dklIQjFZbXhwWXlCdVlXMWxJRDBnUjJ4dlltRnNVM1JoZEdVOFJIbHVZVzFwWTBKNWRHVnpQaWg3SUd0bGVUb2dKMjRuSUgwcENpQWdJQ0J3ZFhOb1lubDBaWE1nSW00aUNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02T1RFS0lDQWdJQzh2SUhKbGRIVnliaUJ1WlhjZ1UzUmhkR2xqUW5sMFpYTThNekkrS0hSb2FYTXVibUZ0WlM1MllXeDFaUzV1WVhScGRtVXBDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklFZHNiMkpoYkZOMFlYUmxJR1Y0YVhOMGN3b2dJQ0FnWlhoMGNtRmpkQ0F5SURBS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCemFYcGxDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9Ea0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNoN0lISmxZV1J2Ym14NU9pQjBjblZsSUgwcENpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPanBCY21NeU1EQXVZWEpqTWpBd1gzTjViV0p2YkZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtRnlZekl3TUY5emVXMWliMnc2Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNVEF4Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdibVYzSUZOMFlYUnBZMEo1ZEdWelBEZytLSFJvYVhNdWMzbHRZbTlzTG5aaGJIVmxMbTVoZEdsMlpTa0tJQ0FnSUdsdWRHTmZNaUF2THlBd0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TkRjS0lDQWdJQzh2SUhCMVlteHBZeUJ6ZVcxaWIyd2dQU0JIYkc5aVlXeFRkR0YwWlR4RWVXNWhiV2xqUW5sMFpYTStLSHNnYTJWNU9pQW5jeWNnZlNrS0lDQWdJSEIxYzJoaWVYUmxjeUFpY3lJS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem94TURFS0lDQWdJQzh2SUhKbGRIVnliaUJ1WlhjZ1UzUmhkR2xqUW5sMFpYTThPRDRvZEdocGN5NXplVzFpYjJ3dWRtRnNkV1V1Ym1GMGFYWmxLUW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUdWNGRISmhZM1FnTWlBd0NpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdjSFZ6YUdsdWRDQTRJQzh2SURnS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnphWHBsQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZPVGtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaDdJSEpsWVdSdmJteDVPaUIwY25WbElIMHBDaUFnSUNCaWVYUmxZMTh3SUM4dklEQjRNVFV4Wmpkak56VUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ29LTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pwQmNtTXlNREF1WVhKak1qQXdYMlJsWTJsdFlXeHpXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWVhKak1qQXdYMlJsWTJsdFlXeHpPZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRXhNUW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11WkdWamFXMWhiSE11ZG1Gc2RXVUtJQ0FnSUdsdWRHTmZNaUF2THlBd0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TlRJS0lDQWdJQzh2SUhCMVlteHBZeUJrWldOcGJXRnNjeUE5SUVkc2IySmhiRk4wWVhSbFBGVnBiblE0UGloN0lHdGxlVG9nSjJRbklIMHBDaUFnSUNCd2RYTm9ZbmwwWlhNZ0ltUWlDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1URXhDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTVrWldOcGJXRnNjeTUyWVd4MVpRb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCSGJHOWlZV3hUZEdGMFpTQmxlR2x6ZEhNS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem94TURrS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qcEJjbU15TURBdVlYSmpNakF3WDNSdmRHRnNVM1Z3Y0d4NVczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZWEpqTWpBd1gzUnZkR0ZzVTNWd2NHeDVPZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRXlNUW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11ZEc5MFlXeFRkWEJ3YkhrdWRtRnNkV1VLSUNBZ0lHbHVkR05mTWlBdkx5QXdDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5UY0tJQ0FnSUM4dklIQjFZbXhwWXlCMGIzUmhiRk4xY0hCc2VTQTlJRWRzYjJKaGJGTjBZWFJsUEZWcGJuUXlOVFkrS0hzZ2EyVjVPaUFuZENjZ2ZTa0tJQ0FnSUdKNWRHVmpYeklnTHk4Z0luUWlDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1USXhDaUFnSUNBdkx5QnlaWFIxY200Z2RHaHBjeTUwYjNSaGJGTjFjSEJzZVM1MllXeDFaUW9nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QkhiRzlpWVd4VGRHRjBaU0JsZUdsemRITUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveE1Ua0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNoN0lISmxZV1J2Ym14NU9pQjBjblZsSUgwcENpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPanBCY21NeU1EQXVZWEpqTWpBd1gySmhiR0Z1WTJWUFpsdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbUZ5WXpJd01GOWlZV3hoYm1ObFQyWTZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1UTXdDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2V5QnlaV0ZrYjI1c2VUb2dkSEoxWlNCOUtRb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1DQXZMeUF6TWdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1emRHRjBhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPQ3dnTXpJK0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TVRNeUNpQWdJQ0F2THlCeVpYUjFjbTRnZEdocGN5NWZZbUZzWVc1alpVOW1LRzkzYm1WeUtRb2dJQ0FnWTJGc2JITjFZaUJmWW1Gc1lXNWpaVTltQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNVE13Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9leUJ5WldGa2IyNXNlVG9nZEhKMVpTQjlLUW9nSUNBZ1lubDBaV05mTUNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pvNlFYSmpNakF3TG1GeVl6SXdNRjkwY21GdWMyWmxjbHR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21GeVl6SXdNRjkwY21GdWMyWmxjam9LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3hORElLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURNeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuTjBZWFJwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0TENBek1qNEtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6QWdMeThnTXpJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkREkxTmdvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakUwTkFvZ0lDQWdMeThnY21WMGRYSnVJSFJvYVhNdVgzUnlZVzV6Wm1WeUtHNWxkeUJCWkdSeVpYTnpLRlI0Ymk1elpXNWtaWElwTENCMGJ5d2dkbUZzZFdVcENpQWdJQ0IwZUc0Z1UyVnVaR1Z5Q2lBZ0lDQmpiM1psY2lBeUNpQWdJQ0JqWVd4c2MzVmlJRjkwY21GdWMyWmxjZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTBNZ29nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tDa0tJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02T2tGeVl6SXdNQzVoY21NeU1EQmZkSEpoYm5ObVpYSkdjbTl0VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpNakF3WDNSeVlXNXpabVZ5Um5KdmJUb0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveE5UVUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNncENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0JrZFhCdUlESUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURNeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuTjBZWFJwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0TENBek1qNEtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUdSMWNBb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVjM1JoZEdsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnc0lETXlQZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNd29nSUNBZ1pIVndDaUFnSUNCamIzWmxjaUF5Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh3SUM4dklETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5ReU5UWUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveE5UY0tJQ0FnSUM4dklHTnZibk4wSUhOd1pXNWtaWElnUFNCdVpYY2dRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlLUW9nSUNBZ2RIaHVJRk5sYm1SbGNnb2dJQ0FnWkhWd0NpQWdJQ0JqYjNabGNpQXpDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1UVTRDaUFnSUNBdkx5QmpiMjV6ZENCemNHVnVaR1Z5WDJGc2JHOTNZVzVqWlNBOUlIUm9hWE11WDJGc2JHOTNZVzVqWlNobWNtOXRMQ0J6Y0dWdVpHVnlLUW9nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqWVd4c2MzVmlJRjloYkd4dmQyRnVZMlVLSUNBZ0lHUjFjQW9nSUNBZ1kyOTJaWElnTWdvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakUxT1FvZ0lDQWdMeThnWVhOelpYSjBLSE53Wlc1a1pYSmZZV3hzYjNkaGJtTmxMbUZ6UW1sblZXbHVkQ2dwSUQ0OUlIWmhiSFZsTG1GelFtbG5WV2x1ZENncExDQW5hVzV6ZFdabWFXTnBaVzUwSUdGd2NISnZkbUZzSnlrS0lDQWdJR1JwWnlBeENpQWdJQ0JpUGowS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5OMVptWnBZMmxsYm5RZ1lYQndjbTkyWVd3S0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem94TmpBS0lDQWdJQzh2SUdsbUlDaDJZV3gxWlM1aGMwSnBaMVZwYm5Rb0tTQStJREJ1S1NCN0NpQWdJQ0JpZVhSbFkxOHpJQzh2SURCNENpQWdJQ0JpUGdvZ0lDQWdZbm9nWVhKak1qQXdYM1J5WVc1elptVnlSbkp2YlY5aFpuUmxjbDlwWmw5bGJITmxRRE1LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3hOalFLSUNBZ0lDOHZJR052Ym5OMElHNWxkMTl6Y0dWdVpHVnlYMkZzYkc5M1lXNWpaU0E5SUc1bGR5QlZhVzUwTWpVMktITndaVzVrWlhKZllXeHNiM2RoYm1ObExtRnpRbWxuVldsdWRDZ3BJQzBnZG1Gc2RXVXVZWE5DYVdkVmFXNTBLQ2twQ2lBZ0lDQmtkWEFLSUNBZ0lHUnBaeUF6Q2lBZ0lDQmlMUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNQ0F2THlBek1nb2dJQ0FnUEQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJ2ZG1WeVpteHZkd29nSUNBZ2FXNTBZMTh3SUM4dklETXlDaUFnSUNCaWVtVnlid29nSUNBZ1lud0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveE5qVUtJQ0FnSUM4dklIUm9hWE11WDJGd2NISnZkbVVvWm5KdmJTd2djM0JsYm1SbGNpd2dibVYzWDNOd1pXNWtaWEpmWVd4c2IzZGhibU5sS1FvZ0lDQWdaR2xuSURVS0lDQWdJR1JwWnlBekNpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lHTmhiR3h6ZFdJZ1gyRndjSEp2ZG1VS0lDQWdJSEJ2Y0FvS1lYSmpNakF3WDNSeVlXNXpabVZ5Um5KdmJWOWhablJsY2w5cFpsOWxiSE5sUURNNkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TVRZM0NpQWdJQ0F2THlCeVpYUjFjbTRnZEdocGN5NWZkSEpoYm5ObVpYSW9abkp2YlN3Z2RHOHNJSFpoYkhWbEtRb2dJQ0FnWkdsbklEUUtJQ0FnSUdScFp5QTBDaUFnSUNCa2FXY2dOQW9nSUNBZ1kyRnNiSE4xWWlCZmRISmhibk5tWlhJS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem94TlRVS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qcEJjbU15TURBdVlYSmpNakF3WDJGd2NISnZkbVZiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTXlNREJmWVhCd2NtOTJaVG9LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3hOemNLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDZ3BDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURNeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuTjBZWFJwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0TENBek1qNEtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6QWdMeThnTXpJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdWRXbHVkREkxTmdvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakUzT1FvZ0lDQWdMeThnWTI5dWMzUWdiM2R1WlhJZ1BTQnVaWGNnUVdSa2NtVnpjeWhVZUc0dWMyVnVaR1Z5S1FvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qRTRNQW9nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11WDJGd2NISnZkbVVvYjNkdVpYSXNJSE53Wlc1a1pYSXNJSFpoYkhWbEtRb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ1kyRnNiSE4xWWlCZllYQndjbTkyWlFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakUzTndvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9rRnlZekl3TUM1aGNtTXlNREJmYVc1amNtVmhjMlZCYkd4dmQyRnVZMlZiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwaGNtTXlNREJmYVc1amNtVmhjMlZCYkd4dmQyRnVZMlU2Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNVGt4Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9LUW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNQ0F2THlBek1nb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNXpkR0YwYVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0N3Z016SStDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXlDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURNeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblF5TlRZS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem94T1RNS0lDQWdJQzh2SUdOdmJuTjBJRzkzYm1WeUlEMGdibVYzSUVGa1pISmxjM01vVkhodUxuTmxibVJsY2lrS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3hPVFFLSUNBZ0lDOHZJR052Ym5OMElHTjFjbkpsYm5RZ1BTQjBhR2x6TGw5aGJHeHZkMkZ1WTJVb2IzZHVaWElzSUhOd1pXNWtaWElwQ2lBZ0lDQmtkWEFLSUNBZ0lHUnBaeUF6Q2lBZ0lDQmpZV3hzYzNWaUlGOWhiR3h2ZDJGdVkyVUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveE9UVUtJQ0FnSUM4dklISmxkSFZ5YmlCMGFHbHpMbDloY0hCeWIzWmxLRzkzYm1WeUxDQnpjR1Z1WkdWeUxDQnVaWGNnVldsdWRESTFOaWhqZFhKeVpXNTBMbUZ6UW1sblZXbHVkQ2dwSUNzZ2RtRnNkV1V1WVhOQ2FXZFZhVzUwS0NrcEtRb2dJQ0FnZFc1amIzWmxjaUF5Q2lBZ0lDQmlLd29nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNQ0F2THlBek1nb2dJQ0FnUEQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJ2ZG1WeVpteHZkd29nSUNBZ2FXNTBZMTh3SUM4dklETXlDaUFnSUNCaWVtVnlid29nSUNBZ1lud0tJQ0FnSUhOM1lYQUtJQ0FnSUdOdmRtVnlJRElLSUNBZ0lHTmhiR3h6ZFdJZ1gyRndjSEp2ZG1VS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem94T1RFS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2dwQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qcEJjbU15TURBdVlYSmpNakF3WDJSbFkzSmxZWE5sUVd4c2IzZGhibU5sVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpNakF3WDJSbFkzSmxZWE5sUVd4c2IzZGhibU5sT2dvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakl3TmdvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVjM1JoZEdsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnc0lETXlQZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNZ29nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNQ0F2THlBek1nb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwTWpVMkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TWpBNENpQWdJQ0F2THlCamIyNXpkQ0J2ZDI1bGNpQTlJRzVsZHlCQlpHUnlaWE56S0ZSNGJpNXpaVzVrWlhJcENpQWdJQ0IwZUc0Z1UyVnVaR1Z5Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNakE1Q2lBZ0lDQXZMeUJqYjI1emRDQmpkWEp5Wlc1MElEMGdkR2hwY3k1ZllXeHNiM2RoYm1ObEtHOTNibVZ5TENCemNHVnVaR1Z5S1FvZ0lDQWdaSFZ3Q2lBZ0lDQmthV2NnTXdvZ0lDQWdZMkZzYkhOMVlpQmZZV3hzYjNkaGJtTmxDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1qRXdDaUFnSUNBdkx5QmhjM05sY25Rb1kzVnljbVZ1ZEM1aGMwSnBaMVZwYm5Rb0tTQStQU0IyWVd4MVpTNWhjMEpwWjFWcGJuUW9LU3dnSjBSbFkzSmxZWE5sSUdWNFkyVmxaSE1nWTNWeWNtVnVkQ0JoYkd4dmQyRnVZMlVuS1FvZ0lDQWdaSFZ3Q2lBZ0lDQmthV2NnTXdvZ0lDQWdZajQ5Q2lBZ0lDQmhjM05sY25RZ0x5OGdSR1ZqY21WaGMyVWdaWGhqWldWa2N5QmpkWEp5Wlc1MElHRnNiRzkzWVc1alpRb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pJeE1Rb2dJQ0FnTHk4Z2NtVjBkWEp1SUhSb2FYTXVYMkZ3Y0hKdmRtVW9iM2R1WlhJc0lITndaVzVrWlhJc0lHNWxkeUJWYVc1ME1qVTJLR04xY25KbGJuUXVZWE5DYVdkVmFXNTBLQ2tnTFNCMllXeDFaUzVoYzBKcFoxVnBiblFvS1NrcENpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lHSXRDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHdJQzh2SURNeUNpQWdJQ0E4UFFvZ0lDQWdZWE56WlhKMElDOHZJRzkyWlhKbWJHOTNDaUFnSUNCcGJuUmpYekFnTHk4Z016SUtJQ0FnSUdKNlpYSnZDaUFnSUNCaWZBb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5MlpYSWdNZ29nSUNBZ1kyRnNiSE4xWWlCZllYQndjbTkyWlFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakl3TmdvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtLQ2tLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk9rRnlZekl3TUM1aGNtTXlNREJmWVd4c2IzZGhibU5sVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYSmpNakF3WDJGc2JHOTNZVzVqWlRvS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem95TWpFS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh3SUM4dklETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbk4wWVhScFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNExDQXpNajRLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRElLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYekFnTHk4Z016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVjM1JoZEdsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnc0lETXlQZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qSXlNd29nSUNBZ0x5OGdjbVYwZFhKdUlIUm9hWE11WDJGc2JHOTNZVzVqWlNodmQyNWxjaXdnYzNCbGJtUmxjaWtLSUNBZ0lHTmhiR3h6ZFdJZ1gyRnNiRzkzWVc1alpRb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pJeU1Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0hzZ2NtVmhaRzl1YkhrNklIUnlkV1VnZlNrS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZPa0Z5WXpJd01DNWZZbUZzWVc1alpVOW1LRzkzYm1WeU9pQmllWFJsY3lrZ0xUNGdZbmwwWlhNNkNsOWlZV3hoYm1ObFQyWTZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1qSTJDaUFnSUNBdkx5QndjbWwyWVhSbElGOWlZV3hoYm1ObFQyWW9iM2R1WlhJNklFRmtaSEpsYzNNcE9pQlZhVzUwTWpVMklIc0tJQ0FnSUhCeWIzUnZJREVnTVFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPalU1Q2lBZ0lDQXZMeUJ3ZFdKc2FXTWdZbUZzWVc1alpYTWdQU0JDYjNoTllYQThRV1JrY21WemN5d2dWV2x1ZERJMU5qNG9leUJyWlhsUWNtVm1hWGc2SUNkaUp5QjlLUW9nSUNBZ1lubDBaV05mTVNBdkx5QWlZaUlLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmtkWEFLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3lNamNLSUNBZ0lDOHZJR2xtSUNnaGRHaHBjeTVpWVd4aGJtTmxjeWh2ZDI1bGNpa3VaWGhwYzNSektTQnlaWFIxY200Z2JtVjNJRlZwYm5ReU5UWW9NQ2tLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW01NklGOWlZV3hoYm1ObFQyWmZZV1owWlhKZmFXWmZaV3h6WlVBeUNpQWdJQ0JpZVhSbFl5QTFJQzh2SURCNE1EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01Bb2dJQ0FnYzNkaGNBb2dJQ0FnY21WMGMzVmlDZ3BmWW1Gc1lXNWpaVTltWDJGbWRHVnlYMmxtWDJWc2MyVkFNam9LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3lNamdLSUNBZ0lDOHZJSEpsZEhWeWJpQjBhR2x6TG1KaGJHRnVZMlZ6S0c5M2JtVnlLUzUyWVd4MVpRb2dJQ0FnWm5KaGJXVmZaR2xuSURBS0lDQWdJR0p2ZUY5blpYUUtJQ0FnSUdGemMyVnlkQ0F2THlCQ2IzZ2diWFZ6ZENCb1lYWmxJSFpoYkhWbENpQWdJQ0J6ZDJGd0NpQWdJQ0J5WlhSemRXSUtDZ292THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02T2tGeVl6SXdNQzVmZEhKaGJuTm1aWElvYzJWdVpHVnlPaUJpZVhSbGN5d2djbVZqYVhCcFpXNTBPaUJpZVhSbGN5d2dZVzF2ZFc1ME9pQmllWFJsY3lrZ0xUNGdZbmwwWlhNNkNsOTBjbUZ1YzJabGNqb0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveU16RUtJQ0FnSUM4dklIQnlhWFpoZEdVZ1gzUnlZVzV6Wm1WeUtITmxibVJsY2pvZ1FXUmtjbVZ6Y3l3Z2NtVmphWEJwWlc1ME9pQkJaR1J5WlhOekxDQmhiVzkxYm5RNklGVnBiblF5TlRZcE9pQkNiMjlzSUhzS0lDQWdJSEJ5YjNSdklETWdNUW9nSUNBZ2FXNTBZMTh5SUM4dklEQUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveU16SUtJQ0FnSUM4dklHTnZibk4wSUhObGJtUmxjbDlpWVd4aGJtTmxJRDBnZEdocGN5NWZZbUZzWVc1alpVOW1LSE5sYm1SbGNpa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnWTJGc2JITjFZaUJmWW1Gc1lXNWpaVTltQ2lBZ0lDQmtkWEFLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3lNek1LSUNBZ0lDOHZJR052Ym5OMElISmxZMmx3YVdWdWRGOWlZV3hoYm1ObElEMGdkR2hwY3k1ZlltRnNZVzVqWlU5bUtISmxZMmx3YVdWdWRDa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWTJGc2JITjFZaUJmWW1Gc1lXNWpaVTltQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNak0wQ2lBZ0lDQXZMeUJoYzNObGNuUW9jMlZ1WkdWeVgySmhiR0Z1WTJVdVlYTkNhV2RWYVc1MEtDa2dQajBnWVcxdmRXNTBMbUZ6UW1sblZXbHVkQ2dwTENBblNXNXpkV1ptYVdOcFpXNTBJR0poYkdGdVkyVWdZWFFnZEdobElITmxibVJsY2lCaFkyTnZkVzUwSnlrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TVFvZ0lDQWdZajQ5Q2lBZ0lDQmhjM05sY25RZ0x5OGdTVzV6ZFdabWFXTnBaVzUwSUdKaGJHRnVZMlVnWVhRZ2RHaGxJSE5sYm1SbGNpQmhZMk52ZFc1MENpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TWpNMkNpQWdJQ0F2THlCcFppQW9jMlZ1WkdWeUlDRTlQU0J5WldOcGNHbGxiblFwSUhzS0lDQWdJR1p5WVcxbFgyUnBaeUF0TXdvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQWhQUW9nSUNBZ1lub2dYM1J5WVc1elptVnlYMkZtZEdWeVgybG1YMlZzYzJWQU5Bb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pJek53b2dJQ0FnTHk4Z1lYTnpaWEowS0hKbFkybHdhV1Z1ZENBaFBUMGdibVYzSUVGa1pISmxjM01vUjJ4dlltRnNMbnBsY205QlpHUnlaWE56S1N3Z0owTmhibTV2ZENCMGNtRnVjMlpsY2lCMGJ5QjBhR1VnZW1WeWJ5QmhaR1J5WlhOekp5a0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWjJ4dlltRnNJRnBsY205QlpHUnlaWE56Q2lBZ0lDQWhQUW9nSUNBZ1lYTnpaWEowSUM4dklFTmhibTV2ZENCMGNtRnVjMlpsY2lCMGJ5QjBhR1VnZW1WeWJ5QmhaR1J5WlhOekNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TlRrS0lDQWdJQzh2SUhCMVlteHBZeUJpWVd4aGJtTmxjeUE5SUVKdmVFMWhjRHhCWkdSeVpYTnpMQ0JWYVc1ME1qVTJQaWg3SUd0bGVWQnlaV1pwZURvZ0oySW5JSDBwQ2lBZ0lDQmllWFJsWTE4eElDOHZJQ0ppSWdvZ0lDQWdabkpoYldWZlpHbG5JQzB5Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1IxY0FvZ0lDQWdabkpoYldWZlluVnllU0F3Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNak00Q2lBZ0lDQXZMeUJwWmlBb0lYUm9hWE11WW1Gc1lXNWpaWE1vY21WamFYQnBaVzUwS1M1bGVHbHpkSE1wSUhzS0lDQWdJR0p2ZUY5c1pXNEtJQ0FnSUdKMWNua2dNUW9nSUNBZ1ltNTZJRjkwY21GdWMyWmxjbDloWm5SbGNsOXBabDlsYkhObFFETUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWhjbU15TURBdVlXeG5ieTUwY3pveU5ERUtJQ0FnSUM4dklHRnpjMlZ5ZENoaGJXOTFiblF1WVhOQ2FXZFZhVzUwS0NrZ1BpQXdiaXdnSjBFZ2VtVnlieTEyWVd4MVpTQjBjbUZ1YzJabGNpQmpZVzV1YjNRZ1ltVWdkWE5sWkNCMGJ5QmpjbVZoZEdVZ1lTQnVaWGNnWW1Gc1lXNWpaU0JpYjNnbktRb2dJQ0FnWm5KaGJXVmZaR2xuSUMweENpQWdJQ0JpZVhSbFkxOHpJQzh2SURCNENpQWdJQ0JpUGdvZ0lDQWdZWE56WlhKMElDOHZJRUVnZW1WeWJ5MTJZV3gxWlNCMGNtRnVjMlpsY2lCallXNXViM1FnWW1VZ2RYTmxaQ0IwYnlCamNtVmhkR1VnWVNCdVpYY2dZbUZzWVc1alpTQmliM2dLQ2w5MGNtRnVjMlpsY2w5aFpuUmxjbDlwWmw5bGJITmxRRE02Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNalF6Q2lBZ0lDQXZMeUIwYUdsekxtSmhiR0Z1WTJWektITmxibVJsY2lrdWRtRnNkV1VnUFNCdVpYY2dWV2x1ZERJMU5paHpaVzVrWlhKZlltRnNZVzVqWlM1aGMwSnBaMVZwYm5Rb0tTQXRJR0Z0YjNWdWRDNWhjMEpwWjFWcGJuUW9LU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXhDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdJdENpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4d0lDOHZJRE15Q2lBZ0lDQThQUW9nSUNBZ1lYTnpaWEowSUM4dklHOTJaWEptYkc5M0NpQWdJQ0JwYm5Salh6QWdMeThnTXpJS0lDQWdJR0o2WlhKdkNpQWdJQ0J6ZDJGd0NpQWdJQ0JrYVdjZ01Rb2dJQ0FnWW53S0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem8xT1FvZ0lDQWdMeThnY0hWaWJHbGpJR0poYkdGdVkyVnpJRDBnUW05NFRXRndQRUZrWkhKbGMzTXNJRlZwYm5ReU5UWStLSHNnYTJWNVVISmxabWw0T2lBbllpY2dmU2tLSUNBZ0lHSjVkR1ZqWHpFZ0x5OGdJbUlpQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dMVE1LSUNBZ0lHTnZibU5oZEFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakkwTXdvZ0lDQWdMeThnZEdocGN5NWlZV3hoYm1ObGN5aHpaVzVrWlhJcExuWmhiSFZsSUQwZ2JtVjNJRlZwYm5ReU5UWW9jMlZ1WkdWeVgySmhiR0Z1WTJVdVlYTkNhV2RWYVc1MEtDa2dMU0JoYlc5MWJuUXVZWE5DYVdkVmFXNTBLQ2twQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmliM2hmY0hWMENpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TWpRMENpQWdJQ0F2THlCMGFHbHpMbUpoYkdGdVkyVnpLSEpsWTJsd2FXVnVkQ2t1ZG1Gc2RXVWdQU0J1WlhjZ1ZXbHVkREkxTmloeVpXTnBjR2xsYm5SZlltRnNZVzVqWlM1aGMwSnBaMVZwYm5Rb0tTQXJJR0Z0YjNWdWRDNWhjMEpwWjFWcGJuUW9LU2tLSUNBZ0lHWnlZVzFsWDJScFp5QXlDaUFnSUNCbWNtRnRaVjlrYVdjZ0xURUtJQ0FnSUdJckNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4d0lDOHZJRE15Q2lBZ0lDQThQUW9nSUNBZ1lYTnpaWEowSUM4dklHOTJaWEptYkc5M0NpQWdJQ0JpZkFvZ0lDQWdabkpoYldWZlpHbG5JREFLSUNBZ0lITjNZWEFLSUNBZ0lHSnZlRjl3ZFhRS0NsOTBjbUZ1YzJabGNsOWhablJsY2w5cFpsOWxiSE5sUURRNkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdllYSmpNakF3TG1Gc1oyOHVkSE02TWpRMkNpQWdJQ0F2THlCbGJXbDBLRzVsZHlCaGNtTXlNREJmVkhKaGJuTm1aWElvZXlCbWNtOXRPaUJ6Wlc1a1pYSXNJSFJ2T2lCeVpXTnBjR2xsYm5Rc0lIWmhiSFZsT2lCaGJXOTFiblFnZlNrcENpQWdJQ0JtY21GdFpWOWthV2NnTFRNS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JtY21GdFpWOWthV2NnTFRFS0lDQWdJR052Ym1OaGRBb2dJQ0FnWW5sMFpXTWdOQ0F2THlCdFpYUm9iMlFnSW1GeVl6SXdNRjlVY21GdWMyWmxjaWhoWkdSeVpYTnpMR0ZrWkhKbGMzTXNkV2x1ZERJMU5pa2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qSTBOd29nSUNBZ0x5OGdjbVYwZFhKdUlHNWxkeUJDYjI5c0tIUnlkV1VwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGc0TUFvZ0lDQWdabkpoYldWZlluVnllU0F3Q2lBZ0lDQnlaWFJ6ZFdJS0Nnb3ZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZPa0Z5WXpJd01DNWZZV3hzYjNkaGJtTmxLRzkzYm1WeU9pQmllWFJsY3l3Z2MzQmxibVJsY2pvZ1lubDBaWE1wSUMwK0lHSjVkR1Z6T2dwZllXeHNiM2RoYm1ObE9nb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pJMU5Bb2dJQ0FnTHk4Z2NISnBkbUYwWlNCZllXeHNiM2RoYm1ObEtHOTNibVZ5T2lCQlpHUnlaWE56TENCemNHVnVaR1Z5T2lCQlpHUnlaWE56S1RvZ1ZXbHVkREkxTmlCN0NpQWdJQ0J3Y205MGJ5QXlJREVLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3lOVEVLSUNBZ0lDOHZJSEpsZEhWeWJpQnVaWGNnVTNSaGRHbGpRbmwwWlhNOE16SStLRzl3TG5Ob1lUSTFOaWh2Y0M1amIyNWpZWFFvYjNkdVpYSXVZbmwwWlhNc0lITndaVzVrWlhJdVlubDBaWE1wS1NrS0lDQWdJR1p5WVcxbFgyUnBaeUF0TWdvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJSE5vWVRJMU5nb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pZeENpQWdJQ0F2THlCd2RXSnNhV01nWVhCd2NtOTJZV3h6SUQwZ1FtOTRUV0Z3UEZOMFlYUnBZMEo1ZEdWelBETXlQaXdnUVhCd2NtOTJZV3hUZEhKMVkzUStLSHNnYTJWNVVISmxabWw0T2lBbllTY2dmU2tLSUNBZ0lIQjFjMmhpZVhSbGN5QWlZU0lLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WVhKak1qQXdMbUZzWjI4dWRITTZNalUyQ2lBZ0lDQXZMeUJwWmlBb0lYUm9hWE11WVhCd2NtOTJZV3h6S0d0bGVTa3VaWGhwYzNSektTQnlaWFIxY200Z2JtVjNJRlZwYm5ReU5UWW9NQ2tLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW01NklGOWhiR3h2ZDJGdVkyVmZZV1owWlhKZmFXWmZaV3h6WlVBeUNpQWdJQ0JpZVhSbFl5QTFJQzh2SURCNE1EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01EQXdNREF3TURBd01Bb2dJQ0FnYzNkaGNBb2dJQ0FnY21WMGMzVmlDZ3BmWVd4c2IzZGhibU5sWDJGbWRHVnlYMmxtWDJWc2MyVkFNam9LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTloY21NeU1EQXVZV3huYnk1MGN6b3lOVGNLSUNBZ0lDOHZJSEpsZEhWeWJpQjBhR2x6TG1Gd2NISnZkbUZzY3loclpYa3BMblpoYkhWbExtRndjSEp2ZG1Gc1FXMXZkVzUwQ2lBZ0lDQm1jbUZ0WlY5a2FXY2dNQW9nSUNBZ1ltOTRYMmRsZEFvZ0lDQWdZWE56WlhKMElDOHZJRUp2ZUNCdGRYTjBJR2hoZG1VZ2RtRnNkV1VLSUNBZ0lHVjRkSEpoWTNRZ01DQXpNZ29nSUNBZ2MzZGhjQW9nSUNBZ2NtVjBjM1ZpQ2dvS0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qcEJjbU15TURBdVgyRndjSEp2ZG1Vb2IzZHVaWEk2SUdKNWRHVnpMQ0J6Y0dWdVpHVnlPaUJpZVhSbGN5d2dZVzF2ZFc1ME9pQmllWFJsY3lrZ0xUNGdZbmwwWlhNNkNsOWhjSEJ5YjNabE9nb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwyRnlZekl3TUM1aGJHZHZMblJ6T2pJMk1Bb2dJQ0FnTHk4Z2NISnBkbUYwWlNCZllYQndjbTkyWlNodmQyNWxjam9nUVdSa2NtVnpjeXdnYzNCbGJtUmxjam9nUVdSa2NtVnpjeXdnWVcxdmRXNTBPaUJWYVc1ME1qVTJLVG9nUW05dmJDQjdDaUFnSUNCd2NtOTBieUF6SURFS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem95TlRFS0lDQWdJQzh2SUhKbGRIVnliaUJ1WlhjZ1UzUmhkR2xqUW5sMFpYTThNekkrS0c5d0xuTm9ZVEkxTmlodmNDNWpiMjVqWVhRb2IzZHVaWEl1WW5sMFpYTXNJSE53Wlc1a1pYSXVZbmwwWlhNcEtTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE13b2dJQ0FnWm5KaGJXVmZaR2xuSUMweUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnYzJoaE1qVTJDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk5qRUtJQ0FnSUM4dklIQjFZbXhwWXlCaGNIQnliM1poYkhNZ1BTQkNiM2hOWVhBOFUzUmhkR2xqUW5sMFpYTThNekkrTENCQmNIQnliM1poYkZOMGNuVmpkRDRvZXlCclpYbFFjbVZtYVhnNklDZGhKeUI5S1FvZ0lDQWdjSFZ6YUdKNWRHVnpJQ0poSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JrZFhBS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aGNtTXlNREF1WVd4bmJ5NTBjem95TmpJS0lDQWdJQzh2SUdsbUlDZ2hkR2hwY3k1aGNIQnliM1poYkhNb2EyVjVLUzVsZUdsemRITXBJSHNLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnWW01NklGOWhjSEJ5YjNabFgyRm1kR1Z5WDJsbVgyVnNjMlZBTWdvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJGeVl6SXdNQzVoYkdkdkxuUnpPakkyTmdvZ0lDQWdMeThnWVhOelpYSjBLR0Z0YjNWdWRDNWhjMEpwWjFWcGJuUW9LU0ErSURCdUxDQW5RU0I2WlhKdkxYWmhiSFZsSUdGd2NISnZkbUZzSUdOaGJtNXZkQ0JpWlNCMWMyVmtJSFJ2SUdOeVpXRjBaU0JoSUc1bGR5QmhjSEJ5YjNaaGJDQmliM2duS1FvZ0lDQWdabkpoYldWZlpHbG5JQzB4Q2lBZ0lDQmllWFJsWTE4eklDOHZJREI0Q2lBZ0lDQmlQZ29nSUNBZ1lYTnpaWEowSUM4dklFRWdlbVZ5YnkxMllXeDFaU0JoY0hCeWIzWmhiQ0JqWVc1dWIzUWdZbVVnZFhObFpDQjBieUJqY21WaGRHVWdZU0J1WlhjZ1lYQndjbTkyWVd3Z1ltOTRDZ3BmWVhCd2NtOTJaVjloWm5SbGNsOXBabDlsYkhObFFESTZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1qWTRMVEkzTWdvZ0lDQWdMeThnWTI5dWMzUWdZWEJ3Y205MllXeENiM2c2SUVGd2NISnZkbUZzVTNSeWRXTjBJRDBnYm1WM0lFRndjSEp2ZG1Gc1UzUnlkV04wS0hzS0lDQWdJQzh2SUNBZ1lYQndjbTkyWVd4QmJXOTFiblE2SUdGdGIzVnVkQ3dLSUNBZ0lDOHZJQ0FnYjNkdVpYSTZJRzkzYm1WeUxBb2dJQ0FnTHk4Z0lDQnpjR1Z1WkdWeU9pQnpjR1Z1WkdWeUxBb2dJQ0FnTHk4Z2ZTa0tJQ0FnSUdaeVlXMWxYMlJwWnlBdE1Rb2dJQ0FnWm5KaGJXVmZaR2xuSUMwekNpQWdJQ0JqYjI1allYUUtJQ0FnSUdaeVlXMWxYMlJwWnlBdE1nb2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZWEpqTWpBd0xtRnNaMjh1ZEhNNk1qY3pDaUFnSUNBdkx5QjBhR2x6TG1Gd2NISnZkbUZzY3loclpYa3BMblpoYkhWbElEMGdZMnh2Ym1Vb1lYQndjbTkyWVd4Q2IzZ3BDaUFnSUNCbWNtRnRaVjlrYVdjZ01Rb2dJQ0FnYzNkaGNBb2dJQ0FnWW05NFgzQjFkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qSTNOQW9nSUNBZ0x5OGdaVzFwZENodVpYY2dZWEpqTWpBd1gwRndjSEp2ZG1Gc0tIc2diM2R1WlhJNklHOTNibVZ5TENCemNHVnVaR1Z5T2lCemNHVnVaR1Z5TENCMllXeDFaVG9nWVcxdmRXNTBJSDBwS1FvZ0lDQWdabkpoYldWZlpHbG5JREFLSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGd4T1RZNVpqZzJOU0F2THlCdFpYUm9iMlFnSW1GeVl6SXdNRjlCY0hCeWIzWmhiQ2hoWkdSeVpYTnpMR0ZrWkhKbGMzTXNkV2x1ZERJMU5pa2lDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkZ5WXpJd01DNWhiR2R2TG5Sek9qSTNOUW9nSUNBZ0x5OGdjbVYwZFhKdUlHNWxkeUJDYjI5c0tIUnlkV1VwQ2lBZ0lDQndkWE5vWW5sMFpYTWdNSGc0TUFvZ0lDQWdabkpoYldWZlluVnllU0F3Q2lBZ0lDQnlaWFJ6ZFdJSyIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCQVlXeG5iM0poYm1SbWIzVnVaR0YwYVc5dUwyRnNaMjl5WVc1a0xYUjVjR1Z6WTNKcGNIUXZZbUZ6WlMxamIyNTBjbUZqZEM1a0xuUnpPanBDWVhObFEyOXVkSEpoWTNRdVkyeGxZWEpUZEdGMFpWQnliMmR5WVcwb0tTQXRQaUIxYVc1ME5qUTZDbTFoYVc0NkNpQWdJQ0J3ZFhOb2FXNTBJREVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZz09In0sImJ5dGVDb2RlIjp7ImFwcHJvdmFsIjoiQ3lBRUlBRUFBaVlHQkJVZmZIVUJZZ0YwQUFSNWc4TmNJQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQU1SdEJBR014R1JSRU1SaEVnZ3dFbDFPQzRnUmxmUlBzQkxhdUdpVUVoT3dUMVFUc21XQkJCSUxsYzhRRTJuQWx1UVJLbG8rUEJMVkNJU1VFa1BBbXpRUzR2dlBOQkx1ekdmTTJHZ0NPREFBTEFJMEFvUUMyQU1JQXpBRGRBUG9CVkFGeEFhQUIxQUF4R1JReEdCUVFSQ05ETmhvQlNTUlpKUWhMQVJVU1JEWWFBa2trV1NVSVN3RVZFa1EyR2dOSkZTTVNSRFlhQkVrVkloSkVNUUF5Q1JKRVN3TlhBZ0FWU1VRaURrUkxBbGNDQUJWSlJJRUlEa1FrS21WRkFSUkVnQUZ1VHdSbmdBRnpUd05uS2tzQlo0QUJaRThDWnpFQUtVc0JVRXNDdnpJRFRGQk1VQ2NFVEZDd2dBVVZIM3gxZ0xBalF5U0FBVzVsUkZjQ0FFa1ZJaEpFS0V4UXNDTkRKSUFCYzJWRVZ3SUFTUldCQ0JKRUtFeFFzQ05ESklBQlpHVkVLRXhRc0NOREpDcGxSQ2hNVUxBalF6WWFBVWtWSWhKRWlBRVdLRXhRc0NORE5ob0JTUlVpRWtRMkdnSkpGU0lTUkRFQVRnS0lBUkVvVEZDd0kwTTJHZ0ZIQWhVaUVrUTJHZ0pKVGdJVkloSkVOaG9EU1U0Q1NSVWlFa1F4QUVsT0EwOENUSWdCVGtsT0Frc0JwMFFycFVFQUZrbExBNkZKRlNJT1JDS3ZxMHNGU3dOUEFvZ0JUMGhMQkVzRVN3U0lBTGNvVEZDd0kwTTJHZ0ZKRlNJU1JEWWFBa2tWSWhKRU1RQk9Bb2dCS0NoTVVMQWpRellhQVVrVkloSkVOaG9DU1JVaUVrUXhBRWxMQTRnQTZFOENvRWtWSWc1RUlxK3JURTRDaUFENUtFeFFzQ05ETmhvQlNSVWlFa1EyR2dKSkZTSVNSREVBU1VzRGlBQzVTVXNEcDBSUEFxRkpGU0lPUkNLdnEweE9Bb2dBeFNoTVVMQWpRellhQVVrVkloSkVOaG9DU1JVaUVrU0lBSW9vVEZDd0kwT0tBUUVwaS85UVNiMUZBVUFBQkNjRlRJbUxBTDVFVEltS0F3RWtpLzJJLzk5SmkvNkkvOWxNaS8rblJJdjlpLzRUUVFBOWkvNHlBeE5FS1l2K1VFbU1BTDFGQVVBQUJZdi9LNlZFaXdHTC82RkpGU0lPUkNLdlRFc0JxeW1ML1ZCTXY0c0NpLytnU1JVaURrU3Jpd0JNdjR2OWkvNVFpLzlRSndSTVVMQ0FBWUNNQUltS0FnR0wvb3YvVUFHQUFXRk1VRW05UlFGQUFBUW5CVXlKaXdDK1JGY0FJRXlKaWdNQmkvMkwvbEJKQVlBQllVeFFTYjFGQVVBQUJZdi9LNlZFaS8rTC9WQ0wvbENMQVV5L2l3Q0wvMUNBQkJscCtHVk1VTENBQVlDTUFJaz0iLCJjbGVhciI6IkM0RUJRdz09In0sImNvbXBpbGVySW5mbyI6eyJjb21waWxlciI6InB1eWEiLCJjb21waWxlclZlcnNpb24iOnsibWFqb3IiOjUsIm1pbm9yIjozLCJwYXRjaCI6MiwiY29tbWl0SGFzaCI6bnVsbH19LCJldmVudHMiOlt7Im5hbWUiOiJhcmMyMDBfVHJhbnNmZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImZyb20iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoidG8iLCJkZXNjIjpudWxsfSx7InR5cGUiOiJ1aW50MjU2Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmFsdWUiLCJkZXNjIjpudWxsfV19LHsibmFtZSI6ImFyYzIwMF9BcHByb3ZhbCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoib3duZXIiLCJkZXNjIjpudWxsfSx7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3BlbmRlciIsImRlc2MiOm51bGx9LHsidHlwZSI6InVpbnQyNTYiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ2YWx1ZSIsImRlc2MiOm51bGx9XX1dLCJ0ZW1wbGF0ZVZhcmlhYmxlcyI6e30sInNjcmF0Y2hWYXJpYWJsZXMiOnt9fQ==";
    }

}
