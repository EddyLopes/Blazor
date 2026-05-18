using App.Infrastructure.Constants;
using Blazored.LocalStorage;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace App.Infrastructure.Services.Auth;

public class ApplicationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;

    public ApplicationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorageService.GetItemAsync<string>(StorageConstants.AuthToken);

        if(string.IsNullOrEmpty(savedToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

        var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJwt(savedToken), "jwt")));
        return state;
    }

    private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith('['))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                    claims.AddRange(parsedRoles.Select(roleName => new Claim(ClaimTypes.Role, roleName)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            keyValuePairs.TryGetValue(ClaimConstants.Permission, out var permissions);

            if (permissions != null)
            {
                if (permissions.ToString().Trim().StartsWith('['))
                {
                    var parsedPermissions = JsonSerializer.Deserialize<string[]>(permissions.ToString());
                    claims.AddRange(parsedPermissions.Select(permission => new Claim(ClaimConstants.Permission, permission)));
                }
                else
                {
                    claims.Add(new Claim(ClaimConstants.Permission, permissions.ToString()));
                }

                keyValuePairs.Remove(ClaimConstants.Permission);
            }
        }
    }

    private byte[] ParseBase64WithoutPadding(string base64Payoad)
    {
        switch (base64Payoad.Length % 4)
        {
            case 2: base64Payoad += "=="; break;
            case 3: base64Payoad += "="; break;
        }
        return Convert.FromBase64String(base64Payoad);
    }
}