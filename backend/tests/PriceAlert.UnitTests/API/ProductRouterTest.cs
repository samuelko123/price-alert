using PriceAlert.API;

namespace PriceAlert.UnitTests.API;

public class ProductRouterTest
{
    [Fact]
    public void GetProduct_ReturnsProduct()
    {
        var product = ProductRouter.GetProduct();
        Assert.Equal("A dummy product", product.Name);
    }
}
