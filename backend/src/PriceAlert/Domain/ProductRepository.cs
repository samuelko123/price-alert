using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public static partial class ProductUrlRegex
{
  [GeneratedRegex(@"https://www.woolworths.com.au/shop/productdetails/([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
  public static partial Regex Woolworths();
}

public class ProductRepository(IWoolworthsApiClient client) : IProductRepository
{
  public async Task<Product> FindProductByUrl(string url)
  {
    var id = ExtractProductIdFromUrl(url);
    return await client.GetProduct(id);
  }

  private static string ExtractProductIdFromUrl(string url)
  {
    var match = ProductUrlRegex.Woolworths().Match(url);
    if (!match.Success)
    {
      throw new DataValidationException("The url is invalid. It should start with 'https://www.woolworths.com.au/shop/productdetails/'.");
    }

    var id = match.Groups[1].Value;
    return id;
  }
}
