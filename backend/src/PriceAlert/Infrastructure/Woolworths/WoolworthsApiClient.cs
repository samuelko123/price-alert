using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PriceAlert.API.Exceptions;
using PriceAlert.Domain;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient(HttpClient httpClient) : IWoolworthsApiClient
{
  private static readonly JsonSerializerOptions _jsonOptions = new()
  {
    RespectNullableAnnotations = true,
  };

  public async Task<Product> GetProduct(string sku)
  {
    var url = $"https://www.woolworths.com.au/api/v3/ui/schemaorg/product/{sku}";
    var response = await httpClient.GetAsync(url);
    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var dto = ParseJson<WoolworthsProductDto>(content);
    if (string.IsNullOrWhiteSpace(dto.Name))
    {
      throw new ProductNotFoundException(sku);
    }

    return new Product()
    {
      Id = dto.Sku,
      Name = dto.Name,
    };
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
