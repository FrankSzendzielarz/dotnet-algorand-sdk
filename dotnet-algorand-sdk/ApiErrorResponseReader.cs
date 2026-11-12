using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Algorand
{
    /// <summary>
    /// Shared reader for the body of a non-success HTTP response, used by the Algod and Indexer clients.
    ///
    /// Error responses do not always come from the Algorand daemon itself. A reverse proxy, WAF, API
    /// gateway or rate limiter in front of the node typically answers 401/403/429/502/503 with a plain
    /// text or HTML body ("Denied", "Too Many Requests", an HTML error page, ...) rather than the
    /// daemon's JSON <c>ErrorResponse</c>. Parsing such a body as JSON used to surface as a confusing
    /// "Could not deserialize the response body stream" exception with the body thrown away, hiding the
    /// real problem (the HTTP status). This reader always keeps the raw body, and reports a non-JSON
    /// body as what it is.
    /// </summary>
    internal static class ApiErrorResponseReader
    {
        /// <summary>
        /// True when the response is a non-success response whose body should be read through
        /// <see cref="ReadAsync{T}"/> (i.e. it is not msgpack, which the daemon never uses for errors
        /// but which the regular code path knows how to decode).
        /// </summary>
        internal static bool ShouldHandle(HttpResponseMessage response)
        {
            if (response == null || response.Content == null || response.IsSuccessStatusCode)
            {
                return false;
            }
            return response.Content.Headers?.ContentType?.MediaType != "application/msgpack";
        }

        /// <summary>
        /// Reads the error body as text and tries to deserialize it as <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The deserialized object (null for an empty body) and the raw body text.</returns>
        /// <exception cref="ApiException">
        /// The body is not valid JSON for <typeparamref name="T"/>. The exception carries the HTTP status
        /// code and the raw body in <see cref="ApiException.Response"/>; the body is deliberately not
        /// embedded in the exception message (see <see cref="ApiException"/>).
        /// </exception>
        internal static async Task<KeyValuePair<T, string>> ReadAsync<T>(
            HttpResponseMessage response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers,
            Newtonsoft.Json.JsonSerializerSettings settings)
        {
            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, settings);
                return new KeyValuePair<T, string>(typedBody, responseText);
            }
            catch (Newtonsoft.Json.JsonException exception)
            {
                var status = (int)response.StatusCode;
                var contentType = response.Content.Headers?.ContentType?.MediaType ?? "unknown";
                var message = "The server returned HTTP " + status + " (" + response.ReasonPhrase + ") with a body that is not a JSON "
                    + typeof(T).Name + " (content type: " + contentType + "). This usually means the request was rejected by a proxy, "
                    + "gateway or rate limiter in front of the node rather than by the node itself.";
                throw new ApiException(message, status, responseText, headers, exception);
            }
        }
    }
}
