using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain;

public class Company
{
    public int Id { get; set; }
    
    [Required]
    public string LegalName { get; set; } = string.Empty;

    [Required]
    public string TradeName { get; set; } = string.Empty;

    [Required]
    public string TaxId { get; set; } = string.Empty;

    [Required]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Neighborhood { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string Complement { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
