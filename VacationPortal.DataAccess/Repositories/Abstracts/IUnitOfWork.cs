namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }

        void Save();
    }
}
