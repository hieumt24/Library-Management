using LibraryManagement.Domain.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepositoryAsync : BaseRepositoryAsync<Book>, IBookRepositoryAsync
    {
        public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Book>> GetBooksByCategoryAsync()
        {
            return await _dbContext.Books.Include(x => x.Category).ToListAsync();
        }
    }
}