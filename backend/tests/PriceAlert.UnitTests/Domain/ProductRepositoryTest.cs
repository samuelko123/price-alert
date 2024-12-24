using PriceAlert.Domain;

namespace PriceAlert.UnitTests.Domain;

public class ProductRepositoryTest
{
  [Fact]
  public void FindProduct()
  {
    // Arrange
    var repository = new ProductRepository();

    // Action
    var product = repository.FindProductById("123");

    // Assert
    Assert.Equal("123", product.Id);
    Assert.Equal("a product name", product.Name);
  }
}
