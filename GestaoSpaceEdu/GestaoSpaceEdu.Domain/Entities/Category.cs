﻿using GestaoSpaceEdu.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain.Entities;

public class Category : ISoftDelete
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo 'Nome' é obrigatório!")]
    [MinLength(3, ErrorMessage = "O campo 'Nome' deve ter pelo menos {1} caracteres!")]
    public string Name { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
}
