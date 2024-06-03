using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class CategoryRepositoryAsync : BaseRepositoryAsync<Category>, ICategoryRepositoryAsync
    {
        public CategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}