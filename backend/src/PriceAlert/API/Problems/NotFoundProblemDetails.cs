using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace PriceAlert.API.Problems;

public class NotFoundProblemDetails : BaseProblemDetails
{
    [SetsRequiredMembers]
    public NotFoundProblemDetails(string message) : this([message]) { }

    [SetsRequiredMembers]
    public NotFoundProblemDetails(IList<string> messages) : base(messages)
    {
        Status = StatusCodes.Status404NotFound;
        Type = "https://httpstatuses.io/404";
        Title = "Item cannot be found.";
    }
}
