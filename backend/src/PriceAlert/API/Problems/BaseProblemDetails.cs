using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PriceAlert.API.Problems;

public class BaseProblemDetails : ProblemDetails
{
    public required List<Error> Errors { get; init; }

    [SetsRequiredMembers]
    public BaseProblemDetails(IList<string> messages)
    {
        Errors = messages.Select(message => new Error { Message = message }).ToList();
    }
}
