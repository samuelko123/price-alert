using System.Net;
using System.Threading.Tasks;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class HealthCheckControllerTest
{
  [Fact]
  public async Task Get_ReturnsOk()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory<Program>();
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/healthcheck");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
