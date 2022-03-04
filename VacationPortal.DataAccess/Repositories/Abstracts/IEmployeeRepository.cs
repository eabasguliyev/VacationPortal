using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee entity);
        int GetPositionIdByEmployeeId(int id);
    } 
}
