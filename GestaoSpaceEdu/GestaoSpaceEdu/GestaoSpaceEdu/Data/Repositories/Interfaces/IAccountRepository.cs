using GestaoSpaceEdu.Client.Libraries.Utilities;
using GestaoSpaceEdu.Domain;

namespace GestaoSpaceEdu.Data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task DeleteAsync(int id);
        Task<List<Account>> GetAllAsync(int companyId);
        Task<PaginatedList<Account>> GetAllAsync(int companyId, int pageIndex, int pageSize);
        Task<Account?> GetAsync(int id);
        Task UpdateAsync(Account account);
    }
}