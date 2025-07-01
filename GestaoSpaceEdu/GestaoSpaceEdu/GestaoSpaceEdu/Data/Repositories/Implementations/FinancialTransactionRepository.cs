using GestaoSpaceEdu.Client.Libraries.Utilities;
using GestaoSpaceEdu.Data.Repositories.Interfaces;
using GestaoSpaceEdu.Domain;
using GestaoSpaceEdu.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class FinancialTransactionRepository : IFinancialTransactionRepository
{
    private readonly ApplicationDbContext _context;

    public FinancialTransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<FinancialTransaction>> GetAllAsync(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize)
    {
        var items = await _context.FinancialTransactions.Where(x => x.CompanyId == companyId && x.TypeFinancialTransaction == type)
                                            .Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();

        var count = await _context.FinancialTransactions.Where(x => x.CompanyId == companyId && x.TypeFinancialTransaction == type).CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
        return new PaginatedList<FinancialTransaction>(items, pageIndex, totalPages);
    }

    public async Task<FinancialTransaction?> GetAsync(int id)
    {
        return await _context.FinancialTransactions.SingleOrDefaultAsync(x => x.Id == id);
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

        if (financialTransaction is not null)
        {
            _context.Remove(financialTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
