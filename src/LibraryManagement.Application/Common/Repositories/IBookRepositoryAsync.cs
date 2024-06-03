using LibraryManagement.Domain.Entities;

<<<<<<< HEAD:src/LibraryManagement.Application/Common/Repositories/IBookRepositoryAsync.cs
namespace LibraryManagement.Application.Common.Repositories
{
    public interface IBookRepositoryAsync : IBaseRepositoryAsync<Book>
    {
=======
namespace LibraryManagement.Domain.Common.Repositories
{
    public interface IBookRepositoryAsync : IBaseRepositoryAsync<Book>
    {
        Task<List<Book>> GetBooksByCategoryAsync();
>>>>>>> d27a830a6df6256e681481fecb324138e493606f:src/LibraryManagement.Domain/Common/Repositories/IBookRepositoryAsync.cs
    }
}