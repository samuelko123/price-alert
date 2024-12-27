using System.Threading.Tasks;

namespace PriceAlert.Infrastructure.Woolworths;

public interface IWoolworthsApiClient
{
  Task<WoolworthsProductDto> GetProduct(string sku);
}
