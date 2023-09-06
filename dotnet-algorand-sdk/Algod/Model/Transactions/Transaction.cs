
using Algorand.Utils;
using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
#if UNITY
using UnityEngine;
#endif

namespace Algorand.Algod.Model.Transactions
{


    [JsonConverter(typeof(ReturnedTransactionConverter))]
    public interface IReturnableTransaction { }

    [JsonConverter(typeof(JsonSubtypes), "type")]
    [JsonSubtypes.KnownSubType(typeof(ApplicationCallTransaction), "appl")]
    [JsonSubtypes.KnownSubType(typeof(KeyRegistrationTransaction), "keyreg")]
    [JsonSubtypes.KnownSubType(typeof(PaymentTransaction), "pay")]
    [JsonSubtypes.KnownSubType(typeof(AssetFreezeTransaction), "afrz")]
    [JsonSubtypes.KnownSubType(typeof(AssetMovementsTransaction), "axfer")]
    [JsonSubtypes.KnownSubType(typeof(AssetConfigurationTransaction), "acfg")]

    public abstract partial class Transaction : IReturnableTransaction
    {
        private const ulong MIN_TX_FEE_UALGOS = 1000;
        private static readonly byte[] TX_SIGN_PREFIX = Encoding.UTF8.GetBytes("TX");


     
        //used by newtonsoft
        public bool ShouldSerializeNote() { return Note?.Length > 0; }

        public bool ShouldSerializeFee() { return Fee!=0; }
        public bool ShouldSerializeLease() { return Lease?.Length > 0; }

        public bool ShouldSerializeGenesisId() { return GenesisId?.Length > 0; }
        public bool ShouldSerializeFirstValid() { return FirstValid!= 0; }
        public bool ShouldSerializeLastValid() { return LastValid != 0; }

        private byte[] _lease { get; set; }
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Lease")]
        [Newtonsoft.Json.JsonIgnore]
       
#endif
        [Newtonsoft.Json.JsonProperty("lx", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public byte[] Lease
        {
            get
            {
                return _lease;
            }
            set
            {
                if (value != null && value.Length > 0)
                    _lease = value;
            }
        }




        private byte[] _note { get; set; }
#if UNITY
    [field:SerializeField]
    [Tooltip(@"")]
    [field:InspectorName(@"Note")]
    [Newtonsoft.Json.JsonIgnore]
  
#endif
        [Newtonsoft.Json.JsonProperty("note", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public byte[] Note
        {
            get
            {
                return _note;
            }
            set
            {
                if (value != null && value.Length > 0)
                    _note = value;
            }
        }




#if UNITY
        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"ConfirmedRound")]
        [JsonIgnore]
        public ulong ConfirmedRound { get; internal set; }

       
        [JsonIgnore]
        public bool Committed => ConfirmedRound > 0;

        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"ReceiverRewards")]
        [JsonIgnore]
        public ulong ReceiverRewards { get; internal set; }

        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"SenderRewards")]
        [JsonIgnore]
        public ulong SenderRewards { get; internal set; }

        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"CloseRewards")]
        [JsonIgnore]
        public ulong CloseRewards { get; internal set; }
#else
        [JsonIgnore]
        public ulong? ConfirmedRound { get; internal set; }

         [JsonIgnore]
        public bool Committed => (ConfirmedRound ?? 0) > 0;

          [JsonIgnore]
        public ulong? ReceiverRewards { get; internal set; }

        
        [JsonIgnore]
        public ulong? SenderRewards { get; internal set; }

        
        [JsonIgnore]
        public ulong? CloseRewards { get; internal set; }
