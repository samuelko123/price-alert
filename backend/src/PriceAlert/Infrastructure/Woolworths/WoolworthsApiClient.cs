using System.Net.Http;
using System.Text.Json;
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
    var dto = JsonSerializer.Deserialize<WoolworthsProductDto>(content)!;

    return new Product()
    {
      Id = dto.Id,
      Name = dto.Name,
    };
  }
}
