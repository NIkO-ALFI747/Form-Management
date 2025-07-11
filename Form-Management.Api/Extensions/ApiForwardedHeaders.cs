﻿using Microsoft.AspNetCore.HttpOverrides;

namespace Form_Management.Api.Extensions;

public static class ApiForwardedHeaders
{
    public static void UseApiForwardedHeaders(this WebApplication app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders =
            ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedProto
        });
    }
}