using System;

namespace PriceAlert.Infrastructure.Exceptions;

public class ProductNotFoundException(string sku) : Exception($"Unable to find product: {sku}");
