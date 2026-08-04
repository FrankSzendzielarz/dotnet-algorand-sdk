using Algorand;
using Algorand.Algod;
using Algorand.KMD;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace specflow.StepDefinitions
{
    [Binding]
    public sealed class httpUtilities
    {
        // run "algokit localnet start" before the tests
        const string ALGOD_API_ADDR = "http://localhost:4001/";
        const string ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        private static HttpClient httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);

        // Lazily assigned by setUp()/setUpKmd() before any test uses them; null! avoids forcing every
        // call site to null-check a field that is always initialized by the time it's read.
        internal static DefaultApi algodDefaultApiInstance = null!;

        internal static Algorand.KMD.Api kmdApi = null!;

        [Given("mock server recording request paths")]
        public void MockServerRecordingRequestPaths()
        {
            setUp();

        }

        
        public static void setUp()
        { 
            if (algodDefaultApiInstance == null) algodDefaultApiInstance = new DefaultApi(httpClient);
     
           
        }

        public static void setUpKmd()
        {
            if (kmdApi == null)
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

                kmdApi = new Api(client);
                kmdApi.BaseUrl = @"http://localhost:4002";
            }
        }
    }
}
