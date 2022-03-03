using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Client.Models.VacationInfoVMs
{
    public class VacationInfoCreateVM
    {
        public VacationInfo VacationInfo { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }
    }
}
