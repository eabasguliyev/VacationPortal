using Microsoft.EntityFrameworkCore;
using System.Linq;
using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Update(Position entity)
        {
            _dbSet.Update(entity);
        }
    }
}
