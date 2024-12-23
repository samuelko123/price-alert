using System.Net.Http;
using System.Threading.Tasks;
using PriceAlert.Domain;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient(HttpClient httpClient)
{
  public async Task<Product> GetProduct(string id)
  {
    var url = $"https://www.woolworths.com.au/api/v3/ui/schemaorg/product/{id}";
    var response = await httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();

    return new Product()
    {
      Id = id,
      Name = content,
    };
  }
}
