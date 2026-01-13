using System.ComponentModel.DataAnnotations;

namespace GestaoSpaceEdu.Domain.Enums;

public enum Recurrence
{
    [Display(Name = "Não")]
    None,

    [Display(Name = "Semanalmente")]
    Weekly,

    [Display(Name = "Mensalmente")]
    Monthly,

    [Display(Name = "Anualmente")]
    Yearly
}
