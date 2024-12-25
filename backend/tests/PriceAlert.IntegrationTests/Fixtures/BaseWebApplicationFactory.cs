using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PriceAlert.API.Controllers;
using PriceAlert.Infrastructure.Woolworths;

namespace PriceAlert.IntegrationTests.Fixtures;

internal class BaseWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddControllers().AddApplicationPart(typeof(HealthCheckController).Assembly);
            services.AddControllers().AddApplicationPart(typeof(ProductController).Assembly);

            services.Replace(new ServiceDescriptor(typeof(IWoolworthsApiClient), A.Fake<IWoolworthsApiClient>()));
        });
    }
}
