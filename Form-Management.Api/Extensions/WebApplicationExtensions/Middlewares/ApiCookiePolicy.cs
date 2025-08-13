using Microsoft.AspNetCore.CookiePolicy;

namespace Form_Management.Api.Extensions.WebApplicationExtensions.Middlewares;

public static class ApiCookiePolicy
{
    public static void UseApiCookiePolicy(this WebApplication app)
    {
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });
    }
}