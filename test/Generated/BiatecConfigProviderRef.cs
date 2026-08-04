using Algorand;
using AVM.ClientGenerator.Core;
using AVM.ClientGenerator.Core.Attributes; 
using System; 

namespace BiatecConfig
{
	public abstract class BiatecConfigProviderReference : SmartContractReference
	{

		///<summary>
		///Initial setup
		///</summary>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> CreateApplication();

		///<summary>
		///addressUdpater from global biatec configuration is allowed to update application
		///</summary>
		///<param name="newVersion"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> UpdateApplication(byte[] newVersion);

		///<summary>
		///Setup the contract
		///</summary>
		///<param name="biatecFee">Biatec fees</param>
		///<param name="appBiatecIdentityProvider"></param>
		///<param name="appBiatecPoolProvider"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> Bootstrap(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee,ulong appBiatecIdentityProvider,ulong appBiatecPoolProvider);

		///<summary>
		///Top secret account with which it is possible update contracts or identity provider
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetAddressUdpater(Algorand.Address a);

		///<summary>
		///Kill switch. In the extreme case all services (deposit, trading, withdrawal, identity modifications and more) can be suspended.
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetPaused(ulong a);

		///<summary>
		///Execution address with which it is possible to opt in for governance
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetAddressGov(Algorand.Address a);

		///<summary>
		///Execution address with which it is possible to change global biatec fees
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetAddressExecutive(Algorand.Address a);

		///<summary>
		///Execution fee address is address which can take fees from pools.
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetAddressExecutiveFee(Algorand.Address a);

		///<summary>
		///App identity setter
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetBiatecIdentity(ulong a);

		///<summary>
		///App identity setter
		///</summary>
		///<param name="a">Address</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetBiatecPool(ulong a);

		///<summary>
		///Fees in 9 decimals. 1_000_000_000 = 100%
///Fees in 9 decimals. 10_000_000 = 1%
///Fees in 9 decimals. 100_000 = 0,01%
///
///
///Fees are respectful from the all fees taken to the LP providers. If LPs charge 1% fee, and biatec charges 10% fee, LP will receive 0.09% fee and biatec 0.01% fee
		///</summary>
		///<param name="biatecFee">Fee</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SetBiatecFee(AVM.ClientGenerator.ABI.ARC4.Types.UInt256 biatecFee);

		///<summary>
		///addressExecutiveFee can perfom key registration for this LP pool
///
///
///Only addressExecutiveFee is allowed to execute this method.
		///</summary>
		///<param name="votePK"></param>
		///<param name="selectionPK"></param>
		///<param name="stateProofPK"></param>
		///<param name="voteFirst"></param>
		///<param name="voteLast"></param>
		///<param name="voteKeyDilution"></param>
		///<param name="fee"></param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> SendOnlineKeyRegistration(byte[] votePK,byte[] selectionPK,byte[] stateProofPK,ulong voteFirst,ulong voteLast,ulong voteKeyDilution,ulong fee);

		///<summary>
		///If someone deposits excess assets to this smart contract biatec can use them.
///
///
///Only addressExecutiveFee is allowed to execute this method.
		///</summary>
		///<param name="asset">Asset to withdraw. If native token, then zero</param>
		///<param name="amount">Amount of the asset to be withdrawn</param>
		///<param name="result"></param>
		[SmartContractMethod(OnCompleteType.NoOp)]
		public abstract ValueTuple<AppCall> WithdrawExcessAssets(ulong asset,ulong amount,out ulong result);
	}
}
