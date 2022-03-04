using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IRepository<T> where T: class, IModel
    {
        T Find(int id, bool noTracking = false, string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> expression, bool noTracking = false, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null, bool noTracking = false, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
