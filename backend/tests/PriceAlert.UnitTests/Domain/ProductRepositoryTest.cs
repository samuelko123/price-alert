using System;
using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Domain;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Domain;

public class ProductRepositoryTest
{
  [Fact]
  public async Task FindProductByUrl_WithUnsupportedHostname_ThrowsNotSupportedException()
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var repository = new ProductRepository(client);

    // Action
    var exception = await Record.ExceptionAsync(() => repository.FindProductByUrl("https://www.coles.com.au/product/123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<NotSupportedException>(exception);
    Assert.Equal("Received unsupported hostname: www.coles.com.au", exception.Message);
  }

  [Fact]
  public async Task FindProductByUrl_WithUnexpectedUriPath_ThrowsNotSupportedException()
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var repository = new ProductRepository(client);

    // Action
    var exception = await Record.ExceptionAsync(() => repository.FindProductByUrl("https://www.woolworths.com.au/shop/storelocator/123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<NotSupportedException>(exception);
    Assert.Equal("Received unsupported uri path: /shop/storelocator/123", exception.Message);
  }

  [Fact]
  public async Task FindProductByUrl_ReturnsProductFromApiClient()
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var product = new Product()
    {
      Id = "123",
      Name = "a product name",
    };
    A.CallTo(() => client.GetProduct("123")).Returns(product);

    var repository = new ProductRepository(client);

    // Action
    var result = await repository.FindProductByUrl("https://www.woolworths.com.au/shop/productdetails/123");

    // Assert
    Assert.Equal(result, product);
  }
}
