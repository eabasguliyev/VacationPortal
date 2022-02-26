using System;

namespace VacationPortal.Models
{
    public enum VacatioApplicationStatus
    {
        Pending,
        Approved,
        Declined
    }

    public class VacationApplication
    {
        public int Id { get; set; }
        public VacatioApplicationStatus Status { get; set; }
        public DateTime StartDatetime { get; set; }
        public int DaysOfVacation { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
