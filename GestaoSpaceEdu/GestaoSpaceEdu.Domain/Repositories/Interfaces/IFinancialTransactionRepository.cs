﻿using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Enums;
using GestaoSpaceEdu.Domain.Libraries.Utilities;

namespace GestaoSpaceEdu.Domain.Repositories.Interfaces
{
    public interface IFinancialTransactionRepository
    {
        Task AddAsync(FinancialTransaction financialTransaction);
        Task DeleteAsync(int id);
        Task DeleteAsync(FinancialTransaction financialTransaction);
        Task<PaginatedList<FinancialTransaction>> GetAllAsync(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord = "");
        Task<FinancialTransaction?> GetAsync(int id);
        Task UpdateAsync(FinancialTransaction financialTransaction);
        Task<int> GetCountTransactionsSameGroupAsync(int id);
        Task<List<FinancialTransaction>> GetTransactionsSameGroupAsync(int id);
    }
}