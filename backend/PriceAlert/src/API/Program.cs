using API.Product;
using Microsoft.AspNetCore.Builder;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.MapGroup("/api/products").MapProduct();

        app.Run();
    }
}
