using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Form_Management.Api.Extensions.ServiceCollection.DataProtection;

public static class ApiX509Certificate2
{
    private const string CERTIFICATE_BASE64_ENV_NAME = "CERTIFICATE_BASE64";

    private const string CERTIFICATE_PASSWORD_ENV_NAME = "CERTIFICATE_PASSWORD";

    public static X509Certificate2 LoadAndValidateX509Certificate(IConfiguration configuration)
    {
        X509Certificate2? cert = null;
        try { cert = LoadX509Certificate(configuration); }
        catch (CryptographicException cryptoEx) { HandleLoadX509CertCryptException(cryptoEx); }
        catch (Exception ex) { HandleLoadX509CertException(ex); }
        return cert!;
    }

    private static X509Certificate2 LoadX509Certificate(IConfiguration configuration)
    {
        var base64CertString = GetBase64CertString(configuration);
        var certPassword = GetCertPassword(configuration);
        byte[] certBytes = Convert.FromBase64String(base64CertString);
        var cert = X509CertificateLoader.LoadPkcs12(certBytes, certPassword);
        return cert;
    }

    private static string GetBase64CertString(IConfiguration configuration)
    {
        var base64CertString = configuration[CERTIFICATE_BASE64_ENV_NAME];
        return ValidateBase64CertString(base64CertString);
    }

    private static string ValidateBase64CertString(string? base64CertString)
    {
        if (string.IsNullOrEmpty(base64CertString))
            throw new InvalidOperationException($"Environment variable {CERTIFICATE_BASE64_ENV_NAME} is not set. Cannot configure Data Protection certificate.");
        return base64CertString;
    }

    private static string GetCertPassword(IConfiguration configuration)
    {
        var certPassword = configuration[CERTIFICATE_PASSWORD_ENV_NAME];
        return ValidateCertPassword(certPassword);
    }

    private static string ValidateCertPassword(string? certPassword)
    {
        if (string.IsNullOrEmpty(certPassword))
            throw new InvalidOperationException($"Environment variable {CERTIFICATE_PASSWORD_ENV_NAME} is not set. Cannot configure Data Protection certificate password.");
        return certPassword;
    }

    private static void HandleLoadX509CertCryptException(CryptographicException cryptoEx)
    {
        throw new InvalidOperationException($"Cryptographic error loading certificate: {cryptoEx.Message}" +
            "Failed to load Data Protection certificate due to cryptographic error. Please check certificate format and password.", cryptoEx);
    }

    private static void HandleLoadX509CertException(Exception ex)
    {
        throw new InvalidOperationException($"General error loading certificate from Base64 string: {ex.Message}" +
            "Failed to load Data Protection certificate from environment variables. See logs for details.", ex);
    }
}