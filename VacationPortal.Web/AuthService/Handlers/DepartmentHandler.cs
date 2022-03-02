using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Web.AuthService.Requirements;

namespace VacationPortal.Web.AuthService.Handlers
{
    public class DepartmentHandler : AuthorizationHandler<DepartmentRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DepartmentRequirement requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if(claim.Value != "0")
            {
                var employee = _unitOfWork.EmployeeRepository.Find(int.Parse(claim.Value), noTracking: true, includeProperties: "Department");

                if(employee.Department.ShortName == requirement.DepartmentShortName)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
