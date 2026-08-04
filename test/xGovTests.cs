using Algorand;
using Algorand.Algod;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XGovRegistry.XGovRegistryProxy.Structs;

namespace test
{
    [TestFixture]
    public class xGovTests
    {

        [Test]
        public async Task TestXGovRegistry()
        {
            // list boxes
            using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.MainNet);

            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var response = await algodApiInstance.GetApplicationBoxesAsync(3147789458);
            Assert.That(response.Boxes.Count, Is.GreaterThan(0));

            var xgovs = response.Boxes.Where(b => b.Name.First() == 'x').ToArray();

            // if folder xGov does not exists, create it
            if (Directory.Exists(Directory.GetCurrentDirectory() + "/xGov") == false)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/xGov");
            }

            foreach (var xgov in xgovs)
            {
                try
                {
                    // byte 1 to 33 is the address
                    var xgovAddress = new Address(xgov.Name.Skip(1).Take(32).ToArray());
                    var xgovCacheFilePath = Path.Combine(Directory.GetCurrentDirectory(), "xGov", $"{xgovAddress.EncodeAsString()}.dat");

                    if (File.Exists(xgovCacheFilePath)) continue;
                    var xgovData = await algodApiInstance.GetApplicationBoxByNameAsync(3147789458, $"b64:{Convert.ToBase64String(xgov.Name)}");

                    File.WriteAllBytes(xgovCacheFilePath, xgovData.Value);
                    await Task.Delay(500);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing box {Convert.ToBase64String(xgov.Name)}: {ex.Message}");
                    await Task.Delay(500);
                }
            }
            foreach (var xgov in xgovs)
            {
                try
                {
                    // byte 1 to 33 is the address
                    var xgovAddress = new Address(xgov.Name.Skip(1).Take(32).ToArray());
                    var xgovCacheFilePath = Path.Combine(Directory.GetCurrentDirectory(), "xGov", $"{xgovAddress.EncodeAsString()}.dat");

                    if (!File.Exists(xgovCacheFilePath)) continue;


                    var xgovData = File.ReadAllBytes(xgovCacheFilePath);
                    var parsedData = XGovBoxValue.Parse(xgovData);
                    var xgovCacheFilePath2 = Path.Combine(Directory.GetCurrentDirectory(), "xGov", $"{xgovAddress.EncodeAsString()}.json");
                    File.WriteAllText(xgovCacheFilePath2, Newtonsoft.Json.JsonConvert.SerializeObject(parsedData, Newtonsoft.Json.Formatting.Indented));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing box {Convert.ToBase64String(xgov.Name)}: {ex.Message}");
                }
            }
        }

        [Test]
        public async Task TestXGovProposal()
        {
            var prposalId = 3368513873;
            // list boxes
            using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.MainNet);

            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var response = await algodApiInstance.GetApplicationBoxesAsync(prposalId);
            Assert.That(response.Boxes.Count, Is.GreaterThan(0));

            var xgovs = response.Boxes.Where(b => b.Name.First() == 'V').ToArray();

            // if folder xGov does not exists, create it
            if (Directory.Exists(Directory.GetCurrentDirectory() + "/xGov") == false)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/xGov");
            }
            foreach (var xgov in xgovs)
            {
                try
                {
                    // byte 1 to 33 is the address
                    var xgovAddress = new Address(xgov.Name.Skip(1).Take(32).ToArray());
                    var xgovCacheFilePath = Path.Combine(Directory.GetCurrentDirectory(), "xGov", $"{prposalId}-{xgovAddress.EncodeAsString()}.dat");

                    if (File.Exists(xgovCacheFilePath)) continue;
                    var xgovData = await algodApiInstance.GetApplicationBoxByNameAsync(prposalId, $"b64:{Convert.ToBase64String(xgov.Name)}");
                    var value = BitConverter.ToUInt64(xgovData.Value.Reverse().ToArray());
                    File.WriteAllText(xgovCacheFilePath, value.ToString());
                    await Task.Delay(500);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing box {Convert.ToBase64String(xgov.Name)}: {ex.Message}");
                    await Task.Delay(500);
                }
            }

            var csv = new StringBuilder();
            csv.AppendLine("Address;Value");
            foreach (var xgov in xgovs)
            {
                try
                {
                    // byte 1 to 33 is the address
                    var xgovAddress = new Address(xgov.Name.Skip(1).Take(32).ToArray());
                    var xgovCacheFilePath = Path.Combine(Directory.GetCurrentDirectory(), "xGov", $"{prposalId}-{xgovAddress.EncodeAsString()}.dat");

                    if (!File.Exists(xgovCacheFilePath)) continue;
                    csv.AppendLine($"{xgovAddress.EncodeAsString()};{File.ReadAllText(xgovCacheFilePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing box {Convert.ToBase64String(xgov.Name)}: {ex.Message}");
                    await Task.Delay(500);
                }
            }
            var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "xGov", $"{prposalId}-Proposals.csv");
            File.WriteAllText(csvPath, csv.ToString());
        }
    }
}
