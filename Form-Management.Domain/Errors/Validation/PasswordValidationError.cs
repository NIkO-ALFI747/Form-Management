using Form_Management.Domain.Errors.ErrorCodes.ValueObject.Password;
using Form_Management.Domain.Models.User.ValueObjects;

namespace Form_Management.Domain.Errors.Validation;

public record PasswordValidationError : ValueObjectValidationError
{
    private PasswordValidationError(string code, string message) :
        base(Password.PropertyName, code, message, nameof(Password))
    {}

    public static PasswordValidationError PasswordIsPwned(string message) =>
        new(PasswordErrorCode.PwnedPassword.Value, message);
}