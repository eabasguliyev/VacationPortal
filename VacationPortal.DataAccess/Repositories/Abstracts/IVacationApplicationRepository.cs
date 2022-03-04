using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IVacationApplicationRepository : IRepository<VacationApplication>
    {
        void UpdateVacationAppStatus(VacationApplication vacationApplication, VacationApplicationStatus status);
        bool IsExistPendingApplicationByEmployeeId(int id);
    } 
}
