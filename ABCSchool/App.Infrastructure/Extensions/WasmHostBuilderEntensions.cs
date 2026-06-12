using App.Infrastructure.Services.Auth;
using App.Infrastructure.Services.Identity;
using App.Infrastructure.Services.Implementations.Identity;
using Blazored.LocalStorage;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class WasmHostBuilderEntensions
{
    private const string _apiClientName = "ABC School Api";

    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore(RegisterPermissions)
                        .AddBlazoredLocalStorage()
                        .AddMudServices(config => 
                        {
                            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                            config.SnackbarConfiguration.HideTransitionDuration = 100;
                            config.SnackbarConfiguration.ShowTransitionDuration = 100;
                            config.SnackbarConfiguration.VisibleStateDuration = 5000;
                            config.SnackbarConfiguration.ShowCloseIcon = true;
                        })
                        .AddScoped<ApplicationStateProvider>()
                        .AddScoped<AuthenticationStateProvider, ApplicationStateProvider>()
                        .AddTransient<AuthenticationHeaderHandler>()
                        .AddScoped<ITokenService, TokenService>()
                        .AddScoped(sp => 
                            sp.GetRequiredService<IHttpClientFactory>()
                              .CreateClient(_apiClientName)
                              .EnableIntercept(sp))
                        .AddHttpClient(_apiClientName, client =>
                        {
                            client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:BaseApiUrl").Get<string>()!);
                        })
                        .AddHttpMessageHandler<AuthenticationHeaderHandler>();
        builder.Services.AddHttpClientInterceptor();

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
