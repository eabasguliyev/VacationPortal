using System;
using System.ComponentModel.DataAnnotations;

namespace VacationPortal.Models
{
    public class VacationInfo:IModel
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public ModelStatus ModelStatus { get; set; }
    }
}
