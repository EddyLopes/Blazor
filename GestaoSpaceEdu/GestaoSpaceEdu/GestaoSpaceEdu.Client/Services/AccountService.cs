using GestaoSpaceEdu.Domain;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using System.Net.Http.Json;

namespace GestaoSpaceEdu.Client.Services;

public class AccountService : IAccountRepository
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task AddAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>> GetAllAsync(int companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<Account>> GetAllAsync(int companyId, int pageIndex, int pageSize, string searchWord = "")
    {
        var url = $"/api/accounts?companyId={companyId}&pageIndex={pageIndex}&searchWord={searchWord}";
        var entities = await _httpClient.GetFromJsonAsync<PaginatedList<Account>>(url);

        return entities!;
    }

    public Task<Account?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Account account)
    {
        throw new NotImplementedException();
    }
}
