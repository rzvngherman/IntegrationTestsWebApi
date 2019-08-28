using FluentAssertions;
using IntegrationTests.Fixtures;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data;
using Xunit;

namespace IntegrationTests.Scenarios
{
    public class EmployeeApiIntegrationTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _factory;

        public EmployeeApiIntegrationTests(WebApplicationFactory factory)
        {
            _factory = factory;
            //assume that we already have one employee in database: name 'name 01'
        }

        // http://localhost:63161/api/values/employee/1
        [Fact]
        public async Task GetNameById_WhenNameExists_ReturnsCorrectResult()
        {
            //Arrange
            var id = 1;
            var expectedemployeeName = "name 01";

            //Act
            var response = await _factory.Client.GetAsync($"api/employee/GetNameById/{id}");

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var contentResponse = await response.Content.ReadAsStringAsync();
            contentResponse.Should().Be("{\"name\":\"" + expectedemployeeName + "\"}");
        }

        [Fact]
        public async Task InsertEmployee_WhenEmployeeNameNotExists_ReturnsCreatedResult()
        {
            //Arrange
            var employeeName = "name_" + Guid.NewGuid();
            var sc = new StringContent(
                    JsonConvert.SerializeObject(new Employee { Name = employeeName })
                    , Encoding.UTF8
                    , "application/json");
            //Act
            var response = await _factory.Client.PostAsync("/api/employee/Insert", sc);

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            var contentResponse = await response.Content.ReadAsStringAsync();
            contentResponse.Should().Be("{\"name\":\""+ employeeName + "\"}");
        }

        [Fact]
        public async Task InsertEmployee_WhenEmployeeNameExists_ThrowsError()
        {
            //Arrange
            var employeeName = "name 01";
            var sc = new StringContent(
                    JsonConvert.SerializeObject(new Employee { Name = employeeName })
                    , Encoding.UTF8
                    , "application/json");

            //Act+Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _factory.Client.PostAsync("/api/employee/Insert", sc));
            Assert.Equal("Employee already exists", exception.Message);
        }
    }
}
