using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.Domain;
using PriceAlert.Domain.Exceptions;
using PriceAlert.IntegrationTests.Fixtures;

namespace PriceAlert.IntegrationTests.API.Controllers;

public class ProductControllerIntegrationTest
{
  [Fact]
  public async Task GetByUrl_WithoutUrl_Returns400()
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
  public async Task GetByUrl_WithDataValidationException_Returns400()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUrl(A<string>._)).ThrowsAsync(new DataValidationException("The url is invalid."));

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://google.com/");

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":400", content);
    Assert.Contains("\"title\":\"One or more validation errors occurred.\"", content);
    Assert.Contains("\"errors\":[{\"message\":\"The url is invalid.\"}]", content);
  }

  [Fact]
  public async Task GetByUrl_WithItemNotFoundException_Returns404()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUrl(A<string>._)).ThrowsAsync(new ItemNotFoundException("We cannot find it!"));

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://google.com/");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":404", content);
    Assert.Contains("\"title\":\"Item cannot be found.\"", content);
    Assert.Contains("\"errors\":[{\"message\":\"We cannot find it!\"}]", content);
  }

  [Fact]
  public async Task GetByUrl_WithGenericException_Returns500()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUrl(A<string>._)).ThrowsAsync(new Exception());

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://google.com/");

    // Assert
    Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"status\":500", content);
    Assert.Contains("\"title\":\"An error occurred while processing your request.\"", content);
  }

  [Fact]
  public async Task GetByUrl_WithoutExceptions_Returns200()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    A.CallTo(() => repository.FindProductByUrl(A<string>._)).Returns(new Product()
    {
      Sku = "123",
      Name = "a product",
      MainImage = new Image()
      {
        Source = "//s3-ap-southeast-2.amazonaws.com/an-image",
      },
      PriceInCents = 123,
    });

    using var factory = new BaseWebApplicationFactory([new ServiceDescriptor(typeof(IProductRepository), repository)]);
    using var client = factory.CreateClient();

    // Action
    var response = await client.GetAsync("/api/products/getByUrl?url=https://google.com/");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("\"sku\":\"123\"", content);
    Assert.Contains("\"name\":\"a product\"", content);
    Assert.Contains("\"mainImage\":{\"src\":\"//s3-ap-southeast-2.amazonaws.com/an-image\"}", content);
    Assert.Contains("\"priceInCents\":123", content);
  }
}
