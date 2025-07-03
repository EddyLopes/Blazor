using GestaoSpaceEdu.Domain.Libraries.Utilities;

namespace GestaoSpaceEdu.Domain.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task DeleteAsync(int id);
        Task<PaginatedList<Company>> GetAllAsync(string applicationUserId, int pageIndex, int pageSize, string searchWord = "");
        Task<Company?> GetAsync(int id);
        Task UpdateAsync(Company company);
    }
}