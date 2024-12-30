using System.Text.Json.Serialization;

namespace PriceAlert.API.Problems;

public class Error
{
    [JsonPropertyName("message")]
    public required string Message { get; init; }
}
