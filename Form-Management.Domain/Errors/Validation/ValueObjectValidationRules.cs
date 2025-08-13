using CSharpFunctionalExtensions;
using Form_Management.Domain.Models.ValueObjects.General;
using System.Text.RegularExpressions;

namespace Form_Management.Domain.Errors.Validation;

public static class ValueObjectValidationRules
{
    public static Result<string, ValueObjectValidationError> ValidateIsNullOrWhiteSpace<TValueObject>(string? value, string propertyName) =>
        string.IsNullOrWhiteSpace(value)
            ? Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.ValueIsRequired($"{typeof(TValueObject).Name} {propertyName} is required.", propertyName, typeof(TValueObject).Name))
            : Result.Success<string, ValueObjectValidationError>(value);

    public static Result<string, ValueObjectValidationError> ValidateIsContained<TValueObject>(string value, TValueObject[] all, string propertyName)
        where TValueObject : EnumValueObject =>
        !all.Any(v => v.Value == value)
            ? Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.ValueIsInvalid($"{typeof(TValueObject).Name} {propertyName} is invalid.", propertyName, typeof(TValueObject).Name))
            : Result.Success<string, ValueObjectValidationError>(value);

    public static Result<string, ValueObjectValidationError> ValidateIsNullOrEmpty<TValueObject>(string? value, string propertyName) =>
        string.IsNullOrEmpty(value)
            ? Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.ValueIsRequired($"{typeof(TValueObject).Name} {propertyName} is required.", propertyName, typeof(TValueObject).Name))
            : Result.Success<string, ValueObjectValidationError>(value);

    public static Result<string, ValueObjectValidationError> ValidateIsMatch<TValueObject>(string value, string regex, string errorMessage, string propertyName) =>
        Regex.IsMatch(value, regex)
            ? Result.Success<string, ValueObjectValidationError>(value)
            : Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.ValueIsInvalid(errorMessage, propertyName, typeof(TValueObject).Name));

    public static Result<string, ValueObjectValidationError> ValidateIsMatch<TValueObject>(string value, Regex regex, string errorMessage, string propertyName) =>
        regex.IsMatch(value)
            ? Result.Success<string, ValueObjectValidationError>(value)
            : Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.ValueIsInvalid(errorMessage, propertyName, typeof(TValueObject).Name));

    public static Result<string, ValueObjectValidationError> ValidateLength<TValueObject>(string value, int minLength, int maxLength, string propertyName) =>
        value.Length >= minLength && value.Length <= maxLength
            ? Result.Success<string, ValueObjectValidationError>(value)
            : Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.LengthIsInvalid($"{typeof(TValueObject).Name} {propertyName} must be at least {minLength} characters long and no more than {maxLength}.", propertyName, typeof(TValueObject).Name));

    public static Result<string, ValueObjectValidationError> ValidateLength<TValueObject>(string value, int minLength, string propertyName) =>
        value.Length >= minLength
            ? Result.Success<string, ValueObjectValidationError>(value)
            : Result.Failure<string, ValueObjectValidationError>(ValueObjectValidationError.LengthIsInvalid($"{typeof(TValueObject).Name} {propertyName} must have at least {minLength} characters.", propertyName, typeof(TValueObject).Name));

    public static Result<int, ValueObjectValidationError> TryParse<TValueObject>(string value, string propertyName) =>
        int.TryParse(value, out int parsedValue)
            ? Result.Success<int, ValueObjectValidationError>(parsedValue)
            : Result.Failure<int, ValueObjectValidationError>(ValueObjectValidationError.ValueCannotBeParsed($"Failed to parse {typeof(TValueObject).Name} {propertyName}.", propertyName, typeof(TValueObject).Name));

    public static Result<int, ValueObjectValidationError> ValidateRange<TValueObject>(int value, int minValue, string propertyName) =>
        value >= minValue
            ? Result.Success<int, ValueObjectValidationError>(value)
            : Result.Failure<int, ValueObjectValidationError>(ValueObjectValidationError.RangeIsInvalid($"{typeof(TValueObject).Name} {propertyName} should be more than or equal to {minValue}.", propertyName, typeof(TValueObject).Name));
}