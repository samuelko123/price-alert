using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.API.Exceptions;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.UnitTests.Infrastructure.Woolworths;

public class WoolworthsApiClientTest
{
  [Fact]
  public async Task GetProduct_ReturnsProduct()
  {
    // Arrange
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

    var httpClient = CreateHttpClient(response);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var product = await apiClient.GetProduct("123");

    // Assert
    Assert.Equal("123", product.Id);
    Assert.Equal("a product name", product.Name);
  }

  [Fact]
  public async void GetProduct_WhenHttpResponseStatusIsNotOK_ThrowsBadHttpStatusCodeException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.InternalServerError,
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<HttpRequestException>(exception);
    Assert.Equal("Response status code does not indicate success: 500 (Internal Server Error).", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenHttpResponseBodyIsNull_ThrowsUnreachableException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("null"),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<UnreachableException>(exception);
    Assert.Equal("Received unexpected HTTP response body. Content: null.", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenHttpResponseBodyIsNotJson_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("<name>a product</name>"),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("Received unexpected HTTP response body. Content: <name>a product</name>.", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenHttpResponseBodyHasNullSku_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""{ "sku": null, "name": "a product name" }"""),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("""Received unexpected HTTP response body. Content: { "sku": null, "name": "a product name" }.""", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenHttpResponseBodyHasEmptyName_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""{ "sku": "123", "name": "     " }"""),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new WoolworthsApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<ProductNotFoundException>(exception);
    Assert.Equal("Unable to find product: 123", exception.Message);
  }

  private static HttpClient CreateHttpClient(HttpResponseMessage response)
  {
    var messageHandler = A.Fake<HttpMessageHandler>();
    A.CallTo(messageHandler)
      .Where(x => x.Method.Name == "SendAsync")
      .WithReturnType<Task<HttpResponseMessage>>()
      .Returns(response);

    return new HttpClient(messageHandler);
  }
}
