using FluentValidation;
using System;
using VacationPortal.Models;

namespace VacationPortal.Web.Validations
{
    public class VacationApplicationValidation : AbstractValidator<VacationApplication>
    {
        public VacationApplicationValidation()
        {
            RuleFor(va => va.DaysOfVacation).NotEmpty().GreaterThan(0).WithMessage("Vacation days must be greater than 0");
            RuleFor(va => va.StartDatetime).NotEmpty().GreaterThan(DateTime.Now).WithMessage("Start date must be greater than now.");
        }
    }
}
