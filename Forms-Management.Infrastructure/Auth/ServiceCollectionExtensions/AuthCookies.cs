namespace Forms_Management.Infrastructure.Auth.ServiceCollectionExtensions;

public class AuthCookies(
    string AuthCookiesKey = "auth-cookies", 
    string AuthCookiesConfigurationKey = "AuthCookies:Key")
{
    public string Key { get; } = AuthCookiesKey;

    public string ConfigurationKey { get; } = AuthCookiesConfigurationKey;
}