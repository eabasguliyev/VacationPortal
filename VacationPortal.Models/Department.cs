using System;
using System.Collections.Generic;

namespace VacationPortal.Models
{
    public class Department:IModel
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ModelStatus ModelStatus { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
