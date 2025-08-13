using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.ValueObjects.General;

namespace Form_Management.Domain.Errors.ErrorCodes.ValueObject.Password;

public class PasswordErrorCode : EnumValueObject
{
    public static readonly PasswordErrorCode PwnedPassword = new(nameof(PwnedPassword));

    private static readonly PasswordErrorCode[] _all = [PwnedPassword];

    private PasswordErrorCode(string value) : base(value) { }

    public static Result<EnumValueObject, ValueObjectValidationError> Create(string? value) => Create(value, _all);
}