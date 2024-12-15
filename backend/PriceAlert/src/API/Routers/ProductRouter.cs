using API.DTOs;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace API.Routers;

public static class ProductRouter
{
    public static RouteGroupBuilder MapProduct(this RouteGroupBuilder group)
    {
        group.MapGet("/1", GetProduct);
        
        return group;
    }

    public static Results<Ok<Product>, NotFound> GetProduct()
    {
        var product = new Product()
        {
            Id = 1,
            Url = "https://www.google.com",
            Name = "A dummy product",
        };

        return TypedResults.Ok(product);
    }
}
