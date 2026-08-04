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

namespace BiatecConfig
{

	
	public class BiatecConfigProviderProxy : ProxyBase
	{
		
		public BiatecConfigProviderProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
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
        ///addressUdpater from global biatec configuration is allowed to update application
        ///No_op: NEVER, Opt_in: NEVER, Close_out: NEVER, Update_application: CALL, Delete_application: NEVER
        ///</summary>
		/// <param name="newVersion"> ABI Type is byte[]  </param>
		public async Task updateApplication (byte[] newVersion, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {105,54,198,47};
			var result = await base.CallApp(new List<object> {abiHandle,newVersion}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> updateApplication_Transactions (byte[] newVersion, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {105,54,198,47};
			return await base.MakeTransactionList(new List<object> {abiHandle,newVersion}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Setup the contract
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="biatecFee">Biatec fees ABI Type is uint256  </param>
		/// <param name="appBiatecIdentityProvider"> ABI Type is uint64  </param>
		/// <param name="appBiatecPoolProvider"> ABI Type is uint64  </param>
		public async Task bootstrap (AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee,ulong appBiatecIdentityProvider,ulong appBiatecPoolProvider, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {73,92,231,237};
			var result = await base.CallApp(new List<object> {abiHandle,biatecFee,appBiatecIdentityProvider,appBiatecPoolProvider}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> bootstrap_Transactions (AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee,ulong appBiatecIdentityProvider,ulong appBiatecPoolProvider, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {73,92,231,237};
			return await base.MakeTransactionList(new List<object> {abiHandle,biatecFee,appBiatecIdentityProvider,appBiatecPoolProvider}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Top secret account with which it is possible update contracts or identity provider
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is address  </param>
		public async Task setAddressUdpater (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {191,194,8,96};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setAddressUdpater_Transactions (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {191,194,8,96};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Kill switch. In the extreme case all services (deposit, trading, withdrawal, identity modifications and more) can be suspended.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is uint64  </param>
		public async Task setPaused (ulong a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {12,220,16,252};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setPaused_Transactions (ulong a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {12,220,16,252};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Execution address with which it is possible to opt in for governance
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is address  </param>
		public async Task setAddressGov (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {107,149,95,75};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setAddressGov_Transactions (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {107,149,95,75};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Execution address with which it is possible to change global biatec fees
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is address  </param>
		public async Task setAddressExecutive (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {139,24,123,61};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setAddressExecutive_Transactions (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {139,24,123,61};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Execution fee address is address which can take fees from pools.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is address  </param>
		public async Task setAddressExecutiveFee (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {80,224,125,136};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setAddressExecutiveFee_Transactions (Algorand.Address a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {80,224,125,136};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///App identity setter
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is uint64  </param>
		public async Task setBiatecIdentity (ulong a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {186,190,30,17};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setBiatecIdentity_Transactions (ulong a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {186,190,30,17};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///App identity setter
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a">Address ABI Type is uint64  </param>
		public async Task setBiatecPool (ulong a, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {197,139,157,164};
			var result = await base.CallApp(new List<object> {abiHandle,a}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setBiatecPool_Transactions (ulong a, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {197,139,157,164};
			return await base.MakeTransactionList(new List<object> {abiHandle,a}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///Fees in 9 decimals. 1_000_000_000 = 100%
///Fees in 9 decimals. 10_000_000 = 1%
///Fees in 9 decimals. 100_000 = 0,01%
///
///
///Fees are respectful from the all fees taken to the LP providers. If LPs charge 1% fee, and biatec charges 10% fee, LP will receive 0.09% fee and biatec 0.01% fee
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="biatecFee">Fee ABI Type is uint256  </param>
		public async Task setBiatecFee (AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {202,52,74,52};
			var result = await base.CallApp(new List<object> {abiHandle,biatecFee}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> setBiatecFee_Transactions (AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {202,52,74,52};
			return await base.MakeTransactionList(new List<object> {abiHandle,biatecFee}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///addressExecutiveFee can perfom key registration for this LP pool
///
///
///Only addressExecutiveFee is allowed to execute this method.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="votePK"> ABI Type is byte[]  </param>
		/// <param name="selectionPK"> ABI Type is byte[]  </param>
		/// <param name="stateProofPK"> ABI Type is byte[]  </param>
		/// <param name="voteFirst"> ABI Type is uint64  </param>
		/// <param name="voteLast"> ABI Type is uint64  </param>
		/// <param name="voteKeyDilution"> ABI Type is uint64  </param>
		/// <param name="fee"> ABI Type is uint64  </param>
		public async Task sendOnlineKeyRegistration (byte[] votePK,byte[] selectionPK,byte[] stateProofPK,ulong voteFirst,ulong voteLast,ulong voteKeyDilution,ulong fee, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {74,230,233,203};
			var result = await base.CallApp(new List<object> {abiHandle,votePK,selectionPK,stateProofPK,voteFirst,voteLast,voteKeyDilution,fee}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		public async Task<List<Transaction>> sendOnlineKeyRegistration_Transactions (byte[] votePK,byte[] selectionPK,byte[] stateProofPK,ulong voteFirst,ulong voteLast,ulong voteKeyDilution,ulong fee, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {74,230,233,203};
			return await base.MakeTransactionList(new List<object> {abiHandle,votePK,selectionPK,stateProofPK,voteFirst,voteLast,voteKeyDilution,fee}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

		}

		///<summary>
        ///If someone deposits excess assets to this smart contract biatec can use them.
///
///
///Only addressExecutiveFee is allowed to execute this method.
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="asset">Asset to withdraw. If native token, then zero ABI Type is uint64  </param>
		/// <param name="amount">Amount of the asset to be withdrawn ABI Type is uint64  </param>
		public async Task<ulong> withdrawExcessAssets (ulong asset,ulong amount, Account _tx_sender, ulong? _tx_fee = null,string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {135,40,55,48};
			var result = await base.CallApp(new List<object> {abiHandle,asset,amount}, _tx_fee: _tx_fee,  _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
			var lastLogBytes = result.Last();
			if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
			var lastLogReturnData = lastLogBytes.Skip(4).ToArray();
			return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);

		}

		public async Task<List<Transaction>> withdrawExcessAssets_Transactions (ulong asset,ulong amount, Account _tx_sender, ulong? _tx_fee = null, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp )
		{
			_tx_boxes ??= new List<BoxRef>();
			_tx_transactions ??= new List<Transaction>();
			_tx_assets ??= new List<ulong>();
			_tx_apps ??= new List<ulong>();
			_tx_accounts ??= new List<Address>();
			byte[] abiHandle = {135,40,55,48};
			return await base.MakeTransactionList(new List<object> {abiHandle,asset,amount}, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions , _tx_apps: _tx_apps, _tx_assets:_tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

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


		//addressUdpater from global biatec configuration is allowed to update application
public class updateApplication_Arc4GroupTransaction: ProxyBase
{
	public updateApplication_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private updateApplication_Arc4GroupTransaction() : base(null,0)  {} 
//
	public AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte> newVersion {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {105,54,198,47};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {newVersion}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Setup the contract
public class bootstrap_Arc4GroupTransaction: ProxyBase
{
	public bootstrap_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private bootstrap_Arc4GroupTransaction() : base(null,0)  {} 
//Biatec fees
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt256)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 appBiatecIdentityProvider {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 appBiatecPoolProvider {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {73,92,231,237};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {biatecFee,appBiatecIdentityProvider,appBiatecPoolProvider}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Top secret account with which it is possible update contracts or identity provider
public class setAddressUdpater_Arc4GroupTransaction: ProxyBase
{
	public setAddressUdpater_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setAddressUdpater_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.Address a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {191,194,8,96};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Kill switch. In the extreme case all services (deposit, trading, withdrawal, identity modifications and more) can be suspended.
public class setPaused_Arc4GroupTransaction: ProxyBase
{
	public setPaused_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setPaused_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {12,220,16,252};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Execution address with which it is possible to opt in for governance
public class setAddressGov_Arc4GroupTransaction: ProxyBase
{
	public setAddressGov_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setAddressGov_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.Address a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {107,149,95,75};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Execution address with which it is possible to change global biatec fees
public class setAddressExecutive_Arc4GroupTransaction: ProxyBase
{
	public setAddressExecutive_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setAddressExecutive_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.Address a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {139,24,123,61};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Execution fee address is address which can take fees from pools.
public class setAddressExecutiveFee_Arc4GroupTransaction: ProxyBase
{
	public setAddressExecutiveFee_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setAddressExecutiveFee_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.Address a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.Address)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {80,224,125,136};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//App identity setter
public class setBiatecIdentity_Arc4GroupTransaction: ProxyBase
{
	public setBiatecIdentity_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setBiatecIdentity_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {186,190,30,17};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//App identity setter
public class setBiatecPool_Arc4GroupTransaction: ProxyBase
{
	public setBiatecPool_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setBiatecPool_Arc4GroupTransaction() : base(null,0)  {} 
//Address
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 a {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {197,139,157,164};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {a}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//Fees in 9 decimals. 1_000_000_000 = 100%\\nFees in 9 decimals. 10_000_000 = 1%\\nFees in 9 decimals. 100_000 = 0,01%\\n\\n\\nFees are respectful from the all fees taken to the LP providers. If LPs charge 1% fee, and biatec charges 10% fee, LP will receive 0.09% fee and biatec 0.01% fee
public class setBiatecFee_Arc4GroupTransaction: ProxyBase
{
	public setBiatecFee_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private setBiatecFee_Arc4GroupTransaction() : base(null,0)  {} 
//Fee
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt256)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint256");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {202,52,74,52};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {biatecFee}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//addressExecutiveFee can perfom key registration for this LP pool\\n\\n\\nOnly addressExecutiveFee is allowed to execute this method.
public class sendOnlineKeyRegistration_Arc4GroupTransaction: ProxyBase
{
	public sendOnlineKeyRegistration_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private sendOnlineKeyRegistration_Arc4GroupTransaction() : base(null,0)  {} 
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
		
		byte[] abiHandle = {74,230,233,203};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {votePK,selectionPK,stateProofPK,voteFirst,voteLast,voteKeyDilution,fee}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}


		//If someone deposits excess assets to this smart contract biatec can use them.\\n\\n\\nOnly addressExecutiveFee is allowed to execute this method.
public class withdrawExcessAssets_Arc4GroupTransaction: ProxyBase
{
	public withdrawExcessAssets_Arc4GroupTransaction(DefaultApi algodApi,ulong appId) : base(algodApi, appId) {}
	private withdrawExcessAssets_Arc4GroupTransaction() : base(null,0)  {} 
//Asset to withdraw. If native token, then zero
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 asset {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
//Amount of the asset to be withdrawn
	public AVM.ClientGenerator.ABI.ARC4.Types.UInt64 amount {get;set;}= (AVM.ClientGenerator.ABI.ARC4.Types.UInt64)AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
	public async Task<List<Transaction>> Invoke(ulong? _tx_fee, OnCompleteType _tx_onComplete, ulong _tx_roundValidity, string _tx_note, Account _tx_sender, List<ulong> _tx_foreignApps, List<ulong> _tx_foreignAssets, List<Address> _tx_accounts, List<BoxRef>? _tx_boxes = null)
	{
		
		byte[] abiHandle = {135,40,55,48};
return await base.MakeArc4TransactionList(null, _tx_fee, _tx_onComplete, _tx_roundValidity, _tx_note, _tx_sender, abiHandle, new List<AVM.ClientGenerator.ABI.ARC4.Types.WireType> {asset,amount}, _tx_foreignApps, _tx_foreignAssets, _tx_accounts, _tx_boxes);
	}
}



		protected override string SourceApproval { get; set; }= "I3ByYWdtYSB2ZXJzaW9uIDEwCmludGNibG9jayAxIDMyIDAKYnl0ZWNibG9jayAweDc1IDB4NjU2NiAweDY1IDB4MDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAzYjlhY2EwMCAweDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDIgIkJJQVRFQy1DT05GSUctMDEtMDItMDEiIDB4NzM2Mzc2NjU3MiAweDY3IDB4NzMgMHg2NiAweDY5IDB4NzAKCi8vIFRoaXMgVEVBTCB3YXMgZ2VuZXJhdGVkIGJ5IFRFQUxTY3JpcHQgdjAuMTA3LjAKLy8gaHR0cHM6Ly9naXRodWIuY29tL2FsZ29yYW5kZm91bmRhdGlvbi9URUFMU2NyaXB0CgovLyBUaGlzIGNvbnRyYWN0IGlzIGNvbXBsaWFudCB3aXRoIGFuZC9vciBpbXBsZW1lbnRzIHRoZSBmb2xsb3dpbmcgQVJDczogWyBBUkM0IF0KCi8vIFRoZSBmb2xsb3dpbmcgdGVuIGxpbmVzIG9mIFRFQUwgaGFuZGxlIGluaXRpYWwgcHJvZ3JhbSBmbG93Ci8vIFRoaXMgcGF0dGVybiBpcyB1c2VkIHRvIG1ha2UgaXQgZWFzeSBmb3IgYW55b25lIHRvIHBhcnNlIHRoZSBzdGFydCBvZiB0aGUgcHJvZ3JhbSBhbmQgZGV0ZXJtaW5lIGlmIGEgc3BlY2lmaWMgYWN0aW9uIGlzIGFsbG93ZWQKLy8gSGVyZSwgYWN0aW9uIHJlZmVycyB0byB0aGUgT25Db21wbGV0ZSBpbiBjb21iaW5hdGlvbiB3aXRoIHdoZXRoZXIgdGhlIGFwcCBpcyBiZWluZyBjcmVhdGVkIG9yIGNhbGxlZAovLyBFdmVyeSBwb3NzaWJsZSBhY3Rpb24gZm9yIHRoaXMgY29udHJhY3QgaXMgcmVwcmVzZW50ZWQgaW4gdGhlIHN3aXRjaCBzdGF0ZW1lbnQKLy8gSWYgdGhlIGFjdGlvbiBpcyBub3QgaW1wbGVtZW50ZWQgaW4gdGhlIGNvbnRyYWN0LCBpdHMgcmVzcGVjdGl2ZSBicmFuY2ggd2lsbCBiZSAiKk5PVF9JTVBMRU1FTlRFRCIgd2hpY2gganVzdCBjb250YWlucyAiZXJyIgp0eG4gQXBwbGljYXRpb25JRAohCnB1c2hpbnQgNgoqCnR4biBPbkNvbXBsZXRpb24KKwpzd2l0Y2ggKmNhbGxfTm9PcCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQgKk5PVF9JTVBMRU1FTlRFRCAqY2FsbF9VcGRhdGVBcHBsaWNhdGlvbiAqTk9UX0lNUExFTUVOVEVEICpjcmVhdGVfTm9PcCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQgKk5PVF9JTVBMRU1FTlRFRCAqTk9UX0lNUExFTUVOVEVEICpOT1RfSU1QTEVNRU5URUQKCipOT1RfSU1QTEVNRU5URUQ6CgkvLyBUaGUgcmVxdWVzdGVkIGFjdGlvbiBpcyBub3QgaW1wbGVtZW50ZWQgaW4gdGhpcyBjb250cmFjdC4gQXJlIHlvdSB1c2luZyB0aGUgY29ycmVjdCBPbkNvbXBsZXRlPyBEaWQgeW91IHNldCB5b3VyIGFwcCBJRD8KCWVycgoKLy8gY3JlYXRlQXBwbGljYXRpb24oKXZvaWQKKmFiaV9yb3V0ZV9jcmVhdGVBcHBsaWNhdGlvbjoKCS8vIGV4ZWN1dGUgY3JlYXRlQXBwbGljYXRpb24oKXZvaWQKCWNhbGxzdWIgY3JlYXRlQXBwbGljYXRpb24KCWludGMgMCAvLyAxCglyZXR1cm4KCi8vIGNyZWF0ZUFwcGxpY2F0aW9uKCk6IHZvaWQKLy8KLy8gSW5pdGlhbCBzZXR1cApjcmVhdGVBcHBsaWNhdGlvbjoKCXByb3RvIDAgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjYxCgkvLyB0aGlzLnZlcnNpb24udmFsdWUgPSB2ZXJzaW9uCglieXRlYyA2IC8vICAic2N2ZXIiCglieXRlYyA1IC8vICJCSUFURUMtQ09ORklHLTAxLTAyLTAxIgoJYXBwX2dsb2JhbF9wdXQKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czo2MgoJLy8gdGhpcy5hZGRyZXNzRXhlY3V0aXZlLnZhbHVlID0gdGhpcy50eG4uc2VuZGVyCglieXRlYyAyIC8vICAiZSIKCXR4biBTZW5kZXIKCWFwcF9nbG9iYWxfcHV0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6NjMKCS8vIHRoaXMuYWRkcmVzc0dvdi52YWx1ZSA9IHRoaXMudHhuLnNlbmRlcgoJYnl0ZWMgNyAvLyAgImciCgl0eG4gU2VuZGVyCglhcHBfZ2xvYmFsX3B1dAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjY0CgkvLyB0aGlzLmFkZHJlc3NVZHBhdGVyLnZhbHVlID0gdGhpcy50eG4uc2VuZGVyCglieXRlYyAwIC8vICAidSIKCXR4biBTZW5kZXIKCWFwcF9nbG9iYWxfcHV0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6NjUKCS8vIHRoaXMuYWRkcmVzc0V4ZWN1dGl2ZUZlZS52YWx1ZSA9IHRoaXMudHhuLnNlbmRlcgoJYnl0ZWMgMSAvLyAgImVmIgoJdHhuIFNlbmRlcgoJYXBwX2dsb2JhbF9wdXQKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czo2NgoJLy8gdGhpcy5zdXNwZW5kZWQudmFsdWUgPSAwCglieXRlYyA4IC8vICAicyIKCWludGMgMiAvLyAwCglhcHBfZ2xvYmFsX3B1dAoJcmV0c3ViCgovLyB1cGRhdGVBcHBsaWNhdGlvbihieXRlW10pdm9pZAoqYWJpX3JvdXRlX3VwZGF0ZUFwcGxpY2F0aW9uOgoJLy8gbmV3VmVyc2lvbjogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglleHRyYWN0IDIgMAoKCS8vIGV4ZWN1dGUgdXBkYXRlQXBwbGljYXRpb24oYnl0ZVtdKXZvaWQKCWNhbGxzdWIgdXBkYXRlQXBwbGljYXRpb24KCWludGMgMCAvLyAxCglyZXR1cm4KCi8vIHVwZGF0ZUFwcGxpY2F0aW9uKG5ld1ZlcnNpb246IGJ5dGVzKTogdm9pZAovLwovLyBhZGRyZXNzVWRwYXRlciBmcm9tIGdsb2JhbCBiaWF0ZWMgY29uZmlndXJhdGlvbiBpcyBhbGxvd2VkIHRvIHVwZGF0ZSBhcHBsaWNhdGlvbgp1cGRhdGVBcHBsaWNhdGlvbjoKCXByb3RvIDEgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjczCgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFkZHJlc3NVZHBhdGVyLnZhbHVlLCAnT25seSBhZGRyZXNzVWRwYXRlciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB1cGRhdGUgYXBwbGljYXRpb24nKQoJdHhuIFNlbmRlcgoJYnl0ZWMgMCAvLyAgInUiCglhcHBfZ2xvYmFsX2dldAoJPT0KCgkvLyBPbmx5IGFkZHJlc3NVZHBhdGVyIHNldHVwIGluIHRoZSBjb25maWcgY2FuIHVwZGF0ZSBhcHBsaWNhdGlvbgoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6NzQKCS8vIGxvZyh2ZXJzaW9uKQoJYnl0ZWMgNSAvLyAiQklBVEVDLUNPTkZJRy0wMS0wMi0wMSIKCWxvZwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjc1CgkvLyBsb2cobmV3VmVyc2lvbikKCWZyYW1lX2RpZyAtMSAvLyBuZXdWZXJzaW9uOiBieXRlcwoJbG9nCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6NzYKCS8vIHRoaXMudmVyc2lvbi52YWx1ZSA9IG5ld1ZlcnNpb24KCWJ5dGVjIDYgLy8gICJzY3ZlciIKCWZyYW1lX2RpZyAtMSAvLyBuZXdWZXJzaW9uOiBieXRlcwoJYXBwX2dsb2JhbF9wdXQKCXJldHN1YgoKLy8gYm9vdHN0cmFwKHVpbnQyNTYsdWludDY0LHVpbnQ2NCl2b2lkCiphYmlfcm91dGVfYm9vdHN0cmFwOgoJLy8gYXBwQmlhdGVjUG9vbFByb3ZpZGVyOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDMKCWJ0b2kKCgkvLyBhcHBCaWF0ZWNJZGVudGl0eVByb3ZpZGVyOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDIKCWJ0b2kKCgkvLyBiaWF0ZWNGZWU6IHVpbnQyNTYKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWR1cAoJbGVuCglpbnRjIDEgLy8gMzIKCT09CgoJLy8gYXJndW1lbnQgMiAoYmlhdGVjRmVlKSBmb3IgYm9vdHN0cmFwIG11c3QgYmUgYSB1aW50MjU2Cglhc3NlcnQKCgkvLyBleGVjdXRlIGJvb3RzdHJhcCh1aW50MjU2LHVpbnQ2NCx1aW50NjQpdm9pZAoJY2FsbHN1YiBib290c3RyYXAKCWludGMgMCAvLyAxCglyZXR1cm4KCi8vIGJvb3RzdHJhcChiaWF0ZWNGZWU6IHVpbnQyNTYsIGFwcEJpYXRlY0lkZW50aXR5UHJvdmlkZXI6IEFwcElELCBhcHBCaWF0ZWNQb29sUHJvdmlkZXI6IEFwcElEKTogdm9pZAovLwovLyBTZXR1cCB0aGUgY29udHJhY3QKLy8gQHBhcmFtIGJpYXRlY0ZlZSBCaWF0ZWMgZmVlcwpib290c3RyYXA6Cglwcm90byAzIDAKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czo4NAoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gdGhpcy5hZGRyZXNzVWRwYXRlci52YWx1ZSwgJ09ubHkgdXBkYXRlciBjYW4gY2FsbCBib290c3RyYXAgbWV0aG9kJykKCXR4biBTZW5kZXIKCWJ5dGVjIDAgLy8gICJ1IgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gT25seSB1cGRhdGVyIGNhbiBjYWxsIGJvb3RzdHJhcCBtZXRob2QKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjg1CgkvLyBhc3NlcnQoYmlhdGVjRmVlIDw9IChTQ0FMRSBhcyB1aW50MjU2KSAvIDIsICdCaWF0ZWMgY2Fubm90IHNldCBmZWVzIGhpZ2hlciB0aGVuIDUwJSBvZiBscCBmZWVzJykKCWZyYW1lX2RpZyAtMSAvLyBiaWF0ZWNGZWU6IHVpbnQyNTYKCWJ5dGVjIDMgLy8gMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDNiOWFjYTAwCglieXRlYyA0IC8vIDB4MDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMgoJYi8KCWI8PQoKCS8vIEJpYXRlYyBjYW5ub3Qgc2V0IGZlZXMgaGlnaGVyIHRoZW4gNTAlIG9mIGxwIGZlZXMKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjg2CgkvLyB0aGlzLmJpYXRlY0ZlZS52YWx1ZSA9IGJpYXRlY0ZlZQoJYnl0ZWMgOSAvLyAgImYiCglmcmFtZV9kaWcgLTEgLy8gYmlhdGVjRmVlOiB1aW50MjU2CglhcHBfZ2xvYmFsX3B1dAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjg3CgkvLyB0aGlzLmFwcEJpYXRlY0lkZW50aXR5UHJvdmlkZXIudmFsdWUgPSBhcHBCaWF0ZWNJZGVudGl0eVByb3ZpZGVyCglieXRlYyAxMCAvLyAgImkiCglmcmFtZV9kaWcgLTIgLy8gYXBwQmlhdGVjSWRlbnRpdHlQcm92aWRlcjogQXBwSUQKCWFwcF9nbG9iYWxfcHV0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6ODgKCS8vIHRoaXMuYXBwQmlhdGVjUG9vbFByb3ZpZGVyLnZhbHVlID0gYXBwQmlhdGVjUG9vbFByb3ZpZGVyCglieXRlYyAxMSAvLyAgInAiCglmcmFtZV9kaWcgLTMgLy8gYXBwQmlhdGVjUG9vbFByb3ZpZGVyOiBBcHBJRAoJYXBwX2dsb2JhbF9wdXQKCXJldHN1YgoKLy8gc2V0QWRkcmVzc1VkcGF0ZXIoYWRkcmVzcyl2b2lkCiphYmlfcm91dGVfc2V0QWRkcmVzc1VkcGF0ZXI6CgkvLyBhOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglkdXAKCWxlbgoJaW50YyAxIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDAgKGEpIGZvciBzZXRBZGRyZXNzVWRwYXRlciBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gZXhlY3V0ZSBzZXRBZGRyZXNzVWRwYXRlcihhZGRyZXNzKXZvaWQKCWNhbGxzdWIgc2V0QWRkcmVzc1VkcGF0ZXIKCWludGMgMCAvLyAxCglyZXR1cm4KCi8vIHNldEFkZHJlc3NVZHBhdGVyKGE6IEFkZHJlc3MpOiB2b2lkCi8vCi8vIFRvcCBzZWNyZXQgYWNjb3VudCB3aXRoIHdoaWNoIGl0IGlzIHBvc3NpYmxlIHVwZGF0ZSBjb250cmFjdHMgb3IgaWRlbnRpdHkgcHJvdmlkZXIKLy8KLy8gQHBhcmFtIGEgQWRkcmVzcwpzZXRBZGRyZXNzVWRwYXRlcjoKCXByb3RvIDEgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjk3CgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFkZHJlc3NVZHBhdGVyLnZhbHVlLCAnT25seSB1cGRhdGVyIGNhbiBjaGFuZ2UgdXBkYXRlciBhZGRyZXNzJykKCXR4biBTZW5kZXIKCWJ5dGVjIDAgLy8gICJ1IgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gT25seSB1cGRhdGVyIGNhbiBjaGFuZ2UgdXBkYXRlciBhZGRyZXNzCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czo5OAoJLy8gdGhpcy5hZGRyZXNzVWRwYXRlci52YWx1ZSA9IGEKCWJ5dGVjIDAgLy8gICJ1IgoJZnJhbWVfZGlnIC0xIC8vIGE6IEFkZHJlc3MKCWFwcF9nbG9iYWxfcHV0CglyZXRzdWIKCi8vIHNldFBhdXNlZCh1aW50NjQpdm9pZAoqYWJpX3JvdXRlX3NldFBhdXNlZDoKCS8vIGE6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJYnRvaQoKCS8vIGV4ZWN1dGUgc2V0UGF1c2VkKHVpbnQ2NCl2b2lkCgljYWxsc3ViIHNldFBhdXNlZAoJaW50YyAwIC8vIDEKCXJldHVybgoKLy8gc2V0UGF1c2VkKGE6IHVpbnQ2NCk6IHZvaWQKLy8KLy8gS2lsbCBzd2l0Y2guIEluIHRoZSBleHRyZW1lIGNhc2UgYWxsIHNlcnZpY2VzIChkZXBvc2l0LCB0cmFkaW5nLCB3aXRoZHJhd2FsLCBpZGVudGl0eSBtb2RpZmljYXRpb25zIGFuZCBtb3JlKSBjYW4gYmUgc3VzcGVuZGVkLgovLwovLyBAcGFyYW0gYSBBZGRyZXNzCnNldFBhdXNlZDoKCXByb3RvIDEgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjEwNwoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gdGhpcy5hZGRyZXNzVWRwYXRlci52YWx1ZSwgJ09ubHkgdXBkYXRlciBjYW4gcGF1c2UgYW5kIHVucGF1c2UgdGhlIGJpYXRlYyBzZXJ2aWNlcycpCgl0eG4gU2VuZGVyCglieXRlYyAwIC8vICAidSIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoKCS8vIE9ubHkgdXBkYXRlciBjYW4gcGF1c2UgYW5kIHVucGF1c2UgdGhlIGJpYXRlYyBzZXJ2aWNlcwoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTA4CgkvLyB0aGlzLnN1c3BlbmRlZC52YWx1ZSA9IGEKCWJ5dGVjIDggLy8gICJzIgoJZnJhbWVfZGlnIC0xIC8vIGE6IHVpbnQ2NAoJYXBwX2dsb2JhbF9wdXQKCXJldHN1YgoKLy8gc2V0QWRkcmVzc0dvdihhZGRyZXNzKXZvaWQKKmFiaV9yb3V0ZV9zZXRBZGRyZXNzR292OgoJLy8gYTogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJZHVwCglsZW4KCWludGMgMSAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAwIChhKSBmb3Igc2V0QWRkcmVzc0dvdiBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gZXhlY3V0ZSBzZXRBZGRyZXNzR292KGFkZHJlc3Mpdm9pZAoJY2FsbHN1YiBzZXRBZGRyZXNzR292CglpbnRjIDAgLy8gMQoJcmV0dXJuCgovLyBzZXRBZGRyZXNzR292KGE6IEFkZHJlc3MpOiB2b2lkCi8vCi8vIEV4ZWN1dGlvbiBhZGRyZXNzIHdpdGggd2hpY2ggaXQgaXMgcG9zc2libGUgdG8gb3B0IGluIGZvciBnb3Zlcm5hbmNlCi8vCi8vIEBwYXJhbSBhIEFkZHJlc3MKc2V0QWRkcmVzc0dvdjoKCXByb3RvIDEgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjExNwoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gdGhpcy5hZGRyZXNzVWRwYXRlci52YWx1ZSwgJ09ubHkgdXBkYXRlciBjYW4gY2hhbmdlIGdvdiBhZGRyZXNzJykKCXR4biBTZW5kZXIKCWJ5dGVjIDAgLy8gICJ1IgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gT25seSB1cGRhdGVyIGNhbiBjaGFuZ2UgZ292IGFkZHJlc3MKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjExOAoJLy8gdGhpcy5hZGRyZXNzR292LnZhbHVlID0gYQoJYnl0ZWMgNyAvLyAgImciCglmcmFtZV9kaWcgLTEgLy8gYTogQWRkcmVzcwoJYXBwX2dsb2JhbF9wdXQKCXJldHN1YgoKLy8gc2V0QWRkcmVzc0V4ZWN1dGl2ZShhZGRyZXNzKXZvaWQKKmFiaV9yb3V0ZV9zZXRBZGRyZXNzRXhlY3V0aXZlOgoJLy8gYTogYWRkcmVzcwoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJZHVwCglsZW4KCWludGMgMSAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAwIChhKSBmb3Igc2V0QWRkcmVzc0V4ZWN1dGl2ZSBtdXN0IGJlIGEgYWRkcmVzcwoJYXNzZXJ0CgoJLy8gZXhlY3V0ZSBzZXRBZGRyZXNzRXhlY3V0aXZlKGFkZHJlc3Mpdm9pZAoJY2FsbHN1YiBzZXRBZGRyZXNzRXhlY3V0aXZlCglpbnRjIDAgLy8gMQoJcmV0dXJuCgovLyBzZXRBZGRyZXNzRXhlY3V0aXZlKGE6IEFkZHJlc3MpOiB2b2lkCi8vCi8vIEV4ZWN1dGlvbiBhZGRyZXNzIHdpdGggd2hpY2ggaXQgaXMgcG9zc2libGUgdG8gY2hhbmdlIGdsb2JhbCBiaWF0ZWMgZmVlcwovLwovLyBAcGFyYW0gYSBBZGRyZXNzCnNldEFkZHJlc3NFeGVjdXRpdmU6Cglwcm90byAxIDAKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoxMjcKCS8vIGFzc2VydCh0aGlzLnR4bi5zZW5kZXIgPT09IHRoaXMuYWRkcmVzc1VkcGF0ZXIudmFsdWUsICdPbmx5IHVwZGF0ZXIgY2FuIGNoYW5nZSBhZGRyZXNzRXhlY3V0aXZlJykKCXR4biBTZW5kZXIKCWJ5dGVjIDAgLy8gICJ1IgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gT25seSB1cGRhdGVyIGNhbiBjaGFuZ2UgYWRkcmVzc0V4ZWN1dGl2ZQoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTI4CgkvLyB0aGlzLmFkZHJlc3NFeGVjdXRpdmUudmFsdWUgPSBhCglieXRlYyAyIC8vICAiZSIKCWZyYW1lX2RpZyAtMSAvLyBhOiBBZGRyZXNzCglhcHBfZ2xvYmFsX3B1dAoJcmV0c3ViCgovLyBzZXRBZGRyZXNzRXhlY3V0aXZlRmVlKGFkZHJlc3Mpdm9pZAoqYWJpX3JvdXRlX3NldEFkZHJlc3NFeGVjdXRpdmVGZWU6CgkvLyBhOiBhZGRyZXNzCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglkdXAKCWxlbgoJaW50YyAxIC8vIDMyCgk9PQoKCS8vIGFyZ3VtZW50IDAgKGEpIGZvciBzZXRBZGRyZXNzRXhlY3V0aXZlRmVlIG11c3QgYmUgYSBhZGRyZXNzCglhc3NlcnQKCgkvLyBleGVjdXRlIHNldEFkZHJlc3NFeGVjdXRpdmVGZWUoYWRkcmVzcyl2b2lkCgljYWxsc3ViIHNldEFkZHJlc3NFeGVjdXRpdmVGZWUKCWludGMgMCAvLyAxCglyZXR1cm4KCi8vIHNldEFkZHJlc3NFeGVjdXRpdmVGZWUoYTogQWRkcmVzcyk6IHZvaWQKLy8KLy8gRXhlY3V0aW9uIGZlZSBhZGRyZXNzIGlzIGFkZHJlc3Mgd2hpY2ggY2FuIHRha2UgZmVlcyBmcm9tIHBvb2xzLgovLwovLyBAcGFyYW0gYSBBZGRyZXNzCnNldEFkZHJlc3NFeGVjdXRpdmVGZWU6Cglwcm90byAxIDAKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoxMzcKCS8vIGFzc2VydCh0aGlzLnR4bi5zZW5kZXIgPT09IHRoaXMuYWRkcmVzc0V4ZWN1dGl2ZS52YWx1ZSwgJ09ubHkgYWRkcmVzc0V4ZWN1dGl2ZSBjYW4gY2hhbmdlIGZlZSBleGVjdXRvciBhZGRyZXNzJykKCXR4biBTZW5kZXIKCWJ5dGVjIDIgLy8gICJlIgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gT25seSBhZGRyZXNzRXhlY3V0aXZlIGNhbiBjaGFuZ2UgZmVlIGV4ZWN1dG9yIGFkZHJlc3MKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjEzOAoJLy8gdGhpcy5hZGRyZXNzRXhlY3V0aXZlRmVlLnZhbHVlID0gYQoJYnl0ZWMgMSAvLyAgImVmIgoJZnJhbWVfZGlnIC0xIC8vIGE6IEFkZHJlc3MKCWFwcF9nbG9iYWxfcHV0CglyZXRzdWIKCi8vIHNldEJpYXRlY0lkZW50aXR5KHVpbnQ2NCl2b2lkCiphYmlfcm91dGVfc2V0QmlhdGVjSWRlbnRpdHk6CgkvLyBhOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWJ0b2kKCgkvLyBleGVjdXRlIHNldEJpYXRlY0lkZW50aXR5KHVpbnQ2NCl2b2lkCgljYWxsc3ViIHNldEJpYXRlY0lkZW50aXR5CglpbnRjIDAgLy8gMQoJcmV0dXJuCgovLyBzZXRCaWF0ZWNJZGVudGl0eShhOiBBcHBJRCk6IHZvaWQKLy8KLy8gQXBwIGlkZW50aXR5IHNldHRlcgovLwovLyBAcGFyYW0gYSBBZGRyZXNzCnNldEJpYXRlY0lkZW50aXR5OgoJcHJvdG8gMSAwCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTQ3CgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFkZHJlc3NVZHBhdGVyLnZhbHVlLCAnT25seSB1cGRhdGVyIGNhbiBjaGFuZ2UgYXBwSWRlbnRpdHlQcm92aWRlcicpCgl0eG4gU2VuZGVyCglieXRlYyAwIC8vICAidSIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoKCS8vIE9ubHkgdXBkYXRlciBjYW4gY2hhbmdlIGFwcElkZW50aXR5UHJvdmlkZXIKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjE0OAoJLy8gdGhpcy5hcHBCaWF0ZWNJZGVudGl0eVByb3ZpZGVyLnZhbHVlID0gYQoJYnl0ZWMgMTAgLy8gICJpIgoJZnJhbWVfZGlnIC0xIC8vIGE6IEFwcElECglhcHBfZ2xvYmFsX3B1dAoJcmV0c3ViCgovLyBzZXRCaWF0ZWNQb29sKHVpbnQ2NCl2b2lkCiphYmlfcm91dGVfc2V0QmlhdGVjUG9vbDoKCS8vIGE6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJYnRvaQoKCS8vIGV4ZWN1dGUgc2V0QmlhdGVjUG9vbCh1aW50NjQpdm9pZAoJY2FsbHN1YiBzZXRCaWF0ZWNQb29sCglpbnRjIDAgLy8gMQoJcmV0dXJuCgovLyBzZXRCaWF0ZWNQb29sKGE6IEFwcElEKTogdm9pZAovLwovLyBBcHAgaWRlbnRpdHkgc2V0dGVyCi8vCi8vIEBwYXJhbSBhIEFkZHJlc3MKc2V0QmlhdGVjUG9vbDoKCXByb3RvIDEgMAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjE1NwoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gdGhpcy5hZGRyZXNzVWRwYXRlci52YWx1ZSwgJ09ubHkgdXBkYXRlciBjYW4gY2hhbmdlIGFwcFBvb2xQcm92aWRlcicpCgl0eG4gU2VuZGVyCglieXRlYyAwIC8vICAidSIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoKCS8vIE9ubHkgdXBkYXRlciBjYW4gY2hhbmdlIGFwcFBvb2xQcm92aWRlcgoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTU4CgkvLyB0aGlzLmFwcEJpYXRlY1Bvb2xQcm92aWRlci52YWx1ZSA9IGEKCWJ5dGVjIDExIC8vICAicCIKCWZyYW1lX2RpZyAtMSAvLyBhOiBBcHBJRAoJYXBwX2dsb2JhbF9wdXQKCXJldHN1YgoKLy8gc2V0QmlhdGVjRmVlKHVpbnQyNTYpdm9pZAoqYWJpX3JvdXRlX3NldEJpYXRlY0ZlZToKCS8vIGJpYXRlY0ZlZTogdWludDI1NgoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMQoJZHVwCglsZW4KCWludGMgMSAvLyAzMgoJPT0KCgkvLyBhcmd1bWVudCAwIChiaWF0ZWNGZWUpIGZvciBzZXRCaWF0ZWNGZWUgbXVzdCBiZSBhIHVpbnQyNTYKCWFzc2VydAoKCS8vIGV4ZWN1dGUgc2V0QmlhdGVjRmVlKHVpbnQyNTYpdm9pZAoJY2FsbHN1YiBzZXRCaWF0ZWNGZWUKCWludGMgMCAvLyAxCglyZXR1cm4KCi8vIHNldEJpYXRlY0ZlZShiaWF0ZWNGZWU6IHVpbnQyNTYpOiB2b2lkCi8vCi8vIEZlZXMgaW4gOSBkZWNpbWFscy4gMV8wMDBfMDAwXzAwMCA9IDEwMCUKLy8gRmVlcyBpbiA5IGRlY2ltYWxzLiAxMF8wMDBfMDAwID0gMSUKLy8gRmVlcyBpbiA5IGRlY2ltYWxzLiAxMDBfMDAwID0gMCwwMSUKLy8KLy8gRmVlcyBhcmUgcmVzcGVjdGZ1bCBmcm9tIHRoZSBhbGwgZmVlcyB0YWtlbiB0byB0aGUgTFAgcHJvdmlkZXJzLiBJZiBMUHMgY2hhcmdlIDElIGZlZSwgYW5kIGJpYXRlYyBjaGFyZ2VzIDEwJSBmZWUsIExQIHdpbGwgcmVjZWl2ZSAwLjA5JSBmZWUgYW5kIGJpYXRlYyAwLjAxJSBmZWUKLy8KLy8gQHBhcmFtIGJpYXRlY0ZlZSBGZWUKc2V0QmlhdGVjRmVlOgoJcHJvdG8gMSAwCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTcxCgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFkZHJlc3NFeGVjdXRpdmUudmFsdWUsICdPbmx5IGV4ZWN1dGl2ZSBhZGRyZXNzIGNhbiBjaGFuZ2UgZmVlcycpCgl0eG4gU2VuZGVyCglieXRlYyAyIC8vICAiZSIKCWFwcF9nbG9iYWxfZ2V0Cgk9PQoKCS8vIE9ubHkgZXhlY3V0aXZlIGFkZHJlc3MgY2FuIGNoYW5nZSBmZWVzCglhc3NlcnQKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoxNzIKCS8vIGFzc2VydChiaWF0ZWNGZWUgPD0gKFNDQUxFIGFzIHVpbnQyNTYpIC8gMiwgJ0JpYXRlYyBjYW5ub3Qgc2V0IGZlZXMgaGlnaGVyIHRoZW4gNTAlIG9mIGxwIGZlZXMnKQoJZnJhbWVfZGlnIC0xIC8vIGJpYXRlY0ZlZTogdWludDI1NgoJYnl0ZWMgMyAvLyAweDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwM2I5YWNhMDAKCWJ5dGVjIDQgLy8gMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAyCgliLwoJYjw9CgoJLy8gQmlhdGVjIGNhbm5vdCBzZXQgZmVlcyBoaWdoZXIgdGhlbiA1MCUgb2YgbHAgZmVlcwoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTczCgkvLyB0aGlzLmJpYXRlY0ZlZS52YWx1ZSA9IGJpYXRlY0ZlZQoJYnl0ZWMgOSAvLyAgImYiCglmcmFtZV9kaWcgLTEgLy8gYmlhdGVjRmVlOiB1aW50MjU2CglhcHBfZ2xvYmFsX3B1dAoJcmV0c3ViCgovLyBzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uKGJ5dGVbXSxieXRlW10sYnl0ZVtdLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCl2b2lkCiphYmlfcm91dGVfc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbjoKCS8vIGZlZTogdWludDY0Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyA3CglidG9pCgoJLy8gdm90ZUtleURpbHV0aW9uOiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDYKCWJ0b2kKCgkvLyB2b3RlTGFzdDogdWludDY0Cgl0eG5hIEFwcGxpY2F0aW9uQXJncyA1CglidG9pCgoJLy8gdm90ZUZpcnN0OiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDQKCWJ0b2kKCgkvLyBzdGF0ZVByb29mUEs6IGJ5dGVbXQoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMwoJZXh0cmFjdCAyIDAKCgkvLyBzZWxlY3Rpb25QSzogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAyCglleHRyYWN0IDIgMAoKCS8vIHZvdGVQSzogYnl0ZVtdCgl0eG5hIEFwcGxpY2F0aW9uQXJncyAxCglleHRyYWN0IDIgMAoKCS8vIGV4ZWN1dGUgc2VuZE9ubGluZUtleVJlZ2lzdHJhdGlvbihieXRlW10sYnl0ZVtdLGJ5dGVbXSx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQpdm9pZAoJY2FsbHN1YiBzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uCglpbnRjIDAgLy8gMQoJcmV0dXJuCgovLyBzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uKHZvdGVQSzogYnl0ZXMsIHNlbGVjdGlvblBLOiBieXRlcywgc3RhdGVQcm9vZlBLOiBieXRlcywgdm90ZUZpcnN0OiB1aW50NjQsIHZvdGVMYXN0OiB1aW50NjQsIHZvdGVLZXlEaWx1dGlvbjogdWludDY0LCBmZWU6IHVpbnQ2NCk6IHZvaWQKLy8KLy8gYWRkcmVzc0V4ZWN1dGl2ZUZlZSBjYW4gcGVyZm9tIGtleSByZWdpc3RyYXRpb24gZm9yIHRoaXMgTFAgcG9vbAovLwovLyBPbmx5IGFkZHJlc3NFeGVjdXRpdmVGZWUgaXMgYWxsb3dlZCB0byBleGVjdXRlIHRoaXMgbWV0aG9kLgpzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uOgoJcHJvdG8gNyAwCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTgyCgkvLyBhc3NlcnQodGhpcy50eG4uc2VuZGVyID09PSB0aGlzLmFkZHJlc3NFeGVjdXRpdmVGZWUudmFsdWUsICdPbmx5IGZlZSBleGVjdXRvciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB0YWtlIHRoZSBjb2xsZWN0ZWQgZmVlcycpCgl0eG4gU2VuZGVyCglieXRlYyAxIC8vICAiZWYiCglhcHBfZ2xvYmFsX2dldAoJPT0KCgkvLyBPbmx5IGZlZSBleGVjdXRvciBzZXR1cCBpbiB0aGUgY29uZmlnIGNhbiB0YWtlIHRoZSBjb2xsZWN0ZWQgZmVlcwoJYXNzZXJ0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTgzCgkvLyBzZW5kT25saW5lS2V5UmVnaXN0cmF0aW9uKHsKCS8vICAgICAgIHNlbGVjdGlvblBLOiBzZWxlY3Rpb25QSywKCS8vICAgICAgIHN0YXRlUHJvb2ZQSzogc3RhdGVQcm9vZlBLLAoJLy8gICAgICAgdm90ZUZpcnN0OiB2b3RlRmlyc3QsCgkvLyAgICAgICB2b3RlS2V5RGlsdXRpb246IHZvdGVLZXlEaWx1dGlvbiwKCS8vICAgICAgIHZvdGVMYXN0OiB2b3RlTGFzdCwKCS8vICAgICAgIHZvdGVQSzogdm90ZVBLLAoJLy8gICAgICAgZmVlOiBmZWUsCgkvLyAgICAgfSkKCWl0eG5fYmVnaW4KCXB1c2hpbnQgMiAvLyBrZXlyZWcKCWl0eG5fZmllbGQgVHlwZUVudW0KCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoxODQKCS8vIHNlbGVjdGlvblBLOiBzZWxlY3Rpb25QSwoJZnJhbWVfZGlnIC0yIC8vIHNlbGVjdGlvblBLOiBieXRlcwoJaXR4bl9maWVsZCBTZWxlY3Rpb25QSwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjE4NQoJLy8gc3RhdGVQcm9vZlBLOiBzdGF0ZVByb29mUEsKCWZyYW1lX2RpZyAtMyAvLyBzdGF0ZVByb29mUEs6IGJ5dGVzCglpdHhuX2ZpZWxkIFN0YXRlUHJvb2ZQSwoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjE4NgoJLy8gdm90ZUZpcnN0OiB2b3RlRmlyc3QKCWZyYW1lX2RpZyAtNCAvLyB2b3RlRmlyc3Q6IHVpbnQ2NAoJaXR4bl9maWVsZCBWb3RlRmlyc3QKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoxODcKCS8vIHZvdGVLZXlEaWx1dGlvbjogdm90ZUtleURpbHV0aW9uCglmcmFtZV9kaWcgLTYgLy8gdm90ZUtleURpbHV0aW9uOiB1aW50NjQKCWl0eG5fZmllbGQgVm90ZUtleURpbHV0aW9uCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTg4CgkvLyB2b3RlTGFzdDogdm90ZUxhc3QKCWZyYW1lX2RpZyAtNSAvLyB2b3RlTGFzdDogdWludDY0CglpdHhuX2ZpZWxkIFZvdGVMYXN0CgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTg5CgkvLyB2b3RlUEs6IHZvdGVQSwoJZnJhbWVfZGlnIC0xIC8vIHZvdGVQSzogYnl0ZXMKCWl0eG5fZmllbGQgVm90ZVBLCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MTkwCgkvLyBmZWU6IGZlZQoJZnJhbWVfZGlnIC03IC8vIGZlZTogdWludDY0CglpdHhuX2ZpZWxkIEZlZQoKCS8vIFN1Ym1pdCBpbm5lciB0cmFuc2FjdGlvbgoJaXR4bl9zdWJtaXQKCXJldHN1YgoKLy8gd2l0aGRyYXdFeGNlc3NBc3NldHModWludDY0LHVpbnQ2NCl1aW50NjQKKmFiaV9yb3V0ZV93aXRoZHJhd0V4Y2Vzc0Fzc2V0czoKCS8vIFRoZSBBQkkgcmV0dXJuIHByZWZpeAoJcHVzaGJ5dGVzIDB4MTUxZjdjNzUKCgkvLyBhbW91bnQ6IHVpbnQ2NAoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMgoJYnRvaQoKCS8vIGFzc2V0OiB1aW50NjQKCXR4bmEgQXBwbGljYXRpb25BcmdzIDEKCWJ0b2kKCgkvLyBleGVjdXRlIHdpdGhkcmF3RXhjZXNzQXNzZXRzKHVpbnQ2NCx1aW50NjQpdWludDY0CgljYWxsc3ViIHdpdGhkcmF3RXhjZXNzQXNzZXRzCglpdG9iCgljb25jYXQKCWxvZwoJaW50YyAwIC8vIDEKCXJldHVybgoKLy8gd2l0aGRyYXdFeGNlc3NBc3NldHMoYXNzZXQ6IEFzc2V0SUQsIGFtb3VudDogdWludDY0KTogdWludDY0Ci8vCi8vIElmIHNvbWVvbmUgZGVwb3NpdHMgZXhjZXNzIGFzc2V0cyB0byB0aGlzIHNtYXJ0IGNvbnRyYWN0IGJpYXRlYyBjYW4gdXNlIHRoZW0uCi8vCi8vIE9ubHkgYWRkcmVzc0V4ZWN1dGl2ZUZlZSBpcyBhbGxvd2VkIHRvIGV4ZWN1dGUgdGhpcyBtZXRob2QuCi8vCi8vIEBwYXJhbSBhc3NldCBBc3NldCB0byB3aXRoZHJhdy4gSWYgbmF0aXZlIHRva2VuLCB0aGVuIHplcm8KLy8gQHBhcmFtIGFtb3VudCBBbW91bnQgb2YgdGhlIGFzc2V0IHRvIGJlIHdpdGhkcmF3bgp3aXRoZHJhd0V4Y2Vzc0Fzc2V0czoKCXByb3RvIDIgMQoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjIwMwoJLy8gYXNzZXJ0KHRoaXMudHhuLnNlbmRlciA9PT0gdGhpcy5hZGRyZXNzRXhlY3V0aXZlRmVlLnZhbHVlLCAnT25seSBmZWUgZXhlY3V0b3Igc2V0dXAgaW4gdGhlIGNvbmZpZyBjYW4gdGFrZSB0aGUgY29sbGVjdGVkIGZlZXMnKQoJdHhuIFNlbmRlcgoJYnl0ZWMgMSAvLyAgImVmIgoJYXBwX2dsb2JhbF9nZXQKCT09CgoJLy8gT25seSBmZWUgZXhlY3V0b3Igc2V0dXAgaW4gdGhlIGNvbmZpZyBjYW4gdGFrZSB0aGUgY29sbGVjdGVkIGZlZXMKCWFzc2VydAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjIwNQoJLy8gdGhpcy5kb0F4ZmVyKHRoaXMudHhuLnNlbmRlciwgYXNzZXQsIGFtb3VudCkKCWZyYW1lX2RpZyAtMiAvLyBhbW91bnQ6IHVpbnQ2NAoJZnJhbWVfZGlnIC0xIC8vIGFzc2V0OiBBc3NldElECgl0eG4gU2VuZGVyCgljYWxsc3ViIGRvQXhmZXIKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoyMDcKCS8vIHJldHVybiBhbW91bnQ7CglmcmFtZV9kaWcgLTIgLy8gYW1vdW50OiB1aW50NjQKCXJldHN1YgoKLy8gZG9BeGZlcihyZWNlaXZlcjogQWRkcmVzcywgYXNzZXQ6IEFzc2V0SUQsIGFtb3VudDogdWludDY0KTogdm9pZAovLwovLyBFeGVjdXRlcyB4ZmVyIG9mIHBheSBwYXltZW50IG1ldGhvZHMgdG8gc3BlY2lmaWVkIHJlY2VpdmVyIGZyb20gc21hcnQgY29udHJhY3QgYWdncmVnYXRlZCBhY2NvdW50IHdpdGggc3BlY2lmaWVkIGFzc2V0IGFuZCBhbW91bnQgaW4gdG9rZW5zIGRlY2ltYWxzCi8vIEBwYXJhbSByZWNlaXZlciBSZWNlaXZlcgovLyBAcGFyYW0gYXNzZXQgQXNzZXQuIFplcm8gZm9yIGFsZ28KLy8gQHBhcmFtIGFtb3VudCBBbW91bnQgdG8gdHJhbnNmZXIKZG9BeGZlcjoKCXByb3RvIDMgMAoKCS8vICppZjBfY29uZGl0aW9uCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoyMTcKCS8vIGFzc2V0LmlkID09PSAwCglmcmFtZV9kaWcgLTIgLy8gYXNzZXQ6IEFzc2V0SUQKCWludGMgMiAvLyAwCgk9PQoJYnogKmlmMF9lbHNlCgoJLy8gKmlmMF9jb25zZXF1ZW50CgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoyMTgKCS8vIHNlbmRQYXltZW50KHsKCS8vICAgICAgICAgcmVjZWl2ZXI6IHJlY2VpdmVyLAoJLy8gICAgICAgICBhbW91bnQ6IGFtb3VudCwKCS8vICAgICAgICAgZmVlOiAwLAoJLy8gICAgICAgfSkKCWl0eG5fYmVnaW4KCWludGMgMCAvLyAgcGF5CglpdHhuX2ZpZWxkIFR5cGVFbnVtCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MjE5CgkvLyByZWNlaXZlcjogcmVjZWl2ZXIKCWZyYW1lX2RpZyAtMSAvLyByZWNlaXZlcjogQWRkcmVzcwoJaXR4bl9maWVsZCBSZWNlaXZlcgoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjIyMAoJLy8gYW1vdW50OiBhbW91bnQKCWZyYW1lX2RpZyAtMyAvLyBhbW91bnQ6IHVpbnQ2NAoJaXR4bl9maWVsZCBBbW91bnQKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoyMjEKCS8vIGZlZTogMAoJaW50YyAyIC8vIDAKCWl0eG5fZmllbGQgRmVlCgoJLy8gU3VibWl0IGlubmVyIHRyYW5zYWN0aW9uCglpdHhuX3N1Ym1pdAoJYiAqaWYwX2VuZAoKKmlmMF9lbHNlOgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MjI0CgkvLyBzZW5kQXNzZXRUcmFuc2Zlcih7CgkvLyAgICAgICAgIGFzc2V0UmVjZWl2ZXI6IHJlY2VpdmVyLAoJLy8gICAgICAgICB4ZmVyQXNzZXQ6IGFzc2V0LAoJLy8gICAgICAgICBhc3NldEFtb3VudDogYW1vdW50LAoJLy8gICAgICAgICBmZWU6IDAsCgkvLyAgICAgICB9KQoJaXR4bl9iZWdpbgoJcHVzaGludCA0IC8vIGF4ZmVyCglpdHhuX2ZpZWxkIFR5cGVFbnVtCgoJLy8gY29udHJhY3RzXEJpYXRlY0NvbmZpZ1Byb3ZpZGVyLmFsZ28udHM6MjI1CgkvLyBhc3NldFJlY2VpdmVyOiByZWNlaXZlcgoJZnJhbWVfZGlnIC0xIC8vIHJlY2VpdmVyOiBBZGRyZXNzCglpdHhuX2ZpZWxkIEFzc2V0UmVjZWl2ZXIKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoyMjYKCS8vIHhmZXJBc3NldDogYXNzZXQKCWZyYW1lX2RpZyAtMiAvLyBhc3NldDogQXNzZXRJRAoJaXR4bl9maWVsZCBYZmVyQXNzZXQKCgkvLyBjb250cmFjdHNcQmlhdGVjQ29uZmlnUHJvdmlkZXIuYWxnby50czoyMjcKCS8vIGFzc2V0QW1vdW50OiBhbW91bnQKCWZyYW1lX2RpZyAtMyAvLyBhbW91bnQ6IHVpbnQ2NAoJaXR4bl9maWVsZCBBc3NldEFtb3VudAoKCS8vIGNvbnRyYWN0c1xCaWF0ZWNDb25maWdQcm92aWRlci5hbGdvLnRzOjIyOAoJLy8gZmVlOiAwCglpbnRjIDIgLy8gMAoJaXR4bl9maWVsZCBGZWUKCgkvLyBTdWJtaXQgaW5uZXIgdHJhbnNhY3Rpb24KCWl0eG5fc3VibWl0CgoqaWYwX2VuZDoKCXJldHN1YgoKKmNyZWF0ZV9Ob09wOgoJcHVzaGJ5dGVzIDB4Yjg0NDdiMzYgLy8gbWV0aG9kICJjcmVhdGVBcHBsaWNhdGlvbigpdm9pZCIKCXR4bmEgQXBwbGljYXRpb25BcmdzIDAKCW1hdGNoICphYmlfcm91dGVfY3JlYXRlQXBwbGljYXRpb24KCgkvLyB0aGlzIGNvbnRyYWN0IGRvZXMgbm90IGltcGxlbWVudCB0aGUgZ2l2ZW4gQUJJIG1ldGhvZCBmb3IgY3JlYXRlIE5vT3AKCWVycgoKKmNhbGxfTm9PcDoKCXB1c2hieXRlcyAweDQ5NWNlN2VkIC8vIG1ldGhvZCAiYm9vdHN0cmFwKHVpbnQyNTYsdWludDY0LHVpbnQ2NCl2b2lkIgoJcHVzaGJ5dGVzIDB4YmZjMjA4NjAgLy8gbWV0aG9kICJzZXRBZGRyZXNzVWRwYXRlcihhZGRyZXNzKXZvaWQiCglwdXNoYnl0ZXMgMHgwY2RjMTBmYyAvLyBtZXRob2QgInNldFBhdXNlZCh1aW50NjQpdm9pZCIKCXB1c2hieXRlcyAweDZiOTU1ZjRiIC8vIG1ldGhvZCAic2V0QWRkcmVzc0dvdihhZGRyZXNzKXZvaWQiCglwdXNoYnl0ZXMgMHg4YjE4N2IzZCAvLyBtZXRob2QgInNldEFkZHJlc3NFeGVjdXRpdmUoYWRkcmVzcyl2b2lkIgoJcHVzaGJ5dGVzIDB4NTBlMDdkODggLy8gbWV0aG9kICJzZXRBZGRyZXNzRXhlY3V0aXZlRmVlKGFkZHJlc3Mpdm9pZCIKCXB1c2hieXRlcyAweGJhYmUxZTExIC8vIG1ldGhvZCAic2V0QmlhdGVjSWRlbnRpdHkodWludDY0KXZvaWQiCglwdXNoYnl0ZXMgMHhjNThiOWRhNCAvLyBtZXRob2QgInNldEJpYXRlY1Bvb2wodWludDY0KXZvaWQiCglwdXNoYnl0ZXMgMHhjYTM0NGEzNCAvLyBtZXRob2QgInNldEJpYXRlY0ZlZSh1aW50MjU2KXZvaWQiCglwdXNoYnl0ZXMgMHg0YWU2ZTljYiAvLyBtZXRob2QgInNlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24oYnl0ZVtdLGJ5dGVbXSxieXRlW10sdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0KXZvaWQiCglwdXNoYnl0ZXMgMHg4NzI4MzczMCAvLyBtZXRob2QgIndpdGhkcmF3RXhjZXNzQXNzZXRzKHVpbnQ2NCx1aW50NjQpdWludDY0IgoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMAoJbWF0Y2ggKmFiaV9yb3V0ZV9ib290c3RyYXAgKmFiaV9yb3V0ZV9zZXRBZGRyZXNzVWRwYXRlciAqYWJpX3JvdXRlX3NldFBhdXNlZCAqYWJpX3JvdXRlX3NldEFkZHJlc3NHb3YgKmFiaV9yb3V0ZV9zZXRBZGRyZXNzRXhlY3V0aXZlICphYmlfcm91dGVfc2V0QWRkcmVzc0V4ZWN1dGl2ZUZlZSAqYWJpX3JvdXRlX3NldEJpYXRlY0lkZW50aXR5ICphYmlfcm91dGVfc2V0QmlhdGVjUG9vbCAqYWJpX3JvdXRlX3NldEJpYXRlY0ZlZSAqYWJpX3JvdXRlX3NlbmRPbmxpbmVLZXlSZWdpc3RyYXRpb24gKmFiaV9yb3V0ZV93aXRoZHJhd0V4Y2Vzc0Fzc2V0cwoKCS8vIHRoaXMgY29udHJhY3QgZG9lcyBub3QgaW1wbGVtZW50IHRoZSBnaXZlbiBBQkkgbWV0aG9kIGZvciBjYWxsIE5vT3AKCWVycgoKKmNhbGxfVXBkYXRlQXBwbGljYXRpb246CglwdXNoYnl0ZXMgMHg2OTM2YzYyZiAvLyBtZXRob2QgInVwZGF0ZUFwcGxpY2F0aW9uKGJ5dGVbXSl2b2lkIgoJdHhuYSBBcHBsaWNhdGlvbkFyZ3MgMAoJbWF0Y2ggKmFiaV9yb3V0ZV91cGRhdGVBcHBsaWNhdGlvbgoKCS8vIHRoaXMgY29udHJhY3QgZG9lcyBub3QgaW1wbGVtZW50IHRoZSBnaXZlbiBBQkkgbWV0aG9kIGZvciBjYWxsIFVwZGF0ZUFwcGxpY2F0aW9uCgllcnI=";
		protected override string SourceClear { get; set; } = "I3ByYWdtYSB2ZXJzaW9uIDEw";
		protected override string SourceApprovalAVM { get; set; }= "CiADASAAJgwBdQJlZgFlIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA7msoAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACFkJJQVRFQy1DT05GSUctMDEtMDItMDEFc2N2ZXIBZwFzAWYBaQFwMRgUgQYLMRkIjQwCEgAAAAAAAAJwAAACBAAAAAAAAAAAAAAAiAACIkOKAAAnBicFZyoxAGcnBzEAZygxAGcpMQBnJwgkZ4k2GgFXAgCIAAIiQ4oBADEAKGQSRCcFsIv/sCcGi/9niTYaAxc2GgIXNhoBSRUjEkSIAAIiQ4oDADEAKGQSRIv/KycEoqZEJwmL/2cnCov+ZycLi/1niTYaAUkVIxJEiAACIkOKAQAxAChkEkQoi/9niTYaAReIAAIiQ4oBADEAKGQSRCcIi/9niTYaAUkVIxJEiAACIkOKAQAxAChkEkQnB4v/Z4k2GgFJFSMSRIgAAiJDigEAMQAoZBJEKov/Z4k2GgFJFSMSRIgAAiJDigEAMQAqZBJEKYv/Z4k2GgEXiAACIkOKAQAxAChkEkQnCov/Z4k2GgEXiAACIkOKAQAxAChkEkQnC4v/Z4k2GgFJFSMSRIgAAiJDigEAMQAqZBJEi/8rJwSipkQnCYv/Z4k2GgcXNhoGFzYaBRc2GgQXNhoDVwIANhoCVwIANhoBVwIAiAACIkOKBwAxAClkEkSxgQKyEIv+sguL/bI/i/yyDIv6sg6L+7INi/+yCov5sgGziYAEFR98dTYaAhc2GgEXiAAFFlCwIkOKAgExAClkEkSL/ov/MQCIAAOL/omKAwCL/iQSQQATsSKyEIv/sgeL/bIIJLIBs0IAFbGBBLIQi/+yFIv+shGL/bISJLIBs4mABLhEezY2GgCOAf3wAIAESVzn7YAEv8IIYIAEDNwQ/IAEa5VfS4AEixh7PYAEUOB9iIAEur4eEYAExYudpIAEyjRKNIAESubpy4AEhyg3MDYaAI4L/dX+C/4m/j7+Wv51/pD+qP7A/uT/NwCABGk2xi82GgCOAf2nAA==";
		protected override string SourceClearAVM { get; set; } = "Cg==";
		protected override ulong? GlobalNumByteSlices { get; set; }=6;
		protected override ulong? GlobalNumUints { get; set; }=3;
		protected override ulong? LocalNumByteSlices { get; set; }=0;
		protected override ulong? LocalNumUints { get; set; }=0;
		protected override ulong? ExtraProgramPages { get; set; }=0;

	}

}
