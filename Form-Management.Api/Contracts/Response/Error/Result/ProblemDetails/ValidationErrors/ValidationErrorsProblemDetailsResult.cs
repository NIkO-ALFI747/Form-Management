using Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails.ValidationErrors;
using Form_Management.Api.Contracts.Response.Error.ResponseObject.ValidationErrors;
using Form_Management.Domain.Errors.Validation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails.ValidationErrors;

public static class ValidationErrorsProblemDetailsResult
{
    public static IActionResult ToValidationErrorsProblemDetailsResult<TErrorObject>(this ValidationResult result, HttpContext context)
        where TErrorObject : ValueObjectValidationError
    {
        if (result.IsValid) throw new InvalidOperationException("Result can not be succeed");
        var problemDetails = result.CreateValidationErrorsProblemDetails<TErrorObject>(context);
        return new ProblemDetailsObjectResult<ValidationErrorsProblemDetailsResponseObject<ValidationErrorsResponseObject<ValidationErrorResponseObject<TErrorObject>>>>(problemDetails);
    }

    private static ValidationErrorsProblemDetailsResponseObject<ValidationErrorsResponseObject<ValidationErrorResponseObject<TErrorObject>>>
    CreateValidationErrorsProblemDetails<TErrorObject>(this ValidationResult result, HttpContext context)
        where TErrorObject : ValueObjectValidationError
    {
        var errorsResponse = result.Errors.Adapt<ValidationErrorsResponseObject<ValidationErrorResponseObject<TErrorObject>>>();
        return new ValidationErrorsProblemDetailsResponseObject<ValidationErrorsResponseObject<ValidationErrorResponseObject<TErrorObject>>>
            (
                instance: context.Request.Path, 
                traceIdentifier: context.TraceIdentifier, 
                errors: errorsResponse
            );
    }

    public static IActionResult ToValidationErrorProblemDetailsResult<TErrorObject>(this TErrorObject error, HttpContext context)
    where TErrorObject : ValueObjectValidationError
    {
        var problemDetailsResponseObject = error.CreateValidationErrorProblemDetails(context);
        return new ProblemDetailsObjectResult<ValidationErrorsProblemDetailsResponseObject<ValidationErrorResponseObject<TErrorObject>>>(problemDetailsResponseObject);
    }

    private static ValidationErrorsProblemDetailsResponseObject<ValidationErrorResponseObject<TErrorObject>>
    CreateValidationErrorProblemDetails<TErrorObject>(this TErrorObject error, HttpContext context)
        where TErrorObject : ValueObjectValidationError
    {
        var responseObject = new ValidationErrorResponseObject<TErrorObject>(error);
        return new ValidationErrorsProblemDetailsResponseObject<ValidationErrorResponseObject<TErrorObject>>
            (
                instance: context.Request.Path,
                traceIdentifier: context.TraceIdentifier,
                errors: responseObject
            );
    }
}