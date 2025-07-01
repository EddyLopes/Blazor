using GestaoSpaceEdu.Client.Libraries.Utilities;
using GestaoSpaceEdu.Domain;

namespace GestaoSpaceEdu.Data.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task DeleteAsync(int id);
        Task<PaginatedList<Company>> GetAllAsync(string applicationUserId, int pageIndex, int pageSize);
        Task<Company?> GetAsync(int id);
        Task UpdateAsync(Company company);
    }
}