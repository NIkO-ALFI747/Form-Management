namespace Form_Management.Api.Contracts.Response.Error.ResponseObject.ValidationErrors;

public record ValidationErrorResponseObject<TErrorObject>(
    TErrorObject? InternalError,
    string? ErrorCode = null,
    string? ErrorMessage = null,
    string? InvalidField = null
    ) : AbstractErrorResponseObject<TErrorObject>(InternalError);