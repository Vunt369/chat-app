using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Interfaces
{
    public interface IGenericRepository <T> where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        IEnumerable<T> Find(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params string[] includes);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entityToUpdate);
        void UpdateRange(List<T> values);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params string[] includes);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entityToUpdate);
        Task UpdateRangeAsync(List<T> values);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<List<TResult>> GetProjectedAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
    }
}
