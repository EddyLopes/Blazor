using GestaoSpaceEdu.Domain.Interfaces;

namespace GestaoSpaceEdu.Domain.Entities;

public class Document : ISoftDelete
{
    public int Id { get; set; }
    public string Path { get; set; } = null!;  // /wwwroot/files/transactions/1/comprovante.pdf
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public int? FinancialTransactionId { get; set; }
    public FinancialTransaction? FinancialTransaction { get; set; }
}
