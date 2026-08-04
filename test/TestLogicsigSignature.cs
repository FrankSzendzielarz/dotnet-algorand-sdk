using Algorand;
using Algorand.Algod.Model;
using NUnit.Framework;
using Org.BouncyCastle.Crypto.Parameters;
using System.Collections.Generic;
using Encoder = Algorand.Utils.Encoder;

namespace test
{
    [TestFixture]
    class TestLogicsigSignature
    {


        [Test]
        public void testLogicsigCreation()
        {
            byte[] program = {
                0x01, 0x20, 0x01, 0x01, 0x22  // int 1
            };
            List<byte[]> args = new List<byte[]>();
            string programHash = "6Z3C3LDVWGMX23BMSYMANACQOSINPFIRF77H7N3AWJZYV6OH6GWTJKVMXY";
            Address sender = new Address(programHash);

            LogicsigSignature lsig = new LogicsigSignature(program);
            Assert.That(lsig.Logic, Is.EqualTo(program));
            Assert.That(lsig.Args, Is.Null);
            Assert.That(lsig.Sig, Is.Null);
            Assert.That(lsig.Msig, Is.Null);
            bool verified = lsig.Verify(sender);
            Assert.That(verified, Is.True);
            Assert.That(lsig.ToAddress(),Is.EqualTo(sender));

            byte[] arg1 = { 1, 2, 3 };
            byte[] arg2 = { 4, 5, 6 };
            args.Add(arg1);
            args.Add(arg2);

            lsig = new LogicsigSignature(program, args);
            Assert.That(lsig.Logic, Is.EqualTo(program));
            Assert.That(lsig.Args, Is.EqualTo(args));
            Assert.That(lsig.Sig, Is.Null);
            Assert.That(lsig.Msig, Is.Null);
            verified = lsig.Verify(sender);
            Assert.That(verified, Is.True);
            Assert.That(lsig.ToAddress(), Is.EqualTo(sender));

            // check serialization
            byte[] outBytes = Encoder.EncodeToMsgPackOrdered(lsig);
            LogicsigSignature lsig1 = Encoder.DecodeFromMsgPack<LogicsigSignature>(outBytes);
            Assert.That(lsig, Is.EqualTo(lsig1));

            // check serialization with null args
            lsig = new LogicsigSignature(program);
            outBytes = Encoder.EncodeToMsgPackOrdered(lsig);
            lsig1 = Encoder.DecodeFromMsgPack<LogicsigSignature>(outBytes);
            Assert.That(lsig, Is.EqualTo(lsig1));

            // check modified program fails on verification
            program[3] = 2;
            lsig = new LogicsigSignature(program);
            verified = lsig.Verify(sender);
            Assert.That(verified, Is.False);
            TestUtil.SerializeDeserializeCheck(lsig);
        }

        [Test]
        public void testLogicsigSignature()
        {
            byte[] program = {
                0x01, 0x20, 0x01, 0x01, 0x22  // int 1
            };

            LogicsigSignature lsig = new LogicsigSignature(program);
            Account account = new Account();
            lsig.SignLogicsig(account);
            Assert.That(lsig.Logic, Is.EqualTo(program));
            Assert.That(lsig.Args, Is.Null);
            Assert.That(lsig.Sig, Is.Not.EqualTo(new Signature()));
            Assert.That(lsig.Sig, Is.Not.Null);

            Assert.That(lsig.Msig, Is.Null);
            bool verified = lsig.Verify(account.Address);
            Assert.That(verified, Is.True);

            // check serialization
            byte[] outBytes = Encoder.EncodeToMsgPackOrdered(lsig);
            LogicsigSignature lsig1 = Encoder.DecodeFromMsgPack<LogicsigSignature>(outBytes);
            Assert.That(lsig1, Is.EqualTo(lsig));
            TestUtil.SerializeDeserializeCheck(lsig);
        }

        [Test]
        public void testLogicsigMultisigSignature()
        {
            byte[] program = { 0x01, 0x20, 0x01, 0x01, 0x22   /*int 1*/            };

            Address one = new Address("DN7MBMCL5JQ3PFUQS7TMX5AH4EEKOBJVDUF4TCV6WERATKFLQF4MQUPZTA");
            Address two = new Address("BFRTECKTOOE7A5LHCF3TTEOH2A7BW46IYT2SX5VP6ANKEXHZYJY77SJTVM");
            Address three = new Address("47YPQTIGQEO7T4Y4RWDYWEKV6RTR2UNBQXBABEEGM72ESWDQNCQ52OPASU");
            MultisigAddress ma = new MultisigAddress(1, 2, new List<Ed25519PublicKeyParameters>
            {
                new Ed25519PublicKeyParameters(one.Bytes, 0),
                new Ed25519PublicKeyParameters(two.Bytes, 0),
                new Ed25519PublicKeyParameters(three.Bytes, 0),
            });

            string mn1 = "auction inquiry lava second expand liberty glass involve ginger illness length room item discover ahead table doctor term tackle cement bonus profit right above catch";
            string mn2 = "since during average anxiety protect cherry club long lawsuit loan expand embark forum theory winter park twenty ball kangaroo cram burst board host ability left";
            Account acc1 = new Account(mn1);
            Account acc2 = new Account(mn2);
            Account account = new Account();

            LogicsigSignature lsig = new LogicsigSignature(program);
            lsig.SignLogicsig(acc1, ma);
            Assert.That(lsig.Logic, Is.EqualTo(program));
            Assert.That(lsig.Args, Is.Null);
            Assert.That(lsig.Sig, Is.Null);
            Assert.That(lsig.Msig, Is.Not.EqualTo(new MultisigSignature()));
            Assert.That(lsig.Msig, Is.Not.Null);

            var verified = lsig.Verify(ma.ToAddress());
            Assert.That(verified, Is.False);

            LogicsigSignature lsigLambda = lsig;
            
            lsig.AppendToLogicsig(lsig, acc2);
            verified = lsig.Verify(ma.ToAddress());
            Assert.That(verified, Is.True);

            // Add a single signature and ensure it fails
            LogicsigSignature lsig1 = new LogicsigSignature(program);
            lsig1.SignLogicsig( account);
            lsig.Sig = lsig1.Sig;
            verified = lsig.Verify(ma.ToAddress());
            Assert.That(verified, Is.False);
            verified = lsig.Verify(account.Address);
            Assert.That(verified, Is.False);

            // Remove and ensure it still works
            lsig.Sig = null;
            verified = lsig.Verify(ma.ToAddress());
            Assert.That(verified, Is.True);

            // check serialization
            byte[] outBytes = Encoder.EncodeToMsgPackOrdered(lsig);
            LogicsigSignature lsig2 = Encoder.DecodeFromMsgPack<LogicsigSignature>(outBytes);
            Assert.That(lsig2, Is.EqualTo(lsig));
            verified = lsig2.Verify(ma.ToAddress());
            Assert.That(verified, Is.True);
            TestUtil.SerializeDeserializeCheck(lsig2);
        }
    }
}
