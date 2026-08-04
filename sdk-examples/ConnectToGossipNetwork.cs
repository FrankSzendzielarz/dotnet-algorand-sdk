using Algorand.Gossip;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdk_examples
{
    public class ConnectToGossipNetwork
    {
        public static async Task Main(params string[] args)
        {
            var gossipClient = new Algorand.Gossip.GossipHttpClient(GossipHttpConfiguration.MainNetArchival);
            var block = await gossipClient.FetchBlockAsync(123);
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} Fetched block {block.Block.Round} from mainnet archival gossip node");


            var _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var clientConfig = new GossipWebsocketClientConfiguration()
            {
                Host = "ws://r-pl.algorand-mainnet.network:4160/v1/mainnet-v1.0/gossip"
                //Host = "ws://fi-k1-s1.a-wallet.net:14160/v1/aramidmain-v1.0/gossip"
                //Host = "ws://d.a-wallet.net:14160/v1/aramidmain-v1.0/gossip"
            };
            var client = new GossipWebsocketClient(_loggerFactory.CreateLogger<GossipWebsocketClient>(), clientConfig);
            client.AgreementVoteReceivedEvent += Client_AgreementVoteReceivedEvent;
            client.MsgDigestSkipReceivedEvent += Client_MsgDigestSkipReceivedEvent;
            client.MsgOfInterestReceivedEvent += Client_MsgOfInterestReceivedEvent;
            client.NetPrioResponseReceivedEvent += Client_NetPrioResponseReceivedEvent;
            client.PingReceivedEvent += Client_PingReceivedEvent;
            client.PingReplyReceivedEvent += Client_PingReplyReceivedEvent;
            client.TransactionReceivedEvent += Client_TransactionReceivedEvent;
            client.ProposalPayloadReceivedEvent += Client_ProposalPayloadReceivedEvent;
            client.StateProofSigReceivedEvent += Client_StateProofSigReceivedEvent;
            client.TopicMsgRespReceivedEvent += Client_TopicMsgRespReceivedEvent;
            client.UniEnsBlockReqReceivedEvent += Client_UniEnsBlockReqReceivedEvent;
            client.VoteBundleReceivedEvent += Client_VoteBundleReceivedEvent;

            await client.Start();
            while(true)
            await Task.Delay(60000);
            
        }

        private static async Task Client_VoteBundleReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} VoteBundleReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_UniEnsBlockReqReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} UniEnsBlockReqReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_TopicMsgRespReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} TopicMsgRespReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_StateProofSigReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} StateProofSigReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_ProposalPayloadReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} ProposalPayloadReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_PingReplyReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} PingReplyReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_PingReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} PingReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_NetPrioResponseReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} NetPrioResponseReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_MsgOfInterestReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} MsgOfInterestReceivedEvent: {bytes.Length}");
        }

        private static async Task Client_AgreementVoteReceivedEvent(object sender, byte[] bytes)
        {
            var decoded = Algorand.Utils.Encoder.DecodeFromMsgPack<Algorand.Algod.Model.Agreement.Vote>(bytes);
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} AV: {decoded.R.Round} {decoded.R.Step} {decoded.R.Proposal?.OriginalProposer.EncodeAsString().Substring(0,4) ?? "N"} {decoded.R.Sender.EncodeAsString().Substring(0, 4)} {bytes.Length}");
        }
        private static async Task Client_MsgDigestSkipReceivedEvent(object sender, byte[] bytes)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} MsgDigestSkipReceivedEvent: {bytes.Length}");
        }


        private static async Task Client_TransactionReceivedEvent(object sender, IEnumerable<Algorand.Algod.Model.Transactions.SignedTransaction> tx)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffffff")} Transactions received: {tx.Count()}");
        }
    }
}
