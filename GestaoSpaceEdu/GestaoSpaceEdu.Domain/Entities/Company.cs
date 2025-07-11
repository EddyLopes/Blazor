﻿using GestaoSpaceEdu.Domain.Interfaces;
using GestaoSpaceEdu.Domain.Libraries.Validations;
using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain.Entities;

public class Company : ISoftDelete
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Razão Social' é obrigatório!")]
    [MinLength(3, ErrorMessage = "O campo 'Razão Social' deve ter pelo menos {1} caracteres!")]
    public string LegalName { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Nome Fantasia' é obrigatório!")]
    [MinLength(3, ErrorMessage = "O campo 'Nome Fantasia' deve ter pelo menos {1} caracteres!")]
    public string TradeName { get; set; } = string.Empty;

    [CNPJ(ErrorMessage = "O campo 'CNPJ' é inválido!")]
    [Required(ErrorMessage = "O campo 'CNPJ' é obrigatório!")]
    public string TaxId { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'CEP' é obrigatório!")]
    [MinLength(10, ErrorMessage = "O campo 'CEP' deve ter {1} caracteres!")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Estado' é obrigatório!")]
    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Cidade' é obrigatório!")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Bairro' é obrigatório!")]
    public string Neighborhood { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Endereço' é obrigatório!")]
    [MinLength(10, ErrorMessage = "O campo 'Endereço' deve ter pelo menos {1} caracteres!")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo 'Complemento' é obrigatório!")]
    [MinLength(3, ErrorMessage = "O campo 'Complemento' deve ter pelo menos {1} caracteres!")]
    public string Complement { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public ICollection<Account>? Accounts { get; set; }
    public ICollection<Category>? Categories { get; set; }
    public ICollection<FinancialTransaction>? FinancialTransactions { get; set; }
}
