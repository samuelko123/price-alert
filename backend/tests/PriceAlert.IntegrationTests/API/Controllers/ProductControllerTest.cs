using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.API.Controllers;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerTest
{
  [Fact]
  public async Task Get_ReturnsOk()
  {
    // Arrange
    using var factory = new WebApplicationFactory<Program>()
      .WithWebHostBuilder(builder =>
      {
        builder.ConfigureServices(services =>
        {
          services.AddControllers().AddApplicationPart(typeof(HealthCheckController).Assembly);
        });
      });
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
