using Form_Management.Api.Contracts.Response.Error.ResponseObject.BadRequestErrors;
using Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails.BadRequestErrors;
using Form_Management.Domain.Errors.Error;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails.BadRequestErrors;

public static class BadRequestErrorsProblemDetailsResult
{
    public static IActionResult ToBadRequestErrorProblemDetailsResult<TErrorObject>(this TErrorObject error, HttpContext context, string title,
        int status = BadRequestProblemDetailsResponseObject<BadRequestErrorResponseObject<TErrorObject>>.STATUS)
        where TErrorObject : AbstractError
    {
        var problemDetailsResponseObject = error.CreateBadRequestErrorProblemDetails(context, title, status);
        return new ProblemDetailsObjectResult<BadRequestProblemDetailsResponseObject<BadRequestErrorResponseObject<TErrorObject>>>(problemDetailsResponseObject);
    }

    private static BadRequestProblemDetailsResponseObject<BadRequestErrorResponseObject<TErrorObject>>
    CreateBadRequestErrorProblemDetails<TErrorObject>(this TErrorObject error, HttpContext context, string title, 
        int status = BadRequestProblemDetailsResponseObject<BadRequestErrorResponseObject<TErrorObject>>.STATUS)
        where TErrorObject : AbstractError
    {
        var responseObject = new BadRequestErrorResponseObject<TErrorObject>(error);
        return new BadRequestProblemDetailsResponseObject<BadRequestErrorResponseObject<TErrorObject>>
            (
                instance: context.Request.Path,
                traceIdentifier: context.TraceIdentifier,
                errors: responseObject,
                title: title,
                status: status
            );
    }
}