using Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class WasmHostBuilderEntensions
{
    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore();

        return builder;
    }

    private static void RegisterPermissions(AuthorizationOptions options)
    {
        foreach (var permission in SchoolPermissions.All)
        {
            options.AddPolicy(permission.Name, policy => policy.RequireClaim(ClaimConstants.Permission, permission.Name));
        }
    }
}
