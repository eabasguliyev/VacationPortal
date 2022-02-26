using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IDepartmentRepository: IRepository<Department>
    {
        void Update(Department entity);
    } 
}
