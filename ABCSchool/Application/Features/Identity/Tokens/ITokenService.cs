using ABCSharedLibrary.Models.Requests.Identity.Tokens;
using ABCSharedLibrary.Models.Responses.Identity.Tokens;

namespace Application.Features.Identity.Tokens;

public interface ITokenService
{
    Task<TokenResponse> LoginAsync(TokenRequest request);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
}
