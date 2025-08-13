namespace Form_Management.Api.Contracts.Response.Error.ResponseObject;

public record ErrorResponseObject<TErrorObject>(TErrorObject? Error) : AbstractErrorResponseObject<TErrorObject>(Error);