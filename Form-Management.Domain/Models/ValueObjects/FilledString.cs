using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;

namespace Form_Management.Domain.Models.ValueObjects;

public class FilledString : ValueObject
{
    public static readonly string PropertyName = nameof(Value);

    public string Value { get; }

    private FilledString(string value)
    {
        Value = value;
    }

    public static Result<FilledString, ValueObjectValidationError> Create(string? value) =>
        ValueObjectValidationRules.ValidateIsNullOrWhiteSpace<FilledString>(value, PropertyName)
        .Map(validatedValue => new FilledString(validatedValue));

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}