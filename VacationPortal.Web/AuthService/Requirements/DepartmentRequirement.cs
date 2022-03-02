using Microsoft.AspNetCore.Authorization;

namespace VacationPortal.Web.AuthService.Requirements
{
    public class DepartmentRequirement:IAuthorizationRequirement
    {
        public string DepartmentShortName { get; set; }


        public DepartmentRequirement(string departmentShortName)
        {
            DepartmentShortName = departmentShortName;
        }
    }
}
