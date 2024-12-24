using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerTest
{
  [Fact]
  public async Task Search_ReturnsOk()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.PostAsync("/api/products/search", new StringContent(string.Empty));

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
