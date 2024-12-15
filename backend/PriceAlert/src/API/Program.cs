using API.DTOs;
using Microsoft.AspNetCore.Builder;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.MapGet("/api/products/1", () =>
        {
            var product = new Product()
            {
                Id = 1,
                Url = "https://www.google.com",
                Name = "A dummy product",
            };

            return product;
        });

        app.Run();
    }
}
