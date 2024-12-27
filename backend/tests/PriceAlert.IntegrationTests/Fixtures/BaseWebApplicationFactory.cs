using System.Collections.Generic;
using System.Linq;
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
    public BaseWebApplicationFactory()
    {
        _serviceDescriptors = [];
    }

    public BaseWebApplicationFactory(IEnumerable<ServiceDescriptor> serviceDescriptors)
    {
        _serviceDescriptors = serviceDescriptors.ToList();
    }

    private readonly List<ServiceDescriptor> _serviceDescriptors;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddControllers().AddApplicationPart(typeof(HealthCheckController).Assembly);
            services.AddControllers().AddApplicationPart(typeof(ProductController).Assembly);

            services.Replace(new ServiceDescriptor(typeof(IWoolworthsApiClient), A.Fake<IWoolworthsApiClient>()));
            _serviceDescriptors.ForEach(service =>
            {
                services.Replace(service);
            });
        });
    }
}
