using FluentValidation;
using FluentValidation.Results;
using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject;
using ExtValueObject = CSharpFunctionalExtensions.ValueObject;

namespace Form_Management.Application.Extensions.Validation.ValueObject;

public static class ValueObjectValidationRuleExtensions
{
    private const string ValueObjectContextKeyPrefix = "FluentValidation_CreatedValueObject_";

    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject, TError>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, TError>> factoryMethod,
        string errorMessage = "ValueObject creation error.")
        where TValueObject : ExtValueObject where TError : ValueObjectValidationError
    {
        return ruleBuilder.Custom((value, context) =>
        {
            ValueObjectCreationRule(factoryMethod, value, context, errorMessage);
        });
    }

    private static void ValueObjectCreationRule<T, TElement, TValueObject, TError>(
        Func<TElement, Result<TValueObject, TError>> factoryMethod,
        TElement value, ValidationContext<T> context, string errorMessage)
        where TValueObject : ExtValueObject where TError : ValueObjectValidationError
    {
        var result = factoryMethod(value);
        if (result.IsSuccess)
        {
            context.RootContextData[ValueObjectContextKeyPrefix + context.PropertyPath] = result.Value;
            return;
        }
        context.AddValidationFailure(result.Error, errorMessage, ValueObjectErrorCode.ValueObjectCreationError.Value);
    }

    public static IRuleBuilderOptions<T, TProperty> MustAsyncOnCreatedValueObject<T, TProperty, TValueObject>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        Func<TValueObject, CancellationToken, Task<bool>> predicate)
        where TValueObject : ExtValueObject
    {
        return ruleBuilder.MustAsync(async (rootObject, propertyObject, context, cancellationToken) =>
        {
            TValueObject? createdValueObject = null;
            if (context.RootContextData.TryGetValue(ValueObjectContextKeyPrefix + context.PropertyPath, out object? value))
                if (value != null) createdValueObject = (TValueObject)value;
            if (createdValueObject == null) return true;
            bool isValid = await predicate(createdValueObject, cancellationToken);
            return isValid;
        });
    }

    private static void AddValidationFailure<T, TError>(this ValidationContext<T> context,
        TError error, string errorMessage, string errorCode) where TError : ValueObjectValidationError
    {
        context.AddFailure(new ValidationFailure(context.PropertyPath, errorMessage)
        {
            ErrorCode = errorCode,
            CustomState = error
        });
    }
}