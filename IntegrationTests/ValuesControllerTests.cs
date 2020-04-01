using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ValuesControllerTests
    {
        protected HttpClient _client;
        protected string GET_API_VALUES;
        protected string GET_API_VALUES_BY_ID;

        public ValuesControllerTests()
        {
            var helper = new IntegrationTestHelper();
            GET_API_VALUES = helper.EnvironmentConstant.GET_API_VALUES;
            GET_API_VALUES_BY_ID = helper.EnvironmentConstant.GET_API_VALUES_BY_ID;

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

            contentResponse.Should().Be(GET_API_VALUES);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task GetAsync_apiValuesById(int id)
        {
            //Act
            var response = await _client.GetAsync($"api/Values/{id}");
            var contentResponse = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            contentResponse.Should().Be(GET_API_VALUES_BY_ID + id);
        }
    }
}
