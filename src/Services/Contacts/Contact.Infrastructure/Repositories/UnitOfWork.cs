using Contact.Application.Contracts.Persistence;
using Contact.Domain.Common;
using Contact.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(ContactContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncRepository<T> Repository<T>() where T : EntityBase
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IAsyncRepository<T>;
            }

            IAsyncRepository<T> repo = new RepositoryBase<T>(_dbContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
