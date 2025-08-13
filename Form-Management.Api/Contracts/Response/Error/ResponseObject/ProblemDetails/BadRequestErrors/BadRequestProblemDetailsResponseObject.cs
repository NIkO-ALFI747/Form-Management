namespace Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails.BadRequestErrors;

public class BadRequestProblemDetailsResponseObject<TErrorsResponseObject>
    : ProblemDetailsResponseObject<TErrorsResponseObject>
{
    public const int STATUS = StatusCodes.Status400BadRequest;

    public BadRequestProblemDetailsResponseObject(
        string instance,
        string traceIdentifier,
        TErrorsResponseObject errors,
        string title,
        string detail = DETAIL,
        int status = STATUS
        ) : base(instance, traceIdentifier, errors, title, status, detail)
    {
        if (status < 400 || status > 500) {
            Status = STATUS;
            throw new ArgumentOutOfRangeException(nameof(status));
        }
    }
}