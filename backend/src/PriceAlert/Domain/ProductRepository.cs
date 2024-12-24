using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public static partial class ProductUrlRegex
{
  [GeneratedRegex(@"/shop/productdetails/([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
  public static partial Regex Woolworths();
}

public class ProductRepository(IWoolworthsApiClient client)
{
  public async Task<Product> FindProductByUrl(string url)
  {
    var uri = new Uri(url);
    if (!uri.Host.ToLower().EndsWith("woolworths.com.au"))
    {
      throw new NotSupportedException($"Received unsupported URL: {url}");
    }

    var match = ProductUrlRegex.Woolworths().Match(uri.LocalPath);
    if (!match.Success)
    {
      throw new NotSupportedException($"Received unsupported URL: {url}");
    }

    var id = match.Groups[1].Value;
    return await client.GetProduct(id);
  }
}
