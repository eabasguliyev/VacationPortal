using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace VacationPortal.DataAccess.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeProperties<T>(this IQueryable<T> query, string includeProperties) where T : class
        {
            var properties = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in properties)
            {
                query = query.Include(property);

            }

            return query;
        }
    }
}
