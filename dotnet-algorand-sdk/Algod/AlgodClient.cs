namespace Algorand.Algod
{
    /// <summary>
    /// Algod client for interacting with the Algorand node REST API.
    /// Recommended alias for DefaultApi.
    /// </summary>
    public class AlgodClient : DefaultApi
    {
        /// <summary>
        /// Create a new Algod client with the provided HTTP client.
        /// </summary>
        /// <param name="httpClient">Configured HTTP client for the Algod node.</param>
        public AlgodClient(System.Net.Http.HttpClient httpClient) : base(httpClient)
        {
        }
    }

    /// <summary>
    /// Algod API interface - use IAlgodClient for new code.
    /// </summary>
    public interface IAlgodClient : IDefaultApi
    {
    }
}
