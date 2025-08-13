using System.Security.Cryptography;
using System.Text;
using TPassword = Form_Management.Domain.Models.User.ValueObjects.Password;

namespace Form_Management.Application.Services.ValueObjects.Password.Validation;

public static class IsPasswordNotPwnedService
{
    private static Dictionary<Type, Func<Exception, bool>> ExceptionHandlers { get; set; } = new()
        {
            { typeof(HttpRequestException), ex => HandleHttpRequestException((HttpRequestException)ex)},
            { typeof(OperationCanceledException), _ => HandleOperationCanceledException() },
            { typeof(Exception), HandleGenericException }
        };

    public static async Task<bool> IsPasswordNotPwnedAsync(TPassword password, CancellationToken cancellationToken)
    {
        var hashBytes = SHA1.HashData(Encoding.UTF8.GetBytes(password.Value));
        var hashString = Convert.ToHexStringLower(hashBytes);
        var prefix = hashString[..5];
        var suffix = hashString[5..];
        return await IsPasswordNotPwnedAsync(prefix, suffix, cancellationToken);
    }

    private static async Task<bool> IsPasswordNotPwnedAsync(string prefix, string suffix, CancellationToken cancellationToken)
    {
        return await IsPasswordNotPwnedAsync(
            prefix, suffix, TryIsPasswordNotPwnedAsync, ExceptionHandlers, cancellationToken);
    }

    private static async Task<bool> IsPasswordNotPwnedAsync(
        string prefix, string suffix, Func<string, string, CancellationToken, Task<bool>> action,
        Dictionary<Type, Func<Exception, bool>> handlers, CancellationToken cancellationToken)
    {
        try
        {
            return await action(prefix, suffix, cancellationToken);
        }
        catch (Exception ex)
        {
            var handler = handlers.GetValueOrDefault(ex.GetType(), handlers[typeof(Exception)]);
            return handler(ex);
        }
    }

    private static async Task<bool> TryIsPasswordNotPwnedAsync(string prefix, string suffix, CancellationToken cancellationToken)
    {
        using HttpClient httpClient = new();
        var response = await httpClient.GetStringAsync($"https://api.pwnedpasswords.com/range/{prefix}", cancellationToken);
        return !response.Split('\n').Any(line =>
            line.Split(':')[0].Equals(suffix, StringComparison.OrdinalIgnoreCase));
    }

    private static bool HandleHttpRequestException(HttpRequestException ex)
    {
        Console.Error.WriteLine($"Error checking pwned password API: {ex.Message}");
        return true;
    }

    private static bool HandleOperationCanceledException()
    {
        Console.Error.WriteLine("Pwned password check was cancelled.");
        return true;
    }

    private static bool HandleGenericException(Exception ex)
    {
        Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        return true;
    }
}