#endif


        [JsonIgnore]
        public string PoolError { get; internal set; }

      



        /// <summary>
        /// Sets the transaction fee according to suggestedFeePerByte * estimateTxSize.
        /// </summary>
        /// <param name="tx">transaction to populate fee field</param>
        /// <param name="suggestedFeePerByte">suggestedFee given by network</param>
        public void SetFeeByFeePerByte( ulong? suggestedFeePerByte)
        {
            ulong? newFee = suggestedFeePerByte * (ulong)EstimatedEncodedSize();
            if (newFee < MIN_TX_FEE_UALGOS)
            {
                newFee = MIN_TX_FEE_UALGOS;
            }
            Fee = newFee.Value;
        }

        /// <summary>
        /// Return encoded representation of the transaction with a prefix
        /// suitable for signing
        /// </summary>
        /// <returns></returns>
        public byte[] BytesToSign()
        {
            byte[] encodedTx = Algorand.Utils.Encoder.EncodeToMsgPackOrdered(this);
            var retList = new List<byte>();
            retList.AddRange(TX_SIGN_PREFIX);
            retList.AddRange(encodedTx);
            return retList.ToArray();
        }


        /// <summary>
        /// Return transaction ID as Digest
        /// </summary>
        /// <returns></returns>
        public Digest RawTxID()
        {
            return new Digest(Digester.Digest(BytesToSign()));
        }

    
        /// <summary>
        /// Return transaction ID as string
        /// </summary>
        /// <returns></returns>
        public string TxID()
        {
            return Base32.EncodeToString(this.RawTxID().Bytes, false);
        }

        public void SetFee(ulong fee)
        {
            if (fee < MIN_TX_FEE_UALGOS) fee = MIN_TX_FEE_UALGOS;
            Fee = fee;
        }

        public SignedTransaction Sign(Account signingAccount)
        {
            byte[] prefixEncodedTx = BytesToSign();
            Signature txSig = signingAccount.SignRawBytes(prefixEncodedTx);
            var stx = new SignedTransaction(this, txSig);
            if (!Sender.Equals(signingAccount.Address))
                stx.AuthAddr = signingAccount.Address;
            
            return stx;
        }

        /// <summary>
        /// Sign a transaction with this account, replacing the fee with the given feePerByte.
        /// </summary>
        /// <param name="tx">transaction to sign</param>
        /// <param name="feePerByte">feePerByte fee per byte, often returned as a suggested fee</param>
        /// <returns>a signed transaction</returns>
        public SignedTransaction Sign(ulong feePerByte,Account signingAccount)
        {
            SetFeeByFeePerByte(feePerByte);
            return Sign(signingAccount);
        }

        public SignedTransaction Sign(LogicsigSignature lsig)
        {
            if (!lsig.Verify(Sender))
            {
                throw new ArgumentException("verification failed");
            }
            return new SignedTransaction(this,null, null, lsig,null);
        }

        public SignedTransaction Sign(MultisigAddress from, Account signingAccount)
        {
            //TODO - replace this old code - add more MultiSig support - add examples

            // check that account secret key is in multisig pk list
            var myEncoded = signingAccount.KeyPair.ClearTextPublicKey;
            int myI = -1;
            for (int i = 0; i < from.publicKeys.Count; i++)                                             //for each key in the 'from' of the transaction
                if (Enumerable.SequenceEqual(myEncoded, from.publicKeys[i].Export(NSec.Cryptography.KeyBlobFormat.PkixPublicKey)))               //check the signing account is there
                {
                    myI = i;
                    break;
                }

            if (myI == -1)
            {
                throw new ArgumentException("Multisig account does not contain this secret key");
            }

            // now, create the multisignature
            SignedTransaction txSig = Sign(signingAccount);                                             //sign this transaction with the signing account
            MultisigSignature mSig = new MultisigSignature(from.version, from.threshold);               //create a new multisignature like the current 'from'
            for (int i = 0; i < from.publicKeys.Count; i++)                                             //for each sig in the original from
            {
                if (i == myI)                                                                           //if it's our key
                {
                    mSig.Subsigs.Add(new MultisigSubsig(signingAccount.KeyPair.PublicKey, txSig.Sig));  //then add the new subsig
                }
                else
                {
                    mSig.Subsigs.Add(new MultisigSubsig(from.publicKeys[i]));                           //otherwise just copy the original
                }
            }


            var ret=new SignedTransaction(this, null,mSig,null,null);
            
            if (!from.ToAddress().Equals(ret.Tx.Sender)) { ret.AuthAddr=from.ToAddress(); }

            return ret;
        }


       

        public int EstimatedEncodedSize()
        {
            Account acc = new Account();
            return Utils.Encoder.EncodeToMsgPackOrdered(
                new SignedTransaction(this, acc.SignRawBytes(BytesToSign()))).Length;
        }


    }
}
