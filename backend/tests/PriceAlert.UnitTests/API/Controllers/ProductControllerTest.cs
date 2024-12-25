using System;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.Controllers;
using PriceAlert.API.DTOs;
using PriceAlert.API.Errors;
using PriceAlert.Domain;

namespace PriceAlert.UnitTests.API.Controllers;

public class ProductControllerTest
{
  [Fact]
  public async Task Search_WithInvalidUrl_ReturnsBadRequest()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    var controller = new ProductController(repository);
    var dto = new ProductSearchDto()
    {
      Url = "it is not a url"
    };

    // Action
    var response = await controller.Search(dto);

    // Assert
    var result = Assert.IsType<BadRequestObjectResult>(response);
    var error = Assert.IsType<InvalidUriError>(result.Value);
    Assert.Equal("Received invalid url: 'it is not a url'.", error.Message);
  }

  [Fact]
  public async Task Search_WithValidDto_ReturnsProduct()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    var product = new Product()
    {
      Id = "123",
      Name = "a product",
    };
    A.CallTo(() => repository.FindProductByUri(new Uri("https://google.com"))).Returns(product);

    var controller = new ProductController(repository);
    var dto = new ProductSearchDto()
    {
      Url = "https://google.com"
    };

    // Action
    var response = await controller.Search(dto);

    // Assert
    var result = Assert.IsType<OkObjectResult>(response);
    var productDto = Assert.IsType<ProductDto>(result.Value);

    Assert.Equal("123", productDto.Id);
    Assert.Equal("a product", productDto.Id);
  }
}
