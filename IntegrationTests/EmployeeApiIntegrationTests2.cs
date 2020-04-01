using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.domain;
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
       
        //// http://localhost:63161/api/values/employee/1
        //[Fact]
        //public async Task GetNameById_WhenNameExists_ReturnsCorrectResult()
        //{
        //    //Arrange
        //    var id = 1;
        //    var expectedemployeeName = "name 01";

        //    //Act
        //    var response = await _client.GetAsync($"api/employee/GetNameById/{id}");

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
        //            JsonConvert.SerializeObject(new Employee(employeeName))
        //            , Encoding.UTF8
        //            , "application/json");
        //    //Act
        //    var response = await _client.PostAsync("/api/employee/Insert", sc);

        //    //Assert
        //    response.EnsureSuccessStatusCode();
        //    response.StatusCode.Should().Be(HttpStatusCode.Created);
        //    Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        //    var contentResponse = await response.Content.ReadAsStringAsync();
        //    contentResponse.Should().Be("{\"name\":\"" + employeeName + "\"}");
        //}

        //[Fact]
        //public async Task InsertEmployee_WhenEmployeeNameExists_ThrowsError()
        //{
        //    //Arrange
        //    var employeeName = "name 01";
        //    var sc = new StringContent(
        //            JsonConvert.SerializeObject(new Employee(employeeName))
        //            , Encoding.UTF8
        //            , "application/json");

        //    //Act+Assert
        //    var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _client.PostAsync("/_client/employee/Insert", sc));
        //    Assert.Equal("Employee already exists", exception.Message);
        //}
    }
}
