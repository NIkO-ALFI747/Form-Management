using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails;
using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails.BadRequestErrors;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject.Password;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.User.ValueObjects;
using Form_Management.Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers.Auth.Login;

public static class MappingLoginErrors
{
    public static IActionResult MapLoginErrorsToProblemDetailsResult(this AbstractError abstractError, HttpContext context)
    {
        AbstractError? loginError = null;
        if (abstractError is ValueObjectValidationError validationError)
        {
            if (validationError.ValueObjectName == nameof(Password))
                loginError = ValueObjectValidationError.SetMessage(validationError, "Provided password is incorrect!");
            if (validationError.ValueObjectName == nameof(Email))
                loginError = ValueObjectValidationError.SetMessage(validationError, "Provided email is incorrect!");
        }
        if (abstractError is Error error)
        {
            if (
                (error.Code == PasswordErrorCode.PwnedPassword.Value) ||
                (error.Code == ValueObjectErrorCode.ValueIsInvalid.Value)
            )
                loginError = Error.SetMessage(error, "Provided password is incorrect!");
            if (error.Code == DbErrorCode.RecordsNotFound.Value)
                loginError = Error.SetMessage(error, "Provided email is incorrect!");
        }
        if (loginError != null)
            return loginError.ToBadRequestErrorProblemDetailsResult(context, "Failed to login!");
        return abstractError.MapErrorToProblemDetailsResult(context);
    }
}