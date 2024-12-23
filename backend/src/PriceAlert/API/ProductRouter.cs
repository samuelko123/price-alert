using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using PriceAlert.Domain;

namespace PriceAlert.API;

public static class ProductRouter
{
    public static RouteGroupBuilder MapProduct(this RouteGroupBuilder group)
    {
        group.MapGet("/1", GetProduct);

        return group;
    }

    public static Product GetProduct()
    {
        var product = new Product()
        {
            Id = 1,
            Url = "https://www.google.com",
            Name = "A dummy product",
        };

        return product;
    }
}
