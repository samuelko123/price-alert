using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class HealthCheckControllerTest
{
  [Fact]
  public async Task Get_ReturnsOk()
  {
    // Arrange
    var factory = new WebApplicationFactory<Program>();
    var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/healthcheck");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
