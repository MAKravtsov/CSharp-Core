using Library.Data.Infrastructure.Contexts;
using Library.Data.Infrastructure.Repository;
using Library.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Books
{
    public class BooksRepository : BaseRepository<LibraryContext>, IBooksRepository
    {
        public BooksRepository(LibraryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await Context.Books.Include(y => y.Genre).ToListAsync();
        }
    }
}
