namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IPositionRepository PositionRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }

        void Save();
    }
}
