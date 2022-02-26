﻿using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories.Abstracts;

namespace VacationPortal.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            DepartmentRepository = new DepartmentRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}