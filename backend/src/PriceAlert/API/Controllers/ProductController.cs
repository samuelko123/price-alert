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

    if (!Uri.IsWellFormedUriString(dto.Url, UriKind.Absolute))
    {
      return BadRequest(new InvalidUriError(dto.Url));
    }

    var uri = new Uri(dto.Url);
    var product = await repository.FindProductByUri(uri);
    return Ok(product);
  }
}
