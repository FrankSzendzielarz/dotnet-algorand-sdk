using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;
using Algorand.Utils;
using Algorand.Algod.Model;
using MessagePack;


namespace Algorand
{
    /// <summary>
    /// A raw serializable signature class.
    /// </summary>
    [JsonConverter(typeof(BytesConverter))]
    [MessagePack.MessagePackObject]
    public class Signature
    {
        private static int ED25519_SIG_SIZE = 64;

        [MessagePack.Key(0)]
        public byte[] Bytes { get; private set; }

        /// <summary>
        /// Create a new Signature wrapping the given bytes.
        /// </summary>
        /// <param name="rawBytes">bytes</param>
        [JsonConstructor]
        public Signature(byte[] rawBytes)
        {
            if (rawBytes == null)
            {
                Bytes = new byte[ED25519_SIG_SIZE];
                return;
            }
            if (rawBytes.Length != ED25519_SIG_SIZE)
            {
                throw new ArgumentException(string.Format("Given signature length is not {0}", ED25519_SIG_SIZE));
            }
            this.Bytes = rawBytes;
        }

        /// <summary>
        /// default values for serializer to ignore
        /// </summary>
        public Signature()
        {
            Bytes = new byte[ED25519_SIG_SIZE];
        }



        public override bool Equals(object? obj)
        {
            if (obj is Signature && Enumerable.SequenceEqual(this.Bytes, (obj as Signature).Bytes))
                return true;
            else return false;
        }
        public override int GetHashCode()
        {
            return Bytes.GetHashCode();
        }
    }
    /// <summary>
    /// Serializable logicsig class. 
    /// LogicsigSignature is constructed from a program and optional arguments. 
    /// Signature sig and MultisigSignature msig property are available for modification by it's clients.
    /// </summary>
    [JsonObject]
    [MessagePack.MessagePackObject]
    public class LogicsigSignature
    {
        [JsonIgnore]
        [IgnoreMember]
        private static byte[] LOGIC_PREFIX = Encoding.UTF8.GetBytes("Program");//.getBytes(StandardCharsets.UTF_8);

        [JsonProperty(PropertyName = "l")]
        [MessagePack.Key("l")]
        public byte[] Logic { get; set; }

        [JsonProperty(PropertyName = "arg")]
        [MessagePack.Key("arg")]
        public List<byte[]> Args { get; set; }

        public bool ShouldSerializeArgs() => Args?.Count > 0;

        [JsonProperty(PropertyName = "sig")]
        [MessagePack.Key("sig")]
        public Signature Sig { get; set; }

        [JsonProperty(PropertyName = "msig")]
        [MessagePack.Key("msig")]
        public MultisigSignature Msig { get; set; }

