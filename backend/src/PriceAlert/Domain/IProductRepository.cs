using System;
using System.Threading.Tasks;

namespace PriceAlert.Domain;

public interface IProductRepository
{
  Task<Product> FindProductByUrl(Uri uri);
}
