using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.DTOs;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController() : ControllerBase
{
  [HttpPost("search")]
  public IActionResult Search([FromBody] ProductSearchDto dto)
  {
    return Ok();
  }
}
