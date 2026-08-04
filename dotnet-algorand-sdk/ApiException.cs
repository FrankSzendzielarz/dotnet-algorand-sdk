namespace Algorand
{
    // Hand-patched: unlike the raw NSwag/client.vm generated default, Message/ToString()
    // deliberately do NOT embed the raw HTTP response body. Some endpoints this exception
    // type is thrown for (e.g. Kmd's ExportKeyAsync/ExportMasterKeyAsync) can return private
    // key material in that body, and Message/ToString() text is what conventionally ends up
    // in application logs. The full body remains available via the Response property for
    // callers who deliberately want to inspect it. If this file is ever regenerated from
    // scratch, re-apply this redaction.
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.14.5.0 (NJsonSchema v10.5.2.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: " + ((response == null) ? "(null)" : response.Length + " character(s) - see the Response property (body redacted from this message to avoid leaking sensitive response content, e.g. exported KMD key material, into logs)"), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: [{0} character(s) - see the Response property; body redacted here to avoid leaking sensitive response content into logs]\n\n{1}", Response == null ? 0 : Response.Length, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.14.5.0 (NJsonSchema v10.5.2.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
        /// <summary>
        /// Pretty print the error
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Result is Algod.Model.ErrorResponse err)
            {
                return $"{err.Message} : {err.Data}";
            }
            return Result.ToString();
        }
    }
}
