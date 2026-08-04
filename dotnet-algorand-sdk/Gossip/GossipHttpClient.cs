using Algorand.Algod.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Algorand.Gossip
{
    public class GossipHttpClient
    {
        private readonly GossipHttpConfiguration _gossipConfiguration;
        private readonly ConcurrentDictionary<string, System.Net.Http.HttpClient> _httpClients;

        private System.Net.Http.HttpClient GetRandomHttpClient()
        {
            if (_httpClients == null || _httpClients.IsEmpty)
                throw new InvalidOperationException("No HTTP clients available.");

            var keys = new List<string>(_httpClients.Keys);
            var random = new Random();
            var randomKey = keys[random.Next(keys.Count)];
            return _httpClients[randomKey];
        }
        public GossipHttpClient(GossipHttpConfiguration config)
        {
            _httpClients = new ConcurrentDictionary<string, System.Net.Http.HttpClient>();
            foreach (var host in config.Hosts)
            {
                var client = HttpClientConfigurator.ConfigureHttpClient(host, string.Empty);
                _httpClients.TryAdd(host, client);
            }

            _gossipConfiguration = config;
        }

        public async Task<CertifiedBlock> FetchBlockAsync(ulong round, int attempts = 3)
        {
            var client = GetRandomHttpClient();
            var roundInBase36 = ToBase36(round);
            var response = await client.GetAsync($"/v1/{_gossipConfiguration.GenesisId}/block/{roundInBase36}");

            if (response.StatusCode > 0)
            {
                var bytes= await response.Content.ReadAsByteArrayAsync();
                return Utils.Encoder.DecodeFromMsgPack<CertifiedBlock>(bytes);
            }
            else
            {
                return await FetchBlockAsync(round, attempts - 1);
            }
        }

        public static string ToBase36(ulong num)
        {
            if (num == 0) return "0";
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder();
            while (num > 0)
            {
                var remainder = (int)(num % 36);
                result.Insert(0, chars[remainder]);
                num /= 36;
            }
            return result.ToString();
        }
    }
}
