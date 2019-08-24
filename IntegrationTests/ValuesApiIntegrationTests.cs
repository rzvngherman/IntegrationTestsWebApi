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
    public class ValuesApiIntegrationTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _factory;

        public ValuesApiIntegrationTests(WebApplicationFactory factory)
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
