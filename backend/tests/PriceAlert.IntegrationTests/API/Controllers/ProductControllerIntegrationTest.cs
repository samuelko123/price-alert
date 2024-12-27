using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PriceAlert.Domain;
using PriceAlert.Domain.Exceptions;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerIntegrationTest
{
  [Fact]
  public async Task GetByUrl_WithoutUrl_ReturnsBadRequest()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=");

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":400", content);
    Assert.Contains("\"title\":\"One or more validation errors occurred.\"", content);
    Assert.Contains("\"errors\":[{\"message\":\"The url field is required.\"}]", content);
  }

  [Fact]
  public async Task GetByUrl_WithInvalidProductUrl_ReturnsBadRequest()
  {
    // Arrange
    using var factory = new BaseWebApplicationFactory();
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=it is not a url");

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":400", content);
    Assert.Contains("\"title\":\"One or more validation errors occurred.\"", content);
    Assert.Contains("\"errors\":[{\"message\":\"The url field is invalid.\"}]", content);
  }

  [Fact]
  public async Task GetByUrl_WithDataValidationException_ReturnsBadRequest()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUri(A<Uri>._)).ThrowsAsync(new DataValidationException("The data is wrong."));

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://google.com");

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":400", content);
    Assert.Contains("\"title\":\"One or more validation errors occurred.\"", content);
    Assert.Contains("\"errors\":[{\"message\":\"The data is wrong.\"}]", content);
  }

  [Fact]
  public async Task GetByUrl_WithProductUrl_ReturnsOk()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUri(A<Uri>._)).Returns(new Product()
    {
      Id = "123",
      Name = "a product",
    });

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://www.woolworths.com.au/shop/productdetails/123");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"sku\":\"123\"", content);
    Assert.Contains("\"name\":\"a product\"", content);
  }

  [Fact]
  public async Task GetByUrl_WithProductNotFoundException_ReturnsStatusNotFound()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUri(A<Uri>._)).ThrowsAsync(new ItemNotFoundException("We cannot find it!"));

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://www.woolworths.com.au/shop/productdetails/123");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":404", content);
    Assert.Contains("\"title\":\"Item cannot be found.\"", content);
    Assert.Contains("\"errors\":[{\"message\":\"We cannot find it!\"}]", content);
  }

  [Fact]
  public async Task GetByUrl_WithException_ReturnsSomethingWentWrong()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUri(A<Uri>._)).ThrowsAsync(new Exception());

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
    Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":500", content);
    Assert.Contains("\"title\":\"An error occurred while processing your request.\"", content);
  }
}
