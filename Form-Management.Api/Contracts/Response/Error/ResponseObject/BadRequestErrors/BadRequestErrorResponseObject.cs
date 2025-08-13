namespace Form_Management.Api.Contracts.Response.Error.ResponseObject.BadRequestErrors;

public record BadRequestErrorResponseObject<TErrorObject> : AbstractErrorResponseObject<TErrorObject>
{
    public new TErrorObject? Error { get; }

    public BadRequestErrorResponseObject(TErrorObject? error) : base(error)
    {
        Error = error;
    }
}
