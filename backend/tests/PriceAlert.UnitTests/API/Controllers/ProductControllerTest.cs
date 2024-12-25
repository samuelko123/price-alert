using System;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PriceAlert.API.Controllers;
using PriceAlert.API.DTOs;
using PriceAlert.Domain;

namespace PriceAlert.UnitTests.API.Controllers;

public class ProductControllerTest
{
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
    A.CallTo(() => repository.FindProductByUrl(new Uri("https://google.com"))).Returns(product);

    var controller = new ProductController(repository);
    var dto = new ProductSearchDto()
    {
      Url = "https://google.com"
    };

    // Action
    var response = await controller.Search(dto);

    // Assert
    var result = Assert.IsType<OkObjectResult>(response);
    Assert.Equal(product, result.Value);
  }
}
