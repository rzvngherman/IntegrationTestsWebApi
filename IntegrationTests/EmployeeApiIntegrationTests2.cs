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
