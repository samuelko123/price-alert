using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.Domain;

public class ProductRepository(IWoolworthsApiClient client) : IProductRepository
{
  public async Task<Product> FindProductByUrl(string url)
  {
    var sku = ExtractProductSkuFromUrl(url);

    var dto = await client.GetProduct(sku);
    if (string.IsNullOrWhiteSpace(dto.Name))
    {
      throw new ItemNotFoundException($"Unable to find product: {sku}");
    }

    return new Product()
    {
      Sku = dto.Sku,
      Name = dto.Name
    };
  }

  private static string ExtractProductSkuFromUrl(string url)
  {
    var match = Regex.Match(url, @"https://www.woolworths.com.au/shop/productdetails/([0-9]+)", RegexOptions.IgnoreCase);
    if (!match.Success)
    {
      throw new DataValidationException("The url is invalid. It should start with 'https://www.woolworths.com.au/shop/productdetails/'.");
    }

    var sku = match.Groups[1].Value;
    return sku;
  }
}
