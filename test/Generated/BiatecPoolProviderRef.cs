using Algorand;
using AVM.ClientGenerator.Core;
using AVM.ClientGenerator.Core.Attributes; 
using System; 

namespace BiatecPoolProvider
{
	public abstract class BiatecPoolProviderReference : SmartContractReference
	{
		public struct GetCurrentStatusReturn
		{
			
			public ulong field0 {get;set;}
			
			public ulong field1 {get;set;}
			
			public ulong field2 {get;set;}
			
			public ulong field3 {get;set;}
			
			public ulong field4 {get;set;}
			
			public ulong field5 {get;set;}
			
			public ulong field6 {get;set;}
			
			public ulong field7 {get;set;}
			
			public ulong field8 {get;set;}
			
			public ulong field9 {get;set;}
			
			public ulong field10 {get;set;}
			
			public ulong field11 {get;set;}
			
			public ulong field12 {get;set;}
			
			public ulong field13 {get;set;}
			
			public ulong field14 {get;set;}
			
			public ulong field15 {get;set;}
			
			public ulong field16 {get;set;}
			
			public ulong field17 {get;set;}
			
			public ulong field18 {get;set;}
			
			public ulong field19 {get;set;}
			
			public ulong field20 {get;set;}
			
			public ulong field21 {get;set;}
			
			public ulong field22 {get;set;}
			
			public ulong field23 {get;set;}
			
			public ulong field24 {get;set;}
			
			public ulong field25 {get;set;}
			
			public ulong field26 {get;set;}
			
			public ulong field27 {get;set;}
			
			public ulong field28 {get;set;}
			
			public ulong field29 {get;set;}
			
			public ulong field30 {get;set;}
			
			public ulong field31 {get;set;}
			
			public ulong field32 {get;set;}
			
			public ulong field33 {get;set;}
			
			public ulong field34 {get;set;}
			
			public ulong field35 {get;set;}
			
			public ulong field36 {get;set;}
			
			public ulong field37 {get;set;}
			
			public ulong field38 {get;set;}
			
			public ulong field39 {get;set;}
			
			public ulong field40 {get;set;}
			
			public ulong field41 {get;set;}
			
			public ulong field42 {get;set;}
			
			public ulong field43 {get;set;}
			
			public ulong field44 {get;set;}
			
			public ulong field45 {get;set;}
			
			public ulong field46 {get;set;}
			
			public ulong field47 {get;set;}
			
			public ulong field48 {get;set;}
			
			public ulong field49 {get;set;}
			
			public ulong field50 {get;set;}
			
			public ulong field51 {get;set;}
			
			public ulong field52 {get;set;}
			
			public ulong field53 {get;set;}
			
			public ulong field54 {get;set;}
			
			public ulong field55 {get;set;}
		}

		public struct GetPriceReturn
		{
			
			public ulong field0 {get;set;}
			
			public ulong field1 {get;set;}
			
			public ulong field2 {get;set;}
			
			public ulong field3 {get;set;}
			
			public ulong field4 {get;set;}
			
			public ulong field5 {get;set;}
			
			public ulong field6 {get;set;}
			
			public ulong field7 {get;set;}
			
			public ulong field8 {get;set;}
			
			public ulong field9 {get;set;}
			
			public ulong field10 {get;set;}
			
			public ulong field11 {get;set;}
			
			public ulong field12 {get;set;}
			
			public ulong field13 {get;set;}
			
			public ulong field14 {get;set;}
			
			public ulong field15 {get;set;}
			
			public ulong field16 {get;set;}
			
			public ulong field17 {get;set;}
			
			public ulong field18 {get;set;}
			
			public ulong field19 {get;set;}
			
			public ulong field20 {get;set;}
			
			public ulong field21 {get;set;}
			
			public ulong field22 {get;set;}
			
			public ulong field23 {get;set;}
			
			public ulong field24 {get;set;}
			
			public ulong field25 {get;set;}
			
			public ulong field26 {get;set;}
			
			public ulong field27 {get;set;}
			
			public ulong field28 {get;set;}
			
			public ulong field29 {get;set;}
			
			public ulong field30 {get;set;}
			
			public ulong field31 {get;set;}
			
			public ulong field32 {get;set;}
			
			public ulong field33 {get;set;}
			
			public ulong field34 {get;set;}
			
			public ulong field35 {get;set;}
			
			public ulong field36 {get;set;}
			
			public ulong field37 {get;set;}
			
			public ulong field38 {get;set;}
			
			public ulong field39 {get;set;}
			
			public ulong field40 {get;set;}
			
			public ulong field41 {get;set;}
			
			public ulong field42 {get;set;}
			
			public ulong field43 {get;set;}
			
			public ulong field44 {get;set;}
			
			public ulong field45 {get;set;}
			
			public ulong field46 {get;set;}
			
			public ulong field47 {get;set;}
			
			public ulong field48 {get;set;}
			
			public ulong field49 {get;set;}
			
			public ulong field50 {get;set;}
			
			public ulong field51 {get;set;}
			
			public ulong field52 {get;set;}
			
			public ulong field53 {get;set;}
			
			public ulong field54 {get;set;}
			
			public ulong field55 {get;set;}
		}


		///<summary>
		///Biatec deploys single pool provider smart contract
		///</summary>
		///<param name="appBiatecConfigProvider">Biatec amm provider</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> Bootstrap(ulong appBiatecConfigProvider);

		///<summary>
		///Returns current status
		///</summary>
		///<param name="appPoolId">Pool id to retrieve the stats</param>
		///<param name="result">Pool info statistics</param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> GetCurrentStatus(ulong appPoolId,out GetCurrentStatusReturn result);

		///<summary>
		///Initial setup
		///</summary>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> CreateApplication();

