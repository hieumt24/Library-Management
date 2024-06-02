using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Common.Repositories
{
    public interface IBookRepositoryAsync : IBaseRepositoryAsync<Book>
    {
        Task<List<Book>> GetBooksByCategoryAsync();
    }
}