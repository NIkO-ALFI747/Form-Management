
namespace Form_Management.Domain.Errors.Error;

public record Error : AbstractError
{
    protected Error(string code, string message, ErrorType type) :
        base(code, message, type)
    {}

    public static Error NotFound(string code, string message) =>
        new (code, message, ErrorType.NotFound);

    public static Error Failure(string code, string message) =>
        new (code, message, ErrorType.Failure);

    public static Error Conflict(string code, string message) =>
        new (code, message, ErrorType.Conflict);

    public static Error BadRequest(string code, string message) =>
        new(code, message, ErrorType.BadRequest);

    public static Error SetMessage(Error error, string message) =>
        new(error.Code, message, error.Type);
}