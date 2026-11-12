using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    /// <summary>
    /// Non-success HTTP responses do not always carry the daemon's JSON ErrorResponse - a proxy, WAF or rate
    /// limiter in front of the node answers with plain text or HTML. The client must report those as an
    /// ApiException carrying the real status code and the raw body, not as a JSON deserialization failure
    /// with the body thrown away.
    /// </summary>
    [TestFixture]
    public class ErrorResponseHandlingTests
    {
        private sealed class StubHandler : HttpMessageHandler
        {
            private readonly Func<HttpResponseMessage> _factory;
            public StubHandler(Func<HttpResponseMessage> factory) { _factory = factory; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => Task.FromResult(_factory());
        }

        private static DefaultApi CreateAlgod(Func<HttpResponseMessage> factory, bool readResponseAsString)
        {
            var http = new HttpClient(new StubHandler(factory)) { BaseAddress = new Uri("http://localhost:4001/") };
            return new DefaultApi(http) { ReadResponseAsString = readResponseAsString };
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task PlainTextForbiddenIsReportedWithStatusAndBody(bool readResponseAsString)
        {
            var algod = CreateAlgod(() => new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent("Denied by upstream policy", Encoding.UTF8, "text/plain")
            }, readResponseAsString);

            var ex = await Assert.ThrowsAsync<ApiException>(async () => await algod.GetBlockAsync(round: 66813789));

            Assert.That(ex.StatusCode, Is.EqualTo(403));
            Assert.That(ex.Response, Is.EqualTo("Denied by upstream policy"));
            Assert.That(ex.Message, Does.Contain("HTTP 403"));
            Assert.That(ex.Message, Does.Contain("text/plain"));
            Assert.That(ex.Message, Does.Not.Contain("Could not deserialize"));
            // the body stays out of Message/ToString() (see ApiExceptionTests)
            Assert.That(ex.Message, Does.Not.Contain("Denied by upstream policy"));
            Assert.That(ex.ToString(), Does.Not.Contain("Denied by upstream policy"));
        }

        [Test]
        public async Task HtmlErrorPageWithoutContentTypeIsReportedWithStatusAndBody()
        {
            const string html = "<html><body><h1>503 Service Temporarily Unavailable</h1></body></html>";
            var algod = CreateAlgod(() =>
            {
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes(html));
                content.Headers.ContentType = null;
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable) { Content = content };
            }, false);

            var ex = await Assert.ThrowsAsync<ApiException>(async () => await algod.GetBlockAsync(round: 1));

            Assert.That(ex.StatusCode, Is.EqualTo(503));
            Assert.That(ex.Response, Is.EqualTo(html));
            Assert.That(ex.Message, Does.Contain("HTTP 503"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task JsonErrorResponseIsStillTyped(bool readResponseAsString)
        {
            const string json = "{\"message\":\"failed to retrieve information from the ledger\"}";
            var algod = CreateAlgod(() => new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            }, readResponseAsString);

            var ex = await Assert.ThrowsAsync<ApiException<ErrorResponse>>(async () => await algod.GetBlockAsync(round: 1));

            Assert.That(ex.StatusCode, Is.EqualTo(404));
            Assert.That(ex.Result.Message, Is.EqualTo("failed to retrieve information from the ledger"));
            // the raw body is now preserved on the typed exception in both modes
            Assert.That(ex.Response, Is.EqualTo(json));
        }

        [Test]
        public async Task EmptyErrorBodyKeepsStatusCode()
        {
            var algod = CreateAlgod(() => new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new ByteArrayContent(Array.Empty<byte>())
            }, false);

            var ex = await Assert.ThrowsAsync<ApiException>(async () => await algod.GetBlockAsync(round: 1));

            Assert.That(ex.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task IndexerPlainTextErrorIsReportedWithStatusAndBody()
        {
            var http = new HttpClient(new StubHandler(() => new HttpResponseMessage((HttpStatusCode)429)
            {
                Content = new StringContent("Too Many Requests", Encoding.UTF8, "text/plain")
            }))
            { BaseAddress = new Uri("http://localhost:8980/") };
            var indexer = new Algorand.Indexer.LookupApi(http);

            var ex = await Assert.ThrowsAsync<ApiException>(async () => await indexer.lookupBlockAsync(1));

            Assert.That(ex.StatusCode, Is.EqualTo(429));
            Assert.That(ex.Response, Is.EqualTo("Too Many Requests"));
            Assert.That(ex.Message, Does.Contain("HTTP 429"));
        }
    }
}
