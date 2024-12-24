using Microsoft.AspNetCore.Mvc;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/healthcheck")]
public class ProductController() : ControllerBase
{
  [HttpGet]
  public IActionResult FindProducts()
  {
    return Ok();
  }
}
