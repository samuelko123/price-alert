using Microsoft.AspNetCore.Mvc;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController() : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok();
  }
}
