using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerIntegrationTest
{
  [Fact]
  public async Task Search_WithoutProductUrl_ReturnsBadRequest()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=");

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    Assert.Contains("The url field is required.", await response.Content.ReadAsStringAsync());
  }

  [Fact]
  public async Task Search_WithProductUrl_ReturnsOk()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://www.woolworths.com.au/shop/productdetails/123");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
