using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.DTOs;
using PriceAlert.API.Errors;
using PriceAlert.Domain;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController(IProductRepository repository) : ControllerBase
{
  [HttpPost("search")]
  public IActionResult Search([FromBody] ProductSearchDto dto)
  {
    if (dto.Url == null)
    {
      return BadRequest(new MissingRequiredPropertyError("url"));
    }

    var product = new ProductDto()
    {
      Id = "123",
      Name = "a product",
    };
    return Ok(product);
  }
}
