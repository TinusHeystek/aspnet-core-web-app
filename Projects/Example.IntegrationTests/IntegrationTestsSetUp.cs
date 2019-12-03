using System.Net.Http;
using Example.App;
using Example.IntegrationTests._SetUp;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Example.IntegrationTests
{
    [SetUpFixture]
    public class IntegrationTestsSetUp
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var appFactory = new CustomWebApplicationFactory<Startup>();
            appFactory.Server.SetGlobalFlurlClient();
        }
    }

    public static class TestServerExtensions
    {
        public static void SetGlobalFlurlClient(this TestServer server)
        {
            var client = server.CreateClient();
            ApiRoute.SetBase(client.BaseAddress.AbsoluteUri);
            FlurlHttp.Configure(settings => settings.HttpClientFactory = new TestHttpClientFactory(new FlurlClient(client)));
        }
    }

    public class TestHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly IFlurlClient _flurlClient;

        public TestHttpClientFactory(IFlurlClient flurlClient)
        {
            _flurlClient = flurlClient;
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return _flurlClient.HttpClient;
        }
    
        public override HttpMessageHandler CreateMessageHandler()
        {
            return _flurlClient.HttpMessageHandler;
        }
    }
}
