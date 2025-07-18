using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Enums;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class FinancialTransactionRepository : IFinancialTransactionRepository
{
    private readonly ApplicationDbContext _context;

    public FinancialTransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<FinancialTransaction>> GetAllAsync(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord = "")
    {
        var items = await _context.FinancialTransactions.Where(x => x.CompanyId == companyId && x.TypeFinancialTransaction == type)
                                                        .Where(x => x.Description.Contains(searchWord))
                                                        .OrderByDescending(x => x.ReferenceDate)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

        var count = await _context.FinancialTransactions.Where(x => x.CompanyId == companyId && x.TypeFinancialTransaction == type)
                                                        .Where(x => x.Description.Contains(searchWord))
                                                        .CountAsync();

        var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
        return new PaginatedList<FinancialTransaction>(items, pageIndex, totalPages);
    }

    public async Task<FinancialTransaction?> GetAsync(int id)
    {
        return await _context.FinancialTransactions
                             .Include(x => x.Category)
                             .Include(x => x.Account)
                             .Include(x => x.Documents)
                             .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(FinancialTransaction financialTransaction)
    {
        _context.FinancialTransactions.Add(financialTransaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FinancialTransaction financialTransaction)
    {
        _context.Update(financialTransaction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var financialTransaction = await GetAsync(id);
        await DeleteAsync(financialTransaction!);
    }

    public async Task DeleteAsync(FinancialTransaction financialTransaction)
    {
        if (financialTransaction is not null)
        {
            _context.Remove(financialTransaction);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetCountTransactionsSameGroupAsync(int id)
    {
        return await _context.FinancialTransactions
                             .Where(x => x.RepeatGroup == id)
                             .CountAsync();
    }

    public async Task<List<FinancialTransaction>> GetTransactionsSameGroupAsync(int id)
    {
        return await _context.FinancialTransactions
                     .Where(x => x.RepeatGroup == id)
                     .OrderBy(x => x.Id)
                     .ToListAsync();
    }
}
