using IntegrationTests.Fixture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using WebApplication1.Api;

namespace IntegrationTests.Fixtures
{
    public class WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public HttpClient Client { get; set; }

        private TestServer _server;
        private IWebHostBuilder _builder;

        public WebApplicationFactory()
        {
            CreateBuilder();
        }

        private void CreateBuilder()
        {
            var builder = new WebHostBuilder()
                //.UseEnvironment("Development")
                .UseStartup<TStartup>();

            var a1 = builder.GetSetting("AppSettings");
            var a2 = builder.GetSetting("UseRemote");
            var a3 = builder.GetSetting("ConnectionStrings");
            var a4 = builder.GetSetting("smap_IT_database");
            _server = new TestServer(builder);
            var a5 = builder.GetSetting("smap_IT_database");

            var x = _server.BaseAddress;
            Client = _server.CreateClient();

            _builder = builder;


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/integration_api.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }

    public class WebApplicationFactory_old : IDisposable
    {
        public HttpClient Client { get; set; }

        private TestServer _server;
        private IWebHostBuilder _builder;

        public WebApplicationFactory_old()
        {
            CreateBuilder();            
        }

        public void CreateBuilder()
        {
            var builder = new WebHostBuilder()
                //.UseEnvironment("Development")
                //.UseStartup<TestStartup>();
                .UseStartup<Startup>();

            var a1 = builder.GetSetting("AppSettings");
            var a2 = builder.GetSetting("UseRemote");
            var a3 = builder.GetSetting("ConnectionStrings");
            var a4 = builder.GetSetting("smap_IT_database");
            _server = new TestServer(builder);
            var a5 = builder.GetSetting("smap_IT_database");

            var x = _server.BaseAddress;
            Client = _server.CreateClient();

            _builder = builder;
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }

        public void SetClientBaseAddress(string address)
        {
            var a1 = _builder.GetSetting("AppSettings");
            var a2 = _builder.GetSetting("UseRemote");
            var a3 = _builder.GetSetting("ConnectionStrings");
            var x = Client.BaseAddress;

            Client.BaseAddress = new Uri(address);

        }
    }
}
