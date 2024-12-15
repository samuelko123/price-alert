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
                ImageUrl = "https://www.google.com/logos/doodles/2024/seasonal-holidays-2024-6753651837110333.4-s.png",
            };

            return product;
        });

        app.Run();
    }
}
