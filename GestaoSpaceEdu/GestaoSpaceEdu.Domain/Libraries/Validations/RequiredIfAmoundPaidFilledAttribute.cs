using GestaoSpaceEdu.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain.Libraries.Validations;

public class RequiredIfAmoundPaidFilledAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        FinancialTransaction transaction = (FinancialTransaction)validationContext.ObjectInstance;

        if (transaction.AmoundPaid.HasValue)
        {
            if (value is null)
                return new ValidationResult("O campo é obrigatório!", new[] { validationContext.MemberName! });
        }

        return ValidationResult.Success;
    }
}
