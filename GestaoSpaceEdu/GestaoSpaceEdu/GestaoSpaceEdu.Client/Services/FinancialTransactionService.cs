using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Enums;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using System.Net.Http.Json;

namespace GestaoSpaceEdu.Client.Services;

public class FinancialTransactionService : IFinancialTransactionRepository
{
    private readonly HttpClient _httpClient;

    public FinancialTransactionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task AddAsync(FinancialTransaction financialTransaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(FinancialTransaction financialTransaction)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<FinancialTransaction>> GetAllAsync(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord = "")
    {
        var url = $"/api/financialtransactions?companyId={companyId}&pageIndex={pageIndex}&searchWord={searchWord}&type={type}";
        var entities = await _httpClient.GetFromJsonAsync<PaginatedList<FinancialTransaction>>(url);

        return entities!;
    }

    public Task<FinancialTransaction?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountTransactionsSameGroupAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<FinancialTransaction>> GetTransactionsSameGroupAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(FinancialTransaction financialTransaction)
    {
        throw new NotImplementedException();
    }
}
