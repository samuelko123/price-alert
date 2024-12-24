namespace PriceAlert.Domain;

public class ProductRepository
{
  public Product FindProductById(string id)
  {
    return new Product()
    {
      Id = id,
      Name = "a product name",
    };
  }
}
