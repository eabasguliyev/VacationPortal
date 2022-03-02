using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IVacationInfoRepository : IRepository<VacationInfo>
    {
        void Update(VacationInfo entity);
    } 
}
