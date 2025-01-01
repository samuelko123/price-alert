using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PriceAlert.API.Problems;
using PriceAlert.Domain;
using PriceAlert.Domain.Exceptions;
using PriceAlert.Infrastructure.Kmart;
using PriceAlert.Infrastructure.Officeworks;

namespace PriceAlert;

internal class Program
{
    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IOfficeworksApiClient, OfficeworksApiClient>();
        builder.Services.AddSingleton<IKmartScraper, KmartScraper>();
        builder.Services.AddHttpClient<OfficeworksApiClient>();

        builder.Services.AddHealthChecks();
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

                if (exception is ItemNotFoundException)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.ProblemDetails = new NotFoundProblemDetails(exception.Message);
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
        app.MapHealthChecks("/api/healthcheck");
        app.MapControllers();

        app.Run();
    }
}
