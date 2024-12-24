using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.DTOs;
using PriceAlert.API.Errors;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController() : ControllerBase
{
  [HttpPost("search")]
  public IActionResult Search([FromBody] ProductSearchDto dto)
  {
    if (dto.Url == null)
    {
      return BadRequest(new MissingRequiredPropertyError("url"));
    }
    return Ok();
  }
}
