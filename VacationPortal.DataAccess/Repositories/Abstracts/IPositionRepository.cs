using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IPositionRepository : IRepository<Position>
    {
        void Update(Position entity);
    } 
}
