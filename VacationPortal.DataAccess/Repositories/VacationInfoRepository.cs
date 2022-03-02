using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories
{
    public class VacationInfoRepository : Repository<VacationInfo>, IVacationInfoRepository
    {
        public VacationInfoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Update(VacationInfo entity)
        {
            _dbSet.Update(entity);
        }
    }
}
