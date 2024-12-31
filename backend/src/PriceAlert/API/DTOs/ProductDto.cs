using System.Text.Json.Serialization;

namespace PriceAlert.API.DTOs;

public class ProductDto
{
  [JsonPropertyName("sku")]
  public required string Sku { get; init; }

  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("priceInCents")]
  public required int PriceInCents { get; init; }

  [JsonPropertyName("mainImage")]
  public required ImageDto MainImage { get; init; }
}
