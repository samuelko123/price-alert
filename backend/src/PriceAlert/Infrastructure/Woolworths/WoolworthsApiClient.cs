using System.Net.Http;
using PriceAlert.Domain;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient(HttpClient httpClient)
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
