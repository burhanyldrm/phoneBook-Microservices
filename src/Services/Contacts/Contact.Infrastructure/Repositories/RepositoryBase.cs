using Contact.Application.Contracts.Persistence;
using Contact.Domain.Common;
using Contact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly ContactContext _dbContext;

        public RepositoryBase(ContactContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual IQueryable<T> Table()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Find(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<T> FindByProperties(Expression<Func<T, bool>> match, string includeProperties = "")
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (match != null)
                query = query.Where(match);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> Filter(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual async Task<ICollection<T>> FilterWithProperties(Expression<Func<T, bool>> filter = null,
           string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return await query.ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual T Update(T entity)
        {
            var entityEntry = _dbContext.Set<T>().Update(entity);
            return entityEntry.Entity;
        }

        public virtual async Task<int> Count()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<int> CountExpression(Expression<Func<T, bool>> predicate, bool isActive = true)
        {
            return await _dbContext.Set<T>().Where(predicate).CountAsync();
        }

        public virtual void BulkUpdate(ICollection<T> entities)
        {
            _dbContext.UpdateRange(entities);
        }

        public virtual async Task BulkInsert(ICollection<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
        }

        public virtual void BulkDelete(ICollection<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

         public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }


    }
}
