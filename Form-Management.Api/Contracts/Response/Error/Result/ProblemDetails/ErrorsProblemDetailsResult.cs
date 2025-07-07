using Form_Management.Api.Contracts.Response.Error.ResponseObject;
using Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails;

public static class ErrorsProblemDetailsResult
{
    public static IActionResult ToErrorProblemDetailsResult<TErrorObject>(this TErrorObject error, HttpContext context,
        string title, int status, string detail = ProblemDetailsResponseObject<ErrorResponseObject<TErrorObject>>.DETAIL)
    {
        var problemDetailsResponseObject = error.CreateErrorProblemDetails(context, title, status, detail);
        return new ProblemDetailsObjectResult<ProblemDetailsResponseObject<ErrorResponseObject<TErrorObject>>>(problemDetailsResponseObject);
    }

    private static ProblemDetailsResponseObject<ErrorResponseObject<TErrorObject>>
    CreateErrorProblemDetails<TErrorObject>(this TErrorObject error, HttpContext context, string title, int status, string detail)
    {
        var responseObject = new ErrorResponseObject<TErrorObject>(error);
        return new ProblemDetailsResponseObject<ErrorResponseObject<TErrorObject>>
            (
                instance: context.Request.Path,
                traceIdentifier: context.TraceIdentifier,
                errors: responseObject,
                title: title,
                status: status,
                detail: detail
            );
    }
}