namespace GestaoSpaceEdu.Domain.Interfaces;

public interface ISoftDelete
{
    DateTimeOffset? DeletedAt { get; set; }
}
