using System.Text.Json.Serialization;

namespace PriceAlert.Infrastructure.Woolworths;

public class WoolworthsProductDto
{
  [JsonPropertyName("sku")]
  public required string Id { get; init; }

  [JsonPropertyName("name")]
  public required string Name { get; init; }
}
