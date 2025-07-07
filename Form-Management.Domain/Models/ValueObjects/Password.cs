using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using System.Text.RegularExpressions;

namespace Form_Management.Domain.Models.User.ValueObjects;

public partial class Password : ValueObject
{
    private static readonly Regex PasswordComplexityRegex = PasswordRegex();

    private const int MIN_LENGTH = 8;

    private const int MAX_LENGTH = 100;

    private const string IS_MATCH_ERROR_MESSAGE =
        "{0} {1} must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";

    public static readonly string PropertyName = nameof(Value);

    public string Value { get; }

    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password, ValueObjectValidationError> Create(string? value) => 
        ValueObjectValidationRules.ValidateIsNullOrWhiteSpace<Password>(value, PropertyName)
        .Bind(filledValue => ValueObjectValidationRules.ValidateLength<Password>(filledValue, MIN_LENGTH, MAX_LENGTH, PropertyName))
        .Bind(validatedLengthValue => ValueObjectValidationRules.ValidateIsMatch<Password>(
            validatedLengthValue, PasswordComplexityRegex, GetIsMatchErrorMessage(), PropertyName))
        .Map(validatedValue => new Password(validatedValue));
    
    private static string GetIsMatchErrorMessage()
    {
        return string.Format(IS_MATCH_ERROR_MESSAGE, nameof(Password), PropertyName);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", RegexOptions.Compiled)]
    private static partial Regex PasswordRegex();
}