using ABCSharedLibrary.Models.Requests.Identity.Tokens;
using ABCSharedLibrary.Wrappers;

namespace App.Infrastructure.Services.Identity;

public interface ITokenService
{
    Task<IResponseWrapper> LoginAsync(string tenant, TokenRequest request);
}
