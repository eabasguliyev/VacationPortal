using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Admin.Models.EmployeeVMs
{
    public class EmployeeUpdateVM
    {
        public EmployeeVM EmployeeVM { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }
    }
}
