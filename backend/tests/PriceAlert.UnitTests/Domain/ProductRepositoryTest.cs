using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Domain;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Domain;

public class ProductRepositoryTest
{
  [Fact]
  public async Task FindProduct_ReturnsProductAsync()
  {
    // Arrange
    var client = A.Fake<IWoolworthsApiClient>();
    var repository = new ProductRepository(client);

    // Action
    var product = await repository.FindProductById("123");

    // Assert
    Assert.Equal("123", product.Id);
    Assert.Equal("a product name", product.Name);
  }
}
