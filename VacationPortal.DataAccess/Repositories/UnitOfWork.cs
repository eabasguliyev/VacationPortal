using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;

namespace VacationPortal.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IPositionRepository PositionRepository { get; private set; }

        public IEmployeeRepository EmployeeRepository { get; private set; }

        public IVacationInfoRepository VacationInfoRepository { get; private set; }
        public IVacationApplicationRepository VacationApplicationRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            DepartmentRepository = new DepartmentRepository(_dbContext);
            PositionRepository = new PositionRepository(_dbContext);
            EmployeeRepository = new EmployeeRepository(_dbContext);
            VacationInfoRepository = new VacationInfoRepository(_dbContext);
            VacationApplicationRepository = new VacationApplicationRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
