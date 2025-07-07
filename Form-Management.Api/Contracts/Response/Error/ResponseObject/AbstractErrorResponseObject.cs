namespace Form_Management.Api.Contracts.Response.Error.ResponseObject;

public abstract record AbstractErrorResponseObject<TErrorObject>(TErrorObject? Error)
{
    protected TErrorObject? Error { get; } = Error;
}