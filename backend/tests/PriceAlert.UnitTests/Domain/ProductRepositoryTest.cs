using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Domain;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Domain;

public class ProductRepositoryTest
{
  [Fact]
  public async Task FindProductByUrl_ParseUrlAndReturnsProductFromApiClient()
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
