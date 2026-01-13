using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Company>> GetAllAsync(string applicationUserId, int pageIndex, int pageSize, string searchWord = "")
    {
        var items = await _context.Companies.Where(x => x.UserId == applicationUserId)
                                            .Where(x => x.TradeName.Contains(searchWord) || x.LegalName.Contains(searchWord))
                                            .OrderBy(x => x.TradeName)
                                            .Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();

        var count = await _context.Companies.Where(x => x.UserId == applicationUserId)
                                            .Where(x => x.TradeName.Contains(searchWord) || x.LegalName.Contains(searchWord))
                                            .CountAsync();

        var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
        return new PaginatedList<Company>(items, pageIndex, totalPages);
    }

    public async Task<Company?> GetAsync(int id)
    {
        return await _context.Companies.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        _context.Update(company);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var company = await GetAsync(id);

        if (company is not null)
        {
            _context.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}
