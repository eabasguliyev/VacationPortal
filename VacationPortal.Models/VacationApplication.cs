using System;

namespace VacationPortal.Models
{
    public enum VacationApplicationStatus
    {
        Pending,
        Approved,
        Declined
    }

    public class VacationApplication:IModel
    {
        public int Id { get; set; }
        public VacationApplicationStatus Status { get; set; }
        public DateTime StartDatetime { get; set; }
        public int DaysOfVacation { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ModelStatus ModelStatus { get; set; }
    }
}
