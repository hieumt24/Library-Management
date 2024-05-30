using LibraryManagement.Domain.Common.Models;

namespace LibraryManagement.Domain.Common.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        Task RollBackChangesAsync();

        IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
    }
}