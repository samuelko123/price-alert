using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.Domain;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IWoolworthsApiClient, WoolworthsApiClient>();
        builder.Services.AddHttpClient<WoolworthsApiClient>();

        var app = builder.Build();
        app.MapControllers();

        app.Run();
    }
}
