using System.Linq;
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

        public bool IsExistPendingApplicationByEmployeeId(int id)
        {
            var vacationApp = _dbSet.FirstOrDefault(va => va.EmployeeId == id && va.Status == VacationApplicationStatus.Pending);

            return vacationApp != null;
        }

        public void UpdateVacationAppStatus(VacationApplication vacationApplication, VacationApplicationStatus status)
        {
            vacationApplication.Status = status;
        }
    }
}
