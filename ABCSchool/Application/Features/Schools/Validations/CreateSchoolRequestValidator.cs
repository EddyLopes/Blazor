using FluentValidation;

namespace Application.Features.Schools.Validations;

public class CreateSchoolRequestValidator : AbstractValidator<CreateSchoolRequest>
{
    public CreateSchoolRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("School name is required.")
            .MaximumLength(60).WithMessage("School name cannot exceed 60 characters.");

        RuleFor(x => x.EstablishedDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Established date cannot be in the future.");

    }
}
