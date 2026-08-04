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

namespace VerifiedProtocolRegression
{


    //
    // Skill Reputation Protocol — append-only Box ledger per wallet.
    //
    //    Every wallet (Algorand account) gets its own Box whose key is the
    //    raw 32-byte sender address.  Skill records are ARC-4 encoded and
    //    length-prefixed so they can be deterministically parsed off-chain.
    //
    //    ABI Methods
    //    -----------
    //    submit_skill_record(mode, domain, score, artifact_hash, timestamp)
    //        Append a new SkillRecord to the caller's Box.
    //    get_skill_records(wallet) → Bytes
    //        Return the raw Box bytes for any wallet (read-only).
    //    
    //
    public class VerifiedProtocolProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public VerifiedProtocolProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
        }

        ///<summary>
        ///Append a new SkillRecord to the sender's Box.
        ///• If the Box does not exist yet, it is created with exactly the   bytes of the first length-prefixed record. • If the Box already exists, the new record is appended at the end. • The caller must ensure adequate MBR (Minimum Balance Requirement)   funding for box creation / growth via an accompanying payment txn.
        ///</summary>
        /// <param name="mode"> </param>
        /// <param name="domain"> </param>
        /// <param name="score"> </param>
        /// <param name="artifact_hash"> </param>
        /// <param name="timestamp"> </param>
        public async Task SubmitSkillRecord(string mode, string domain, ulong score, string artifact_hash, ulong timestamp, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 93, 102, 129, 131 };
            var modeAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); modeAbi.From(mode);
            var domainAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); domainAbi.From(domain);
            var scoreAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); scoreAbi.From(score);
            var artifact_hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); artifact_hashAbi.From(artifact_hash);
            var timestampAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); timestampAbi.From(timestamp);

            var result = await base.CallApp(new List<object> { abiHandle, modeAbi, domainAbi, scoreAbi, artifact_hashAbi, timestampAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> SubmitSkillRecord_Transactions(string mode, string domain, ulong score, string artifact_hash, ulong timestamp, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 93, 102, 129, 131 };
            var modeAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); modeAbi.From(mode);
            var domainAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); domainAbi.From(domain);
            var scoreAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); scoreAbi.From(score);
            var artifact_hashAbi = new AVM.ClientGenerator.ABI.ARC4.Types.String(); artifact_hashAbi.From(artifact_hash);
            var timestampAbi = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64(); timestampAbi.From(timestamp);

            return await base.MakeTransactionList(new List<object> { abiHandle, modeAbi, domainAbi, scoreAbi, artifact_hashAbi, timestampAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Return the raw Box bytes for a given wallet address.
        ///Returns empty bytes if the wallet has no records. Callers can decode the result off-chain by iterating:   1. Read 2-byte big-endian length prefix → record_len   2. Read next record_len bytes → ARC-4 SkillRecord   3. Repeat until buffer exhausted.
        ///</summary>
        /// <param name="wallet"> </param>
        public async Task<byte[]> GetSkillRecords(Algorand.Address wallet, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 230, 50, 19, 157 };
            var walletAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); walletAbi.From(wallet);

            var result = await base.SimApp(new List<object> { abiHandle, walletAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>("byte");
            returnValueObj.Decode(lastLogReturnData);
            return returnValueObj.ToByteArray();

        }

        public async Task<List<Transaction>> GetSkillRecords_Transactions(Algorand.Address wallet, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 230, 50, 19, 157 };
            var walletAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); walletAbi.From(wallet);

            return await base.MakeTransactionList(new List<object> { abiHandle, walletAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Return the number of skill records stored for a wallet.
        ///Walks the length-prefixed buffer and counts entries. Returns 0 if the wallet has no Box.
        ///</summary>
        /// <param name="wallet"> </param>
        public async Task<ulong> GetRecordCount(Algorand.Address wallet, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 111, 165, 0, 92 };
            var walletAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); walletAbi.From(wallet);

            var result = await base.SimApp(new List<object> { abiHandle, walletAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
            var returnValueObj = new AVM.ClientGenerator.ABI.ARC4.Types.UInt64();
            returnValueObj.Decode(lastLogReturnData);
            return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

        }

        public async Task<List<Transaction>> GetRecordCount_Transactions(Algorand.Address wallet, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 111, 165, 0, 92 };
            var walletAbi = new AVM.ClientGenerator.ABI.ARC4.Types.Address(); walletAbi.From(wallet);

            return await base.MakeTransactionList(new List<object> { abiHandle, walletAbi }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiVmVyaWZpZWRQcm90b2NvbCIsImRlc2MiOiJTa2lsbCBSZXB1dGF0aW9uIFByb3RvY29sIOKAlCBhcHBlbmQtb25seSBCb3ggbGVkZ2VyIHBlciB3YWxsZXQuXG5cbiAgICBFdmVyeSB3YWxsZXQgKEFsZ29yYW5kIGFjY291bnQpIGdldHMgaXRzIG93biBCb3ggd2hvc2Uga2V5IGlzIHRoZVxuICAgIHJhdyAzMi1ieXRlIHNlbmRlciBhZGRyZXNzLiAgU2tpbGwgcmVjb3JkcyBhcmUgQVJDLTQgZW5jb2RlZCBhbmRcbiAgICBsZW5ndGgtcHJlZml4ZWQgc28gdGhleSBjYW4gYmUgZGV0ZXJtaW5pc3RpY2FsbHkgcGFyc2VkIG9mZi1jaGFpbi5cblxuICAgIEFCSSBNZXRob2RzXG4gICAgLS0tLS0tLS0tLS1cbiAgICBzdWJtaXRfc2tpbGxfcmVjb3JkKG1vZGUsIGRvbWFpbiwgc2NvcmUsIGFydGlmYWN0X2hhc2gsIHRpbWVzdGFtcClcbiAgICAgICAgQXBwZW5kIGEgbmV3IFNraWxsUmVjb3JkIHRvIHRoZSBjYWxsZXIncyBCb3guXG4gICAgZ2V0X3NraWxsX3JlY29yZHMod2FsbGV0KSDihpIgQnl0ZXNcbiAgICAgICAgUmV0dXJuIHRoZSByYXcgQm94IGJ5dGVzIGZvciBhbnkgd2FsbGV0IChyZWFkLW9ubHkpLlxuICAgICIsIm5ldHdvcmtzIjp7fSwic3RydWN0cyI6e30sIk1ldGhvZHMiOlt7Im5hbWUiOiJzdWJtaXRfc2tpbGxfcmVjb3JkIiwiZGVzYyI6IkFwcGVuZCBhIG5ldyBTa2lsbFJlY29yZCB0byB0aGUgc2VuZGVyJ3MgQm94Llxu4oCiIElmIHRoZSBCb3ggZG9lcyBub3QgZXhpc3QgeWV0LCBpdCBpcyBjcmVhdGVkIHdpdGggZXhhY3RseSB0aGUgICBieXRlcyBvZiB0aGUgZmlyc3QgbGVuZ3RoLXByZWZpeGVkIHJlY29yZC4g4oCiIElmIHRoZSBCb3ggYWxyZWFkeSBleGlzdHMsIHRoZSBuZXcgcmVjb3JkIGlzIGFwcGVuZGVkIGF0IHRoZSBlbmQuIOKAoiBUaGUgY2FsbGVyIG11c3QgZW5zdXJlIGFkZXF1YXRlIE1CUiAoTWluaW11bSBCYWxhbmNlIFJlcXVpcmVtZW50KSAgIGZ1bmRpbmcgZm9yIGJveCBjcmVhdGlvbiAvIGdyb3d0aCB2aWEgYW4gYWNjb21wYW55aW5nIHBheW1lbnQgdHhuLiIsImFyZ3MiOlt7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJtb2RlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJkb21haW4iLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9LHsidHlwZSI6InVpbnQ2NCIsInN0cnVjdCI6bnVsbCwibmFtZSI6InNjb3JlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJzdHJpbmciLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJhcnRpZmFjdF9oYXNoIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfSx7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsIm5hbWUiOiJ0aW1lc3RhbXAiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoidm9pZCIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6ZmFsc2UsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoiZ2V0X3NraWxsX3JlY29yZHMiLCJkZXNjIjoiUmV0dXJuIHRoZSByYXcgQm94IGJ5dGVzIGZvciBhIGdpdmVuIHdhbGxldCBhZGRyZXNzLlxuUmV0dXJucyBlbXB0eSBieXRlcyBpZiB0aGUgd2FsbGV0IGhhcyBubyByZWNvcmRzLiBDYWxsZXJzIGNhbiBkZWNvZGUgdGhlIHJlc3VsdCBvZmYtY2hhaW4gYnkgaXRlcmF0aW5nOiAgIDEuIFJlYWQgMi1ieXRlIGJpZy1lbmRpYW4gbGVuZ3RoIHByZWZpeCDihpIgcmVjb3JkX2xlbiAgIDIuIFJlYWQgbmV4dCByZWNvcmRfbGVuIGJ5dGVzIOKGkiBBUkMtNCBTa2lsbFJlY29yZCAgIDMuIFJlcGVhdCB1bnRpbCBidWZmZXIgZXhoYXVzdGVkLiIsImFyZ3MiOlt7InR5cGUiOiJhZGRyZXNzIiwic3RydWN0IjpudWxsLCJuYW1lIjoid2FsbGV0IiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6ImJ5dGVbXSIsInN0cnVjdCI6bnVsbCwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJnZXRfcmVjb3JkX2NvdW50IiwiZGVzYyI6IlJldHVybiB0aGUgbnVtYmVyIG9mIHNraWxsIHJlY29yZHMgc3RvcmVkIGZvciBhIHdhbGxldC5cbldhbGtzIHRoZSBsZW5ndGgtcHJlZml4ZWQgYnVmZmVyIGFuZCBjb3VudHMgZW50cmllcy4gUmV0dXJucyAwIGlmIHRoZSB3YWxsZXQgaGFzIG5vIEJveC4iLCJhcmdzIjpbeyJ0eXBlIjoiYWRkcmVzcyIsInN0cnVjdCI6bnVsbCwibmFtZSI6IndhbGxldCIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiJ1aW50NjQiLCJzdHJ1Y3QiOm51bGwsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX1dLCJzdGF0ZSI6eyJzY2hlbWEiOnsiZ2xvYmFsIjp7ImludHMiOjAsImJ5dGVzIjowfSwibG9jYWwiOnsiaW50cyI6MCwiYnl0ZXMiOjB9fSwia2V5cyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJsb2NhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwia2V5IjoiIn19LCJtYXBzIjp7Imdsb2JhbCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImJveCI6eyJkZXNjIjpudWxsLCJrZXlUeXBlIjoiIiwidmFsdWVUeXBlIjoiIiwicHJlZml4IjpudWxsfX19LCJiYXJlQWN0aW9ucyI6eyJjcmVhdGUiOlsiTm9PcCJdLCJjYWxsIjpbXX0sInNvdXJjZUluZm8iOnsiYXBwcm92YWwiOnsic291cmNlSW5mbyI6W3sicGMiOls2OSw4NSwxMDldLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIGFycmF5IGxlbmd0aCBoZWFkZXIiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls3OSw5NSwxMTZdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3IgYXJjNC5keW5hbWljX2FycmF5PGFyYzQudWludDg+IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjE1LDI1MF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBhcmM0LnN0YXRpY19hcnJheTxhcmM0LnVpbnQ4LCAzMj4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxMDMsMTI0XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIGFyYzQudWludDY0IiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfV0sInBjT2Zmc2V0TWV0aG9kIjoibm9uZSJ9LCJjbGVhciI6eyJzb3VyY2VJbmZvIjpbXSwicGNPZmZzZXRNZXRob2QiOiJub25lIn19LCJzb3VyY2UiOnsiYXBwcm92YWwiOiJJM0J5WVdkdFlTQjJaWEp6YVc5dUlERXhDaU53Y21GbmJXRWdkSGx3WlhSeVlXTnJJR1poYkhObENnb3ZMeUJoYkdkdmNIa3VZWEpqTkM1QlVrTTBRMjl1ZEhKaFkzUXVZWEJ3Y205MllXeGZjSEp2WjNKaGJTZ3BJQzArSUhWcGJuUTJORG9LYldGcGJqb0tJQ0FnSUdsdWRHTmliRzlqYXlBd0lESWdNU0E0Q2lBZ0lDQmllWFJsWTJKc2IyTnJJREI0TVRVeFpqZGpOelVnTUhnS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkyWlhKcFptbGxaRjl3Y205MGIyTnZiQzlqYjI1MGNtRmpkQzV3ZVRvM015MDNOZ29nSUNBZ0x5OGdJeUF0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwS0lDQWdJQzh2SUNNZ1EyOXVkSEpoWTNRS0lDQWdJQzh2SUNNZ0xTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRMUzB0TFMwdExTMHRDaUFnSUNBdkx5QmpiR0Z6Y3lCV1pYSnBabWxsWkZCeWIzUnZZMjlzS0VGU1F6UkRiMjUwY21GamRDazZDaUFnSUNCMGVHNGdUblZ0UVhCd1FYSm5jd29nSUNBZ1lub2diV0ZwYmw5ZlgyRnNaMjl3ZVY5a1pXWmhkV3gwWDJOeVpXRjBaVUF4TUFvZ0lDQWdkSGh1SUU5dVEyOXRjR3hsZEdsdmJnb2dJQ0FnSVFvZ0lDQWdZWE56WlhKMENpQWdJQ0IwZUc0Z1FYQndiR2xqWVhScGIyNUpSQW9nSUNBZ1lYTnpaWEowQ2lBZ0lDQndkWE5vWW5sMFpYTnpJREI0TldRMk5qZ3hPRE1nTUhobE5qTXlNVE01WkNBd2VEWm1ZVFV3TURWaklDOHZJRzFsZEdodlpDQWljM1ZpYldsMFgzTnJhV3hzWDNKbFkyOXlaQ2h6ZEhKcGJtY3NjM1J5YVc1bkxIVnBiblEyTkN4emRISnBibWNzZFdsdWREWTBLWFp2YVdRaUxDQnRaWFJvYjJRZ0ltZGxkRjl6YTJsc2JGOXlaV052Y21SektHRmtaSEpsYzNNcFlubDBaVnRkSWl3Z2JXVjBhRzlrSUNKblpYUmZjbVZqYjNKa1gyTnZkVzUwS0dGa1pISmxjM01wZFdsdWREWTBJZ29nSUNBZ2RIaHVZU0JCY0hCc2FXTmhkR2x2YmtGeVozTWdNQW9nSUNBZ2JXRjBZMmdnYzNWaWJXbDBYM05yYVd4c1gzSmxZMjl5WkNCblpYUmZjMnRwYkd4ZmNtVmpiM0prY3lCblpYUmZjbVZqYjNKa1gyTnZkVzUwQ2lBZ0lDQmxjbklLQ20xaGFXNWZYMTloYkdkdmNIbGZaR1ZtWVhWc2RGOWpjbVZoZEdWQU1UQTZDaUFnSUNCMGVHNGdUMjVEYjIxd2JHVjBhVzl1Q2lBZ0lDQWhDaUFnSUNCMGVHNGdRWEJ3YkdsallYUnBiMjVKUkFvZ0lDQWdJUW9nSUNBZ0ppWUtJQ0FnSUhKbGRIVnliZ29LQ2k4dklITnRZWEowWDJOdmJuUnlZV04wY3k1MlpYSnBabWxsWkY5d2NtOTBiMk52YkM1amIyNTBjbUZqZEM1V1pYSnBabWxsWkZCeWIzUnZZMjlzTG5OMVltMXBkRjl6YTJsc2JGOXlaV052Y21SYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncHpkV0p0YVhSZmMydHBiR3hmY21WamIzSmtPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzWmxjbWxtYVdWa1gzQnliM1J2WTI5c0wyTnZiblJ5WVdOMExuQjVPamt4TFRreUNpQWdJQ0F2THlBaklPS1VnT0tVZ0NCWGNtbDBaU0RpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSUFLSUNBZ0lDOHZJRUJoWW1sdFpYUm9iMlFvS1FvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnYVc1MFkxOHhJQzh2SURJS0lDQWdJQ3NLSUNBZ0lHUnBaeUF4Q2lBZ0lDQnNaVzRLSUNBZ0lHUjFjQW9nSUNBZ2RXNWpiM1psY2lBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdiblZ0WW1WeUlHOW1JR0o1ZEdWeklHWnZjaUJoY21NMExtUjVibUZ0YVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0Q0S0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURJS0lDQWdJR1IxY0FvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQmthV2NnTVFvZ0lDQWdiR1Z1Q2lBZ0lDQmtkWEFLSUNBZ0lIVnVZMjkyWlhJZ01nb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNWtlVzVoYldsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnK0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBekNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eklDOHZJRGdLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1ZFdsdWREWTBDaUFnSUNCMGVHNWhJRUZ3Y0d4cFkyRjBhVzl1UVhKbmN5QTBDaUFnSUNCa2RYQUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQnBiblJqWHpFZ0x5OGdNZ29nSUNBZ0t3b2dJQ0FnWkdsbklERUtJQ0FnSUd4bGJnb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNWtlVzVoYldsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnK0NpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBMUNpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdhVzUwWTE4eklDOHZJRGdLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlHRnlZelF1ZFdsdWREWTBDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRtVnlhV1pwWldSZmNISnZkRzlqYjJ3dlkyOXVkSEpoWTNRdWNIazZNVEE1TFRFeE5nb2dJQ0FnTHk4Z0l5QXhMaUJDZFdsc1pDQjBhR1VnUVZKRExUUWdaVzVqYjJSbFpDQnlaV052Y21RS0lDQWdJQzh2SUhKbFkyOXlaQ0E5SUZOcmFXeHNVbVZqYjNKa0tBb2dJQ0FnTHk4Z0lDQWdJRzF2WkdVOWJXOWtaU3dLSUNBZ0lDOHZJQ0FnSUNCa2IyMWhhVzQ5Wkc5dFlXbHVMQW9nSUNBZ0x5OGdJQ0FnSUhOamIzSmxQWE5qYjNKbExBb2dJQ0FnTHk4Z0lDQWdJR0Z5ZEdsbVlXTjBYMmhoYzJnOVlYSjBhV1poWTNSZmFHRnphQ3dLSUNBZ0lDOHZJQ0FnSUNCMGFXMWxjM1JoYlhBOWRHbHRaWE4wWVcxd0xBb2dJQ0FnTHk4Z0tRb2dJQ0FnY0hWemFHbHVkQ0F5TWdvZ0lDQWdkVzVqYjNabGNpQTJDaUFnSUNBckNpQWdJQ0JrZFhBS0lDQWdJR2wwYjJJS0lDQWdJR1Y0ZEhKaFkzUWdOaUF5Q2lBZ0lDQndkWE5vWW5sMFpYTWdNSGd3TURFMkNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUhOM1lYQUtJQ0FnSUhWdVkyOTJaWElnTlFvZ0lDQWdLd29nSUNBZ2MzZGhjQW9nSUNBZ2RXNWpiM1psY2lBMENpQWdJQ0JqYjI1allYUUtJQ0FnSUhOM1lYQUtJQ0FnSUdsMGIySUtJQ0FnSUdWNGRISmhZM1FnTmlBeUNpQWdJQ0JqYjI1allYUUtJQ0FnSUhOM1lYQUtJQ0FnSUdOdmJtTmhkQW9nSUNBZ2RXNWpiM1psY2lBekNpQWdJQ0JqYjI1allYUUtJQ0FnSUhWdVkyOTJaWElnTWdvZ0lDQWdZMjl1WTJGMENpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MlpYSnBabWxsWkY5d2NtOTBiMk52YkM5amIyNTBjbUZqZEM1d2VUb3hNVGt0TVRJd0NpQWdJQ0F2THlBaklESXVJRUoxYVd4a0lIUm9aU0JzWlc1bmRHZ3RjSEpsWm1sNFpXUWdjR0Y1Ykc5aFpEb2dXekl0WW5sMFpTQnNaVzVkVzNKbFkyOXlaQ0JpZVhSbGMxMEtJQ0FnSUM4dklISmxZMjl5WkY5c1pXNGdQU0J5WldOdmNtUmZZbmwwWlhNdWJHVnVaM1JvQ2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzWmxjbWxtYVdWa1gzQnliM1J2WTI5c0wyTnZiblJ5WVdOMExuQjVPall6Q2lBZ0lDQXZMeUJ5WlhSMWNtNGdiM0F1WlhoMGNtRmpkQ2h2Y0M1cGRHOWlLSFpoYkhWbEtTd2dOaXdnTWlrS0lDQWdJR2wwYjJJS0lDQWdJR1Y0ZEhKaFkzUWdOaUF5Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkbVZ5YVdacFpXUmZjSEp2ZEc5amIyd3ZZMjl1ZEhKaFkzUXVjSGs2TVRJeENpQWdJQ0F2THlCd1lYbHNiMkZrSUQwZ1gzVnBiblF4Tmw5MGIxOWllWFJsY3loeVpXTnZjbVJmYkdWdUtTQXJJSEpsWTI5eVpGOWllWFJsY3dvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZG1WeWFXWnBaV1JmY0hKdmRHOWpiMnd2WTI5dWRISmhZM1F1Y0hrNk1USXpMVEV5TkFvZ0lDQWdMeThnSXlBekxpQlRaVzVrWlhJZ2EyVjVJQ2h5WVhjZ016SXRZbmwwWlNCaFpHUnlaWE56S1FvZ0lDQWdMeThnYzJWdVpHVnlYMnRsZVNBOUlGUjRiaTV6Wlc1a1pYSXVZbmwwWlhNS0lDQWdJSFI0YmlCVFpXNWtaWElLSUNBZ0lHUjFjQW9nSUNBZ1kyOTJaWElnTWdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1psY21sbWFXVmtYM0J5YjNSdlkyOXNMMk52Ym5SeVlXTjBMbkI1T2pFeU5pMHhNamNLSUNBZ0lDOHZJQ01nTkM0Z1UyRm1aU0JpYjNnZ2FHRnVaR3hwYm1jZzRvQ1VJR05vWldOcklHVjRhWE4wWlc1alpTQm1hWEp6ZEFvZ0lDQWdMeThnWDJWNGFYTjBhVzVuWDJSaGRHRXNJR0p2ZUY5bGVHbHpkSE1nUFNCdmNDNUNiM2d1WjJWMEtITmxibVJsY2w5clpYa3BDaUFnSUNCaWIzaGZiR1Z1Q2lBZ0lDQmlkWEo1SURFS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkyWlhKcFptbGxaRjl3Y205MGIyTnZiQzlqYjI1MGNtRmpkQzV3ZVRveE1qa0tJQ0FnSUM4dklHbG1JR0p2ZUY5bGVHbHpkSE02Q2lBZ0lDQmllaUJ6ZFdKdGFYUmZjMnRwYkd4ZmNtVmpiM0prWDJWc2MyVmZZbTlrZVVBekNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZG1WeWFXWnBaV1JmY0hKdmRHOWpiMnd2WTI5dWRISmhZM1F1Y0hrNk1UTXdMVEV6TVFvZ0lDQWdMeThnSXlCQ2IzZ2dZV3h5WldGa2VTQmxlR2x6ZEhNZzRvQ1VJR052YlhCMWRHVWdZM1Z5Y21WdWRDQnphWHBsSUdGdVpDQnlaWE5wZW1VZ0t5QmhjSEJsYm1RdUNpQWdJQ0F2THlCamRYSnlaVzUwWDJ4bGJtZDBhQ0E5SUc5d0xrSnZlQzVzWlc1bmRHZ29jMlZ1WkdWeVgydGxlU2xiTUYwS0lDQWdJSE4zWVhBS0lDQWdJR1IxY0FvZ0lDQWdZbTk0WDJ4bGJnb2dJQ0FnY0c5d0NpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12ZG1WeWFXWnBaV1JmY0hKdmRHOWpiMnd2WTI5dWRISmhZM1F1Y0hrNk1UTXlDaUFnSUNBdkx5QndZWGxzYjJGa1gyeGxibWQwYUNBOUlIQmhlV3h2WVdRdWJHVnVaM1JvQ2lBZ0lDQmthV2NnTWdvZ0lDQWdiR1Z1Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkbVZ5YVdacFpXUmZjSEp2ZEc5amIyd3ZZMjl1ZEhKaFkzUXVjSGs2TVRNekNpQWdJQ0F2THlCdVpYZGZkRzkwWVd3Z1BTQmpkWEp5Wlc1MFgyeGxibWQwYUNBcklIQmhlV3h2WVdSZmJHVnVaM1JvQ2lBZ0lDQmthV2NnTVFvZ0lDQWdLd29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzWmxjbWxtYVdWa1gzQnliM1J2WTI5c0wyTnZiblJ5WVdOMExuQjVPakV6TlMweE16WUtJQ0FnSUM4dklDTWdVbVZ6YVhwbElIUm9aU0JpYjNnZ2RHOGdZV05qYjIxdGIyUmhkR1VnZEdobElHNWxkeUJ5WldOdmNtUUtJQ0FnSUM4dklHOXdMa0p2ZUM1eVpYTnBlbVVvYzJWdVpHVnlYMnRsZVN3Z2JtVjNYM1J2ZEdGc0tRb2dJQ0FnWkdsbklESUtJQ0FnSUhOM1lYQUtJQ0FnSUdKdmVGOXlaWE5wZW1VS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkyWlhKcFptbGxaRjl3Y205MGIyTnZiQzlqYjI1MGNtRmpkQzV3ZVRveE16Y3RNVE00Q2lBZ0lDQXZMeUFqSUZkeWFYUmxJSFJvWlNCdVpYY2djR0Y1Ykc5aFpDQmhkQ0IwYUdVZ2IyeGtJR1Z1WkNCdlptWnpaWFFLSUNBZ0lDOHZJRzl3TGtKdmVDNXlaWEJzWVdObEtITmxibVJsY2w5clpYa3NJR04xY25KbGJuUmZiR1Z1WjNSb0xDQndZWGxzYjJGa0tRb2dJQ0FnZFc1amIzWmxjaUF5Q2lBZ0lDQmliM2hmY21Wd2JHRmpaUW9LYzNWaWJXbDBYM05yYVd4c1gzSmxZMjl5WkY5aFpuUmxjbDlwWmw5bGJITmxRRFE2Q2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkbVZ5YVdacFpXUmZjSEp2ZEc5amIyd3ZZMjl1ZEhKaFkzUXVjSGs2T1RFdE9USUtJQ0FnSUM4dklDTWc0cFNBNHBTQUlGZHlhWFJsSU9LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ0FvZ0lDQWdMeThnUUdGaWFXMWxkR2h2WkNncENpQWdJQ0JwYm5Salh6SWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNncHpkV0p0YVhSZmMydHBiR3hmY21WamIzSmtYMlZzYzJWZlltOWtlVUF6T2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1psY21sbWFXVmtYM0J5YjNSdlkyOXNMMk52Ym5SeVlXTjBMbkI1T2pFME1DMHhOREVLSUNBZ0lDOHZJQ01nUW05NElHUnZaWE1nYm05MElHVjRhWE4wSU9LQWxDQmpjbVZoZEdVZ2QybDBhQ0IwYUdVZ2FXNXBkR2xoYkNCd1lYbHNiMkZrSUhacFlTQndkWFF1Q2lBZ0lDQXZMeUJ2Y0M1Q2IzZ3VjSFYwS0hObGJtUmxjbDlyWlhrc0lIQmhlV3h2WVdRcENpQWdJQ0JpYjNoZmNIVjBDaUFnSUNCaUlITjFZbTFwZEY5emEybHNiRjl5WldOdmNtUmZZV1owWlhKZmFXWmZaV3h6WlVBMENnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMblpsY21sbWFXVmtYM0J5YjNSdlkyOXNMbU52Ym5SeVlXTjBMbFpsY21sbWFXVmtVSEp2ZEc5amIyd3VaMlYwWDNOcmFXeHNYM0psWTI5eVpITmJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3BuWlhSZmMydHBiR3hmY21WamIzSmtjem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTJaWEpwWm1sbFpGOXdjbTkwYjJOdmJDOWpiMjUwY21GamRDNXdlVG94TkRNdE1UUTBDaUFnSUNBdkx5QWpJT0tVZ09LVWdDQlNaV0ZrSU9LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdBb2dJQ0FnTHk4Z1FHRmlhVzFsZEdodlpDaHlaV0ZrYjI1c2VUMVVjblZsS1FvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lIQjFjMmhwYm5RZ016SUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0J1ZFcxaVpYSWdiMllnWW5sMFpYTWdabTl5SUdGeVl6UXVjM1JoZEdsalgyRnljbUY1UEdGeVl6UXVkV2x1ZERnc0lETXlQZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzWmxjbWxtYVdWa1gzQnliM1J2WTI5c0wyTnZiblJ5WVdOMExuQjVPakUxTkFvZ0lDQWdMeThnWW05NFgyUmhkR0VzSUdKdmVGOWxlR2x6ZEhNZ1BTQnZjQzVDYjNndVoyVjBLSGRoYkd4bGRDNWllWFJsY3lrS0lDQWdJR0p2ZUY5blpYUUtJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MlpYSnBabWxsWkY5d2NtOTBiMk52YkM5amIyNTBjbUZqZEM1d2VUb3hOVFVLSUNBZ0lDOHZJR2xtSUdKdmVGOWxlR2x6ZEhNNkNpQWdJQ0JpZWlCblpYUmZjMnRwYkd4ZmNtVmpiM0prYzE5aFpuUmxjbDlwWmw5bGJITmxRRE1LSUNBZ0lHUjFjQW9LWjJWMFgzTnJhV3hzWDNKbFkyOXlaSE5mWVdaMFpYSmZhVzVzYVc1bFpGOXpiV0Z5ZEY5amIyNTBjbUZqZEhNdWRtVnlhV1pwWldSZmNISnZkRzlqYjJ3dVkyOXVkSEpoWTNRdVZtVnlhV1pwWldSUWNtOTBiMk52YkM1blpYUmZjMnRwYkd4ZmNtVmpiM0prYzBBME9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNabGNtbG1hV1ZrWDNCeWIzUnZZMjlzTDJOdmJuUnlZV04wTG5CNU9qRTBNeTB4TkRRS0lDQWdJQzh2SUNNZzRwU0E0cFNBSUZKbFlXUWc0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBQ2lBZ0lDQXZMeUJBWVdKcGJXVjBhRzlrS0hKbFlXUnZibXg1UFZSeWRXVXBDaUFnSUNCa2RYQUtJQ0FnSUd4bGJnb2dJQ0FnYVhSdllnb2dJQ0FnWlhoMGNtRmpkQ0EySURJS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHlJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0taMlYwWDNOcmFXeHNYM0psWTI5eVpITmZZV1owWlhKZmFXWmZaV3h6WlVBek9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNabGNtbG1hV1ZrWDNCeWIzUnZZMjlzTDJOdmJuUnlZV04wTG5CNU9qRTFOd29nSUNBZ0x5OGdjbVYwZFhKdUlFSjVkR1Z6S0dJaUlpa0tJQ0FnSUdKNWRHVmpYekVnTHk4Z01IZ0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MlpYSnBabWxsWkY5d2NtOTBiMk52YkM5amIyNTBjbUZqZEM1d2VUb3hORE10TVRRMENpQWdJQ0F2THlBaklPS1VnT0tVZ0NCU1pXRmtJT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ09LVWdPS1VnT0tVZ0FvZ0lDQWdMeThnUUdGaWFXMWxkR2h2WkNoeVpXRmtiMjVzZVQxVWNuVmxLUW9nSUNBZ1lpQm5aWFJmYzJ0cGJHeGZjbVZqYjNKa2MxOWhablJsY2w5cGJteHBibVZrWDNOdFlYSjBYMk52Ym5SeVlXTjBjeTUyWlhKcFptbGxaRjl3Y205MGIyTnZiQzVqYjI1MGNtRmpkQzVXWlhKcFptbGxaRkJ5YjNSdlkyOXNMbWRsZEY5emEybHNiRjl5WldOdmNtUnpRRFFLQ2dvdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdWRtVnlhV1pwWldSZmNISnZkRzlqYjJ3dVkyOXVkSEpoWTNRdVZtVnlhV1pwWldSUWNtOTBiMk52YkM1blpYUmZjbVZqYjNKa1gyTnZkVzUwVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS1oyVjBYM0psWTI5eVpGOWpiM1Z1ZERvS0lDQWdJR0o1ZEdWalh6RWdMeThnSWlJS0lDQWdJR1IxY0c0Z01nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNabGNtbG1hV1ZrWDNCeWIzUnZZMjlzTDJOdmJuUnlZV04wTG5CNU9qRTFPUzB4TmpBS0lDQWdJQzh2SUNNZzRwU0E0cFNBSUZWMGFXeHBkSGtnNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQUNpQWdJQ0F2THlCQVlXSnBiV1YwYUc5a0tISmxZV1J2Ym14NVBWUnlkV1VwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ2NIVnphR2x1ZENBek1nb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnWVhKak5DNXpkR0YwYVdOZllYSnlZWGs4WVhKak5DNTFhVzUwT0N3Z016SStDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRtVnlhV1pwWldSZmNISnZkRzlqYjJ3dlkyOXVkSEpoWTNRdWNIazZNVFkzQ2lBZ0lDQXZMeUJpYjNoZlpHRjBZU3dnWW05NFgyVjRhWE4wY3lBOUlHOXdMa0p2ZUM1blpYUW9kMkZzYkdWMExtSjVkR1Z6S1FvZ0lDQWdZbTk0WDJkbGRBb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNabGNtbG1hV1ZrWDNCeWIzUnZZMjlzTDJOdmJuUnlZV04wTG5CNU9qRTJPQW9nSUNBZ0x5OGdhV1lnYm05MElHSnZlRjlsZUdsemRITTZDaUFnSUNCaWJub2daMlYwWDNKbFkyOXlaRjlqYjNWdWRGOWhablJsY2w5cFpsOWxiSE5sUURNS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkyWlhKcFptbGxaRjl3Y205MGIyTnZiQzlqYjI1MGNtRmpkQzV3ZVRveE5qa0tJQ0FnSUM4dklISmxkSFZ5YmlCVlNXNTBOalFvTUNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2dwblpYUmZjbVZqYjNKa1gyTnZkVzUwWDJGbWRHVnlYMmx1YkdsdVpXUmZjMjFoY25SZlkyOXVkSEpoWTNSekxuWmxjbWxtYVdWa1gzQnliM1J2WTI5c0xtTnZiblJ5WVdOMExsWmxjbWxtYVdWa1VISnZkRzlqYjJ3dVoyVjBYM0psWTI5eVpGOWpiM1Z1ZEVBM09nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDNabGNtbG1hV1ZrWDNCeWIzUnZZMjlzTDJOdmJuUnlZV04wTG5CNU9qRTFPUzB4TmpBS0lDQWdJQzh2SUNNZzRwU0E0cFNBSUZWMGFXeHBkSGtnNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQTRwU0E0cFNBNHBTQUNpQWdJQ0F2THlCQVlXSnBiV1YwYUc5a0tISmxZV1J2Ym14NVBWUnlkV1VwQ2lBZ0lDQnBkRzlpQ2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dwblpYUmZjbVZqYjNKa1gyTnZkVzUwWDJGbWRHVnlYMmxtWDJWc2MyVkFNem9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OTJaWEpwWm1sbFpGOXdjbTkwYjJOdmJDOWpiMjUwY21GamRDNXdlVG94TnpFS0lDQWdJQzh2SUdOdmRXNTBJRDBnVlVsdWREWTBLREFwQ2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1luVnllU0EwQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZkbVZ5YVdacFpXUmZjSEp2ZEc5amIyd3ZZMjl1ZEhKaFkzUXVjSGs2TVRjeUNpQWdJQ0F2THlCdlptWnpaWFFnUFNCVlNXNTBOalFvTUNrS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmlkWEo1SURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkyWlhKcFptbGxaRjl3Y205MGIyTnZiQzlqYjI1MGNtRmpkQzV3ZVRveE56TUtJQ0FnSUM4dklHUmhkR0ZmYkdWdUlEMGdZbTk0WDJSaGRHRXViR1Z1WjNSb0NpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdZblZ5ZVNBekNncG5aWFJmY21WamIzSmtYMk52ZFc1MFgzZG9hV3hsWDNSdmNFQTBPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzWmxjbWxtYVdWa1gzQnliM1J2WTI5c0wyTnZiblJ5WVdOMExuQjVPakUzTlFvZ0lDQWdMeThnZDJocGJHVWdiMlptYzJWMElEd2daR0YwWVY5c1pXNDZDaUFnSUNCa2FXY2dNUW9nSUNBZ1pHbG5JRE1LSUNBZ0lEd0tJQ0FnSUdKNklHZGxkRjl5WldOdmNtUmZZMjkxYm5SZllXWjBaWEpmZDJocGJHVkFOZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwzWmxjbWxtYVdWa1gzQnliM1J2WTI5c0wyTnZiblJ5WVdOMExuQjVPakUzTmkweE56Y0tJQ0FnSUM4dklDTWdVbVZoWkNCMGFHVWdNaTFpZVhSbElHeGxibWQwYUNCd2NtVm1hWGdLSUNBZ0lDOHZJSEpsWTI5eVpGOXNaVzRnUFNCZllubDBaWE5mZEc5ZmRXbHVkREUyS0c5d0xtVjRkSEpoWTNRb1ltOTRYMlJoZEdFc0lHOW1abk5sZEN3Z01pa3BDaUFnSUNCa2RYQUtJQ0FnSUdScFp5QXlDaUFnSUNCa2RYQUtJQ0FnSUdOdmRtVnlJRElLSUNBZ0lHbHVkR05mTVNBdkx5QXlDaUFnSUNCbGVIUnlZV04wTXdvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMM1psY21sbWFXVmtYM0J5YjNSdlkyOXNMMk52Ym5SeVlXTjBMbkI1T2pZNUxUY3dDaUFnSUNBdkx5QWpJRkJoWkNCMGJ5QTRJR0o1ZEdWeklITnZJR0owYjJrZ2QyOXlhM01nWTI5eWNtVmpkR3g1TGdvZ0lDQWdMeThnY21WMGRYSnVJRzl3TG1KMGIya29iM0F1WW5wbGNtOG9OaWtnS3lCeVlYY3BDaUFnSUNCd2RYTm9hVzUwSURZS0lDQWdJR0o2WlhKdkNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUdKMGIya0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5MlpYSnBabWxsWkY5d2NtOTBiMk52YkM5amIyNTBjbUZqZEM1d2VUb3hOemd0TVRjNUNpQWdJQ0F2THlBaklFRmtkbUZ1WTJVZ2NHRnpkQ0IwYUdVZ2NISmxabWw0SUNzZ2NtVmpiM0prSUdKdlpIa0tJQ0FnSUM4dklHOW1abk5sZENBOUlHOW1abk5sZENBcklGVkpiblEyTkNneUtTQXJJSEpsWTI5eVpGOXNaVzRLSUNBZ0lITjNZWEFLSUNBZ0lHbHVkR05mTVNBdkx5QXlDaUFnSUNBckNpQWdJQ0FyQ2lBZ0lDQmlkWEo1SURJS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTkyWlhKcFptbGxaRjl3Y205MGIyTnZiQzlqYjI1MGNtRmpkQzV3ZVRveE9EQUtJQ0FnSUM4dklHTnZkVzUwSUQwZ1kyOTFiblFnS3lCVlNXNTBOalFvTVNrS0lDQWdJR1JwWnlBekNpQWdJQ0JwYm5Salh6SWdMeThnTVFvZ0lDQWdLd29nSUNBZ1luVnllU0EwQ2lBZ0lDQmlJR2RsZEY5eVpXTnZjbVJmWTI5MWJuUmZkMmhwYkdWZmRHOXdRRFFLQ21kbGRGOXlaV052Y21SZlkyOTFiblJmWVdaMFpYSmZkMmhwYkdWQU5qb0tJQ0FnSUdScFp5QXpDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdmRtVnlhV1pwWldSZmNISnZkRzlqYjJ3dlkyOXVkSEpoWTNRdWNIazZNVFU1TFRFMk1Bb2dJQ0FnTHk4Z0l5RGlsSURpbElBZ1ZYUnBiR2wwZVNEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJRGlsSURpbElEaWxJQUtJQ0FnSUM4dklFQmhZbWx0WlhSb2IyUW9jbVZoWkc5dWJIazlWSEoxWlNrS0lDQWdJR0lnWjJWMFgzSmxZMjl5WkY5amIzVnVkRjloWm5SbGNsOXBibXhwYm1Wa1gzTnRZWEowWDJOdmJuUnlZV04wY3k1MlpYSnBabWxsWkY5d2NtOTBiMk52YkM1amIyNTBjbUZqZEM1V1pYSnBabWxsWkZCeWIzUnZZMjlzTG1kbGRGOXlaV052Y21SZlkyOTFiblJBTndvPSIsImNsZWFyIjoiSTNCeVlXZHRZU0IyWlhKemFXOXVJREV4Q2lOd2NtRm5iV0VnZEhsd1pYUnlZV05ySUdaaGJITmxDZ292THlCaGJHZHZjSGt1WVhKak5DNUJVa00wUTI5dWRISmhZM1F1WTJ4bFlYSmZjM1JoZEdWZmNISnZaM0poYlNncElDMCtJSFZwYm5RMk5Eb0tiV0ZwYmpvS0lDQWdJSEIxYzJocGJuUWdNUW9nSUNBZ2NtVjBkWEp1Q2c9PSJ9LCJieXRlQ29kZSI6eyJhcHByb3ZhbCI6IkN5QUVBQUlCQ0NZQ0JCVWZmSFVBTVJ0QkFDUXhHUlJFTVJoRWdnTUVYV2FCZ3dUbU1oT2RCRytsQUZ3MkdnQ09Bd0FKQUpnQXVBQXhHUlF4R0JRUVF6WWFBVWtpV1NNSVN3RVZTVThDRWtRMkdnSkpJbGtqQ0VzQkZVbFBBaEpFTmhvRFNSVWxFa1EyR2dSSklsa2pDRXNCRlJKRU5ob0ZTUlVsRWtTQkZrOEdDRWtXVndZQ2dBSUFGa3hRVEU4RkNFeFBCRkJNRmxjR0FsQk1VRThEVUU4Q1VFeFFTUlVXVndZQ1RGQXhBRWxPQXIxRkFVRUFFMHhKdlVoTEFoVkxBUWhMQWt6VFR3SzdKRU8vUXYvNk5ob0JTUldCSUJKRXZrRUFEMGxKRlJaWEJnSk1VQ2hNVUxBa1F5bEMvKzRwUndJMkdnRkpGWUVnRWtTK1FBQUlJaFlvVEZDd0pFTWlSUVFpUlFKSkZVVURTd0ZMQXd4QkFCMUpTd0pKVGdJaldJRUdyMHhRRjB3akNBaEZBa3NESkFoRkJFTC8yMHNEUXYvRiIsImNsZWFyIjoiQzRFQlF3PT0ifSwiY29tcGlsZXJJbmZvIjp7ImNvbXBpbGVyIjoicHV5YSIsImNvbXBpbGVyVmVyc2lvbiI6eyJtYWpvciI6NSwibWlub3IiOjcsInBhdGNoIjoxLCJjb21taXRIYXNoIjpudWxsfX0sImV2ZW50cyI6W10sInRlbXBsYXRlVmFyaWFibGVzIjp7fSwic2NyYXRjaFZhcmlhYmxlcyI6e319";
    }

}
