using FluentValidation;
using Form_Management.Application.Extensions.Validation.ValueObject;
using Form_Management.Application.Interfaces.Services.ValueObjects.Password;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject.Password;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.User.ValueObjects;
using Form_Management.Domain.Models.ValueObjects;

namespace Form_Management.Application.Contracts.Auth.SignUp;

public class SignUpUserRequestValidator : AbstractValidator<SignUpUserRequest>
{
    private readonly IPasswordService _passwordService;

    public SignUpUserRequestValidator(IPasswordService passwordService)
    {
        _passwordService = passwordService;
        RuleFor(request => request.Name).MustBeValueObject(FilledString.Create);
        RuleFor(request => request.Email).MustBeValueObject(Email.Create);
        RuleFor(request => request.Password).MustBeValueObject(Password.Create);
        RuleForIsPasswordNotPwnedAsync();
    }

    private void RuleForIsPasswordNotPwnedAsync()
    {
        RuleFor(request => request.Password)
            .MustAsyncOnCreatedValueObject<SignUpUserRequest, string, Password>(_passwordService.IsPasswordNotPwnedAsync)
            .WithMessage("This password has been exposed in a data breach. Please choose a more secure password.")
            .WithErrorCode(PasswordErrorCode.PwnedPassword.Value)
            .WithState(request => PasswordValidationError.PasswordIsPwned(
                "This password has been exposed in a data breach. Please choose a more secure password."));
    }
}