using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Utils;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdk_examples
{
    public class CompileTeal
    {
        public static async Task Main(string[] args)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algod = new AlgodClient(httpClient);

            byte[] data = Encoding.ASCII.GetBytes(TEALContractsForExamples.HelloWorld());

            using (var datams = new MemoryStream(data))
            {
                CompileResponse response = await algod.TealCompileAsync(datams,true);
                Console.WriteLine("response: " + response);
                Console.WriteLine("Hash: " + response.Hash);
                Console.WriteLine("Result: " + response.Result);

                // Demonstrate using a source map
                
                SourceMap map = new SourceMap(response.Sourcemap);
                Console.WriteLine($"PC for line 2 is {map.GetPcsForLine(2).First()}");



            }
        }
    }
}

