using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
