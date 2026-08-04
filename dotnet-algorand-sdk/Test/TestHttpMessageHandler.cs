using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Algorand.Algod.Test
{
    public class TestHttpMessageHandler : DelegatingHandler 
    {
        public static HttpRequestMessage LastRequest { get; set; }

        public static HttpResponseMessage NextResponse { get; set; }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            if (NextResponse == null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }
            else
            {
                return NextResponse;
            }
        }
    }
}
