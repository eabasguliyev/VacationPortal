using FluentValidation;
using VacationPortal.Web.Areas.Admin.Models.EmployeeVMs;

namespace VacationPortal.Web.Validations
{
    public class EmployeeValidation : AbstractValidator<EmployeeVM>
    {
        public EmployeeValidation()
        {
            RuleFor(d => d.FirstName)
                    .NotEmpty().WithMessage("First Name can not be empty")
                    .MaximumLength(128).WithMessage("Max length of first name is 128 characters");
            RuleFor(d => d.LastName)
                    .NotEmpty().WithMessage("Last Name can not be empty")
                    .MaximumLength(128).WithMessage("Max length of last name is 128 characters");
            RuleFor(d => d.Email)
                    .NotEmpty().WithMessage("Email can not be empty")
                    .EmailAddress().WithMessage("Email address is not valid")
                    .MaximumLength(255).WithMessage("Max length of email is 255 characters");
            RuleFor(d => d.Password)
                    .NotEmpty().WithMessage("Password can not be empty")
                    .MinimumLength(6).WithMessage("Minimum length of password is 5 characters");
        }
    }
}
