namespace GestaoSpaceEdu.Data.Repositories.Implementations;

public class DocumentAttachmentRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentAttachmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}
