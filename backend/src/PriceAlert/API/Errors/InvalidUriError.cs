namespace PriceAlert.API.Errors;

public class InvalidUriError(string uri) : Error($"Received invalid url: '{uri}'.");
