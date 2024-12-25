using System;
using System.Threading.Tasks;

namespace PriceAlert.Domain;

public interface IProductRepository
{
  Task<Product> FindProductByUri(Uri uri);
}
