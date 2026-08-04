using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Algorand.Gossip
{
    /// <summary>
    /// A single SRV record (RFC 2782): where a service is hosted, and how to weigh it against others.
    /// </summary>
    public class DnsSrvRecord
    {
        public string Target { get; set; } = string.Empty;
        public ushort Port { get; set; }
        public ushort Priority { get; set; }
        public ushort Weight { get; set; }
    }

    /// <summary>
    /// A minimal DNS SRV record (RFC 2782) resolver implemented over raw UDP sockets. .NET's BCL has no public
    /// API for SRV lookups, and Algorand (and Algorand-compatible networks like Voi/Aramid) publish their relay
    /// and archival gossip nodes as SRV records - e.g. "_archive._tcp.mainnet.algorand.network" - specifically
    /// so clients don't have to hardcode node lists that rotate over time.
    /// </summary>
    public static class DnsSrvResolver
    {
        private static readonly string[] FallbackDnsServers = { "1.1.1.1", "8.8.8.8" };

        /// <summary>
        /// Resolves the SRV records for the given service name, ordered by priority (ascending) then weight
        /// (descending), per RFC 2782. Tries the machine's configured DNS servers first, then falls back to
        /// public resolvers if none are configured or none answer.
        /// </summary>
        public static List<DnsSrvRecord> Resolve(string serviceName, int timeoutMs = 5000)
        {
            Exception lastError = null;
            foreach (var dnsServer in GetDnsServersToTry())
            {
                try
                {
                    var records = Query(serviceName, dnsServer, timeoutMs);
                    if (records.Count > 0) return Order(records);
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }
            }
            throw new InvalidOperationException($"Could not resolve SRV records for '{serviceName}'.", lastError);
        }

        private static List<DnsSrvRecord> Order(List<DnsSrvRecord> records)
        {
            return records.OrderBy(r => r.Priority).ThenByDescending(r => r.Weight).ToList();
        }

        private static IEnumerable<string> GetDnsServersToTry()
        {
            var seen = new HashSet<string>();
            foreach (var ip in GetSystemDnsServers())
            {
                if (seen.Add(ip)) yield return ip;
            }
            foreach (var ip in FallbackDnsServers)
            {
                if (seen.Add(ip)) yield return ip;
            }
        }

        private static IEnumerable<string> GetSystemDnsServers()
        {
            var servers = new List<string>();
            try
            {
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus != OperationalStatus.Up) continue;
                    foreach (var dns in nic.GetIPProperties().DnsAddresses)
                    {
                        if (dns.AddressFamily == AddressFamily.InterNetwork)
                        {
                            servers.Add(dns.ToString());
                        }
                    }
                }
            }
            catch
            {
                // Best-effort only; fall back to public resolvers below.
            }
            return servers;
        }

        /// <summary>
        /// The UDP payload size advertised via EDNS0 (RFC 6891). Without this, resolvers cap plain UDP DNS
        /// responses at 512 bytes and truncate (TC=1, zero answers) anything larger - which archival/relay SRV
        /// answer sets routinely exceed (Algorand MainNet relay alone publishes ~70 records).
        /// </summary>
        private const int EdnsUdpPayloadSize = 4096;

        private static List<DnsSrvRecord> Query(string serviceName, string dnsServer, int timeoutMs)
        {
            var query = BuildQuery(serviceName, out ushort transactionId);
            using (var udp = new UdpClient())
            {
                udp.Client.ReceiveTimeout = timeoutMs;
                udp.Connect(dnsServer, 53);
                udp.Send(query, query.Length);

                var remote = new IPEndPoint(IPAddress.Any, 0);
                var response = udp.Receive(ref remote);

                if (IsTruncated(response))
                {
                    return QueryOverTcp(query, dnsServer, timeoutMs, transactionId);
                }

                return ParseResponse(response, transactionId);
            }
        }

        private static bool IsTruncated(byte[] response)
        {
            if (response.Length < 4) return false;
            ushort flags = (ushort)((response[2] << 8) | response[3]);
            return ((flags >> 9) & 1) == 1;
        }

        private static List<DnsSrvRecord> QueryOverTcp(byte[] query, string dnsServer, int timeoutMs, ushort transactionId)
        {
            using (var tcp = new TcpClient())
            {
                tcp.ReceiveTimeout = timeoutMs;
                tcp.SendTimeout = timeoutMs;
                tcp.Connect(dnsServer, 53);
                using (var stream = tcp.GetStream())
                {
                    // DNS-over-TCP messages are prefixed with a 2-byte big-endian length.
                    var lengthPrefix = new byte[] { (byte)((query.Length >> 8) & 0xFF), (byte)(query.Length & 0xFF) };
                    stream.Write(lengthPrefix, 0, lengthPrefix.Length);
                    stream.Write(query, 0, query.Length);

                    var responseLengthBytes = ReadExact(stream, 2);
                    int responseLength = (responseLengthBytes[0] << 8) | responseLengthBytes[1];
                    var response = ReadExact(stream, responseLength);
                    return ParseResponse(response, transactionId);
                }
            }
        }

        private static byte[] ReadExact(System.Net.Sockets.NetworkStream stream, int count)
        {
            var buffer = new byte[count];
            int read = 0;
            while (read < count)
            {
                int n = stream.Read(buffer, read, count - read);
                if (n == 0) throw new InvalidOperationException("DNS-over-TCP connection closed before the full response was received.");
                read += n;
            }
            return buffer;
        }

        public static byte[] BuildQuery(string serviceName, out ushort transactionId)
        {
            transactionId = (ushort)new Random().Next(ushort.MinValue, ushort.MaxValue + 1);
            var bytes = new List<byte>();

            // Header: ID, flags (standard query, recursion desired), QDCOUNT=1, ANCOUNT=0, NSCOUNT=0, ARCOUNT=1 (EDNS0 OPT below)
            bytes.AddRange(ToBigEndian(transactionId));
            bytes.AddRange(new byte[] { 0x01, 0x00 });
            bytes.AddRange(ToBigEndian(1));
            bytes.AddRange(ToBigEndian(0));
            bytes.AddRange(ToBigEndian(0));
            bytes.AddRange(ToBigEndian(1));

            // Question: QNAME, QTYPE=SRV(33), QCLASS=IN(1)
            bytes.AddRange(EncodeName(serviceName));
            bytes.AddRange(ToBigEndian(33));
            bytes.AddRange(ToBigEndian(1));

            // EDNS0 OPT pseudo-RR (RFC 6891): root name, TYPE=OPT(41), CLASS=UDP payload size, TTL=0 (no flags), RDLENGTH=0
            bytes.Add(0x00);
            bytes.AddRange(ToBigEndian(41));
            bytes.AddRange(ToBigEndian(EdnsUdpPayloadSize));
            bytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            bytes.AddRange(ToBigEndian(0));

            return bytes.ToArray();
        }

        private static byte[] ToBigEndian(int value)
        {
            return new byte[] { (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) };
        }

        public static byte[] EncodeName(string name)
        {
            var bytes = new List<byte>();
            foreach (var label in name.Trim('.').Split('.'))
            {
                var labelBytes = Encoding.ASCII.GetBytes(label);
                if (labelBytes.Length > 63) throw new ArgumentException($"DNS label too long: {label}");
                bytes.Add((byte)labelBytes.Length);
                bytes.AddRange(labelBytes);
            }
            bytes.Add(0);
            return bytes.ToArray();
        }

        public static List<DnsSrvRecord> ParseResponse(byte[] response, ushort? expectedTransactionId = null)
        {
            if (response.Length < 12) throw new InvalidOperationException("DNS response too short.");

            ushort responseId = ReadUInt16(response, 0);
            if (expectedTransactionId.HasValue && responseId != expectedTransactionId.Value)
                throw new InvalidOperationException("DNS response transaction ID mismatch.");

            ushort flags = ReadUInt16(response, 2);
            int rcode = flags & 0x0F;
            if (rcode != 0)
                throw new InvalidOperationException($"DNS server returned error code {rcode}.");

            ushort qdCount = ReadUInt16(response, 4);
            ushort anCount = ReadUInt16(response, 6);

            int pos = 12;
            for (int i = 0; i < qdCount; i++)
            {
                SkipName(response, ref pos);
                pos += 4; // QTYPE + QCLASS
            }

            var records = new List<DnsSrvRecord>();
            for (int i = 0; i < anCount; i++)
            {
                SkipName(response, ref pos);
                ushort type = ReadUInt16(response, pos); pos += 2;
                pos += 2; // CLASS
                pos += 4; // TTL
                ushort rdLength = ReadUInt16(response, pos); pos += 2;
                int rdStart = pos;

                if (type == 33) // SRV
                {
                    ushort priority = ReadUInt16(response, pos); pos += 2;
                    ushort weight = ReadUInt16(response, pos); pos += 2;
                    ushort port = ReadUInt16(response, pos); pos += 2;
                    int targetPos = pos;
                    string target = ReadName(response, ref targetPos);
                    records.Add(new DnsSrvRecord { Priority = priority, Weight = weight, Port = port, Target = target });
                }

                pos = rdStart + rdLength;
            }

            return records;
        }

        private static ushort ReadUInt16(byte[] buffer, int offset)
        {
            return (ushort)((buffer[offset] << 8) | buffer[offset + 1]);
        }

        private static void SkipName(byte[] buffer, ref int pos)
        {
            while (true)
            {
                byte len = buffer[pos];
                if (len == 0)
                {
                    pos++;
                    return;
                }
                if ((len & 0xC0) == 0xC0)
                {
                    pos += 2;
                    return;
                }
                pos += 1 + len;
            }
        }

        internal static string ReadName(byte[] buffer, ref int pos)
        {
            var labels = new List<string>();
            bool jumped = false;
            int originalPos = pos;

            while (true)
            {
                byte len = buffer[pos];
                if (len == 0)
                {
                    pos++;
                    break;
                }
                if ((len & 0xC0) == 0xC0)
                {
                    int pointer = ((len & 0x3F) << 8) | buffer[pos + 1];
                    if (!jumped)
                    {
                        originalPos = pos + 2;
                        jumped = true;
                    }
                    pos = pointer;
                    continue;
                }

                pos++;
                labels.Add(Encoding.ASCII.GetString(buffer, pos, len));
                pos += len;
            }

            if (jumped) pos = originalPos;
            return string.Join(".", labels);
        }
    }
}
