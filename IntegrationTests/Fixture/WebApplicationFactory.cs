using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using WebApplication1.Api;

namespace IntegrationTests.Fixtures
{
    public class WebApplicationFactory : IDisposable
    {
        public HttpClient Client { get; set; }

        private TestServer _server;

        public WebApplicationFactory()
        {
            var builder = new WebHostBuilder()
                //.UseEnvironment("Development")
                .UseStartup<Startup>();

            _server = new TestServer(builder);
            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
