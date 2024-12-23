using System.Net;
using System.Net.Http;
using FakeItEasy;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Infrastructure.Woolworths;

public class WoolworthsProductApiClientTest
{
  [Fact]
  public void GetProduct_ReturnsProduct()
  {
    // Arrange
    var messageHandler = A.Fake<HttpMessageHandler>();
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("product name from Woolworths"),
    };
    SetResponse(messageHandler, response);

    var httpClient = new HttpClient(messageHandler);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var product = apiClient.GetProduct("123");

    // Assert
    Assert.Equal("123", product.Id);
    Assert.Equal("product name from Woolworths", product.Name);
  }

  private static void SetResponse(HttpMessageHandler messageHandler, HttpResponseMessage response)
  {
    A.CallTo(messageHandler)
          .Where(x => x.Method.Name == "SendAsync")
          .WithReturnType<HttpResponseMessage>()
          .Returns(response);
  }
}
