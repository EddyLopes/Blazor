using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using System.Net.Http.Json;

namespace GestaoSpaceEdu.Client.Services;

public class CompanyService : ICompanyRepository
{
    private readonly HttpClient _httpClient;

    public CompanyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task AddAsync(Company company)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<Company>> GetAllAsync(string applicationUserId, int pageIndex, int pageSize, string searchWord = "")
    {
        var url = $"/api/companies?applicationUserId={applicationUserId}&pageIndex={pageIndex}&searchWord={searchWord}";
        var entities = await _httpClient.GetFromJsonAsync<PaginatedList<Company>>(url);

        return entities!;
    }

    public Task<Company?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Company company)
    {
        throw new NotImplementedException();
    }
}
