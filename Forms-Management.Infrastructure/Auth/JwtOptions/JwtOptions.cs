using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using TSecretKey = Forms_Management.Infrastructure.Auth.JwtOptions.ValueObjects.SecretKey;
using TExpiresHours = Forms_Management.Infrastructure.Auth.JwtOptions.ValueObjects.ExpiresHours;

namespace Forms_Management.Infrastructure.Auth.JwtOptions;

public class JwtOptions
{
    private const string ERROR_MESSAGE = "Failed to set {0}.\n{1}";

    public TSecretKey? SecretKey { get; private set; }

    public TExpiresHours? ExpiresHours { get; private set; }

    public string? SecretKeySetter
    {
        set
        {
            var secretKeyResult = TSecretKey.Create(value);
            if (secretKeyResult.IsFailure)
                throw new ArgumentException(GetErrorMessage(secretKeyResult.Error.Message));
            SecretKey = secretKeyResult.Value;
        }
    }

    public string? ExpiresHoursSetter
    {
        set
        {
            var expiresHoursResult = TExpiresHours.Create(value);
            if (expiresHoursResult.IsFailure)
                throw new ArgumentException(GetErrorMessage(expiresHoursResult.Error.Message));
            ExpiresHours = expiresHoursResult.Value;
        }
    }

    public JwtOptions(){}

    private JwtOptions(TSecretKey secretKey, TExpiresHours expiresHours)
    {
        SecretKey = secretKey;
        ExpiresHours = expiresHours;
    }

    public static Result<JwtOptions, ValueObjectValidationError> Create(string? secretKey, string? expiresHours) =>
        TSecretKey.Create(secretKey)
        .Bind(secretKeyValue => TExpiresHours.Create(expiresHours)
        .Map(expiresHoursValue => new JwtOptions(secretKeyValue, expiresHoursValue)))
        .MapError(originalError => ValueObjectValidationError.SetMessage(originalError, GetErrorMessage(originalError.Message)));

    private static string GetErrorMessage(string internalMessage)
    {
        return string.Format(ERROR_MESSAGE, nameof(JwtOptions), internalMessage);
    }
}