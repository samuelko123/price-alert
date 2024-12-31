using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Domain;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Officeworks;

namespace PriceAlert.UnitTests.Domain;

public class ProductRepositoryTest
{
  [Theory]
  [InlineData("https://www.coles.com.au/product/123")]
  [InlineData("https://www.officeworks.com.au/shop/officeworks/c/education/student-stationery/exercise-books")]
  public async Task FindProductByUrl_WithInvalidUrl_ThrowsDataValidationException(string url)
  {
    // Arrange
    var client = A.Fake<IOfficeworksApiClient>();
    var repository = new ProductRepository(client);

    // Action
    var exception = await Record.ExceptionAsync(() => repository.FindProductByUrl(url));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<DataValidationException>(exception);
    Assert.Equal("The url is invalid. It should start with 'https://www.officeworks.com.au/shop/officeworks/p/'.", exception.Message);
  }

  [Theory]
  [InlineData("https://www.officeworks.com.au/shop/officeworks/p/some-sort-of-product-abcd1234")]
  [InlineData("https://www.OFFICEWORKS.com.au/shop/officeworks/p/some-sort-of-product-abcd1234")]
  [InlineData("https://www.officeworks.com.au/shop/officeworks/p/some-sort-of-product-abcd1234?query=nothing&page=1")]
  public async Task FindProductByUrl_WithValidUrl_ReturnsProduct(string url)
  {
    // Arrange
    var client = A.Fake<IOfficeworksApiClient>();
    var productDto = new OfficeworksProductDto()
    {
      Sku = "ABCD1234",
      Name = "a product name",
      MainImageSource = "//s3-ap-southeast-2.amazonaws.com/an-image"
    };
    A.CallTo(() => client.GetProduct("ABCD1234")).Returns(productDto);
    var priceDto = new OfficeworksProductPriceDto()
    {
      PriceInCents = 1000,
    };
    A.CallTo(() => client.GetProductPrice("ABCD1234")).Returns(priceDto);

    var repository = new ProductRepository(client);

    // Action
    var product = await repository.FindProductByUrl(url);

    // Assert
    Assert.Equal("ABCD1234", product.Sku);
    Assert.Equal("a product name", product.Name);
    Assert.Equal("https://s3-ap-southeast-2.amazonaws.com/an-image", product.MainImage.Source);
    Assert.Equal(1000, product.PriceInCents);
  }
}
