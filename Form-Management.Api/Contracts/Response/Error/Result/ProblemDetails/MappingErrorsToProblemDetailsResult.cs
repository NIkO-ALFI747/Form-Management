using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails.BadRequestErrors;
using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails.ValidationErrors;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.Validation;
using Microsoft.AspNetCore.Mvc;
using TError = Form_Management.Domain.Errors.Error.Error;

namespace Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails;

public static class MappingErrorsToProblemDetailsResult
{
    public static IActionResult MapErrorToProblemDetailsResult(this AbstractError error, HttpContext context)
    {
        if (error is ValueObjectValidationError validationError)
            return validationError.ToValidationErrorProblemDetailsResult(context);
        if (error is TError generalError)
            return generalError.MapErrorToProblemDetailsResult(context);
        throw new Exception($"A descendant of {nameof(AbstractError)} hasn't been implemented.");
    }

    public static IActionResult MapErrorToProblemDetailsResult(this TError error, HttpContext context)
    {
        if (error.Type == ErrorType.NotFound)
            return error.ToBadRequestErrorProblemDetailsResult(context, "Resource Not Found", StatusCodes.Status404NotFound);
        if (error.Type == ErrorType.Conflict)
            return error.ToBadRequestErrorProblemDetailsResult(context, "Conflict", StatusCodes.Status409Conflict);
        if (error.Type == ErrorType.BadRequest)
            return error.ToBadRequestErrorProblemDetailsResult(context, "Bad Request");
        return error.ToErrorProblemDetailsResult(context, "Failure", StatusCodes.Status500InternalServerError);
    }
}