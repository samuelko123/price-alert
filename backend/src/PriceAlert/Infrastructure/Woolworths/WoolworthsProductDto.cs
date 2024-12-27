using System.Text.Json.Serialization;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsProductDto
{
  [JsonPropertyName("sku")]
  public required string Sku { get; init; }

  [JsonPropertyName("name")]
  public required string Name { get; init; }
}