		///<summary>
		///addressUdpater from global biatec configuration is allowed to update application
		///</summary>
		///<param name="appBiatecConfigProvider"></param>
		///<param name="newVersion"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> UpdateApplication(ulong appBiatecConfigProvider,byte[] newVersion);

		///<summary>
		///
		///</summary>
		///<param name="appBiatecConfigProvider"></param>
		///<param name="nativeTokenName"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetNativeTokenName(ulong appBiatecConfigProvider,byte[] nativeTokenName);

		///<summary>
		///
		///</summary>
		///<param name="appBiatecConfigProvider"></param>
		///<param name="approvalProgramSize"></param>
		///<param name="offset"></param>
		///<param name="data"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> LoadClammContractData(ulong appBiatecConfigProvider,ulong approvalProgramSize,ulong offset,byte[] data);

		///<summary>
		///No op tx to increase the app call and box size limits
		///</summary>
		///<param name="_i"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> Noop(ulong _i);

		///<summary>
		///Anybody can call this method to bootstrap new clamm pool
		///</summary>
		///<param name="assetA">Asset A ID must be lower then Asset B ID</param>
		///<param name="assetB">Asset B</param>
		///<param name="appBiatecConfigProvider">Biatec amm provider</param>
		///<param name="appBiatecPoolProvider">Pool provider</param>
		///<param name="fee">Fee in base level (9 decimals). value 1_000_000_000 = 1 = 100%. 10_000_000 = 1%. 1_000_000 = 0.1%</param>
		///<param name="priceMin">Min price range. At this point all assets are in asset A.</param>
		///<param name="priceMax">Max price range. At this point all assets are in asset B.</param>
		///<param name="currentPrice">Deployer can specify the current price for easier deployemnt.</param>
		///<param name="verificationClass">Minimum verification level from the biatec identity. Level 0 means no kyc.</param>
		///<param name="result">LP token ID</param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract (Payment txSeed,AppCall) DeployPool(Payment txSeed,ulong assetA,ulong assetB,ulong appBiatecConfigProvider,ulong appBiatecPoolProvider,ulong fee,ulong priceMin,ulong priceMax,ulong currentPrice,ulong verificationClass,out ulong result);

		///<summary>
		///This method is called by constructor of the luquidity pool
		///</summary>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> RegisterPool();

		///<summary>
		///This metod registers the trade and calculates and store the trade statistics
		///</summary>
		///<param name="appPoolId">Liquidity pool smart contract</param>
		///<param name="assetA">Asset A</param>
		///<param name="assetB">Asset B</param>
		///<param name="priceFrom">The original price</param>
		///<param name="priceTo">The new price</param>
		///<param name="amountA">Asset A amount spent or received</param>
		///<param name="amountB">Asset B amount spent or received</param>
		///<param name="feeAmountA">Fees paid in asset A if any</param>
		///<param name="feeAmountB">Fees paid in asset B if any</param>
		///<param name="s">Scale multiplier</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> RegisterTrade(ulong appPoolId,ulong assetA,ulong assetB,ulong priceFrom,ulong priceTo,ulong amountA,ulong amountB,ulong feeAmountA,ulong feeAmountB,ulong s);

		///<summary>
		///addressExecutiveFee can perfom key registration for this LP pool
///
///
///Only addressExecutiveFee is allowed to execute this method.
		///</summary>
		///<param name="appBiatecConfigProvider"></param>
		///<param name="votePK"></param>
		///<param name="selectionPK"></param>
		///<param name="stateProofPK"></param>
		///<param name="voteFirst"></param>
		///<param name="voteLast"></param>
		///<param name="voteKeyDilution"></param>
		///<param name="fee"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SendOnlineKeyRegistration(ulong appBiatecConfigProvider,byte[] votePK,byte[] selectionPK,byte[] stateProofPK,ulong voteFirst,ulong voteLast,ulong voteKeyDilution,ulong fee);

		///<summary>
		///If someone deposits excess assets to this smart contract biatec can use them.
///
///
///Only addressExecutiveFee is allowed to execute this method.
		///</summary>
		///<param name="appBiatecConfigProvider">Biatec config app. Only addressExecutiveFee is allowed to execute this method.</param>
		///<param name="asset">Asset to withdraw. If native token, then zero</param>
		///<param name="amount">Amount of the asset to be withdrawn</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> WithdrawExcessAssets(ulong appBiatecConfigProvider,ulong asset,ulong amount,out ulong result);

		///<summary>
		///Retuns the full price info for the asset pair. If app pool is defined, then it returns the pool info.
		///</summary>
		///<param name="assetA">Asset A must be less than Asset B</param>
		///<param name="assetB">Asset B</param>
		///<param name="appPoolId">Liquidity pool app id. If zero, then aggregated price info is returned.</param>
		///<param name="result">AppPoolInfo with the price info for the asset pair</param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> GetPrice(ulong assetA,ulong assetB,ulong appPoolId,out GetPriceReturn result);

		///<summary>
		///Calculates how much asset B will be taken from the smart contract on LP asset deposit
		///</summary>
		///<param name="inAmount">LP Asset amount in Base decimal representation..</param>
		///<param name="assetBBalance">Asset B balance. Variable ab, in base scale</param>
		///<param name="liquidity">Current liquidity. Variable L, in base scale</param>
		///<param name="result">Amount of asset B to be given to the caller before fees. The result is in Base decimals (9)</param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> CalculateAssetBWithdrawOnLpDeposit(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 inAmount,AVM.ClientGenerator.ABI.ARC4.Types.UInt256 assetBBalance,AVM.ClientGenerator.ABI.ARC4.Types.UInt256 liquidity,out AVM.ClientGenerator.ABI.ARC4.Types.UInt256 result);
	}
}
