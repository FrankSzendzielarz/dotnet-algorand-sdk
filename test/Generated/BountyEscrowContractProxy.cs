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

namespace BountyEscrowContractRegression
{


    //
    // 
    //    Contract-level bounty escrow for the decentralized v2 path.
    //
    //    This contract is designed to replace the current backend-managed escrow model.
    //    One application instance should represent one bounty.
    //    
    //
    public class BountyEscrowContractProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public BountyEscrowContractProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="creator"> </param>
        /// <param name="reward_microalgos"> </param>
        /// <param name="deadline"> </param>
        public async Task BootstrapBounty(Algorand.Address creator, ulong reward_microalgos, ulong deadline, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 197, 225, 27, 173 };
            var creatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); creatorAbi.From(creator);
            var reward_microalgosAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); reward_microalgosAbi.From(reward_microalgos);
            var deadlineAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); deadlineAbi.From(deadline);

            var result = await base.CallApp(new List<object> { abiHandle, creatorAbi, reward_microalgosAbi, deadlineAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> BootstrapBounty_Transactions(Algorand.Address creator, ulong reward_microalgos, ulong deadline, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 197, 225, 27, 173 };
            var creatorAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); creatorAbi.From(creator);
            var reward_microalgosAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); reward_microalgosAbi.From(reward_microalgos);
            var deadlineAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); deadlineAbi.From(deadline);

            return await base.MakeTransactionList(new List<object> { abiHandle, creatorAbi, reward_microalgosAbi, deadlineAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<ulong> MinimumFundingAmount(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 143, 9, 34, 194 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> MinimumFundingAmount_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 143, 9, 34, 194 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="pay"> </param>
        public async Task FundBounty(PaymentTransaction pay, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { pay });
            byte[] abiHandle = { 178, 9, 85, 53 };

            var result = await base.CallApp(new List<object> { abiHandle, pay }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> FundBounty_Transactions(PaymentTransaction pay, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            _tx_transactions.AddRange(new List<Transaction> { pay });
            byte[] abiHandle = { 178, 9, 85, 53 };

            return await base.MakeTransactionList(new List<object> { abiHandle, pay }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="proof_hash"> </param>
        public async Task SubmitWork(byte[] proof_hash, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 152, 175, 98, 188 };
            var proof_hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_hashAbi.From(proof_hash);

            var result = await base.CallApp(new List<object> { abiHandle, proof_hashAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> SubmitWork_Transactions(byte[] proof_hash, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 152, 175, 98, 188 };
            var proof_hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte"); proof_hashAbi.From(proof_hash);

            return await base.MakeTransactionList(new List<object> { abiHandle, proof_hashAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task CloseSubmissions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 70, 23, 94, 191 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> CloseSubmissions_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 70, 23, 94, 191 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task MarkDisputed(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 250, 25, 12, 238 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> MarkDisputed_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 250, 25, 12, 238 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="winner"> </param>
        public async Task ApproveWinner(Algorand.Address winner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 66, 99, 112, 204 };
            var winnerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); winnerAbi.From(winner);

            var result = await base.CallApp(new List<object> { abiHandle, winnerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> ApproveWinner_Transactions(Algorand.Address winner, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 66, 99, 112, 204 };
            var winnerAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); winnerAbi.From(winner);

            return await base.MakeTransactionList(new List<object> { abiHandle, winnerAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task RefundCreator(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 138, 115, 190, 239 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> RefundCreator_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 138, 115, 190, 239 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<byte[]> GetStatus(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 252, 57, 173, 194 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> GetStatus_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 252, 57, 173, 194 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        public async Task<ulong> GetSubmissionCount(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 89, 153, 131, 1 };

            var result = await base.CallApp(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> GetSubmissionCount_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 89, 153, 131, 1 };

            return await base.MakeTransactionList(new List<object> { abiHandle }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="submitter"> </param>
        public async Task<bool> HasSubmission(Algorand.Address submitter, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 89, 89, 5, 251 };
            var submitterAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); submitterAbi.From(submitter);

            var result = await base.CallApp(new List<object> { abiHandle, submitterAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.Bool();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> HasSubmission_Transactions(Algorand.Address submitter, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 89, 89, 5, 251 };
            var submitterAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); submitterAbi.From(submitter);

            return await base.MakeTransactionList(new List<object> { abiHandle, submitterAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="submitter"> </param>
        public async Task<byte[]> GetSubmissionHash(Algorand.Address submitter, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 23, 108, 6, 137 };
            var submitterAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); submitterAbi.From(submitter);

            var result = await base.CallApp(new List<object> { abiHandle, submitterAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> GetSubmissionHash_Transactions(Algorand.Address submitter, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 23, 108, 6, 137 };
            var submitterAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); submitterAbi.From(submitter);

            return await base.MakeTransactionList(new List<object> { abiHandle, submitterAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiQm91bnR5RXNjcm93Q29udHJhY3QiLCJkZXNjIjoiXG4gICAgQ29udHJhY3QtbGV2ZWwgYm91bnR5IGVzY3JvdyBmb3IgdGhlIGRlY2VudHJhbGl6ZWQgdjIgcGF0aC5cblxuICAgIFRoaXMgY29udHJhY3QgaXMgZGVzaWduZWQgdG8gcmVwbGFjZSB0aGUgY3VycmVudCBiYWNrZW5kLW1hbmFnZWQgZXNjcm93IG1vZGVsLlxuICAgIE9uZSBhcHBsaWNhdGlvbiBpbnN0YW5jZSBzaG91bGQgcmVwcmVzZW50IG9uZSBib3VudHkuXG4gICAgIiwibmV0d29ya3MiOnt9LCJzdHJ1Y3RzIjp7fSwiTWV0aG9kcyI6W3sibmFtZSI6ImJvb3RzdHJhcF9ib3VudHkiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6ImNyZWF0b3IiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InJld2FyZF9taWNyb2FsZ29zIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkZWFkbGluZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJtaW5pbXVtX2Z1bmRpbmdfYW1vdW50IiwiZGVzYyI6bnVsbCwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiZnVuZF9ib3VudHkiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoicGF5Iiwic3RydWN0IjpudWxsLCJuYW1lIjoicGF5IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InN1Ym1pdF93b3JrIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwibmFtZSI6InByb29mX2hhc2giLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiY2xvc2Vfc3VibWlzc2lvbnMiLCJkZXNjIjpudWxsLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoibWFya19kaXNwdXRlZCIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ2b2lkIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJhcHByb3ZlX3dpbm5lciIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoid2lubmVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InJlZnVuZF9jcmVhdG9yIiwiZGVzYyI6bnVsbCwiYXJncyI6W10sInJldHVybnMiOnsidHlwZSI6InZvaWQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6ImdldF9zdGF0dXMiLCJkZXNjIjpudWxsLCJhcmdzIjpbXSwicmV0dXJucyI6eyJ0eXBlIjoiYnl0ZVtdIiwic3RydWN0IjpudWxsLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5IjpmYWxzZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJnZXRfc3VibWlzc2lvbl9jb3VudCIsImRlc2MiOm51bGwsImFyZ3MiOltdLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOmZhbHNlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6Imhhc19zdWJtaXNzaW9uIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6ImFkZHJlc3MiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJzdWJtaXR0ZXIiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiYm9vbCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiZ2V0X3N1Ym1pc3Npb25faGFzaCIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoic3VibWl0dGVyIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX1dLCJzdGF0ZSI6eyJzY2hlbWEiOnsiZ2xvYmFsIjp7ImludHMiOjQsImJ5dGVzIjozfSwibG9jYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjB9fSwia2V5cyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn19LCJtYXBzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfX19LCJiYXJlQWN0aW9ucyI6eyJjcmVhdGUiOlsiTm9PcCJdLCJjYWxsIjpbXX0sInNvdXJjZUluZm8iOnsiYXBwcm92YWwiOnsic291cmNlSW5mbyI6W3sicGMiOlszNDRdLCJlcnJvck1lc3NhZ2UiOiJib3VudHkgYWxyZWFkeSBpbml0aWFsaXplZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ2Nl0sImVycm9yTWVzc2FnZSI6ImJvdW50eSBpcyBub3QgYWNjZXB0aW5nIHN1Ym1pc3Npb25zIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTI1XSwiZXJyb3JNZXNzYWdlIjoiYm91bnR5IGlzIG5vdCBvcGVuIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjE5XSwiZXJyb3JNZXNzYWdlIjoiYm91bnR5IGlzIG5vdCByZXZpZXdhYmxlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTc1XSwiZXJyb3JNZXNzYWdlIjoiYm91bnR5IGlzIG5vdCB1bmRlciByZXZpZXciLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MDRdLCJlcnJvck1lc3NhZ2UiOiJib3VudHkgaXMgbm90IHdhaXRpbmcgZm9yIGZ1bmRpbmciLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MTAsNDgxLDU2NSw1OTUsNjM1LDY4NSw3MTRdLCJlcnJvck1lc3NhZ2UiOiJjaGVjayBzZWxmLmNyZWF0b3IgZXhpc3RzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDczLDUzMl0sImVycm9yTWVzc2FnZSI6ImNoZWNrIHNlbGYuZGVhZGxpbmUgZXhpc3RzIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzc0LDQzMiw2NDRdLCJlcnJvck1lc3NhZ2UiOiJjaGVjayBzZWxmLnJld2FyZF9taWNyb2FsZ29zIGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM0MCw0MDAsNDYyLDUyMSw1NzEsNjAxLDYxMSw2OTEsNzAxLDczNl0sImVycm9yTWVzc2FnZSI6ImNoZWNrIHNlbGYuc3RhdHVzIGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUxMCw1MzgsNzU1XSwiZXJyb3JNZXNzYWdlIjoiY2hlY2sgc2VsZi5zdWJtaXNzaW9uX2NvdW50IGV4aXN0cyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzYzOV0sImVycm9yTWVzc2FnZSI6ImNyZWF0b3IgY2Fubm90IGJlIHRoZSB3aW5uZXIiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0ODNdLCJlcnJvck1lc3NhZ2UiOiJjcmVhdG9yIGNhbm5vdCBzdWJtaXQgdG8gdGhlaXIgb3duIGJvdW50eSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUzNF0sImVycm9yTWVzc2FnZSI6ImRlYWRsaW5lIGhhcyBub3QgcGFzc2VkIHlldCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM1Ml0sImVycm9yTWVzc2FnZSI6ImRlYWRsaW5lIG11c3QgYmUgaW4gdGhlIGZ1dHVyZSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQzOF0sImVycm9yTWVzc2FnZSI6Imluc3VmZmljaWVudCBmdW5kaW5nIGFtb3VudCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ1MF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgYXJyYXkgbGVuZ3RoIGhlYWRlciIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ1OF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LmR5bmFtaWNfYXJyYXk8YXJjNC51aW50OD4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlszMTgsNTg5LDc3MSw4MDBdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5zdGF0aWNfYXJyYXk8YXJjNC51aW50OCwgMzI+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzI2LDMzNV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnVpbnQ2NCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzU5N10sImVycm9yTWVzc2FnZSI6Im9ubHkgY3JlYXRvciBjYW4gYXBwcm92ZSB0aGUgd2lubmVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDEyXSwiZXJyb3JNZXNzYWdlIjoib25seSBjcmVhdG9yIGNhbiBmdW5kIHRoZSBib3VudHkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls1NjddLCJlcnJvck1lc3NhZ2UiOiJvbmx5IGNyZWF0b3IgY2FuIG1hcmsgdGhlIGJvdW50eSBkaXNwdXRlZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzY4N10sImVycm9yTWVzc2FnZSI6Im9ubHkgY3JlYXRvciBjYW4gcmVmdW5kIHRoZSBib3VudHkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0OTNdLCJlcnJvck1lc3NhZ2UiOiJvbmx5IG9uZSBzdWJtaXNzaW9uIHBlciB3YWxsZXQgaXMgYWxsb3dlZCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQyNl0sImVycm9yTWVzc2FnZSI6InBheW1lbnQgbXVzdCBmdW5kIHRoZSBhcHAgYWNjb3VudCIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQxOV0sImVycm9yTWVzc2FnZSI6InBheW1lbnQgc2VuZGVyIG11c3QgbWF0Y2ggYXBwIGNhbGwgc2VuZGVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNzA5XSwiZXJyb3JNZXNzYWdlIjoicmVmdW5kcyBhcmUgb25seSBhbGxvd2VkIGFmdGVyIGV4cGlyeSBvciBkaXNwdXRlIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzQ3XSwiZXJyb3JNZXNzYWdlIjoicmV3YXJkIG11c3QgYmUgZ3JlYXRlciB0aGFuIHplcm8iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0NzVdLCJlcnJvck1lc3NhZ2UiOiJzdWJtaXNzaW9uIGRlYWRsaW5lIGhhcyBwYXNzZWQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlszOTZdLCJlcnJvck1lc3NhZ2UiOiJ0cmFuc2FjdGlvbiB0eXBlIGlzIHBheSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzYzMV0sImVycm9yTWVzc2FnZSI6Indpbm5lciBtdXN0IGhhdmUgYSByZWNvcmRlZCBzdWJtaXNzaW9uIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfV0sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9LCJjbGVhciI6eyJzb3VyY2VJbmZvIjpbXSwicGNPZmZzZXRNZXRob2QiOiJub25lIn19LCJzb3VyY2UiOnsiYXBwcm92YWwiOiJJM0J5WVdkdFlTQjJaWEp6YVc5dUlERXhDaU53Y21GbmJXRWdkSGx3WlhSeVlXTnJJR1poYkhObENnb3ZMeUJoYkdkdmNIa3VZWEpqTkM1QlVrTTBRMjl1ZEhKaFkzUXVZWEJ3Y205MllXeGZjSEp2WjNKaGJTZ3BJQzArSUhWcGJuUTJORG9LYldGcGJqb0tJQ0FnSUdsdWRHTmliRzlqYXlBd0lERWdNeklnT0FvZ0lDQWdZbmwwWldOaWJHOWpheUFpYzNSaGRIVnpJaUFpWTNKbFlYUnZjaUlnSW5KbGQyRnlaRjl0YVdOeWIyRnNaMjl6SWlBaWMzVmliV2x6YzJsdmJsOWpiM1Z1ZENJZ01IZ3hOVEZtTjJNM05TQWljM1ZpYldsemMybHZiam9pSUNKa1pXRmtiR2x1WlNJZ01IZzJaamN3TmpVMlpTQXdlRGMxTm1VMk5EWTFOekkxWmpjeU5qVTNOalk1TmpVM055QXdlRFkwTnpJMk1UWTJOelFnSW5kcGJtNWxjaUlnSW5KbGRtbGxkMTl6ZEdGeWRHVmtYMkYwSWlBd2VEY3dOalUyWlRZME5qazJaVFkzTldZMk5qYzFObVUyTkRZNU5tVTJOeUF3ZURZMU56ZzNNRFk1TnpJMk5UWTBJREI0TmpRMk9UY3pOekEzTlRjME5qVTJOQW9nSUNBZ2RIaHVJRUZ3Y0d4cFkyRjBhVzl1U1VRS0lDQWdJR0p1ZWlCdFlXbHVYMkZtZEdWeVgybG1YMlZzYzJWQU1nb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwySnZkVzUwZVY5bGMyTnliM2N2WTI5dWRISmhZM1F1Y0hrNk1qUUtJQ0FnSUM4dklITmxiR1l1WTNKbFlYUnZjaUE5SUVGalkyOTFiblFvS1FvZ0lDQWdZbmwwWldOZk1TQXZMeUFpWTNKbFlYUnZjaUlLSUNBZ0lHZHNiMkpoYkNCYVpYSnZRV1JrY21WemN3b2dJQ0FnWVhCd1gyZHNiMkpoYkY5d2RYUUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qSTFDaUFnSUNBdkx5QnpaV3htTG5KbGQyRnlaRjl0YVdOeWIyRnNaMjl6SUQwZ1ZVbHVkRFkwS0RBcENpQWdJQ0JpZVhSbFkxOHlJQzh2SUNKeVpYZGhjbVJmYldsamNtOWhiR2R2Y3lJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJKdmRXNTBlVjlsYzJOeWIzY3ZZMjl1ZEhKaFkzUXVjSGs2TWpZS0lDQWdJQzh2SUhObGJHWXVaR1ZoWkd4cGJtVWdQU0JWU1c1ME5qUW9NQ2tLSUNBZ0lHSjVkR1ZqSURZZ0x5OGdJbVJsWVdSc2FXNWxJZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3lOd29nSUNBZ0x5OGdjMlZzWmk1emRHRjBkWE1nUFNCQ2VYUmxjeWhpSW1SeVlXWjBJaWtLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdJbk4wWVhSMWN5SUtJQ0FnSUdKNWRHVmpJRGtnTHk4Z01IZzJORGN5TmpFMk5qYzBDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZNamdLSUNBZ0lDOHZJSE5sYkdZdWQybHVibVZ5SUQwZ1FXTmpiM1Z1ZENncENpQWdJQ0JpZVhSbFl5QXhNQ0F2THlBaWQybHVibVZ5SWdvZ0lDQWdaMnh2WW1Gc0lGcGxjbTlCWkdSeVpYTnpDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZNamtLSUNBZ0lDOHZJSE5sYkdZdWMzVmliV2x6YzJsdmJsOWpiM1Z1ZENBOUlGVkpiblEyTkNnd0tRb2dJQ0FnWW5sMFpXTmZNeUF2THlBaWMzVmliV2x6YzJsdmJsOWpiM1Z1ZENJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJKdmRXNTBlVjlsYzJOeWIzY3ZZMjl1ZEhKaFkzUXVjSGs2TXpBS0lDQWdJQzh2SUhObGJHWXVjbVYyYVdWM1gzTjBZWEowWldSZllYUWdQU0JWU1c1ME5qUW9NQ2tLSUNBZ0lHSjVkR1ZqSURFeElDOHZJQ0p5WlhacFpYZGZjM1JoY25SbFpGOWhkQ0lLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9LYldGcGJsOWhablJsY2w5cFpsOWxiSE5sUURJNkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRveE5Rb2dJQ0FnTHk4Z1kyeGhjM01nUW05MWJuUjVSWE5qY205M1EyOXVkSEpoWTNRb1FWSkRORU52Ym5SeVlXTjBLVG9LSUNBZ0lIUjRiaUJPZFcxQmNIQkJjbWR6Q2lBZ0lDQmllaUJ0WVdsdVgxOWZZV3huYjNCNVgyUmxabUYxYkhSZlkzSmxZWFJsUURJeENpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0JoYzNObGNuUUtJQ0FnSUhSNGJpQkJjSEJzYVdOaGRHbHZia2xFQ2lBZ0lDQmhjM05sY25RS0lDQWdJSEIxYzJoaWVYUmxjM01nTUhoak5XVXhNV0poWkNBd2VEaG1NRGt5TW1NeUlEQjRZakl3T1RVMU16VWdNSGc1T0dGbU5qSmlZeUF3ZURRMk1UYzFaV0ptSURCNFptRXhPVEJqWldVZ01IZzBNall6TnpCall5QXdlRGhoTnpOaVpXVm1JREI0Wm1Nek9XRmtZeklnTUhnMU9UazVPRE13TVNBd2VEVTVOVGt3TldaaUlEQjRNVGMyWXpBMk9Ea2dMeThnYldWMGFHOWtJQ0ppYjI5MGMzUnlZWEJmWW05MWJuUjVLR0ZrWkhKbGMzTXNkV2x1ZERZMExIVnBiblEyTkNsMmIybGtJaXdnYldWMGFHOWtJQ0p0YVc1cGJYVnRYMloxYm1ScGJtZGZZVzF2ZFc1MEtDbDFhVzUwTmpRaUxDQnRaWFJvYjJRZ0ltWjFibVJmWW05MWJuUjVLSEJoZVNsMmIybGtJaXdnYldWMGFHOWtJQ0p6ZFdKdGFYUmZkMjl5YXloaWVYUmxXMTBwZG05cFpDSXNJRzFsZEdodlpDQWlZMnh2YzJWZmMzVmliV2x6YzJsdmJuTW9LWFp2YVdRaUxDQnRaWFJvYjJRZ0ltMWhjbXRmWkdsemNIVjBaV1FvS1hadmFXUWlMQ0J0WlhSb2IyUWdJbUZ3Y0hKdmRtVmZkMmx1Ym1WeUtHRmtaSEpsYzNNcGRtOXBaQ0lzSUcxbGRHaHZaQ0FpY21WbWRXNWtYMk55WldGMGIzSW9LWFp2YVdRaUxDQnRaWFJvYjJRZ0ltZGxkRjl6ZEdGMGRYTW9LV0o1ZEdWYlhTSXNJRzFsZEdodlpDQWlaMlYwWDNOMVltMXBjM05wYjI1ZlkyOTFiblFvS1hWcGJuUTJOQ0lzSUcxbGRHaHZaQ0FpYUdGelgzTjFZbTFwYzNOcGIyNG9ZV1JrY21WemN5bGliMjlzSWl3Z2JXVjBhRzlrSUNKblpYUmZjM1ZpYldsemMybHZibDlvWVhOb0tHRmtaSEpsYzNNcFlubDBaVnRkSWdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTUFvZ0lDQWdiV0YwWTJnZ1ltOXZkSE4wY21Gd1gySnZkVzUwZVNCdGFXNXBiWFZ0WDJaMWJtUnBibWRmWVcxdmRXNTBJR1oxYm1SZlltOTFiblI1SUhOMVltMXBkRjkzYjNKcklHTnNiM05sWDNOMVltMXBjM05wYjI1eklHMWhjbXRmWkdsemNIVjBaV1FnWVhCd2NtOTJaVjkzYVc1dVpYSWdjbVZtZFc1a1gyTnlaV0YwYjNJZ1oyVjBYM04wWVhSMWN5Qm5aWFJmYzNWaWJXbHpjMmx2Ymw5amIzVnVkQ0JvWVhOZmMzVmliV2x6YzJsdmJpQm5aWFJmYzNWaWJXbHpjMmx2Ymw5b1lYTm9DaUFnSUNCbGNuSUtDbTFoYVc1ZlgxOWhiR2R2Y0hsZlpHVm1ZWFZzZEY5amNtVmhkR1ZBTWpFNkNpQWdJQ0IwZUc0Z1QyNURiMjF3YkdWMGFXOXVDaUFnSUNBaENpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ0lRb2dJQ0FnSmlZS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUdKdmRXNTBlVjlsYzJOeWIzY3VZMjl1ZEhKaFkzUXVRbTkxYm5SNVJYTmpjbTkzUTI5dWRISmhZM1F1WW05dmRITjBjbUZ3WDJKdmRXNTBlVnR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21KdmIzUnpkSEpoY0Y5aWIzVnVkSGs2Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG96TXdvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURNeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuTjBZWFJwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0TENBek1qNEtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklESUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwYm5Salh6TWdMeThnT0FvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1MWFXNTBOalFLSUNBZ0lHSjBiMmtLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJRE1LSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCcGJuUmpYek1nTHk4Z09Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNTFhVzUwTmpRS0lDQWdJR0owYjJrS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pNMUNpQWdJQ0F2THlCaGMzTmxjblFnYzJWc1ppNXpkR0YwZFhNZ1BUMGdZaUprY21GbWRDSXNJQ0ppYjNWdWRIa2dZV3h5WldGa2VTQnBibWwwYVdGc2FYcGxaQ0lLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh3SUM4dklDSnpkR0YwZFhNaUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUhObGJHWXVjM1JoZEhWeklHVjRhWE4wY3dvZ0lDQWdZbmwwWldNZ09TQXZMeUF3ZURZME56STJNVFkyTnpRS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdZbTkxYm5SNUlHRnNjbVZoWkhrZ2FXNXBkR2xoYkdsNlpXUUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qTTJDaUFnSUNBdkx5QmhjM05sY25RZ2NtVjNZWEprWDIxcFkzSnZZV3huYjNNZ1BpQXdMQ0FpY21WM1lYSmtJRzExYzNRZ1ltVWdaM0psWVhSbGNpQjBhR0Z1SUhwbGNtOGlDaUFnSUNCa2FXY2dNUW9nSUNBZ1lYTnpaWEowSUM4dklISmxkMkZ5WkNCdGRYTjBJR0psSUdkeVpXRjBaWElnZEdoaGJpQjZaWEp2Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG96TndvZ0lDQWdMeThnWVhOelpYSjBJR1JsWVdSc2FXNWxJRDRnUjJ4dlltRnNMbXhoZEdWemRGOTBhVzFsYzNSaGJYQXNJQ0prWldGa2JHbHVaU0J0ZFhOMElHSmxJR2x1SUhSb1pTQm1kWFIxY21VaUNpQWdJQ0JrZFhBS0lDQWdJR2RzYjJKaGJDQk1ZWFJsYzNSVWFXMWxjM1JoYlhBS0lDQWdJRDRLSUNBZ0lHRnpjMlZ5ZENBdkx5QmtaV0ZrYkdsdVpTQnRkWE4wSUdKbElHbHVJSFJvWlNCbWRYUjFjbVVLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPak01Q2lBZ0lDQXZMeUJ6Wld4bUxtTnlaV0YwYjNJZ1BTQmpjbVZoZEc5eUNpQWdJQ0JpZVhSbFkxOHhJQzh2SUNKamNtVmhkRzl5SWdvZ0lDQWdkVzVqYjNabGNpQXpDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZOREFLSUNBZ0lDOHZJSE5sYkdZdWNtVjNZWEprWDIxcFkzSnZZV3huYjNNZ1BTQnlaWGRoY21SZmJXbGpjbTloYkdkdmN3b2dJQ0FnWW5sMFpXTmZNaUF2THlBaWNtVjNZWEprWDIxcFkzSnZZV3huYjNNaUNpQWdJQ0IxYm1OdmRtVnlJRElLSUNBZ0lHRndjRjluYkc5aVlXeGZjSFYwQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG8wTVFvZ0lDQWdMeThnYzJWc1ppNWtaV0ZrYkdsdVpTQTlJR1JsWVdSc2FXNWxDaUFnSUNCaWVYUmxZeUEySUM4dklDSmtaV0ZrYkdsdVpTSUtJQ0FnSUhOM1lYQUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUbzBNZ29nSUNBZ0x5OGdjMlZzWmk1emRHRjBkWE1nUFNCQ2VYUmxjeWhpSW5CbGJtUnBibWRmWm5WdVpHbHVaeUlwQ2lBZ0lDQmllWFJsWTE4d0lDOHZJQ0p6ZEdGMGRYTWlDaUFnSUNCaWVYUmxZeUF4TWlBdkx5QXdlRGN3TmpVMlpUWTBOamsyWlRZM05XWTJOamMxTm1VMk5EWTVObVUyTndvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pNekNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmliM1Z1ZEhsZlpYTmpjbTkzTG1OdmJuUnlZV04wTGtKdmRXNTBlVVZ6WTNKdmQwTnZiblJ5WVdOMExtMXBibWx0ZFcxZlpuVnVaR2x1WjE5aGJXOTFiblJiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwdGFXNXBiWFZ0WDJaMWJtUnBibWRmWVcxdmRXNTBPZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZORFl0TkRjS0lDQWdJQzh2SUNNZ2NtVjNZWEprSUNzZ1luVm1abVZ5SUdadmNpQjBkMjhnYVc1dVpYSWdjR0Y1YldWdWRDQjBjbUZ1YzJGamRHbHZibk1nWkhWeWFXNW5JR0Z3Y0hKdmRtRnNMM0psWm5WdVpDQm1iRzkzY3dvZ0lDQWdMeThnY21WMGRYSnVJSE5sYkdZdWNtVjNZWEprWDIxcFkzSnZZV3huYjNNZ0t5QlZTVzUwTmpRb00xOHdNREFwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV05mTWlBdkx5QWljbVYzWVhKa1gyMXBZM0p2WVd4bmIzTWlDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklITmxiR1l1Y21WM1lYSmtYMjFwWTNKdllXeG5iM01nWlhocGMzUnpDaUFnSUNCd2RYTm9hVzUwSURNd01EQUtJQ0FnSUNzS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pRMENpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lHbDBiMklLSUNBZ0lHSjVkR1ZqSURRZ0x5OGdNSGd4TlRGbU4yTTNOUW9nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQnNiMmNLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmliM1Z1ZEhsZlpYTmpjbTkzTG1OdmJuUnlZV04wTGtKdmRXNTBlVVZ6WTNKdmQwTnZiblJ5WVdOMExtWjFibVJmWW05MWJuUjVXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LWm5WdVpGOWliM1Z1ZEhrNkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRvME9Rb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrQ2lBZ0lDQjBlRzRnUjNKdmRYQkpibVJsZUFvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lDMEtJQ0FnSUdSMWNBb2dJQ0FnWjNSNGJuTWdWSGx3WlVWdWRXMEtJQ0FnSUdsdWRHTmZNU0F2THlCd1lYa0tJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnZEhKaGJuTmhZM1JwYjI0Z2RIbHdaU0JwY3lCd1lYa0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qVXhDaUFnSUNBdkx5QmhjM05sY25RZ2MyVnNaaTV6ZEdGMGRYTWdQVDBnWWlKd1pXNWthVzVuWDJaMWJtUnBibWNpTENBaVltOTFiblI1SUdseklHNXZkQ0IzWVdsMGFXNW5JR1p2Y2lCbWRXNWthVzVuSWdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdJbk4wWVhSMWN5SUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ2MyVnNaaTV6ZEdGMGRYTWdaWGhwYzNSekNpQWdJQ0JpZVhSbFl5QXhNaUF2THlBd2VEY3dOalUyWlRZME5qazJaVFkzTldZMk5qYzFObVUyTkRZNU5tVTJOd29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCaWIzVnVkSGtnYVhNZ2JtOTBJSGRoYVhScGJtY2dabTl5SUdaMWJtUnBibWNLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPalV5Q2lBZ0lDQXZMeUJoYzNObGNuUWdWSGh1TG5ObGJtUmxjaUE5UFNCelpXeG1MbU55WldGMGIzSXNJQ0p2Ym14NUlHTnlaV0YwYjNJZ1kyRnVJR1oxYm1RZ2RHaGxJR0p2ZFc1MGVTSUtJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eElDOHZJQ0pqY21WaGRHOXlJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QnpaV3htTG1OeVpXRjBiM0lnWlhocGMzUnpDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5dWJIa2dZM0psWVhSdmNpQmpZVzRnWm5WdVpDQjBhR1VnWW05MWJuUjVDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUbzFNd29nSUNBZ0x5OGdZWE56WlhKMElIQmhlUzV6Wlc1a1pYSWdQVDBnVkhodUxuTmxibVJsY2l3Z0luQmhlVzFsYm5RZ2MyVnVaR1Z5SUcxMWMzUWdiV0YwWTJnZ1lYQndJR05oYkd3Z2MyVnVaR1Z5SWdvZ0lDQWdaSFZ3Q2lBZ0lDQm5kSGh1Y3lCVFpXNWtaWElLSUNBZ0lIUjRiaUJUWlc1a1pYSUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnY0dGNWJXVnVkQ0J6Wlc1a1pYSWdiWFZ6ZENCdFlYUmphQ0JoY0hBZ1kyRnNiQ0J6Wlc1a1pYSUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qVTBDaUFnSUNBdkx5QmhjM05sY25RZ2NHRjVMbkpsWTJWcGRtVnlJRDA5SUVkc2IySmhiQzVqZFhKeVpXNTBYMkZ3Y0d4cFkyRjBhVzl1WDJGa1pISmxjM01zSUNKd1lYbHRaVzUwSUcxMWMzUWdablZ1WkNCMGFHVWdZWEJ3SUdGalkyOTFiblFpQ2lBZ0lDQmtkWEFLSUNBZ0lHZDBlRzV6SUZKbFkyVnBkbVZ5Q2lBZ0lDQm5iRzlpWVd3Z1EzVnljbVZ1ZEVGd2NHeHBZMkYwYVc5dVFXUmtjbVZ6Y3dvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QndZWGx0Wlc1MElHMTFjM1FnWm5WdVpDQjBhR1VnWVhCd0lHRmpZMjkxYm5RS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pVMUNpQWdJQ0F2THlCaGMzTmxjblFnY0dGNUxtRnRiM1Z1ZENBK1BTQnpaV3htTG0xcGJtbHRkVzFmWm5WdVpHbHVaMTloYlc5MWJuUW9LU3dnSW1sdWMzVm1abWxqYVdWdWRDQm1kVzVrYVc1bklHRnRiM1Z1ZENJS0lDQWdJR2QwZUc1eklFRnRiM1Z1ZEFvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJKdmRXNTBlVjlsYzJOeWIzY3ZZMjl1ZEhKaFkzUXVjSGs2TkRZdE5EY0tJQ0FnSUM4dklDTWdjbVYzWVhKa0lDc2dZblZtWm1WeUlHWnZjaUIwZDI4Z2FXNXVaWElnY0dGNWJXVnVkQ0IwY21GdWMyRmpkR2x2Ym5NZ1pIVnlhVzVuSUdGd2NISnZkbUZzTDNKbFpuVnVaQ0JtYkc5M2N3b2dJQ0FnTHk4Z2NtVjBkWEp1SUhObGJHWXVjbVYzWVhKa1gyMXBZM0p2WVd4bmIzTWdLeUJWU1c1ME5qUW9NMTh3TURBcENpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdZbmwwWldOZk1pQXZMeUFpY21WM1lYSmtYMjFwWTNKdllXeG5iM01pQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJSE5sYkdZdWNtVjNZWEprWDIxcFkzSnZZV3huYjNNZ1pYaHBjM1J6Q2lBZ0lDQndkWE5vYVc1MElETXdNREFLSUNBZ0lDc0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qVTFDaUFnSUNBdkx5QmhjM05sY25RZ2NHRjVMbUZ0YjNWdWRDQStQU0J6Wld4bUxtMXBibWx0ZFcxZlpuVnVaR2x1WjE5aGJXOTFiblFvS1N3Z0ltbHVjM1ZtWm1samFXVnVkQ0JtZFc1a2FXNW5JR0Z0YjNWdWRDSUtJQ0FnSUQ0OUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1emRXWm1hV05wWlc1MElHWjFibVJwYm1jZ1lXMXZkVzUwQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG8xTndvZ0lDQWdMeThnYzJWc1ppNXpkR0YwZFhNZ1BTQkNlWFJsY3loaUltOXdaVzRpS1FvZ0lDQWdZbmwwWldOZk1DQXZMeUFpYzNSaGRIVnpJZ29nSUNBZ1lubDBaV01nTnlBdkx5QXdlRFptTnpBMk5UWmxDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZORGtLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpBb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUdKdmRXNTBlVjlsYzJOeWIzY3VZMjl1ZEhKaFkzUXVRbTkxYm5SNVJYTmpjbTkzUTI5dWRISmhZM1F1YzNWaWJXbDBYM2R2Y210YmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncHpkV0p0YVhSZmQyOXlhem9LSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPalU1Q2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUUtJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJR0Z5Y21GNUlHeGxibWQwYUNCb1pXRmtaWElLSUNBZ0lIQjFjMmhwYm5RZ01nb2dJQ0FnS3dvZ0lDQWdaR2xuSURFS0lDQWdJR3hsYmdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1a2VXNWhiV2xqWDJGeWNtRjVQR0Z5WXpRdWRXbHVkRGcrQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG8yTVFvZ0lDQWdMeThnWVhOelpYSjBJSE5sYkdZdWMzUmhkSFZ6SUQwOUlHSWliM0JsYmlJc0lDSmliM1Z1ZEhrZ2FYTWdibTkwSUdGalkyVndkR2x1WnlCemRXSnRhWE56YVc5dWN5SUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFkxOHdJQzh2SUNKemRHRjBkWE1pQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJSE5sYkdZdWMzUmhkSFZ6SUdWNGFYTjBjd29nSUNBZ1lubDBaV01nTnlBdkx5QXdlRFptTnpBMk5UWmxDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdKdmRXNTBlU0JwY3lCdWIzUWdZV05qWlhCMGFXNW5JSE4xWW0xcGMzTnBiMjV6Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG8yTWdvZ0lDQWdMeThnWVhOelpYSjBJRWRzYjJKaGJDNXNZWFJsYzNSZmRHbHRaWE4wWVcxd0lEdzlJSE5sYkdZdVpHVmhaR3hwYm1Vc0lDSnpkV0p0YVhOemFXOXVJR1JsWVdSc2FXNWxJR2hoY3lCd1lYTnpaV1FpQ2lBZ0lDQm5iRzlpWVd3Z1RHRjBaWE4wVkdsdFpYTjBZVzF3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1lubDBaV01nTmlBdkx5QWlaR1ZoWkd4cGJtVWlDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklITmxiR1l1WkdWaFpHeHBibVVnWlhocGMzUnpDaUFnSUNBOFBRb2dJQ0FnWVhOelpYSjBJQzh2SUhOMVltMXBjM05wYjI0Z1pHVmhaR3hwYm1VZ2FHRnpJSEJoYzNObFpBb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwySnZkVzUwZVY5bGMyTnliM2N2WTI5dWRISmhZM1F1Y0hrNk5qTUtJQ0FnSUM4dklHRnpjMlZ5ZENCVWVHNHVjMlZ1WkdWeUlDRTlJSE5sYkdZdVkzSmxZWFJ2Y2l3Z0ltTnlaV0YwYjNJZ1kyRnVibTkwSUhOMVltMXBkQ0IwYnlCMGFHVnBjaUJ2ZDI0Z1ltOTFiblI1SWdvZ0lDQWdkSGh1SUZObGJtUmxjZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdKNWRHVmpYekVnTHk4Z0ltTnlaV0YwYjNJaUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUhObGJHWXVZM0psWVhSdmNpQmxlR2x6ZEhNS0lDQWdJQ0U5Q2lBZ0lDQmhjM05sY25RZ0x5OGdZM0psWVhSdmNpQmpZVzV1YjNRZ2MzVmliV2wwSUhSdklIUm9aV2x5SUc5M2JpQmliM1Z1ZEhrS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pZMENpQWdJQ0F2THlCaGMzTmxjblFnVkhodUxuTmxibVJsY2lCdWIzUWdhVzRnYzJWc1ppNXpkV0p0YVhOemFXOXVjeXdnSW05dWJIa2diMjVsSUhOMVltMXBjM05wYjI0Z2NHVnlJSGRoYkd4bGRDQnBjeUJoYkd4dmQyVmtJZ29nSUNBZ1lubDBaV01nTlNBdkx5QWljM1ZpYldsemMybHZiam9pQ2lBZ0lDQjBlRzRnVTJWdVpHVnlDaUFnSUNCamIyNWpZWFFLSUNBZ0lHSnZlRjlzWlc0S0lDQWdJR0oxY25rZ01Rb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMElDOHZJRzl1YkhrZ2IyNWxJSE4xWW0xcGMzTnBiMjRnY0dWeUlIZGhiR3hsZENCcGN5QmhiR3h2ZDJWa0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRvMk5nb2dJQ0FnTHk4Z2MyVnNaaTV6ZFdKdGFYTnphVzl1YzF0VWVHNHVjMlZ1WkdWeVhTQTlJSEJ5YjI5bVgyaGhjMmd1Ym1GMGFYWmxDaUFnSUNCbGVIUnlZV04wSURJZ01Bb2dJQ0FnWW5sMFpXTWdOU0F2THlBaWMzVmliV2x6YzJsdmJqb2lDaUFnSUNCMGVHNGdVMlZ1WkdWeUNpQWdJQ0JqYjI1allYUUtJQ0FnSUdSMWNBb2dJQ0FnWW05NFgyUmxiQW9nSUNBZ2NHOXdDaUFnSUNCemQyRndDaUFnSUNCaWIzaGZjSFYwQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG8yTndvZ0lDQWdMeThnYzJWc1ppNXpkV0p0YVhOemFXOXVYMk52ZFc1MElEMGdjMlZzWmk1emRXSnRhWE56YVc5dVgyTnZkVzUwSUNzZ01Rb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWalh6TWdMeThnSW5OMVltMXBjM05wYjI1ZlkyOTFiblFpQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJSE5sYkdZdWMzVmliV2x6YzJsdmJsOWpiM1Z1ZENCbGVHbHpkSE1LSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNBckNpQWdJQ0JpZVhSbFkxOHpJQzh2SUNKemRXSnRhWE56YVc5dVgyTnZkVzUwSWdvZ0lDQWdjM2RoY0FvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pVNUNpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmliM1Z1ZEhsZlpYTmpjbTkzTG1OdmJuUnlZV04wTGtKdmRXNTBlVVZ6WTNKdmQwTnZiblJ5WVdOMExtTnNiM05sWDNOMVltMXBjM05wYjI1elczSnZkWFJwYm1kZEtDa2dMVDRnZG05cFpEb0tZMnh2YzJWZmMzVmliV2x6YzJsdmJuTTZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUbzNNUW9nSUNBZ0x5OGdZWE56WlhKMElITmxiR1l1YzNSaGRIVnpJRDA5SUdJaWIzQmxiaUlzSUNKaWIzVnVkSGtnYVhNZ2JtOTBJRzl3Wlc0aUNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdZbmwwWldOZk1DQXZMeUFpYzNSaGRIVnpJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QnpaV3htTG5OMFlYUjFjeUJsZUdsemRITUtJQ0FnSUdKNWRHVmpJRGNnTHk4Z01IZzJaamN3TmpVMlpRb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJpYjNWdWRIa2dhWE1nYm05MElHOXdaVzRLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPamN5Q2lBZ0lDQXZMeUJoYzNObGNuUWdSMnh2WW1Gc0xteGhkR1Z6ZEY5MGFXMWxjM1JoYlhBZ1BpQnpaV3htTG1SbFlXUnNhVzVsTENBaVpHVmhaR3hwYm1VZ2FHRnpJRzV2ZENCd1lYTnpaV1FnZVdWMElnb2dJQ0FnWjJ4dlltRnNJRXhoZEdWemRGUnBiV1Z6ZEdGdGNBb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWaklEWWdMeThnSW1SbFlXUnNhVzVsSWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJ6Wld4bUxtUmxZV1JzYVc1bElHVjRhWE4wY3dvZ0lDQWdQZ29nSUNBZ1lYTnpaWEowSUM4dklHUmxZV1JzYVc1bElHaGhjeUJ1YjNRZ2NHRnpjMlZrSUhsbGRBb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwySnZkVzUwZVY5bGMyTnliM2N2WTI5dWRISmhZM1F1Y0hrNk56UUtJQ0FnSUM4dklHbG1JSE5sYkdZdWMzVmliV2x6YzJsdmJsOWpiM1Z1ZENBK0lEQTZDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWW5sMFpXTmZNeUF2THlBaWMzVmliV2x6YzJsdmJsOWpiM1Z1ZENJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnYzJWc1ppNXpkV0p0YVhOemFXOXVYMk52ZFc1MElHVjRhWE4wY3dvZ0lDQWdZbm9nWTJ4dmMyVmZjM1ZpYldsemMybHZibk5mWld4elpWOWliMlI1UURNS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pjMUNpQWdJQ0F2THlCelpXeG1Mbk4wWVhSMWN5QTlJRUo1ZEdWektHSWlkVzVrWlhKZmNtVjJhV1YzSWlrS0lDQWdJR0o1ZEdWalh6QWdMeThnSW5OMFlYUjFjeUlLSUNBZ0lHSjVkR1ZqSURnZ0x5OGdNSGczTlRabE5qUTJOVGN5TldZM01qWTFOelkyT1RZMU56Y0tJQ0FnSUdGd2NGOW5iRzlpWVd4ZmNIVjBDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUbzNOZ29nSUNBZ0x5OGdjMlZzWmk1eVpYWnBaWGRmYzNSaGNuUmxaRjloZENBOUlFZHNiMkpoYkM1c1lYUmxjM1JmZEdsdFpYTjBZVzF3Q2lBZ0lDQmllWFJsWXlBeE1TQXZMeUFpY21WMmFXVjNYM04wWVhKMFpXUmZZWFFpQ2lBZ0lDQm5iRzlpWVd3Z1RHRjBaWE4wVkdsdFpYTjBZVzF3Q2lBZ0lDQmhjSEJmWjJ4dlltRnNYM0IxZEFvS1kyeHZjMlZmYzNWaWJXbHpjMmx2Ym5OZllXWjBaWEpmYVdaZlpXeHpaVUEwT2dvZ0lDQWdMeThnWTI5dWRISmhZM1J6TDJKdmRXNTBlVjlsYzJOeWIzY3ZZMjl1ZEhKaFkzUXVjSGs2TmprS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQW9nSUNBZ2FXNTBZMTh4SUM4dklERUtJQ0FnSUhKbGRIVnliZ29LWTJ4dmMyVmZjM1ZpYldsemMybHZibk5mWld4elpWOWliMlI1UURNNkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRvM09Bb2dJQ0FnTHk4Z2MyVnNaaTV6ZEdGMGRYTWdQU0JDZVhSbGN5aGlJbVY0Y0dseVpXUWlLUW9nSUNBZ1lubDBaV05mTUNBdkx5QWljM1JoZEhWeklnb2dJQ0FnWW5sMFpXTWdNVE1nTHk4Z01IZzJOVGM0TnpBMk9UY3lOalUyTkFvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJR0lnWTJ4dmMyVmZjM1ZpYldsemMybHZibk5mWVdaMFpYSmZhV1pmWld4elpVQTBDZ29LTHk4Z1ltOTFiblI1WDJWelkzSnZkeTVqYjI1MGNtRmpkQzVDYjNWdWRIbEZjMk55YjNkRGIyNTBjbUZqZEM1dFlYSnJYMlJwYzNCMWRHVmtXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LYldGeWExOWthWE53ZFhSbFpEb0tJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qZ3lDaUFnSUNBdkx5QmhjM05sY25RZ1ZIaHVMbk5sYm1SbGNpQTlQU0J6Wld4bUxtTnlaV0YwYjNJc0lDSnZibXg1SUdOeVpXRjBiM0lnWTJGdUlHMWhjbXNnZEdobElHSnZkVzUwZVNCa2FYTndkWFJsWkNJS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh4SUM4dklDSmpjbVZoZEc5eUlnb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCelpXeG1MbU55WldGMGIzSWdaWGhwYzNSekNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJRzl1YkhrZ1kzSmxZWFJ2Y2lCallXNGdiV0Z5YXlCMGFHVWdZbTkxYm5SNUlHUnBjM0IxZEdWa0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRvNE13b2dJQ0FnTHk4Z1lYTnpaWEowSUhObGJHWXVjM1JoZEhWeklEMDlJR0lpZFc1a1pYSmZjbVYyYVdWM0lpd2dJbUp2ZFc1MGVTQnBjeUJ1YjNRZ2RXNWtaWElnY21WMmFXVjNJZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdKNWRHVmpYekFnTHk4Z0luTjBZWFIxY3lJS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmWjJWMFgyVjRDaUFnSUNCaGMzTmxjblFnTHk4Z1kyaGxZMnNnYzJWc1ppNXpkR0YwZFhNZ1pYaHBjM1J6Q2lBZ0lDQmllWFJsWXlBNElDOHZJREI0TnpVMlpUWTBOalUzTWpWbU56STJOVGMyTmprMk5UYzNDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdKdmRXNTBlU0JwY3lCdWIzUWdkVzVrWlhJZ2NtVjJhV1YzQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG80TlFvZ0lDQWdMeThnYzJWc1ppNXpkR0YwZFhNZ1BTQkNlWFJsY3loaUltUnBjM0IxZEdWa0lpa0tJQ0FnSUdKNWRHVmpYekFnTHk4Z0luTjBZWFIxY3lJS0lDQWdJR0o1ZEdWaklERTBJQzh2SURCNE5qUTJPVGN6TnpBM05UYzBOalUyTkFvZ0lDQWdZWEJ3WDJkc2IySmhiRjl3ZFhRS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pnd0NpQWdJQ0F2THlCQVlYSmpOQzVoWW1sdFpYUm9iMlFLSUNBZ0lHbHVkR05mTVNBdkx5QXhDaUFnSUNCeVpYUjFjbTRLQ2dvdkx5QmliM1Z1ZEhsZlpYTmpjbTkzTG1OdmJuUnlZV04wTGtKdmRXNTBlVVZ6WTNKdmQwTnZiblJ5WVdOMExtRndjSEp2ZG1WZmQybHVibVZ5VzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1lYQndjbTkyWlY5M2FXNXVaWEk2Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG80TndvZ0lDQWdMeThnUUdGeVl6UXVZV0pwYldWMGFHOWtDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QXhDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVc1MFkxOHlJQzh2SURNeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExuTjBZWFJwWTE5aGNuSmhlVHhoY21NMExuVnBiblE0TENBek1qNEtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qZzVDaUFnSUNBdkx5QmhjM05sY25RZ1ZIaHVMbk5sYm1SbGNpQTlQU0J6Wld4bUxtTnlaV0YwYjNJc0lDSnZibXg1SUdOeVpXRjBiM0lnWTJGdUlHRndjSEp2ZG1VZ2RHaGxJSGRwYm01bGNpSUtJQ0FnSUhSNGJpQlRaVzVrWlhJS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eElDOHZJQ0pqY21WaGRHOXlJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QnpaV3htTG1OeVpXRjBiM0lnWlhocGMzUnpDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUc5dWJIa2dZM0psWVhSdmNpQmpZVzRnWVhCd2NtOTJaU0IwYUdVZ2QybHVibVZ5Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG81TUFvZ0lDQWdMeThnWVhOelpYSjBJSE5sYkdZdWMzUmhkSFZ6SUQwOUlHSWliM0JsYmlJZ2IzSWdjMlZzWmk1emRHRjBkWE1nUFQwZ1lpSjFibVJsY2w5eVpYWnBaWGNpTENBaVltOTFiblI1SUdseklHNXZkQ0J5WlhacFpYZGhZbXhsSWdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdJbk4wWVhSMWN5SUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ2MyVnNaaTV6ZEdGMGRYTWdaWGhwYzNSekNpQWdJQ0JpZVhSbFl5QTNJQzh2SURCNE5tWTNNRFkxTm1VS0lDQWdJRDA5Q2lBZ0lDQmlibm9nWVhCd2NtOTJaVjkzYVc1dVpYSmZZbTl2YkY5MGNuVmxRRE1LSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh3SUM4dklDSnpkR0YwZFhNaUNpQWdJQ0JoY0hCZloyeHZZbUZzWDJkbGRGOWxlQW9nSUNBZ1lYTnpaWEowSUM4dklHTm9aV05ySUhObGJHWXVjM1JoZEhWeklHVjRhWE4wY3dvZ0lDQWdZbmwwWldNZ09DQXZMeUF3ZURjMU5tVTJORFkxTnpJMVpqY3lOalUzTmpZNU5qVTNOd29nSUNBZ1BUMEtJQ0FnSUdKNklHRndjSEp2ZG1WZmQybHVibVZ5WDJKdmIyeGZabUZzYzJWQU5Bb0tZWEJ3Y205MlpWOTNhVzV1WlhKZlltOXZiRjkwY25WbFFETTZDaUFnSUNCcGJuUmpYekVnTHk4Z01Rb0tZWEJ3Y205MlpWOTNhVzV1WlhKZlltOXZiRjl0WlhKblpVQTFPZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZPVEFLSUNBZ0lDOHZJR0Z6YzJWeWRDQnpaV3htTG5OMFlYUjFjeUE5UFNCaUltOXdaVzRpSUc5eUlITmxiR1l1YzNSaGRIVnpJRDA5SUdJaWRXNWtaWEpmY21WMmFXVjNJaXdnSW1KdmRXNTBlU0JwY3lCdWIzUWdjbVYyYVdWM1lXSnNaU0lLSUNBZ0lHRnpjMlZ5ZENBdkx5QmliM1Z1ZEhrZ2FYTWdibTkwSUhKbGRtbGxkMkZpYkdVS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2preENpQWdJQ0F2THlCaGMzTmxjblFnZDJsdWJtVnlJR2x1SUhObGJHWXVjM1ZpYldsemMybHZibk1zSUNKM2FXNXVaWElnYlhWemRDQm9ZWFpsSUdFZ2NtVmpiM0prWldRZ2MzVmliV2x6YzJsdmJpSUtJQ0FnSUdKNWRHVmpJRFVnTHk4Z0luTjFZbTFwYzNOcGIyNDZJZ29nSUNBZ1pHbG5JREVLSUNBZ0lHUjFjQW9nSUNBZ1kyOTJaWElnTWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0JpYjNoZmJHVnVDaUFnSUNCaWRYSjVJREVLSUNBZ0lHRnpjMlZ5ZENBdkx5QjNhVzV1WlhJZ2JYVnpkQ0JvWVhabElHRWdjbVZqYjNKa1pXUWdjM1ZpYldsemMybHZiZ29nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZPVElLSUNBZ0lDOHZJR0Z6YzJWeWRDQjNhVzV1WlhJZ0lUMGdjMlZzWmk1amNtVmhkRzl5TENBaVkzSmxZWFJ2Y2lCallXNXViM1FnWW1VZ2RHaGxJSGRwYm01bGNpSUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFkxOHhJQzh2SUNKamNtVmhkRzl5SWdvZ0lDQWdZWEJ3WDJkc2IySmhiRjluWlhSZlpYZ0tJQ0FnSUdGemMyVnlkQ0F2THlCamFHVmpheUJ6Wld4bUxtTnlaV0YwYjNJZ1pYaHBjM1J6Q2lBZ0lDQmthV2NnTVFvZ0lDQWdJVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QmpjbVZoZEc5eUlHTmhibTV2ZENCaVpTQjBhR1VnZDJsdWJtVnlDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUbzVOQzA1TndvZ0lDQWdMeThnYVhSNGJpNVFZWGx0Wlc1MEtBb2dJQ0FnTHk4Z0lDQWdJSEpsWTJWcGRtVnlQWGRwYm01bGNpd0tJQ0FnSUM4dklDQWdJQ0JoYlc5MWJuUTljMlZzWmk1eVpYZGhjbVJmYldsamNtOWhiR2R2Y3l3S0lDQWdJQzh2SUNrdWMzVmliV2wwS0NrS0lDQWdJR2wwZUc1ZlltVm5hVzRLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPamsyQ2lBZ0lDQXZMeUJoYlc5MWJuUTljMlZzWmk1eVpYZGhjbVJmYldsamNtOWhiR2R2Y3l3S0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eUlDOHZJQ0p5WlhkaGNtUmZiV2xqY205aGJHZHZjeUlLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2djMlZzWmk1eVpYZGhjbVJmYldsamNtOWhiR2R2Y3lCbGVHbHpkSE1LSUNBZ0lHbDBlRzVmWm1sbGJHUWdRVzF2ZFc1MENpQWdJQ0JrZFhBS0lDQWdJR2wwZUc1ZlptbGxiR1FnVW1WalpXbDJaWElLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPamswQ2lBZ0lDQXZMeUJwZEhodUxsQmhlVzFsYm5Rb0NpQWdJQ0JwYm5Salh6RWdMeThnY0dGNUNpQWdJQ0JwZEhodVgyWnBaV3hrSUZSNWNHVkZiblZ0Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ2FYUjRibDltYVdWc1pDQkdaV1VLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPamswTFRrM0NpQWdJQ0F2THlCcGRIaHVMbEJoZVcxbGJuUW9DaUFnSUNBdkx5QWdJQ0FnY21WalpXbDJaWEk5ZDJsdWJtVnlMQW9nSUNBZ0x5OGdJQ0FnSUdGdGIzVnVkRDF6Wld4bUxuSmxkMkZ5WkY5dGFXTnliMkZzWjI5ekxBb2dJQ0FnTHk4Z0tTNXpkV0p0YVhRb0tRb2dJQ0FnYVhSNGJsOXpkV0p0YVhRS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2prNUNpQWdJQ0F2THlCelpXeG1MbmRwYm01bGNpQTlJSGRwYm01bGNnb2dJQ0FnWW5sMFpXTWdNVEFnTHk4Z0luZHBibTVsY2lJS0lDQWdJSE4zWVhBS0lDQWdJR0Z3Y0Y5bmJHOWlZV3hmY0hWMENpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRveE1EQUtJQ0FnSUM4dklITmxiR1l1YzNSaGRIVnpJRDBnUW5sMFpYTW9ZaUpqYjIxd2JHVjBaV1FpS1FvZ0lDQWdZbmwwWldOZk1DQXZMeUFpYzNSaGRIVnpJZ29nSUNBZ2NIVnphR0o1ZEdWeklEQjROak0yWmpaa056QTJZelkxTnpRMk5UWTBDaUFnSUNCaGNIQmZaMnh2WW1Gc1gzQjFkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZPRGNLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpBb2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tZWEJ3Y205MlpWOTNhVzV1WlhKZlltOXZiRjltWVd4elpVQTBPZ29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdJZ1lYQndjbTkyWlY5M2FXNXVaWEpmWW05dmJGOXRaWEpuWlVBMUNnb0tMeThnWW05MWJuUjVYMlZ6WTNKdmR5NWpiMjUwY21GamRDNUNiM1Z1ZEhsRmMyTnliM2REYjI1MGNtRmpkQzV5WldaMWJtUmZZM0psWVhSdmNsdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbkpsWm5WdVpGOWpjbVZoZEc5eU9nb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwySnZkVzUwZVY5bGMyTnliM2N2WTI5dWRISmhZM1F1Y0hrNk1UQTBDaUFnSUNBdkx5QmhjM05sY25RZ1ZIaHVMbk5sYm1SbGNpQTlQU0J6Wld4bUxtTnlaV0YwYjNJc0lDSnZibXg1SUdOeVpXRjBiM0lnWTJGdUlISmxablZ1WkNCMGFHVWdZbTkxYm5SNUlnb2dJQ0FnZEhodUlGTmxibVJsY2dvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqWHpFZ0x5OGdJbU55WldGMGIzSWlDaUFnSUNCaGNIQmZaMnh2WW1Gc1gyZGxkRjlsZUFvZ0lDQWdZWE56WlhKMElDOHZJR05vWldOcklITmxiR1l1WTNKbFlYUnZjaUJsZUdsemRITUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYjI1c2VTQmpjbVZoZEc5eUlHTmhiaUJ5WldaMWJtUWdkR2hsSUdKdmRXNTBlUW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZNVEExQ2lBZ0lDQXZMeUJoYzNObGNuUWdjMlZzWmk1emRHRjBkWE1nUFQwZ1lpSmxlSEJwY21Wa0lpQnZjaUJ6Wld4bUxuTjBZWFIxY3lBOVBTQmlJbVJwYzNCMWRHVmtJaXdnSW5KbFpuVnVaSE1nWVhKbElHOXViSGtnWVd4c2IzZGxaQ0JoWm5SbGNpQmxlSEJwY25rZ2IzSWdaR2x6Y0hWMFpTSUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpZVhSbFkxOHdJQzh2SUNKemRHRjBkWE1pQ2lBZ0lDQmhjSEJmWjJ4dlltRnNYMmRsZEY5bGVBb2dJQ0FnWVhOelpYSjBJQzh2SUdOb1pXTnJJSE5sYkdZdWMzUmhkSFZ6SUdWNGFYTjBjd29nSUNBZ1lubDBaV01nTVRNZ0x5OGdNSGcyTlRjNE56QTJPVGN5TmpVMk5Bb2dJQ0FnUFQwS0lDQWdJR0p1ZWlCeVpXWjFibVJmWTNKbFlYUnZjbDlpYjI5c1gzUnlkV1ZBTXdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHSjVkR1ZqWHpBZ0x5OGdJbk4wWVhSMWN5SUtJQ0FnSUdGd2NGOW5iRzlpWVd4ZloyVjBYMlY0Q2lBZ0lDQmhjM05sY25RZ0x5OGdZMmhsWTJzZ2MyVnNaaTV6ZEdGMGRYTWdaWGhwYzNSekNpQWdJQ0JpZVhSbFl5QXhOQ0F2THlBd2VEWTBOamszTXpjd056VTNORFkxTmpRS0lDQWdJRDA5Q2lBZ0lDQmllaUJ5WldaMWJtUmZZM0psWVhSdmNsOWliMjlzWDJaaGJITmxRRFFLQ25KbFpuVnVaRjlqY21WaGRHOXlYMkp2YjJ4ZmRISjFaVUF6T2dvZ0lDQWdhVzUwWTE4eElDOHZJREVLQ25KbFpuVnVaRjlqY21WaGRHOXlYMkp2YjJ4ZmJXVnlaMlZBTlRvS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pFd05Rb2dJQ0FnTHk4Z1lYTnpaWEowSUhObGJHWXVjM1JoZEhWeklEMDlJR0lpWlhod2FYSmxaQ0lnYjNJZ2MyVnNaaTV6ZEdGMGRYTWdQVDBnWWlKa2FYTndkWFJsWkNJc0lDSnlaV1oxYm1SeklHRnlaU0J2Ym14NUlHRnNiRzkzWldRZ1lXWjBaWElnWlhod2FYSjVJRzl5SUdScGMzQjFkR1VpQ2lBZ0lDQmhjM05sY25RZ0x5OGdjbVZtZFc1a2N5QmhjbVVnYjI1c2VTQmhiR3h2ZDJWa0lHRm1kR1Z5SUdWNGNHbHllU0J2Y2lCa2FYTndkWFJsQ2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG94TURjdE1URXdDaUFnSUNBdkx5QnBkSGh1TGxCaGVXMWxiblFvQ2lBZ0lDQXZMeUFnSUNBZ2NtVmpaV2wyWlhJOWMyVnNaaTVqY21WaGRHOXlMQW9nSUNBZ0x5OGdJQ0FnSUdGdGIzVnVkRDFWU1c1ME5qUW9NQ2tzQ2lBZ0lDQXZMeUFwTG5OMVltMXBkQ2dwQ2lBZ0lDQnBkSGh1WDJKbFoybHVDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNRGdLSUNBZ0lDOHZJSEpsWTJWcGRtVnlQWE5sYkdZdVkzSmxZWFJ2Y2l3S0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmllWFJsWTE4eElDOHZJQ0pqY21WaGRHOXlJZ29nSUNBZ1lYQndYMmRzYjJKaGJGOW5aWFJmWlhnS0lDQWdJR0Z6YzJWeWRDQXZMeUJqYUdWamF5QnpaV3htTG1OeVpXRjBiM0lnWlhocGMzUnpDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNRGtLSUNBZ0lDOHZJR0Z0YjNWdWREMVZTVzUwTmpRb01Da3NDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnYVhSNGJsOW1hV1ZzWkNCQmJXOTFiblFLSUNBZ0lHbDBlRzVmWm1sbGJHUWdVbVZqWldsMlpYSUtJQ0FnSUM4dklHTnZiblJ5WVdOMGN5OWliM1Z1ZEhsZlpYTmpjbTkzTDJOdmJuUnlZV04wTG5CNU9qRXdOd29nSUNBZ0x5OGdhWFI0Ymk1UVlYbHRaVzUwS0FvZ0lDQWdhVzUwWTE4eElDOHZJSEJoZVFvZ0lDQWdhWFI0Ymw5bWFXVnNaQ0JVZVhCbFJXNTFiUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdsMGVHNWZabWxsYkdRZ1JtVmxDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNRGN0TVRFd0NpQWdJQ0F2THlCcGRIaHVMbEJoZVcxbGJuUW9DaUFnSUNBdkx5QWdJQ0FnY21WalpXbDJaWEk5YzJWc1ppNWpjbVZoZEc5eUxBb2dJQ0FnTHk4Z0lDQWdJR0Z0YjNWdWREMVZTVzUwTmpRb01Da3NDaUFnSUNBdkx5QXBMbk4xWW0xcGRDZ3BDaUFnSUNCcGRIaHVYM04xWW0xcGRBb2dJQ0FnTHk4Z1kyOXVkSEpoWTNSekwySnZkVzUwZVY5bGMyTnliM2N2WTI5dWRISmhZM1F1Y0hrNk1UQXlDaUFnSUNBdkx5QkFZWEpqTkM1aFltbHRaWFJvYjJRS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0NuSmxablZ1WkY5amNtVmhkRzl5WDJKdmIyeGZabUZzYzJWQU5Eb0tJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JpSUhKbFpuVnVaRjlqY21WaGRHOXlYMkp2YjJ4ZmJXVnlaMlZBTlFvS0NpOHZJR0p2ZFc1MGVWOWxjMk55YjNjdVkyOXVkSEpoWTNRdVFtOTFiblI1UlhOamNtOTNRMjl1ZEhKaFkzUXVaMlYwWDNOMFlYUjFjMXR5YjNWMGFXNW5YU2dwSUMwK0lIWnZhV1E2Q21kbGRGOXpkR0YwZFhNNkNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRveE1UUUtJQ0FnSUM4dklISmxkSFZ5YmlCelpXeG1Mbk4wWVhSMWN3b2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR0o1ZEdWalh6QWdMeThnSW5OMFlYUjFjeUlLSUNBZ0lHRndjRjluYkc5aVlXeGZaMlYwWDJWNENpQWdJQ0JoYzNObGNuUWdMeThnWTJobFkyc2djMlZzWmk1emRHRjBkWE1nWlhocGMzUnpDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNVElLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpBb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2wwYjJJS0lDQWdJR1Y0ZEhKaFkzUWdOaUF5Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR0o1ZEdWaklEUWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1TQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJpYjNWdWRIbGZaWE5qY205M0xtTnZiblJ5WVdOMExrSnZkVzUwZVVWelkzSnZkME52Ym5SeVlXTjBMbWRsZEY5emRXSnRhWE56YVc5dVgyTnZkVzUwVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1oyVjBYM04xWW0xcGMzTnBiMjVmWTI5MWJuUTZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNVGdLSUNBZ0lDOHZJSEpsZEhWeWJpQnpaV3htTG5OMVltMXBjM05wYjI1ZlkyOTFiblFLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCaWVYUmxZMTh6SUM4dklDSnpkV0p0YVhOemFXOXVYMk52ZFc1MElnb2dJQ0FnWVhCd1gyZHNiMkpoYkY5blpYUmZaWGdLSUNBZ0lHRnpjMlZ5ZENBdkx5QmphR1ZqYXlCelpXeG1Mbk4xWW0xcGMzTnBiMjVmWTI5MWJuUWdaWGhwYzNSekNpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRveE1UWUtJQ0FnSUM4dklFQmhjbU0wTG1GaWFXMWxkR2h2WkFvZ0lDQWdhWFJ2WWdvZ0lDQWdZbmwwWldNZ05DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eElDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJR0p2ZFc1MGVWOWxjMk55YjNjdVkyOXVkSEpoWTNRdVFtOTFiblI1UlhOamNtOTNRMjl1ZEhKaFkzUXVhR0Z6WDNOMVltMXBjM05wYjI1YmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncG9ZWE5mYzNWaWJXbHpjMmx2YmpvS0lDQWdJQzh2SUdOdmJuUnlZV04wY3k5aWIzVnVkSGxmWlhOamNtOTNMMk52Ym5SeVlXTjBMbkI1T2pFeU1Bb2dJQ0FnTHk4Z1FHRnlZelF1WVdKcGJXVjBhRzlrQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2FXNTBZMTh5SUM4dklETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCaGNtTTBMbk4wWVhScFkxOWhjbkpoZVR4aGNtTTBMblZwYm5RNExDQXpNajRLSUNBZ0lDOHZJR052Ym5SeVlXTjBjeTlpYjNWdWRIbGZaWE5qY205M0wyTnZiblJ5WVdOMExuQjVPakV5TWdvZ0lDQWdMeThnY21WMGRYSnVJSE4xWW0xcGRIUmxjaUJwYmlCelpXeG1Mbk4xWW0xcGMzTnBiMjV6Q2lBZ0lDQmllWFJsWXlBMUlDOHZJQ0p6ZFdKdGFYTnphVzl1T2lJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnWW05NFgyeGxiZ29nSUNBZ1luVnllU0F4Q2lBZ0lDQXZMeUJqYjI1MGNtRmpkSE12WW05MWJuUjVYMlZ6WTNKdmR5OWpiMjUwY21GamRDNXdlVG94TWpBS0lDQWdJQzh2SUVCaGNtTTBMbUZpYVcxbGRHaHZaQW9nSUNBZ2NIVnphR0o1ZEdWeklEQjRNREFLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCMWJtTnZkbVZ5SURJS0lDQWdJSE5sZEdKcGRBb2dJQ0FnWW5sMFpXTWdOQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHhJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUdKdmRXNTBlVjlsYzJOeWIzY3VZMjl1ZEhKaFkzUXVRbTkxYm5SNVJYTmpjbTkzUTI5dWRISmhZM1F1WjJWMFgzTjFZbTFwYzNOcGIyNWZhR0Z6YUZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNtZGxkRjl6ZFdKdGFYTnphVzl1WDJoaGMyZzZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNalFLSUNBZ0lDOHZJRUJoY21NMExtRmlhVzFsZEdodlpBb2dJQ0FnZEhodVlTQkJjSEJzYVdOaGRHbHZia0Z5WjNNZ01Rb2dJQ0FnWkhWd0NpQWdJQ0JzWlc0S0lDQWdJR2x1ZEdOZk1pQXZMeUF6TWdvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdZWEpqTkM1emRHRjBhV05mWVhKeVlYazhZWEpqTkM1MWFXNTBPQ3dnTXpJK0NpQWdJQ0F2THlCamIyNTBjbUZqZEhNdlltOTFiblI1WDJWelkzSnZkeTlqYjI1MGNtRmpkQzV3ZVRveE1qWUtJQ0FnSUM4dklISmxkSFZ5YmlCelpXeG1Mbk4xWW0xcGMzTnBiMjV6TG1kbGRDaHpkV0p0YVhSMFpYSXNJR1JsWm1GMWJIUTlRbmwwWlhNb0tTa0tJQ0FnSUdKNWRHVmpJRFVnTHk4Z0luTjFZbTFwYzNOcGIyNDZJZ29nSUNBZ2MzZGhjQW9nSUNBZ1kyOXVZMkYwQ2lBZ0lDQmliM2hmWjJWMENpQWdJQ0J3ZFhOb1lubDBaWE1nTUhnS0lDQWdJR052ZG1WeUlESUtJQ0FnSUhObGJHVmpkQW9nSUNBZ0x5OGdZMjl1ZEhKaFkzUnpMMkp2ZFc1MGVWOWxjMk55YjNjdlkyOXVkSEpoWTNRdWNIazZNVEkwQ2lBZ0lDQXZMeUJBWVhKak5DNWhZbWx0WlhSb2IyUUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JwZEc5aUNpQWdJQ0JsZUhSeVlXTjBJRFlnTWdvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JpZVhSbFl5QTBJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6RWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnPT0iLCJjbGVhciI6IkkzQnlZV2R0WVNCMlpYSnphVzl1SURFeENpTndjbUZuYldFZ2RIbHdaWFJ5WVdOcklHWmhiSE5sQ2dvdkx5QmliM1Z1ZEhsZlpYTmpjbTkzTG1OdmJuUnlZV04wTGtKdmRXNTBlVVZ6WTNKdmQwTnZiblJ5WVdOMExtTnNaV0Z5WDNOMFlYUmxYM0J5YjJkeVlXMG9LU0F0UGlCMWFXNTBOalE2Q20xaGFXNDZDaUFnSUNBdkx5QmpiMjUwY21GamRITXZZbTkxYm5SNVgyVnpZM0p2ZHk5amIyNTBjbUZqZEM1d2VUb3hNamtLSUNBZ0lDOHZJSEpsZEhWeWJpQlVjblZsQ2lBZ0lDQndkWE5vYVc1MElERUtJQ0FnSUhKbGRIVnliZ289In0sImJ5dGVDb2RlIjp7ImFwcHJvdmFsIjoiQ3lBRUFBRWdDQ1lQQm5OMFlYUjFjd2RqY21WaGRHOXlFWEpsZDJGeVpGOXRhV055YjJGc1oyOXpFSE4xWW0xcGMzTnBiMjVmWTI5MWJuUUVGUjk4ZFF0emRXSnRhWE56YVc5dU9naGtaV0ZrYkdsdVpRUnZjR1Z1REhWdVpHVnlYM0psZG1sbGR3VmtjbUZtZEFaM2FXNXVaWElSY21WMmFXVjNYM04wWVhKMFpXUmZZWFFQY0dWdVpHbHVaMTltZFc1a2FXNW5CMlY0Y0dseVpXUUlaR2x6Y0hWMFpXUXhHRUFBR3lreUEyY3FJbWNuQmlKbktDY0paeWNLTWdObkt5Sm5Kd3NpWnpFYlFRQmpNUmtVUkRFWVJJSU1CTVhoRzYwRWp3a2l3Z1N5Q1ZVMUJKaXZZcndFUmhkZXZ3VDZHUXp1QkVKamNNd0Vpbk8rN3dUOE9hM0NCRm1aZ3dFRVdWa0Yrd1FYYkFhSk5ob0FqZ3dBQ1FCRkFGVUFqd0RZQVFJQkdBRjZBYThCd2dIT0Flc0FNUmtVTVJnVUVFTTJHZ0ZKRlNRU1JEWWFBa2tWSlJKRUZ6WWFBMGtWSlJKRUZ5SW9aVVFuQ1JKRVN3RkVTVElIRFVRcFR3Tm5LazhDWnljR1RHY29Kd3huSTBNaUttVkVnYmdYQ0JZbkJFeFFzQ05ETVJZakNVazRFQ01TUkNJb1pVUW5EQkpFTVFBaUtXVkVFa1JKT0FBeEFCSkVTVGdITWdvU1JEZ0lJaXBsUklHNEZ3Z1BSQ2duQjJjalF6WWFBVWtpV1lFQ0NFc0JGUkpFSWlobFJDY0hFa1F5QnlJbkJtVkVEa1F4QUNJcFpVUVRSQ2NGTVFCUXZVVUJGRVJYQWdBbkJURUFVRW04U0V5L0lpdGxSQ01JSzB4bkkwTWlLR1ZFSndjU1JESUhJaWNHWlVRTlJDSXJaVVJCQUFzb0p3aG5Kd3N5QjJjalF5Z25EV2RDLy9jeEFDSXBaVVFTUkNJb1pVUW5DQkpFS0NjT1p5TkROaG9CU1JVa0VrUXhBQ0lwWlVRU1JDSW9aVVFuQnhKQUFBb2lLR1ZFSndnU1FRQTZJMFFuQlVzQlNVNENVTDFGQVVRaUtXVkVTd0VUUkxFaUttVkVzZ2hKc2djanNoQWlzZ0d6SndwTVp5aUFDV052YlhCc1pYUmxaR2NqUXlKQy84TXhBQ0lwWlVRU1JDSW9aVVFuRFJKQUFBb2lLR1ZFSnc0U1FRQVZJMFN4SWlsbFJDS3lDTElISTdJUUlySUJzeU5ESWtMLzZDSW9aVVJKRlJaWEJnSk1VQ2NFVEZDd0kwTWlLMlZFRmljRVRGQ3dJME0yR2dGSkZTUVNSQ2NGVEZDOVJRR0FBUUFpVHdKVUp3Uk1VTEFqUXpZYUFVa1ZKQkpFSndWTVVMNkFBRTRDVFVrVkZsY0dBa3hRSndSTVVMQWpRdz09IiwiY2xlYXIiOiJDNEVCUXc9PSJ9LCJjb21waWxlckluZm8iOnsiY29tcGlsZXIiOiJwdXlhIiwiY29tcGlsZXJWZXJzaW9uIjp7Im1ham9yIjo1LCJtaW5vciI6NywicGF0Y2giOjEsImNvbW1pdEhhc2giOm51bGx9fSwiZXZlbnRzIjpbXSwidGVtcGxhdGVWYXJpYWJsZXMiOnt9LCJzY3JhdGNoVmFyaWFibGVzIjp7fX0=";
    }

}
