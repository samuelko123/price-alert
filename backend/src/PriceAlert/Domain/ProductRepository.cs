using System;
using System.Linq;
using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public class ProductRepository(IWoolworthsApiClient client)
{
  public async Task<Product> FindProductByUrl(string url)
  {
    var uri = new Uri(url);

    var host = uri.Host;
    if (host != "www.woolworths.com.au")
    {
      throw new NotSupportedException($"Received unsupported hostname: {uri.Host}");
    }

    var id = uri.Segments.Last();
    return await client.GetProduct(id);
  }
}
