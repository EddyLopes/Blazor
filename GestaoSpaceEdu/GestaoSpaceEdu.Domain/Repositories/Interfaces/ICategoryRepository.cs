﻿using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Libraries.Utilities;

namespace GestaoSpaceEdu.Domain.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task DeleteAsync(int id);
        Task<List<Category>> GetAllAsync(int companyId);
        Task<PaginatedList<Category>> GetAllAsync(int companyId, int pageIndex, int pageSize);
        Task<Category?> GetAsync(int id);
        Task UpdateAsync(Category category);
    }
}