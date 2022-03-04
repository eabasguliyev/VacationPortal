using FluentValidation;
using VacationPortal.Models;

namespace VacationPortal.Web.Validations
{
    public class PositionValidation : AbstractValidator<Position>
    {
        public PositionValidation()
        {
            RuleFor(p => p.Name)
                    .NotEmpty().WithMessage("Name can not be empty")
                    .MaximumLength(128).WithMessage("Max length of name is 128 characters");
        }
    }
}
