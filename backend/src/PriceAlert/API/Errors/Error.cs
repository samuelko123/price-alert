using System.Text.Json.Serialization;

namespace PriceAlert.API.Errors;

public class Error(string message)
{
  [JsonPropertyName("error")]
  public string Message { get; init; } = message;
}
