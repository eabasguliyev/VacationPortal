using Microsoft.AspNetCore.Identity;
using System;

namespace VacationPortal.Models
{
    public class User: IdentityUser<int>, IModel
    {
        public DateTime CreatedDate { get; set; }
        public ModelStatus ModelStatus { get; set; }
    }
}
