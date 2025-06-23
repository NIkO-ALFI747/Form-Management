using Form_Management.Api.DataAccess;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Form_Management.Api.Extensions;

public static class ApiDataProtection
{
    public static void AddApiDataProtection(this IServiceCollection services)
    {
        services.AddApiDbContext<DataProtectionKeyDbContext>();
        services.AddDataProtection()
            .PersistKeysToDbContext<DataProtectionKeyDbContext>()
            .ProtectKeysWithCertificate(LoadValidatedX509Certificate(services));
    }

    private static X509Certificate2 LoadValidatedX509Certificate(IServiceCollection services)
    {
        X509Certificate2? cert = null;
        try { cert = LoadX509Certificate(services); }
        catch (CryptographicException cryptoEx) { HandleLoadX509CertCryptExc(cryptoEx); }
        catch (Exception ex) { HandleLoadX509CertExc(ex); }
        return cert!;
    }

    private static X509Certificate2 LoadX509Certificate(IServiceCollection services)
    {
        var base64CertString = GetBase64CertString(services);
        var certPassword = GetCertPassword(services);
        byte[] certBytes = Convert.FromBase64String(base64CertString);
        var cert = X509CertificateLoader.LoadPkcs12(certBytes, certPassword);
        return cert;
    }

    private static string GetBase64CertString(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var base64CertString = configuration["CERTIFICATE_BASE64"];
        return ValidateBase64CertString(base64CertString);
    }

    private static string ValidateBase64CertString(string? base64CertString)
    {
        if (string.IsNullOrEmpty(base64CertString))
            throw new InvalidOperationException(
                "Environment variable 'CERTIFICATE_BASE64' is not set. Cannot configure Data Protection certificate."
            );
        return base64CertString;
    }

    private static string GetCertPassword(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var certPassword = configuration["CERTIFICATE_PASSWORD"];
        return ValidateCertPassword(certPassword);
    }

    private static string ValidateCertPassword(string? certPassword)
    {
        if (string.IsNullOrEmpty(certPassword))
            throw new InvalidOperationException(
                "Environment variable 'CERTIFICATE_PASSWORD' is not set. Cannot configure Data Protection certificate password."
            );
        return certPassword;
    }

    private static void HandleLoadX509CertCryptExc(CryptographicException cryptoEx)
    {
        Console.Error.WriteLine($"Cryptographic error loading certificate: {cryptoEx.Message}");
        Console.Error.WriteLine($"Stack Trace: {cryptoEx.StackTrace}");
        throw new InvalidOperationException(
            "Failed to load Data Protection certificate due to cryptographic error. Please check certificate format and password.",
            cryptoEx
        );
    }

    private static void HandleLoadX509CertExc(Exception ex)
    {
        Console.Error.WriteLine($"General error loading certificate from Base64 string: {ex.Message}");
        Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
        throw new InvalidOperationException(
            "Failed to load Data Protection certificate from environment variables. See logs for details.",
            ex
        );
    }
}