using FluentValidation;
using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails;
using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails.ValidationErrors;
using Form_Management.Api.Extensions.ServiceCollection.Auth;
using Form_Management.Application.Contracts.Auth.SignUp;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Domain.Errors.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SignUpController(IValidator<SignUpUserRequest> signUpUserRequestValidator,
ISignUpService signUpService, IConfiguration configuration) : ControllerBase
{
    private readonly IValidator<SignUpUserRequest> _signUpUserRequestValidator = signUpUserRequestValidator;

    private readonly ISignUpService _signUpService = signUpService;

    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpUserRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _signUpUserRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return validationResult.ToValidationErrorsProblemDetailsResult<ValueObjectValidationError>(HttpContext);
        var token = await _signUpService.SignUp(request, cancellationToken);
        if (token.IsFailure) return token.Error.MapErrorToProblemDetailsResult(HttpContext);
        HttpContext.Response.Cookies.Append(_configuration[AuthCookies.ConfigurationKey] ?? AuthCookies.Key, token.Value);
        return Ok();
    }
}