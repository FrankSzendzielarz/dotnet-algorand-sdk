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

namespace TruthRegistryRegression
{


    public class TruthRegistryProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public TruthRegistryProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class ZkProofStatus : AVMObjectType
            {
                public byte[] ProofId { get; set; }

                public ulong Threshold { get; set; }

                public byte[] ProofHash { get; set; }

                public bool IsVerified { get; set; }

                public ulong SubmittedAtRound { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vProofId = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vProofId.From(ProofId);
                    ret.AddRange(vProofId.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vThreshold = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vThreshold.From(Threshold);
                    ret.AddRange(vThreshold.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vProofHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8[32]");
                    vProofHash.From(ProofHash);
                    ret.AddRange(vProofHash.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vIsVerified = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vIsVerified.From(IsVerified);
                    ret.AddRange(vIsVerified.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSubmittedAtRound = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vSubmittedAtRound.From(SubmittedAtRound);
                    ret.AddRange(vSubmittedAtRound.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ZkProofStatus Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new ZkProofStatus();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vProofId = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vProofId.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueProofId = vProofId.ToValue();
                    if (valueProofId is byte[] vProofIdValue) { ret.ProofId = vProofIdValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vThreshold = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vThreshold.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueThreshold = vThreshold.ToValue();
                    if (valueThreshold is ulong vThresholdValue) { ret.Threshold = vThresholdValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vProofHash = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8[32]");
                    count = vProofHash.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueProofHash = vProofHash.ToValue();
                    if (valueProofHash is byte[] vProofHashValue) { ret.ProofHash = vProofHashValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vIsVerified = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vIsVerified.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueIsVerified = vIsVerified.ToValue();
                    if (valueIsVerified is bool vIsVerifiedValue) { ret.IsVerified = vIsVerifiedValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSubmittedAtRound = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vSubmittedAtRound.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueSubmittedAtRound = vSubmittedAtRound.ToValue();
                    if (valueSubmittedAtRound is ulong vSubmittedAtRoundValue) { ret.SubmittedAtRound = vSubmittedAtRoundValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ZkProofStatus);
                }
                public bool Equals(ZkProofStatus? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ZkProofStatus left, ZkProofStatus right)
                {
                    return EqualityComparer<ZkProofStatus>.Default.Equals(left, right);
                }
                public static bool operator !=(ZkProofStatus left, ZkProofStatus right)
                {
                    return !(left == right);
                }

            }

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="identity_registry_app_id"> </param>
        public async Task Create(ulong identity_registry_app_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 36, 13, 47, 103 };
            var identity_registry_app_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); identity_registry_app_idAbi.From(identity_registry_app_id);

            var result = await base.CallApp(new List<object> { abiHandle, identity_registry_app_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> Create_Transactions(ulong identity_registry_app_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 36, 13, 47, 103 };
            var identity_registry_app_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); identity_registry_app_idAbi.From(identity_registry_app_id);

            return await base.MakeTransactionList(new List<object> { abiHandle, identity_registry_app_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="verifier_app_id"> </param>
        public async Task SetVerifier(ulong verifier_app_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 214, 93, 177, 147 };
            var verifier_app_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); verifier_app_idAbi.From(verifier_app_id);

            var result = await base.CallApp(new List<object> { abiHandle, verifier_app_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> SetVerifier_Transactions(ulong verifier_app_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 214, 93, 177, 147 };
            var verifier_app_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); verifier_app_idAbi.From(verifier_app_id);

            return await base.MakeTransactionList(new List<object> { abiHandle, verifier_app_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="trait_id"> </param>
        /// <param name="commitment"> </param>
        public async Task RegisterAnchor(byte[] trait_id, byte[] commitment, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 36, 22, 142, 163 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);
            var commitmentAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); commitmentAbi.From(commitment);

            var result = await base.CallApp(new List<object> { abiHandle, trait_idAbi, commitmentAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> RegisterAnchor_Transactions(byte[] trait_id, byte[] commitment, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 36, 22, 142, 163 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);
            var commitmentAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); commitmentAbi.From(commitment);

            return await base.MakeTransactionList(new List<object> { abiHandle, trait_idAbi, commitmentAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Verify and settle a ZK claim.
        ///The claim is verified off-chain by the institution and then anchored here.
        ///</summary>
        /// <param name="trait_id"> </param>
        /// <param name="proof_id"> </param>
        /// <param name="threshold"> </param>
        /// <param name="proof_hash"> </param>
        public async Task<bool> VerifyZkClaim(byte[] trait_id, byte[] proof_id, ulong threshold, byte[] proof_hash, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 209, 94, 52, 145 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);
            var proof_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_idAbi.From(proof_id);
            var thresholdAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); thresholdAbi.From(threshold);
            var proof_hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(32, "byte"); proof_hashAbi.From(proof_hash);

            var result = await base.CallApp(new List<object> { abiHandle, trait_idAbi, proof_idAbi, thresholdAbi, proof_hashAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> VerifyZkClaim_Transactions(byte[] trait_id, byte[] proof_id, ulong threshold, byte[] proof_hash, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 209, 94, 52, 145 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);
            var proof_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_idAbi.From(proof_id);
            var thresholdAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); thresholdAbi.From(threshold);
            var proof_hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>(32, "byte"); proof_hashAbi.From(proof_hash);

            return await base.MakeTransactionList(new List<object> { abiHandle, trait_idAbi, proof_idAbi, thresholdAbi, proof_hashAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="proof_id"> </param>
        public async Task<Structs.ZkProofStatus> GetProofStatus(byte[] proof_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 161, 174, 200, 175 };
            var proof_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_idAbi.From(proof_id);

            var result = await base.SimApp(new List<object> { abiHandle, proof_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ZkProofStatus.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> GetProofStatus_Transactions(byte[] proof_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 161, 174, 200, 175 };
            var proof_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_idAbi.From(proof_id);

            return await base.MakeTransactionList(new List<object> { abiHandle, proof_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="proof_id"> </param>
        public async Task<bool> IsProofVerified(byte[] proof_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 85, 4, 182, 10 };
            var proof_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_idAbi.From(proof_id);

            var result = await base.SimApp(new List<object> { abiHandle, proof_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> IsProofVerified_Transactions(byte[] proof_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 85, 4, 182, 10 };
            var proof_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_idAbi.From(proof_id);

            return await base.MakeTransactionList(new List<object> { abiHandle, proof_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="trait_id"> </param>
        public async Task<byte[]> GetCommitment(byte[] trait_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 4, 76, 33, 170 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);

            var result = await base.SimApp(new List<object> { abiHandle, trait_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> GetCommitment_Transactions(byte[] trait_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 4, 76, 33, 170 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);

            return await base.MakeTransactionList(new List<object> { abiHandle, trait_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="trait_id"> </param>
        public async Task<bool> IsTraitRegistered(byte[] trait_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 232, 254, 104, 51 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);

            var result = await base.SimApp(new List<object> { abiHandle, trait_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> IsTraitRegistered_Transactions(byte[] trait_id, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 232, 254, 104, 51 };
            var trait_idAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); trait_idAbi.From(trait_id);

            return await base.MakeTransactionList(new List<object> { abiHandle, trait_idAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<ulong> GetAnchorCount(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 178, 117, 181, 57 };

            var result = await base.SimApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> GetAnchorCount_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 178, 117, 181, 57 };

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
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiVHJ1dGhSZWdpc3RyeSIsImRlc2MiOm51bGwsIm5ldHdvcmtzIjp7fSwic3RydWN0cyI6eyJaS1Byb29mU3RhdHVzIjpbeyJuYW1lIjoicHJvb2ZfaWQiLCJ0eXBlIjoiYnl0ZVtdIn0seyJuYW1lIjoidGhyZXNob2xkIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6InByb29mX2hhc2giLCJ0eXBlIjoidWludDhbMzJdIn0seyJuYW1lIjoiaXNfdmVyaWZpZWQiLCJ0eXBlIjoiYm9vbCJ9LHsibmFtZSI6InN1Ym1pdHRlZF9hdF9yb3VuZCIsInR5cGUiOiJ1aW50NjQifV19LCJNZXRob2RzIjpbeyJuYW1lIjoiY3JlYXRlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImlkZW50aXR5X3JlZ2lzdHJ5X2FwcF9pZCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOlsiTm9PcCJdLCJjYWxsIjpbXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJzZXRfdmVyaWZpZXIiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidmVyaWZpZXJfYXBwX2lkIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InJlZ2lzdGVyX2FuY2hvciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0cmFpdF9pZCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoiY29tbWl0bWVudCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJ2ZXJpZnlfemtfY2xhaW0iLCJkZXNjIjoiVmVyaWZ5IGFuZCBzZXR0bGUgYSBaSyBjbGFpbS5cblRoZSBjbGFpbSBpcyB2ZXJpZmllZCBvZmYtY2hhaW4gYnkgdGhlIGluc3RpdHV0aW9uIGFuZCB0aGVuIGFuY2hvcmVkIGhlcmUuIiwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InRyYWl0X2lkIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJieXRlW10iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwcm9vZl9pZCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH0seyJ0eXBlIjoidWludDY0Iiwic3RydWN0IjpudWxsLCJuYW1lIjoidGhyZXNob2xkIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50OFszMl0iLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJwcm9vZl9oYXNoIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImdldF9wcm9vZl9zdGF0dXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoicHJvb2ZfaWQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKGJ5dGVbXSx1aW50NjQsdWludDhbMzJdLGJvb2wsdWludDY0KSIsInN0cnVjdCI6IlpLUHJvb2ZTdGF0dXMiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImlzX3Byb29mX3ZlcmlmaWVkIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InByb29mX2lkIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJvb2wiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiZ2V0X2NvbW1pdG1lbnQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhaXRfaWQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImlzX3RyYWl0X3JlZ2lzdGVyZWQiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJuYW1lIjoidHJhaXRfaWQiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJnZXRfYW5jaG9yX2NvdW50IiwiZGVzYyI6bnVsbCwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fV0sInN0YXRlIjp7InNjaGVtYSI6eyJnbG9iYWwiOnsiaW50cyI6MywiYnl0ZXMiOjB9LCJsb2NhbCI6eyJpbnRzIjowLCJieXRlcyI6MH19LCJrZXlzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImxvY2FsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifSwiYm94Ijp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifX0sIm1hcHMiOnsiZ2xvYmFsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwiYm94Ijp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9fX0sImJhcmVBY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOltdfSwic291cmNlSW5mbyI6eyJhcHByb3ZhbCI6eyJzb3VyY2VJbmZvIjpbeyJwYyI6WzI1OCwzMzNdLCJlcnJvck1lc3NhZ2UiOiJDYWxsZXIgbXVzdCBiZSBhIHJlZ2lzdGVyZWQgaW5zdGl0dXRpb24iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyMTldLCJlcnJvck1lc3NhZ2UiOiJPbmx5IGNyZWF0b3IgY2FuIHNldCB2ZXJpZmllciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQwNV0sImVycm9yTWVzc2FnZSI6IlByb29mIG5vdCBmb3VuZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI2OF0sImVycm9yTWVzc2FnZSI6IlRyYWl0IElEIGFscmVhZHkgcmVnaXN0ZXJlZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM0MSw0ODBdLCJlcnJvck1lc3NhZ2UiOiJUcmFpdCBJRCBub3QgcmVnaXN0ZXJlZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzU2OV0sImVycm9yTWVzc2FnZSI6ImFwcGxpY2F0aW9uIGxvZyB2YWx1ZSBpcyBub3QgdGhlIHJlc3VsdCBvZiBhbiBBQkkgcmV0dXJuIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjc3LDUyMl0sImVycm9yTWVzc2FnZSI6ImNoZWNrIHNlbGYuYW5jaG9yX2NvdW50IGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUzOF0sImVycm9yTWVzc2FnZSI6ImNoZWNrIHNlbGYuaWRlbnRpdHlfcmVnaXN0cnlfYXBwX2lkIGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ0OV0sImVycm9yTWVzc2FnZSI6ImluZGV4IG91dCBvZiBib3VuZHMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsyMzIsMjQ1LDI5MCwzMDMsMzg5LDQxOSw0NjUsNDk0XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBhcnJheSBsZW5ndGggaGVhZGVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTc3XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuYm9vbCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzIzOSwyNTIsMjk3LDMxMCwzOTYsNDI2LDQ3Miw1MDFdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PGFyYzQudWludDg+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzI3XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQuc3RhdGljX2FycmF5PGFyYzQudWludDgsIDMyPiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE5NSwyMTMsMzE4XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudWludDY0IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfV0sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9LCJjbGVhciI6eyJzb3VyY2VJbmZvIjpbXSwicGNPZmZzZXRNZXRob2QiOiJub25lIn19LCJzb3VyY2UiOnsiYXBwcm92YWwiOiJJM0J5WVdkdFlTQjJaWEp6YVc5dUlERXhDaU53Y21GbmJXRWdkSGx3WlhSeVlXTnJJR1poYkhObENnb3ZMeUJoYkdkdmNIa3VZWEpqTkM1QlVrTTBRMjl1ZEhKaFkzUXVZWEJ3Y205MllXeGZjSEp2WjNKaGJTZ3BJQzArSUhWcGJuUTJORG9LYldGcGJqb0tJQ0FnSUdsdWRHTmliRzlqYXlBd0lERWdNaUE0Q2lBZ0lDQmllWFJsWTJKc2IyTnJJREI0TVRVeFpqZGpOelVnSW1GdVkyaHZjbDlqYjNWdWRDSWdJbUZ1WTJodmNsOGlJREI0TURBZ0ltbGtaVzUwYVhSNVgzSmxaMmx6ZEhKNVgyRndjRjlwWkNJZ0luQnliMjltWHlJZ0luWmxjbWxtYVdWeVgyRndjRjlwWkNJS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaWJub2diV0ZwYmw5aFpuUmxjbDlwWmw5bGJITmxRRElLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUb3lOd29nSUNBZ0x5OGdjMlZzWmk1cFpHVnVkR2wwZVY5eVpXZHBjM1J5ZVY5aGNIQmZhV1FnUFNCVlNXNTBOalFvTUNrS0lDQWdJR0o1ZEdWaklEUWdMeThnSW1sa1pXNTBhWFI1WDNKbFoybHpkSEo1WDJGd2NGOXBaQ0lLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzUnlkWFJvWDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qSTRDaUFnSUNBdkx5QnpaV3htTG1GdVkyaHZjbDlqYjNWdWRDQTlJRlZKYm5RMk5DZ3dLUW9nSUNBZ1lubDBaV05mTVNBdkx5QWlZVzVqYUc5eVgyTnZkVzUwSWdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZNekFLSUNBZ0lDOHZJSE5sYkdZdWRtVnlhV1pwWlhKZllYQndYMmxrSUQwZ1ZVbHVkRFkwS0RBcENpQWdJQ0JpZVhSbFl5QTJJQzh2SUNKMlpYSnBabWxsY2w5aGNIQmZhV1FpQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLQ20xaGFXNWZZV1owWlhKZmFXWmZaV3h6WlVBeU9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNSeWRYUm9YM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pJMUNpQWdJQ0F2THlCamJHRnpjeUJVY25WMGFGSmxaMmx6ZEhKNUtFRlNRelJEYjI1MGNtRmpkQ2s2Q2lBZ0lDQjBlRzRnVDI1RGIyMXdiR1YwYVc5dUNpQWdJQ0FoQ2lBZ0lDQmhjM05sY25RS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNCaWVpQnRZV2x1WDJOeVpXRjBaVjlPYjA5d1FERTBDaUFnSUNCd2RYTm9ZbmwwWlhOeklEQjRaRFkxWkdJeE9UTWdNSGd5TkRFMk9HVmhNeUF3ZUdReE5XVXpORGt4SURCNFlURmhaV000WVdZZ01IZzFOVEEwWWpZd1lTQXdlREEwTkdNeU1XRmhJREI0WlRobVpUWTRNek1nTUhoaU1qYzFZalV6T1NBdkx5QnRaWFJvYjJRZ0luTmxkRjkyWlhKcFptbGxjaWgxYVc1ME5qUXBkbTlwWkNJc0lHMWxkR2h2WkNBaWNtVm5hWE4wWlhKZllXNWphRzl5S0dKNWRHVmJYU3hpZVhSbFcxMHBkbTlwWkNJc0lHMWxkR2h2WkNBaWRtVnlhV1o1WDNwclgyTnNZV2x0S0dKNWRHVmJYU3hpZVhSbFcxMHNkV2x1ZERZMExIVnBiblE0V3pNeVhTbGliMjlzSWl3Z2JXVjBhRzlrSUNKblpYUmZjSEp2YjJaZmMzUmhkSFZ6S0dKNWRHVmJYU2tvWW5sMFpWdGRMSFZwYm5RMk5DeDFhVzUwT0Zzek1sMHNZbTl2YkN4MWFXNTBOalFwSWl3Z2JXVjBhRzlrSUNKcGMxOXdjbTl2Wmw5MlpYSnBabWxsWkNoaWVYUmxXMTBwWW05dmJDSXNJRzFsZEdodlpDQWlaMlYwWDJOdmJXMXBkRzFsYm5Rb1lubDBaVnRkS1dKNWRHVmJYU0lzSUcxbGRHaHZaQ0FpYVhOZmRISmhhWFJmY21WbmFYTjBaWEpsWkNoaWVYUmxXMTBwWW05dmJDSXNJRzFsZEdodlpDQWlaMlYwWDJGdVkyaHZjbDlqYjNWdWRDZ3BkV2x1ZERZMElnb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Bb2dJQ0FnYldGMFkyZ2djMlYwWDNabGNtbG1hV1Z5SUhKbFoybHpkR1Z5WDJGdVkyaHZjaUIyWlhKcFpubGZlbXRmWTJ4aGFXMGdaMlYwWDNCeWIyOW1YM04wWVhSMWN5QnBjMTl3Y205dlpsOTJaWEpwWm1sbFpDQm5aWFJmWTI5dGJXbDBiV1Z1ZENCcGMxOTBjbUZwZEY5eVpXZHBjM1JsY21Wa0lHZGxkRjloYm1Ob2IzSmZZMjkxYm5RS0lDQWdJR1Z5Y2dvS2JXRnBibDlqY21WaGRHVmZUbTlQY0VBeE5Eb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MGNuVjBhRjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRveU5Rb2dJQ0FnTHk4Z1kyeGhjM01nVkhKMWRHaFNaV2RwYzNSeWVTaEJVa00wUTI5dWRISmhZM1FwT2dvZ0lDQWdjSFZ6YUdKNWRHVnpJREI0TWpRd1pESm1OamNnTHk4Z2JXVjBhRzlrSUNKamNtVmhkR1VvZFdsdWREWTBLWFp2YVdRaUNpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBd0NpQWdJQ0J0WVhSamFDQmpjbVZoZEdVS0lDQWdJR1Z5Y2dvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5NTBjblYwYUY5eVpXZHBjM1J5ZVM1amIyNTBjbUZqZEM1VWNuVjBhRkpsWjJsemRISjVMbU55WldGMFpWdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbU55WldGMFpUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MGNuVjBhRjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRvek13b2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0dOeVpXRjBaVDBpY21WeGRXbHlaU0lwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh6SUM4dklEZ0tJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVkV2x1ZERZMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZEhKMWRHaGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TXpVS0lDQWdJQzh2SUhObGJHWXVhV1JsYm5ScGRIbGZjbVZuYVhOMGNubGZZWEJ3WDJsa0lEMGdhV1JsYm5ScGRIbGZjbVZuYVhOMGNubGZZWEJ3WDJsa0xtNWhkR2wyWlFvZ0lDQWdZblJ2YVFvZ0lDQWdZbmwwWldNZ05DQXZMeUFpYVdSbGJuUnBkSGxmY21WbmFYTjBjbmxmWVhCd1gybGtJZ29nSUNBZ2MzZGhjQW9nSUNBZ1lYQndYMmRzYjJKaGJGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUb3pOZ29nSUNBZ0x5OGdjMlZzWmk1aGJtTm9iM0pmWTI5MWJuUWdQU0JWU1c1ME5qUW9NQ2tLSUNBZ0lHSjVkR1ZqWHpFZ0x5OGdJbUZ1WTJodmNsOWpiM1Z1ZENJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPak16Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9ZM0psWVhSbFBTSnlaWEYxYVhKbElpa0tJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE11ZEhKMWRHaGZjbVZuYVhOMGNua3VZMjl1ZEhKaFkzUXVWSEoxZEdoU1pXZHBjM1J5ZVM1elpYUmZkbVZ5YVdacFpYSmJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3B6WlhSZmRtVnlhV1pwWlhJNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZEhKMWRHaGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TXpnS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCc1pXNEtJQ0FnSUdsdWRHTmZNeUF2THlBNENpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuVnBiblEyTkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalF3Q2lBZ0lDQXZMeUJoYzNObGNuUWdWSGh1TG5ObGJtUmxjaUE5UFNCSGJHOWlZV3d1WTNKbFlYUnZjbDloWkdSeVpYTnpMQ0FpVDI1c2VTQmpjbVZoZEc5eUlHTmhiaUJ6WlhRZ2RtVnlhV1pwWlhJaUNpQWdJQ0IwZUc0Z1UyVnVaR1Z5Q2lBZ0lDQm5iRzlpWVd3Z1EzSmxZWFJ2Y2tGa1pISmxjM01LSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z1QyNXNlU0JqY21WaGRHOXlJR05oYmlCelpYUWdkbVZ5YVdacFpYSUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MGNuVjBhRjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRvME1Rb2dJQ0FnTHk4Z2MyVnNaaTUyWlhKcFptbGxjbDloY0hCZmFXUWdQU0IyWlhKcFptbGxjbDloY0hCZmFXUXVibUYwYVhabENpQWdJQ0JpZEc5cENpQWdJQ0JpZVhSbFl5QTJJQzh2SUNKMlpYSnBabWxsY2w5aGNIQmZhV1FpQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPak00Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE11ZEhKMWRHaGZjbVZuYVhOMGNua3VZMjl1ZEhKaFkzUXVWSEoxZEdoU1pXZHBjM1J5ZVM1eVpXZHBjM1JsY2w5aGJtTm9iM0piY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dweVpXZHBjM1JsY2w5aGJtTm9iM0k2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZOVElLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpBb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnWkhWd0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2FXNTBZMTh5SUM4dklESUtJQ0FnSUNzS0lDQWdJR1JwWnlBeENpQWdJQ0JzWlc0S0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTWdvZ0lDQWdaSFZ3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnYVc1MFkxOHlJQzh2SURJS0lDQWdJQ3NLSUNBZ0lHUnBaeUF4Q2lBZ0lDQnNaVzRLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1WkhsdVlXMXBZMTloY25KaGVUeGhjbU0wTG5WcGJuUTRQZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzUnlkWFJvWDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qVTRDaUFnSUNBdkx5QmhjM05sY25RZ2MyVnNaaTVmYVhOZmNtVm5hWE4wWlhKbFpGOXBibk4wYVhSMWRHbHZiaWhoY21NMExrRmtaSEpsYzNNb1ZIaHVMbk5sYm1SbGNpNWllWFJsY3lrcExDQW9DaUFnSUNCMGVHNGdVMlZ1WkdWeUNpQWdJQ0JqWVd4c2MzVmlJRjlwYzE5eVpXZHBjM1JsY21Wa1gybHVjM1JwZEhWMGFXOXVDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRISjFkR2hmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk5UZ3ROakFLSUNBZ0lDOHZJR0Z6YzJWeWRDQnpaV3htTGw5cGMxOXlaV2RwYzNSbGNtVmtYMmx1YzNScGRIVjBhVzl1S0dGeVl6UXVRV1JrY21WemN5aFVlRzR1YzJWdVpHVnlMbUo1ZEdWektTa3NJQ2dLSUNBZ0lDOHZJQ0FnSUNBaVEyRnNiR1Z5SUcxMWMzUWdZbVVnWVNCeVpXZHBjM1JsY21Wa0lHbHVjM1JwZEhWMGFXOXVJZ29nSUNBZ0x5OGdLUW9nSUNBZ1lYTnpaWEowSUM4dklFTmhiR3hsY2lCdGRYTjBJR0psSUdFZ2NtVm5hWE4wWlhKbFpDQnBibk4wYVhSMWRHbHZiZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzUnlkWFJvWDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qWXlDaUFnSUNBdkx5QmhjM05sY25RZ2RISmhhWFJmYVdRZ2JtOTBJR2x1SUhObGJHWXVZVzVqYUc5eWN5d2dJbFJ5WVdsMElFbEVJR0ZzY21WaFpIa2djbVZuYVhOMFpYSmxaQ0lLSUNBZ0lHSjVkR1ZqWHpJZ0x5OGdJbUZ1WTJodmNsOGlDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJR052Ym1OaGRBb2dJQ0FnWkhWd0NpQWdJQ0JpYjNoZmJHVnVDaUFnSUNCaWRYSjVJREVLSUNBZ0lDRUtJQ0FnSUdGemMyVnlkQ0F2THlCVWNtRnBkQ0JKUkNCaGJISmxZV1I1SUhKbFoybHpkR1Z5WldRS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG8yTkFvZ0lDQWdMeThnYzJWc1ppNWhibU5vYjNKelczUnlZV2wwWDJsa0xtTnZjSGtvS1YwZ1BTQmpiMjF0YVhSdFpXNTBMbU52Y0hrb0tRb2dJQ0FnWkhWd0NpQWdJQ0JpYjNoZlpHVnNDaUFnSUNCd2IzQUtJQ0FnSUhOM1lYQUtJQ0FnSUdKdmVGOXdkWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUbzJOUW9nSUNBZ0x5OGdjMlZzWmk1aGJtTm9iM0pmWTI5MWJuUWdLejBnVlVsdWREWTBLREVwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV05mTVNBdkx5QWlZVzVqYUc5eVgyTnZkVzUwSWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJ6Wld4bUxtRnVZMmh2Y2w5amIzVnVkQ0JsZUdsemRITUtJQ0FnSUdsdWRHTmZNU0F2THlBeENpQWdJQ0FyQ2lBZ0lDQmllWFJsWTE4eElDOHZJQ0poYm1Ob2IzSmZZMjkxYm5RaUNpQWdJQ0J6ZDJGd0NpQWdJQ0JoY0hCZloyeHZZbUZzWDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNSeWRYUm9YM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pVeUNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdWRISjFkR2hmY21WbmFYTjBjbmt1WTI5dWRISmhZM1F1VkhKMWRHaFNaV2RwYzNSeWVTNTJaWEpwWm5sZmVtdGZZMnhoYVcxYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncDJaWEpwWm5sZmVtdGZZMnhoYVcwNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZEhKMWRHaGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TmpjS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQW9nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNUW9nSUNBZ1pIVndDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdhVzUwWTE4eUlDOHZJRElLSUNBZ0lDc0tJQ0FnSUdScFp5QXhDaUFnSUNCc1pXNEtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVaSGx1WVcxcFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNFBnb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01nb2dJQ0FnWkhWd0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2FXNTBZMTh5SUM4dklESUtJQ0FnSUNzS0lDQWdJR1JwWnlBeENpQWdJQ0JzWlc0S0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTXdvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHbHVkR05mTXlBdkx5QTRDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMblZwYm5RMk5Bb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ05Bb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJSEIxYzJocGJuUWdNeklLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1YzNSaGRHbGpYMkZ5Y21GNVBHRnlZelF1ZFdsdWREZ3NJRE15UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPamM1Q2lBZ0lDQXZMeUJ6Wlc1a1pYSWdQU0JVZUc0dWMyVnVaR1Z5Q2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRISjFkR2hmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk9ERXRPRElLSUNBZ0lDOHZJQ01nTVM0Z1JXNXpkWEpsSUdOaGJHeGxjaUJwY3lCaElISmxaMmx6ZEdWeVpXUWdhVzV6ZEdsMGRYUnBiMjRLSUNBZ0lDOHZJR0Z6YzJWeWRDQnpaV3htTGw5cGMxOXlaV2RwYzNSbGNtVmtYMmx1YzNScGRIVjBhVzl1S0dGeVl6UXVRV1JrY21WemN5aHpaVzVrWlhJdVlubDBaWE1wS1N3Z0lrTmhiR3hsY2lCdGRYTjBJR0psSUdFZ2NtVm5hWE4wWlhKbFpDQnBibk4wYVhSMWRHbHZiaUlLSUNBZ0lHTmhiR3h6ZFdJZ1gybHpYM0psWjJsemRHVnlaV1JmYVc1emRHbDBkWFJwYjI0S0lDQWdJR0Z6YzJWeWRDQXZMeUJEWVd4c1pYSWdiWFZ6ZENCaVpTQmhJSEpsWjJsemRHVnlaV1FnYVc1emRHbDBkWFJwYjI0S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG80TkMwNE5Rb2dJQ0FnTHk4Z0l5QXlMaUJGYm5OMWNtVWdkR2hsSUhSeVlXbDBJR2x6SUdGamRIVmhiR3g1SUhKbFoybHpkR1Z5WldRZ1lXNWtJRzFoZEdOb1pYTWdkR2hsSUdOdmJXMXBkRzFsYm5RS0lDQWdJQzh2SUdGemMyVnlkQ0IwY21GcGRGOXBaQ0JwYmlCelpXeG1MbUZ1WTJodmNuTXNJQ0pVY21GcGRDQkpSQ0J1YjNRZ2NtVm5hWE4wWlhKbFpDSUtJQ0FnSUdKNWRHVmpYeklnTHk4Z0ltRnVZMmh2Y2w4aUNpQWdJQ0IxYm1OdmRtVnlJRFFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdZbTk0WDJ4bGJnb2dJQ0FnWW5WeWVTQXhDaUFnSUNCaGMzTmxjblFnTHk4Z1ZISmhhWFFnU1VRZ2JtOTBJSEpsWjJsemRHVnlaV1FLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUbzVNd29nSUNBZ0x5OGdjM1ZpYldsMGRHVmtYMkYwWDNKdmRXNWtQV0Z5WXpRdVZVbHVkRFkwS0Vkc2IySmhiQzV5YjNWdVpDa3NDaUFnSUNCbmJHOWlZV3dnVW05MWJtUUtJQ0FnSUdsMGIySUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MGNuVjBhRjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRvNE55MDVOQW9nSUNBZ0x5OGdJeUF6TGlCVGRHOXlaU0IwYUdVZ2RtVnlhV1pwWTJGMGFXOXVJSE4wWVhSMWN3b2dJQ0FnTHk4Z2MzUmhkSFZ6SUQwZ1drdFFjbTl2WmxOMFlYUjFjeWdLSUNBZ0lDOHZJQ0FnSUNCd2NtOXZabDlwWkQxd2NtOXZabDlwWkM1amIzQjVLQ2tzQ2lBZ0lDQXZMeUFnSUNBZ2RHaHlaWE5vYjJ4a1BYUm9jbVZ6YUc5c1pDd0tJQ0FnSUM4dklDQWdJQ0J3Y205dlpsOW9ZWE5vUFhCeWIyOW1YMmhoYzJndVkyOXdlU2dwTEFvZ0lDQWdMeThnSUNBZ0lHbHpYM1psY21sbWFXVmtQV0Z5WXpRdVFtOXZiQ2hVY25WbEtTd0tJQ0FnSUM4dklDQWdJQ0J6ZFdKdGFYUjBaV1JmWVhSZmNtOTFibVE5WVhKak5DNVZTVzUwTmpRb1IyeHZZbUZzTG5KdmRXNWtLU3dLSUNBZ0lDOHZJQ2tLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlREF3TXpNS0lDQWdJSFZ1WTI5MlpYSWdNd29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQjFibU52ZG1WeUlESUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzUnlkWFJvWDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qa3lDaUFnSUNBdkx5QnBjMTkyWlhKcFptbGxaRDFoY21NMExrSnZiMndvVkhKMVpTa3NDaUFnSUNCd2RYTm9ZbmwwWlhNZ01IZzRNQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzUnlkWFJvWDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qZzNMVGswQ2lBZ0lDQXZMeUFqSURNdUlGTjBiM0psSUhSb1pTQjJaWEpwWm1sallYUnBiMjRnYzNSaGRIVnpDaUFnSUNBdkx5QnpkR0YwZFhNZ1BTQmFTMUJ5YjI5bVUzUmhkSFZ6S0FvZ0lDQWdMeThnSUNBZ0lIQnliMjltWDJsa1BYQnliMjltWDJsa0xtTnZjSGtvS1N3S0lDQWdJQzh2SUNBZ0lDQjBhSEpsYzJodmJHUTlkR2h5WlhOb2IyeGtMQW9nSUNBZ0x5OGdJQ0FnSUhCeWIyOW1YMmhoYzJnOWNISnZiMlpmYUdGemFDNWpiM0I1S0Nrc0NpQWdJQ0F2THlBZ0lDQWdhWE5mZG1WeWFXWnBaV1E5WVhKak5DNUNiMjlzS0ZSeWRXVXBMQW9nSUNBZ0x5OGdJQ0FnSUhOMVltMXBkSFJsWkY5aGRGOXliM1Z1WkQxaGNtTTBMbFZKYm5RMk5DaEhiRzlpWVd3dWNtOTFibVFwTEFvZ0lDQWdMeThnS1FvZ0lDQWdZMjl1WTJGMENpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUdScFp5QXhDaUFnSUNCamIyNWpZWFFLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUbzVOUW9nSUNBZ0x5OGdjMlZzWmk1d2NtOXZabk5iY0hKdmIyWmZhV1F1WTI5d2VTZ3BYU0E5SUhOMFlYUjFjeTVqYjNCNUtDa0tJQ0FnSUdKNWRHVmpJRFVnTHk4Z0luQnliMjltWHlJS0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmtkWEFLSUNBZ0lHSnZlRjlrWld3S0lDQWdJSEJ2Y0FvZ0lDQWdjM2RoY0FvZ0lDQWdZbTk0WDNCMWRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNSeWRYUm9YM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pZM0NpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lIQjFjMmhpZVhSbGN5QXdlREUxTVdZM1l6YzFPREFLSUNBZ0lHeHZad29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k1MGNuVjBhRjl5WldkcGMzUnllUzVqYjI1MGNtRmpkQzVVY25WMGFGSmxaMmx6ZEhKNUxtZGxkRjl3Y205dlpsOXpkR0YwZFhOYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncG5aWFJmY0hKdmIyWmZjM1JoZEhWek9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNSeWRYUm9YM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2prNUNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFvY21WaFpHOXViSGs5VkhKMVpTa0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJR0Z5Y21GNUlHeGxibWQwYUNCb1pXRmtaWElLSUNBZ0lHbHVkR05mTWlBdkx5QXlDaUFnSUNBckNpQWdJQ0JrYVdjZ01Rb2dJQ0FnYkdWdUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExtUjVibUZ0YVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0Q0S0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG94TURFS0lDQWdJQzh2SUdGemMyVnlkQ0J3Y205dlpsOXBaQ0JwYmlCelpXeG1MbkJ5YjI5bWN5d2dJbEJ5YjI5bUlHNXZkQ0JtYjNWdVpDSUtJQ0FnSUdKNWRHVmpJRFVnTHk4Z0luQnliMjltWHlJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnWkhWd0NpQWdJQ0JpYjNoZmJHVnVDaUFnSUNCaWRYSjVJREVLSUNBZ0lHRnpjMlZ5ZENBdkx5QlFjbTl2WmlCdWIzUWdabTkxYm1RS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG94TURJS0lDQWdJQzh2SUhKbGRIVnliaUJ6Wld4bUxuQnliMjltYzF0d2NtOXZabDlwWkM1amIzQjVLQ2xkTG1OdmNIa29LUW9nSUNBZ1ltOTRYMmRsZEFvZ0lDQWdjRzl3Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZPVGtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaHlaV0ZrYjI1c2VUMVVjblZsS1FvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5NTBjblYwYUY5eVpXZHBjM1J5ZVM1amIyNTBjbUZqZEM1VWNuVjBhRkpsWjJsemRISjVMbWx6WDNCeWIyOW1YM1psY21sbWFXVmtXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LYVhOZmNISnZiMlpmZG1WeWFXWnBaV1E2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZNVEEwQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9jbVZoWkc5dWJIazlWSEoxWlNrS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNaUF2THlBeUNpQWdJQ0FyQ2lBZ0lDQmthV2NnTVFvZ0lDQWdiR1Z1Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQmhjbU0wTG1SNWJtRnRhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPRDRLSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUb3hNRFlLSUNBZ0lDOHZJR2xtSUhCeWIyOW1YMmxrSUc1dmRDQnBiaUJ6Wld4bUxuQnliMjltY3pvS0lDQWdJR0o1ZEdWaklEVWdMeThnSW5CeWIyOW1YeUlLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdaSFZ3Q2lBZ0lDQmliM2hmYkdWdUNpQWdJQ0JpZFhKNUlERUtJQ0FnSUdKdWVpQnBjMTl3Y205dlpsOTJaWEpwWm1sbFpGOWhablJsY2w5cFpsOWxiSE5sUURNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG94TURjS0lDQWdJQzh2SUhKbGRIVnliaUJoY21NMExrSnZiMndvUm1Gc2MyVXBDaUFnSUNCaWVYUmxZMTh6SUM4dklEQjRNREFLQ21selgzQnliMjltWDNabGNtbG1hV1ZrWDJGbWRHVnlYMmx1YkdsdVpXUmZjMjFoY25SZlkyOXVkSEpoWTNSekxuUnlkWFJvWDNKbFoybHpkSEo1TG1OdmJuUnlZV04wTGxSeWRYUm9VbVZuYVhOMGNua3VhWE5mY0hKdmIyWmZkbVZ5YVdacFpXUkFORG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUb3hNRFFLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpDaHlaV0ZrYjI1c2VUMVVjblZsS1FvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS2FYTmZjSEp2YjJaZmRtVnlhV1pwWldSZllXWjBaWEpmYVdaZlpXeHpaVUF6T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPakV3T0FvZ0lDQWdMeThnY21WMGRYSnVJSE5sYkdZdWNISnZiMlp6VzNCeWIyOW1YMmxrTG1OdmNIa29LVjB1YVhOZmRtVnlhV1pwWldRS0lDQWdJR1IxY0FvZ0lDQWdjSFZ6YUdsdWRDQTBNZ29nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUdKdmVGOWxlSFJ5WVdOMElDOHZJRzl1SUdWeWNtOXlPaUJwYm1SbGVDQnZkWFFnYjJZZ1ltOTFibVJ6Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1oyVjBZbWwwQ2lBZ0lDQmllWFJsWTE4eklDOHZJREI0TURBS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQjFibU52ZG1WeUlESUtJQ0FnSUhObGRHSnBkQW9nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzUnlkWFJvWDNKbFoybHpkSEo1TDJOdmJuUnlZV04wTG5CNU9qRXdOQW9nSUNBZ0x5OGdRR0Z5WXpRdVlXSnBiV1YwYUc5a0tISmxZV1J2Ym14NVBWUnlkV1VwQ2lBZ0lDQmlJR2x6WDNCeWIyOW1YM1psY21sbWFXVmtYMkZtZEdWeVgybHViR2x1WldSZmMyMWhjblJmWTI5dWRISmhZM1J6TG5SeWRYUm9YM0psWjJsemRISjVMbU52Ym5SeVlXTjBMbFJ5ZFhSb1VtVm5hWE4wY25rdWFYTmZjSEp2YjJaZmRtVnlhV1pwWldSQU5Bb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTUwY25WMGFGOXlaV2RwYzNSeWVTNWpiMjUwY21GamRDNVVjblYwYUZKbFoybHpkSEo1TG1kbGRGOWpiMjF0YVhSdFpXNTBXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWjJWMFgyTnZiVzFwZEcxbGJuUTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRISjFkR2hmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk1URXdDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2NtVmhaRzl1YkhrOVZISjFaU2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUdGeWNtRjVJR3hsYm1kMGFDQm9aV0ZrWlhJS0lDQWdJR2x1ZEdOZk1pQXZMeUF5Q2lBZ0lDQXJDaUFnSUNCa2FXY2dNUW9nSUNBZ2JHVnVDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbVI1Ym1GdGFXTmZZWEp5WVhrOFlYSmpOQzUxYVc1ME9ENEtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MGNuVjBhRjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRveE1USUtJQ0FnSUM4dklHRnpjMlZ5ZENCMGNtRnBkRjlwWkNCcGJpQnpaV3htTG1GdVkyaHZjbk1zSUNKVWNtRnBkQ0JKUkNCdWIzUWdjbVZuYVhOMFpYSmxaQ0lLSUNBZ0lHSjVkR1ZqWHpJZ0x5OGdJbUZ1WTJodmNsOGlDaUFnSUNCemQyRndDaUFnSUNCamIyNWpZWFFLSUNBZ0lHUjFjQW9nSUNBZ1ltOTRYMnhsYmdvZ0lDQWdZblZ5ZVNBeENpQWdJQ0JoYzNObGNuUWdMeThnVkhKaGFYUWdTVVFnYm05MElISmxaMmx6ZEdWeVpXUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MGNuVjBhRjl5WldkcGMzUnllUzlqYjI1MGNtRmpkQzV3ZVRveE1UTUtJQ0FnSUM4dklISmxkSFZ5YmlCelpXeG1MbUZ1WTJodmNuTmJkSEpoYVhSZmFXUXVZMjl3ZVNncFhRb2dJQ0FnWW05NFgyZGxkQW9nSUNBZ2NHOXdDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRISjFkR2hmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk1URXdDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRb2NtVmhaRzl1YkhrOVZISjFaU2tLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdWRISjFkR2hmY21WbmFYTjBjbmt1WTI5dWRISmhZM1F1VkhKMWRHaFNaV2RwYzNSeWVTNXBjMTkwY21GcGRGOXlaV2RwYzNSbGNtVmtXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LYVhOZmRISmhhWFJmY21WbmFYTjBaWEpsWkRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG94TVRVS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQ2h5WldGa2IyNXNlVDFVY25WbEtRb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnWkhWd0NpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2FXNTBZMTh5SUM4dklESUtJQ0FnSUNzS0lDQWdJR1JwWnlBeENpQWdJQ0JzWlc0S0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQnVkVzFpWlhJZ2IyWWdZbmwwWlhNZ1ptOXlJR0Z5WXpRdVpIbHVZVzFwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0UGdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPakV4TndvZ0lDQWdMeThnY21WMGRYSnVJR0Z5WXpRdVFtOXZiQ2gwY21GcGRGOXBaQ0JwYmlCelpXeG1MbUZ1WTJodmNuTXBDaUFnSUNCaWVYUmxZMTh5SUM4dklDSmhibU5vYjNKZklnb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCaWIzaGZiR1Z1Q2lBZ0lDQmlkWEo1SURFS0lDQWdJR0o1ZEdWalh6TWdMeThnTUhnd01Bb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJSFZ1WTI5MlpYSWdNZ29nSUNBZ2MyVjBZbWwwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZNVEUxQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUW9jbVZoWkc5dWJIazlWSEoxWlNrS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXVkSEoxZEdoZmNtVm5hWE4wY25rdVkyOXVkSEpoWTNRdVZISjFkR2hTWldkcGMzUnllUzVuWlhSZllXNWphRzl5WDJOdmRXNTBXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWjJWMFgyRnVZMmh2Y2w5amIzVnVkRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTBjblYwYUY5eVpXZHBjM1J5ZVM5amIyNTBjbUZqZEM1d2VUb3hNakVLSUNBZ0lDOHZJSEpsZEhWeWJpQmhjbU0wTGxWSmJuUTJOQ2h6Wld4bUxtRnVZMmh2Y2w5amIzVnVkQ2tLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh4SUM4dklDSmhibU5vYjNKZlkyOTFiblFpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJSE5sYkdZdVlXNWphRzl5WDJOdmRXNTBJR1Y0YVhOMGN3b2dJQ0FnYVhSdllnb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNSeWRYUm9YM0psWjJsemRISjVMMk52Ym5SeVlXTjBMbkI1T2pFeE9Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrS0hKbFlXUnZibXg1UFZSeWRXVXBDaUFnSUNCaWVYUmxZMTh3SUM4dklEQjRNVFV4Wmpkak56VUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2JHOW5DaUFnSUNCcGJuUmpYekVnTHk4Z01Rb2dJQ0FnY21WMGRYSnVDZ29LTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TG5SeWRYUm9YM0psWjJsemRISjVMbU52Ym5SeVlXTjBMbFJ5ZFhSb1VtVm5hWE4wY25rdVgybHpYM0psWjJsemRHVnlaV1JmYVc1emRHbDBkWFJwYjI0b1lXUmtjam9nWW5sMFpYTXBJQzArSUhWcGJuUTJORG9LWDJselgzSmxaMmx6ZEdWeVpXUmZhVzV6ZEdsMGRYUnBiMjQ2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZORE10TkRRS0lDQWdJQzh2SUVCemRXSnliM1YwYVc1bENpQWdJQ0F2THlCa1pXWWdYMmx6WDNKbFoybHpkR1Z5WldSZmFXNXpkR2wwZFhScGIyNG9jMlZzWml3Z1lXUmtjam9nWVhKak5DNUJaR1J5WlhOektTQXRQaUJpYjI5c09nb2dJQ0FnY0hKdmRHOGdNU0F4Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkSEoxZEdoZmNtVm5hWE4wY25rdlkyOXVkSEpoWTNRdWNIazZORFV0TkRrS0lDQWdJQzh2SUdselgzSmxaMmx6ZEdWeVpXUXNJRjkwZUc0Z1BTQmhjbU0wTG1GaWFWOWpZV3hzS0FvZ0lDQWdMeThnSUNBZ0lFbGtaVzUwYVhSNVVtVm5hWE4wY25rdWFYTmZjbVZuYVhOMFpYSmxaQ3dLSUNBZ0lDOHZJQ0FnSUNCaFpHUnlMQW9nSUNBZ0x5OGdJQ0FnSUdGd2NGOXBaRDFCY0hCc2FXTmhkR2x2YmloelpXeG1MbWxrWlc1MGFYUjVYM0psWjJsemRISjVYMkZ3Y0Y5cFpDa3NDaUFnSUNBdkx5QXBDaUFnSUNCcGRIaHVYMkpsWjJsdUNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZEhKMWRHaGZjbVZuYVhOMGNua3ZZMjl1ZEhKaFkzUXVjSGs2TkRnS0lDQWdJQzh2SUdGd2NGOXBaRDFCY0hCc2FXTmhkR2x2YmloelpXeG1MbWxrWlc1MGFYUjVYM0psWjJsemRISjVYMkZ3Y0Y5cFpDa3NDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTWdOQ0F2THlBaWFXUmxiblJwZEhsZmNtVm5hWE4wY25sZllYQndYMmxrSWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJ6Wld4bUxtbGtaVzUwYVhSNVgzSmxaMmx6ZEhKNVgyRndjRjlwWkNCbGVHbHpkSE1LSUNBZ0lHbDBlRzVmWm1sbGJHUWdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1J5ZFhSb1gzSmxaMmx6ZEhKNUwyTnZiblJ5WVdOMExuQjVPalEyQ2lBZ0lDQXZMeUJKWkdWdWRHbDBlVkpsWjJsemRISjVMbWx6WDNKbFoybHpkR1Z5WldRc0NpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnNU1UbGpNREprWmlBdkx5QnRaWFJvYjJRZ0ltbHpYM0psWjJsemRHVnlaV1FvWVdSa2NtVnpjeWxpYjI5c0lnb2dJQ0FnYVhSNGJsOW1hV1ZzWkNCQmNIQnNhV05oZEdsdmJrRnlaM01LSUNBZ0lHWnlZVzFsWDJScFp5QXRNUW9nSUNBZ2FYUjRibDltYVdWc1pDQkJjSEJzYVdOaGRHbHZia0Z5WjNNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkwY25WMGFGOXlaV2RwYzNSeWVTOWpiMjUwY21GamRDNXdlVG8wTlMwME9Rb2dJQ0FnTHk4Z2FYTmZjbVZuYVhOMFpYSmxaQ3dnWDNSNGJpQTlJR0Z5WXpRdVlXSnBYMk5oYkd3b0NpQWdJQ0F2THlBZ0lDQWdTV1JsYm5ScGRIbFNaV2RwYzNSeWVTNXBjMTl5WldkcGMzUmxjbVZrTEFvZ0lDQWdMeThnSUNBZ0lHRmtaSElzQ2lBZ0lDQXZMeUFnSUNBZ1lYQndYMmxrUFVGd2NHeHBZMkYwYVc5dUtITmxiR1l1YVdSbGJuUnBkSGxmY21WbmFYTjBjbmxmWVhCd1gybGtLU3dLSUNBZ0lDOHZJQ2tLSUNBZ0lIQjFjMmhwYm5RZ05pQXZMeUJoY0hCc0NpQWdJQ0JwZEhodVgyWnBaV3hrSUZSNWNHVkZiblZ0Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2FYUjRibDltYVdWc1pDQkdaV1VLSUNBZ0lHbDBlRzVmYzNWaWJXbDBDaUFnSUNCcGRIaHVJRXhoYzNSTWIyY0tJQ0FnSUdSMWNBb2dJQ0FnWlhoMGNtRmpkQ0F3SURRS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QmhjSEJzYVdOaGRHbHZiaUJzYjJjZ2RtRnNkV1VnYVhNZ2JtOTBJSFJvWlNCeVpYTjFiSFFnYjJZZ1lXNGdRVUpKSUhKbGRIVnliZ29nSUNBZ1pYaDBjbUZqZENBMElEQUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1aWIyOXNDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRISjFkR2hmY21WbmFYTjBjbmt2WTI5dWRISmhZM1F1Y0hrNk5UQUtJQ0FnSUM4dklISmxkSFZ5YmlCaWIyOXNLR2x6WDNKbFoybHpkR1Z5WldRcENpQWdJQ0JpZVhSbFkxOHpJQzh2SURCNE1EQUtJQ0FnSUNFOUNpQWdJQ0J5WlhSemRXSUsiLCJjbGVhciI6IkkzQnlZV2R0WVNCMlpYSnphVzl1SURFeENpTndjbUZuYldFZ2RIbHdaWFJ5WVdOcklHWmhiSE5sQ2dvdkx5QmhiR2R2Y0hrdVlYSmpOQzVCVWtNMFEyOXVkSEpoWTNRdVkyeGxZWEpmYzNSaGRHVmZjSEp2WjNKaGJTZ3BJQzArSUhWcGJuUTJORG9LYldGcGJqb0tJQ0FnSUhCMWMyaHBiblFnTVFvZ0lDQWdjbVYwZFhKdUNnPT0ifSwiYnl0ZUNvZGUiOnsiYXBwcm92YWwiOiJDeUFFQUFFQ0NDWUhCQlVmZkhVTVlXNWphRzl5WDJOdmRXNTBCMkZ1WTJodmNsOEJBQmhwWkdWdWRHbDBlVjl5WldkcGMzUnllVjloY0hCZmFXUUdjSEp2YjJaZkQzWmxjbWxtYVdWeVgyRndjRjlwWkRFWVFBQUxKd1FpWnlraVp5Y0dJbWN4R1JSRU1SaEJBRUNDQ0FUV1hiR1RCQ1FXanFNRTBWNDBrUVNocnNpdkJGVUV0Z29FQkV3aHFnVG8vbWd6QkxKMXRUazJHZ0NPQ0FBaEFEWUFjQURUQVBFQkh3RThBVm9BZ0FRa0RTOW5OaG9BamdFQUFRQTJHZ0ZKRlNVU1JCY25CRXhuS1NKbkkwTTJHZ0ZKRlNVU1JERUFNZ2tTUkJjbkJreG5JME0yR2dGSklsa2tDRXNCRlJKRU5ob0NTU0paSkFoTEFSVVNSREVBaUFFUVJDcFBBbEJKdlVVQkZFUkp2RWhNdnlJcFpVUWpDQ2xNWnlORE5ob0JTU0paSkFoTEFSVVNSRFlhQWtraVdTUUlTd0VWRWtRMkdnTkpGU1VTUkRZYUJFa1ZnU0FTUkRFQWlBREZSQ3BQQkZDOVJRRkVNZ1lXZ0FJQU0wOERVRThDVUlBQmdGQk1VRXNCVUNjRlR3SlFTYnhJVEwrQUJSVWZmSFdBc0NORE5ob0JTU0paSkFoTEFSVVNSQ2NGVEZCSnZVVUJSTDVJS0V4UXNDTkROaG9CU1NKWkpBaExBUlVTUkNjRlRGQkp2VVVCUUFBSEt5aE1VTEFqUTBtQktpTzZJbE1ySWs4Q1ZFTC82ellhQVVraVdTUUlTd0VWRWtRcVRGQkp2VVVCUkw1SUtFeFFzQ05ETmhvQlNTSlpKQWhMQVJVU1JDcE1VTDFGQVNzaVR3SlVLRXhRc0NORElpbGxSQllvVEZDd0kwT0tBUUd4SWljRVpVU3lHSUFFa1p3QzM3SWFpLyt5R29FR3NoQWlzZ0d6dEQ1SlZ3QUVLQkpFVndRQVNSVWpFa1FyRTRrPSIsImNsZWFyIjoiQzRFQlF3PT0ifSwiY29tcGlsZXJJbmZvIjp7ImNvbXBpbGVyIjoicHV5YSIsImNvbXBpbGVyVmVyc2lvbiI6eyJtYWpvciI6NSwibWlub3IiOjgsInBhdGNoIjowLCJjb21taXRIYXNoIjpudWxsfX0sImV2ZW50cyI6W10sInRlbXBsYXRlVmFyaWFibGVzIjp7fSwic2NyYXRjaFZhcmlhYmxlcyI6e319";
    }

}
