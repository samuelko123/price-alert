using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PriceAlert;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();

        var app = builder.Build();
        app.MapControllers();

        app.Run();
    }
}
