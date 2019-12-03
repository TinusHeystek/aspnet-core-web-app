using System.Net.Http;
using Flurl.Http.Configuration;

namespace Example.Core.Extensions
{
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler() {
            return new HttpClientHandler {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            };
        }
    }
}