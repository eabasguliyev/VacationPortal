using System.Linq;
using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public int GetPositionIdByEmployeeId(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id).PositionId.Value;
        }

        public void Update(Employee entity)
        {
            _dbSet.Update(entity);
        }
    }
}
