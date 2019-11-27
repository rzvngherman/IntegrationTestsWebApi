using FluentAssertions;
using IntegrationTests.Fixtures;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data;
using Xunit;

namespace IntegrationTests.Scenarios
{
    //public class EmployeeApiIntegrationTests : IClassFixture<WebApplicationFactory<WebApplication1.Api.Startup>>
    public class EmployeeApiIntegrationTests_old : IClassFixture<WebApplicationFactory<Fixture.TestStartup>>
    {
        private readonly WebApplicationFactory<Fixture.TestStartup> _factory;

        public EmployeeApiIntegrationTests_old(WebApplicationFactory<Fixture.TestStartup> factory)
        {
            _factory = factory;

            //var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            //                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //Microsoft.Extensions.Configuration.IConfigurationRoot configuration = builder.Build();
            //var a1 = configuration.GetConnectionString("smap_IT_database");
            //var a2 = configuration.GetSection("AppSettings:UseRemote").Value;

        }

        // http://localhost/integration_api.com/api/Values
        [Fact]
        public async Task x2_GetNameById_WhenNameExists_ReturnsCorrectResult()
        {            
            //2
            //Arrange
            //_factory.Client.BaseAddress = new Uri("http://localhost/integration_api.com");
            //Act
            var response = await _factory.Client.GetAsync("api/Values");

            //Assert
            var contentResponse = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            //contentResponse.Should().Be("{\"name\":\"" + expectedemployeeName + "\"}");
        }

        //// http://localhost:63161/api/values/employee/1
        //[Fact]
        //public async Task GetNameById_WhenNameExists_ReturnsCorrectResult()
        //{
        //    //1
        //    //HttpClient client = new HttpClient();
        //    //client.BaseAddress = new Uri("http://localhost/integration_api.com/");
        //    //client.DefaultRequestHeaders.Accept.Clear();
        //    //client.DefaultRequestHeaders.Accept.Add(
        //    //    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    //var res2 = await client.GetAsync("api/Values");
        //    //var contentResponse1 = await res2.Content.ReadAsStringAsync();

        //    //2
        //    //Arrange
        //    var id = 1;
        //    var expectedemployeeName = "name 01";

        //    //Act
        //    var response = await _factory.Client.GetAsync($"api/employee/GetNameById/{id}");

        //    //Assert
        //    response.EnsureSuccessStatusCode();
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //    var contentResponse = await response.Content.ReadAsStringAsync();
        //    contentResponse.Should().Be("{\"name\":\"" + expectedemployeeName + "\"}");
        //}

        //[Fact]
        //public async Task InsertEmployee_WhenEmployeeNameNotExists_ReturnsCreatedResult()
        //{
        //    //Arrange
        //    var employeeName = "name 04"; //"name_" + Guid.NewGuid();
        //    var sc = new StringContent(
        //            JsonConvert.SerializeObject(new Employee { Name = employeeName })
        //            , Encoding.UTF8
        //            , "application/json");
        //    //Act
        //    var response = await _factory.Client.PostAsync("/api/employee/Insert", sc);

        //    //Assert
        //    response.EnsureSuccessStatusCode();
        //    response.StatusCode.Should().Be(HttpStatusCode.Created);
        //    Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        //    var contentResponse = await response.Content.ReadAsStringAsync();
        //    contentResponse.Should().Be("{\"name\":\""+ employeeName + "\"}");
        //}

        //[Fact]
        //public async Task InsertEmployee_WhenEmployeeNameExists_ThrowsError()
        //{
        //    //Arrange
        //    var employeeName = "name 01";
        //    var sc = new StringContent(
        //            JsonConvert.SerializeObject(new Employee { Name = employeeName })
        //            , Encoding.UTF8
        //            , "application/json");

        //    //Act+Assert
        //    var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _factory.Client.PostAsync("/api/employee/Insert", sc));
        //    Assert.Equal("Employee already exists", exception.Message);
        //}
    }
}
