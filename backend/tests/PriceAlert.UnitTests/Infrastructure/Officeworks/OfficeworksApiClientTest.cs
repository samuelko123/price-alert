using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FakeItEasy;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Officeworks;

namespace PriceAlert.UnitTests.Infrastructure.Officeworks;

public class OfficeworksApiClientTest
{
  [Fact]
  public async Task GetProduct_WhenResponseStatusIs200_ReturnsProduct()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""
      {
        "sku": "ABC123",
        "name": "a product name",
        "image": "//s3-ap-southeast-2.amazonaws.com/an-image"
      }
      """),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var product = await apiClient.GetProduct("ABC123");

    // Assert
    Assert.Equal("ABC123", product.Sku);
    Assert.Equal("a product name", product.Name);
    Assert.Equal("//s3-ap-southeast-2.amazonaws.com/an-image", product.MainImageSource);
  }

  [Fact]
  public async void GetProduct_WhenResponseStatusIs404_ThrowsHttpRequestException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.NotFound,
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<ItemNotFoundException>(exception);
    Assert.Equal("Unable to find product: 123", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenResponseStatusIsNot200_ThrowsHttpRequestException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.InternalServerError,
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<HttpRequestException>(exception);
    Assert.Equal("Response status code does not indicate success: 500 (Internal Server Error).", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenResponseBodyIsNull_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("null"),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("Response body is invalid. Content: null", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenResponseBodyIsNotJson_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("<name>a product</name>"),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("Response body is invalid. Content: <name>a product</name>", exception.Message);
  }

  [Fact]
  public async void GetProduct_WhenRequiredFieldIsMissing_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""{ "sku": null, "name": "a product name" }"""),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProduct("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("""Response body is invalid. Content: { "sku": null, "name": "a product name" }""", exception.Message);
  }

  [Fact]
  public async Task GetProductPrice_WhenResponseStatusIs200_ReturnsProductPrice()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""
      {
        "ABC123": {
          "price": 12345,
          "gstRate": 10,
          "tax": 1000
        }
      }
      """),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var dto = await apiClient.GetProductPrice("ABC123");

    // Assert
    Assert.Equal(12345, dto.PriceInCents);
  }

  [Fact]
  public async void GetProductPrice_WhenResponseStatusIs404_ThrowsHttpRequestException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.NotFound,
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProductPrice("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<ItemNotFoundException>(exception);
    Assert.Equal("Unable to find product: 123", exception.Message);
  }

  [Fact]
  public async void GetProductPrice_WhenResponseStatusIsNot200_ThrowsHttpRequestException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.InternalServerError,
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProductPrice("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<HttpRequestException>(exception);
    Assert.Equal("Response status code does not indicate success: 500 (Internal Server Error).", exception.Message);
  }

  [Fact]
  public async void GetProductPrice_WhenResponseBodyIsNull_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("null"),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProductPrice("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("Response body is invalid. Content: null", exception.Message);
  }

  [Fact]
  public async void GetProductPrice_WhenResponseBodyIsNotJson_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("<name>a product</name>"),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProductPrice("123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("Response body is invalid. Content: <name>a product</name>", exception.Message);
  }

  [Fact]
  public async void GetProductPrice_WhenPriceFieldIsMissing_ThrowsJsonException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""{"ABC123":{}}"""),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProductPrice("ABC123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<JsonException>(exception);
    Assert.Equal("""Response body is invalid. Content: {"ABC123":{}}""", exception.Message);
  }

  [Fact]
  public async void GetProductPrice_WhenKeyNotFound_ThrowsKeyNotFoundException()
  {
    // Arrange
    var response = new HttpResponseMessage
    {
      StatusCode = HttpStatusCode.OK,
      Content = new StringContent("""{"DEF123":{"price": 12345}}"""),
    };

    var httpClient = CreateHttpClient(response);
    var apiClient = new OfficeworksApiClient(httpClient);

    // Action
    var exception = await Record.ExceptionAsync(() => apiClient.GetProductPrice("ABC123"));

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<KeyNotFoundException>(exception);
    Assert.Equal("""The given key 'ABC123' was not present in the response: {"DEF123":{"price": 12345}}""", exception.Message);
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
