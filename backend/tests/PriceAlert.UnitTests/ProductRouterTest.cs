using PriceAlert.Product;

namespace PriceAlert.UnitTests;

public class UnitTest1
{
    [Fact]
    public void GetProduct_ReturnsProduct()
    {
        var product = ProductRouter.GetProduct();
        Assert.Equal("A dummy product", product.Name);
    }
}
