using FluentValidation;
using VacationPortal.Models;

namespace VacationPortal.Web.Validations
{
    public class DepartmentValidation: AbstractValidator<Department>
    {
        public DepartmentValidation()
        {
            RuleFor(d => d.ShortName)
                    .NotEmpty().WithMessage("Short Name can not be empty")
                    .MaximumLength(128).WithMessage("Max length of short name is 128 characters");
            RuleFor(d => d.FullName)
                    .NotEmpty().WithMessage("Full Name can not be empty")
                    .MaximumLength(128).WithMessage("Max length of full name is 128 characters");
            RuleFor(d => d.Description)
                    .MaximumLength(255).WithMessage("Max length of description is 255 characters");

        }
    }
}
