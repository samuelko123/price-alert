using System.Text.Json.Serialization;

namespace PriceAlert.Infrastructure.Officeworks;

public class OfficeworksProductDto
{
  [JsonPropertyName("sku")]
  public required string Sku { get; init; }

  [JsonPropertyName("name")]
  public required string Name { get; init; }
}
