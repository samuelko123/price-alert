using System;

namespace PriceAlert.Infrastructure.Exceptions;

public class BadHttpResponseException(string message) : Exception(message);
