using System;
using System.Threading.Tasks;
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
  public async Task<IActionResult> Search([FromBody] ProductSearchDto dto)
  {
    if (dto.Url == null)
    {
      return BadRequest(new MissingRequiredPropertyError("url"));
    }

    var uri = new Uri(dto.Url);
    var product = await repository.FindProductByUrl(uri);
    return Ok(product);
  }
}
