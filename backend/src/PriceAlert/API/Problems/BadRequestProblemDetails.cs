using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PriceAlert.API.Problems;

public class BadRequestProblemDetails : BaseProblemDetails
{
    [SetsRequiredMembers]
    public BadRequestProblemDetails(IList<string> messages) : base(messages)
    {
        Type = "https://httpstatuses.io/400";
        Title = "One or more validation errors occurred.";
    }
}
