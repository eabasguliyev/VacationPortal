using System;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Admin.Models.EmployeeVMs
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
