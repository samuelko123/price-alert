using System;

namespace PriceAlert.API.Exceptions;

public class NotFoundException(string message) : Exception(message);
