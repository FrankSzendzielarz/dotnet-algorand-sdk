using Algorand.Algod.Model.Transactions;
using Algorand.Gossip.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace Algorand.Gossip
{
    public class GossipWebsocketClient : IDisposable
    {
        private readonly GossipWebsocketClientConfiguration _config;
        private readonly ILogger<GossipWebsocketClient> _logger;
        private WebsocketClient? client;
        private bool disposedValue;

        public delegate Task AgreementVoteReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task MsgOfInterestReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task MsgDigestSkipReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task NetPrioResponseReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task PingReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task PingReplyReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task ProposalPayloadReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task StateProofSigReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task TopicMsgRespReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task TransactionReceivedEventHandler(object sender, IEnumerable<SignedTransaction> tx);
        public delegate Task UniEnsBlockReqReceivedEventHandler(object sender, byte[] bytes);
        public delegate Task VoteBundleReceivedEventHandler(object sender, byte[] bytes);
        public event AgreementVoteReceivedEventHandler AgreementVoteReceivedEvent;
        public event MsgOfInterestReceivedEventHandler MsgOfInterestReceivedEvent;
        public event MsgDigestSkipReceivedEventHandler MsgDigestSkipReceivedEvent;
        public event NetPrioResponseReceivedEventHandler NetPrioResponseReceivedEvent;
        public event PingReceivedEventHandler PingReceivedEvent;
        public event PingReplyReceivedEventHandler PingReplyReceivedEvent;
        public event ProposalPayloadReceivedEventHandler ProposalPayloadReceivedEvent;
        public event StateProofSigReceivedEventHandler StateProofSigReceivedEvent;
        public event TopicMsgRespReceivedEventHandler TopicMsgRespReceivedEvent;
        public event TransactionReceivedEventHandler TransactionReceivedEvent;
        public event UniEnsBlockReqReceivedEventHandler UniEnsBlockReqReceivedEvent;
        public event VoteBundleReceivedEventHandler VoteBundleReceivedEvent;



        /// <summary>
        /// Constructor for the GossipClient.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public GossipWebsocketClient(ILogger<GossipWebsocketClient> logger, GossipWebsocketClientConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<bool> Stop()
        {
            return await client?.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "Closed");
        }
        public async Task Start()
        {
            if (client != null)
            {
                client.Dispose();
            }

            var uri = new Uri(_config.Host);
            client = new WebsocketClient(uri, () =>
            {
                var clientWs = new System.Net.WebSockets.ClientWebSocket();
                clientWs.Options.SetRequestHeader("User-Agent", "dotnet-algo-sdk/4.4.1");
                clientWs.Options.SetRequestHeader("X-Algorand-TelId", "mainnet-v1.0");
                clientWs.Options.SetRequestHeader("X-Algorand-NodeRandom", "mainnet-v1.0");
                clientWs.Options.SetRequestHeader("X-Algorand-Location", "Earth");
                clientWs.Options.SetRequestHeader("X-Algorand-Genesis", "mainnet-v1.0");
                clientWs.Options.SetRequestHeader("X-Algorand-InstanceName", "Node1");
                clientWs.Options.SetRequestHeader("X-Algorand-Peer-Features", "ppzstd");
                clientWs.Options.SetRequestHeader("X-Algorand-Version", "2.2");
                clientWs.Options.SetRequestHeader("X-Algorand-Accept-Version", "2.2");
                return clientWs;
            });

            client.ReconnectTimeout = TimeSpan.FromSeconds(30);
            client.ReconnectionHappened.Subscribe(info =>
                _logger.LogInformation($"Reconnection happened, type: {info.Type}"));

            client.MessageReceived.Subscribe(msg =>
            {
                if (msg?.Binary == null)
                {
                    _logger.LogWarning("Received message with null binary data.");
                    return;
                }
                if (msg.Binary.Length < 2)
                {
                    _logger.LogWarning("Received message with insufficient length.");
                    return;
                }
                string msgTag = Encoding.ASCII.GetString(new byte[] { msg.Binary[0], msg.Binary[1] });
                switch (msgTag)
                {
                    case Tag.AgreementVote:
                        AgreementVoteReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.MsgOfInterestTag:
                        MsgOfInterestReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.MsgDigestSkipTag:
                        MsgDigestSkipReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.NetPrioResponseTag:
                        NetPrioResponseReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.PingTag:
                        PingReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.PingReplyTag:
                        PingReplyReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.ProposalPayloadTag:
                        ProposalPayloadReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.StateProofSigTag:
                        StateProofSigReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.TopicMsgRespTag:
                        TopicMsgRespReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;

                    case Tag.TxnTag:
                        var txBytes = SplitBytes(msg.Binary.AsSpan(2), new byte[] { 0x82, 0xa3, 0x73, 0x69, 0x67, 0xc4 });
                        var txs = txBytes.Select(t => Algorand.Utils.Encoder.DecodeFromMsgPack<Algorand.Algod.Model.Transactions.SignedTransaction>(t));
                        //foreach (var t in txBytes)
                        //{
                        //    if (t.Length == 0)
                        //    {
                        //        _logger.LogWarning("Received empty transaction byte array.");
                        //        continue;
                        //    }
                        //    try
                        //    {
                        //        var signedTx = Algorand.Utils.Encoder.DecodeFromMsgPack<Algorand.Algod.Model.Transactions.SignedTransaction>(t);
                        //        TransactionReceivedEvent?.Invoke(this, signedTx);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        _logger.LogError(ex, "Failed to decode transaction from MsgPack.");
                        //    }
                        //}
                        //var tx = txBytes.Select(t => Algorand.Utils.Encoder.DecodeFromMsgPack<Algorand.Algod.Model.Transactions.SignedTransaction>(t)).ToArray();
                        //_logger.LogInformation($"Tx received: {Algorand.Utils.Encoder.EncodeToJson(tx.Tx)}");
                        TransactionReceivedEvent?.Invoke(this, txs);
                        break;
                    case Tag.UniEnsBlockReqTag:
                        UniEnsBlockReqReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    case Tag.VoteBundleTag:
                        VoteBundleReceivedEvent?.Invoke(this, msg.Binary[2..]);
                        break;
                    default:
                        //Console.WriteLine(BitConverter.ToString(msg.Binary).Replace("-", "").ToLower());
                        //_logger.LogInformation($"{msgTag} Message received: {msg.Binary.Length}");
                        break;
                }
            });
            client.DisconnectionHappened.Subscribe(info =>
                _logger.LogInformation($"Disconnection happened, type: {info.Type} {info.Exception?.Message}"
                ));
            await client.Start();


            var requestOfInterest = new Topics()
            {
                new Topic()
                {
                    Key = "tags",
                    Data = Encoding.ASCII.GetBytes(Tag.TxnTag)
                }
            };

            var requestOfInterestMsgPack = MessagePack.MessagePackSerializer.Serialize(requestOfInterest);
            var requestOfInterestFrame = Encoding.ASCII.GetBytes(Tag.MsgOfInterestTag).Concat(requestOfInterestMsgPack).ToArray();

            client.Send(requestOfInterestFrame);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    client?.Dispose();
                }

                client = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public static List<byte[]> SplitBytes(ReadOnlySpan<byte> input, ReadOnlySpan<byte> delimiter)
        {
            // split the input bytes by the multibyte delimiter which is the prefix
            // do not include empty segments
            // delimiter must be at the start of the segment and is included in the segment
            // for example 82a30182a302 with delimiter 82a3
            // will return segments [82a301, 82a302]
            List<byte[]> segments = new List<byte[]>();
            int start = 0;

            while (start < input.Length)
            {
                int index = input.Slice(start).IndexOf(delimiter);
                if (index == -1)
                {
                    // No more delimiters found, add remaining data if any
                    if (start < input.Length)
                    {
                        segments.Add(input.Slice(start).ToArray());
                    }
                    break;
                }

                // Adjust index to the original span
                index += start;

                // Find the next delimiter to determine the end of this segment
                int nextDelimiterStart = index + delimiter.Length;
                int nextIndex = -1;

                if (nextDelimiterStart < input.Length)
                {
                    nextIndex = input.Slice(nextDelimiterStart).IndexOf(delimiter);
                    if (nextIndex != -1)
                    {
                        nextIndex += nextDelimiterStart;
                    }
                }

                // Create segment from current delimiter to next delimiter (or end)
                if (nextIndex != -1)
                {
                    // There's another delimiter, segment goes from current delimiter to next delimiter
                    segments.Add(input.Slice(index, nextIndex - index).ToArray());
                    start = nextIndex;
                }
                else
                {
                    // No more delimiters, segment goes from current delimiter to end
                    segments.Add(input.Slice(index).ToArray());
                    break;
                }
            }

            return segments;
        }

    }
}
