using System;
using System.Net.Http;
using System.Threading;
using Algorand.Algod;

namespace Algorand
{
    public class HttpClientConfigurator
    {
        /// <summary>
        /// Configures and returns an <see cref="HttpClient"/> instance for interacting with an Algod API.
        /// </summary>
        /// <param name="algodConfiguration">The configuration object containing the host, API key, and optional headers for the Algod API.</param>
        /// <param name="timeout">The timeout duration, in milliseconds, for the HTTP client.  A value of -1 indicates that the default
        /// timeout will be used.</param>
        /// <param name="shim">An optional <see cref="HttpMessageHandler"/> to use for customizing the HTTP client's behavior,  such as for
        /// testing or adding custom processing logic. If null, the default handler is used.</param>
        /// <returns>A configured <see cref="HttpClient"/> instance ready to communicate with the Algod API.</returns>
        public static HttpClient ConfigureHttpClient(AlgodConfiguration algodConfiguration, int timeout = -1, HttpMessageHandler shim = null)
        {
            return ConfigureHttpClient(algodConfiguration.Host, algodConfiguration.ApiKey, algodConfiguration.Header, timeout, shim);
        }
        /// <summary>
        /// Configures and returns an <see cref="HttpClient"/> instance with the specified settings.
        /// </summary>
        /// <remarks>If the <paramref name="tokenHeader"/> is not provided, a default header name is
        /// selected based on the host. Additionally, if the base address does not end with a trailing slash, one is
        /// appended automatically.</remarks>
        /// <param name="host">The base address for the HTTP client. Must be an absolute URI.</param>
        /// <param name="token">The authentication token to include in the default request headers.</param>
        /// <param name="tokenHeader">The name of the header to use for the authentication token. If not specified, a default value is determined
        /// based on the host.</param>
        /// <param name="timeout">The timeout duration, in milliseconds, for HTTP requests. A value of -1 indicates no timeout (infinite).</param>
        /// <param name="shim">An optional <see cref="HttpMessageHandler"/> to use for the <see cref="HttpClient"/>. If not provided, a
        /// default handler is used.</param>
        /// <returns>A configured <see cref="HttpClient"/> instance with the specified base address, authentication token, and
        /// timeout.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="host"/> is not an absolute URI.</exception>
        public static HttpClient ConfigureHttpClient(string host, string token, string tokenHeader = "", int timeout = -1, HttpMessageHandler shim=null)
        {
#if TEST_DEBUG
            HttpClient _httpClient = new HttpClient(new TestHttpMessageHandler());
#else
            
            HttpClient _httpClient;
            if (shim != null)
                _httpClient = new HttpClient(shim);
            else
                _httpClient = new HttpClient();
           
#endif

            _httpClient.BaseAddress = new Uri(host);
            if (!_httpClient.BaseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException("Host must be an absolute path.");
            }
            else
            {
                if (!_httpClient.BaseAddress.AbsolutePath.EndsWith("/"))
                {
                    UriBuilder uriBuilder = new UriBuilder(_httpClient.BaseAddress);
                    uriBuilder.Path = _httpClient.BaseAddress.AbsolutePath + "/";
                    _httpClient.BaseAddress = uriBuilder.Uri;
                }
            }
            

            if (string.IsNullOrEmpty(tokenHeader))
            {
                if (host.Contains("algorand.api.purestake.io") || host.Contains("bsngate.com/api"))
                {
                    tokenHeader = "X-API-Key";
                }
                else
                {
                    tokenHeader = "X-Algo-API-Token";
                }
            }

            if (tokenHeader != null && tokenHeader.Length > 0)
                _httpClient.DefaultRequestHeaders.Add(tokenHeader, token);

            _httpClient.Timeout = timeout > 0 ? (TimeSpan.FromMilliseconds((double)timeout)) : Timeout.InfiniteTimeSpan;

            return _httpClient;
        }
    }
}
