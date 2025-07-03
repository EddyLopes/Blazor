using GestaoSpaceEdu.Domain.Enums;
using GestaoSpaceEdu.Domain.Libraries.Utilities;

namespace GestaoSpaceEdu.Domain.Repositories.Interfaces
{
    public interface IFinancialTransactionRepository
    {
        Task AddAsync(FinancialTransaction financialTransaction);
        Task DeleteAsync(int id);
        Task<PaginatedList<FinancialTransaction>> GetAllAsync(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord = "");
        Task<FinancialTransaction?> GetAsync(int id);
        Task UpdateAsync(FinancialTransaction financialTransaction);
    }
}