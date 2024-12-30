using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceAlert.Infrastructure.Officeworks;

public class OfficeworksApiClient(HttpClient httpClient) : IOfficeworksApiClient
{
  private static readonly JsonSerializerOptions _jsonOptions = new()
  {
    RespectNullableAnnotations = true,
  };

  public async Task<OfficeworksProductDto> GetProduct(string sku)
  {
    var url = $"https://www.officeworks.com.au/catalogue-app/api/products/{sku}";
    var response = await httpClient.GetAsync(url);
    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    return ParseJson<OfficeworksProductDto>(content);
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
