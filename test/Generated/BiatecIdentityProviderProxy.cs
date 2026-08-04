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

namespace BiatecIdentity
{

	
	public class BiatecIdentityProviderProxy : ProxyBase
	{
		
		public BiatecIdentityProviderProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		///<summary>
        ///Initial setup
        ///No_op: CREATE, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		public async Task createApplication (Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {184,68,123,54};
			var result = await base.CallApp(new List<object> {abiHandle}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> createApplication_Transactions (Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {184,68,123,54};
			return await base.MakeTransactionList(new List<object> {abiHandle}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Biatec deploys single identity provider smart contract
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="appBiatecConfigProvider">Biatec amm provider ABI Type is uint64  </param>
		/// <param name="governor"> ABI Type is address  </param>
		/// <param name="verificationSetter"> ABI Type is address  </param>
		/// <param name="engagementSetter"> ABI Type is address  </param>
		public async Task bootstrap (ulong appBiatecConfigProvider,Algorand.Address governor,Algorand.Address verificationSetter,Algorand.Address engagementSetter, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {227,191,92,31};
			var result = await base.CallApp(new List<object> {abiHandle,appBiatecConfigProvider,governor,verificationSetter,engagementSetter}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> bootstrap_Transactions (ulong appBiatecConfigProvider,Algorand.Address governor,Algorand.Address verificationSetter,Algorand.Address engagementSetter, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {227,191,92,31};
			return await base.MakeTransactionList(new List<object> {abiHandle,appBiatecConfigProvider,governor,verificationSetter,engagementSetter}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///addressUdpater from global biatec configuration is allowed to update application
        ///No_op: NEVER, Opt_in: NEVER, Close_out: NEVER, Update_application: CALL, Delete_application: NEVER
        ///</summary>
		/// <param name="appBiatecConfigProvider"> ABI Type is uint64  </param>
		/// <param name="newVersion"> ABI Type is byte[]  </param>
		public async Task updateApplication (ulong appBiatecConfigProvider,byte[] newVersion, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {95,200,133,160};
			var result = await base.CallApp(new List<object> {abiHandle,appBiatecConfigProvider,newVersion}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> updateApplication_Transactions (ulong appBiatecConfigProvider,byte[] newVersion, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {95,200,133,160};
			return await base.MakeTransactionList(new List<object> {abiHandle,appBiatecConfigProvider,newVersion}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="user"> ABI Type is address  </param>
		/// <param name="info"> ABI Type is (uint64,bool,uint64,uint64,uint64,uint64,uint64,bool,string,string,uint64,uint64,uint64,uint64,uint64,uint64,bool)  </param>
		public async Task selfRegistration (Algorand.Address user,SelfRegistrationArgInfo info, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {174,100,193,103};
			var result = await base.CallApp(new List<object> {abiHandle,user,info}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> selfRegistration_Transactions (Algorand.Address user,SelfRegistrationArgInfo info, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {174,100,193,103};
			return await base.MakeTransactionList(new List<object> {abiHandle,user,info}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///This method can set fees, verification class, engagement class .. Only engagementSetter is allowed to execute this method.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="user">User address to set info for ABI Type is address  </param>
		/// <param name="info">Data to be set ABI Type is (uint64,bool,uint64,uint64,uint64,uint64,uint64,bool,string,string,uint64,uint64,uint64,uint64,uint64,uint64,bool)  </param>
		public async Task setInfo (Algorand.Address user,SetInfoArgInfo info, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {164,140,251,188};
			var result = await base.CallApp(new List<object> {abiHandle,user,info}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setInfo_Transactions (Algorand.Address user,SetInfoArgInfo info, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {164,140,251,188};
			return await base.MakeTransactionList(new List<object> {abiHandle,user,info}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///addressExecutiveFee can perfom key registration for this LP pool
///
///
///Only addressExecutiveFee is allowed to execute this method.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="appBiatecConfigProvider"> ABI Type is uint64  </param>
		/// <param name="votePK"> ABI Type is byte[]  </param>
		/// <param name="selectionPK"> ABI Type is byte[]  </param>
		/// <param name="stateProofPK"> ABI Type is byte[]  </param>
		/// <param name="voteFirst"> ABI Type is uint64  </param>
		/// <param name="voteLast"> ABI Type is uint64  </param>
		/// <param name="voteKeyDilution"> ABI Type is uint64  </param>
		/// <param name="fee"> ABI Type is uint64  </param>
		public async Task sendOnlineKeyRegistration (ulong appBiatecConfigProvider,byte[] votePK,byte[] selectionPK,byte[] stateProofPK,ulong voteFirst,ulong voteLast,ulong voteKeyDilution,ulong fee, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {103,145,66,100};
			var result = await base.CallApp(new List<object> {abiHandle,appBiatecConfigProvider,votePK,selectionPK,stateProofPK,voteFirst,voteLast,voteKeyDilution,fee}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> sendOnlineKeyRegistration_Transactions (ulong appBiatecConfigProvider,byte[] votePK,byte[] selectionPK,byte[] stateProofPK,ulong voteFirst,ulong voteLast,ulong voteKeyDilution,ulong fee, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {103,145,66,100};
			return await base.MakeTransactionList(new List<object> {abiHandle,appBiatecConfigProvider,votePK,selectionPK,stateProofPK,voteFirst,voteLast,voteKeyDilution,fee}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Returns user information - fee multiplier, verification class, engagement class ..
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="user">Get info for specific user address ABI Type is address  </param>
		/// <param name="v">Version of the data structure to return ABI Type is uint8  </param>
		public async Task<GetUserreturn> getUser (Algorand.Address user,byte v, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {232,173,24,146};
			var result = await base.CallApp(new List<object> {abiHandle,user,v}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
			throw new Exception("Conversion not implemented"); // <unknown return conversion>

		}

		public async Task<List<Transaction>> getUser_Transactions (Algorand.Address user,byte v, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {232,173,24,146};
			return await base.MakeTransactionList(new List<object> {abiHandle,user,v}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Returns short user information - fee multiplier, verification class, engagement class ..
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="user">Get info for specific user address ABI Type is address  </param>
		/// <param name="v">Version of the data structure to return ABI Type is uint8  </param>
		public async Task<GetUserShortreturn> getUserShort (Algorand.Address user,byte v, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {18,127,251,123};
			var result = await base.CallApp(new List<object> {abiHandle,user,v}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
			throw new Exception("Conversion not implemented"); // <unknown return conversion>

		}

		public async Task<List<Transaction>> getUserShort_Transactions (Algorand.Address user,byte v, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {18,127,251,123};
			return await base.MakeTransactionList(new List<object> {abiHandle,user,v}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///If someone deposits excess assets to this smart contract biatec can use them.
///
///
///Only addressExecutiveFee is allowed to execute this method.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="appBiatecConfigProvider">Biatec config app. Only addressExecutiveFee is allowed to execute this method. ABI Type is uint64  </param>
		/// <param name="asset">Asset to withdraw. If native token, then zero ABI Type is uint64  </param>
		/// <param name="amount">Amount of the asset to be withdrawn ABI Type is uint64  </param>
		public async Task<ulong> withdrawExcessAssets (ulong appBiatecConfigProvider,ulong asset,ulong amount, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {203,162,233,93};
			var result = await base.CallApp(new List<object> {abiHandle,appBiatecConfigProvider,asset,amount}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
			var lastLogBytes = result.Last();
			if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
			var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
			return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

		}

		public async Task<List<Transaction>> withdrawExcessAssets_Transactions (ulong appBiatecConfigProvider,ulong asset,ulong amount, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {203,162,233,93};
			return await base.MakeTransactionList(new List<object> {abiHandle,appBiatecConfigProvider,asset,amount}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		//Initial setup
public class createApplication_Arc4GroupTransaction: ProxyBase
{
	public createApplication_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private createApplication_Arc4GroupTransaction() : base(null,0)  {} 
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {184,68,123,54};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Biatec deploys single identity provider smart contract
public class bootstrap_Arc4GroupTransaction: ProxyBase
{
	public bootstrap_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private bootstrap_Arc4GroupTransaction() : base(null,0)  {} 
//Biatec amm provider
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 appBiatecConfigProvider {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.Address governor {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.Address verificationSetter {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.Address engagementSetter {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {227,191,92,31};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {appBiatecConfigProvider,governor,verificationSetter,engagementSetter}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//addressUdpater from global biatec configuration is allowed to update application
public class updateApplication_Arc4GroupTransaction: ProxyBase
{
	public updateApplication_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private updateApplication_Arc4GroupTransaction() : base(null,0)  {} 
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 appBiatecConfigProvider {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte> newVersion {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {95,200,133,160};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {appBiatecConfigProvider,newVersion}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//
public class selfRegistration_Arc4GroupTransaction: ProxyBase
{
	public selfRegistration_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private selfRegistration_Arc4GroupTransaction() : base(null,0)  {} 
//
	public AVM.ClientGenerator.ABI.ARC4.Types.Address user {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.Tuple info {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Tuple)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("(uint64,bool,uint64,uint64,uint64,uint64,uint64,bool,string,string,uint64,uint64,uint64,uint64,uint64,uint64,bool)");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {174,100,193,103};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {user,info}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//This method can set fees, verification class, engagement class .. Only engagementSetter is allowed to execute this method.
public class setInfo_Arc4GroupTransaction: ProxyBase
{
	public setInfo_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setInfo_Arc4GroupTransaction() : base(null,0)  {} 
//User address to set info for
	public AVM.ClientGenerator.ABI.ARC4.Types.Address user {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
//Data to be set
	public AVM.ClientGenerator.ABI.ARC4.Types.Tuple info {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Tuple)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("(uint64,bool,uint64,uint64,uint64,uint64,uint64,bool,string,string,uint64,uint64,uint64,uint64,uint64,uint64,bool)");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {164,140,251,188};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {user,info}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//addressExecutiveFee can perfom key registration for this LP pool\\n\\n\\nOnly addressExecutiveFee is allowed to execute this method.
public class sendOnlineKeyRegistration_Arc4GroupTransaction: ProxyBase
{
	public sendOnlineKeyRegistration_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private sendOnlineKeyRegistration_Arc4GroupTransaction() : base(null,0)  {} 
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 appBiatecConfigProvider {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte> votePK {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte> selectionPK {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte> stateProofPK {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 voteFirst {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 voteLast {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 voteKeyDilution {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 fee {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {103,145,66,100};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {appBiatecConfigProvider,votePK,selectionPK,stateProofPK,voteFirst,voteLast,voteKeyDilution,fee}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Returns user information - fee multiplier, verification class, engagement class ..
public class getUser_Arc4GroupTransaction: ProxyBase
{
	public getUser_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private getUser_Arc4GroupTransaction() : base(null,0)  {} 
//Get info for specific user address
	public AVM.ClientGenerator.ABI.ARC4.Types.Address user {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
//Version of the data structure to return
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt v {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {232,173,24,146};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {user,v}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Returns short user information - fee multiplier, verification class, engagement class ..
public class getUserShort_Arc4GroupTransaction: ProxyBase
{
	public getUserShort_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private getUserShort_Arc4GroupTransaction() : base(null,0)  {} 
//Get info for specific user address
	public AVM.ClientGenerator.ABI.ARC4.Types.Address user {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
//Version of the data structure to return
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt v {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {18,127,251,123};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {user,v}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//If someone deposits excess assets to this smart contract biatec can use them.\\n\\n\\nOnly addressExecutiveFee is allowed to execute this method.
public class withdrawExcessAssets_Arc4GroupTransaction: ProxyBase
{
	public withdrawExcessAssets_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private withdrawExcessAssets_Arc4GroupTransaction() : base(null,0)  {} 
//Biatec config app. Only addressExecutiveFee is allowed to execute this method.
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 appBiatecConfigProvider {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//Asset to withdraw. If native token, then zero
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 asset {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//Amount of the asset to be withdrawn
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 amount {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {203,162,233,93};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {appBiatecConfigProvider,asset,amount}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


				public struct SelfRegistrationArgInfo
		{
			
			public ulong field0 {get;set;}
			
			public bool field1 {get;set;}
			
			public ulong field2 {get;set;}
			
			public ulong field3 {get;set;}
			
			public ulong field4 {get;set;}
			
			public ulong field5 {get;set;}
			
			public ulong field6 {get;set;}
			
			public bool field7 {get;set;}
			
			public string field8 {get;set;}
			
			public string field9 {get;set;}
			
			public ulong field10 {get;set;}
			
			public ulong field11 {get;set;}
			
			public ulong field12 {get;set;}
			
			public ulong field13 {get;set;}
			
			public ulong field14 {get;set;}
			
			public ulong field15 {get;set;}
			
			public bool field16 {get;set;}
		}

				public struct SetInfoArgInfo
		{
			
			public ulong field0 {get;set;}
			
			public bool field1 {get;set;}
			
			public ulong field2 {get;set;}
			
			public ulong field3 {get;set;}
			
			public ulong field4 {get;set;}
			
			public ulong field5 {get;set;}
			
			public ulong field6 {get;set;}
			
			public bool field7 {get;set;}
			
			public string field8 {get;set;}
			
			public string field9 {get;set;}
			
			public ulong field10 {get;set;}
			
			public ulong field11 {get;set;}
			
			public ulong field12 {get;set;}
			
			public ulong field13 {get;set;}
			
			public ulong field14 {get;set;}
			
			public ulong field15 {get;set;}
			
			public bool field16 {get;set;}
		}

				public struct GetUserreturn
		{
			
			public byte field0 {get;set;}
			
			public ulong field1 {get;set;}
			
			public ulong field2 {get;set;}
			
			public bool field3 {get;set;}
			
			public string field4 {get;set;}
			
			public string field5 {get;set;}
			
			public ulong field6 {get;set;}
			
			public ulong field7 {get;set;}
			
			public ulong field8 {get;set;}
			
			public ulong field9 {get;set;}
			
			public ulong field10 {get;set;}
			
			public ulong field11 {get;set;}
			
			public ulong field12 {get;set;}
			
			public ulong field13 {get;set;}
			
			public bool field14 {get;set;}
			
			public ulong field15 {get;set;}
			
			public ulong field16 {get;set;}
			
			public bool field17 {get;set;}
		}

				public struct GetUserShortreturn
		{
			
			public byte field0 {get;set;}
			
			public ulong field1 {get;set;}
			
			public ulong field2 {get;set;}
			
			public ulong field3 {get;set;}
			
			public bool field4 {get;set;}
		}


		protected override string SourceApproval { get; set; }= "I3ByYWdtYSB2ZXJzaW9uIDEwCmludGNibG9jayAwIDEgMzIgMiA2NCAxMDAwMDAwMDAwIDgxNiA0IDUwIDUyCmJ5dGVjYmxvY2sgMHggMHgwMDAwMDAwMDAwMDAwMDAwIDB4MDAgMHg2OSAweDQyIDB4NzMgMHgxNTFmN2M3NSAweDMwMzAzMDMwMzAzMDMwMzAyZDMwMzAzMDMwMmQzMDMwMzAzMDJkMzAzMDMwMzAyZDMwMzAzMDMwMzAzMDMwMzAzMDMwMzAzMCAiQklBVEVDLUlERU5ULTAxLTAzLTAxIiAweDAwMDAwMDAwNzczNTk0MDAgMHgwMDAwMDAwMDNiOWFjYTAwIDB4NzM2Mzc2NjU3MiAweDY1NjYgMHgwMDY4IDB4MDAwMCAweDY1CgovLyBUaGlzIFRFQUwgd2FzIGdlbmVyYXRlZCBieSBURUFMU2NyaXB0IHYwLjEwNy4wCi8vIGh0dHBzOi8vZ2l0aHViLmNvbS9hbGdvcmFuZGZvdW5kYXRpb24vVEVBTFNjcmlwdAoKLy8gVGhpcyBjb250cmFjdCBpcyBjb21wbGlhbnQgd2l0aCBhbmQvb3IgaW1wbGVtZW50cyB0aGUgZm9sbG93aW5nIEFSQ3M6IFsgQVJDNCBdCgovLyBUaGUgZm9sbG93aW5nIHRlbiBsaW5lcyBvZiBURUFMIGhhbmRsZSBpbml0aWFsIHByb2dyYW0gZmxvdwovLyBUaGlzIHBhdHRlcm4gaXMgdXNlZCB0byBtYWtlIGl0IGVhc3kgZm9yIGFueW9uZSB0byBwYXJzZSB0aGUgc3RhcnQgb2YgdGhlIHByb2dyYW0gYW5kIGRldGVybWluZSBpZiBhIHNwZWNpZmljIGFjdGlvbiBpcyBhbGxvd2VkCi8vIEhlcmUsIGFjdGlvbiByZWZlcnMgdG8gdGhlIE9uQ29tcGxldGUgaW4gY29tYmluYXRpb24gd2l0aCB3aGV0aGVyIHRoZSBhcHAgaXMgYmVpbmcgY3JlYXRlZCBvciBjYWxsZWQKLy8gRXZlcnkgcG9zc2libGUgYWN0aW9uIGZvciB0aGlzIGNvbnRyYWN0IGlzIHJlcHJlc2VudGVkIGluIHRoZSBzd2l0Y2ggc3RhdGVtZW50Ci8vIElmIHRoZSBhY3Rpb24gaXMgbm90IGltcGxlbWVudGVkIGluIHRoZSBjb250cmFjdCwgaXRzIHJlc3BlY3RpdmUgYnJhbmNoIHdpbGwgYmUgIipOT1RfSU1QTEVNRU5URUQiIHdoaWNoIGp1c3QgY29udGFpbnMgImVyciIKdHhuIEFwcGxpY2F0aW9uSUQKIQpwdXNoaW50IDYKKgp0eG4gT25Db21wbGV0aW9uCisKc3dpdGNoICpjYWxsX05vT3AgKk5PVF9JTVBMRU1FTlRFRCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQgKmNhbGxfVXBkYXRlQXBwbGljYXRpb24gKk5PVF9JTVBMRU1FTlRFRCAqY3JlYXRlX05vT3AgKk5PVF9JTVBMRU1FTlRFRCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQgKk5PVF9JTVBMRU1FTlRFRCAqTk9UX0lNUExFTUVOVEVECgoqTk9UX0lNUExFTUVOVEVEOgoJLy8gVGhlIHJlcXVlc3RlZCBhY3Rpb24gaXMgbm90IGltcGxlbWVudGVkIGluIHRoaXMgY29udHJhY3QuIEFyZSB5b3UgdXNpbmcgdGhlIGNvcnJlY3QgT25Db21wbGV0ZT8gRGlkIHlvdSBzZXQgeW91ciBhcHAgSUQ/CgllcnIKCi8vIGNyZWF0ZUFwcGxpY2F0aW9uKCl2b2lkCiphYmlfcm91dGVfY3JlYXRlQXBwbGljYXRpb246CgkvLyBleGVjdXRlIGNyZWF0ZUFwcGxpY2F0aW9uKCl2b2lkCgljYWxsc3ViIGNyZWF0ZUFwcGxpY2F0aW9uCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyBjcmVhdGVBcHBsaWNhdGlvbigpOiB2b2lkCi8vCi8vIEluaXRpYWwgc2V0dXAKY3JlYXRlQXBwbGljYXRpb246Cglwcm90byAwIDAKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIwNwoJLy8gdGhpcy52ZXJzaW9uLnZhbHVlID0gdmVyc2lvbgoJYnl0ZWMgMTEgLy8gICJzY3ZlciIKCWJ5dGVjIDggLy8gIkJJQVRFQy1JREVOVC0wMS0wMy0wMSIKCWFwcF9nbG9iYWxfcHV0CglyZXRzdWIKCi8vIGJvb3RzdHJhcCh1aW50NjQsYWRkcmVzcyxhZGRyZXNzLGFkZHJlc3Mpdm9pZAoqYWJpX3JvdXRlX2Jvb3RzdHJhcDoKCS8vIGVuZ2FnZW1lbnRTZXR0ZXI6IGFkZHJlc3MKCXR4bmEgQXBwbGljYXRpb25BcmdzIDQKCWR1cAoJbGVuCglpbnRjIDIgLy8gMzIKCT09CgoJLy8gYXJndW1lbnQgMCAoZW5nYWdlbWVudFNldHRlcikgZm9yIGJvb3RzdHJhcCBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gdmVyaWZpY2F0aW9uU2V0dGVyOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAzCglkdXAKCWxlbgoJaW50YyAyIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDEgKHZlcmlmaWNhdGlvblNldHRlcikgZm9yIGJvb3RzdHJhcCBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gZ292ZXJub3I6IGFkZHJlc3MKCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCWR1cAoJbGVuCglpbnRjIDIgLy8gMzIKCT09CgoJLy8gYXJndW1lbnQgMiAoZ292ZXJub3IpIGZvciBib290c3RyYXAgbXVzdCBiZSBhIGFkZHJlc3MKCWFzc2VydAoKCS8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWJ0b2kKCgkvLyBleGVjdXRlIGJvb3RzdHJhcCh1aW50NjQsYWRkcmVzcyxhZGRyZXNzLGFkZHJlc3Mpdm9pZAoJY2FsbHN1YiBib290c3RyYXAKCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIGJvb3RzdHJhcChhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQsIGdvdmVybm9yOiBBZGRyZXNzLCB2ZXJpZmljYXRpb25TZXR0ZXI6IEFkZHJlc3MsIGVuZ2FnZW1lbnRTZXR0ZXI6IEFkZHJlc3MpOiB2b2lkCi8vCi8vIEJpYXRlYyBkZXBsb3lzIHNpbmdsZSBpZGVudGl0eSBwcm92aWRlciBzbWFydCBjb250cmFjdAovLyBAcGFyYW0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIgQmlhdGVjIGFtbSBwcm92aWRlcgpib290c3RyYXA6Cglwcm90byA0IDAKCgkvLyBQdXNoIGVtcHR5IGJ5dGVzIGFmdGVyIHRoZSBmcmFtZSBwb2ludGVyIHRvIHJlc2VydmUgc3BhY2UgZm9yIGxvY2FsIHZhcmlhYmxlcwoJYnl0ZWMgMCAvLyAweAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjE1CgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFwcC5jcmVhdG9yLCAnT25seSBjcmVhdG9yIG9mIHRoZSBhcHAgY2FuIHNldCBpdCB1cCcpCgl0eG4gU2VuZGVyCgl0eG5hIEFwcGxpY2F0aW9ucyAwCglhcHBfcGFyYW1zX2dldCBBcHBDcmVhdG9yCglwb3AKCT09CgoJLy8gT25seSBjcmVhdG9yIG9mIHRoZSBhcHAgY2FuIHNldCBpdCB1cAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMTYKCS8vIHRoaXMuYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIudmFsdWUgPSBhcHBCaWF0ZWNDb25maWdQcm92aWRlcgoJYnl0ZWMgNCAvLyAgIkIiCglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglhcHBfZ2xvYmFsX3B1dAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjE3CgkvLyB0aGlzLmdvdmVybm9yLnZhbHVlID0gZ292ZXJub3IKCXB1c2hieXRlcyAweDY3IC8vICJnIgoJZnJhbWVfZGlnIC0yIC8vIGdvdmVybm9yOiBBZGRyZXNzCglhcHBfZ2xvYmFsX3B1dAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjE4CgkvLyB0aGlzLnZlcmlmaWNhdGlvblNldHRlci52YWx1ZSA9IHZlcmlmaWNhdGlvblNldHRlcgoJcHVzaGJ5dGVzIDB4NzYgLy8gInYiCglmcmFtZV9kaWcgLTMgLy8gdmVyaWZpY2F0aW9uU2V0dGVyOiBBZGRyZXNzCglhcHBfZ2xvYmFsX3B1dAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjE5CgkvLyB0aGlzLmVuZ2FnZW1lbnRTZXR0ZXIudmFsdWUgPSBlbmdhZ2VtZW50U2V0dGVyCglieXRlYyAxNSAvLyAgImUiCglmcmFtZV9kaWcgLTQgLy8gZW5nYWdlbWVudFNldHRlcjogQWRkcmVzcwoJYXBwX2dsb2JhbF9wdXQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIyMQoJLy8gcGF1c2VkID0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3MnKSBhcyB1aW50NjQKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDUgLy8gICJzIgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdzJykKCWFzc2VydAoJZnJhbWVfYnVyeSAwIC8vIHBhdXNlZDogdWludDY0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMjIKCS8vIGFzc2VydChwYXVzZWQgPT09IDAsICdFUlJfUEFVU0VEJykKCWZyYW1lX2RpZyAwIC8vIHBhdXNlZDogdWludDY0CglpbnRjIDAgLy8gMAoJPT0KCgkvLyBFUlJfUEFVU0VECglhc3NlcnQKCXJldHN1YgoKLy8gdXBkYXRlQXBwbGljYXRpb24odWludDY0LGJ5dGVbXSl2b2lkCiphYmlfcm91dGVfdXBkYXRlQXBwbGljYXRpb246CgkvLyBuZXdWZXJzaW9uOiBieXRlW10KCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCWV4dHJhY3QgMiAwCgoJLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJYnRvaQoKCS8vIGV4ZWN1dGUgdXBkYXRlQXBwbGljYXRpb24odWludDY0LGJ5dGVbXSl2b2lkCgljYWxsc3ViIHVwZGF0ZUFwcGxpY2F0aW9uCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyB1cGRhdGVBcHBsaWNhdGlvbihhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQsIG5ld1ZlcnNpb246IGJ5dGVzKTogdm9pZAovLwovLyBhZGRyZXNzVWRwYXRlciBmcm9tIGdsb2JhbCBiaWF0ZWMgY29uZmlndXJhdGlvbiBpcyBhbGxvd2VkIHRvIHVwZGF0ZSBhcHBsaWNhdGlvbgp1cGRhdGVBcHBsaWNhdGlvbjoKCXByb3RvIDIgMAoKCS8vIFB1c2ggZW1wdHkgYnl0ZXMgYWZ0ZXIgdGhlIGZyYW1lIHBvaW50ZXIgdG8gcmVzZXJ2ZSBzcGFjZSBmb3IgbG9jYWwgdmFyaWFibGVzCglieXRlYyAwIC8vIDB4CglkdXAKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIyOQoJLy8gYXNzZXJ0KGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyID09PSB0aGlzLmFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLnZhbHVlLCAnQ29uZmlndXJhdGlvbiBhcHAgZG9lcyBub3QgbWF0Y2gnKQoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYnl0ZWMgNCAvLyAgIkIiCglhcHBfZ2xvYmFsX2dldAoJPT0KCgkvLyBDb25maWd1cmF0aW9uIGFwcCBkb2VzIG5vdCBtYXRjaAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMzAKCS8vIGFkZHJlc3NVZHBhdGVyID0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3UnKSBhcyBBZGRyZXNzCglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglwdXNoYnl0ZXMgMHg3NSAvLyAidSIKCWFwcF9nbG9iYWxfZ2V0X2V4CgoJLy8gZ2xvYmFsIHN0YXRlIHZhbHVlIGRvZXMgbm90IGV4aXN0OiBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgndScpCglhc3NlcnQKCWZyYW1lX2J1cnkgMCAvLyBhZGRyZXNzVWRwYXRlcjogYWRkcmVzcwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjMxCgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSBhZGRyZXNzVWRwYXRlciwgJ09ubHkgYWRkcmVzc1VkcGF0ZXIgc2V0dXAgaW4gdGhlIGNvbmZpZyBjYW4gdXBkYXRlIGFwcGxpY2F0aW9uJykKCXR4biBTZW5kZXIKCWZyYW1lX2RpZyAwIC8vIGFkZHJlc3NVZHBhdGVyOiBhZGRyZXNzCgk9PQoKCS8vIE9ubHkgYWRkcmVzc1VkcGF0ZXIgc2V0dXAgaW4gdGhlIGNvbmZpZyBjYW4gdXBkYXRlIGFwcGxpY2F0aW9uCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIzMgoJLy8gcGF1c2VkID0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3MnKSBhcyB1aW50NjQKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDUgLy8gICJzIgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdzJykKCWFzc2VydAoJZnJhbWVfYnVyeSAxIC8vIHBhdXNlZDogdWludDY0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMzMKCS8vIGFzc2VydChwYXVzZWQgPT09IDAsICdFUlJfUEFVU0VEJykKCWZyYW1lX2RpZyAxIC8vIHBhdXNlZDogdWludDY0CglpbnRjIDAgLy8gMAoJPT0KCgkvLyBFUlJfUEFVU0VECglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIzNAoJLy8gbG9nKHZlcnNpb24pCglieXRlYyA4IC8vICJCSUFURUMtSURFTlQtMDEtMDMtMDEiCglsb2cKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIzNQoJLy8gbG9nKG5ld1ZlcnNpb24pCglmcmFtZV9kaWcgLTIgLy8gbmV3VmVyc2lvbjogYnl0ZXMKCWxvZwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjM2CgkvLyB0aGlzLnZlcnNpb24udmFsdWUgPSBuZXdWZXJzaW9uCglieXRlYyAxMSAvLyAgInNjdmVyIgoJZnJhbWVfZGlnIC0yIC8vIG5ld1ZlcnNpb246IGJ5dGVzCglhcHBfZ2xvYmFsX3B1dAoJcmV0c3ViCgovLyBzZWxmUmVnaXN0cmF0aW9uKGFkZHJlc3MsKHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCxzdHJpbmcsc3RyaW5nLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpKXZvaWQKKmFiaV9yb3V0ZV9zZWxmUmVnaXN0cmF0aW9uOgoJLy8gaW5mbzogKHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCxzdHJpbmcsc3RyaW5nLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAyCgoJLy8gdXNlcjogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJZHVwCglsZW4KCWludGMgMiAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAxICh1c2VyKSBmb3Igc2VsZlJlZ2lzdHJhdGlvbiBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gZXhlY3V0ZSBzZWxmUmVnaXN0cmF0aW9uKGFkZHJlc3MsKHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCxzdHJpbmcsc3RyaW5nLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpKXZvaWQKCWNhbGxzdWIgc2VsZlJlZ2lzdHJhdGlvbgoJaW50YyAxIC8vIDEKCXJldHVybgoKLy8gc2VsZlJlZ2lzdHJhdGlvbih1c2VyOiBBZGRyZXNzLCBpbmZvOiBJZGVudGl0eUluZm8pOiB2b2lkCnNlbGZSZWdpc3RyYXRpb246Cglwcm90byAyIDAKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI0MAoJLy8gYXNzZXJ0KCF0aGlzLmlkZW50aXRpZXModXNlcikuZXhpc3RzLCAnU2VsZiByZWdpc3RyYXRpb24gY2Fubm90IGJlIGV4ZWN1dGVkIGlmIGFkZHJlc3MgaXMgYWxyZWFkeSByZWdpc3RlcmVkJykKCWJ5dGVjIDMgLy8gICJpIgoJZnJhbWVfZGlnIC0xIC8vIHVzZXI6IEFkZHJlc3MKCWNvbmNhdAoJYm94X2xlbgoJc3dhcAoJcG9wCgkhCgoJLy8gU2VsZiByZWdpc3RyYXRpb24gY2Fubm90IGJlIGV4ZWN1dGVkIGlmIGFkZHJlc3MgaXMgYWxyZWFkeSByZWdpc3RlcmVkCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI0MwoJLy8gYXNzZXJ0KGluZm8udmVyaWZpY2F0aW9uU3RhdHVzID09PSAxLCAnVmVyaWZpY2F0aW9uIHN0YXR1cyBtdXN0IGJlIGVtcHR5JykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDQxIDgKCWJ0b2kKCWludGMgMSAvLyAxCgk9PQoKCS8vIFZlcmlmaWNhdGlvbiBzdGF0dXMgbXVzdCBiZSBlbXB0eQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyNDUKCS8vIGFzc2VydChpbmZvLnZlcmlmaWNhdGlvbkNsYXNzID09PSAwLCAndmVyaWZpY2F0aW9uQ2xhc3MgbXVzdCBlcXVhbCB0byAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDAgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gdmVyaWZpY2F0aW9uQ2xhc3MgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI0OAoJLy8gYXNzZXJ0KGluZm8ucGVyc29uVVVJRCA9PT0gJzAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCcsICdwZXJzb25VVUlEIG11c3QgZXF1YWwgdG8gMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDggLy8gNTAKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXVuY292ZXIgMgoJZXh0cmFjdF91aW50MTYKCWR1cCAvLyBkdXBsaWNhdGUgc3RhcnQgb2YgZWxlbWVudAoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJc3dhcAoJZXh0cmFjdF91aW50MTYgLy8gZ2V0IG51bWJlciBvZiBlbGVtZW50cwoJaW50YyAxIC8vICBnZXQgdHlwZSBsZW5ndGgKCSogLy8gbXVsdGlwbHkgYnkgdHlwZSBsZW5ndGgKCWludGMgMyAvLyAyCgkrIC8vIGFkZCB0d28gZm9yIGxlbmd0aAoJZXh0cmFjdDMKCWV4dHJhY3QgMiAwCglieXRlYyA3IC8vICAiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIgoJPT0KCgkvLyBwZXJzb25VVUlEIG11c3QgZXF1YWwgdG8gMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI1MAoJLy8gYXNzZXJ0KGluZm8ubGVnYWxFbnRpdHlVVUlEID09PSAnMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwJywgJ2xlZ2FsRW50aXR5VVVJRCBtdXN0IGVxdWFsIHRvIDAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCcpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA5IC8vIDUyCglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cgl1bmNvdmVyIDIKCWV4dHJhY3RfdWludDE2CglkdXAgLy8gZHVwbGljYXRlIHN0YXJ0IG9mIGVsZW1lbnQKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXN3YXAKCWV4dHJhY3RfdWludDE2IC8vIGdldCBudW1iZXIgb2YgZWxlbWVudHMKCWludGMgMSAvLyAgZ2V0IHR5cGUgbGVuZ3RoCgkqIC8vIG11bHRpcGx5IGJ5IHR5cGUgbGVuZ3RoCglpbnRjIDMgLy8gMgoJKyAvLyBhZGQgdHdvIGZvciBsZW5ndGgKCWV4dHJhY3QzCglleHRyYWN0IDIgMAoJYnl0ZWMgNyAvLyAgIjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIKCT09CgoJLy8gbGVnYWxFbnRpdHlVVUlEIG11c3QgZXF1YWwgdG8gMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI1MgoJLy8gYXNzZXJ0KGluZm8uYmlhdGVjRW5nYWdlbWVudFBvaW50cyA9PT0gMCwgJ2JpYXRlY0VuZ2FnZW1lbnRQb2ludHMgbXVzdCBlcXVhbCB0byAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDU0IDgKCWJ0b2kKCWludGMgMCAvLyAwCgk9PQoKCS8vIGJpYXRlY0VuZ2FnZW1lbnRQb2ludHMgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI1NAoJLy8gYXNzZXJ0KGluZm8uYmlhdGVjRW5nYWdlbWVudFJhbmsgPT09IDAsICdiaWF0ZWNFbmdhZ2VtZW50UmFuayBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgNjIgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gYmlhdGVjRW5nYWdlbWVudFJhbmsgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI1NgoJLy8gYXNzZXJ0KGluZm8uYXZtRW5nYWdlbWVudFBvaW50cyA9PT0gMCwgJ2F2bUVuZ2FnZW1lbnRQb2ludHMgbXVzdCBlcXVhbCB0byAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDcwIDgKCWJ0b2kKCWludGMgMCAvLyAwCgk9PQoKCS8vIGF2bUVuZ2FnZW1lbnRQb2ludHMgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI1OAoJLy8gYXNzZXJ0KGluZm8uYXZtRW5nYWdlbWVudFJhbmsgPT09IDAsICdhdm1FbmdhZ2VtZW50UmFuayBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgNzggOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gYXZtRW5nYWdlbWVudFJhbmsgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI2MAoJLy8gYXNzZXJ0KGluZm8udHJhZGluZ0VuZ2FnZW1lbnRQb2ludHMgPT09IDAsICd0cmFkaW5nRW5nYWdlbWVudFBvaW50cyBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgODYgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gdHJhZGluZ0VuZ2FnZW1lbnRQb2ludHMgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI2MgoJLy8gYXNzZXJ0KGluZm8udHJhZGluZ0VuZ2FnZW1lbnRSYW5rID09PSAwLCAndHJhZGluZ0VuZ2FnZW1lbnRSYW5rIG11c3QgZXF1YWwgdG8gMCcpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA5NCA4CglidG9pCglpbnRjIDAgLy8gMAoJPT0KCgkvLyB0cmFkaW5nRW5nYWdlbWVudFJhbmsgbXVzdCBlcXVhbCB0byAwCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI2NAoJLy8gYXNzZXJ0KGluZm8uaXNMb2NrZWQgPT09IGZhbHNlLCAnaXNMb2NrZWQgbXVzdCBlcXVhbCB0byBmYWxzZScpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA0IC8vIDY0CglnZXRiaXQKCWludGMgMCAvLyAwCgk9PQoKCS8vIGlzTG9ja2VkIG11c3QgZXF1YWwgdG8gZmFsc2UKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjY2CgkvLyBhc3NlcnQoaW5mby5reWNFeHBpcmF0aW9uID09PSAwLCAna3ljRXhwaXJhdGlvbiBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMjUgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8ga3ljRXhwaXJhdGlvbiBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjY4CgkvLyBhc3NlcnQoaW5mby5pbnZlc3RvckZvckV4cGlyYXRpb24gPT09IDAsICdpbnZlc3RvckZvckV4cGlyYXRpb24gbXVzdCBlcXVhbCB0byAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDMzIDgKCWJ0b2kKCWludGMgMCAvLyAwCgk9PQoKCS8vIGludmVzdG9yRm9yRXhwaXJhdGlvbiBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjcwCgkvLyBhc3NlcnQoaW5mby5pc1Byb2Zlc3Npb25hbEludmVzdG9yID09PSBmYWxzZSwgJ2lzUHJvZmVzc2lvbmFsSW52ZXN0b3IgbXVzdCBlcXVhbCB0byBmYWxzZScpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA2IC8vIDgxNgoJZ2V0Yml0CglpbnRjIDAgLy8gMAoJPT0KCgkvLyBpc1Byb2Zlc3Npb25hbEludmVzdG9yIG11c3QgZXF1YWwgdG8gZmFsc2UKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjcyCgkvLyBhc3NlcnQoaW5mby5mZWVNdWx0aXBsaWVyQmFzZSA9PT0gU0NBTEUsICdGZWVNdWx0aXBsaWVyQmFzZSBtdXN0IGJlIHNldCBwcm9wZXJseScpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCAxNyA4CglidG9pCglpbnRjIDUgLy8gMTAwMDAwMDAwMAoJPT0KCgkvLyBGZWVNdWx0aXBsaWVyQmFzZSBtdXN0IGJlIHNldCBwcm9wZXJseQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyNzMKCS8vIGFzc2VydChpbmZvLmZlZU11bHRpcGxpZXIgPT09ICgoMiAqIFNDQUxFKSBhcyB1aW50NjQpLCAnSW5pdGlhbCBmZWUgbXVsdGlwbGllciBtdXN0IGJlIHNldCB0byAyICogU0NBTEUnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgOSA4CglidG9pCglwdXNoaW50IDIwMDAwMDAwMDAKCT09CgoJLy8gSW5pdGlhbCBmZWUgbXVsdGlwbGllciBtdXN0IGJlIHNldCB0byAyICogU0NBTEUKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6Mjc0CgkvLyB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUgPSBpbmZvCglieXRlYyAzIC8vICAiaSIKCWZyYW1lX2RpZyAtMSAvLyB1c2VyOiBBZGRyZXNzCgljb25jYXQKCWR1cAoJYm94X2RlbAoJcG9wCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglib3hfcHV0CglyZXRzdWIKCi8vIHNldEluZm8oYWRkcmVzcywodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkpdm9pZAoqYWJpX3JvdXRlX3NldEluZm86CgkvLyBpbmZvOiAodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkKCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCgkvLyB1c2VyOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglkdXAKCWxlbgoJaW50YyAyIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDEgKHVzZXIpIGZvciBzZXRJbmZvIG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyBleGVjdXRlIHNldEluZm8oYWRkcmVzcywodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkpdm9pZAoJY2FsbHN1YiBzZXRJbmZvCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyBzZXRJbmZvKHVzZXI6IEFkZHJlc3MsIGluZm86IElkZW50aXR5SW5mbyk6IHZvaWQKLy8KLy8gVGhpcyBtZXRob2QgY2FuIHNldCBmZWVzLCB2ZXJpZmljYXRpb24gY2xhc3MsIGVuZ2FnZW1lbnQgY2xhc3MgLi4gT25seSBlbmdhZ2VtZW50U2V0dGVyIGlzIGFsbG93ZWQgdG8gZXhlY3V0ZSB0aGlzIG1ldGhvZC4KLy8gQHBhcmFtIHVzZXIgVXNlciBhZGRyZXNzIHRvIHNldCBpbmZvIGZvcgovLyBAcGFyYW0gaW5mbyBEYXRhIHRvIGJlIHNldApzZXRJbmZvOgoJcHJvdG8gMiAwCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyODIKCS8vIGFzc2VydCh0aGlzLnR4bi5zZW5kZXIgPT09IHRoaXMuZW5nYWdlbWVudFNldHRlci52YWx1ZSkKCXR4biBTZW5kZXIKCWJ5dGVjIDE1IC8vICAiZSIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyODMKCS8vIGFzc2VydChpbmZvLmZlZU11bHRpcGxpZXJCYXNlID09PSBTQ0FMRSwgJ0ZlZU11bHRpcGxpZXJCYXNlIG11c3QgYmUgc2V0IHByb3Blcmx5JykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDE3IDgKCWJ0b2kKCWludGMgNSAvLyAxMDAwMDAwMDAwCgk9PQoKCS8vIEZlZU11bHRpcGxpZXJCYXNlIG11c3QgYmUgc2V0IHByb3Blcmx5Cglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI4NAoJLy8gYXNzZXJ0KGluZm8udmVyaWZpY2F0aW9uQ2xhc3MgPD0gNCwgJ1ZlcmlmaWNhdGlvbiBjbGFzcyBvdXQgb2YgYm91bmRzJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDAgOAoJYnRvaQoJaW50YyA3IC8vIDQKCTw9CgoJLy8gVmVyaWZpY2F0aW9uIGNsYXNzIG91dCBvZiBib3VuZHMKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6Mjg1CgkvLyB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUgPSBpbmZvCglieXRlYyAzIC8vICAiaSIKCWZyYW1lX2RpZyAtMSAvLyB1c2VyOiBBZGRyZXNzCgljb25jYXQKCWR1cAoJYm94X2RlbAoJcG9wCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglib3hfcHV0CglyZXRzdWIKCi8vIHNlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24odWludDY0LGJ5dGVbXSxieXRlW10sYnl0ZVtdLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCl2b2lkCiphYmlfcm91dGVfc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbjoKCS8vIGZlZTogdWludDY0Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyA4CglidG9pCgoJLy8gdm90ZUtleURpbHV0aW9uOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDcKCWJ0b2kKCgkvLyB2b3RlTGFzdDogdWludDY0Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyA2CglidG9pCgoJLy8gdm90ZUZpcnN0OiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDUKCWJ0b2kKCgkvLyBzdGF0ZVByb29mUEs6IGJ5dGVbXQoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgNAoJZXh0cmFjdCAyIDAKCgkvLyBzZWxlY3Rpb25QSzogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAzCglleHRyYWN0IDIgMAoKCS8vIHZvdGVQSzogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAyCglleHRyYWN0IDIgMAoKCS8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWJ0b2kKCgkvLyBleGVjdXRlIHNlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24odWludDY0LGJ5dGVbXSxieXRlW10sYnl0ZVtdLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCl2b2lkCgljYWxsc3ViIHNlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24KCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIHNlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24oYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElELCB2b3RlUEs6IGJ5dGVzLCBzZWxlY3Rpb25QSzogYnl0ZXMsIHN0YXRlUHJvb2ZQSzogYnl0ZXMsIHZvdGVGaXJzdDogdWludDY0LCB2b3RlTGFzdDogdWludDY0LCB2b3RlS2V5RGlsdXRpb246IHVpbnQ2NCwgZmVlOiB1aW50NjQpOiB2b2lkCi8vCi8vIGFkZHJlc3NFeGVjdXRpdmVGZWUgY2FuIHBlcmZvbSBrZXkgcmVnaXN0cmF0aW9uIGZvciB0aGlzIExQIHBvb2wKLy8KLy8gT25seSBhZGRyZXNzRXhlY3V0aXZlRmVlIGlzIGFsbG93ZWQgdG8gZXhlY3V0ZSB0aGlzIG1ldGhvZC4Kc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbjoKCXByb3RvIDggMAoKCS8vIFB1c2ggZW1wdHkgYnl0ZXMgYWZ0ZXIgdGhlIGZyYW1lIHBvaW50ZXIgdG8gcmVzZXJ2ZSBzcGFjZSBmb3IgbG9jYWwgdmFyaWFibGVzCglieXRlYyAwIC8vIDB4CglkdXAKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI5NAoJLy8gYXNzZXJ0KGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyID09PSB0aGlzLmFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLnZhbHVlLCAnQ29uZmlndXJhdGlvbiBhcHAgZG9lcyBub3QgbWF0Y2gnKQoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYnl0ZWMgNCAvLyAgIkIiCglhcHBfZ2xvYmFsX2dldAoJPT0KCgkvLyBDb25maWd1cmF0aW9uIGFwcCBkb2VzIG5vdCBtYXRjaAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyOTUKCS8vIGFkZHJlc3NFeGVjdXRpdmVGZWUgPSBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgnZWYnKSBhcyBBZGRyZXNzCglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglieXRlYyAxMiAvLyAgImVmIgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdlZicpCglhc3NlcnQKCWZyYW1lX2J1cnkgMCAvLyBhZGRyZXNzRXhlY3V0aXZlRmVlOiBhZGRyZXNzCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyOTYKCS8vIGFzc2VydCh0aGlzLnR4bi5zZW5kZXIgPT09IGFkZHJlc3NFeGVjdXRpdmVGZWUsICdPbmx5IGZlZSBleGVjdXRvciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB0YWtlIHRoZSBjb2xsZWN0ZWQgZmVlcycpCgl0eG4gU2VuZGVyCglmcmFtZV9kaWcgMCAvLyBhZGRyZXNzRXhlY3V0aXZlRmVlOiBhZGRyZXNzCgk9PQoKCS8vIE9ubHkgZmVlIGV4ZWN1dG9yIHNldHVwIGluIHRoZSBjb25maWcgY2FuIHRha2UgdGhlIGNvbGxlY3RlZCBmZWVzCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI5NwoJLy8gcGF1c2VkID0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3MnKSBhcyB1aW50NjQKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDUgLy8gICJzIgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdzJykKCWFzc2VydAoJZnJhbWVfYnVyeSAxIC8vIHBhdXNlZDogdWludDY0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyOTgKCS8vIGFzc2VydChwYXVzZWQgPT09IDAsICdFUlJfUEFVU0VEJykKCWZyYW1lX2RpZyAxIC8vIHBhdXNlZDogdWludDY0CglpbnRjIDAgLy8gMAoJPT0KCgkvLyBFUlJfUEFVU0VECglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI5OQoJLy8gc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbih7CgkvLyAgICAgICBzZWxlY3Rpb25QSzogc2VsZWN0aW9uUEssCgkvLyAgICAgICBzdGF0ZVByb29mUEs6IHN0YXRlUHJvb2ZQSywKCS8vICAgICAgIHZvdGVGaXJzdDogdm90ZUZpcnN0LAoJLy8gICAgICAgdm90ZUtleURpbHV0aW9uOiB2b3RlS2V5RGlsdXRpb24sCgkvLyAgICAgICB2b3RlTGFzdDogdm90ZUxhc3QsCgkvLyAgICAgICB2b3RlUEs6IHZvdGVQSywKCS8vICAgICAgIGZlZTogZmVlLAoJLy8gICAgIH0pCglpdHhuX2JlZ2luCglpbnRjIDMgLy8gIGtleXJlZwoJaXR4bl9maWVsZCBUeXBlRW51bQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzAwCgkvLyBzZWxlY3Rpb25QSzogc2VsZWN0aW9uUEsKCWZyYW1lX2RpZyAtMyAvLyBzZWxlY3Rpb25QSzogYnl0ZXMKCWl0eG5fZmllbGQgU2VsZWN0aW9uUEsKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMwMQoJLy8gc3RhdGVQcm9vZlBLOiBzdGF0ZVByb29mUEsKCWZyYW1lX2RpZyAtNCAvLyBzdGF0ZVByb29mUEs6IGJ5dGVzCglpdHhuX2ZpZWxkIFN0YXRlUHJvb2ZQSwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzAyCgkvLyB2b3RlRmlyc3Q6IHZvdGVGaXJzdAoJZnJhbWVfZGlnIC01IC8vIHZvdGVGaXJzdDogdWludDY0CglpdHhuX2ZpZWxkIFZvdGVGaXJzdAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzAzCgkvLyB2b3RlS2V5RGlsdXRpb246IHZvdGVLZXlEaWx1dGlvbgoJZnJhbWVfZGlnIC03IC8vIHZvdGVLZXlEaWx1dGlvbjogdWludDY0CglpdHhuX2ZpZWxkIFZvdGVLZXlEaWx1dGlvbgoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzA0CgkvLyB2b3RlTGFzdDogdm90ZUxhc3QKCWZyYW1lX2RpZyAtNiAvLyB2b3RlTGFzdDogdWludDY0CglpdHhuX2ZpZWxkIFZvdGVMYXN0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMDUKCS8vIHZvdGVQSzogdm90ZVBLCglmcmFtZV9kaWcgLTIgLy8gdm90ZVBLOiBieXRlcwoJaXR4bl9maWVsZCBWb3RlUEsKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMwNgoJLy8gZmVlOiBmZWUKCWZyYW1lX2RpZyAtOCAvLyBmZWU6IHVpbnQ2NAoJaXR4bl9maWVsZCBGZWUKCgkvLyBTdWJtaXQgaW5uZXIgdHJhbnNhY3Rpb24KCWl0eG5fc3VibWl0CglyZXRzdWIKCi8vIGdldFVzZXIoYWRkcmVzcyx1aW50OCkodWludDgsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsYm9vbCkKKmFiaV9yb3V0ZV9nZXRVc2VyOgoJLy8gVGhlIEFCSSByZXR1cm4gcHJlZml4CglieXRlYyA2IC8vIDB4MTUxZjdjNzUKCgkvLyB2OiB1aW50OAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMgoJZHVwCglsZW4KCWludGMgMSAvLyAxCgk9PQoKCS8vIGFyZ3VtZW50IDAgKHYpIGZvciBnZXRVc2VyIG11c3QgYmUgYSB1aW50OAoJYXNzZXJ0CglidG9pCgoJLy8gdXNlcjogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJZHVwCglsZW4KCWludGMgMiAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAxICh1c2VyKSBmb3IgZ2V0VXNlciBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gZXhlY3V0ZSBnZXRVc2VyKGFkZHJlc3MsdWludDgpKHVpbnQ4LHVpbnQ2NCx1aW50NjQsYm9vbCxzdHJpbmcsc3RyaW5nLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCx1aW50NjQsdWludDY0LGJvb2wpCgljYWxsc3ViIGdldFVzZXIKCWNvbmNhdAoJbG9nCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyBnZXRVc2VyKHVzZXI6IEFkZHJlc3MsIHY6IHVpbnQ4KTogVXNlckluZm9WMQovLwovLyBSZXR1cm5zIHVzZXIgaW5mb3JtYXRpb24gLSBmZWUgbXVsdGlwbGllciwgdmVyaWZpY2F0aW9uIGNsYXNzLCBlbmdhZ2VtZW50IGNsYXNzIC4uCi8vCi8vIEBwYXJhbSB1c2VyIEdldCBpbmZvIGZvciBzcGVjaWZpYyB1c2VyIGFkZHJlc3MKLy8gQHBhcmFtIHYgVmVyc2lvbiBvZiB0aGUgZGF0YSBzdHJ1Y3R1cmUgdG8gcmV0dXJuCmdldFVzZXI6Cglwcm90byAyIDEKCgkvLyBQdXNoIGVtcHR5IGJ5dGVzIGFmdGVyIHRoZSBmcmFtZSBwb2ludGVyIHRvIHJlc2VydmUgc3BhY2UgZm9yIGxvY2FsIHZhcmlhYmxlcwoJYnl0ZWMgMCAvLyAweAoJZHVwbiAyCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMTgKCS8vIGFzc2VydCh2ID09PSAxLCAiQ3VycmVudGx5IHN1cHBvcnRlZCB2ZXJzaW9uIG9mIHRoZSBkYXRhIHN0cnVjdHVyZSBpcyAnMSciKQoJZnJhbWVfZGlnIC0yIC8vIHY6IHVpbnQ4CglpbnRjIDEgLy8gMQoJPT0KCgkvLyBDdXJyZW50bHkgc3VwcG9ydGVkIHZlcnNpb24gb2YgdGhlIGRhdGEgc3RydWN0dXJlIGlzICcxJwoJYXNzZXJ0CgoJLy8gKmlmMF9jb25kaXRpb24KCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzE5CgkvLyAhdGhpcy5pZGVudGl0aWVzKHVzZXIpLmV4aXN0cwoJYnl0ZWMgMyAvLyAgImkiCglmcmFtZV9kaWcgLTEgLy8gdXNlcjogQWRkcmVzcwoJY29uY2F0Cglib3hfbGVuCglzd2FwCglwb3AKCSEKCWJ6ICppZjBfZW5kCgoJLy8gKmlmMF9jb25zZXF1ZW50CgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMyMAoJLy8gcmV0Tm9JZGVudGl0eTogVXNlckluZm9WMSA9IHsKCS8vICAgICAgICAgdmVyc2lvbjogdiwKCS8vICAgICAgICAgYmFzZTogU0NBTEUgYXMgdWludDY0LAoJLy8gICAgICAgICBmZWVNdWx0aXBsaWVyOiAoMiAqIFNDQUxFKSBhcyB1aW50NjQsCgkvLyAgICAgICAgIGlzTG9ja2VkOiBmYWxzZSwKCS8vICAgICAgICAgdmVyaWZpY2F0aW9uQ2xhc3M6IDAsCgkvLyAgICAgICAgIHZlcmlmaWNhdGlvblN0YXR1czogMCwKCS8vICAgICAgICAgYmlhdGVjRW5nYWdlbWVudFBvaW50czogMCwKCS8vICAgICAgICAgYmlhdGVjRW5nYWdlbWVudFJhbms6IDAsCgkvLyAgICAgICAgIGF2bUVuZ2FnZW1lbnRQb2ludHM6IDAsCgkvLyAgICAgICAgIGF2bUVuZ2FnZW1lbnRSYW5rOiAwLAoJLy8gICAgICAgICB0cmFkaW5nRW5nYWdlbWVudFBvaW50czogMCwKCS8vICAgICAgICAgdHJhZGluZ0VuZ2FnZW1lbnRSYW5rOiAwLAoJLy8gICAgICAgICBreWNFeHBpcmF0aW9uOiAwLAoJLy8gICAgICAgICBpbnZlc3RvckZvckV4cGlyYXRpb246IDAsCgkvLyAgICAgICAgIGlzUHJvZmVzc2lvbmFsSW52ZXN0b3I6IGZhbHNlLAoJLy8gICAgICAgICBpc0NvbXBhbnk6IGZhbHNlLAoJLy8gICAgICAgICBwZXJzb25VVUlEOiAnJywKCS8vICAgICAgICAgbGVnYWxFbnRpdHlVVUlEOiAnJywKCS8vICAgICAgIH0KCWJ5dGVjIDAgLy8gIGluaXRpYWwgaGVhZAoJYnl0ZWMgMCAvLyAgaW5pdGlhbCB0YWlsCglieXRlYyAxMyAvLyAgaW5pdGlhbCBoZWFkIG9mZnNldAoJZnJhbWVfZGlnIC0yIC8vIHY6IHVpbnQ4CglpdG9iCglleHRyYWN0IDcgMQoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglkdXAKCXNldGJpdAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMTQgLy8gMHgwMDAwCgljYWxsc3ViICpwcm9jZXNzX2R5bmFtaWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMTQgLy8gMHgwMDAwCgljYWxsc3ViICpwcm9jZXNzX2R5bmFtaWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAxIC8vIDB4MDAwMDAwMDAwMDAwMDAwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAxIC8vIDB4MDAwMDAwMDAwMDAwMDAwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgOSAvLyAweDAwMDAwMDAwNzczNTk0MDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEwIC8vIDB4MDAwMDAwMDAzYjlhY2EwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMiAvLyAweDAwCglpbnRjIDAgLy8gMAoJZHVwCglzZXRiaXQKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAxIC8vIDB4MDAwMDAwMDAwMDAwMDAwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMiAvLyAweDAwCglpbnRjIDAgLy8gMAoJZHVwCglzZXRiaXQKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCXBvcCAvLyBwb3AgaGVhZCBvZmZzZXQKCWNvbmNhdCAvLyBjb25jYXQgaGVhZCBhbmQgdGFpbAoJZnJhbWVfYnVyeSAwIC8vIHJldE5vSWRlbnRpdHk6IFVzZXJJbmZvVjEKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjM0MAoJLy8gcmV0dXJuIHJldE5vSWRlbnRpdHk7CglmcmFtZV9kaWcgMCAvLyByZXROb0lkZW50aXR5OiBVc2VySW5mb1YxCgliICpnZXRVc2VyKnJldHVybgoKKmlmMF9lbmQ6CgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjM0MgoJLy8gaWRlbnRpdHkgPSB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWJ5dGVjIDMgLy8gICJpIgoJZnJhbWVfZGlnIC0xIC8vIHVzZXI6IEFkZHJlc3MKCWNvbmNhdAoJZnJhbWVfYnVyeSAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzQ0CgkvLyByZXQ6IFVzZXJJbmZvVjEgPSB7CgkvLyAgICAgICB2ZXJzaW9uOiB2LAoJLy8gICAgICAgYmFzZTogaWRlbnRpdHkuZmVlTXVsdGlwbGllckJhc2UsCgkvLyAgICAgICBmZWVNdWx0aXBsaWVyOiBpZGVudGl0eS5mZWVNdWx0aXBsaWVyLAoJLy8gICAgICAgaXNMb2NrZWQ6IGlkZW50aXR5LmlzTG9ja2VkLAoJLy8gICAgICAgdmVyaWZpY2F0aW9uQ2xhc3M6IGlkZW50aXR5LnZlcmlmaWNhdGlvbkNsYXNzLAoJLy8gICAgICAgdmVyaWZpY2F0aW9uU3RhdHVzOiBpZGVudGl0eS52ZXJpZmljYXRpb25TdGF0dXMsCgkvLyAgICAgICBiaWF0ZWNFbmdhZ2VtZW50UG9pbnRzOiBpZGVudGl0eS5iaWF0ZWNFbmdhZ2VtZW50UG9pbnRzLAoJLy8gICAgICAgYmlhdGVjRW5nYWdlbWVudFJhbms6IGlkZW50aXR5LmJpYXRlY0VuZ2FnZW1lbnRSYW5rLAoJLy8gICAgICAgYXZtRW5nYWdlbWVudFBvaW50czogaWRlbnRpdHkuYXZtRW5nYWdlbWVudFBvaW50cywKCS8vICAgICAgIGF2bUVuZ2FnZW1lbnRSYW5rOiBpZGVudGl0eS5hdm1FbmdhZ2VtZW50UmFuaywKCS8vICAgICAgIHRyYWRpbmdFbmdhZ2VtZW50UG9pbnRzOiBpZGVudGl0eS50cmFkaW5nRW5nYWdlbWVudFBvaW50cywKCS8vICAgICAgIHRyYWRpbmdFbmdhZ2VtZW50UmFuazogaWRlbnRpdHkudHJhZGluZ0VuZ2FnZW1lbnRSYW5rLAoJLy8gICAgICAga3ljRXhwaXJhdGlvbjogaWRlbnRpdHkua3ljRXhwaXJhdGlvbiwKCS8vICAgICAgIGludmVzdG9yRm9yRXhwaXJhdGlvbjogaWRlbnRpdHkuaW52ZXN0b3JGb3JFeHBpcmF0aW9uLAoJLy8gICAgICAgaXNQcm9mZXNzaW9uYWxJbnZlc3RvcjogaWRlbnRpdHkuaXNQcm9mZXNzaW9uYWxJbnZlc3RvciwKCS8vICAgICAgIGlzQ29tcGFueTogaWRlbnRpdHkuaXNDb21wYW55LAoJLy8gICAgICAgcGVyc29uVVVJRDogaWRlbnRpdHkucGVyc29uVVVJRCwKCS8vICAgICAgIGxlZ2FsRW50aXR5VVVJRDogaWRlbnRpdHkubGVnYWxFbnRpdHlVVUlELAoJLy8gICAgIH0KCWJ5dGVjIDAgLy8gIGluaXRpYWwgaGVhZAoJYnl0ZWMgMCAvLyAgaW5pdGlhbCB0YWlsCglieXRlYyAxMyAvLyAgaW5pdGlhbCBoZWFkIG9mZnNldAoJZnJhbWVfZGlnIC0yIC8vIHY6IHVpbnQ4CglpdG9iCglleHRyYWN0IDcgMQoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgNDEgOAoJYnRvaQoJaXRvYgoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMCA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJcHVzaGludCAzOTIKCWdldGJpdAoJc2V0Yml0CgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA4IC8vIDUwCglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cgl1bmNvdmVyIDIKCWV4dHJhY3RfdWludDE2CglkdXAgLy8gZHVwbGljYXRlIHN0YXJ0IG9mIGVsZW1lbnQKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXN3YXAKCWV4dHJhY3RfdWludDE2IC8vIGdldCBudW1iZXIgb2YgZWxlbWVudHMKCWludGMgMSAvLyAgZ2V0IHR5cGUgbGVuZ3RoCgkqIC8vIG11bHRpcGx5IGJ5IHR5cGUgbGVuZ3RoCglpbnRjIDMgLy8gMgoJKyAvLyBhZGQgdHdvIGZvciBsZW5ndGgKCWV4dHJhY3QzCglleHRyYWN0IDIgMAoJZHVwCglsZW4KCWl0b2IKCWV4dHJhY3QgNiAyCglzd2FwCgljb25jYXQKCWNhbGxzdWIgKnByb2Nlc3NfZHluYW1pY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA5IC8vIDUyCglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cgl1bmNvdmVyIDIKCWV4dHJhY3RfdWludDE2CglkdXAgLy8gZHVwbGljYXRlIHN0YXJ0IG9mIGVsZW1lbnQKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXN3YXAKCWV4dHJhY3RfdWludDE2IC8vIGdldCBudW1iZXIgb2YgZWxlbWVudHMKCWludGMgMSAvLyAgZ2V0IHR5cGUgbGVuZ3RoCgkqIC8vIG11bHRpcGx5IGJ5IHR5cGUgbGVuZ3RoCglpbnRjIDMgLy8gMgoJKyAvLyBhZGQgdHdvIGZvciBsZW5ndGgKCWV4dHJhY3QzCglleHRyYWN0IDIgMAoJZHVwCglsZW4KCWl0b2IKCWV4dHJhY3QgNiAyCglzd2FwCgljb25jYXQKCWNhbGxzdWIgKnByb2Nlc3NfZHluYW1pY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA1NCA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA2MiA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA3MCA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA3OCA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA4NiA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA5NCA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA5IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDE3IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDIgLy8gMHgwMAoJaW50YyAwIC8vIDAKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDQgLy8gNjQKCWdldGJpdAoJc2V0Yml0CgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCAyNSA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCAzMyA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA2IC8vIDgxNgoJZ2V0Yml0CglzZXRiaXQKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCXBvcCAvLyBwb3AgaGVhZCBvZmZzZXQKCWNvbmNhdCAvLyBjb25jYXQgaGVhZCBhbmQgdGFpbAoJZnJhbWVfYnVyeSAyIC8vIHJldDogVXNlckluZm9WMQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzY0CgkvLyByZXR1cm4gcmV0OwoJZnJhbWVfZGlnIDIgLy8gcmV0OiBVc2VySW5mb1YxCgoqZ2V0VXNlcipyZXR1cm46CgkvLyBzZXQgdGhlIHN1YnJvdXRpbmUgcmV0dXJuIHZhbHVlCglmcmFtZV9idXJ5IDAKCgkvLyBwb3AgYWxsIGxvY2FsIHZhcmlhYmxlcyBmcm9tIHRoZSBzdGFjawoJcG9wbiAyCglyZXRzdWIKCi8vIGdldFVzZXJTaG9ydChhZGRyZXNzLHVpbnQ4KSh1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKQoqYWJpX3JvdXRlX2dldFVzZXJTaG9ydDoKCS8vIFRoZSBBQkkgcmV0dXJuIHByZWZpeAoJYnl0ZWMgNiAvLyAweDE1MWY3Yzc1CgoJLy8gdjogdWludDgKCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCWR1cAoJbGVuCglpbnRjIDEgLy8gMQoJPT0KCgkvLyBhcmd1bWVudCAwICh2KSBmb3IgZ2V0VXNlclNob3J0IG11c3QgYmUgYSB1aW50OAoJYXNzZXJ0CglidG9pCgoJLy8gdXNlcjogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJZHVwCglsZW4KCWludGMgMiAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAxICh1c2VyKSBmb3IgZ2V0VXNlclNob3J0IG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyBleGVjdXRlIGdldFVzZXJTaG9ydChhZGRyZXNzLHVpbnQ4KSh1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKQoJY2FsbHN1YiBnZXRVc2VyU2hvcnQKCWNvbmNhdAoJbG9nCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyBnZXRVc2VyU2hvcnQodXNlcjogQWRkcmVzcywgdjogdWludDgpOiBVc2VySW5mb1Nob3J0VjEKLy8KLy8gUmV0dXJucyBzaG9ydCB1c2VyIGluZm9ybWF0aW9uIC0gZmVlIG11bHRpcGxpZXIsIHZlcmlmaWNhdGlvbiBjbGFzcywgZW5nYWdlbWVudCBjbGFzcyAuLgovLwovLyBAcGFyYW0gdXNlciBHZXQgaW5mbyBmb3Igc3BlY2lmaWMgdXNlciBhZGRyZXNzCi8vIEBwYXJhbSB2IFZlcnNpb24gb2YgdGhlIGRhdGEgc3RydWN0dXJlIHRvIHJldHVybgpnZXRVc2VyU2hvcnQ6Cglwcm90byAyIDEKCgkvLyBQdXNoIGVtcHR5IGJ5dGVzIGFmdGVyIHRoZSBmcmFtZSBwb2ludGVyIHRvIHJlc2VydmUgc3BhY2UgZm9yIGxvY2FsIHZhcmlhYmxlcwoJYnl0ZWMgMCAvLyAweAoJZHVwbiAyCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozNzUKCS8vIGFzc2VydCh2ID09PSAxLCAiQ3VycmVudGx5IHN1cHBvcnRlZCB2ZXJzaW9uIG9mIHRoZSBkYXRhIHN0cnVjdHVyZSBpcyAnMSciKQoJZnJhbWVfZGlnIC0yIC8vIHY6IHVpbnQ4CglpbnRjIDEgLy8gMQoJPT0KCgkvLyBDdXJyZW50bHkgc3VwcG9ydGVkIHZlcnNpb24gb2YgdGhlIGRhdGEgc3RydWN0dXJlIGlzICcxJwoJYXNzZXJ0CgoJLy8gKmlmMV9jb25kaXRpb24KCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6Mzc2CgkvLyAhdGhpcy5pZGVudGl0aWVzKHVzZXIpLmV4aXN0cwoJYnl0ZWMgMyAvLyAgImkiCglmcmFtZV9kaWcgLTEgLy8gdXNlcjogQWRkcmVzcwoJY29uY2F0Cglib3hfbGVuCglzd2FwCglwb3AKCSEKCWJ6ICppZjFfZW5kCgoJLy8gKmlmMV9jb25zZXF1ZW50CgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjM3NwoJLy8gcmV0Tm9JZGVudGl0eTogVXNlckluZm9TaG9ydFYxID0gewoJLy8gICAgICAgICB2ZXJzaW9uOiB2LAoJLy8gICAgICAgICBiYXNlOiBTQ0FMRSBhcyB1aW50NjQsCgkvLyAgICAgICAgIGZlZU11bHRpcGxpZXI6ICgyICogU0NBTEUpIGFzIHVpbnQ2NCwKCS8vICAgICAgICAgaXNMb2NrZWQ6IGZhbHNlLAoJLy8gICAgICAgICB2ZXJpZmljYXRpb25DbGFzczogMCwKCS8vICAgICAgIH0KCWZyYW1lX2RpZyAtMiAvLyB2OiB1aW50OAoJaXRvYgoJZXh0cmFjdCA3IDEKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljb25jYXQKCWJ5dGVjIDkgLy8gMHgwMDAwMDAwMDc3MzU5NDAwCgljb25jYXQKCWJ5dGVjIDEwIC8vIDB4MDAwMDAwMDAzYjlhY2EwMAoJY29uY2F0CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglkdXAKCXNldGJpdAoJY29uY2F0CglmcmFtZV9idXJ5IDAgLy8gcmV0Tm9JZGVudGl0eTogVXNlckluZm9TaG9ydFYxCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozODQKCS8vIHJldHVybiByZXROb0lkZW50aXR5OwoJZnJhbWVfZGlnIDAgLy8gcmV0Tm9JZGVudGl0eTogVXNlckluZm9TaG9ydFYxCgliICpnZXRVc2VyU2hvcnQqcmV0dXJuCgoqaWYxX2VuZDoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6Mzg2CgkvLyBpZGVudGl0eSA9IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYnl0ZWMgMyAvLyAgImkiCglmcmFtZV9kaWcgLTEgLy8gdXNlcjogQWRkcmVzcwoJY29uY2F0CglmcmFtZV9idXJ5IDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozODgKCS8vIHJldDogVXNlckluZm9TaG9ydFYxID0gewoJLy8gICAgICAgdmVyc2lvbjogdiwKCS8vICAgICAgIGJhc2U6IGlkZW50aXR5LmZlZU11bHRpcGxpZXJCYXNlLAoJLy8gICAgICAgZmVlTXVsdGlwbGllcjogaWRlbnRpdHkuZmVlTXVsdGlwbGllciwKCS8vICAgICAgIGlzTG9ja2VkOiBpZGVudGl0eS5pc0xvY2tlZCwKCS8vICAgICAgIHZlcmlmaWNhdGlvbkNsYXNzOiBpZGVudGl0eS52ZXJpZmljYXRpb25DbGFzcywKCS8vICAgICB9CglmcmFtZV9kaWcgLTIgLy8gdjogdWludDgKCWl0b2IKCWV4dHJhY3QgNyAxCglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCAwIDgKCWJ0b2kKCWl0b2IKCWNvbmNhdAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgOSA4CglidG9pCglpdG9iCgljb25jYXQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDE3IDgKCWJ0b2kKCWl0b2IKCWNvbmNhdAoJYnl0ZWMgMiAvLyAweDAwCglpbnRjIDAgLy8gMAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWludGMgNCAvLyA2NAoJZ2V0Yml0CglzZXRiaXQKCWNvbmNhdAoJZnJhbWVfYnVyeSAyIC8vIHJldDogVXNlckluZm9TaG9ydFYxCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozOTUKCS8vIHJldHVybiByZXQ7CglmcmFtZV9kaWcgMiAvLyByZXQ6IFVzZXJJbmZvU2hvcnRWMQoKKmdldFVzZXJTaG9ydCpyZXR1cm46CgkvLyBzZXQgdGhlIHN1YnJvdXRpbmUgcmV0dXJuIHZhbHVlCglmcmFtZV9idXJ5IDAKCgkvLyBwb3AgYWxsIGxvY2FsIHZhcmlhYmxlcyBmcm9tIHRoZSBzdGFjawoJcG9wbiAyCglyZXRzdWIKCi8vIHdpdGhkcmF3RXhjZXNzQXNzZXRzKHVpbnQ2NCx1aW50NjQsdWludDY0KXVpbnQ2NAoqYWJpX3JvdXRlX3dpdGhkcmF3RXhjZXNzQXNzZXRzOgoJLy8gVGhlIEFCSSByZXR1cm4gcHJlZml4CglieXRlYyA2IC8vIDB4MTUxZjdjNzUKCgkvLyBhbW91bnQ6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMwoJYnRvaQoKCS8vIGFzc2V0OiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCWJ0b2kKCgkvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogdWludDY0Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglidG9pCgoJLy8gZXhlY3V0ZSB3aXRoZHJhd0V4Y2Vzc0Fzc2V0cyh1aW50NjQsdWludDY0LHVpbnQ2NCl1aW50NjQKCWNhbGxzdWIgd2l0aGRyYXdFeGNlc3NBc3NldHMKCWl0b2IKCWNvbmNhdAoJbG9nCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyB3aXRoZHJhd0V4Y2Vzc0Fzc2V0cyhhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQsIGFzc2V0OiBBc3NldElELCBhbW91bnQ6IHVpbnQ2NCk6IHVpbnQ2NAovLwovLyBJZiBzb21lb25lIGRlcG9zaXRzIGV4Y2VzcyBhc3NldHMgdG8gdGhpcyBzbWFydCBjb250cmFjdCBiaWF0ZWMgY2FuIHVzZSB0aGVtLgovLwovLyBPbmx5IGFkZHJlc3NFeGVjdXRpdmVGZWUgaXMgYWxsb3dlZCB0byBleGVjdXRlIHRoaXMgbWV0aG9kLgovLwovLyBAcGFyYW0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIgQmlhdGVjIGNvbmZpZyBhcHAuIE9ubHkgYWRkcmVzc0V4ZWN1dGl2ZUZlZSBpcyBhbGxvd2VkIHRvIGV4ZWN1dGUgdGhpcyBtZXRob2QuCi8vIEBwYXJhbSBhc3NldCBBc3NldCB0byB3aXRoZHJhdy4gSWYgbmF0aXZlIHRva2VuLCB0aGVuIHplcm8KLy8gQHBhcmFtIGFtb3VudCBBbW91bnQgb2YgdGhlIGFzc2V0IHRvIGJlIHdpdGhkcmF3bgp3aXRoZHJhd0V4Y2Vzc0Fzc2V0czoKCXByb3RvIDMgMQoKCS8vIFB1c2ggZW1wdHkgYnl0ZXMgYWZ0ZXIgdGhlIGZyYW1lIHBvaW50ZXIgdG8gcmVzZXJ2ZSBzcGFjZSBmb3IgbG9jYWwgdmFyaWFibGVzCglieXRlYyAwIC8vIDB4CglkdXAKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQwOAoJLy8gYXNzZXJ0KGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyID09PSB0aGlzLmFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLnZhbHVlLCAnQ29uZmlndXJhdGlvbiBhcHAgZG9lcyBub3QgbWF0Y2gnKQoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYnl0ZWMgNCAvLyAgIkIiCglhcHBfZ2xvYmFsX2dldAoJPT0KCgkvLyBDb25maWd1cmF0aW9uIGFwcCBkb2VzIG5vdCBtYXRjaAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MDkKCS8vIGFkZHJlc3NFeGVjdXRpdmVGZWUgPSBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgnZWYnKSBhcyBBZGRyZXNzCglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglieXRlYyAxMiAvLyAgImVmIgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdlZicpCglhc3NlcnQKCWZyYW1lX2J1cnkgMCAvLyBhZGRyZXNzRXhlY3V0aXZlRmVlOiBhZGRyZXNzCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MTAKCS8vIHBhdXNlZCA9IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdzJykgYXMgdWludDY0CglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglieXRlYyA1IC8vICAicyIKCWFwcF9nbG9iYWxfZ2V0X2V4CgoJLy8gZ2xvYmFsIHN0YXRlIHZhbHVlIGRvZXMgbm90IGV4aXN0OiBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgncycpCglhc3NlcnQKCWZyYW1lX2J1cnkgMSAvLyBwYXVzZWQ6IHVpbnQ2NAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDExCgkvLyBhc3NlcnQocGF1c2VkID09PSAwLCAnRVJSX1BBVVNFRCcpCglmcmFtZV9kaWcgMSAvLyBwYXVzZWQ6IHVpbnQ2NAoJaW50YyAwIC8vIDAKCT09CgoJLy8gRVJSX1BBVVNFRAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MTIKCS8vIGFzc2VydCh0aGlzLnR4bi5zZW5kZXIgPT09IGFkZHJlc3NFeGVjdXRpdmVGZWUsICdPbmx5IGZlZSBleGVjdXRvciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB0YWtlIHRoZSBjb2xsZWN0ZWQgZmVlcycpCgl0eG4gU2VuZGVyCglmcmFtZV9kaWcgMCAvLyBhZGRyZXNzRXhlY3V0aXZlRmVlOiBhZGRyZXNzCgk9PQoKCS8vIE9ubHkgZmVlIGV4ZWN1dG9yIHNldHVwIGluIHRoZSBjb25maWcgY2FuIHRha2UgdGhlIGNvbGxlY3RlZCBmZWVzCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQxNAoJLy8gdGhpcy5kb0F4ZmVyKHRoaXMudHhuLnNlbmRlciwgYXNzZXQsIGFtb3VudCkKCWZyYW1lX2RpZyAtMyAvLyBhbW91bnQ6IHVpbnQ2NAoJZnJhbWVfZGlnIC0yIC8vIGFzc2V0OiBBc3NldElECgl0eG4gU2VuZGVyCgljYWxsc3ViIGRvQXhmZXIKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQxNgoJLy8gcmV0dXJuIGFtb3VudDsKCWZyYW1lX2RpZyAtMyAvLyBhbW91bnQ6IHVpbnQ2NAoKCS8vIHNldCB0aGUgc3Vicm91dGluZSByZXR1cm4gdmFsdWUKCWZyYW1lX2J1cnkgMAoKCS8vIHBvcCBhbGwgbG9jYWwgdmFyaWFibGVzIGZyb20gdGhlIHN0YWNrCglwb3BuIDEKCXJldHN1YgoKLy8gZG9BeGZlcihyZWNlaXZlcjogQWRkcmVzcywgYXNzZXQ6IEFzc2V0SUQsIGFtb3VudDogdWludDY0KTogdm9pZAovLwovLyBFeGVjdXRlcyB4ZmVyIG9mIHBheSBwYXltZW50IG1ldGhvZHMgdG8gc3BlY2lmaWVkIHJlY2VpdmVyIGZyb20gc21hcnQgY29udHJhY3QgYWdncmVnYXRlZCBhY2NvdW50IHdpdGggc3BlY2lmaWVkIGFzc2V0IGFuZCBhbW91bnQgaW4gdG9rZW5zIGRlY2ltYWxzCi8vIEBwYXJhbSByZWNlaXZlciBSZWNlaXZlcgovLyBAcGFyYW0gYXNzZXQgQXNzZXQuIFplcm8gZm9yIGFsZ28KLy8gQHBhcmFtIGFtb3VudCBBbW91bnQgdG8gdHJhbnNmZXIKZG9BeGZlcjoKCXByb3RvIDMgMAoKCS8vICppZjJfY29uZGl0aW9uCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQyNgoJLy8gYXNzZXQuaWQgPT09IDAKCWZyYW1lX2RpZyAtMiAvLyBhc3NldDogQXNzZXRJRAoJaW50YyAwIC8vIDAKCT09CglieiAqaWYyX2Vsc2UKCgkvLyAqaWYyX2NvbnNlcXVlbnQKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDI3CgkvLyBzZW5kUGF5bWVudCh7CgkvLyAgICAgICAgIHJlY2VpdmVyOiByZWNlaXZlciwKCS8vICAgICAgICAgYW1vdW50OiBhbW91bnQsCgkvLyAgICAgICAgIGZlZTogMCwKCS8vICAgICAgIH0pCglpdHhuX2JlZ2luCglpbnRjIDEgLy8gIHBheQoJaXR4bl9maWVsZCBUeXBlRW51bQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDI4CgkvLyByZWNlaXZlcjogcmVjZWl2ZXIKCWZyYW1lX2RpZyAtMSAvLyByZWNlaXZlcjogQWRkcmVzcwoJaXR4bl9maWVsZCBSZWNlaXZlcgoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDI5CgkvLyBhbW91bnQ6IGFtb3VudAoJZnJhbWVfZGlnIC0zIC8vIGFtb3VudDogdWludDY0CglpdHhuX2ZpZWxkIEFtb3VudAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDMwCgkvLyBmZWU6IDAKCWludGMgMCAvLyAwCglpdHhuX2ZpZWxkIEZlZQoKCS8vIFN1Ym1pdCBpbm5lciB0cmFuc2FjdGlvbgoJaXR4bl9zdWJtaXQKCWIgKmlmMl9lbmQKCippZjJfZWxzZToKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDMzCgkvLyBzZW5kQXNzZXRUcmFuc2Zlcih7CgkvLyAgICAgICAgIGFzc2V0UmVjZWl2ZXI6IHJlY2VpdmVyLAoJLy8gICAgICAgICB4ZmVyQXNzZXQ6IGFzc2V0LAoJLy8gICAgICAgICBhc3NldEFtb3VudDogYW1vdW50LAoJLy8gICAgICAgICBmZWU6IDAsCgkvLyAgICAgICB9KQoJaXR4bl9iZWdpbgoJaW50YyA3IC8vICBheGZlcgoJaXR4bl9maWVsZCBUeXBlRW51bQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDM0CgkvLyBhc3NldFJlY2VpdmVyOiByZWNlaXZlcgoJZnJhbWVfZGlnIC0xIC8vIHJlY2VpdmVyOiBBZGRyZXNzCglpdHhuX2ZpZWxkIEFzc2V0UmVjZWl2ZXIKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQzNQoJLy8geGZlckFzc2V0OiBhc3NldAoJZnJhbWVfZGlnIC0yIC8vIGFzc2V0OiBBc3NldElECglpdHhuX2ZpZWxkIFhmZXJBc3NldAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDM2CgkvLyBhc3NldEFtb3VudDogYW1vdW50CglmcmFtZV9kaWcgLTMgLy8gYW1vdW50OiB1aW50NjQKCWl0eG5fZmllbGQgQXNzZXRBbW91bnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQzNwoJLy8gZmVlOiAwCglpbnRjIDAgLy8gMAoJaXR4bl9maWVsZCBGZWUKCgkvLyBTdWJtaXQgaW5uZXIgdHJhbnNhY3Rpb24KCWl0eG5fc3VibWl0CgoqaWYyX2VuZDoKCXJldHN1YgoKKmNyZWF0ZV9Ob09wOgoJcHVzaGJ5dGVzIDB4Yjg0NDdiMzYgLy8gbWV0aG9kICJjcmVhdGVBcHBsaWNhdGlvbigpdm9pZCIKCXR4bmEgQXBwbGljYXRpb25BcmdzIDAKCW1hdGNoICphYmlfcm91dGVfY3JlYXRlQXBwbGljYXRpb24KCgkvLyB0aGlzIGNvbnRyYWN0IGRvZXMgbm90IGltcGxlbWVudCB0aGUgZ2l2ZW4gQUJJIG1ldGhvZCBmb3IgY3JlYXRlIE5vT3AKCWVycgoKKmNhbGxfTm9PcDoKCXB1c2hieXRlcyAweGUzYmY1YzFmIC8vIG1ldGhvZCAiYm9vdHN0cmFwKHVpbnQ2NCxhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyl2b2lkIgoJcHVzaGJ5dGVzIDB4YWU2NGMxNjcgLy8gbWV0aG9kICJzZWxmUmVnaXN0cmF0aW9uKGFkZHJlc3MsKHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCxzdHJpbmcsc3RyaW5nLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpKXZvaWQiCglwdXNoYnl0ZXMgMHhhNDhjZmJiYyAvLyBtZXRob2QgInNldEluZm8oYWRkcmVzcywodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkpdm9pZCIKCXB1c2hieXRlcyAweDY3OTE0MjY0IC8vIG1ldGhvZCAic2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbih1aW50NjQsYnl0ZVtdLGJ5dGVbXSxieXRlW10sdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0KXZvaWQiCglwdXNoYnl0ZXMgMHhlOGFkMTg5MiAvLyBtZXRob2QgImdldFVzZXIoYWRkcmVzcyx1aW50OCkodWludDgsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsYm9vbCkiCglwdXNoYnl0ZXMgMHgxMjdmZmI3YiAvLyBtZXRob2QgImdldFVzZXJTaG9ydChhZGRyZXNzLHVpbnQ4KSh1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKSIKCXB1c2hieXRlcyAweGNiYTJlOTVkIC8vIG1ldGhvZCAid2l0aGRyYXdFeGNlc3NBc3NldHModWludDY0LHVpbnQ2NCx1aW50NjQpdWludDY0IgoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMAoJbWF0Y2ggKmFiaV9yb3V0ZV9ib290c3RyYXAgKmFiaV9yb3V0ZV9zZWxmUmVnaXN0cmF0aW9uICphYmlfcm91dGVfc2V0SW5mbyAqYWJpX3JvdXRlX3NlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24gKmFiaV9yb3V0ZV9nZXRVc2VyICphYmlfcm91dGVfZ2V0VXNlclNob3J0ICphYmlfcm91dGVfd2l0aGRyYXdFeGNlc3NBc3NldHMKCgkvLyB0aGlzIGNvbnRyYWN0IGRvZXMgbm90IGltcGxlbWVudCB0aGUgZ2l2ZW4gQUJJIG1ldGhvZCBmb3IgY2FsbCBOb09wCgllcnIKCipjYWxsX1VwZGF0ZUFwcGxpY2F0aW9uOgoJcHVzaGJ5dGVzIDB4NWZjODg1YTAgLy8gbWV0aG9kICJ1cGRhdGVBcHBsaWNhdGlvbih1aW50NjQsYnl0ZVtdKXZvaWQiCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAwCgltYXRjaCAqYWJpX3JvdXRlX3VwZGF0ZUFwcGxpY2F0aW9uCgoJLy8gdGhpcyBjb250cmFjdCBkb2VzIG5vdCBpbXBsZW1lbnQgdGhlIGdpdmVuIEFCSSBtZXRob2QgZm9yIGNhbGwgVXBkYXRlQXBwbGljYXRpb24KCWVycgoKKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQ6Cglwcm90byA0IDMKCWZyYW1lX2RpZyAtNCAvLyB0dXBsZSBoZWFkCglmcmFtZV9kaWcgLTEgLy8gZWxlbWVudAoJY29uY2F0CglmcmFtZV9kaWcgLTMgLy8gdHVwbGUgdGFpbAoJZnJhbWVfZGlnIC0yIC8vIGhlYWQgb2Zmc2V0CglyZXRzdWIKCipwcm9jZXNzX2R5bmFtaWNfdHVwbGVfZWxlbWVudDoKCXByb3RvIDQgMwoJZnJhbWVfZGlnIC00IC8vIHR1cGxlIGhlYWQKCWZyYW1lX2RpZyAtMiAvLyBoZWFkIG9mZnNldAoJY29uY2F0CglmcmFtZV9idXJ5IC00IC8vIHR1cGxlIGhlYWQKCWZyYW1lX2RpZyAtMSAvLyBlbGVtZW50CglkdXAKCWxlbgoJZnJhbWVfZGlnIC0yIC8vIGhlYWQgb2Zmc2V0CglidG9pCgkrCglpdG9iCglleHRyYWN0IDYgMgoJZnJhbWVfYnVyeSAtMiAvLyBoZWFkIG9mZnNldAoJZnJhbWVfZGlnIC0zIC8vIHR1cGxlIHRhaWwKCXN3YXAKCWNvbmNhdAoJZnJhbWVfYnVyeSAtMyAvLyB0dXBsZSB0YWlsCglmcmFtZV9kaWcgLTQgLy8gdHVwbGUgaGVhZAoJZnJhbWVfZGlnIC0zIC8vIHR1cGxlIHRhaWwKCWZyYW1lX2RpZyAtMiAvLyBoZWFkIG9mZnNldAoJcmV0c3Vi";
		protected override string SourceClear { get; set; } = "I3ByYWdtYSB2ZXJzaW9uIDEw";
		protected override string SourceApprovalAVM { get; set; }= "CiAKAAEgAkCAlOvcA7AGBDI0JhAACAAAAAAAAAAAAQABaQFCAXMEFR98dSQwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAVQklBVEVDLUlERU5ULTAxLTAzLTAxCAAAAAB3NZQACAAAAAA7msoABXNjdmVyAmVmAgBoAgAAAWUxGBSBBgsxGQiNDAWUAAAAAAAABdIAAAWGAAAAAAAAAAAAAACIAAIjQ4oAACcLJwhniTYaBEkVJBJENhoDSRUkEkQ2GgJJFSQSRDYaAReIAAIjQ4oEACgxADYyAHIHSBJEJwSL/2eAAWeL/meAAXaL/WcnD4v8Z4v/JwVlRIwAiwAiEkSJNhoCVwIANhoBF4gAAiNDigIAKEmL/ycEZBJEi/+AAXVlRIwAMQCLABJEi/8nBWVEjAGLASISRCcIsIv+sCcLi/5niTYaAjYaAUkVJBJEiAACI0OKAgAri/9QvUxIFESL/jX/NP9XKQgXIxJEi/41/zT/VwAIFyISRIv+Nf8hCDT/NP9PAllJNP9MWSMLJQhYVwIAJwcSRIv+Nf8hCTT/NP9PAllJNP9MWSMLJQhYVwIAJwcSRIv+Nf80/1c2CBciEkSL/jX/NP9XPggXIhJEi/41/zT/V0YIFyISRIv+Nf80/1dOCBciEkSL/jX/NP9XVggXIhJEi/41/zT/V14IFyISRIv+Nf80/yEEUyISRIv+Nf80/1cZCBciEkSL/jX/NP9XIQgXIhJEi/41/zT/IQZTIhJEi/41/zT/VxEIFyEFEkSL/jX/NP9XCQgXgYCo1rkHEkQri/9QSbxIi/6/iTYaAjYaAUkVJBJEiAACI0OKAgAxACcPZBJEi/41/zT/VxEIFyEFEkSL/jX/NP9XAAgXIQcORCuL/1BJvEiL/r+JNhoIFzYaBxc2GgYXNhoFFzYaBFcCADYaA1cCADYaAlcCADYaAReIAAIjQ4oIAChJi/8nBGQSRIv/JwxlRIwAMQCLABJEi/8nBWVEjAGLASISRLElshCL/bILi/yyP4v7sgyL+bIOi/qyDYv+sgqL+LIBs4knBjYaAkkVIxJEFzYaAUkVJBJEiAAEULAjQ4oCAShHAov+IxJEK4v/UL1MSBRBAGcoKCcNi/4WVwcBiAMsKYgDKCmIAyQqIklUiAMdJw6IAyUnDogDICmIAw8piAMLKYgDBymIAwMpiAL/KYgC+ycJiAL2JwqIAvEqIklUiALqKYgC5imIAuIqIklUiALbSFCMAIsAQgFbK4v/UIwBKCgnDYv+FlcHAYgCv4sBvkQ1/zT/VykIFxaIAq+LAb5ENf80/1cACBcWiAKfKiKLAb5ENf80/4GIA1NUiAKNiwG+RDX/IQg0/zT/TwJZSTT/TFkjCyUIWFcCAEkVFlcGAkxQiAJziwG+RDX/IQk0/zT/TwJZSTT/TFkjCyUIWFcCAEkVFlcGAkxQiAJMiwG+RDX/NP9XNggXFogCL4sBvkQ1/zT/Vz4IFxaIAh+LAb5ENf80/1dGCBcWiAIPiwG+RDX/NP9XTggXFogB/4sBvkQ1/zT/V1YIFxaIAe+LAb5ENf80/1deCBcWiAHfiwG+RDX/NP9XCQgXFogBz4sBvkQ1/zT/VxEIFxaIAb8qIosBvkQ1/zT/IQRTVIgBrosBvkQ1/zT/VxkIFxaIAZ6LAb5ENf80/1chCBcWiAGOKiKLAb5ENf80/yEGU1SIAX1IUIwCiwKMAEYCiScGNhoCSRUjEkQXNhoBSRUkEkSIAARQsCNDigIBKEcCi/4jEkQri/9QvUxIFEEAGov+FlcHASlQJwlQJwpQKiJJVFCMAIsAQgBJK4v/UIwBi/4WVwcBiwG+RDX/NP9XAAgXFlCLAb5ENf80/1cJCBcWUIsBvkQ1/zT/VxEIFxZQKiKLAb5ENf80/yEEU1RQjAKLAowARgKJJwY2GgMXNhoCFzYaAReIAAUWULAjQ4oDAShJi/8nBGQSRIv/JwxlRIwAi/8nBWVEjAGLASISRDEAiwASRIv9i/4xAIgAB4v9jABGAYmKAwCL/iISQQATsSOyEIv/sgeL/bIIIrIBs0IAFbEhB7IQi/+yFIv+shGL/bISIrIBs4mABLhEezY2GgCOAfpuAIAE479cH4AErmTBZ4AEpIz7vIAEZ5FCZIAE6K0YkoAEEn/7e4AEy6LpXTYaAI4H+j761Pvx/DL8pv6d/zUAgARfyIWgNhoAjgH6gwCKBAOL/Iv/UIv9i/6JigQDi/yL/lCM/Iv/SRWL/hcIFlcGAoz+i/1MUIz9i/yL/Yv+iQ==";
		protected override string SourceClearAVM { get; set; } = "Cg==";
		protected override ulong? GlobalNumByteSlices { get; set; }=4;
		protected override ulong? GlobalNumUints { get; set; }=1;
		protected override ulong? LocalNumByteSlices { get; set; }=0;
		protected override ulong? LocalNumUints { get; set; }=0;
		protected override ulong? ExtraProgramPages { get; set; }=0;

	}

}
