namespace GestaoSpaceEdu.Domain.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task AddAsync(Document document);
        Task DeleteAsync(int id);
        Task<Document?> GetAsync(int id);
        Task UpdateAsync(Document document);
    }
}