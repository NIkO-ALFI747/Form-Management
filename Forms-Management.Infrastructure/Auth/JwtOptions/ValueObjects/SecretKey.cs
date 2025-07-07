using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;

namespace Forms_Management.Infrastructure.Auth.JwtOptions.ValueObjects;

public class SecretKey : ValueObject
{
    private const int MIN_LENGTH = 64;

    public static readonly string PropertyName = nameof(Value);

    public string Value { get; }

    private SecretKey(string value)
    {
        Value = value;
    }

    public static Result<SecretKey, ValueObjectValidationError> Create(string? value) =>
        ValueObjectValidationRules.ValidateIsNullOrEmpty<SecretKey>(value, PropertyName)
        .Bind(filledValue => ValueObjectValidationRules.ValidateLength<SecretKey>(filledValue, MIN_LENGTH, PropertyName))
        .Map(validatedValue => new SecretKey(validatedValue));

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}