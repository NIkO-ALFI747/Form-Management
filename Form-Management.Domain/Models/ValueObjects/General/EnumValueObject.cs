using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject;
using Form_Management.Domain.Errors.Validation;

namespace Form_Management.Domain.Models.ValueObjects.General;

public class EnumValueObject : ValueObject
{
    public static readonly string PropertyName = nameof(Value);

    public string Value { get; }

    protected EnumValueObject(string value)
    {
        Value = value;
    }

    public static Result<EnumValueObject, ValueObjectValidationError> Create(string? value, EnumValueObject[] _all) =>
        ValueObjectValidationRules.ValidateIsNullOrWhiteSpace<ValueObjectErrorCode>(value, PropertyName)
        .Bind(filledValue => ValueObjectValidationRules.ValidateIsContained(filledValue, _all, PropertyName))
        .Map(validatedValue => new EnumValueObject(validatedValue));

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}