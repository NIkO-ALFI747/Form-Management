namespace Form_Management.Domain.Errors.Error;

public abstract record AbstractError
{
    public string Code { get; }

    public string Message { get; }

    public ErrorType Type { get; }

    protected AbstractError(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }
}