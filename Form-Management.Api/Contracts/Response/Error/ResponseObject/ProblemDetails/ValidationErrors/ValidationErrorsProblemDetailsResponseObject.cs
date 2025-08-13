using Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails.BadRequestErrors;

namespace Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails.ValidationErrors;

public class ValidationErrorsProblemDetailsResponseObject<TValidationErrorsResponseObject>(
    string instance,
    string traceIdentifier,
    TValidationErrorsResponseObject errors,
    string detail = BadRequestProblemDetailsResponseObject<TValidationErrorsResponseObject>.DETAIL,
    string title = ValidationErrorsProblemDetailsResponseObject<TValidationErrorsResponseObject>.TITLE,
    int status = BadRequestProblemDetailsResponseObject<TValidationErrorsResponseObject>.STATUS) 
    : BadRequestProblemDetailsResponseObject<TValidationErrorsResponseObject>(instance, traceIdentifier, errors, title, detail, status)
{
    public const string TITLE = "One or more validation errors occurred.";
}