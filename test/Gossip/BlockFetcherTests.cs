using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model.Transactions;
using Algorand.Gossip;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace test.Gossip
{
    [TestFixture]
    public class BlockFetcherTests
    {
        [Test]
        public async Task FetchBlocks1To100()
        {
            var gossipClient = new Algorand.Gossip.GossipHttpClient(GossipHttpConfiguration.MainNetArchival);

            for (ulong i = 1; i < 100; i++)
            {
                var block = await gossipClient.FetchBlockAsync(i);
                Assert.That(block, Is.Not.Null, $"Block {i} should not be null.");
                Assert.That(block.Block.Round, Is.EqualTo(i));
            }
        }
        [Test]
        public async Task FetchBlocksAreEqualToAlgodNode()
        {
            using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.MainNet);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var status = await algodApiInstance.GetStatusAsync();

            ulong round = 52402526; //50_000_440;


            var gossipClient = new Algorand.Gossip.GossipHttpClient(GossipHttpConfiguration.MainNetArchival);

            var block = await gossipClient.FetchBlockAsync(round);
            Assert.That(block, Is.Not.Null, $"Block {round} should not be null.");
            Assert.That(block.Block.Round, Is.EqualTo(round));

            long durationJson = 0;
            long durationMsgPack = 0;
            long durationMsgPackGossip = 0;

            await algodApiInstance.WaitForBlockAsync(round);
            var watchJson = new Stopwatch();
            watchJson.Start();
            var blockJson = await algodApiInstance.GetBlockAsync(round, Algorand.Algod.Model.Format.Json, false);
            watchJson.Stop();
            durationJson += watchJson.ElapsedMilliseconds;

            var watchMsgPack = new Stopwatch();
            watchMsgPack.Start();
            var blockMsgPack = await algodApiInstance.GetBlockAsync(round, Algorand.Algod.Model.Format.Msgpack, false);
            watchMsgPack.Stop();
            durationMsgPack += watchMsgPack.ElapsedMilliseconds;
            blockMsgPack.Cert = null;

            var watchMsgPackGossip = new Stopwatch();
            watchMsgPackGossip.Start();
            var blockMsgPackGossip = await gossipClient.FetchBlockAsync(round);
            watchMsgPackGossip.Stop();
            durationMsgPackGossip += watchMsgPackGossip.ElapsedMilliseconds;
            blockMsgPackGossip.Cert = null;

            // ValueDelta.Bytes ("bs") is lossy in algod's own JSON API for non-UTF8 binary values (algod itself
            // substitutes U+FFFD before the response leaves the server), so it can never match byte-for-byte
            // against the (lossless) msgpack-decoded value - see BlockComparisonHelpers for details.
            BlockComparisonHelpers.ClearLossyStateDeltaBytes(blockJson);
            BlockComparisonHelpers.ClearLossyStateDeltaBytes(blockMsgPack);
            BlockComparisonHelpers.ClearLossyStateDeltaBytes(blockMsgPackGossip);

            var jsonFromJson = Algorand.Utils.Encoder.EncodeToJson(blockJson);
            var jsonFromMsgPack = Algorand.Utils.Encoder.EncodeToJson(blockMsgPack);
            var jsonFromMsgPackGossip = Algorand.Utils.Encoder.EncodeToJson(blockMsgPackGossip);
            Assert.That(jsonFromJson, Is.EqualTo(jsonFromMsgPack), $"Block round failed {round}");
            Assert.That(jsonFromMsgPackGossip, Is.EqualTo(jsonFromMsgPack), $"Block round failed {round}");

            Console.WriteLine($"Json: {durationJson} ms, MsgPack: {durationMsgPack} ms, MsgPackGossip: {durationMsgPackGossip} ms");

        }
        [Test]
        public async Task DeltaValueWorks55983440Gossip()
        {
            using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.MainNet);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var status = await algodApiInstance.GetStatusAsync();

            ulong round = 55983440;


            var gossipClient = new Algorand.Gossip.GossipHttpClient(GossipHttpConfiguration.MainNetArchival);

            var block = await gossipClient.FetchBlockAsync(round);
            Assert.That(block, Is.Not.Null, $"Block {round} should not be null.");
            Assert.That(block.Block.Round, Is.EqualTo(round));

            long durationJson = 0;
            long durationMsgPack = 0;
            long durationMsgPackGossip = 0;

            await algodApiInstance.WaitForBlockAsync(round);
            var watchJson = new Stopwatch();
            watchJson.Start();
            var blockJson = await algodApiInstance.GetBlockAsync(round, Algorand.Algod.Model.Format.Json, false);
            watchJson.Stop();
            durationJson += watchJson.ElapsedMilliseconds;

            var watchMsgPack = new Stopwatch();
            watchMsgPack.Start();
            var blockMsgPack = await algodApiInstance.GetBlockAsync(round, Algorand.Algod.Model.Format.Msgpack, false);
            watchMsgPack.Stop();
            durationMsgPack += watchMsgPack.ElapsedMilliseconds;
            blockMsgPack.Cert = null;

            var watchMsgPackGossip = new Stopwatch();
            watchMsgPackGossip.Start();
            var blockMsgPackGossip = await gossipClient.FetchBlockAsync(round);
            watchMsgPackGossip.Stop();
            durationMsgPackGossip += watchMsgPackGossip.ElapsedMilliseconds;
            blockMsgPackGossip.Cert = null;

            var jsonFromJson = Algorand.Utils.Encoder.EncodeToJson(blockJson);
            var jsonFromMsgPack = Algorand.Utils.Encoder.EncodeToJson(blockMsgPack);
            var jsonFromMsgPackGossip = Algorand.Utils.Encoder.EncodeToJson(blockMsgPackGossip);
            //Assert.That(jsonFromJson, Is.EqualTo(jsonFromMsgPackGossip), $"Block round failed {round}");
            //Assert.That(jsonFromJson, Is.EqualTo(jsonFromMsgPack), $"Block round failed {round} {jsonFromJson.Length}/{jsonFromMsgPack.Length}/{jsonFromMsgPackGossip.Length}");
            Assert.That(jsonFromMsgPackGossip, Is.EqualTo(jsonFromMsgPack), $"Block round failed {round}");

            Console.WriteLine($"Json: {durationJson} ms, MsgPack: {durationMsgPack} ms, MsgPackGossip: {durationMsgPackGossip} ms");

            Assert.That(blockMsgPackGossip.Block.Round, Is.EqualTo(round));
            Assert.That(blockMsgPackGossip.Block.Transactions.Count, Is.EqualTo(15));
            var txs = blockMsgPackGossip.Block.Transactions.ToArray();
            Assert.That(txs[5].Detail.InnerTxns.Count, Is.EqualTo(5));
            var inners = txs[5].Detail.InnerTxns.ToArray();
            var appCall = inners[1].Tx as ApplicationNoopTransaction;

            Assert.That(appCall, Is.Not.Null);
            Assert.That(appCall.type, Is.EqualTo("appl"));
            Assert.That(appCall.ApplicationId, Is.EqualTo(3136517663ul));
            Assert.That(inners[1].Detail.GlobalDelta.Count, Is.EqualTo(6));
            var gd = inners[1].Detail.GlobalDelta.ToArray();
            Assert.That(gd[1].Key, Is.EqualTo("Lb"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[1].Value.Bytes as string), Is.EqualTo("0x000000000000000000000000000000000000000000000000000008a677166d10"));
            Assert.That(gd[0].Key, Is.EqualTo("L"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[0].Value.Bytes as string), Is.EqualTo("0x000000000000000000000000000000000000000000000000002112687f3e2b01"));
            Assert.That(gd[2].Key, Is.EqualTo("Lu"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[2].Value.Bytes as string), Is.EqualTo("0x00000000000000000000000000000000000000000000000000002299d095789d"));
            Assert.That(gd[3].Key, Is.EqualTo("ab"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[3].Value.Bytes as string), Is.EqualTo("0x00000000000000000000000000000000000000000000000000023d3bc6f1a3d8"));
            Assert.That(gd[4].Key, Is.EqualTo("bb"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[4].Value.Bytes as string), Is.EqualTo("0x0000000000000000000000000000000000000000000000000000816cac502e78"));
            Assert.That(gd[5].Key, Is.EqualTo("price"));
            Assert.That(gd[5].Value.Uint64, Is.EqualTo(151673299ul));
        }
        [Test]
        public async Task DeltaValueWorks55983440AlgodApi()
        {
            using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.MainNet);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var status = await algodApiInstance.GetStatusAsync();

            ulong round = 55983440;


            var gossipClient = new Algorand.Gossip.GossipHttpClient(GossipHttpConfiguration.MainNetArchival);

            var block = await gossipClient.FetchBlockAsync(round);
            Assert.That(block, Is.Not.Null, $"Block {round} should not be null.");
            Assert.That(block.Block.Round, Is.EqualTo(round));

            long durationJson = 0;
            long durationMsgPack = 0;
            long durationMsgPackGossip = 0;

            await algodApiInstance.WaitForBlockAsync(round);
            var watchJson = new Stopwatch();
            watchJson.Start();
            var blockJson = await algodApiInstance.GetBlockAsync(round, Algorand.Algod.Model.Format.Json, false);
            watchJson.Stop();
            durationJson += watchJson.ElapsedMilliseconds;

            var watchMsgPack = new Stopwatch();
            watchMsgPack.Start();
            var blockMsgPack = await algodApiInstance.GetBlockAsync(round, Algorand.Algod.Model.Format.Msgpack, false);
            watchMsgPack.Stop();
            durationMsgPack += watchMsgPack.ElapsedMilliseconds;
            blockMsgPack.Cert = null;

            var watchMsgPackGossip = new Stopwatch();
            watchMsgPackGossip.Start();
            var blockMsgPackGossip = await gossipClient.FetchBlockAsync(round);
            watchMsgPackGossip.Stop();
            durationMsgPackGossip += watchMsgPackGossip.ElapsedMilliseconds;
            blockMsgPackGossip.Cert = null;

            var jsonFromJson = Algorand.Utils.Encoder.EncodeToJson(blockJson);
            var jsonFromMsgPack = Algorand.Utils.Encoder.EncodeToJson(blockMsgPack);
            var jsonFromMsgPackGossip = Algorand.Utils.Encoder.EncodeToJson(blockMsgPackGossip);
            //Assert.That(jsonFromJson, Is.EqualTo(jsonFromMsgPackGossip), $"Block round failed {round}");
            //Assert.That(jsonFromJson, Is.EqualTo(jsonFromMsgPack), $"Block round failed {round} {jsonFromJson.Length}/{jsonFromMsgPack.Length}/{jsonFromMsgPackGossip.Length}");
            Assert.That(jsonFromMsgPackGossip, Is.EqualTo(jsonFromMsgPack), $"Block round failed {round}");

            Console.WriteLine($"Json: {durationJson} ms, MsgPack: {durationMsgPack} ms, MsgPackGossip: {durationMsgPackGossip} ms");

            Assert.That(blockMsgPack.Block.Round, Is.EqualTo(round));
            Assert.That(blockMsgPack.Block.Transactions.Count, Is.EqualTo(15));
            var txs = blockMsgPack.Block.Transactions.ToArray();
            Assert.That(txs[5].Detail.InnerTxns.Count, Is.EqualTo(5));
            var inners = txs[5].Detail.InnerTxns.ToArray();
            var appCall = inners[1].Tx as ApplicationNoopTransaction;

            Assert.That(appCall, Is.Not.Null);
            Assert.That(appCall.type, Is.EqualTo("appl"));
            Assert.That(appCall.ApplicationId, Is.EqualTo(3136517663ul));
            Assert.That(inners[1].Detail.GlobalDelta.Count, Is.EqualTo(6));
            var gd = inners[1].Detail.GlobalDelta.ToArray();
            Assert.That(gd[1].Key, Is.EqualTo("Lb"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[1].Value.Bytes as string), Is.EqualTo("0x000000000000000000000000000000000000000000000000000008a677166d10"));
            Assert.That(gd[0].Key, Is.EqualTo("L"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[0].Value.Bytes as string), Is.EqualTo("0x000000000000000000000000000000000000000000000000002112687f3e2b01"));
            Assert.That(gd[2].Key, Is.EqualTo("Lu"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[2].Value.Bytes as string), Is.EqualTo("0x00000000000000000000000000000000000000000000000000002299d095789d"));
            Assert.That(gd[3].Key, Is.EqualTo("ab"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[3].Value.Bytes as string), Is.EqualTo("0x00000000000000000000000000000000000000000000000000023d3bc6f1a3d8"));
            Assert.That(gd[4].Key, Is.EqualTo("bb"));
            Assert.That(Algorand.Utils.Utils.DeltaValueStringToBytes(gd[4].Value.Bytes as string), Is.EqualTo("0x0000000000000000000000000000000000000000000000000000816cac502e78"));
            Assert.That(gd[5].Key, Is.EqualTo("price"));
            Assert.That(gd[5].Value.Uint64, Is.EqualTo(151673299ul));
        }
        //[Test]
        //public void ConversionTest2112687f3e2b019d67994b()
        //{
        //    // input and output is correct
        //    var input = "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0!\u0012h\u007f>+\u0001";
        //    var output = "000000000000000000000000000000000000000000000000002112687f3e2b01";

        //    //var converted = Algorand.Utils.Utils.DeltaValueStringToBytes(input);
        //    var converted = Encoding.Latin1.GetBytes(input);
        //    Assert.That(converted.Length, Is.EqualTo(32));
        //    Assert.That(Convert.ToHexString(converted).ToLower(), Is.EqualTo(output));
        //}
        //[Test]
        //public void ConversionTest8a677166d1005c26eb3()
        //{
        //    // input and output is correct
        //    var input = "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\b�w\u0016m\u0010";
        //    var output = "000000000000000000000000000000000000000000000000000008a677166d10";

        //    //var converted = Algorand.Utils.Utils.DeltaValueStringToBytes(input);
        //    var converted = Encoding.Latin1.GetBytes(input);
        //    Assert.That(converted.Length, Is.EqualTo(32));
        //    Assert.That(Convert.ToHexString(converted).ToLower(), Is.EqualTo(output));
        //}
    }
}
