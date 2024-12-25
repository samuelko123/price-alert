using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PriceAlert.API.Exceptions;
using PriceAlert.Domain;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerIntegrationTest
{
  [Fact]
  public async Task GetByUrl_WithoutProductUrl_ReturnsBadRequest()
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
  public async Task GetByUrl_WithProductNotFoundException_ReturnsStatusNotFound()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUri(A<Uri>._)).ThrowsAsync(new ProductNotFoundException("123"));

    using var factory = new BaseWebApplicationFactory()
      .WithWebHostBuilder(builder =>
      {
        builder.ConfigureServices(services =>
        {
          services.Replace(new ServiceDescriptor(typeof(IProductRepository), repository));
        });
      });
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://www.woolworths.com.au/shop/productdetails/123");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    Assert.Equal("""{"error":"Unable to find product: 123"}""", await response.Content.ReadAsStringAsync());
  }

  [Fact]
  public async Task GetByUrl_WithProductUrl_ReturnsOk()
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
