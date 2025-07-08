using GestaoSpaceEdu.Domain.Interfaces;

namespace GestaoSpaceEdu.Domain.Entities;

public class Account : ISoftDelete
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime BalanceDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
}
