using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public class ProductRepository(IWoolworthsApiClient client)
{
  public async Task<Product> FindProductById(string id)
  {
    return await client.GetProduct(id);
  }
}
