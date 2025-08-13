using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.ValueObjects.General;

namespace Form_Management.Domain.Errors.ErrorCodes;

public class ErrorCode : EnumValueObject
{
    public static readonly ErrorCode HttpContextIsNull = new (nameof(HttpContextIsNull));

    private static readonly ErrorCode[] _all = [HttpContextIsNull];

    private ErrorCode(string value) : base(value) { }

    public static Result<EnumValueObject, ValueObjectValidationError> Create(string? value) => Create(value, _all);
}