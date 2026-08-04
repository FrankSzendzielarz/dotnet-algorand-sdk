using Algorand;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using MessagePack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    /// <summary>
    /// Fast, network-free unit tests for canonical msgpack transaction encoding (Utils.Encoder.EncodeToMsgPackOrdered),
    /// guarding against two regressions found while testing online key registration transactions:
    ///
    /// 1. ParticipationPublicKeyConverterJson/VRFPublicKeyConverterJson used to pre-encode their bytes to a base64
    ///    *string* before handing them to the writer. That's fine for text JSON, but the same converters also run
    ///    under the msgpack writer bridge used for signing/submission, where it produced a msgpack string instead
    ///    of raw binary - corrupting the bytes that get signed and hashed, so algod rejected the transaction with
    ///    "signature didn't pass verification".
    ///
    /// 2. KeyRegisterOnlineTransaction.NonParticipation is `bool?`; explicitly setting it to `false` (a natural
    ///    thing for a caller to do) got serialized, because Newtonsoft's DefaultValueHandling.Ignore only omits a
    ///    *null* nullable value, not an explicit false. That's non-canonical (Algorand's canonical msgpack
    ///    encoding omits default/zero-valued fields, including false booleans), so algod's own re-derivation of
    ///    the transaction hash for signature verification produced different bytes than the client signed - same
    ///    "signature didn't pass verification" symptom.
    /// </summary>
    [TestFixture]
    public class TransactionCanonicalEncodingTests
    {
        private static KeyRegisterOnlineTransaction MakeOnlineKeyRegTransaction(bool? nonParticipation)
        {
            return new KeyRegisterOnlineTransaction()
            {
                Fee = 1000,
                Sender = new Address(new byte[32]),
                FirstValid = 1000,
                LastValid = 2000,
                GenesisHash = new Digest(new byte[32]),
                GenesisId = "test-v1.0",
                NonParticipation = nonParticipation,
                Votepk = new ParticipationPublicKey(Enumerable.Repeat((byte)0xAB, 32).ToArray()),
                SelectionPk = new VRFPublicKey(Enumerable.Repeat((byte)0xCD, 32).ToArray()),
                VoteFirst = 1000,
                VoteLast = 21000,
                VoteKeyDilution = 10000,
                StateProofPK = Enumerable.Repeat((byte)0xEF, 64).ToArray(),
            };
        }

        private static Dictionary<string, object?> DecodeCanonicalMap(byte[] bytesToSign)
        {
            // Strip the 2-byte "TX" domain-separation prefix that BytesToSign() prepends before the msgpack body.
            var body = bytesToSign.Skip(2).ToArray();
            var reader = new MessagePackReader(body);
            int count = reader.ReadMapHeader();
            var map = new Dictionary<string, object?>();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                map[key] = ReadRawValue(ref reader);
            }
            return map;
        }

        private static object? ReadRawValue(ref MessagePackReader reader)
        {
            var code = reader.NextMessagePackType;
            switch (code)
            {
                case MessagePackType.Binary:
                    {
                        var seq = reader.ReadBytes();
                        return seq.HasValue ? System.Buffers.BuffersExtensions.ToArray(seq.Value) : null;
                    }
                case MessagePackType.String:
                    return reader.ReadString();
                case MessagePackType.Boolean:
                    return reader.ReadBoolean();
                case MessagePackType.Integer:
                    return reader.ReadInt64();
                case MessagePackType.Map:
                    {
                        int n = reader.ReadMapHeader();
                        var m = new Dictionary<string, object?>();
                        for (int i = 0; i < n; i++)
                        {
                            m[reader.ReadString()] = ReadRawValue(ref reader);
                        }
                        return m;
                    }
                default:
                    reader.Skip();
                    return null;
            }
        }

        [Test]
        public void OnlineKeyReg_ParticipationKeys_AreEncodedAsRawBinaryNotBase64String()
        {
            var txn = MakeOnlineKeyRegTransaction(nonParticipation: null);
            var map = DecodeCanonicalMap(txn.BytesToSign());

            Assert.That(map["selkey"], Is.InstanceOf<byte[]>(), "selkey (SelectionPk) must be raw binary, not a base64 string");
            Assert.That((byte[])map["selkey"]!, Is.EqualTo(Enumerable.Repeat((byte)0xCD, 32).ToArray()));

            Assert.That(map["votekey"], Is.InstanceOf<byte[]>(), "votekey (Votepk) must be raw binary, not a base64 string");
            Assert.That((byte[])map["votekey"]!, Is.EqualTo(Enumerable.Repeat((byte)0xAB, 32).ToArray()));
        }

        [Test]
        public void OnlineKeyReg_NonParticipationFalse_IsOmittedFromCanonicalEncoding()
        {
            var txn = MakeOnlineKeyRegTransaction(nonParticipation: false);
            var map = DecodeCanonicalMap(txn.BytesToSign());

            Assert.That(map.ContainsKey("nonpart"), Is.False, "an explicit `false` NonParticipation must be omitted, matching Algorand's canonical zero-value omission rule");
        }

        [Test]
        public void OnlineKeyReg_NonParticipationUnset_IsOmittedFromCanonicalEncoding()
        {
            var txn = MakeOnlineKeyRegTransaction(nonParticipation: null);
            var map = DecodeCanonicalMap(txn.BytesToSign());

            Assert.That(map.ContainsKey("nonpart"), Is.False);
        }

        [Test]
        public void OnlineKeyReg_NonParticipationTrue_IsIncludedInCanonicalEncoding()
        {
            var txn = MakeOnlineKeyRegTransaction(nonParticipation: true);
            var map = DecodeCanonicalMap(txn.BytesToSign());

            Assert.That(map.ContainsKey("nonpart"), Is.True);
            Assert.That(map["nonpart"], Is.EqualTo(true));
        }

        [Test]
        public void OnlineKeyReg_SigningAndSubmissionEncodings_AreByteForByteIdentical()
        {
            // BytesToSign() and the actual on-the-wire SignedTransaction encoding must use identical bytes for
            // the embedded transaction, or the signature computed over one won't verify against the other.
            var txn = MakeOnlineKeyRegTransaction(nonParticipation: false);
            var account = new Account();
            var signed = txn.Sign(account);

            var signBytes = txn.BytesToSign().Skip(2).ToArray(); // drop "TX" prefix
            var wireBytes = Encoder.EncodeToMsgPackOrdered(signed);

            // The embedded "txn" map within the wire payload should be a byte-for-byte substring match of the
            // signed transaction body.
            var signBytesHex = Convert.ToHexString(signBytes);
            var wireBytesHex = Convert.ToHexString(wireBytes);
            Assert.That(wireBytesHex, Does.Contain(signBytesHex));
        }
    }
}
