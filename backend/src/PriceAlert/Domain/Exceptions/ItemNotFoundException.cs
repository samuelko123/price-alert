using System;

namespace PriceAlert.Domain.Exceptions;

public class ItemNotFoundException(string message) : Exception(message);
