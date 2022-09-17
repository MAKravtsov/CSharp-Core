using Library.Data.Models;
using Library.Data.Repositories.Books;

namespace Library.Domain.Catalog.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IBooksRepository _booksRepository;

        public CatalogService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<IEnumerable<Book>> GetCatalog()
        {
            return await _booksRepository.Get();
        }
    }
}
