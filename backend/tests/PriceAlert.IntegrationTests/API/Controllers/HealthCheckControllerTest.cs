using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class HealthCheckControllerTest
{
  [Fact]
  public async Task GetProduct_ReturnsProduct()
  {
    // Arrange
    var app = new WebApplicationFactory<Program>();
    var client = app.CreateClient();

    // Action
    var response = await client.GetAsync("/api/healthcheck");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
