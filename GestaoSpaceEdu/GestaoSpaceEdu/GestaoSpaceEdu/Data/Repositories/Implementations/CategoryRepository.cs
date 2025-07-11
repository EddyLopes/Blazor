﻿using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Libraries.Utilities;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Category>> GetAllAsync(int companyId, int pageIndex, int pageSize)
    {
        var items = await _context.Categories.Where(x => x.CompanyId == companyId)
                                             .OrderBy(x => x.Name)
                                             .Skip((pageIndex - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();

        var count = await _context.Categories.Where(x => x.CompanyId == companyId).CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
        return new PaginatedList<Category>(items, pageIndex, totalPages);
    }

    public async Task<List<Category>> GetAllAsync(int companyId)
    {
        return await _context.Categories.Where(x => x.CompanyId == companyId)
                                        .ToListAsync();
    }

    public async Task<Category?> GetAsync(int id)
    {
        return await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetAsync(id);

        if (category is not null)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
