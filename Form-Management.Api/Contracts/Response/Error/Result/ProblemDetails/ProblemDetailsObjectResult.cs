using Microsoft.AspNetCore.Mvc;
using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails;

public class ProblemDetailsObjectResult<TProblemDetailsResponseObject> : ObjectResult
    where TProblemDetailsResponseObject : MvcProblemDetails
{
    public ProblemDetailsObjectResult(TProblemDetailsResponseObject problemDetails)
        : base(problemDetails)
    {
        StatusCode = problemDetails.Status ?? StatusCodes.Status400BadRequest;
        if (StatusCode >= 200 && StatusCode < 300)
        {
            throw new InvalidOperationException("Successful status codes (2xx) are not allowed for ProblemDetails responses.");
        }
        ContentTypes = ["application/problem+json", "application/json"];
    }
}