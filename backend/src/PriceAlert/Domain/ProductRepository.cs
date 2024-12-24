using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public static partial class ProductUrlPathRegex
{
  [GeneratedRegex(@"/shop/productdetails/([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
  public static partial Regex Woolworths();
}

public static partial class ProductUrlHostRegex
{
  [GeneratedRegex(@"[www.]?woolworths.com.au", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
  public static partial Regex Woolworths();
}

public class ProductRepository(IWoolworthsApiClient client)
{
  public async Task<Product> FindProductByUrl(Uri uri)
  {
    var id = ExtractProductIdFromUrl(uri);
    return await client.GetProduct(id);
  }

  private static string ExtractProductIdFromUrl(Uri uri)
  {
    if (!ProductUrlHostRegex.Woolworths().IsMatch(uri.Host))
    {
      throw new NotSupportedException($"Received unsupported URL: {uri}");
    }

    var match = ProductUrlPathRegex.Woolworths().Match(uri.LocalPath);
    if (!match.Success)
    {
      throw new NotSupportedException($"Received unsupported URL: {uri}");
    }

    var id = match.Groups[1].Value;
    return id;
  }
}
