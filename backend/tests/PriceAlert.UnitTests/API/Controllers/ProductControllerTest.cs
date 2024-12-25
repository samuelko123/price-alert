using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.Controllers;
using PriceAlert.API.DTOs;
using PriceAlert.Domain;

namespace PriceAlert.UnitTests.API.Controllers;

public class ProductControllerTest
{
  [Fact]
  public void Search_WithValidDto_ReturnsProduct()
  {
    // Arrange
    var repository = A.Fake<IProductRepository>();
    var controller = new ProductController(repository);
    var dto = new ProductSearchDto()
    {
      Url = "https://google.com"
    };

    // Action
    var response = controller.Search(dto);

    // Assert
    var result = Assert.IsType<OkObjectResult>(response);
    var product = Assert.IsType<ProductDto>(result.Value);

    Assert.Equal("123", product.Id);
    Assert.Equal("a product", product.Name);
  }
}
