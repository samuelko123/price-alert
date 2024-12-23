using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Infrastructure.Woolworths;

public class WoolworthsProductApiClientTest
{
  [Fact]
  public async Task GetProduct_ReturnsProduct()
  {
    // Arrange
    var messageHandler = A.Fake<HttpMessageHandler>();
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""
      {
        "sku": "123",
        "name": "a product name"
      }
      """),
    };
    SetResponse(messageHandler, response);

    var httpClient = new HttpClient(messageHandler);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var product = await apiClient.GetProduct("123");

    // Assert
    Assert.Equal("123", product.Id);
    Assert.Equal("a product name", product.Name);
  }

  private void SetResponse(HttpMessageHandler messageHandler, HttpResponseMessage response)
  {
    A.CallTo(messageHandler)
      .Where(x => x.Method.Name == "SendAsync")
      .WithReturnType<Task<HttpResponseMessage>>()
      .Returns(response);
  }
}
