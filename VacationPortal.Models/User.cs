using Microsoft.AspNetCore.Identity;
using System;

namespace VacationPortal.Models
{
    public class User: IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; }
    }
}
