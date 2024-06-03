using LibraryManagement.Domain.Common.Models;
using LibraryManagement.Domain.Common.Specifications;

namespace LibraryManagement.Application.Common.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<List<T>> ListAllAsync();

        Task<IList<T>> ListAsync(ISpecification<T> spec);

        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(Guid id);

        Task<int> CountAsync(ISpecification<T> spec);
    }
}