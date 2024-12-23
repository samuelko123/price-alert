using PriceAlert.Domain;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient()
{
  public Product GetProduct()
  {
    return new Product()
    {
      Id = "a product id",
      Name = "a product name",
    };
  }
}
