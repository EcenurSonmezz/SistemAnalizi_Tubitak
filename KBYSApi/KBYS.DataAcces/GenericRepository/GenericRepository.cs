using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.DataAcces.GenericRepository
{
    public class GenericRepository<T, C> : IGenericRepository<T> where T : class where C : DbContext
    {
        protected readonly C Context;
        internal readonly DbSet<T> DbSet;
        protected IUnitOfWork<C> _uow;
        protected GenericRepository(IUnitOfWork<C> uow
            )
        {
            Context = uow.Context;
            this._uow = uow;
            DbSet = Context.Set<T>();
        }
        public IQueryable<T> All => Context.Set<T>();
        public void Add(T entity)
        {
            Context.Add(entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties);
        }

        //public async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        //{
        //    var query = GetAllIncluding(includeProperties);
        //    IEnumerable<T> results = await query.ToListAsync();
        //    return results;
        //}

        public IQueryable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            return query.Where(predicate);
        }

        public async Task<T> FindByFirstAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        //public async Task<IEnumerable<T>> FindByIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    var query = GetAllIncluding(includeProperties);
        //    IEnumerable<T> results = await query.Where(predicate).ToListAsync();
        //    return results;
        //}

        //public  IQueryable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    var query = GetAllIncluding(includeProperties);
        //    return query.Where(predicate);
        //}

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            return queryable.Where(predicate);
        }

        //public IQueryable<T> FindOnly(Expression<Func<T, bool>> predicate)
        //{
        //    IQueryable<T> queryable = DbSet.AsNoTracking();
        //    return queryable.Where(predicate);
        //}

        //public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        //{
        //    IQueryable<T> queryable = DbSet.AsNoTracking();
        //    IEnumerable<T> results = await queryable.Where(predicate).ToListAsync();
        //    return results;
        //}

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();

            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty));
        }
        public T Find(Guid id)
        {
            return Context.Set<T>().Find(id);
        }

        public T FindByInt(int id)
        {
            return Context.Set<T>().Find(id);
        }
        public async Task<T> FindAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
        }
        public virtual void UpdateRange(List<T> entities)
        {
            Context.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<T> lstEntities)
        {
            Context.Set<T>().RemoveRange(lstEntities);
        }

        public void AddRange(IEnumerable<T> lstEntities)
        {
            Context.Set<T>().AddRange(lstEntities);
        }

        public void InsertUpdateGraph(T entity)
        {
            Context.Set<T>().Add(entity);
            //Context.ApplyStateChanges(user);
        }
        public virtual void Delete(Guid id)
        {
            var entity = Context.Set<T>().Find(id) as BaseEntity;
            if (entity != null)
            {
                entity.IsDeleted = true;
                Context.Update(entity);
            }
        }
        public virtual void Delete(T entityData)
        {
            //var entity = entityData as BaseEntity;
            //entity.IsDeleted = true;
            Context.Update(entityData);
        }
        public virtual void Remove(T entity)
        {
            Context.Remove(entity);
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }
    }

}
