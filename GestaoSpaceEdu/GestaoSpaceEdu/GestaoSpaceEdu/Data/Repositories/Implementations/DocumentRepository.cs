using GestaoSpaceEdu.Domain;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Document?> GetAsync(int id)
    {
        return await _context.Documents.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Document document)
    {
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Document document)
    {
        _context.Update(document);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var document = await GetAsync(id);

        if (document is not null)
        {
            _context.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}
