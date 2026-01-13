using GestaoSpaceEdu.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain.Libraries.Validations;

public class DiscountNotBeGreaterThanAmountAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        FinancialTransaction transaction = (FinancialTransaction)validationContext.ObjectInstance;

        if (transaction.Amount.HasValue && value is not null)
        {
            decimal discount = (decimal)value;
            if (discount > transaction.Amount)
            {
                return new ValidationResult("O desconto é maior que valor da conta!", new[] { validationContext.MemberName! });
            }
        }
        return ValidationResult.Success;
    }
}
