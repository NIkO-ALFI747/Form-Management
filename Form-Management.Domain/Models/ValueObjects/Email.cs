using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;

namespace Form_Management.Domain.Models.ValueObjects;

public class Email : ValueObject
{
    private const string emailFormatRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    private const string IS_MATCH_ERROR_MESSAGE = "Invalid {0} {1} format.";

    public static readonly string PropertyName = nameof(Value);

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email, ValueObjectValidationError> Create(string? value) =>
        ValueObjectValidationRules.ValidateIsNullOrWhiteSpace<Email>(value, PropertyName)
        .Bind(filledValue => ValueObjectValidationRules.ValidateIsMatch<Email>(
            filledValue, emailFormatRegex, GetIsMatchErrorMessage(), PropertyName))
        .Map(validatedValue => new Email(validatedValue));

    private static string GetIsMatchErrorMessage()
    {
        return string.Format(IS_MATCH_ERROR_MESSAGE, nameof(Email), PropertyName);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}