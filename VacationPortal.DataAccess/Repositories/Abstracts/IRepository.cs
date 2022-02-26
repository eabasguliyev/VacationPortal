using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VacationPortal.DataAccess.Repositories.Abstracts
{
    public interface IRepository<T> where T: class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> expression, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        void Add(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
