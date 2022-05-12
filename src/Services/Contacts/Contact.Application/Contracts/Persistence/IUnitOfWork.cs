using Contact.Domain.Common;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IAsyncRepository<T> Repository<T>() where T : EntityBase;
        Task<int> CommitAsync();
        void Rollback();
    }
}
