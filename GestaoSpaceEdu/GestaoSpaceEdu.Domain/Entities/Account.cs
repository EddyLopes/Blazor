using GestaoSpaceEdu.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain.Entities;

public class Account : ISoftDelete
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo 'Descrição/Nome' é obrigatório!")]
    [MinLength(3, ErrorMessage = "O campo 'Descrição/Nome' deve ter pelo menos {1} caracteres!")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Saldo inicial' é obrigatório!")]
    public decimal Balance { get; set; }


    public DateTimeOffset BalanceDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }

    //public ICollection<FinancialTransaction>? FinancialTransactions { get; set; }
}
