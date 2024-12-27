using System.Threading.Tasks;

namespace PriceAlert.Domain;

public interface IProductRepository
{
  Task<Product> FindProductByUrl(string url);
}
