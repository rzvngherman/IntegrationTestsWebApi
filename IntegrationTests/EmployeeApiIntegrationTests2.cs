using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests._2
{
    public class EmployeeApiIntegrationTests2
    {
        protected HttpClient _client;

        public EmployeeApiIntegrationTests2()
        {
            var helper = new IntegrationTestHelper();
            _client = helper.Client;
        }

        // http://localhost/integration_api.com/api/Values
        [Fact]
        public async Task GetAsync_apiValues()
        {
            //Act
            var response = await _client.GetAsync("api/Values");
            var contentResponse = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
