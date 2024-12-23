using PriceAlert.Domain;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient()
{
  public Product GetProduct(string id)
  {
    return new Product()
    {
      Id = id,
      Name = "a product name",
    };
  }
}
