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
  [HttpPost("getByUrl")]
  public async Task<IActionResult> GetByUrl([FromBody] GetByUrlPayloadDto dto)
  {
    if (dto.Url == null)
    {
      return BadRequest(new MissingRequiredPropertyError("url"));
    }

    if (!Uri.IsWellFormedUriString(dto.Url, UriKind.Absolute))
    {
      return BadRequest(new InvalidUriError(dto.Url));
    }

    var product = await repository.FindProductByUri(new Uri(dto.Url));
    var productDto = new ProductDto()
    {
      Id = product.Id,
      Name = product.Name,
    };

    return Ok(productDto);
  }
}
