using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using System.Net.Http.Json;

namespace GestaoSpaceEdu.Client.Services;

public class CategoryService : ICategoryRepository
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task AddAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetAllAsync(int companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<Category>> GetAllAsync(int companyId, int pageIndex, int pageSize)
    {
        var url = $"/api/categories?companyId={companyId}&pageIndex={pageIndex}";
        var entities = await _httpClient.GetFromJsonAsync<PaginatedList<Category>>(url);

        return entities!;
    }

    public Task<Category?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category category)
    {
        throw new NotImplementedException();
    }
}
