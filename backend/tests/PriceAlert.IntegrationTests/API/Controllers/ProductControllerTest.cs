using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerTest
{
  [Fact]
  public async Task Search_WithEmptyBody_ReturnsBadRequest()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.PostAsync("/api/products/search", new StringContent("{}", Encoding.UTF8, "application/json"));

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Fact]
  public async Task Search_WithValidBody_ReturnsOk()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.PostAsync("/api/products/search", new StringContent("""{"url": "https://google.com"}""", Encoding.UTF8, "application/json"));

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
