using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.API.Controllers;

namespace PriceAlert.IntegrationTests.Fixtures;

public class BaseWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
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
