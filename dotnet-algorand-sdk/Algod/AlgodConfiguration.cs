namespace Algorand.Algod
{
    public class AlgodConfiguration
    {
        public static AlgodConfiguration MainNet = new AlgodConfiguration()
        {
            Host = "https://mainnet-api.4160.nodely.dev"
        };
        public static AlgodConfiguration TestNet = new AlgodConfiguration()
        {
            Host = "https://testnet-api.4160.nodely.dev"
        };
        public static AlgodConfiguration BetaNet = new AlgodConfiguration()
        {
            Host = "https://betanet-api.4160.nodely.dev"
        };
        public static AlgodConfiguration DockerNet = new AlgodConfiguration()
        {
            Host = "http://localhost:4001",
            ApiKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            Header = "X-Algo-API-Token"
        };
        public static AlgodConfiguration VoiMain = new AlgodConfiguration()
        {
            Host = "https://mainnet-api.voi.nodely.dev"
        };
        public static AlgodConfiguration AramidMain = new AlgodConfiguration()
        {
            Host = "https://algod.aramidmain.a-wallet.net"
        };

        /// <summary>
        /// Algod Host
        /// </summary>
        public string Host { get; set; } = "https://mainnet-api.4160.nodely.dev";
        /// <summary>
        /// Api Key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// Use custom header for api key if defined
        /// </summary>
        public string Header { get; set; } = string.Empty;
    }
}
