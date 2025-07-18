﻿using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Libraries.Utilities;

namespace GestaoSpaceEdu.Domain.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task DeleteAsync(int id);
        Task<List<Account>> GetAllAsync(int companyId);
        Task<PaginatedList<Account>> GetAllAsync(int companyId, int pageIndex, int pageSize, string searchWord = "");
        Task<Account?> GetAsync(int id);
        Task UpdateAsync(Account account);
    }
}