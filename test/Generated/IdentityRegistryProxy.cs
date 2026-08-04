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

namespace IdentityRegistryRegression
{


    public class IdentityRegistryProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public IdentityRegistryProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class InstitutionMetadata : AVMObjectType
            {
                public string Name { get; set; }

                public string Did { get; set; }

                public byte[] PublicKey { get; set; }

                public bool IsActive { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.From(Name);
                    stringRef[ret.Count] = vName.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vDid.From(Did);
                    stringRef[ret.Count] = vDid.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPublicKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vPublicKey.From(PublicKey);
                    ret.AddRange(vPublicKey.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vIsActive = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vIsActive.From(IsActive);
                    ret.AddRange(vIsActive.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static InstitutionMetadata Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new InstitutionMetadata();
                    uint count = 0;
                    var indexName = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.Decode(bytes.Skip(indexName + prefixOffset).ToArray());
                    var valueName = vName.ToValue();
                    if (valueName is string vNameValue) { ret.Name = vNameValue; }
                    var indexDid = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vDid.Decode(bytes.Skip(indexDid + prefixOffset).ToArray());
                    var valueDid = vDid.ToValue();
                    if (valueDid is string vDidValue) { ret.Did = vDidValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPublicKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vPublicKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePublicKey = vPublicKey.ToValue();
                    if (valuePublicKey is byte[] vPublicKeyValue) { ret.PublicKey = vPublicKeyValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vIsActive = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vIsActive.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueIsActive = vIsActive.ToValue();
                    if (valueIsActive is bool vIsActiveValue) { ret.IsActive = vIsActiveValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as InstitutionMetadata);
                }
                public bool Equals(InstitutionMetadata? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(InstitutionMetadata left, InstitutionMetadata right)
                {
                    return EqualityComparer<InstitutionMetadata>.Default.Equals(left, right);
                }
                public static bool operator !=(InstitutionMetadata left, InstitutionMetadata right)
                {
                    return !(left == right);
                }

            }

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="admin"> </param>
        public async Task Create(Algorand.Address admin, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 204, 105, 78, 170 };
            var adminAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); adminAbi.From(admin);

            var result = await base.CallApp(new List<object> { abiHandle, adminAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Create_Transactions(Algorand.Address admin, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 204, 105, 78, 170 };
            var adminAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); adminAbi.From(admin);

            return await base.MakeTransactionList(new List<object> { abiHandle, adminAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="addr"> </param>
        /// <param name="name"> </param>
        /// <param name="did"> </param>
        /// <param name="public_key"> </param>
        public async Task RegisterInstitution(Algorand.Address addr, string name, string did, byte[] public_key, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 96, 177, 76, 161 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); nameAbi.From(name);
            var didAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); didAbi.From(did);
            var public_keyAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); public_keyAbi.From(public_key);

            var result = await base.CallApp(new List<object> { abiHandle, addrAbi, nameAbi, didAbi, public_keyAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> RegisterInstitution_Transactions(Algorand.Address addr, string name, string did, byte[] public_key, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 96, 177, 76, 161 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);
            var nameAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); nameAbi.From(name);
            var didAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); didAbi.From(did);
            var public_keyAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); public_keyAbi.From(public_key);

            return await base.MakeTransactionList(new List<object> { abiHandle, addrAbi, nameAbi, didAbi, public_keyAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="addr"> </param>
        public async Task DeactivateInstitution(Algorand.Address addr, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 9, 94, 154, 35 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);

            var result = await base.CallApp(new List<object> { abiHandle, addrAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> DeactivateInstitution_Transactions(Algorand.Address addr, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 9, 94, 154, 35 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);

            return await base.MakeTransactionList(new List<object> { abiHandle, addrAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="addr"> </param>
        public async Task<bool> IsRegistered(Algorand.Address addr, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 145, 156, 2, 223 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);

            var result = await base.SimApp(new List<object> { abiHandle, addrAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> IsRegistered_Transactions(Algorand.Address addr, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 145, 156, 2, 223 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);

            return await base.MakeTransactionList(new List<object> { abiHandle, addrAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="addr"> </param>
        public async Task<Structs.InstitutionMetadata> GetInstitution(Algorand.Address addr, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 249, 34, 47, 18 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);

            var result = await base.SimApp(new List<object> { abiHandle, addrAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.InstitutionMetadata.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> GetInstitution_Transactions(Algorand.Address addr, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 249, 34, 47, 18 };
            var addrAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); addrAbi.From(addr);

            return await base.MakeTransactionList(new List<object> { abiHandle, addrAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<ulong> GetInstitutionCount(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 105, 140, 60, 81 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> GetInstitutionCount_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 105, 140, 60, 81 };

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

        protected override ulong? ExtraProgramPages { get; set; } = 0;
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiSWRlbnRpdHlSZWdpc3RyeSIsImRlc2MiOm51bGwsIm5ldHdvcmtzIjp7fSwic3RydWN0cyI6eyJJbnN0aXR1dGlvbk1ldGFkYXRhIjpbeyJuYW1lIjoibmFtZSIsInR5cGUiOiJzdHJpbmcifSx7Im5hbWUiOiJkaWQiLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoicHVibGljX2tleSIsInR5cGUiOiJieXRlW10ifSx7Im5hbWUiOiJpc19hY3RpdmUiLCJ0eXBlIjoiYm9vbCJ9XX0sIk1ldGhvZHMiOlt7Im5hbWUiOiJjcmVhdGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImFkbWluIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6WyJOb09wIl0sImNhbGwiOltdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InJlZ2lzdGVyX2luc3RpdHV0aW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJuYW1lIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkaWQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InB1YmxpY19rZXkiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiZGVhY3RpdmF0ZV9pbnN0aXR1dGlvbiIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoiYWRkciIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJpc19yZWdpc3RlcmVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiZ2V0X2luc3RpdHV0aW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhZGRyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihzdHJpbmcsc3RyaW5nLGJ5dGVbXSxib29sKSIsInN0cnVjdCI6Ikluc3RpdHV0aW9uTWV0YWRhdGEiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImdldF9pbnN0aXR1dGlvbl9jb3VudCIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX1dLCJzdGF0ZSI6eyJzY2hlbWEiOnsiZ2xvYmFsIjp7ImludHMiOjEsImJ5dGVzIjoxfSwibG9jYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjB9fSwia2V5cyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn19LCJtYXBzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfX19LCJiYXJlQWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbXX0sInNvdXJjZUluZm8iOnsiYXBwcm92YWwiOnsic291cmNlSW5mbyI6W3sicGMiOlsyODIsNDM4XSwiZXJyb3JNZXNzYWdlIjoiSW5zdGl0dXRpb24gbm90IHJlZ2lzdGVyZWQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyNzRdLCJlcnJvck1lc3NhZ2UiOiJPbmx5IGFkbWluIGNhbiBkZWFjdGl2YXRlIGluc3RpdHV0aW9ucyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIwM10sImVycm9yTWVzc2FnZSI6Ik9ubHkgYWRtaW4gY2FuIHJlZ2lzdGVyIGluc3RpdHV0aW9ucyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIwMSwyNzJdLCJlcnJvck1lc3NhZ2UiOiJjaGVjayBzZWxmLmFkbWluIGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI1MSw0NTFdLCJlcnJvck1lc3NhZ2UiOiJjaGVjayBzZWxmLmluc3RpdHV0aW9uX2NvdW50IGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQxMl0sImVycm9yTWVzc2FnZSI6ImluZGV4IG91dCBvZiBib3VuZHMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNTYsMTcyLDE4OF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgYXJyYXkgbGVuZ3RoIGhlYWRlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE2NiwxODIsMTk1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuZHluYW1pY19hcnJheTxhcmM0LnVpbnQ4PiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzEzNCwxNTAsMjY2LDM4OSw0MzBdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5zdGF0aWNfYXJyYXk8YXJjNC51aW50OCwgMzI+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfV0sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9LCJjbGVhciI6eyJzb3VyY2VJbmZvIjpbXSwicGNPZmZzZXRNZXRob2QiOiJub25lIn19LCJzb3VyY2UiOnsiYXBwcm92YWwiOiJJM0J5WVdkdFlTQjJaWEp6YVc5dUlERXhDaU53Y21GbmJXRWdkSGx3WlhSeVlXTnJJR1poYkhObENnb3ZMeUJoYkdkdmNIa3VZWEpqTkM1QlVrTTBRMjl1ZEhKaFkzUXVZWEJ3Y205MllXeGZjSEp2WjNKaGJTZ3BJQzArSUhWcGJuUTJORG9LYldGcGJqb0tJQ0FnSUdsdWRHTmliRzlqYXlBd0lESWdNU0F6TWdvZ0lDQWdZbmwwWldOaWJHOWpheUFpYVc1emRHbDBkWFJwYjI1ZlkyOTFiblFpSUNKaFpHMXBiaUlnSW1sdWMzUmZJaUF3ZURBd0lEQjRNVFV4Wmpkak56VWdNSGd3TURBM0NpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1ltNTZJRzFoYVc1ZllXWjBaWEpmYVdaZlpXeHpaVUF5Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZhV1JsYm5ScGRIbGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TWpBS0lDQWdJQzh2SUhObGJHWXVZV1J0YVc0Z1BTQkJZMk52ZFc1MEtDa0tJQ0FnSUdKNWRHVmpYekVnTHk4Z0ltRmtiV2x1SWdvZ0lDQWdaMnh2WW1Gc0lGcGxjbTlCWkdSeVpYTnpDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPakl4Q2lBZ0lDQXZMeUJ6Wld4bUxtbHVjM1JwZEhWMGFXOXVYMk52ZFc1MElEMGdWVWx1ZERZMEtEQXBDaUFnSUNCaWVYUmxZMTh3SUM4dklDSnBibk4wYVhSMWRHbHZibDlqYjNWdWRDSUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb0tiV0ZwYmw5aFpuUmxjbDlwWmw5bGJITmxRREk2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZhV1JsYm5ScGRIbGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TVRnS0lDQWdJQzh2SUdOc1lYTnpJRWxrWlc1MGFYUjVVbVZuYVhOMGNua29RVkpETkVOdmJuUnlZV04wS1RvS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJR0Z6YzJWeWRBb2dJQ0FnZEhodUlFRndjR3hwWTJGMGFXOXVTVVFLSUNBZ0lHSjZJRzFoYVc1ZlkzSmxZWFJsWDA1dlQzQkFNVEVLSUNBZ0lIQjFjMmhpZVhSbGMzTWdNSGcyTUdJeE5HTmhNU0F3ZURBNU5XVTVZVEl6SURCNE9URTVZekF5WkdZZ01IaG1PVEl5TW1ZeE1pQXdlRFk1T0dNell6VXhJQzh2SUcxbGRHaHZaQ0FpY21WbmFYTjBaWEpmYVc1emRHbDBkWFJwYjI0b1lXUmtjbVZ6Y3l4emRISnBibWNzYzNSeWFXNW5MR0o1ZEdWYlhTbDJiMmxrSWl3Z2JXVjBhRzlrSUNKa1pXRmpkR2wyWVhSbFgybHVjM1JwZEhWMGFXOXVLR0ZrWkhKbGMzTXBkbTlwWkNJc0lHMWxkR2h2WkNBaWFYTmZjbVZuYVhOMFpYSmxaQ2hoWkdSeVpYTnpLV0p2YjJ3aUxDQnRaWFJvYjJRZ0ltZGxkRjlwYm5OMGFYUjFkR2x2YmloaFpHUnlaWE56S1NoemRISnBibWNzYzNSeWFXNW5MR0o1ZEdWYlhTeGliMjlzS1NJc0lHMWxkR2h2WkNBaVoyVjBYMmx1YzNScGRIVjBhVzl1WDJOdmRXNTBLQ2wxYVc1ME5qUWlDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXdDaUFnSUNCdFlYUmphQ0J5WldkcGMzUmxjbDlwYm5OMGFYUjFkR2x2YmlCa1pXRmpkR2wyWVhSbFgybHVjM1JwZEhWMGFXOXVJR2x6WDNKbFoybHpkR1Z5WldRZ1oyVjBYMmx1YzNScGRIVjBhVzl1SUdkbGRGOXBibk4wYVhSMWRHbHZibDlqYjNWdWRBb2dJQ0FnWlhKeUNncHRZV2x1WDJOeVpXRjBaVjlPYjA5d1FERXhPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPakU0Q2lBZ0lDQXZMeUJqYkdGemN5QkpaR1Z1ZEdsMGVWSmxaMmx6ZEhKNUtFRlNRelJEYjI1MGNtRmpkQ2s2Q2lBZ0lDQndkWE5vWW5sMFpYTWdNSGhqWXpZNU5HVmhZU0F2THlCdFpYUm9iMlFnSW1OeVpXRjBaU2hoWkdSeVpYTnpLWFp2YVdRaUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBd0NpQWdJQ0J0WVhSamFDQmpjbVZoZEdVS0lDQWdJR1Z5Y2dvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5NXBaR1Z1ZEdsMGVWOXlaV2RwYzNSeWVTNWpiMjUwY21GamRDNUpaR1Z1ZEdsMGVWSmxaMmx6ZEhKNUxtTnlaV0YwWlZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtTnlaV0YwWlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTlwWkdWdWRHbDBlVjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRveU5Bb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0dOeVpXRjBaVDBpY21WeGRXbHlaU0lwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh6SUM4dklETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbk4wWVhScFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNExDQXpNajRLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXBaR1Z1ZEdsMGVWOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG95TmdvZ0lDQWdMeThnYzJWc1ppNWhaRzFwYmlBOUlHRmtiV2x1Q2lBZ0lDQmllWFJsWTE4eElDOHZJQ0poWkcxcGJpSUtJQ0FnSUhOM1lYQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmFXUmxiblJwZEhsZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZNamNLSUNBZ0lDOHZJSE5sYkdZdWFXNXpkR2wwZFhScGIyNWZZMjkxYm5RZ1BTQlZTVzUwTmpRb01Da0tJQ0FnSUdKNWRHVmpYekFnTHk4Z0ltbHVjM1JwZEhWMGFXOXVYMk52ZFc1MElnb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmY0hWMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YVdSbGJuUnBkSGxmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk1qUUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNoamNtVmhkR1U5SW5KbGNYVnBjbVVpS1FvZ0lDQWdhVzUwWTE4eUlDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5NXBaR1Z1ZEdsMGVWOXlaV2RwYzNSeWVTNWpiMjUwY21GamRDNUpaR1Z1ZEdsMGVWSmxaMmx6ZEhKNUxuSmxaMmx6ZEdWeVgybHVjM1JwZEhWMGFXOXVXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LY21WbmFYTjBaWEpmYVc1emRHbDBkWFJwYjI0NkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YVdSbGJuUnBkSGxmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk1qa0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkFvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTXlBdkx5QXpNZ29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ1lYSmpOQzV6ZEdGMGFXTmZZWEp5WVhrOFlYSmpOQzUxYVc1ME9Dd2dNekkrQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF5Q2lBZ0lDQmtkWEFLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCbGVIUnlZV04wWDNWcGJuUXhOaUF2THlCdmJpQmxjbkp2Y2pvZ2FXNTJZV3hwWkNCaGNuSmhlU0JzWlc1bmRHZ2dhR1ZoWkdWeUNpQWdJQ0JwYm5Salh6RWdMeThnTWdvZ0lDQWdLd29nSUNBZ1pHbG5JREVLSUNBZ0lHeGxiZ29nSUNBZ1pIVndDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTXdvZ0lDQWdaSFZ3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnYVc1MFkxOHhJQzh2SURJS0lDQWdJQ3NLSUNBZ0lHUnBaeUF4Q2lBZ0lDQnNaVzRLSUNBZ0lHUjFjQW9nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExtUjVibUZ0YVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0Q0S0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURRS0lDQWdJR1IxY0FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQmthV2NnTVFvZ0lDQWdiR1Z1Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG1SNWJtRnRhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPRDRLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXBaR1Z1ZEdsMGVWOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG96TndvZ0lDQWdMeThnWVhOelpYSjBJRlI0Ymk1elpXNWtaWElnUFQwZ2MyVnNaaTVoWkcxcGJpd2dJazl1YkhrZ1lXUnRhVzRnWTJGdUlISmxaMmx6ZEdWeUlHbHVjM1JwZEhWMGFXOXVjeUlLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFkxOHhJQzh2SUNKaFpHMXBiaUlLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2djMlZzWmk1aFpHMXBiaUJsZUdsemRITUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnVDI1c2VTQmhaRzFwYmlCallXNGdjbVZuYVhOMFpYSWdhVzV6ZEdsMGRYUnBiMjV6Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZhV1JsYm5ScGRIbGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TXprdE5EUUtJQ0FnSUM4dklHMWxkR0ZrWVhSaElEMGdTVzV6ZEdsMGRYUnBiMjVOWlhSaFpHRjBZU2dLSUNBZ0lDOHZJQ0FnSUNCdVlXMWxQVzVoYldVc0NpQWdJQ0F2THlBZ0lDQWdaR2xrUFdScFpDd0tJQ0FnSUM4dklDQWdJQ0J3ZFdKc2FXTmZhMlY1UFhCMVlteHBZMTlyWlhrdVkyOXdlU2dwTEFvZ0lDQWdMeThnSUNBZ0lHbHpYMkZqZEdsMlpUMWhjbU0wTGtKdmIyd29WSEoxWlNrc0NpQWdJQ0F2THlBcENpQWdJQ0J3ZFhOb2FXNTBJRGNLSUNBZ0lIVnVZMjkyWlhJZ05Bb2dJQ0FnS3dvZ0lDQWdaSFZ3Q2lBZ0lDQnBkRzlpQ2lBZ0lDQmxlSFJ5WVdOMElEWWdNZ29nSUNBZ1lubDBaV01nTlNBdkx5QXdlREF3TURjS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYzNkaGNBb2dJQ0FnZFc1amIzWmxjaUF6Q2lBZ0lDQXJDaUFnSUNCcGRHOWlDaUFnSUNCbGVIUnlZV04wSURZZ01nb2dJQ0FnWTI5dVkyRjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmFXUmxiblJwZEhsZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZORE1LSUNBZ0lDOHZJR2x6WDJGamRHbDJaVDFoY21NMExrSnZiMndvVkhKMVpTa3NDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZzRNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPak01TFRRMENpQWdJQ0F2THlCdFpYUmhaR0YwWVNBOUlFbHVjM1JwZEhWMGFXOXVUV1YwWVdSaGRHRW9DaUFnSUNBdkx5QWdJQ0FnYm1GdFpUMXVZVzFsTEFvZ0lDQWdMeThnSUNBZ0lHUnBaRDFrYVdRc0NpQWdJQ0F2THlBZ0lDQWdjSFZpYkdsalgydGxlVDF3ZFdKc2FXTmZhMlY1TG1OdmNIa29LU3dLSUNBZ0lDOHZJQ0FnSUNCcGMxOWhZM1JwZG1VOVlYSmpOQzVDYjI5c0tGUnlkV1VwTEFvZ0lDQWdMeThnS1FvZ0lDQWdZMjl1WTJGMENpQWdJQ0IxYm1OdmRtVnlJRE1LSUNBZ0lHTnZibU5oZEFvZ0lDQWdkVzVqYjNabGNpQXlDaUFnSUNCamIyNWpZWFFLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMmxrWlc1MGFYUjVYM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pRMUNpQWdJQ0F2THlCelpXeG1MbWx1YzNScGRIVjBhVzl1YzF0aFpHUnlYU0E5SUcxbGRHRmtZWFJoTG1OdmNIa29LUW9nSUNBZ1lubDBaV05mTWlBdkx5QWlhVzV6ZEY4aUNpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmWkdWc0NpQWdJQ0J3YjNBS0lDQWdJSE4zWVhBS0lDQWdJR0p2ZUY5d2RYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5cFpHVnVkR2wwZVY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUbzBOZ29nSUNBZ0x5OGdjMlZzWmk1cGJuTjBhWFIxZEdsdmJsOWpiM1Z1ZENBclBTQlZTVzUwTmpRb01Ta0tJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFkxOHdJQzh2SUNKcGJuTjBhWFIxZEdsdmJsOWpiM1Z1ZENJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnYzJWc1ppNXBibk4wYVhSMWRHbHZibDlqYjNWdWRDQmxlR2x6ZEhNS0lDQWdJR2x1ZEdOZk1pQXZMeUF4Q2lBZ0lDQXJDaUFnSUNCaWVYUmxZMTh3SUM4dklDSnBibk4wYVhSMWRHbHZibDlqYjNWdWRDSUtJQ0FnSUhOM1lYQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmFXUmxiblJwZEhsZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZNamtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpBb2dJQ0FnYVc1MFkxOHlJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTVwWkdWdWRHbDBlVjl5WldkcGMzUnllUzVqYjI1MGNtRmpkQzVKWkdWdWRHbDBlVkpsWjJsemRISjVMbVJsWVdOMGFYWmhkR1ZmYVc1emRHbDBkWFJwYjI1YmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncGtaV0ZqZEdsMllYUmxYMmx1YzNScGRIVjBhVzl1T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMmxrWlc1MGFYUjVYM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pRNENpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYek1nTHk4Z016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVjM1JoZEdsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnc0lETXlQZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalV3Q2lBZ0lDQXZMeUJoYzNObGNuUWdWSGh1TG5ObGJtUmxjaUE5UFNCelpXeG1MbUZrYldsdUxDQWlUMjVzZVNCaFpHMXBiaUJqWVc0Z1pHVmhZM1JwZG1GMFpTQnBibk4wYVhSMWRHbHZibk1pQ2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTmZNU0F2THlBaVlXUnRhVzRpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJSE5sYkdZdVlXUnRhVzRnWlhocGMzUnpDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUU5dWJIa2dZV1J0YVc0Z1kyRnVJR1JsWVdOMGFYWmhkR1VnYVc1emRHbDBkWFJwYjI1ekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YVdSbGJuUnBkSGxmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk5URUtJQ0FnSUM4dklHRnpjMlZ5ZENCaFpHUnlJR2x1SUhObGJHWXVhVzV6ZEdsMGRYUnBiMjV6TENBaVNXNXpkR2wwZFhScGIyNGdibTkwSUhKbFoybHpkR1Z5WldRaUNpQWdJQ0JpZVhSbFkxOHlJQzh2SUNKcGJuTjBYeUlLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmYkdWdUNpQWdJQ0JpZFhKNUlERUtJQ0FnSUdGemMyVnlkQ0F2THlCSmJuTjBhWFIxZEdsdmJpQnViM1FnY21WbmFYTjBaWEpsWkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMmxrWlc1MGFYUjVYM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pVMUNpQWdJQ0F2THlCdVlXMWxQVzFsZEdGa1lYUmhMbTVoYldVc0NpQWdJQ0JrZFhBS0lDQWdJSEIxYzJocGJuUWdOd29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUdKdmVGOWxlSFJ5WVdOMENpQWdJQ0JpZEc5cENpQWdJQ0JwYm5Salh6RWdMeThnTWdvZ0lDQWdLd29nSUNBZ1pHbG5JREVLSUNBZ0lIQjFjMmhwYm5RZ053b2dJQ0FnZFc1amIzWmxjaUF5Q2lBZ0lDQmliM2hmWlhoMGNtRmpkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalUyQ2lBZ0lDQXZMeUJrYVdROWJXVjBZV1JoZEdFdVpHbGtMQW9nSUNBZ1pHbG5JREVLSUNBZ0lHbHVkR05mTVNBdkx5QXlDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOWxlSFJ5WVdOMENpQWdJQ0JpZEc5cENpQWdJQ0JrYVdjZ01nb2dJQ0FnWkdsbklERUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0JpYjNoZlpYaDBjbUZqZEFvZ0lDQWdZblJ2YVFvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QXpDaUFnSUNCamIzWmxjaUF5Q2lBZ0lDQmliM2hmWlhoMGNtRmpkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalUzQ2lBZ0lDQXZMeUJ3ZFdKc2FXTmZhMlY1UFcxbGRHRmtZWFJoTG5CMVlteHBZMTlyWlhrdVkyOXdlU2dwTEFvZ0lDQWdaR2xuSURJS0lDQWdJSEIxYzJocGJuUWdOQW9nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUdKdmVGOWxlSFJ5WVdOMENpQWdJQ0JpZEc5cENpQWdJQ0JrYVdjZ013b2dJQ0FnWkdsbklERUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0JpYjNoZlpYaDBjbUZqZEFvZ0lDQWdZblJ2YVFvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QTBDaUFnSUNCamIzWmxjaUF5Q2lBZ0lDQmliM2hmWlhoMGNtRmpkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalUwTFRVNUNpQWdJQ0F2THlCelpXeG1MbWx1YzNScGRIVjBhVzl1YzF0aFpHUnlYU0E5SUVsdWMzUnBkSFYwYVc5dVRXVjBZV1JoZEdFb0NpQWdJQ0F2THlBZ0lDQWdibUZ0WlQxdFpYUmhaR0YwWVM1dVlXMWxMQW9nSUNBZ0x5OGdJQ0FnSUdScFpEMXRaWFJoWkdGMFlTNWthV1FzQ2lBZ0lDQXZMeUFnSUNBZ2NIVmliR2xqWDJ0bGVUMXRaWFJoWkdGMFlTNXdkV0pzYVdOZmEyVjVMbU52Y0hrb0tTd0tJQ0FnSUM4dklDQWdJQ0JwYzE5aFkzUnBkbVU5WVhKak5DNUNiMjlzS0VaaGJITmxLU3dLSUNBZ0lDOHZJQ2t1WTI5d2VTZ3BDaUFnSUNCa2FXY2dNZ29nSUNBZ2JHVnVDaUFnSUNCd2RYTm9hVzUwSURjS0lDQWdJQ3NLSUNBZ0lHUjFjQW9nSUNBZ2FYUnZZZ29nSUNBZ1pYaDBjbUZqZENBMklESUtJQ0FnSUdKNWRHVmpJRFVnTHk4Z01IZ3dNREEzQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR1JwWnlBekNpQWdJQ0JzWlc0S0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ0t3b2dJQ0FnYVhSdllnb2dJQ0FnWlhoMGNtRmpkQ0EySURJS0lDQWdJR052Ym1OaGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJsa1pXNTBhWFI1WDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qVTRDaUFnSUNBdkx5QnBjMTloWTNScGRtVTlZWEpqTkM1Q2IyOXNLRVpoYkhObEtTd0tJQ0FnSUdKNWRHVmpYek1nTHk4Z01IZ3dNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalUwTFRVNUNpQWdJQ0F2THlCelpXeG1MbWx1YzNScGRIVjBhVzl1YzF0aFpHUnlYU0E5SUVsdWMzUnBkSFYwYVc5dVRXVjBZV1JoZEdFb0NpQWdJQ0F2THlBZ0lDQWdibUZ0WlQxdFpYUmhaR0YwWVM1dVlXMWxMQW9nSUNBZ0x5OGdJQ0FnSUdScFpEMXRaWFJoWkdGMFlTNWthV1FzQ2lBZ0lDQXZMeUFnSUNBZ2NIVmliR2xqWDJ0bGVUMXRaWFJoWkdGMFlTNXdkV0pzYVdOZmEyVjVMbU52Y0hrb0tTd0tJQ0FnSUM4dklDQWdJQ0JwYzE5aFkzUnBkbVU5WVhKak5DNUNiMjlzS0VaaGJITmxLU3dLSUNBZ0lDOHZJQ2t1WTI5d2VTZ3BDaUFnSUNCamIyNWpZWFFLSUNBZ0lIVnVZMjkyWlhJZ013b2dJQ0FnWTI5dVkyRjBDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJR052Ym1OaGRBb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCa2FXY2dNUW9nSUNBZ1ltOTRYMlJsYkFvZ0lDQWdjRzl3Q2lBZ0lDQmliM2hmY0hWMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YVdSbGJuUnBkSGxmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk5EZ0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkFvZ0lDQWdhVzUwWTE4eUlDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5NXBaR1Z1ZEdsMGVWOXlaV2RwYzNSeWVTNWpiMjUwY21GamRDNUpaR1Z1ZEdsMGVWSmxaMmx6ZEhKNUxtbHpYM0psWjJsemRHVnlaV1JiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwcGMxOXlaV2RwYzNSbGNtVmtPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPall4Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9jbVZoWkc5dWJIazlWSEoxWlNrS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpNZ0x5OGdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1YzNSaGRHbGpYMkZ5Y21GNVBHRnlZelF1ZFdsdWREZ3NJRE15UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMmxrWlc1MGFYUjVYM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pZekNpQWdJQ0F2THlCcFppQmhaR1J5SUc1dmRDQnBiaUJ6Wld4bUxtbHVjM1JwZEhWMGFXOXVjem9LSUNBZ0lHSjVkR1ZqWHpJZ0x5OGdJbWx1YzNSZklnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCa2RYQUtJQ0FnSUdKdmVGOXNaVzRLSUNBZ0lHSjFjbmtnTVFvZ0lDQWdZbTU2SUdselgzSmxaMmx6ZEdWeVpXUmZZV1owWlhKZmFXWmZaV3h6WlVBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YVdSbGJuUnBkSGxmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk5qUUtJQ0FnSUM4dklISmxkSFZ5YmlCaGNtTTBMa0p2YjJ3b1JtRnNjMlVwQ2lBZ0lDQmllWFJsWTE4eklDOHZJREI0TURBS0NtbHpYM0psWjJsemRHVnlaV1JmWVdaMFpYSmZhVzVzYVc1bFpGOXpiV0Z5ZEY5amIyNTBjbUZqZEhNdWFXUmxiblJwZEhsZmNtVm5hWE4wY25rdVkyOXVkSEpoWTNRdVNXUmxiblJwZEhsU1pXZHBjM1J5ZVM1cGMxOXlaV2RwYzNSbGNtVmtRRFE2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZhV1JsYm5ScGRIbGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TmpFS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2h5WldGa2IyNXNlVDFVY25WbEtRb2dJQ0FnWW5sMFpXTWdOQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHlJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0thWE5mY21WbmFYTjBaWEpsWkY5aFpuUmxjbDlwWmw5bGJITmxRRE02Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZhV1JsYm5ScGRIbGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TmpVS0lDQWdJQzh2SUhKbGRIVnliaUJ6Wld4bUxtbHVjM1JwZEhWMGFXOXVjMXRoWkdSeVhTNXBjMTloWTNScGRtVUtJQ0FnSUdSMWNBb2dJQ0FnY0hWemFHbHVkQ0EyQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNUW9nSUNBZ1ltOTRYMlY0ZEhKaFkzUWdMeThnYjI0Z1pYSnliM0k2SUdsdVpHVjRJRzkxZENCdlppQmliM1Z1WkhNS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQm5aWFJpYVhRS0lDQWdJR0o1ZEdWalh6TWdMeThnTUhnd01Bb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ2MyVjBZbWwwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZhV1JsYm5ScGRIbGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TmpFS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2h5WldGa2IyNXNlVDFVY25WbEtRb2dJQ0FnWWlCcGMxOXlaV2RwYzNSbGNtVmtYMkZtZEdWeVgybHViR2x1WldSZmMyMWhjblJmWTI5dWRISmhZM1J6TG1sa1pXNTBhWFI1WDNKbFoybHpkSEo1TG1OdmJuUnlZV04wTGtsa1pXNTBhWFI1VW1WbmFYTjBjbmt1YVhOZmNtVm5hWE4wWlhKbFpFQTBDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TG1sa1pXNTBhWFI1WDNKbFoybHpkSEo1TG1OdmJuUnlZV04wTGtsa1pXNTBhWFI1VW1WbmFYTjBjbmt1WjJWMFgybHVjM1JwZEhWMGFXOXVXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWjJWMFgybHVjM1JwZEhWMGFXOXVPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwybGtaVzUwYVhSNVgzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalkzQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9jbVZoWkc5dWJIazlWSEoxWlNrS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQnBiblJqWHpNZ0x5OGdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1YzNSaGRHbGpYMkZ5Y21GNVBHRnlZelF1ZFdsdWREZ3NJRE15UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMmxrWlc1MGFYUjVYM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pZNUNpQWdJQ0F2THlCaGMzTmxjblFnWVdSa2NpQnBiaUJ6Wld4bUxtbHVjM1JwZEhWMGFXOXVjeXdnSWtsdWMzUnBkSFYwYVc5dUlHNXZkQ0J5WldkcGMzUmxjbVZrSWdvZ0lDQWdZbmwwWldOZk1pQXZMeUFpYVc1emRGOGlDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHUjFjQW9nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JoYzNObGNuUWdMeThnU1c1emRHbDBkWFJwYjI0Z2JtOTBJSEpsWjJsemRHVnlaV1FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OXBaR1Z1ZEdsMGVWOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG8zTUFvZ0lDQWdMeThnY21WMGRYSnVJSE5sYkdZdWFXNXpkR2wwZFhScGIyNXpXMkZrWkhKZExtTnZjSGtvS1FvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnY0c5d0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12YVdSbGJuUnBkSGxmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk5qY0tJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkNoeVpXRmtiMjVzZVQxVWNuVmxLUW9nSUNBZ1lubDBaV01nTkNBdkx5QXdlREUxTVdZM1l6YzFDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh5SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k1cFpHVnVkR2wwZVY5eVpXZHBjM1J5ZVM1amIyNTBjbUZqZEM1SlpHVnVkR2wwZVZKbFoybHpkSEo1TG1kbGRGOXBibk4wYVhSMWRHbHZibDlqYjNWdWRGdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbWRsZEY5cGJuTjBhWFIxZEdsdmJsOWpiM1Z1ZERvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTlwWkdWdWRHbDBlVjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRvM05Bb2dJQ0FnTHk4Z2NtVjBkWEp1SUdGeVl6UXVWVWx1ZERZMEtITmxiR1l1YVc1emRHbDBkWFJwYjI1ZlkyOTFiblFwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV05mTUNBdkx5QWlhVzV6ZEdsMGRYUnBiMjVmWTI5MWJuUWlDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklITmxiR1l1YVc1emRHbDBkWFJwYjI1ZlkyOTFiblFnWlhocGMzUnpDaUFnSUNCcGRHOWlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmFXUmxiblJwZEhsZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZOeklLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaHlaV0ZrYjI1c2VUMVVjblZsS1FvZ0lDQWdZbmwwWldNZ05DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eUlDOHZJREVLSUNBZ0lISmxkSFZ5YmdvPSIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCaGJHZHZjSGt1WVhKak5DNUJVa00wUTI5dWRISmhZM1F1WTJ4bFlYSmZjM1JoZEdWZmNISnZaM0poYlNncElDMCtJSFZwYm5RMk5Eb0tiV0ZwYmpvS0lDQWdJSEIxYzJocGJuUWdNUW9nSUNBZ2NtVjBkWEp1Q2c9PSJ9LCJieXRlQ29kZSI6eyJhcHByb3ZhbCI6IkN5QUVBQUlCSUNZR0VXbHVjM1JwZEhWMGFXOXVYMk52ZFc1MEJXRmtiV2x1QldsdWMzUmZBUUFFRlI5OGRRSUFCekVZUUFBSEtUSURaeWdpWnpFWkZFUXhHRUVBSzRJRkJHQ3hUS0VFQ1Y2YUl3U1JuQUxmQlBraUx4SUVhWXc4VVRZYUFJNEZBQjhBa3dFT0FUY0JVQUNBQk14cFRxbzJHZ0NPQVFBQkFEWWFBVWtWSlJKRUtVeG5LQ0puSkVNMkdnRkpGU1VTUkRZYUFra2lXU01JU3dFVlNVOENFa1EyR2dOSklsa2pDRXNCRlVsUEFoSkVOaG9FU1NKWkl3aExBUlVTUkRFQUlpbGxSQkpFZ1FkUEJBaEpGbGNHQWljRlRGQk1Ud01JRmxjR0FsQ0FBWUJRVHdOUVR3SlFURkFxVHdKUVNieElUTDhpS0dWRUpBZ29UR2NrUXpZYUFVa1ZKUkpFTVFBaUtXVkVFa1FxVEZCSnZVVUJSRW1CQnlPNkZ5TUlTd0dCQjA4Q3Vrc0JJMG02RjBzQ1N3RWp1aGNqQ0VzRFRnSzZTd0tCQkNPNkYwc0RTd0VqdWhjakNFc0VUZ0s2U3dJVmdRY0lTUlpYQmdJbkJVeFFTd01WVHdJSUZsY0dBbEFyVUU4RFVFOENVRXhRU3dHOFNMOGtRellhQVVrVkpSSkVLa3hRU2IxRkFVQUFDQ3NuQkV4UXNDUkRTWUVHSkxvaVV5c2lUd0pVUXYvcU5ob0JTUlVsRWtRcVRGQkp2VVVCUkw1SUp3Uk1VTEFrUXlJb1pVUVdKd1JNVUxBa1F3PT0iLCJjbGVhciI6IkM0RUJRdz09In0sImNvbXBpbGVySW5mbyI6eyJjb21waWxlciI6InB1eWEiLCJjb21waWxlclZlcnNpb24iOnsibWFqb3IiOjUsIm1pbm9yIjo4LCJwYXRjaCI6MCwiY29tbWl0SGFzaCI6bnVsbH19LCJldmVudHMiOltdLCJ0ZW1wbGF0ZVZhcmlhYmxlcyI6e30sInNjcmF0Y2hWYXJpYWJsZXMiOnt9fQ==";
    }

}
