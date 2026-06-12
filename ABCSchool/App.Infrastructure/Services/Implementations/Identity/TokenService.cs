using ABCSharedLibrary.Models.Requests.Identity.Tokens;
using ABCSharedLibrary.Models.Responses.Identity.Tokens;
using ABCSharedLibrary.Wrappers;
using App.Infrastructure.Constants;
using App.Infrastructure.Extensions;
using App.Infrastructure.Services.Auth;
using App.Infrastructure.Services.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace App.Infrastructure.Services.Implementations.Identity;

public class TokenService : ITokenService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ApiSettings _apiSettings;

    public TokenService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, ApiSettings apiSettings)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
        _apiSettings = apiSettings;
    }

    public async Task<IResponseWrapper> LoginAsync(string tenant, TokenRequest request)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _apiSettings.TokenEndpoints.Login)
        {
            Content = JsonContent.Create(request)
        };

        AddTenantHeader(httpRequest, headerName: "tenant", value: tenant);

        var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        var result = await response.WrapToResponse<TokenResponse>();

        if(result.IsSuccessful)
        {
            var token = result.Data.Jwt;
            var refreshToken = result.Data.RefreshToken;

            await _localStorageService.SetItemAsync(StorageConstants.AuthToken, token);
            await _localStorageService.SetItemAsync(StorageConstants.RefreshToken, refreshToken);

            ((ApplicationStateProvider)_authenticationStateProvider).MarkUserAuthenticated(request.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await ResponseWrapper.SuccessAsync();
        }
        else 
        { 
            return await ResponseWrapper.FailAsync(messages: result.Messages);
        }
    }

    private static void AddTenantHeader(HttpRequestMessage request, string headerName, string value)
    {
        if (string.IsNullOrEmpty(value) || request.Headers.Contains(headerName))
            return;

        request.Headers.TryAddWithoutValidation(headerName, value);
    }
}
