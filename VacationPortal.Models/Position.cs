using System;
using System.Collections.Generic;

namespace VacationPortal.Models
{
    public class Position:IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ModelStatus ModelStatus { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<VacationInfo> VacationInfos { get; set; }
    }
}
