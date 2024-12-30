using System;

namespace PriceAlert.Domain.Exceptions;

public class DataValidationException(string message) : Exception(message);
