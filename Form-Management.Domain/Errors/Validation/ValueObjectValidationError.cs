using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject;

namespace Form_Management.Domain.Errors.Validation;

public record ValueObjectValidationError : AbstractError
{
    public string? ValueObjectName { get; }

    public string PropertyName { get; }

    protected ValueObjectValidationError(string propertyName, string code, string message, string valueObjectName) :
        base(code, message, ErrorType.Validation)
    {
        PropertyName = propertyName;
        ValueObjectName = valueObjectName;
    }

    private ValueObjectValidationError(string code, string message, ErrorType errorType, string propertyName, string? valueObjectName) :
        base(code, message, errorType)
    {
        PropertyName = propertyName;
        ValueObjectName = valueObjectName;
    }

    public static ValueObjectValidationError SetMessage(ValueObjectValidationError error, string newMessage) 
    {
        return new (error.Code, newMessage, error.Type, error.PropertyName, error.ValueObjectName);
    }

    public static ValueObjectValidationError ValueAlreadyExist(string message, string propertyName, string valueObjectName) =>
        new(propertyName, ValueObjectErrorCode.ValueAlreadyExists.Value, message, valueObjectName);
    
    public static ValueObjectValidationError ValueIsRequired(string message, string propertyName, string valueObjectName) =>
        new(propertyName, ValueObjectErrorCode.ValueIsRequired.Value, message, valueObjectName);

    public static ValueObjectValidationError ValueIsInvalid(string message, string propertyName, string valueObjectName) =>
        new(propertyName, ValueObjectErrorCode.ValueIsInvalid.Value, message, valueObjectName);

    public static ValueObjectValidationError LengthIsInvalid(string message, string propertyName, string valueObjectName) =>
        new(propertyName, ValueObjectErrorCode.LengthIsInvalid.Value, message, valueObjectName);

    public static ValueObjectValidationError ValueCannotBeParsed(string message, string propertyName, string valueObjectName) =>
        new(propertyName, ValueObjectErrorCode.ValueCannotBeParsed.Value, message, valueObjectName);

    public static ValueObjectValidationError RangeIsInvalid(string message, string propertyName, string valueObjectName) =>
        new(propertyName, ValueObjectErrorCode.RangeIsInvalid.Value, message, valueObjectName);
}