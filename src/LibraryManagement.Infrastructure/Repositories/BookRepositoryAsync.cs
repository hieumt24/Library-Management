using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepositoryAsync : BaseRepositoryAsync<Book>, IBookRepositoryAsync
    {
        public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}