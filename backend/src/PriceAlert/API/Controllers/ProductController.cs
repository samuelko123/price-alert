using Microsoft.AspNetCore.Mvc;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController() : ControllerBase
{
  [HttpPost("search")]
  public IActionResult Search()
  {
    return Ok();
  }
}
