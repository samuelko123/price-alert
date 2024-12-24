using System.Threading.Tasks;
using PriceAlert.Domain;

namespace PriceAlert.Infrastructure.Woolworths;

public interface IWoolworthsApiClient
{
  Task<Product> GetProduct(string id);
}
