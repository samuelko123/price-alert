using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.API.Problems;
using PriceAlert.Domain;
using PriceAlert.Domain.Exceptions;
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
        builder.Services.AddProblemDetails(option =>
        {
            option.CustomizeProblemDetails = context =>
            {
                var exception = context.Exception;
                if (exception is DataValidationException)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.ProblemDetails = new BadRequestProblemDetails(exception.Message);
                }
            };
        });
        builder.Services.AddMvc()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorMessages = context.ModelState.Values
                        .SelectMany(value => value.Errors.Select(error => error.ErrorMessage))
                        .ToList();

                    return new BadRequestObjectResult(new BadRequestProblemDetails(errorMessages));
                };
            });

        var app = builder.Build();
        app.UseExceptionHandler();
        app.MapControllers();

        app.Run();
    }
}