        /// <summary>
        /// LogicsigSignature
        /// </summary>
        /// <param name="logic">Unsigned logicsig object</param>
        /// <param name="args">Unsigned logicsig object's arguments</param>
        /// <param name="sig"></param>
        /// <param name="msig"></param>
        [JsonConstructor]
        public LogicsigSignature(
            [JsonProperty("l")] byte[] logic,
            [JsonProperty("arg")] List<byte[]> args = null,
            [JsonProperty("sig")] byte[] sig = null,
            [JsonProperty("msig")] MultisigSignature msig = null)
        {
            this.Logic = JavaHelper<byte[]>.RequireNotNull(logic, "program must not be null");
            this.Args = args;

            //if (!Algorand.Utils.Logic.CheckProgram(this.Logic, this.Args))
            //    throw new Exception("program verified failed!");

            if (sig != null)
            {
                this.Sig = new Signature(sig);
            }
            this.Msig = msig;
        }
        /// <summary>
        /// Uninitialized object used for serializer to ignore default values.
        /// </summary>
        public LogicsigSignature()
        {
            this.Logic = null;
            this.Args = null;
        }
        /// <summary>
        /// alculate escrow address from logic sig program
        /// DEPRECATED
        /// Please use Address property.
        /// The address of the LogicsigSignature
        /// </summary>
        /// <returns>The address of the LogicsigSignature</returns>
        public Address ToAddress()
        {
            return Address;
        }
        /// <summary>
        /// The address of the LogicsigSignature
        /// </summary>
        [JsonIgnore]
        [IgnoreMember]
        public Address Address
        {
            get
            {
                return new Address(Digester.Digest(BytesToSign()));
            }
        }
        /// <summary>
        /// Return prefixed program as byte array that is ready to sign
        /// </summary>
        /// <returns>byte array</returns>
        public byte[] BytesToSign()
        {
            List<byte> prefixedEncoded = new List<byte>(LOGIC_PREFIX);
            prefixedEncoded.AddRange(this.Logic);
            return prefixedEncoded.ToArray();
        }
        /// <summary>
        /// Perform signature verification against the sender address
        /// </summary>
        /// <param name="address">Address to verify</param>
        /// <returns>bool</returns>
        public bool Verify(Address address)
        {
            if (this.Logic == null)
            {
                return false;
            }
            else if (this.Sig != null && this.Msig != null)
            {
                return false;
            }
            else
            {
                try
                {
                    Algorand.Utils.Logic.CheckProgram(this.Logic, this.Args);
                }
                catch (Exception)
                {
                    return false;
                }

                if (this.Sig == null && this.Msig == null)
                {
                    try
                    {
                        return address.Equals(this.ToAddress());
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                if (this.Sig != null)
                {
                    try
                    {
                        var pk = new Ed25519PublicKeyParameters(address.Bytes, 0);
                        var signer = new Ed25519Signer();
                        signer.Init(false, pk); //false代表用于VerifySignature
                        signer.BlockUpdate(this.BytesToSign(), 0, this.BytesToSign().Length);
                        return signer.VerifySignature(this.Sig.Bytes);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return this.Msig.Verify(this.BytesToSign());
                }

            }
        }

        /// <summary>
        /// Sign LogicSig with account's secret key
        /// </summary>
        /// <param name="lsig">LogicsigSignature to sign</param>
        /// <returns>LogicsigSignature with updated signature</returns>
        public void Sign(Account signingAccount)
        {
            byte[] bytesToSign = BytesToSign();
            this.Sig = signingAccount.SignRawBytes(bytesToSign);


        }

        /// <summary>
        /// Sign LogicSig with account's secret key
        /// </summary>
        /// <param name="lsig">LogicsigSignature to sign</param>
        /// <returns>LogicsigSignature with updated signature</returns>
        public void SignLogicsig(Account signingAccount)
        {
            byte[] bytesToSign = BytesToSign();
            Signature sig = signingAccount.SignRawBytes(bytesToSign);
            Sig = sig;

        }

        public void SignLogicsig(Account signingAccount, MultisigAddress ma)
        {
            var pk = signingAccount.KeyPair.PublicKey;
            int pkIndex = -1;
            for (int i = 0; i < ma.publicKeys.Count; i++)
            {
                if (Enumerable.SequenceEqual(pk.GetEncoded(), ma.publicKeys[i].GetEncoded()))
                {
                    pkIndex = i;
                    break;
                }
            }

            if (pkIndex == -1)
            {
                throw new ArgumentException("Multisig account does not contain this secret key");
            }
            // now, create the multisignature
            byte[] bytesToSign = BytesToSign();
            Signature sig = signingAccount.SignRawBytes(bytesToSign);
            MultisigSignature mSig = new MultisigSignature(ma.version, ma.threshold);
            for (int i = 0; i < ma.publicKeys.Count; i++)
            {
                if (i == pkIndex)
                {
                    mSig.Subsigs.Add(new MultisigSubsig(pk, sig));
                }
                else
                {
                    mSig.Subsigs.Add(new MultisigSubsig(ma.publicKeys[i]));
                }
            }
            Msig = mSig;

        }

        /// <summary>
        /// Appends a signature to multisig logic signed transaction
        /// </summary>
        /// <param name="lsig">LogicsigSignature append to</param>
        /// <returns>LogicsigSignature</returns>
        public void AppendToLogicsig(LogicsigSignature lsig, Account signingAccount)
        {
            var pk = signingAccount.KeyPair.PublicKey;
            int pkIndex = -1;
            for (int i = 0; i < lsig.Msig.Subsigs.Count; i++)
            {
                MultisigSubsig subsig = lsig.Msig.Subsigs[i];
                if (Enumerable.SequenceEqual(subsig.key.GetEncoded(), pk.GetEncoded()))
                {
                    pkIndex = i;
                }
            }
            if (pkIndex == -1)
            {
                throw new ArgumentException("Multisig account does not contain this secret key");
            }
            // now, create the multisignature
            byte[] bytesToSign = lsig.BytesToSign();
            Signature sig = signingAccount.SignRawBytes(bytesToSign);
            lsig.Msig.Subsigs[pkIndex] = new MultisigSubsig(pk, sig);

        }




        private static bool NullCheck(object o1, object o2)
        {
            if (o1 == null && o2 == null)
            {
                return true;
            }
            else if (o1 == null && o2 != null)
            {
                return false;
            }
            else
            {
                return o1 == null || o2 != null;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is LogicsigSignature actual)
                if ((this.Logic is null && actual.Logic is null) || (!(this.Logic is null || actual.Logic is null) && Enumerable.SequenceEqual(this.Logic, actual.Logic)))
                    if ((this.Sig is null && actual.Sig is null) || this.Sig.Equals(actual.Sig))
                        if ((this.Msig is null && actual.Msig is null) || this.Msig.Equals(actual.Msig))
                            if ((this.Args is null && actual.Args is null) || ArgsEqual(this.Args, actual.Args))
                                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return this.Logic.GetHashCode() + this.Args.GetHashCode() + this.Sig.GetHashCode() + this.Msig.GetHashCode();
        }

        private static bool ArgsEqual(List<byte[]> args1, List<byte[]> args2)
        {
            bool flag = true;
            if (args1.Count == args2.Count)
            {
                for (int i = 0; i < args1.Count; i++)
                {
                    if (!Enumerable.SequenceEqual(args1[i], args2[i]))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            else
                flag = false;
            return flag;
        }
    }
    /// <summary>
    /// Serializable raw multisig class.
    /// </summary>
    [JsonObject]
    [MessagePack.MessagePackObject]
    public class MultisigSignature
    {
        [JsonProperty(PropertyName = "v")]
        [MessagePack.Key("v")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "thr")]
        [MessagePack.Key("thr")]
        public int Threshold { get; set; }

        [JsonProperty(PropertyName = "subsig")]
        [MessagePack.Key("subsig")]
        public List<MultisigSubsig> Subsigs { get; set; }

        public bool ShouldSerializeSubsigs() => Subsigs?.Count > 0;

        /// <summary>
        /// create a multisig signature.
        /// </summary>
        /// <param name="version">required</param>
        /// <param name="threshold">required</param>
        /// <param name="subsigs">can be empty or null</param>
        [JsonConstructor]
        public MultisigSignature(
            [JsonProperty(PropertyName = "v")] int version,
            [JsonProperty(PropertyName = "thr")] int threshold,
            [JsonProperty(PropertyName = "subsig")] List<MultisigSubsig> subsigs = null)
        {
            this.Version = version;
            this.Threshold = threshold;
            if (subsigs is null)
                this.Subsigs = new List<MultisigSubsig>();
            else
                this.Subsigs = subsigs;
        }

        public MultisigSignature()
        {
            this.Subsigs = new List<MultisigSubsig>();
        }
        /// <summary>
        /// Performs signature verification
        /// </summary>
        /// <param name="message">raw message to verify</param>
        /// <returns>bool</returns>
        public bool Verify(byte[] message)
        {
            if (this.Version == 1 && this.Threshold > 0 && this.Subsigs.Count != 0)
            {
                if (this.Threshold > this.Subsigs.Count)
                {
                    return false;
                }
                else
                {
                    int verifiedCount = 0;
                    Signature emptySig = new Signature();

                    for (int i = 0; i < this.Subsigs.Count; ++i)
                    {
                        MultisigSubsig subsig = Subsigs[i];
                        if (!subsig.sig.Equals(emptySig))
                        {
                            try
                            {
                                var pk = subsig.key;
                                var signer = new Ed25519Signer();
                                signer.Init(false, pk); //for verify
                                signer.BlockUpdate(message, 0, message.Length);
                                bool verified = signer.VerifySignature(subsig.sig.Bytes);
                                if (verified)
                                {
                                    ++verifiedCount;
                                }
                            }
                            catch (Exception var9)
                            {
                                throw new ArgumentException("verification of subsig " + i + "failed", var9);
                            }
                        }
                    }

                    if (verifiedCount < this.Threshold)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is MultisigSignature actual)
            {
                if (this.Version == actual.Version && this.Threshold == actual.Threshold && Enumerable.SequenceEqual(this.Subsigs, actual.Subsigs))
                    return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Version.GetHashCode() + this.Threshold.GetHashCode() + this.Subsigs.GetHashCode();
        }
    }
    /// <summary>
    /// Serializable multisig sub-signature
    /// </summary>
    [JsonObject]
    [MessagePack.MessagePackObject]
    public class MultisigSubsig
    {
        [JsonProperty(PropertyName = "pk")]
        [JsonConverter(typeof(BytesConverter))]
        [MessagePack.Key("pk")]
        public Ed25519PublicKeyParameters key { get; set; }

        [JsonProperty(PropertyName = "s")]
        [MessagePack.Key("s")]
        public Signature sig { get; set; }


        public bool ShouldSerializesig() => sig.Bytes.Any(b => b != 0);
        
        /// <summary>
        /// workaround wrapped json values
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sig"></param>
        [JsonConstructor]
        public MultisigSubsig([JsonProperty("pk")] byte[] key = null, [JsonProperty("s")] byte[] sig = null)
        {
            if (key != null)
                this.key = new Ed25519PublicKeyParameters(key, 0);
            else
                this.key = new Ed25519PublicKeyParameters(new byte[0], 0);

            if (sig != null)
                this.sig = new Signature(sig);
            else
                this.sig = new Signature();
        }

        public MultisigSubsig(Ed25519PublicKeyParameters key, Signature sig = null)
        {
            this.key = JavaHelper<Ed25519PublicKeyParameters>.RequireNotNull(key, "public key cannot be null");
            if (sig is null)
                this.sig = new Signature();
            else
                this.sig = sig;
        }

        public override bool Equals(object? obj)
        {
            if ((obj is MultisigSubsig actual))
            {
                return Enumerable.SequenceEqual(this.key.GetEncoded(), actual.key.GetEncoded()) && this.sig.Equals(actual.sig);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return key.GetHashCode() + sig.GetHashCode();
        }
    }
}