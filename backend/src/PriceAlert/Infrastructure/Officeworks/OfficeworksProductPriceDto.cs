using System.Text.Json.Serialization;

namespace PriceAlert.Infrastructure.Officeworks;

public class OfficeworksProductPriceDto
{
  [JsonPropertyName("price")]
  public required int Price { get; init; }
}
