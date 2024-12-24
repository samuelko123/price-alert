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
    if (!uri.Host.ToLower().EndsWith("woolworths.com.au") || !ProductUrlRegex.Woolworths().IsMatch(uri.LocalPath))
    {
      throw new NotSupportedException($"Received unsupported URL: {url}");
    }

    var id = uri.Segments.Last();
    return await client.GetProduct(id);
  }
}
