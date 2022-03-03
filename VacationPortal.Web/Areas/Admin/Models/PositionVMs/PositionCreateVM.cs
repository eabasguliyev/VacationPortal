using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Admin.Models.PositionVMs
{
    public class PositionCreateVM
    {
        public Position Position { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}
