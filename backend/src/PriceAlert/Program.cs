using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.MapGet("/api/healthcheck", () => Results.Ok());

        app.Run();
    }
}
