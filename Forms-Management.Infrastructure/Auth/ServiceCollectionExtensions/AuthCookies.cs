namespace Forms_Management.Infrastructure.Auth.ServiceCollectionExtensions;

public class AuthCookies(
    string AuthCookiesKey = "form_management_auth_cookies", 
    string AuthCookiesConfigurationKey = "AUTH_COOKIES_KEY")
{
    public string Key { get; } = AuthCookiesKey;

    public string ConfigurationKey { get; } = AuthCookiesConfigurationKey;
}