using System;
using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Domain;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Domain;

public class ProductRepositoryTest
{
  [Theory]
  [InlineData("https://www.coles.com.au/product/123")]
  [InlineData("https://www.woolworths.com.au/shop/storelocator/123")]
  public async Task FindProductByUrl_WithUnsupportedUrl_ThrowsNotSupportedException(string url)
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var repository = new ProductRepository(client);

    // Action
    var exception = await Record.ExceptionAsync(() => repository.FindProductByUri(new Uri(url)));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<DataValidationException>(exception);
    Assert.Equal($"Received unsupported URL: {url}", exception.Message);
  }

  [Theory]
  [InlineData("https://woolworths.com.au/shop/productdetails/123")]
  [InlineData("https://Woolworths.com.au/shop/productdetails/123")]
  [InlineData("https://www.woolworths.com.au/shop/productdetails/123")]
  [InlineData("https://www.woolworths.com.au/Shop/ProductDetails/123")]
  [InlineData("https://www.woolworths.com.au/shop/productdetails/123?googleshop=true")]
  [InlineData("https://www.woolworths.com.au/shop/productdetails/123?googleshop=true&utm_source=google")]
  public async Task FindProductByUrl_WithValidUrl_ReturnsProductFromApiClient(string url)
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
    var result = await repository.FindProductByUri(new Uri(url));

    // Assert
    Assert.Equal(result, product);
  }
}
