using Algorand.Gossip;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace test.Gossip
{
    [TestFixture]
    public class GossipHttpConfigurationTests
    {
        // Captured live response for "_archive._tcp.aramidmain.biatec.io" (SRV, class IN), containing two
        // records: s3-k1-fi.a-wallet.net priority=10 weight=10 port=14160, and de-k4-s1.biatec.io
        // priority=10 weight=10 port=14161. Used to test response parsing without hitting the network.
        private static readonly byte[] CannedAramidBiatecArchivalResponse = Convert.FromHexString(
            "123481800001000200000000085f61726368697665045f7463700a6172616d69646d61696e0662696174656302696f0000210001c00c002100010000012c001d000a000a37500873332d6b312d666908612d77616c6c6574036e657400c00c002100010000012c001a000a000a37510864652d6b342d73310662696174656302696f00");

        [Test]
        public void ParseResponse_CannedSrvAnswer_DecodesBothRecords()
        {
            var records = DnsSrvResolver.ParseResponse(CannedAramidBiatecArchivalResponse, 0x1234);

            Assert.That(records.Count, Is.EqualTo(2));

            var s3 = records.Single(r => r.Target == "s3-k1-fi.a-wallet.net");
            Assert.That(s3.Port, Is.EqualTo(14160));
            Assert.That(s3.Priority, Is.EqualTo(10));
            Assert.That(s3.Weight, Is.EqualTo(10));

            var de = records.Single(r => r.Target == "de-k4-s1.biatec.io");
            Assert.That(de.Port, Is.EqualTo(14161));
            Assert.That(de.Priority, Is.EqualTo(10));
            Assert.That(de.Weight, Is.EqualTo(10));
        }

        [Test]
        public void ParseResponse_TransactionIdMismatch_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => DnsSrvResolver.ParseResponse(CannedAramidBiatecArchivalResponse, 0xFFFF));
        }

        [Test]
        public void ParseResponse_TooShort_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => DnsSrvResolver.ParseResponse(new byte[] { 1, 2, 3 }));
        }

        [Test]
        public void EncodeName_ProducesLengthPrefixedLabelsTerminatedByZero()
        {
            var encoded = DnsSrvResolver.EncodeName("_archive._tcp.mainnet.algorand.network");

            // 1 (len) + 8 ("_archive") + 1 (len) + 4 ("_tcp") + 1 (len) + 7 ("mainnet")
            // + 1 (len) + 8 ("algorand") + 1 (len) + 7 ("network") + 1 (terminator)
            Assert.That(encoded.Length, Is.EqualTo(1 + 8 + 1 + 4 + 1 + 7 + 1 + 8 + 1 + 7 + 1));
            Assert.That(encoded[0], Is.EqualTo(8));
            Assert.That(encoded[encoded.Length - 1], Is.EqualTo(0));
        }

        [Test]
        public void BuildQuery_SetsSrvQuestionAndEdnsAdditionalRecord()
        {
            var query = DnsSrvResolver.BuildQuery("_archive._tcp.mainnet.algorand.network", out ushort transactionId);

            // QDCOUNT (bytes 4-5) must be 1, AN/NSCOUNT (bytes 6-9) must be 0, ARCOUNT (bytes 10-11) must be 1
            // (the EDNS0 OPT pseudo-record, needed so large SRV answer sets aren't truncated over plain UDP).
            Assert.That(query[4], Is.EqualTo(0));
            Assert.That(query[5], Is.EqualTo(1));
            for (int i = 6; i < 10; i++)
            {
                Assert.That(query[i], Is.EqualTo(0));
            }
            Assert.That(query[10], Is.EqualTo(0));
            Assert.That(query[11], Is.EqualTo(1));

            // QTYPE/QCLASS are the 4 bytes right after the terminating 0x00 label byte of the question name,
            // and must be SRV(33)/IN(1).
            int zeroIndex = Array.IndexOf(query, (byte)0, 12);
            Assert.That(query[zeroIndex + 1], Is.EqualTo(0));
            Assert.That(query[zeroIndex + 2], Is.EqualTo(33));
            Assert.That(query[zeroIndex + 3], Is.EqualTo(0));
            Assert.That(query[zeroIndex + 4], Is.EqualTo(1));

            // The EDNS0 OPT record (root name 0x00, TYPE=41) follows immediately after QTYPE/QCLASS.
            int optIndex = zeroIndex + 5;
            Assert.That(query[optIndex], Is.EqualTo(0));
            Assert.That((query[optIndex + 1] << 8) | query[optIndex + 2], Is.EqualTo(41));

            // ID (first 2 bytes) round-trips the out parameter
            Assert.That((ushort)((query[0] << 8) | query[1]), Is.EqualTo(transactionId));
        }

        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AlgorandMainNet, "_archive._tcp.mainnet.algorand.network")]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AlgorandMainNet, "_algobootstrap._tcp.mainnet.algorand.network")]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AlgorandTestNet, "_archive._tcp.testnet.algorand.network")]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AlgorandTestNet, "_algobootstrap._tcp.testnet.algorand.network")]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.VoiMainNet, "_archive._tcp.voimain.mainnet-voi.network")]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.VoiMainNet, "_algobootstrap._tcp.voimain.mainnet-voi.network")]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AramidMainNetBiatec, "_archive._tcp.aramidmain.biatec.io")]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AramidMainNetBiatec, "_algobootstrap._tcp.aramidmain.biatec.io")]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AramidMainNetAWallet, "_archive._tcp.aramidmain.a-wallet.net")]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AramidMainNetAWallet, "_algobootstrap._tcp.aramidmain.a-wallet.net")]
        public void GetSrvServiceName_MapsPurposeAndNetworkToExpectedServiceName(GossipNodePurpose purpose, GossipNetwork network, string expected)
        {
            Assert.That(GossipHttpConfiguration.GetSrvServiceName(purpose, network), Is.EqualTo(expected));
        }

        [TestCase(GossipNetwork.AlgorandMainNet, "mainnet-v1.0")]
        [TestCase(GossipNetwork.AlgorandTestNet, "testnet-v1.0")]
        [TestCase(GossipNetwork.VoiMainNet, "voimain-v1.0")]
        [TestCase(GossipNetwork.AramidMainNetBiatec, "aramidmain-v1.0")]
        [TestCase(GossipNetwork.AramidMainNetAWallet, "aramidmain-v1.0")]
        public void GetGenesisId_MapsNetworkToExpectedGenesisId(GossipNetwork network, string expected)
        {
            Assert.That(GossipHttpConfiguration.GetGenesisId(network), Is.EqualTo(expected));
        }

        [Test]
        public void MainNetArchival_SnapshotIsNonEmptyAndWellFormed()
        {
            Assert.That(GossipHttpConfiguration.MainNetArchival.Hosts, Is.Not.Empty);
            Assert.That(GossipHttpConfiguration.MainNetArchival.GenesisId, Is.EqualTo("mainnet-v1.0"));
            foreach (var host in GossipHttpConfiguration.MainNetArchival.Hosts)
            {
                Assert.That(host, Does.StartWith("http://"));
                Assert.That(host, Does.Contain("algorand-mainnet.network:4160"));
            }
        }

        // The following hit live DNS SRV records and are only as stable as the underlying networks' published
        // records (mirrors test.Gossip.BlockFetcherTests, which hits MainNet gossip nodes directly).
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AlgorandMainNet)]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AlgorandMainNet)]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AlgorandTestNet)]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AlgorandTestNet)]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.VoiMainNet)]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.VoiMainNet)]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AramidMainNetBiatec)]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AramidMainNetBiatec)]
        [TestCase(GossipNodePurpose.Archival, GossipNetwork.AramidMainNetAWallet)]
        [TestCase(GossipNodePurpose.Relay, GossipNetwork.AramidMainNetAWallet)]
        public void Constructor_ResolvesAtLeastOneHostFromLiveSrvRecords(GossipNodePurpose purpose, GossipNetwork network)
        {
            var config = new GossipHttpConfiguration(purpose, network);

            Assert.That(config.Hosts, Is.Not.Empty);
            Assert.That(config.GenesisId, Is.EqualTo(GossipHttpConfiguration.GetGenesisId(network)));
            foreach (var host in config.Hosts)
            {
                Assert.That(host, Does.StartWith("http://"));
                Assert.That(Uri.TryCreate(host, UriKind.Absolute, out _), Is.True, $"'{host}' should be a valid URI");
            }
        }

        [Test]
        public async Task Constructor_AlgorandMainNetArchival_CanFetchBlockFromResolvedHosts()
        {
            var config = new GossipHttpConfiguration(GossipNodePurpose.Archival, GossipNetwork.AlgorandMainNet);
            var gossipClient = new GossipHttpClient(config);

            var block = await gossipClient.FetchBlockAsync(1);
            Assert.That(block, Is.Not.Null);
            Assert.That(block.Block.Round, Is.EqualTo(1UL));
        }
    }
}
