using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PriceAlert.Domain.Exceptions;

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
    if (response.StatusCode == HttpStatusCode.NotFound)
    {
      throw new ItemNotFoundException($"Unable to find product: {sku}");
    }
    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    return ParseJson<OfficeworksProductDto>(content);
  }

  public async Task<OfficeworksProductPriceDto> GetProductPrice(string sku)
  {
    var url = $"https://www.officeworks.com.au/catalogue-app/api/prices/{sku}";

    var response = await httpClient.GetAsync(url);
    if (response.StatusCode == HttpStatusCode.NotFound)
    {
      throw new ItemNotFoundException($"Unable to find product: {sku}");
    }
    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var data = ParseJson<Dictionary<string, OfficeworksProductPriceDto>>(content);

    try
    {
      return data[sku];
    }
    catch (KeyNotFoundException ex)
    {
      throw new KeyNotFoundException($"The given key '{sku}' was not present in the response: {content}", ex);
    }
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
