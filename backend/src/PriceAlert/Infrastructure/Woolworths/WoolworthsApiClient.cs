using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PriceAlert.Domain;
using PriceAlert.Infrastructure.Exceptions;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsApiClient(HttpClient httpClient)
{
  public async Task<Product> GetProduct(string id)
  {
    var url = $"https://www.woolworths.com.au/api/v3/ui/schemaorg/product/{id}";
    var response = await httpClient.GetAsync(url);
    ValidateResponse(response, HttpStatusCode.OK);

    var content = await response.Content.ReadAsStringAsync();
    var dto = ParseJson<WoolworthsProductDto>(content);

    return new Product()
    {
      Id = dto.Id,
      Name = dto.Name,
    };
  }

  private static void ValidateResponse(HttpResponseMessage response, HttpStatusCode expectedStatusCode)
  {
    var actualStatusCode = response.StatusCode;
    if (actualStatusCode != expectedStatusCode)
    {
      throw new BadHttpResponseException($"""Unexpected HTTP status code. Received: {actualStatusCode.GetHashCode()} {actualStatusCode}. Expected: {expectedStatusCode.GetHashCode()} {expectedStatusCode}.""");
    }
  }

  private static T ParseJson<T>(string content)
  {
    var data = JsonSerializer.Deserialize<T>(content);
    if (data == null)
    {
      throw new UnreachableException("""We received "null" API response.""");
    }

    return data;
  }
}
