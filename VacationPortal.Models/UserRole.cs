using Microsoft.AspNetCore.Identity;

namespace VacationPortal.Models
{
    public class UserRole: IdentityRole<int>, IModel
    {
        public ModelStatus ModelStatus { get; set; }
    }
}
