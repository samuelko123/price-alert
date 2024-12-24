using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public class ProductRepository(IWoolworthsApiClient client)
{
  public async Task<Product> FindProductByUrl(string url)
  {
    return await client.GetProduct(url);
  }
}
