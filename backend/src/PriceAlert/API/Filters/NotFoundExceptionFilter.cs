using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PriceAlert.API.Errors;
using PriceAlert.API.Exceptions;

namespace PriceAlert.API.Filters;

public class NotFoundExceptionFilter() : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is NotFoundException)
        {
            context.Result = new NotFoundObjectResult(new Error(exception.Message));
            context.ExceptionHandled = true;
        }
    }
}
