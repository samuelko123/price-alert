using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.API.Filters;
using PriceAlert.Domain;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<NotFoundExceptionFilter>();
        });
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IWoolworthsApiClient, WoolworthsApiClient>();
        builder.Services.AddHttpClient<WoolworthsApiClient>();

        var app = builder.Build();
        app.MapControllers();

        app.Run();
    }
}
