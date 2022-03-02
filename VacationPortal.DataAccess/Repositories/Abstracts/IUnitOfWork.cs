namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IPositionRepository PositionRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IVacationInfoRepository VacationInfoRepository { get; }

        void Save();
    }
}
