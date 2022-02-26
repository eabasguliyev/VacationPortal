﻿using System;

namespace VacationPortal.Models
{
    public class VacationInfo
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
