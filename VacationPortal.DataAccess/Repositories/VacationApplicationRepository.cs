using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories
{
    public class VacationApplicationRepository : Repository<VacationApplication>, IVacationApplicationRepository
    {
        public VacationApplicationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
