using Contact.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task Add(T entity);
        void BulkDelete(ICollection<T> entities);
        Task BulkInsert(ICollection<T> entities);
        void BulkUpdate(ICollection<T> entities);
        Task<int> Count();
        Task<int> CountExpression(Expression<Func<T, bool>> predicate, bool isActive = true);
        Task<ICollection<T>> Filter(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FilterWithProperties(Expression<Func<T, bool>> filter = null,
           string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           int? page = null, int? pageSize = null);
        Task<T> Find(Expression<Func<T, bool>> match);
        Task<T> FindByProperties(Expression<Func<T, bool>> match, string includeProperties = "");
        Task<ICollection<T>> GetAll();
        Task<T> GetById(Guid id);
        IQueryable<T> Table();
        T Update(T entity);
        void Delete(T entity);
    }
}
