using Algorand.Algod.Model.Transactions;
using Algorand.Gossip;
using Microsoft.Extensions.Logging;

namespace gossip_client_tests
{
    public class Tests
    {
        [Test]
        public async Task StartClientTest ()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger<GossipWebsocketClient> logger = loggerFactory.CreateLogger<GossipWebsocketClient>();

            var client = new Algorand.Gossip.GossipWebsocketClient(
                logger,
                new Algorand.Gossip.GossipWebsocketClientConfiguration
                {
                    //Host = "ws://a-m7.algorand-mainnet.network:4160/v1/mainnet-v1.0/gossip"
                    Host = "ws://r-a0.algorand-mainnet.network:4160/v1/mainnet-v1.0/gossip"
                });
            var transactions = new List<SignedTransaction>();
            client.TransactionReceivedEvent += (sender, tx) =>
            {
                transactions.AddRange(tx);
                return Task.CompletedTask;
            };

            await client.Start();
            await Task.Delay(2000); // Wait for the client to connect
            await client.Stop();

            Assert.That(transactions.Count, Is.GreaterThan(0));
        }

        [Test]
        public void SplitBytes_WithExampleFromComment_ShouldMatchExpectedBehavior()
        {
            // Arrange - Using the exact example from the method comment
            // "82a30182a302 with delimiter 82a3 will return segments [82a301, 82a302]"
            var input = new byte[] { 0x82, 0xa3, 0x01, 0x82, 0xa3, 0x02 };
            var delimiter = new byte[] { 0x82, 0xa3 };

            // Act
            var result = GossipWebsocketClient.SplitBytes(input, delimiter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(new byte[] { 0x82, 0xa3, 0x01 }));
            Assert.That(result[1], Is.EqualTo(new byte[] { 0x82, 0xa3, 0x02 }));
        }
   }
}