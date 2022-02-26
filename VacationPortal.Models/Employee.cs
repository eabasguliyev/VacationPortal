using System.Collections.Generic;

namespace VacationPortal.Models
{
    public class Employee:User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public ICollection<VacationApplication> VacationApplications { get; set; }
    }
}
