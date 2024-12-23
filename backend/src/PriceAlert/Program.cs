using PriceAlert.Product;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.MapGet("/api/healthcheck", () => Results.Ok());
        app.MapGroup("/api/products").MapProduct();

        app.Run();
    }
}
