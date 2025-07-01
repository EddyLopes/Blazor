using GestaoSpaceEdu.Client.Libraries.Utilities;
using GestaoSpaceEdu.Data.Repositories.Interfaces;
using GestaoSpaceEdu.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Account>> GetAllAsync(int companyId, int pageIndex, int pageSize, string searchWord = "")
    {
        var items = await _context.Accounts.Where(x => x.CompanyId == companyId)
                                           .Where(x => x.Description.Contains(searchWord))
                                           .OrderBy(x => x.Description)
                                           .Skip((pageIndex - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToListAsync();

        var count = await _context.Accounts.Where(x => x.CompanyId == companyId)
                                           .Where(x => x.Description.Contains(searchWord))
                                           .CountAsync();

        var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
        return new PaginatedList<Account>(items, pageIndex, totalPages);
    }

    public async Task<List<Account>> GetAllAsync(int companyId)
    {
        return await _context.Accounts.Where(x => x.CompanyId == companyId)
                                      .ToListAsync();
    }

    public async Task<Account?> GetAsync(int id)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var account = await GetAsync(id);

        if (account is not null)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
        }
    }

}
