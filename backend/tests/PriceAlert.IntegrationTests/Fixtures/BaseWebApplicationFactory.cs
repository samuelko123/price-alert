using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.API.Controllers;

namespace PriceAlert.IntegrationTests.Fixtures;

internal class BaseWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddControllers().AddApplicationPart(typeof(HealthCheckController).Assembly);
            services.AddControllers().AddApplicationPart(typeof(ProductController).Assembly);
        });
    }
}
