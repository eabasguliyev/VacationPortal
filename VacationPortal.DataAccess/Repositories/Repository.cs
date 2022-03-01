using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Extensions;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IModel
    {
        private readonly ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Find(int id, bool noTracking = false, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet.Where(e => e.Id == id);

            query = includeProperties != null ? query.IncludeProperties(includeProperties) : query;

            query = noTracking ? query.AsNoTracking() : query;

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null, string includeProperties = null)
        {
            IQueryable<T> query = expression != null ? _dbSet.Where(expression) : _dbSet;

            query = includeProperties != null ? query.IncludeProperties(includeProperties) : query;

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression, bool noTracking = false, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet.Where(expression);

            query = includeProperties != null ? query.IncludeProperties(includeProperties) : query;

            query = noTracking ? query.AsNoTracking() : query;

            return query.FirstOrDefault();
        }
        
        public void Remove(T entity)
        {
            entity.ModelStatus = ModelStatus.Deleted;
            //_dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.ModelStatus = ModelStatus.Deleted;
            }
            //_dbSet.RemoveRange(entities);
        }
    }
}
