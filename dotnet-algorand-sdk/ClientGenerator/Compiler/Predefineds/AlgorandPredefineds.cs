using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AVM.ClientGenerator.Compiler.Operators;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class AlgorandPredefineds : Core.ISmartContractPredefineds,Core.ISmartSignaturePredefineds,  ITypePredefined
    {
        CodeBuilder Code;
     
        List<IParameterSymbol> nulledOptionals;
        Dictionary<string,string> literals = new Dictionary<string,string>();

        internal AlgorandPredefineds(CodeBuilder code, Scope scope, List<IParameterSymbol> nulledOptionals, Dictionary<string,string> literals)
        {
            Code = code;
        
            this.nulledOptionals=nulledOptionals;
            this.literals= literals;

        }

        internal AlgorandPredefineds(CodeBuilder code)
        {
            Code = code;
        }

        public ulong Balance
        {
            get
            {
                Code.global("CurrentApplicationAddress");
                Code.balance();
                return 0;
            }
        }

        public ulong MinTransactionFee
        {
            get {
                Code.global("MinTxnFee");
                return 0;
            }
        }

        public ulong MinBalance
        {
            get
            {
                Code.global("CurrentApplicationAddress");
                Code.MinBalance();
                return 0;
            }
        }
        public ulong MaxTransactionLife
        {
            get
            {
                Code.global("MaxTxnLife");
                return 0;
            }
        }
        public byte[] ZeroAddress
        {
            get
            {
                Code.global("ZeroAddress");
                return null;
            }
        }
        public ulong GroupSize
        {
            get
            {
                Code.global("GroupSize");
                return 0;
            }
        }
        public ulong LogicSigVersion
        {
            get
            {
                Code.global("LogicSigVersion");
                return 0;
            }
        }
        public ulong Round
        {
            get
            {
                Code.global("Round");
                return 0;
            }
        }
        public ulong LatestTimeStamp
        {
            get
            {
                Code.global("LatestTimestamp");
                return 0;
            }
        }
        public ulong CurrentApplicationID
        {
            get
            {
                Code.global("CurrentApplicationID");
                return 0;
            }
        }

        public byte[] CreatorAddress
        {
            get
            {
                Code.global("CreatorAddress");
                return null;
            }
        }

        public byte[] CurrentApplicationAddress
        {
            get
            {
                Code.global("CurrentApplicationAddress");
                return null;
            }
        }
        public byte[] GroupId
        {
            get
            {
                Code.global("GroupID");
                return null;
            }
        }

        public ulong OpcodeBudget
        {
            get
            {
                Code.global("OpcodeBudget");
                return 0;
            }
        }
        public ulong CallerApplicationID
        {
            get
            {
                Code.global("CallerApplicationID");
                return 0;
            }
        }
        public byte[] CallerApplicationAddress
        {
            get
            {
                Code.global("CallerApplicationAddress");
                return null;
            }
        }

        public Core.AssetReference[] AssetReferences
        {
            get
            {
                Code.byte_string_literal("");   // retval
                Code.txn("NumAssets");          // retval arlen
                Code.dup();                     // retval arlen pos 
                string loopstart=Code.ReserveLabel();
                string exitloop = Code.ReserveLabel();
                
                Code.AddLabel(loopstart);       // retval arlen pos 

                Code.dup();                     // retval arlen pos pos
                Code.bz(exitloop);              // retval arlen pos 
                
                Code.int_literal_constant(1);   // retval arlen pos 1
                Code.minus();                   // retval arlen pos-1
                Code.dup();                     // retval arlen pos-1 pos-1
                Code.txnas("Assets");           // retval arlen pos-1 Assets[pos-1]
                Code.itob();                    // retval arlen pos-1 Assets[pos-1]b
                Code.uncover(3);                // arlen pos Assets[pos-1]b retval
                
                Code.concat();                  // arlen pos retval
                Code.cover(2);                  // retval arlen pos-1
                
                Code.b(loopstart);              

                Code.AddLabel(exitloop);        // retval arlen pos
                Code.pop();                     // retval arlen
                Code.pop();                     // retval 
                return null;
            }

        }

        public void Ecdsa_pk_decompress_secp256k1(byte[] pk, out byte[] pubkey_X, out byte[] pubkey_Y)
        {
            pubkey_X = null;
            pubkey_Y = null;


            Code.AddComment("Predefined ecdsa_pk_decompress secp256k1");

            Code.ecdsa_pk_decompress("Secp256k1");

        }

        public void Ecdsa_pk_recover_secp256k1(byte[] data, ulong recover_id, byte[] signature_R, byte[] signature_S, out byte[] pubkey_X, out byte[] pubkey_Y)
        {
            pubkey_X = null;
            pubkey_Y = null;


            Code.AddComment("Predefined ecdsa_pk_recover secp256k1");

            Code.ecdsa_pk_recover("Secp256k1");
        }

        public bool Ecdsa_verify_secp256k1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y)
        {
            Code.AddComment("Predefined ecdsa_verify secp256k1");

            Code.ecdsa_verify("Secp256k1");

            return true;
        }

        public bool Ecdsa_verify_secp256r1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y)
        {
            Code.AddComment("Predefined ecdsa_verify secp256r1");

            Code.ecdsa_verify("Secp256r1");

            return true;
        }

        public bool Ed25519verify(byte[] data, byte[] signature, byte[] publicKey)
        {
            Code.AddComment("Predefined ed25519verify ");

            Code.ed25519verify();

            return true;
        }

    

        //TODO - (Important) Add parameter type checking code to enforce that the expected transactions are the right types etc
        public void InvokeSmartContractMethod()
        {
            //This needs to be deferred until all output Code blocks are known
            Code.InvokeSmartContractMethod();
        }

        public void InvokeSmartSignatureMethod()
        {
            //This needs to be deferred until all output Code blocks are known
            Code.InvokeSmartSignatureMethod();
        }

        public byte[] Keccak256(byte[] toHash)
        {
            Code.AddComment("Predefined Keccak256 ");

            Code.keccak256();

            return null;
        }

        public byte[] Sha256(byte[] toHash)
        {
            Code.AddComment("Predefined Sha256 ");

            Code.sha256();

            return null;
        }

        public byte[] Sha512_256(byte[] toHash)
        {
            Code.AddComment("Predefined Sha256 ");

            Code.sha512_256();

            return null;
        }

        public void SwitchLocalAccountContext(int acccountIndex)
        {
            Code.storeabsolute((byte)(Core.Constants.ScratchSpaceSize - 1)); //reserved for local account context info
        }


        public byte[] BigIntegerToByteArray(BigInteger bytes)
        {
            return null;
        }

       
        public BigInteger BigIntegerFromByteArray(byte[] bytes)
        {
            return new BigInteger();
        }

       

        public void LogBytes(byte[] bytes)
        {
            Code.log();
        }

        public void LogUInt(ulong integral)
        {
            Code.itob();
            Code.log();
        }

        public void LogInt(long integral)
        {
            Code.itob();
            Code.log();
        }

        public BigInteger BigIntegerFromIntegral(long integral)
        {
            //TODO - Make this a registered predefined (so that it can be inlined, and later optionally inlined)
            //TODO - Consider switching to sign bit
            Code.dup();
            Code.int_literal_constant(63);
            Code.getbit();
            var pos=Code.ReserveLabel();
            var exit = Code.ReserveLabel();
            Code.bz(pos);
            Code.itob();
            Code.byte_literal_constant("0x"+ String.Concat(Enumerable.Repeat("ff", 64-8))+String.Concat("00",8));
            Code.b_bitwise_or();
            Code.b(exit);
            Code.AddLabel(pos);
            Code.itob();
            Code.byte_literal_constant(BigIntegerOperatorCodeGenerator.maxBigIntegerMask);
            Code.b_bitwise_or();
            Code.AddLabel(exit);
            //TODO - Consider, instead of just returning this dummy value, maintain semantic equivalence.
            return new BigInteger();
        }

       

        public byte[] TealBytesFromDecimal(decimal dec)
        {
            return null;
        }

        public decimal DecimalFromTealBytes(byte[] dec)
        {
            return 0.0m;
        }

        public void Fail()
        {
            Code.err();
        }



        /// <summary>
        /// Create or update a box
        /// </summary>
        /// <param name="boxName">Name of box</param>
        public void BoxSet(byte[] boxName, byte[] data)
        {
            //boxname data
            Code.dig(1);                                        //boxname data boxname
            Code.box_del();                                     //boxname data existed
            Code.pop();                                         //boxname data
            Code.box_put();                                                                



        }

        /// <summary>
        /// Delete box by name. 
        /// </summary>
        /// <param name="boxName">Name of box</param>
        /// <returns>False if box did not exist </returns>
        public void BoxDel(byte[] boxName)
        {
            Code.box_del();
            Code.pop();
        }

        /// <summary>
        /// Length of box by name, or -1 if the box did not exist.
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        /// <exception cref="IntentionallyNotImplementedException"></exception>
        public long BoxLen(byte[] boxName)
        {
            Code.box_len();
            Code.pop();
            return 0;
        }

        /// <summary>
        /// Get the box contents by name. Error if box does not exist.
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        public byte[] BoxGet(byte[] boxName)
        {
            Code.box_get();
            Code.pop();
            return new byte[] { };
        }

        /// <summary>
        /// Check if box exists
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns></returns>
        public bool BoxExists(byte[] boxName)
        {
            Code.box_get();
            Code.swap();
            Code.pop();
            return true;

        }


        public ulong CreateAsset(ulong total, uint decimals, bool defaultFrozen, byte[] unitName = null, byte[] assetName = null, byte[] url = null, byte[] metaDataHash = null, byte[] manager = null, byte[] reserve = null, byte[] freeze = null, byte[] clawback = null)
        {
            Code.itxn_begin();
            Code.int_literal_constant("acfg");
            Code.itxn_field("TypeEnum");
            
            
            if (!nulledOptionals.Any(p => p.Name == "clawback"))
            {
                Code.itxn_field("ConfigAssetClawback");
            }
            if (!nulledOptionals.Any(p => p.Name == "freeze"))
            {
                Code.itxn_field("ConfigAssetFreeze");
            }
            if (!nulledOptionals.Any(p => p.Name == "reserve"))
            {
                Code.itxn_field("ConfigAssetReserve");
            }
            if (!nulledOptionals.Any(p => p.Name == "manager"))
            {
                Code.itxn_field("ConfigAssetManager");
            }
            if (!nulledOptionals.Any(p => p.Name == "metaDataHash"))
            {
                Code.itxn_field("ConfigAssetMetadataHash");
            }
            if (!nulledOptionals.Any(p => p.Name == "url"))
            {
                Code.itxn_field("ConfigAssetURL");
            }
            if (!nulledOptionals.Any(p => p.Name == "assetName"))
            {
                Code.itxn_field("ConfigAssetName");
            }
            if (!nulledOptionals.Any(p => p.Name == "uintName"))
            {
                Code.itxn_field("ConfigAssetUnitName");
            }
           
            Code.itxn_field("ConfigAssetDefaultFrozen");
            Code.itxn_field("ConfigAssetDecimals");
            Code.itxn_field("ConfigAssetTotal");

            Code.itxn_submit();

            Code.itxn("CreatedAssetID");

            return 0;
        }

        public byte[] GetArgument(int n)
        {
            Code.args();
            return null;
        }
    }
}
