using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Infrastructure.Woolworths;

public class WoolworthsProductApiClientTest
{
  [Fact]
  public void GetProduct_ReturnsProduct()
  {
    // Arrange
    var client = new WoolworthsApiClient();

    // Action
    var product = client.GetProduct("123");

    // Assert
    Assert.Equal("123", product.Id);
    Assert.Equal("a product name", product.Name);
  }
}
