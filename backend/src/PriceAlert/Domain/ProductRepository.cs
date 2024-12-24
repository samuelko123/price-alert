using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public static partial class ProductUrlRegex
{
  [GeneratedRegex(@"/shop/productdetails/[0-9]+", RegexOptions.IgnoreCase)]
  public static partial Regex Woolworths();
}

public class ProductRepository(IWoolworthsApiClient client)
{
  public async Task<Product> FindProductByUrl(string url)
  {
    var uri = new Uri(url);

    var host = uri.Host;
    if (host != "www.woolworths.com.au")
    {
      throw new NotSupportedException($"Received unsupported hostname: {host}");
    }

    var path = uri.LocalPath;
    if (!ProductUrlRegex.Woolworths().IsMatch(path))
    {
      throw new NotSupportedException($"Received unsupported uri path: {path}");
    }

    var id = uri.Segments.Last();
    return await client.GetProduct(id);
  }
}
