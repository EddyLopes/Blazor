using GestaoSpaceEdu.Domain.Interfaces;

namespace GestaoSpaceEdu.Domain.Entities;

public class Category : ISoftDelete
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
}
