


namespace Algorand.Algod.Model
{
    using Algorand.Crypto;
    using Algorand.Utils;
    using Algorand.Utils.Crypto;
    using MessagePack;
    using Newtonsoft.Json;
    using Org.BouncyCastle.Crypto.Signers;
    using Org.BouncyCastle.Security;
    using System;
    using System.Collections.Generic;
    using System.Text;
#if UNITY
    using UnityEngine;
#endif

    using System = global::System;
    /// <summary>Account information at a given round.
    /// <br/>
    /// <br/>Definition:
    /// <br/>data/basics/userBalance.go : AccountData
    /// <br/></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.5.2.0 (Newtonsoft.Json v12.0.0.0)")]
    [MessagePack.MessagePackObject]
    public partial class Account : IDisposable
    {


        private static readonly byte[] BYTES_SIGN_PREFIX = Encoding.UTF8.GetBytes("MX");
        private static readonly byte[] PROGDATA_SIGN_PREFIX = Encoding.UTF8.GetBytes("ProgData");
        [JsonIgnore]
        [IgnoreMember]
        public KeyPair KeyPair { get; private set; }





        //TODO - omit this from the .generated codegen

        /// <summary>Indicates what type of signature is used by this account, must be one of:
        /// <br/>* sig
        /// <br/>* msig
        /// <br/>* lsig</summary>
        [Newtonsoft.Json.JsonProperty("sig-type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("sig-type")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
#if UNITY
        [field: SerializeField]
        [Tooltip(@"Indicates what type of signature is used by this account, must be one of:
* sig
* msig
* lsig")]
        [field: InspectorName(@"SigType")]
#endif
        public AccountSigType? SigType { get; set; }




        /// <summary>
        /// Create an account from a private key
        /// </summary>
        /// <param name="privateKey">Private Key</param>
        /// <returns>the account</returns>
        public Account(KeyPair keyPair)
        {
            KeyPair = keyPair;
            Address = new Address(KeyPair.ClearTextPublicKey);
        }

        /// <summary>
        /// Generate a new, random account.
        /// </summary>
        public Account() : this(new SecureRandom())
        {

        }

        /// <summary>
        /// Generate a newc account with seed(master derivation key)
        /// </summary>
        /// <param name="seed">seed(master derivation key)</param>
        public Account(byte[] seed) : this(new FixedSecureRandom(seed))
        {

        }


        /// <summary>
        /// Create a new account with mnemonic
        /// </summary>
        /// <param name="mnemonic">the mnemonic</param>
        public Account(string mnemonic) : this(Mnemonic.ToKey(mnemonic)) { }

        private Account(SecureRandom srandom)
        {
            KeyPair = new KeyPair(srandom);
            Address = new Address(KeyPair.ClearTextPublicKey);
        }

        public string ToMnemonic()
        {
            return KeyPair.ToMnemonic();
        }


        public Signature SignRawBytes(byte[] bytes)
        {
            var signer = new Ed25519Signer();
            signer.Init(true, KeyPair.Pair.Private);
            signer.BlockUpdate(bytes, 0, bytes.Length);
            byte[] signature = signer.GenerateSignature();
            return new Signature(signature);
        }

        public Signature SignBytes(byte[] bytes)
        {
            List<byte> retByte = new List<byte>();
            retByte.AddRange(BYTES_SIGN_PREFIX);
            retByte.AddRange(bytes);
            return SignRawBytes(retByte.ToArray());
        }

        /// <summary>
        /// Creates Signature compatible with ed25519verify TEAL opcode from data and contract address(program hash).
        /// </summary>
        /// <param name="data">data byte[]</param>

        /// <returns>Signature</returns>
        public Signature TealSign(byte[] data, Address contractAddress)
        {
            byte[] rawAddress = contractAddress.Bytes;
            List<byte> baos = new List<byte>();
            baos.AddRange(PROGDATA_SIGN_PREFIX);
            baos.AddRange(rawAddress);
            baos.AddRange(data);
            return SignRawBytes(baos.ToArray());
        }

        public override string ToString()
        {
            var str = this.Address.EncodeAsString();
            if (string.IsNullOrEmpty(str))
            {
                return "?Address?";
            }
            return $"{str.Substring(0, 4)}..{str.Substring(str.Length-4)}";
        }

        /// <summary>
        /// Best-effort wipe of this account's private key material. See
        /// <see cref="KeyPair.Dispose"/> for what this can and cannot guarantee.
        /// </summary>
        public void Dispose()
        {
            KeyPair?.Dispose();
        }
    }
}
