namespace PriceAlert.Domain;

public class Product
{
  public required string Sku { get; init; }
  public required string Name { get; init; }
  public required int PriceInCents { get; init; }
}
