using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.Controllers;
using PriceAlert.API.DTOs;

namespace PriceAlert.UnitTests.API.Controllers;

public class ProductControllerTest
{
  [Fact]
  public void Search_WithValidDto_ReturnsOk()
  {
    // Arrange
    var controller = new ProductController();
    var dto = new ProductSearchDto()
    {
      Url = "https://google.com"
    };

    // Action
    var result = controller.Search(dto);

    // Assert
    Assert.IsType<OkResult>(result);
  }
}
