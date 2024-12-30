using System.Threading.Tasks;

namespace PriceAlert.Infrastructure.Officeworks;

public interface IOfficeworksApiClient
{
  Task<OfficeworksProductDto> GetProduct(string sku);
}
