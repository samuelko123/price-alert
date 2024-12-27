using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.DTOs;
using PriceAlert.API.Errors;
using PriceAlert.API.Exceptions;
using PriceAlert.Domain;

namespace PriceAlert.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController(IProductRepository repository) : ControllerBase
{
  [HttpGet("getByUrl")]
  public async Task<IActionResult> GetByUrl([FromQuery] string url)
  {
    try
    {
      if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
      {
        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
          ["url"] = ["The url field is invalid."],
        }));
      }

      var product = await repository.FindProductByUri(new Uri(url));
      var productDto = new ProductDto()
      {
        Sku = product.Id,
        Name = product.Name,
      };

      return Ok(productDto);
    }
    catch (NotFoundException ex)
    {
      return NotFound(new Error(ex.Message));
    }
  }
}
