using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;

namespace Forms_Management.Infrastructure.Auth.JwtOptions.ValueObjects;

public class ExpiresHours : ValueObject
{
    private const int MIN_VALUE = 1;

    public static readonly string PropertyName = nameof(Value);

    public int Value { get; }

    private ExpiresHours(int value)
    {
        Value = value;
    }

    public static Result<ExpiresHours, ValueObjectValidationError> Create(string? value) =>
        ValueObjectValidationRules.ValidateIsNullOrEmpty<ExpiresHours>(value, PropertyName)
        .Bind(filledValue => ValueObjectValidationRules.TryParse<ExpiresHours>(filledValue, PropertyName))
        .Bind(parsedValue => ValueObjectValidationRules.ValidateRange<ExpiresHours>(parsedValue, MIN_VALUE, PropertyName))
        .Map(validatedValue => new ExpiresHours(validatedValue));

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}