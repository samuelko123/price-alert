using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient(HttpClient httpClient) : IWoolworthsApiClient
{
  private static readonly JsonSerializerOptions _jsonOptions = new()
  {
    RespectNullableAnnotations = true,
  };

  public async Task<WoolworthsProductDto> GetProduct(string sku)
  {
    var url = $"https://www.woolworths.com.au/api/v3/ui/schemaorg/product/{sku}";
    var response = await httpClient.GetAsync(url);
    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    return ParseJson<WoolworthsProductDto>(content);
  }

  private static T ParseJson<T>(string content)
  {
    try
    {
      return JsonSerializer.Deserialize<T>(content, _jsonOptions) ?? throw new JsonException();
    }
    catch (JsonException ex)
    {
      throw new JsonException($"Response body is invalid. Content: {content}", ex);
    }
  }
}
