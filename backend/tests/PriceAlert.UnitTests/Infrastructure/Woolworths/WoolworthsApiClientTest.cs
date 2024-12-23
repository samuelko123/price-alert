using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Infrastructure.Exceptions;
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

  [Fact]
  public async void GetProduct_WhenHttpStatusIsNotOK_ThrowsBadHttpStatusCodeException()
  {
    // Arrange
    var messageHandler = A.Fake<HttpMessageHandler>();
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.InternalServerError,
    };
    SetResponse(messageHandler, response);

    var httpClient = new HttpClient(messageHandler);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<BadHttpResponseException>(exception);
    Assert.Equal("Unexpected HTTP status code. Received: 500 InternalServerError. Expected: 200 OK.", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenHttpResponseIsNull_ThrowsUnreachableException()
  {
    // Arrange
    var messageHandler = A.Fake<HttpMessageHandler>();
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("null"),
    };
    SetResponse(messageHandler, response);

    var httpClient = new HttpClient(messageHandler);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<UnreachableException>(exception);
  }

  private void SetResponse(HttpMessageHandler messageHandler, HttpResponseMessage response)
  {
    A.CallTo(messageHandler)
      .Where(x => x.Method.Name == "SendAsync")
      .WithReturnType<Task<HttpResponseMessage>>()
      .Returns(response);
  }
}
