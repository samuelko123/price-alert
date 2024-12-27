using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.DTOs;
using PriceAlert.Domain;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController(IProductRepository repository) : ControllerBase
{
  [HttpGet("getByUrl")]
  public async Task<IActionResult> GetByUrl([FromQuery] string url)
  {
    var product = await repository.FindProductByUrl(url);
    var productDto = new ProductDto()
    {
      Sku = product.Id,
      Name = product.Name,
    };

    return Ok(productDto);
  }
}
