using System;

namespace PriceAlert.API.Exceptions;

public class ProductNotFoundException(string sku) : NotFoundException($"Unable to find product: {sku}");
