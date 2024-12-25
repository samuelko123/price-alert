using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerIntegrationTest
{
  [Fact]
  public async Task Search_WithMissingRequiredProperty_ReturnsBadRequest()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.PostAsync("/api/products/getByUrl", new StringContent("{}", Encoding.UTF8, "application/json"));

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    Assert.Equal("""{"error":"missing required property: 'url'."}""", await response.Content.ReadAsStringAsync());
  }

  [Fact]
  public async Task Search_WithValidBody_ReturnsOk()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.PostAsync("/api/products/getByUrl", new StringContent("""{"url": "https://www.woolworths.com.au/shop/productdetails/123"}""", Encoding.UTF8, "application/json"));

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
