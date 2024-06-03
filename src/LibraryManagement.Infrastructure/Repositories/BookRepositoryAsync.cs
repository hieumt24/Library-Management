<<<<<<< HEAD
﻿using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;
=======
﻿using LibraryManagement.Domain.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
>>>>>>> d27a830a6df6256e681481fecb324138e493606f

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepositoryAsync : BaseRepositoryAsync<Book>, IBookRepositoryAsync
    {
        public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
<<<<<<< HEAD
=======

        public async Task<List<Book>> GetBooksByCategoryAsync()
        {
            return await _dbContext.Books.Include(x => x.Category).ToListAsync();
        }
>>>>>>> d27a830a6df6256e681481fecb324138e493606f
    }
}