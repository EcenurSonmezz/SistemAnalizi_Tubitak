using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.DataAcces.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        //Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        //Task<IEnumerable<T>> FindByIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FindByFirstAsync(Expression<Func<T, bool>> predicate);
        //Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        //IQueryable<T> FindOnly(Expression<Func<T, bool>> predicate);
        T Find(Guid id);
        T FindByInt(int id);
        Task<T> FindAsync(Guid id);
        void Add(T entity);
        Task AddAsync(T entity);

        void Update(T entity);
        void UpdateRange(List<T> entities);
        void Delete(Guid id);
        void Delete(T entity);
        void Remove(T entity);
        void InsertUpdateGraph(T entity);
        void RemoveRange(IEnumerable<T> lstEntities);
        void AddRange(IEnumerable<T> lstEntities);
    }

}
