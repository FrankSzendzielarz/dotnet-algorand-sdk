using System;
using System.Linq;

namespace Algorand.Gossip
{
    /// <summary>
    /// The role a gossip node plays: an archival node keeps full block history, a relay node only participates
    /// in current-round consensus gossip. Each is published under its own DNS SRV service name.
    /// </summary>
    public enum GossipNodePurpose
    {
        Archival,
        Relay
    }

    /// <summary>
    /// The Algorand-protocol network to discover gossip nodes for.
    /// </summary>
    public enum GossipNetwork
    {
        AlgorandMainNet,
        VoiMainNet,
        AramidMainNetBiatec,
        AramidMainNetAWallet,
        // Appended rather than inserted alongside AlgorandMainNet so existing members keep their
        // underlying int values for anyone persisting/comparing this enum numerically.
        AlgorandTestNet
    }

    public class GossipHttpConfiguration
    {
        /// <summary>
        /// A snapshot of Algorand MainNet archival nodes, refreshed from the
        /// "_archive._tcp.mainnet.algorand.network" SRV record. These hostnames rotate over time, so prefer
        /// constructing a fresh <see cref="GossipHttpConfiguration(GossipNodePurpose, GossipNetwork, string)"/>
        /// via DNS SRV lookup over relying on this fixed list where possible.
        /// </summary>
        public static GossipHttpConfiguration MainNetArchival = new GossipHttpConfiguration()
        {
            Hosts = new string[]{
                "http://a-ae.algorand-mainnet.network:4160",
                "http://a-e8.algorand-mainnet.network:4160",
                "http://a-il.algorand-mainnet.network:4160",
                "http://a-rc.algorand-mainnet.network:4160",
                "http://a-sa.algorand-mainnet.network:4160",
                "http://a-sd.algorand-mainnet.network:4160",
                "http://a-w7.algorand-mainnet.network:4160",
                "http://a-xb.algorand-mainnet.network:4160",
                "http://a-yw.algorand-mainnet.network:4160",
                "http://a-11.algorand-mainnet.network:4160",
                "http://a-64.algorand-mainnet.network:4160",
                "http://a-8v.algorand-mainnet.network:4160",
                "http://a-8z.algorand-mainnet.network:4160"
            },
            GenesisId = "mainnet-v1.0"
        };

        public string GenesisId { get; set; } = "mainnet-v1.0";

        public string[] Hosts { get; set; } = new string[]{
            "http://a-ae.algorand-mainnet.network:4160",
        };

        public GossipHttpConfiguration()
        {
        }

        /// <summary>
        /// Builds a configuration by resolving the DNS SRV record that publishes the given network's gossip
        /// nodes for the given purpose (archival or relay), e.g. purpose=Archival, network=VoiMainNet resolves
        /// "_archive._tcp.voimain.mainnet-voi.network". This is the recommended way to get an up-to-date host
        /// list instead of a hardcoded one, since these rotate over time.
        /// </summary>
        /// <param name="purpose">Whether to discover archival or relay nodes.</param>
        /// <param name="network">Which network's nodes to discover.</param>
        /// <param name="scheme">URL scheme to prefix each resolved host with.</param>
        public GossipHttpConfiguration(GossipNodePurpose purpose, GossipNetwork network, string scheme = "http")
        {
            var serviceName = GetSrvServiceName(purpose, network);
            var records = DnsSrvResolver.Resolve(serviceName);
            if (records.Count == 0)
                throw new InvalidOperationException($"No SRV records found for '{serviceName}'.");

            Hosts = records.Select(r => $"{scheme}://{r.Target.TrimEnd('.')}:{r.Port}").ToArray();
            GenesisId = GetGenesisId(network);
        }

        /// <summary>
        /// The DNS SRV service name that publishes gossip nodes of the given purpose for the given network.
        /// </summary>
        public static string GetSrvServiceName(GossipNodePurpose purpose, GossipNetwork network)
        {
            string service = purpose == GossipNodePurpose.Archival ? "_archive._tcp" : "_algobootstrap._tcp";
            string domain;
            switch (network)
            {
                case GossipNetwork.AlgorandMainNet:
                    domain = "mainnet.algorand.network";
                    break;
                case GossipNetwork.AlgorandTestNet:
                    domain = "testnet.algorand.network";
                    break;
                case GossipNetwork.VoiMainNet:
                    domain = "voimain.mainnet-voi.network";
                    break;
                case GossipNetwork.AramidMainNetBiatec:
                    domain = "aramidmain.biatec.io";
                    break;
                case GossipNetwork.AramidMainNetAWallet:
                    domain = "aramidmain.a-wallet.net";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(network), network, "Unknown gossip network.");
            }
            return $"{service}.{domain}";
        }

        /// <summary>
        /// The block-gossip genesis ID (network name + genesis version) used in the gossip HTTP path, e.g.
        /// "mainnet-v1.0".
        /// </summary>
        public static string GetGenesisId(GossipNetwork network)
        {
            switch (network)
            {
                case GossipNetwork.AlgorandMainNet:
                    return "mainnet-v1.0";
                case GossipNetwork.AlgorandTestNet:
                    return "testnet-v1.0";
                case GossipNetwork.VoiMainNet:
                    return "voimain-v1.0";
                case GossipNetwork.AramidMainNetBiatec:
                case GossipNetwork.AramidMainNetAWallet:
                    return "aramidmain-v1.0";
                default:
                    throw new ArgumentOutOfRangeException(nameof(network), network, "Unknown gossip network.");
            }
        }
    }
}
