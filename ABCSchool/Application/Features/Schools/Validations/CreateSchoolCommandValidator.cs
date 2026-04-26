using Application.Features.Schools.Commands;
using FluentValidation;

namespace Application.Features.Schools.Validations;

public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
{
    public CreateSchoolCommandValidator()
    {
        RuleFor(x => x.CreateSchool)
            .SetValidator(new CreateSchoolRequestValidator()); //.NotNull().WithMessage("CreateSchool request cannot be null.");
    }
}
