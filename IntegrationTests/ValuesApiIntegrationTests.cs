using FluentAssertions;
using IntegrationTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ValuesApiIntegrationTests : IClassFixture<WebApplicationFactory_old>
    {
        private readonly WebApplicationFactory_old _factory;

        public ValuesApiIntegrationTests(WebApplicationFactory_old factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_Get_All()
        {
            //Act
            var response = await _factory.Client.GetAsync("api/values");

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var contentResponse = await response.Content.ReadAsStringAsync();
            contentResponse.Should().Be("[\"value1\",\"value2\"]");
        }


    }
}
