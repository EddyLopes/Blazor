using ABCSharedLibrary.Models.Requests.Identity.Tokens;
using ABCSharedLibrary.Wrappers;
using App.Infrastructure.Services.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

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

    public Task<IResponseWrapper> LoginAsync(string tenant, TokenRequest request)
    {
        throw new NotImplementedException();
    }
}
