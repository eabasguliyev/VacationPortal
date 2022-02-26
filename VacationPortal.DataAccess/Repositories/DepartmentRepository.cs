using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Update(Department entity)
        {
            _dbSet.Update(entity);
        }
    }
}
