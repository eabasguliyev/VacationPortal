using Microsoft.AspNetCore.Identity;

namespace VacationPortal.Models
{
    public class UserRole: IdentityRole<int>
    {
        public ModelStatus ModelStatus { get; set; }
    }
}
