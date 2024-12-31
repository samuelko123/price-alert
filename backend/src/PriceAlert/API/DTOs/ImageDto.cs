using System.Text.Json.Serialization;

namespace PriceAlert.API.DTOs;

public class ImageDto
{
  [JsonPropertyName("src")]
  public required string Source { get; init; }
}
