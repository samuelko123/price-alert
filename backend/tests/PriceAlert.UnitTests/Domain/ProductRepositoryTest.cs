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
  public async Task FindProductByUrl_WithInvalidUrl_ThrowsDataValidationException(string url)
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var repository = new ProductRepository(client);

    // Action
    var exception = await Record.ExceptionAsync(() => repository.FindProductByUrl(url));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<DataValidationException>(exception);
    Assert.Equal("The url is invalid. It should start with 'https://www.woolworths.com.au/shop/productdetails/'.", exception.Message);
  }

  [Theory]
  [InlineData("https://www.woolworths.com.au/shop/productdetails/123")]
  [InlineData("https://www.WOOLWORTHS.com.au/Shop/productDetails/123")]
  [InlineData("https://www.woolworths.com.au/shop/productdetails/123?googleshop=true&utm_source=google")]
  public async Task FindProductByUrl_WithValidUrl_ReturnsProduct(string url)
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var dto = new WoolworthsProductDto()
    {
      Sku = "123",
      Name = "a product name",
    };
    A.CallTo(() => client.GetProduct("123")).Returns(dto);

    var repository = new ProductRepository(client);

    // Action
    var product = await repository.FindProductByUrl(url);

    // Assert
    Assert.Equal("123", product.Sku);
    Assert.Equal("a product name", product.Name);
  }

  [Fact]
  public async void FindProductByUrl_WithEmptyProductName_ThrowsItemNotFoundException()
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var dto = new WoolworthsProductDto()
    {
      Sku = "123",
      Name = "     ",
    };
    A.CallTo(() => client.GetProduct("123")).Returns(dto);

    var repository = new ProductRepository(client);

    // Action
    var exception = await Record.ExceptionAsync(() => repository.FindProductByUrl("https://www.woolworths.com.au/shop/productdetails/123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<ItemNotFoundException>(exception);
    Assert.Equal("Unable to find product: 123", exception.Message);
  }
}
