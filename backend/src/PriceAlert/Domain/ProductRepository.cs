using System;
using System.Linq;
using System.Threading.Tasks;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Officeworks;

namespace PriceAlert.Domain;

public class ProductRepository(IOfficeworksApiClient client) : IProductRepository
{
  public async Task<Product> FindProductByUrl(string url)
  {
    var sku = ExtractProductSkuFromUrl(url);

    var dto = await client.GetProduct(sku);
    return new Product()
    {
      Sku = dto.Sku,
      Name = dto.Name
    };
  }

  private static string ExtractProductSkuFromUrl(string url)
  {
    if (!url.StartsWith("https://www.officeworks.com.au/shop/officeworks/p/", StringComparison.OrdinalIgnoreCase))
    {
      throw new DataValidationException("The url is invalid. It should start with 'https://www.officeworks.com.au/shop/officeworks/p/'.");
    }

    var uri = new Uri(url);
    var productSlug = uri.Segments.Last();
    var sku = productSlug.Split("-").Last().ToUpperInvariant();

    return sku;
  }
}
