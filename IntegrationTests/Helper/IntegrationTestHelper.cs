using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace IntegrationTests
{
    public class IntegrationTestHelper
    {
        public HttpClient Client { get; private set; }

        public IntegrationTestHelper()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            //var a1 = configuration.GetConnectionString("smap_IT_database");
            bool.TryParse(configuration.GetSection("AppSettings:UseRemote").Value, out bool useRemote);
            if (!useRemote)
                CreateClient<WebApplication1.Api.Startup>();
            else
            {
                var remoteServerAddress = configuration
                    .GetSection("AppSettings:RemoteServerAddress")
                    .Value;
                CreateClient(remoteServerAddress);
            }
        }

        private void CreateClient(string url)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void CreateClient<T>()
            where T : class
        {
            var builder = new Microsoft.AspNetCore.Hosting.WebHostBuilder()
                .UseStartup<T>();
            var server = new Microsoft.AspNetCore.TestHost.TestServer(builder);
            Client = server.CreateClient();
        }
    }
}
