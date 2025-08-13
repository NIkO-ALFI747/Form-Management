using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.ValueObjects.General;

namespace Form_Management.Domain.Errors.ErrorCodes.ValueObject;

public class ValueObjectErrorCode : EnumValueObject
{
    public static readonly ValueObjectErrorCode ValueAlreadyExists = new(nameof(ValueAlreadyExists));

    public static readonly ValueObjectErrorCode ValueIsRequired = new(nameof(ValueIsRequired));

    public static readonly ValueObjectErrorCode LengthIsInvalid = new(nameof(LengthIsInvalid));

    public static readonly ValueObjectErrorCode ValueIsInvalid = new(nameof(ValueIsInvalid));
    
    public static readonly ValueObjectErrorCode ValueCannotBeParsed = new(nameof(ValueCannotBeParsed));

    public static readonly ValueObjectErrorCode RangeIsInvalid = new(nameof(RangeIsInvalid));

    public static readonly ValueObjectErrorCode ValueObjectCreationError = new(nameof(ValueObjectCreationError));

    private static readonly ValueObjectErrorCode[] _all = [
        ValueAlreadyExists, ValueIsRequired, LengthIsInvalid, ValueIsInvalid, ValueCannotBeParsed, RangeIsInvalid, ValueObjectCreationError];

    private ValueObjectErrorCode(string value) : base(value) { }

    public static Result<EnumValueObject, ValueObjectValidationError> Create(string? value) => Create(value, _all);
}