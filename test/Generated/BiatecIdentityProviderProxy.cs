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


		//addressExecutiveFee can perfom key registration for this LP pool\n\n\nOnly addressExecutiveFee is allowed to execute this method.
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


		//If someone deposits excess assets to this smart contract biatec can use them.\n\n\nOnly addressExecutiveFee is allowed to execute this method.
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


		protected override string SourceApproval { get; set; }= "I3ByYWdtYSB2ZXJzaW9uIDEwCmludGNibG9jayAwIDEgMzIgMiA2NCAxMDAwMDAwMDAwIDgxNiA0IDUwIDUyCmJ5dGVjYmxvY2sgMHggMHgwMDAwMDAwMDAwMDAwMDAwIDB4MDAgMHg2OSAweDQyIDB4NzMgMHgxNTFmN2M3NSAweDMwMzAzMDMwMzAzMDMwMzAyZDMwMzAzMDMwMmQzMDMwMzAzMDJkMzAzMDMwMzAyZDMwMzAzMDMwMzAzMDMwMzAzMDMwMzAzMCAiQklBVEVDLUlERU5ULTAxLTAzLTAyIiAweDAwMDAwMDAwNzczNTk0MDAgMHgwMDAwMDAwMDNiOWFjYTAwIDB4NzM2Mzc2NjU3MiAweDY1NjYgMHgwMDY4IDB4MDAwMCAweDc1IDB4NjUKCi8vIFRoaXMgVEVBTCB3YXMgZ2VuZXJhdGVkIGJ5IFRFQUxTY3JpcHQgdjAuMTA3LjIKLy8gaHR0cHM6Ly9naXRodWIuY29tL2FsZ29yYW5kZm91bmRhdGlvbi9URUFMU2NyaXB0CgovLyBUaGlzIGNvbnRyYWN0IGlzIGNvbXBsaWFudCB3aXRoIGFuZC9vciBpbXBsZW1lbnRzIHRoZSBmb2xsb3dpbmcgQVJDczogWyBBUkM0IF0KCi8vIFRoZSBmb2xsb3dpbmcgdGVuIGxpbmVzIG9mIFRFQUwgaGFuZGxlIGluaXRpYWwgcHJvZ3JhbSBmbG93Ci8vIFRoaXMgcGF0dGVybiBpcyB1c2VkIHRvIG1ha2UgaXQgZWFzeSBmb3IgYW55b25lIHRvIHBhcnNlIHRoZSBzdGFydCBvZiB0aGUgcHJvZ3JhbSBhbmQgZGV0ZXJtaW5lIGlmIGEgc3BlY2lmaWMgYWN0aW9uIGlzIGFsbG93ZWQKLy8gSGVyZSwgYWN0aW9uIHJlZmVycyB0byB0aGUgT25Db21wbGV0ZSBpbiBjb21iaW5hdGlvbiB3aXRoIHdoZXRoZXIgdGhlIGFwcCBpcyBiZWluZyBjcmVhdGVkIG9yIGNhbGxlZAovLyBFdmVyeSBwb3NzaWJsZSBhY3Rpb24gZm9yIHRoaXMgY29udHJhY3QgaXMgcmVwcmVzZW50ZWQgaW4gdGhlIHN3aXRjaCBzdGF0ZW1lbnQKLy8gSWYgdGhlIGFjdGlvbiBpcyBub3QgaW1wbGVtZW50ZWQgaW4gdGhlIGNvbnRyYWN0LCBpdHMgcmVzcGVjdGl2ZSBicmFuY2ggd2lsbCBiZSAiKk5PVF9JTVBMRU1FTlRFRCIgd2hpY2gganVzdCBjb250YWlucyAiZXJyIgp0eG4gQXBwbGljYXRpb25JRAohCnB1c2hpbnQgNgoqCnR4biBPbkNvbXBsZXRpb24KKwpzd2l0Y2ggKmNhbGxfTm9PcCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQgKk5PVF9JTVBMRU1FTlRFRCAqY2FsbF9VcGRhdGVBcHBsaWNhdGlvbiAqTk9UX0lNUExFTUVOVEVEICpjcmVhdGVfTm9PcCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQgKk5PVF9JTVBMRU1FTlRFRCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQKCipOT1RfSU1QTEVNRU5URUQ6CgkvLyBUaGUgcmVxdWVzdGVkIGFjdGlvbiBpcyBub3QgaW1wbGVtZW50ZWQgaW4gdGhpcyBjb250cmFjdC4gQXJlIHlvdSB1c2luZyB0aGUgY29ycmVjdCBPbkNvbXBsZXRlPyBEaWQgeW91IHNldCB5b3VyIGFwcCBJRD8KCWVycgoKLy8gY3JlYXRlQXBwbGljYXRpb24oKXZvaWQKKmFiaV9yb3V0ZV9jcmVhdGVBcHBsaWNhdGlvbjoKCS8vIGV4ZWN1dGUgY3JlYXRlQXBwbGljYXRpb24oKXZvaWQKCWNhbGxzdWIgY3JlYXRlQXBwbGljYXRpb24KCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIGNyZWF0ZUFwcGxpY2F0aW9uKCk6IHZvaWQKLy8KLy8gSW5pdGlhbCBzZXR1cApjcmVhdGVBcHBsaWNhdGlvbjoKCXByb3RvIDAgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjA3CgkvLyB0aGlzLnZlcnNpb24udmFsdWUgPSB2ZXJzaW9uCglieXRlYyAxMSAvLyAgInNjdmVyIgoJYnl0ZWMgOCAvLyAiQklBVEVDLUlERU5ULTAxLTAzLTAyIgoJYXBwX2dsb2JhbF9wdXQKCXJldHN1YgoKLy8gYm9vdHN0cmFwKHVpbnQ2NCxhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyl2b2lkCiphYmlfcm91dGVfYm9vdHN0cmFwOgoJLy8gZW5nYWdlbWVudFNldHRlcjogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgNAoJZHVwCglsZW4KCWludGMgMiAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAwIChlbmdhZ2VtZW50U2V0dGVyKSBmb3IgYm9vdHN0cmFwIG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyB2ZXJpZmljYXRpb25TZXR0ZXI6IGFkZHJlc3MKCXR4bmEgQXBwbGljYXRpb25BcmdzIDMKCWR1cAoJbGVuCglpbnRjIDIgLy8gMzIKCT09CgoJLy8gYXJndW1lbnQgMSAodmVyaWZpY2F0aW9uU2V0dGVyKSBmb3IgYm9vdHN0cmFwIG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyBnb3Zlcm5vcjogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMgoJZHVwCglsZW4KCWludGMgMiAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAyIChnb3Zlcm5vcikgZm9yIGJvb3RzdHJhcCBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJYnRvaQoKCS8vIGV4ZWN1dGUgYm9vdHN0cmFwKHVpbnQ2NCxhZGRyZXNzLGFkZHJlc3MsYWRkcmVzcyl2b2lkCgljYWxsc3ViIGJvb3RzdHJhcAoJaW50YyAxIC8vIDEKCXJldHVybgoKLy8gYm9vdHN0cmFwKGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRCwgZ292ZXJub3I6IEFkZHJlc3MsIHZlcmlmaWNhdGlvblNldHRlcjogQWRkcmVzcywgZW5nYWdlbWVudFNldHRlcjogQWRkcmVzcyk6IHZvaWQKLy8KLy8gQmlhdGVjIGRlcGxveXMgc2luZ2xlIGlkZW50aXR5IHByb3ZpZGVyIHNtYXJ0IGNvbnRyYWN0Ci8vIEBwYXJhbSBhcHBCaWF0ZWNDb25maWdQcm92aWRlciBCaWF0ZWMgYW1tIHByb3ZpZGVyCmJvb3RzdHJhcDoKCXByb3RvIDQgMAoKCS8vIFB1c2ggZW1wdHkgYnl0ZXMgYWZ0ZXIgdGhlIGZyYW1lIHBvaW50ZXIgdG8gcmVzZXJ2ZSBzcGFjZSBmb3IgbG9jYWwgdmFyaWFibGVzCglieXRlYyAwIC8vIDB4CglkdXBuIDIKCgkvLyAqaWYwX2NvbmRpdGlvbgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMTUKCS8vIHRoaXMuYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZXhpc3RzCgl0eG5hIEFwcGxpY2F0aW9ucyAwCglieXRlYyA0IC8vICAiQiIKCWFwcF9nbG9iYWxfZ2V0X2V4Cglzd2FwCglwb3AKCWJ6ICppZjBfZWxzZQoKCS8vICppZjBfY29uc2VxdWVudAoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMTgKCS8vIGN1cnJlbnRDb25maWcgPSB0aGlzLmFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLnZhbHVlIGFzIEFwcElECglieXRlYyA0IC8vICAiQiIKCWFwcF9nbG9iYWxfZ2V0CglmcmFtZV9idXJ5IDAgLy8gY3VycmVudENvbmZpZzogdWludDY0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMTkKCS8vIGFkZHJlc3NVZHBhdGVyID0gY3VycmVudENvbmZpZy5nbG9iYWxTdGF0ZSgndScpIGFzIEFkZHJlc3MKCWZyYW1lX2RpZyAwIC8vIGN1cnJlbnRDb25maWc6IHVpbnQ2NAoJYnl0ZWMgMTUgLy8gICJ1IgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGN1cnJlbnRDb25maWcuZ2xvYmFsU3RhdGUoJ3UnKQoJYXNzZXJ0CglmcmFtZV9idXJ5IDEgLy8gYWRkcmVzc1VkcGF0ZXI6IGFkZHJlc3MKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIyMAoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gYWRkcmVzc1VkcGF0ZXIsICdFX1VQREFURVInKQoJdHhuIFNlbmRlcgoJZnJhbWVfZGlnIDEgLy8gYWRkcmVzc1VkcGF0ZXI6IGFkZHJlc3MKCT09CgoJLy8gRV9VUERBVEVSCglhc3NlcnQKCWIgKmlmMF9lbmQKCippZjBfZWxzZToKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjIyCgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFwcC5jcmVhdG9yLCAnT25seSBjcmVhdG9yIG9mIHRoZSBhcHAgY2FuIHNldCBpdCB1cCcpCgl0eG4gU2VuZGVyCgl0eG5hIEFwcGxpY2F0aW9ucyAwCglhcHBfcGFyYW1zX2dldCBBcHBDcmVhdG9yCglwb3AKCT09CgoJLy8gT25seSBjcmVhdG9yIG9mIHRoZSBhcHAgY2FuIHNldCBpdCB1cAoJYXNzZXJ0CgoqaWYwX2VuZDoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjI0CgkvLyB0aGlzLmFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLnZhbHVlID0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIKCWJ5dGVjIDQgLy8gICJCIgoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYXBwX2dsb2JhbF9wdXQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIyNQoJLy8gdGhpcy5nb3Zlcm5vci52YWx1ZSA9IGdvdmVybm9yCglwdXNoYnl0ZXMgMHg2NyAvLyAiZyIKCWZyYW1lX2RpZyAtMiAvLyBnb3Zlcm5vcjogQWRkcmVzcwoJYXBwX2dsb2JhbF9wdXQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIyNgoJLy8gdGhpcy52ZXJpZmljYXRpb25TZXR0ZXIudmFsdWUgPSB2ZXJpZmljYXRpb25TZXR0ZXIKCXB1c2hieXRlcyAweDc2IC8vICJ2IgoJZnJhbWVfZGlnIC0zIC8vIHZlcmlmaWNhdGlvblNldHRlcjogQWRkcmVzcwoJYXBwX2dsb2JhbF9wdXQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjIyNwoJLy8gdGhpcy5lbmdhZ2VtZW50U2V0dGVyLnZhbHVlID0gZW5nYWdlbWVudFNldHRlcgoJYnl0ZWMgMTYgLy8gICJlIgoJZnJhbWVfZGlnIC00IC8vIGVuZ2FnZW1lbnRTZXR0ZXI6IEFkZHJlc3MKCWFwcF9nbG9iYWxfcHV0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMjkKCS8vIHBhdXNlZCA9IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdzJykgYXMgdWludDY0CglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglieXRlYyA1IC8vICAicyIKCWFwcF9nbG9iYWxfZ2V0X2V4CgoJLy8gZ2xvYmFsIHN0YXRlIHZhbHVlIGRvZXMgbm90IGV4aXN0OiBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgncycpCglhc3NlcnQKCWZyYW1lX2J1cnkgMiAvLyBwYXVzZWQ6IHVpbnQ2NAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjMwCgkvLyBhc3NlcnQocGF1c2VkID09PSAwLCAnRVJSX1BBVVNFRCcpCglmcmFtZV9kaWcgMiAvLyBwYXVzZWQ6IHVpbnQ2NAoJaW50YyAwIC8vIDAKCT09CgoJLy8gRVJSX1BBVVNFRAoJYXNzZXJ0CglyZXRzdWIKCi8vIHVwZGF0ZUFwcGxpY2F0aW9uKHVpbnQ2NCxieXRlW10pdm9pZAoqYWJpX3JvdXRlX3VwZGF0ZUFwcGxpY2F0aW9uOgoJLy8gbmV3VmVyc2lvbjogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAyCglleHRyYWN0IDIgMAoKCS8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWJ0b2kKCgkvLyBleGVjdXRlIHVwZGF0ZUFwcGxpY2F0aW9uKHVpbnQ2NCxieXRlW10pdm9pZAoJY2FsbHN1YiB1cGRhdGVBcHBsaWNhdGlvbgoJaW50YyAxIC8vIDEKCXJldHVybgoKLy8gdXBkYXRlQXBwbGljYXRpb24oYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElELCBuZXdWZXJzaW9uOiBieXRlcyk6IHZvaWQKLy8KLy8gYWRkcmVzc1VkcGF0ZXIgZnJvbSBnbG9iYWwgYmlhdGVjIGNvbmZpZ3VyYXRpb24gaXMgYWxsb3dlZCB0byB1cGRhdGUgYXBwbGljYXRpb24KdXBkYXRlQXBwbGljYXRpb246Cglwcm90byAyIDAKCgkvLyBQdXNoIGVtcHR5IGJ5dGVzIGFmdGVyIHRoZSBmcmFtZSBwb2ludGVyIHRvIHJlc2VydmUgc3BhY2UgZm9yIGxvY2FsIHZhcmlhYmxlcwoJYnl0ZWMgMCAvLyAweAoJZHVwCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMzcKCS8vIGFzc2VydChhcHBCaWF0ZWNDb25maWdQcm92aWRlciA9PT0gdGhpcy5hcHBCaWF0ZWNDb25maWdQcm92aWRlci52YWx1ZSwgJ0NvbmZpZ3VyYXRpb24gYXBwIGRvZXMgbm90IG1hdGNoJykKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDQgLy8gICJCIgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gQ29uZmlndXJhdGlvbiBhcHAgZG9lcyBub3QgbWF0Y2gKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjM4CgkvLyBhZGRyZXNzVWRwYXRlciA9IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCd1JykgYXMgQWRkcmVzcwoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYnl0ZWMgMTUgLy8gICJ1IgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCd1JykKCWFzc2VydAoJZnJhbWVfYnVyeSAwIC8vIGFkZHJlc3NVZHBhdGVyOiBhZGRyZXNzCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyMzkKCS8vIGFzc2VydCh0aGlzLnR4bi5zZW5kZXIgPT09IGFkZHJlc3NVZHBhdGVyLCAnT25seSBhZGRyZXNzVWRwYXRlciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB1cGRhdGUgYXBwbGljYXRpb24nKQoJdHhuIFNlbmRlcgoJZnJhbWVfZGlnIDAgLy8gYWRkcmVzc1VkcGF0ZXI6IGFkZHJlc3MKCT09CgoJLy8gT25seSBhZGRyZXNzVWRwYXRlciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB1cGRhdGUgYXBwbGljYXRpb24KCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjQwCgkvLyBwYXVzZWQgPSBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgncycpIGFzIHVpbnQ2NAoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYnl0ZWMgNSAvLyAgInMiCglhcHBfZ2xvYmFsX2dldF9leAoKCS8vIGdsb2JhbCBzdGF0ZSB2YWx1ZSBkb2VzIG5vdCBleGlzdDogYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3MnKQoJYXNzZXJ0CglmcmFtZV9idXJ5IDEgLy8gcGF1c2VkOiB1aW50NjQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI0MQoJLy8gYXNzZXJ0KHBhdXNlZCA9PT0gMCwgJ0VSUl9QQVVTRUQnKQoJZnJhbWVfZGlnIDEgLy8gcGF1c2VkOiB1aW50NjQKCWludGMgMCAvLyAwCgk9PQoKCS8vIEVSUl9QQVVTRUQKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjQyCgkvLyBsb2codmVyc2lvbikKCWJ5dGVjIDggLy8gIkJJQVRFQy1JREVOVC0wMS0wMy0wMiIKCWxvZwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjQzCgkvLyBsb2cobmV3VmVyc2lvbikKCWZyYW1lX2RpZyAtMiAvLyBuZXdWZXJzaW9uOiBieXRlcwoJbG9nCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyNDQKCS8vIHRoaXMudmVyc2lvbi52YWx1ZSA9IG5ld1ZlcnNpb24KCWJ5dGVjIDExIC8vICAic2N2ZXIiCglmcmFtZV9kaWcgLTIgLy8gbmV3VmVyc2lvbjogYnl0ZXMKCWFwcF9nbG9iYWxfcHV0CglyZXRzdWIKCi8vIHNlbGZSZWdpc3RyYXRpb24oYWRkcmVzcywodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkpdm9pZAoqYWJpX3JvdXRlX3NlbGZSZWdpc3RyYXRpb246CgkvLyBpbmZvOiAodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkKCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCgkvLyB1c2VyOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglkdXAKCWxlbgoJaW50YyAyIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDEgKHVzZXIpIGZvciBzZWxmUmVnaXN0cmF0aW9uIG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyBleGVjdXRlIHNlbGZSZWdpc3RyYXRpb24oYWRkcmVzcywodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkpdm9pZAoJY2FsbHN1YiBzZWxmUmVnaXN0cmF0aW9uCglpbnRjIDEgLy8gMQoJcmV0dXJuCgovLyBzZWxmUmVnaXN0cmF0aW9uKHVzZXI6IEFkZHJlc3MsIGluZm86IElkZW50aXR5SW5mbyk6IHZvaWQKc2VsZlJlZ2lzdHJhdGlvbjoKCXByb3RvIDIgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjQ4CgkvLyBhc3NlcnQoIXRoaXMuaWRlbnRpdGllcyh1c2VyKS5leGlzdHMsICdTZWxmIHJlZ2lzdHJhdGlvbiBjYW5ub3QgYmUgZXhlY3V0ZWQgaWYgYWRkcmVzcyBpcyBhbHJlYWR5IHJlZ2lzdGVyZWQnKQoJYnl0ZWMgMyAvLyAgImkiCglmcmFtZV9kaWcgLTEgLy8gdXNlcjogQWRkcmVzcwoJY29uY2F0Cglib3hfbGVuCglzd2FwCglwb3AKCSEKCgkvLyBTZWxmIHJlZ2lzdHJhdGlvbiBjYW5ub3QgYmUgZXhlY3V0ZWQgaWYgYWRkcmVzcyBpcyBhbHJlYWR5IHJlZ2lzdGVyZWQKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjUxCgkvLyBhc3NlcnQoaW5mby52ZXJpZmljYXRpb25TdGF0dXMgPT09IDEsICdWZXJpZmljYXRpb24gc3RhdHVzIG11c3QgYmUgZW1wdHknKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgNDEgOAoJYnRvaQoJaW50YyAxIC8vIDEKCT09CgoJLy8gVmVyaWZpY2F0aW9uIHN0YXR1cyBtdXN0IGJlIGVtcHR5Cglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI1MwoJLy8gYXNzZXJ0KGluZm8udmVyaWZpY2F0aW9uQ2xhc3MgPT09IDAsICd2ZXJpZmljYXRpb25DbGFzcyBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMCA4CglidG9pCglpbnRjIDAgLy8gMAoJPT0KCgkvLyB2ZXJpZmljYXRpb25DbGFzcyBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjU2CgkvLyBhc3NlcnQoaW5mby5wZXJzb25VVUlEID09PSAnMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwJywgJ3BlcnNvblVVSUQgbXVzdCBlcXVhbCB0byAwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWludGMgOCAvLyA1MAoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJdW5jb3ZlciAyCglleHRyYWN0X3VpbnQxNgoJZHVwIC8vIGR1cGxpY2F0ZSBzdGFydCBvZiBlbGVtZW50Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5Cglzd2FwCglleHRyYWN0X3VpbnQxNiAvLyBnZXQgbnVtYmVyIG9mIGVsZW1lbnRzCglpbnRjIDEgLy8gIGdldCB0eXBlIGxlbmd0aAoJKiAvLyBtdWx0aXBseSBieSB0eXBlIGxlbmd0aAoJaW50YyAzIC8vIDIKCSsgLy8gYWRkIHR3byBmb3IgbGVuZ3RoCglleHRyYWN0MwoJZXh0cmFjdCAyIDAKCWJ5dGVjIDcgLy8gICIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiCgk9PQoKCS8vIHBlcnNvblVVSUQgbXVzdCBlcXVhbCB0byAwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjU4CgkvLyBhc3NlcnQoaW5mby5sZWdhbEVudGl0eVVVSUQgPT09ICcwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAnLCAnbGVnYWxFbnRpdHlVVUlEIG11c3QgZXF1YWwgdG8gMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDkgLy8gNTIKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXVuY292ZXIgMgoJZXh0cmFjdF91aW50MTYKCWR1cCAvLyBkdXBsaWNhdGUgc3RhcnQgb2YgZWxlbWVudAoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJc3dhcAoJZXh0cmFjdF91aW50MTYgLy8gZ2V0IG51bWJlciBvZiBlbGVtZW50cwoJaW50YyAxIC8vICBnZXQgdHlwZSBsZW5ndGgKCSogLy8gbXVsdGlwbHkgYnkgdHlwZSBsZW5ndGgKCWludGMgMyAvLyAyCgkrIC8vIGFkZCB0d28gZm9yIGxlbmd0aAoJZXh0cmFjdDMKCWV4dHJhY3QgMiAwCglieXRlYyA3IC8vICAiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIgoJPT0KCgkvLyBsZWdhbEVudGl0eVVVSUQgbXVzdCBlcXVhbCB0byAwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjYwCgkvLyBhc3NlcnQoaW5mby5iaWF0ZWNFbmdhZ2VtZW50UG9pbnRzID09PSAwLCAnYmlhdGVjRW5nYWdlbWVudFBvaW50cyBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgNTQgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gYmlhdGVjRW5nYWdlbWVudFBvaW50cyBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjYyCgkvLyBhc3NlcnQoaW5mby5iaWF0ZWNFbmdhZ2VtZW50UmFuayA9PT0gMCwgJ2JpYXRlY0VuZ2FnZW1lbnRSYW5rIG11c3QgZXF1YWwgdG8gMCcpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA2MiA4CglidG9pCglpbnRjIDAgLy8gMAoJPT0KCgkvLyBiaWF0ZWNFbmdhZ2VtZW50UmFuayBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjY0CgkvLyBhc3NlcnQoaW5mby5hdm1FbmdhZ2VtZW50UG9pbnRzID09PSAwLCAnYXZtRW5nYWdlbWVudFBvaW50cyBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgNzAgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gYXZtRW5nYWdlbWVudFBvaW50cyBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjY2CgkvLyBhc3NlcnQoaW5mby5hdm1FbmdhZ2VtZW50UmFuayA9PT0gMCwgJ2F2bUVuZ2FnZW1lbnRSYW5rIG11c3QgZXF1YWwgdG8gMCcpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA3OCA4CglidG9pCglpbnRjIDAgLy8gMAoJPT0KCgkvLyBhdm1FbmdhZ2VtZW50UmFuayBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjY4CgkvLyBhc3NlcnQoaW5mby50cmFkaW5nRW5nYWdlbWVudFBvaW50cyA9PT0gMCwgJ3RyYWRpbmdFbmdhZ2VtZW50UG9pbnRzIG11c3QgZXF1YWwgdG8gMCcpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA4NiA4CglidG9pCglpbnRjIDAgLy8gMAoJPT0KCgkvLyB0cmFkaW5nRW5nYWdlbWVudFBvaW50cyBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjcwCgkvLyBhc3NlcnQoaW5mby50cmFkaW5nRW5nYWdlbWVudFJhbmsgPT09IDAsICd0cmFkaW5nRW5nYWdlbWVudFJhbmsgbXVzdCBlcXVhbCB0byAwJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDk0IDgKCWJ0b2kKCWludGMgMCAvLyAwCgk9PQoKCS8vIHRyYWRpbmdFbmdhZ2VtZW50UmFuayBtdXN0IGVxdWFsIHRvIDAKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjcyCgkvLyBhc3NlcnQoaW5mby5pc0xvY2tlZCA9PT0gZmFsc2UsICdpc0xvY2tlZCBtdXN0IGVxdWFsIHRvIGZhbHNlJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDQgLy8gNjQKCWdldGJpdAoJaW50YyAwIC8vIDAKCT09CgoJLy8gaXNMb2NrZWQgbXVzdCBlcXVhbCB0byBmYWxzZQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyNzQKCS8vIGFzc2VydChpbmZvLmt5Y0V4cGlyYXRpb24gPT09IDAsICdreWNFeHBpcmF0aW9uIG11c3QgZXF1YWwgdG8gMCcpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCAyNSA4CglidG9pCglpbnRjIDAgLy8gMAoJPT0KCgkvLyBreWNFeHBpcmF0aW9uIG11c3QgZXF1YWwgdG8gMAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyNzYKCS8vIGFzc2VydChpbmZvLmludmVzdG9yRm9yRXhwaXJhdGlvbiA9PT0gMCwgJ2ludmVzdG9yRm9yRXhwaXJhdGlvbiBtdXN0IGVxdWFsIHRvIDAnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMzMgOAoJYnRvaQoJaW50YyAwIC8vIDAKCT09CgoJLy8gaW52ZXN0b3JGb3JFeHBpcmF0aW9uIG11c3QgZXF1YWwgdG8gMAoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyNzgKCS8vIGFzc2VydChpbmZvLmlzUHJvZmVzc2lvbmFsSW52ZXN0b3IgPT09IGZhbHNlLCAnaXNQcm9mZXNzaW9uYWxJbnZlc3RvciBtdXN0IGVxdWFsIHRvIGZhbHNlJykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDYgLy8gODE2CglnZXRiaXQKCWludGMgMCAvLyAwCgk9PQoKCS8vIGlzUHJvZmVzc2lvbmFsSW52ZXN0b3IgbXVzdCBlcXVhbCB0byBmYWxzZQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyODAKCS8vIGFzc2VydChpbmZvLmZlZU11bHRpcGxpZXJCYXNlID09PSBTQ0FMRSwgJ0ZlZU11bHRpcGxpZXJCYXNlIG11c3QgYmUgc2V0IHByb3Blcmx5JykKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDE3IDgKCWJ0b2kKCWludGMgNSAvLyAxMDAwMDAwMDAwCgk9PQoKCS8vIEZlZU11bHRpcGxpZXJCYXNlIG11c3QgYmUgc2V0IHByb3Blcmx5Cglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI4MQoJLy8gYXNzZXJ0KGluZm8uZmVlTXVsdGlwbGllciA9PT0gKCgyICogU0NBTEUpIGFzIHVpbnQ2NCksICdJbml0aWFsIGZlZSBtdWx0aXBsaWVyIG11c3QgYmUgc2V0IHRvIDIgKiBTQ0FMRScpCglmcmFtZV9kaWcgLTIgLy8gaW5mbzogSWRlbnRpdHlJbmZvCglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA5IDgKCWJ0b2kKCXB1c2hpbnQgMjAwMDAwMDAwMAoJPT0KCgkvLyBJbml0aWFsIGZlZSBtdWx0aXBsaWVyIG11c3QgYmUgc2V0IHRvIDIgKiBTQ0FMRQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyODIKCS8vIHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZSA9IGluZm8KCWJ5dGVjIDMgLy8gICJpIgoJZnJhbWVfZGlnIC0xIC8vIHVzZXI6IEFkZHJlc3MKCWNvbmNhdAoJZHVwCglib3hfZGVsCglwb3AKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCWJveF9wdXQKCXJldHN1YgoKLy8gc2V0SW5mbyhhZGRyZXNzLCh1aW50NjQsYm9vbCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wsc3RyaW5nLHN0cmluZyx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKSl2b2lkCiphYmlfcm91dGVfc2V0SW5mbzoKCS8vIGluZm86ICh1aW50NjQsYm9vbCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wsc3RyaW5nLHN0cmluZyx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKQoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMgoKCS8vIHVzZXI6IGFkZHJlc3MKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWR1cAoJbGVuCglpbnRjIDIgLy8gMzIKCT09CgoJLy8gYXJndW1lbnQgMSAodXNlcikgZm9yIHNldEluZm8gbXVzdCBiZSBhIGFkZHJlc3MKCWFzc2VydAoKCS8vIGV4ZWN1dGUgc2V0SW5mbyhhZGRyZXNzLCh1aW50NjQsYm9vbCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wsc3RyaW5nLHN0cmluZyx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKSl2b2lkCgljYWxsc3ViIHNldEluZm8KCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIHNldEluZm8odXNlcjogQWRkcmVzcywgaW5mbzogSWRlbnRpdHlJbmZvKTogdm9pZAovLwovLyBUaGlzIG1ldGhvZCBjYW4gc2V0IGZlZXMsIHZlcmlmaWNhdGlvbiBjbGFzcywgZW5nYWdlbWVudCBjbGFzcyAuLiBPbmx5IGVuZ2FnZW1lbnRTZXR0ZXIgaXMgYWxsb3dlZCB0byBleGVjdXRlIHRoaXMgbWV0aG9kLgovLyBAcGFyYW0gdXNlciBVc2VyIGFkZHJlc3MgdG8gc2V0IGluZm8gZm9yCi8vIEBwYXJhbSBpbmZvIERhdGEgdG8gYmUgc2V0CnNldEluZm86Cglwcm90byAyIDAKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI5MQoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gdGhpcy5lbmdhZ2VtZW50U2V0dGVyLnZhbHVlKQoJdHhuIFNlbmRlcgoJYnl0ZWMgMTYgLy8gICJlIgoJYXBwX2dsb2JhbF9nZXQKCT09Cglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjI5MgoJLy8gYXNzZXJ0KGluZm8uZmVlTXVsdGlwbGllckJhc2UgPT09IFNDQUxFLCAnRmVlTXVsdGlwbGllckJhc2UgbXVzdCBiZSBzZXQgcHJvcGVybHknKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMTcgOAoJYnRvaQoJaW50YyA1IC8vIDEwMDAwMDAwMDAKCT09CgoJLy8gRmVlTXVsdGlwbGllckJhc2UgbXVzdCBiZSBzZXQgcHJvcGVybHkKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MjkzCgkvLyBhc3NlcnQoaW5mby52ZXJpZmljYXRpb25DbGFzcyA8PSA0LCAnVmVyaWZpY2F0aW9uIGNsYXNzIG91dCBvZiBib3VuZHMnKQoJZnJhbWVfZGlnIC0yIC8vIGluZm86IElkZW50aXR5SW5mbwoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMCA4CglidG9pCglpbnRjIDcgLy8gNAoJPD0KCgkvLyBWZXJpZmljYXRpb24gY2xhc3Mgb3V0IG9mIGJvdW5kcwoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czoyOTQKCS8vIHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZSA9IGluZm8KCWJ5dGVjIDMgLy8gICJpIgoJZnJhbWVfZGlnIC0xIC8vIHVzZXI6IEFkZHJlc3MKCWNvbmNhdAoJZHVwCglib3hfZGVsCglwb3AKCWZyYW1lX2RpZyAtMiAvLyBpbmZvOiBJZGVudGl0eUluZm8KCWJveF9wdXQKCXJldHN1YgoKLy8gc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbih1aW50NjQsYnl0ZVtdLGJ5dGVbXSxieXRlW10sdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0KXZvaWQKKmFiaV9yb3V0ZV9zZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uOgoJLy8gZmVlOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDgKCWJ0b2kKCgkvLyB2b3RlS2V5RGlsdXRpb246IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgNwoJYnRvaQoKCS8vIHZvdGVMYXN0OiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDYKCWJ0b2kKCgkvLyB2b3RlRmlyc3Q6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgNQoJYnRvaQoKCS8vIHN0YXRlUHJvb2ZQSzogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyA0CglleHRyYWN0IDIgMAoKCS8vIHNlbGVjdGlvblBLOiBieXRlW10KCXR4bmEgQXBwbGljYXRpb25BcmdzIDMKCWV4dHJhY3QgMiAwCgoJLy8gdm90ZVBLOiBieXRlW10KCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCWV4dHJhY3QgMiAwCgoJLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJYnRvaQoKCS8vIGV4ZWN1dGUgc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbih1aW50NjQsYnl0ZVtdLGJ5dGVbXSxieXRlW10sdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0KXZvaWQKCWNhbGxzdWIgc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbgoJaW50YyAxIC8vIDEKCXJldHVybgoKLy8gc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbihhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQsIHZvdGVQSzogYnl0ZXMsIHNlbGVjdGlvblBLOiBieXRlcywgc3RhdGVQcm9vZlBLOiBieXRlcywgdm90ZUZpcnN0OiB1aW50NjQsIHZvdGVMYXN0OiB1aW50NjQsIHZvdGVLZXlEaWx1dGlvbjogdWludDY0LCBmZWU6IHVpbnQ2NCk6IHZvaWQKLy8KLy8gYWRkcmVzc0V4ZWN1dGl2ZUZlZSBjYW4gcGVyZm9tIGtleSByZWdpc3RyYXRpb24gZm9yIHRoaXMgTFAgcG9vbAovLwovLyBPbmx5IGFkZHJlc3NFeGVjdXRpdmVGZWUgaXMgYWxsb3dlZCB0byBleGVjdXRlIHRoaXMgbWV0aG9kLgpzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uOgoJcHJvdG8gOCAwCgoJLy8gUHVzaCBlbXB0eSBieXRlcyBhZnRlciB0aGUgZnJhbWUgcG9pbnRlciB0byByZXNlcnZlIHNwYWNlIGZvciBsb2NhbCB2YXJpYWJsZXMKCWJ5dGVjIDAgLy8gMHgKCWR1cAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzAzCgkvLyBhc3NlcnQoYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIgPT09IHRoaXMuYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIudmFsdWUsICdDb25maWd1cmF0aW9uIGFwcCBkb2VzIG5vdCBtYXRjaCcpCglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglieXRlYyA0IC8vICAiQiIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoKCS8vIENvbmZpZ3VyYXRpb24gYXBwIGRvZXMgbm90IG1hdGNoCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMwNAoJLy8gYWRkcmVzc0V4ZWN1dGl2ZUZlZSA9IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdlZicpIGFzIEFkZHJlc3MKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDEyIC8vICAiZWYiCglhcHBfZ2xvYmFsX2dldF9leAoKCS8vIGdsb2JhbCBzdGF0ZSB2YWx1ZSBkb2VzIG5vdCBleGlzdDogYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ2VmJykKCWFzc2VydAoJZnJhbWVfYnVyeSAwIC8vIGFkZHJlc3NFeGVjdXRpdmVGZWU6IGFkZHJlc3MKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMwNQoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gYWRkcmVzc0V4ZWN1dGl2ZUZlZSwgJ09ubHkgZmVlIGV4ZWN1dG9yIHNldHVwIGluIHRoZSBjb25maWcgY2FuIHRha2UgdGhlIGNvbGxlY3RlZCBmZWVzJykKCXR4biBTZW5kZXIKCWZyYW1lX2RpZyAwIC8vIGFkZHJlc3NFeGVjdXRpdmVGZWU6IGFkZHJlc3MKCT09CgoJLy8gT25seSBmZWUgZXhlY3V0b3Igc2V0dXAgaW4gdGhlIGNvbmZpZyBjYW4gdGFrZSB0aGUgY29sbGVjdGVkIGZlZXMKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzA2CgkvLyBwYXVzZWQgPSBhcHBCaWF0ZWNDb25maWdQcm92aWRlci5nbG9iYWxTdGF0ZSgncycpIGFzIHVpbnQ2NAoJZnJhbWVfZGlnIC0xIC8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRAoJYnl0ZWMgNSAvLyAgInMiCglhcHBfZ2xvYmFsX2dldF9leAoKCS8vIGdsb2JhbCBzdGF0ZSB2YWx1ZSBkb2VzIG5vdCBleGlzdDogYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3MnKQoJYXNzZXJ0CglmcmFtZV9idXJ5IDEgLy8gcGF1c2VkOiB1aW50NjQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMwNwoJLy8gYXNzZXJ0KHBhdXNlZCA9PT0gMCwgJ0VSUl9QQVVTRUQnKQoJZnJhbWVfZGlnIDEgLy8gcGF1c2VkOiB1aW50NjQKCWludGMgMCAvLyAwCgk9PQoKCS8vIEVSUl9QQVVTRUQKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzA4CgkvLyBzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uKHsKCS8vICAgICAgIHNlbGVjdGlvblBLOiBzZWxlY3Rpb25QSywKCS8vICAgICAgIHN0YXRlUHJvb2ZQSzogc3RhdGVQcm9vZlBLLAoJLy8gICAgICAgdm90ZUZpcnN0OiB2b3RlRmlyc3QsCgkvLyAgICAgICB2b3RlS2V5RGlsdXRpb246IHZvdGVLZXlEaWx1dGlvbiwKCS8vICAgICAgIHZvdGVMYXN0OiB2b3RlTGFzdCwKCS8vICAgICAgIHZvdGVQSzogdm90ZVBLLAoJLy8gICAgICAgZmVlOiBmZWUsCgkvLyAgICAgfSkKCWl0eG5fYmVnaW4KCWludGMgMyAvLyAga2V5cmVnCglpdHhuX2ZpZWxkIFR5cGVFbnVtCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMDkKCS8vIHNlbGVjdGlvblBLOiBzZWxlY3Rpb25QSwoJZnJhbWVfZGlnIC0zIC8vIHNlbGVjdGlvblBLOiBieXRlcwoJaXR4bl9maWVsZCBTZWxlY3Rpb25QSwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzEwCgkvLyBzdGF0ZVByb29mUEs6IHN0YXRlUHJvb2ZQSwoJZnJhbWVfZGlnIC00IC8vIHN0YXRlUHJvb2ZQSzogYnl0ZXMKCWl0eG5fZmllbGQgU3RhdGVQcm9vZlBLCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMTEKCS8vIHZvdGVGaXJzdDogdm90ZUZpcnN0CglmcmFtZV9kaWcgLTUgLy8gdm90ZUZpcnN0OiB1aW50NjQKCWl0eG5fZmllbGQgVm90ZUZpcnN0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMTIKCS8vIHZvdGVLZXlEaWx1dGlvbjogdm90ZUtleURpbHV0aW9uCglmcmFtZV9kaWcgLTcgLy8gdm90ZUtleURpbHV0aW9uOiB1aW50NjQKCWl0eG5fZmllbGQgVm90ZUtleURpbHV0aW9uCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMTMKCS8vIHZvdGVMYXN0OiB2b3RlTGFzdAoJZnJhbWVfZGlnIC02IC8vIHZvdGVMYXN0OiB1aW50NjQKCWl0eG5fZmllbGQgVm90ZUxhc3QKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMxNAoJLy8gdm90ZVBLOiB2b3RlUEsKCWZyYW1lX2RpZyAtMiAvLyB2b3RlUEs6IGJ5dGVzCglpdHhuX2ZpZWxkIFZvdGVQSwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzE1CgkvLyBmZWU6IGZlZQoJZnJhbWVfZGlnIC04IC8vIGZlZTogdWludDY0CglpdHhuX2ZpZWxkIEZlZQoKCS8vIFN1Ym1pdCBpbm5lciB0cmFuc2FjdGlvbgoJaXR4bl9zdWJtaXQKCXJldHN1YgoKLy8gZ2V0VXNlcihhZGRyZXNzLHVpbnQ4KSh1aW50OCx1aW50NjQsdWludDY0LGJvb2wsc3RyaW5nLHN0cmluZyx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCxib29sKQoqYWJpX3JvdXRlX2dldFVzZXI6CgkvLyBUaGUgQUJJIHJldHVybiBwcmVmaXgKCWJ5dGVjIDYgLy8gMHgxNTFmN2M3NQoKCS8vIHY6IHVpbnQ4Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyAyCglkdXAKCWxlbgoJaW50YyAxIC8vIDEKCT09CgoJLy8gYXJndW1lbnQgMCAodikgZm9yIGdldFVzZXIgbXVzdCBiZSBhIHVpbnQ4Cglhc3NlcnQKCWJ0b2kKCgkvLyB1c2VyOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglkdXAKCWxlbgoJaW50YyAyIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDEgKHVzZXIpIGZvciBnZXRVc2VyIG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyBleGVjdXRlIGdldFVzZXIoYWRkcmVzcyx1aW50OCkodWludDgsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHVpbnQ2NCx1aW50NjQsYm9vbCkKCWNhbGxzdWIgZ2V0VXNlcgoJY29uY2F0Cglsb2cKCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIGdldFVzZXIodXNlcjogQWRkcmVzcywgdjogdWludDgpOiBVc2VySW5mb1YxCi8vCi8vIFJldHVybnMgdXNlciBpbmZvcm1hdGlvbiAtIGZlZSBtdWx0aXBsaWVyLCB2ZXJpZmljYXRpb24gY2xhc3MsIGVuZ2FnZW1lbnQgY2xhc3MgLi4KLy8KLy8gQHBhcmFtIHVzZXIgR2V0IGluZm8gZm9yIHNwZWNpZmljIHVzZXIgYWRkcmVzcwovLyBAcGFyYW0gdiBWZXJzaW9uIG9mIHRoZSBkYXRhIHN0cnVjdHVyZSB0byByZXR1cm4KZ2V0VXNlcjoKCXByb3RvIDIgMQoKCS8vIFB1c2ggZW1wdHkgYnl0ZXMgYWZ0ZXIgdGhlIGZyYW1lIHBvaW50ZXIgdG8gcmVzZXJ2ZSBzcGFjZSBmb3IgbG9jYWwgdmFyaWFibGVzCglieXRlYyAwIC8vIDB4CglkdXBuIDIKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjMyNwoJLy8gYXNzZXJ0KHYgPT09IDEsICJDdXJyZW50bHkgc3VwcG9ydGVkIHZlcnNpb24gb2YgdGhlIGRhdGEgc3RydWN0dXJlIGlzICcxJyIpCglmcmFtZV9kaWcgLTIgLy8gdjogdWludDgKCWludGMgMSAvLyAxCgk9PQoKCS8vIEN1cnJlbnRseSBzdXBwb3J0ZWQgdmVyc2lvbiBvZiB0aGUgZGF0YSBzdHJ1Y3R1cmUgaXMgJzEnCglhc3NlcnQKCgkvLyAqaWYxX2NvbmRpdGlvbgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozMjgKCS8vICF0aGlzLmlkZW50aXRpZXModXNlcikuZXhpc3RzCglieXRlYyAzIC8vICAiaSIKCWZyYW1lX2RpZyAtMSAvLyB1c2VyOiBBZGRyZXNzCgljb25jYXQKCWJveF9sZW4KCXN3YXAKCXBvcAoJIQoJYnogKmlmMV9lbmQKCgkvLyAqaWYxX2NvbnNlcXVlbnQKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzI5CgkvLyByZXROb0lkZW50aXR5OiBVc2VySW5mb1YxID0gewoJLy8gICAgICAgICB2ZXJzaW9uOiB2LAoJLy8gICAgICAgICBiYXNlOiBTQ0FMRSBhcyB1aW50NjQsCgkvLyAgICAgICAgIGZlZU11bHRpcGxpZXI6ICgyICogU0NBTEUpIGFzIHVpbnQ2NCwKCS8vICAgICAgICAgaXNMb2NrZWQ6IGZhbHNlLAoJLy8gICAgICAgICB2ZXJpZmljYXRpb25DbGFzczogMCwKCS8vICAgICAgICAgdmVyaWZpY2F0aW9uU3RhdHVzOiAwLAoJLy8gICAgICAgICBiaWF0ZWNFbmdhZ2VtZW50UG9pbnRzOiAwLAoJLy8gICAgICAgICBiaWF0ZWNFbmdhZ2VtZW50UmFuazogMCwKCS8vICAgICAgICAgYXZtRW5nYWdlbWVudFBvaW50czogMCwKCS8vICAgICAgICAgYXZtRW5nYWdlbWVudFJhbms6IDAsCgkvLyAgICAgICAgIHRyYWRpbmdFbmdhZ2VtZW50UG9pbnRzOiAwLAoJLy8gICAgICAgICB0cmFkaW5nRW5nYWdlbWVudFJhbms6IDAsCgkvLyAgICAgICAgIGt5Y0V4cGlyYXRpb246IDAsCgkvLyAgICAgICAgIGludmVzdG9yRm9yRXhwaXJhdGlvbjogMCwKCS8vICAgICAgICAgaXNQcm9mZXNzaW9uYWxJbnZlc3RvcjogZmFsc2UsCgkvLyAgICAgICAgIGlzQ29tcGFueTogZmFsc2UsCgkvLyAgICAgICAgIHBlcnNvblVVSUQ6ICcnLAoJLy8gICAgICAgICBsZWdhbEVudGl0eVVVSUQ6ICcnLAoJLy8gICAgICAgfQoJYnl0ZWMgMCAvLyAgaW5pdGlhbCBoZWFkCglieXRlYyAwIC8vICBpbml0aWFsIHRhaWwKCWJ5dGVjIDEzIC8vICBpbml0aWFsIGhlYWQgb2Zmc2V0CglmcmFtZV9kaWcgLTIgLy8gdjogdWludDgKCWl0b2IKCWV4dHJhY3QgNyAxCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAxIC8vIDB4MDAwMDAwMDAwMDAwMDAwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDIgLy8gMHgwMAoJaW50YyAwIC8vIDAKCWR1cAoJc2V0Yml0CgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAxNCAvLyAweDAwMDAKCWNhbGxzdWIgKnByb2Nlc3NfZHluYW1pY190dXBsZV9lbGVtZW50CglieXRlYyAxNCAvLyAweDAwMDAKCWNhbGxzdWIgKnByb2Nlc3NfZHluYW1pY190dXBsZV9lbGVtZW50CglieXRlYyAxIC8vIDB4MDAwMDAwMDAwMDAwMDAwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAxIC8vIDB4MDAwMDAwMDAwMDAwMDAwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyA5IC8vIDB4MDAwMDAwMDA3NzM1OTQwMAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMTAgLy8gMHgwMDAwMDAwMDNiOWFjYTAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglkdXAKCXNldGJpdAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDEgLy8gMHgwMDAwMDAwMDAwMDAwMDAwCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglkdXAKCXNldGJpdAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJcG9wIC8vIHBvcCBoZWFkIG9mZnNldAoJY29uY2F0IC8vIGNvbmNhdCBoZWFkIGFuZCB0YWlsCglmcmFtZV9idXJ5IDAgLy8gcmV0Tm9JZGVudGl0eTogVXNlckluZm9WMQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzQ5CgkvLyByZXR1cm4gcmV0Tm9JZGVudGl0eTsKCWZyYW1lX2RpZyAwIC8vIHJldE5vSWRlbnRpdHk6IFVzZXJJbmZvVjEKCWIgKmdldFVzZXIqcmV0dXJuCgoqaWYxX2VuZDoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6MzUxCgkvLyBpZGVudGl0eSA9IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYnl0ZWMgMyAvLyAgImkiCglmcmFtZV9kaWcgLTEgLy8gdXNlcjogQWRkcmVzcwoJY29uY2F0CglmcmFtZV9idXJ5IDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozNTMKCS8vIHJldDogVXNlckluZm9WMSA9IHsKCS8vICAgICAgIHZlcnNpb246IHYsCgkvLyAgICAgICBiYXNlOiBpZGVudGl0eS5mZWVNdWx0aXBsaWVyQmFzZSwKCS8vICAgICAgIGZlZU11bHRpcGxpZXI6IGlkZW50aXR5LmZlZU11bHRpcGxpZXIsCgkvLyAgICAgICBpc0xvY2tlZDogaWRlbnRpdHkuaXNMb2NrZWQsCgkvLyAgICAgICB2ZXJpZmljYXRpb25DbGFzczogaWRlbnRpdHkudmVyaWZpY2F0aW9uQ2xhc3MsCgkvLyAgICAgICB2ZXJpZmljYXRpb25TdGF0dXM6IGlkZW50aXR5LnZlcmlmaWNhdGlvblN0YXR1cywKCS8vICAgICAgIGJpYXRlY0VuZ2FnZW1lbnRQb2ludHM6IGlkZW50aXR5LmJpYXRlY0VuZ2FnZW1lbnRQb2ludHMsCgkvLyAgICAgICBiaWF0ZWNFbmdhZ2VtZW50UmFuazogaWRlbnRpdHkuYmlhdGVjRW5nYWdlbWVudFJhbmssCgkvLyAgICAgICBhdm1FbmdhZ2VtZW50UG9pbnRzOiBpZGVudGl0eS5hdm1FbmdhZ2VtZW50UG9pbnRzLAoJLy8gICAgICAgYXZtRW5nYWdlbWVudFJhbms6IGlkZW50aXR5LmF2bUVuZ2FnZW1lbnRSYW5rLAoJLy8gICAgICAgdHJhZGluZ0VuZ2FnZW1lbnRQb2ludHM6IGlkZW50aXR5LnRyYWRpbmdFbmdhZ2VtZW50UG9pbnRzLAoJLy8gICAgICAgdHJhZGluZ0VuZ2FnZW1lbnRSYW5rOiBpZGVudGl0eS50cmFkaW5nRW5nYWdlbWVudFJhbmssCgkvLyAgICAgICBreWNFeHBpcmF0aW9uOiBpZGVudGl0eS5reWNFeHBpcmF0aW9uLAoJLy8gICAgICAgaW52ZXN0b3JGb3JFeHBpcmF0aW9uOiBpZGVudGl0eS5pbnZlc3RvckZvckV4cGlyYXRpb24sCgkvLyAgICAgICBpc1Byb2Zlc3Npb25hbEludmVzdG9yOiBpZGVudGl0eS5pc1Byb2Zlc3Npb25hbEludmVzdG9yLAoJLy8gICAgICAgaXNDb21wYW55OiBpZGVudGl0eS5pc0NvbXBhbnksCgkvLyAgICAgICBwZXJzb25VVUlEOiBpZGVudGl0eS5wZXJzb25VVUlELAoJLy8gICAgICAgbGVnYWxFbnRpdHlVVUlEOiBpZGVudGl0eS5sZWdhbEVudGl0eVVVSUQsCgkvLyAgICAgfQoJYnl0ZWMgMCAvLyAgaW5pdGlhbCBoZWFkCglieXRlYyAwIC8vICBpbml0aWFsIHRhaWwKCWJ5dGVjIDEzIC8vICBpbml0aWFsIGhlYWQgb2Zmc2V0CglmcmFtZV9kaWcgLTIgLy8gdjogdWludDgKCWl0b2IKCWV4dHJhY3QgNyAxCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA0MSA4CglidG9pCglpdG9iCgljYWxsc3ViICpwcm9jZXNzX3N0YXRpY190dXBsZV9lbGVtZW50CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCAwIDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDIgLy8gMHgwMAoJaW50YyAwIC8vIDAKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglwdXNoaW50IDM5MgoJZ2V0Yml0CglzZXRiaXQKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDggLy8gNTAKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXVuY292ZXIgMgoJZXh0cmFjdF91aW50MTYKCWR1cCAvLyBkdXBsaWNhdGUgc3RhcnQgb2YgZWxlbWVudAoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJc3dhcAoJZXh0cmFjdF91aW50MTYgLy8gZ2V0IG51bWJlciBvZiBlbGVtZW50cwoJaW50YyAxIC8vICBnZXQgdHlwZSBsZW5ndGgKCSogLy8gbXVsdGlwbHkgYnkgdHlwZSBsZW5ndGgKCWludGMgMyAvLyAyCgkrIC8vIGFkZCB0d28gZm9yIGxlbmd0aAoJZXh0cmFjdDMKCWV4dHJhY3QgMiAwCglkdXAKCWxlbgoJaXRvYgoJZXh0cmFjdCA2IDIKCXN3YXAKCWNvbmNhdAoJY2FsbHN1YiAqcHJvY2Vzc19keW5hbWljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDkgLy8gNTIKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCXVuY292ZXIgMgoJZXh0cmFjdF91aW50MTYKCWR1cCAvLyBkdXBsaWNhdGUgc3RhcnQgb2YgZWxlbWVudAoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJc3dhcAoJZXh0cmFjdF91aW50MTYgLy8gZ2V0IG51bWJlciBvZiBlbGVtZW50cwoJaW50YyAxIC8vICBnZXQgdHlwZSBsZW5ndGgKCSogLy8gbXVsdGlwbHkgYnkgdHlwZSBsZW5ndGgKCWludGMgMyAvLyAyCgkrIC8vIGFkZCB0d28gZm9yIGxlbmd0aAoJZXh0cmFjdDMKCWV4dHJhY3QgMiAwCglkdXAKCWxlbgoJaXRvYgoJZXh0cmFjdCA2IDIKCXN3YXAKCWNvbmNhdAoJY2FsbHN1YiAqcHJvY2Vzc19keW5hbWljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDU0IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDYyIDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDcwIDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDc4IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDg2IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDk0IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDkgOAoJYnRvaQoJaXRvYgoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMTcgOAoJYnRvaQoJaXRvYgoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJYnl0ZWMgMiAvLyAweDAwCglpbnRjIDAgLy8gMAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWludGMgNCAvLyA2NAoJZ2V0Yml0CglzZXRiaXQKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDI1IDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDMzIDgKCWJ0b2kKCWl0b2IKCWNhbGxzdWIgKnByb2Nlc3Nfc3RhdGljX3R1cGxlX2VsZW1lbnQKCWJ5dGVjIDIgLy8gMHgwMAoJaW50YyAwIC8vIDAKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglpbnRjIDYgLy8gODE2CglnZXRiaXQKCXNldGJpdAoJY2FsbHN1YiAqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudAoJcG9wIC8vIHBvcCBoZWFkIG9mZnNldAoJY29uY2F0IC8vIGNvbmNhdCBoZWFkIGFuZCB0YWlsCglmcmFtZV9idXJ5IDIgLy8gcmV0OiBVc2VySW5mb1YxCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozNzMKCS8vIHJldHVybiByZXQ7CglmcmFtZV9kaWcgMiAvLyByZXQ6IFVzZXJJbmZvVjEKCipnZXRVc2VyKnJldHVybjoKCS8vIHNldCB0aGUgc3Vicm91dGluZSByZXR1cm4gdmFsdWUKCWZyYW1lX2J1cnkgMAoKCS8vIHBvcCBhbGwgbG9jYWwgdmFyaWFibGVzIGZyb20gdGhlIHN0YWNrCglwb3BuIDIKCXJldHN1YgoKLy8gZ2V0VXNlclNob3J0KGFkZHJlc3MsdWludDgpKHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpCiphYmlfcm91dGVfZ2V0VXNlclNob3J0OgoJLy8gVGhlIEFCSSByZXR1cm4gcHJlZml4CglieXRlYyA2IC8vIDB4MTUxZjdjNzUKCgkvLyB2OiB1aW50OAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMgoJZHVwCglsZW4KCWludGMgMSAvLyAxCgk9PQoKCS8vIGFyZ3VtZW50IDAgKHYpIGZvciBnZXRVc2VyU2hvcnQgbXVzdCBiZSBhIHVpbnQ4Cglhc3NlcnQKCWJ0b2kKCgkvLyB1c2VyOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglkdXAKCWxlbgoJaW50YyAyIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDEgKHVzZXIpIGZvciBnZXRVc2VyU2hvcnQgbXVzdCBiZSBhIGFkZHJlc3MKCWFzc2VydAoKCS8vIGV4ZWN1dGUgZ2V0VXNlclNob3J0KGFkZHJlc3MsdWludDgpKHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpCgljYWxsc3ViIGdldFVzZXJTaG9ydAoJY29uY2F0Cglsb2cKCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIGdldFVzZXJTaG9ydCh1c2VyOiBBZGRyZXNzLCB2OiB1aW50OCk6IFVzZXJJbmZvU2hvcnRWMQovLwovLyBSZXR1cm5zIHNob3J0IHVzZXIgaW5mb3JtYXRpb24gLSBmZWUgbXVsdGlwbGllciwgdmVyaWZpY2F0aW9uIGNsYXNzLCBlbmdhZ2VtZW50IGNsYXNzIC4uCi8vCi8vIEBwYXJhbSB1c2VyIEdldCBpbmZvIGZvciBzcGVjaWZpYyB1c2VyIGFkZHJlc3MKLy8gQHBhcmFtIHYgVmVyc2lvbiBvZiB0aGUgZGF0YSBzdHJ1Y3R1cmUgdG8gcmV0dXJuCmdldFVzZXJTaG9ydDoKCXByb3RvIDIgMQoKCS8vIFB1c2ggZW1wdHkgYnl0ZXMgYWZ0ZXIgdGhlIGZyYW1lIHBvaW50ZXIgdG8gcmVzZXJ2ZSBzcGFjZSBmb3IgbG9jYWwgdmFyaWFibGVzCglieXRlYyAwIC8vIDB4CglkdXBuIDIKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjM4NAoJLy8gYXNzZXJ0KHYgPT09IDEsICJDdXJyZW50bHkgc3VwcG9ydGVkIHZlcnNpb24gb2YgdGhlIGRhdGEgc3RydWN0dXJlIGlzICcxJyIpCglmcmFtZV9kaWcgLTIgLy8gdjogdWludDgKCWludGMgMSAvLyAxCgk9PQoKCS8vIEN1cnJlbnRseSBzdXBwb3J0ZWQgdmVyc2lvbiBvZiB0aGUgZGF0YSBzdHJ1Y3R1cmUgaXMgJzEnCglhc3NlcnQKCgkvLyAqaWYyX2NvbmRpdGlvbgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozODUKCS8vICF0aGlzLmlkZW50aXRpZXModXNlcikuZXhpc3RzCglieXRlYyAzIC8vICAiaSIKCWZyYW1lX2RpZyAtMSAvLyB1c2VyOiBBZGRyZXNzCgljb25jYXQKCWJveF9sZW4KCXN3YXAKCXBvcAoJIQoJYnogKmlmMl9lbmQKCgkvLyAqaWYyX2NvbnNlcXVlbnQKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6Mzg2CgkvLyByZXROb0lkZW50aXR5OiBVc2VySW5mb1Nob3J0VjEgPSB7CgkvLyAgICAgICAgIHZlcnNpb246IHYsCgkvLyAgICAgICAgIGJhc2U6IFNDQUxFIGFzIHVpbnQ2NCwKCS8vICAgICAgICAgZmVlTXVsdGlwbGllcjogKDIgKiBTQ0FMRSkgYXMgdWludDY0LAoJLy8gICAgICAgICBpc0xvY2tlZDogZmFsc2UsCgkvLyAgICAgICAgIHZlcmlmaWNhdGlvbkNsYXNzOiAwLAoJLy8gICAgICAgfQoJZnJhbWVfZGlnIC0yIC8vIHY6IHVpbnQ4CglpdG9iCglleHRyYWN0IDcgMQoJYnl0ZWMgMSAvLyAweDAwMDAwMDAwMDAwMDAwMDAKCWNvbmNhdAoJYnl0ZWMgOSAvLyAweDAwMDAwMDAwNzczNTk0MDAKCWNvbmNhdAoJYnl0ZWMgMTAgLy8gMHgwMDAwMDAwMDNiOWFjYTAwCgljb25jYXQKCWJ5dGVjIDIgLy8gMHgwMAoJaW50YyAwIC8vIDAKCWR1cAoJc2V0Yml0Cgljb25jYXQKCWZyYW1lX2J1cnkgMCAvLyByZXROb0lkZW50aXR5OiBVc2VySW5mb1Nob3J0VjEKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjM5MwoJLy8gcmV0dXJuIHJldE5vSWRlbnRpdHk7CglmcmFtZV9kaWcgMCAvLyByZXROb0lkZW50aXR5OiBVc2VySW5mb1Nob3J0VjEKCWIgKmdldFVzZXJTaG9ydCpyZXR1cm4KCippZjJfZW5kOgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czozOTUKCS8vIGlkZW50aXR5ID0gdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglieXRlYyAzIC8vICAiaSIKCWZyYW1lX2RpZyAtMSAvLyB1c2VyOiBBZGRyZXNzCgljb25jYXQKCWZyYW1lX2J1cnkgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjM5NwoJLy8gcmV0OiBVc2VySW5mb1Nob3J0VjEgPSB7CgkvLyAgICAgICB2ZXJzaW9uOiB2LAoJLy8gICAgICAgYmFzZTogaWRlbnRpdHkuZmVlTXVsdGlwbGllckJhc2UsCgkvLyAgICAgICBmZWVNdWx0aXBsaWVyOiBpZGVudGl0eS5mZWVNdWx0aXBsaWVyLAoJLy8gICAgICAgaXNMb2NrZWQ6IGlkZW50aXR5LmlzTG9ja2VkLAoJLy8gICAgICAgdmVyaWZpY2F0aW9uQ2xhc3M6IGlkZW50aXR5LnZlcmlmaWNhdGlvbkNsYXNzLAoJLy8gICAgIH0KCWZyYW1lX2RpZyAtMiAvLyB2OiB1aW50OAoJaXRvYgoJZXh0cmFjdCA3IDEKCWZyYW1lX2RpZyAxIC8vIHN0b3JhZ2Uga2V5Ly9pZGVudGl0eQoJYm94X2dldAoKCS8vIGJveCB2YWx1ZSBkb2VzIG5vdCBleGlzdDogdGhpcy5pZGVudGl0aWVzKHVzZXIpLnZhbHVlCglhc3NlcnQKCXN0b3JlIDI1NSAvLyBmdWxsIGFycmF5Cglsb2FkIDI1NSAvLyBmdWxsIGFycmF5CglleHRyYWN0IDAgOAoJYnRvaQoJaXRvYgoJY29uY2F0CglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJZXh0cmFjdCA5IDgKCWJ0b2kKCWl0b2IKCWNvbmNhdAoJZnJhbWVfZGlnIDEgLy8gc3RvcmFnZSBrZXkvL2lkZW50aXR5Cglib3hfZ2V0CgoJLy8gYm94IHZhbHVlIGRvZXMgbm90IGV4aXN0OiB0aGlzLmlkZW50aXRpZXModXNlcikudmFsdWUKCWFzc2VydAoJc3RvcmUgMjU1IC8vIGZ1bGwgYXJyYXkKCWxvYWQgMjU1IC8vIGZ1bGwgYXJyYXkKCWV4dHJhY3QgMTcgOAoJYnRvaQoJaXRvYgoJY29uY2F0CglieXRlYyAyIC8vIDB4MDAKCWludGMgMCAvLyAwCglmcmFtZV9kaWcgMSAvLyBzdG9yYWdlIGtleS8vaWRlbnRpdHkKCWJveF9nZXQKCgkvLyBib3ggdmFsdWUgZG9lcyBub3QgZXhpc3Q6IHRoaXMuaWRlbnRpdGllcyh1c2VyKS52YWx1ZQoJYXNzZXJ0CglzdG9yZSAyNTUgLy8gZnVsbCBhcnJheQoJbG9hZCAyNTUgLy8gZnVsbCBhcnJheQoJaW50YyA0IC8vIDY0CglnZXRiaXQKCXNldGJpdAoJY29uY2F0CglmcmFtZV9idXJ5IDIgLy8gcmV0OiBVc2VySW5mb1Nob3J0VjEKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQwNAoJLy8gcmV0dXJuIHJldDsKCWZyYW1lX2RpZyAyIC8vIHJldDogVXNlckluZm9TaG9ydFYxCgoqZ2V0VXNlclNob3J0KnJldHVybjoKCS8vIHNldCB0aGUgc3Vicm91dGluZSByZXR1cm4gdmFsdWUKCWZyYW1lX2J1cnkgMAoKCS8vIHBvcCBhbGwgbG9jYWwgdmFyaWFibGVzIGZyb20gdGhlIHN0YWNrCglwb3BuIDIKCXJldHN1YgoKLy8gd2l0aGRyYXdFeGNlc3NBc3NldHModWludDY0LHVpbnQ2NCx1aW50NjQpdWludDY0CiphYmlfcm91dGVfd2l0aGRyYXdFeGNlc3NBc3NldHM6CgkvLyBUaGUgQUJJIHJldHVybiBwcmVmaXgKCWJ5dGVjIDYgLy8gMHgxNTFmN2M3NQoKCS8vIGFtb3VudDogdWludDY0Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyAzCglidG9pCgoJLy8gYXNzZXQ6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMgoJYnRvaQoKCS8vIGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWJ0b2kKCgkvLyBleGVjdXRlIHdpdGhkcmF3RXhjZXNzQXNzZXRzKHVpbnQ2NCx1aW50NjQsdWludDY0KXVpbnQ2NAoJY2FsbHN1YiB3aXRoZHJhd0V4Y2Vzc0Fzc2V0cwoJaXRvYgoJY29uY2F0Cglsb2cKCWludGMgMSAvLyAxCglyZXR1cm4KCi8vIHdpdGhkcmF3RXhjZXNzQXNzZXRzKGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyOiBBcHBJRCwgYXNzZXQ6IEFzc2V0SUQsIGFtb3VudDogdWludDY0KTogdWludDY0Ci8vCi8vIElmIHNvbWVvbmUgZGVwb3NpdHMgZXhjZXNzIGFzc2V0cyB0byB0aGlzIHNtYXJ0IGNvbnRyYWN0IGJpYXRlYyBjYW4gdXNlIHRoZW0uCi8vCi8vIE9ubHkgYWRkcmVzc0V4ZWN1dGl2ZUZlZSBpcyBhbGxvd2VkIHRvIGV4ZWN1dGUgdGhpcyBtZXRob2QuCi8vCi8vIEBwYXJhbSBhcHBCaWF0ZWNDb25maWdQcm92aWRlciBCaWF0ZWMgY29uZmlnIGFwcC4gT25seSBhZGRyZXNzRXhlY3V0aXZlRmVlIGlzIGFsbG93ZWQgdG8gZXhlY3V0ZSB0aGlzIG1ldGhvZC4KLy8gQHBhcmFtIGFzc2V0IEFzc2V0IHRvIHdpdGhkcmF3LiBJZiBuYXRpdmUgdG9rZW4sIHRoZW4gemVybwovLyBAcGFyYW0gYW1vdW50IEFtb3VudCBvZiB0aGUgYXNzZXQgdG8gYmUgd2l0aGRyYXduCndpdGhkcmF3RXhjZXNzQXNzZXRzOgoJcHJvdG8gMyAxCgoJLy8gUHVzaCBlbXB0eSBieXRlcyBhZnRlciB0aGUgZnJhbWUgcG9pbnRlciB0byByZXNlcnZlIHNwYWNlIGZvciBsb2NhbCB2YXJpYWJsZXMKCWJ5dGVjIDAgLy8gMHgKCWR1cAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDE3CgkvLyBhc3NlcnQoYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIgPT09IHRoaXMuYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIudmFsdWUsICdDb25maWd1cmF0aW9uIGFwcCBkb2VzIG5vdCBtYXRjaCcpCglmcmFtZV9kaWcgLTEgLy8gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXI6IEFwcElECglieXRlYyA0IC8vICAiQiIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoKCS8vIENvbmZpZ3VyYXRpb24gYXBwIGRvZXMgbm90IG1hdGNoCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQxOAoJLy8gYWRkcmVzc0V4ZWN1dGl2ZUZlZSA9IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdlZicpIGFzIEFkZHJlc3MKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDEyIC8vICAiZWYiCglhcHBfZ2xvYmFsX2dldF9leAoKCS8vIGdsb2JhbCBzdGF0ZSB2YWx1ZSBkb2VzIG5vdCBleGlzdDogYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ2VmJykKCWFzc2VydAoJZnJhbWVfYnVyeSAwIC8vIGFkZHJlc3NFeGVjdXRpdmVGZWU6IGFkZHJlc3MKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQxOQoJLy8gcGF1c2VkID0gYXBwQmlhdGVjQ29uZmlnUHJvdmlkZXIuZ2xvYmFsU3RhdGUoJ3MnKSBhcyB1aW50NjQKCWZyYW1lX2RpZyAtMSAvLyBhcHBCaWF0ZWNDb25maWdQcm92aWRlcjogQXBwSUQKCWJ5dGVjIDUgLy8gICJzIgoJYXBwX2dsb2JhbF9nZXRfZXgKCgkvLyBnbG9iYWwgc3RhdGUgdmFsdWUgZG9lcyBub3QgZXhpc3Q6IGFwcEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmdsb2JhbFN0YXRlKCdzJykKCWFzc2VydAoJZnJhbWVfYnVyeSAxIC8vIHBhdXNlZDogdWludDY0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MjAKCS8vIGFzc2VydChwYXVzZWQgPT09IDAsICdFUlJfUEFVU0VEJykKCWZyYW1lX2RpZyAxIC8vIHBhdXNlZDogdWludDY0CglpbnRjIDAgLy8gMAoJPT0KCgkvLyBFUlJfUEFVU0VECglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjSWRlbnRpdHlQcm92aWRlci5hbGdvLnRzOjQyMQoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gYWRkcmVzc0V4ZWN1dGl2ZUZlZSwgJ09ubHkgZmVlIGV4ZWN1dG9yIHNldHVwIGluIHRoZSBjb25maWcgY2FuIHRha2UgdGhlIGNvbGxlY3RlZCBmZWVzJykKCXR4biBTZW5kZXIKCWZyYW1lX2RpZyAwIC8vIGFkZHJlc3NFeGVjdXRpdmVGZWU6IGFkZHJlc3MKCT09CgoJLy8gT25seSBmZWUgZXhlY3V0b3Igc2V0dXAgaW4gdGhlIGNvbmZpZyBjYW4gdGFrZSB0aGUgY29sbGVjdGVkIGZlZXMKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDIzCgkvLyB0aGlzLmRvQXhmZXIodGhpcy50eG4uc2VuZGVyLCBhc3NldCwgYW1vdW50KQoJZnJhbWVfZGlnIC0zIC8vIGFtb3VudDogdWludDY0CglmcmFtZV9kaWcgLTIgLy8gYXNzZXQ6IEFzc2V0SUQKCXR4biBTZW5kZXIKCWNhbGxzdWIgZG9BeGZlcgoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDI1CgkvLyByZXR1cm4gYW1vdW50OwoJZnJhbWVfZGlnIC0zIC8vIGFtb3VudDogdWludDY0CgoJLy8gc2V0IHRoZSBzdWJyb3V0aW5lIHJldHVybiB2YWx1ZQoJZnJhbWVfYnVyeSAwCgoJLy8gcG9wIGFsbCBsb2NhbCB2YXJpYWJsZXMgZnJvbSB0aGUgc3RhY2sKCXBvcG4gMQoJcmV0c3ViCgovLyBkb0F4ZmVyKHJlY2VpdmVyOiBBZGRyZXNzLCBhc3NldDogQXNzZXRJRCwgYW1vdW50OiB1aW50NjQpOiB2b2lkCi8vCi8vIEV4ZWN1dGVzIHhmZXIgb2YgcGF5IHBheW1lbnQgbWV0aG9kcyB0byBzcGVjaWZpZWQgcmVjZWl2ZXIgZnJvbSBzbWFydCBjb250cmFjdCBhZ2dyZWdhdGVkIGFjY291bnQgd2l0aCBzcGVjaWZpZWQgYXNzZXQgYW5kIGFtb3VudCBpbiB0b2tlbnMgZGVjaW1hbHMKLy8gQHBhcmFtIHJlY2VpdmVyIFJlY2VpdmVyCi8vIEBwYXJhbSBhc3NldCBBc3NldC4gWmVybyBmb3IgYWxnbwovLyBAcGFyYW0gYW1vdW50IEFtb3VudCB0byB0cmFuc2Zlcgpkb0F4ZmVyOgoJcHJvdG8gMyAwCgoJLy8gKmlmM19jb25kaXRpb24KCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDM1CgkvLyBhc3NldC5pZCA9PT0gMAoJZnJhbWVfZGlnIC0yIC8vIGFzc2V0OiBBc3NldElECglpbnRjIDAgLy8gMAoJPT0KCWJ6ICppZjNfZWxzZQoKCS8vICppZjNfY29uc2VxdWVudAoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MzYKCS8vIHNlbmRQYXltZW50KHsKCS8vICAgICAgICAgcmVjZWl2ZXI6IHJlY2VpdmVyLAoJLy8gICAgICAgICBhbW91bnQ6IGFtb3VudCwKCS8vICAgICAgICAgZmVlOiAwLAoJLy8gICAgICAgfSkKCWl0eG5fYmVnaW4KCWludGMgMSAvLyAgcGF5CglpdHhuX2ZpZWxkIFR5cGVFbnVtCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MzcKCS8vIHJlY2VpdmVyOiByZWNlaXZlcgoJZnJhbWVfZGlnIC0xIC8vIHJlY2VpdmVyOiBBZGRyZXNzCglpdHhuX2ZpZWxkIFJlY2VpdmVyCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MzgKCS8vIGFtb3VudDogYW1vdW50CglmcmFtZV9kaWcgLTMgLy8gYW1vdW50OiB1aW50NjQKCWl0eG5fZmllbGQgQW1vdW50CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0MzkKCS8vIGZlZTogMAoJaW50YyAwIC8vIDAKCWl0eG5fZmllbGQgRmVlCgoJLy8gU3VibWl0IGlubmVyIHRyYW5zYWN0aW9uCglpdHhuX3N1Ym1pdAoJYiAqaWYzX2VuZAoKKmlmM19lbHNlOgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0NDIKCS8vIHNlbmRBc3NldFRyYW5zZmVyKHsKCS8vICAgICAgICAgYXNzZXRSZWNlaXZlcjogcmVjZWl2ZXIsCgkvLyAgICAgICAgIHhmZXJBc3NldDogYXNzZXQsCgkvLyAgICAgICAgIGFzc2V0QW1vdW50OiBhbW91bnQsCgkvLyAgICAgICAgIGZlZTogMCwKCS8vICAgICAgIH0pCglpdHhuX2JlZ2luCglpbnRjIDcgLy8gIGF4ZmVyCglpdHhuX2ZpZWxkIFR5cGVFbnVtCgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0NDMKCS8vIGFzc2V0UmVjZWl2ZXI6IHJlY2VpdmVyCglmcmFtZV9kaWcgLTEgLy8gcmVjZWl2ZXI6IEFkZHJlc3MKCWl0eG5fZmllbGQgQXNzZXRSZWNlaXZlcgoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDQ0CgkvLyB4ZmVyQXNzZXQ6IGFzc2V0CglmcmFtZV9kaWcgLTIgLy8gYXNzZXQ6IEFzc2V0SUQKCWl0eG5fZmllbGQgWGZlckFzc2V0CgoJLy8gY29udHJhY3RzXEJpYXRlY0lkZW50aXR5UHJvdmlkZXIuYWxnby50czo0NDUKCS8vIGFzc2V0QW1vdW50OiBhbW91bnQKCWZyYW1lX2RpZyAtMyAvLyBhbW91bnQ6IHVpbnQ2NAoJaXR4bl9maWVsZCBBc3NldEFtb3VudAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLmFsZ28udHM6NDQ2CgkvLyBmZWU6IDAKCWludGMgMCAvLyAwCglpdHhuX2ZpZWxkIEZlZQoKCS8vIFN1Ym1pdCBpbm5lciB0cmFuc2FjdGlvbgoJaXR4bl9zdWJtaXQKCippZjNfZW5kOgoJcmV0c3ViCgoqY3JlYXRlX05vT3A6CglwdXNoYnl0ZXMgMHhiODQ0N2IzNiAvLyBtZXRob2QgImNyZWF0ZUFwcGxpY2F0aW9uKCl2b2lkIgoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMAoJbWF0Y2ggKmFiaV9yb3V0ZV9jcmVhdGVBcHBsaWNhdGlvbgoKCS8vIHRoaXMgY29udHJhY3QgZG9lcyBub3QgaW1wbGVtZW50IHRoZSBnaXZlbiBBQkkgbWV0aG9kIGZvciBjcmVhdGUgTm9PcAoJZXJyCgoqY2FsbF9Ob09wOgoJcHVzaGJ5dGVzIDB4ZTNiZjVjMWYgLy8gbWV0aG9kICJib290c3RyYXAodWludDY0LGFkZHJlc3MsYWRkcmVzcyxhZGRyZXNzKXZvaWQiCglwdXNoYnl0ZXMgMHhhZTY0YzE2NyAvLyBtZXRob2QgInNlbGZSZWdpc3RyYXRpb24oYWRkcmVzcywodWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sLHN0cmluZyxzdHJpbmcsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbCkpdm9pZCIKCXB1c2hieXRlcyAweGE0OGNmYmJjIC8vIG1ldGhvZCAic2V0SW5mbyhhZGRyZXNzLCh1aW50NjQsYm9vbCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wsc3RyaW5nLHN0cmluZyx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKSl2b2lkIgoJcHVzaGJ5dGVzIDB4Njc5MTQyNjQgLy8gbWV0aG9kICJzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uKHVpbnQ2NCxieXRlW10sYnl0ZVtdLGJ5dGVbXSx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQpdm9pZCIKCXB1c2hieXRlcyAweGU4YWQxODkyIC8vIG1ldGhvZCAiZ2V0VXNlcihhZGRyZXNzLHVpbnQ4KSh1aW50OCx1aW50NjQsdWludDY0LGJvb2wsc3RyaW5nLHN0cmluZyx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wsdWludDY0LHVpbnQ2NCxib29sKSIKCXB1c2hieXRlcyAweDEyN2ZmYjdiIC8vIG1ldGhvZCAiZ2V0VXNlclNob3J0KGFkZHJlc3MsdWludDgpKHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpIgoJcHVzaGJ5dGVzIDB4Y2JhMmU5NWQgLy8gbWV0aG9kICJ3aXRoZHJhd0V4Y2Vzc0Fzc2V0cyh1aW50NjQsdWludDY0LHVpbnQ2NCl1aW50NjQiCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAwCgltYXRjaCAqYWJpX3JvdXRlX2Jvb3RzdHJhcCAqYWJpX3JvdXRlX3NlbGZSZWdpc3RyYXRpb24gKmFiaV9yb3V0ZV9zZXRJbmZvICphYmlfcm91dGVfc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbiAqYWJpX3JvdXRlX2dldFVzZXIgKmFiaV9yb3V0ZV9nZXRVc2VyU2hvcnQgKmFiaV9yb3V0ZV93aXRoZHJhd0V4Y2Vzc0Fzc2V0cwoKCS8vIHRoaXMgY29udHJhY3QgZG9lcyBub3QgaW1wbGVtZW50IHRoZSBnaXZlbiBBQkkgbWV0aG9kIGZvciBjYWxsIE5vT3AKCWVycgoKKmNhbGxfVXBkYXRlQXBwbGljYXRpb246CglwdXNoYnl0ZXMgMHg1ZmM4ODVhMCAvLyBtZXRob2QgInVwZGF0ZUFwcGxpY2F0aW9uKHVpbnQ2NCxieXRlW10pdm9pZCIKCXR4bmEgQXBwbGljYXRpb25BcmdzIDAKCW1hdGNoICphYmlfcm91dGVfdXBkYXRlQXBwbGljYXRpb24KCgkvLyB0aGlzIGNvbnRyYWN0IGRvZXMgbm90IGltcGxlbWVudCB0aGUgZ2l2ZW4gQUJJIG1ldGhvZCBmb3IgY2FsbCBVcGRhdGVBcHBsaWNhdGlvbgoJZXJyCgoqcHJvY2Vzc19zdGF0aWNfdHVwbGVfZWxlbWVudDoKCXByb3RvIDQgMwoJZnJhbWVfZGlnIC00IC8vIHR1cGxlIGhlYWQKCWZyYW1lX2RpZyAtMSAvLyBlbGVtZW50Cgljb25jYXQKCWZyYW1lX2RpZyAtMyAvLyB0dXBsZSB0YWlsCglmcmFtZV9kaWcgLTIgLy8gaGVhZCBvZmZzZXQKCXJldHN1YgoKKnByb2Nlc3NfZHluYW1pY190dXBsZV9lbGVtZW50OgoJcHJvdG8gNCAzCglmcmFtZV9kaWcgLTQgLy8gdHVwbGUgaGVhZAoJZnJhbWVfZGlnIC0yIC8vIGhlYWQgb2Zmc2V0Cgljb25jYXQKCWZyYW1lX2J1cnkgLTQgLy8gdHVwbGUgaGVhZAoJZnJhbWVfZGlnIC0xIC8vIGVsZW1lbnQKCWR1cAoJbGVuCglmcmFtZV9kaWcgLTIgLy8gaGVhZCBvZmZzZXQKCWJ0b2kKCSsKCWl0b2IKCWV4dHJhY3QgNiAyCglmcmFtZV9idXJ5IC0yIC8vIGhlYWQgb2Zmc2V0CglmcmFtZV9kaWcgLTMgLy8gdHVwbGUgdGFpbAoJc3dhcAoJY29uY2F0CglmcmFtZV9idXJ5IC0zIC8vIHR1cGxlIHRhaWwKCWZyYW1lX2RpZyAtNCAvLyB0dXBsZSBoZWFkCglmcmFtZV9kaWcgLTMgLy8gdHVwbGUgdGFpbAoJZnJhbWVfZGlnIC0yIC8vIGhlYWQgb2Zmc2V0CglyZXRzdWI=";
		protected override string SourceClear { get; set; } = "I3ByYWdtYSB2ZXJzaW9uIDEw";
		protected override string SourceApprovalAVM { get; set; }= "CiAKAAEgAkCAlOvcA7AGBDI0JhEACAAAAAAAAAAAAQABaQFCAXMEFR98dSQwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAVQklBVEVDLUlERU5ULTAxLTAzLTAyCAAAAAB3NZQACAAAAAA7msoABXNjdmVyAmVmAgBoAgAAAXUBZTEYFIEGCzEZCI0MBbYAAAAAAAAF9AAABagAAAAAAAAAAAAAAIgAAiNDigAAJwsnCGeJNhoESRUkEkQ2GgNJFSQSRDYaAkkVJBJENhoBF4gAAiNDigQAKEcCNjIAJwRlTEhBABYnBGSMAIsAJw9lRIwBMQCLARJEQgAKMQA2MgByB0gSRCcEi/9ngAFni/5ngAF2i/1nJxCL/GeL/ycFZUSMAosCIhJEiTYaAlcCADYaAReIAAIjQ4oCAChJi/8nBGQSRIv/Jw9lRIwAMQCLABJEi/8nBWVEjAGLASISRCcIsIv+sCcLi/5niTYaAjYaAUkVJBJEiAACI0OKAgAri/9QvUxIFESL/jX/NP9XKQgXIxJEi/41/zT/VwAIFyISRIv+Nf8hCDT/NP9PAllJNP9MWSMLJQhYVwIAJwcSRIv+Nf8hCTT/NP9PAllJNP9MWSMLJQhYVwIAJwcSRIv+Nf80/1c2CBciEkSL/jX/NP9XPggXIhJEi/41/zT/V0YIFyISRIv+Nf80/1dOCBciEkSL/jX/NP9XVggXIhJEi/41/zT/V14IFyISRIv+Nf80/yEEUyISRIv+Nf80/1cZCBciEkSL/jX/NP9XIQgXIhJEi/41/zT/IQZTIhJEi/41/zT/VxEIFyEFEkSL/jX/NP9XCQgXgYCo1rkHEkQri/9QSbxIi/6/iTYaAjYaAUkVJBJEiAACI0OKAgAxACcQZBJEi/41/zT/VxEIFyEFEkSL/jX/NP9XAAgXIQcORCuL/1BJvEiL/r+JNhoIFzYaBxc2GgYXNhoFFzYaBFcCADYaA1cCADYaAlcCADYaAReIAAIjQ4oIAChJi/8nBGQSRIv/JwxlRIwAMQCLABJEi/8nBWVEjAGLASISRLElshCL/bILi/yyP4v7sgyL+bIOi/qyDYv+sgqL+LIBs4knBjYaAkkVIxJEFzYaAUkVJBJEiAAEULAjQ4oCAShHAov+IxJEK4v/UL1MSBRBAGcoKCcNi/4WVwcBiAMsKYgDKCmIAyQqIklUiAMdJw6IAyUnDogDICmIAw8piAMLKYgDBymIAwMpiAL/KYgC+ycJiAL2JwqIAvEqIklUiALqKYgC5imIAuIqIklUiALbSFCMAIsAQgFbK4v/UIwBKCgnDYv+FlcHAYgCv4sBvkQ1/zT/VykIFxaIAq+LAb5ENf80/1cACBcWiAKfKiKLAb5ENf80/4GIA1NUiAKNiwG+RDX/IQg0/zT/TwJZSTT/TFkjCyUIWFcCAEkVFlcGAkxQiAJziwG+RDX/IQk0/zT/TwJZSTT/TFkjCyUIWFcCAEkVFlcGAkxQiAJMiwG+RDX/NP9XNggXFogCL4sBvkQ1/zT/Vz4IFxaIAh+LAb5ENf80/1dGCBcWiAIPiwG+RDX/NP9XTggXFogB/4sBvkQ1/zT/V1YIFxaIAe+LAb5ENf80/1deCBcWiAHfiwG+RDX/NP9XCQgXFogBz4sBvkQ1/zT/VxEIFxaIAb8qIosBvkQ1/zT/IQRTVIgBrosBvkQ1/zT/VxkIFxaIAZ6LAb5ENf80/1chCBcWiAGOKiKLAb5ENf80/yEGU1SIAX1IUIwCiwKMAEYCiScGNhoCSRUjEkQXNhoBSRUkEkSIAARQsCNDigIBKEcCi/4jEkQri/9QvUxIFEEAGov+FlcHASlQJwlQJwpQKiJJVFCMAIsAQgBJK4v/UIwBi/4WVwcBiwG+RDX/NP9XAAgXFlCLAb5ENf80/1cJCBcWUIsBvkQ1/zT/VxEIFxZQKiKLAb5ENf80/yEEU1RQjAKLAowARgKJJwY2GgMXNhoCFzYaAReIAAUWULAjQ4oDAShJi/8nBGQSRIv/JwxlRIwAi/8nBWVEjAGLASISRDEAiwASRIv9i/4xAIgAB4v9jABGAYmKAwCL/iISQQATsSOyEIv/sgeL/bIIIrIBs0IAFbEhB7IQi/+yFIv+shGL/bISIrIBs4mABLhEezY2GgCOAfpMAIAE479cH4AErmTBZ4AEpIz7vIAEZ5FCZIAE6K0YkoAEEn/7e4AEy6LpXTYaAI4H+hz61Pvx/DL8pv6d/zUAgARfyIWgNhoAjgH6hACKBAOL/Iv/UIv9i/6JigQDi/yL/lCM/Iv/SRWL/hcIFlcGAoz+i/1MUIz9i/yL/Yv+iQ==";
		protected override string SourceClearAVM { get; set; } = "Cg==";
		protected override ulong? GlobalNumByteSlices { get; set; }=4;
		protected override ulong? GlobalNumUints { get; set; }=1;
		protected override ulong? LocalNumByteSlices { get; set; }=0;
		protected override ulong? LocalNumUints { get; set; }=0;
		protected override ulong? ExtraProgramPages { get; set; }=0;

	}

}
